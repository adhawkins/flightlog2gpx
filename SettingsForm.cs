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
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        public string FlightlogPath
        {
            get { return txtFlightlogPath.Text; }
            set { txtFlightlogPath.Text = value; }
        }

        public string GPXPath
        {
            get { return txtGPXPath.Text; }
            set { txtGPXPath.Text = value; }
        }

        private void btnFlightlogBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();

            if (0 != FlightlogPath.Length)
            {
                dlg.SelectedPath = FlightlogPath;
            }

            if (DialogResult.OK == dlg.ShowDialog())
            {
                FlightlogPath = dlg.SelectedPath;
            }
        }

        private void btnGPXBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();

            if (0 != GPXPath.Length)
            {
                dlg.SelectedPath = GPXPath;
            }

            if (DialogResult.OK == dlg.ShowDialog())
            {
                GPXPath = dlg.SelectedPath;
            }
        }
    }
}
