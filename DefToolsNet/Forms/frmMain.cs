using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DefToolsNet.Models;

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
            ICollection<LootAward> awards = parser.ParseImportText(this.txtboxData.Text);
            this.pbImport.Style = ProgressBarStyle.Blocks;
            return;
        }
    }
}
