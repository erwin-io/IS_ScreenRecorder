using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using CoreAudioApi;
using Microsoft.AspNet.SignalR.Client;
using IS_ScreenRecorder.Global;
using IS_ScreenRecorder.SignalR;
using Microsoft.AspNet.SignalR;

namespace IS_ScreenRecorder.Presentation
{
    public partial class Main : Form
    {
        private bool IsRecording = false;
        private MMDevice micDevice;
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );
        IHubContext hubContext;

        public Main()
        {
            InitializeComponent();

            var connection = new HubConnection("http://localhost:8080/signalr");
            connection.Start().Wait();

            MMDeviceEnumerator mMDeviceEnumerator = new MMDeviceEnumerator();
            MMDeviceCollection mMDeviceCollection = mMDeviceEnumerator.EnumerateAudioEndPoints(EDataFlow.eCapture, EDeviceState.DEVICE_STATE_ACTIVE);
            for (int i = 0; i < mMDeviceCollection.Count; i++)
            {
                MMDevice mMDevice = mMDeviceCollection[i];
                if (mMDevice.FriendlyName.ToLower() == "microphone")
                {
                    micDevice = mMDevice;
                }
            }
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

            hubContext = GlobalHost.ConnectionManager.GetHubContext<MicrophoneHub>();

            GlobalFunction.MicrophoneButtonIconChanged += new EventHandler(MicrophoneButtonIcon_Changed);
        }

        private void btnOpenScreenRecorder_Click(object sender, EventArgs e)
        {
            // open in default browser
            Process.Start("http://localhost:8080/home/recorder");
        }

        private void Main_Load(object sender, EventArgs e)
        {
            //labelRecording.Text = "RECORDING NOT STARTED" + Environment.NewLine + "00:00:00";
        }
        private void Form1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show("The Screen Recorder service is running in the background, Do you want to stop the application", "Stop Screen Recorder", buttons);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            //if the form is minimized  
            //hide it from the task bar  
            //and show the system tray icon (represented by the NotifyIcon control)  
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                screenRecorderNotif.Visible = true;
            }
        }
        private void screenRecorderNotif_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            screenRecorderNotif.Visible = false;
        }

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.IsRecording)
                {
                    this.IsRecording = false;
                    //btnStartStop.BackgroundImage = IS_ScreenRecorder.Properties.Resources.icons8_unchecked_radio_button_100__2_;
                    //labelRecording.Text = "RECORDING STOPPED" + Environment.NewLine + "00:00:00";
                    hubContext.Clients.All.StopRecord();
                }
                else
                {
                    this.IsRecording = true;
                    //btnStartStop.BackgroundImage = IS_ScreenRecorder.Properties.Resources.icons8_stop_circled_100;
                    //labelRecording.Text = "RECORDING YOUR SCREEN" + Environment.NewLine + "00:00:00";
                    hubContext.Clients.All.StartRecord();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnMuteUnMute_Click(object sender, EventArgs e)
        {
            try
            {
                if (micDevice == null)
                {
                    throw new InvalidOperationException("Microphone not found by MicMute Library!");
                }
                if (micDevice.AudioEndpointVolume.Mute)
                {
                    micDevice.AudioEndpointVolume.Mute = false;
                    btnMuteUnMute.BackgroundImage = IS_ScreenRecorder.Properties.Resources.icons8_microphone_100;
                    hubContext.Clients.All.ReceivedMicrophoneStatus("1");
                }
                else
                {
                    micDevice.AudioEndpointVolume.Mute = true;
                    btnMuteUnMute.BackgroundImage = IS_ScreenRecorder.Properties.Resources.icons8_mute_unmute_100;
                    hubContext.Clients.All.ReceivedMicrophoneStatus("0");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void MicrophoneButtonIcon_Changed(object sender, EventArgs e)
        {
            btnMuteUnMute.BackgroundImage = GlobalFunction.MicrophoneButtonIcon;
        }
    }
}
