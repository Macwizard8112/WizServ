using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;

namespace WizServ
{
    public partial class GenerateEstimateReport : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        public string claim_no;
        private readonly string database = @"I:\\Datafile\\Control\\Database.CSV";
        public readonly string notified = @"I:\\Datafile\\Control\\Notified.CSV";
        public readonly string tech_assign = @"I:\\Datafile\\Control\\Tech_Assign.CSV";
        static readonly string key = @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\lanmanserver\parameters";
        public readonly string computerDescription = (string)Registry.GetValue(key, "srvcomment", null);
        public string ma, mb, mc, md, me, mf, mg, mh, mi, mj, mk, ml, mm, mn, mo, mp, mq, mr, ms, mt, mu, mv, mw, mx, my, mz;
        public string maa, mab, mac, mad, mae, maf, mag, mah, mai, maj, mak, mal, mam, man, mao, map, maq, mar, mas, mat, mau, mav, maw, max, may, maz;
        public string mba, mbb, mbc, mbd, mbe, mbf, mbg, mbh, mbi, mbj, mbk, mbl, mbm, mbn, mbo, mbp, mbq, mbr, mbs, mbt;
        public string mbu, mbv, mbw, mbx, TheTechis;
        public string who, ssn;
        public string TheSelectedText, BU, mApproved;
        public static string SelectedText;
        private int loopCount, loop;
        public int pass = 0;
        public decimal ePartsTotal;
        public decimal mTotal;
        public static string zClaim_NO;
        public static string zDate_IN;
        public static string zWar_Note;
        public static string zBench;
        public static string zWHLoc;
        public static string zTheTech;
        public static string zIsWarr;
        public static string zEstimate;
        public static string zRush;

        public GenerateEstimateReport()
        {
            InitializeComponent();
            Icon = image100;
            timer1.Enabled = true;
            timer1.Interval = 7000;
            timer1.Start();
            string t1 = "Double-Click Claim # to mark as notified, then click Refresh.";
            label10.Text = t1;
            label11.Text = "";
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
            string msg = "The following people need calls to pay estimate.";
            label2.Text = msg;
            label12.Text = computerDescription;
            GetDatabaseInfo();
            GetEstimatesSaved();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            button4.PerformClick();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            loop = 0;
            mTotal = 0;
            ePartsTotal = 0;
            label11.Text = "";
            GetDatabaseInfo();
            richTextBox2.Text = "";
            GetEstimatesSaved();
        }

        private void richTextBox1_DoubleClick(object sender, EventArgs e)
        {
            SelectedText = richTextBox1.SelectedText;
            Version.SELECTEDTEXT = richTextBox1.SelectedText;
            TheSelectedText = SelectedText;
            if (SelectedText.Length <= 5)
            {
                return;
            }
            string message = "Did Customer Approve Estimate ?";
            string title = "Claim #: " + SelectedText;
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {
                PullRequiredData(); 
                Version.APPROVED = "A";
                mApproved = "A";    // Approved
                WhoSSN f2 = new WhoSSN();
                f2.Show();
            }
            else
            {
                string message1 = "Did Customer Decline Estimate ?\nNo = marked as Pending Status.";
                string title1 = "Claim #: " + SelectedText;
                MessageBoxButtons buttons1 = MessageBoxButtons.YesNo;
                DialogResult result1 = MessageBox.Show(message1, title1, buttons1);
                if (result == DialogResult.Yes)
                {
                    PullRequiredData();
                    Version.APPROVED = "_";
                    mApproved = "_";    // Not Approved
                    WhoSSN f2 = new WhoSSN();
                    f2.Show();
                    if (Version.CLOSED == true)
                    {
                        button4.PerformClick();
                    }
                }
                else
                {
                    PullRequiredData();
                    Version.APPROVED = "P";
                    mApproved = "P";    // Pending
                    WhoSSN f2 = new WhoSSN();
                    f2.Show();
                    if (Version.CLOSED == true)
                    {
                        button4.PerformClick();
                    }
                }

            }

            EditCSV();
            // RecordEstimates();                                                           // moved to WHOSSN.cs
            label11.Text = "Claim: " + TheSelectedText + " marked as Customer Notified.";
            button4.PerformClick();
        }

