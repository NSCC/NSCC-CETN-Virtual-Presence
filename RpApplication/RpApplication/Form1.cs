using RPClientLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RpApplication
{
    public partial class Form1 : Form
    {
        RPClient client = null;
        String ipAddress = "192.168.0.100";
        Int32 port = 33000;
        bool isKeyDown;
        bool connected = false;
        int xPos;
        int yPos;

        public Form1()
        {
            InitializeComponent();
            this.client = new RPClient();
        }

        

        private void Connect_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            client.ReceiveMessage += new ReceiveMessageEventHandler(RPClientLib_ReceiveMessage);

            if (!client.Connect(ipAddress, port))
            {
                tb_log.AppendText("Could not connect.");
            }
            else
            {
                client.ListenForMessages();
                tb_log.AppendText("Connection successful.\n");
                connected = true;

                // TODO: Disable/enable menu buttons 
            }                       
            
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!isKeyDown) {
                System.Diagnostics.Debug.WriteLine("key pressed: " + e.KeyCode.ToString().ToLower());
                if (client != null)
                {
                    client.SendCommand(e.KeyCode.ToString().ToLower());
                }
                isKeyDown = true;
            }   
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("key up");
            if (client != null) {
                client.SendCommand("stop");
            }           
            isKeyDown = false;
        }

        private void Disconnect_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (client != null)
            {
                client.Disconnect();
                connected = false;
            }            
        }


        private void pb_video_MouseMove(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("X: " + e.X + " Y: " + e.Y);
            if (client != null && connected)
            {
                client.SendCommand("h");
            }
        }

        private void RPClientLib_ReceiveMessage(object sender, ReceivedMessageEventArgs e)
        {
            if (tb_log.InvokeRequired)
            {

                MethodInvoker invoker = new MethodInvoker(delegate ()
                {

                    tb_log.AppendText(e.Message + "\n");

                });

                tb_log.BeginInvoke(invoker);

            }
            else
            {

                tb_log.AppendText(e.Message + "\n");

            }
        }
        
    }
}
