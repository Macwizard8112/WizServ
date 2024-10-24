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
    public partial class QSCDecoder : Form
    {
        //public Icon image100 = Resources.R.png;
        public string answer, seconddigit, thirddigit, fourthdigit, fifthdigit, Month, Year, Week, versionX;
        public string serial = Version.Serial;
        public string model = Version.Model;
        public string make = Version.Make;
        public bool TF;
        public bool TheOSis;
        private object OSVersion;
        private readonly string msg = "You must install new AC Board Assembly 534-WP200015-02 - Service Bulletin 7";
        private string msg2 = "This program ONLY checks if Service Bulletin 7 is needed, you are responsible for checking Service Bulletins 1 thru 6";
        private string msg3 = "                                                                                                                                                                                           ";
        static readonly string key = @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\lanmanserver\parameters";
        public readonly string computerDescription = (string)Registry.GetValue(key, "srvcomment", null);
        private bool IsMsg;

        public QSCDecoder()
        {
            InitializeComponent();
            this.TopMost = true;
            this.Focus();
            this.BringToFront();
            GAssembLyInfo();
            PullOSInfo();
            label1.Text = " Serial: " + serial;
            label23.Text = " " + make + " " + model + " ";
            label3.Text = "Serial lookup good for units manufactured from January 2016 to December 2035.";
            label4.Text = "";
            Start();
            button1.Select();
        }

        private void Start()
        {
            label4.BackColor = Color.Green;
            label4.ForeColor = Color.White;
            label4.Font = new Font("Microsoft Sans Serif,", 20, FontStyle.Bold);
            label4.Text = "                                 Does not need Service Bulletin K2-007                                 ";
            answer = "";
            answer = Version.Serial;
            if (answer.Length != 9)
            {
                MessageBox.Show("Serial # length must be 9 Characters");
                //textBox1.Text = "";
            }
            else
            {
                DisassembleSN();
            }
        }

        private async void Blink()
        {
            while (true)
            {
                await Task.Delay(500);
                label4.BackColor = label4.BackColor == Color.Red ? Color.Blue : Color.Red;
            }
        }

        public void GAssembLyInfo()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            versionX = fvi.FileVersion;
            Text = "Wizard Electronics Inc. - WizServ   v" + versionX + ",  OS: " + OSVersion;      // Set Menu Title
        }

        public void PullOSInfo()   // User friendly OS Version "Microsoft Windows XP Professional","Microsoft Windows 10 Pro", etc.
        {
            OSVersion = (from x in new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem").Get().Cast<ManagementObject>()
                         select x.GetPropertyValue("Caption")).FirstOrDefault();
            TheOSis = Environment.Is64BitOperatingSystem;
            if (TheOSis == true)
            {
                // "64bit";
                this.Text = "QSC Serial Number Decoder v " + versionX + ", " + OSVersion.ToString() + ", " + "64bit";
            }
            else
            {
                // "32bit";
                this.Text = "QSC Serial Number Decoder v " + versionX + ", " + OSVersion.ToString() + ", " + "32bit";
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics P_R\\QSC\\K.2 Family (K DOT 2 FAMILY)\\K.2 Series Service Bulletins\\Service Bulletins K12.2\\k2-001.pdf";
            process.Start();
            process.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics P_R\\QSC\\K.2 Family (K DOT 2 FAMILY)\\K.2 Series Service Bulletins\\Service Bulletins K12.2\\k2-002.pdf";
            process.Start();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics P_R\\QSC\\K.2 Family (K DOT 2 FAMILY)\\K.2 Series Service Bulletins\\Service Bulletins K12.2\\k2-003.pdf";
            process.Start();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics P_R\\QSC\\K.2 Family (K DOT 2 FAMILY)\\K.2 Series Service Bulletins\\Service Bulletins K12.2\\k2-004.pdf";
            process.Start();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics P_R\\QSC\\K.2 Family (K DOT 2 FAMILY)\\K.2 Series Service Bulletins\\Service Bulletins K12.2\\k2-005.pdf";
            process.Start();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics P_R\\QSC\\K.2 Family (K DOT 2 FAMILY)\\K.2 Series Service Bulletins\\Service Bulletins K12.2\\k2-006.pdf";
            process.Start();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\\Schematics P_R\\QSC\\K.2 Family (K DOT 2 FAMILY)\\K.2 Series Service Bulletins\\Service Bulletins K12.2\\k2-007_ra.pdf";
            process.Start();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\Schematics P_R\QSC\K.2 Family (K DOT 2 FAMILY)\K.2 Series Service Bulletins\Service Bulletins K12.2\k2-006_rb\\k2-006_rb.pdf";
            process.Start();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\Schematics P_R\QSC\K.2 Family (K DOT 2 FAMILY)\K.2 Series Service Bulletins\Service Bulletins K8.2\\q_spk_k2_firmwareupdate_tn.pdf";
            process.Start();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = "https://www.qsc.com/live-sound/products/loudspeakers/powered-loudspeakers/k2-series/k2_firmware/";
            process.Start();
        }

        private void ClearVars()
        {
            seconddigit = "";
            thirddigit = "";
            fourthdigit = "";
            fifthdigit = "";
        }

        private void DisassembleSN()
        {
            ClearVars();
            seconddigit = answer.Substring(1, 1);
            TF = seconddigit.All(Char.IsLetter);
            thirddigit = answer.Substring(2, 1);
            fourthdigit = answer.Substring(3, 1);
            fifthdigit = answer.Substring(4, 1);
            if (TF == true)
            {
                Format1();
                Format2();
                SetupMsg();
            }
            else
            {
                Format3();
                Format4();
                SetupMsg();
            }
        }

        private void SetTextColor()
        {
            label4.BackColor = Color.Red;
            label4.ForeColor = Color.White;
        }

        private void SetupMsg()
        {
            if (seconddigit == "A" && IsMsg == true)
            {
                SetTextColor();
                label4.Text = " " + msg + " ";
                Blink();
            }
            if (seconddigit == "B" && IsMsg == true)
            {
                SetTextColor();
                label4.Text = " " + msg + " ";
                Blink();
            }
            if (seconddigit == "C" && IsMsg == true)
            {
                SetTextColor();
                label4.Text = " " + msg + " ";
                Blink();
            }
            if (seconddigit == "D" && IsMsg == true)
            {
                SetTextColor();
                label4.Text = " " + msg + " ";
                Blink();
            }
            if (seconddigit == "E" && IsMsg == true)
            {
                SetTextColor();
                label4.Text = " " + msg + " ";
                Blink();
            }
            if (seconddigit == "F" && IsMsg == true)
            {
                SetTextColor();
                label4.Text = " " + msg + " ";
                Blink();
            }

            var d = seconddigit.All(Char.IsLetter);
            if (d == false)
            {
                var t = seconddigit + thirddigit;
                int result = Int32.Parse(t);
                if (result <= 30 && IsMsg == true)
                {
                    SetTextColor();
                    label4.Text = " " + msg + " ";
                    Blink();
                }
                if (IsMsg == true)
                {
                    SetTextColor();
                    label4.Text = " " + msg + " ";
                    Blink();
                }
            }

        }

        private void Format1()
        {
            switch (seconddigit)
            {
                case "A":
                    Month = "January ";
                    break;
                case "B":
                    Month = "February ";
                    break;
                case "C":
                    Month = "March ";
                    break;
                case "D":
                    Month = "April ";
                    break;
                case "E":
                    Month = "May ";
                    break;
                case "F":
                    Month = "June ";
                    break;
                case "G":
                    Month = "July ";
                    break;
                case "H":
                    Month = "August ";
                    break;
                case "I":
                    Month = "September ";
                    break;
                case "J":
                    Month = "October ";
                    break;
                case "K":
                    Month = "November ";
                    break;
                case "L":
                    Month = "December ";
                    break;
            }
        }

        private void Format2()
        {
            switch (thirddigit)
            {
                case "G":
                    Year = "2016 ";
                    IsMsg = true;
                    break;
                case "H":
                    Year = "2017 ";
                    IsMsg = true;
                    break;
                case "I":
                    Year = "2018 ";
                    IsMsg = true;
                    break;
                case "J":
                    Year = "2019 ";
                    IsMsg = true;
                    break;
                case "K":
                    Year = "2020 ";
                    IsMsg = true;
                    break;
                case "L":
                    Year = "2021 ";
                    IsMsg = true;
                    break;
                case "M":
                    Year = "2022 ";
                    IsMsg = true;
                    break;
                case "N":
                    Year = "2023 ";
                    IsMsg = false;
                    break;
                case "O":
                    Year = "2024 ";
                    IsMsg = false;
                    break;
                case "P":
                    Year = "2025 ";
                    IsMsg = false;
                    break;
                case "Q":
                    Year = "2026 ";
                    IsMsg = false;
                    break;
                case "R":
                    Year = "2027 ";
                    IsMsg = false;
                    break;
                case "S":
                    Year = "2028 ";
                    IsMsg = false;
                    break;
                case "T":
                    Year = "2029 ";
                    IsMsg = false;
                    break;
                case "U":
                    Year = "2030 ";
                    IsMsg = false;
                    break;
                case "V":
                    Year = "2031 ";
                    IsMsg = false;
                    break;
                case "W":
                    Year = "2032 ";
                    IsMsg = false;
                    break;
                case "X":
                    Year = "2033 ";
                    IsMsg = false;
                    break;
                case "Y":
                    Year = "2034 ";
                    IsMsg = false;
                    break;
                case "Z":
                    Year = "2035 ";
                    IsMsg = false;
                    break;
            }
            label2.Text = "Manufactured Month / Year: " + Month + Year;
        }

        private void Format3()
        {
            Week = seconddigit + thirddigit;
            label2.Text = "Manufactured Week: " + Week;
        }

        private void Format4()
        {
            var t = fourthdigit + fifthdigit;
            switch (t)
            {
                case "16":
                    Year = " of 2016";
                    IsMsg = true;
                    break;
                case "17":
                    Year = " of 2017";
                    IsMsg = true;
                    break;
                case "18":
                    Year = " of 2018";
                    IsMsg = true;
                    break;
                case "19":
                    Year = " of 2019";
                    IsMsg = true;
                    break;
                case "20":
                    Year = " of 2020";
                    IsMsg = true;
                    break;
                case "21":
                    Year = " of 2021";
                    IsMsg = true;
                    break;
                case "22":
                    Year = " of 2022";
                    IsMsg = true;
                    break;
                case "23":
                    Year = " of 2023";
                    IsMsg = false;
                    break;
                case "24":
                    Year = " of 2024";
                    IsMsg = false;
                    break;
                case "25":
                    Year = " of 2025";
                    IsMsg = false;
                    break;
                case "26":
                    Year = " of 2026";
                    IsMsg = false;
                    break;
                case "27":
                    Year = " of 2027";
                    IsMsg = false;
                    break;
                case "28":
                    Year = " of 2028";
                    IsMsg = false;
                    break;
                case "29":
                    Year = " of 2029";
                    IsMsg = false;
                    break;
                case "30":
                    Year = " of 2030";
                    IsMsg = false;
                    break;
                case "31":
                    Year = " of 2031";
                    IsMsg = false;
                    break;
                case "32":
                    Year = " of 2032";
                    IsMsg = false;
                    break;
                case "33":
                    Year = " of 2033";
                    IsMsg = false;
                    break;
                case "34":
                    Year = " of 2034";
                    IsMsg = false;
                    break;
                case "35":
                    Year = " of 2035";
                    IsMsg = false;
                    break;
            }
            label2.Text = "Manufactured Week " + Week + Year;
        }
    }
}
