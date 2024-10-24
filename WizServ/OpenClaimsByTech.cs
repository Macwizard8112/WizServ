using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WizServ
{
    public partial class OpenClaimsByTech : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        private int loopCount, loop;
        private string TECHNICIAN;
        private static readonly string Database = @"I:\\Datafile\\Control\\Database.CSV";
        private int linesPrinted;
        private string[] lines;
        public string PCNAME;

        public bool Found { get; private set; }

        public OpenClaimsByTech()
        {
            InitializeComponent();
            Icon = image100;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            CustStatusMenu f2 = new CustStatusMenu();
            f2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }

        private void button5_Click(object sender, EventArgs e)  // Walter
        {
            TECHNICIAN = "WALTER";
            richTextBox1.Text = "";
            loop = 0;
            loopCount = 0;
            GetData();
        }

        private void button4_Click(object sender, EventArgs e)  // Derek
        {
            TECHNICIAN = "DEREK";
            richTextBox1.Text = "";
            loop = 0;
            loopCount = 0;
            GetData();
        }

        private void button3_Click(object sender, EventArgs e)  // Cole
        {
            TECHNICIAN = "COLE";
            richTextBox1.Text = "";
            loop = 0;
            loopCount = 0;
            GetData();
        }

        private void button6_Click(object sender, EventArgs e)  // Billy
        {
            TECHNICIAN = "BILLY";
            richTextBox1.Text = "";
            loop = 0;
            loopCount = 0;
            GetData();
        }

        private void button7_Click(object sender, EventArgs e)  // Noel
        {
            TECHNICIAN = "NOEL";
            richTextBox1.Text = "";
            loop = 0;
            loopCount = 0;
            GetData();
        }

        public void GetData()
        {
            try
            {
                if (Version.DatabaseIsLocked == true)
                {
                    MessageBox.Show("Database locked, retrying...");
                    Thread.Sleep(3000);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database locked, retrying...");
                Thread.Sleep(3000);
                GetData();
            }
            Version.DatabaseIsLocked = true;
            loop = 0;
            loopCount = 0;

            try
            {
                StreamReader reader = new StreamReader(Database, Encoding.GetEncoding("Windows-1252"));
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

                    switch (mBrand.Length)
                    {
                        case 2:
                            mBrand += "                    ";
                            break;
                        case 3:
                            mBrand += "                   ";
                            break;
                        case 4:
                            mBrand += "                  ";
                            break;
                        case 5:
                            mBrand += "                 ";
                            break;
                        case 6:
                            mBrand += "                ";
                            break;
                        case 7:
                            mBrand += "               ";
                            break;
                        case 8:
                            mBrand += "              ";
                            break;
                        case 9:
                            mBrand += "             ";
                            break;
                        case 10:
                            mBrand += "            ";
                            break;
                        case 11:
                            mBrand += "            ";
                            break;
                        case 12:
                            mBrand += "           ";
                            break;
                        case 13:
                            mBrand += "          ";
                            break;
                        case 14:
                            mBrand += "         ";
                            break;
                        case 15:
                            mBrand += "        ";
                            break;
                        case 16:
                            mBrand += "       ";
                            break;
                        case 17:
                            mBrand += "      ";
                            break;

                    }

                    switch (mModel.Length)
                    {
                        case 2:
                            mModel += "                      ";
                            break;
                        case 3:
                            mModel += "                     ";
                            break;
                        case 4:
                            mModel += "                    ";
                            break;
                        case 5:
                            mModel += "                   ";
                            break;
                        case 6:
                            mModel += "                  ";
                            break;
                        case 7:
                            mModel += "                 ";
                            break;
                        case 8:
                            mModel += "                ";
                            break;
                        case 9:
                            mModel += "               ";
                            break;
                        case 10:
                            mModel += "              ";
                            break;
                        case 11:
                            mModel += "             ";
                            break;
                        case 12:
                            mModel += "            ";
                            break;
                        case 13:
                            mModel += "           ";
                            break;
                        case 14:
                            mModel += "          ";
                            break;
                        case 15:
                            mModel += "         ";
                            break;
                        case 16:
                            mModel += "        ";
                            break;
                        case 17:
                            mModel += "       ";
                            break;
                        case 18:
                            mModel += "      ";
                            break;
                        case 19:
                            mModel += "     ";
                            break;
                        case 20:
                            mModel += "     ";
                            break;
                        case 21:
                            mModel += "    ";
                            break;
                        case 22:
                            mModel += "    ";
                            break;
                        case 23:
                            mModel += "   ";
                            break;
                        case 24:
                            mModel += "  ";
                            break;
                        case 25:
                            mModel += " ";
                            break;
                    }
                    switch (mIsWarr.Length)
                    {
                        case 8:
                            mIsWarr += "    ";
                            break;
                    }
                    switch (mBench.Length)
                    {
                        case 8:
                            mBench += "                 ";
                            break;
                        case 9:
                            mBench += "                ";
                            break;
                        case 16:
                            mBench += "          ";
                            break;
                        case 19:
                            mBench += "       ";
                            break;
                        case 20:
                            mBench += "      ";
                            break;
                        case 22:
                            mBench += "    ";
                            break;
                        case 23:
                            mBench += "   ";
                            break;
                        case 24:
                            mBench += "  ";
                            break;
                    }

                    if (!mBench.Contains("SERVICE"))
                    {
                        if (!mBench.Contains("COMPLETED"))
                        {
                            if (!mBench.StartsWith("PARTS ARE"))
                            {
                                if (!mBench.StartsWith("PARTS ORDERED ON"))
                                {
                                    if (!mBench.StartsWith("PARTS ARE BACK ORDERED"))
                                    {
                                        if (!mBench.StartsWith("WAITING ESTIMATE "))
                                        {
                                            if (mTheTech == TECHNICIAN)
                                            {
                                                loop++;
                                                var t = mBench;
                                                switch (t.Length)
                                                {
                                                    case 25:
                                                        richTextBox1.Text += mClaim_NO + "\t" + mBrand + "\t" + mModel + "\t" + mIsWarr + "\t" + mBench.Substring(0, 25) + "\n";
                                                        break;
                                                    case 26:
                                                        richTextBox1.Text += mClaim_NO + "\t" + mBrand + "\t" + mModel + "\t" + mIsWarr + "\t" + mBench.Substring(0, 26) + "\n";
                                                        break;
                                                    case 27:
                                                        richTextBox1.Text += mClaim_NO + "\t" + mBrand + "\t" + mModel + "\t" + mIsWarr + "\t" + mBench + "\n";
                                                        break;
                                                    case 28:
                                                        richTextBox1.Text += mClaim_NO + "\t" + mBrand + "\t" + mModel + "\t" + mIsWarr + "\t" + mBench.Substring(0, 28) + "\n";
                                                        break;
                                                    default:
                                                        richTextBox1.Text += mClaim_NO + "\t" + mBrand + "\t" + mModel + "\t" + mIsWarr + "\t" + mBench.Substring(0, 27) + "\n";
                                                        break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    loopCount++;
                }
                reader.Close();
                Version.DatabaseIsLocked = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception line 491\n" + ex);
            }
            label1.Text = "Found: " + loop.ToString();
        }

        private void richTextBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {   //click event
                ContextMenu contextMenu = new ContextMenu();
                MenuItem menuItem = new MenuItem("Cut       Ctrl+X");
                menuItem.Click += new EventHandler(CutAction);
                contextMenu.MenuItems.Add(menuItem);
                menuItem = new MenuItem("Copy    Ctrl+C");
                menuItem.Click += new EventHandler(CopyAction);
                contextMenu.MenuItems.Add(menuItem);
                menuItem = new MenuItem("Paste    Ctrl+V");
                menuItem.Click += new EventHandler(PasteAction);
                contextMenu.MenuItems.Add(menuItem);

                richTextBox1.ContextMenu = contextMenu;
            }
        }
        void CutAction(object sender, EventArgs e)
        {
            try
            {
                richTextBox1.Cut();
            }
            catch (Exception)
            {
                //
            }
        }

        void CopyAction(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(richTextBox1.SelectedText);
            }
            catch (Exception ex)
            {
                if (ex.ToString().Contains("Value cannot be null."))
                {
                    // Ignore nothing selected
                }
                else
                {
                    MessageBox.Show("Sorry an exception has occured.\n" + ex);
                }
            }

        }

        void PasteAction(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                richTextBox1.Text += Clipboard.GetText(TextDataFormat.Text).ToString();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Length > 0)
            {
                richTextBox1.SelectAll();
                var tScreen = richTextBox1.Text;
                DateTime date = DateTime.Now;
                var shortDate = date.ToString("MM-dd-yyyy");
                var tMessage = shortDate + "\t" + Version.PCNAME + " Claims by Technician " + "\n\n";
                var tHeader = "Claim Manufacturer              Model                     Warr Status  Disposition\n";
                richTextBox2.Text = "";
                richTextBox2.Text = tMessage + tHeader + tScreen;
                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.Print();
                }
            }
            else
            {
                if (richTextBox1.Text.Length <= 0)
                {
                    richTextBox1.Text = "\nClick buttons above to list / Print first.";
                    return;
                }
            }
        }

        private void printDocument1_BeginPrint(object sender, PrintEventArgs e)
        {
            printDocument1.DefaultPageSettings.Landscape = true;
            char[] param = { '\n' };

            if (printDialog1.PrinterSettings.PrintRange == PrintRange.Selection)
            {
                lines = richTextBox2.SelectedText.Split(param);
            }
            else
            {
                lines = richTextBox2.Text.Split(param);
            }

            int i = 0;
            char[] trimParam = { '\r' };
            foreach (string s in lines)
            {
                lines[i++] = s.TrimEnd(trimParam);
            }
        }

        private void OnPrintPage(object sender, PrintPageEventArgs e)
        {
            printDocument1.DefaultPageSettings.Landscape = true;
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            loop = 0;
            label1.Text = "Found: " + loop.ToString();
        }
    }
}
