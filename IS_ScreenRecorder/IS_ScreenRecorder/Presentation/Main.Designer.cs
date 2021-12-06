namespace IS_ScreenRecorder.Presentation
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.screenRecorderNotif = new System.Windows.Forms.NotifyIcon(this.components);
            this.btnMuteUnMute = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.PictureBox();
            this.btnOpenScreenRecorder = new System.Windows.Forms.PictureBox();
            this.btnMinimize = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.btnMuteUnMute)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOpenScreenRecorder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimize)).BeginInit();
            this.SuspendLayout();
            // 
            // screenRecorderNotif
            // 
            this.screenRecorderNotif.Icon = ((System.Drawing.Icon)(resources.GetObject("screenRecorderNotif.Icon")));
            this.screenRecorderNotif.Text = "IS Screen Recorder";
            this.screenRecorderNotif.Visible = true;
            this.screenRecorderNotif.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.screenRecorderNotif_MouseDoubleClick);
            // 
            // btnMuteUnMute
            // 
            this.btnMuteUnMute.BackColor = System.Drawing.Color.Transparent;
            this.btnMuteUnMute.BackgroundImage = global::IS_ScreenRecorder.Properties.Resources.icons8_microphone_100;
            this.btnMuteUnMute.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMuteUnMute.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMuteUnMute.Location = new System.Drawing.Point(110, 50);
            this.btnMuteUnMute.Name = "btnMuteUnMute";
            this.btnMuteUnMute.Size = new System.Drawing.Size(40, 40);
            this.btnMuteUnMute.TabIndex = 5;
            this.btnMuteUnMute.TabStop = false;
            this.btnMuteUnMute.Click += new System.EventHandler(this.btnMuteUnMute_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BackgroundImage = global::IS_ScreenRecorder.Properties.Resources.outline_highlight_off_black_24dp;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Location = new System.Drawing.Point(172, 8);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(23, 32);
            this.btnClose.TabIndex = 3;
            this.btnClose.TabStop = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOpenScreenRecorder
            // 
            this.btnOpenScreenRecorder.BackColor = System.Drawing.Color.Transparent;
            this.btnOpenScreenRecorder.BackgroundImage = global::IS_ScreenRecorder.Properties.Resources.icons8_open_in_popup_100;
            this.btnOpenScreenRecorder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOpenScreenRecorder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpenScreenRecorder.Location = new System.Drawing.Point(50, 50);
            this.btnOpenScreenRecorder.Name = "btnOpenScreenRecorder";
            this.btnOpenScreenRecorder.Size = new System.Drawing.Size(40, 40);
            this.btnOpenScreenRecorder.TabIndex = 2;
            this.btnOpenScreenRecorder.TabStop = false;
            this.btnOpenScreenRecorder.Click += new System.EventHandler(this.btnOpenScreenRecorder_Click);
            // 
            // btnMinimize
            // 
            this.btnMinimize.BackColor = System.Drawing.Color.Transparent;
            this.btnMinimize.BackgroundImage = global::IS_ScreenRecorder.Properties.Resources.outline_do_not_disturb_on_black_24dp;
            this.btnMinimize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnMinimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMinimize.Location = new System.Drawing.Point(143, 8);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(23, 32);
            this.btnMinimize.TabIndex = 1;
            this.btnMinimize.TabStop = false;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(150)))), ((int)(((byte)(141)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(205, 115);
            this.ControlBox = false;
            this.Controls.Add(this.btnMuteUnMute);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOpenScreenRecorder);
            this.Controls.Add(this.btnMinimize);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.Padding = new System.Windows.Forms.Padding(30);
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IS Screen Recorder";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Main_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.btnMuteUnMute)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOpenScreenRecorder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimize)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox btnMinimize;
        private System.Windows.Forms.PictureBox btnOpenScreenRecorder;
        private System.Windows.Forms.PictureBox btnClose;
        private System.Windows.Forms.NotifyIcon screenRecorderNotif;
        private System.Windows.Forms.PictureBox btnMuteUnMute;
    }
}