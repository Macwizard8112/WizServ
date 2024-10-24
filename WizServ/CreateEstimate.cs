using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Printing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
/*
 * Lines 423-444 Added line about Parts costs being good for 5 days
 * modify for new claim 300876 rates
 * 11/17/23 Added checkbox for claims that went from Warranty to Non-Warranty (Overdriven, etc)
 * 12/27/2023 Updated Estimate data to Databse
*/

namespace WizServ
{
    public partial class CreateEstimate : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        private readonly string Database = @"I:\Datafile\Control\Database.CSV";              // This is Read only CSV
        private readonly string Estimates = @"I:\Datafile\Mel\Estimates.CSV";                // This is Read only CSV
        private readonly string Vendors = @"I:\Datafile\Control\Vendors_Purchase.CSV";       // This is Read only CSV
        public decimal PC0 = 0.00m, PC_0 = 0.00m;
        public decimal PC1 = 0.00m, PC_1 = 0.00m;
        public decimal PC2 = 0.00m, PC_2 = 0.00m;
        public decimal PC3 = 0.00m, PC_3 = 0.00m;
        public decimal PC4 = 0.00m, PC_4 = 0.00m;
        public decimal PC5 = 0.00m, PC_5 = 0.00m;
        public decimal PC6 = 0.00m, PC_6 = 0.00m;
        public decimal PC7 = 0.00m, PC_7 = 0.00m;
        public decimal PC8 = 0.00m, PC_8 = 0.00m;
        public decimal PC9 = 0.00m, PC_9 = 0.00m;
        public decimal sh1 = 0.00m, ps1 = 0.00m;
        public decimal NewPctDisc = 0.00m;
        public decimal mLaborHours, mLaborTotal, mlabor;
        public decimal GrandTotal = 0.00m, GrandTotal1 = 0.00m;
        private int loopCount, ndx0, ndx1, ndx2, comIndex;
        public string claim_no, FIRST, LAST, EMAIL, mwarr, sentDate, Body, mms;
        public decimal ShopCost = 0.00m, PartsShip = 0.00m;
        public decimal GATax;
        public decimal GATaxTotal = 0.00m;
        public decimal HourRate = 0.00m;
        public decimal xPT = 0.00m;
        public decimal WarrtoNonWarr = 65.00m;
        public decimal mPartsCosts;
        public decimal MUSICSTORE = 0.00m;
        public decimal mShopCost;
        public decimal mPartsShip;
        public decimal OLDRATE = 1.3m;
        public decimal NEWRATE = 1.30m;
        public string mCB1, mCB2, mCB3, mCB4, mCB5, mCB6, mCB7, mCB8, mCB9;
        public string mV1, mV2, mV3, mV4, mV5, mV6, mV7, mV8, mV9;
        public string mCB10, mCB11, mCB12, mCB13, mCB14, mCB15, mCB16, mCB17, mCB18;
        public bool IsNewPrice = false;
        public bool IsValid;
        public int IsValid2;
        public string SecEmail;
        public decimal TheRate;
        public bool found = false;
        private string message;
        public string xPartsTotal, xShopSupplies, xPartsShipping, xLaborHours, xGrandTotal;
        public bool IsRendered;

        public bool IsBodyHtml { get; set; }

        public CreateEstimate()
        {
            InitializeComponent();
            IsRendered = false;
            label13.Text = " Set these rates BEFORE \n Entering costs! ↓↓↓↓";
            IsBodyHtml = true;
            GATax = Convert.ToDecimal(textBox39.Text);
            SetMultiplierMessages();
            SetMarkupPercent();
            Icon = image100;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
            AdjustTax();
            timer1.Interval = 1000;
            timer1.Enabled = true;
            timer1.Start();
            label14.Visible = true;
            label16.Visible = true;
            label11.Visible = false;
            label12.Visible = false;
            textBox1.SelectAll();
            linkLabel1.Text = "Send Estimate";
            SetScreenSize();
            //GetCombos();
            //SetupCB10Plus();
            checkBox1.Text = "$" + textBox41.Text + " or $" + textBox42.Text + "/HR";
            checkBox2.Text = "Music Store " + textBox40.Text + "% off";
            textBox2.SelectAll();
        }

        private void SetMultiplierMessages()
        {
            label17.Text = "1.3X";
            label24.Text = "1.3X";
            label25.Text = "1.3X";
            label26.Text = "1.3X";
            label27.Text = "1.3X";
            label28.Text = "1.3X";
            label29.Text = "1.3X";
            label30.Text = "1.3X";
            label31.Text = "1.3X";
        }

