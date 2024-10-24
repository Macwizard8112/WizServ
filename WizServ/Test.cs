using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Windows.Forms;

namespace WizServ
{
    public partial class Test : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        private readonly string file = @"I:\\Datafile\\Control\\Database.CSV";
        //private readonly string Related = @"I:\\Datafile\\Control\\Related.CSV";
        static readonly string key = @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\lanmanserver\parameters";
        public readonly string computerDescription = (string)Registry.GetValue(key, "srvcomment", null);
        private string claim_no;
        private int loopCount, loop;
        private string IsClosed = "";
        public bool Found = false;
        public string Mex, TheFileIs, TheFileNameIs, yeardigit;
        private SoundPlayer Player = new SoundPlayer();
        private StreamReader streamToPrint;
        private Font printFont;
        public string FROM = Version.From;

        private void pageSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void retrieveMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void mainMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void mainMenuToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void f2PartsListToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void f5NotesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void getTechSupportToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        public string SELECTEDTEXT = Version.SELECTEDTEXT, SAVEDDATA;
        public decimal d4, d5;

        public Test()
        {
            InitializeComponent();
            Icon = image100;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
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
                    var Est_Total = listBV[loopCount];
                    var Est_Parts = listBW[loopCount];

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
                        if (mClaim_NO == claim_no || mClaim_NO == (yeardigit + mClaim_NO))
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
                            if (listBU[loopCount] != "B")
                            {
                                try
                                {
                                    d4 = decimal.Parse(Est_Total);
                                    d5 = decimal.Parse(Est_Parts);
                                }
                                catch (Exception)
                                {
                                    MessageBox.Show("Database is damaged, notify Doc or Cole.\nThere is a comma in database.");
                                }
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
                                        label54.Text = "Estimate: " + d4.ToString("C2") + " Parts: " + d5.ToString("C2") + " Pending";    // Convert text to decimal w/ $
                                    }
                                    if (mEstimate == "A")
                                    {
                                        label54.Text = "Estimate: " + d4.ToString("C2") + " Parts: " + d5.ToString("C2") + " Approved";    // Convert text to decimal w/ $
                                    }
                                    if (mEstimate == "N")
                                    {
                                        label54.Text = "Estimate: Not Requested";    // Convert text to decimal w/ $
                                    }
                                    if (mEstimate == "_")
                                    {
                                        label54.Text = "Estimate: " + d4.ToString("C2") + " Parts: " + d5.ToString("C2") + " DECLINED";    // Convert text to decimal w/ $
                                    }

                                }
                            }
                            else
                            {
                                label54.Text = "";
                            }
                            if (mBrand.StartsWith("JBL"))
                            {
                                button5.Visible = true;
                            }
                            if (mBrand.StartsWith("KRK"))
                            {
                                button5.Visible = true;
                            }
                            if (mBrand.StartsWith("CROWN"))
                            {
                                button5.Visible = true;
                            }
                            if (mBrand.StartsWith("DBX"))
                            {
                                button5.Visible = true;
                            }
                            if (mBrand.StartsWith("BIAMP"))
                            {
                                button5.Visible = true;
                            }
                            if (mBrand.StartsWith("LEXICON"))
                            {
                                button5.Visible = true;
                            }
                            if (mBrand.StartsWith("SOUNDCRAFT"))
                            {
                                button5.Visible = true;
                            }
                            if (mBrand.StartsWith("MARTIN"))
                            {
                                button5.Visible = true;
                            }
                            if (mBrand.StartsWith("AKG"))
                            {
                                button5.Visible = true;
                            }
                            if (mBrand.StartsWith("DIGITECH"))
                            {
                                button5.Visible = true;
                            }
                            if (mBrand.StartsWith("AMX"))
                            {
                                button5.Visible = true;
                            }
                            if (mBrand.StartsWith("BSS"))
                            {
                                button5.Visible = true;
                            }
                            if (mBrand.StartsWith("YAMAHA"))
                            {
                                button5.Visible = true;
                            }
                            if (mBrand.StartsWith("MACKIE"))
                            {
                                button5.Visible = true;
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
                            label48.Text = mEmail;
                            if (mEmail == ".")
                            {
                                label43.Text = listBP[loopCount];
                            }
                            label33.Text = mTS1;
                            label34.Text = mTS2;
                            label35.Text = mTS3;
                            label36.Text = mts4;
                            if (mWarranty.Contains("WAR"))
                            {
                                label38.BackColor = Color.White;
                            }
                            if (mWarranty.Contains("NON"))
                            {
                                label38.ForeColor = Color.Green;
                            }
                            else
                            {
                                label38.ForeColor = Color.Red;
                            }
                            label38.Text = mWarranty;
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

                            //label48.Text = mTheNewClaimNum.ToString();
                            if (mBench.Contains("SERVICE RENDERED"))
                            {
                                label47.ForeColor = Color.Red;
                                label47.Text = "CLOSED";
                                Text += "  CLOSED CLAIM !";
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
                                label56.Text = " Yes ";
                                label56.BackColor = Color.Red;
                                label56.ForeColor = Color.White;
                            }
                            else
                            {
                                label45.Text = "Estimate:";  // No
                                label56.Text = " No ";
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
                            if (label36.Text.Contains("PT#") || label35.Text.Contains("PT#"))
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
                            label43.Text = mEmail;
                            if (mEmail == ".")
                            {
                                label43.Text = listBP[loopCount];
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
                            richTextBox1.Text = richTextBox1.Text + "************************************\t Date In: " + mDate_IN + "    CLAIM # " + mClaim_NO + "\n";
                            richTextBox1.Text = richTextBox1.Text + "* Wizard Electronics, Inc.         *\t Prodcut: " + listBJ[loopCount] + "\n";
                            richTextBox1.Text = richTextBox1.Text + "* 554 Deering Road Northwest       *\t Brand:   " + mBrand + "\n";
                            richTextBox1.Text = richTextBox1.Text + "* Atlanta, GA 30309                *\t Model:   " + mModel + "\n";
                            richTextBox1.Text = richTextBox1.Text + "* (404)325-4891 Fax (404)325-4175  *\t Serial#: " + mSerial + "\n";
                            richTextBox1.Text = richTextBox1.Text + "************************************\t Shelf Location: " + listBE[loopCount] + " Rush Claim: " + mxRush + "\n";
                            richTextBox1.Text += "\n";
                            richTextBox1.Text = richTextBox1.Text + "Customer Name:    " + mFname + ", " + mLname + "\t\t" + listAH[loopCount] + "\n";
                            richTextBox1.Text = richTextBox1.Text + "Customer Address: " + mAddr + "\n";
                            richTextBox1.Text = richTextBox1.Text + "City, State, Zip: " + mCity + ", " + mState + " " + mZip + " " + "Home : " + mHphone + " Work: " + mWPhone + "\n";
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
                            richTextBox1.Text += "Email:   " + mEmail + "\n";
                            richTextBox1.Text += "*****************************************************************************\n";
                            richTextBox1.Text += "Technical Services Rendered:\n";
                            richTextBox1.Text += listAU[loopCount] + "\n";
                            richTextBox1.Text += listAV[loopCount] + "\n";
                            richTextBox1.Text += listAW[loopCount] + "\n";
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
                        if (mBrand.StartsWith("JBL"))
                        {
                            button5.Visible = true;
                        }
                        if (mBrand.StartsWith("CROWN"))
                        {
                            button5.Visible = true;
                        }
                        if (mBrand.StartsWith("DBX"))
                        {
                            button5.Visible = true;
                        }
                        if (mBrand.StartsWith("LEXICON"))
                        {
                            button5.Visible = true;
                        }
                        if (mBrand.StartsWith("SOUNDCRAFT"))
                        {
                            button5.Visible = true;
                        }
                        if (mBrand.StartsWith("MARTIN"))
                        {
                            button5.Visible = true;
                        }
                        if (mBrand.StartsWith("AKG"))
                        {
                            button5.Visible = true;
                        }
                        if (mBrand.StartsWith("DIGITECH"))
                        {
                            button5.Visible = true;
                        }
                        if (mBrand.StartsWith("AMX"))
                        {
                            button5.Visible = true;
                        }
                        if (mBrand.StartsWith("BSS"))
                        {
                            button5.Visible = true;
                        }
                        if (mBrand.StartsWith("YAMAHA"))
                        {
                            button5.Visible = true;
                        }
                        if (mBrand.StartsWith("MACKIE"))
                        {
                            button5.Visible = true;
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
                        label30.Text = "Email: ";
                        label43.Text = "&" + mFthr_exp2;
                        if (mEmail == ".")
                        {
                            label43.Text = listBP[loopCount];
                        }
                        label33.Text = mTS1;
                        label34.Text = mTS2;
                        label35.Text = mTS3;
                        label36.Text = mts4;
                        label38.Text = mWarranty;
                        if (mWarranty.Contains("RECALL") || mIsWarr == "WARRANTY")
                        {
                            label49.Text = " RECALL ";
                        }
                        if (mIsWarr.Contains("RECALL"))
                        {
                            label49.Text += "PARTS ONLY No Labor";
                        }
                        label39.Text = mBench;
                        //label48.Text = mTheNewClaimNum.ToString();
                        if (mBench.Contains("SERVICE RENDERED"))
                        {
                            label47.ForeColor = Color.Red;
                            label47.Text = "CLOSED";
                            Text += "  CLOSED CLAIM !";
                            label49.Text = "CLOSED CLAIM";
                        }
                        else
                        {
                            label47.ForeColor = Color.Green;
                            label47.Text = "Open Claim";
                        }
                        //label45.Text = "Estimate: " + listBN[loopCount];
                        label40.Text = "Technician: " + mTheTech;
                        if (listBE[loopCount] == "FC")
                        {
                            listBE[loopCount] = "Front Counter";
                        }
                        label41.Text = listBE[loopCount];
                        if (label36.Text.Contains("PT#"))
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
                        richTextBox1.Text += "************************************\tDate In: " + mDate_IN + "    CLAIM # " + yeardigit + mClaim_NO + "\n";
                        richTextBox1.Text += "* Wizard Electronics, Inc.         *\tProdcut: " + listBJ[loopCount] + "\n";
                        richTextBox1.Text += "* 554 Deering Road Northwest       *\tBrand:   " + mBrand + "\n";
                        richTextBox1.Text += "* Atlanta, GA 30309                *\tModel:   " + mModel + "\n";
                        richTextBox1.Text += "* (404)325-4891 Fax (404)325-4175  *\tSerial#: " + mSerial + "\n";
                        richTextBox1.Text += "************************************\tShelf Location: " + listBE[loopCount] + "\n";
                        richTextBox1.Text += "\n";
                        richTextBox1.Text += "Customer Name:    " + mFname + ", " + mLname + "\t\t" + listAH[loopCount] + "\n";
                        richTextBox1.Text += "Customer Address: " + mAddr + "\n";
                        richTextBox1.Text += "City, State, Zip: " + mCity + ", " + mState + " " + mZip + " " + "Home : " + mHphone + " Work: " + mWPhone + "\n";
                        richTextBox1.Text += "*****************************************************************************\n";
                        richTextBox1.Text += "Client/Dealer name: " + listAI[loopCount] + "\tPhone: " + listAN[loopCount] + "\n";
                        richTextBox1.Text += "Address:            " + listAJ[loopCount] + "\t\tInvoice/Claim # " + listBF[loopCount] + "\n";
                        richTextBox1.Text += "City, State, Zip:   " + listAK[loopCount] + " " + listAL[loopCount] + "  " + listAM[loopCount] + "\n";
                        richTextBox1.Text += "*****************************************************************************\n";
                        richTextBox1.Text += "Unit Status is: " + listBL[loopCount] + "\n";
                        Version.Warranty = mWarranty;
                        if (listQ[loopCount].Length <= 6)
                        {
                            richTextBox1.Text += "\tTechnical Services: $  " + listQ[loopCount] + "\n";
                        }
                        else
                        {
                            richTextBox1.Text += "\tTechnical Services: $ " + listQ[loopCount] + "\n";
                        }
                        richTextBox1.Text += "\tTechnical Services: $ " + listQ[loopCount] + "\n";
                        richTextBox1.Text += "\t             Parts: $ " + listS[loopCount] + "\n";
                        richTextBox1.Text += "\t------------------------------------" + "\n";
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
                        richTextBox1.Text += "No warranty repairs W/O Sales Receipt/RA# at drop off. If NOT warranty,";
                        richTextBox1.Text += "EST Diagnostic Fee will apply if repair declined. Items left over 10 days,";
                        richTextBox1.Text += "add $ 1.00/Day storage fee.\n";
                        richTextBox1.Text += "*****************************************************************************\n";
                        richTextBox1.Text += "Problem: " + mProblem + "\n"; ;
                        richTextBox1.Text += "Problem: " + mFthr_exp1 + "\n";
                        richTextBox1.Text += "Problem: " + mFthr_exp2 + "\n";
                        richTextBox1.Text += "Email:   " + mEmail + "\n";
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

        // NegInvRpt            Print landscape
        // CreateEstimates      Sort CSV file
        // CreateEstimates      Copy CSV file
        // CreateEstimates      Delete CSV file
        // CreateEstimates      Rename CSV file
        // EditExistingPO       Show / Hide all labels Line 698
        // EstimteApprovalRPT   Print contents of RichTextBox
        //
        //
        //
        //
        //

    }
}
