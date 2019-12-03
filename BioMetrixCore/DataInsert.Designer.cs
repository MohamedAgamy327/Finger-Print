namespace BioMetrixCore
{
    partial class DataInsert
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnInsert = new System.Windows.Forms.Button();
            this.btnGetLogs = new System.Windows.Forms.Button();
            this.dgvLogs = new System.Windows.Forms.DataGridView();
            this.btnManageMachines = new System.Windows.Forms.Button();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.btnExport = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogs)).BeginInit();
            this.SuspendLayout();
            // 
            // btnInsert
            // 
            this.btnInsert.Location = new System.Drawing.Point(13, 65);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(175, 36);
            this.btnInsert.TabIndex = 0;
            this.btnInsert.Text = "Insert New Data";
            this.btnInsert.UseVisualStyleBackColor = true;
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // btnGetLogs
            // 
            this.btnGetLogs.Location = new System.Drawing.Point(292, 42);
            this.btnGetLogs.Name = "btnGetLogs";
            this.btnGetLogs.Size = new System.Drawing.Size(171, 36);
            this.btnGetLogs.TabIndex = 1;
            this.btnGetLogs.Text = "Get Logs";
            this.btnGetLogs.UseVisualStyleBackColor = true;
            this.btnGetLogs.Click += new System.EventHandler(this.btnGetLogs_Click);
            // 
            // dgvLogs
            // 
            this.dgvLogs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLogs.Location = new System.Drawing.Point(13, 142);
            this.dgvLogs.Name = "dgvLogs";
            this.dgvLogs.RowHeadersWidth = 51;
            this.dgvLogs.RowTemplate.Height = 26;
            this.dgvLogs.Size = new System.Drawing.Size(775, 219);
            this.dgvLogs.TabIndex = 2;
            // 
            // btnManageMachines
            // 
            this.btnManageMachines.Location = new System.Drawing.Point(12, 12);
            this.btnManageMachines.Name = "btnManageMachines";
            this.btnManageMachines.Size = new System.Drawing.Size(171, 36);
            this.btnManageMachines.TabIndex = 3;
            this.btnManageMachines.Text = "Manage Machines";
            this.btnManageMachines.UseVisualStyleBackColor = true;
            this.btnManageMachines.Click += new System.EventHandler(this.btnManageMachines_Click);
            // 
            // dtpFrom
            // 
            this.dtpFrom.Location = new System.Drawing.Point(572, 24);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(200, 24);
            this.dtpFrom.TabIndex = 4;
            // 
            // dtpTo
            // 
            this.dtpTo.Location = new System.Drawing.Point(572, 54);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(200, 24);
            this.dtpTo.TabIndex = 5;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(276, 385);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(171, 36);
            this.btnExport.TabIndex = 6;
            this.btnExport.Text = "Export To Excel";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // DataInsert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.btnManageMachines);
            this.Controls.Add(this.dgvLogs);
            this.Controls.Add(this.btnGetLogs);
            this.Controls.Add(this.btnInsert);
            this.Name = "DataInsert";
            this.Text = "DataInsert";
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogs)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.Button btnGetLogs;
        private System.Windows.Forms.DataGridView dgvLogs;
        private System.Windows.Forms.Button btnManageMachines;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Button btnExport;
    }
}