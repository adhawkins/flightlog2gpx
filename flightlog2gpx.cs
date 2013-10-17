using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace flightlog2gpx
{
    public partial class flightlog2gpx : Form
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new flightlog2gpx());
        }

        private NotifyIcon trayIcon;
        private ContextMenu trayMenu;

        private enum StatusType
        {
            eIdle,
            eBuildingFileList,
            eProcessing,
            eMonitoring,
            eUnconfigured
        }

        private StatusType m_Status = StatusType.eIdle;

        public flightlog2gpx()
        {
            InitializeComponent();

            // Create a simple tray menu with only one item.
            trayMenu = new ContextMenu();
            trayMenu.MenuItems.Add("Open", OnOpen);
            trayMenu.MenuItems.Add("Settings", OnSettings);
            trayMenu.MenuItems.Add("-");
            trayMenu.MenuItems.Add("Exit", OnExit);

            // Create a tray icon. In this example we use a
            // standard system icon for simplicity, but you
            // can of course use your own custom icon too.
            trayIcon = new NotifyIcon();
            trayIcon.Text = "flightlog2gpx";
            trayIcon.Icon = new Icon(SystemIcons.Application, 40, 40);
            trayIcon.DoubleClick += new System.EventHandler(this.iconDoubleClick);

            // Add menu to tray icon and show it.
            trayIcon.ContextMenu = trayMenu;
            trayIcon.Visible = true;

            if (Properties.Settings.Default.FlightlogPath.Length != 0 && Properties.Settings.Default.GPXPath.Length != 0)
                EnterNewState(StatusType.eBuildingFileList);
            else
                EnterNewState(StatusType.eUnconfigured);
        }

        private void EnterNewState(StatusType statusType)
        {
            switch (statusType)
            {
                case StatusType.eIdle:
                    break;

                case StatusType.eBuildingFileList:
                    break;

                case StatusType.eProcessing:
                    break;

                case StatusType.eMonitoring:
                    break;

                case StatusType.eUnconfigured:
                    break;
            }

            m_Status = statusType;
        }

        protected override void OnLoad(EventArgs e)
        {
            Visible = false; // Hide form window.
            ShowInTaskbar = false; // Remove from taskbar.

            base.OnLoad(e);
        }

        private void OnExit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void OnSettings(object sender, EventArgs e)
        {
            SettingsForm frmSettings = new SettingsForm();

            frmSettings.FlightlogPath = Properties.Settings.Default.FlightlogPath;
            frmSettings.GPXPath = Properties.Settings.Default.GPXPath;

            if (DialogResult.OK == frmSettings.ShowDialog())
            {
                Properties.Settings.Default.FlightlogPath = frmSettings.FlightlogPath;
                Properties.Settings.Default.GPXPath = frmSettings.GPXPath;
                Properties.Settings.Default.Save();
            }
        }

        private void iconDoubleClick(object Sender, EventArgs e)
        {
            ShowWindow();
        }

        private void OnOpen(object Sender, EventArgs e)
        {
            ShowWindow();
        }

        private void ShowWindow()
        {
            if (this.WindowState == FormWindowState.Minimized)
                this.WindowState = FormWindowState.Normal;

            this.Show();
            this.Activate();
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.onTimer);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Status:";
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(69, 13);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.Size = new System.Drawing.Size(189, 20);
            this.txtStatus.TabIndex = 1;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(69, 40);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(189, 23);
            this.progressBar1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Progress:";
            // 
            // flightlog2gpx
            // 
            this.ClientSize = new System.Drawing.Size(270, 79);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.label1);
            this.Name = "flightlog2gpx";
            this.Text = "Status";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void onTimer(object sender, EventArgs e)
        {
            switch (m_Status)
            {
                case StatusType.eIdle:
                    txtStatus.Text = "Idle";
                    break;

                case StatusType.eBuildingFileList:
                    txtStatus.Text = "Building file list";
                    break;

                case StatusType.eProcessing:
                    txtStatus.Text = "Processing";
                    break;

                case StatusType.eMonitoring:
                    txtStatus.Text = "Monitoring";
                    break;

                case StatusType.eUnconfigured:
                    txtStatus.Text = "Unconfigured";
                    break;
            }

            trayIcon.Text = txtStatus.Text;
        }
    }
}
