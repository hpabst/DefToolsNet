namespace DefToolsNet
{
    partial class FrmMain
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
            this.tbCtrlMain = new System.Windows.Forms.TabControl();
            this.tabDataImport = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbRclcExportType = new System.Windows.Forms.ComboBox();
            this.lblRclcExportType = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lblRclcExportedTxt = new System.Windows.Forms.Label();
            this.btnImportData = new System.Windows.Forms.Button();
            this.tabDBManage = new System.Windows.Forms.TabPage();
            this.tabPlayerManagement = new System.Windows.Forms.TabPage();
            this.pbImport = new System.Windows.Forms.ProgressBar();
            this.tbCtrlMain.SuspendLayout();
            this.tabDataImport.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbCtrlMain
            // 
            this.tbCtrlMain.Controls.Add(this.tabDataImport);
            this.tbCtrlMain.Controls.Add(this.tabDBManage);
            this.tbCtrlMain.Controls.Add(this.tabPlayerManagement);
            this.tbCtrlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbCtrlMain.Location = new System.Drawing.Point(0, 0);
            this.tbCtrlMain.Name = "tbCtrlMain";
            this.tbCtrlMain.SelectedIndex = 0;
            this.tbCtrlMain.Size = new System.Drawing.Size(812, 529);
            this.tbCtrlMain.TabIndex = 0;
            // 
            // tabDataImport
            // 
            this.tabDataImport.Controls.Add(this.tableLayoutPanel1);
            this.tabDataImport.Location = new System.Drawing.Point(4, 22);
            this.tabDataImport.Name = "tabDataImport";
            this.tabDataImport.Padding = new System.Windows.Forms.Padding(3);
            this.tabDataImport.Size = new System.Drawing.Size(804, 503);
            this.tabDataImport.TabIndex = 0;
            this.tabDataImport.Text = "Data Import";
            this.tabDataImport.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 131F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnImportData, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.pbImport, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 53F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 400F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(798, 497);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbRclcExportType);
            this.panel1.Controls.Add(this.lblRclcExportType);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(661, 47);
            this.panel1.TabIndex = 0;
            // 
            // cbRclcExportType
            // 
            this.cbRclcExportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRclcExportType.FormattingEnabled = true;
            this.cbRclcExportType.Items.AddRange(new object[] {
            "TSV (Excel)"});
            this.cbRclcExportType.Location = new System.Drawing.Point(4, 21);
            this.cbRclcExportType.Name = "cbRclcExportType";
            this.cbRclcExportType.Size = new System.Drawing.Size(169, 21);
            this.cbRclcExportType.TabIndex = 1;
            // 
            // lblRclcExportType
            // 
            this.lblRclcExportType.AutoSize = true;
            this.lblRclcExportType.Location = new System.Drawing.Point(4, 4);
            this.lblRclcExportType.Name = "lblRclcExportType";
            this.lblRclcExportType.Size = new System.Drawing.Size(169, 13);
            this.lblRclcExportType.TabIndex = 0;
            this.lblRclcExportType.Text = "Enter RCLootCouncil Export Type:";
            // 
            // panel2
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel2, 2);
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Controls.Add(this.lblRclcExportedTxt);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 56);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(792, 394);
            this.panel2.TabIndex = 1;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(4, 20);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(785, 371);
            this.textBox1.TabIndex = 1;
            // 
            // lblRclcExportedTxt
            // 
            this.lblRclcExportedTxt.AutoSize = true;
            this.lblRclcExportedTxt.Location = new System.Drawing.Point(4, 4);
            this.lblRclcExportedTxt.Name = "lblRclcExportedTxt";
            this.lblRclcExportedTxt.Size = new System.Drawing.Size(130, 13);
            this.lblRclcExportedTxt.TabIndex = 0;
            this.lblRclcExportedTxt.Text = "Enter Exported Text Data:";
            // 
            // btnImportData
            // 
            this.btnImportData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImportData.Location = new System.Drawing.Point(720, 471);
            this.btnImportData.Name = "btnImportData";
            this.btnImportData.Size = new System.Drawing.Size(75, 23);
            this.btnImportData.TabIndex = 2;
            this.btnImportData.Text = "Import Data";
            this.btnImportData.UseVisualStyleBackColor = true;
            // 
            // tabDBManage
            // 
            this.tabDBManage.Location = new System.Drawing.Point(4, 22);
            this.tabDBManage.Name = "tabDBManage";
            this.tabDBManage.Padding = new System.Windows.Forms.Padding(3);
            this.tabDBManage.Size = new System.Drawing.Size(801, 503);
            this.tabDBManage.TabIndex = 1;
            this.tabDBManage.Text = "DB Management";
            this.tabDBManage.UseVisualStyleBackColor = true;
            // 
            // tabPlayerManagement
            // 
            this.tabPlayerManagement.Location = new System.Drawing.Point(4, 22);
            this.tabPlayerManagement.Name = "tabPlayerManagement";
            this.tabPlayerManagement.Padding = new System.Windows.Forms.Padding(3);
            this.tabPlayerManagement.Size = new System.Drawing.Size(801, 503);
            this.tabPlayerManagement.TabIndex = 2;
            this.tabPlayerManagement.Text = "PlayerManagement";
            this.tabPlayerManagement.UseVisualStyleBackColor = true;
            // 
            // pbImport
            // 
            this.pbImport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbImport.Location = new System.Drawing.Point(59, 470);
            this.pbImport.Name = "pbImport";
            this.pbImport.Size = new System.Drawing.Size(100, 23);
            this.pbImport.TabIndex = 3;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(812, 529);
            this.Controls.Add(this.tbCtrlMain);
            this.MinimumSize = new System.Drawing.Size(828, 568);
            this.Name = "FrmMain";
            this.Text = "DefTools";
            this.tbCtrlMain.ResumeLayout(false);
            this.tabDataImport.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tbCtrlMain;
        private System.Windows.Forms.TabPage tabDataImport;
        private System.Windows.Forms.TabPage tabDBManage;
        private System.Windows.Forms.TabPage tabPlayerManagement;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbRclcExportType;
        private System.Windows.Forms.Label lblRclcExportType;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblRclcExportedTxt;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnImportData;
        private System.Windows.Forms.ProgressBar pbImport;
    }
}

