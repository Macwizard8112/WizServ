using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace WizServ
{
    public partial class SRNONWARR : Form
    {
        private readonly string DatabaseFile = @"I:\\Datafile\\Control\\Database.CSV";
        private static readonly string PartsUsedInClaim = @"I:\\Datafile\\Control\\Partsused.CSV";    // This is Read only CSV
        private static readonly string Ordered = @"I:\\Datafile\\Control\\Ordered.CSV";         // This is Read only CSV
        private static readonly string PRIPARTS = @"I:\\Datafile\\Control\\part_pri.csv";       // This is Read only CSV
        private static readonly string TechNames = @"I:\\Datafile\\Control\\Tech_Names.csv";    // This is Read only CSV
        private static readonly string Product = @"I:\\Datafile\\Control\\Product.csv";         // This is Read only CSV
        public string DocNotesPath = @"I:\Datafile\DOC\Tech Notes.rtf";
        private int loopCount, foundcount;
        public string claimno = Version.Claim;
        public string from = Version.From, Mex;
        public string mTab = "\t", myNewCSVLine;
        public string mTab2 = "\t\t", mTotal;
        public decimal decimalRounded, decRounded;
        public string t = "0", s1;
        private int TickCount;
        public decimal t_price, mBOCost, partstotal, totRounded;
        private string PN;
        public bool mRendered;
        public string FROM = Version.From;
        public string SELECTEDTEXT = Version.SELECTEDTEXT;  // Pass along from Password PRG to enable editing
        public string XPRIPN, XDDESCR, XONHAND, XLOWLIMIT, XCOST, XRETAIL, XTHECOST, XTHERETAIL, XSUPPLIER, XLASTUSED, XQTY;
        public string WordSelection;
        public string Technician, TechInitials, TechNumber, ProductType;
        public decimal Hours;
        public string first, second, third, fourth, PartsProfit;
        public string Theline, lastnewmessage;
        public string PooIndex, PooPN;          // Parts on Order Index #, PN

        public SRNONWARR()
        {
            InitializeComponent();
            label16.Visible = false;
            comboBox1.Visible = false;
            comboBox2.Visible = false;
            comboBox3.Visible = false;
            textBox7.Visible = false;
            textBox8.Visible = false;
            textBox9.Visible = false;
            textBox10.Visible = false;
            label28.Visible = false;
            label29.Visible = false;
            label32.Visible = false;
            label34.Visible = false;
            timer1.Interval = 100;
            timer1.Stop();
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
            label20.Text = SELECTEDTEXT;
            claimno = Version.Claim;
            label21.Visible = false;
            label22.Visible = false;
            label23.Visible = false;
            label24.Visible = false;
            label25.Visible = false;
            label26.Visible = false;
            label27.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            textBox5.Visible = false;
            textBox6.Visible = false;
            label26.Visible = false;
            button3.Visible = false;
            button4.Visible = true;
            SetCB2();
            GetProductTypes();
            if (mRendered != true)
            {
                PartsLookupInfo();
            }
            CheckStstus();
            if (richTextBox2.Text.Length >= 1)
            {
                button2.Visible = false;
                button4.Visible = false;
                button5.Visible = false;
                label16.Visible = true;
                label16.Text = " You must remove Parts on Order before closing claim !\nDouble-click on Index # to send message to Parts Manager. ";

            }
        }

        private void RichTextBox2_DoubleClick(object sender, EventArgs e)
        {
            WordSelection = richTextBox2.SelectedText;

            if (WordSelection == PooIndex)
            {
                string message = "Do you want Parts MGR\nto remove " + PooPN + " from Claim?";
                string title = "Ordered parts still in Claim";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    richTextBox3.LoadFile(DocNotesPath);

                    richTextBox3.SelectAll();
                    var t = richTextBox3.Text;
                    richTextBox3.Text = "";
                    richTextBox3.Text = "This is a request from WizServ " + DateTime.Now.ToString() + " \n";
                    richTextBox3.Text += "Claim # " + claimno + " Please remove part " + PooPN + " ASAP\n\n";
                    richTextBox3.Text += t;

                    richTextBox3.SelectAll();
                    richTextBox3.SelectionFont = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
                    richTextBox3.ForeColor = Color.Yellow;
                    richTextBox3.BackColor = Color.Black;
                    richTextBox3.DeselectAll();
                    richTextBox3.SaveFile(DocNotesPath, RichTextBoxStreamType.RichText);
                    label6.Visible = true;
                    label6.Text = "";
                    label16.Text = " Parts Manager notified, you will be notified when " + PooPN + "\n has been removed from claim " + claimno + " ";
                }
                else
                {
                    MessageBox.Show("Claim can not be closed with open orders.");
                }
            }
        }

        private void SetCB2()
        {
            comboBox2.Items.Add("0.00");
            comboBox2.Items.Add("0.50");
            comboBox2.Items.Add("1.00");
            comboBox2.Items.Add("1.50");
            comboBox2.Items.Add("2.00");
            comboBox2.Items.Add("2.50");
            comboBox2.Items.Add("3.00");
            comboBox2.Items.Add("3.50");
            comboBox2.Items.Add("4.00");
            comboBox2.Items.Add("4.50");
            comboBox2.Items.Add("5.00");
            comboBox2.Items.Add("5.50");
            comboBox2.Items.Add("6.00");
            comboBox2.Items.Add("6.50");
            comboBox2.Items.Add("7.00");
            comboBox2.Items.Add("7.50");
            comboBox2.Items.Add("8.00");
            comboBox2.Items.Add("8.50");
            comboBox2.Items.Add("9.00");
            comboBox2.Items.Add("9.50");
            comboBox2.Items.Add("10.00");
            comboBox2.Items.Add("10.50");
            comboBox2.Items.Add("11.00");
            comboBox2.Items.Add("11.50");
            comboBox2.Items.Add("12.00");
            comboBox2.Items.Add("12.00");
        }

        private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProductType = comboBox3.SelectedItem.ToString();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Technician = comboBox1.SelectedItem.ToString();
            switch (Technician)
            {
                case "COLE":
                    TechInitials = "CH";
                    TechNumber = "1";
                    break;
                case "WALTER":
                    TechInitials = "WK";
                    TechNumber = "2";
                    break;
                case "DEREK":
                    TechInitials = "DN";
                    TechNumber = "3";
                    break;
                case "BILLY":
                    TechInitials = "BS";
                    TechNumber = "4";
                    break;
                case "NOEL":
                    TechInitials = "NA";
                    TechNumber = "5";
                    break;
                case "WILLIAM":
                    TechInitials = "WB";
                    TechNumber = "6";
                    break;
                case "KIAN":
                    TechInitials = "KF";
                    TechNumber = "7";
                    break;
                case "PARTS":
                    TechInitials = "PA";
                    TechNumber = "8";
                    break;
                case "CONSIGN":
                    TechInitials = "CS";
                    TechNumber = "9";
                    break;
                case "MATT":
                    TechInitials = "MM";
                    TechNumber = "10";
                    break;
            }

        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)     // Selected Technician name
        {
            Hours = Convert.ToDecimal(comboBox2.Text);
        }

        private void TextBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                XQTY = textBox6.Text;
                decimal cst = decimal.Parse(XCOST);
                decimal qty = decimal.Parse(textBox6.Text);
                textBox5.Text = (cst * qty).ToString("0.00");
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            button2.Visible = false;
            button4.Visible = false;
            var l = richTextBox2.Text.Length;
            if (l != 0)
            {
                button4.Visible = false;
                MessageBox.Show("There are parts still on order for claim !\nThey must be cancelled and removed first.", "Alert");
            }
            else
            {
                RenderClaim();
            }
        }

        private void RenderClaim()
        {
            GetTechNames();
            comboBox1.Visible = true;
            comboBox2.Visible = true;
            comboBox3.Visible = true;
            label28.Visible = true;
            label29.Visible = true;
            label32.Visible = true;
            textBox7.Visible = true;
            textBox8.Visible = true;
            textBox9.Visible = true;
            textBox10.Visible = true;
        }
        private void Button5_Click(object sender, EventArgs e)
        {
            label34.Visible = true;
            // Setup variables to replace
            first = textBox7.Text;
            second = textBox8.Text;
            third = textBox9.Text;
            fourth = textBox10.Text;
            if (textBox7.Text.Length == 0)
            {
                textBox7.Text = ".";
                first = textBox7.Text;
            }
            if (textBox8.Text.Length == 0)
            {
                textBox8.Text = ".";
                second = textBox8.Text;
            }
            if (textBox9.Text.Length == 0)
            {
                textBox9.Text = ".";
                third = textBox9.Text;
            }
            if (textBox10.Text.Length == 0)
            {
                textBox10.Text = ".";
                fourth = textBox10.Text;
            }
            first.Replace(",", ";");        // make sure user didn't type a comma ","
            second.Replace(",", ";");
            third.Replace(",", ";");
            fourth.Replace(",", ";");
            textBox7.Text = first;
            textBox8.Text = second;
            textBox9.Text = third;
            textBox10.Text = fourth;

            List<String> lines = new List<String>();

            if (File.Exists(DatabaseFile))
            {
                using (StreamReader reader = new StreamReader(DatabaseFile))
                {
                    String line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Contains(","))
                        {
                            String[] split = line.Split(',');
                            try
                            {                                                   // Add all other lines to sve here
                                if (split[1] == Version.Claim) // Claim # 
                                {
                                    split[47] = first;      // TextBox1
                                    split[48] = second;     // TextBox2
                                    split[49] = third;      // TextBox3
                                    split[50] = fourth;     // TextBox4
                                    line = String.Join(",", split);
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error 287: \n" + ex);
                            }
                        }
                        lines.Add(line);
                    }
                    reader.Close();
                }
                try
                {
                    using (StreamWriter writer = new StreamWriter(DatabaseFile, false))
                    {
                        foreach (String line in lines)
                            writer.WriteLine(line);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error line 109: \n" + ex);
                }
            }
        }


        public void GetProductTypes()
        {
            try
            {
                StreamReader reader = new StreamReader(Product, Encoding.GetEncoding("Windows-1252"));
                String line = reader.ReadLine();

                List<string> listA = new List<string>();
                List<string> listB = new List<string>();
                List<string> listC = new List<string>();


                loopCount = 0;

                while (!reader.EndOfStream)
                {
                    var lineRead = reader.ReadLine();
                    var values = lineRead.Split(',');

                    listA.Add(values[0]);       //  war_prd         Unused
                    listB.Add(values[1]);       //  claim_no        Claim Number
                    listC.Add(values[2]);       //  datein          Equipment Entry Date

                    if (listB[loopCount] != "code")
                    {
                        comboBox3.Items.Add(listB[loopCount]);
                    }
                    loopCount++;
                }
                reader.Close(); // Close the open file
            }
            catch (Exception ex)
            {
                Mex = ex.ToString();
                if (Mex.Contains("AccessViolationException"))
                {
                    MessageBox.Show("AccessViolationException");
                }
                if (Mex.Contains("AggregateException"))
                {
                    MessageBox.Show("AggregateException");
                }
                if (Mex.Contains("FileFormatException"))
                {
                    MessageBox.Show("FileFormatException");
                }
                // if (Mex.Contains("IndexOutOfRangeException"))
                {
                    // MessageBox.Show("IndexOutOfRangeException");
                }

                MessageBox.Show("Error 161: Sorry an error has occured: " + ex.Message);
            }
        }

        public void GetTechNames()
        {
            try
            {
                StreamReader reader = new StreamReader(TechNames, Encoding.GetEncoding("Windows-1252"));
                String line = reader.ReadLine();

                List<string> listA = new List<string>();
                List<string> listB = new List<string>();
                List<string> listC = new List<string>();
               

                loopCount = 0;

                while (!reader.EndOfStream)
                {
                    var lineRead = reader.ReadLine();
                    var values = lineRead.Split(',');

                    listA.Add(values[0]);       //  war_prd         Unused
                    listB.Add(values[1]);       //  claim_no        Claim Number
                    listC.Add(values[2]);       //  datein          Equipment Entry Date
                   
                    if (listA[loopCount] != "TECH_NAME")
                    {
                        comboBox1.Items.Add(listA[loopCount]);
                    }
                    loopCount++;
                }
                reader.Close(); // Close the open file
            }
            catch (Exception ex)
            {
                Mex = ex.ToString();
                if (Mex.Contains("AccessViolationException"))
                {
                    MessageBox.Show("AccessViolationException");
                }
                if (Mex.Contains("AggregateException"))
                {
                    MessageBox.Show("AggregateException");
                }
                if (Mex.Contains("FileFormatException"))
                {
                    MessageBox.Show("FileFormatException");
                }
               // if (Mex.Contains("IndexOutOfRangeException"))
                {
                   // MessageBox.Show("IndexOutOfRangeException");
                }

                MessageBox.Show("Error 161: Sorry an error has occured: " + ex.Message);
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                label26.Visible = true;
                TickCount++;
                if (TickCount >= 10)
                {
                    timer1.Stop();
                    label26.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception line 486:\n" + ex);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            button3.Visible = true;
            label21.Visible = true;
            textBox1.Visible = true;
            textBox2.Visible = true;
            textBox3.Visible = true;
            textBox4.Visible = true;
            textBox5.Visible = true;
            textBox6.Visible = true;
            label22.Visible = true;
            label23.Visible = true;
            label24.Visible = true;
            label25.Visible = true;
            label27.Visible = true;
            textBox1.Select();
            timer1.Stop();
        }

        private void Button3_Click(object sender, EventArgs e)  // Save new PN information:
        {
            timer1.Start();
            label21.Visible = false;
            label22.Visible = false;
            label23.Visible = false;
            label24.Visible = false;
            label25.Visible = false;
            label26.Visible = true;
            label27.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            textBox5.Visible = false;
            textBox6.Visible = false;
            XQTY = textBox6.Text;
            decimal cst = decimal.Parse(XCOST);
            decimal qty = decimal.Parse(textBox6.Text);
            switch (PN.Length)
            {
                case 1:
                    richTextBox1.Text += XQTY + "\t" + PN + "\t\t\t" + (cst * qty).ToString("0.00") + "\t\t" + XDDESCR;
                    break;
                case 2:
                    richTextBox1.Text += XQTY + "\t" + PN + "\t\t\t" + (cst * qty).ToString("0.00") + "\t\t" + XDDESCR;
                    break;
                case 3:
                    richTextBox1.Text += XQTY + "\t" + PN + "\t\t\t" + (cst * qty).ToString("0.00") + "\t\t" + XDDESCR;
                    break;
                case 4:
                    richTextBox1.Text += XQTY + "\t" + PN + "\t\t\t" + (cst * qty).ToString("0.00") + "\t\t" + XDDESCR;
                    break;
                case 5:
                    richTextBox1.Text += XQTY + "\t" + PN + "\t\t\t" + (cst * qty).ToString("0.00") + "\t\t" + XDDESCR;
                    break;
                case 6:
                    richTextBox1.Text += XQTY + "\t" + PN + "\t\t\t" + (cst * qty).ToString("0.00") + "\t\t" + XDDESCR;
                    break;
                case 7:
                    richTextBox1.Text += XQTY + "\t" + PN + "\t\t\t" + (cst * qty).ToString("0.00") + "\t\t" + XDDESCR;
                    break;
                case 8:
                    richTextBox1.Text += XQTY + "\t" + PN + "\t\t\t" + (cst * qty).ToString("0.00") + "\t\t" + XDDESCR;
                    break;
                default:
                    richTextBox1.Text += XQTY + "\t" + PN + "\t\t" + (cst * qty).ToString("0.00") + "\t\t" + XDDESCR;
                    break;
            }

            // Create save CSV here
            using (FileStream fs = new FileStream(PartsUsedInClaim, FileMode.Append, FileAccess.Write))
            {
                try
                {
                    var todayDate = DateTime.Today;
                    string strTodayUS = todayDate.ToString("MM/dd/yyyy"); // converts date to string in MM/DD/YYYY format
                    s1 = strTodayUS;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception \n" + ex);
                }
                PN = textBox1.Text;
                decimal total = decimal.Parse(XQTY) * decimal.Parse(XCOST);
                mTotal = total.ToString("0.00");
                try
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        // qty	part_no	Claim	Description	price	claim_no	cost	part_date	ppurch	InClaim
                        myNewCSVLine = XQTY + "," + PN + "," + claimno + "," + XDDESCR + "," + XCOST + "," + claimno + "," + total + "," + s1 + "," + s1 + "," + "Y";
                        sw.WriteLine(myNewCSVLine);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception line 587:\n" + ex);
                }
            }
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    PN = textBox1.Text;
                    switch (PN)
                    {
                        case "0002":
                            PN = "2";
                            break;
                        case "0003":
                            PN = "3";
                            break;
                        case "0004":
                            PN = "4";
                            break;
                        case "0005":
                            PN = "5";
                            break;
                        case "0006":
                            PN = "6";
                            break;
                        case "0008":
                            PN = "8";
                            break;
                    }
                    PrimaryPartsLookup();
                    switch (PN)
                    {
                        case "2":
                            PN = "0002";
                            break;
                        case "3":
                            PN = "0003";
                            break;
                        case "4":
                            PN = "0004";
                            break;
                        case "5":
                            PN = "0005";
                            break;
                        case "6":
                            PN = "0006";
                            break;
                        case "8":
                            PN = "0008";
                            break;
                    }
                    if (PN == "2")
                    {
                        PN = "0002";
                    }
                    textBox2.Text = XDDESCR;
                    textBox3.Text = XONHAND;
                    textBox4.Text = XSUPPLIER;
                    decimal cst = decimal.Parse(XRETAIL);
                    textBox5.Text = cst.ToString("0.00");
                    textBox6.Select();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception Line 654: \t" + ex.Message);
                }
            }
        }

        private void PrimaryPartsLookup()
        {
            try
            {
                StreamReader reader = new StreamReader(PRIPARTS);
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

                loopCount = 0;

                while (!reader.EndOfStream)
                {
                    var lineRead = reader.ReadLine();
                    var values = lineRead.Split(',');

                    listA.Add(values[0]);   // PRI_PARTNO
                    listB.Add(values[1]);   // Description
                    listC.Add(values[2]);   // on_hand
                    listD.Add(values[3]);   // low_limit
                    listE.Add(values[4]);   // cost
                    listF.Add(values[5]);   // Retail
                    listG.Add(values[6]);   // Thecost
                    listH.Add(values[7]);   // TheRetail
                    listI.Add(values[8]);   // supplier
                    listJ.Add(values[9]);   // last_used

                    if (PN == listA[loopCount])
                    {
                        XPRIPN = listA[loopCount];
                        XDDESCR = listB[loopCount];
                        XONHAND = listC[loopCount];
                        XLOWLIMIT = listD[loopCount];
                        XCOST = listE[loopCount];
                        XRETAIL = listF[loopCount];
                        XTHECOST = listG[loopCount];
                        XTHERETAIL = listH[loopCount];
                        XSUPPLIER = listI[loopCount];
                        XLASTUSED = listJ[loopCount];
                    }
                    loopCount++;
                }
                reader.Close(); // Close the open file
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 714: Sorry an error has occured: " + ex.Message);
            }
        }

        private void PartsLookupInfo()
        {
            try
            {
                StreamReader reader = new StreamReader(PartsUsedInClaim);
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
                    listC.Add(values[2]);   // Ref_no
                    listD.Add(values[3]);   // Description
                    listE.Add(values[4]);   // Price
                    listF.Add(values[5]);   // Claim_no
                    listG.Add(values[6]);   // Cost
                    listH.Add(values[7]);   // Part_Date
                    listI.Add(values[8]);   // Ppurch Date
                    listJ.Add(values[9]);   // Part in Claim
                    listK.Add(values[10]);  // Index #

                    var Apn = listB[loopCount];
                    var Apn2 = Apn.Length;
                    if (Apn.Length <= 10)
                    {
                        //Apn += "\t";
                    }
                    switch (Apn2)
                    {
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            Apn += "            ";
                            break;
                        case 4:
                            Apn += "           ";
                            break;
                        case 5:
                            Apn += "          ";
                            break;
                        case 6:
                            Apn += "         \t";
                            break;
                        case 7:
                            Apn += "       \t";
                            break;
                        case 8:
                            Apn += "       \t";
                            break;
                        case 9:
                            Apn += "      ";
                            break;
                        case 10:
                            Apn += "     ";
                            break;
                        case 11:
                            Apn += "    ";
                            break;
                        case 12:
                            Apn += "   ";
                            break;
                        case 13:
                            Apn += "  ";
                            break;
                        case 14:
                            Apn += " ";
                            break;
                        case 15:
                            break;
                        case 16:
                            break;
                        case 17:
                            break;
                        case 18:
                            break;
                        case 19:
                            break;
                        case 20:
                            break;
                    }
                    if (Apn == "2")
                    {
                        Apn = "0002\t\t";
                    }
                    if (Apn == "5")
                    {
                        Apn = "0005\t\t";
                    }

                    if (values[5] == claimno && listJ[loopCount] == "Y")    // Parts in Claim
                    {
                        if (listB[loopCount].Contains("FREIGHT"))
                        {
                            return;
                        }
                    }
                    if (values[5] == claimno && listJ[loopCount] == "N")    // Parts in Claim
                    {
                        if (listB[loopCount].Contains("FREIGHT"))
                        {
                            return;
                        }
                    }

                    if (values[5] == claimno && listJ[loopCount] == "Y")    // Parts in Claim
                    {
                        var c_price = decimal.Parse((listE[loopCount]));
                        decimal decimalRounded = Decimal.Parse(c_price.ToString("0.00"));
                        t_price = t_price + c_price;
                        totRounded = Decimal.Parse(t_price.ToString("0.00"));
                        var w_price = decimal.Parse((listG[loopCount]));
                        decimal decRounded = Decimal.Parse(w_price.ToString("0.00"));
                        if (decimalRounded.ToString().Length == 4)
                        {
                            t = "  ";
                        }
                        else
                        {
                            t = "";
                        }
                        foundcount++;
                        richTextBox1.Text = richTextBox1.Text + listA[loopCount] + "\t" + Apn + "\t" + t + decimalRounded + "\t\t" + listD[loopCount] + "\n";
                        label6.Text = "Found: " + foundcount.ToString() + " items,  Total $ " + totRounded;
                    }
                    if (values[5] == claimno && listJ[loopCount] == "N")    // Parts on Order for Claim
                    {
                        var c_price = decimal.Parse((listE[loopCount]));
                        decimal decimalRounded = Decimal.Parse(c_price.ToString("0.00"));
                        t_price = t_price + c_price;
                        decimal totRounded = Decimal.Parse(t_price.ToString("0.00"));
                        var w_price = decimal.Parse((listG[loopCount]));
                        decimal decRounded = Decimal.Parse(w_price.ToString("0.00"));
                        if (decimalRounded.ToString().Length == 4)
                        {
                            t = "  ";
                        }
                        else
                        {
                            t = "";
                        }
                        foundcount++;
                        PooIndex = listK[loopCount];
                        PooPN = listB[loopCount];
                        richTextBox2.Text = richTextBox2.Text + listK[loopCount] + "\t\t" + listA[loopCount] + "\t" + Apn + "\t" + t + decimalRounded + "\t\t" + listD[loopCount] +  "\n";
                        label6.Text = "Found: " + foundcount.ToString() + " items,  Total $ " + totRounded;
                    }
                    loopCount++;
                }
                reader.Close(); // Close the open file
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 889: Sorry an error has occured: " + ex.Message);
            }
        }

        public void CheckStstus()
        {
            try
            {
                if (Version.DatabaseIsLocked == true)
                {
                    MessageBox.Show("Database in use, retrying...");
                }
            }
            catch (Exception)
            {
                Thread.Sleep(2700);
                CheckStstus();
            }
            try
            {
                StreamReader reader = new StreamReader(DatabaseFile, Encoding.GetEncoding("Windows-1252"));
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
                List<string> listBY = new List<string>();
                List<string> listBZ = new List<string>();
                List<String> listCA = new List<string>();
                List<String> listCB = new List<string>();

                loopCount = 0;

                var csv = new StringBuilder();

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
                    listBY.Add(values[76]);
                    listBZ.Add(values[77]);
                    listCA.Add(values[78]);
                    listCB.Add(values[79]);

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
                    var mTechNum = listBA[loopCount];
                    var mTech = listBC[loopCount];
                    var mBench = listBD[loopCount];
                    var mTheTech = listAZ[loopCount];
                    var COMPLETED = listBB[loopCount];
                    var mTheNewClaimNum = listBQ[loopCount];
                    var mStatus = listBD[loopCount];
                    var mIsWarr = listBL[loopCount];
                    var mEmail = listBP[loopCount];
                    if (listBT[loopCount] != "NONE")
                    {
                        mEmail += ", " + listBT[loopCount];
                    }
                    var mEstimate = listBU[loopCount];
                    var mRush = listBX[loopCount];
                    var Est_Total = listBV[loopCount];
                    var Est_Parts = listBW[loopCount];
                    var CLOSED = listCA[loopCount];
                    var PICKUP = listCB[loopCount];

                    if (mTheNewClaimNum.Length >= 7)   // Convert new claim# to Remove the "A" prefix
                    {
                        var tt = mTheNewClaimNum;
                        var yy = mTheNewClaimNum.Length;
                        yy--;
                        var uu = tt.Substring(1, yy);
                        mTheNewClaimNum = uu;
                    }
                    if (claimno == mClaim_NO)
                    {
                        if (mStatus.Contains("RENDER"))
                        {
                            MessageBox.Show("Claim Already Rendered !");
                            mRendered = true;
                            button2.Visible = false;
                            button4.Visible = false;
                            button5.Visible = false;
                            return;
                        }
                        textBox7.Text = listAV[loopCount];
                        textBox8.Text = listAW[loopCount];
                        textBox9.Text = listAX[loopCount];
                        textBox10.Text = listAY[loopCount];
                        PartsProfit = (Int32.Parse(listS[loopCount]) / 1.5).ToString("0.00");

                    }
                    loopCount++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 1158: Sorry an error has occured: " + ex.Message);
            }
        }
    }
}
