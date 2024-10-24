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
using System.Threading;
using System.Windows.Forms;
using System.Media;
using Microsoft.Win32;

namespace WizServ
{
    public partial class _Render : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        private string ans;
        private readonly string file = @"I:\\Datafile\\Control\\Database.CSV";
        private readonly string Related = @"I:\\Datafile\\Control\\Related.CSV";
        static readonly string key = @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\lanmanserver\parameters";
        private static readonly string PartsUsed1 = @"I:\\Datafile\\Control\\Partsused.CSV";    // This is Read only CSV
        public readonly string computerDescription = (string)Registry.GetValue(key, "srvcomment", null);
        private string claim_no, fname, lname, addr, city, state, zip, hphone, wphone, a, b, c, d;
        private bool war_prd;
        private DateTime datein;
        private int loopCount, loop;
        private string IsClosed = "";
        public bool Found = false;
        private SoundPlayer Player = new SoundPlayer();
        public string IsWarranty, TheBrand, s;
        public bool IsError = false;
        private double timertik;
        public string line1, line2, line3, line4, yeardigit;
        public bool BOparts;

        public _Render()
        {
            InitializeComponent();
            GetClaimPrefix();
            this.Player.LoadCompleted += new AsyncCompletedEventHandler(Player_LoadCompleted);
            this.BackColor = Color.FromArgb(0, 132, 129);
            panel4.BackColor = Color.FromArgb(0, 132, 129);
            label34.Visible = false;
            Icon = image100;
            label57.Text = "";
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
            claim_no = Version.Claim;
            Text = "Retrieve Claim by Claim Number - Page 1";
            ClearButLabels();
            GetData();
            if (Found == false)
            {
                string message = "Claim " + claim_no + " not found.";
                string title = "Not Found:";
                MessageBox.Show(message, title);
                Hide();
                return;
            }
            timer1.Interval = 1000;
            timer1.Enabled = true;
            label32.Visible = false;
            //IsFileLocked(FileInfo file);
            SetBackColor();
            CheckforNotes();
            textBox1.Select();
            textBox1.DeselectAll();
            textBox1.Focus();
            textBox1.SelectionStart = textBox1.Text.Length;
            //Reminder();
            CheckIfOK();


        }

        private void CheckIfPartsOrdered()
        {
            PartsLookupInfo();
            if (BOparts == false)
            {
                label46.BackColor = Color.White;
                label46.ForeColor = Color.Black;
                label46.Text = " No Parts Ordered ";
                label46.BackColor = Color.White;
            }
            else
            {
                MessageBox.Show("Can't render claim\nthere are parts on order\nfor claim # " + claim_no);
                label46.BackColor = Color.Red;
                label46.ForeColor = Color.White;
                label46.Text = " Parts still on Order ";
                label46.BackColor = Color.Red;
            }
        }

        public void GetClaimPrefix()
        {
            var date = DateTime.Now.ToShortDateString();
            var len = date.Length;
            var year = date.Substring((len - 2), 2);
            yeardigit = year.Substring(0, 1);
        }

        private void Reminder()
        {
            string message = "Reminder:\nLine 1 starts with hours\n2.5 Hours, Disassembled...";
            string title = "Reminder";
            MessageBox.Show(message, title);
        }

        private void CheckforNotes()
        {
            string f5file = @"I:\Datafile\Control\Notes\" + claim_no.ToString() + "ClaimNotes.rtf";
            if (File.Exists(f5file))
            {
                label32.Visible = true;
                label32.Text = "*F5 Notes*";
                PlayAlarmSound();
            }
        }

        public void PlayAlarmSound()
        {
            this.LoadAsyncSound();      // Play sound Async
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            ByClaimNumPg2 f2 = new ByClaimNumPg2();
            f2.Show();
        }

        public void ParseDataBeforeSave()       // Replace any comma with semicolon in TextBoxes
        {
            line1 = textBox1.Text;
            line2 = textBox2.Text;
            line3 = textBox3.Text;
            line4 = textBox4.Text;
            line1 = line1.Replace(',', ';');
            line2 = line2.Replace(',', ';');
            line3 = line3.Replace(',', ';');
            line4 = line4.Replace(',', ';');
            textBox1.Text = line1;
            textBox2.Text = line2;
            textBox3.Text = line3;
            textBox4.Text = line4;
            if (textBox1.Text == "")
            {
                textBox1.Text = ".";
            }
            if (textBox2.Text == "")
            {
                textBox2.Text = ".";
            }
            if (textBox3.Text == "")
            {
                textBox3.Text = ".";
            }
            if (textBox4.Text == "")
            {
                textBox4.Text = "Thank you for choosing Wizard Electronics!";
            }
            label39.Text = "MISC. DESCRIPTION: RENDER " + DateTime.Now.ToShortDateString().ToString();
        }

