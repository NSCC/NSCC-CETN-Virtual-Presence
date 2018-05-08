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
        String ipAddress;
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
        }
        

        private void Connect_ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            using (ConnectDialog connDialog = new ConnectDialog())
            {
                if ( connDialog.ShowDialog() == DialogResult.OK)
                {
                    ipAddress = connDialog.IpAddress;

                    client.ReceiveMessage += new ReceiveMessageEventHandler(RPClientLib_ReceiveMessage);

                    if (!client.Connect(ipAddress, port))
                    {
                        tb_log.AppendText("Could not connect.\n");
                    }
                    else
                    {
                        client.ListenForMessages();
                        tb_log.AppendText("Connection successful.\n");
                        connected = true;

                        disconnectToolStripMenuItem.Enabled = true;
                        connectToolStripMenuItem.Enabled = false;

                        StartVideo();
                    }
                }
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

            axVLCPlugin21.playlist.stop();
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

                if (e.X > xPos && Math.Abs(xPos - e.X) > 5)
                {
                    xDiff = 5;
                }
                else if (e.X < xPos && Math.Abs(xPos - e.X) > 5)
                {
                    xDiff = -5;
                }

                if (e.Y > yPos && Math.Abs(yPos - e.Y) > 5)
                {
                    yDiff = 2;
                }
                else if (e.Y < yPos && Math.Abs(yPos - e.Y) > 5)
                {
                    yDiff = -2;
                }

                // TODO: Remove for testing only
                System.Diagnostics.Debug.WriteLine("X: " + xDiff + " Y: " + yDiff);

                if (client != null && connected && mouseEnabled)
                {
                    client.SendCommand("hx" + xDiff.ToString() + "y" + yDiff.ToString());

                    // Sleep to avoid sending multiple commands too quickly
                    System.Threading.Thread.Sleep(60);
                }

                xPos = e.X;
                yPos = e.Y;
            }
        }


        private void transparentPanel1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseIsDown = false;
        }


        private void axVLCPlugin21_MediaPlayerEncounteredError(object sender, EventArgs e)
        {
            tb_log.AppendText("Unable to connect to video stream\n");            
        }

        private void StartVideo()
        {
            string options = ":network-caching=0, :file-caching=0, :disc-caching=0, :live-capture-caching=0";
            //axVLCPlugin21.playlist.add(@"file:///C:\Users\Mike Laptop\Desktop\movies\2001.mp4");
            //axVLCPlugin21.playlist.add(@"udp://@", null, options);
            axVLCPlugin21.playlist.add(@"rtsp://192.168.137.7:8554/rp7.stream", null, options);
            axVLCPlugin21.playlist.play();
            tb_log.AppendText("Waiting for video...\n");
        }

        private void transparentPanel1_DoubleClick(object sender, EventArgs e)
        {
            client.SendCommand("cen");
        }
    }
}