        private void SetLowerMultiplierMessages()
        {
            textBox29.Text = "1.3";
            textBox29.BackColor = Color.Red;
            textBox29.ForeColor = Color.White;
            label17.ForeColor = Color.Red;
            label24.ForeColor = Color.Red;
            label25.ForeColor = Color.Red;
            label26.ForeColor = Color.Red;
            label27.ForeColor = Color.Red;
            label28.ForeColor = Color.Red;
            label29.ForeColor = Color.Red;
            label30.ForeColor = Color.Red;
            label31.ForeColor = Color.Red;
            label17.BackColor = Color.White;
            label24.BackColor = Color.White;
            label25.BackColor = Color.White;
            label26.BackColor = Color.White;
            label27.BackColor = Color.White;
            label28.BackColor = Color.White;
            label29.BackColor = Color.White;
            label30.BackColor = Color.White;
            label31.BackColor = Color.White;
            label17.Text = "1.3X";
            label24.Text = "1.3X";
            label25.Text = "1.3X";
            label26.Text = "1.3X";
            label27.Text = "1.3X";
            label28.Text = "1.3X";
            label29.Text = "1.3X";
            label30.Text = "1.3X";
            label31.Text = "1.3X";
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == false)
            {
                WarrtoNonWarr = 0.00m;
            }
            if (checkBox3.Checked == true)
            {
                WarrtoNonWarr = 65.00m;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedItem)
            {
                case "POWERED SPEAKRS":
                    if (IsNewPrice == false)
                    {
                        textBox41.Text = "110.00";
                        textBox42.Text = "125.00";
                        textBox41.BackColor = Color.LightGreen;
                        textBox42.BackColor = Color.White;
                    }
                    else
                    {
                        textBox41.Text = "120.00";
                        textBox42.Text = "135.00";
                        textBox41.BackColor = Color.LightGreen;
                        textBox42.BackColor = Color.White;
                    }
                    break;
                case "KBD/E-DRUMS/SEQ":
                    if (IsNewPrice == false)
                    {
                        textBox41.Text = "110.00";
                        textBox42.Text = "125.00";
                        textBox41.BackColor = Color.LightGreen;
                        textBox42.BackColor = Color.White;
                    }
                    else
                    {
                        textBox41.Text = "120.00";
                        textBox42.Text = "135.00";
                        textBox41.BackColor = Color.LightGreen;
                        textBox42.BackColor = Color.White;
                    }
                    break;
                case "POWER AMPLIFIER":
                    if (IsNewPrice == false)
                    {
                        textBox41.Text = "110.00";
                        textBox42.Text = "125.00";
                        textBox41.BackColor = Color.LightGreen;
                        textBox42.BackColor = Color.White;
                    }
                    else
                    {
                        textBox41.Text = "120.00";
                        textBox42.Text = "135.00";
                        textBox41.BackColor = Color.LightGreen;
                        textBox42.BackColor = Color.White;
                    }
                    break;
                case "16+ CH MIXERS":
                    if (IsNewPrice == false)
                    {
                        textBox41.Text = "110.00";
                        textBox42.Text = "125.00";
                        textBox41.BackColor = Color.LightGreen;
                        textBox42.BackColor = Color.White;
                    }
                    else
                    {
                        textBox41.Text = "120.00";
                        textBox42.Text = "135.00";
                        textBox41.BackColor = Color.LightGreen;
                        textBox42.BackColor = Color.White;
                    }
                    break;
                case "TUBE AMPLIFIER":
                    if (IsNewPrice == false)
                    {
                        textBox41.Text = "110.00";
                        textBox42.Text = "125.00";
                        textBox41.BackColor = Color.LightGreen;
                        textBox42.BackColor = Color.White;
                    }
                    else
                    {
                        textBox41.Text = "120.00";
                        textBox42.Text = "135.00";
                        textBox41.BackColor = Color.LightGreen;
                        textBox42.BackColor = Color.White;
                    }
                    break;
                case "DIGI MIXER/REC":
                    if (IsNewPrice == false)
                    {
                        textBox41.Text = "110.00";
                        textBox42.Text = "125.00";
                        textBox41.BackColor = Color.LightGreen;
                        textBox42.BackColor = Color.White;
                    }
                    else
                    {
                        textBox41.Text = "120.00";
                        textBox42.Text = "135.00";
                        textBox41.BackColor = Color.LightGreen;
                        textBox42.BackColor = Color.White;
                    }
                    break;
                case "SPEAKERS":
                    if (IsNewPrice == false)
                    {
                        textBox41.Text = "110.00";
                        textBox42.Text = "125.00";
                        textBox41.BackColor = Color.LightGreen;
                        textBox42.BackColor = Color.White;
                    }
                    else
                    {
                        textBox41.Text = "120.00";
                        textBox42.Text = "135.00";
                        textBox41.BackColor = Color.LightGreen;
                        textBox42.BackColor = Color.White;
                    }
                    break;
                case "SMALL MIXERS":
                    if (IsNewPrice == false)
                    {
                        textBox41.Text = "110.00";
                        textBox42.Text = "125.00";
                        textBox41.BackColor = Color.LightGreen;
                        textBox42.BackColor = Color.White;
                    }
                    else
                    {
                        textBox41.Text = "120.00";
                        textBox42.Text = "135.00";
                        textBox41.BackColor = Color.LightGreen;
                        textBox42.BackColor = Color.White;
                    }
                    break;
                case "EFX PROCESR/EQ":
                    if (IsNewPrice == false)
                    {
                        textBox41.Text = "110.00";
                        textBox42.Text = "125.00";
                        textBox41.BackColor = Color.LightGreen;
                        textBox42.BackColor = Color.White;
                    }
                    else
                    {
                        textBox41.Text = "120.00";
                        textBox42.Text = "135.00";
                        textBox41.BackColor = Color.LightGreen;
                        textBox42.BackColor = Color.White;
                    }
                    break;
                case "MICS/WIRLS/INST":
                    if (IsNewPrice == false)
                    {
                        textBox41.Text = "110.00";
                        textBox42.Text = "125.00";
                        textBox41.BackColor = Color.LightGreen;
                        textBox42.BackColor = Color.White;
                    }
                    else
                    {
                        textBox41.Text = "120.00";
                        textBox42.Text = "135.00";
                        textBox41.BackColor = Color.LightGreen;
                        textBox42.BackColor = Color.White;
                    }
                    break;
                case "SS GUITAR AMPS":
                    if (IsNewPrice == false)
                    {
                        textBox41.Text = "110.00";
                        textBox42.Text = "125.00";
                        textBox41.BackColor = Color.LightGreen;
                        textBox42.BackColor = Color.White;
                    }
                    else
                    {
                        textBox41.Text = "120.00";
                        textBox42.Text = "135.00";
                        textBox41.BackColor = Color.LightGreen;
                        textBox42.BackColor = Color.White;
                    }
                    break;
                case "ANALOG TAPE DCK":
                    if (IsNewPrice == false)
                    {
                        textBox41.Text = "110.00";
                        textBox42.Text = "125.00";
                        textBox41.BackColor = Color.LightGreen;
                        textBox42.BackColor = Color.White;
                    }
                    else
                    {
                        textBox41.Text = "120.00";
                        textBox42.Text = "135.00";
                        textBox41.BackColor = Color.LightGreen;
                        textBox42.BackColor = Color.White;
                    }
                    break;
                case "CD PLAYER/BURNR":
                    if (IsNewPrice == false)
                    {
                        textBox41.Text = "110.00";
                        textBox42.Text = "125.00";
                        textBox41.BackColor = Color.LightGreen;
                        textBox42.BackColor = Color.White;
                    }
                    else
                    {
                        textBox41.Text = "120.00";
                        textBox42.Text = "135.00";
                        textBox41.BackColor = Color.LightGreen;
                        textBox42.BackColor = Color.White;
                    }
                    break;
                case "POWERED SPEAKR":
                    if (IsNewPrice == false)
                    {
                        textBox41.Text = "110.00";
                        textBox42.Text = "125.00";
                        textBox41.BackColor = Color.LightGreen;
                        textBox42.BackColor = Color.White;
                    }
                    else
                    {
                        textBox41.Text = "120.00";
                        textBox42.Text = "135.00";
                        textBox41.BackColor = Color.LightGreen;
                        textBox42.BackColor = Color.White;
                    }
                    break;
                case "TRNTABLE/FT.PDL":
                    if (IsNewPrice == false)
                    {
                        textBox41.Text = "110.00";
                        textBox42.Text = "125.00";
                        textBox41.BackColor = Color.LightGreen;
                        textBox42.BackColor = Color.White;
                    }
                    else
                    {
                        textBox41.Text = "120.00";
                        textBox42.Text = "135.00";
                        textBox41.BackColor = Color.LightGreen;
                        textBox42.BackColor = Color.White;
                    }
                    break;
                case "PRO-AMPLIFERS":
                    if (IsNewPrice == false)
                    {
                        textBox41.Text = "110.00";
                        textBox42.Text = "125.00";
                        textBox41.BackColor = Color.LightGreen;
                        textBox42.BackColor = Color.White;
                    }
                    else
                    {
                        textBox41.Text = "120.00";
                        textBox42.Text = "135.00";
                        textBox41.BackColor = Color.LightGreen;
                        textBox42.BackColor = Color.White;
                    }
                    break;
                case "MISCELLANEOUS":
                    if (IsNewPrice == false)
                    {
                        textBox41.Text = "110.00";
                        textBox42.Text = "125.00";
                        textBox41.BackColor = Color.LightGreen;
                        textBox42.BackColor = Color.White;
                    }
                    else
                    {
                        textBox41.Text = "120.00";
                        textBox42.Text = "135.00";
                        textBox41.BackColor = Color.LightGreen;
                        textBox42.BackColor = Color.White;
                    }
                    break;
                case "PREAMP":
                    if (IsNewPrice == false)
                    {
                        textBox41.Text = "110.00";
                        textBox42.Text = "125.00";
                        textBox41.BackColor = Color.LightGreen;
                        textBox42.BackColor = Color.White;
                    }
                    else
                    {
                        textBox41.Text = "120.00";
                        textBox42.Text = "135.00";
                        textBox41.BackColor = Color.LightGreen;
                        textBox42.BackColor = Color.White;
                    }
                    break;
                default:
                    if (IsNewPrice == false)
                    {
                        textBox41.Text = "110.00";
                        textBox42.Text = "125.00";
                        textBox41.BackColor = Color.LightGreen;
                        textBox42.BackColor = Color.White;
                    }
                    else
                    {
                        textBox41.Text = "120.00";
                        textBox42.Text = "135.00";
                        textBox41.BackColor = Color.LightGreen;
                        textBox42.BackColor = Color.White;
                    }
                    break;
            }
        }



        private void CreateEstimate_Load(object sender, EventArgs e)
        {

        }

        private void textBox29_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox29.Text.Contains("1.5"))
                {
                    SetMarkupPercent();
                    UpdateMarkup();
                    NEWRATE = Convert.ToDecimal("1.30");
                    OLDRATE = Convert.ToDecimal(textBox29.Text);
                    if (textBox29.Text != "1.30")
                    {
                        if (textBox29.Text != "1.5")
                        {
                            NEWRATE = Convert.ToDecimal("1.30");
                            SetMarkupPercentOther();
                            UpdateMarkup();
                        }
                    }
                }
                if (textBox29.Text.Contains("1.3"))
                {
                    SetMarkupPercent();
                    UpdateMarkup();
                    NEWRATE = Convert.ToDecimal("1.30");
                    OLDRATE = Convert.ToDecimal(textBox29.Text);
                    if (textBox29.Text != "1.30")
                    {
                        if (textBox29.Text != "1.5")
                        {
                            NEWRATE = Convert.ToDecimal("1.30");
                            SetMarkupPercentOther();
                            UpdateMarkup();
                        }
                    }
                }
            }
            UpdateMarkup();
        }

        private void UpdateMarkup()
        {
            var line1 = Convert.ToDecimal(textBox2.Text) * Convert.ToDecimal("1.3");
            textBox16.Text = line1.ToString("$0.00");
            var h = Convert.ToDecimal(textBox2.Text);
            var t = Convert.ToDecimal(textBox29.Text);
            if ((h * t) >= 250m)
            {
                NewPctDisc = Convert.ToDecimal(textBox2.Text) * Convert.ToDecimal("1.3");
                textBox16.Text = NewPctDisc.ToString("$0.00");
                textBox29.Text = "1.30";
            }
            var line2 = Convert.ToDecimal(textBox3.Text) * Convert.ToDecimal(textBox29.Text);
            textBox17.Text = line1.ToString("$0.00");
            var line3 = Convert.ToDecimal(textBox4.Text) * Convert.ToDecimal(textBox29.Text);
            textBox18.Text = line1.ToString("$0.00");
            var line4 = Convert.ToDecimal(textBox5.Text) * Convert.ToDecimal(textBox29.Text);
            textBox19.Text = line1.ToString("$0.00");
            var line5 = Convert.ToDecimal(textBox6.Text) * Convert.ToDecimal(textBox29.Text);
            textBox20.Text = line1.ToString("$0.00");
            var line6 = Convert.ToDecimal(textBox7.Text) * Convert.ToDecimal(textBox29.Text);
            textBox21.Text = line1.ToString("$0.00");
            var line7 = Convert.ToDecimal(textBox8.Text) * Convert.ToDecimal(textBox29.Text);
            textBox22.Text = line1.ToString("$0.00");
            var line8 = Convert.ToDecimal(textBox9.Text) * Convert.ToDecimal(textBox29.Text);
            textBox23.Text = line1.ToString("$0.00");
            var line9 = Convert.ToDecimal(textBox10.Text) * Convert.ToDecimal(textBox29.Text);
            textBox24.Text = line1.ToString("$0.00");
        }

        private void textBox40_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var yy = Convert.ToDecimal(textBox40.Text);
                MUSICSTORE = yy / 100;
                if (textBox40.TextLength == 2)
                {
                    textBox40.Text += ".00";
                }
                checkBox2.Text = "Music Store " + yy.ToString("0.00") + "% off";
            }
        }
        private void SetMarkupPercentOther()
        {
            TheRate = Convert.ToDecimal(textBox29.Text);
            TheRate = 1.3m;
            var rate = TheRate.ToString();
            label17.Text = rate + "x";
            label24.Text = rate + "x";
            label25.Text = rate + "x";
            label26.Text = rate + "x";
            label27.Text = rate + "x";
            label28.Text = rate + "x";
            label29.Text = rate + "x";
            label30.Text = rate + "x";
            label31.Text = rate + "x";
        }


        private void SetMarkupPercent()
        {
            label17.Text = "1.3x";
            label24.Text = "1.3x";
            label25.Text = "1.3x";
            label26.Text = "1.3x";
            label27.Text = "1.3x";
            label28.Text = "1.3x";
            label29.Text = "1.3x";
            label30.Text = "1.3x";
            label31.Text = "1.3x";
        }

        private void SetMarkupPercentOld()
        {
            label17.Text = "1.3x";
            label24.Text = "1.3x";
            label25.Text = "1.3x";
            label26.Text = "1.3x";
            label27.Text = "1.3x";
            label28.Text = "1.3x";
            label29.Text = "1.3x";
            label30.Text = "1.3x";
            label31.Text = "1.3x";
        }

        private void textBox41_KeyDown(object sender, KeyEventArgs e)   // Analog Hourly Rate $
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox41.Text.Contains("70"))
                {
                    textBox41.Text = "70.00";
                    textBox42.Text = "80.00";
                    textBox12.Text = "15.00";
                    textBox29.Text = "1.3";
                    SetMarkupPercentOld();
                }
                if (textBox41.Text.Contains("80"))
                {
                    textBox41.Text = "80.00";
                    textBox42.Text = "90.00";
                    textBox12.Text = "15.00";
                    textBox29.Text = "1.3";
                    SetMarkupPercentOld();
                }
                else
                {
                    textBox12.Text = "20.00";
                }
                if (textBox41.Text.Contains("110"))
                {
                    textBox41.Text = "110.00";
                    textBox42.Text = "125.00";
                    textBox12.Text = "20.00";
                    SetMarkupPercent();
                }
                checkBox1.Text = "$" + textBox41.Text + " or $" + textBox42.Text + "/HR";
                var t = Convert.ToDecimal(textBox41.Text);
                HourRate = t;
            }
        }

        private void textBox42_KeyDown(object sender, KeyEventArgs e)   // Digital Hourly Rate $
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox42.Text.Contains("80"))
                {
                    textBox41.Text = "80.00";
                    textBox42.Text = "90.00";
                    textBox12.Text = "15.00";
                    textBox29.Text = "1.3";
                    SetMarkupPercentOld();
                }
                if (textBox42.Text.Contains("90"))
                {
                    textBox41.Text = "80.00";
                    textBox12.Text = "15.00";
                    textBox29.Text = "1.3";
                    SetMarkupPercentOld(); ;
                }
                else
                {
                    textBox12.Text = "20.00";
                }
                if (textBox42.Text.Contains("125"))
                {
                    textBox41.Text = "110.00";
                    textBox42.Text = "125.00";
                    textBox12.Text = "20.00";
                    SetMarkupPercent();
                }
                checkBox1.Text = "$" + textBox41.Text + " or $" + textBox42.Text + "/HR";
                var t = Convert.ToDecimal(textBox42.Text);
                HourRate = t;
            }
            textBox2.SelectAll();
        }

        private void textBox39_KeyDown(object sender, KeyEventArgs e)   // GA Tax Rate %
        {
            var tttt = textBox39.Text;
            label14.Text = "Enter GA Tax (" + tttt + "%)";
            label6.Text = "GA Tax (" + tttt + "%)";
            //label15.Text = "GA Tax (" + tttt + "%)";
            GATax = Convert.ToDecimal(textBox39.Text);
        }

        private void SetScreenSize()
        {
            Size = new Size(520, 125);
            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                          (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
            textBox2.SelectAll();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Hide();
            MainUtilitiesMenu f1 = new MainUtilitiesMenu();
            f1.Show();
        }

        private void AdjustTax()
        {
            var tttt = textBox39.Text;
            label14.Text = "Enter GA Tax (" + tttt + "%)";
            label6.Text = "GA Tax (" + tttt + "%)";
            //label15.Text = "GA Tax (" + tttt + "%)";
            var yy = Convert.ToDecimal(textBox40.Text);
            checkBox2.Text = "Music Store " + yy.ToString("0.00") + "% off";
            GATax = Convert.ToDecimal(textBox39.Text) / 100;
        }

        private void textBox11_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                textBox11.Text = "11";
                var t = textBox11.Text;
                var k1 = Convert.ToInt32("1");
                var k = Convert.ToDecimal(textBox11.Text) * k1;
                ShopCost = Convert.ToDecimal(k);
                textBox11.Text = ShopCost.ToString();

                GrandTotal += ShopCost;
                if (GrandTotal <= 999.99m)
                {
                    if (checkBox3.Checked == true)
                    {
                        GrandTotal += WarrtoNonWarr;
                        textBox15.Text = GrandTotal.ToString("0.00");
                    }
                    else
                    {
                        textBox15.Text = GrandTotal.ToString("0.00");
                    }
                }
                else
                {
                    if (checkBox3.Checked == true)
                    {
                        GrandTotal += WarrtoNonWarr;
                        textBox15.Text = GrandTotal.ToString("0.00");
                    }
                    else
                    {
                        textBox15.Text = GrandTotal.ToString("0.00");
                    }
                }
                var tttax = Convert.ToDecimal(textBox39.Text);
                GATax = tttax / 100;
                textBox14.Text = (GrandTotal * GATax).ToString("0.00");
                GATaxTotal = (GrandTotal * GATax);
                textBox12.Select();
            }
        }

        public void UpdateServicesData()            // Update Database with Estimate information
        {
            List<String> lines = new List<String>();

            if (File.Exists(Database))
            {
                using (StreamReader reader = new StreamReader(Database))
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
                                    var t = mPartsCosts.ToString("0.00");
                                    var k = mPartsCosts.ToString("0.00");
                                    var h = xGrandTotal.Replace(",", "");   // Make sure no Commas are in text.
                                    h = h.Replace("$", "");
                                    var y = textBox15.Text;
                                    var u = y.Replace(",", "");             // Make sure no Commas are in text.
                                    string msg1 = "ESTIMATE " + u + "  PARTS $ " + t + " Hours: " + textBox13.Text;
                                    split[16] = h;      //  Total Estimate  $
                                    var nn = textBox25.Text;
                                    nn = nn.Replace("$", "");
                                    split[17] = nn;
                                    split[18] = k;
                                    split[49] = msg1;   //  Tech_Serv4              4 lines of what was reapired
                                    split[73] = h;      //  EST_TOTAL               Estimate Total $
                                    split[74] = k;      //  EST_PARTS               Estimate Parts $
                                    line = String.Join(",", split);
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error 721: \n" + ex);
                            }
                        }
                        lines.Add(line);
                    }
                    reader.Close();
                }
                try
                {
                    using (StreamWriter writer = new StreamWriter(Database, false))
                    {
                        foreach (String line in lines)
                            writer.WriteLine(line);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error line 814: \n" + ex);
                }
            }
        }

        private void textBox12_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var q = textBox12.Text;
                PartsShip = Convert.ToDecimal(q);

                GrandTotal += PartsShip;
                if (GrandTotal <= 999.99m)
                {
                    if (checkBox3.Checked == true)
                    {
                        GrandTotal += WarrtoNonWarr;
                        textBox15.Text = GrandTotal.ToString("0.00");
                    }
                    else
                    {
                        textBox15.Text = GrandTotal.ToString("0.00");
                    }
                }
                else
                {
                    if (checkBox3.Checked == true)
                    {
                        GrandTotal += WarrtoNonWarr;
                        textBox15.Text = GrandTotal.ToString("0.00");
                    }
                    else
                    {
                        textBox15.Text = GrandTotal.ToString("0.00");
                    }
                }
                textBox13.Select();
            }
        }

        private void textBox13_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox13.TextLength == 0)
                {
                    textBox13.Select();
                }
                else
                {
                    mLaborHours = Convert.ToDecimal(textBox13.Text);
                }
            }
        }

        private void textBox2_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox2.TextLength != 0)
                {
                    var f = Convert.ToDecimal(textBox2.Text);
                    {
                        textBox2.Text = f.ToString("0.00");
                        if (f <= 249.95m)
                        {
                            PC0 = Convert.ToDecimal(textBox2.Text);
                            if (textBox41.Text.Contains("120"))
                            {
                                PC_0 = PC0 * NEWRATE;
                            }
                            if (textBox41.Text.Contains("110"))
                            {
                                PC_0 = PC0 * NEWRATE;
                            }
                            if (textBox41.Text.Contains("80"))
                            {
                                PC_0 = PC0 * OLDRATE;
                            }
                            if (textBox41.Text.Contains("70"))
                            {
                                PC_0 = PC0 * OLDRATE;
                            }
                            GrandTotal = PC_0;
                            textBox16.Text = PC_0.ToString("0.00");
                            if (GrandTotal >= 250.00m)
                            {
                                SetLowerMultiplierMessages();
                            }
                            textBox28.Text = PC_0.ToString("0.00");
                            if (PC_0 <= 999.99m)
                            {
                                if (checkBox3.Checked == true)
                                {
                                    textBox15.Text = PC_0.ToString("0.00");
                                }
                                else
                                {
                                    textBox15.Text = PC_0.ToString("0.00");
                                }

                            }
                            else
                            {
                                if (checkBox3.Checked == true)
                                {
                                    textBox15.Text = PC_0.ToString("0.00");
                                }
                                else
                                {
                                    textBox15.Text = PC_0.ToString("0.00");
                                }
                            }
                            textBox16.Text = PC_0.ToString("0.00");
                            textBox3.Select();
                        }
                        if (f >= 250.00m)
                        {
                            PC0 = Convert.ToDecimal(textBox2.Text);
                            if (textBox41.Text.Contains("120"))
                            {
                                PC_0 = PC0 * 1.3m;
                            }
                            if (textBox41.Text.Contains("110"))
                            {
                                PC_0 = PC0 * 1.3m;
                            }
                            if (textBox41.Text.Contains("80"))
                            {
                                PC_0 = PC0 * 1.3m;
                            }
                            if (textBox41.Text.Contains("70"))
                            {
                                PC_0 = PC0 * 1.3m;
                            }
                            GrandTotal = PC_0;
                            if (GrandTotal >= 250.00m)
                            {
                                SetLowerMultiplierMessages();
                            }
                            textBox28.Text = PC_0.ToString("0.00");
                            if (PC_0 <= 999.99m)
                            {
                                if (checkBox3.Checked == true)
                                {
                                    textBox15.Text = PC_0.ToString("0.00");
                                }
                                else
                                {
                                    textBox15.Text = PC_0.ToString("0.00");
                                }

                            }
                            else
                            {
                                if (checkBox3.Checked == true)
                                {
                                    textBox15.Text = PC_0.ToString("0.00");
                                }
                                else
                                {
                                    textBox15.Text = PC_0.ToString("0.00");
                                }
                            }
                            textBox16.Text = PC_0.ToString("0.00");
                            textBox3.Select();
                        }
                    }
                }
                else
                {
                    PC0 = 0;
                    textBox2.Text = "0";
                    GrandTotal = PC_0;
                    textBox16.Text = PC0.ToString("0.00");
                    textBox3.Select();
                }
                   
            }
        }

        private void CheckGrandTotal()
        {
            textBox29.Font = new Font(textBox29.Font, FontStyle.Bold);
            if (GrandTotal >= 250.00m)
            {
                PC0 = Convert.ToDecimal(textBox2.Text);
                if (textBox41.Text.Contains("120"))
                {
                    PC_0 = PC0 * 1.3m;
                }
                if (textBox41.Text.Contains("110"))
                {
                    PC_0 = PC0 * 1.3m;
                }
                if (textBox41.Text.Contains("80"))
                {
                    PC_0 = PC0 * 1.3m;
                }
                if (textBox41.Text.Contains("70"))
                {
                    PC_0 = PC0 * 1.3m;
                }
                textBox16.Text = PC_0.ToString("0.00");
                if (textBox42.Text.Contains("135.00"))
                {
                    PC_1 = PC1 * 1.3m;
                }
                if (textBox41.Text.Contains("120"))
                {
                    PC_1 = PC1 * 1.3m;
                }
                if (textBox41.Text.Contains("110"))
                {
                    PC_1 = PC1 * 1.3m;
                }
                if (textBox41.Text.Contains("80"))
                {
                    PC_1 = PC1 * 1.3m;
                }
                if (textBox41.Text.Contains("70"))
                {
                    PC_1 = PC1 * 1.3m;
                }
                textBox17.Text = (PC_1).ToString("0.00");
                PC2 = Convert.ToDecimal(textBox4.Text);
                if (textBox41.Text.Contains("120"))
                {
                    PC_2 = PC2 * 1.3m;
                }
                if (textBox41.Text.Contains("110"))
                {
                    PC_2 = PC2 * 1.3m;
                }
                if (textBox41.Text.Contains("80"))
                {
                    PC_2 = PC2 * 1.3m;
                }
                if (textBox41.Text.Contains("70"))
                {
                    PC_2 = PC2 * 1.3m;
                }
                textBox18.Text = (PC_2).ToString("0.00");
                PC3 = Convert.ToDecimal(textBox5.Text);
                if (textBox41.Text.Contains("120"))
                {
                    PC_3 = PC3 * 1.3m;
                }
                if (textBox41.Text.Contains("110"))
                {
                    PC_3 = PC3 * 1.3m;
                }
                if (textBox41.Text.Contains("80"))
                {
                    PC_3 = PC3 * 1.3m;
                }
                if (textBox41.Text.Contains("70"))
                {
                    PC_3 = PC3 * 1.3m;
                }
                textBox19.Text = (PC_3).ToString("0.00");
                GrandTotal = PC_0 + PC_1 + PC_2 + PC_3;
                if (GrandTotal >= 250.00m)
                {
                    SetLowerMultiplierMessages();
                }
                textBox28.Text = (PC_0 + PC_1 + PC_2 + PC_3).ToString("0.00");

            }
        }

        private void textBox3_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox3.TextLength != 0)
                {
                    var f = Convert.ToDecimal(textBox3.Text);
                    textBox3.Text = f.ToString("0.00");
                    {
                        textBox3.Text = f.ToString("0.00");
                        if (f <= 249.95m)
                        {
                            PC1 = Convert.ToDecimal(textBox3.Text);
                            if (textBox41.Text.Contains("120"))
                            {
                                PC_1 = PC1 * NEWRATE;
                            }
                            if (textBox41.Text.Contains("110"))
                            {
                                PC_1 = PC1 * NEWRATE;
                            }
                            if (textBox41.Text.Contains("80"))
                            {
                                PC_1 = PC1 * OLDRATE;
                            }
                            if (textBox41.Text.Contains("70"))
                            {
                                PC_1 = PC1 * OLDRATE;
                            }
                            GrandTotal = PC_0 + PC_1;
                            CheckGrandTotal();
                            if (GrandTotal >= 250.00m)
                            {
                                //SetLowerMultiplierMessages();
                            }
                            GrandTotal = PC_0 + PC_1;
                            if (GrandTotal >= 250.00m)
                            {
                                //SetLowerMultiplierMessages();
                            }
                            textBox28.Text = GrandTotal.ToString("0.00");
                            if (GrandTotal <= 999.99m)
                            {
                                if (checkBox3.Checked == true)
                                {
                                    GrandTotal += WarrtoNonWarr;
                                    textBox15.Text = GrandTotal.ToString("0.00");
                                }
                                else
                                {
                                    textBox15.Text = GrandTotal.ToString("0.00");
                                }

                            }
                            else
                            {
                                if (checkBox3.Checked == true)
                                {
                                    GrandTotal += WarrtoNonWarr;
                                    textBox15.Text = GrandTotal.ToString("0.00");
                                }
                                else
                                {
                                    textBox15.Text = GrandTotal.ToString("0.00");
                                }
                            }
                            textBox4.Select();
                        }
                        if (f >= 250.00m)
                        {
                            PC1 = Convert.ToDecimal(textBox3.Text);
                            if (textBox41.Text.Contains("120"))
                            {
                                PC_1 = PC1 * 1.3m;
                            }
                            if (textBox41.Text.Contains("110"))
                            {
                                PC_1 = PC1 * 1.3m;
                            }
                            if (textBox41.Text.Contains("80"))
                            {
                                PC_1 = PC1 * 1.3m;
                            }
                            if (textBox41.Text.Contains("70"))
                            {
                                PC_1 = PC1 * 1.3m;
                            }
                            if (textBox41.Text.Contains("120"))
                            {
                                textBox17.Text = (PC1 * 1.3m).ToString("0.00");
                            }
                            if (textBox41.Text.Contains("110"))
                            {
                                textBox17.Text = (PC1 * 1.3m).ToString("0.00");
                            }
                            if (textBox41.Text.Contains("80"))
                            {
                                textBox17.Text = (PC1 * 1.3m).ToString("0.00");
                            }
                            if (textBox41.Text.Contains("70"))
                            {
                                textBox17.Text = (PC1 * 1.3m).ToString("0.00");
                            }
                            GrandTotal = PC_0 + PC_1;
                            CheckGrandTotal();
                            if (GrandTotal >= 250.00m)
                            {
                                SetLowerMultiplierMessages();
                            }
                            textBox28.Text = GrandTotal.ToString("0.00");

                            if (GrandTotal <= 999.99m)
                            {
                                if (checkBox3.Checked == true)
                                {
                                    GrandTotal += WarrtoNonWarr;
                                    textBox15.Text = GrandTotal.ToString("0.00");
                                }
                                else
                                {
                                    textBox15.Text = GrandTotal.ToString("0.00");
                                }
                            }
                            textBox4.Select();
                        }
                    }
                }

                else
                {
                    PC1 = 0;
                    textBox3.Text = "0";
                    GrandTotal = PC_0 + PC_1;
                    textBox17.Text = PC1.ToString("0.00");
                    textBox4.Select();
                }
            }
        }

        private void textBox4_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox4.TextLength != 0)
                {
                    var f = Convert.ToDecimal(textBox4.Text);
                    textBox4.Text = f.ToString("0.00");
                    if (f <= 249.95m)
                    {
                        PC2 = Convert.ToDecimal(textBox4.Text);
                        if (textBox41.Text.Contains("120"))
                        {
                            PC_2 = PC2 * NEWRATE;
                        }
                        if (textBox41.Text.Contains("110"))
                        {
                            PC_2 = PC2 * NEWRATE;
                        }
                        if (textBox41.Text.Contains("80"))
                        {
                            PC_2 = PC2 * OLDRATE;
                        }
                        if (textBox41.Text.Contains("70"))
                        {
                            PC_2 = PC2 * OLDRATE;
                        }
                        GrandTotal = PC_0 + PC_1 + PC_2;
                        CheckGrandTotal();
                        if (GrandTotal >= 250.00m)
                        {
                            SetLowerMultiplierMessages();
                        }
                        textBox28.Text = GrandTotal.ToString("0.00");
                        if (GrandTotal <= 999.99m)
                        {
                            if (checkBox3.Checked == true)
                            {
                                GrandTotal += WarrtoNonWarr;
                                textBox15.Text = GrandTotal.ToString("0.00");
                            }
                            else
                            {
                                textBox15.Text = GrandTotal.ToString("0.00");
                            }

                        }
                        else
                        {
                            if (checkBox3.Checked == true)
                            {
                                GrandTotal += WarrtoNonWarr;
                                textBox15.Text = GrandTotal.ToString("0.00");
                            }
                            else
                            {
                                textBox15.Text = GrandTotal.ToString("0.00");
                            }

                        }
                        textBox18.Text = PC_2.ToString("0.00");
                        textBox5.Select();
                    }
                    //
                    if (f >= 250.00m)
                    {
                        PC2 = Convert.ToDecimal(textBox4.Text);
                        if (textBox41.Text.Contains("120"))
                        {
                            PC_2 = PC2 * 1.3m;
                        }
                        if (textBox41.Text.Contains("110"))
                        {
                            PC_2 = PC2 * 1.3m;
                        }
                        if (textBox41.Text.Contains("80"))
                        {
                            PC_2 = PC2 * 1.3m;
                        }
                        if (textBox41.Text.Contains("70"))
                        {
                            PC_2 = PC2 * 1.3m;
                        }
                        GrandTotal = PC_0 + PC_1 + PC_2;
                        CheckGrandTotal();
                        GrandTotal = PC_0 + PC_1 + PC_2;
                        textBox28.Text = GrandTotal.ToString("0.00");
                        textBox18.Text = PC_2.ToString("0.00");
                        if (GrandTotal <= 999.99m)
                        {
                            if (checkBox3.Checked == true)
                            {
                                GrandTotal += WarrtoNonWarr;
                                textBox15.Text = GrandTotal.ToString("0.00");
                            }
                            else
                            {
                                textBox15.Text = GrandTotal.ToString("0.00");
                            }

                        }
                        else
                        {
                            if (checkBox3.Checked == true)
                            {
                                GrandTotal += WarrtoNonWarr;
                                textBox15.Text = GrandTotal.ToString("0.00");
                            }
                            else
                            {
                                textBox15.Text = GrandTotal.ToString("0.00");
                            }

                        }
                        
                        textBox18.Text = (PC_2).ToString("0.00");
                        textBox5.Select();
                    }
                }
                else
                {
                    PC2 = 0;
                    textBox4.Text = "0";
                    GrandTotal = PC_0 + PC_1 + PC_2;
                    textBox18.Text = PC2.ToString("0.00");
                    textBox5.Select();
                }
            }
        }

        private void textBox5_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var f = Convert.ToDecimal(textBox5.Text);
                textBox5.Text = f.ToString("0.00");
                if (textBox5.TextLength != 0)
                {
                    if (f <= 249.95m)
                    {
                        PC3 = Convert.ToDecimal(textBox5.Text);
                        if (textBox41.Text.Contains("120"))
                        {
                            PC_3 = PC3 * NEWRATE;
                        }
                        if (textBox41.Text.Contains("110"))
                        {
                            PC_3 = PC3 * NEWRATE;
                        }
                        if (textBox41.Text.Contains("80"))
                        {
                            PC_3 = PC3 * OLDRATE;
                        }
                        if (textBox41.Text.Contains("70"))
                        {
                            PC_3 = PC3 * OLDRATE;
                        }
                        GrandTotal = PC_0 + PC_1 + PC_2 + PC_3;
                        CheckGrandTotal();
                        GrandTotal = PC_0 + PC_1 + PC_2 + PC_3;
                        textBox28.Text = GrandTotal.ToString("0.00");
                        if (GrandTotal <= 999.99m)
                        {
                            if (checkBox3.Checked == true)
                            {
                                GrandTotal += WarrtoNonWarr;
                                textBox15.Text = GrandTotal.ToString("0.00");
                            }
                            else
                            {
                                textBox15.Text = GrandTotal.ToString("0.00");
                            }

                        }
                        else
                        {
                            if (checkBox3.Checked == true)
                            {
                                GrandTotal += WarrtoNonWarr;
                                textBox15.Text = GrandTotal.ToString("0.00");
                            }
                            else
                            {
                                textBox15.Text = GrandTotal.ToString("0.00");
                            }

                        }
                        if (textBox41.Text.Contains("120"))
                        {
                            textBox19.Text = (PC3 * NEWRATE).ToString("0.00");
                        }
                        if (textBox41.Text.Contains("110"))
                        {
                            textBox19.Text = (PC3 * NEWRATE).ToString("0.00");
                        }
                        if (textBox41.Text.Contains("80"))
                        {
                            textBox19.Text = (PC3 * OLDRATE).ToString("0.00");
                        }
                        if (textBox41.Text.Contains("70"))
                        {
                            textBox19.Text = (PC3 * OLDRATE).ToString("0.00");
                        }
                        textBox19.Text = PC_3.ToString("0.00");
                        textBox6.Select();
                    }

                    if (Convert.ToDecimal(textBox28.Text) >= 250.00m)
                    {
                        PC2 = Convert.ToDecimal(textBox5.Text);
                        if (textBox41.Text.Contains("120"))
                        {
                            PC_3 = PC2 * 1.3m;
                        }
                        if (textBox41.Text.Contains("110"))
                        {
                            PC_3 = PC2 * 1.3m;
                        }
                        if (textBox41.Text.Contains("80"))
                        {
                            PC_3 = PC2 * 1.3m;
                        }
                        if (textBox41.Text.Contains("70"))
                        {
                            PC_3 = PC2 * 1.3m;
                        }
                        textBox19.Text = PC_3.ToString("0.00");
                    }
                    else
                    {
                        PC2 = Convert.ToDecimal(textBox5.Text);
                        if (textBox41.Text.Contains("120"))
                        {
                            PC_3 = PC2 * 1.3m;
                        }
                        if (textBox41.Text.Contains("110"))
                        {
                            PC_3 = PC2 * 1.3m;
                        }
                        if (textBox41.Text.Contains("80"))
                        {
                            PC_3 = PC2 * 1.3m;
                        }
                        if (textBox41.Text.Contains("70"))
                        {
                            PC_3 = PC2 * 1.3m;
                        }
                        textBox19.Text = PC_3.ToString("0.00");
                    }
                    textBox19.Text = PC_3.ToString("0.00");
                    GrandTotal = PC_0 + PC_1 + PC_2 + PC_3;
                    CheckGrandTotal();
                    GrandTotal = PC_0 + PC_1 + PC_2 + PC_3;
                    textBox28.Text = GrandTotal.ToString("0.00");
                    if (GrandTotal <= 999.99m)
                    {
                        if (checkBox3.Checked == true)
                        {
                            GrandTotal += WarrtoNonWarr;
                            textBox15.Text = GrandTotal.ToString("0.00");
                        }
                        else
                        {
                            textBox15.Text = GrandTotal.ToString("0.00");
                        }
                    }
                    
                }

                else
                {
                    PC3 = 0;
                    textBox5.Text = "0";
                    GrandTotal = PC_0 + PC_1 + PC_2 + PC_3;
                    textBox19.Text = PC3.ToString("0.00");
                    textBox6.Select();
                }
                textBox6.Select();
            }
        }

        private void textBox6_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox6.TextLength != 0)
                {
                    var f = Convert.ToDecimal(textBox6.Text);
                    PC4 = Convert.ToDecimal(textBox6.Text);
                    textBox6.Text = f.ToString("0.00");
                    if (f <= 249.95m)
                    {
                        if (textBox41.Text.Contains("120"))
                        {
                            PC_4 = PC4 * NEWRATE;
                        }
                        if (textBox41.Text.Contains("110"))
                        {
                            PC_4 = PC4 * NEWRATE;
                        }
                        if (textBox41.Text.Contains("80"))
                        {
                            PC_4 = PC4 * OLDRATE;
                        }
                        if (textBox41.Text.Contains("70"))
                        {
                            PC_4 = PC4 * OLDRATE;
                        }
                        GrandTotal = PC_0 + PC_1 + PC_2 + PC_3 + PC_4;
                        textBox28.Text = GrandTotal.ToString("0.00");
                        if (GrandTotal <= 999.99m)
                        {
                            if (checkBox3.Checked == true)
                            {
                                GrandTotal += WarrtoNonWarr;
                                textBox15.Text = GrandTotal.ToString("0.00");
                            }
                            else
                            {
                                textBox15.Text = GrandTotal.ToString("0.00");
                            }

                        }
                        else
                        {
                            if (checkBox3.Checked == true)
                            {
                                GrandTotal += WarrtoNonWarr;
                                textBox15.Text = GrandTotal.ToString("0.00");
                            }
                            else
                            {
                                textBox15.Text = GrandTotal.ToString("0.00");
                            }
                        }
                        if (textBox41.Text.Contains("120"))
                        {
                            textBox20.Text = (PC4 * NEWRATE).ToString("0.00");
                        }
                        if (textBox41.Text.Contains("110"))
                        {
                            textBox20.Text = (PC4 * NEWRATE).ToString("0.00");
                        }
                        if (textBox41.Text.Contains("80"))
                        {
                            textBox20.Text = (PC4 * OLDRATE).ToString("0.00");
                        }
                        if (textBox41.Text.Contains("70"))
                        {
                            textBox20.Text = (PC4 * OLDRATE).ToString("0.00");
                        }
                        textBox7.Select();
                    }
                    if (Convert.ToDecimal(textBox28.Text) >= 250.00m)
                    {
                        PC2 = Convert.ToDecimal(textBox6.Text);
                        if (textBox41.Text.Contains("120"))
                        {
                            PC_4 = PC4 * 1.3m;
                        }
                        if (textBox41.Text.Contains("110"))
                        {
                            PC_4 = PC4 * 1.3m;
                        }
                        if (textBox41.Text.Contains("80"))
                        {
                            PC_4 = PC4 * 1.3m;
                        }
                        if (textBox41.Text.Contains("70"))
                        {
                            PC_4 = PC4 * 1.3m;
                        }
                        textBox20.Text = PC_4.ToString("0.00");
                        textBox7.Select();
                    }
                }
                else
                {
                    PC4 = 0;
                    textBox6.Text = "0";
                    GrandTotal = PC_0 + PC_1 + PC_2 + PC_3 + PC_4;
                    textBox20.Text = PC4.ToString("0.00");
                    textBox7.Select();
                }
            }
        }

        private void textBox7_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PC5 = Convert.ToDecimal(textBox7.Text);
                var f = Convert.ToDecimal(textBox7.Text);
                textBox7.Text = f.ToString("0.00");
                if (textBox7.TextLength != 0)
                {
                    if (f <= 249.95m)
                    {
                        if (textBox41.Text.Contains("120"))
                        {
                            PC_5 = PC5 * NEWRATE;
                        }
                        if (textBox41.Text.Contains("110"))
                        {
                            PC_5 = PC5 * NEWRATE;
                        }
                        if (textBox41.Text.Contains("80"))
                        {
                            PC_5 = PC5 * OLDRATE;
                        }
                        if (textBox41.Text.Contains("70"))
                        {
                            PC_5 = PC5 * OLDRATE;
                        }
                        GrandTotal = PC_0 + PC_1 + PC_2 + PC_3 + PC_4 + PC_5;
                        textBox28.Text = GrandTotal.ToString("0.00");
                        if (GrandTotal <= 999.99m)
                        {
                            if (checkBox3.Checked == true)
                            {
                                GrandTotal += WarrtoNonWarr;
                                textBox15.Text = GrandTotal.ToString("0.00");
                            }
                            else
                            {
                                textBox15.Text = GrandTotal.ToString("0.00");
                            }
                        }
                        else
                        {
                            if (checkBox3.Checked == true)
                            {
                                GrandTotal += WarrtoNonWarr;
                                textBox15.Text = GrandTotal.ToString("0.00");
                            }
                            else
                            {
                                textBox15.Text = GrandTotal.ToString("0.00");
                            }
                        }
                        if (textBox41.Text.Contains("120"))
                        {
                            textBox21.Text = (PC5 * NEWRATE).ToString("0.00");
                        }
                        if (textBox41.Text.Contains("110"))
                        {
                            textBox21.Text = (PC5 * NEWRATE).ToString("0.00");
                        }
                        if (textBox41.Text.Contains("80"))
                        {
                            textBox21.Text = (PC5 * OLDRATE).ToString("0.00");
                        }
                        if (textBox41.Text.Contains("70"))
                        {
                            textBox21.Text = (PC5 * OLDRATE).ToString("0.00");
                        }
                        textBox8.Select();
                    }
                    if (Convert.ToDecimal(textBox28.Text) >= 250.00m)
                    {
                        if (textBox41.Text.Contains("120"))
                        {
                            PC_5 = PC5 * 1.3m;
                        }
                        if (textBox41.Text.Contains("110"))
                        {
                            PC_5 = PC5 * 1.3m;
                        }
                        if (textBox41.Text.Contains("80"))
                        {
                            PC_5 = PC5 * 1.3m;
                        }
                        if (textBox41.Text.Contains("70"))
                        {
                            PC_5 = PC5 * 1.3m;
                        }
                        GrandTotal = PC_0 + PC_1 + PC_2 + PC_3 + PC_4 + PC_5;
                        textBox28.Text = GrandTotal.ToString("0.00");
                        textBox21.Text = PC_5.ToString("0.00");
                        textBox8.Select();
                    }
                }
                else
                {
                    PC5 = 0;
                    textBox7.Text = "0";
                    GrandTotal = PC_0 + PC_1 + PC_2 + PC_3 + PC_4 + PC_5;
                    textBox21.Text = PC5.ToString("0.00");
                    textBox8.Select();
                }
            }
        }

        private void textBox8_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PC6 = Convert.ToDecimal(textBox8.Text);
                var f = Convert.ToDecimal(textBox8.Text);
                textBox8.Text = f.ToString("0.00");
                if (textBox8.TextLength != 0)
                {
                    PC6 = Convert.ToDecimal(textBox8.Text);
                    if (textBox41.Text.Contains("120"))
                    {
                        PC_6 = PC6 * NEWRATE;
                    }
                    if (textBox41.Text.Contains("110"))
                    {
                        PC_6 = PC6 * NEWRATE;
                    }
                    if (textBox41.Text.Contains("80"))
                    {
                        PC_6 = PC6 * OLDRATE;
                    }
                    if (textBox41.Text.Contains("70"))
                    {
                        PC_6 = PC6 * OLDRATE;
                    }
                    GrandTotal = PC_0 + PC_1 + PC_2 + PC_3 + PC_4 + PC_5 + PC_6;
                    textBox28.Text = GrandTotal.ToString("0.00");
                    if (GrandTotal <= 999.99m)
                    {
                        if (checkBox3.Checked == true)
                        {
                            GrandTotal += WarrtoNonWarr;
                            textBox15.Text = GrandTotal.ToString("0.00");
                        }
                        else
                        {
                            textBox15.Text = GrandTotal.ToString("0.00");
                        }
                    }
                    else
                    {
                        if (checkBox3.Checked == true)
                        {
                            GrandTotal += WarrtoNonWarr;
                            textBox15.Text = GrandTotal.ToString("0.00");
                        }
                        else
                        {
                            textBox15.Text = GrandTotal.ToString("0.00");
                        }
                    }
                    if (textBox41.Text.Contains("120"))
                    {
                        textBox22.Text = (PC6 * NEWRATE).ToString("0.00");
                    }
                    if (textBox41.Text.Contains("110"))
                    {
                        textBox22.Text = (PC6 * NEWRATE).ToString("0.00");
                    }
                    if (textBox41.Text.Contains("80"))
                    {
                        textBox22.Text = (PC6 * OLDRATE).ToString("0.00");
                    }
                    if (textBox41.Text.Contains("70"))
                    {
                        textBox22.Text = (PC6 * OLDRATE).ToString("0.00");
                    }
                    textBox9.Select();
                }
                if (GrandTotal >= 250.00m)
                {
                    if (textBox41.Text.Contains("120"))
                    {
                        PC_6 = PC6 * 1.3m;
                    }
                    if (textBox41.Text.Contains("110"))
                    {
                        PC_6 = PC6 * 1.3m;
                    }
                    if (textBox41.Text.Contains("80"))
                    {
                        PC_6 = PC6 * 1.3m;
                    }
                    if (textBox41.Text.Contains("70"))
                    {
                        PC_6 = PC6 * 1.3m;
                    }
                    GrandTotal = PC_0 + PC_1 + PC_2 + PC_3 + PC_4 + PC_5 + PC_6;
                    textBox28.Text = GrandTotal.ToString("0.00");
                    textBox22.Text = PC_6.ToString("0.00");
                    textBox9.Select();
                }
                else
                {
                    PC6 = 0;
                    textBox8.Text = "0";
                    GrandTotal = PC_0 + PC_1 + PC_2 + PC_3 + PC_4 + PC_5 + PC_6;
                    textBox22.Text = PC6.ToString("0.00");
                    textBox9.Select();
                }
            }
        }

        private void textBox9_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox9.TextLength != 0)
                {
                    PC7 = Convert.ToDecimal(textBox9.Text);
                    if (textBox41.Text.Contains("120"))
                    {
                        PC_7 = PC7 * NEWRATE;
                    }
                    if (textBox41.Text.Contains("110"))
                    {
                        PC_7 = PC7 * NEWRATE;
                    }
                    if (textBox41.Text.Contains("80"))
                    {
                        PC_7 = PC7 * OLDRATE;
                    }
                    if (textBox41.Text.Contains("70"))
                    {
                        PC_7 = PC7 * OLDRATE;
                    }
                    GrandTotal = PC_0 + PC_1 + PC_2 + PC_3 + PC_4 + PC_5 + PC_6 + PC_7;
                    textBox28.Text = GrandTotal.ToString("0.00");
                    if (GrandTotal <= 999.99m)
                    {
                        if (checkBox3.Checked == true)
                        {
                            GrandTotal += WarrtoNonWarr;
                            textBox15.Text = GrandTotal.ToString("0.00");
                        }
                        else
                        {
                            textBox15.Text = GrandTotal.ToString("0.00");
                        }
                    }
                    else
                    {
                        if (checkBox3.Checked == true)
                        {
                            GrandTotal += WarrtoNonWarr;
                            textBox15.Text = GrandTotal.ToString("0.00");
                        }
                        else
                        {
                            textBox15.Text = GrandTotal.ToString("0.00");
                        }

                    }
                    
                    if (textBox41.Text.Contains("120"))
                    {
                        textBox23.Text = (PC7 * NEWRATE).ToString("0.00");
                    }
                    if (textBox41.Text.Contains("110"))
                    {
                        textBox23.Text = (PC7 * NEWRATE).ToString("0.00");
                    }
                    if (textBox41.Text.Contains("80"))
                    {
                        textBox23.Text = (PC7 * OLDRATE).ToString("0.00");
                    }
                    if (textBox41.Text.Contains("70"))
                    {
                        textBox23.Text = (PC7 * OLDRATE).ToString("0.00");
                    }
                    
                    textBox10.Select();
                }
                else
                {
                    PC7 = 0;
                    textBox9.Text = "0";
                    GrandTotal = PC_0 + PC_1 + PC_2 + PC_3 + PC_4 + PC_5 + PC_6 + PC_7;
                    textBox23.Text = PC7.ToString("0.00");
                    textBox10.Select();
                }
            }
        }

        private void textBox10_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox10.TextLength != 0)
                {
                    PC8 = Convert.ToDecimal(textBox10.Text);
                    if (textBox41.Text.Contains("120"))
                    {
                        PC_8 = PC8 * NEWRATE;
                    }
                    if (textBox41.Text.Contains("110"))
                    {
                        PC_8 = PC8 * NEWRATE;
                    }
                    if (textBox41.Text.Contains("80"))
                    {
                        PC_8 = PC8 * OLDRATE;
                    }
                    if (textBox41.Text.Contains("70"))
                    {
                        PC_8 = PC8 * OLDRATE;
                    }
                    GrandTotal = PC_0 + PC_1 + PC_2 + PC_3 + PC_4 + PC_5 + PC_6 + PC_7 + PC_8;
                    textBox28.Text = GrandTotal.ToString("0.00");
                    if (GrandTotal <= 999.99m)
                    {
                        if (checkBox3.Checked == true)
                        {
                            GrandTotal += WarrtoNonWarr;
                            textBox15.Text = GrandTotal.ToString("0.00");
                        }
                        else
                        {
                            textBox15.Text = GrandTotal.ToString("0.00");
                        }
                    }
                    else
                    {
                        if (checkBox3.Checked == true)
                        {
                            GrandTotal += WarrtoNonWarr;
                            textBox15.Text = GrandTotal.ToString("0.00");
                        }
                        else
                        {
                            textBox15.Text = GrandTotal.ToString("0.00");
                        }
                    }
                    if (textBox41.Text.Contains("120"))
                    {
                        textBox24.Text = (PC8 * NEWRATE).ToString("0.00");
                    }
                    if (textBox41.Text.Contains("110"))
                    {
                        textBox24.Text = (PC8 * NEWRATE).ToString("0.00");
                    }
                    if (textBox41.Text.Contains("80"))
                    {
                        textBox24.Text = (PC8 * OLDRATE).ToString("0.00");
                    }
                    if (textBox41.Text.Contains("70"))
                    {
                        textBox24.Text = (PC8 * OLDRATE).ToString("0.00");
                    }
                    mPartsCosts = PC_0 + PC_1 + PC_2 + PC_3 + PC_4 + PC_5 + PC_6 + PC_7 + PC_8;
                    textBox11.Select();

                }
                else
                {
                    PC0 = 0;
                    textBox10.Text = "0";
                    GrandTotal = PC_0 + PC_1 + PC_2 + PC_3 + PC_4 + PC_5 + PC_6 + PC_7 + PC_8;
                    textBox24.Text = PC7.ToString("0.00");
                    mPartsCosts = PC_0 + PC_1 + PC_2 + PC_3 + PC_4 + PC_5 + PC_6 + PC_7 + PC_8;
                    textBox11.Select();
                }
            }
        }

        private void textBox1_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GetIndexNumber();
                if (ndx2 > 1)
                {
                    MessageBox.Show(textBox1.Text + " Has already been sent on " + sentDate);
                }

                claim_no = textBox1.Text;
                Size = new Size(927, 595);                                                              // resize screen
                this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                              (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);             // center screen

                textBox27.Text = textBox1.Text;
                CheckTheClaimNum();
                PullFromDB();                                       // Get Name, Email, Warranty Status, etc.
                if (IsRendered == true)
                {
                    MessageBox.Show("Claim is in 'Service Rendered XX status !\nReturning to previous menu.");
                    Hide();
                    MainUtilitiesMenu f1 = new MainUtilitiesMenu();
                    f1.Show();
                    return;
                }
                if (found != true)
                {
                    MessageBox.Show("Claim not found!.");
                    Hide();
                    MainUtilitiesMenu f1 = new MainUtilitiesMenu();
                    f1.Show();
                    return;
                }
                if (EMAIL == "NONE")
                {
                    label10.Text = FIRST + " " + LAST + "\n" + "***** MISSING EMAIL ADDRESS *****\t *****MISSING EMAIL ADDRESS *****\t *****MISSING EMAIL ADDRESS *****";
                }
                if (EMAIL == SecEmail)
                {
                    label10.Text = FIRST + " " + LAST + "\n" + EMAIL;   // Show above data to verify right claim info
                }
                if (EMAIL != SecEmail)
                {
                    label10.Text = FIRST + " " + LAST + "\nPrimary Email:      " + EMAIL + "\nSecondaty Email: " + SecEmail;   // Show above data to verify right claim info
                }
                var clm = Convert.ToInt32(textBox1.Text);
                if (clm <= 200556)  // Change new prices $ 110, $ 125
                {
                    checkBox1.Text = "$80 or $ 90/HR";
                    textBox39.Text = "8.50";
                    textBox40.Text = "10.00";
                    textBox41.Text = "80.00";
                    textBox42.Text = "90.00";
                    textBox29.Text = "1.3";
                }
                if (clm >= 200557)  // Change new prices $ 110, $ 125
                {
                    textBox39.Text = "8.50";
                    textBox40.Text = "10.00";
                    textBox41.Text = "110.00";
                    textBox42.Text = "125.00";
                    textBox29.Text = "1.5";
                }
                if (clm >= 300876)  // Change new price $ 120, $ 135
                {
                    textBox39.Text = "8.50";
                    textBox40.Text = "10.00";
                    textBox41.Text = "120.00";
                    textBox42.Text = "135.00";
                    textBox29.Text = "1.5";
                }
                textBox2.Select();
            }
        }

        private void CheckTheClaimNum()
        {
            var t = textBox1.Text;
            int numVal = Int32.Parse(t);
            if (numVal >= 300876)
            {
                textBox1.Visible = true;
                label1.Visible = true;
                textBox27.Visible = false;
                label18.Visible = true;
                label1.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
                label1.ForeColor = Color.Red;
                label1.BackColor = Color.Yellow;
                textBox1.SelectAll();
                textBox1.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
                textBox1.ForeColor = Color.Red;
                textBox1.BackColor = Color.Yellow;
                textBox1.DeselectAll();
                label18.Text = "NEW PRICE $";
                label18.ForeColor = Color.Red;
                label18.BackColor = Color.Yellow;
                label18.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
                checkBox1.Text = "$120 or $135 /Hour";
                textBox41.Text = "120.00";
                textBox42.Text = "135.00";
                IsNewPrice = true;
                if (IsNewPrice == true)
                {
                    textBox41.Text = "120.00";
                    textBox42.Text = "135.00";
                    textBox41.BackColor = Color.LightGreen;
                    textBox42.BackColor = Color.White;
                }
            }
        }

        private void textBox25_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false)
            {
                HourRate = Convert.ToDecimal(textBox41.Text);
                mLaborHours = Convert.ToDecimal(textBox13.Text);
                mLaborTotal = mLaborHours * HourRate;
                textBox25.Text = mLaborTotal.ToString("0.00");
                GrandTotal = PC_0 + PC_1 + PC_2 + PC_3 + PC_4 + PC_5 + PC_6 + PC_7 + PC_8 + ShopCost + PartsShip + GATaxTotal;
                GrandTotal += mLaborTotal;
                if (GrandTotal <= 999.99m)
                {
                    if (checkBox3.Checked == true)
                    {
                        GrandTotal += WarrtoNonWarr;
                        textBox15.Text = GrandTotal.ToString("0.00");
                    }
                    else
                    {
                        textBox15.Text = GrandTotal.ToString("0.00");
                    }
                }
                else
                {
                    if (checkBox3.Checked == true)
                    {
                        GrandTotal += WarrtoNonWarr;
                        textBox15.Text = GrandTotal.ToString("0.00");
                    }
                    else
                    {
                        textBox15.Text = GrandTotal.ToString("0.00");
                    }
                }
            }
            if (checkBox1.Checked == true)
            {
                HourRate = Convert.ToDecimal(textBox42.Text);
                var t = textBox13.Text;
                mLaborHours = Convert.ToDecimal(t);
                mLaborTotal = mLaborHours * HourRate;
                textBox25.Text = mLaborTotal.ToString("0.00");
                GrandTotal = PC_0 + PC_1 + PC_2 + PC_3 + PC_4 + PC_5 + PC_6 + PC_7 + PC_8 + ShopCost + PartsShip + GATaxTotal;
                GrandTotal += mLaborTotal;
                if (GrandTotal <= 999.99m)
                {
                    if (checkBox3.Checked == true)
                    {
                        GrandTotal += WarrtoNonWarr;
                        textBox15.Text = GrandTotal.ToString("0.00");
                    }
                    else
                    {
                        textBox15.Text = GrandTotal.ToString("0.00");
                    }
                }
                else
                {
                    if (checkBox3.Checked == true)
                    {
                        GrandTotal += WarrtoNonWarr;
                        textBox15.Text = GrandTotal.ToString("0.00");
                    }
                    else
                    {
                        textBox15.Text = GrandTotal.ToString("0.00");
                    }
                }
            }
        }

        private void checkBox2_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox2.Checked == false)
            {
                textBox26.Text = "";
                GrandTotal = PC_0 + PC_1 + PC_2 + PC_3 + PC_4 + PC_5 + PC_6 + PC_7 + PC_8 + ShopCost + PartsShip + GATaxTotal + mLaborTotal;
                if (GrandTotal <= 999.99m)
                {
                    GrandTotal += WarrtoNonWarr;
                    textBox15.Text = GrandTotal.ToString("0.00");
                }
                else
                {
                    GrandTotal += WarrtoNonWarr;
                    textBox15.Text = GrandTotal.ToString("0.00");
                }
            }
            if (checkBox2.Checked == true)
            {
                var jj = Convert.ToDecimal(textBox40.Text);
                jj = jj / 100m;
                var t = mLaborTotal * jj;
                MUSICSTORE = mLaborTotal - t;
                GrandTotal = PC_0 + PC_1 + PC_2 + PC_3 + PC_4 + PC_5 + PC_6 + PC_7 + PC_8 + ShopCost + PartsShip + GATaxTotal + MUSICSTORE;
                textBox26.Text = MUSICSTORE.ToString("0.00");
                if (GrandTotal <= 999.99m)
                {
                    GrandTotal += WarrtoNonWarr;
                    textBox15.Text = GrandTotal.ToString("0.00");
                }
                else
                {
                    GrandTotal += WarrtoNonWarr;
                    textBox15.Text = GrandTotal.ToString("0.00");
                }
            }
        }

        public bool IsDataValid()               // Check to make sure all data is good before send
        {
            IsValid2 = 1;
            if (textBox16.TextLength == 0)
            {
                IsValid2 = 0;
            }
            if (textBox17.TextLength == 0)
            {
                IsValid2 = 0;
            }
            if (textBox18.TextLength == 0)
            {
                IsValid2 = 0;
            }
            if (textBox19.TextLength == 0)
            {
                IsValid2 = 0;
            }
            if (textBox20.TextLength == 0)
            {
                IsValid2 = 0;
            }
            if (textBox21.TextLength == 0)
            {
                IsValid2 = 0;
            }
            if (textBox22.TextLength == 0)
            {
                IsValid2 = 0;
            }
            if (textBox23.TextLength == 0)
            {
                IsValid2 = 0;
            }
            if (textBox24.TextLength == 0)
            {
                IsValid2 = 0;
            }
            if (IsValid2 == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            IsValid = IsDataValid();
            if (IsValid == true)
            {
                if (textBox15.Text.Length == 0 ^ textBox2.Text.Length == 0 ^ textBox3.Text.Length == 0)
                {
                    MessageBox.Show("Fill in all fields first.");
                    textBox2.Select();
                    return;
                }
                label16.Visible = true;
                linkLabel1.VisitedLinkColor = Color.Red;
                Process proc = new Process();
                var sh = textBox11.Text;
                sh1 = Convert.ToDecimal(sh);
                var ps = textBox12.Text;
                ps1 = Convert.ToDecimal(ps);
                
                var msg = "We have the estimate ready for your: ";
                var msg2 = "Please advise how you would like us to proceed.";
                var shopMGR = "%0A" + "Thank you. " + "%0A" + "David 'Doc' Reynics" + "%0A" + "Parts and Shop Manager" + "%0A" + "Wizard Electronics. Inc." + "%0A"
                    + "554 Deering Road NW" + "%0A" + "Atlanta, GA 30309" + "%0A" + "800-274-8863 (Outside GA)" + "%0A"
                    + "P: 404-325-4891" + "%0A" + "www.wizardelectronics.com" + "%0A";

                if (mwarr.StartsWith("WARRANTY"))
                {
                    if (mPartsCosts <= 99.99m)
                    {
                        IsBodyHtml = true;
                        Body = FIRST + " " + LAST + "," + "%0A" + "%0A" + msg + " " + mms + "%0A" + "%0A" + "Parts:                 \t\t\t" + mPartsCosts.ToString("C2") +
                            "%0A" + "Labor:                \t\t\t" + textBox25.Text + " (" + textBox13.Text + " Hours @ " + HourRate.ToString("C2") + "/Hour )" +
                            "%0A" + "Shop Supplies: " + sh1.ToString("C2") + "%0A" + "Parts Shipping: " + ps1.ToString("C2") + "%0A" +
                            "GA Tax:              " + GATaxTotal.ToString("C2") + "%0A" + "%0A";
                        Body += "Total Repair: $" + textBox15.Text + "%0A" + "%0A" + msg2 + "%0A" + "%0A" + shopMGR;    // %0A = Enter
                        xPartsTotal = textBox28.Text;
                        xGrandTotal = textBox15.Text;

                    }
                    else
                    {
                        if (mPartsCosts >= 100.00m && mPartsCosts <= 999.99m)
                        {
                            IsBodyHtml = true;
                            Body = FIRST + " " + LAST + "," + "%0A" + "%0A" + msg + " " + mms + "%0A" + "%0A" + "Parts:                 \t\t\t" + mPartsCosts.ToString("C2") +
                            "%0A" + "Labor:                \t\t\t$" + textBox25.Text + " (" + textBox13.Text + " Hours @ " + HourRate.ToString("C2") + "/Hour )" +
                            "%0A" + "Shop Supplies: " + sh1.ToString("C2") + "%0A" + "Parts Shipping: " + ps1.ToString("C2") + "%0A" +
                            "GA Tax:              " + GATaxTotal.ToString("C2") + "%0A" + "%0A"
                            + "Total Repair: $" + textBox15.Text + "%0A" + "%0A" + msg2 + "%0A" + "%0A";    // %0A = Enter
                            Body += "Per company policy requires parts payment up-front (" + mPartsCosts.ToString("C2") + ")." + "%0A" + shopMGR;
                            xPartsTotal = textBox28.Text;
                            xGrandTotal = textBox15.Text;
                        }
                        else
                        {
                            IsBodyHtml = true;
                            Body = FIRST + " " + LAST + "," + "%0A" + "%0A" + msg + " " + mms + "%0A" + "%0A" + "Parts:                 \t\t\t" + mPartsCosts.ToString("C2") +
                            "%0A" + "Labor:                \t\t\t$" + textBox25.Text + " (" + textBox13.Text + " Hours @ " + HourRate.ToString("C2") + "/Hour )" +
                            "%0A" + "Shop Supplies: " + sh1.ToString("C2") + "%0A" + "Parts Shipping: " + ps1.ToString("C2") + "%0A" +
                            "GA Tax:              " + GATaxTotal.ToString("C2") + "%0A" + "%0A"
                            + "Total Repair: $" + textBox15.Text + "%0A" + "%0A" + msg2 + "%0A" + "%0A";    // %0A = Enter
                            Body += "Per company policy requires parts payment up-front (" + mPartsCosts.ToString("C2") + ")." + "%0A" + shopMGR;
                            xPartsTotal = textBox28.Text;
                            xGrandTotal = textBox15.Text;
                        }
                    }

                }
                if (mwarr.StartsWith("NON-WARR"))
                {
                    if (mPartsCosts <= 0.00m)
                    {
                        if (checkBox3.Checked == false)
                        {
                            IsBodyHtml = true;
                            //var kk = "%0A** PARTS PRICING IS GOOD FOR 5 DAYS, **  As manufacturers keep changing prices almost daily." + "%0A";
                            var kk = " %0A";
                            Body = FIRST + " " + LAST + "," + "%0A" + "%0A" + msg + " " + mms + "%0A" + "%0A" + "Parts:                 \t\t\t" + mPartsCosts.ToString("C2") +
                                "%0A" + "Labor:                \t\t\t$" + textBox25.Text + " (" + textBox13.Text + " Hours @ " + HourRate.ToString("C2") + "/Hour )" +
                                "%0A" + "Shop Supplies: " + sh1.ToString("C2") + "%0A" + "Parts Shipping: " + ps1.ToString("C2") + "%0A" +
                                "GA Tax:              " + GATaxTotal.ToString("C2") + "%0A" + "%0A"
                                + "Total Repair: $" + textBox15.Text + "%0A";
                            xPartsTotal = textBox28.Text;
                            xGrandTotal = textBox15.Text;
                        }
                        if (checkBox3.Checked == true)
                        {
                            IsBodyHtml = true;
                            //var kk = "%0A** PARTS PRICING IS GOOD FOR 5 DAYS, **  As manufacturers keep changing prices almost daily." + "%0A";
                            var kk = " %0A";
                            Body = FIRST + " " + LAST + "," + "%0A" + "%0A" + msg + " " + mms + "%0A" + "%0A" + "Parts:                 \t\t\t" + mPartsCosts.ToString("C2") +
                                "%0A" + "Labor:                \t\t\t$" + textBox25.Text + " (" + textBox13.Text + " Hours @ " + HourRate.ToString("C2") + "/Hour )" +
                                "%0A" + "Shop Supplies: " + sh1.ToString("C2") + "%0A" + "Parts Shipping: " + ps1.ToString("C2") + "%0A" +
                                "GA Tax:              " + GATaxTotal.ToString("C2") + "%0A" +  "Diagnostic Fee " + "$65.00  Was Warranty, now Non-Warranty Repair." + "%0A" + "%0A"
                                + "Total Repair: $" + textBox15.Text + "%0A";
                            xPartsTotal = textBox28.Text;
                            xGrandTotal = textBox15.Text;
                        }
                        
                        if (mPartsCosts >= 20)
                        {
                            if (mPartsCosts <= 999.99m)
                            {
                                var kk = "";
                                Body += "<b> My bold text </b> %0A %0A";
                                Body += "Per company policy requires parts payment up-front (" + mPartsCosts.ToString("C2") + "). " + "%0A" + "%0A" + kk + "%0A" + msg2 + "%0A";    // %0A = Enter
                                Body += " %0A %0A" + shopMGR;
                            }
                            else
                            {
                                var kk = "";
                                Body += "Per company policy requires parts payment up-front (" + mPartsCosts.ToString("C2") + "). " + "%0A" + "%0A" + kk + "%0A" + msg2 + "%0A";    // %0A = Enter
                                Body += " %0A %0A" + shopMGR;
                            }
                        }
                        else
                        {
                            Body += "%0A %0A" + shopMGR;
                        }
                    }
                    else
                    {
                        if (checkBox2.Checked == true)
                        {
                            if (checkBox3.Checked == false)
                            {
                                IsBodyHtml = true;
                                //var kk = "%0A** PARTS PRICING IS GOOD FOR 5 DAYS, **  As manufacturers keep changing prices almost daily." + "%0A";
                                var kk = "%0A";
                                Body = FIRST + " " + LAST + "," + "%0A" + "%0A" + msg + " " + mms + "%0A" + "%0A" + "Parts:                 \t\t\t" + mPartsCosts.ToString("C2") +
                                "%0A" + "Labor:                \t\t\t" + textBox26.Text + " (" + textBox13.Text + " Hours @ " + HourRate.ToString("C2") + "/Hour - 15 percent Discount)" +
                                "%0A" + "Shop Supplies: " + sh1.ToString("C2") + "%0A" + "Parts Shipping: " + ps1.ToString("C2") + "%0A" +
                                "GA Tax:              " + GATaxTotal.ToString("C2") + "%0A" + "%0A"
                                + "Total Repair: $" + textBox15.Text + "%0A";
                                xPartsTotal = textBox28.Text;
                                xGrandTotal = textBox15.Text;
                            }
                            if (checkBox3.Checked == true)
                            {
                                IsBodyHtml = true;
                                //var kk = "%0A** PARTS PRICING IS GOOD FOR 5 DAYS, **  As manufacturers keep changing prices almost daily." + "%0A";
                                var kk = "%0A";
                                Body = FIRST + " " + LAST + "," + "%0A" + "%0A" + msg + " " + mms + "%0A" + "%0A" + "Parts:                 \t\t\t" + mPartsCosts.ToString("C2") +
                                "%0A" + "Labor:                \t\t\t" + textBox26.Text + " (" + textBox13.Text + " Hours @ " + HourRate.ToString("C2") + "/Hour - 15 percent Discount)" +
                                "%0A" + "Shop Supplies: " + sh1.ToString("C2") + "%0A" + "Parts Shipping: " + ps1.ToString("C2") + "%0A" +
                                "GA Tax:              " + GATaxTotal.ToString("C2") + "%0A" + "Diagnostic Fee $65.00  Was Warranty, now Non-Warranty Repair." + "%0A" + "%0A"
                                + "Total Repair: $" + textBox15.Text + "%0A";
                                xPartsTotal = textBox28.Text;
                                xGrandTotal = textBox15.Text;
                            }
                            if (mPartsCosts >= 20 && mPartsCosts <= 999.99m)
                            {
                                var kk = "";
                                Body += "Per company policy any parts order requires parts payment up-front (" + mPartsCosts.ToString("C2") + "). " + "%0A" + "%0A";
                                Body += msg2 + "%0A" + kk + "%0A";    // %0A = Enter
                                Body += "%0A %0A" + shopMGR;
                                xPartsTotal = textBox28.Text;
                                xGrandTotal = textBox15.Text;
                            }
                            if (mPartsCosts >= 999.99m)
                            {
                                var kk = "";
                                Body += "Per company policy any parts order requires parts payment up-front (" + mPartsCosts.ToString("C2") + "). " + "%0A" + "%0A";
                                Body += msg2 + "%0A" + kk + "%0A";    // %0A = Enter
                                Body += " %0A %0A" + shopMGR;
                            }
                            else if (mPartsCosts <= 20.00m)
                            {
                                var kk = "";
                                Body += msg2 + "%0A" + kk + "%0A";    // %0A = Enter
                                Body += "%0A %0A" + shopMGR;
                            }
                        }

                        else
                        {
                            if (checkBox3.Checked == false)
                            {
                                IsBodyHtml = true;
                                //var kk = "%0A** PARTS PRICING IS GOOD FOR 5 DAYS, **  As manufacturers keep changing prices almost daily." + "%0A";
                                var kk = "%0A";
                                Body = FIRST + " " + LAST + "," + "%0A" + "%0A" + msg + " " + mms + "%0A" + "%0A" + "Parts:                 \t\t\t" + mPartsCosts.ToString("C2") +
                                "%0A" + "Labor:                \t\t\t$" + textBox25.Text + " (" + textBox13.Text + " Hours @ " + HourRate.ToString("C2") + "/Hour )" +
                                "%0A" + "Shop Supplies: " + sh1.ToString("C2") + "%0A" + "Parts Shipping: " + ps1.ToString("C2") + "%0A" +
                                "GA Tax:              " + GATaxTotal.ToString("C2") + "%0A" + "%0A"
                                + "Total Repair: $" + textBox15.Text + "%0A";    // %0A = Enter
                                xPartsTotal = textBox28.Text;
                                xGrandTotal = textBox15.Text;
                            }
                            if (checkBox3.Checked == true)
                            {
                                IsBodyHtml = true;
                                //var kk = "%0A** PARTS PRICING IS GOOD FOR 5 DAYS, **  As manufacturers keep changing prices almost daily." + "%0A";
                                var kk = "%0A";
                                Body = FIRST + " " + LAST + "," + "%0A" + "%0A" + msg + " " + mms + "%0A" + "%0A" + "Parts:                 \t\t\t" + mPartsCosts.ToString("C2") +
                                "%0A" + "Labor:                \t\t\t$" + textBox25.Text + " (" + textBox13.Text + " Hours @ " + HourRate.ToString("C2") + "/Hour )" +
                                "%0A" + "Shop Supplies: " + sh1.ToString("C2") + "%0A" + "Parts Shipping: " + ps1.ToString("C2") + "%0A" +
                                "GA Tax:              " + GATaxTotal.ToString("C2") + "%0A" + "Diagnostic Fee $65.00  Was Warranty, now Non-Warranty Repair." +  "%0A" + "%0A"
                                + "Total Repair: $" + textBox15.Text + "%0A";    // %0A = Enter
                                xPartsTotal = textBox28.Text;
                                xGrandTotal = textBox15.Text;
                            }
                            if (mPartsCosts >= 20)
                            {
                                Body += "Per company policy any parts order requires parts payment up-front (" + mPartsCosts.ToString("C2") + ")." + "%0A" + "%0A";
                                Body += msg2 + "%0A";
                                Body += "%0A %0A" + shopMGR;
                            }
                            else
                            {
                                Body += msg2 + "%0A";
                                Body += " %0A %0A" + shopMGR; ;
                            }
                        }
                    }
                }
                if (EMAIL == SecEmail)  // Send to Primary Email Address Only
                {
                    
                    proc.StartInfo.FileName = "mailto:" + EMAIL + "; " + "repairs@wizardelectronics.com; parts@wizardelectronics.com; techlab@wizardelectronics.com" + "?subject=Estimate Claim #" + claim_no + " " + mms + "&body=" + Body;
                }
                if (EMAIL != SecEmail)  // Send to Primary Email Address & Secondary Email Address
                {
                    if (SecEmail == "NONE")
                    {
                        proc.StartInfo.FileName = "mailto:" + EMAIL + "; " + "repairs@wizardelectronics.com; parts@wizardelectronics.com; techlab@wizardelectronics.com" + "?subject=Estimate Claim #" + claim_no + " " + mms + "&body=" + Body;
                    }
                    else
                    {
                        proc.StartInfo.FileName = "mailto:" + EMAIL + "; " + SecEmail + "; " + "repairs@wizardelectronics.com; parts@wizardelectronics.com; techlab@wizardelectronics.com" + "?subject=Estimate Claim #" + claim_no + " " + mms + "&body=" + Body;
                    }
                }
                proc.Start();
                UpdateSpreadsheet();
                UpdateServicesData();
            }
            else
            {
                MessageBox.Show("Parts Marked up must not be blank\nEstimate not sent.\nCorrect & retry.");
            }
        }

        private void UpdateSpreadsheet()
        {
            label11.Visible = true;
            var csv = new StringBuilder();
            var comma = ",";

            /*
             * 
             *  var d = decimal.Parse("0.4351242134");
             *  Console.WriteLine(decimal.Round(d, 2));
             * 
             * 
             */

            var mcpc = decimal.Round(mPartsCosts, 2);
            var mlc = decimal.Round(mLaborTotal, 2);
            var msh1 = decimal.Round(sh1, 2);
            var mps1 = decimal.Round(ps1, 2);
            var mTax = decimal.Round(GATaxTotal, 2);
            var mGT = decimal.Round(GrandTotal, 2);

            var newLine = (claim_no + comma + FIRST + comma + LAST + comma +
                mcpc.ToString("0.00") + comma +
                mlc.ToString("0.00") + comma +
                msh1.ToString("0.00") + comma +
                mps1.ToString("0.00") + comma +
                mTax.ToString("0.00") + comma +
                mGT.ToString("0.00") + comma +
                DateTime.Now.ToShortDateString() +  comma + ndx1.ToString() + comma + "00/00/0000" + comma + "0.00" + comma + "0.00" + Environment.NewLine);
            csv.Append(newLine);
            try
            {
                File.AppendAllText(Estimates, csv.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error line 2046: ( UpdateSpreadsheet() )\n" + ex);
            }
            SortSpreadsheet();
        }

        private void SortSpreadsheet()
        {
            string[] lines = File.ReadAllLines(Estimates);
            var data = lines.Skip(1);
            var sorted = data.Select(line => new
            {
                SortKey = Int32.Parse(line.Split(',')[0]),
                Line = line
            })
                        .OrderBy(x => x.SortKey)
                        .Select(x => x.Line);
            try
            {
                File.WriteAllLines(@"I:\\Datafile\\Mel\\Estimates2.CSV", lines.Take(1).Concat(sorted)); // Write sorted data to new file
                File.Delete(@"I:\\Datafile\\Mel\\Estimates.CSV");                                       // Delete old file
                File.Move(@"I:\\Datafile\\Mel\\Estimates2.CSV", @"I:\\Datafile\\Mel\\Estimates.CSV");   // Rename the oldFileName into newFileName
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Line 2564 (Sort CSV)\n" + ex);
            }
            label12.Visible = true;
        }

        private void GetIndexNumber()                                   // Get next Index # in sequence
        {
            try
            {
                StreamReader reader = new StreamReader(Estimates);
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

                ndx0 = 0;
                ndx1 = 0;
                ndx2 = 0;
                loopCount = 0;

                while (!reader.EndOfStream)
                {
                    var lineRead = reader.ReadLine();
                    var values = lineRead.Split(',');

                    listA.Add(values[0]);       //  Claim_No
                    listB.Add(values[1]);       //  First Name
                    listC.Add(values[2]);       //  Last Name
                    listD.Add(values[3]);       //  Parts $
                    listE.Add(values[4]);       //  Labor $
                    listF.Add(values[5]);       //  Shop $
                    listG.Add(values[6]);       //  Ship $
                    listH.Add(values[7]);       //  Tax $
                    listI.Add(values[8]);       //  Total $
                    listJ.Add(values[9]);       //  Sent_Date 00/00/0000
                    listK.Add(values[10]);      //  Index Number
                    listL.Add(values[11]);      //  Approved Date 00/00/0000
                    listM.Add(values[12]);      //  Paid Down $
                    listN.Add(values[13]);      //  Rush $

                    var t = listK[loopCount];
                    if (t.Contains("0/0"))
                    {
                        ndx0 = 2;
                    }
                    else
                    {
                        var gg = t.ToString();
                        if (gg.Contains("."))
                        {
                            decimal kk = Convert.ToDecimal(t);
                            var ll = Convert.ToInt32(kk);
                            t = Convert.ToInt32(ll).ToString();
                            ndx0 = Convert.ToInt32(t);
                        }
                        ndx0 = Convert.ToInt32(t);
                        if (ndx1 <= ndx0)
                        {
                            ndx1 = ndx0;
                        }
                    }
                    if (listA[loopCount] == textBox1.Text)
                    {
                        ndx2++;
                        sentDate = listJ[loopCount];
                    }
                    loopCount++;
                }
                reader.Close(); // Close the open file
                ndx1++;
                //label9.Text = "Index: " + ndx1.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Line 2126: GetIndexNumber\n Sorry an error has occured: " + ex.Message);
            }
        }

        private void PullFromDB()
        {
            try
            {
                StreamReader reader = new StreamReader(Database);
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
                List<string> listBZ = new List<string>();

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
                    listV.Add(values[21]);      //  Deposit Date    Date
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
                    listBP.Add(values[67]);     //  Cust_Extn               Primary Email Address
                    listBQ.Add(values[68]);     //  Claim_Num               'A' Claim Number A210403
                    listBR.Add(values[69]);     //  Company                 Company Name or N/A
                    listBS.Add(values[70]);     //  Real_Claim              Unused (Old new claim #)
                    listBT.Add(values[71]);     //  Email                   Secondary Email Address
                    listBU.Add(values[72]);     //  EST_YN                  Estimate Yes / No
                    listBV.Add(values[73]);     //  EST_TOTAL               Estimate Total $
                    listBW.Add(values[74]);     //  EST_PARTS               Estimate Parts $
                    listBX.Add(values[75]);     //  Rush                    Rush Y or N
                    listBZ.Add(values[76]);     //  Est_Deposit             $

                    if (listB[loopCount] == claim_no)
                    {
                        FIRST = listD[loopCount];
                        LAST = listE[loopCount];
                        EMAIL = listBP[loopCount];
                        SecEmail = listBT[loopCount];
                        mms = listM[loopCount] + " " + listO[loopCount];
                        mwarr = listBL[loopCount];
                        found = true;
                        if (listBD[loopCount].Contains( "SERVICE RENDERED  XX"))
                        {
                            IsRendered = true;
                        }
                        if (listBD[loopCount].Contains("SERVICE RENDERED XX"))
                        {
                            IsRendered = true;
                        }
                    }
                    else
                    {
                        //
                    }
                    loopCount++;
                }
                reader.Close(); // Close the open file
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Line 2852: ( PullFromDB() )\n Sorry an error has occured: " + ex.Message);
            }
        }
    }
}
