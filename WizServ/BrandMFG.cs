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
using System.Threading.Tasks;
using System.Management;
using System.Drawing.Printing;
using System.Threading.Tasks;

namespace WizServ
{
    public partial class BrandMFG : Form
    {
        public readonly string DBSortedM1 = @"I:\Datafile\Control\BrandSortedMFG1.CSV";
        public Icon image100 = Properties.Resources.WizServ;
        private string mBrand, mModel, mFname, mLname, mClaim_NO, mStatus;
        private int linesPrinted, loopCount;
        public bool IsChecked;
        private string[] lines;
        public string t, k;

        public BrandMFG()
        {
            InitializeComponent();
            label2.Visible = false;
            panel1.Visible = false;
            var k = " Wizard Electronics Nightly Brand Report by MFG " + DateTime.Now.ToShortDateString() + " ";
            label1.Text = k;
            Icon = image100;
            GetData();
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            richTextBox1.SelectAll();
            t = richTextBox1.Text;
            richTextBox1.DeselectAll();
            k = "               Wizard Electronics Nightly Brand Report by MFG " + DateTime.Now.ToShortDateString() + "\n\n";
            richTextBox1.Text = "";
            richTextBox1.Text = k + t;
            {
                printDocument1.DefaultPageSettings.Landscape = false;   // false = Portrait, true = landscape
                int x = e.MarginBounds.Left;
                int y = e.MarginBounds.Top;
                x -= 60;                        // Move to Center on paper
                y -= 30;

                Brush brush = new SolidBrush(richTextBox1.ForeColor);

                char[] param = { '\n' };

                lines = richTextBox1.Text.Split(param);

                while (linesPrinted < lines.Length)
                {
                    e.Graphics.DrawString(lines[linesPrinted++],
                        richTextBox1.Font, brush, x, y);
                    y += 15;
                    if (y >= e.MarginBounds.Bottom)
                    {
                        e.HasMorePages = true;
                        return;
                    }
                }
                linesPrinted = 0;
                e.HasMorePages = false;
            }
            richTextBox1.Text = "";
            richTextBox1.Text = t;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string _Path = @"I:\Datafile\Control\BrandDailyMFG.txt";

            richTextBox1.SelectAll();
            t = richTextBox1.Text;
            richTextBox1.DeselectAll();
            k = "            Wizard Electronics Nightly Brand Report by MFG " + DateTime.Now.ToShortDateString() + "\n";
            richTextBox1.Text = "";
            var j = "                          (Sorted by Manufacturer)\n";
            var l = "=======================================================================================\n";
            var f = "Manuf.           Model #       First Name    Last Name     Claim#  Disposition\n";
            var m = k + j + l + f + l + t;
            //richTextBox1.Text = k + t;
            richTextBox1.Text = m;
            if (!File.Exists(_Path))
            {
                StreamWriter SW = new StreamWriter(_Path);
                SW.WriteLine(richTextBox1.Text);
                SW.Close();
            }
            else if (File.Exists(_Path))
            {
                StreamWriter SW = new StreamWriter(_Path);
                SW.WriteLine(richTextBox1.Text);
                SW.Close();
            }
            richTextBox1.Text = "";
            richTextBox1.Text = t;
            label2.Visible = true;
            panel1.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                IsChecked = true;
                printDocument1.Print();
                richTextBox1.Text = "";
                richTextBox1.Text = t;
            }
            if (IsChecked == true)
            {
                Thread.Sleep(3000);
                Hide();
                MainMenu f2 = new MainMenu();
                f2.Show();
            }
        }

        private void GetData()
        {
            loopCount = 0;
            StreamReader reader = new StreamReader(DBSortedM1, Encoding.GetEncoding("Windows-1252"));
            String line = reader.ReadLine();

            List<string> listA = new List<string>();
            List<string> listB = new List<string>();
            List<string> listC = new List<string>();
            List<string> listD = new List<string>();
            List<string> listE = new List<string>();
            List<string> listF = new List<string>();

            while (!reader.EndOfStream)
            {
                var lineRead = reader.ReadLine();
                var values = lineRead.Split(',');

                listA.Add(values[0]);       //  Manuf
                listB.Add(values[1]);       //  Model
                listC.Add(values[2]);       //  fname           Customer First Name
                listD.Add(values[3]);       //  lname           Customer Last Name
                listE.Add(values[4]);       //  Claim #
                listF.Add(values[5]);       //  Disposition

                mBrand = listA[loopCount];
                mModel = listB[loopCount];
                mFname = listC[loopCount];
                mLname = listD[loopCount];
                mClaim_NO = listE[loopCount];
                mStatus = listF[loopCount];

                mBrand += "                       ";
                mModel += "                    ";
                mFname += "                    ";
                mLname += "                    ";
                mClaim_NO += "  ";
                mStatus += "                    ";

                var col1 = mBrand.Substring(0, 16);
                var col2 = mModel.Substring(0, 13);
                var col3 = mFname.Substring(0, 13);
                var col4 = mLname.Substring(0, 13);
                var col5 = mClaim_NO.Substring(0, 7);
                var col6 = mStatus.Substring(0, 20);

                if (!mStatus.StartsWith("CONSIGNMENT"))
                {
                    if (!mStatus.StartsWith("SENT"))
                    {
                        if (!mStatus.StartsWith("COMPLETE"))
                        {
                            richTextBox1.Text += col1 + " " + col2 + " " + col3 + " " + col4 + " " + col5 + " " + col6 + "\n";

                        }
                    }
                }
                loopCount++;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }
    }
}
