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

        public flightlog2gpx()
        {
            // Create a simple tray menu with only one item.
            trayMenu = new ContextMenu();
            trayMenu.MenuItems.Add("Settings", OnSettings);
            trayMenu.MenuItems.Add("-");
            trayMenu.MenuItems.Add("Exit", OnExit);

            // Create a tray icon. In this example we use a
            // standard system icon for simplicity, but you
            // can of course use your own custom icon too.
            trayIcon = new NotifyIcon();
            trayIcon.Text = "flightlog2gpx";
            trayIcon.Icon = new Icon(SystemIcons.Application, 40, 40);

            // Add menu to tray icon and show it.
            trayIcon.ContextMenu = trayMenu;
            trayIcon.Visible = true;
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
    }
}
