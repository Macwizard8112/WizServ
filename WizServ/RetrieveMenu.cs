using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WizServ
{
    public partial class RetrieveMenu : Form 
    {
        public Icon image100 = Properties.Resources.WizServ;
        private readonly string Database = @"I:\\Datafile\\Control\\Database.CSV";
        private int mTimerCount, d4count, mWidth, mHeight;
        private string versionX, ClaimNum, butNum;
        public bool Claim_Exists;
        private int loopCount;

        public RetrieveMenu()
        {
            InitializeComponent();
            timer1.Start();
            timer1.Interval = 1000;
            Icon = image100;
            this.BackColor = Color.LightSeaGreen;
            label3.Text = "";
            label6.Text = "";
            label1.Visible = false;
            label5.Visible = false;
            textBox1.Visible = false;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
            GetScreenSize();
            GAssembLyInfo();
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }

        private void Button1_Click(object sender, EventArgs e)  // By Customer Claim Number
        {
            label1.Text = "Enter Claim Number:";
            label5.Visible = false;
            butNum = "Button1";
            label1.Visible = true;
            textBox1.Visible = true;
            textBox1.Select();
        }

        private void ClaimExists()
        {
            Claim_Exists = false;
            GetData();
            if (Claim_Exists == false)
            {
                MessageBox.Show("Claim: " + ClaimNum + " not found.\nPlease check number and try again.");
                Hide();
                RetrieveMenu f2 = new RetrieveMenu();
                f2.Show();
            }
            else
            {
                // Continue showing information
            }
        }
        public void GetData()
        {
            Claim_Exists = false;
            try
            {
                if (Version.DatabaseIsLocked == true)
                {
                    MessageBox.Show("Database in use, please wait");

                }
            }
            catch (Exception)
            {
                Thread.Sleep(4000);
            }
            try
            {
                StreamReader reader = new StreamReader(Database, Encoding.GetEncoding("Windows-1252"));
                String line = reader.ReadLine();

                List<string> listA = new List<string>();
                List<string> listB = new List<string>();
                List<string> listC = new List<string>();
                List<string> listD = new List<string>();
                List<string> listE = new List<string>();
               


                loopCount = 0;

                while (!reader.EndOfStream)
                {
                    var lineRead = reader.ReadLine();
                    var values = lineRead.Split(',');

                    listA.Add(values[0]);       //  war_prd
                    listB.Add(values[1]);       //  claim_no
                    listC.Add(values[2]);       //  datein
                    listD.Add(values[3]);       //  fname
                    listE.Add(values[4]);       //  lname
                    

                    if (listB[loopCount] == ClaimNum)
                    {
                        Claim_Exists = true;
                    }
                    loopCount++;
                }
                reader.Close(); // Close the open file

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 108: Sorry an error has occured: " + ex.Message);
            }
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {  
            if (e.KeyCode == Keys.Enter)
            {
                switch (butNum)
                {
                    case "Button1":                 // Search by Claim #
                        ClaimNum = textBox1.Text;
                        Version.Claim = ClaimNum;
                        label1.Visible = false;
                        textBox1.Visible = false;
                        timer1.Stop();
                        ClaimExists();
                        if (Claim_Exists == false)
                        {
                            return;
                        }
                        Hide();
                        ByClaimNum f2 = new ByClaimNum();
                        f2.Show();
                        break;
                    case "Button2":                 // Search by Last Name
                        ClaimNum = textBox1.Text;
                        Version.Claim = ClaimNum;
                        label1.Visible = false;
                        textBox1.Visible = false;
                        label5.Visible = false;
                        timer1.Stop();
                        Hide();
                        ByLastName f3 = new ByLastName();
                        f3.Show();
                        break;
                    case "Button3":                 // Search by Home Phone
                        ClaimNum = textBox1.Text;
                        Version.Claim = ClaimNum;
                        label1.Visible = false;
                        textBox1.Visible = false;
                        label5.Visible = false;
                        timer1.Stop();
                        Hide();
                        ByPhoneNum f4 = new ByPhoneNum();
                        f4.Show();
                        break;
                    case "Button4":
                        ClaimNum = textBox1.Text;
                        Version.Claim = ClaimNum;
                        label1.Visible = false;
                        textBox1.Visible = false;
                        label5.Visible = false;
                        timer1.Stop();
                        Hide();
                        ByINT_CLM_Number f14 = new ByINT_CLM_Number();
                        f14.Show();
                        break;
                    case "Button5":
                        ClaimNum = textBox1.Text;
                        Version.Claim = ClaimNum;
                        Hide();
                        BySerialNumber f5 = new BySerialNumber();
                        f5.Show();
                        break;
                    case "Button6":                 // Search by First Name
                        ClaimNum = textBox1.Text;
                        Version.Claim = ClaimNum;
                        label1.Visible = false;
                        textBox1.Visible = false;
                        label5.Visible = false;
                        timer1.Stop();
                        Hide();
                        ByFirstName f6 = new ByFirstName();
                        f6.Show();
                        break;
                    case "Button7":                 // By Manufacturer
                        if (textBox1.MaxLength <= 1)
                        {
                            textBox1.Select();
                        }
                        if (textBox1.Text.Length >= 2)
                        {
                            ClaimNum = textBox1.Text;
                            Version.Claim = ClaimNum;
                            label1.Visible = false;
                            textBox1.Visible = false;
                            label5.Visible = false;
                            timer1.Stop();
                            Hide();
                            ByManuf f7 = new ByManuf();
                            f7.Show();
                        }
                        break;
                    case "Button8":                 // Search by Work Phone
                        ClaimNum = textBox1.Text;
                        Version.Claim = ClaimNum;
                        label1.Visible = false;
                        textBox1.Visible = false;
                        label5.Visible = false;
                        timer1.Stop();
                        Hide();
                        ByPhoneWork f8 = new ByPhoneWork();
                        f8.Show();
                        break;
                    case "Button9":
                        ClaimNum = textBox1.Text;
                        Version.From = "Retrieve";
                        Version.Claim = ClaimNum;
                        label1.Visible = false;
                        textBox1.Visible = false;
                        label5.Visible = false;
                        timer1.Stop();
                        Hide();
                        ByClaimNumF5Notes f10 = new ByClaimNumF5Notes();
                        f10.Show();
                        break;
                    case "Button10":
                        Version.Claim = textBox1.Text;
                        label1.Visible = false;
                        textBox1.Visible = false;
                        label5.Visible = false;
                        timer1.Stop();
                        Hide();
                        ByStreetName f11 = new ByStreetName();
                        f11.Show();
                        break;
                    case "Button11":
                        Version.Claim = textBox1.Text;
                        label1.Visible = false;
                        textBox1.Visible = false;
                        label5.Visible = false;
                        timer1.Stop();
                        Hide();
                        ByCityName f12 = new ByCityName();
                        f12.Show();
                        break;
                    case "Button12":
                        Version.Claim = textBox1.Text;
                        label1.Visible = false;
                        textBox1.Visible = false;
                        label5.Visible = false;
                        timer1.Stop();
                        Hide();
                        ByEmailAddr f13 = new ByEmailAddr();
                        f13.Show();
                        break;
                }
                
            }
            if (e.KeyCode == Keys.Escape)
            {
                timer1.Stop();
                Hide();
                MainMenu f2 = new MainMenu();
                f2.Show();
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            label1.Text = "Enter all or part of Last Name:";
            label5.Visible = true;
            butNum = "Button2";
            label1.Visible = true;
            textBox1.Visible = true;
            textBox1.Select();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            butNum = "Button3";
            label1.Visible = true;
            label1.Text = "Enter Home phone  number";
            label5.Text = "                     Format = xxx-xxx-xxxx";
            label5.Visible = true;
            textBox1.Visible = true;
            textBox1.Select();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            butNum = "Button4";
            label1.Visible = true;
            label1.Text = "Enter Client / Dealer Inv/Clm #";
            label5.Text = "Use 'SO#' for SO#'s, CRM or a number for Store #";
            label5.Visible = true;
            textBox1.Visible = true;
            textBox1.Select();
        }

        private void RetrieveMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void RetrieveMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Stop();
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            label1.Text = "Enter all or part of First Name:";
            label5.Visible = true;
            butNum = "Button6";
            label1.Visible = true;
            textBox1.Visible = true;
            textBox1.Select();
        }

        private void Button7_Click(object sender, EventArgs e)  // By Manufacturer
        {
            label1.Text = "Enter all or part of Manufacturer:";
            label5.Visible = true;
            butNum = "Button7";
            label1.Visible = true;
            textBox1.Visible = true;
            textBox1.Select();
        }

        private void byCustomerClaimNumberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Text = "Enter Claim Number:";
            butNum = "Button1";
            label1.Visible = true;
            textBox1.Visible = true;
            textBox1.Select();
        }

        private void byCustomerLASTNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Text = "Enter all or part of Last Name:";
            label5.Visible = true;
            butNum = "Button2";
            label1.Visible = true;
            textBox1.Visible = true;
            textBox1.Select();
        }

        private void byCustomerHomePhoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            butNum = "Button3";
            label1.Visible = true;
            label1.Text = "Enter Home phone number";
            label5.Text = "                     Format = xxx-xxx-xxxx";
            label5.Visible = true;
            textBox1.Visible = true;
            textBox1.Select();
        }

        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            butNum = "Button8";
            label1.Visible = true;
            label1.Text = "Enter Home phone number";
            label5.Text = "                     Format = xxx-xxx-xxxx";
            label5.Visible = true;
            textBox1.Visible = true;
            textBox1.Select();
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            butNum = "Button8";
            label1.Visible = true;
            label1.Text = "Enter Work phone number";
            label5.Text = "                     Format = xxx-xxx-xxxx";
            label5.Visible = true;
            textBox1.Visible = true;
            textBox1.Select();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            butNum = "Button9";
            label1.Visible = true;
            label1.Text = "Enter Claim #:";
            label5.Visible = false;
            textBox1.Visible = true;
            textBox1.Select();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            butNum = "Button9";
            label1.Visible = true;
            label1.Text = "Enter Claim #:";
            label5.Visible = false;
            textBox1.Visible = true;
            textBox1.Select();
            if (textBox1.TextLength >= 6)
            {
                Hide();
                ByClaimNumF5Notes f2 = new ByClaimNumF5Notes();
                f2.Show();
            }
        }

        private void mainMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            //button1.ForeColor = Color.Gold;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            //button1.ForeColor = Color.Black;
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            //button2.ForeColor = Color.Black;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
           // button2.ForeColor = Color.Gold;
        }

        private void button3_MouseHover(object sender, EventArgs e)
        {
            //button3.ForeColor = Color.Black;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            //button3.ForeColor = Color.Gold;
        }

        private void button4_MouseHover(object sender, EventArgs e)
        {
            //button4.ForeColor = Color.Black;
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            //button4.ForeColor = Color.Yellow;
        }

        private void button5_MouseHover(object sender, EventArgs e)
        {
            //button5.ForeColor = Color.Black;
        }

        private void button5_MouseLeave(object sender, EventArgs e)
        {
            //button5.ForeColor = Color.Yellow;
        }

        private void button6_MouseHover(object sender, EventArgs e)
        {
            //button6.ForeColor = Color.Black;
        }

        private void button6_MouseLeave(object sender, EventArgs e)
        {
            //button6.ForeColor = Color.Yellow;
        }

        private void button7_MouseHover(object sender, EventArgs e)
        {
            //button7.ForeColor = Color.Black;
        }

        private void button7_MouseLeave(object sender, EventArgs e)
        {
            //button7.ForeColor = Color.Yellow;
        }

        private void button8_MouseHover(object sender, EventArgs e)
        {
            //button8.ForeColor = Color.Black;
        }

        private void button8_MouseLeave(object sender, EventArgs e)
        {
           // button8.ForeColor = Color.Yellow;
        }

        private void button9_MouseHover(object sender, EventArgs e)
        {
            //button9.ForeColor = Color.Black;
        }

        private void button9_MouseLeave(object sender, EventArgs e)
        {
           // button9.ForeColor = Color.Yellow;
        }

        private void button14_MouseHover(object sender, EventArgs e)
        {
           // button14.ForeColor = Color.Black;
        }

        private void button14_MouseLeave(object sender, EventArgs e)
        {
            //button14.ForeColor = Color.Yellow;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            label1.Text = "Enter all or part of Street" + " Name:";
            label5.Visible = true;
            butNum = "Button10";
            label1.Visible = true;
            textBox1.Visible = true;
            textBox1.Select();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            label1.Text = "Enter all or part of City" + " Name:";
            label5.Visible = true;
            butNum = "Button11";
            label1.Visible = true;
            textBox1.Visible = true;
            textBox1.Select();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            label1.Text = "Enter all or part of Email" + " Address:";
            label5.Visible = true;
            butNum = "Button12";
            label1.Visible = true;
            textBox1.Visible = true;
            textBox1.Select();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            label1.Text = "Enter Claim Number:";
            label5.Visible = false;
            butNum = "Button1";
            label1.Visible = true;
            textBox1.Visible = true;
            textBox1.Select();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            label1.Text = "Enter all or part of Last Name:";
            label5.Visible = true;
            butNum = "Button2";
            label1.Visible = true;
            textBox1.Visible = true;
            textBox1.Select();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            label1.Text = "Enter all or part of First Name:";
            label5.Visible = true;
            butNum = "Button6";
            label1.Visible = true;
            textBox1.Visible = true;
            textBox1.Select();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            butNum = "Button3";
            label1.Visible = true;
            label1.Text = "Enter Home phone  number";
            label5.Text = "                     Format = xxx-xxx-xxxx";
            label5.Visible = true;
            textBox1.Visible = true;
            textBox1.Select();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            butNum = "Button8";
            label1.Visible = true;
            label1.Text = "Enter Work phone number";
            label5.Text = "                     Format = xxx-xxx-xxxx";
            label5.Visible = true;
            textBox1.Visible = true;
            textBox1.Select();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            butNum = "Button4";
            label1.Visible = true;
            label1.Text = "Enter Client / Dealer Inv/Clm #";
            label5.Text = "Use 'SO#' for SO#'s, CRM or a number for Store #";
            label5.Visible = true;
            textBox1.Visible = true;
            textBox1.Select();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            label1.Text = "Enter all or part of Manufacturer:";
            label5.Visible = true;
            butNum = "Button7";
            label1.Visible = true;
            textBox1.Visible = true;
            textBox1.Select();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            butNum = "Button5";
            label1.Text = "Enter unit Serial Number:";
            label5.Visible = true;
            label1.Visible = true;
            textBox1.Visible = true;
            textBox1.Select();
            Version.Claim = textBox1.Text;
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            butNum = "Button9";
            label1.Visible = true;
            label1.Text = "Enter Claim #:";
            label5.Visible = false;
            textBox1.Visible = true;
            textBox1.Select();
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            label1.Text = "Enter all or part of Street" + " Name:";
            label5.Visible = true;
            butNum = "Button10";
            label1.Visible = true;
            textBox1.Visible = true;
            textBox1.Select();
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            label1.Text = "Enter all or part of City" + " Name:";
            label5.Visible = true;
            butNum = "Button11";
            label1.Visible = true;
            textBox1.Visible = true;
            textBox1.Select();
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            label1.Text = "Enter all or part of Email" + " Address:";
            label5.Visible = true;
            butNum = "Button12";
            label1.Visible = true;
            textBox1.Visible = true;
            textBox1.Select();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Hide();
            RawSearch f2 = new RawSearch();
            f2.Show();
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {

        }

        private void Button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                Hide();
                MainMenu f2 = new MainMenu();
                f2.Show();
            }
            if (e.KeyData == Keys.A)
            {
                label1.Text = "Enter Claim Number:";
                butNum = "Button1";
                label1.Visible = true;
                textBox1.Visible = true;
                textBox1.Select();
            }
            if (e.KeyData == Keys.B)
            {
                label1.Text = "Enter all or part of Last Name:";
                label5.Visible = true;
                butNum = "Button2";
                label1.Visible = true;
                textBox1.Visible = true;
                textBox1.Select();
            }
            if (e.KeyData == Keys.C)
            {
                butNum = "Button3";
                label1.Visible = true;
                label1.Text = "Enter phone number";
                label5.Text = "                     Format = xxx-xxx-xxxx";
                label5.Visible = true;
                textBox1.Visible = true;
                textBox1.Select();
            }
            if (e.KeyData == Keys.D)
            {
                Version.From = "Retrieve";
                Hide();
                ByClientDealer f5 = new ByClientDealer();
                f5.Show();
            }
            if (e.KeyData == Keys.E)
            {
                butNum = "Button5";
                label1.Text = "Enter unit Serial Number:";
                label5.Visible = true;
                label1.Visible = true;
                textBox1.Visible = true;
                textBox1.Select();
                Version.Claim = textBox1.Text;
            }
            if (e.KeyData == Keys.F)
            {
                label1.Text = "Enter all or part of First Name:";
                label5.Visible = true;
                butNum = "Button6";
                label1.Visible = true;
                textBox1.Visible = true;
                textBox1.Select();
            }
            if (e.KeyData == Keys.G)
            {
                label1.Text = "Enter all or part of Manufacturer:";
                label5.Visible = true;
                butNum = "Button7";
                label1.Visible = true;
                textBox1.Visible = true;
                textBox1.Select();
            }
            if (e.KeyData == Keys.H)
            {
                butNum = "Button8";
                label1.Visible = true;
                label1.Text = "Enter phone number";
                label5.Text = "                     Format = xxx-xxx-xxxx";
                label5.Visible = true;
                textBox1.Visible = true;
                textBox1.Select();
            }
            if (e.KeyData == Keys.I)
            {
                butNum = "Button9";
                label1.Visible = true;
                label1.Text = "Enter Claim #:";
                label5.Visible = false;
                textBox1.Visible = true;
                textBox1.Select();
            }
            if (e.KeyData == Keys.Q)
            {
                timer1.Stop();
                Hide();
                MainMenu f2 = new MainMenu();
                f2.Show();
            }
        }

        private void Button5_Click(object sender, EventArgs e)  // By Serial Number
        {
            butNum = "Button5";
            label1.Text = "Enter unit Serial Number:";
            label5.Visible = true;
            label1.Visible = true;
            textBox1.Visible = true;
            textBox1.Select();
            Version.Claim = textBox1.Text;
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
                MessageBox.Show("Error GetScreenSize:\r\n" + ex);
            }
        }
        public void GAssembLyInfo()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            versionX = fvi.FileVersion;
            Version.versionX = versionX;
            Text = "Wizard Electronics, Inc.   v" + versionX;      // Set Menu Title
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            return;
            mTimerCount++; 
            if (mTimerCount >= 501)
            {
                mTimerCount = 0;
            }
            string startTime = DateTime.Now.ToShortTimeString();
            string endTime = "17:00";
            TimeSpan duration = DateTime.Parse(endTime).Subtract(DateTime.Parse(startTime));
            var Mtime = duration.ToString(@"hh\:mm");
            label3.Text = DateTime.Now.ToShortTimeString();
            label6.ForeColor = Color.GreenYellow;
            label6.Text = "Program auto-closes in: " + Mtime.ToString() + " hh:mm";
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
                    label3.Text = "Program auto-closes in: " + Mtime1.ToString() + " minutes";
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
        }
    }
}
