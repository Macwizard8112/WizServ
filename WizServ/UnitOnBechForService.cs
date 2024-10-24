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
    public partial class UnitOnBechForService : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        private int loopCount, loop;
        private string TECHNICIAN;
        private static readonly string Database = @"I:\\Datafile\\Control\\Database.CSV";
        private static readonly string PartsUsed = @"I:\\Datafile\\Control\\Partsused.CSV";
        private static readonly string PriParts = @"I:\\Datafile\\Control\\Part_Pri.CSV";
        private int linesPrinted;
        private string[] lines;
        public string PCNAME, claimno, mTS1, mTS2, mTS3, mTS4;
        public string mTech, mBrand, mModel, mSerial, mClaim_NO, mFname, mLname, mProd, mDate_IN, mClosed, mTheTech, mTechNum;
        public string xQty, xPartNo, xClaim, xDesc, xPrice, xClaim_no, xCost, xPartUsedDate, xPurchDate, xInClaim;
        public string first, second, third, fourth;

        public UnitOnBechForService()
        {
            InitializeComponent();
            button1.Visible = true;
            Icon = image100;
            claimno = Version.Claim;    // pass along the claim # from Main Menu
            SetLabel();
            GetData();
            GetPartsUsed();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (e.ToString() == Keys.Back.ToString())
            {
                label27.Text = textBox1.Text.Length.ToString();
                textBox1.Text = textBox1.Text.Replace(",", ";");
            }
            if (textBox1.Text.Length >= 78)
            {
                textBox2.Focus();
                textBox2.SelectionLength = 0;
            }
            label27.Text = textBox1.Text.Length.ToString();
            textBox1.Text = textBox1.Text.Replace(",", ";");
            if (e.ToString() == Keys.Enter.ToString())
            {
                textBox2.Focus();
                textBox2.SelectionLength = 0;
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (textBox1.Text.Length == 0)
                {
                    textBox1.Text = ".";
                }
                textBox2.Focus();
                textBox2.SelectionLength = 0;
            }
            if (e.KeyData == Keys.Down)
            {
                textBox2.Focus();
                textBox2.SelectionLength = 0;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ',')
            {
                textBox1.Text.Replace(",", ";");
            }
            textBox1.Text = textBox1.Text.Replace(",", ";");
        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (e.ToString() == Keys.Back.ToString())
            {
                label26.Text = textBox2.Text.Length.ToString();
                textBox2.Text = textBox2.Text.Replace(",", ";");
            }
            if (textBox2.Text.Length >= 78)
            {
                textBox3.Focus();
                textBox3.SelectionLength = 0;
            }
            label26.Text = textBox2.Text.Length.ToString();
            textBox2.Text = textBox2.Text.Replace(",", ";");
            if (e.ToString() == Keys.Enter.ToString())
            {
                textBox3.Focus();
                textBox3.SelectionLength = 0;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ',')
            {
                textBox2.Text.Replace(",", ";");
            }
            textBox2.Text = textBox2.Text.Replace(",", ";");
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (textBox2.Text.Length == 0)
                {
                    textBox2.Text = ".";
                }
                textBox3.Focus();
                textBox3.SelectionLength = 0;
            }
            if (e.KeyData == Keys.Down)
            {
                textBox3.Focus();
                textBox3.SelectionLength = 0;
            }
            if (e.KeyData == Keys.Up)
            {
                textBox1.Focus();
                textBox1.SelectionLength = 0;
            }

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (e.ToString() == Keys.Back.ToString())
            {
                label28.Text = textBox3.Text.Length.ToString();
                textBox3.Text = textBox3.Text.Replace(",", ";");
            }
            if (textBox3.Text.Length >= 78)
            {
                textBox4.Focus();
                textBox4.SelectionLength = 0;
            }
            label28.Text = textBox3.Text.Length.ToString();
            textBox3.Text = textBox3.Text.Replace(",", ";");
            if (e.ToString() == Keys.Enter.ToString())
            {
                textBox3.Text = textBox3.Text.Replace(",", ";");
                textBox4.Focus();
                textBox4.SelectionLength = 0;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ',')
            {
                textBox3.Text.Replace(",", ";");
            }
            textBox3.Text = textBox3.Text.Replace(",", ";");
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (textBox3.Text.Length == 0)
                {
                    textBox3.Text = ".";
                }
                textBox4.Focus();
                textBox4.SelectionLength = 0;
            }
            if (e.KeyData == Keys.Down)
            {
                textBox4.Focus();
                textBox4.SelectionLength = 0;
            }
            if (e.KeyData == Keys.Up)
            {
                textBox2.Focus();
                textBox2.SelectionLength = 0;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (e.ToString() == Keys.Back.ToString())
            {
                label29.Text = textBox4.Text.Length.ToString();
                textBox4.Text = textBox4.Text.Replace(",", ";");
            }
            if (textBox4.Text.Length >= 78)
            {
                button2.Select();
            }
            label29.Text = textBox4.Text.Length.ToString();
            textBox4.Text = textBox4.Text.Replace(",", ";");
            if (e.ToString() == Keys.Enter.ToString())
            {
                button2.Select();
            }
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (textBox4.Text.Length == 0)
                {
                    textBox4.Text = ".";
                }
                button2.Select();
            }
            if (e.KeyData == Keys.Up)
            {
                textBox3.Focus();
                textBox3.SelectionLength = 0;
            }
        }

        private void SetLabel()
        {
            label38.Text = "【――――――――――――――――――――――――――――――――――――― Services Performed  ―――――――――――――――――――――――――――――――――――――】";
        }

        private void button1_Click(object sender, EventArgs e)      // Save Changes, Add Parts
        {

        }

        private void button2_Click(object sender, EventArgs e)      // Save Changes, No Parts
        {
            button1.Visible = false;        // User selected save with no new parts, turn off save with new parts.
            // Setup variables to replace
            first = textBox1.Text;
            second = textBox2.Text;
            third = textBox3.Text;
            fourth = textBox4.Text;
            if (textBox1.Text.Length == 0)
            {
                textBox1.Text = ".";
                first = textBox1.Text;
            }
            if (textBox2.Text.Length == 0)
            {
                textBox2.Text = ".";
                second = textBox2.Text;
            }
            if (textBox3.Text.Length == 0)
            {
                textBox3.Text = ".";
                third = textBox3.Text;
            }
            if (textBox4.Text.Length == 0)
            {
                textBox4.Text = ".";
                fourth = textBox4.Text;
            }
            first.Replace(",", ";");        // make sure user didn't type a comma ","
            second.Replace(",", ";");
            third.Replace(",", ";");
            fourth.Replace(",", ";");
            textBox1.Text = first;
            textBox2.Text = second;
            textBox3.Text = third;
            textBox4.Text = fourth;

            List<String> lines = new List<String>();

            if (File.Exists(Database))
            {
                using (StreamReader reader = new StreamReader(Database))
                {
                    String line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Contains(","))
                        {
                            String[] split = line.Split(',');
                            try
                            {
                                if (split[1] == label6.Text) // Claim # 
                                {
                                    split[47] = first;      // TextBox1
                                    split[48] = second;     // TextBox2
                                    split[49] = third;      // TextBox3
                                    split[50] = fourth;     // TextBox4
                                    line = String.Join(",", split);
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error 288: \n" + ex);
                            }
                        }
                        lines.Add(line);
                    }
                    reader.Close();
                }
                try
                {
                    using (StreamWriter writer = new StreamWriter(Database, false))
                    {
                        foreach (String line in lines)
                            writer.WriteLine(line);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error line 109: \n" + ex);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }

        private void button4_Click(object sender, EventArgs e)  // Return to previous menu
        {
            Hide();
            ServiceRenderClaimMenu f2 = new ServiceRenderClaimMenu();
            f2.Show();
        }

        public void GetPartsUsed()
        {
            loop = 0;
            loopCount = 0;
            string specifier;

            try
            {
                StreamReader reader = new StreamReader(PartsUsed, Encoding.GetEncoding("Windows-1252"));
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
               

                loopCount = 0;

                while (!reader.EndOfStream)
                {
                    var lineRead = reader.ReadLine();
                    var values = lineRead.Split(',');

                    listA.Add(values[0]);       //  Quantity
                    listB.Add(values[1]);       //  Part Number
                    listC.Add(values[2]);       //  Claim #
                    listD.Add(values[3]);       //  Description
                    listE.Add(values[4]);       //  Price   (Customer Cost)
                    listF.Add(values[5]);       //  Claim_no
                    listG.Add(values[6]);       //  Cost    (Our Cost)
                    listH.Add(values[7]);       //  Added Date
                    listI.Add(values[8]);       //  Last Purchased Date
                    listJ.Add(values[9]);       //  InClaim (Y/N)

                    xClaim_no = listC[loopCount];

                    if (xClaim_no == claimno)
                    {
                        xPartNo = listB[loopCount];
                        if (xPartNo == "2")
                        {
                            xPartNo = "0002";
                        }
                        if (xPartNo == "3")
                        {
                            xPartNo = "0003";
                        }
                        if (xPartNo == "4")
                        {
                            xPartNo = "0004";
                        }
                        if (xPartNo == "5")
                        {
                            xPartNo = "0005";
                        }
                        if (xPartNo == "6")
                        {
                            xPartNo = "0006";
                        }
                        if (xPartNo == "8")
                        {
                            xPartNo = "0008";
                        }
                        xPrice = listE[loopCount];
                        var xp = decimal.Parse(xPrice);

                        xDesc = listD[loopCount];
                       
                        switch (xPartNo.Length)
                        {
                            case 17:
                                xPartNo += "";
                                break;
                            case 16:
                                xPartNo += " ";
                                break;
                            case 15:
                                xPartNo += "  ";
                                break;
                            case 14:
                                xPartNo += "   ";
                                break;
                            case 13:
                                xPartNo += "    ";
                                break;
                            case 12:
                                xPartNo += "     ";
                                break;
                            case 11:
                                xPartNo += "      ";
                                break;
                            case 10:
                                xPartNo += "       ";
                                break;
                            case 9:
                                xPartNo += "        ";
                                break;
                            case 8:
                                xPartNo += "         ";
                                break;
                            case 7:
                                xPartNo += "          ";
                                break;
                            case 6:
                                xPartNo += "           ";
                                break;
                            case 5:
                                xPartNo += "            ";
                                break;
                            case 4:
                                xPartNo += "             ";
                                break;
                            case 3:
                                xPartNo += "             ";
                                break;

                        }
                        decimal value = xp;
                        specifier = "0,0.00";
                        var xp2 = (value.ToString(specifier));
                        switch (xp2.Length)
                        {
                            case 5:
                                xp2 += "\t";
                                break;
                            case 6:
                                xp2 += " ";
                                break;
                            default:
                                xp2 += " ";
                                break;
                        }
                        richTextBox1.Text += xPartNo + "\t" + xp2 + "\t" + xDesc + "\n";
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
                    mClaim_NO = listB[loopCount];
                    mDate_IN = listC[loopCount];
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
                    var mWarranty = listBL[loopCount];
                    var mFthr_exp1 = listAE[loopCount];
                    var mFthr_exp2 = listAF[loopCount];
                    var mDealer = listAI[loopCount];

                    mTS1 = listAU[loopCount];
                    mTS2 = listAV[loopCount];
                    mTS3 = listAW[loopCount];
                    mTS4 = listAX[loopCount];
                    mTechNum = listBA[loopCount];
                    mClosed = listBC[loopCount];
                    var mBench = listBD[loopCount];
                    mProd = listBJ[loopCount];
                    mTheTech = listBB[loopCount];
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


                    if (mClaim_NO == claimno)
                    {
                        claimno = mClaim_NO;
                        label6.Text = mClaim_NO;
                        label7.Text = mFname + " " + mLname;
                        label8.Text = mProd;
                        label9.Text = mBrand;
                        label11.Text = mModel;
                        label14.Text = mSerial;
                        label16.Text = mDate_IN;
                        label17.Text = mClosed;
                        label19.Text = mTechNum; 
                        label21.Text = mTheTech;
                        textBox1.Text = mTS1;
                        textBox2.Text = mTS2;
                        textBox3.Text = mTS3;
                        textBox4.Text = mTS4;
                    }
                    loopCount++;
                }
                reader.Close();
                Version.DatabaseIsLocked = false;
                textBox1.DeselectAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception line 743\n" + ex);
            }
        }
    }
}
