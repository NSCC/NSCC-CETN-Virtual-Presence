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
    public partial class ConnectDialog : Form
    {

        public String IpAddress { get; set; }
        public ConnectDialog()
        {
            InitializeComponent();
            tb_ipAddress.Text = "192.168.137.7";
        }
        

        private void btn_connect_Click(object sender, EventArgs e)
        {
            IpAddress = tb_ipAddress.Text;
        }
    }
}