        public void UpdateServicesData()
        {
            label34.Visible = true;
            claim_no = Version.Claim;
            string path = file;
            List<String> lines = new List<String>();
            ParseDataBeforeSave();

            if (File.Exists(path))
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    String line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Contains(","))
                        {
                            String[] split = line.Split(',');
                            try
                            {
                                if (split[1] == claim_no)
                                {
                                    split[46] = textBox1.Text;
                                    split[47] = textBox2.Text;
                                    split[48] = textBox3.Text;
                                    split[49] = textBox4.Text;
                                    split[53] = DateTime.Now.ToShortDateString().ToString();
                                    split[55] = "MISC. DESCRIPTION: RENDER " + DateTime.Now.ToShortDateString().ToString();
                                    line = String.Join(",", split);
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error 181: \n" + ex);
                            }
                        }
                        lines.Add(line);
                    }
                }
                try
                {
                    using (StreamWriter writer = new StreamWriter(path, false))
                    {
                        foreach (String line in lines)
                            writer.WriteLine(line);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error line 205: \n" + ex);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)      // SAVE NEW RENDER DATA
        { 

            PartsLookupInfo();
                if (BOparts == false)
                {
                    label46.BackColor = Color.White;
                    label46.ForeColor = Color.Black;
                    label46.Text = " No Parts Ordered ";
                    label46.BackColor = Color.White;
                Reminder();
                }
                else
                {
                    MessageBox.Show("Can't render claim\nthere are parts on order\nfor claim # " + claim_no);
                label46.BackColor = Color.Red;
                label46.ForeColor = Color.White;
                label46.Text = " Parts still on Order ";
                label46.BackColor = Color.Red;
            }
            
            if (textBox3.Text.Contains("PT.#"))
            {
                label46.BackColor = Color.White;
                label46.ForeColor = Color.Green;
                label46.Text = " Parts Ordered ";
                label46.BackColor = Color.White;
            }
            if (textBox4.Text.Contains("PT.#"))
            {
                label46.BackColor = Color.White;
                label46.ForeColor = Color.Green;
                label46.Text = " Parts Ordered ";
                label46.BackColor = Color.White;
            }

            if (BOparts == false)
            {
                UpdateServicesData();
                MessageBox.Show("Claim has been updated!");
            }
        }

        private void PartsLookupInfo()
        {
            BOparts = false;

            try
            {
                StreamReader reader = new StreamReader(PartsUsed1);
                String line = reader.ReadLine();

                List<string> listA = new List<string>();
                List<string> listB = new List<string>();
                List<string> listC = new List<string>();
                List<string> listD = new List<string>();
                List<string> listE = new List<string>();
                List<string> listF = new List<string>();
                List<string> listG = new List<string>();
                List<string> listH = new List<string>();
                List<string> listI = new List<string>();
                List<string> listJ = new List<string>();
                List<string> listK = new List<string>();

                loopCount = 0;

                while (!reader.EndOfStream)
                {
                    var lineRead = reader.ReadLine();
                    var values = lineRead.Split(',');

                    listA.Add(values[0]);   // Qty
                    listB.Add(values[1]);   // Part_no
                    listC.Add(values[2]);   // Ref_no / Claim_no
                    listD.Add(values[3]);   // Description
                    listE.Add(values[4]);   // Price
                    listF.Add(values[5]);   // Claim_no
                    listG.Add(values[6]);   // Cost
                    listH.Add(values[7]);   // Part_Date
                    listI.Add(values[8]);   // Ppurch Date
                    listJ.Add(values[9]);   // Part in Claim
                    listK.Add(values[10]);  // Index #

                    var xClaimNo = listC[loopCount];

                    if (claim_no == xClaimNo)
                    {
                        if (listJ[loopCount] != "Y")
                        {
                            BOparts = true;
                        }
                    }
                    loopCount++;
                }
                reader.Close(); // Close the open file
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 302: Sorry an error has occured: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Hide();
                RetrieveMenu f2 = new RetrieveMenu();
                f2.Show();
            }
            if (e.KeyCode == Keys.F2)
            {
                Hide();
                Version.From = "Retrieve1";
                PartsUsed f2 = new PartsUsed();
                f2.Show();
            }
            if (e.KeyCode == Keys.F5)
            {
                Version.Claim = claim_no;
                Version.From = "Retrieve1";
                ByClaimNumF5Notes f3 = new ByClaimNumF5Notes();
                f3.Show();
            }
        }

        private void textBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    textBox2.Select();
                    textBox2.DeselectAll();
                    textBox2.SelectionStart = textBox2.Text.Length;
                    textBox2.SelectionLength = 0;
                    break;
                case Keys.Right:
                    //action
                    break;
                case Keys.Up:
                    break;
                case Keys.Left:
                    //action
                    break;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            b = textBox2.Text;
            if (b.Length >= 83)
            {
                textBox3.Select();
            }
            if (e.KeyChar == (char)Keys.Down)
            {
                textBox3.Select();
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            c = textBox3.Text;
            if (c.Length >= 84)
            {
                textBox4.Select();
            }
            if (e.KeyChar == (char)Keys.Down)
            {
                textBox4.Select();
            }
        }

        private void textBox2_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    textBox3.Select();
                    textBox3.DeselectAll();
                    textBox3.SelectionStart = textBox3.Text.Length;
                    textBox3.SelectionLength = 0;
                    break;
                case Keys.Right:
                    //action
                    break;
                case Keys.Up:
                    textBox1.Select();
                    textBox1.DeselectAll();
                    textBox1.SelectionStart = textBox1.Text.Length;
                    textBox1.SelectionLength = 0;
                    break;
                case Keys.Left:
                    //action
                    break;
            }
        }

        private void textBox3_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    textBox4.Select();
                    textBox4.DeselectAll();
                    textBox4.SelectionStart = textBox4.Text.Length;
                    textBox4.SelectionLength = 0;
                    break;
                case Keys.Right:
                    //action
                    break;
                case Keys.Up:
                    textBox2.Select();
                    textBox2.DeselectAll();
                    textBox2.SelectionStart = textBox2.Text.Length;
                    textBox2.SelectionLength = 0;
                    break;
                case Keys.Left:
                    //action
                    break;
            }
        }

        private void textBox4_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    button2.Select();
                    break;
                case Keys.Right:
                    //action
                    break;
                case Keys.Up:
                    textBox3.Select();
                    textBox3.DeselectAll();
                    textBox3.SelectionStart = textBox3.Text.Length;
                    textBox3.SelectionLength = 0;
                    break;
                case Keys.Left:
                    //action
                    break;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            a = textBox1.Text;
            if (a.Length >= 84)
            {
                textBox2.Select();
            }
            if (e.KeyChar == (char)Keys.Down)
            {
                textBox2.Select();
            }
        }

        private void ProcessAnswer()
        {
            //label2.Text = "";
            var pass = 0;
            //string s = textBox1.Text;

            string[] subs = s.Split(' ');

            foreach (var sub in subs)
            {
                pass++;
                if (pass == 1)
                {
                    ans = sub;
                }
            }
            if (IsWarranty.StartsWith("WARRANTY"))
                {
                try
                {
                    switch (TheBrand)
                    {
                        case "AER":
                            var hours0 = decimal.Parse(ans);
                            if (hours0 > 1)
                            {
                                MessageBox.Show("AER Pays Maximum of 1 hour1\non Warranty work.\nPlease Adjust hours.");
                                IsError = true;
                                textBox1.Select();
                            }
                            break;
                        case "ALLEN & HEATH":
                            var hours1 = decimal.Parse(ans);
                            if (hours1 > 3)
                            {
                                MessageBox.Show("ALLEN & HEATH Pays Maximum of 3 hours\non Warranty work.\nPlease Adjust hours.\nIf using 3 Hours, justify all 3 hours\nin description !!");
                                IsError = true;
                                textBox1.Select();
                            }
                            break;
                        case "AMPEG/CRATE/AUDIO CE":
                            var hours2 = decimal.Parse(ans);
                            if (hours2 > 1)
                            {
                                var XMSG = "AMPEG/CRATE/AUDIO CE Pays Maximum\n of 1 hour on Warranty work.\nPlease Adjust hours.\n";
                                XMSG = XMSG + "Notify Lynnette if more hours needed.\nBEFORE WORK STARTS\n 1.5, 2.0. 2.5, 3.0 Hours etc.";
                                MessageBox.Show(XMSG);
                                IsError = true;
                                textBox1.Select();
                            }
                            break;
                        case "ASHDOWN":
                            var hours3 = decimal.Parse(ans);
                            if (hours3 > 1)
                            {
                                var xmsg = "ASHDOWN Pays Maximum\n of 1 hour on Warranty work.\nPlease Adjust hours.\n";
                                xmsg = xmsg + "Notify Lynnette if more hours needed.\nBEFORE WORK STARTS\n 1.5, 2.0. 2.5, 3.0 Hours etc.";
                                MessageBox.Show(xmsg);
                                IsError = true;
                                textBox1.Select();
                            }
                            break;
                        case "B52":
                            var hours4 = decimal.Parse(ans);
                            if (hours4 > 1)
                            {
                                MessageBox.Show("B52 Pays Maximum\n of 1 hour on Warranty work.\nPlease Adjust hours.");
                                IsError = true;
                                textBox1.Select();
                            }
                            break;
                        case "BOSS":
                            var hours5 = decimal.Parse(ans);
                            if (hours5 > 2)
                            {
                                MessageBox.Show("BOSS Pays Maximum\n of 2 hours on Warranty work.\nPlease Adjust hours.");
                                IsError = true;
                                textBox1.Select();
                            }
                            break;
                        case "CASIO. INC.":
                            var hours6 = decimal.Parse(ans);
                            if (hours6 > 2)
                            {
                                MessageBox.Show("CASIO. INC. Pays Maximum\n of 2 hours on Warranty work.\nPlease Adjust hours.");
                                IsError = true;
                                textBox1.Select();
                            }
                            break;
                        case "CERWIN VEGA":
                            var hours7 = decimal.Parse(ans);
                            if (hours7 > 1)
                            {
                                MessageBox.Show("CERWIN VEGA Pays Maximum\n of 1 hour on Warranty work.\nPlease Adjust hours.");
                                IsError = true;
                                textBox1.Select();
                            }
                            break;
                        case "COMMUNITY PRO":
                            var hours8 = decimal.Parse(ans);
                            if (hours8 > 1)
                            {
                                MessageBox.Show("COMMUNITY PRO Pays Maximum\n of 1 hour on Warranty work.\nPlease Adjust hours.");
                                IsError = true;
                                textBox1.Select();
                            }
                            break;
                        case "CRATE":
                            var hours9 = decimal.Parse(ans);
                            if (hours9 > 2)
                            {
                                MessageBox.Show("CRATE Pays Maximum\n of 2 hours on Warranty work.\nPlease Adjust hours.");
                                IsError = true;
                                textBox1.Select();
                            }
                            break;
                        case "CREST AUDIO":
                            var hours10 = decimal.Parse(ans);
                            if (hours10 > 2)
                            {
                                MessageBox.Show("CREST AUDIO Pays Maximum\n of 2 hours on Warranty work.\nPlease Adjust hours.");
                                IsError = true;
                                textBox1.Select();
                            }
                            break;
                        case "CROWN INT'L":
                            var hours11 = decimal.Parse(ans);
                            if (hours11 > 3)
                            {
                                MessageBox.Show("CASIO. INC. Pays Maximum\n of 3 hours on Warranty work.\nPlease Adjust hours.");
                                IsError = true;
                                textBox1.Select();
                            }
                            break;
                        case "DB TECH":
                            var hours12 = decimal.Parse(ans);
                            if (hours12 > 2)
                            {
                                MessageBox.Show("DB TECH Pays Maximum\n of 2 hours on Warranty work.\nPlease Adjust hours.");
                                IsError = true;
                                textBox1.Select();
                            }
                            break;
                        case "DBX":
                            var hours13 = decimal.Parse(ans);
                            if (hours13 > 1)
                            {
                                MessageBox.Show("DB TECH Pays Maximum\n of 1 hour on Warranty work.\nPlease Adjust hours.");
                                IsError = true;
                                textBox1.Select();
                            }
                            break;
                        case "DIGITECH":
                            var hours14 = decimal.Parse(ans);
                            if (hours14 > 1)
                            {
                                MessageBox.Show("DIGITECH Pays Maximum\n of 1 hour on Warranty work.\nPlease Adjust hours.");
                                IsError = true;
                                textBox1.Select();
                            }
                            break;
                        case "EDEN":
                            var hours15 = decimal.Parse(ans);
                            if (hours15 > 1)
                            {
                                MessageBox.Show("EDEN Pays Maximum\n of 1 hour on Warranty work.\nPlease Adjust hours.");
                                IsError = true;
                                textBox1.Select();
                            }
                            break;
                        case "EGNATOR":
                            var hours16 = decimal.Parse(ans);
                            if (hours16 > 1)
                            {
                                MessageBox.Show("EGNATOR Pays Maximum\n of 1 hour on Warranty work.\nPlease Adjust hours.");
                                IsError = true;
                                textBox1.Select();
                            }
                            break;
                        case "EV - BOSCH":
                            var hours17 = decimal.Parse(ans);
                            if (hours17 > 1)
                            {
                                var xmsg = "EV - BOSCH Pays Maximum\n of 1 hour on Warranty work.\nPlease Adjust hours.\n";
                                xmsg = xmsg + "Notify Lynnette if more hours needed.\nBEFORE WORK STARTS\n 1.5, 2.0. 2.5, 3.0 Hours etc.";
                                MessageBox.Show(xmsg);
                                IsError = true;
                                textBox1.Select();
                            }
                            break;
                        case "FENDER":
                            var hours18 = decimal.Parse(ans);
                            if (hours18 > 2)
                            {
                                MessageBox.Show("FENDER Pays Maximum\n of 2 hours on Warranty work.\nPlease Adjust hours.");
                                IsError = true;
                                textBox1.Select();
                            }
                            break;
                        case "FOCUSRITE":
                            var hours19 = decimal.Parse(ans);
                            if (hours19 > 2)
                            {
                                MessageBox.Show("FOCUSRITE Pays Maximum\n of 2 hours on Warranty work.\nPlease Adjust hours.");
                                IsError = true;
                                textBox1.Select();
                            }
                            break;
                        case "GALLIEN KRUEGER":
                            var hours20 = decimal.Parse(ans);
                            if (hours20 > 1)
                            {
                                var xmsg = "GALLIEN KRUEGER Pays Maximum\n of 1 hour on Warranty work.\nPlease Adjust hours.\n";
                                MessageBox.Show(xmsg);
                                IsError = true;
                                textBox1.Select();
                            }
                            break;
                        case "HARTKE":
                            var hours21 = decimal.Parse(ans);
                            if (hours21 > 2)
                            {
                                MessageBox.Show("HARTKE Pays Maximum\n of 2 hours on Warranty work.\nPlease Adjust hours.");
                                IsError = true;
                                textBox1.Select();
                            }
                            break;
                        case "SAMSON":
                            var hours210 = decimal.Parse(ans);
                            if (hours210 > 2)
                            {
                                MessageBox.Show("SAMSON Pays Maximum\n of 2 hours on Warranty work.\nPlease Adjust hours.");
                                IsError = true;
                                textBox1.Select();
                            }
                            break;
                        case "HAYDEN":
                            var hours22 = decimal.Parse(ans);
                            if (hours22 > 1)
                            {
                                var xmsg = "HAYDEN Pays Maximum\n of 1 hour on Warranty work.\nPlease Adjust hours.\n";
                                MessageBox.Show(xmsg);
                                IsError = true;
                                textBox1.Select();
                            }
                            break;
                        case "JBL PROFESSIONAL":
                            var hours23 = decimal.Parse(ans);
                            if (hours23 > 1)
                            {
                                var xmsg = "JBL PROFESSIONAL Pays Maximum\n of 1 hour on Warranty work.\nPlease Adjust hours.\n";
                                xmsg = xmsg + "Component level repair rate:\n$80 flat rate, Module swap $ 25 flat rate.";
                                MessageBox.Show(xmsg);
                                IsError = true;
                                textBox1.Select();
                            }
                            break;
                        case "KAWAI":
                            var hours24 = decimal.Parse(ans);
                            if (hours24 > 2)
                            {
                                MessageBox.Show("KAWAI Pays Maximum\n of 2 hours on Warranty work.\nPlease Adjust hours.");
                                IsError = true;
                                textBox1.Select();
                            }
                            break;
                        case "KEF":
                            var hours25 = decimal.Parse(ans);
                            if (hours25 > 2)
                            {
                                MessageBox.Show("KEF Pays Maximum\n of 2 hours on Warranty work.\nPlease Adjust hours.");
                                IsError = true;
                                textBox1.Select();
                            }
                            break;
                        case "KORG":
                            var hours26 = decimal.Parse(ans);
                            if (hours26 > 1)
                            {
                                var xmsg = "KORG Pays Maximum\n of 1 hour on Warranty work.\nPlease Adjust hours.\n";
                                MessageBox.Show(xmsg);
                                IsError = true;
                                textBox1.Select();
                            }
                            break;
                        case "KRK":
                            var hours27 = decimal.Parse(ans);
                            if (hours27 > 1)
                            {
                                var xmsg = "KRK Pays Maximum\n of 1 hour on Warranty work.\nPlease Adjust hours.\n";
                                MessageBox.Show(xmsg);
                                IsError = true;
                                textBox1.Select();
                            }
                            break;
                        case "KURZWEIL":
                            var hours28 = decimal.Parse(ans);
                            if (hours28 > 3)
                            {
                                MessageBox.Show("KURZWEIL Pays Maximum\n of 3 hours on Warranty work.\nPlease Adjust hours.");
                                IsError = true;
                                textBox1.Select();
                            }
                            break;
                        case "KUSTOM":
                            var hours29 = decimal.Parse(ans);
                            if (hours29 > 1)
                            {
                                var xmsg = "KUSTOM Pays Maximum\n of 1 hour on Warranty work.\nPlease Adjust hours.\n";
                                MessageBox.Show(xmsg);
                                IsError = true;
                                textBox1.Select();
                            }
                            break;
                        case "LINE 6":
                            var hours30 = decimal.Parse(ans);
                            if (hours30 > 1)
                            {
                                var xmsg = "LINE 6 Pays Maximum\n of 1 hour on Warranty work.\nPlease Adjust hours.\n";
                                xmsg = xmsg + "Notify Lynnette if more hours needed.\nBEFORE WORK STARTS\n 1.5, 2.0. 2.5, 3.0 Hours etc.";
                                MessageBox.Show(xmsg);
                                IsError = true;
                                textBox1.Select();
                            }
                            break;
                        case "MACKIE":
                            var hours31 = decimal.Parse(ans);
                            if (hours31 > 1)
                            {
                                var xmsg = "MACKIE Pays Maximum\n of 1 hour on Warranty work.\nPlease Adjust hours.\n";
                                xmsg = xmsg + "Notify Lynnette if more hours needed.\nBEFORE WORK STARTS\n 1.5, 2.0. 2.5, 3.0 Hours etc.";
                                MessageBox.Show(xmsg);
                                IsError = true;
                                textBox1.Select();
                            }
                            break;
                        case "MARKBASS":
                            var hours32= decimal.Parse(ans);
                            if (hours32 > 2)
                            {
                                MessageBox.Show("MARKBASS Pays Maximum\n of 2 hours on Warranty work.\nPlease Adjust hours.");
                                IsError = true;
                                textBox1.Select();
                            }
                            break;
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show("1st line MUST start with\n# of Hours i.e.: 2.0 Hours, 3.0 Hours, etc.");
                    IsError = true;
                }
                }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox1.TextLength > 0)
                {
                    s = textBox1.Text;
                    ProcessAnswer();
                    if (IsError == false)
                    {
                        textBox2.SelectAll();
                        b = textBox2.Text;
                        textBox2.Select();
                        textBox2.Select();
                        textBox2.DeselectAll();
                        textBox2.Focus();
                        textBox2.SelectionStart = textBox2.Text.Length;
                    }
                    else
                    {
                        textBox1.SelectAll();
                        a = textBox1.Text;
                        textBox1.Select();
                        textBox1.Select();
                        textBox1.DeselectAll();
                        textBox1.Focus();
                        textBox1.SelectionStart = textBox1.Text.Length;
                        IsError = false;
                    }
                }
                if (textBox1.TextLength == 0)
                {
                    MessageBox.Show("Line 1 can't be blank");
                }
            }
        }

        private void pageSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void f2PartsListToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox2.TextLength > 0)
                {
                    s = textBox2.Text;

                    textBox3.Select();
                    textBox3.DeselectAll();
                    textBox3.Focus();
                    textBox3.SelectionStart = textBox3.Text.Length;
                }
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox3.TextLength > 0)
                {
                    textBox4.Select();
                    textBox4.DeselectAll();
                    textBox4.Focus();
                    textBox4.SelectionStart = textBox4.Text.Length;
                }
            }
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox4.TextLength > 0)
                {
                    button2.Select();
                }
            }
        }

        public void LoadAsyncSound()
        {
            try
            {
                // Replace this file name with a valid file name.
                this.Player.SoundLocation = "c:\\windows\\media\\Magic.wav";
                this.Player.LoadAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error loading sound");
            }
        }

        // This is the event handler for the LoadCompleted event.
        void Player_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (Player.IsLoadCompleted)
            {
                try
                {
                    this.Player.Play();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error playing sound");
                }
            }
        }

        private void ByClaimNum_MouseUp(object sender, MouseEventArgs e)
        {
            Hide();
            ByClaimNumPg2 f2 = new ByClaimNumPg2();
            f2.Show();
        }
        public void PlaySimpleSound()
        {
            SoundPlayer simpleSound = new SoundPlayer(@"c:\Windows\Media\chimes.wav");
            simpleSound.Play();
        }

        private void SetBackColor()
        {
            // foreach (var lbl in Controls.OfType<Label>())
            //     lbl.Hide(); 
            label15.BackColor = Color.FromArgb(0, 132, 129);
            label16.BackColor = Color.FromArgb(0, 132, 129);
            label17.BackColor = Color.FromArgb(0, 132, 129);
            label18.BackColor = Color.FromArgb(0, 132, 129);
            label19.BackColor = Color.FromArgb(0, 132, 129);
            label20.BackColor = Color.FromArgb(0, 132, 129);
            label21.BackColor = Color.FromArgb(0, 132, 129);
            label22.BackColor = Color.FromArgb(0, 132, 129);
            label23.BackColor = Color.FromArgb(0, 132, 129);
            label24.BackColor = Color.FromArgb(0, 132, 129);
            label25.BackColor = Color.FromArgb(0, 132, 129);
            label26.BackColor = Color.FromArgb(0, 132, 129);
            label27.BackColor = Color.FromArgb(0, 132, 129);
            label28.BackColor = Color.FromArgb(0, 132, 129);
            label29.BackColor = Color.FromArgb(0, 132, 129);
            label30.BackColor = Color.FromArgb(0, 132, 129);
            label31.BackColor = Color.FromArgb(0, 132, 129);
            label37.BackColor = Color.FromArgb(0, 132, 129);
            label38.BackColor = Color.FromArgb(0, 132, 129);
            label39.BackColor = Color.FromArgb(0, 132, 129);
            label40.BackColor = Color.FromArgb(0, 132, 129);
            label43.BackColor = Color.FromArgb(0, 132, 129);
            label45.BackColor = Color.FromArgb(0, 132, 129);
        }

        private void ClearButLabels()
        {
            label6.Text = "";
            label7.Text = "";
            label8.Text = "";
            label9.Text = "";
            label10.Text = "";
            label22.Text = "";
            label23.Text = "";
            label24.Text = "";
            label25.Text = "";
            label26.Text = "";
            label27.Text = "";
        }


        public void GetData()
        {
            try
            {
                StreamReader reader = new StreamReader(file, Encoding.GetEncoding("Windows-1252"));
                String line = reader.ReadLine();

                List<string> listA = new List<string>();
                List<string> listB = new List<string>();
                List<string> listC = new List<string>();
                List<string> listD = new List<string>();
                List<string> listE = new List<string>();
                List<string> listF = new List<string>();
                List<string> listG = new List<string>();
                List<string> listH = new List<string>();
                List<string> listI = new List<string>();
                List<string> listJ = new List<string>();
                List<string> listK = new List<string>();
                List<string> listL = new List<string>();
                List<string> listM = new List<string>();
                List<string> listN = new List<string>();
                List<string> listO = new List<string>();
                List<string> listP = new List<string>();
                List<string> listQ = new List<string>();
                List<string> listR = new List<string>();
                List<string> listS = new List<string>();
                List<string> listT = new List<string>();
                List<string> listU = new List<string>();
                List<string> listV = new List<string>();
                List<string> listW = new List<string>();
                List<string> listX = new List<string>();
                List<string> listY = new List<string>();
                List<string> listZ = new List<string>();
                List<string> listAA = new List<string>();
                List<string> listAB = new List<string>();
                List<string> listAC = new List<string>();
                List<string> listAD = new List<string>();
                List<string> listAE = new List<string>();
                List<string> listAF = new List<string>();
                List<string> listAG = new List<string>();
                List<string> listAH = new List<string>();
                List<string> listAI = new List<string>();
                List<string> listAJ = new List<string>();
                List<string> listAK = new List<string>();
                List<string> listAL = new List<string>();
                List<string> listAM = new List<string>();
                List<string> listAN = new List<string>();
                List<string> listAO = new List<string>();
                List<string> listAP = new List<string>();
                List<string> listAQ = new List<string>();
                List<string> listAR = new List<string>();
                List<string> listAS = new List<string>();
                List<string> listAT = new List<string>();
                List<string> listAU = new List<string>();
                List<string> listAV = new List<string>();
                List<string> listAW = new List<string>();
                List<string> listAX = new List<string>();
                List<string> listAY = new List<string>();
                List<string> listAZ = new List<string>();
                List<string> listBA = new List<string>();
                List<string> listBB = new List<string>();
                List<string> listBC = new List<string>();
                List<string> listBD = new List<string>();
                List<string> listBE = new List<string>();
                List<string> listBF = new List<string>();
                List<string> listBG = new List<string>();
                List<string> listBH = new List<string>();
                List<string> listBI = new List<string>();
                List<string> listBJ = new List<string>();
                List<string> listBK = new List<string>();
                List<string> listBL = new List<string>();
                List<string> listBM = new List<string>();
                List<string> listBN = new List<string>();
                List<string> listBO = new List<string>();
                List<string> listBP = new List<string>();
                List<string> listBQ = new List<string>();
                List<string> listBR = new List<string>();
                List<string> listBS = new List<string>();
                List<string> listBT = new List<string>();
                List<string> listBU = new List<string>();
                List<string> listBV = new List<string>();
                List<string> listBW = new List<string>();
                List<string> listBX = new List<string>();

                loopCount = 0;

                while (!reader.EndOfStream)
                {
                    var lineRead = reader.ReadLine();
                    var values = lineRead.Split(',');

                    listA.Add(values[0]);       //  war_prd         Unused
                    listB.Add(values[1]);       //  claim_no        Claim Number
                    listC.Add(values[2]);       //  datein          Equipment Entry Date
                    listD.Add(values[3]);       //  fname           Customer First Name
                    listE.Add(values[4]);       //  lname           Customer Last Name
                    listF.Add(values[5]);       //  addr            Customer Address
                    listG.Add(values[6]);       //  city            Customer City
                    listH.Add(values[7]);       //  state           Customer State (2 char)
                    listI.Add(values[8]);       //  zip             Customer Zip Code XXXXX-XXXX
                    listJ.Add(values[9]);       //  hphone          Home Phone #
                    listK.Add(values[10]);      //  wphone          Work Phone #
                    listL.Add(values[11]);      //  prob_compl      Problem Complaint
                    listM.Add(values[12]);      //  brand           Manuf Brand
                    listN.Add(values[13]);      //  serv_no
                    listO.Add(values[14]);      //  model
                    listP.Add(values[15]);      //  Serial_no
                    listQ.Add(values[16]);      //  Total Estimate  $ $
                    listR.Add(values[17]);      //  Lab_Est         $ $
                    listS.Add(values[18]);      //  Part Estimate   $ $
                    listT.Add(values[19]);      //  Actual Cost     $ $
                    listU.Add(values[20]);      //  Deposit         $ $
                    listV.Add(values[21]);      //  Deposit Date
                    listW.Add(values[22]);      //  Postcard
                    listX.Add(values[23]);      //  Part_Prof       $ $
                    listY.Add(values[24]);      //  Profit          $ $
                    listZ.Add(values[25]);      //  Other Info
                    listAA.Add(values[26]);     //  Other Estimate  $ $
                    listAB.Add(values[27]);     //  Tax             $ $
                    listAC.Add(values[28]);     //  war_stat                Warranty Status
                    listAD.Add(values[29]);     //  purch_date              Purchase Date for Warranty Claim
                    listAE.Add(values[30]);     //  fthr_exp1               Further Explination C/C line 2
                    listAF.Add(values[31]);     //  frth_exp2               Further Explination C/C line 3
                    listAG.Add(values[32]);     //  Access                  Paid by Cash or Card
                    listAH.Add(values[33]);     //  DLV_Stat                Cust Pickup or OnSite Service
                    listAI.Add(values[34]);     //  Dname                   Dealer Name
                    listAJ.Add(values[35]);     //  Daddr                   Dealer Address
                    listAK.Add(values[36]);     //  DCity                   Dealer City
                    listAL.Add(values[37]);     //  DState                  Dealer State
                    listAM.Add(values[38]);     //  DZip                    Dealer Zip Code
                    listAN.Add(values[39]);     //  Dphone                  Dealer Phone Number
                    listAO.Add(values[40]);     //  TVorStereo              Skip
                    listAP.Add(values[41]);     //  Repr_cat                Number ? Column
                    listAQ.Add(values[42]);     //  Serv_Perf               Number ? Column
                    listAR.Add(values[43]);     //  Service                 Number ? Column
                    listAS.Add(values[44]);     //  Toj_Total               Claim Repair in Hours
                    listAT.Add(values[45]);     //  War_Note                Claim Status (Warr,Non-Warr, Parts Ordered, etc)
                    listAU.Add(values[46]);     //  Tech_Serv1              4 lines of what was reapired
                    listAV.Add(values[47]);     //  Tech_Serv2              4 lines of what was reapired
                    listAW.Add(values[48]);     //  Tech_Serv3              4 lines of what was reapired
                    listAX.Add(values[49]);     //  Tech_Serv4              4 lines of what was reapired
                    listAY.Add(values[50]);     //  Tech_ID                 2 letters of Tech Name
                    listAZ.Add(values[51]);     //  Tech                    Tech Name (COLE, DAVID, CONNER, etc)
                    listBA.Add(values[52]);     //  Tech_NO                 Tech ID Num (1 = Cole, 3 = David, etc)
                    listBB.Add(values[53]);     //  DTE_Compl               Date Complete
                    listBC.Add(values[54]);     //  DTE_Closed              Service Render Date
                    listBD.Add(values[55]);     //  Status                  On Bench, Parts Ordered, etc
                    listBE.Add(values[56]);     //  Comment                 Warehouse Location (A1, F2, G3, etc)
                    listBF.Add(values[57]);     //  Deal_No                 Dealer Name, School Name, etc
                    listBG.Add(values[58]);     //  Narda                   P or '.' - Ask Cole
                    listBH.Add(values[59]);     //  Distname                . XX XXX or Ship Date (We shipped unit)
                    listBI.Add(values[60]);     //  Distcode                Freight, Estimate, Recall or '.'
                    listBJ.Add(values[61]);     //  Product                 List of Model Types (Mixer, Powered Spkr, etc)
                    listBK.Add(values[62]);     //  Auth_Code               Tech Name (Cole, David, etc)
                    listBL.Add(values[63]);     //  Refb_Code               Warranty, Non-Warranty
                    listBM.Add(values[64]);     //  Microwave               Unknown Date - Ask Cole
                    listBN.Add(values[65]);     //  Estimate                ESTIMATE or NONE if requested Estimate
                    listBO.Add(values[66]);     //  Dealer_Num              Dealer Number or 999
                    listBP.Add(values[67]);     //  Cust_Extn               Unknown - Ask Cole
                    listBQ.Add(values[68]);     //  Claim_Num               'A' Claim Number A210403
                    listBR.Add(values[69]);     //  Company                 Company Name or N/A
                    listBS.Add(values[70]);     //  Real_Claim              Unused (Old new claim #)
                    listBT.Add(values[71]);     //  Email                   Customer/Dealer Email Address
                    listBU.Add(values[72]);     //  EST_YN                  Estimate Yes / No
                    listBV.Add(values[73]);     //  EST_TOTAL               Estimate Total $
                    listBW.Add(values[74]);     //  EST_PARTS               Estimate Parts $
                    listBX.Add(values[75]);     //  Rush                    Rush Y or N


                    var mWarr = listA[loopCount];
                    var mClaim_NO = listB[loopCount];
                    var mDate_IN = listC[loopCount];
                    var mFname = listD[loopCount];
                    var mLname = listE[loopCount];
                    var mAddr = listF[loopCount];
                    var mCity = listG[loopCount];
                    var mState = listH[loopCount];
                    var mZip = listI[loopCount];
                    var mHphone = listJ[loopCount];
                    var mWPhone = listK[loopCount];
                    var mProblem = listL[loopCount];
                    var mBrand = listM[loopCount];
                    var mServNo = listN[loopCount];
                    var mModel = listO[loopCount];
                    var mSerial = listP[loopCount];
                    var mWarranty = listBL[loopCount];
                    var mFthr_exp1 = listAE[loopCount];
                    var mFthr_exp2 = listAF[loopCount];
                    var mTS1 = listAU[loopCount];
                    var mTS2 = listAV[loopCount];
                    var mTS3 = listAW[loopCount];
                    var mts4 = listAX[loopCount];
                    var mComp = listBB[loopCount];
                    var mTech = listBC[loopCount];
                    var mBench = listBD[loopCount];
                    var mTheTech = listAZ[loopCount];
                    var mTheNewClaimNum = listBQ[loopCount];
                    var mIsWarr = listBL[loopCount];
                    var mEmail = listBT[loopCount];
                    var mEstimate = listBU[loopCount];
                    var mRush = listBX[loopCount];

                    if (mTheNewClaimNum.Length >= 7)   // Convert new claim# to Remove the "A" prefix
                    {
                        var tt = mTheNewClaimNum;
                        var yy = mTheNewClaimNum.Length;
                        yy = yy - 1;
                        var uu = tt.Substring(1, yy);
                        mTheNewClaimNum = uu;
                    }

                    if (claim_no.Length == 6)
                    {
                        if (mClaim_NO == claim_no)
                        {
                            Found = true;
                            label6.Text = mFname + " " + mLname;
                            label7.Text = mAddr;
                            label8.Text = mCity + ", " + mState + " " + mZip;
                            label9.Text = yeardigit + claim_no;
                            label10.Text = listBJ[loopCount];
                            label11.Text = mHphone;
                            label14.Text = mWPhone;
                            label22.Text = mBrand;

                            var parsedDate = DateTime.Parse(mComp);
                            if (parsedDate.Month == 1)
                            {
                                mComp = "0" + mComp;
                            }
                          
                            if (parsedDate.Day <= 9)
                            {
                                var t = mComp.Substring(3, 1);
                                mComp = mComp.Substring(0, 2) + "/0" + mComp.Substring(3, 1) + @"/" + parsedDate.Year.ToString();
                            }
                            label26.Text = mComp;   // Completed Date
                            TheBrand = mBrand;
                            if (listBU[loopCount] != "B")
                            {
                                decimal d4 = decimal.Parse(listBV[loopCount]);
                                decimal d5 = decimal.Parse(listBW[loopCount]);
                                if (computerDescription.Contains("TECH"))
                                {
                                    if (mEstimate == "Y")
                                    {
                                        label54.Text = "Estimate: " + "Pending";    // Convert text to decimal w/ $
                                    }
                                    if (mEstimate == "A")
                                    {
                                        label54.Text = "Estimate: " + "Approved";    // Convert text to decimal w/ $
                                    }
                                    if (mEstimate == "N")
                                    {
                                        label54.Text = "Estimate: " + "Not Requested";   // Convert text to decimal w/ $
                                    }
                                    if (mEstimate == "_")
                                    {
                                        label54.Text = "Estimate: " + "DECLINED-REASSEMBLE";    // Convert text to decimal w/ $
                                    }
                                }
                                else
                                {
                                    if (mEstimate == "Y")
                                    {
                                        var hh = d4.ToString("0.00");
                                        hh = hh.Replace(",", ";");
                                        var jj = d5.ToString("0.00");
                                        jj = jj.Replace(",", ";");
                                        label54.Text = "Estimate: $" + hh + " Parts: $" + jj + " Pending";    // Convert text to decimal w/ $
                                    }
                                    if (mEstimate == "A")
                                    {
                                        var hh = d4.ToString("0.00");
                                        hh = hh.Replace(",", ";");
                                        var jj = d5.ToString("0.00");
                                        jj = jj.Replace(",", ";");
                                        label54.Text = "Estimate: $" + hh + " Parts: $" + jj + " Approved";    // Convert text to decimal w/ $
                                    }
                                    if (mEstimate == "N")
                                    {
                                        label54.Text = "Estimate: Not Requested";    // Convert text to decimal w/ $
                                    }
                                    if (mEstimate == "_")
                                    {
                                        var hh = d4.ToString("0.00");
                                        hh = hh.Replace(",", ";");
                                        var jj = d5.ToString("0.00");
                                        jj = jj.Replace(",", ";");
                                        label54.Text = "Estimate: $" + hh + " Parts: $" + jj + " DECLINED";    // Convert text to decimal w/ $
                                    }

                                }
                            }
                            else
                            {
                                label54.Text = "";
                            }
                            label23.Text = mModel;
                            label24.Text = mSerial;
                            Version.MMS = mBrand + " Model: " + mModel + " Serial: " + mSerial;
                            Version.Make = mBrand;
                            Version.Model = mModel;
                            Version.Serial = mSerial;
                            label25.Text = mDate_IN;
                            label28.Text = mWarranty + ", " + mProblem;
                            label29.Text = mFthr_exp1;
                            label55.Text = mFthr_exp2;
                            label30.Text = "Email: ";
                            label43.Text = mEmail;

                            textBox1.Text = mTS1;
                            textBox2.Text = mTS2;
                            textBox3.Text = mTS3;
                            textBox4.Text = mts4;

                            label38.Text = mWarranty;
                            IsWarranty = mWarranty;
                            var mEstimate2 = "";
                            if (mEstimate == "Y")
                            {
                                mEstimate2 = "Yes";
                            }
                            else
                            {
                                mEstimate2 = "No";
                            }
                            label50.Text = "Estimate: ";
                            if (mEstimate.Contains("Y") || mEstimate.Contains("A"))
                            {
                                label52.ForeColor = Color.Red;
                                label52.BackColor = Color.White;
                            }
                            else
                            {
                                label52.ForeColor = Color.White;
                                label52.BackColor = Color.Black;
                            }
                            if (mEstimate == "Y")
                            {
                                label52.Text = "Yes";
                            }
                            if (mEstimate == "A")
                            {
                                label52.Text = "Yes";
                            }
                            if (mEstimate == "_")
                            {
                                label52.Text = "Dec";
                            }
                            if (mEstimate == "N")
                            {
                                label52.Text = "No";
                            }

                            var mRush2 = "";
                            if (mRush == "Y")
                            {
                                mRush2 = "Yes";
                            }
                            else
                            {
                                mRush2 = "No";
                            }
                            label51.Text = "Rush Claim: ";
                            if (mRush2 == "Yes")
                            {
                                label53.ForeColor = Color.Red;
                                label53.BackColor = Color.White;
                            }
                            else
                            {
                                label53.ForeColor = Color.White;
                                label53.BackColor = Color.Black;
                            }
                            label53.Text = mRush2;
                            if (mWarranty.Contains("RECALL") || mIsWarr == "WARRANTY")
                            {
                                label49.Text = " RECALL ";
                            }
                            if (mIsWarr.Contains("RECALL"))
                            {
                                label49.Text += "PARTS ONLY No Labor";
                            }
                            label39.Text = mBench;
                            if (mBench.StartsWith("MISC. DESCRIPTION: RENDER"))
                            {
                                MessageBox.Show("Already in render status.\nSave is disabled.\nUnrender to modify.");
                                button2.Visible = false;
                                textBox1.Enabled = false;
                                textBox2.Enabled = false;
                                textBox3.Enabled = false;
                                textBox4.Enabled = false;
                            }
                            else
                            {
                                
                            }
                            label48.Text = mTheNewClaimNum.ToString();
                            if (mBench.Contains("SERVICE RENDERED"))
                            {
                                label47.ForeColor = Color.Red;
                                label47.Text = "CLOSED";
                                Text = Text + "  CLOSED CLAIM !";
                                //label49.Text = "CLOSED CLAIM";
                            }
                            else
                            {
                                label47.ForeColor = Color.Green;
                                label47.Text = "Active Claim";
                            }
                            if (listBN[loopCount] == "ESTIMATE")
                            {
                                label45.Text = "Estimate:"; // Yes
                                label56.Text = "Yes";
                                label56.BackColor = Color.Red;
                                label56.ForeColor = Color.White;
                            }
                            else
                            {
                                label45.Text = "Estimate:";  // No
                                label56.Text = "No";
                                label56.BackColor = Color.Black;
                                label56.ForeColor = Color.White;
                            }
                            //label45.Text = "Estimate: " + listBN[loopCount];
                            label40.Text = "Technician: " + mTheTech;
                            if (listBE[loopCount] == "FC")
                            {
                                listBE[loopCount] = "Front Counter";
                            }
                            label41.Text = listBE[loopCount];
                            if (textBox3.Text.Contains("PT#") || textBox4.Text.Contains("PT#"))
                            {
                                label46.BackColor = Color.White;
                                label46.ForeColor = Color.Green;
                                label46.Text = " Parts Ordered ";
                                label46.BackColor = Color.White;
                            }
                            else
                            {
                                label46.Text = "No Parts Ordered";
                            }
                            var mxRush = "";
                            if (mRush == "Y")
                            {
                                mxRush = "Yes";
                            }
                            else
                            {
                                mxRush = "No";
                            }
                            Found = true;
                            richTextBox1.Text = richTextBox1.Text + "************************************\tDate In: " + mDate_IN + "    CLAIM # " + mClaim_NO + "\n";
                            richTextBox1.Text = richTextBox1.Text + "* Wizard Electronics, Inc.         *\tProdcut: " + listBJ[loopCount] + "\n";
                            richTextBox1.Text = richTextBox1.Text + "* 554 Deering Road Northwest       *\tBrand:   " + mBrand + "\n";
                            richTextBox1.Text = richTextBox1.Text + "* Atlanta, GA 30309                *\tModel:   " + mModel + "\n";
                            richTextBox1.Text = richTextBox1.Text + "* (404)325-4891 Fax (404)325-4175  *\tSerial#: " + mSerial + "\n";
                            richTextBox1.Text = richTextBox1.Text + "************************************\tShelf Location: " + listBE[loopCount] + " ## Rush Claim: " + mxRush + " ##\n";
                            richTextBox1.Text = richTextBox1.Text + "\n";
                            richTextBox1.Text = richTextBox1.Text + "Customer Name:    " + mFname + ", " + mLname + "\t\t" + listAH[loopCount] + "\n";
                            richTextBox1.Text = richTextBox1.Text + "Customer Address: " + mAddr + "\n";
                            richTextBox1.Text = richTextBox1.Text + "City, State, Zip: " + mCity + ", " + mState + " " + mZip + " " + "Home : " + mHphone + " Work: " + mWPhone + "\n";
                            richTextBox1.Text = richTextBox1.Text + "*****************************************************************************\n";
                            richTextBox1.Text = richTextBox1.Text + "Client/Dealer name: " + listAI[loopCount] + "\tPhone: " + listAN[loopCount] + "\n";
                            richTextBox1.Text = richTextBox1.Text + "Address:            " + listAJ[loopCount] + "\t\tInvoice/Claim # " + listBF[loopCount] + "\n";
                            richTextBox1.Text = richTextBox1.Text + "City, State, Zip:   " + listAK[loopCount] + " " + listAL[loopCount] + "  " + listAM[loopCount] + "\n";
                            richTextBox1.Text = richTextBox1.Text + "*****************************************************************************\n";
                            richTextBox1.Text = richTextBox1.Text + "Unit Status is: " + listBL[loopCount] + "\n";
                            Version.Warranty = mWarranty;
                            richTextBox1.Text = richTextBox1.Text + "\tTechnical Services: $ " + listQ[loopCount] + "\n";
                            richTextBox1.Text = richTextBox1.Text + "\t             Parts: $  " + listS[loopCount] + "\n";
                            richTextBox1.Text = richTextBox1.Text + "\t------------------------------------" + "\n";
                            var d = Convert.ToDouble(listQ[loopCount]);
                            var f = Convert.ToDouble(listS[loopCount]);
                            var total = d + f;
                            var tax = total * .095;
                            var newtotal = total + tax;
                            var k = 0.00m;
                            if (newtotal > 0)
                            {
                                IsClosed = "This is a Closed Claim";
                            }
                            else
                            {
                                IsClosed = "This is an Open Claim";
                            }
                            richTextBox1.Text = richTextBox1.Text + "\t             Total: $ " + total.ToString() + "\n\n";
                            richTextBox1.Text = richTextBox1.Text + "No warranty repairs W/O Sales Receipt/RA# at drop off. If NOT warranty,";
                            richTextBox1.Text = richTextBox1.Text + "EST Diagnostic Fee will apply if repair declined. Items left over 10 days,";
                            richTextBox1.Text = richTextBox1.Text + "add $ 1.00/Day storage fee.\n";
                            richTextBox1.Text = richTextBox1.Text + "*****************************************************************************\n";
                            richTextBox1.Text = richTextBox1.Text + "Problem: " + mProblem + "\n"; ;
                            richTextBox1.Text = richTextBox1.Text + "Problem: " + mFthr_exp1 + "\n";
                            richTextBox1.Text = richTextBox1.Text + "Problem: " + mFthr_exp2 + "\n";
                            richTextBox1.Text = richTextBox1.Text + "Email:   " + mEmail + "\n";
                            richTextBox1.Text = richTextBox1.Text + "*****************************************************************************\n";
                            richTextBox1.Text = richTextBox1.Text + "Technical Services Rendered:\n";
                            richTextBox1.Text = richTextBox1.Text + listAU[loopCount] + "\n";
                            richTextBox1.Text = richTextBox1.Text + listAV[loopCount] + "\n";
                            richTextBox1.Text = richTextBox1.Text + listAW[loopCount] + "\n";
                            richTextBox1.Text = richTextBox1.Text + "*****************************************************************************\n";
                            richTextBox1.Text = richTextBox1.Text + "Materials Used:                               *\n";
                            richTextBox1.Text = richTextBox1.Text + "                                              *    Totals:\n";
                            richTextBox1.Text = richTextBox1.Text + "QTY    Part Number     Description            *    \n";
                            richTextBox1.Text = richTextBox1.Text + "_____________________________________________ *    Services      $ " + d.ToString("0.##") + "\n";
                            richTextBox1.Text = richTextBox1.Text + "_____________________________________________ *    Parts         $  " + f.ToString("0.##") + "\n";
                            richTextBox1.Text = richTextBox1.Text + "_____________________________________________ *    Other         $   " + k.ToString("0.##") + "\n";
                            richTextBox1.Text = richTextBox1.Text + "_____________________________________________ *    Tax           $  " + tax.ToString("0.##") + "\n";
                            richTextBox1.Text = richTextBox1.Text + "_____________________________________________ *    Down Payment  $  65.00\n";
                            richTextBox1.Text = richTextBox1.Text + "_____________________________________________ *    ========================\n";
                            richTextBox1.Text = richTextBox1.Text + "_____________________________________________ *    Grand Total   $ " + newtotal.ToString("0.##") + "\n";
                            richTextBox1.Text = richTextBox1.Text + "*****************************************************************************\n";
                            richTextBox1.Text = richTextBox1.Text + "Items left over 45 days will be sold. I authorize service as specified.\n";
                            richTextBox1.Text = richTextBox1.Text + "Rush Charge is $ 50.00 in addition to repair charges. \n";
                            richTextBox1.Text = richTextBox1.Text + "Payment must be Cash/Bank Card\n\n";
                            richTextBox1.Text = richTextBox1.Text + "Signed:______________________________________   \tDate: " + DateTime.Now.ToShortDateString() + "\n\n";
                            richTextBox1.Text = richTextBox1.Text + IsClosed + "\n";
                            loop++;
                        }
                        //loopCount++;
                    }

                    //reader.Close(); // Close the open file
                    if (mTheNewClaimNum == claim_no)
                    {
                        Found = true;
                        label6.Text = mFname + " " + mLname;
                        label7.Text = mAddr;
                        label8.Text = mCity + ", " + mState + " " + mZip;
                        label9.Text = yeardigit + claim_no;
                        label10.Text = listBJ[loopCount];
                        label11.Text = mHphone;
                        label14.Text = mWPhone;
                        label22.Text = mBrand;
                        TheBrand = mBrand;
                        label23.Text = mModel;
                        label24.Text = mSerial;
                        Version.MMS = mBrand + " Model: " + mModel + " Serial: " + mSerial;
                        Version.Make = mBrand;
                        Version.Model = mModel;
                        Version.Serial = mSerial;
                        label25.Text = mDate_IN;
                        label28.Text = mWarranty + ", " + mProblem;
                        label29.Text = mFthr_exp1;
                        label30.Text = "Email: ";
                        label43.Text = "&" + mFthr_exp2;
                        textBox1.Text = mTS1;
                        textBox2.Text = mTS2;
                        textBox3.Text = mTS3;
                        textBox4.Text = mts4;
                        label38.Text = mWarranty;
                        IsWarranty = mWarranty;
                        if (mWarranty.Contains("RECALL") || mIsWarr == "WARRANTY")
                        {
                            label49.Text = " RECALL ";
                        }
                        if (mIsWarr.Contains("RECALL"))
                        {
                            label49.Text = label49.Text + "PARTS ONLY No Labor";
                        }
                        label39.Text = mBench;
                        label48.Text = mTheNewClaimNum.ToString();
                        if (mBench.Contains("SERVICE RENDERED"))
                        {
                            label47.ForeColor = Color.Red;
                            label47.Text = "CLOSED";
                            Text = Text + "  CLOSED CLAIM !";
                            label49.Text = "CLOSED CLAIM";
                        }
                        else
                        {
                            label47.ForeColor = Color.Green;
                            label47.Text = "Open Claim";
                        }
                        label45.Text = "Estimate: " + listBN[loopCount];
                        label40.Text = "Technician: " + mTheTech;
                        if (listBE[loopCount] == "FC")
                        {
                            listBE[loopCount] = "Front Counter";
                        }
                        label41.Text = listBE[loopCount];
                        if (textBox4.Text.Contains("PT#"))
                        {
                            label46.BackColor = Color.White;
                            label46.ForeColor = Color.Green;
                            label46.Text = " Parts Ordered ";
                            label46.BackColor = Color.White;
                        }
                        else
                        {
                            label46.Text = "No Parts Ordered";
                        }
                        richTextBox1.Text = richTextBox1.Text + "************************************\tDate In: " + mDate_IN + "    CLAIM # " + mClaim_NO + "\n";
                        richTextBox1.Text = richTextBox1.Text + "* Wizard Electronics, Inc.         *\tProdcut: " + listBJ[loopCount] + "\n";
                        richTextBox1.Text = richTextBox1.Text + "* 554 Deering Road Northwest       *\tBrand:   " + mBrand + "\n";
                        richTextBox1.Text = richTextBox1.Text + "* Atlanta, GA 30309                *\tModel:   " + mModel + "\n";
                        richTextBox1.Text = richTextBox1.Text + "* (404)325-4891 Fax (404)325-4175  *\tSerial#: " + mSerial + "\n";
                        richTextBox1.Text = richTextBox1.Text + "************************************\tShelf Location: " + listBE[loopCount] + "\n";
                        richTextBox1.Text = richTextBox1.Text + "\n";
                        richTextBox1.Text = richTextBox1.Text + "Customer Name:    " + mFname + ", " + mLname + "\t\t" + listAH[loopCount] + "\n";
                        richTextBox1.Text = richTextBox1.Text + "Customer Address: " + mAddr + "\n";
                        richTextBox1.Text = richTextBox1.Text + "City, State, Zip: " + mCity + ", " + mState + " " + mZip + " " + "Home : " + mHphone + " Work: " + mWPhone + "\n";
                        richTextBox1.Text = richTextBox1.Text + "*****************************************************************************\n";
                        richTextBox1.Text = richTextBox1.Text + "Client/Dealer name: " + listAI[loopCount] + "\tPhone: " + listAN[loopCount] + "\n";
                        richTextBox1.Text = richTextBox1.Text + "Address:            " + listAJ[loopCount] + "\t\tInvoice/Claim # " + listBF[loopCount] + "\n";
                        richTextBox1.Text = richTextBox1.Text + "City, State, Zip:   " + listAK[loopCount] + " " + listAL[loopCount] + "  " + listAM[loopCount] + "\n";
                        richTextBox1.Text = richTextBox1.Text + "*****************************************************************************\n";
                        richTextBox1.Text = richTextBox1.Text + "Unit Status is: " + listBL[loopCount] + "\n";
                        Version.Warranty = mWarranty;
                        if (listQ[loopCount].Length <= 6)
                        {
                            richTextBox1.Text = richTextBox1.Text + "\tTechnical Services: $  " + listQ[loopCount] + "\n";
                        }
                        else
                        {
                            richTextBox1.Text = richTextBox1.Text + "\tTechnical Services: $ " + listQ[loopCount] + "\n";
                        }
                        richTextBox1.Text = richTextBox1.Text + "\tTechnical Services: $ " + listQ[loopCount] + "\n";
                        richTextBox1.Text = richTextBox1.Text + "\t             Parts: $ " + listS[loopCount] + "\n";
                        richTextBox1.Text = richTextBox1.Text + "\t------------------------------------" + "\n";
                        var d = Convert.ToDouble(listQ[loopCount]);
                        var f = Convert.ToDouble(listS[loopCount]);
                        var total = d + f;
                        var tax = total * .095;
                        var newtotal = total + tax;
                        var k = 0.00m;
                        if (newtotal > 0)
                        {
                            IsClosed = "This is a Closed Claim";
                        }
                        else
                        {
                            IsClosed = "This is an Open Claim";
                        }
                        richTextBox1.Text = richTextBox1.Text + "\t             Total: $ " + total.ToString() + "\n\n";
                        richTextBox1.Text = richTextBox1.Text + "No warranty repairs W/O Sales Receipt/RA# at drop off. If NOT warranty,";
                        richTextBox1.Text = richTextBox1.Text + "EST Diagnostic Fee will apply if repair declined. Items left over 10 days,";
                        richTextBox1.Text = richTextBox1.Text + "add $ 1.00/Day storage fee.\n";
                        richTextBox1.Text = richTextBox1.Text + "*****************************************************************************\n";
                        richTextBox1.Text = richTextBox1.Text + "Problem: " + mProblem + "\n"; ;
                        richTextBox1.Text = richTextBox1.Text + "Problem: " + mFthr_exp1 + "\n";
                        richTextBox1.Text = richTextBox1.Text + "Problem: " + mFthr_exp2 + "\n";
                        richTextBox1.Text = richTextBox1.Text + "Email:   " + mEmail + "\n";
                        richTextBox1.Text = richTextBox1.Text + "*****************************************************************************\n";
                        richTextBox1.Text = richTextBox1.Text + "Technical Services Rendered:\n";
                        richTextBox1.Text = richTextBox1.Text + listAU[loopCount] + "\n";
                        richTextBox1.Text = richTextBox1.Text + listAV[loopCount] + "\n";
                        richTextBox1.Text = richTextBox1.Text + listAW[loopCount] + "\n";
                        richTextBox1.Text = richTextBox1.Text + listAX[loopCount] + "\n";
                        richTextBox1.Text = richTextBox1.Text + "*****************************************************************************\n";
                        richTextBox1.Text = richTextBox1.Text + "Materials Used:                               *\n";
                        richTextBox1.Text = richTextBox1.Text + "                                              *    Totals:\n";
                        richTextBox1.Text = richTextBox1.Text + "QTY    Part Number     Description            *    \n";
                        richTextBox1.Text = richTextBox1.Text + "_____________________________________________ *    Services      $ " + d.ToString("0.##") + "\n";
                        richTextBox1.Text = richTextBox1.Text + "_____________________________________________ *    Parts         $  " + f.ToString("0.##") + "\n";
                        richTextBox1.Text = richTextBox1.Text + "_____________________________________________ *    Other         $   " + k.ToString("0.##") + "\n";
                        richTextBox1.Text = richTextBox1.Text + "_____________________________________________ *    Tax           $  " + tax.ToString("0.##") + "\n";
                        richTextBox1.Text = richTextBox1.Text + "_____________________________________________ *    Down Payment  $  65.00\n";
                        richTextBox1.Text = richTextBox1.Text + "_____________________________________________ *    ========================\n";
                        richTextBox1.Text = richTextBox1.Text + "_____________________________________________ *    Grand Total   $ " + newtotal.ToString("0.##") + "\n";
                        richTextBox1.Text = richTextBox1.Text + "*****************************************************************************\n";
                        richTextBox1.Text = richTextBox1.Text + "Items left over 45 days will be sold. I authorize service as specified.\n";
                        richTextBox1.Text = richTextBox1.Text + "Rush Charge is $ 50.00 in addition to repair charges. \n";
                        richTextBox1.Text = richTextBox1.Text + "Payment must be Cash/Bank Card\n\n";
                        richTextBox1.Text = richTextBox1.Text + "Signed:______________________________________   \tDate: " + DateTime.Now.ToShortDateString() + "\n\n";
                        richTextBox1.Text = richTextBox1.Text + IsClosed + "\n";
                        loop++;
                    }

                    loopCount++;
                }
                reader.Close(); // Close the open file
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 1683: Sorry an error has occured: " + ex.Message);
            }
            CheckIfPartsOrdered();
        }

        private void CheckIfOK()
        {
            if (BOparts == true)
            {
                button2.Visible = false;
                textBox1.ReadOnly = true;
                textBox2.ReadOnly = true;
                textBox3.ReadOnly = true;
                textBox4.ReadOnly = true;
            }
            else
            {
                button2.Visible = true;
                textBox1.ReadOnly = false;
                textBox2.ReadOnly = false;
                textBox3.ReadOnly = false;
                textBox4.ReadOnly = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timertik = timertik + 1.1;
            if (label34.Visible == true)
            {
                bool isInt = timertik == (int)timertik;
                if (isInt = true)
                {
                    label34.Visible = false;
                }
            }
        }
    }
}
