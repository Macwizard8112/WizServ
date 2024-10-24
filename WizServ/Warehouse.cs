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

namespace WizServ
{
    public partial class Warehouse : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        private string ans;
        private readonly string file = @"I:\\Datafile\\Control\\Database.CSV";
        public string ma, mb, mc, md, me, mf, mg, mh, mi, mj, mk, ml, mm, mn, mo, mp, mq, mr, ms, mt, mu, mv, mw, mx, my, mz;
        public string maa, mab, mac, mad, mae, maf, mag, mah, mai, maj, mak, mal, mam, man, mao, map, maq, mar, mas, mat, mau, mav, maw, max, may, maz;
        public string mba, mbb, mbc, mbd, mbe, mbf, mbg, mbh, mbi, mbj, mbk, mbl, mbm, mbn, mbo, mbp, mbq, mbr, mbs, mbt;
        public string mbu, mbv, mbw, mbx;
        public bool valid, kp, found;
        private int loopCount, loop, keyd, txtcount;
        public string claim_no;
        public int pass = 0;
        static readonly string key = @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\lanmanserver\parameters";
        public readonly string computerDescription = (string)Registry.GetValue(key, "srvcomment", null);

        public Warehouse()
        {
            InitializeComponent();
            timer1.Enabled = true;
            timer1.Interval = 7000;
            timer1.Start();
            label20.Visible = false;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = false;
            Icon = image100;
            label24.Visible = false;
            label23.Visible = false;
            label21.Visible = false;
            label22.Visible = false;
            textBox2.Visible = false;
            ShowLabels();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                label20.Visible = false;
                label21.Visible = false;
                label22.Visible = false;
                label23.Visible = false;
                label24.Visible = false;
                textBox2.Visible = false;
                //textBox1.Text = "000000";
                textBox1.Select();
                textBox1.DeselectAll();
                textBox1.SelectionStart = textBox1.Text.Length;
                textBox1.SelectionLength = 0;
                txtcount = 0;
            }
            if (kp == true)
            {
                label20.Visible = false;
                label21.Visible = false;
                label22.Visible = false;
                label23.Visible = false;
                label24.Visible = false;
                textBox2.Visible = false;
                //textBox1.Text = "000000";
                textBox1.Select();
                textBox1.DeselectAll();
                textBox1.SelectionStart = textBox1.Text.Length;
                textBox1.SelectionLength = 0;
                txtcount = 0;
                kp = false;
            }
        }

        public void UpdateShelfLocation()
        {
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
                                if (split[1] == claim_no)
                                {
                                    split[56] = textBox2.Text;          // Save new Shelf Location
                                    line = String.Join(",", split);
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error 113: \n" + ex);
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
                    MessageBox.Show("Error line 129: \n" + ex);
                }
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox2.TextLength < 2)
                {
                    valid = false;
                    kp = false;
                }
                if (mbe == textBox2.Text)
                {
                    valid = false;
                    kp = false;
                }
                if (textBox2.TextLength == 2)
                {
                    if (mbe != textBox2.Text)
                    {
                        UpdateShelfLocation();
                        valid = true;
                        kp = true;
                    }
                }
                textBox1.Text = "";
            }
            if (e.KeyCode == Keys.Escape)
            {
                if (Version.From == "ClaimsMGTMenu")
                {
                    Hide();
                    MainMenu f2 = new MainMenu();
                    f2.Show();
                }
                if (Version.From == "MainMenu")
                {
                    Hide();
                    MainMenu f2 = new MainMenu();
                    f2.Show();
                }
                else
                {
                    this.Close();
                    //Version.From = "WAREHOUSE";
                    Application.ExitThread();
                    Close();
                }
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)8)
            {
                txtcount--;
            }
            if (e.KeyChar != (char)8)
            {
                txtcount++;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)8)   // Check if BackSpace pressed
            {
                txtcount--;
            }
            if (e.KeyChar != (char)8)
            {
                txtcount++;
            }
            if (e.KeyChar == (char)13)
            {
                txtcount--;
            }
            if (txtcount == 7)
            {
                claim_no = textBox1.Text;
                GetData();
                textBox2.SelectAll();
                txtcount = 0;
            }
        }


        private void ShowLabels()
        {
            label15.Text = "This Program is for finding items && Entering a shelf location";
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtcount <= 5)
                {
                    label20.Visible = true;
                }
                if (txtcount >= 8)
                {
                    label20.Visible = true;
                }
                if (txtcount == 6)
                {
                    claim_no = textBox1.Text;
                    textBox2.SelectAll();
                    GetData();
                    if (found == true)
                    {
                        found = false;
                    }
                    else
                    {
                        label21.Visible = false;
                        label22.Visible = false;
                        textBox2.Visible = false;
                        label23.Visible = false;
                        label24.Visible = true;
                        label24.Text = "                " + textBox1.Text + " Claim number not found !";
                        textBox1.Text = "";
                    }
                }
                if (txtcount == 7)
                {
                    claim_no = textBox1.Text;
                    textBox2.SelectAll();
                    GetData();
                    if (found == true)
                    {
                        found = false;
                    }
                    else
                    {
                        label21.Visible = false;
                        label22.Visible = false;
                        textBox2.Visible = false;
                        label23.Visible = false;
                        label24.Visible = true;
                        label24.Text = "                " + textBox1.Text + " Claim number not found !";
                        textBox1.Text = "";
                    }
                }
                txtcount = 0;
            }
            if (e.KeyCode == Keys.Escape)
            {
                if (Version.From == "ClaimsMGTMenu")
                {
                    Hide();
                    MainMenu f2 = new MainMenu();
                    f2.Show();
                    return;
                }
                if (Version.From == "MainMenu")
                {
                    Hide();
                    MainMenu f2 = new MainMenu();
                    f2.Show();
                    return;
                }
                else
                {
                    this.Close();
                    //Version.From = "WAREHOUSE";
                    Application.ExitThread();
                    Close();
                }
            }
        }

        public void GetData()
        {
            pass = 0;
            label21.Visible = true;
            label22.Visible = true;
            label24.Visible = true;
            label23.Visible = true;
            textBox2.Visible = true;
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
                        found = true;
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
                        mbu = listBU[loopCount].ToUpper();
                        mbv = listBV[loopCount].ToUpper();
                        mbw = listBW[loopCount].ToUpper();
                        mbx = listBX[loopCount].ToUpper();

                        if (listB[loopCount] ==  claim_no)
                        {
                            label21.Text = "Current Location is:  " + mbe;
                            textBox2.Text = mbe;
                            label24.Text = "Brand: " + mm + ",  Model: " + mo + "  Serial: " + mp;
                            label23.Text = "Tech:  " + maz + "  Date Received In: " + mc; ;
                            textBox2.SelectAll();
                        }
                    }
                    
                    loopCount++;
                }
                reader.Close(); // Close the open file
                textBox2.Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 575: Sorry an error has occured: " + ex.Message);
            }
        }


    }
}
