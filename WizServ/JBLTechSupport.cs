using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;

namespace WizServ
{
    public partial class JBLTechSupport : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        public string mms = Version.MMS;
        public string make = Version.Make;
        public string model = Version.Model;
        public string serial = Version.Serial;
        public Bitmap image1 = Properties.Resources.Yamaha;
        public Bitmap image2 = Properties.Resources.MackieLogo;
        public Bitmap image3 = Properties.Resources.BiAmp;
        public Bitmap image4 = Properties.Resources.KRK;
        public Bitmap image5 = Properties.Resources.Line6_2;
        public Bitmap image6 = Properties.Resources.gallien_krueger;
        public bool JBLFAMILY = false;
        public string mwarr = Version.Warranty;
        public string TheBody, mUCCTag, mRestofCode;
        public string mProdCode, mManufCode, mMManufYear, mManufWeek, mWeeklySerial, mCheckDigit;
        public string pass, passBack;

        public JBLTechSupport()
        {
            InitializeComponent();
            Icon = image100;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
            linkLabel1.Text = "https://pro.harman.com/service";
            linkLabel2.Text = "hprotechsupportusa@harman.com";
            label11.Visible = false;
            label12.Visible = false;
            linkLabel3.Visible = false;
            label13.Visible = false;
            linkLabel3.Text = "Software here";
            EnterText();
        }

        private void JBLFamily()
        {
            label1.Text = "Harman Technical Support\nDigitTech Sold off\n4-18-2022";
            label2.Text = "There are 3 ways to get technical support:";
            label3.Text = "1) Call 1-844-776-4899";
            label8.Text = "Brand:  " + make;
            label9.Text = "Model:  " + model;
            label10.Text = "Serial:   " + serial;
            linkLabel1.Text = "https://pro.harman.com/service";
            if (make.StartsWith("DIGITECH"))
            {
                linkLabel2.Text = "digitech@cortek.co.kr";
            }
            else
            {
                linkLabel2.Text = "hprotechsupportusa@harman.com";
            }
            pictureBox1.Visible = true;
            pictureBox2.Visible = true;
            pictureBox3.Visible = true;
            pictureBox4.Visible = true;
            pictureBox5.Visible = true;
            pictureBox6.Visible = true;
            pictureBox7.Visible = true;
            pictureBox8.Visible = true;
            pictureBox9.Visible = false;
            pictureBox10.Visible = true;
            JBLFAMILY = true;
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel3.Text = "Software here";
            this.linkLabel3.LinkVisited = true;
            Process.Start("https://line6.com/software/");
        }

