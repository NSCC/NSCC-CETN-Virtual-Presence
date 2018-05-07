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
        String ipAddress = "192.168.137.7";
        Int32 port = 33000;
        bool isKeyDown;
        bool connected = false;
        bool mouseEnabled = false;
        bool mouseIsDown = false;
        int xPos = 0;
        int yPos = 0;

        public Form1()
        {
            InitializeComponent();
            this.client = new RPClient();
            string options = ":network-caching=10, :file-caching=10, :disc-caching=10, :live-capture-caching=10";
            //axVLCPlugin21.playlist.add(@"file:///C:\Users\Mike Laptop\Desktop\movies\2001.mp4");
            //axVLCPlugin21.playlist.add(@"udp://@");

            axVLCPlugin21.playlist.add(@"rtsp://192.168.137.7:8554/rp7.stream", null, options);

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

                disconnectToolStripMenuItem.Enabled = true;
                connectToolStripMenuItem.Enabled = false;
                axVLCPlugin21.playlist.play();
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
            }

            connected = false;
            connectToolStripMenuItem.Enabled = true;

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


        private void enableMouseControlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mouseEnabled)
            {
                mouseEnabled = false;
                MItem_MouseControl.Checked = false;
            }
            else
            {
                mouseEnabled = true;
                MItem_MouseControl.Checked = true;               
            }
        }
            

        private void TransparentPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseIsDown = true;
        }

        private void transparentPanel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseIsDown)
            {
                int yDiff = 0;
                int xDiff = 0;

                if (e.X > xPos && Math.Abs(xPos - e.X) > 10)
                {
                    xDiff = 22;
                }
                else if (e.X < xPos && Math.Abs(xPos - e.X) > 10)
                {
                    xDiff = -22;
                }

                if (e.Y > yPos && Math.Abs(yPos - e.Y) > 10)
                {
                    yDiff = 5;
                }
                else if (e.Y < yPos && Math.Abs(yPos - e.Y) > 10)
                {
                    yDiff = -5;
                }

                // TODO: Remove for testing only
                System.Diagnostics.Debug.WriteLine("X: " + xDiff + " Y: " + yDiff);

                if (client != null && connected && mouseEnabled)
                {
                    client.SendCommand("hx" + xDiff.ToString() + "y" + yDiff.ToString());

                    // Sleep to avoid sending multiple commands too quickly
                    System.Threading.Thread.Sleep(80);
                }

                xPos = e.X;
                yPos = e.Y;
            }
        }

        private void transparentPanel1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseIsDown = false;
        }
    }
}
