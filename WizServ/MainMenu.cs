using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Media;
using System.Threading;
using System.Management;
using System.Speech.Synthesis;
using System.Speech;

//
// Copyright 2021, 2022, 2023, 2024 Wizard Electronics, Inc. As an unpublished liscensed propritary work
// Version 1.0.0.0 - Initial Release, written by David Reynics
// Version 1.2.1.0 - Changed Estimates for new pricing start at Claim 300876
// Version 1.2.2.0 - Added checkBox3 for claims that go from Warranty to Non-Warranty repairs (Overdriven, etc)
// Version 1.2.2.2 - Added Gallien Kruger to Tech Support files.
// Version 1.2.2.4 - Added Backup to Main Menu
// Version 1.2.2.5 - Added Re-Assign to another tech

namespace WizServ
{
    public partial class MainMenu : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        private int mTimerCount, d4count, mWidth, mHeight, butpress, mPlayed;
        private string versionX;
        public bool TheOSis;
        public string claimno, pwWasSelected, PCNAME;
        public static string method1, method2, method3;
        public readonly string TechNames = @"I:\\Datafile\\Control\\Technician_Names.csv";
        static readonly string key = @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\lanmanserver\parameters";
        public readonly string computerDescription = (string)Registry.GetValue(key, "srvcomment", null);
        private object OSVersion;
        public int HighestClaimNumber, loopCount1, loop1;
        public string HighestClaimDate;
        public string TechicianNames1, TechicianNames2, TechicianNames3, TechicianNames4, TechicianNames5;
        public string IsDate;
        public string BUFileName = @"I:\Datafile\Control\BUDate.txt";

        public MainMenu()
        {
            InitializeComponent();
            label5.Text = "";
            ReadTheFile();
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = false;
            Icon = image100;
            //this.label29.Text = "V\nE\nR\nT\nI\nC\nA\nL\n"; // Display label Vertically
            label30.Visible = true;
            button13.Visible = false;
            button19.Visible = false;
            button22.Visible = false;
            button23.Visible = false;
            label30.Text = "Wizard Electronics, Inc.\nAs an unpublished\nliscensed propritary work";
            Hide();
            GetTechnicianNames();
            HighestClaimNumber f2 = new HighestClaimNumber();
            f2.Show();
            //HighestClaimNumber = Version.HighestClaimNumber;
            HighestClaimDate = Version.HighestClaimDate;
            HighestClaimNumber = Version.HighestClaimNumber;
            label28.Text += "\n" + HighestClaimNumber.ToString() + "  " + HighestClaimDate;
            if (computerDescription.Contains("PARTS"))
            {
                button19.Visible = true;
                button13.Visible = true;
                button22.Visible = true;
                button23.Visible = true;
            }
            else
            {
                button19.Visible = false;
                button13.Visible = false;
                button22.Visible = false;
                button23.Visible = false;
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            PullOSInfo();
            if (computerDescription == "PARTSTWO")              // If PC name is PARTSTWO, show Tech Assignment screen
            {
                Hide();
                Tech_Assign f9 = new Tech_Assign();
                f9.Show();
            }
            if (computerDescription == "PARTSTWO")
            {
                Hide();
            }
            BackColor = Color.LightSeaGreen;
            label26.BackColor = Color.LightSeaGreen;
            label10.BackColor = Color.LightSeaGreen;
            string dotw = DateTime.Now.DayOfWeek.ToString();
            DateTime dateValue = new DateTime();
            //label27.Text = dotw;
            label26.Text = dotw + ", " + DateTime.Now.ToShortDateString();
            label6.Visible = false;
            textBox1.Visible = false;
            label7.Visible = false;
            label11.Visible = true;
            //label3.Visible = true;
            label10.Text = "";
            method1 = Environment.MachineName.ToUpper();
            label9.Text = method1 + " Computer";
            pwWasSelected = Version.PWSelected;
            if (pwWasSelected == "Yes")
            {
                label7.Visible = true;
                label7.Text = "Password was requested.\nCole notified.";
            }
            TheOSis = Environment.Is64BitOperatingSystem;
            if (TheOSis == true)
            {
                //label5.Visible = true;
                //label5.Text = "64bit";
            }
            else
            {
               // label5.Visible = true;
                //label5.Text = "32bit";
            }
            label11.Text = "";
            timer1.Start();
            timer1.Interval = 1000;
            timer1.Enabled = true;
            GetScreenSize();
            GAssembLyInfo();
            if (Version.From == "WAREHOUSE")
            {
                button14.PerformClick();
            }
            SetPCNames();
            GetRam();
        }


        private void ReadTheFile()
        {
            if (File.Exists(BUFileName))
            {

                // Open the stream and read it back.
                using (StreamReader sr = File.OpenText(BUFileName))
                {

                    string s = "";
                    s = sr.ReadToEnd();
                    /*
                    while ((s = sr.ReadLine()) != null)
                    {
                        //listBox1.Items.Add(s);
                        IsDate = s;
                    }
                    */
                    s = s.Replace("\n", "");
                    IsDate = s;
                    sr.Close();
                    CheckIfToday();
                }
            }
            else
            {
                // Create a new file
                using (FileStream fs = File.Create(BUFileName))
                {
                    // Add some text to file
                    var dt = DateTime.Now.ToShortDateString();
                    Byte[] title = new UTF8Encoding(true).GetBytes(dt + "\n");
                    fs.Write(title, 0, title.Length);
                }
            }
            
        }

        private void CheckIfToday()
        {
            var dt = DateTime.Now.ToShortDateString();
            if (IsDate == dt)
            {
                //listBox1.Items.Add("Same\n");
                label5.Text = "Already backed up.";
            }
            else
            {
                label5.Text = "Backed up. " + dt.ToString(); ;
                // do backup, write todays date
                string Path = @"C:\\Windows\\System32\\";
                string sourcePath = @"I:\\_CSV_BACKUP_BU\\";
                var countDirectories = Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories).Count();
                Process proc = new Process();
                proc.StartInfo.UseShellExecute = true;
                proc.StartInfo.FileName = "xcopy.exe";
                proc.StartInfo.Arguments = @"I:\Datafile\Control I:\_CSV_BACKUP_BU\Backup /E /I /F /Y /H";
                proc.Start();
                string Answer = "Files Backed up to B/U Directory\n" + countDirectories.ToString() + " Directories copied.";
                int fileCount = Directory.EnumerateFiles(sourcePath, "*.*", SearchOption.AllDirectories).Count();
                int total = fileCount;
                Thread.Sleep(2000);
                CreateFile();
            }
        }