        public void EditCSV()              // Edit CSV File - Mark as Customer Notified of Estimate / Collected Cash / Card
        {

            string path = database;
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
                            try
                            {
                                if (split[1].Contains(TheSelectedText))
                                {
                                    split[72] = mApproved;
                                    if (mApproved == "A")
                                    {
                                        split[55] = "BENCH ON BENCH - " + DateTime.Now.ToShortDateString(); // Move to On Bench Status
                                    }
                                    if (mApproved == "_")
                                    {
                                        split[49] = "*****  ESTIMATE DECLINED - REASSEMBLE  *****";             // Mark as Estimate Declined
                                        split[55] = "ESTIMATE DECLINED - " + DateTime.Now.ToShortDateString();  // Move to On Bench Status
                                    }
                                    if (mApproved == "P")
                                    {
                                        split[49] = "*****  ESTIMATE PENDING  *****";                           // Mark as Estimate Pending
                                        split[55] = "ESTIMATE PENDING - " + DateTime.Now.ToShortDateString();   // Move to PENDING Status
                                    }
                                    line = String.Join(",", split);
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error: \n" + ex);
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

        public void PullRequiredData()
        {
            claim_no = SelectedText;

            try
            {
                StreamReader reader = new StreamReader(database, Encoding.GetEncoding("Windows-1252"));
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
                    var mWar_Note = listAT[loopCount];
                    var mTS1 = listAU[loopCount];
                    var mTS2 = listAV[loopCount];
                    var mTS3 = listAW[loopCount];
                    var mts4 = listAX[loopCount];
                    var mTech = listBC[loopCount];
                    var mBench = listBD[loopCount];
                    var mWHLoc = listBE[loopCount];
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
                        yy = yy - 1;
                        var uu = tt.Substring(1, yy);
                        mTheNewClaimNum = uu;
                    }

                    if (claim_no == SelectedText)
                    {
                        zClaim_NO = mClaim_NO;
                        zDate_IN = mDate_IN;
                        zWar_Note = mWar_Note;
                        zBench = mBench;
                        zWHLoc = mWHLoc;
                        zTheTech = mTheTech;
                        zIsWarr = mIsWarr;
                        zEstimate = mEstimate;
                        zRush = mRush;

                        loop++;
                    }
                    loopCount++;
                }
                reader.Close(); // Close the open file
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 411: Sorry an error has occured: " + ex.Message);
            }
        }

        public void RecordEstimates()              // Edit CSV File - Mark as Customer Notified of Estimate / Collected Cash / Card
        {

            string path = notified;
            who = Version.WHO;
            ssn = Version.SSN;

            string theDate = DateTime.Now.ToString("MM/dd/yyyy");
            string TheTime = DateTime.Now.ToString("HH:mm:ss");
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.WriteLine(TheSelectedText + "," + theDate + "," + TheTime + "," + who + "," + ssn + "," + mApproved + "," + TheTechis);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured: \n" + ex);
            }
        }

        private void button3_Click(object sender, EventArgs e)  // Print
        {
            richTextBox1.SaveFile(@"I:\\Datafile\\Doc\\Estimate2.rtf", RichTextBoxStreamType.RichText);
            TextWriter txt = new StreamWriter("I:\\Datafile\\Doc\\Estimate.rtf");
            txt.Write(richTextBox1.Text);
            txt.Close();
            Process.Start("wordpad.exe", "/p I:\\Datafile\\Doc\\Estimate2.rtf");
        }

