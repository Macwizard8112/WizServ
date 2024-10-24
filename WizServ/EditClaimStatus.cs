using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WizServ
{
    public partial class EditClaimStatus : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        public string claim_no;
        private readonly string file = @"I:\\Datafile\\Control\\Database.CSV";
        public string ma, mb, mc, md, me, mf, mg, mh, mi, mj, mk, ml, mm, mn, mo, mp, mq, mr, ms, mt, mu, mv, mw, mx, my, mz;
        public string maa, mab, mac, mad, mae, maf, mag, mah, mai, maj, mak, mal, mam, man, mao, map, maq, mar, mas, mat, mau, mav, maw, max, may, maz;
        public string mba, mbb, mbc, mbd, mbe, mbf, mbg, mbh, mbi, mbj, mbk, mbl, mbm, mbn, mbo, mbp, mbq, mbr, mbs, mbt;
        private bool button1WasClicked;
        private int loopCount, loop, keyd;
        public int pass = 0;

        public EditClaimStatus()
        {
            InitializeComponent();
            //HideLabels();
            textBox28.Visible = false;
            textBox29.Visible = false;
            textBox30.Visible = false;
            textBox31.Visible = false;
            textBox32.Visible = false;
            textBox33.Visible = false;
            textBox34.Visible = false;
            textBox36.Visible = false;
            label28.Visible = false;
            label29.Visible = false;
            label30.Visible = false;
            label31.Visible = false;
            textBox35.Visible = false;
            Icon = image100;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
            claim_no = Version.Claim;
            //label25.Text = "Claim #: " + claim_no;
            GetData();
            //GetDealerNames();
            //GetClaimData();

        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            textBox27.Select();
        }

        private void textBox17_Leave(object sender, EventArgs e)
        {
            textBox26.Select();
        }

        private void textBox26_Leave(object sender, EventArgs e)
        {
            var xxx = mbl;
            var yyy = textBox26.Text;
            if (xxx != yyy)
            {
                string message = "Are you sure you\nwant to change this value?\n(Asked twice to ensure\nyou are absolutly sure.)";
                string title = "Verify Change of Data";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show(message, title, buttons);
                if (result == DialogResult.Yes)
                {
                    //this.Close();
                }
                else
                {
                    textBox26.Text = xxx;
                    textBox26.Focus();
                }
            }
            textBox18.Select();
        }

        private void textBox18_Leave(object sender, EventArgs e)
        {
            textBox19.Select();
        }

        private void textBox23_Leave(object sender, EventArgs e)
        {
            textBox25.Select();
        }

        private void textBox25_Leave(object sender, EventArgs e)
        {
            textBox24.Select();
        }

        private void textBox24_Leave(object sender, EventArgs e)
        {
            button1.Select();
        }


        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                textBox2.Select();
                textBox2.DeselectAll();
                textBox2.SelectionStart = textBox2.Text.Length;
                textBox2.SelectionLength = 0;
            }
            keyd = textBox1.Text.Length;
            keyd++;
            if (keyd >= 33)
            {
                textBox2.Select();
                textBox2.DeselectAll();
                textBox2.SelectionStart = textBox2.Text.Length;
                textBox2.SelectionLength = 0;
                keyd = 0;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            textBox2.SelectionStart = textBox2.Text.Length;
            textBox2.SelectionLength = 0;
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                textBox3.Select();
                textBox3.DeselectAll();
            }
            keyd = textBox2.Text.Length;
            keyd++;
            if (keyd >= 41)
            {
                textBox3.Select();
                textBox3.DeselectAll();
                keyd = 0;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            textBox3.SelectionStart = textBox3.Text.Length;
            textBox3.SelectionLength = 0;
            textBox3.Focus();
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                textBox4.Select();
                textBox4.DeselectAll();
            }
            keyd = textBox3.Text.Length;
            keyd++;
            if (keyd >= 41)
            {
                textBox4.Select();
                textBox4.DeselectAll();
                keyd = 0;
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            textBox6.Select();
        }

        private void textBox27_Leave(object sender, EventArgs e)
        {
            textBox7.Select();
        }


        private void HideLabels()
        {
            for (int i = 1; i < 28; i++)
            {
                Label l = (Label)this.Controls["label" + i.ToString()];
                l.Visible = false;
            }
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            textBox5.Visible = false;
            textBox6.Visible = false;
            textBox7.Visible = false;
            textBox8.Visible = false;
            textBox9.Visible = false;
            textBox10.Visible = false;
            textBox11.Visible = false;
            textBox12.Visible = false;
            textBox13.Visible = false;
            textBox14.Visible = false;
            textBox15.Visible = false;
            textBox16.Visible = false;
            textBox17.Visible = false;
            textBox18.Visible = false;
            textBox19.Visible = false;
            textBox20.Visible = false;
            textBox21.Visible = false;
            textBox22.Visible = false;
            textBox23.Visible = false;
            textBox24.Visible = false;
            textBox25.Visible = false;
            textBox26.Visible = false;
            textBox27.Visible = false;
        }

        public void ErrorCheckingPart1()
        {
            if (textBox1.Text.Contains(","))
            {
                String str = textBox1.Text;
                str = str.Replace(",", ";");
                md = str;
                textBox1.Text = md;                 // First Name
            }
            if (textBox2.Text.Contains(","))
            {
                String str = textBox2.Text;
                str = str.Replace(",", ";");
                me = str;
                textBox2.Text = me;                 // Last Name
            }
            if (textBox3.Text.Contains(","))
            {
                String str = textBox3.Text;
                str = str.Replace(",", ";");
                mf = str;
                textBox3.Text = mf;                 // Address
            }
            if (textBox4.Text.Contains(","))
            {
                String str = textBox4.Text;
                str = str.Replace(",", ";");
                mg = str;
                textBox4.Text = mg;                 // City
            }
            if (textBox5.Text.Contains(","))
            {
                String str = textBox5.Text;
                str = str.Replace(",", ";");
                mh = str;
                textBox5.Text = mh;                 // State
            }
            if (textBox6.Text.Contains(","))
            {
                String str = textBox6.Text;
                str = str.Replace(",", ";");
                mi = str;
                textBox6.Text = mi;                 // Zip Code
            }
            if (textBox7.Text.Contains(","))
            {
                String str = textBox7.Text;
                str = str.Replace(",", ";");
                mj = str;
                textBox7.Text = mj;                 // Home / Cell Phone
            }
            if (textBox8.Text.Contains(","))
            {
                String str = textBox8.Text;
                str = str.Replace(",", ";");
                mk = str;
                textBox8.Text = mk;                 // Work Phone
            }
            if (textBox9.Text.Contains(","))
            {
                String str = textBox9.Text;
                str = str.Replace(",", ";");
                mbp = str;
                textBox9.Text = mbp;                 // Phone Extension
            }
            if (textBox10.Text.Contains(","))
            {
                String str = textBox10.Text;
                str = str.Replace(",", ";");
                mm = str;
                textBox10.Text = mm;                 // Brand (SANSUI, JBL PROFESSIONAL, etc)
            }
            if (textBox11.Text.Contains(","))
            {
                String str = textBox11.Text;
                str = str.Replace(",", ";");
                mbj = str;
                textBox11.Text = mbj;                 // Product
            }
            if (textBox12.Text.Contains(","))
            {
                String str = textBox12.Text;
                str = str.Replace(",", ";");
                mo = str;
                textBox12.Text = mo;                 // Model (EON615, etc)
            }
            if (textBox13.Text.Contains(","))
            {
                String str = textBox13.Text;
                str = str.Replace(",", ";");
                mp = str;
                textBox13.Text = mp;                 // Serial Number
            }
            if (textBox14.Text.Contains(","))
            {
                String str = textBox14.Text;
                str = str.Replace(",", ";");
                mad = str;
                textBox14.Text = mad;                 // Date Purchased
            }
            if (textBox15.Text.Contains(","))
            {
                String str = textBox15.Text;
                str = str.Replace(",", ";");
                mc = str;
                textBox15.Text = mc;                 // Date in Shop
            }
            if (textBox16.Text.Contains(","))
            {
                String str = textBox16.Text;
                str = str.Replace(",", ";");
                mbm = str;
                textBox16.Text = mbm;                // Date Completed
            }
            if (textBox17.Text.Contains(","))
            {
                String str = textBox17.Text;
                str = str.Replace(",", ";");
                mbc = str;
                textBox17.Text = mbc;                 // Date Closed
            }
            if (textBox18.Text.Contains(","))
            {
                String str = textBox18.Text;
                str = str.Replace(",", ";");
                mai = str;
                textBox18.Text = mai;                 // Client / Dealer Name
            }
            if (textBox19.Text.Contains(","))
            {
                String str = textBox19.Text;
                str = str.Replace(",", ";");
                maj = str;
                textBox19.Text = maj;                 // Client Address
            }
            if (textBox20.Text.Contains(","))
            {
                String str = textBox20.Text;
                str = str.Replace(",", ";");
                mak = str;
                textBox20.Text = mak;                 // Client Address
            }
            if (textBox21.Text.Contains(","))
            {
                String str = textBox21.Text;
                str = str.Replace(",", ";");
                mal = str;
                textBox21.Text = mal;                 // Client State
            }
            if (textBox22.Text.Contains(","))
            {
                String str = textBox22.Text;
                str = str.Replace(",", ";");
                mam = str;
                textBox22.Text = mam;                 // Client Zip Code
            }
            if (textBox23.Text.Contains(","))
            {
                String str = textBox23.Text;
                str = str.Replace(",", ";");
                man = str;
                textBox23.Text = man;                 // Client Phone Number
            }
            if (textBox24.Text.Contains(","))
            {
                String str = textBox24.Text;
                str = str.Replace(",", ";");
                mbf = str;
                textBox24.Text = mbf;                 // Client INV / Claim Number
            }
            if (textBox25.Text.Contains(","))
            {
                String str = textBox25.Text;
                str = str.Replace(",", ";");
                mbe = str;
                textBox25.Text = mbe;                 // Shelf Location in Warehouse
            }
            if (textBox26.Text.Contains(","))
            {
                String str = textBox26.Text;
                str = str.Replace(",", ";");
                mbl = str;
                textBox26.Text = mbl;                 // Claim Status (RECALL, WARRANTY, NON-WARRANTY, etc)
            }
            if (textBox27.Text.Contains(","))
            {
                String str = textBox27.Text;
                str = str.Replace(",", ";");
                mbt = str;
                textBox2.Text = mbt;                // Email address
            }
        }

        private void Button1_Click(object sender, EventArgs e)  // Page2
        {

            if (button1.Text == "Page 2")
            {
                EditCSV();
                HideLabels();

                if (pass == 0)
                {
                    label31.Visible = true;
                    label31.Text = "Data Saved ! (Page 1)";
                    label31.ForeColor = Color.Yellow;
                }
                button1.Text = "SAVE";
            }
            if (button1.Text == "SAVE")
            {
                button1WasClicked = true;
                //GetPage2();
                button1WasClicked = false;
                //button1.Text = "Done-Exit";

                if (pass == 0)
                {
                    label31.Visible = true;
                    label31.Text = "Data Saved ! (Page 1)";
                    label31.ForeColor = Color.Yellow;
                }
                if (pass == 1)
                {
                    label31.Visible = true;
                    label31.Text = "Data Saved ! (Page 1 and Page 2)";
                    label31.ForeColor = Color.Yellow;
                    EditCSV2();
                    button1.Text = "DONE-EXIT";
                }
                pass++;
                button1WasClicked = true;
                GetData();
                //EditCSV2();

            }

            if (pass >= 2)
            {
                //EditCSV();
                pass = 0;
                button1.Text = "DONE-EXIT";
                //MessageBox.Show("Ready to Exit");
                button1.Visible = true;
            }
            if (button1.Text == "DONE-EXIT")
            {
                Thread.Sleep(1000);
                Hide();
                ClaimsMGTMenu f2 = new ClaimsMGTMenu();
                f2.Show();
            }
        }

        private void Button2_Click(object sender, EventArgs e)  // Return
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }

        public void GetData()
        {
            pass = 0;
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


                    if (listB[loopCount].Contains(claim_no))
                    {
                        ma = listA[loopCount].ToUpper();
                        mb = listB[loopCount].ToUpper();
                        mc = listC[loopCount].ToUpper();
                        md = listD[loopCount].ToUpper();
                        me = listE[loopCount].ToUpper();
                        mf = listF[loopCount].ToUpper();
                        mg = listG[loopCount].ToUpper();
                        mh = listH[loopCount].ToUpper();
                        mi = listI[loopCount].ToUpper();
                        if (mi == "0")
                        {
                            mi = "00000";
                        }
                        mj = listJ[loopCount].ToUpper();
                        mk = listK[loopCount].ToUpper();
                        ml = listL[loopCount].ToUpper();
                        mm = listM[loopCount].ToUpper();
                        mn = listN[loopCount].ToUpper();
                        mo = listO[loopCount].ToUpper();
                        mp = listP[loopCount].ToUpper();
                        mq = listQ[loopCount].ToUpper();
                        mr = listR[loopCount].ToUpper();
                        ms = listS[loopCount].ToUpper();
                        mt = listT[loopCount].ToUpper();
                        mu = listU[loopCount].ToUpper();
                        mv = listV[loopCount].ToUpper();
                        mw = listW[loopCount].ToUpper();
                        mx = listX[loopCount].ToUpper();
                        my = listY[loopCount].ToUpper();
                        mz = listZ[loopCount].ToUpper();
                        maa = listAA[loopCount].ToUpper();
                        mab = listAB[loopCount].ToUpper();
                        mac = listAC[loopCount].ToUpper();
                        mad = listAD[loopCount].ToUpper();
                        mae = listAE[loopCount].ToUpper();
                        maf = listAF[loopCount].ToUpper();
                        mag = listAG[loopCount].ToUpper();
                        mah = listAH[loopCount].ToUpper();
                        mai = listAI[loopCount].ToUpper();
                        maj = listAJ[loopCount].ToUpper();
                        mak = listAK[loopCount].ToUpper();
                        mal = listAL[loopCount].ToUpper();
                        mam = listAM[loopCount].ToUpper();
                        man = listAN[loopCount].ToUpper();
                        mao = listAO[loopCount].ToUpper();
                        map = listAP[loopCount].ToUpper();
                        maq = listAQ[loopCount].ToUpper();
                        mar = listAR[loopCount].ToUpper();
                        mas = listAS[loopCount].ToUpper();
                        mat = listAT[loopCount].ToUpper();
                        mau = listAU[loopCount].ToUpper();
                        mav = listAV[loopCount].ToUpper();
                        maw = listAW[loopCount].ToUpper();
                        max = listAX[loopCount].ToUpper();
                        may = listAY[loopCount].ToUpper();
                        maz = listAZ[loopCount].ToUpper();
                        mba = listBA[loopCount].ToUpper();
                        mbb = listBB[loopCount].ToUpper();
                        mbc = listBC[loopCount].ToUpper();
                        mbd = listBD[loopCount].ToUpper();
                        mbe = listBE[loopCount].ToUpper();
                        mbf = listBF[loopCount].ToUpper();
                        mbg = listBG[loopCount].ToUpper();
                        mbh = listBH[loopCount].ToUpper();
                        mbi = listBI[loopCount].ToUpper();
                        mbj = listBJ[loopCount].ToUpper();
                        mbk = listBK[loopCount].ToUpper();
                        mbl = listBL[loopCount].ToUpper();
                        mbm = listBM[loopCount].ToUpper();
                        mbn = listBN[loopCount].ToUpper();
                        mbo = listBO[loopCount].ToUpper();
                        mbp = listBP[loopCount].ToUpper();
                        mbq = listBQ[loopCount].ToUpper();
                        mbr = listBR[loopCount].ToUpper();
                        mbr = listBR[loopCount].ToUpper();
                        mbs = listBS[loopCount].ToUpper();
                        mbt = listBT[loopCount].ToUpper();

                        textBox1.Text = md;
                        textBox2.Text = me;
                        textBox3.Text = mf;
                        textBox4.Text = mg;
                        textBox5.Text = mh;
                        textBox6.Text = mi;
                        textBox7.Text = mj;
                        textBox8.Text = mk;
                        textBox9.Text = mbp;
                        textBox10.Text = mm;
                        textBox11.Text = mbj;
                        textBox12.Text = mo;
                        textBox13.Text = mp;
                        textBox14.Text = mad;       // Date Purchased
                        textBox15.Text = mc;
                        textBox16.Text = mbm;
                        textBox17.Text = mbc;
                        textBox18.Text = mai;
                        textBox19.Text = maj;       // Client Address
                        textBox20.Text = mak;       // Client City
                        textBox21.Text = mal;       // Client State
                        if (mam == "0")
                        {
                            mam = "00000";
                        }
                        textBox22.Text = mam;       // Client Zip Code
                        textBox23.Text = man;       // Client Phone
                        textBox24.Text = mbf;       // Client Claim / Inventory #
                        textBox25.Text = mbe;       // Shelf Location
                        textBox26.Text = mbl;       // Claim Status, recall, etc
                        textBox27.Text = mbt;       // Email address
                        loop++;
                        if (claim_no == mb)
                        {
                            //label25.Text = claim_no + ",  " + listBQ[loopCount];
                            Text = "Edit Claim # " + claim_no + ",  " + mbq + "    Use tab to skip through fields below"; ;
                        }

                        if (button1WasClicked == true)
                        {
                            pass++;
                            textBox36.Select();
                            label30.Visible = true;
                            label30.ForeColor = Color.Yellow;
                            label4.Visible = true;
                            label4.Text = "Explanation:";
                            label25.Visible = true;
                            label25.Text = "Explanation:";
                            label27.Visible = true;
                            label27.Text = "Shelf Location:";
                            label28.Visible = true;
                            label29.Visible = true;
                            label29.ForeColor = Color.Yellow;
                            label29.Text = "Technical Services";
                            textBox36.Visible = true;
                            textBox36.Text = listL[loopCount];
                            textBox28.Visible = true;
                            textBox28.Text = listAE[loopCount];
                            textBox29.Visible = true;
                            textBox29.Text = listAF[loopCount];
                            textBox30.Visible = true;
                            textBox31.Visible = true;
                            textBox32.Visible = true;
                            textBox33.Visible = true;
                            textBox34.Visible = true;
                            textBox35.Visible = true;
                            textBox30.Text = listAU[loopCount];
                            textBox31.Text = listAV[loopCount];
                            textBox32.Text = listAW[loopCount];
                            textBox33.Text = listAX[loopCount];
                            textBox34.Text = listBE[loopCount];

                            switch (listW[loopCount])
                            {
                                case "FALSE":
                                    textBox35.Text = "F";
                                    break;
                                case "TRUE":
                                    textBox35.Text = "T";
                                    break;
                                default:
                                    textBox35.Text = "U";
                                    break;
                            }
                        }

                    }
                    loopCount++;
                }
                reader.Close(); // Close the open file
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 231: Sorry an error has occured: " + ex.Message);
            }
        }

        public void EditCSV()
        {
            if (pass == 0)
            {
                ErrorCheckingPart1();
                label31.Visible = true;
                label31.Text = "Data Saved ! (Page 1)";
                label31.ForeColor = Color.Yellow;

                md = textBox1.Text;
                me = textBox2.Text;
                mf = textBox3.Text;
                mg = textBox4.Text;
                mh = textBox5.Text;
                mi = textBox6.Text;
                mj = textBox7.Text;
                mk = textBox8.Text;
                mbp = textBox9.Text;
                mm = textBox10.Text;
                mbj = textBox11.Text;
                mo = textBox12.Text;
                mp = textBox13.Text;
                mad = textBox14.Text;       // Date Purchased
                mc = textBox15.Text;
                mbm = textBox16.Text;
                mbc = textBox17.Text;
                mai = textBox18.Text;
                maj = textBox19.Text;       // Client Address
                mak = textBox20.Text;       // Client City
                mal = textBox21.Text;       // Client State
                mam = textBox22.Text;       // Client Zip Code
                man = textBox23.Text;       // Client Phone
                mbf = textBox24.Text;       // Client Claim / Inventory #
                mbe = textBox25.Text;       // Shelf Location
                mbl = textBox26.Text;       // Claim Status, recall, etc
                mbt = textBox27.Text;       // Email address
            }

            if (pass == 1)
            {
                ml = textBox36.Text;
                mae = textBox28.Text;
                maf = textBox29.Text;
                mai = textBox18.Text;
                mau = textBox30.Text;
                mav = textBox31.Text;
                maw = textBox32.Text;
                max = textBox33.Text;

                mbe = textBox34.Text;

                if (mx.Contains("T"))
                {
                    mx = "TRUE";
                }
                if (mx.Contains("F"))
                {
                    mx = "FALSE";
                }
                if (mx.Contains("U"))
                {
                    mx = "FALSE";
                }
            }


            string path = file;
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
                                if (split[1].Contains(claim_no))
                                {
                                    split[0] = ma;
                                    split[1] = mb;
                                    split[2] = mc;
                                    split[3] = md;  // First Name
                                    split[4] = me;
                                    split[5] = mf;
                                    split[6] = mg;
                                    split[7] = mh;
                                    split[8] = mi;
                                    split[9] = mj;
                                    split[10] = mk;
                                    split[11] = ml;
                                    split[12] = mm;
                                    split[13] = mn;
                                    split[14] = mo;
                                    split[15] = mp;
                                    split[16] = mq;
                                    split[17] = mr;
                                    split[18] = ms;
                                    split[19] = mt;
                                    split[20] = mu;
                                    split[21] = mv;
                                    split[22] = mw;
                                    split[23] = mx;
                                    split[24] = my;
                                    split[25] = mz;
                                    split[26] = maa;
                                    split[27] = mab;
                                    split[28] = mac;
                                    split[29] = mad;
                                    split[30] = mae;
                                    split[31] = maf;
                                    split[32] = mag;
                                    split[33] = mah;
                                    split[34] = mai;
                                    split[35] = maj;
                                    split[36] = mak;
                                    split[37] = mal;
                                    split[38] = mam;
                                    split[39] = man;
                                    split[40] = mao;
                                    split[41] = map;
                                    split[42] = maq;
                                    split[43] = mar;
                                    split[44] = mas;
                                    split[45] = mat;
                                    split[46] = mau;
                                    split[47] = mav;
                                    split[48] = maw;
                                    split[49] = max;
                                    split[50] = may;
                                    split[51] = maz;
                                    split[52] = mba;
                                    split[53] = mbb;
                                    split[54] = mbc;
                                    split[55] = mbd;
                                    split[56] = mbe;
                                    split[57] = mbf;
                                    split[58] = mbg;
                                    split[59] = mbh;
                                    split[60] = mbi;
                                    split[61] = mbj;
                                    split[62] = mbk;
                                    split[63] = mbl;
                                    split[64] = mbm;
                                    split[65] = mbn;
                                    split[66] = mbo;
                                    split[67] = mbp;
                                    split[68] = mbq;
                                    split[69] = mbr;
                                    split[70] = mbs;
                                    split[71] = mbt;

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

        public void EditCSV2()      // Page 2
        {


            if (pass >= 0)
            {
                ml = textBox36.Text;
                mae = textBox28.Text;
                maf = textBox29.Text;
                mai = textBox18.Text;
                mau = textBox30.Text;
                mav = textBox31.Text;
                maw = textBox32.Text;
                max = textBox33.Text;

                mbe = textBox34.Text;

                if (mx.Contains("T"))
                {
                    mx = "TRUE";
                }
                if (mx.Contains("F"))
                {
                    mx = "FALSE";
                }
                if (mx.Contains("U"))
                {
                    mx = "FALSE";
                }
            }


            string path = file;
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
                                if (split[1].Contains(claim_no))
                                {

                                    split[11] = ml;

                                    split[30] = mae;
                                    split[31] = maf;

                                    split[34] = mai;

                                    split[46] = mau;
                                    split[47] = mav;
                                    split[48] = maw;
                                    split[49] = max;


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

        public void GetPage2()
        {
            textBox36.Select();
            button1.Text = "SAVE";
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

                    if (listB[loopCount].Contains(claim_no))
                    {


                        loop++;
                        if (claim_no == listB[loopCount])
                        {
                            //label25.Text = claim_no + ",  " + listBQ[loopCount];
                            Text = "Edit Claim # " + claim_no + ",  " + listBQ[loopCount] + "    Use tab to skip through fields below"; ;
                        }
                        if (button1WasClicked == true)
                        {
                            textBox36.Select();
                            label30.Visible = true;
                            label30.ForeColor = Color.Yellow;
                            label4.Visible = true;
                            label4.Text = "Explanation:";
                            label25.Visible = true;
                            label25.Text = "Explanation:";
                            label27.Visible = true;
                            label27.Text = "Shelf Location:";
                            label28.Visible = true;
                            label29.Visible = true;
                            label29.ForeColor = Color.Yellow;
                            label29.Text = "Technical Services";
                            textBox36.Visible = true;
                            textBox36.Text = listL[loopCount];
                            textBox28.Visible = true;
                            textBox28.Text = listAE[loopCount];
                            textBox29.Visible = true;
                            textBox29.Text = listAF[loopCount];
                            textBox30.Visible = true;
                            textBox31.Visible = true;
                            textBox32.Visible = true;
                            textBox33.Visible = true;
                            textBox34.Visible = true;
                            textBox35.Visible = true;
                            textBox30.Text = listAU[loopCount];
                            textBox31.Text = listAV[loopCount];
                            textBox32.Text = listAW[loopCount];
                            textBox33.Text = listAX[loopCount];
                            textBox34.Text = listBE[loopCount];

                            switch (listW[loopCount])
                            {
                                case "FALSE":
                                    textBox35.Text = "F";
                                    break;
                                case "TRUE":
                                    textBox35.Text = "T";
                                    break;
                                default:
                                    textBox35.Text = "U";
                                    break;
                            }
                        }

                    }
                    loopCount++;
                }
                reader.Close(); // Close the open file

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 231: Sorry an error has occured: " + ex.Message);
            }
        }

        public void GetDealerNames()
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

                loopCount = 0;

                while (!reader.EndOfStream)
                {
                    var lineRead = reader.ReadLine();
                    var values = lineRead.Split(',');

                    listA.Add(values[0]);       //  Dealer_czx
                    listB.Add(values[1]);       //  deal_name
                    listC.Add(values[2]);       //  deal_addr
                    listD.Add(values[3]);       //  deal_cty
                    listE.Add(values[4]);       //  deal_st
                    listF.Add(values[5]);       //  deal_zip
                    listG.Add(values[6]);       //  deal_phone
                    listH.Add(values[7]);       //  info1
                    listI.Add(values[8]);       //  info2
                    listJ.Add(values[9]);       //  info3
                    listK.Add(values[10]);      //  info4
                    listL.Add(values[11]);      //  info5
                    listM.Add(values[12]);      //  info6
                    listN.Add(values[13]);      //  ups_code
                    listO.Add(values[14]);      //  ups_code
                    listP.Add(values[15]);      //  Number

                    var TheCount = loopCount.ToString();
                    //comboBox1.Items.Add(listB[loopCount]);
                    loop++;

                    loopCount++;
                }
                reader.Close(); // Close the open file

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 94: Sorry an error has occured: " + ex.Message);
            }
        }

    }
}
