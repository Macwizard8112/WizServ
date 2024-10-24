﻿using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace WizServ
{
    public partial class AssignClaim : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        private readonly string file = @"I:\\Datafile\\Control\\Database.CSV";
        public string claim_no;
        public string ma, mb, mc, md, me, mf, mg, mh, mi, mj, mk, ml, mm, mn, mo, mp, mq, mr, ms, mt, mu, mv, mw, mx, my, mz;
        public string maa, mab, mac, mad, mae, maf, mag, mah, mai, maj, mak, mal, mam, man, mao, map, maq, mar, mas, mat, mau, mav, maw, max, may, maz;
        public string mba, mbb, mbc, mbd, mbe, mbf, mbg, mbh, mbi, mbj, mbk, mbl, mbm, mbn, mbo, mbp, mbq, mbr, mbs, mbt;
        public string mbu, mbv, mbw, mbx, oldTech, oldPriority;
        private int loopCount;
        static readonly string key = @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\lanmanserver\parameters";
        public readonly string computerDescription = (string)Registry.GetValue(key, "srvcomment", null);

        public AssignClaim()
        {
            InitializeComponent();
            Icon = image100;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
            AssignTech();
            HideLabels();
            AddPriority();
            label4.Text = label4.Text + " on: " + computerDescription;
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                comboBox2.Select();
            }
        }

        private void comboBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                label33.Visible = true;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label33.Visible = false;
        }

        private void AssignTech()
        {
            comboBox1.Items.Add("COLE");
            comboBox1.Items.Add("DAVID");
            comboBox1.Items.Add("BRETT");
            comboBox1.Items.Add("CONNOR");
            comboBox1.Items.Add("VIC");
            comboBox1.SelectedIndex = 0;
        }

        private void AddPriority()
        {
            comboBox2.Items.Add("$- Paid Rush");
            comboBox2.Items.Add("P- In House Rush (Priority Unit)");
            comboBox2.Items.Add("W- Warranty Unit");
            comboBox2.Items.Add("R- Recall Unit (Priority)");
            comboBox2.Items.Add("M- Music Store Unit (Priority)");
            comboBox2.Items.Add(".  Not Yet Assigned Priority !!");
            comboBox2.Items.Add("N- Normal Claim");
            comboBox2.SelectedIndex = 0;
        }

        private void HideLabels()
        {
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            label12.Visible = false;
            label13.Visible = false;
            label14.Visible = false;
            label15.Visible = false;
            label16.Visible = false;
            label17.Visible = false;
            label18.Visible = false;
            label19.Visible = false;
            label20.Visible = false;
            label21.Visible = false;
            label22.Visible = false;
            label23.Visible = false;
            label24.Visible = false;
            label25.Visible = false;
            label26.Visible = false;
            label27.Visible = false;
            label28.Visible = false;
            label29.Visible = false;
            label30.Visible = false;
            label31.Visible = false;
            label32.Visible = false;
            label33.Visible = false;
            label34.Visible = false;
            label35.Visible = false;
            label36.Visible = false;
            comboBox1.Visible = false;
            comboBox2.Visible = false;
            checkBox1.Visible = false;
        }

        private void ShowLabels()
        {
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            label6.Visible = true;
            label7.Visible = true;
            label8.Visible = true;
            label9.Visible = true;
            label10.Visible = true;
            label11.Visible = true;
            label12.Visible = true;
            label13.Visible = true;
            label14.Visible = true;
            label15.Visible = true;
            label16.Visible = true;
            label17.Visible = true;
            label18.Visible = true;
            label19.Visible = true;
            label20.Visible = true;
            label21.Visible = true;
            label22.Visible = true;
            label23.Visible = true;
            label24.Visible = true;
            label25.Visible = true;
            label26.Visible = true;
            label27.Visible = true;
            label28.Visible = true;
            label28.Visible = true;
            label30.Visible = true;
            label31.Visible = true;
            label32.Visible = true;
            label33.Visible = false;
            label34.Visible = true;
            label35.Visible = true;
            comboBox1.Visible = true;
            comboBox2.Visible = true;
            checkBox1.Visible = true;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var t = textBox1.Text;
                if (t.Length <= 5)
                {
                    MessageBox.Show("Claim Number must be 6 or 7 characters");
                    textBox1.Text = "";
                    textBox1.Select();
                    return;
                }
                if (t.Length >= 8)
                {
                    MessageBox.Show("Claim Number must be 6 or 7 characters");
                    textBox1.Text = "";
                    textBox1.Select();
                    return;
                }
                if (t.Length == 6 || t.Length == 7)
                {
                    label3.Visible = true;
                    label4.Visible = true;
                    label36.Visible = true;
                    label36.Text = t;
                    claim_no = t;
                    label1.Visible = false;
                    label2.Visible = false;
                    label4.Visible = true;
                    textBox1.Visible = false;
                    ShowLabels();
                    GetData();
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
                        if (mi.Length == 4)
                        {
                            mi = "0" + mi;
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
                        mbu = listBU[loopCount].ToUpper();
                        mbv = listBV[loopCount].ToUpper();
                        mbw = listBW[loopCount].ToUpper();
                        mbx = listBX[loopCount].ToUpper();
                        oldPriority = listBG[loopCount].ToUpper();
                        oldTech = listAZ[loopCount].ToUpper();

                        if (mam == "0")
                        {
                            mam = "00000";
                        }
                        if (mam.Length == 4)
                        {
                            mam = "0" + mam;
                        }
                        label5.Text = "Customer / Client: " + md + " " + me;
                        label13.Text = mbj;
                        label12.Text = mm;
                        label11.Text = mo;
                        label10.Text = mp;
                        label15.Text = mj;
                        label17.Text = mae;
                        label18.Text = maf;
                        label19.Text = mau;
                        label20.Text = mav;
                        label22.Text = mc;
                        label24.Text = DateTime.Now.ToShortDateString();
                        label26.Text = mbd;
                        label27.Text = mbl;
                        label29.Text = maz;
             
                        var lft = mbg.Substring(0, 1);
                        switch (lft)                                // Get left letter of priority
                        {
                            case "$":
                                comboBox2.SelectedIndex = 0;
                                break;
                            case "P":
                                comboBox2.SelectedIndex = 1;
                                break;
                            case "W":
                                comboBox2.SelectedIndex = 2;
                                break;
                            case "R":
                                comboBox2.SelectedIndex = 3;
                                break;
                            case "M":
                                comboBox2.SelectedIndex = 4;
                                break;
                            case ".":
                                comboBox2.SelectedIndex = 5;
                                break;
                            case "N":
                                comboBox2.SelectedIndex = 6;
                                break;
                            default:
                                comboBox2.SelectedIndex = 5;
                                break;
                        }
                        var rght = mbg.Substring(1, 1);
                        switch (rght)                                           // Get Ship or Not Ship to customer
                        {
                            case "S":
                                checkBox1.Checked = true;
                                checkBox1.Text = "We are shipping to customer";
                            break;
                            default:
                                checkBox1.Checked = false;
                                checkBox1.Text = "We are NOT shipping to customer";
                                break;
                        }
                        maz = maz.ToUpper();
                        switch (maz)                                            // Get Tech Name
                        {
                            case "COLE":
                                comboBox1.SelectedIndex = 0;
                                break;
                            case "DAVID":
                                comboBox1.SelectedIndex = 1;
                                break;
                            case "BRETT":
                                comboBox1.SelectedIndex = 2;
                                break;
                            case "CONNOR":
                                comboBox1.SelectedIndex = 3;
                                break;
                            case "VIC":
                                comboBox1.SelectedIndex = 4;
                                break;
                            default:
                                comboBox1.SelectedIndex = 1;
                                break;
                        }
                    }
                    loopCount++;
                }
                reader.Close(); // Close the open file
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 548: Sorry an error has occured: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }
    }
}