        private void button2_Click(object sender, EventArgs e)  // Page Setup
        {
            richTextBox1.SaveFile(@"I:\\Datafile\\Doc\\Estimate2.rtf", RichTextBoxStreamType.RichText);
            TextWriter txt = new StreamWriter("I:\\Datafile\\Doc\\Estimate.rtf");
            txt.Write(richTextBox1.Text);
            txt.Close();
            var fileToOpen = "I:\\Datafile\\Doc\\Estimate2.rtf";
            if (!File.Exists(fileToOpen))
            {
                button1.PerformClick();
            }
            var process = new Process
            {
                StartInfo = new ProcessStartInfo()
                {
                    UseShellExecute = true,
                    FileName = fileToOpen
                }
            };

            process.Start();
            process.WaitForExit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
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
            catch (Exception ex)
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

        public void AddNewLine()
        {
            return;
            var csv = new StringBuilder();

            string first = zClaim_NO;
            string second = zDate_IN;
            string third = zWar_Note;
            string fourth = zBench;
            string fifth = zWHLoc;
            string sixth = zTheTech;
            string seventh = zIsWarr;
            string eighth = zEstimate;
            string ninth = zRush;
            var newLine = first + "," + second + "," + third + "," + sixth + "," + fourth + "," + fifth + "," + seventh + "," + eighth + "," + ninth + Environment.NewLine;
            csv.Append(newLine);

            //File.WriteAllText(tech_assign, csv.ToString());
        }

        public void GetEstimatesSaved()
        {
            try
            {
                StreamReader reader = new StreamReader(notified, Encoding.GetEncoding("Windows-1252"));
                String line = reader.ReadLine();

                List<string> claim = new List<string>();
                List<string> date = new List<string>();
                List<string> time = new List<string>();
                List<string> CloseD = new List<string>();
                List<string> CloseT = new List<string>();
                List<string> Appr = new List<string>();
                List<string> Tech = new List<string>();

                loopCount = 0;
                richTextBox2.Text = richTextBox2.Text + "\t\tEstimates Already Approved / Declined / Pending:\n";
                richTextBox2.Text = richTextBox2.Text + "Claim #     Date        Time        Who   SSN   Status    Tech\n\n";
                while (!reader.EndOfStream)
                {
                    var lineRead = reader.ReadLine();
                    var values = lineRead.Split(',');

                    claim.Add(values[0]);      //  Claim #         Claim Number
                    date.Add(values[1]);       //  Date            Date
                    time.Add(values[2]);       //  Time            Time
                    CloseD.Add(values[3]);     //  Who Approved
                    CloseT.Add(values[4]);     //  SSN
                    Appr.Add(values[5]);       //  Approved 
                    Tech.Add(values[6]);       //  Technician

                    var cclaim = claim[loopCount];
                    var cdate = date[loopCount];
                    var ctime = time[loopCount];
                    var cWho = CloseD[loopCount];
                    var cSsn = CloseT[loopCount];
                    var cAppr = Appr[loopCount];
                    var cTech = Tech[loopCount];

                    if (cAppr.Contains("A"))
                    {
                        richTextBox2.Text = richTextBox2.Text + cclaim + "\t" + cdate + "\t" + ctime + "\t" + cWho + "\t" + cSsn + "\t" + "Approved" + "\t" + cTech + "\n";
                    }
                    if (cAppr.Contains("_"))
                    {
                        richTextBox2.Text = richTextBox2.Text + cclaim + "\t" + cdate + "\t" + ctime + "\t" + cWho + "\t" + cSsn + "\t" + "Declined" + "\t" + cTech + "\n";
                    }
                    if (cAppr.Contains("P"))
                    {
                        richTextBox2.Text = richTextBox2.Text + cclaim + "\t" + cdate + "\t" + ctime + "\t" + cWho + "\t" + cSsn + "\t" + "Pending" + "\t" + cTech + "\n";
                    }

                    loopCount++;
                }
            reader.Close(); // Close the open file
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: (Line 297) \n" + ex);
            }
        }
    
    public void GetDatabaseInfo()
        {
            try
            {
                zClaim_NO = "";
                zDate_IN = "";
                zWar_Note = "";
                zBench = "";
                zWHLoc = "";
                zTheTech = "";
                zIsWarr = "";
                zEstimate = "";

                StreamReader reader = new StreamReader(database, Encoding.GetEncoding("Windows-1252"));
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
                richTextBox1.Text = richTextBox1.Text + "\t\t\tEstimate Collections Report\n\n";
                richTextBox1.Text = richTextBox1.Text + "Claim #     Repairs     Parts       Phone             Name\n";
                while (!reader.EndOfStream)
                {
                    var lineRead = reader.ReadLine();
                    var values = lineRead.Split(',');

                    listA.Add(values[0]);       //  war_prd         Estimate Approved / Customer Notified
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
                    listBX.Add(values[75]);     //  Rush Y or N

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
                    var mWar_Note = listAT[loopCount];
                    var mTS1 = listAU[loopCount];
                    var mTS2 = listAV[loopCount];
                    var mTS3 = listAW[loopCount];
                    var mts4 = listAX[loopCount];
                    var mTech = listBC[loopCount];
                    var mBench = listBD[loopCount];
                    var mWHLoc = listBE[loopCount];
                    var mTheTech = listAZ[loopCount];
                    var mTheNewClaimNum = listBQ[loopCount];
                    var mIsWarr = listBL[loopCount];
                    var mEmail = listBT[loopCount];
                    var mEstimate = listBU[loopCount];
                    var mRush = listBX[loopCount];

                    

                    if (listB[loopCount] == SelectedText)
                    {
                        zClaim_NO = listB[loopCount];
                        zDate_IN = listC[loopCount];
                        zWar_Note = listAT[loopCount];
                        zBench = listBD[loopCount];
                        zWHLoc = listBE[loopCount];
                        zTheTech = listAZ[loopCount];
                        zIsWarr = listBL[loopCount];
                        zEstimate = listBU[loopCount];
                        zRush = listBX[loopCount];

                        TheTechis = listAZ[loopCount];
                        Version.TECH = listAZ[loopCount];
                    }

                    if (listBU[loopCount] == "Y" || listBU[loopCount] == "P")
                    {
                        BU = listBU[loopCount];
                        var flName = listD[loopCount] + " " + listE[loopCount];
                        var eTotal = decimal.Parse(listBV[loopCount]).ToString("C2");
                        if (eTotal.Length == 5)
                        {
                            eTotal = "  " + eTotal;
                        }
                        if (eTotal.Length == 6)
                        {
                            eTotal = " " + eTotal;
                        }
                        var ePartsCost = decimal.Parse(listBW[loopCount]).ToString("C2");
                        if (ePartsCost.Length <= 5)
                        {
                            ePartsCost = "  " + ePartsCost;
                        }
                        if (ePartsCost.Length == 6)
                        {
                            ePartsCost = " " + ePartsCost;
                        }
                        mTotal = mTotal + decimal.Parse(listBW[loopCount]);
                        ePartsTotal = ePartsTotal + decimal.Parse(listBV[loopCount]);
                        var tTotal = decimal.Parse(listBV[loopCount]);
                        if (tTotal >= 0m)
                        {
                            if (eTotal.Length <= 6)
                            {
                                if (listJ[loopCount] == "000-000-0000")
                                {
                                    richTextBox1.Text = richTextBox1.Text + listB[loopCount] + "\t" + eTotal + "\t\t" + ePartsCost + "\t" + listK[loopCount] + "\t" + flName + "\n";

                                }
                                else
                                {
                                    richTextBox1.Text = richTextBox1.Text + listB[loopCount] + "\t" + eTotal + "\t\t" + ePartsCost + "\t" + listJ[loopCount] + "\t" + flName + "\n";

                                }

                            }
                            else
                            {
                                richTextBox1.Text = richTextBox1.Text + listB[loopCount] + "\t" + eTotal + "\t" + ePartsCost + "\t" + listJ[loopCount] + "\t" + flName + "\n";

                            }
                        }
                        loop++;
                    }
                    loopCount++;
                }
                reader.Close(); // Close the open file
                label1.Text = "Found: " + loop.ToString();
                label3.Text = "Parts Total:  " + mTotal.ToString("C2");
                label9.Text = "Claim Total: " + ePartsTotal.ToString("C2");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 520: Sorry an error has occured: " + ex.Message);
            }
        }
    }
}

    

