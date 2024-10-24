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
using Microsoft.Win32;
using System.Windows.Forms;
using System.Media;
using System.Threading;
using System.Threading.Tasks;
using System.Management;
using System.Drawing.Printing;
using System.Threading.Tasks;

namespace WizServ
{
    public partial class Brands : Form
    {
        private readonly string DatabaseFile = @"I:\Datafile\Control\Database.CSV";
        public readonly string DBSorted = @"I:\Datafile\Control\BrandSortedClaim.CSV";
        public readonly string DBSortedM = @"I:\Datafile\Control\BrandSortedMFG.CSV";
        public readonly string DBSortedM1 = @"I:\Datafile\Control\BrandSortedMFG1.CSV";
        public Icon image100 = Properties.Resources.WizServ;
        private string mClaim_NO, mFname, mLname, mBrand, mModel, mStatus;
        private int linesPrinted, loopCount, IsFound, loop;
        public bool IsChecked;
        private string[] lines;
        public StringBuilder csv;
        public string butPress;

        public Brands()
        {
            InitializeComponent();
            csv = new StringBuilder();
            Icon = image100;
            CheckStstus();
            var k = " Wizard Electronics Nightly Brand Report by Claim # " + DateTime.Now.ToShortDateString() + " ";
            label1.Text = k;
        }

