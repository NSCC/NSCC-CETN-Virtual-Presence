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
        int xDistance = 7; // how much to pan the head each time the command is sent
        int yDistance = 4; // how much to tilt the head each time the command is sent

        // A list of keys that will send commands. This list is checked so that key presses that don't do anything aren't sent.
        List<Keys> keys = new List<Keys>() {Keys.A, Keys.S, Keys.D, Keys.W, Keys.Q, Keys.E, // Direction controls
                                            Keys.J, Keys.K, Keys.L, Keys.I, // Head controls                                            
                                            Keys.Enter, // send a message
                                            Keys.Space}; // center the head

        List<Keys> soundKeys = new List<Keys>() { Keys.D0, Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5, Keys.D6, Keys.D7, Keys.D8, Keys.D9 }; // Sound controls}



        //DebounceDispatcher mouseMoveThrottle = new DebounceDispatcher();

            /// <summary>
            /// Creates an instance of the main application form.
            /// </summary>
        public Form1()
        {
            InitializeComponent();
            tb_log.AppendText("Wait for robot to calibrate before connecting.\n");
        }     


        /// <summary>
        /// Handles key presses that will send a command to control the robot's motions and sounds.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!isKeyDown && !tb_message.Focused && (keys.Contains(e.KeyCode) || soundKeys.Contains(e.KeyCode)))
            {
                // TODO: Remove this line, for testing only
                System.Diagnostics.Debug.WriteLine("key pressed: " + e.KeyCode.ToString().ToLower());

                if (client != null)
                {
                    if (soundKeys.Contains(e.KeyCode))
                    {
                        client.SendCommand("p" + e.KeyCode.ToString().Substring(1)); // Play sound commands begin with "p"
                    }
                    else
                    {
                        client.SendCommand(e.KeyCode.ToString().ToLower());
                    }
                }
                isKeyDown = true;
            }
            else if (e.KeyCode == Keys.M) // puts focus on the text-to-speech message box
            {
                this.ActiveControl = tb_message;
            }
            else
            {
                if (e.KeyCode == Keys.Enter) // sends the message in the text-to-speech message box
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

            // Send key up all the time as a safety precaution except for the sound keys (for now anyway)
            if (client != null && !soundKeys.Contains(e.KeyCode))
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

            
            if (e.Message[0] == 'q') // The robot application was termintated
            {
                message = "The robot has disconnected";
                InvokeRemoteDisconnect();                
            }
            else if (e.Message.Substring(0, 3) == "pan") // Robot is sending the head pan position
            {
                InvokePanTrackBar(e.Message);                
            }
            else if (e.Message.Substring(0, 4) == "batt") // Robot is sending a bettery reading
            {
                InvokeBatteryLevelPanel(e.Message);                
            }
            else
            {
                message = e.Message; // all other messages
            }           

            // Write the message to the GUI
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
            if (mouseIsDown) // need to be pressing the mouse button to control the head
            {
                int yDiff = 0;
                int xDiff = 0;
                    
                // Check the difference in mouse position to know which way to move and 
                // also check that the mouse moved more than 5 to avoid over sending commands
                if (e.X > xPos && Math.Abs(xPos - e.X) > 5)
                {
                    xDiff = xDistance;
                }
                else if (e.X < xPos && Math.Abs(xPos - e.X) > 5)
                {
                    xDiff = xDistance * -1;
                }

                if (e.Y > yPos && Math.Abs(yPos - e.Y) > 5)
                {
                    yDiff = yDistance;
                }
                else if (e.Y < yPos && Math.Abs(yPos - e.Y) > 5)
                {
                    yDiff = yDistance * -1;
                }

                // TODO: Remove for testing only
                System.Diagnostics.Debug.WriteLine("X: " + xDiff + " Y: " + yDiff);

                if (client != null && mouseEnabled)
                {
                    client.SendCommand("hx" + xDiff.ToString() + "y" + yDiff.ToString()); // Commands to move the head begin with "h" followed by the x and y coords

                    // Sleep to avoid sending commands too quickly
                    System.Threading.Thread.Sleep(120);
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
            // set all caching values to 0 to reduce lag times
            string options = ":network-caching=0, :file-caching=0, :disc-caching=0, :live-capture-caching=0";  
            
            // ### Uncomment the next line to use RTSP stream ###
            axVLCPlugin21.playlist.add(@"rtsp://" + ipAddress + ":8554/rp7.stream", null, options);

            // ### Uncomment this line to use UDP stream ###
            //axVLCPlugin21.playlist.add(@"udp://@");

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
                    axVLCPlugin21.playlist.stop();
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
                        client = null;
                    }
                    else
                    {
                        client.ListenForMessages();
                        tb_log.AppendText("Connection successful.\n");
                        connected = true;

                        disconnectToolStripMenuItem.Enabled = true;
                        connectToolStripMenuItem.Enabled = false;

                        StartVideo();
                        stopVideoToolStripMenuItem.Enabled = true;
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
            stopVideoToolStripMenuItem.Enabled = false;
            startVideoToolStripMenuItem.Enabled = false;

            // reset the GUI pan position  
            tbar_pan.Value = 0;

            // stop the video
            axVLCPlugin21.playlist.stop();
            tb_log.AppendText("Disconnected\n");
        }


        /// <summary>
        /// Handles clicking the C3PO menu item in the SOUNDS menu and sends 
        /// the command to switch sound banks.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void C3POToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (client != null)
            {
                client.SendCommand("B0"); // sound bank commands start with "B"
            }
        }


        /// <summary>
        /// Handles clicking the LOST IN SPACE menu item in the SOUNDS menu and sends
        /// the command to switch sound banks.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LostInSpaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (client != null)
            {
                client.SendCommand("B1"); // sound bank commands start with "B"
            }
        }
        

        /// <summary>
        /// Handles clicking the DALEK menu item in the SOUNDS menu and sends
        /// the command to switch sound banks.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DalekToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (client != null)
            {
                client.SendCommand("B2"); // sound bank commands start with "B"
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
            // move the focus away from the message textbox
            this.ActiveControl = label1;
            btn_send.Enabled = false;
        }


        /// <summary>
        /// Enables the text-to-speech send button when the text box enters focus.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tb_message_Enter(object sender, EventArgs e)
        {
            if (tb_message.Text.Length > 0)
            {
                btn_send.Enabled = true;
            }
        }


        /// <summary>
        /// Handles clicking the COMMANDS LIST menu item in the HELP menu 
        /// and displays a dialog box with the available commands.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommandListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommandsDialog commandDialog = new CommandsDialog();
            commandDialog.ShowDialog();            
        }


        /// <summary>
        /// Handles clicking the ABOUT menu intem in the HELP menu
        /// and displays a dialog with information about the application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutDialog aboutDialog = new AboutDialog();
            aboutDialog.ShowDialog();
        }


        /// <summary>
        /// Handles clicking the START VIDEO menu item in the CAMERA menu and starts the video stream.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartVideoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axVLCPlugin21.playlist.play();
            startVideoToolStripMenuItem.Enabled = false;
            stopVideoToolStripMenuItem.Enabled = true;
        }


        /// <summary>
        /// Handles clicking the STOP VIDEO menu item in the CAMERA menu and stops the video stream.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StopVideoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axVLCPlugin21.playlist.stop();
            stopVideoToolStripMenuItem.Enabled = false;
            startVideoToolStripMenuItem.Enabled = true;
        }


        /// <summary>
        /// Determines if an invoke is required to handle a remote disconnect.
        /// </summary>
        private void InvokeRemoteDisconnect() {
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


        /// <summary>
        /// Determines if an invoke is required to adjust the Pan track bar.
        /// </summary>
        /// <param name="message"></param>
        private void InvokePanTrackBar(string message)
        {
            if (tbar_pan.InvokeRequired)
            {
                MethodInvoker invoker = new MethodInvoker(delegate ()
                {
                    Int32 val;
                    if (int.TryParse(message.Substring(3), out val))
                    {
                        if (val <= 115 && val > -115)
                        {
                            tbar_pan.Value = val;
                        }

                    }
                });
                tbar_pan.BeginInvoke(invoker);
            }
            else
            {
                Int32 val;
                if (int.TryParse(message.Substring(3), out val))
                {
                    if (val <= 115 && val > -115)
                    {
                        tbar_pan.Value = val;
                    }
                }
            }
        }


        /// <summary>
        /// Determines if an invoke is required to adjust the Battery Level.
        /// </summary>
        /// <param name="message"></param>
        private void InvokeBatteryLevelPanel(string message)
        {
            if (panel_battLevel.InvokeRequired)
            {
                MethodInvoker invoker = new MethodInvoker(delegate ()
                {
                    double batt;
                    if (double.TryParse(message.Substring(4), out batt))
                    {
                        if (batt < 20.5)
                        {
                            panel_battLevel.Width = 0;
                        }
                        else if (batt > 28.5)
                        {
                            panel_battLevel.Width = 200;
                        }
                        else
                        {
                            panel_battLevel.Width = (int)((batt - 20) * 20);
                        }
                    }
                });
                panel_battLevel.BeginInvoke(invoker);
            }
            else
            {
                double batt;
                if (double.TryParse(message.Substring(4), out batt))
                {
                    if (batt < 20.5)
                    {
                        panel_battLevel.Width = 0;
                    }
                    else if (batt > 28.5)
                    {
                        panel_battLevel.Width = 200;
                    }
                    else
                    {
                        panel_battLevel.Width = (int)((batt - 20) * 20);
                    }
                }
            }
        }

    }// end class
} //end namespace
