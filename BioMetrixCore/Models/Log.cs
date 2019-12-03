using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BioMetrixCore.Models
{
    public class Log
    {
        public int Id { get; set; }
        public int MachineNumber { get; set; }
        public int IndRegID { get; set; }

        public StatusEnum Status { get; set; }
        public DateTime DateTimeRecord { get; set; }
        [Column(TypeName = "Date")]
        public DateTime DateOnlyRecord { get; set; }
        public string TimeOnlyRecord { get; set; }

    }
}
