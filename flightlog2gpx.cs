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
        private FileList m_FileList;
        private FileProcessor m_Processor;

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
            {
                m_FileList = new FileList(Properties.Settings.Default.FlightlogPath);
                m_Processor = new FileProcessor(Properties.Settings.Default.FlightlogPath,
                    Properties.Settings.Default.GPXPath, 
                    m_FileList);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            Visible = false; // Hide form window.
            ShowInTaskbar = false; // Remove from taskbar.

            base.OnLoad(e);
        }

        private void OnExit(object sender, EventArgs e)
        {
            m_FileList.Dispose();
            m_Processor.Dispose();
            trayIcon.Dispose();

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
            this.SuspendLayout();
            // 
            // flightlog2gpx
            // 
            this.ClientSize = new System.Drawing.Size(270, 79);
            this.Name = "flightlog2gpx";
            this.Text = "Status";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
