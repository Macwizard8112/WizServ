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
using System.Windows.Forms;
using System.Media;
using Microsoft.Win32;

namespace WizServ
{
    public partial class ClaimStatusReport : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        private int loopCount, loop;
        public int RECEIVED, ASSIGNED, TRIAGED, PODS, ESTIMATE, PODSRECD, REPAIRED, TECHRENDER, COLERENDER, FINALRENDER, PARKED;
        private readonly string Database = @"I:\\Datafile\\Control\\Database.CSV";

        public ClaimStatusReport()
        {
            InitializeComponent();
            Icon = image100;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
            timer1.Interval = 60000; // Poll every 60 seconds
            timer1.Start();
            GetData();
            ShowData();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Version.From = "RECEIVED";
            Version.ClaimsPassThru = loop;
            Hide();
            ClaimsAssigned f2 = new ClaimsAssigned();
            f2.Show();
        }

        private void Restart()
        {
            timer1.Start();
            GetData();
            ShowData();
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {
            Version.From = "RECEIVED";
            Version.ClaimsPassThru = loop;
            Hide();
            ClaimsAssigned f2 = new ClaimsAssigned();
            f2.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ZeroOutBars();
            RECEIVED = 0;
            ASSIGNED = 0;
            TRIAGED = 0;
            PODS = 0;
            PODSRECD = 0;
            REPAIRED = 0;
            TECHRENDER = 0;
            COLERENDER = 0;
            FINALRENDER = 0;
            PARKED = 0;
            ESTIMATE = 0;
            loop = 0;
            loopCount = 0;
            GetData();
            ShowData();
        }

        private void ZeroOutBars()
        {
            progressBar1.Value = 0;
            progressBar2.Value = 0;
            progressBar3.Value = 0;
            progressBar4.Value = 0;
            progressBar5.Value = 0;
            progressBar6.Value = 0;
            progressBar7.Value = 0;
            progressBar8.Value = 0;
            progressBar9.Value = 0;
            progressBar10.Value = 0;
            progressBar11.Value = 0;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            Tech_AssignmentMenu f2 = new Tech_AssignmentMenu();
            f2.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Version.ClaimsPassThru = ASSIGNED;
            Version.From = "ASSIGNED";
            Hide();
            ClaimsAssigned f2 = new ClaimsAssigned();
            f2.Show();
        }

        private void progressBar2_Click(object sender, EventArgs e)
        {
            Version.ClaimsPassThru = ASSIGNED;
            Version.From = "ASSIGNED";
            Hide();
            ClaimsAssigned f2 = new ClaimsAssigned();
            f2.Show();
        }

        private void ShowData()
        {
            label2.Text = loop.ToString();
            label4.Text = ASSIGNED.ToString();
            label6.Text = TRIAGED.ToString();
            label8.Text = PODS.ToString();
            label10.Text = PODSRECD.ToString();
            label12.Text = REPAIRED.ToString();
            label14.Text = TECHRENDER.ToString();
            label16.Text = COLERENDER.ToString();
            label18.Text = FINALRENDER.ToString();
            label20.Text = PARKED.ToString();
            label24.Text = ESTIMATE.ToString();
            InitialProgressBarState();
            progressBar1.Value = loop;
            progressBar2.Value = ASSIGNED;
            progressBar3.Value = TRIAGED;
            progressBar4.Value = PODS;
            progressBar5.Value = PODSRECD;
            progressBar6.Value = REPAIRED;
            progressBar7.Value = TECHRENDER;
            progressBar8.Value = COLERENDER;
            progressBar9.Value = FINALRENDER;
            progressBar10.Value = PARKED;
            progressBar11.Value = ESTIMATE;
        }

        private void InitialProgressBarState()
        {
            progressBar1.Minimum = 0;
            progressBar2.Minimum = 0;
            progressBar3.Minimum = 0;
            progressBar4.Minimum = 0;
            progressBar5.Minimum = 0;
            progressBar6.Minimum = 0;
            progressBar7.Minimum = 0;
            progressBar8.Minimum = 0;
            progressBar9.Minimum = 0;
            progressBar10.Minimum = 0;
            progressBar11.Minimum = 0;
            progressBar1.Maximum = loop;
            progressBar2.Maximum = loop;
            progressBar3.Maximum = loop;
            progressBar4.Maximum = loop;
            progressBar5.Maximum = loop;
            progressBar6.Maximum = loop;
            progressBar7.Maximum = loop;
            progressBar8.Maximum = loop;
            progressBar9.Maximum = loop;
            progressBar10.Maximum = loop;
            progressBar11.Maximum = loop;
        }

        public void GetData()
        {
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

                loopCount = 0;
                loop = 0;

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

                    loop++; // Total # of Claims

                    if (mBench.Contains("ASSIGNED"))
                    {
                        ASSIGNED++;
                    }
                    if (mBench.Contains("TRIAGED") ^ mBench.Contains("BENCH ON BENCH"))
                    {
                        TRIAGED++;
                    }
                    if (mBench.Contains("CHECKING PART") ^ mBench.Contains("MISC. DESCRIPTION: RENDER"))
                    {
                        TRIAGED++;
                    }
                    if (mBench.Contains("WAITING ESTIMATE APPROVAL"))
                    {
                        ESTIMATE++;
                    }
                    if (mBench.Contains("PARTS ARE BACK ORDERED"))
                    {
                        TRIAGED++;
                    }
                    if (mBench.Contains("PARTS ORDERED BY PO") ^ mBench.Contains("THE PARTS ON PO# ") ^ mBench.Contains("PARTS ARE BACK ORDERED"))
                    {
                        PODS++;
                    }
                    if (mBench.Contains("PARTS RECEIVED"))
                    {
                        PODSRECD++;
                    }
                    if (mBench.Contains("SERVICE REND"))
                    {
                        COLERENDER++;
                        REPAIRED++;
                    }
                    if (mBench.Contains("DESCRIPTION: RENDER"))
                    {
                        TECHRENDER++;
                        REPAIRED++;
                    }
                    if (mBench.Contains("MISC. DESCRIPTION: PARK"))
                    {
                        PARKED++;
                    }
                    if (listBC[loopCount] != "00/00/0000")
                    {
                        FINALRENDER++;
                        REPAIRED++;
                    }
                    loopCount++;
                }
                reader.Close(); // Close the open file
            }
            catch (Exception ex)
            {
                if (ex.ToString().Contains("The process cannot access the file 'I:\\Datafile\\Control\\Database.CSV' because it is being used by another process."))
                {
                    Thread.Sleep(2000);
                    Restart();
                }
                MessageBox.Show("Error 372: Sorry an error has occured: " + ex.Message);
            }
        }
    }
}
