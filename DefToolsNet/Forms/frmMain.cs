using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DefToolsNet.DB;
using DefToolsNet.Models;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using DefToolsNet.Sheets;

namespace DefToolsNet
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void SubmitText(object sender, EventArgs e)
        {
            string format = this.cbRclcExportType.Text;
            RcParser parser;
            if (format == "TSV (Excel)")
            {
                parser = new TsvParser();
            }
            else
            {
                MessageBox.Show("Please select an export text type.", "!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.pbImport.Style = ProgressBarStyle.Marquee;
            this.btnImportData.Enabled = false;
            Thread thread = new Thread(() => ParseAndSubmitText(parser, this.txtboxData.Text));
            thread.Start();
            return;
        }

        private void ParseAndSubmitText(RcParser parser, string importText)
        {

            ICollection<LootAward> awards = parser.ParseImportText(importText);
            DbControl ctrl = new DbControl(DbControl.DBNAME_DEFAULT);
            foreach (var a in awards)
            {
                ctrl.AddLootAward(a);
            }
            SetControlPropertyThreadSafe(pbImport, "Style", ProgressBarStyle.Blocks);
            SetControlPropertyThreadSafe(btnImportData, "Enabled", true);
            SetControlPropertyThreadSafe(txtboxData, "Text", "");
            return;
        }



        private delegate void SetControlPropertyThreadSafeDelegate(
            Control control,
            string propertyName,
            object propertyValue);

        /// <summary>
        /// Sets GUI control properties from different thread.
        /// Taken from https://stackoverflow.com/questions/661561/how-to-update-the-gui-from-another-thread-in-c
        /// </summary>
        /// <param name="control"></param>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        public static void SetControlPropertyThreadSafe(
            Control control,
            string propertyName,
            object propertyValue)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new SetControlPropertyThreadSafeDelegate
                        (SetControlPropertyThreadSafe),
                    new object[] { control, propertyName, propertyValue });
            }
            else
            {
                control.GetType().InvokeMember(
                    propertyName,
                    BindingFlags.SetProperty,
                    null,
                    control,
                    new object[] { propertyValue });
            }
        }

        private void btnUploadToGoogle_Click(object sender, EventArgs e)
        {
            String idRegex = "/spreadsheets/d/([a-zA-Z0-9-_]+)";
            Match match = Regex.Match(this.textBox1.Text, idRegex);
            if (!match.Success)
            {
                return;
            }
            String id = match.Groups[1].Value;
            GSheets g = new GSheets(id, new DbControl(DbControl.DBNAME_DEFAULT));
            g.UpdateGoogleSheet();
        }
    }
}
