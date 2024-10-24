using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Microsoft.Win32;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WizServ
{
    public partial class CloseClaim : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        public string claim_no;
        private readonly string Database = @"I:\\Datafile\\Control\\Database.CSV";
        public string ma, mb, mc, md, me, mf, mg, mh, mi, mj, mk, ml, mm, mn, mo, mp, mq, mr, ms, mt, mu, mv, mw, mx, my, mz;
        public string maa, mab, mac, mad, mae, maf, mag, mah, mai, maj, mak, mal, mam, man, mao, map, maq, mar, mas, mat, mau, mav, maw, max, may, maz;
        public string mba, mbb, mbc, mbd, mbe, mbf, mbg, mbh, mbi, mbj, mbk, mbl, mbm, mbn, mbo, mbp, mbq, mbr, mbs, mbt;
        public string mbu, mbv, mbw, mbx, temp, ServRender;
        public decimal GATAX, total, tax;
        private bool button1WasClicked;
        private int loopCount, loop, keyd;
        public int pass = 0, days;
        static readonly string key = @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\lanmanserver\parameters";
        public readonly string computerDescription = (string)Registry.GetValue(key, "srvcomment", null);
        public bool Error;

        public CloseClaim()
        {
            InitializeComponent();
            claim_no = Version.Claim;
            label1.Text = "Claim #: " + claim_no;
            GetData();
            textBox4.Select();
            textBox4.Text = DateTime.Now.ToShortDateString();
        }

        private void button1_Click(object sender, EventArgs e)
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
                        label27.Text = mau;
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
                        mbu = listBU[loopCount].ToUpper();
                        mbv = listBV[loopCount].ToUpper();
                        mbw = listBW[loopCount].ToUpper();
                        mbx = listBX[loopCount].ToUpper();

                        ServRender = mbd;
                        label28.Text = ServRender;
                        if (ServRender.Contains("SERVICE RENDER"))
                        {
                            Error = true;
                            MessageBox.Show("Claim has already been rendered.");
                        }
                        label2.Text = mc;
                        string iDate = mc;
                        DateTime oDate = Convert.ToDateTime(iDate);
                        TimeSpan diff0 = DateTime.Now - oDate;
                        var t4 = diff0;
                        var t6 = diff0.ToString();
                        var t7 = t6.Substring(0, 2);
                        var t8 = t6.Substring(0, 3);
                        var t9 = t6.Substring(0, 4);
                        var t10 = t6.Substring(0, 5);
                        if (t9.Contains("."))
                        {
                            if (t8.Contains("."))
                            {
                                days = Convert.ToInt32(t7);
                            }
                            else
                            {
                                days = Convert.ToInt32(t8);
                            }
                        }
                        if (t9.Length >= 4)
                        {
                            if (!t9.Contains("."))
                            {
                                days = Convert.ToInt32(t9);
                            }
                        }
                        //var t5 = Convert.ToInt32(t7);
                        if (days <= 45)
                        {
                            label3.ForeColor = Color.Green;
                            label3.BackColor = Color.White;
                        }
                        if (days > 45 && days <= 90)
                        {
                            label3.ForeColor = Color.Yellow;
                            label3.BackColor = Color.Black;
                        }
                        if (days > 90)
                        {
                            label3.ForeColor = Color.Red;
                            label3.BackColor = Color.White;
                        }
                        label3.Text = " " + days.ToString() + " days ";

                        label7.Text = md + " " + me;
                        label9.Text = mf + "\n" + mg + ", " + mh + " " + mi + "\n" + "Home: " + mj + "\n" + "Work: " + mk;
                        label11.Text = mm + ", " + mo + ", SN: " + mp;
                        var mr1 = decimal.Parse(mr);

                        textBox1.Text = mr;
                        textBox2.Text = ms;
                        textBox3.Text = mz;
                        //textBox4.Text = "11.00";
                        textBox5.Text = maa;
                        if (maa != "15")
                        {
                            maa = "15.00";
                            textBox5.Text = "15.00";
                        }
                        //if ()
                        textBox6.Text = mu;
                        var a = Convert.ToDecimal(mu);
                        if (a > 65)
                        {
                            var z = a - 65;
                            mu = z.ToString("0.00");
                            textBox6.Text = mu;
                        }
                        var bb = Convert.ToDecimal(mz);
                        if (a == bb)
                        {
                            textBox6.Text = "0.00";
                        }
                        textBox7.Text = mab;
                        textBox8.Text = mq;
                    }
                    loopCount++;
                }
                reader.Close(); // Close the open file
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 386: Sorry an error has occured: " + ex.Message);
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)    // Labor
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox1.Text.Contains("."))
                {
                    textBox2.Select();
                }
                else
                {
                    textBox1.Text += ".00";
                    textBox2.Select();
                }

                CheckHours();
            }
        }

        private void CheckHours()
        {
            switch (textBox1.Text)
            {
                case "40.00":
                    label21.Text = "0.50 Hour @ $ 80/Hour - Analog";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "45.00":
                    label21.Text = "0.50 Hour @ $ 90/Hour - Digital";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "60.00":
                    label21.Text = "0.75 Hour @ $ 80/Hour - Analog";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "67.50":
                    label21.Text = "0.75 Hour @ $ 90/Hour - Digital";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "80.00":
                    label21.Text = "1 Hour @ $ 80/Hour - Analog";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "90.00":
                    label21.Text = "1 Hour @ $ 90/Hour - Digital";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "120.00":
                    label21.Text = "1.5 Hours @ $ 80/Hour - Analog";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "135.00":
                    label21.Text = "1.5 Hours @ $ 90/Hour - Digital";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "180.00":
                    label21.Text = "2 Hours @ $ 80/Hour - Analog";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "190.00":
                    label21.Text = "2 Hours @ $ 80/Hour - Digital";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "200.00":
                    label21.Text = "2.5 Hours @ $ 80/Hour - Analog";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "225.00":
                    label21.Text = "2.5 Hours @ $ 90/Hour - Digital";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "240.00":
                    label21.Text = "3.0 Hours @ $ 80/Hour - Analog";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "270.00":
                    label21.Text = "3.0 Hours @ $ 90/Hour - Digital";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "280.00":
                    label21.Text = "3.5 Hours @ $ 80/Hour - Analog";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "315.00":
                    label21.Text = "3.5 Hours @ $ 90/Hour - Digital";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "320.00":
                    label21.Text = "4.0 Hours @ $ 80/Hour - Analog";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "360.00":
                    label21.Text = "4.0 Hours @ $ 90/Hour - Digital or 4.5 Hours @ $ 80/HR Analog";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "405.00":
                    label21.Text = "4.5 Hours @ $ 90/Hour - Digital";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "400.00":
                    label21.Text = "5.0 Hours @ $ 80/Hour - Analog";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "450.00":
                    label21.Text = "5.0 Hours @ $ 90/Hour - Digital";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "440.00":
                    label21.Text = "5.5 Hours @ $ 80/Hour - Analog";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "495.00":
                    label21.Text = "5.5 Hours @ $ 90/Hour - Digital";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "480.00":
                    label21.Text = "6.0 Hours @ $ 80/Hour - Analog";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "540.00":
                    label21.Text = "6.0 Hours @ $ 90/Hour - Digital";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "520.00":
                    label21.Text = "6.5 Hours @ $ $ 80/Hour - Analog";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "585.00":
                    label21.Text = "6.5 Hours @ $ 90/Hour - Digital";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "560.00":
                    label21.Text = "7.0 Hours @ $ 80/Hour - Analog";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "630.00":
                    label21.Text = "7.0 Hours @ $ 90/Hour - Digital";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "600.00":
                    label21.Text = "7.5 Hours @ $ 80/Hour - Analog";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "675.00":
                    label21.Text = "7.5 Hours @ $ 90/Hour - Digital";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "640.00":
                    label21.Text = "8.0 Hours @ $ 80/Hour - Analog";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "720.00":
                    label21.Text = "8.0 Hours @ $ 90/Hour - Digital or 9.0 Hours @ $ 80/Hour - Analog";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "680.00":
                    label21.Text = "8.5 Hours @ $ 80/Hour - Analog";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "765.00":
                    label21.Text = "8.5 Hours @ $ 90/Hour - Digital";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "810.00":
                    label21.Text = "9.0 Hours @ $ 90/Hour - Digital";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "760.00":
                    label21.Text = "9.5 Hours @ $ 80/Hour - Analog";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "855.00":
                    label21.Text = "9.5 Hours @ $ 90/Hour - Digital";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "800.00":
                    label21.Text = "10.0 Hours @ $ 80/Hour - Analog";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "900.00":
                    label21.Text = "10.0 Hours @ $ 90/Hour - Digital";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "840.00":
                    label21.Text = "10.5 Hours @ $ 80/Hour - Analog";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "945.00":
                    label21.Text = "10.5 Hours @ $ 90/Hour - Digital";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "880.00":
                    label21.Text = "11.0 Hours @ $ 80/Hour - Analog";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "990.00":
                    label21.Text = "11.0 Hours @ $ 90/Hour - Digital";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "920.00":
                    label21.Text = "11.5 Hours @ $ 80/Hour - Analog";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "1035.00":
                    label21.Text = "11.5 Hours @ $ 90/Hour - Digital";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "960.00":
                    label21.Text = "12.0 Hours @ $ 80/Hour - Analog";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "1080.00":
                    label21.Text = "12.0 Hours @ $ 90/Hour - Digital or 13.5 Hours @ $ 80/Hour - Analog";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "1000.00":
                    label21.Text = "12.5 Hours @ $ 80/Hour - Analog";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "1125.00":
                    label21.Text = "12.5 Hours @ $ 90/Hour - Digital";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "1040.00":
                    label21.Text = "13.0 Hours @ $ 80/Hour - Analog";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "1170.00":
                    label21.Text = "13.0 Hours @ $ 90/Hour - Digital";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "1215.00":
                    label21.Text = "13.5 Hours @ $ 90/Hour - Digital";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "1120.00":
                    label21.Text = "14.0 Hours @ $ 80/Hour - Analog";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "1260.00":
                    label21.Text = "14.0 Hours @ $ 90/Hour - Digital";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "1160.00":
                    label21.Text = "14.5 Hours @ $ 80/Hour - Analog";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "1305.00":
                    label21.Text = "14.5 Hours @ $ 90/Hour - Digital";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "1200.00":
                    label21.Text = "15.0 Hours @ $ 80/Hour - Analog";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
                case "1350.00":
                    label21.Text = "15.0 Hours @ $ 90/Hour - Digital";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;

                default:
                    var j = 80m;
                    label21.Text = (Convert.ToDecimal(textBox1.Text) / j).ToString() + " Hours @ $ 80/Hour";
                    label21.Text += " " + mau.Substring(0, 10);
                    break;
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)    // Parts
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox2.Text.Contains("."))
                {
                    textBox3.Select();
                    label25.Text = "Parts: $ " + (Convert.ToDecimal(ms) - 11.00m).ToString("0.00") + " + Shop Fee $ 11.00";
                }
                else
                {
                    textBox2.Text += ".00";
                    label25.Text = "Parts: $ " + (Convert.ToDecimal(ms) - 11.00m).ToString("0.00") + " + Shop Fee $ 11.00";
                    textBox3.Select();
                }

            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)   // Diagnostics $ 65.00
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox3.Text.Contains("."))
                {
                    textBox5.Select();
                }
                else
                {
                    textBox3.Text += ".00";
                    textBox5.Select();
                }

            }
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (mbb != "00/00/0000")
                {
                    textBox4.Text = mbb;
                }
                if (e.KeyCode == Keys.Enter)
                {
                    var dateInput = Convert.ToDateTime(textBox4.Text);
                    string iDate = mc;
                    DateTime oDate = Convert.ToDateTime(iDate);
                    TimeSpan diff0 = dateInput - oDate;
                    var t4 = diff0;
                    var t6 = diff0.ToString();
                    var t7 = t6.Substring(0, 2);
                    var t8 = t6.Substring(0, 3);
                    var t9 = t6.Substring(0, 4);
                    var t10 = t6.Substring(0, 5);
                    if (t9.Contains("."))
                    {
                        if (t8.Contains("."))
                        {
                            if (t7.Contains("."))
                            {
                                t7 = Convert.ToInt32(t7).ToString();
                            }
                            days = Convert.ToInt32(t7);
                        }
                        else
                        {
                            days = Convert.ToInt32(t8);
                        }
                    }
                    if (t9.Length >= 4)
                    {
                        if (!t9.Contains("."))
                        {
                            days = Convert.ToInt32(t9);
                        }
                    }
                    //var t5 = Convert.ToInt32(t7);
                    if (days <= 45)
                    {
                        label3.ForeColor = Color.Green;
                        label3.BackColor = Color.White;
                    }
                    if (days > 45 && days <= 90)
                    {
                        label3.ForeColor = Color.Yellow;
                        label3.BackColor = Color.Black;
                    }
                    if (days > 90)
                    {
                        label3.ForeColor = Color.Red;
                        label3.BackColor = Color.White;
                    }
                    label3.Text = " " + days.ToString() + " days ";
                    textBox9.Select();

                    textBox9.Select();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception Line 760 \n" + ex);
            }
        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox5.Text.Contains("."))
                {
                    textBox6.Select();
                }
                else
                {
                    textBox5.Text += ".00";
                    textBox6.Select();
                }
            }
        }

        private void textBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox6.Text.Contains("."))
                {
                    textBox7.Select();
                }
                else
                {
                    textBox6.Text += ".00";
                    textBox7.Select();
                }
            }
        }

        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox7.Text.Contains("."))
                {
                    textBox8.Select();
                }
                else
                {
                    textBox7.Text += ".00";
                    textBox8.Select();
                }
                switch (textBox7.Text.Substring(2, 2))
                {
                    case ".1":
                        textBox7.Text += "0";
                        break;
                    case ".2":
                        textBox7.Text += "0";
                        break;
                    case ".3":
                        textBox7.Text += "0";
                        break;
                    case ".4":
                        textBox7.Text += "0";
                        break;
                    case ".5":
                        textBox7.Text += "0";
                        break;
                    case ".6":
                        textBox7.Text += "0";
                        break;
                    case ".7":
                        textBox7.Text += "0";
                        break;
                    case ".8":
                        textBox7.Text += "0";
                        break;
                    case ".9":
                        textBox7.Text += "0";
                        break;
                }
                total = Convert.ToDecimal(textBox2.Text);
                tax = total * GATAX;
                if (mat == "WARRANTY")
                {
                    // Ignore if WARRANTY claim
                    textBox7.Text = "0.00";
                    label26.Text = "No tax - WARRANTY";
                }
                else
                {
                    total += tax;
                    textBox7.Text = tax.ToString("0.00");
                    label26.Text = "Taxed - NON-WARRANTY Claim";
                }


                total += Convert.ToDecimal(textBox5.Text) + Convert.ToDecimal(textBox1.Text);

                //total += total - Convert.ToDecimal(textBox3.Text);
                if (Convert.ToDecimal(textBox6.Text) == 0.00m)
                {
                    // Ignore if zero deposit made
                }
                else
                {
                    label22.Text = "Before deposit: $" + total.ToString("0.00");
                }
                textBox8.Text = (total - Convert.ToDecimal(textBox6.Text)).ToString("0.00");
            }
        }

        private void textBox8_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox8.Text.Contains("."))
                {
                    button2.Select();
                    button2.BackColor = Color.Gold;
                }
                else
                {
                    textBox8.Text += ".00";
                    button2.Select();
                    button2.BackColor = Color.Gold;
                }
            }
        }

        private void textBox9_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var s = Convert.ToDecimal(textBox9.Text);
                s = s / 100;
                GATAX = s;
                label24.Text = s.ToString() + " %";
                try
                {
                    temp = mau.Substring(0, 3);
                }
                catch (Exception)
                {
                    temp = "Unknown Hours - Please fix, retry Render.";
                    Error = true;
                    button1.Select();
                }
                try
                {
                    var d = Convert.ToDecimal(temp);
                    label21.Text = mau.Substring(0, 10);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Please correct 1st line of tech data to start with Hours,\ni.e.: 1.0 Hours, Disassembled...");
                    Error = true;
                    button1.Select();
                    button2.Visible = false;

                }
                if (Error != true)
                {
                    textBox1.Select();
                    Error = false;
                }
                else
                {
                    button1.Select();
                }
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label20.ForeColor = Color.Green;
            label20.BackColor = Color.White;
            label20.Text = " Rendered," + " Please press ENTER.";
            UpdateDateBase();
            button1.Select();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox4.Text = DateTime.Now.ToShortDateString();
            textBox4.Select();
        }

        public void UpdateDateBase()
        {
            string path = Database;
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
                                if (split[1] == claim_no)
                                {
                                    split[16] = Convert.ToDecimal(textBox8.Text).ToString("0.00");      // Grand Total
                                    split[17] = Convert.ToDecimal(textBox1.Text).ToString("0.00");      // Labor Chagres
                                    split[18] = Convert.ToDecimal(textBox2.Text).ToString("0.00");      // Parts Charges
                                    var pt1 = Convert.ToDecimal(textBox2.Text);
                                    var pt2 = pt1 - 11.00m;
                                    split[19] = pt2.ToString("0.00");               // Actual Parts Charges
                                    split[20] = textBox3.Text;
                                    var y1 = Convert.ToDecimal(textBox2.Text);
                                    var y2 = y1 - 11.00m;
                                    var y3 = y1 / 1.3m;
                                    var y4 = (y2 - y3) + 11.00m;
                                    split[23] = y4.ToString("0.00");                // Parts Profit
                                    var m1 = (Convert.ToDecimal(textBox1.Text) - (Convert.ToDecimal(textBox1.Text)/75.00m)) + y2;
                                    split[24] = m1.ToString("0.00");                // Profit
                                    split[27] = tax.ToString("0.00");
                                    split[53] = textBox4.Text;                      // Date Completed
                                    split[55] = "SERVICE RENDERED XX - " + textBox4.Text;
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
                try
                {
                    using (StreamWriter writer = new StreamWriter(path, false))
                    {
                        foreach (String line in lines)
                            writer.WriteLine(line);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error line 773: \n" + ex);
                }
            }
        }
    }
}
