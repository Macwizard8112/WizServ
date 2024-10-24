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
    public partial class Yamaha : Form
    {
        private string model = Version.Model;
        private string manuf = Version.Make;

        public Yamaha()
        {
            InitializeComponent();
            Icon = Properties.Resources.WizServ;
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
                if (model.Contains("MOD X6"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There are Service Bulletins available\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics X_Y_Z\YAMAHA\MOD Family\MOD X6\Service News",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("MODX6"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There are Service Bulletins available\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics X_Y_Z\YAMAHA\MOD Family\MOD X6\Service News",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("MODX7"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There are Service Bulletins available\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics X_Y_Z\YAMAHA\MOD Family\MOD X6\Service News",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("MOD X7"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There are Service Bulletins available\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics X_Y_Z\YAMAHA\MOD Family\MOD X6\Service News",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("MODX8"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There are Service Bulletins available\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics X_Y_Z\YAMAHA\MOD Family\MOD X6\Service News",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("MOD X8"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There are Service Bulletins available\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics X_Y_Z\YAMAHA\MOD Family\MOD X6\Service News",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("MONTAGE"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There are Service Bulletins available\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics X_Y_Z\YAMAHA\Montage Family",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("MOTIF"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There are Service Bulletins available\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics X_Y_Z\YAMAHA\MOTIF Family- All Family",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("P-71"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There are Service Bulletins available\nOpening folder now.\nA P71 is just a P45 that was sold on Amazon.com";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics X_Y_Z\YAMAHA\P- Family\P-71",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("P 71"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There are Service Bulletins available\nOpening folder now.\nA P71 is just a P45 that was sold on Amazon.com";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics X_Y_Z\YAMAHA\P- Family\P-71",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("P71"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There are Service Bulletins available\nOpening folder now.\nA P71 is just a P45 that was sold on Amazon.com";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics X_Y_Z\YAMAHA\P- Family\P-71",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("P515"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There are Service Bulletins / Software available\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics X_Y_Z\YAMAHA\P- Family\P-515 Family",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("P 515"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There are Service Bulletins / Software available\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics X_Y_Z\YAMAHA\P- Family\P-515 Family",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("P-515"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There are Service Bulletins / Software available\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics X_Y_Z\YAMAHA\P- Family\P-515 Family",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("PF-70"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There are Service Bulletins available\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics X_Y_Z\YAMAHA\PF FAMILY\PF-70\Service News",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("PF70"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There are Service Bulletins available\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics X_Y_Z\YAMAHA\PF FAMILY\PF-70\Service News",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("PF 70"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There are Service Bulletins available\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics X_Y_Z\YAMAHA\PF FAMILY\PF-70\Service News",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("PF-80"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There are Service Bulletins available\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics X_Y_Z\YAMAHA\PF FAMILY\PF-80\Service News",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("PF80"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There are Service Bulletins available\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics X_Y_Z\YAMAHA\PF FAMILY\PF-80\Service News",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("PF 80"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There are Service Bulletins available\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics X_Y_Z\YAMAHA\PF FAMILY\PF-80\Service News",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("PSR-600"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There Read me file available\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics X_Y_Z\YAMAHA\PSR Family\PSR-600",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("PSR600"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There Read me file available\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics X_Y_Z\YAMAHA\PSR Family\PSR-600",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("PSR 600"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There Read me file available\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics X_Y_Z\YAMAHA\PF FAMILY\PSR Family\PSR-600",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("PSR 2700"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is Read me file available\nSerice Bulletins\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics X_Y_Z\YAMAHA\PSR Family\PSR-2700",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("PSR-2700"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is Read me file available\nSerice Bulletins\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics X_Y_Z\YAMAHA\PSR Family\PSR-2700",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("PSR2700"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is Read me file available\nSerice Bulletins\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics X_Y_Z\YAMAHA\PSR Family\PSR-2700",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("TF-1"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is Read me file available\nSerice Bulletins\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics X_Y_Z\YAMAHA\TF5 TF3 TF1",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("TF 1"))
                {
                    label1.Text = "There is Read me file available\nSerice Bulletins\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics X_Y_Z\YAMAHA\TF5 TF3 TF1",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("TF1"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is Read me file available\nSerice Bulletins\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics X_Y_Z\YAMAHA\TF5 TF3 TF1",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("TF-3"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is Read me file available\nSerice Bulletins\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics X_Y_Z\YAMAHA\TF5 TF3 TF1",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("TF 3"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is Read me file available\nSerice Bulletins\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics X_Y_Z\YAMAHA\TF5 TF3 TF1",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("TF3"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is Read me file available\nSerice Bulletins\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics X_Y_Z\YAMAHA\TF5 TF3 TF1",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("TF-5"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is Read me file available\nSerice Bulletins\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics X_Y_Z\YAMAHA\TF5 TF3 TF1",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("TF 5"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is Read me file available\nSerice Bulletins\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics X_Y_Z\YAMAHA\TF5 TF3 TF1",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("TF5"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is Read me file available\nSerice Bulletins\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics X_Y_Z\YAMAHA\TF5 TF3 TF1",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("TF RACK"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Service Bulletin available\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics X_Y_Z\YAMAHA\TF-Rack",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("TFRACK"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Service Bulletin available\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics X_Y_Z\YAMAHA\TF-Rack",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("TF-RACK"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Service Bulletin available\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics X_Y_Z\YAMAHA\TF-Rack",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.StartsWith("THR"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Service Bulletin and Firmware Update available\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics X_Y_Z\YAMAHA\THR30 II",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.StartsWith("TIO"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Service Bulletins available\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics X_Y_Z\YAMAHA\Tio1608-D\Service Bulletins",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("TYROS5"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Service Bulletin and Firmware / Drivers available\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics X_Y_Z\YAMAHA\Tyros Family\Tyros 5",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("TYROS 5"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Service Bulletin and Firmware / Drivers available\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics X_Y_Z\YAMAHA\Tyros Family\Tyros 5",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("TYROS-5"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There is a Service Bulletin and Firmware / Drivers available\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics X_Y_Z\YAMAHA\Tyros Family\Tyros 5",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("YDP-142"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There are Service Bulletins available\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics X_Y_Z\YAMAHA\YDP Family\YDP-142\Service News",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("YDP 142"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There are Service Bulletins available\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics X_Y_Z\YAMAHA\YDP Family\YDP-142\Service News",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("YDP142"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There are Service Bulletins available\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics X_Y_Z\YAMAHA\YDP Family\YDP-142\Service News",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("YDP184"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There are Service Bulletins and Software available\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics X_Y_Z\YAMAHA\YDP Family\YDP-184",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("YDP 184"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There are Service Bulletins and Software available\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics X_Y_Z\YAMAHA\YDP Family\YDP-184",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (model.Contains("YDP-184"))
                {
                    label1.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
                    label1.Text = "There are Service Bulletins and Software available\nOpening folder now.";
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics X_Y_Z\YAMAHA\YDP Family\YDP-184",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Line 581, Exception:\n" + ex);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
