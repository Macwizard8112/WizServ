using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using WizServ.Properties;
using WizServ.Resources;

namespace WizServ
{
    public partial class AdamAudio : Form
    {
        private string model = Version.Model;
        private string manuf = Version.Make;


        public AdamAudio()
        {
            InitializeComponent();
            this.TopMost = true;
            this.Focus();
            this.BringToFront();
            Icon = Properties.Resources.WizServ;
            StartHere();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void StartHere()
        {
            if (model.Contains("AX7"))
            {
                label1.Text = "There are Test WAV files on server to test unit with.\nOpening folder now.";
                Process.Start(new ProcessStartInfo()
                {
                    FileName = @"X:\\GoogleDrive\\WizardElect\\Schematics\\Schematics A\\Adam Audio\\Test Tones",
                    UseShellExecute = true,
                    Verb = "open"
                });
            }
            if (model.Contains("SUB"))
            {
                label1.Text = "There are Test WAV files on server to test unit with.\nOpening folder now.";
                Process.Start(new ProcessStartInfo()
                {
                    FileName = @"X:\\GoogleDrive\\WizardElect\\Schematics\\Schematics A\\Adam Audio\\Test Tones",
                    UseShellExecute = true,
                    Verb = "open"
                });
            }
        }
    }
}
