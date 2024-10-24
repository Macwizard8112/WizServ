using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WizServ
{
    public partial class Reassign : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        public static readonly string Database = @"I:\\Datafile\\Control\\Database.CSV";
        private int loopCount;
        public string SelectedText, claim_no;
        public static string mWarr, mClaim_NO, mDate_IN, mFname, mLname, mAddr, mCity, mState, mZip, mHphone, mWPhone;
        public string mProblem, mBrand, mServNo, mModel, mSerial, mq, mr, ms, mt, mu, mv, mw, mx, my, mz;
        public static string maa, mab, mac, mad, mFthr_exp1, mFthr_exp2, mag, mah, mai, maj, mak, mal, mam, man, mao;
        public static string map, maq, mar, mas, mWar_Note, mau, mav, maw, max, may, maz, mT;
        public static string mba, mbb, mbc, mbd, mbe, mbf, mbg, mbh, mbi, mbj, mbk, mWarranty, mbm, mbn, mbo, mbp, mTech;
        public bool Start = false;
        public string xFname, xLname, xMFG, xModel, xTechID, TechNumber, xClaim_no;
        public string xTech, TechName;
        public string TodayString;

        public Reassign()
        {
            InitializeComponent();
            label12.Visible = false;
            Icon = image100;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
            SetComboBox();
            Start = false;
            GetData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Hide();
            MainMenu f0 = new MainMenu();
            f0.Show();
        }

        private void SetComboBox()
        {
            comboBox1.Items.Add("TECH1");
            comboBox1.Items.Add("TECH2");
            comboBox1.Items.Add("TECH3");
            comboBox1.Items.Add("TECH4");
            comboBox1.Items.Add("TECH5");
            comboBox1.Items.Add("PARTS");
            comboBox1.Items.Add("CONSIGNMENT");
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            SelectedText = listBox1.SelectedItem.ToString();
            SelectedText = SelectedText.Substring(0, 7).Trim();
            label1.Text = "Claim: " + SelectedText;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            listBox1.Visible = false;
            Start = true;
            GetData();
            label7.Text = "First Name: " + xFname;
            label8.Text = "Last Name: " + xLname;
            label9.Text = "Manufacturer: " + xMFG;
            label10.Text = "Model: " + xModel;
            label11.Text = "Previous Tech: " + xTech;
            Start = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpDateDB();
        }

        private void SetTechID()
        {
            switch(comboBox1.Text)
            {
                case "TECH1":
                    xTechID = "CH";
                    TechNumber = "1";
                    TechName = "COLE";
                    break;
                case "TECH2":
                    xTechID = "WK";
                    TechNumber = "2";
                    TechName = "WALTER";
                    break;
                case "TECH3":
                    xTechID = "DN";
                    TechNumber = "3";
                    TechName = "DEREK";
                    break;
                case "TECH4":
                    xTechID = "AN";
                    TechNumber = "4";
                    TechName = "ANTONIA";
                    break;
                case "TECH5":
                    xTechID = "AA";
                    TechName = "ANGELO";
                    TechNumber = "5";
                    break;
                case "PARTS":
                    xTechID = "PA";
                    TechName = "PARTS";
                    TechNumber = "8";
                    break;
                case "CONSIGNMENT":
                    xTechID = "CS";
                    TechName = "CONSIGNMENT";
                    TechNumber = "9";
                    break;
            }
        }

        private void UpDateDB()
        {
            SetTechID();
            TodayString = DateTime.Today.ToLongDateString();

            List<String> lines = new List<String>();

            if (File.Exists(Database))
            {
                try
                {
                    using (StreamReader reader = new StreamReader(Database))
                    {
                        String line;

                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line.Contains(","))
                            {
                                String[] split = line.Split(',');

                                if (split[1].Contains(SelectedText))
                                {
                                    split[0] = "REDRUM";
                                    split[1] = xClaim_no;    // Claim #
                                    split[50] = xTechID;
                                    split[51] = TechName;
                                    split[52] = TechNumber;
                                    //split[55] = "BENCH ON BENCH - " + TodayString;
                                    split[62] = TechName;
                                    line = String.Join(",", split);
                                }
                            }

                            lines.Add(line);
                        }
                        reader.Close();
                    }

                    using (StreamWriter writer = new StreamWriter(Database, false))
                    {
                        foreach (String line in lines)
                            writer.WriteLine(line);
                    }
                    label12.Visible = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error 177: Please try again\nDatbase is in use.");
                    label12.Visible = false;
                }
            }
        }

        public void GetData()
        {
            listBox1.Items.Clear();
            loopCount = 0;
            mTech = "";
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
                    listW.Add(values[22]);      //  Postcard        Taxable
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
                    listBY.Add(values[76]);     //  Used
                    listBZ.Add(values[77]);     //  Estimate Deposit
                    listCA.Add(values[78]);     //  Closed
                    listCB.Add(values[79]);     //  Picked up

                    mWarr = listA[loopCount];
                    mClaim_NO = listB[loopCount];
                    mDate_IN = listC[loopCount];
                    mFname = listD[loopCount];
                    mLname = listE[loopCount];
                    mAddr = listF[loopCount];
                    mCity = listG[loopCount];
                    mState = listH[loopCount];
                    mZip = listI[loopCount];
                    mHphone = listJ[loopCount];
                    mWPhone = listK[loopCount];
                    mProblem = listL[loopCount];
                    mBrand = listM[loopCount];
                    mServNo = listN[loopCount];
                    mModel = listO[loopCount];
                    mSerial = listP[loopCount];
                    mq = listQ[loopCount];
                    mr = listR[loopCount];
                    ms = listS[loopCount];
                    mt = listT[loopCount];
                    mu = listU[loopCount];
                    mv = listV[loopCount];
                    mw = listW[loopCount];
                    mx = listX[loopCount];
                    my = listY[loopCount];
                    mz = listZ[loopCount];
                    maa = listAA[loopCount];
                    mab = listAB[loopCount];
                    mac = listAC[loopCount];
                    mad = listAD[loopCount];
                    mFthr_exp1 = listAE[loopCount];
                    mFthr_exp2 = listAF[loopCount];
                    mag = listAG[loopCount];
                    mah = listAH[loopCount];
                    mai = listAI[loopCount];
                    maj = listAJ[loopCount];
                    mak = listAK[loopCount];
                    mal = listAL[loopCount];
                    mam = listAM[loopCount];
                    man = listAN[loopCount];
                    mao = listAO[loopCount];
                    map = listAP[loopCount];
                    maq = listAQ[loopCount];
                    mar = listAR[loopCount];
                    mas = listAS[loopCount];
                    mWar_Note = listAT[loopCount];
                    mT = listAZ[loopCount];         // Tech Name
                    mbi = listBI[loopCount];
                    mbe = listBE[loopCount];


                    var mTS1 = listAU[loopCount];
                    var mTS2 = listAV[loopCount];
                    var mTS3 = listAW[loopCount];
                    var mts4 = listAX[loopCount];
                    var mTechNum = listBA[loopCount];
                    var mTech = listBC[loopCount];
                    var mStatus = listBD[loopCount];
                    var mProduct = listBJ[loopCount];
                    mWarranty = listBL[loopCount];
                    var mTheNewClaimNum = listBQ[loopCount];
                    var mIsWarr = listBL[loopCount];
                    var mEmail = listBP[loopCount];

                    if (listBT[loopCount] != "NONE")
                    {
                        mEmail += ", " + listBT[loopCount];
                    }
                    var mEstimate = listBU[loopCount];
                    var mRush = listBX[loopCount];

                    SetBrand();
                    SetModel();

                    if (Start == false)
                    {
                        listBox1.Items.Add(mClaim_NO + "\t" + mBrand + "\t" + mModel + "\t" + mFname + " " + mLname);
                    }
                    if (Start == true)
                    {
                        if (SelectedText == mClaim_NO)
                        {
                            xClaim_no = mClaim_NO;
                            xFname = mFname;
                            xLname = mLname;
                            xMFG = mBrand;
                            xModel = mModel;
                            xTech = mT;

                        }
                    }
                    loopCount++;
                }
                reader.Close(); // Close the open file
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 311: Please try again\nDatabase in use");
            }
        }

        private void SetBrand()
        {
            switch (mModel.Length)
            {
                case 2:
                    mModel += "                          ";
                    break;
                case 3:
                    mModel += "                         ";
                    break;
                case 4:
                    mModel += "                        ";
                    break;
                case 5:
                    mModel += "                       ";
                    break;
                case 6:
                    mModel += "                      ";
                    break;
                case 7:
                    mModel += "                     ";
                    break;
                case 8:
                    mModel += "                    ";
                    break;
                case 9:
                    mModel += "                   ";
                    break;
                case 10:
                    mModel += "                  ";
                    break;
                case 11:
                    mModel += "                 ";
                    break;
                case 12:
                    mModel += "                ";
                    break;
                case 13:
                    mModel += "               ";
                    break;
                case 14:
                    mModel += "              ";
                    break;
                case 15:
                    mModel += "             ";
                    break;
                case 16:
                    mModel += "            ";
                    break;
                case 17:
                    mModel += "           ";
                    break;
                case 18:
                    mModel += "          ";
                    break;
                case 19:
                    mModel += "         ";
                    break;
                case 20:
                    mModel += "        ";
                    break;
                case 21:
                    mModel += "       ";
                    break;
                case 22:
                    mModel += "      ";
                    break;
                case 23:
                    mModel += "     ";
                    break;
                case 24:
                    mModel += "    ";
                    break;
                case 25:
                    mModel += "   ";
                    break;
                case 26:
                    mModel += "  ";
                    break;
                case 27:
                    mModel += " ";
                    break;
                case 28:
                    mModel += "";
                    break;
            }
        }

        private void SetModel()
        {
            switch (mBrand.Length)
            {
                case 2:
                    mBrand += "                  ";
                    break;
                case 3:
                    mBrand += "                 ";
                    break;
                case 4:
                    mBrand += "                ";
                    break;
                case 5:
                    mBrand += "               ";
                    break;
                case 6:
                    mBrand += "              ";
                    break;
                case 7:
                    mBrand += "             ";
                    break;
                case 8:
                    mBrand += "            ";
                    break;
                case 9:
                    mBrand += "           ";
                    break;
                case 10:
                    mBrand += "          ";
                    break;
                case 11:
                    mBrand += "         ";
                    break;
                case 12:
                    mBrand += "        ";
                    break;
                case 13:
                    mBrand += "       ";
                    break;
                case 14:
                    mBrand += "      ";
                    break;
                case 15:
                    mBrand += "     ";
                    break;
                case 16:
                    mBrand += "    ";
                    break;
                case 17:
                    mBrand += "   ";
                    break;
                case 18:
                    mBrand += "  ";
                    break;
                case 19:
                    mBrand += " ";
                    break;
                case 20:
                    mBrand += "";
                    break;
            }
        }
    }
}