        private void CreateFile()
        {
            try
            {
                // Check if file already exists. If yes, process it.
                if (File.Exists(BUFileName))
                {
                    File.Delete(BUFileName);
                    // Create a new file
                    using (FileStream fs = File.Create(BUFileName))
                    {
                        // Add some text to file
                        var dt = DateTime.Now.ToShortDateString();
                        Byte[] title = new UTF8Encoding(true).GetBytes(dt + "\n");
                        fs.Write(title, 0, title.Length);
                    }

                    // Open the stream and read it back.
                    using (StreamReader sr = File.OpenText(BUFileName))
                    {
                        string s = "";
                        while ((s = sr.ReadLine()) != null)
                        {
                            //listBox1.Items.Add(s);
                            IsDate = s;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error: \n" + Ex);
            }
        }

        private void SetMenuItemsByPCName()
        {
            if (computerDescription == "PARTS2")
            {
                button21.Visible = true;
            }
            else
            {
                button21.Visible = false;
            }
        }

        public void GetTechnicianNames()
        {
            try
            {
                StreamReader reader = new StreamReader(TechNames, Encoding.GetEncoding("Windows-1252"));
                String line = reader.ReadLine();

                List<string> listA = new List<string>();
                List<string> listB = new List<string>();
                List<string> listC = new List<string>();
                List<string> listD = new List<string>();
                List<string> listE = new List<string>();

                loopCount1 = 0;
                loop1 = 0;

                while (!reader.EndOfStream)
                {
                    var lineRead = reader.ReadLine();
                    var values = lineRead.Split(',');

                    listA.Add(values[0]);
                    listB.Add(values[1]);
                    listC.Add(values[2]);
                    listD.Add(values[3]);
                    listE.Add(values[4]);

                    TechicianNames1 = listA[loopCount1];    //  Tech 1  Cole
                    TechicianNames2 = listB[loopCount1];    //  Tech 2  William
                    TechicianNames3 = listC[loopCount1];    //  Tech 3  Derek
                    TechicianNames4 = listD[loopCount1];    //  Tech 4  Billy
                    TechicianNames5 = listE[loopCount1];    //  Tech 5  Noel

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 1176: Sorry an error has occured: " + ex.Message);
            }
        }

        public void SetPCNames()
        {
            switch (computerDescription)
            {
                case "TECH5":
                    PCNAME = TechicianNames5;
                    Version.PCNAME = PCNAME;
                    button20.Text = PCNAME + "s Claims in Que";
                    break;
                case "TECH4":
                    PCNAME = TechicianNames4;
                    Version.PCNAME = PCNAME;
                    button20.Text = PCNAME + "s Claims in Que";
                    break;
                case "WIZTECH3":
                    PCNAME = TechicianNames3;
                    Version.PCNAME = PCNAME;
                    button20.Text = PCNAME + "s Claims in Que";
                    break;
                case "WIZTECH2":
                    PCNAME = TechicianNames2;
                    Version.PCNAME = PCNAME;
                    button20.Text = PCNAME + "s Claims in Que";
                    break;
                case "WIZTECH1":
                    PCNAME = TechicianNames1;
                    Version.PCNAME = PCNAME;
                    button20.Text = PCNAME + "s Claims in Que";
                    break;
                case "PARTS2":
                    /*
                    button20.Visible = true;
                    button20.Text = computerDescription.ToUpper() + " Button disabled";
                    Version.PCNAME = PCNAME;
                    button20.Enabled = false;
                    */
                    PCNAME = TechicianNames3;
                    Version.PCNAME = PCNAME;
                    button20.Text = PCNAME + "s Claims in Que";
                    break;
                case "PARTS3":
                    PCNAME = "PARTS";
                    Version.PCNAME = PCNAME;
                    button20.Text = PCNAME + "s Claims in Que";
                    break;
                default:
                    button20.Visible = true;
                    button20.Text = computerDescription.ToUpper() + " Butten disabled";
                    Version.PCNAME = PCNAME;
                    button20.Enabled = false;
                    break;
            }
        }

        private void GetRam()
        {
            Process proc = Process.GetCurrentProcess();
            var t = proc.PrivateMemorySize64;
            label27.Text = "Ram: " + t.ToString("n0") + " bytes";
        }

        public void PullOSInfo()   // User friendly OS Version "Microsoft Windows XP Professional","Microsoft Windows 10 Pro", etc.
        {
            OSVersion = (from x in new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem").Get().Cast<ManagementObject>()
                         select x.GetPropertyValue("Caption")).FirstOrDefault();
            TheOSis = Environment.Is64BitOperatingSystem;
            if (TheOSis == true)
            {
                // label5.Text = "64bit";
                label3.Text = OSVersion.ToString() + " " + "64bit";
            }
            else
            {
                //label5.Text = "32bit";
                label3.Text = OSVersion.ToString() + " " + "32bit";
            }
            
        }

        public void GetScreenSize()
        {
            try
            {
                mWidth = Screen.PrimaryScreen.Bounds.Width;  // Horizontal
                mHeight = Screen.PrimaryScreen.Bounds.Height; // Vertical
                Version.Width = mWidth;
                Version.Height = mHeight;
                label4.Text = "Width: " + mWidth.ToString() + "  Height: " + mHeight.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error line 133, GetScreenSize:\r\n" + ex);
            }
        }
        public void GAssembLyInfo()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            versionX = fvi.FileVersion;
            Version.versionX = versionX;
            Text = "Wizard Electronics Inc. - WizServ   v" + versionX + ",  OS: " + OSVersion;      // Set Menu Title
        }


        private void Button1_Click(object sender, EventArgs e)  // Enter Service Customer
        {
            butpress = 1;
            Hide();
            EnterServiceCustMenu f2 = new EnterServiceCustMenu();
            f2.Show();
        }

        private void Button2_Click(object sender, EventArgs e)  // Service Render Claims
        {
            butpress = 2;
            timer1.Stop();
            label6.Visible = true;
            textBox1.Visible = true;
            textBox1.Select();
        }

        private void Button3_Click(object sender, EventArgs e)  // Close Customer Claims
        {
            butpress = 3;
            timer1.Stop();
            label6.Visible = true;
            textBox1.Visible = true;
            textBox1.Select();

        }

        private void Button4_Click(object sender, EventArgs e)  // Retrieve Customer Claims
        {
            butpress = 4;
            timer1.Stop();
            Hide();
            RetrieveMenu f2 = new RetrieveMenu();
            f2.Show();
        }

        private void Button5_Click(object sender, EventArgs e)  // Claims Management Menu
        {
            butpress = 5;
            timer1.Stop();
            Hide();
            ClaimsMGTMenu f2 = new ClaimsMGTMenu();
            f2.Show();
        }

        private void Button6_Click(object sender, EventArgs e)  // Inventory Control System
        {
            butpress = 6;
            Hide();
            InventoryMenu f2 = new InventoryMenu();
            f2.Show();
        }

        private void Button7_Click(object sender, EventArgs e)  // Over Counter Parts Sales
        {
            butpress = 7;
            timer1.Stop();
            Hide();
            OTCSale f2 = new OTCSale();
            f2.Show();
        }

        private void Button8_Click(object sender, EventArgs e)  // Warranty Claims Menu
        {
            butpress = 8;
        }

        private void Button9_Click(object sender, EventArgs e)  // Repoarts and A/R Menu
        {
            butpress = 9;
            Hide();
            ARMenu f2 = new ARMenu();
            f2.Show();
        }

        private void Button10_Click(object sender, EventArgs e) // Customer Status Menu
        {
            butpress = 10;
            Hide();
            CustStatusMenu f2 = new CustStatusMenu();
            f2.Show();
        }

        private void Button11_Click(object sender, EventArgs e) // Print Options Menu
        {
            butpress = 11;
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        { 
            if (e.KeyData == Keys.Escape)
            {
                MainMenu.ActiveForm.Close();
                Application.ExitThread();
                Close();
            }
            if (e.KeyData == Keys.A)
            {
                butpress = 1;
                Hide();
                EnterServiceCustMenu f2 = new EnterServiceCustMenu();
                f2.Show();
            }
            if (e.KeyData == Keys.B)
            {
                butpress = 2;
                timer1.Stop();
                label6.Visible = true;
                textBox1.Visible = true;
                textBox1.Select();
            }
            if (e.KeyData == Keys.C)
            {
                butpress = 3;
                timer1.Stop();
                label6.Visible = true;
                textBox1.Visible = true;
                textBox1.Select();
            }
            if (e.KeyData == Keys.D)
            {
                butpress = 4;
                timer1.Stop();
                Hide();
                RetrieveMenu f2 = new RetrieveMenu();
                f2.Show();
            }
            if (e.KeyData == Keys.E)
            {
                butpress = 5;
                timer1.Stop();
                Hide();
                ClaimsMGTMenu f2 = new ClaimsMGTMenu();
                f2.Show();
            }
            if (e.KeyData == Keys.F)
            {
                butpress = 6;
            }
            if (e.KeyData == Keys.G)
            {
                butpress = 7;
            }
            if (e.KeyData == Keys.H)
            {
                butpress = 8;
            }
            if (e.KeyData == Keys.I)
            {
                butpress = 9;
                Hide();
                ARMenu f2 = new ARMenu();
                f2.Show();
            }
            if (e.KeyData == Keys.J)
            {
                butpress = 10;
                Hide();
                CustStatusMenu f2 = new CustStatusMenu();
                f2.Show();
            }
            if (e.KeyData == Keys.K)
            {
                butpress = 11;
            }
            if (e.KeyData == Keys.L)
            {
                butpress = 12;
                Version.From = "MainMenu";
                Hide();
                Password f2 = new Password();
                f2.Show();
            }
            if (e.KeyData == Keys.M)
            {
                butpress = 13;
            }
            if (e.KeyData == Keys.Q)
            {
                Application.ExitThread();
                Close();
            }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
            Close();
        }

        private void enterServiceCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            butpress = 1;
            Hide();
            EnterServiceCustMenu f2 = new EnterServiceCustMenu();
            f2.Show();
        }

        private void serviceRenderClaimMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            butpress = 1;
            Hide();
            EnterServiceCustMenu f2 = new EnterServiceCustMenu();
            f2.Show();
        }

        private void closeCustomerClaimsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            butpress = 3;
            timer1.Stop();
            label6.Visible = true;
            textBox1.Visible = true;
            textBox1.Select();
        }

        private void retrieveCustomerClaimsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            butpress = 4;
            timer1.Stop();
            Hide();
            RetrieveMenu f2 = new RetrieveMenu();
            f2.Show();
        }

