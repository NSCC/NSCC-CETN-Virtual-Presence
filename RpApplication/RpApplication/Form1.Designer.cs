namespace RpApplication
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.networkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.headToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MItem_MouseControl = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startVideoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopVideoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.soundsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.c3POToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lostInSpaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dalekToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.commandListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tb_log = new System.Windows.Forms.TextBox();
            this.tb_message = new System.Windows.Forms.TextBox();
            this.btn_send = new System.Windows.Forms.Button();
            this.axVLCPlugin21 = new AxAXVLC.AxVLCPlugin2();
            this.tbar_pan = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel_battLevel = new System.Windows.Forms.Panel();
            this.tp_video = new VideoTest.TransparentPanel(this.components);
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axVLCPlugin21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbar_pan)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.networkToolStripMenuItem,
            this.headToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.soundsToolStripMenuItem,
            this.helpToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1584, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItem1.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // networkToolStripMenuItem
            // 
            this.networkToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToolStripMenuItem,
            this.disconnectToolStripMenuItem});
            this.networkToolStripMenuItem.Name = "networkToolStripMenuItem";
            this.networkToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.networkToolStripMenuItem.Text = "Network";
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.connectToolStripMenuItem.Text = "Connect";
            this.connectToolStripMenuItem.Click += new System.EventHandler(this.Connect_ToolStripMenuItem_Click);
            // 
            // disconnectToolStripMenuItem
            // 
            this.disconnectToolStripMenuItem.Enabled = false;
            this.disconnectToolStripMenuItem.Name = "disconnectToolStripMenuItem";
            this.disconnectToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.disconnectToolStripMenuItem.Text = "Disconnect";
            this.disconnectToolStripMenuItem.Click += new System.EventHandler(this.Disconnect_ToolStripMenuItem_Click);
            // 
            // headToolStripMenuItem
            // 
            this.headToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MItem_MouseControl});
            this.headToolStripMenuItem.Name = "headToolStripMenuItem";
            this.headToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.headToolStripMenuItem.Text = "Head";
            // 
            // MItem_MouseControl
            // 
            this.MItem_MouseControl.Name = "MItem_MouseControl";
            this.MItem_MouseControl.Size = new System.Drawing.Size(191, 22);
            this.MItem_MouseControl.Text = "Enable Mouse Control";
            this.MItem_MouseControl.Click += new System.EventHandler(this.EnableMouseControl_ToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startVideoToolStripMenuItem,
            this.stopVideoToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.helpToolStripMenuItem.Text = "Camera";
            // 
            // startVideoToolStripMenuItem
            // 
            this.startVideoToolStripMenuItem.Enabled = false;
            this.startVideoToolStripMenuItem.Name = "startVideoToolStripMenuItem";
            this.startVideoToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.startVideoToolStripMenuItem.Text = "Start Video";
            this.startVideoToolStripMenuItem.Click += new System.EventHandler(this.StartVideoToolStripMenuItem_Click);
            // 
            // stopVideoToolStripMenuItem
            // 
            this.stopVideoToolStripMenuItem.Enabled = false;
            this.stopVideoToolStripMenuItem.Name = "stopVideoToolStripMenuItem";
            this.stopVideoToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.stopVideoToolStripMenuItem.Text = "Stop Video";
            this.stopVideoToolStripMenuItem.Click += new System.EventHandler(this.StopVideoToolStripMenuItem_Click);
            // 
            // soundsToolStripMenuItem
            // 
            this.soundsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.c3POToolStripMenuItem,
            this.lostInSpaceToolStripMenuItem,
            this.dalekToolStripMenuItem});
            this.soundsToolStripMenuItem.Name = "soundsToolStripMenuItem";
            this.soundsToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.soundsToolStripMenuItem.Text = "Sounds";
            // 
            // c3POToolStripMenuItem
            // 
            this.c3POToolStripMenuItem.Checked = true;
            this.c3POToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.c3POToolStripMenuItem.Enabled = false;
            this.c3POToolStripMenuItem.Name = "c3POToolStripMenuItem";
            this.c3POToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.c3POToolStripMenuItem.Text = "C3PO";
            this.c3POToolStripMenuItem.Click += new System.EventHandler(this.C3POToolStripMenuItem_Click);
            // 
            // lostInSpaceToolStripMenuItem
            // 
            this.lostInSpaceToolStripMenuItem.Enabled = false;
            this.lostInSpaceToolStripMenuItem.Name = "lostInSpaceToolStripMenuItem";
            this.lostInSpaceToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.lostInSpaceToolStripMenuItem.Text = "Lost In Space";
            this.lostInSpaceToolStripMenuItem.Click += new System.EventHandler(this.LostInSpaceToolStripMenuItem_Click);
            // 
            // dalekToolStripMenuItem
            // 
            this.dalekToolStripMenuItem.Enabled = false;
            this.dalekToolStripMenuItem.Name = "dalekToolStripMenuItem";
            this.dalekToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.dalekToolStripMenuItem.Text = "Dalek";
            this.dalekToolStripMenuItem.Click += new System.EventHandler(this.DalekToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.commandListToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem1.Text = "Help";
            // 
            // commandListToolStripMenuItem
            // 
            this.commandListToolStripMenuItem.Name = "commandListToolStripMenuItem";
            this.commandListToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.commandListToolStripMenuItem.Text = "Command List";
            this.commandListToolStripMenuItem.Click += new System.EventHandler(this.CommandListToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // tb_log
            // 
            this.tb_log.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_log.Location = new System.Drawing.Point(1174, 61);
            this.tb_log.Multiline = true;
            this.tb_log.Name = "tb_log";
            this.tb_log.ReadOnly = true;
            this.tb_log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_log.Size = new System.Drawing.Size(398, 554);
            this.tb_log.TabIndex = 2;
            // 
            // tb_message
            // 
            this.tb_message.Location = new System.Drawing.Point(1174, 656);
            this.tb_message.Multiline = true;
            this.tb_message.Name = "tb_message";
            this.tb_message.Size = new System.Drawing.Size(398, 86);
            this.tb_message.TabIndex = 3;
            this.tb_message.TextChanged += new System.EventHandler(this.Tb_message_TextChanged);
            this.tb_message.Enter += new System.EventHandler(this.Tb_message_Enter);
            this.tb_message.Leave += new System.EventHandler(this.Tb_message_Leave);
            // 
            // btn_send
            // 
            this.btn_send.Enabled = false;
            this.btn_send.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_send.Location = new System.Drawing.Point(1174, 758);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(398, 31);
            this.btn_send.TabIndex = 4;
            this.btn_send.Text = "Send";
            this.btn_send.UseVisualStyleBackColor = true;
            this.btn_send.Click += new System.EventHandler(this.Btn_send_Click);
            // 
            // axVLCPlugin21
            // 
            this.axVLCPlugin21.Enabled = true;
            this.axVLCPlugin21.Location = new System.Drawing.Point(20, 40);
            this.axVLCPlugin21.Name = "axVLCPlugin21";
            this.axVLCPlugin21.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axVLCPlugin21.OcxState")));
            this.axVLCPlugin21.Size = new System.Drawing.Size(1128, 749);
            this.axVLCPlugin21.TabIndex = 5;
            this.axVLCPlugin21.MediaPlayerEncounteredError += new System.EventHandler(this.AxVLCPlugin21_MediaPlayerEncounteredError);
            // 
            // tbar_pan
            // 
            this.tbar_pan.Enabled = false;
            this.tbar_pan.LargeChange = 10;
            this.tbar_pan.Location = new System.Drawing.Point(294, 815);
            this.tbar_pan.Maximum = 115;
            this.tbar_pan.Minimum = -115;
            this.tbar_pan.Name = "tbar_pan";
            this.tbar_pan.Size = new System.Drawing.Size(584, 45);
            this.tbar_pan.SmallChange = 10;
            this.tbar_pan.TabIndex = 0;
            this.tbar_pan.TickFrequency = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.label1.Location = new System.Drawing.Point(1176, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 18);
            this.label1.TabIndex = 7;
            this.label1.Text = "Log";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.label2.Location = new System.Drawing.Point(1176, 632);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(247, 18);
            this.label2.TabIndex = 8;
            this.label2.Text = "Enter a Message for Text-to-Speech";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.label3.Location = new System.Drawing.Point(518, 794);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(149, 18);
            this.label3.TabIndex = 9;
            this.label3.Text = "Camera Pan Position";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.label4.Location = new System.Drawing.Point(17, 815);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 18);
            this.label4.TabIndex = 11;
            this.label4.Text = "Battery";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(77, 815);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 23);
            this.panel1.TabIndex = 12;
            // 
            // panel_battLevel
            // 
            this.panel_battLevel.BackColor = System.Drawing.SystemColors.HotTrack;
            this.panel_battLevel.Location = new System.Drawing.Point(77, 815);
            this.panel_battLevel.Name = "panel_battLevel";
            this.panel_battLevel.Size = new System.Drawing.Size(0, 23);
            this.panel_battLevel.TabIndex = 13;
            // 
            // tp_video
            // 
            this.tp_video.Location = new System.Drawing.Point(20, 40);
            this.tp_video.Name = "tp_video";
            this.tp_video.Opacity = 0;
            this.tp_video.Size = new System.Drawing.Size(1128, 749);
            this.tp_video.TabIndex = 6;
            this.tp_video.DoubleClick += new System.EventHandler(this.Tp_video_DoubleClick);
            this.tp_video.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Tp_video_MouseDown);
            this.tp_video.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Tp_video_MouseMove);
            this.tp_video.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Tp_video_MouseUp);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1584, 861);
            this.Controls.Add(this.panel_battLevel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbar_pan);
            this.Controls.Add(this.tp_video);
            this.Controls.Add(this.axVLCPlugin21);
            this.Controls.Add(this.btn_send);
            this.Controls.Add(this.tb_message);
            this.Controls.Add(this.tb_log);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RP Application";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Click += new System.EventHandler(this.Form1_Click);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axVLCPlugin21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbar_pan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem networkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disconnectToolStripMenuItem;
        private System.Windows.Forms.TextBox tb_log;
        private System.Windows.Forms.TextBox tb_message;
        private System.Windows.Forms.Button btn_send;
        private System.Windows.Forms.ToolStripMenuItem headToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MItem_MouseControl;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private AxAXVLC.AxVLCPlugin2 axVLCPlugin21;
        private VideoTest.TransparentPanel tp_video;
        private System.Windows.Forms.ToolStripMenuItem soundsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem c3POToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lostInSpaceToolStripMenuItem;
        private System.Windows.Forms.TrackBar tbar_pan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem commandListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dalekToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startVideoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopVideoToolStripMenuItem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel_battLevel;
    }
}