        private void SortByMFG()
        {
            try
            {
                // Create the IEnumerable data source  
                string[] lines = File.ReadAllLines(DBSorted);

                // Create the query. Put field 0 first (Manufacturer), then  
                //  
                IEnumerable<string> query =
                    from line in lines
                    let x = line.Split(',')
                    orderby x[0]
                    select x[0] + "," + x[1] + "," + x[2] + "," + x[3] + "," + x[4] + "," + x[5];

                // Execute the query and write out the new file. Note that WriteAllLines  
                // takes a string[], so ToArray is called on the query.  

                File.WriteAllLines(DBSortedM, query.ToArray());

                WrSoMFG();

               // WriteSortedMFG();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception line 72:\n" + ex);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            butPress = "2";
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            Hide();
            BrandMFG f2 = new BrandMFG();
            f2.Show();
        }

        private void WrSoMFG()
        {
            var ffirst = "Manuf.";
            var fsecond = "Model";
            var fthird = "Fist Name";
            var ffourth = "Last Name";
            var ffifth = "Claim #:";
            var fsixth = "Disposition";
            var fnewLine = string.Format(ffirst + "," + fsecond + "," + fthird + "," + ffourth + "," + ffifth + "," + fsixth) + Environment.NewLine;

            loopCount = 0;
            StreamReader reader = new StreamReader(DBSortedM, Encoding.GetEncoding("Windows-1252"));
            String line = reader.ReadLine();

            List<string> listA = new List<string>();
            List<string> listB = new List<string>();
            List<string> listC = new List<string>();
            List<string> listD = new List<string>();
            List<string> listE = new List<string>();
            List<string> listF = new List<string>();

            while (!reader.EndOfStream)
            {
                var lineRead = reader.ReadLine();
                var values = lineRead.Split(',');

                listA.Add(values[0]);       //  Manuf
                listB.Add(values[1]);       //  Model
                listC.Add(values[2]);       //  fname           Customer First Name
                listD.Add(values[3]);       //  lname           Customer Last Name
                listE.Add(values[4]);       //  Claim #
                listF.Add(values[5]);       //  Disposition

                var a = listA[loopCount];
                var b = listB[loopCount];
                var c = listC[loopCount];
                var d = listD[loopCount];
                var e = listE[loopCount];
                var f = listF[loopCount];
                if (f.StartsWith("BENCH ON BENCH"))
                {
                    f = "BENCH ON BENCH -" + "    ";
                }

                if (!listF[loopCount].StartsWith("CONSIGN"))
                {
                    if (a == "Manuf.")
                    {

                    }
                    else
                    {
                        fnewLine += a + "," + b + "," + c + "," + d + "," + e + "," + f + Environment.NewLine;
                        richTextBox2.Text = fnewLine;
                    }
                }
                loopCount++;
            }
            if (File.Exists("DBSortedM1"))
            {
                File.Delete("DBSortedM1");
                Thread.Sleep(100);
            }
            //File.AppendAllText(DBSortedM1, fnewLine);
            File.WriteAllText(DBSortedM1, fnewLine);
        }

        private void WriteSortedMFG()
        {
            // Create the IEnumerable data source  
            string[] lines = File.ReadAllLines(DBSortedM);

            // Create the query. Put field 0 first (Manufacturer), then  
            //  
            IEnumerable<string> query =
                from line in lines
                let x = line.Split(',')
                orderby x[0]
                select x[0] + "," + x[1] + "," + x[2] + "," + x[3] + "," + x[4] + "," + x[5];

            // Execute the query and write out the new file. Note that WriteAllLines  
            // takes a string[], so ToArray is called on the query.  

            File.WriteAllLines(@DBSortedM, query.ToArray());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string _Path = @"I:\Datafile\Control\BrandDaily.txt";
            if (!File.Exists(_Path))
            {
                StreamWriter SW = new StreamWriter(_Path);
                SW.WriteLine(richTextBox1.Text);
                SW.Close();
            }
            else if (File.Exists(_Path))
            {
                MessageBox.Show("This File is Exists");
                SaveFileDialog SFD = new SaveFileDialog();
                SFD.FileName = "";
                SFD.AddExtension = true;
                SFD.DefaultExt = ".txt";
                DialogResult result = SFD.ShowDialog();
                if (string.IsNullOrEmpty(SFD.FileName))
                {
                    return;
                }
                else if (result == DialogResult.OK)
                {
                    StreamWriter SW = new StreamWriter(SFD.FileName);
                    SW.WriteLine(richTextBox1.Text);
                    SW.Close();
                }
            }
        }

        private async void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
           if (checkBox1.Checked == true)
            {
                checkBox1.Text = "Include Consignment Claims";
                await Task.Delay(200);
                IsChecked = true;
                CheckStstus();
            }
           if (checkBox1.Checked == false)
            {
                IsChecked = false;
                checkBox1.Text = "Skip Consignment Claims"; 
                await Task.Delay(200);
                CheckStstus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            butPress = "1";
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            var t = richTextBox1.Text;
            var k = "               Wizard Electronics Nightly Brand Report by Claim# " + DateTime.Now.ToShortDateString() + "\n\n";
            if (butPress == "1")
            {
                richTextBox2.Text = k + t;
            }
            {
                printDocument1.DefaultPageSettings.Landscape = false;   // false = Portrait, true = landscape
                int x = e.MarginBounds.Left;
                int y = e.MarginBounds.Top;
                x -= 60;                        // Move to Center on paper
                y -= 30;

                Brush brush = new SolidBrush(richTextBox2.ForeColor);
                
                char[] param = { '\n' };

                lines = richTextBox2.Text.Split(param);
                
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

        public void CheckStstus()
        { 
            try
            {
                if (Version.DatabaseIsLocked == true)
                {
                    MessageBox.Show("Database in use, retrying...");
                }
            }
            catch (Exception)
            {
                Thread.Sleep(2700);
                CheckStstus();
            }
            try
            { 
                StreamReader reader = new StreamReader(DatabaseFile, Encoding.GetEncoding("Windows-1252"));
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
                IsFound = 0;
                loop = 0;
                richTextBox1.Text = "";
                richTextBox2.Text = "";

                var csv = new StringBuilder();

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
                    mStatus = listBD[loopCount];
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

                    mBrand += "                       ";
                    mModel += "                    ";
                    mFname += "                    ";
                    mLname += "                    ";
                    mClaim_NO += "  ";
                    mStatus += "                    ";

                    var col1 = mBrand.Substring(0, 16);
                    var col2 = mModel.Substring(0, 13);
                    var col3 = mFname.Substring(0, 13);
                    var col4 = mLname.Substring(0, 13);
                    var col5 = mClaim_NO.Substring(0, 7);
                    var col6 = mStatus.Substring(0, 20);
                    if (col6.StartsWith("BENCH ON BENCH"))
                    {
                        mStatus = mStatus.Substring(0, 15) + " -   ";
                    }

                    if (!col6.StartsWith("CONSIGNMENT"))
                    {
                        if (!col6.StartsWith("SENT"))
                        {
                            if (!col6.StartsWith("COMPLETE"))
                            {
                                richTextBox1.Text += col1 + " " + col2 + " " + col3 + " " + col4 + " " + col5 + " " + col6 + "\n";
                                IsFound++;
                                if (loop == 0)
                                {
                                    var ffirst = "Manuf.";
                                    var fsecond = "Model";
                                    var fthird = "Fist Name";
                                    var ffourth = "Last Name";
                                    var ffifth = "Claim #:";
                                    var fsixth = "Disposition";
                                    //Suggestion made by KyleMit
                                    var fnewLine = string.Format(ffirst + "," + fsecond + "," + fthird + "," + ffourth + "," + ffifth + "," + fsixth);
                                    csv.AppendLine(fnewLine);
                                }
                                loop++;
                                var first = mBrand.Trim();
                                var second = mModel.Trim();
                                var third = mFname.Trim();
                                var fourth = mLname.Trim();
                                var fifth = mClaim_NO.Trim();
                                var sixth = mStatus.Trim();
                                //Suggestion made by KyleMit
                                var newLine = string.Format(first + "," + second + "," + third + "," + fourth + "," + fifth + "," + sixth);
                                csv.AppendLine(newLine);

                            }
                        }
                        if (col6.StartsWith("CONSIGNMENT"))
                        {
                            if (IsChecked == true)
                            {
                                richTextBox1.Text += col1 + " " + col2 + " " + col3 + " " + col4 + " " + col5 + " " + col6 + "\n";
                                IsFound++;
                                var first = mBrand.Trim();
                                var second = mModel.Trim();
                                var third = mFname.Trim();
                                var fourth = mLname.Trim();
                                var fifth = mClaim_NO.Trim();
                                var sixth = mStatus.Trim();
                                //Suggestion made by KyleMit
                                var newLine = string.Format(first + "," + second + "," + third + "," + fourth + "," + fifth + "," + sixth);
                                csv.AppendLine(newLine);
                            }
                        }
                    }
                    loopCount++;
                }
                File.WriteAllText(DBSorted, csv.ToString());
                Thread.Sleep(100);
                SortByMFG();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 589: Sorry an error has occured: " + ex.Message);
            }
            label2.Text = "Found: " + IsFound.ToString();
        }
    }
}
