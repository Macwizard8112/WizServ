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
using System.Windows.Forms;

namespace WizServ
{
    public partial class ByClaimNumPg2 : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        private readonly string file = @"I:\\Datafile\\Control\\Database.CSV";
        private readonly string claim_no;
        private int loopCount;
        public string TheFileIs;

        public ByClaimNumPg2()
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(0, 132, 129);
            panel4.BackColor = Color.FromArgb(0, 132, 129);
            panel5.BackColor = Color.FromArgb(0, 132, 129);
            SetBackColor();
            Icon = image100;
            timer1.Interval = 1000;
            timer1.Enabled = true;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
            claim_no = Version.Claim;
            Text = "Retrieve Claim by Claim Number - Page 2";
            GetData();
            CheckFileOpenStatus();
        }

        private void SetBackColor()
        {
            label15.BackColor = Color.FromArgb(0, 132, 129);
            label16.BackColor = Color.FromArgb(0, 132, 129);
            label17.BackColor = Color.FromArgb(0, 132, 129);
            label18.BackColor = Color.FromArgb(0, 132, 129);
            label19.BackColor = Color.FromArgb(0, 132, 129);
            label20.BackColor = Color.FromArgb(0, 132, 129);
            label21.BackColor = Color.FromArgb(0, 132, 129);
            label22.BackColor = Color.FromArgb(0, 132, 129);
            label23.BackColor = Color.FromArgb(0, 132, 129);
            label24.BackColor = Color.FromArgb(0, 132, 129);
            label25.BackColor = Color.FromArgb(0, 132, 129);
            label26.BackColor = Color.FromArgb(0, 132, 129);
            label27.BackColor = Color.FromArgb(0, 132, 129);
            label28.BackColor = Color.FromArgb(0, 132, 129);
            label29.BackColor = Color.FromArgb(0, 132, 129);
            label30.BackColor = Color.FromArgb(0, 132, 129);
            label31.BackColor = Color.FromArgb(0, 132, 129);
            label33.BackColor = Color.FromArgb(0, 132, 129);
            label34.BackColor = Color.FromArgb(0, 132, 129);
            label35.BackColor = Color.FromArgb(0, 132, 129);
            label36.BackColor = Color.FromArgb(0, 132, 129);
            label37.BackColor = Color.FromArgb(0, 132, 129);
        }

            private void Button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Hide();
                RetrieveMenu f2 = new RetrieveMenu();
                f2.Show();
            }
            if (e.KeyCode == Keys.F2)
            {
                Hide();
                Version.From = "Retrieve2";
                PartsUsed f2 = new PartsUsed();
                f2.Show();
            }
        }

        private void backToPage1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            ByClaimNum f2 = new ByClaimNum();
            f2.Show();
        }

        private void retrieveMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            RetrieveMenu f2 = new RetrieveMenu();
            f2.Show();
        }

        private void mainMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }


        private void Timer1_Tick(object sender, EventArgs e)
        {
            //var d = DateTime.Now.ToShortDateString();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Hide();
            RetrieveMenu f2 = new RetrieveMenu();
            f2.Show();
        }
        private void ByCalimNumPg2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Hide();
            RetrieveMenu f2 = new RetrieveMenu();
            f2.Show();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (Version.From == "EditServices")
            {
                Hide();
                EditServices f2 = new EditServices();
                f2.Show();
            }
            else
            {
                Hide();
                ByClaimNum f2 = new ByClaimNum();
                f2.Show();
            }
        }

        private void ByCalimNumPg2_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }
        public void CheckFileOpenStatus()
        {
            String path = @"I:\\Datafile\\Control\\FileLocking.csv";
            List<String> lines = new List<String>();

            if (File.Exists(path))
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    String line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Contains(","))
                        {
                            String[] split = line.Split(',');

                            if (split[0].Contains("Brand_DNR"))
                            {
                                split[1] = "Open";
                                line = String.Join(",", split);
                                TheFileIs = "Open";
                            }
                            if (split[0].Contains("Database"))
                            {
                                split[1] = "Open";
                                line = String.Join(",", split);
                                TheFileIs = "Open";
                            }
                            if (split[0].Contains("Dealers"))
                            {
                                split[1] = "Open";
                                line = String.Join(",", split);
                                TheFileIs = "Open";
                            }
                            if (split[0].Contains("Product"))
                            {
                                split[1] = "Open";
                                line = String.Join(",", split);
                                TheFileIs = "Open";
                            }
                            if (split[0].Contains("NextClaim"))
                            {
                                split[1] = "Open";
                                line = String.Join(",", split);
                                TheFileIs = "Open";
                            }
                            if (split[0].Contains("Related"))
                            {
                                split[1] = "Open";
                                line = String.Join(",", split);
                                TheFileIs = "Open";
                            }
                            if (split[0].Contains("Dealers_Number"))
                            {
                                split[1] = "Open";
                                line = String.Join(",", split);
                                TheFileIs = "Open";
                            }
                            if (split[0].Contains("Estimates"))
                            {
                                split[1] = "Open";
                                line = String.Join(",", split);
                                TheFileIs = "Open";
                            }
                            if (split[0].Contains("Gold"))
                            {
                                split[1] = "Open";
                                line = String.Join(",", split);
                                TheFileIs = "Open";
                            }
                        }

                        lines.Add(line);
                    }
                }

                using (StreamWriter writer = new StreamWriter(path, false))
                {
                    foreach (String line in lines)
                        writer.WriteLine(line);
                }
            }
        }

        public void CheckFileClosedStatus()
        {
            String path = @"I:\\Datafile\\Control\\FileLocking.csv";
            List<String> lines = new List<String>();

            if (File.Exists(path))
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    String line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Contains(","))
                        {
                            String[] split = line.Split(',');

                            if (split[0].Contains("Brand_DNR"))
                            {
                                split[1] = "Closed";
                                line = String.Join(",", split);
                                TheFileIs = "Closed";
                            }
                            if (split[0].Contains("Database"))
                            {
                                split[1] = "Closed";
                                line = String.Join(",", split);
                                TheFileIs = "Closed";
                            }
                            if (split[0].Contains("Dealers"))
                            {
                                split[1] = "Closed";
                                line = String.Join(",", split);
                                TheFileIs = "Closed";
                            }
                            if (split[0].Contains("Product"))
                            {
                                split[1] = "Closed";
                                line = String.Join(",", split);
                                TheFileIs = "Closed";
                            }
                            if (split[0].Contains("NextClaim"))
                            {
                                split[1] = "Closed";
                                line = String.Join(",", split);
                                TheFileIs = "Closed";
                            }
                            if (split[0].Contains("Related"))
                            {
                                split[1] = "Closed";
                                line = String.Join(",", split);
                                TheFileIs = "Closed";
                            }
                            if (split[0].Contains("Dealers_Number"))
                            {
                                split[1] = "Closed";
                                line = String.Join(",", split);
                                TheFileIs = "Closed";
                            }
                            if (split[0].Contains("Estimates"))
                            {
                                split[1] = "Closed";
                                line = String.Join(",", split);
                                TheFileIs = "Closed";
                            }
                            if (split[0].Contains("Gold"))
                            {
                                split[1] = "Closed";
                                line = String.Join(",", split);
                                TheFileIs = "Closed";
                            }
                        }

                        lines.Add(line);
                    }
                }

                using (StreamWriter writer = new StreamWriter(path, false))
                {
                    foreach (String line in lines)
                        writer.WriteLine(line);
                }
            }
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


                loopCount = 0;

                while (!reader.EndOfStream)
                {
                    var lineRead = reader.ReadLine();
                    var values = lineRead.Split(',');

                    listA.Add(values[0]);       //  war_prd
                    listB.Add(values[1]);       //  claim_no
                    listC.Add(values[2]);       //  datein
                    listD.Add(values[3]);       //  fname
                    listE.Add(values[4]);       //  lname
                    listF.Add(values[5]);       //  addr
                    listG.Add(values[6]);       //  city
                    listH.Add(values[7]);       //  state
                    listI.Add(values[8]);       //  zip
                    listJ.Add(values[9]);       //  hphone          Home Phone #
                    listK.Add(values[10]);      //  wphone          Work Phone #
                    listL.Add(values[11]);      //  prob_compl      Problem Complaint
                    listM.Add(values[12]);      //  brand           Manuf Brand
                    listN.Add(values[13]);      //  serv_no
                    listO.Add(values[14]);
                    listP.Add(values[15]);
                    listQ.Add(values[16]);
                    listR.Add(values[17]);
                    listS.Add(values[18]);
                    listT.Add(values[19]);
                    listU.Add(values[20]);
                    listV.Add(values[21]);
                    listW.Add(values[22]);
                    listX.Add(values[23]);
                    listY.Add(values[24]);
                    listZ.Add(values[25]);
                    listAA.Add(values[26]);
                    listAB.Add(values[27]);
                    listAC.Add(values[28]);     //  war_stat         Warranty Status
                    listAD.Add(values[29]);     //  purch_date       Purchase Date for Warranty Claim
                    listAE.Add(values[30]);     //  fthr_exp1        Further Explination C/C line 2
                    listAF.Add(values[31]);     //  frth_exp2        Further Explination C/C line 3
                    listAG.Add(values[32]);
                    listAH.Add(values[33]);
                    listAI.Add(values[34]);     //  dname
                    listAJ.Add(values[35]);     //  daddr
                    listAK.Add(values[36]);     //  dcity
                    listAL.Add(values[37]);     //  dstate
                    listAM.Add(values[38]);     //  dzip
                    listAN.Add(values[39]);     //  dphone
                    listAO.Add(values[40]);
                    listAP.Add(values[41]);
                    listAQ.Add(values[42]);
                    listAR.Add(values[43]);
                    listAS.Add(values[44]);
                    listAT.Add(values[45]);
                    listAU.Add(values[46]);
                    listAV.Add(values[47]);
                    listAW.Add(values[48]);
                    listAX.Add(values[49]);
                    listAY.Add(values[50]);
                    listAZ.Add(values[51]);
                    listBA.Add(values[52]);
                    listBB.Add(values[53]);
                    listBC.Add(values[54]);
                    listBD.Add(values[55]);
                    listBE.Add(values[56]);
                    listBF.Add(values[57]);
                    listBG.Add(values[58]);
                    listBH.Add(values[59]);
                    listBI.Add(values[60]);
                    listBJ.Add(values[61]);
                    listBK.Add(values[62]);
                    listBL.Add(values[63]);
                    listBM.Add(values[64]);
                    listBN.Add(values[65]);
                    listBO.Add(values[66]);
                    listBP.Add(values[67]);
                    listBQ.Add(values[68]);
                    listBR.Add(values[69]);
                    listBS.Add(values[70]);
                    listBT.Add(values[71]);
                    listBU.Add(values[72]);
                    listBV.Add(values[73]);



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
                    var mDealer = listAI[loopCount];
                    var mBench = listBD[loopCount];
                    var mWarranty = listBL[loopCount];
                    var mIsWarr = listBL[loopCount];
                    var mTheNewClaimNum = listBQ[loopCount];

                    if (mTheNewClaimNum.Length >= 7)   // Convert new claim# to Remove the "A" prefix
                    {
                        var tt = mTheNewClaimNum;
                        var yy = mTheNewClaimNum.Length;
                        yy--;                               // subtract 1
                        var uu = tt.Substring(1, yy);
                        mTheNewClaimNum = uu;
                        
                    }


                    if (mClaim_NO == claim_no)
                    {
                        label6.Text = mFname + " " + mLname;
                        label7.Text = mAddr;
                        label8.Text = mCity + ", " + mState + " " + mZip;
                        label9.Text = claim_no;
                        label10.Text = listBJ[loopCount];
                        label11.Text = mHphone;
                        label14.Text = mWPhone;
                        label15.Text = "Dealer:     " + mDealer;
                        label16.Text = "Address:  " + listAJ[loopCount];
                        if (listAM[loopCount] == "0")
                        {
                            listAM[loopCount] = "00000";
                        }
                        label17.Text = "C,S, Zip:   " + listAK[loopCount] + ", " + listAL[loopCount] + " " + listAM[loopCount];
                        label18.Text = "Phone:        " + listAN[loopCount];
                        label19.Text = "Claim #:      " + claim_no;
                        label20.Text = "Purchased: " + listAD[loopCount];
                        label21.Text = "Labor Charges: ";
                        var tLaborChg = decimal.Parse(listR[loopCount]).ToString("C2").Length;
                        decimal LaborCharge = decimal.Parse(listR[loopCount]);
                        switch (tLaborChg)
                        {
                            case 1: 
                                label34.Text = "           " + decimal.Parse(listR[loopCount]).ToString("C2");
                                break;
                            case 2:
                                label34.Text = "          " + decimal.Parse(listR[loopCount]).ToString("C2");
                                break;
                            case 3:
                                label34.Text = "         " + decimal.Parse(listR[loopCount]).ToString("C2");
                                break;
                            case 4:
                                label34.Text = "        " + decimal.Parse(listR[loopCount]).ToString("C2");
                                break;
                            case 5: 
                                label34.Text = "       " + decimal.Parse(listR[loopCount]).ToString("C2");
                                break;
                            case 6:
                                label34.Text = "      " + decimal.Parse(listR[loopCount]).ToString("C2");
                                break;
                            case 7:
                                label34.Text = "     " + decimal.Parse(listR[loopCount]).ToString("C2");
                                break;
                            case 8:
                                label34.Text = "    " + decimal.Parse(listR[loopCount]).ToString("C2");
                                break;
                            case 9:
                                label34.Text = "   " + decimal.Parse(listR[loopCount]).ToString("C2");
                                break;
                            case 10:
                                label34.Text = "  " + decimal.Parse(listR[loopCount]).ToString("C2");
                                break;
                        }
                        
                        label22.Text = "Parts Charges:  ";
                        var tPartsChg = decimal.Parse(listS[loopCount]).ToString("C2").Length;
                        decimal PartsCharge = decimal.Parse(listS[loopCount]);
                        switch (tPartsChg)
                        {
                            case 1:
                                label33.Text = "            " + decimal.Parse(listS[loopCount]).ToString("C2");
                                break;
                            case 2:
                                label33.Text = "           " + decimal.Parse(listS[loopCount]).ToString("C2");
                                break;
                            case 3:
                                label33.Text = "          " + decimal.Parse(listS[loopCount]).ToString("C2");
                                break;
                            case 4:
                                label33.Text = "         " + decimal.Parse(listS[loopCount]).ToString("C2");
                                break;
                            case 5:
                                label33.Text = "       " + decimal.Parse(listS[loopCount]).ToString("C2");
                                break;
                            case 6:
                                label33.Text = "       " + decimal.Parse(listS[loopCount]).ToString("C2");
                                break;
                            case 7:
                                label33.Text = "     " + decimal.Parse(listS[loopCount]).ToString("C2");
                                break;
                            case 8:
                                label33.Text = "    " + decimal.Parse(listS[loopCount]).ToString("C2");
                                break;
                            case 9:
                                label33.Text = "  " + decimal.Parse(listS[loopCount]).ToString("C2");
                                break;
                            case 10:
                                label33.Text = " " + decimal.Parse(listS[loopCount]).ToString("C2");
                                break;
                        }

                        label23.Text = "Parts Shipping:";

                        var tDepositShipping =  decimal.Parse(listAA[loopCount]).ToString("C2").Length;
                        decimal Shipping = decimal.Parse(listAA[loopCount]);
                        switch (tDepositShipping)
                        {
                            case 1:
                                label30.Text = "            " + decimal.Parse(listAA[loopCount]).ToString("C2");
                                break;
                            case 2:
                                label30.Text = "           " + decimal.Parse(listAA[loopCount]).ToString("C2");
                                break;
                            case 3:
                                label30.Text = "          " + decimal.Parse(listAA[loopCount]).ToString("C2");
                                break;
                            case 4:
                                label30.Text = "         " + decimal.Parse(listAA[loopCount]).ToString("C2");
                                break;
                            case 5:
                                label30.Text = "       " + decimal.Parse(listAA[loopCount]).ToString("C2");
                                break;
                            case 6:
                                label30.Text = "       " + decimal.Parse(listAA[loopCount]).ToString("C2");
                                break;
                            case 7:
                                label30.Text = "     " + decimal.Parse(listAA[loopCount]).ToString("C2");
                                break;
                            case 8:
                                label30.Text = "     " + decimal.Parse(listAA[loopCount]).ToString("C2");
                                break;
                            case 9:
                                label30.Text = "" + decimal.Parse(listAA[loopCount]).ToString("C2");
                                break;
                            case 10:
                                label30.Text = "" + decimal.Parse(listAA[loopCount]).ToString("C2");
                                break;
                        }
                                
                        //
                        label24.Text = "Minus Down Payment: ";

                        //
                        var tDownPMNT = decimal.Parse(listU[loopCount]).ToString("C2").Length;
                        decimal DownPayment = decimal.Parse(listU[loopCount]);
                        switch (tDownPMNT)
                        {
                            case 1:
                                label29.Text = "            " + decimal.Parse(listU[loopCount]).ToString("C2");
                                break;
                            case 2:
                                label29.Text = "           " + decimal.Parse(listU[loopCount]).ToString("C2");
                                break;
                            case 3:
                                label29.Text = "          " + decimal.Parse(listU[loopCount]).ToString("C2");
                                break;
                            case 4:
                                label29.Text = "         " + decimal.Parse(listU[loopCount]).ToString("C2");
                                break;
                            case 5:
                                label29.Text = "       " + decimal.Parse(listU[loopCount]).ToString("C2");
                                break;
                            case 6:
                                label29.Text = "       " + decimal.Parse(listU[loopCount]).ToString("C2");
                                break;
                            case 7:
                                label29.Text = "     " + decimal.Parse(listU[loopCount]).ToString("C2");
                                break;
                            case 8:
                                label29.Text = "     " + decimal.Parse(listU[loopCount]).ToString("C2");
                                break;
                            case 9:
                                label29.Text = "  " + decimal.Parse(listU[loopCount]).ToString("C2");
                                break;
                            case 10:
                                label29.Text = "   " + decimal.Parse(listU[loopCount]).ToString("C2");
                                break;
                        } 
                        label25.Text = "Tax Charges:   ";
                        var tTaxes = decimal.Parse(listAB[loopCount]).ToString("C2").Length;
                        decimal Taxes = decimal.Parse(listAB[loopCount]);
                        switch (tTaxes)
                        {
                            case 1:
                                label28.Text = "              " + decimal.Parse(listAB[loopCount]).ToString("C2");
                                break;
                            case 2:
                                label28.Text = "             " + decimal.Parse(listAB[loopCount]).ToString("C2");
                                break;
                            case 3:
                                label28.Text = "            " + decimal.Parse(listAB[loopCount]).ToString("C2");
                                break;
                            case 4:
                                label28.Text = "           " + decimal.Parse(listAB[loopCount]).ToString("C2");
                                break;
                            case 5:
                                label28.Text = "         " + decimal.Parse(listAB[loopCount]).ToString("C2");
                                break;
                            case 6:
                                label28.Text = "       " + decimal.Parse(listAB[loopCount]).ToString("C2");
                                break;
                            case 7:
                                label28.Text = "      " + decimal.Parse(listAB[loopCount]).ToString("C2");
                                break;
                            case 8:  
                                label28.Text = "     " + decimal.Parse(listAB[loopCount]).ToString("C2");
                                break;
                            case 9:
                                label28.Text = "       " + decimal.Parse(listAB[loopCount]).ToString("C2");
                                break;
                            case 10:
                                label28.Text = "  " + decimal.Parse(listAB[loopCount]).ToString("C2");
                                break;

                        }
                        label26.Text = "Balance Due.:";
                        var tTotal = decimal.Parse(listQ[loopCount]).ToString("C2").Length;
                        switch (tTotal)
                        {
                            case 1:
                                label27.Text = "           " + decimal.Parse(listQ[loopCount]).ToString("C2");
                                break;
                            case 2:
                                label27.Text = "          " + decimal.Parse(listQ[loopCount]).ToString("C2");
                                break;
                            case 3:
                                label27.Text = "         " + decimal.Parse(listQ[loopCount]).ToString("C2");
                                break;
                            case 4:
                                label27.Text = "        " + decimal.Parse(listQ[loopCount]).ToString("C2");
                                break;
                            case 5:
                                label27.Text = "       " + decimal.Parse(listQ[loopCount]).ToString("C2");
                                break;
                            case 6:
                                label27.Text = "       " + decimal.Parse(listQ[loopCount]).ToString("C2");
                                break;
                            case 7:
                                label27.Text = "     " + decimal.Parse(listQ[loopCount]).ToString("C2");
                                break;
                            case 8:
                                label27.Text = "    " + decimal.Parse(listQ[loopCount]).ToString("C2");
                                break;
                            case 9:
                                label27.Text = "" + decimal.Parse(listQ[loopCount]).ToString("C2");
                                break;
                            case 10:
                                label27.Text = "  " + decimal.Parse(listQ[loopCount]).ToString("C2");
                                break;
                        }
                        label39.Text = "Diagnostics:";
                        var tDiag = decimal.Parse(listZ[loopCount]).ToString("C2").Length;
                        decimal Diagnostics = decimal.Parse(listZ[loopCount]);
                        switch (tDiag)
                        {
                            case 1:
                                label40.Text = "           " + decimal.Parse(listZ[loopCount]).ToString("C2");
                                break;
                            case 2:
                                label40.Text = "          " + decimal.Parse(listZ[loopCount]).ToString("C2");
                                break;
                            case 3:
                                label40.Text = "         " + decimal.Parse(listZ[loopCount]).ToString("C2");
                                break;
                            case 4:
                                label40.Text = "        " + decimal.Parse(listZ[loopCount]).ToString("C2");
                                break;
                            case 5:
                                label40.Text = "        " + decimal.Parse(listZ[loopCount]).ToString("C2");
                                break;
                            case 6:
                                label40.Text = "       " + decimal.Parse(listZ[loopCount]).ToString("C2");
                                break;
                            case 7:
                                label40.Text = "       " + decimal.Parse(listZ[loopCount]).ToString("C2");
                                break;
                            case 8:
                                label40.Text = "      " + decimal.Parse(listZ[loopCount]).ToString("C2");
                                break;
                            case 9:
                                label40.Text = "" + decimal.Parse(listZ[loopCount]).ToString("C2");
                                break;
                            case 10:
                                label40.Text = "  " + decimal.Parse(listZ[loopCount]).ToString("C2");
                                break; 
                        }
                        var xSubTotal = PartsCharge + LaborCharge + Shipping + Diagnostics + Taxes;
                        switch (tDiag)
                        {
                            case 1:
                                label43.Text = "           " + xSubTotal.ToString("C2");
                                break;
                            case 2:
                                label43.Text = "          " + xSubTotal.ToString("C2");
                                break;
                            case 3:
                                label43.Text = "         " + xSubTotal.ToString("C2");
                                break;
                            case 4:
                                label43.Text = "        " + xSubTotal.ToString("C2");
                                break;
                            case 5:
                                label43.Text = "        " + xSubTotal.ToString("C2");
                                break;
                            case 6:
                                label43.Text = "     " + xSubTotal.ToString("C2");
                                break;
                            case 7:
                                label43.Text = "   " + xSubTotal.ToString("C2");
                                break;
                            case 8:
                                label43.Text = "  " + xSubTotal.ToString("C2");
                                break;
                            case 9:
                                label43.Text = "" + xSubTotal.ToString("C2");
                                break;
                            case 10:
                                label43.Text = "  " + xSubTotal.ToString("C2");
                                break;
                        }
                       // label43.Text = "     " + xSubTotal.ToString("C2");

                        label36.Text = listAH[loopCount];
                        label38.Text = mTheNewClaimNum; 
                        if (listW[loopCount] == "FALSE")
                        {
                            label37.Text = "NO POST CARD SENT";
                        }
                        else
                        {
                            label37.Text = "POST CARD SENT";
                        }
                        if (listBE[loopCount] == "FC")
                        {
                            listBE[loopCount] = "Front Counter";
                        }
                        label41.Text = listBE[loopCount];
                        if (mBench.Contains("SERVICE RENDERED"))
                        {
                            label47.ForeColor = Color.Red;
                            label47.Text = "CLOSED CLAIM";
                            Text += "  CLOSED CLAIM !";
                        }
                        else
                        {
                            label47.ForeColor = Color.Green;
                            label47.Text = "Open Claim";
                        }
                        if (mWarranty.Contains("RECALL") || mIsWarr == "WARRANTY")
                        {
                            label32.Text = " RECALL ";
                        }
                        if (mIsWarr.Contains("RECALL"))
                        {
                            label32.Text += "PARTS ONLY No Labor";
                        }
                        //loop++;
                    }
                    loopCount++;
                }
                reader.Close();                 // Close the open file
                CheckFileClosedStatus();        // Close all open files
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 876: Sorry an error has occured: " + ex.Message);
            }
        }
    }
}
