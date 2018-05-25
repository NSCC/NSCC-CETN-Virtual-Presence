using RpApplication.Properties;
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
        /// <summary>
        /// The ip address of the robot.
        /// </summary>
        public String IpAddress { get; set; }
        


        /// <summary>
        /// Creates an instance of the connect dialog.
        /// </summary>
        public ConnectDialog()
        {
            InitializeComponent();
            tb_ipAddress.Text = "192.168.137.161";
        }
        
        /// <summary>
        /// Handles clicking the connect button and sets the ip address to the value the user entered.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_connect_Click(object sender, EventArgs e)
        {
            IpAddress = tb_ipAddress.Text;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void ConnectDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.Default.Save();
        }

        private void ConnectDialog_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_connect.PerformClick();
            }
        }
    }
}
