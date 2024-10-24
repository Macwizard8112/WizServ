using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WizServ
{
    public partial class DailyReportByTech : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        public string claim_no = Version.Claim;
        private readonly string file = @"I:\\Datafile\\Control\\Database.CSV";
        private string fname, lname, addr, city, state, zip, hphone, wphone, Lines;
        private bool war_prd;
        private DateTime datein;
        private int loopCount, loop, zBench, zService, zPartsOrd, zAssign, zCheck, zCompleted, zPartsBack, zSent, zWait;
        public string calledfrom;
        public string lab, TodaysDate, dt1, dt2, dt3, dt4, dt5, dt6, dt8, dt9, DateWOZeros;
        public int mtotal, dt7;
        private int T1a, T1b, T1c, T1d, T1e, T1f, T1g, T1h, T1i, T1j, T1k, T1l;
        private int T11a, T11b, T11c, T11d, T11e, T11f, T11g, T11h, T11i, T11j, T11k, T11l;
        private int T2a, T2b, T2c, T2d, T2e, T2f, T2g, T2h, T2i, T2j, T2k, T2l;
        private int T22a, T22b, T22c, T22d, T22e, T22f, T22g, T22h, T22i, T22j, T22k, T22l;
        private int T3a, T3b, T3c, T3d, T3e, T3f, T3g, T3h, T3i, T3j, T3k, T3l;
        private int T33a, T33b, T33c, T33d, T33e, T33f, T33g, T33h, T33i, T33j, T33k, T33l;
        private int T4a, T4b, T4c, T4d, T4e, T4f, T4g, T4h, T4i, T4j, T4k, T4l;
        private int T44a, T44b, T44c, T44d, T44e, T44f, T44g, T44h, T44i, T44j, T44k, T44l;
        private int T5a, T5b, T5c, T5d, T5e, T5f, T5g, T5h, T5i, T5j, T5k, T5l;
        private int T55a, T55b, T55c, T55d, T55e, T55f, T55g, T55h, T55i, T55j, T55k, T55l;
        private int T6a, T6b, T6c, T6d, T6e, T6f, T6g, T6h, T6i, T6j, T6k, T6l;
        private int T66a, T66b, T66c, T66d, T66e, T66f, T66g, T66h, T66i, T66j, T66k, T66l;
        private int linesPrinted;
        private string[] lines;
        private string tScreen;

        public DailyReportByTech()
        {
            InitializeComponent();
            SetToZero();
            GetTodaysDate();
            Icon = image100;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
            GetData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Length > 0)
            {
                richTextBox1.SelectAll();
                tScreen = richTextBox1.Text;
                DateTime date = DateTime.Now;
                var tMessage = TodaysDate + "\t" + label1.Text + "\nClaims by Technician\n" + "All claims in database:" + "\n\n";
                var tMsg = TodaysDate + " Todays\n" + "Claims in database" + ":\n\n";
                var tHeader = "   \n";
                richTextBox14.Text = "";
                richTextBox14.Text = tMessage + tHeader + tScreen + "\n\n" + tMsg + richTextBox7.Text;
                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.Print();
                }
            }
            else
            {
                if (richTextBox14.Text.Length <= 0)
                {
                    richTextBox14.Text = "\nClick buttons above to list / Print first.";
                    return;
                }
            }
            //richTextBox1.Text = "";
            richTextBox14.Text = "";
            //richTextBox1.Text = tScreen;
        }

        private void printDocument1_BeginPrint(object sender, PrintEventArgs e)
        {
            printDocument1.DefaultPageSettings.Landscape = false;
            char[] param = { '\n' };

            if (printDialog1.PrinterSettings.PrintRange == PrintRange.Selection)
            {
                lines = richTextBox14.SelectedText.Split(param);
            }
            else
            {
                lines = richTextBox14.Text.Split(param);
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
            printDocument1.DefaultPageSettings.Landscape = false;
            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;
            Brush brush = new SolidBrush(richTextBox14.ForeColor);

            while (linesPrinted < lines.Length)
            {
                e.Graphics.DrawString(lines[linesPrinted++],
                    richTextBox14.Font, brush, x, y);
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

        private void button4_Click(object sender, EventArgs e)
        {
            if (richTextBox2.Text.Length > 0)
            {
                richTextBox2.SelectAll();
                tScreen = richTextBox2.Text;
                DateTime date = DateTime.Now;
                var tMessage = TodaysDate + "\t" + label2.Text + "\nClaims by Technician\n" + "All claims in database:" + "\n\n";
                var tMsg = TodaysDate + " Todays\n" + "Claims in database" + ":\n\n";
                var tHeader = "   \n";
                richTextBox14.Text = "";
                richTextBox14.Text = tMessage + tHeader + tScreen + "\n\n" + tMsg + richTextBox8.Text;
                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.Print();
                }
            }
            else
            {
                if (richTextBox14.Text.Length <= 0)
                {
                    richTextBox14.Text = "\nClick buttons above to list / Print first.";
                    return;
                }
            }
            richTextBox14.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (richTextBox3.Text.Length > 0)
            {
                richTextBox3.SelectAll();
                tScreen = richTextBox3.Text;
                DateTime date = DateTime.Now;
                var tMessage = TodaysDate + "\t" + label3.Text + "\nClaims by Technician\n" + "All claims in database:" + "\n\n";
                var tMsg = TodaysDate + " Todays\n" + "Claims in database" + ":\n\n";
                var tHeader = "   \n";
                richTextBox14.Text = "";
                richTextBox14.Text = tMessage + tHeader + tScreen + "\n\n" + tMsg + richTextBox9.Text;
                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.Print();
                }
            }
            else
            {
                if (richTextBox14.Text.Length <= 0)
                {
                    richTextBox14.Text = "\nClick buttons above to list / Print first.";
                    return;
                }
            }
            richTextBox14.Text = "";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (richTextBox4.Text.Length > 0)
            {
                richTextBox4.SelectAll();
                tScreen = richTextBox4.Text;
                DateTime date = DateTime.Now;
                var tMessage = TodaysDate + "\t" + label4.Text + "\nClaims by Technician\n" + "All claims in database:" + "\n\n";
                var tMsg = TodaysDate + " Todays\n" + "Claims in database" + ":\n\n";
                var tHeader = "   \n";
                richTextBox14.Text = "";
                richTextBox14.Text = tMessage + tHeader + tScreen + "\n\n" + tMsg + richTextBox10.Text;
                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.Print();
                }
            }
            else
            {
                if (richTextBox14.Text.Length <= 0)
                {
                    richTextBox14.Text = "\nClick buttons above to list / Print first.";
                    return;
                }
            }
            richTextBox14.Text = "";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (richTextBox5.Text.Length > 0)
            {
                richTextBox5.SelectAll();
                tScreen = richTextBox5.Text;
                DateTime date = DateTime.Now;
                var tMessage = TodaysDate + "\t" + label5.Text + "\nClaims by Technician\n" + "All claims in database:" + "\n\n";
                var tMsg = TodaysDate + " Todays\n" + "Claims in database" + ":\n\n";
                var tHeader = "   \n";
                richTextBox14.Text = "";
                richTextBox14.Text = tMessage + tHeader + tScreen + "\n\n" + tMsg + richTextBox11.Text;
                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.Print();
                }
            }
            else
            {
                if (richTextBox14.Text.Length <= 0)
                {
                    richTextBox14.Text = "\nClick buttons above to list / Print first.";
                    return;
                }
            }
            richTextBox14.Text = "";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (richTextBox6.Text.Length > 0)
            {
                richTextBox6.SelectAll();
                tScreen = richTextBox6.Text;
                DateTime date = DateTime.Now;
                var tMessage = TodaysDate + "\t" + label6.Text + "\nClaims by Technician\n" + "All claims in database:" + "\n\n";
                var tMsg = TodaysDate + " Todays\n" + "Claims in database" + ":\n\n";
                var tHeader = "   \n";
                richTextBox14.Text = "";
                richTextBox14.Text = tMessage + tHeader + tScreen + "\n\n" + tMsg + richTextBox12.Text;
                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.Print();
                }
            }
            else
            {
                if (richTextBox14.Text.Length <= 0)
                {
                    richTextBox14.Text = "\nClick buttons above to list / Print first.";
                    return;
                }
            }
            richTextBox14.Text = "";
        }

        public void GetTodaysDate()
        {
            try
            {
                // Generate todays date
                dt2 = DateTime.Now.Month.ToString("00");    // Month as 2 digits
                if (int.Parse(dt2) <= 9)
                {
                    dt1 = DateTime.Now.Month.ToString("0");
                }
                else
                {
                    dt1 = DateTime.Now.Month.ToString("00");
                }
                dt4 = DateTime.Now.Day.ToString("00");      // Day as 2 digits
                if (int.Parse(dt4) <= 9)
                {
                    dt5 = DateTime.Now.Day.ToString("0");
                }
                else
                {
                    dt5 = DateTime.Now.Day.ToString("00");
                }
                dt8 = DateTime.Now.Year.ToString("0000");   // Year as 4 digits
                dt3 = dt1 + "/" + dt5 + "/" + dt8;
                DateWOZeros = dt3;
                dt9 = dt2 + "/" + dt4 + "/" + dt8;          // Add back in seperators "/"
                TodaysDate = dt9;                           // Store Todays Date in Public variable
                // End generate todays date
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 79, Error\n" + ex);
            }
        }

        private void SetToZero()
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            OpenClaimsMenu f2 = new OpenClaimsMenu();
            f2.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }

        public void GetData()
        {
            zBench = 0;
            zService = 0;
            zPartsOrd = 0;
            zAssign = 0;
            zCheck = 0;
            zCompleted = 0;
            zPartsBack = 0;
            zSent = 0;
            zWait = 0;

            try
            {
                StreamReader reader = new StreamReader(file);
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

                    var Tech1NAME = listAZ[loopCount];

                    if (Tech1NAME == "COLE")
                    {
                        if (listBD[loopCount].StartsWith("BENCH ON"))
                        {
                            T1a++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T11a++;
                            }
                        }
                        if (listBD[loopCount].StartsWith("CHECKING"))
                        {
                            T1b++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T11b++;
                            }
                        }
                        if (listBD[loopCount].StartsWith("COMPLETED"))
                        {
                            T1c++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T11c++;
                            }
                        }
                        if (listBD[loopCount].StartsWith("MISC."))
                        {
                            T1d++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T11d++;
                            }
                        }
                        if (listBD[loopCount].StartsWith("PARTS ARE BACK"))
                        {
                            T1e++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T11e++;
                            }
                        }
                        if (listBD[loopCount].StartsWith("PARTS ORDERED BY"))
                        {
                            T1f++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T11f++;
                            }
                        }
                        if (listBD[loopCount].StartsWith("PARTS ORDERED ON"))
                        {
                            T1g++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T11g++;
                            }
                        }
                        if (listBD[loopCount].StartsWith("SENT TO"))
                        {
                            T1h++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T11h++;
                            }
                        }
                        if (listBD[loopCount].StartsWith("SERVICE RENDER"))
                        {
                            T1i++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T11i++;
                            }
                        }
                        if (listBD[loopCount].StartsWith("WAITING EST"))
                        {
                            T1j++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T11j++;
                            }
                        }
                        if (listBD[loopCount].Contains(DateWOZeros))
                        {
                            T1k++;
                        }
                        if (listBD[loopCount].Contains(TodaysDate))
                        {
                            T1k++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T11k++;
                            }
                        }


                    }
                    if (Tech1NAME == "WILLIAM")
                    {
                        if (listBD[loopCount].StartsWith("BENCH ON"))
                        {
                            T2a++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T22a++;
                            }
                        }
                        if (listBD[loopCount].StartsWith("CHECKING"))
                        {
                            T2b++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T22b++;
                            }
                        }
                        if (listBD[loopCount].StartsWith("COMPLETED"))
                        {
                            T2c++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T22c++;
                            }
                        }
                        if (listBD[loopCount].StartsWith("MISC."))
                        {
                            T2d++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T22d++;
                            }
                        }
                        if (listBD[loopCount].StartsWith("PARTS ARE BACK"))
                        {
                            T2e++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T22e++;
                            }
                        }
                        if (listBD[loopCount].StartsWith("PARTS ORDERED BY"))
                        {
                            T2f++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T22f++;
                            }
                        }
                        if (listBD[loopCount].StartsWith("PARTS ORDERED ON"))
                        {
                            T2g++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T22g++;
                            }
                        }
                        if (listBD[loopCount].StartsWith("SENT TO"))
                        {
                            T2h++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T22h++;
                            }
                        }
                        if (listBD[loopCount].StartsWith("SERVICE RENDER"))
                        {
                            T2i++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T22i++;
                            }
                        }
                        if (listBD[loopCount].StartsWith("WAITING EST"))
                        {
                            T2j++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T22j++;
                            }
                        }
                        if (listBD[loopCount].Contains(DateWOZeros))
                        {
                            T2k++;
                        }
                        if (listBD[loopCount].Contains(TodaysDate))
                        {
                            T2k++;
                        }
                    }
                    if (Tech1NAME == "WALTER")
                    {
                        if (listBD[loopCount].StartsWith("BENCH ON"))
                        {
                            T3a++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T33a++;
                            }
                        }
                        if (listBD[loopCount].StartsWith("CHECKING"))
                        {
                            T3b++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T33b++;
                            }
                        }
                        if (listBD[loopCount].StartsWith("COMPLETED"))
                        {
                            T3c++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T33c++;
                            }
                        }
                        if (listBD[loopCount].StartsWith("MISC."))
                        {
                            T3d++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T33d++;
                            }
                        }
                        if (listBD[loopCount].StartsWith("PARTS ARE BACK"))
                        {
                            T3e++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T33e++;
                            }
                        }
                        if (listBD[loopCount].StartsWith("PARTS ORDERED BY"))
                        {
                            T3f++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T33f++;
                            }
                        }
                        if (listBD[loopCount].StartsWith("PARTS ORDERED ON"))
                        {
                            T3g++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T33g++;
                            }
                        }
                        if (listBD[loopCount].StartsWith("SENT TO"))
                        {
                            T3h++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T33h++;
                            }
                        }
                        if (listBD[loopCount].StartsWith("SERVICE RENDER"))
                        {
                            T3i++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T33i++;
                            }
                        }
                        if (listBD[loopCount].StartsWith("WAITING EST"))
                        {
                            T3j++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T33j++;
                            }
                        }
                        if (listBD[loopCount].Contains(DateWOZeros))
                        {
                            T3k++;
                        }
                        if (listBD[loopCount].Contains(TodaysDate))
                        {
                            T3k++;
                        }
                    }
                    if (Tech1NAME == "DEREK")
                    {
                        if (listBD[loopCount].StartsWith("BENCH ON"))
                        {
                            T4a++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T44a++;
                            }
                        }
                        if (listBD[loopCount].StartsWith("CHECKING"))
                        {
                            T4b++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T44b++;
                            }
                        }
                        if (listBD[loopCount].StartsWith("COMPLETED"))
                        {
                            T4c++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T44c++;
                            }
                        }
                        if (listBD[loopCount].StartsWith("MISC."))
                        {
                            T4d++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T44d++;
                            }
                        }
                        if (listBD[loopCount].StartsWith("PARTS ARE BACK"))
                        {
                            T4e++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T44e++;
                            }
                        }
                        if (listBD[loopCount].StartsWith("PARTS ORDERED BY"))
                        {
                            T4f++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T44f++;
                            }
                        }
                        if (listBD[loopCount].StartsWith("PARTS ORDERED ON"))
                        {
                            T4g++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T44g++;
                            }
                        }
                        if (listBD[loopCount].StartsWith("SENT TO"))
                        {
                            T4h++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T44h++;
                            }
                        }
                        if (listBD[loopCount].StartsWith("SERVICE RENDER"))
                        {
                            T4i++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T44i++;
                            }
                        }
                        if (listBD[loopCount].StartsWith("WAITING EST"))
                        {
                            T4j++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T44j++;
                            }
                        }
                        if (listBD[loopCount].Contains(DateWOZeros))
                        {
                            T4k++;
                        }
                        if (listBD[loopCount].Contains(TodaysDate))
                        {
                            T4k++;
                        }
                    }
                    if (Tech1NAME == "BILLY")
                    {
                        if (listBD[loopCount].StartsWith("BENCH ON"))
                        {
                            T5a++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T55a++;
                            }
                        }
                        if (listBD[loopCount].StartsWith("CHECKING"))
                        {
                            T5b++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T55b++;
                            }
                        }
                        if (listBD[loopCount].StartsWith("COMPLETED"))
                        {
                            T5c++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T55c++;
                            }
                        }
                        if (listBD[loopCount].StartsWith("MISC."))
                        {
                            T5d++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T55d++;
                            }
                        }
                        if (listBD[loopCount].StartsWith("PARTS ARE BACK"))
                        {
                            T5e++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T55e++;
                            }
                        }
                        if (listBD[loopCount].StartsWith("PARTS ORDERED BY"))
                        {
                            T5f++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T55f++;
                            }
                        }
                        if (listBD[loopCount].StartsWith("PARTS ORDERED ON"))
                        {
                            T5g++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T55g++;
                            }
                        }
                        if (listBD[loopCount].StartsWith("SENT TO"))
                        {
                            T5h++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T55h++;
                            }
                        }
                        if (listBD[loopCount].StartsWith("SERVICE RENDER"))
                        {
                            T5i++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T55i++;
                            }
                        }
                        if (listBD[loopCount].StartsWith("WAITING EST"))
                        {
                            T5j++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T55j++;
                            }
                        }
                        if (listBD[loopCount].Contains(DateWOZeros))
                        {
                            T5k++;
                        }
                        if (listBD[loopCount].Contains(TodaysDate))
                        {
                            T5k++;
                        }
                    }
                    if (Tech1NAME == "NOEL")
                    {
                        if (listBD[loopCount].StartsWith("BENCH ON"))
                        {
                            T6a++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T66a++;
                            }
                        }
                        if (listBD[loopCount].StartsWith("CHECKING"))
                        {
                            T6b++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T66b++;
                            }
                        }
                        if (listBD[loopCount].StartsWith("COMPLETED"))
                        {
                            T6c++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T66c++;
                            }
                        }
                        if (listBD[loopCount].StartsWith("MISC."))
                        {
                            T6d++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T66d++;
                            }
                        }
                        if (listBD[loopCount].StartsWith("PARTS ARE BACK"))
                        {
                            T6e++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T66e++;
                            }
                        }
                        if (listBD[loopCount].StartsWith("PARTS ORDERED BY"))
                        {
                            T6f++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T66f++;
                            }
                        }
                        if (listBD[loopCount].StartsWith("PARTS ORDERED ON"))
                        {
                            T6g++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T66g++;
                            }
                        }
                        if (listBD[loopCount].StartsWith("SENT TO"))
                        {
                            T6h++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T66h++;
                            }
                        }
                        if (listBD[loopCount].StartsWith("SERVICE RENDER"))
                        {
                            T6i++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T66i++;
                            }
                        }
                        if (listBD[loopCount].StartsWith("WAITING EST"))
                        {
                            T6j++;
                            if (listBD[loopCount].Contains(TodaysDate))
                            {
                                T66j++;
                            }
                        }
                        if (listBD[loopCount].Contains(DateWOZeros))
                        {
                            T6k++;
                        }
                        if (listBD[loopCount].Contains(TodaysDate))
                        {
                            T6k++;
                        }
                    }

                    loopCount++;
                }
                reader.Close(); // Close the open file
                richTextBox1.Text += "Bench on Bench :   " + T1a.ToString() + "\n";
                richTextBox1.Text += "Checking Parts :   " + T1b.ToString() + "\n";
                richTextBox1.Text += "Completed      :   " + T1c.ToString() + "\n";
                richTextBox1.Text += "Misc. Decription:  " + T1d.ToString() + "\n";
                richTextBox1.Text += "Parts Backordered: " + T1e.ToString() + "\n";
                richTextBox1.Text += "Parts Ord By Desc: " + T1f.ToString() + "\n";
                richTextBox1.Text += "Parts Ord By PO:   " + T1g.ToString() + "\n";
                richTextBox1.Text += "Sent to Vendor:    " + T1h.ToString() + "\n";
                richTextBox1.Text += "Service Redered:   " + T1i.ToString() + "\n";
                richTextBox1.Text += "Waiting Estimate:  " + T1j.ToString() + "\n\n";
                richTextBox1.Text += "Today:             " + T1k.ToString() + "\n";

                richTextBox7.Text += "Bench on Bench :   " + T11a.ToString() + "\n";
                richTextBox7.Text += "Checking Parts :   " + T11b.ToString() + "\n";
                richTextBox7.Text += "Completed      :   " + T11c.ToString() + "\n";
                richTextBox7.Text += "Misc. Decription:  " + T11d.ToString() + "\n";
                richTextBox7.Text += "Parts Backordered: " + T11e.ToString() + "\n";
                richTextBox7.Text += "Parts Ord By Desc: " + T11f.ToString() + "\n";
                richTextBox7.Text += "Parts Ord By PO:   " + T11g.ToString() + "\n";
                richTextBox7.Text += "Sent to Vendor:    " + T11h.ToString() + "\n";
                richTextBox7.Text += "Service Redered:   " + T11i.ToString() + "\n";
                richTextBox7.Text += "Waiting Estimate:  " + T11j.ToString() + "\n\n";
                var tech1Total = T11a + T11b + T11c + T11d + T11e + T11f + T11g + T11h + T11i + T11j;
                richTextBox7.Text += "Today:             " + tech1Total.ToString() + "\n";

                richTextBox2.Text += "Bench on Bench:    " + T2a.ToString() + "\n";
                richTextBox2.Text += "Checking Parts :   " + T2b.ToString() + "\n";
                richTextBox2.Text += "Completed      :   " + T2c.ToString() + "\n";
                richTextBox2.Text += "Misc. Decription:  " + T2d.ToString() + "\n";
                richTextBox2.Text += "Parts Backordered: " + T2e.ToString() + "\n";
                richTextBox2.Text += "Parts Ord By Desc: " + T2f.ToString() + "\n";
                richTextBox2.Text += "Parts Ord By PO:   " + T2g.ToString() + "\n";
                richTextBox2.Text += "Sent to Vendor:    " + T2h.ToString() + "\n";
                richTextBox2.Text += "Service Redered:   " + T2i.ToString() + "\n";
                richTextBox2.Text += "Waiting Estimate:  " + T2j.ToString() + "\n\n";
                richTextBox2.Text += "Today:             " + T2k.ToString() + "\n";

                richTextBox8.Text += "Bench on Bench :   " + T22a.ToString() + "\n";
                richTextBox8.Text += "Checking Parts :   " + T22b.ToString() + "\n";
                richTextBox8.Text += "Completed      :   " + T22c.ToString() + "\n";
                richTextBox8.Text += "Misc. Decription:  " + T22d.ToString() + "\n";
                richTextBox8.Text += "Parts Backordered: " + T22e.ToString() + "\n";
                richTextBox8.Text += "Parts Ord By Desc: " + T22f.ToString() + "\n";
                richTextBox8.Text += "Parts Ord By PO:   " + T22g.ToString() + "\n";
                richTextBox8.Text += "Sent to Vendor:    " + T22h.ToString() + "\n";
                richTextBox8.Text += "Service Redered:   " + T22i.ToString() + "\n";
                richTextBox8.Text += "Waiting Estimate:  " + T22j.ToString() + "\n\n";
                var tech2Total = T22a + T22b + T22c + T22d + T22e + T22f + T22g + T22h + T22i + T22j;
                richTextBox8.Text += "Today:             " + tech2Total.ToString() + "\n";

                richTextBox3.Text += "Bench on Bench:    " + T3a.ToString() + "\n";
                richTextBox3.Text += "Checking Parts :   " + T3b.ToString() + "\n";
                richTextBox3.Text += "Completed      :   " + T3c.ToString() + "\n";
                richTextBox3.Text += "Misc. Decription:  " + T3d.ToString() + "\n";
                richTextBox3.Text += "Parts Backordered: " + T3e.ToString() + "\n";
                richTextBox3.Text += "Parts Ord By Desc: " + T3f.ToString() + "\n";
                richTextBox3.Text += "Parts Ord By PO:   " + T3g.ToString() + "\n";
                richTextBox3.Text += "Sent to Vendor:    " + T3h.ToString() + "\n";
                richTextBox3.Text += "Service Redered:   " + T3i.ToString() + "\n";
                richTextBox3.Text += "Waiting Estimate:  " + T3j.ToString() + "\n\n";
                richTextBox3.Text += "Today:             " + T3k.ToString() + "\n";

                richTextBox9.Text += "Bench on Bench :   " + T33a.ToString() + "\n";
                richTextBox9.Text += "Checking Parts :   " + T33b.ToString() + "\n";
                richTextBox9.Text += "Completed      :   " + T33c.ToString() + "\n";
                richTextBox9.Text += "Misc. Decription:  " + T33d.ToString() + "\n";
                richTextBox9.Text += "Parts Backordered: " + T33e.ToString() + "\n";
                richTextBox9.Text += "Parts Ord By Desc: " + T33f.ToString() + "\n";
                richTextBox9.Text += "Parts Ord By PO:   " + T33g.ToString() + "\n";
                richTextBox9.Text += "Sent to Vendor:    " + T33h.ToString() + "\n";
                richTextBox9.Text += "Service Redered:   " + T33i.ToString() + "\n";
                richTextBox9.Text += "Waiting Estimate:  " + T33j.ToString() + "\n\n";
                var tech3Total = T33a + T33b + T33c + T33d + T33e + T33f + T33g + T33h + T33i + T33j;
                richTextBox9.Text += "Today:             " + tech3Total.ToString() + "\n";

                richTextBox4.Text += "Bench on Bench:    " + T4a.ToString() + "\n";
                richTextBox4.Text += "Checking Parts :   " + T4b.ToString() + "\n";
                richTextBox4.Text += "Completed      :   " + T4c.ToString() + "\n";
                richTextBox4.Text += "Misc. Decription:  " + T4d.ToString() + "\n";
                richTextBox4.Text += "Parts Backordered: " + T4e.ToString() + "\n";
                richTextBox4.Text += "Parts Ord By Desc: " + T4f.ToString() + "\n";
                richTextBox4.Text += "Parts Ord By PO:   " + T4g.ToString() + "\n";
                richTextBox4.Text += "Sent to Vendor:    " + T4h.ToString() + "\n";
                richTextBox4.Text += "Service Redered:   " + T4i.ToString() + "\n";
                richTextBox4.Text += "Waiting Estimate:  " + T4j.ToString() + "\n\n";
                richTextBox4.Text += "Today:             " + T4k.ToString() + "\n";

                richTextBox10.Text += "Bench on Bench :   " + T44a.ToString() + "\n";
                richTextBox10.Text += "Checking Parts :   " + T44b.ToString() + "\n";
                richTextBox10.Text += "Completed      :   " + T44c.ToString() + "\n";
                richTextBox10.Text += "Misc. Decription:  " + T44d.ToString() + "\n";
                richTextBox10.Text += "Parts Backordered: " + T44e.ToString() + "\n";
                richTextBox10.Text += "Parts Ord By Desc: " + T44f.ToString() + "\n";
                richTextBox10.Text += "Parts Ord By PO:   " + T44g.ToString() + "\n";
                richTextBox10.Text += "Sent to Vendor:    " + T44h.ToString() + "\n";
                richTextBox10.Text += "Service Redered:   " + T44i.ToString() + "\n";
                richTextBox10.Text += "Waiting Estimate:  " + T44j.ToString() + "\n\n";
                var tech4Total = T44a + T44b + T44c + T44d + T44e + T44f + T44g + T44h + T44i + T44j;
                richTextBox10.Text += "Today:             " + tech4Total.ToString() + "\n";

                richTextBox5.Text += "Bench on Bench:    " + T5a.ToString() + "\n";
                richTextBox5.Text += "Checking Parts :   " + T5b.ToString() + "\n";
                richTextBox5.Text += "Completed      :   " + T5c.ToString() + "\n";
                richTextBox5.Text += "Misc. Decription:  " + T3d.ToString() + "\n";
                richTextBox5.Text += "Parts Backordered: " + T5e.ToString() + "\n";
                richTextBox5.Text += "Parts Ord By Desc: " + T5f.ToString() + "\n";
                richTextBox5.Text += "Parts Ord By PO:   " + T5g.ToString() + "\n";
                richTextBox5.Text += "Sent to Vendor:    " + T5h.ToString() + "\n";
                richTextBox5.Text += "Service Redered:   " + T5i.ToString() + "\n";
                richTextBox5.Text += "Waiting Estimate:  " + T5j.ToString() + "\n\n";
                richTextBox5.Text += "Today:             " + T5k.ToString() + "\n";

                richTextBox11.Text += "Bench on Bench :   " + T55a.ToString() + "\n";
                richTextBox11.Text += "Checking Parts :   " + T55b.ToString() + "\n";
                richTextBox11.Text += "Completed      :   " + T55c.ToString() + "\n";
                richTextBox11.Text += "Misc. Decription:  " + T55d.ToString() + "\n";
                richTextBox11.Text += "Parts Backordered: " + T55e.ToString() + "\n";
                richTextBox11.Text += "Parts Ord By Desc: " + T55f.ToString() + "\n";
                richTextBox11.Text += "Parts Ord By PO:   " + T55g.ToString() + "\n";
                richTextBox11.Text += "Sent to Vendor:    " + T55h.ToString() + "\n";
                richTextBox11.Text += "Service Redered:   " + T55i.ToString() + "\n";
                richTextBox11.Text += "Waiting Estimate:  " + T55j.ToString() + "\n\n";
                var tech5Total = T55a + T55b + T55c + T55d + T55e + T55f + T55g + T55h + T55i + T55j;
                richTextBox11.Text += "Today:             " + tech5Total.ToString() + "\n";

                richTextBox6.Text += "Bench on Bench:    " + T6a.ToString() + "\n";
                richTextBox6.Text += "Checking Parts :   " + T6b.ToString() + "\n";
                richTextBox6.Text += "Completed      :   " + T6c.ToString() + "\n";
                richTextBox6.Text += "Misc. Decription:  " + T6d.ToString() + "\n";
                richTextBox6.Text += "Parts Backordered: " + T6e.ToString() + "\n";
                richTextBox6.Text += "Parts Ord By Desc: " + T6f.ToString() + "\n";
                richTextBox6.Text += "Parts Ord By PO:   " + T6g.ToString() + "\n";
                richTextBox6.Text += "Sent to Vendor:    " + T6h.ToString() + "\n";
                richTextBox6.Text += "Service Redered:   " + T6i.ToString() + "\n";
                richTextBox6.Text += "Waiting Estimate:  " + T6j.ToString() + "\n\n";
                richTextBox6.Text += "Today:             " + T6k.ToString() + "\n";

                richTextBox12.Text += "Bench on Bench:    " + T66a.ToString() + "\n";
                richTextBox12.Text += "Checking Parts :   " + T66b.ToString() + "\n";
                richTextBox12.Text += "Completed      :   " + T66c.ToString() + "\n";
                richTextBox12.Text += "Misc. Decription:  " + T66d.ToString() + "\n";
                richTextBox12.Text += "Parts Backordered: " + T66e.ToString() + "\n";
                richTextBox12.Text += "Parts Ord By Desc: " + T66f.ToString() + "\n";
                richTextBox12.Text += "Parts Ord By PO:   " + T66g.ToString() + "\n";
                richTextBox12.Text += "Sent to Vendor:    " + T66h.ToString() + "\n";
                richTextBox12.Text += "Service Redered:   " + T66i.ToString() + "\n";
                richTextBox12.Text += "Waiting Estimate:  " + T66j.ToString() + "\n\n";
                var tech6Total = T66a + T66b + T66c + T66d + T66e + T66f + T66g + T66h + T66i + T66j;
                richTextBox12.Text += "Today:             " + tech6Total.ToString() + "\n";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 1220: Sorry an error has occured: " + ex.Message);
            }
        }
    }
}
