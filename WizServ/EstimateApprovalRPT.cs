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

namespace WizServ
{
    public partial class EstimateApprovalRPT : Form
    {
        private readonly string Estimates = @"I:\\Datafile\\Mel\\Estimates.CSV";                   // Estimates DB
        private readonly string ESTBU = @"I:\\Datafile\\Mel\\Estimates2.CSV";                   // Estimates DB
        public Icon image100 = Properties.Resources.WizServ;
        private int loopCount;
        public string claimno;
        public string SelectedText;
        public string CLAIM, FIRST, LAST, PARTS, LABOR, SHOP, SHIPPING, TAX, TOTAL, SENTDATE, INDEX, APPROVED, PAIDDOWN, RUSHFEE, mVBuyer, mVPmnt;
        private string FULLNAME;
        public string ans;
        private readonly string HEADER = "Index CLAIM       FULL NAME         APPROVED    TOTAL       PARTS     PAID DOWN\n";
        private readonly string msg = "Approved Estimates for " + DateTime.Now.ToShortDateString();
        public string SAVEDDATA, selected;

        public EstimateApprovalRPT()
        {
            InitializeComponent();
            Icon = image100;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
            label1.Text = msg;
            DisplayData();
            textBox1.Select();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            MainUtilitiesMenu f0 = new MainUtilitiesMenu();
            f0.Show();
        }

        private void richTextBox1_DoubleClick(object sender, EventArgs e)
        {
            SelectedText = richTextBox1.SelectedText;
        }

        private void PullData()
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

                loopCount = 0;