        private void claimsManagementMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            butpress = 5;
            timer1.Stop();
            Hide();
            ClaimsMGTMenu f2 = new ClaimsMGTMenu();
            f2.Show();
        }

        private void inventoryControlSystemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            butpress = 6;
        }

        private void overCounterPartsSalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            butpress = 7;
        }

        private void warrantyClaimsMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            butpress = 8;
        }

        private void reportsAndARMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            butpress = 9;
            Hide();
            ARMenu f2 = new ARMenu();
            f2.Show();
        }

        private void customerStatusMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            butpress = 10;
            Hide();
            CustStatusMenu f2 = new CustStatusMenu();
            f2.Show();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            butpress = 11;
        }

        private void utilitiesMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            butpress = 12;
            Version.From = "MainMenu";
            Hide();
            Password f2 = new Password();
            f2.Show();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Version.From = "MainMenu";
            Hide();
            Warehouse f2 = new Warehouse();
            f2.Show();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Version.From = "MainMenu";
            Hide();
            Tech_Assign f2 = new Tech_Assign();
            f2.Show();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            Hide();
            Info f2 = new Info();
            f2.Show();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            frmChild ChildForm = new frmChild();
            // keeps Parent window open in background
            ChildForm.Show();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            Hide();
            FixDatabase f2 = new FixDatabase();
            f2.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            butpress = 1;
            Hide();
            EnterServiceCustMenu f2 = new EnterServiceCustMenu();
            f2.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            butpress = 2;
            timer1.Stop();
            label6.Visible = true;
            textBox1.Visible = true;
            textBox1.Select();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            butpress = 3;
            timer1.Stop();
            label6.Visible = true;
            textBox1.Visible = true;
            textBox1.Select();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            butpress = 4;
            timer1.Stop();
            Hide();
            RetrieveMenu f2 = new RetrieveMenu();
            f2.Show();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            butpress = 5;
            timer1.Stop();
            Hide();
            ClaimsMGTMenu f2 = new ClaimsMGTMenu();
            f2.Show();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            butpress = 6;
            Hide();
            InventoryMenu f2 = new InventoryMenu();
            f2.Show();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            butpress = 7;
            timer1.Stop();
            Hide();
            OTCSale f2 = new OTCSale();
            f2.Show();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            butpress = 8;
        }


        private void pictureBox9_Click(object sender, EventArgs e)
        {
            butpress = 9;
            Hide();
            ARMenu f2 = new ARMenu();
            f2.Show();
        }

        private void button13_Click_1(object sender, EventArgs e)
        {
            Version.From = "MAINMENU";
            Hide();
            CreateEstimate f2 = new CreateEstimate();
            f2.Show();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            Hide();
            TechAssignment f2 = new TechAssignment();
            f2.Show();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            Hide();
            FinalRender f2 = new FinalRender();
            f2.Show();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            //Heavy work (simulated by thread.sleep)
            string Path = @"C:\\Windows\\System32\\";
            string sourcePath = @"I:\\_CSV_BACKUP_BU\\";
            var countDirectories = Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories).Count();
            Process proc = new Process();
            proc.StartInfo.UseShellExecute = true;
            proc.StartInfo.FileName = "xcopy.exe";
            proc.StartInfo.Arguments = @"I:\Datafile\Control I:\_CSV_BACKUP_BU\Backup /E /I /F /Y /H";
            proc.Start();
            string Answer = "Files Backed up to B/U Directory\n" + countDirectories.ToString() + " Directories copied.";
            int fileCount = Directory.EnumerateFiles(sourcePath, "*.*", SearchOption.AllDirectories).Count();
            int total = fileCount;
            //label2.Visible = true;
            //label2.Text = Answer + "Files Copied: " + fileCount.ToString();
            //MessageBox.Show("Backup Completed!");
        }

        private void button25_Click(object sender, EventArgs e)
        {
            Hide();
            ViewEstimatesTwo f0 = new ViewEstimatesTwo();
            f0.Show();
        }

        private void button26_Click(object sender, EventArgs e)
        {
            Hide();
            Reassign f2 = new Reassign();
            f2.Show();
        }

        private void button27_Click(object sender, EventArgs e)
        {
            Hide();
            FixDatabase f2 = new FixDatabase();
            f2.Show();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            butpress = 10;
            Hide();
            CustStatusMenu f2 = new CustStatusMenu();
            f2.Show();
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            butpress = 11;
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            Version.From = "MAINMENU";
            Hide();
            Password f2 = new Password();
            f2.Show();
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            butpress = 13;
            Hide();
            Tech_AssignmentMenu f3 = new Tech_AssignmentMenu();
            f3.Show();
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            Application.ExitThread();
            Close();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            Hide();
            ThisTechAssign f2 = new ThisTechAssign();
            f2.Show();
        }

        private void Button12_Click(object sender, EventArgs e) // Utilities Menu
        {
            Version.From = "MAINMENU";
            Hide();
            Password f2 = new Password();
            f2.Show();
        }

        private void MainMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void MainMenu_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)    // TextBox1 - Enter Claim #
        {
            if (e.KeyCode == Keys.Enter)
            {
                claimno = textBox1.Text;
                Version.Claim = claimno;
                switch (butpress)
                {
                    case 2:
                        Hide();
                        ServiceRenderClaimMenu f2 = new ServiceRenderClaimMenu();
                        f2.Show();
                        break;
                    case 3:
                        Hide();
                        CloseClaim f3 = new CloseClaim();
                        f3.Show();
                        break;
                }
                
            }
        }

        private void Button13_Click(object sender, EventArgs e) // Future
        {
            timer1.Stop();
            butpress = 13;
            Hide();
            Tech_AssignmentMenu f3 = new Tech_AssignmentMenu();
            f3.Show();
        }

        private void Button14_Click(object sender, EventArgs e) // Quit
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            Application.ExitThread();
            Close();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            butpress = 21;
            Hide();
            NightlyMenu f3 = new NightlyMenu();
            f3.Show();
        }

        public void PlaySimpleSound()
        {
            //SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.ChurchBell);
            SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.Alarm10);
            simpleSound.Play();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (computerDescription == "WAREHOUSE")
            {
                button15.PerformClick();
            }
            mTimerCount++;
            if (mTimerCount >= 501)
            {
                mTimerCount = 0;
            }
            var d88 = 0;
            string startTime = DateTime.Now.ToShortTimeString();
            string endTime = "17:00";
            string startTime2 = DateTime.Now.ToShortTimeString();
            string endTime2 = "17:00";
            TimeSpan duration2 = DateTime.Parse(endTime2).Subtract(DateTime.Parse(startTime2));
            TimeSpan duration = DateTime.Parse(endTime).Subtract(DateTime.Parse(startTime));
            var uit = duration2.ToString();
            var rr = DateTime.Now.ToLongTimeString();
            if (uit == "00:15:00")
            {
                if (rr == "5:45:01 PM")
                {
                    uit = "Program Closes in 15 minutes.";
                    MessageBox.Show("Message " + uit);
                }

            }
            var Mtime = duration.ToString(@"hh\:mm");
            if (Mtime == "00:01")
            {
                mPlayed++;
                if (mPlayed == 1)
                {
                    label10.ForeColor = Color.White;
                    label10.Text = "Program auto-closes in: 1 Minute !";
                }
                    if (mPlayed == 1)
                {
                    label10.ForeColor = Color.White;
                    label10.Text = "Program auto-closes in: 1 Minute !";
                    Thread.Sleep(100);
                    PlaySimpleSound();
                    Thread.Sleep(3500);
                    mPlayed++;
                }

            }
            if (Mtime == "00:01")
            {
                label11.Text = "" + DateTime.Now.ToShortTimeString();
                label10.ForeColor = Color.White;
                label10.Text = "Program auto-closes in: " + Mtime.ToString() + " hh:mm";
            }
            else
            {
                label11.Text = "" + DateTime.Now.ToShortTimeString();
                label10.ForeColor = Color.White;
                label10.Text = "Program auto-closes in: " + Mtime.ToString() + " hh:mm";
            }
            var d2 = DateTime.Now.ToShortTimeString();
            var d3 = d2.ToString();
           
            if (d2.Contains("4:55 PM"))
            {
                d4count++;
            }
            if (d2.Contains("4:55 PM"))
            {
                if (d4count <= 2)
                {
                    string startTime1 = "4:45 PM";
                    string endTime1 = "5:00 PM";
                    TimeSpan duration1 = DateTime.Parse(endTime1).Subtract(DateTime.Parse(startTime1));
                    var Mtime1 = duration1.ToString(@"hh\:mm");
                    //MessageBox.Show("Program will self-close\nin 5 minutes.");
                    label10.Text = "Program auto-closes in: " + Mtime1.ToString() + " minutes";
                }
            }
            if (d3.Contains("5:00 PM"))                        // Close program everyday @ 5PM
            {
                if (Version.HasUsedMenu == true)
                {
                    Application.ExitThread();
                    Close();
                }
                else
                {
                    Close();
                }

            }
            if (Version.InHide == true)
            {
                mTimerCount++;
                var d = DateTime.Now.ToShortTimeString();
                var d1 = d.ToString();
                if (d1.Contains("5:00 PM"))                        // Close program everyday @ 5PM
                {
                    Application.ExitThread();
                    Close();
                }
            }
            GetRam();
        }
    }
}
