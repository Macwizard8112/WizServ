using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WizServ
{
    public partial class ReprintClaim : Form
    {
        public string claim_no = Version.Claim;
        public Icon image100 = Properties.Resources.WizServ;
        private readonly string file = @"I:\\Datafile\\Control\\Database.CSV";
        private readonly string PartsUsed = @"I:\Datafile\Control\partsused.csv";
        static readonly string key = @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\lanmanserver\parameters";
        public readonly string computerDescription = (string)Registry.GetValue(key, "srvcomment", null);
        private int loopCount, loop;
        private string IsClosed = "";
        public bool Found = false;
        public string Mex, TheFileIs, TheFileNameIs, yeardigit;
        private StreamReader streamToPrint;
        private Font printFont;
        private string printerName;
        private float leftMargin, topMargin;
        public string CR = Environment.NewLine, mSN, line1;
        public string X, Y, Z, IsBold;
        private int claimIndex1;

        public ReprintClaim()
        {
            InitializeComponent();
            textBox1.Focus();
            textBox1.SelectAll();
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
            Icon = image100;
            label2.Visible = false;
            label1.Text = claim_no;
            GetData();
            textBox1.Focus();
            textBox1.SelectAll();
        }

        private void ReprintClaim_Load(object sender, EventArgs e)
        {
            LoadCsvFile();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            ClaimsMGTMenu f0 = new ClaimsMGTMenu();
            f0.Show();
        }

        private void LoadCsvFile()
        {
            X = "";
            Y = "";
            Z = "";
            textBox1.Focus();
            textBox1.SelectAll();
            try
            {
                if (string.IsNullOrEmpty(claim_no))
                {
                    MessageBox.Show("Please enter a claim number.");
                    return;
                }

                string filePath = @"I:\Datafile\Control\Database.csv";
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    string[] headers = sr.ReadLine().Split(',');

                    int claimIndex = Array.IndexOf(headers, "1"); // Change "ClaimNumber" to the actual header of the claim number column
                    if (claimIndex == -1)
                    {
                        MessageBox.Show("Claim number column not found.");
                        return;
                    }
                    
                    richTextBox2.Text = "";

                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] values = line.Split(',');

                        if (values[claimIndex].Equals(claim_no, StringComparison.OrdinalIgnoreCase))
                        {
                            // Select specific columns, e.g., 0 for column 1, 2 for column 3, and 4 for column 5
                            string selectedColumns = $"{values[0]}, {values[2]}, {values[4]}";
                            label1.Text = "Claim #: " + values[1];
                            richTextBox2.Text  = "▓▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▓  Date In :  " + values[2] + "  ▬► CLAIM# " + values[1]  + " ◄▬" + CR;
                            IsBold = "  CLAIM# " + values[1];
                            richTextBox2.Text += "▓ Wizard Electronics, Inc.         ▓  Product :  " + values[61] + CR;
                            richTextBox2.Text += "▓ 554 Deering Road Northwest       ▓  Brand   :  " + values[12] + CR;
                            richTextBox2.Text += "▓ Atlanta, GA 30309-2267           ▓  Model   :  " + values[14] + CR;
                            if (values[15].EndsWith("_"))
                            {
                                var k = values[15].Length;
                                mSN = values[15].Substring(0, (k - 1));
                            }
                            else
                            {
                                mSN = values[15];
                            }
                            richTextBox2.Text += "▓ (404)325-4891 Fax (404)325-4175  ▓  Serial #:  " + mSN + CR;
                            richTextBox2.Text += "▓▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▓  Shelf   :  " + values[56].ToUpper() + "  Rush Claim: " + values[75] + CR + CR;
                            richTextBox2.Text += "Customer Name:    " + values[3] + ", " + values[4] + "     " + values[33] + CR;
                            richTextBox2.Text += "Cust. Address:    " + values[5] + CR;
                            richTextBox2.Text += "City, State, Zip: " + values[6] + "  " + values[7] + "  " + values[8] + CR;
                            richTextBox2.Text += "Home Phone Number " + values[9] + "  Work Phone: " + values[10] + CR;
                            richTextBox2.Text += "Email Address:    " + values[67].ToLower() + CR;
                            richTextBox2.Text += "▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬" + CR;
                            richTextBox2.Text += "Client/Dealer Name: " + values[34] + "    Dealer Phone:  " + values[39] + CR;
                            richTextBox2.Text += "Address:            " + values[35] + "    Dealer Number: " + values[57] + CR;
                            richTextBox2.Text += "City, State, Zip:   " + values[36] + "  " + values[37] + "  " + values[38] + CR;
                            richTextBox2.Text += "▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬" + CR;
                            richTextBox2.Text += "Unit Status is: " + values[45] + "              ***** This is a Reprint *****" + CR;
                            richTextBox2.Text += "No warranty on repairs W/O Sales Receipt/RA# at drop off. If NOT warranty,\n";
                            richTextBox2.Text += "EST Diagnostic Fee will apply if repair declined. Items left over 10 days,\n";
                            richTextBox2.Text += "After repair completed, We add a $ 1.00/Day storage fee.\n";
                            richTextBox2.Text += "▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬" + CR;
                            richTextBox2.Text += "Problem: " + values[11] + "\n"; ;
                            richTextBox2.Text += "Problem: " + values[30] + "\n";
                            richTextBox2.Text += "Problem: " + "" + "\n";
                            richTextBox2.Text += "▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬" + CR;
                            richTextBox2.Text += "Technical Services Rendered:\n";
                            richTextBox2.Text += values[46] + CR;
                            richTextBox2.Text += values[47] + CR;
                            richTextBox2.Text += values[48] + CR;
                            richTextBox2.Text += values[49] + CR;
                            richTextBox2.Text += "▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬" + CR;
                            richTextBox2.Text += "Materials Used:                               " + CR + CR;
                            richTextBox2.Text += "QTY    Part Number          Description                   Cost" + CR;

                            using (StreamReader PARTS = new StreamReader(PartsUsed))
                            {

                                string[] headers1 = PARTS.ReadLine().Split(',');

                                claimIndex1 = Array.IndexOf(headers1, "Claim"); // Change "ClaimNumber" to the actual header of the claim number column
                                if (claimIndex1 == -1)
                                {
                                    MessageBox.Show("Claim number column not found.");
                                    return;
                                }

                                while ((line1 = PARTS.ReadLine()) != null)
                                {
                                    string[] values1 = line1.Split(',');

                                    if (values1[claimIndex1].Equals(Version.Claim, StringComparison.OrdinalIgnoreCase))
                                    {
                                        X = values1[3];
                                        Y = values1[6];
                                        Z = values1[1];
                                        FixLength();
                                        if (Z.Trim().Length <= 4)
                                        {
                                            richTextBox2.Text += values1[0] + "      " + Z + "    " + X + "  $  " + Y + CR;
                                        }
                                        else
                                        {
                                            richTextBox2.Text += values1[0] + "      " + Z + "    " + X + "  $ " + Y + CR;
                                        }
                                    }
                                }
                            }
                            richTextBox2.Text += "▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬" + CR;
                            richTextBox2.Text += "Repairs Breakdown:" + CR;
                            var t = values[17];
                            var h = Convert.ToDecimal(t);
                            richTextBox2.Text += "Labor:        $  " + h.ToString("0.00") + CR;
                            t = values[18];
                            h = Convert.ToDecimal(t);
                            richTextBox2.Text += "Parts:        $  " + h.ToString("0.00") + CR;
                            if (values[26] == "65")
                            {
                                t = values[26];
                                h = Convert.ToDecimal(t);
                                richTextBox2.Text += "Diagnostics:  $   " + h.ToString("0.00") + CR;
                                h = 0.00m;
                                richTextBox2.Text += "Parts Ship:   $   " + h.ToString("0.00") + CR;
                                t = values[27];
                                h = Convert.ToDecimal(t);
                                if (t.Length <= 5)
                                {
                                    richTextBox2.Text += "GA Tax:       $   " + h.ToString("0.00") + CR;
                                }
                                if (t.Length == 6)
                                {
                                    richTextBox2.Text += "GA Tax:       $  " + h.ToString("0.00") + CR;
                                }

                            }
                            if (values[26] == "85")
                            {
                                t = values[26];
                                h = Convert.ToDecimal(t);
                                h = h - 20;
                                richTextBox2.Text += "Diagnostics:  $   " + h.ToString("0.00") + CR;
                                t = values[26];
                                h = Convert.ToDecimal(t);
                                h = h - 65;
                                richTextBox2.Text += "Parts Ship:   $   " + h.ToString("0.00") + CR;
                                t = values[27];
                                h = Convert.ToDecimal(t);
                                if (t.Length <= 5)
                                {
                                    richTextBox2.Text += "GA Tax:       $   " + h.ToString("0.00") + CR;
                                }
                                if (t.Length == 6)
                                {
                                    richTextBox2.Text += "GA Tax:       $  " + h.ToString("0.00") + CR;
                                }
                            }
                            richTextBox2.Text += "------------------------------" + CR;
                            t = values[20];
                            h = Convert.ToDecimal(t);
                            richTextBox2.Text += "Down Payment  $ -" + h.ToString("0.00") + CR;
                            richTextBox2.Text += "------------------------------" + CR;
                            t = values[16];
                            h = Convert.ToDecimal(t);
                            richTextBox2.Text += "Balance Due   $  " + h.ToString("0.00") + CR + CR;
                            richTextBox2.Text += "Items left over 45 days will be sold. I authorize service as specified.\n";
                            richTextBox2.Text += "Rush Charge is $ 50.00 in addition to diagnostics fee / repair charges. \n";
                            richTextBox2.Text += "Payment must be Cash/Bank Card\n\n\n";
                            richTextBox2.Text += "Signed:__________________________________________       Date: " + DateTime.Now.ToShortDateString() + "\n\n";

                            //textBox1.AppendText(selectedColumns + Environment.NewLine);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: \n" + ex.Message);
            }
            MakeWordBold(IsBold);
        }

        private void MakeWordBold(string word)
        {
            int startIndex = richTextBox2.Text.IndexOf(word);
            if (startIndex != -1)
            {
                richTextBox2.Select(startIndex, word.Length);
                richTextBox2.SelectionFont = new Font(richTextBox2.Font, FontStyle.Bold);
            }
        }

        public void FixLength()
        {
            switch (X.Length)
            {
                case 2:
                    X += "                        ";
                    break;
                case 3:
                    X += "                       ";
                    break;
                case 4:
                    X += "                      ";
                    break;
                case 5:
                    X += "                     ";
                    break;
                case 6:
                    X += "                    ";
                    break;
                case 7:
                    X += "                   ";
                    break;
                case 8:
                    X += "                  ";
                    break;
                case 9:
                    X += "                 ";
                    break;
                case 10:
                    X += "                ";
                    break;
                case 11:
                    X += "               ";
                    break;
                case 12:
                    X += "              ";
                    break;
                case 13:
                    X += "             ";
                    break;
                case 14:
                    X += "            ";
                    break;
                case 15:
                    X += "           ";
                    break;
                case 16:
                    X += "          ";
                    break;
                case 17:
                    X += "         ";
                    break;
                case 18:
                    X += "        ";
                    break;
                case 19:
                    X += "       ";
                    break;
                case 20:
                    X += "      ";
                    break;
                case 21:
                    X += "     ";
                    break;
                case 22:
                    X += "    ";
                    break;
                case 23:
                    X += "   ";
                    break;
                case 24:
                    X += "  ";
                    break;
                case 25:
                    X += " ";
                    break;
            }
            switch (Z.Length)
            {
                case 2:
                    Z += "               ";
                    break;
                case 3:
                    Z += "              ";
                    break;
                case 4:
                    Z += "             ";
                    break;
                case 5:
                    Z += "            ";
                    break;
                case 6:
                    Z += "           ";
                    break;
                case 7:
                    Z += "          ";
                    break;
                case 8:
                    Z += "         ";
                    break;
                case 9:
                    Z += "        ";
                    break;
                case 10:
                    Z += "       ";
                    break;
                case 11:
                    Z += "      ";
                    break;
                case 12:
                    Z += "     ";
                    break;
                case 13:
                    Z += "    ";
                    break;
                case 14:
                    Z += "   ";
                    break;
                case 15:
                    Z += "  ";
                    break;
                case 16:
                    Z += " ";
                    break;

            }

        }


        private void button2_Click(object sender, EventArgs e)
        { 
            try
            {
                // Create output file
                TextWriter txt = new StreamWriter("I:\\Datafile\\Doc\\ClaimNum.RTF");
                txt.Write(richTextBox2.Text);
                txt.Close();
                // Print output file
                streamToPrint = new StreamReader("I:\\Datafile\\Doc\\ClaimNum.RTF");
                try
                {
                    printFont = new Font("Courier New", 10, FontStyle.Bold);
                    PrintDocument pd = new PrintDocument();
                    pd.DefaultPageSettings.Landscape = false;                    // Set to Landscape, False = Portrait

                    // Adjust margins
                    pd.DefaultPageSettings.Margins = new Margins(25, 50, 40, 100);

                    // Set the number of copies
                    var mCopies = Convert.ToInt16(textBox1.Text);
                    pd.PrinterSettings.Copies = mCopies; // Set to 3 copies
      
                    pd.PrintPage += new PrintPageEventHandler
                       (this.pd_PrintPage);
                    // Get the printer name
                    printerName = pd.PrinterSettings.PrinterName;
                    //MessageBox.Show($"Printer Name: {printerName}", "Printer Info");
                    pd.Print();
                }
                finally
                {
                    streamToPrint.Close();
                }
                label2.Visible = true;
                label2.Text = "Print Request Sent.";
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
            //float leftMargin = ev.MarginBounds.Left;
            leftMargin = 25;                          // Setup for Pantum Printer
            //float topMargin = ev.MarginBounds.Top;
            topMargin = 20;                           // Setup for Pantum Printer
            string line = null;
            if (!printerName.Contains("Pantum"))
            {
                leftMargin = 50;
                topMargin = 40;
            }
            

            // Calculate the number of lines per page.
            linesPerPage = ev.MarginBounds.Height / printFont.GetHeight(ev.Graphics);

            // Print each line of the file.
            while (count < linesPerPage && ((line = streamToPrint.ReadLine()) != null))
            {
                yPos = topMargin + (count * printFont.GetHeight(ev.Graphics));
                ev.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());
                count++;
            }

            // If more lines exist, print another page.
            ev.HasMorePages = (line != null);
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
                        yy--;
                        var uu = tt.Substring(1, yy);
                        mTheNewClaimNum = uu;
                    }

                    if (claim_no.Length == 6)
                    {
                        if (mClaim_NO == claim_no)
                        {
                            Found = true;
                            richTextBox1.Text = "************************************\t Date In: " + mDate_IN + "    CLAIM # " + mClaim_NO + "\n";
                            richTextBox1.Text = richTextBox1.Text + "* Wizard Electronics, Inc.         *\t Prodcut: " + listBJ[loopCount] + "\n";
                            richTextBox1.Text = richTextBox1.Text + "* 554 Deering Road Northwest       *\t Brand:   " + mBrand + "\n";
                            richTextBox1.Text = richTextBox1.Text + "* Atlanta, GA 30309-2267           *\t Model:   " + mModel + "\n";
                            richTextBox1.Text = richTextBox1.Text + "* (404)325-4891 Fax (404)325-4175  *\t Serial#: " + mSerial + "\n";
                            richTextBox1.Text = richTextBox1.Text + "************************************\t Shelf Location: " + listBE[loopCount] + " Rush Claim: " + mRush + "\n";
                            richTextBox1.Text += "\n";
                            richTextBox1.Text = richTextBox1.Text + "Customer Name:    " + mFname + ", " + mLname + "\t\t" + listAH[loopCount] + "\n";
                            richTextBox1.Text = richTextBox1.Text + "Customer Address: " + mAddr + "\n";
                            richTextBox1.Text = richTextBox1.Text + "City, State, Zip: " + mCity + ", " + mState + " " + mZip + " " + "Home : " + mHphone + " Work: " + mWPhone + "\n";
                            richTextBox1.Text = richTextBox1.Text + "Email:            " + mEmail + "\n";
                            richTextBox1.Text += "*****************************************************************************\n";
                            richTextBox1.Text = richTextBox1.Text + "Client/Dealer name: " + listAI[loopCount] + "\tPhone: " + listAN[loopCount] + "\n";
                            richTextBox1.Text = richTextBox1.Text + "Address:            " + listAJ[loopCount] + "\t\t Invoice/Claim # " + listBF[loopCount] + "\n";
                            richTextBox1.Text = richTextBox1.Text + "City, State, Zip:   " + listAK[loopCount] + " " + listAL[loopCount] + "  " + listAM[loopCount] + "\n";
                            richTextBox1.Text += "*****************************************************************************\n";
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
                            richTextBox1.Text += "\t             Total: $ " + total.ToString() + "\n\n";
                            richTextBox1.Text += "No warranty repairs W/O Sales Receipt/RA# at drop off. If NOT warranty,\n";
                            richTextBox1.Text += "EST Diagnostic Fee will apply if repair declined. Items left over 10 days,\n";
                            richTextBox1.Text += "add $ 1.00/Day storage fee.\n";
                            richTextBox1.Text += "*****************************************************************************\n";
                            richTextBox1.Text += "Problem: " + mProblem + "\n"; ;
                            richTextBox1.Text += "Problem: " + mFthr_exp1 + "\n";
                            richTextBox1.Text += "Problem: " + mFthr_exp2 + "\n";
                            richTextBox1.Text += "*****************************************************************************\n";
                            richTextBox1.Text += "Technical Services Rendered:\n";
                            richTextBox1.Text += listAU[loopCount] + "\n";
                            richTextBox1.Text += listAV[loopCount] + "\n";
                            richTextBox1.Text += listAW[loopCount] + "\n";
                            richTextBox1.Text += listAX[loopCount] + "\n";
                            richTextBox1.Text += "*****************************************************************************\n";
                            richTextBox1.Text += "Materials Used:                               *\n";
                            richTextBox1.Text += "                                              *    Totals:\n";
                            richTextBox1.Text += "QTY    Part Number     Description            *    \n";
                            richTextBox1.Text += "_____________________________________________ *    Services      $ " + d.ToString("0.##") + "\n";
                            richTextBox1.Text += "_____________________________________________ *    Parts         $  " + f.ToString("0.##") + "\n";
                            richTextBox1.Text += "_____________________________________________ *    Other         $   " + k.ToString("0.##") + "\n";
                            richTextBox1.Text += "_____________________________________________ *    Tax           $  " + tax.ToString("0.##") + "\n";
                            richTextBox1.Text += "_____________________________________________ *    Down Payment  $  65.00\n";
                            richTextBox1.Text += "_____________________________________________ *    ========================\n";
                            richTextBox1.Text += "_____________________________________________ *    Grand Total   $ " + newtotal.ToString("0.##") + "\n";
                            richTextBox1.Text += "*****************************************************************************\n";
                            richTextBox1.Text += "Items left over 45 days will be sold. I authorize service as specified.\n";
                            richTextBox1.Text += "Rush Charge is $ 50.00 in addition to repair charges. \n";
                            richTextBox1.Text += "Payment must be Cash/Bank Card\n\n";
                            richTextBox1.Text += "Signed:______________________________________   \tDate: " + DateTime.Now.ToShortDateString() + "\n\n";
                            richTextBox1.Text += IsClosed + "\n";
                            loop++;
                        }
                        loopCount++;
                    }
                    //loopCount++;
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
                if (Mex.Contains("IndexOutOfRangeException"))
                {
                    MessageBox.Show("IndexOutOfRangeException");
                }

                MessageBox.Show("Error 1329: Sorry an error has occured: " + ex.Message);
            }
        }
    }
}
