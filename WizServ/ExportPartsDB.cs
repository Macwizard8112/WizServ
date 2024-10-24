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
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace WizServ
{
    public partial class ExportPartsDB : Form
    {
        private static readonly string Ordered = @"I:\\Datafile\\Control\\Pri.CSV";  // This is Read only CSV
        public Icon image100 = Properties.Resources.WizServ;
        private int loopCount, foundcount, foundcount2;
        public string claimno = Version.Claim;
        public string from = Version.From;
        public string mTab = "\t";
        public string mTab2 = "\t\t";
        public decimal decimalRounded, decRounded;
        public string t = "0";
        public decimal t_price, mBOCost, partstotal, totRounded;
        private string dd, mTab3;
        private Font printFont;
        private StreamReader streamToPrint;
        public string FROM = Version.From;
        public string SELECTEDTEXT = Version.SELECTEDTEXT;  // Pass along from Password PRG to enable editing
        public int loop;
        private string searchTerm;

        public ExportPartsDB()
        {
            InitializeComponent();
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
            Icon = image100;
            ExportPartsData();
            searchTerm = "";
            textBox1.Text = "";
            textBox1.Select();
            textBox1.Focus();
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                searchTerm = textBox1.Text;
            }
            // Clear any previous highlighting
            listBox1.ClearSelected();

            if (string.IsNullOrEmpty(searchTerm))
            {
                return;
            }

            foreach (var item in listBox1.Items)
            {
                string listItem = item.ToString().ToLower();
                int index = listItem.IndexOf(searchTerm);

                if (index >= 0)
                {
                    // Highlight the search term within the item
                    listBox1.SetSelected(listBox1.Items.IndexOf(item), true);

                    // Jump to the highlighted item
                    listBox1.SetSelected(listBox1.Items.IndexOf(item), true);
                    break; // Exit loop after finding the first match
                }
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                TextWriter txt = new StreamWriter("I:\\Datafile\\Doc\\Pri.txt");
                txt.Write(richTextBox1.Text);
                txt.Close();
                // Print output file
                streamToPrint = new StreamReader("I:\\Datafile\\Doc\\Pri.txt");
                try
                {
                    printFont = new Font("Courier New", 10);
                    PrintDocument pd = new PrintDocument();
                    pd.DefaultPageSettings.Landscape = true;                    // Set to Landscape, False = Portrait
                    pd.PrintPage += new PrintPageEventHandler
                       (this.pd_PrintPage);
                    pd.Print();
                }
                finally
                {
                    streamToPrint.Close();
                }
                //PrintRTB3();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PrintRTB3()
        {
            try
            {
                TextWriter txt = new StreamWriter("I:\\Datafile\\Doc\\Pri.txt");
                txt.Write(richTextBox1.Text);
                txt.Close();
                // Print output file
                streamToPrint = new StreamReader("I:\\Datafile\\Doc\\Pri.txt");
                try
                {
                    printFont = new Font("Courier New", 10);
                    PrintDocument pd = new PrintDocument();
                    pd.DefaultPageSettings.Landscape = false;                    // Set to Landscape, False = Portrait
                    pd.PrintPage += new PrintPageEventHandler
                       (this.pd_PrintPage);
                    pd.Print();
                }
                finally
                {
                    streamToPrint.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pd_PrintPage(object sender, PrintPageEventArgs ev)
        {
            float linesPerPage = 0;
            float yPos = 0;
            int count = 0;
            float leftMargin = ev.MarginBounds.Left;
            float topMargin = ev.MarginBounds.Top;
            string line = null;

            // Calculate the number of lines per page.
            linesPerPage = ev.MarginBounds.Height /
               printFont.GetHeight(ev.Graphics);

            // Print each line of the file.
            while (count < linesPerPage &&
               ((line = streamToPrint.ReadLine()) != null))
            {
                yPos = topMargin + (count *
                   printFont.GetHeight(ev.Graphics));
                ev.Graphics.DrawString(line, printFont, Brushes.Black,
                   leftMargin, yPos, new StringFormat());
                count++;
            }

            // If more lines exist, print another page.
            if (line != null)
                ev.HasMorePages = true;
            else
                ev.HasMorePages = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            MainUtilitiesMenu f0 = new MainUtilitiesMenu();
            f0.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TextWriter txt = new StreamWriter("I:\\Datafile\\Doc\\Pri.txt");
            txt.Write(richTextBox1.Text);
            txt.Close();
            var fileToOpen = "I:\\Datafile\\Doc\\Pri.txt";
            if (!File.Exists(fileToOpen))
            {
                button1.PerformClick();
            }
            var process = new Process
            {
                StartInfo = new ProcessStartInfo()
                {
                    UseShellExecute = true,
                    FileName = fileToOpen
                }
            };

            process.Start();
            process.WaitForExit();
        }

        private void ExportPartsData()
        {
            listBox1.Items.Clear();
            try
            {
                StreamReader reader = new StreamReader(Ordered);
                String line = reader.ReadLine();

                List<string> listA = new List<string>();
                List<string> listB = new List<string>();
                List<string> listC = new List<string>();
                List<string> listD = new List<string>();
                List<string> listE = new List<string>();
                List<string> listF = new List<string>();
                List<string> listG = new List<string>();
                List<string> listH = new List<string>();

                loopCount = 0;
                richTextBox1.Text = richTextBox1.Text + "Index       Our Cost    Sell $      Qty   Description\n";
                //listBox1.Items.Add("Index   Part Number      Our Cost         Sell $         Qty    Description\n");

                while (!reader.EndOfStream)
                {
                    var lineRead = reader.ReadLine();
                    var values = lineRead.Split(',');

                    listA.Add(values[0]);   // Part_Num
                    listB.Add(values[1]);   // Description      43 Chars Max
                    listC.Add(values[2]);   // Price            Our Cost
                    listD.Add(values[3]);   // Stock
                    listE.Add(values[4]);   // Cost             Customer Cost
                    listF.Add(values[5]);   // Index
                    listG.Add(values[6]);   // VendorPN
                    listH.Add(values[7]);   // Vendor

                    var b = listC[loopCount];
                    var oc = Convert.ToDecimal(listC[loopCount]);
                    var cc = Convert.ToDecimal(listE[loopCount]);
                    var oc1 = oc.ToString("C2").Length;
                    var cc1 = cc.ToString("C2").Length;

                    if (oc1 == 5)
                    {
                        mTab = "\t";
                    }
                    if (oc1 == 6)
                    {
                        mTab = " ";
                    }
                    if (oc1 == 7)
                    {
                        mTab = " ";
                    }

                    string mCost = "";
                    string mCC = "";
                    richTextBox1.Text += listF[loopCount] + "\t\t" + oc.ToString("0.00") + mTab + "\t" + cc.ToString("C2") + "\t" + mTab + listD[loopCount] + "\t" + listB[loopCount] + "\n";
                    var gg = oc.ToString("0.00");
                    var hh = cc.ToString("0.00");
                    switch (gg.Length)
                    {
                        case 4:
                            mCost = "   " + oc.ToString("0.00");
                            break;
                        case 5:
                            mCost = "  " + oc.ToString("0.00");
                            break;
                        case 6:
                            mCost = " " + oc.ToString("0.00");
                            break;
                        case 7:
                            mCost = "" + oc.ToString("0.00");
                            break;
                    }
                    switch (hh.Length)
                    {
                        case 4:
                            mCC = "   " + cc.ToString("0.00");
                            break;
                        case 5:
                            mCC = "  " + cc.ToString("0.00");
                            break;
                        case 6:
                            mCC = " " + cc.ToString("0.00");
                            break;
                        case 7:
                            mCC = "" + cc.ToString("0.00");
                            break;

                    }
                    string mQuantity = listD[loopCount];
                    switch (mQuantity.Length)
                    {
                        case 4:
                            mQuantity = "" + mQuantity;
                            break;
                        case 3:
                            mQuantity = " " + mQuantity;
                            break;
                        case 2:
                            mQuantity = "  " + mQuantity;
                            break;
                        case 1:
                            mQuantity = "   " + mQuantity;
                            break;

                    }
                    string ThePartNum = listA[loopCount];
                    switch (ThePartNum)
                    {
                        case "1":
                            ThePartNum = "0001";
                            break;
                        case "2":
                            ThePartNum = "0002";
                            break;
                        case "3":
                            ThePartNum = "0003";
                            break;
                        case "4":
                            ThePartNum = "0004";
                            break;
                        case "5":
                            ThePartNum = "0005";
                            break;
                        case "6":
                            ThePartNum = "0006";
                            break;
                        case "7":
                            ThePartNum = "0007";
                            break;
                        case "8":
                            ThePartNum = "0008";
                            break;
                        case "9":
                            ThePartNum = "0009";
                            break;
                    }

                    switch (ThePartNum.Length)
                    {
                        case 15:
                            ThePartNum = ThePartNum + "";
                            break;
                        case 14:
                            ThePartNum = ThePartNum + " ";
                            break;
                        case 13:
                            ThePartNum = ThePartNum + "  ";
                            break;
                        case 12:
                            ThePartNum = ThePartNum + "   ";
                            break;
                        case 11:
                            ThePartNum = ThePartNum + "    ";
                            break;
                        case 10:
                            ThePartNum = ThePartNum + "     ";
                            break;
                        case 9:
                            ThePartNum = ThePartNum + "      ";
                            break;
                        case 8:
                            ThePartNum = ThePartNum + "       ";
                            break;
                        case 7:
                            ThePartNum = ThePartNum + "        ";
                            break;
                        case 6:
                            ThePartNum = ThePartNum + "         ";
                            break;
                        case 5:
                            ThePartNum = ThePartNum + "          ";
                            break;
                        case 4:
                            ThePartNum = ThePartNum + "           ";
                            break;
                        case 3:
                            ThePartNum = ThePartNum + "            ";
                            break;
                        case 2:
                            ThePartNum = ThePartNum + "             ";
                            break;
                        case 1:
                            ThePartNum = ThePartNum + "              ";
                            break;
                    }
                    listBox1.Items.Add(listF[loopCount] + "\t" + ThePartNum + "\t" + mCost + "\t\t" + mCC + "\t\t" + mQuantity + "\t" + listB[loopCount] + "\n");
                    loopCount++;
                    loop++;
                }
               

                reader.Close(); // Close the open file
                var t = loop / 67;
                label1.Text = "Pages: " + t.ToString();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error 232: Sorry an error has occured: " + ex.Message);
                var t = loop / 67;
                label1.Text = "Pages: " + t.ToString();
            }
        
        }
    }
}