                while (!reader.EndOfStream)
                {
                    var lineRead = reader.ReadLine();
                    var values = lineRead.Split(',');

                    listA.Add(values[0]);   // Claim
                    listB.Add(values[1]);   // First
                    listC.Add(values[2]);   // Last
                    listD.Add(values[3]);   // Parts    $
                    listE.Add(values[4]);   // Labor    $
                    listF.Add(values[5]);   // Shop     $
                    listG.Add(values[6]);   // Shipping $
                    listH.Add(values[7]);   // Tax      $
                    listI.Add(values[8]);   // Total    $
                    listJ.Add(values[9]);   // Sent Date
                    listK.Add(values[10]);  // Index
                    listL.Add(values[11]);  // Approved Date
                    listM.Add(values[12]);  // Paid Down $
                    listN.Add(values[13]);  // Rush Fee  $

                    if (listA[loopCount] == SelectedText)
                    {
                        CLAIM = listA[loopCount];
                        FIRST = listB[loopCount];
                        LAST = listC[loopCount];
                        PARTS = listD[loopCount];
                        LABOR = listE[loopCount];
                        SHOP = listF[loopCount];
                        SHIPPING = listG[loopCount];
                        TAX = listH[loopCount];
                        TOTAL = listI[loopCount];
                        SENTDATE = listJ[loopCount];
                        INDEX = listK[loopCount];
                        APPROVED = listL[loopCount];
                        PAIDDOWN = listM[loopCount];
                        RUSHFEE = listN[loopCount];

                    }
                    loopCount++;
                }
                reader.Close(); // Close the open file
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 122: Sorry an error has occured: " + ex.Message);
            }
        }

        private void DisplayData()
        {
            richTextBox1.Text = "";
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

                loopCount = 0;
                richTextBox1.Text = HEADER + "\n";

                while (!reader.EndOfStream)
                {
                    var lineRead = reader.ReadLine();
                    var values = lineRead.Split(',');

                    listA.Add(values[0]);   // Claim
                    listB.Add(values[1]);   // First
                    listC.Add(values[2]);   // Last
                    listD.Add(values[3]);   // Parts    $
                    listE.Add(values[4]);   // Labor    $
                    listF.Add(values[5]);   // Shop     $
                    listG.Add(values[6]);   // Shipping $
                    listH.Add(values[7]);   // Tax      $
                    listI.Add(values[8]);   // Total    $
                    listJ.Add(values[9]);   // Sent Date
                    listK.Add(values[10]);  // Index
                    listL.Add(values[11]);  // Approved Date
                    listM.Add(values[12]);  // Paid Down $
                    listN.Add(values[13]);  // Rush Fee  $

                    APPROVED = listL[loopCount];
                    FULLNAME = listB[loopCount].Trim() + " " + listC[loopCount].Trim();

                    if (textBox1.Text.Length == 0)
                    {
                        if (listL[loopCount] == DateTime.Now.ToShortDateString() && textBox1.Text != "*")
                        {
                            label1.Text = msg;
                            CLAIM = listA[loopCount];
                            FIRST = listB[loopCount];
                            LAST = listC[loopCount];
                            PARTS = listD[loopCount];
                            var parts1 = Convert.ToDecimal(PARTS);
                            LABOR = listE[loopCount];
                            SHOP = listF[loopCount];
                            SHIPPING = listG[loopCount];
                            TAX = listH[loopCount];
                            TOTAL = listI[loopCount];
                            var tot = Convert.ToDecimal(TOTAL);
                            SENTDATE = listJ[loopCount];
                            INDEX = listK[loopCount];
                            APPROVED = listL[loopCount];
                            PAIDDOWN = listM[loopCount];
                            var paid = Convert.ToDecimal(PAIDDOWN);
                            RUSHFEE = listN[loopCount];

                            FULLNAME = FIRST.Trim() + " " + LAST.Trim();
                            FixName();
                            richTextBox1.Text = richTextBox1.Text + INDEX + "\t" + CLAIM + "\t" + FULLNAME + "\t" + APPROVED + "\t" + tot.ToString("C2") + "\t" + parts1.ToString("C2") + "\t" + paid.ToString("C2") + "\n";
                        }
                    }
                    if (textBox1.TextLength > 0 && textBox1.Text != "*")
                    {
                        if (listL[loopCount] == textBox1.Text)
                        {
                            label1.Text = "Approved Estimates for " + textBox1.Text;
                            CLAIM = listA[loopCount];
                            FIRST = listB[loopCount];
                            LAST = listC[loopCount];
                            PARTS = listD[loopCount];
                            var parts1 = Convert.ToDecimal(PARTS);
                            LABOR = listE[loopCount];
                            SHOP = listF[loopCount];
                            SHIPPING = listG[loopCount];
                            TAX = listH[loopCount];
                            TOTAL = listI[loopCount];
                            var tot = Convert.ToDecimal(TOTAL);
                            SENTDATE = listJ[loopCount];
                            INDEX = listK[loopCount];
                            APPROVED = listL[loopCount];
                            PAIDDOWN = listM[loopCount];
                            var paid = Convert.ToDecimal(PAIDDOWN);
                            RUSHFEE = listN[loopCount];

                            FULLNAME = FIRST.Trim() + " " + LAST.Trim();
                            FixName();
                            richTextBox1.Text = richTextBox1.Text + INDEX + "\t" + CLAIM + "\t" + FULLNAME + "\t" + APPROVED + "\t" + tot.ToString("C2") + "\t" + parts1.ToString("C2") + "\t" + paid.ToString("C2") + "\n";
                        }
                    }
                        if (textBox1.Text == "*")
                        {
                        var t = APPROVED.StartsWith("00");
                            if (t != true)
                            {
                                label1.Text = "Approved Estimates for " + textBox1.Text;
                            CLAIM = listA[loopCount];
                            FIRST = listB[loopCount];
                            LAST = listC[loopCount];
                            PARTS = listD[loopCount];
                            var parts1 = Convert.ToDecimal(PARTS);
                            LABOR = listE[loopCount];
                            SHOP = listF[loopCount];
                            SHIPPING = listG[loopCount];
                            TAX = listH[loopCount];
                            TOTAL = listI[loopCount];
                            var tot = Convert.ToDecimal(TOTAL);
                            SENTDATE = listJ[loopCount];
                            INDEX = listK[loopCount];
                            APPROVED = listL[loopCount];
                            PAIDDOWN = listM[loopCount];
                            var paid = Convert.ToDecimal(PAIDDOWN);
                            RUSHFEE = listN[loopCount];

                            FULLNAME = FIRST.Trim() + " " + LAST.Trim();
                                FixName();
                            richTextBox1.Text = richTextBox1.Text + INDEX + "\t" + CLAIM + "\t" + FULLNAME + "\t" + APPROVED + "\t" + tot.ToString("C2") + "\t" + parts1.ToString("C2") + "\t" + paid.ToString("C2") + "\n";
                        }
                    }
                    
                    loopCount++;
                }
                reader.Close(); // Close the open file
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 267: Sorry an error has occured: " + ex.Message);
            }
        }

        public void FixName()
        {
            switch (FULLNAME.Length)
            {
                case 6:
                    FULLNAME += "\t";
                    break;
                case 7:
                    FULLNAME += "\t";
                    break;
                case 8:
                    FULLNAME += "\t";
                    break;
                case 9:
                    FULLNAME += "\t";
                    break;
                case 10:
                    FULLNAME += "\t";
                    break;
                case 11:
                    FULLNAME += "\t";
                    break;
                default:
                    FULLNAME += "                ";
                    FULLNAME = FULLNAME.Substring(0, 16);
                    break;
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            richTextBox1.Text = "";
            selected = textBox1.Text;
            if (e.KeyCode == Keys.Enter)
            {
                DisplayData();
            }
            if (selected.Contains("*"))
            {
                SortBeforeDisplay();
                DisplayData();
            }
        }

        private void SortBeforeDisplay()
        {
            return;
            try
            {
                // Create the IEnumerable data source  
                string[] lines = File.ReadAllLines(Estimates);
                // @"I:\\Datafile\\Mel\\Estimates.CSV" = Estimates.csv
                // Create the query. Put field 11 first, then  
                // reverse and combine fields 0 and 1 from the old field  
                IEnumerable<string> query =
                    from line in lines
                    let x = line.Split(',')
                    orderby x[11]
                    select x[0] + "," + x[1] + "," + x[2] + "," + x[3] + "," + x[4] + "," + x[5]
                    + "," + x[6] + "," + x[7] + "," + x[8] + "," + x[9] + "," + x[10] + "," + x[11]
                    + "," + x[12] + "," + x[13];

                // Execute the query and write out the new file. Note that WriteAllLines  
                // takes a string[], so ToArray is called on the query.  
                File.WriteAllLines(ESTBU, query.ToArray());
                // @"I:\\Datafile\\Mel\\Estimates2.CSV" = ESTBU.csv

                File.Delete(Estimates);
                File.Move(ESTBU, Estimates); // Rename the oldFileName into newFileName
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error line 344\n" + ex);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                SAVEDDATA = richTextBox1.Text;
                printDocument1.Print();
            }
            else
            {
                SAVEDDATA = richTextBox1.Text;
                printDocument1.Print();
            }
            richTextBox1.Text = "";
            richTextBox1.Text = SAVEDDATA;
        }

        private void printDocument1_BeginPrint(object sender, PrintEventArgs e)
        {
            SAVEDDATA = richTextBox1.Text;
            richTextBox1.Text = "              Approved Estimates Report - " + DateTime.Now.ToShortDateString() + "\n\n" + richTextBox1.Text;

            char[] param = { '\n' };

            if (printDialog1.PrinterSettings.PrintRange == PrintRange.Selection)
            {
                lines = richTextBox1.SelectedText.Split(param);
            }
            else
            {
                lines = richTextBox1.Text.Split(param);
            }

            int i = 0;
            char[] trimParam = { '\r' };
            foreach (string s in lines)
            {
                lines[i++] = s.TrimEnd(trimParam);
            }
        }
        private int linesPrinted;
        private string[] lines;

        private void OnPrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;
            Brush brush = new SolidBrush(richTextBox1.ForeColor);

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

    }
}
