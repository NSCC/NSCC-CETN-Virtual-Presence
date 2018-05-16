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
    /// <summary>
    /// Represents the main application form.
    /// </summary>
    public partial class Form1 : Form
    {
        RPClient client = null;
        String ipAddress;
        Int32 port = 33000;
        bool isKeyDown; // is a key cuurrently being pressed
        bool connected = false; // are we currently connected to the robot
        bool mouseEnabled = false; // has mouse control of the head been enabled
        bool mouseIsDown = false; // is the mouse button being pressed
        int xPos = 0;
        int yPos = 0;
        List<Keys> keys = new List<Keys>() {Keys.A, Keys.S, Keys.D, Keys.W, Keys.Q, Keys.E, // Direction controls
                                            Keys.J, Keys.K, Keys.L, Keys.I, // Head controls
                                            Keys.D0, Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5, Keys.D6, Keys.D7, Keys.D8, Keys.D9, // Sound controls
                                            Keys.Enter}; // send a message

        /// <summary>
        /// Creates an instance of the main application form.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            tb_log.AppendText("Wait for robot to calibrate before connecting.\n");
        }
        
        
        /// <summary>
        /// Handles clicking the CONNECT menu item located in the NETWORK menu. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Connect_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.client = new RPClient();

            using (ConnectDialog connDialog = new ConnectDialog())
            {
                if (connDialog.ShowDialog() == DialogResult.OK)
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


        /// <summary>
        /// Handles clicking the DISCONNECT menu item in the NETWORKING menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Disconnect_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (client != null)
            {
                client.SendCommand("quit");
                client.Disconnect();
            }

            connected = false;
            connectToolStripMenuItem.Enabled = true;
            disconnectToolStripMenuItem.Enabled = false;

            // reset the GUI pan position  
            tbar_pan.Value = 0;

            // stop the video
            axVLCPlugin21.playlist.stop();
        }


        /// <summary>
        /// Handles key presses that will send a command to control the robot's motions and sounds.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {            
                
                if (!isKeyDown && !tb_message.Focused && keys.Contains(e.KeyCode))
                {
                    // TODO: Remove this line, for testing only
                    System.Diagnostics.Debug.WriteLine("key pressed: " + e.KeyCode.ToString().ToLower());

                    if (client != null)
                    {
                        if (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9)
                        {
                            client.SendCommand("p" + e.KeyCode.ToString().Substring(1));
                        }

                        else
                        {
                            client.SendCommand(e.KeyCode.ToString().ToLower());
                        }
                    }
                    isKeyDown = true;
                }
                else
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        btn_send.PerformClick();
                    }
                }           
        }


        /// <summary>
        /// Handles the key up event that will send a command to stop the robot's current motion.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            //TODO: Remove this code -  for testing only
            System.Diagnostics.Debug.WriteLine("key up");

            // Send key up all the time as a safety precaution (for now anyway)
            if (client != null)
            {
                client.SendCommand("stop");
            }           
            isKeyDown = false;
        }
                       

        /// <summary>
        /// Handles the message received event and processes them as required.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RPClientLib_ReceiveMessage(object sender, ReceivedMessageEventArgs e)
        {
            String message = String.Empty;

            if (e.Message[0] == 'q')
            {
                message = "The robot has disconnected";
                if (this.InvokeRequired)
                {
                    MethodInvoker invoker = new MethodInvoker(delegate ()
                    {
                        this.RemoteDisconnect();
                    });

                    this.BeginInvoke(invoker);
                }
                else
                {
                    this.RemoteDisconnect();
                }
            }
            else if (e.Message.Substring(0,3) == "pan")
            {
                if (tbar_pan.InvokeRequired)
                {
                    MethodInvoker invoker = new MethodInvoker(delegate ()
                    {
                        Int32 val;
                        if (int.TryParse(e.Message.Substring(3), out val))
                        {
                            if (val <= 115 && val > -115) {
                                tbar_pan.Value = val;
                            } 
                            
                        }
                    });
                    tbar_pan.BeginInvoke(invoker);
                }
                else {
                    Int32 val;
                    if (int.TryParse(e.Message.Substring(3), out val))
                    {
                        if (val <= 115 && val > -115)
                        {
                            tbar_pan.Value = val;
                        }
                    }
                }
            }
            else
            {
                message = e.Message;
            }           

            if (tb_log.InvokeRequired)
            {
                MethodInvoker invoker = new MethodInvoker(delegate ()
                {
                    tb_log.AppendText(message + "\n");
                    
                });

                tb_log.BeginInvoke(invoker);
            }
            else
            {
                tb_log.AppendText(message + "\n");
            }
        }


        /// <summary>
        /// Handles clicking the ENABLE MOUSE CONTROL menu item in the HEAD menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnableMouseControl_ToolStripMenuItem_Click(object sender, EventArgs e)
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
            

        /// <summary>
        /// Handles the mouse down event whent the user clicks in the video display. 
        /// The transparent panel is used because the VLC plugin does not have an on click event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tp_video_MouseDown(object sender, MouseEventArgs e)
        {
            mouseIsDown = true;
        }


        /// <summary>
        /// Handles the mouse move event (while the mouse button is down) on 
        /// the video display (actually the transparent panel overlay) and 
        /// sends a command to control the motion of the robot's head.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tp_video_MouseMove(object sender, MouseEventArgs e)
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

                if (client != null && mouseEnabled)
                {
                    client.SendCommand("hx" + xDiff.ToString() + "y" + yDiff.ToString());

                    // Sleep to avoid sending multiple commands too quickly
                    System.Threading.Thread.Sleep(100);
                }

                xPos = e.X;
                yPos = e.Y;
            }
        }


        /// <summary>
        /// Handles the mouse up event for the video display (actually the transparent panel).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tp_video_MouseUp(object sender, MouseEventArgs e)
        {
            mouseIsDown = false;
        }


        /// <summary>
        /// Handles errors from the VLC plugin.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AxVLCPlugin21_MediaPlayerEncounteredError(object sender, EventArgs e)
        {
            tb_log.AppendText("Unable to connect to video stream\n");            
        }


        /// <summary>
        /// Handles the double click event for the video display sending the 
        /// command to center the robot's head.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tp_video_DoubleClick(object sender, EventArgs e)
        {
            if (client != null)
            {
                client.SendCommand("cen");
            }
            
        }


        /// <summary>
        /// Starts the video stream after a successful connection to the robot.
        /// Note: The video stream takes quite a while to initially load (maybe 10+ seconds)
        /// </summary>
        private void StartVideo()
        {
            string options = ":network-caching=0, :file-caching=0, :disc-caching=0, :live-capture-caching=0";           
            axVLCPlugin21.playlist.add(@"rtsp://192.168.137.7:8554/rp7.stream", null, options);
            axVLCPlugin21.playlist.play();
            tb_log.AppendText("Waiting for video...\n");
        }
                

        /// <summary>
        /// Handles the form closing event making sure that we properly close the connection and 
        /// deal with the receive message thread.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!CloseForm())
            {
                e.Cancel = true;
            }
            else
            {
                if (client != null)
                {
                    client.SendCommand("quit");
                    client.Disconnect();
                }
            }           
        }


        /// <summary>
        /// Handles clicking the send button and sends the message in the message 
        /// text box to the robot to be processed with TTS.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_send_Click(object sender, EventArgs e)
        {
            if (client != null)
            {
                client.SendCommand("##" + tb_message.Text);
            }
            tb_message.Clear();
            btn_send.Enabled = false;
            this.ActiveControl = tp_video;
        }


        /// <summary>
        /// Handles clicking the C3PO menu item in the SOUNDS menu and sends 
        /// the command to switch to the C3PO sound bank.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void C3POToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (client != null)
            {
                client.SendCommand("B0");
            }
        }


        /// <summary>
        /// Handles clicking the LOST IN SPACE menu item in the COUNDS menu and sends
        /// the command to switch to the LOST IN SPACE sound bank.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LostInSpaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (client != null)
            {
                client.SendCommand("B1");
            }
        }


        /// <summary>
        /// Handles a remote disconnect. Triggered when the robot's sent message is "quit".
        /// </summary>
        private void RemoteDisconnect()
        {
            client.Disconnect();
            disconnectToolStripMenuItem.Enabled = false;
            connectToolStripMenuItem.Enabled = true;           
        }


        /// <summary>
        /// Displays a message box to confirm that the user wants to exit the application.
        /// </summary>
        /// <returns>True if the user confirms exit or False if cancelled.</returns>
        private bool CloseForm()
        {
            string message = "Are you sure you wan to exit?";
            string caption = "Confirm Exit";
            var result = MessageBox.Show(message, caption,
                             MessageBoxButtons.YesNo,
                             MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                return true;
            }
            else
            {                   
                return false;
            }
        }


        /// <summary>
        /// Handle clicking the EXIT menu item in the FILE menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();           
        }


        /// <summary>
        /// Handles text changed event for the message text box to enable/disable 
        /// the send button whether or not there is any text to send.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tb_message_TextChanged(object sender, EventArgs e)
        {
            if (tb_message.Text.Length > 0)
            {
                btn_send.Enabled = true;
            }
            else
            {
                btn_send.Enabled = false;
            }
        }


        /// <summary>
        /// Handles the leave focus event for the message textbox and disables the send button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tb_message_Leave(object sender, EventArgs e)
        {
            btn_send.Enabled = false;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Click(object sender, EventArgs e)
        {
            this.ActiveControl = tp_video;
            btn_send.Enabled = false;
        }

        private void tb_message_Enter(object sender, EventArgs e)
        {
            if (tb_message.Text.Length > 0)
            {
                btn_send.Enabled = true;
            }
        }

        private void CommandListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommandsDialog commandDialog = new CommandsDialog();
            commandDialog.ShowDialog();
            
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