        private void EnterText()
        {
            if (make.StartsWith("JBL"))
            {
                JBLFamily();
            }
            if (make.StartsWith("CROWN"))
            {
                JBLFamily();
            }
            if (make.StartsWith("DBX"))
            {
                JBLFamily();
            }
            if (make.StartsWith("LEXICON"))
            {
                JBLFamily();
            }
            if (make.StartsWith("SOUNDCRAFT"))
            {
                JBLFamily();
            }
            if (make.StartsWith("MARTIN"))
            {
                JBLFamily();
            }
            if (make.StartsWith("AKG"))
            {
                JBLFamily();
            }
            if (make.StartsWith("DIGITECH"))
            {
                JBLFamily();
            }
            if (make.StartsWith("BSS"))
            {
                JBLFamily();
            }
            if (make.StartsWith("AMX"))
            {
                JBLFamily();
            }
            if (make.StartsWith("GALLIEN"))
            {
                label1.Text = "Gallien Kruger Technical Support";
                pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                pictureBox1.Image = image6;
                pictureBox2.Visible = false;
                pictureBox3.Visible = false;
                pictureBox4.Visible = false;
                pictureBox5.Visible = false;
                pictureBox6.Visible = false;
                pictureBox7.Visible = false;
                pictureBox8.Visible = false;
                pictureBox9.Visible = false;
                pictureBox10.Visible = false;
                label2.Text = "There are 2 ways to get technical support:";
                label3.Text = "1) Call 1-206-342-7300 Extension 2116";
                label4.Text = "2) Web Service:  NONE.";
                label7.Text = "Account # S29500";
                label8.Text = "Brand:  " + make;
                label9.Text = "Model:  " + model;
                label10.Text = "Serial:   " + serial;
                linkLabel1.Text = "";
                linkLabel2.Text = "tech01@gallien.com";
            }
            if (make.StartsWith("YAMAHA"))
            {
                label1.Text = "Yamaha Technical Support";
                pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                pictureBox1.Image = image1;
                pictureBox2.Visible = false;
                pictureBox3.Visible = false;
                pictureBox4.Visible = false;
                pictureBox5.Visible = false;
                pictureBox6.Visible = false;
                pictureBox7.Visible = false;
                pictureBox8.Visible = false;
                pictureBox9.Visible = false;
                pictureBox10.Visible = false;
                label2.Text = "There are 3 ways to get technical support:";
                label3.Text = "1) Call 1-800-854-1965 Option 1 then 2";
                label7.Text = "Account # 700451";
                label8.Text = "Brand:  " + make;
                label9.Text = "Model:  " + model;
                label10.Text = "Serial:   " + serial;
                linkLabel1.Text = "https://partsyca@yamaha.com";
                linkLabel2.Text = "techsupport@yamaha.com";
            }
            if (make.StartsWith("KRK"))
            {
                label1.Text = "KRK Technical Support\n(Owned by Gibson)";
                pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                pictureBox1.Image = image4;
                pictureBox2.Visible = false;
                pictureBox3.Visible = false;
                pictureBox4.Visible = false;
                pictureBox5.Visible = false;
                pictureBox6.Visible = false;
                pictureBox7.Visible = false;
                pictureBox8.Visible = false;
                pictureBox9.Visible = false;
                pictureBox10.Visible = false;
                label2.Text = "There are 3 ways to get technical support:";
                label3.Text = "1) Call 1-800-444-2766";
                label7.Text = "Account # OGA896";
                label8.Text = "Brand:  " + make;
                label9.Text = "Model:  " + model;
                label10.Text = "Serial:   " + serial;
                linkLabel1.Text = "https://www.gibson.com/en-US/Support/Contact";
                linkLabel2.Text = "service@gibson.com";
            }
            if (make.StartsWith("LINE6"))
            {
                linkLabel3.Visible = true;
                label13.Visible = true;
                linkLabel3.Text = "Software here";
                label1.Text = "Line6 Technical Support\n(Owned by Yamaha)";
                pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                pictureBox1.Image = image5;
                pictureBox2.Visible = false;
                pictureBox3.Visible = false;
                pictureBox4.Visible = false;
                pictureBox5.Visible = false;
                pictureBox6.Visible = false;
                pictureBox7.Visible = false;
                pictureBox8.Visible = false;
                pictureBox9.Visible = false;
                pictureBox10.Visible = false;
                label2.Text = "There are 3 ways to get technical support:";
                label3.Text = "1) Call 1-818-575-3600 (option 2), 8am-12pm M-F Pacific Time.";
                label7.Text = "Account # 700451";
                label8.Text = "Brand:  " + make;
                label9.Text = "Model:  " + model;
                label10.Text = "Serial:   " + serial;
                linkLabel1.Text = "https://https://yamahaguitargroup.com/support/";
                linkLabel2.Text = "jmauck@line6.com";
            }
            if (make.StartsWith("MACKIE"))
            {
                label1.Text = "Mackie Technical Support";
                pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                pictureBox1.Image = image2;
                pictureBox2.Visible = false;
                pictureBox3.Visible = false;
                pictureBox4.Visible = false;
                pictureBox5.Visible = false;
                pictureBox6.Visible = false;
                pictureBox7.Visible = false;
                pictureBox8.Visible = false;
                pictureBox9.Visible = false;
                pictureBox10.Visible = false;
                label2.Text = "There are 3 ways to get technical support:";
                label3.Text = "1) Call 1-800-258-6883 Option 1.";
                label7.Text = "Account # 21401";
                label8.Text = "Brand:  " + make;
                label9.Text = "Model:  " + model;
                label10.Text = "Serial:   " + serial;
                linkLabel1.Text = "http://mackie.com/support";
                linkLabel2.Text = "techsupport@loudaudio.com";
            }
            if (make.StartsWith("BIAMP"))
            {
                label1.Text = "BiAmp Technical Support";
                pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                pictureBox1.Image = image3;
                pictureBox2.Visible = false;
                pictureBox3.Visible = false;
                pictureBox4.Visible = false;
                pictureBox5.Visible = false;
                pictureBox6.Visible = false;
                pictureBox7.Visible = false;
                pictureBox8.Visible = false;
                pictureBox9.Visible = false;
                pictureBox10.Visible = false;
                label4.Visible = false;
                linkLabel1.Visible = false;
                label2.Text = "There are 2 ways to get technical support:";
                label3.Text = "1) Call 503-641-7287";
                label5.Text = "2) Email to:";
                label7.Text = "No Account";
                label11.Visible = true;
                label12.Visible = true;
                label8.Text = "Brand:  " + make;
                label9.Text = "Model:  " + model;
                label10.Text = "Serial:   " + serial;
                linkLabel1.Text = "";
                linkLabel2.Text = "service@biamp.com";
            }
        }

