using BioMetrixCore.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace BioMetrixCore
{
    public partial class DataInsert : Form
    {
        DeviceManipulator manipulator = new DeviceManipulator();
        public ZkemClient objZkeeper;

        List<LogsHoursDisplay> logsHours;

        public List<Machine> machines = new List<Machine>
        {
            new Machine {IP="10.50.145.74",Port="4370",Number=1,Type=StatusEnum.Entry},
            new Machine {IP="10.50.145.73",Port="4370",Number=2,Type=StatusEnum.Exit},
            new Machine {IP="10.50.145.77",Port="4370",Number=3,Type=StatusEnum.Entry},
            new Machine {IP="10.50.145.78",Port="4370",Number=4,Type=StatusEnum.Entry}
        };


        public DataInsert()
        {
            InitializeComponent();
            //DateTime date = Convert.ToDateTime("2019-12-03 05:27:37.033");
            //string timeonly = Convert.ToDateTime("2019-12-03 05:27:37.033").ToShortTimeString();
            //using (FingerPrintDB db = new FingerPrintDB())
            //{

            //    if (db.Logs.Any(f => f.IndRegID == 1 && f.DateOnlyRecord == date.Date && f.TimeOnlyRecord == timeonly) != true)
            //    {
            //        db.Logs.Add(new Log
            //        {
            //            IndRegID = 1,
            //            MachineNumber = 1,
            //            DateTimeRecord = date,
            //            DateOnlyRecord = date.Date,
            //            TimeOnlyRecord = date.ToShortTimeString(),
            //            Status = StatusEnum.Entry
            //        });
            //        db.SaveChanges();
            //    }
            //}
        }

        private void RaiseDeviceEvent(object sender, string actionType)
        {
            switch (actionType)
            {
                case UniversalStatic.acx_Disconnect:
                    {
                        break;
                    }

                default:
                    break;
            }

        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                using (FingerPrintDB db = new FingerPrintDB())
                {
                    foreach (var machine in machines)
                    {
                        string ipAddress = machine.IP;
                        string port = machine.Port;
                        if (ipAddress == string.Empty || port == string.Empty)
                            throw new Exception("The Device IP Address and Port is mandotory !!");

                        int portNumber = 4370;
                        if (!int.TryParse(port, out portNumber))
                            throw new Exception("Not a valid port number");

                        bool isValidIpA = UniversalStatic.ValidateIP(ipAddress);
                        if (!isValidIpA)
                            throw new Exception("The Device IP is invalid !!");

                        isValidIpA = UniversalStatic.PingTheDevice(ipAddress);
                        if (!isValidIpA)
                            throw new Exception("The device at " + ipAddress + ":" + port + " did not respond!!");

                        objZkeeper = new ZkemClient(RaiseDeviceEvent);
                        objZkeeper.Connect_Net(ipAddress, portNumber);

                        ICollection<MachineInfo> lstMachineInfo = manipulator.GetLogData(objZkeeper, machine.Number);

                        foreach (var log in lstMachineInfo)
                        {
                            string timeOnly = Convert.ToDateTime(log.DateTimeRecord).ToShortTimeString();
                            if (db.Logs.Any(f => f.IndRegID == log.IndRegID && f.DateOnlyRecord == log.DateOnlyRecord && f.TimeOnlyRecord == timeOnly) != true)
                            {
                                db.Logs.Add(new Log
                                {
                                    Status = machine.Type,
                                    MachineNumber = machine.Number,
                                    IndRegID = log.IndRegID,
                                    DateTimeRecord = Convert.ToDateTime(log.DateTimeRecord),
                                    DateOnlyRecord = log.DateOnlyRecord,
                                    TimeOnlyRecord = Convert.ToDateTime(log.DateTimeRecord).ToShortTimeString()
                                });
                                db.SaveChanges();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnGetLogs_Click(object sender, EventArgs e)
        {
            try
            {
                using (FingerPrintDB db = new FingerPrintDB())
                {
                    SqlParameter dtFrom = new SqlParameter("@dtFrom", dtpFrom.Value.Date);
                    SqlParameter dtTo = new SqlParameter("@dtTo", dtpTo.Value.Date);
                    var logs = db.Database.SqlQuery<LogsHours>("GetHours @dtFrom,@dtTo", dtFrom, dtTo).ToList();
                    logsHours = logs.Select(s => new LogsHoursDisplay
                    {
                        IndRegID = s.IndRegID,
                        Date = s.Date,
                        Hours = (s.Minutes / 60).ToString("D2") + ":" + (s.Minutes % 60).ToString("D2")
                    }).ToList();
                    dgvLogs.DataSource = logsHours;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnManageMachines_Click(object sender, EventArgs e)
        {
            new Master().ShowDialog();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvLogs.Rows.Count == 0)
                    return;
                SaveFileDialog dlg = new SaveFileDialog
                {
                    FileName = "الساعات المسجله",
                    DefaultExt = ".xls",
                    Filter = "Text documents (.xls)|*.xls"
                };
                if (dlg.ShowDialog() != DialogResult.OK)
                    return;
                int i = 2;
                Excel.Application xlApp;
                Excel.Workbook xlWorkBook;
                Excel.Worksheet xlWorkSheet;
                object misValue = System.Reflection.Missing.Value;

                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Add(misValue);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                xlWorkSheet.Cells[1, 1] = "IndRegID";
                xlWorkSheet.Cells[1, 2] = "Date";
                xlWorkSheet.Cells[1, 3] = "Hours";

                foreach (var item in logsHours)
                {
                    xlWorkSheet.Cells[i, 1] = item.IndRegID;
                    xlWorkSheet.Cells[i, 2] = item.Date;
                    xlWorkSheet.Cells[i, 3] = item.Hours;
                    i++;
                }
                xlWorkBook.SaveAs(dlg.FileName, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();
                ReleaseObject(xlWorkSheet);
                ReleaseObject(xlWorkBook);
                ReleaseObject(xlApp);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        private void btnGetAllData_Click(object sender, EventArgs e)
        {
            using (FingerPrintDB db = new FingerPrintDB())
            {
                dgvLogs.DataSource = db.Logs.Where(w => w.DateOnlyRecord >= dtpFrom.Value.Date && w.DateOnlyRecord <= dtpTo.Value.Date).ToList();
                dgvLogs.Columns["Id"].Visible = false;
            }

        }

        private void btnExportAllData_Click(object sender, EventArgs e)
        {
            try
            {
                using (FingerPrintDB db = new FingerPrintDB())
                {
                    var allData = db.Logs.Where(w => w.DateOnlyRecord >= dtpFrom.Value.Date && w.DateOnlyRecord <= dtpTo.Value.Date).ToList();

                    if (dgvLogs.Rows.Count == 0)
                        return;
                    SaveFileDialog dlg = new SaveFileDialog
                    {
                        FileName = "جميع البيانات",
                        DefaultExt = ".xls",
                        Filter = "Text documents (.xls)|*.xls"
                    };
                    if (dlg.ShowDialog() != DialogResult.OK)
                        return;
                    int i = 2;
                    Excel.Application xlApp;
                    Excel.Workbook xlWorkBook;
                    Excel.Worksheet xlWorkSheet;
                    object misValue = System.Reflection.Missing.Value;

                    xlApp = new Excel.Application();
                    xlWorkBook = xlApp.Workbooks.Add(misValue);
                    xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                    xlWorkSheet.Cells[1, 1] = "MachineNumber";
                    xlWorkSheet.Cells[1, 2] = "Status";
                    xlWorkSheet.Cells[1, 3] = "IndRegID";
                    xlWorkSheet.Cells[1, 4] = "DateTimeRecord";
                    xlWorkSheet.Cells[1, 5] = "DateOnlyRecord";
                    xlWorkSheet.Cells[1, 6] = "TimeOnlyRecord";

                    foreach (var item in allData)
                    {
                        xlWorkSheet.Cells[i, 1] = item.MachineNumber;
                        xlWorkSheet.Cells[i, 2] = item.Status;
                        xlWorkSheet.Cells[i, 3] = item.IndRegID;
                        xlWorkSheet.Cells[i, 4] = item.DateTimeRecord;
                        xlWorkSheet.Cells[i, 5] = item.DateOnlyRecord;
                        xlWorkSheet.Cells[i, 6] = item.TimeOnlyRecord;
                        i++;
                    }
                    xlWorkBook.SaveAs(dlg.FileName, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                    xlWorkBook.Close(true, misValue, misValue);
                    xlApp.Quit();
                    ReleaseObject(xlWorkSheet);
                    ReleaseObject(xlWorkBook);
                    ReleaseObject(xlApp);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

    public class Machine
    {
        public string IP { get; set; }
        public string Port { get; set; }
        public int Number { get; set; }
        public StatusEnum Type { get; set; }
    }

    public class LogsHours
    {
        public int IndRegID { get; set; }
        public DateTime Date { get; set; }
        public int Minutes { get; set; }
    }

    public class LogsHoursDisplay
    {
        public int IndRegID { get; set; }
        public DateTime Date { get; set; }
        public string Hours { get; set; }
    }
}
