using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RpApplication
{
    public partial class AboutDialog : Form
    {
        public AboutDialog()
        {
            InitializeComponent();
            tb_about.Select(0, 0);
        }

        private void Btn_ok_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