        private void JBLTechSupport_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false; 
        }

        private void JBLTechSupport_FormClosed(object sender, FormClosedEventArgs e)
        {
            Hide();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (JBLFAMILY == true)
            {
                this.linkLabel1.LinkVisited = true;
                if (make.StartsWith("DIGITECH"))
                {
                    Process.Start("digitech@cortek.co.kr");
                }
                else
                {
                    Process.Start("https://pro.harman.com/service");
                }
            }
            if (make.StartsWith("YAMAHA"))
            {
                this.linkLabel1.LinkVisited = true;
                Process.Start("https://partsyca@yamaha.com");
            }
            if (make.StartsWith("LINE6"))
            {
                this.linkLabel1.LinkVisited = true;
                Process.Start("https://line6.com/support/");
            }
            if (make.StartsWith("MACKIE"))
            {
                this.linkLabel1.LinkVisited = true;
                Process.Start("https://mackie.com/support");
            }
            if (make.StartsWith("KRK"))
            {
                this.linkLabel1.LinkVisited = true;
                Process.Start("https://www.gibson.com/en-US/Support/Contact");
            }
            if (make.StartsWith("BIAMP"))
            {
                this.linkLabel1.LinkVisited = true;
                Process.Start("");
            }
        }

        private void Line6UPC()
        {
            var t = serial.Length;
            if (serial.Contains("("))
            {
                mUCCTag = serial.Substring(1, 2);
                mRestofCode = serial.Substring((t - 15), 15);
                mProdCode = serial.Substring(4, 4);
                mManufCode = serial.Substring(8, 1);
                pass = serial.Substring(9, 2);
                GetYear();
                mMManufYear = passBack;
                mManufWeek = serial.Substring(11, 2);
                mWeeklySerial = serial.Substring(13, 5);
                mCheckDigit = serial.Substring(t - 1, 1);
            }
            else
            {
                mUCCTag = serial.Substring(0, 2);
                mRestofCode = serial.Substring(2, 15);
                mProdCode = serial.Substring(2, 4);
                mManufCode = serial.Substring(6, 1);
                pass = serial.Substring(7, 2);
                GetYear();
                mMManufYear = passBack;
                mManufWeek = serial.Substring(9, 2);
                mWeeklySerial = serial.Substring(11, 5);
                mCheckDigit = serial.Substring(t - 1, 1);
            }
            
            
        }

        private void GetYear()
        {
            switch (pass)
            {
                case "20":
                    passBack = "1970";
                    break;
                case "21":
                    passBack = "1971";
                    break;
                case "22":
                    passBack = "1972";
                    break;
                case "23":
                    passBack = "1973";
                    break;
                case "24":
                    passBack = "1974";
                    break;
                case "25":
                    passBack = "1975";
                    break;
                case "26":
                    passBack = "1976";
                    break;
                case "27":
                    passBack = "1977";
                    break;
                case "28":
                    passBack = "1978";
                    break;
                case "29":
                    passBack = "1979";
                    break;
                case "30":
                    passBack = "1980";
                    break;
                case "31":
                    passBack = "1981";
                    break;
                case "32":
                    passBack = "1982";
                    break;
                case "33":
                    passBack = "1983";
                    break;
                case "34":
                    passBack = "1984";
                    break;
                case "35":
                    passBack = "1985";
                    break;
                case "36":
                    passBack = "1986";
                    break;
                case "37":
                    passBack = "1987";
                    break;
                case "38":
                    passBack = "1988";
                    break;
                case "39":
                    passBack = "1989";
                    break;
                case "40":
                    passBack = "1990";
                    break;
                case "41":
                    passBack = "1991";
                    break;
                case "42":
                    passBack = "1992";
                    break;
                case "43":
                    passBack = "1993";
                    break;
                case "44":
                    passBack = "1994";
                    break;
                case "45":
                    passBack = "1995";
                    break;
                case "46":
                    passBack = "1996";
                    break;
                case "47":
                    passBack = "1997";
                    break;
                case "48":
                    passBack = "1998";
                    break;
                case "49":
                    passBack = "1999";
                    break;
                case "50":
                    passBack = "2000";
                    break;
                case "51":
                    passBack = "2001";
                    break;
                case "52":
                    passBack = "2002";
                    break;
                case "53":
                    passBack = "2003";
                    break;
                case "54":
                    passBack = "2004";
                    break;
                case "55":
                    passBack = "2005";
                    break;
                case "56":
                    passBack = "2006";
                    break;
                case "57":
                    passBack = "2007";
                    break;
                case "58":
                    passBack = "2008";
                    break;
                case "59":
                    passBack = "2009";
                    break;
                case "60":
                    passBack = "2010";
                    break;
                case "61":
                    passBack = "2011";
                    break;
                case "62":
                    passBack = "2012";
                    break;
                case "63":
                    passBack = "2013";
                    break;
                case "64":
                    passBack = "2014";
                    break;
                case "65":
                    passBack = "2015";
                    break;
                case "66":
                    passBack = "2016";
                    break;
                case "67":
                    passBack = "2017";
                    break;
                case "68":
                    passBack = "2018";
                    break;
                case "69":
                    passBack = "2019";
                    break;
                case "70":
                    passBack = "2020";
                    break;
                case "71":
                    passBack = "2021";
                    break;
                case "72":
                    passBack = "2022";
                    break;
                case "73":
                    passBack = "2023";
                    break;
                case "74":
                    passBack = "2024";
                    break;
                case "75":
                    passBack = "2025";
                    break;
                case "76":
                    passBack = "2026";
                    break;
                case "77":
                    passBack = "2027";
                    break;
                case "78":
                    passBack = "2028";
                    break;
                case "79":
                    passBack = "2029";
                    break;
                case "80":
                    passBack = "2030";
                    break;
                case "81":
                    passBack = "2031";
                    break;
                case "82":
                    passBack = "2032";
                    break;
                case "83":
                    passBack = "2033";
                    break;
                case "84":
                    passBack = "2034";
                    break;
                case "85":
                    passBack = "2035";
                    break;
                case "86":
                    passBack = "2036";
                    break;
                case "87":
                    passBack = "2037";
                    break;
                case "88":
                    passBack = "2038";
                    break;
                case "89":
                    passBack = "2039";
                    break;
                case "90":
                    passBack = "2040";
                    break;
                case "91":
                    passBack = "2041";
                    break;
                case "92":
                    passBack = "2042";
                    break;
                case "93":
                    passBack = "2043";
                    break;
                case "94":
                    passBack = "2044";
                    break;
                case "95":
                    passBack = "2045";
                    break;
                case "96":
                    passBack = "2046";
                    break;
                case "97":
                    passBack = "2047";
                    break;
                case "98":
                    passBack = "2048";
                    break;
                case "99":
                    passBack = "2049";
                    break;
            }
        }

        private void LinkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (JBLFAMILY == true)
            {
                Process proc = new Process();
                if (mwarr.StartsWith("WARRANTY"))
                {
                    TheBody = "Account # 105081" + "%0A" + "%0A" + "Warranty Unit" + "%0A" + "%0A" + "Having trouble with: " + mms + "%0A" + "%0A";  // %0A = Enter
                }
                if (mwarr.StartsWith("NON-WARR"))
                {
                    TheBody = "Account # 105081" + "%0A" + "%0A" + "Non-Warranty Unit" + "%0A" + "%0A" + "Having trouble with: " + mms + "%0A" + "%0A";  // %0A = Enter
                }
                proc.StartInfo.FileName = "mailto:hprotechsupportusa@harman.com?subject=Tech_Support&body=" + TheBody;
                proc.Start();
            }

            if (make.StartsWith("LINE6"))
            {
                Line6UPC();
                if (mwarr.StartsWith("WARRANTY"))
                {
                    Process proc = new Process();
                    TheBody = "Account # 700451" + "%0A" + "%0A" + "Warranty Unit" + "%0A" + "%0A" + "Having trouble with: " + mms + "%0A" + "%0A";  // %0A = Enter
                    TheBody += "UCC 128 Tag:   " + mUCCTag + "%0A";
                    TheBody += "Product Code:  " + mProdCode + "%0A";
                    TheBody += "Manuf Code:    " + mManufCode + "%0A";
                    TheBody += "Manuf Year:    " + mMManufYear + "%0A";
                    TheBody += "Manuf Week:    " + mManufWeek + "%0A";
                    TheBody += "Weekly Serial: " + mWeeklySerial + "%0A";
                    TheBody += "Check Digit:   " + mCheckDigit + "%0A" + "%0A";
                    proc.StartInfo.FileName = "mailto:jmauck@line6.com?subject=Tech_Support&body=" + TheBody;
                    proc.Start();
                }
                if (mwarr.StartsWith("NON-WARR"))
                {
                    Process proc = new Process();
                    TheBody = "Account # 700451" + "%0A" + "%0A" + "Non-Warranty Unit" + "%0A" + "%0A" + "Having trouble with: " + mms + "%0A" + "%0A";  // %0A = Enter
                    TheBody += "UCC 128 Tag:   " + mUCCTag + "%0A";
                    TheBody += "Product Code:  " + mProdCode + "%0A";
                    TheBody += "Manuf Code:    " + mManufCode + "%0A";
                    TheBody += "Manuf Year:    " + mMManufYear + "%0A";
                    TheBody += "Manuf Week:    " + mManufWeek + "%0A";
                    TheBody += "Weekly Serial: " + mWeeklySerial + "%0A";
                    TheBody += "Check Digit:   " + mCheckDigit + "%0A" + "%0A";
                    proc.StartInfo.FileName = "mailto:jmauck@line6.com?subject=Tech_Support&body=" + TheBody;
                    proc.Start();
                }
            }

            if (make.StartsWith("GALL"))
            {
                if (mwarr.StartsWith("WARRANTY"))
                {
                    Process proc = new Process();
                    TheBody = "Account # S29500" + "%0A" + "%0A" + "Warranty Unit" + "%0A" + "%0A" + "Having trouble with: " + mms + "%0A" + "%0A";  // %0A = Enter
                    proc.StartInfo.FileName = "mailto:tech01@gallien.com?subject=Tech_Support&body=" + TheBody;
                    proc.Start();
                }
                if (mwarr.StartsWith("NON-WARR"))
                {
                    Process proc = new Process();
                    TheBody = "Account # S29500" + "%0A" + "%0A" + "Non-Warranty Unit" + "%0A" + "%0A" + "Having trouble with: " + mms + "%0A" + "%0A";  // %0A = Enter
                    proc.StartInfo.FileName = "mailto:tech01@gallien.com?subject=Tech_Support&body=" + TheBody;
                    proc.Start();
                }
            }

            if (make.StartsWith("KRK"))
            {
                if (mwarr.StartsWith("WARRANTY"))
                {
                    Process proc = new Process();
                    TheBody = "Account # OGA896" + "%0A" + "%0A" + "Warranty Unit" + "%0A" + "%0A" + "Having trouble with: " + mms + "%0A" + "%0A";  // %0A = Enter
                    proc.StartInfo.FileName = "mailto:service@gibson.com?subject=Tech_Support&body=" + TheBody;
                    proc.Start();
                }
                if (mwarr.StartsWith("NON-WARR"))
                {
                    Process proc = new Process();
                    TheBody = "Account # OGA896" + "%0A" + "%0A" + "Non-Warranty Unit" + "%0A" + "%0A" + "Having trouble with: " + mms + "%0A" + "%0A";  // %0A = Enter
                    proc.StartInfo.FileName = "mailto:service@gibson.com?subject=Tech_Support&body=" + TheBody;
                    proc.Start();
                }
            }

            if (make.StartsWith("YAMAHA"))
            {
                if (mwarr.StartsWith("WARRANTY"))
                {
                    Process proc = new Process();
                    TheBody = "Account # 700451" + "%0A" + "%0A" + "Warranty Unit" + "%0A" + "%0A" + "Having trouble with: " + mms + "%0A" + "%0A";  // %0A = Enter
                    proc.StartInfo.FileName = "mailto:techsupport@yamaha.com?subject=Tech_Support&body=" + TheBody;
                    proc.Start();
                }
                if (mwarr.StartsWith("NON-WARR"))
                {
                    Process proc = new Process();
                    TheBody = "Account # 700451" + "%0A" + "%0A" + "Non-Warranty Unit" + "%0A" + "%0A" + "Having trouble with: " + mms + "%0A" + "%0A";  // %0A = Enter
                    proc.StartInfo.FileName = "mailto:techsupport@yamaha.com?subject=Tech_Support&body=" + TheBody;
                    proc.Start();
                }
            }
            if (make.StartsWith("MACKIE"))
            {
                if (mwarr.StartsWith("WARRANTY"))
                {
                    Process proc = new Process();
                    TheBody = "Account # 21401" + "%0A" + "%0A" + "Warranty Unit" + "%0A" + "%0A" + "Having trouble with: " + mms + "%0A" + "%0A";  // %0A = Enter
                    proc.StartInfo.FileName = "mailto:techsupport@loudaudio.com?subject=Tech_Support&body=" + TheBody;
                    proc.Start();
                }
                if (mwarr.StartsWith("NON-WARR"))
                {
                    Process proc = new Process();
                    TheBody = "Account # 21401" + "%0A" + "%0A" + "Non-Warranty Unit" + "%0A" + "%0A" + "Having trouble with: " + mms + "%0A" + "%0A";  // %0A = Enter
                    proc.StartInfo.FileName = "mailto:techsupport@loudaudio.com?subject=Tech_Support&body=" + TheBody;
                    proc.Start();
                }
            }
            if (make.StartsWith("BIAMP"))
            {
                if (mwarr.StartsWith("WARRANTY"))
                {
                    Process proc = new Process();
                    TheBody = "" + "%0A" + "%0A" + "Warranty Unit" + "%0A" + "%0A" + "Having trouble with: " + mms + "%0A" + "%0A";  // %0A = Enter
                    proc.StartInfo.FileName = "mailto:service@bimap.com.com?subject=Tech_Support&body=" + TheBody;
                    proc.Start();
                }
                if (mwarr.StartsWith("NON-WARR"))
                {
                    Process proc = new Process();
                    TheBody = "" + "%0A" + "%0A" + "Non-Warranty Unit" + "%0A" + "%0A" + "Having trouble with: " + mms + "%0A" + "%0A";  // %0A = Enter
                    proc.StartInfo.FileName = "mailto:service@bimap.com.com?subject=Tech_Support&body=" + TheBody;
                    proc.Start();
                }
            }
        }

    }
}
