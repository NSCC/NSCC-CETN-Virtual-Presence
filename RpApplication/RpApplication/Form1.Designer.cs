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
            this.soundsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.c3POToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lostInSpaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tb_log = new System.Windows.Forms.TextBox();
            this.tb_message = new System.Windows.Forms.TextBox();
            this.btn_send = new System.Windows.Forms.Button();
            this.axVLCPlugin21 = new AxAXVLC.AxVLCPlugin2();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.tp_video = new VideoTest.TransparentPanel(this.components);
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axVLCPlugin21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
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
            this.soundsToolStripMenuItem});
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
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
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
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.helpToolStripMenuItem.Text = "Camera";
            // 
            // soundsToolStripMenuItem
            // 
            this.soundsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.c3POToolStripMenuItem,
            this.lostInSpaceToolStripMenuItem});
            this.soundsToolStripMenuItem.Name = "soundsToolStripMenuItem";
            this.soundsToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.soundsToolStripMenuItem.Text = "Sounds";
            // 
            // c3POToolStripMenuItem
            // 
            this.c3POToolStripMenuItem.Name = "c3POToolStripMenuItem";
            this.c3POToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.c3POToolStripMenuItem.Text = "C3PO";
            this.c3POToolStripMenuItem.Click += new System.EventHandler(this.C3POToolStripMenuItem_Click);
            // 
            // lostInSpaceToolStripMenuItem
            // 
            this.lostInSpaceToolStripMenuItem.Name = "lostInSpaceToolStripMenuItem";
            this.lostInSpaceToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.lostInSpaceToolStripMenuItem.Text = "Lost In Space";
            this.lostInSpaceToolStripMenuItem.Click += new System.EventHandler(this.LostInSpaceToolStripMenuItem_Click);
            // 
            // tb_log
            // 
            this.tb_log.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_log.Location = new System.Drawing.Point(1174, 40);
            this.tb_log.Multiline = true;
            this.tb_log.Name = "tb_log";
            this.tb_log.ReadOnly = true;
            this.tb_log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_log.Size = new System.Drawing.Size(398, 575);
            this.tb_log.TabIndex = 2;
            // 
            // tb_message
            // 
            this.tb_message.Location = new System.Drawing.Point(1174, 638);
            this.tb_message.Multiline = true;
            this.tb_message.Name = "tb_message";
            this.tb_message.Size = new System.Drawing.Size(398, 104);
            this.tb_message.TabIndex = 3;
            // 
            // btn_send
            // 
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
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(285, 813);
            this.trackBar1.Maximum = 115;
            this.trackBar1.Minimum = -115;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(584, 45);
            this.trackBar1.TabIndex = 0;
            // 
            // tp_video
            // 
            this.tp_video.Location = new System.Drawing.Point(20, 40);
            this.tp_video.Name = "tp_video";
            this.tp_video.Opacity = 0;
            this.tp_video.Size = new System.Drawing.Size(1128, 767);
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
            this.Controls.Add(this.trackBar1);
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
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axVLCPlugin21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
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
        private System.Windows.Forms.TrackBar trackBar1;
    }
}

