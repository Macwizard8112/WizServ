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
    public partial class AKAI : Form
    {
        private string model = Version.Model;
        private string manuf = Version.Make;

        public AKAI()
        {
            InitializeComponent();
            this.TopMost = true;
            this.Focus();
            this.BringToFront();
            Icon = Properties.Resources.WizServ;
            StartHere();
        }

        private void StartHere()
        {
            try
            {
                if (model.Contains("MPC1000"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There are Test WAV files on server to test unit with.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\AKAI\MPC 1000 SOUNDS",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("MPC 1000"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There are Test WAV files on server to test unit with.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\AKAI\MPC 1000 SOUNDS",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("MPC LIVE"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There are Firmware Updates / USB Drivers available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\AKAI\MPC Live!",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("MPCLIVE"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There are Firmware Updates / USB Drivers available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\AKAI\MPC Live!",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("MPC LIVE!"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There are Firmware Updates / USB Drivers available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\AKAI\MPC Live!",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("MPC 2KXL"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There are Firmware Updates / USB Drivers available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\AKAI\MPC2KXL\mpc2x114",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("MPC2KXL"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There are Firmware Updates / USB Drivers available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\AKAI\MPC2KXL\mpc2x114",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("MPC 200XL"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There are Test WAV / Midi files on server to test unit with.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\AKAI\mpc200xl",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("MPC200XL"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There are Test WAV / Midi files on server to test unit with.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\AKAI\mpc200xl",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("MPC 3000"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There are Test WAV / Midi files on server to test unit with.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\AKAI\MPC3000\MRSOUND",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("MPC3000"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There are Test WAV / Midi files on server to test unit with.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\AKAI\MPC3000\MRSOUND",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("MPC 4000"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There are 2 Firmware Updates / USB Drivers available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\AKAI\MPC4000",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("MPC4000"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There are 2 Firmware Updates / USB Drivers available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\AKAI\MPC4000",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("MPC 5000"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Firmware Update available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\AKAI\MPC5000\Firmware 1.02",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("MPC5000"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Firmware Update available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\AKAI\MPC5000\Firmware 1.02",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("A6"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Firmware Update / Sound / MIDI files available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Alesis\A6 Andromeda\A6 Sound - Firmware Files",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("A6 Andromeda"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Firmware Update / Sound / MIDI files available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Alesis\A6 Andromeda\A6 Sound - Firmware Files",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("DM10"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Firmware Update available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Alesis\DM10\DM10 v1.03 Update\5 - DM10 Sound ROM",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("DM 10"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Firmware Update available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Alesis\DM10\DM10 v1.03 Update\5 - DM10 Sound ROM",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("DPS12"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Software Update available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\AKAI\DPS12",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("DPS 12"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Software Update available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\AKAI\DPS12",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("DPS-12"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Software Update available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\AKAI\DPS12",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("QU16"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There are Service Bulletins available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Allen & Heath\QU-16\Service Bulletins",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("QU 16"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There are Service Bulletins available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Allen & Heath\QU-16\Service Bulletins",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("QU-16"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There are Service Bulletins available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Allen & Heath\QU-16\Service Bulletins",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("QU-24"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There are Service Bulletins && Software available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Allen & Heath\QU-24",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("QU24"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There are Service Bulletins && Software available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Allen & Heath\QU-24",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("QU 24"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There are Service Bulletins && Software available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Allen & Heath\QU-24",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("RPS11"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a README file available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Allen & Heath\RPS11",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("RPS 11"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a README file available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Allen & Heath\RPS11",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("RPS-11"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a README file available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Allen & Heath\RPS11",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("S3000"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Service Bulletin available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Allen & Heath\S3000\Technical Bulletin",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("S 3000"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Service Bulletin available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Allen & Heath\S3000\Technical Bulletin",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("S-3000"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Service Bulletin available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Allen & Heath\S3000\Technical Bulletin",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("S-5000"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Service Bulletin available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Allen & Heath\S5000\Technical Bulletin",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("S5000"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Service Bulletin available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Allen & Heath\S5000\Technical Bulletin",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("S 5000"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Service Bulletin available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Allen & Heath\S5000\Technical Bulletin",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("S 7000"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Service Bulletin available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Allen & Heath\S7000\Technical Bulletin",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("S7000"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Service Bulletin available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Allen & Heath\S7000\Technical Bulletin",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("S-7000"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Service Bulletin available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Allen & Heath\S7000\Technical Bulletin",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("SQ6"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Service Bulletin available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Allen & Heath\SQ6",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("SQ 6"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Service Bulletin available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Allen & Heath\SQ6",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("SQ-6"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Service Bulletin available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Allen & Heath\SQ6",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("ZED-428"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Service Bulletin available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Allen & Heath\\Zed_428",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("ZED 428"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Service Bulletin available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Allen & Heath\\Zed_428",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("ZED428"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Service Bulletin available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Allen & Heath\\Zed_428",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("B5R"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a file 'Common Failure Modes' available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Ampeg\\B5R",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("BA108-V2"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "IF WARRANTY - DO NOT REPAIR, GET FACTORY REPLACEMENT!\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Ampeg\\BA-108 V2",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("BA108 V2"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "IF WARRANTY - DO NOT REPAIR, GET FACTORY REPLACEMENT!\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Ampeg\\BA-108 V2",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("BA108V2"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "IF WARRANTY - DO NOT REPAIR, GET FACTORY REPLACEMENT!\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Ampeg\\BA-108 V2",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("BA110-V2"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "IF WARRANTY - DO NOT REPAIR, GET FACTORY REPLACEMENT!\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Ampeg\\BA-110 V2",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("BA110 V2"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "IF WARRANTY - DO NOT REPAIR, GET FACTORY REPLACEMENT!\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Ampeg\\BA-110 V2",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("BA110V2"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "IF WARRANTY - DO NOT REPAIR, GET FACTORY REPLACEMENT!\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Ampeg\\BA-110 V2",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }

                if (model.Contains("BA112-V2"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "IF WARRANTY - DO NOT REPAIR, GET FACTORY REPLACEMENT!\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Ampeg\\BA-112 V2",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("BA112 V2"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "IF WARRANTY - DO NOT REPAIR, GET FACTORY REPLACEMENT!\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Ampeg\\BA-112 V2",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("BA112V2"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "IF WARRANTY - DO NOT REPAIR, GET FACTORY REPLACEMENT!\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Ampeg\\BA-112 V2",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model == ("BA115"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Service Bulletin available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Ampeg\\BA-115 & HP",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model ==("BA 115"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Service Bulletin available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Ampeg\\BA-115 & HP",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model == ("BA-115"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Service Bulletin available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Ampeg\\BA-115 & HP",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }

                if (model == ("BA115 V2"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "IF WARRANTY - DO NOT REPAIR, GET FACTORY REPLACEMENT!\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Ampeg\\BA-115 V2",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model == ("BA 115 V2"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "IF WARRANTY - DO NOT REPAIR, GET FACTORY REPLACEMENT!\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Ampeg\\BA-115 V2",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model == ("BA-115 V2"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "IF WARRANTY - DO NOT REPAIR, GET FACTORY REPLACEMENT!\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Ampeg\\BA-115 V2",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model == ("BA-115-V2"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "IF WARRANTY - DO NOT REPAIR, GET FACTORY REPLACEMENT!\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Ampeg\\BA-115 V2",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model == ("BA115-V2"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "IF WARRANTY - DO NOT REPAIR, GET FACTORY REPLACEMENT!\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Ampeg\\BA-115 V2",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }

                if (model == ("BA-210 V2"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "IF WARRANTY - DO NOT REPAIR, GET FACTORY REPLACEMENT!\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Ampeg\\BA-210 V2",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model == ("BA-210-V2"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "IF WARRANTY - DO NOT REPAIR, GET FACTORY REPLACEMENT!\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Ampeg\\BA-210 V2",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model == ("BA210-V2"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "IF WARRANTY - DO NOT REPAIR, GET FACTORY REPLACEMENT!\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Ampeg\\BA-210 V2",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model == ("BA210 V2"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "IF WARRANTY - DO NOT REPAIR, GET FACTORY REPLACEMENT!\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Ampeg\\BA-210 V2",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("LIQUIFIER"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "IF WARRANTY - DO NOT REPAIR, GET FACTORY REPLACEMENT!\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Ampeg\\LIQUIFIER_ANALOG_CHORUS",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("MICRO CL"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "IF WARRANTY - DO NOT REPAIR, GET FACTORY REPLACEMENT!\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Ampeg\\MICRO CL",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("MICRO-CL"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "IF WARRANTY - DO NOT REPAIR, GET FACTORY REPLACEMENT!\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Ampeg\\MICRO CL",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("MICROCL"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "IF WARRANTY - DO NOT REPAIR, GET FACTORY REPLACEMENT!\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Ampeg\\MICRO CL",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("MICRO VR"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "IF WARRANTY - DO NOT REPAIR, GET FACTORY REPLACEMENT!\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Ampeg\\MICRO-VR",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("MICRO-VR"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "IF WARRANTY - DO NOT REPAIR, GET FACTORY REPLACEMENT!\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Ampeg\\MICRO-VR",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("MICROVR"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "IF WARRANTY - DO NOT REPAIR, GET FACTORY REPLACEMENT!\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Ampeg\\MICRO-VR",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }

                if (model.Contains("OPTO COMP"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "IF WARRANTY - DO NOT REPAIR, GET FACTORY REPLACEMENT!\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Ampeg\\OPTO_COMP_OPTICAL_COMPRESSOR",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("OPTOCOMP"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "IF WARRANTY - DO NOT REPAIR, GET FACTORY REPLACEMENT!\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Ampeg\\OPTO_COMP_OPTICAL_COMPRESSOR",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("OPTO-COMP"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "IF WARRANTY - DO NOT REPAIR, GET FACTORY REPLACEMENT!\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Ampeg\\OPTO_COMP_OPTICAL_COMPRESSOR",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("PF500"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "OPEN READ ME FIRST FIRST!\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Ampeg\\PF-500",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("PF 500"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "OPEN READ ME FIRST FIRST!\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Ampeg\\PF-500",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("PF-500"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "OPEN READ ME FIRST FIRST!\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Ampeg\\PF-500",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("SCR D1"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "IF WARRANTY - DO NOT REPAIR, GET FACTORY REPLACEMENT!\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Ampeg\\SCR D1",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("SCR-D1"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "IF WARRANTY - DO NOT REPAIR, GET FACTORY REPLACEMENT!\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Ampeg\\SCR D1",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("SCRD1"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "IF WARRANTY - DO NOT REPAIR, GET FACTORY REPLACEMENT!\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Ampeg\\SCR D1",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("SVPPRO"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Service Bulletin available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Ampeg\\SVP-PRO",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("SVP PRO"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Service Bulletin available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Ampeg\\SVP-PRO",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("SVP-PRO"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Service Bulletin available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Ampeg\\SVP-PRO",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("SVT2PRO"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Service Bulletin available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Ampeg\\SVT2-PRO\All files\Service Bulletins",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("SVT2 PRO"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Service Bulletin available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Ampeg\\SVT2-PRO\All files\Service Bulletins",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("SVT2-PRO"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Service Bulletin available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Ampeg\\SVT2-PRO\All files\Service Bulletins",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }

                if (model.Contains("SVT3PRO"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Service Bulletin available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Ampeg\\SVT3-PRO\Service Bulletins",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("SVT3 PRO"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Service Bulletin available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Ampeg\\SVT3-PRO\Service Bulletins",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("SVT3-PRO"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Service Bulletin available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Ampeg\\SVT3-PRO\Service Bulletins",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("SVT4PRO"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Service Bulletin available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Ampeg\\SVT4-PRO\\Tech_Notes",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("SVT4 PRO"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Service Bulletin available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Ampeg\\SVT4-PRO\\Tech_Notes",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("SVT4-PRO"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Service Bulletin available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Ampeg\\SVT4-PRO\\Tech_Notes",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("SVT-CL"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Zener Diode Upgrade document.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Ampeg\\SVT-CL",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("SVTCL"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Zener Diode Upgrade document.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Ampeg\\SVT-CL",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("SVT CL"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Zener Diode Upgrade document.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Ampeg\\SVT-CL",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("MX-508"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a README first document.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Ashley\\MX508",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("MX 508"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a README first document.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Ashley\\MX508",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("MX-508"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a README first document.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Ashley\\MX508",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("AD-2022"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Capacitor Readme document.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Avalon\\AD-2022",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("AD 2022"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Capacitor Readme document.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Avalon\\AD-2022",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("AD2022"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Capacitor Readme document.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Avalon\\AD-2022",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("VT-737"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There Engineering Update documents.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Avalon\\VT-737_SP",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("VT 737"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There Engineering Update documents.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Avalon\\VT-737_SP",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("VT737"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There Engineering Update documents.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics A\\Avalon\\VT-737_SP",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("AT-100"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There Engineering Update documents.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics B_C\\B52\\AT-100",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("AT 100"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There Engineering Update documents.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics B_C\\B52\\AT-100",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("AT100"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There Engineering Update documents.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics B_C\\B52\\AT-100",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("AT212"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Service Bulletin available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics B_C\\B52\\AT-212",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("AT 212"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Service Bulletin available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics B_C\\B52\\AT-212",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("AT-212"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Service Bulletin available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics B_C\\B52\\AT-212",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("HT-5"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Service Bulletin available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics B_C\Black_Star\HT5",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("HT 5"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Service Bulletin available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics B_C\Black_Star\HT5",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("HT5"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Service Bulletin available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics B_C\Black_Star\HT5",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("SP303"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Service Bulletin / Firmware available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics B_C\Boss\SP-303",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("SP-303"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Service Bulletin / Firmware available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics B_C\Boss\SP-303",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("SP 303"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Service Bulletin / Firmware available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics B_C\Boss\SP-303",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("AP-200"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Service Bulletins available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics B_C\CASIO\AP-200",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("AP 200"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Service Bulletins available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics B_C\CASIO\AP-200",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("AP200"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Service Bulletins available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics B_C\CASIO\AP-200",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("AP-620"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Service Bulletins available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics B_C\CASIO\AP-620",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("AP 620"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Service Bulletins available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics B_C\CASIO\AP-620",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("AP620"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Service Bulletins available.\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics B_C\CASIO\AP-620",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 1432\n" + ex);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
