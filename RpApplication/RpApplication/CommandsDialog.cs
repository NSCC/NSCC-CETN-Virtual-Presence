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
    public partial class CommandsDialog : Form
    {
        
        public CommandsDialog()
        {
            InitializeComponent();
            try
            {
                rtb_commands.LoadFile(".\\resources\\commands.rtf");
            }
            catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine("File could not be opened: " + ex.Message);
                rtb_commands.Font = new Font("Microsoft Sans Serif", 11);
                rtb_commands.Text = "The file 'commands.rtf' could not be found or is in use by another process.";
            }
           

        }

        private void Btn_ok_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
