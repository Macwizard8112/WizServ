using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using Microsoft.Win32;
using WizServ.Properties;
using WizServ.Resources;

namespace WizServ
{
    public partial class FinalRender : Form
    {
        public string mCLaimNumber;
        public Icon image100 = Properties.Resources.WizServ;
        private static readonly string Database = @"I:\\Datafile\\Control\\Database.CSV";
        private static readonly string Notes = @"I:\Datafile\Control\Notes\Claim_Notes.csv";
        public string ClaimNotes = "ClaimNotes.rtf";
        private static readonly string PartsUsed1 = @"I:\\Datafile\\Control\\Partsused.CSV";  // This is Read only CSV
        static readonly string key = @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\lanmanserver\parameters";
        public readonly string computerDescription = (string)Registry.GetValue(key, "srvcomment", null);
        private string claim_no;
        private int loopCount, loop;
        public bool Found = false, ServiceBulletin, ServiceManual;
        public string Mex, TheFileIs, TheFileNameIs, yeardigit;
        private SoundPlayer Player = new SoundPlayer();
        public string FROM = Version.From, kkk;
        public string mModel, mBrand, mFname, mSerial, mLname, mTS1, mTS2, mTS3, mTS4;
        public string yModel, yBrand, yFname, ySerial, yLname, yTS1, yTS2, yTS3, yTS4;
        public string SELECTEDTEXT = Version.SELECTEDTEXT, SAVEDDATA;
        public decimal d4, d5, kkkShip;
        public string ptu1, ptu2, ptu3, ptu4, ptu5, ptu6, ptu7, ptu8, ptu9, ptu10;
        public string ppn1, ppn2, ppn3, ppn4, ppn5, ppn6, ppn7, ppn8, ppn9, ppn10;
        public string ppd1, ppd2, ppd3, ppd4, ppd5, ppd6, ppd7, ppd8, ppd9, ppd10;
        public int len1, len2, len3, len4, len5, len6, len7, len8, len9, len10;
        public int pploop;
        public string[] Claims1;

        public FinalRender()
        {
            InitializeComponent();
            Icon = image100;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EditTheData();
        }

        private void EditTheData()
        {
            yTS1 = textBox2.Text;
            yTS2 = textBox3.Text;
            yTS3 = textBox4.Text;
            yTS4 = textBox5.Text;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;   // Only accept numbers, no letters allowed
            }
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)       // If Escpae is pressed return to Main Menu
            {
                Hide();
                MainMenu f2 = new MainMenu();
                f2.Show();
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                var temp = textBox1.Text;
                if (temp.Length != 6)       // Check if claim # is valid
                {
                    MessageBox.Show("Claim # must be 6 characters long.");
                    textBox1.Text = "";
                    textBox1.Focus();
                    textBox1.Select();
                }
                if (temp.Length == 6)       // Claim # is valid
                {
                    mCLaimNumber = textBox1.Text;
                    Version.Claim = textBox1.Text;
                    label1.Visible = false;         // hide text & box after entering Claim info
                    textBox1.Visible = false;
                    claim_no = mCLaimNumber;
                    GetData();                      // Find the Claim information
                }
            }
        }

        public async void GetData()
        {
            try
            {
                ServiceBulletin = false;
                if (Version.DatabaseIsLocked == true)
                {
                    MessageBox.Show("Database in use, please wait a few seconds.");
                }
            }
            catch (Exception)
            {
                Thread.Sleep(4000);
            }
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
                    listBP.Add(values[67]);     //  Primary Email           Cust Primary Email Address
                    listBQ.Add(values[68]);     //  Claim_Num               Short Claim # A2400403
                    listBR.Add(values[69]);     //  Company                 Company Name or N/A
                    listBS.Add(values[70]);     //  Real_Claim              Unused (Old new claim #)
                    listBT.Add(values[71]);     //  Secondary Email         Secondary Email Address
                    listBU.Add(values[72]);     //  EST_YN                  Estimate Yes / No
                    listBV.Add(values[73]);     //  EST_TOTAL               Estimate Total $
                    listBW.Add(values[74]);     //  EST_PARTS               Estimate Parts $
                    listBX.Add(values[75]);     //  Rush                    Rush Y or N
                    listBY.Add(values[76]);     //  Used                    Used Y/N
                    listBZ.Add(values[77]);     //  Est_Deposit             Estimate Deposit Amount $
                    listCA.Add(values[78]);     //  Closed                  Service Rendered Claim
                    listCB.Add(values[79]);     //  Picked up               Claim P/U by customer

                    var mWarr = listA[loopCount];
                    var mClaim_NO = listB[loopCount];
                    var mDate_IN = listC[loopCount];
                    mFname = listD[loopCount];
                    mLname = listE[loopCount];
                    var mAddr = listF[loopCount];
                    var mCity = listG[loopCount];
                    var mState = listH[loopCount];
                    var mZip = listI[loopCount];
                    var mHphone = listJ[loopCount];
                    var mWPhone = listK[loopCount];
                    var mProblem = listL[loopCount];
                    mBrand = listM[loopCount];
                    var mServNo = listN[loopCount];
                    mModel = listO[loopCount];
                    mSerial = listP[loopCount];
                    var mWarranty = listBN[loopCount] + " " + listBL[loopCount];
                    var mFthr_exp1 = listAE[loopCount];
                    var mFthr_exp2 = listAF[loopCount];
                    var t = listAU[loopCount];
                    mTS1 = listAU[loopCount];
                    mTS2 = listAV[loopCount];
                    mTS3 = listAW[loopCount];
                    mTS4 = listAX[loopCount];
                    var mTechNum = listBA[loopCount];
                    var mTech = listBC[loopCount];
                    var mBench = listBD[loopCount];
                    var mTheTech = listAZ[loopCount];
                    var COMPLETED = listBC[loopCount];
                    var mTheNewClaimNum = listBR[loopCount];
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

                    if (mClaim_NO == claim_no)
                    {
                        Found = true;
                        Version.Serial = mSerial;
                        textBox2.Text = mTS1;
                        textBox3.Text = mTS2;
                        textBox4.Text = mTS3;
                        var p = "THANK YOU FOR CHOOSING WIZARD ELECTRONICS!";
                        var x = p.Length;
                        var g = mTS4.Length;
                        if (g <= 2)
                        {
                            textBox5.Text = p;
                        }
                        else
                        {
                            textBox5.Text = mTS4;   // p
                        }
                        label11.Text = mFname;
                        label12.Text = mLname;
                        label13.Text = mBrand;
                        label14.Text = mModel;
                        label15.Text = mSerial;
                        label16.Text = mDate_IN;

                        {                       // Store saved data for later updating Database file
                            yModel = mModel;
                            yBrand = mBrand;
                            yFname = mFname;
                            yLname = mLname;
                            ySerial = mSerial;
                            yTS1 = mTS1;
                            yTS2 = mTS2;
                            yTS3 = mTS3;
                            yTS4 = mTS4;
                        }
                        loop++;
                    }
                    loopCount++;
                    
                }
                if (Found == true)
                {
                    label10.Text = "Found !";
                }
                else
                {
                    label10.Text = "Not found !";
                }
                //reader.Close(); // Close the open file
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
                if (Mex.Contains("StackOverflowException"))
                {
                    MessageBox.Show("StackOverflowException");
                }
                if (Mex.Contains("Input string was not in a correct format."))
                {

                }
                else
                {
                    MessageBox.Show("Error 410: Sorry an error has occured: " + ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)  // Return to Main Menu
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }

    }
}
