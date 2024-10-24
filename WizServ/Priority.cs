using System;
using System.IO;
using System.Resources;
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
    public partial class Priority : Form
    {
        public string claimno, Mex;
        public Icon image100 = Properties.Resources.WizServ;
        private int loopCount;
        public bool SetIt, PriorityClaim, RushClaim, PauseDBAccess;
        private static readonly string Database = @"I:\\Datafile\\Control\\Database.CSV";
        private static readonly string TechAssign = @"I:\\Datafile\\Control\\Tech_Assign.CSV";
        private static readonly string TechAssign2 = @"I:\\Datafile\\Control\\Tech_Assign2.CSV";
        private static readonly string TechAssign3 = @"I:\\Datafile\\Control\\Tech_Assign3.CSV";

        public Priority()
        {
            InitializeComponent();
            PauseDBAccess = Version.PauseDBAccess;
            Icon = image100;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            button5.Visible = false;
            textBox1.Select();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PauseDBAccess = true;
                Version.PauseDBAccess = true;
                button2.Visible = true;
                button3.Visible = true;
                button4.Visible = true;
                button5.Visible = true;
                claimno = textBox1.Text;
                GetData();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            PauseDBAccess = false;
            Version.PauseDBAccess = false;
            CopyTechAssign3();
            MainUtilitiesMenu f0 = new MainUtilitiesMenu();
            f0.Show();
        }

        private void CopyTechAssign3()
        {
            if (Version.PauseDBAccess == true)
            {
                Thread.Sleep(5000);
            }
            string sourceFile = TechAssign;
            string destinationFile = TechAssign3;
            try
            {
                File.Copy(sourceFile, destinationFile, true);
            }
            catch (IOException iox)
            {
                MessageBox.Show("Error 81 occured during copy\n " + iox.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SetIt = false;
            PriorityClaim = true;
            ChangeDB();
            ChangeTechAssign();
            ChangeTechAssign2();
            GetData();
            label5.Text = "Set to Priority Claim";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SetIt = true;
            RushClaim = true;
            ChangeDBRush();
            ChangeTechAssign();
            ChangeTechAssign2();
            GetData();
            label5.Text = "Set to Rush Claim";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SetIt = false;
            PriorityClaim = false;
            ChangeDB();
            ChangeTechAssign();
            ChangeTechAssign2();
            GetData();
            label5.Text = "Set to Non-Priority Claim";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            RushClaim = false;
            SetIt = true;
            ChangeDBRush();
            ChangeTechAssign();
            ChangeTechAssign2();
            GetData();
            label5.Text = "Set to Non-Rush Claim";

        }

        private void ChangeDB()
        {
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

                            if (split[1].Contains(claimno))
                            {
                                if (PriorityClaim == true)
                                {
                                    split[32] = "PREF";
                                    split[58] = "P";
                                    if (SetIt == true)
                                    {
                                        split[75] = "Y";
                                    }
                                    line = String.Join(",", split);
                                }
                                if (PriorityClaim == false)
                                {
                                    split[32] = ".";
                                    split[58] = ".";
                                    if (SetIt == true)
                                    {
                                        split[75] = "N";
                                    }
                                    line = String.Join(",", split);
                                }
                            }
                        }

                        lines.Add(line);
                    }
                }

                using (StreamWriter writer = new StreamWriter(Database, false))
                {
                    foreach (String line in lines)
                        writer.WriteLine(line);
                }
            }
        }

        private void ChangeDBRush()
        {
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

                            if (split[1].Contains(claimno))
                            {
                                if (RushClaim == true)
                                {
                                    split[32] = "PREF";
                                    split[58] = "R";
                                    split[75] = "Y";
                                    if (SetIt == true)
                                    {
                                        split[75] = "Y";
                                    }
                                    line = String.Join(",", split);
                                }
                                if (RushClaim == false)
                                {
                                    split[32] = ".";
                                    split[58] = "N";
                                    split[75] = "N";
                                    if (SetIt == true)
                                    {
                                        split[75] = "Y";
                                    }
                                    line = String.Join(",", split);
                                }
                            }
                        }

                        lines.Add(line);
                    }
                }

                using (StreamWriter writer = new StreamWriter(Database, false))
                {
                    foreach (String line in lines)
                        writer.WriteLine(line);
                }
            }
        }

        private void ChangeTechAssign()
        {
            List<String> lines = new List<String>();

            if (File.Exists(TechAssign))
            {
                using (StreamReader reader = new StreamReader(TechAssign))
                {
                    String line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Contains(","))
                        {
                            String[] split = line.Split(',');

                            if (split[1].Contains(claimno))
                            {
                                if (PriorityClaim == true)
                                {
                                    split[8] = "Y";
                                    line = String.Join(",", split);
                                }
                                if (PriorityClaim == false)
                                {
                                    split[8] = "N";
                                    line = String.Join(",", split);
                                }
                            }
                        }

                        lines.Add(line);
                    }
                }

                using (StreamWriter writer = new StreamWriter(TechAssign, false))
                {
                    foreach (String line in lines)
                        writer.WriteLine(line);
                }
            }
        }

        private void ChangeTechAssign2()
        {
            List<String> lines = new List<String>();

            if (File.Exists(TechAssign))
            {
                using (StreamReader reader = new StreamReader(TechAssign2))
                {
                    String line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Contains(","))
                        {
                            String[] split = line.Split(',');

                            if (split[1].Contains(claimno))
                            {
                                if (PriorityClaim == true)
                                {
                                    split[8] = "Y";
                                    line = String.Join(",", split);
                                }
                                if (PriorityClaim == false)
                                {
                                    split[8] = "N";
                                    line = String.Join(",", split);
                                }
                            }
                        }

                        lines.Add(line);
                    }
                }

                using (StreamWriter writer = new StreamWriter(TechAssign2, false))
                {
                    foreach (String line in lines)
                        writer.WriteLine(line);
                }
            }
        }

        public void GetData()
        {
            SetIt = false;
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

                    if (listB[loopCount] == claimno)
                    {
                        var mClaim_NO = listB[loopCount];

                        if (listBX[loopCount] == "N")
                        {
                            label2.Text = "Rush    : Not a Rush Claim";
                        }
                        else
                        {
                            label2.BackColor = Color.White;
                            label2.ForeColor = Color.DarkBlue;
                            label2.Text = "Rush    : Is now a Rush Claim";
                        }
                        if (listAG[loopCount] == ".")
                        {
                            label3.Text = "ACCESS : Not Priority";
                        }
                        else
                        {
                            label3.BackColor = Color.White;
                            label3.ForeColor = Color.DarkBlue;
                            label3.Text = " ACCESS : Is now a Priority or Rush Claim ";
                        }
                        if (listBG[loopCount] == ".")
                        {
                            label4.Text = "NARDA : Not Priority";
                        }
                        else
                        {
                            label4.BackColor = Color.White;
                            label4.ForeColor = Color.DarkBlue;
                            label4.Text = " NARDA : Is now a Priority or Rush Claim ";
                        }

                        loopCount++;
                    }
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
                if (Mex.Contains("StackOverflowException"))
                {
                    MessageBox.Show("StackOverflowException");
                }

                MessageBox.Show("Error 1329: Sorry an error has occured: " + ex.Message);
            }
        }
    }
}
