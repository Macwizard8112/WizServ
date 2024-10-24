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
using System.Threading;
using System.Windows.Forms;
using System.Media;
using Microsoft.Win32;

namespace WizServ
{
    public partial class EnterPartsIntoClaim : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        private readonly string file = @"I:\\Datafile\\Control\\Database.CSV";
        private readonly string Search = @"I:\\Datafile\\Control\\Part_Pri.CSV";
        private readonly string Locate = @"I:\\Datafile\\Control\\Pri.CSV";
        private readonly string Ordered = @"I:\\Datafile\\Control\\Ordered.csv";
        public string claimno, yeardigit, Mex;
        private string mTheNewClaimNum;
        private int loopCount, loop;
        // Get Data Variables
        public string ma, mb, mc, md, me, mf, mg, mh, mi, mj, mk, ml, mm, mn, mo, mp, mq, mr, ms, mt, mu, mv, mw, mx, my, mz;
        public string maa, mab, mac, mad, mae, maf, mag, mah, mai, maj, mak, mal, mam, man, mao, map, maq, mar, mas, mat, mau, mav, maw, max, may, maz;
        public string mba, mbb, mbc, mbd, mbe, mbf, mbg, mbh, mbi, mbj, mbk, mbl, mbm, mbn, mbo, mbp, mbq, mbr, mbs, mbt;
        public string mbu, mbv, mbw, mbx, x;
        // Search String
        public string searchtext;
        // Search String Variables
        public string sma, smb, smc, smd, sme, smf, smg, smh, smi, smj, smk, sml, smm, smn, smo, smp, smq, smr, sms, smt, smu, smv, smw, smx, smy, smz;
        public string smaa, smab, smac, smad, smae, smaf, smag, smah, smai, smaj, smak, smal, smam, sman, smao;
        //
        public string Ssma, Ssmb, Ssmc, Ssmd, Ssme, Ssmf;
        public bool locate;
        public string SelectedText, comma = ",";            // create "," Comma inbetween each variable
        private decimal smc1;
        private decimal sme1;
        private string spacer, TEN;
        public int Max, Max1;
        private string[] array;

        public EnterPartsIntoClaim()
        {
            InitializeComponent();
            Icon = image100;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
            HideLabels();
            GetClaimPrefix();
        }

        private void HideLabels()
        {
            label2.Visible = false;
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
            label37.Visible = false;
            textBox2.Visible = false;
            richTextBox1.Visible = false;
            richTextBox2.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "";
        }

        private void richTextBox2_DoubleClick(object sender, EventArgs e)
        {
            label31.Text = Ssma;
            label32.Text = Ssmb;
            label33.Text = Ssmc;
            label34.Text = Ssmd;
            label35.Text = Ssme;
            label36.Text = Ssmf;
            label37.Text = "Claim: " + textBox1.Text;
            {
                searchtext = textBox2.Text;
                richTextBox1.Text = "";
                locate = false;
                LocateInfo();
                UpdateClaim();
            }
        }


        private void UpdateClaim()
        {

            FindNextIndex();    // Find Max Number, we will add +1 to Max number for new record.

            // Max1 +1 is new Index Number in Ordered.csv file.

            //string path = Ordered;
            var csv = new StringBuilder();
            string Zero = "1";
            string One = Ssma.Trim();
            string Two = claimno.Trim();
            string Three = Ssmb.Trim();
            string Four = Ssmc.Trim();
            string Five = claimno.Trim();
            string Six = Ssme.Trim();
            string Seven = DateTime.Now.ToShortDateString();
            string Eight = DateTime.Now.ToShortDateString();
            string Nine = "Y";
            DialogResult dialogResult = MessageBox.Show("Is Part on B/O ?", "BackOrdered Part", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                TEN = "Y";
            }
            else if (dialogResult == DialogResult.No)
            {
                TEN = "N";
            }
            if (TEN.Length == 0)
            {
                MessageBox.Show("Sorry TEN = 0");
            }
            try
            {
                var newLine = string.Format(Zero + comma + One + comma + Two + comma + Three + comma + Four + comma + Five + comma + Six + comma + Seven + comma + Eight + comma + Nine + comma + TEN + comma + (Max1 + 1) + Environment.NewLine);
                csv.Append(newLine);

                File.AppendAllText(Ordered, csv.ToString());
                button2.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 168: \n" + ex);
            }
        }

        private void richTextBox1_DoubleClick(object sender, EventArgs e)
        {
            SelectedText = richTextBox1.SelectedText;
            locate = true;
            LocateInfo();
        }

        public void GetClaimPrefix()
        {
            var date = DateTime.Now.ToShortDateString();
            var len = date.Length;
            var year = date.Substring((len - 2), 2);
            yeardigit = year.Substring(0, 1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            MainUtilitiesMenu f0 = new MainUtilitiesMenu();
            f0.Show();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Hide();
                MainUtilitiesMenu f0 = new MainUtilitiesMenu();
                f0.Show();
            }
            if (e.KeyCode == Keys.Enter)
            {
                claimno = textBox1.Text;
                label1.Visible = false;
                textBox1.Visible = false;
                GetData();
                ShowLabels();
                textBox2.Select();
            }
        }

        private void ShowLabels()
        {
            label2.Visible = true;
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
            label29.Visible = true;
            label30.Visible = true;
            label31.Visible = true;
            label32.Visible = true;
            label33.Visible = true;
            label34.Visible = true;
            label35.Visible = true;
            label36.Visible = true;
            label37.Visible = true;
            textBox2.Visible = true;
            richTextBox1.Visible = true;
            richTextBox2.Visible = true;
        }

        public void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                searchtext = textBox2.Text;
                richTextBox1.Text = "";
                locate = false;
                LocateInfo();
            }
        }

        private void FindNextIndex()
        {
            x = "0,";
            try
            {
                StreamReader reader = new StreamReader(Ordered, Encoding.GetEncoding("Windows-1252"));   // Ordered.csv
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
                List<string> listM = new List<string>();

                loopCount = 0;

                while (!reader.EndOfStream)
                {
                    var lineRead = reader.ReadLine();
                    var values = lineRead.Split(',');

                    listA.Add(values[0]);       //  QTY             Quantity Added
                    listB.Add(values[1]);       //  Part_Num        Part Number Added
                    listC.Add(values[2]);       //  Claim           Claim Number
                    listD.Add(values[3]);       //  Descr           Part Full Description
                    listE.Add(values[4]);       //  Price_Cust      Customer Cost
                    listF.Add(values[5]);       //  Claim           Claim Number
                    listG.Add(values[6]);       //  Cost_Our        Our Cost of Part
                    listH.Add(values[7]);       //  PartDateUsed    Date Added to Claim
                    listI.Add(values[8]);       //  Purchase Date   Date Part was Purchased
                    listJ.Add(values[9]);       //  In_Claim        Y or N
                    listK.Add(values[10]);      //  Index           Index Number
                    listM.Add(values[11]);      //  Index           Index Number

                    if (listM[loopCount] == "" || listM[loopCount] == null)
                    {
                        listM[loopCount] = "0";
                    }
                    x = x + listM[loopCount] + ",";
                    //array = x.Split(',');
                    //Console.WriteLine("highest is: " + array.Max(c => int.Parse(c)));
                    //Max1 = array.Max(c => int.Parse(c));
                    loopCount++;

                }
                reader.Close(); // Close the open file
                array = x.Split(',');
                array[loopCount + 1] = "0";
                Max1 = array.Max(c => int.Parse(c));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 335: Sorry an error has occured: " + ex.Message);
            }
        }

        public void LocateInfo()
        {
            try
            {
                StreamReader reader = new StreamReader(Locate, Encoding.GetEncoding("Windows-1252"));   // Pri.csv
                String line = reader.ReadLine();

                List<string> listA = new List<string>();
                List<string> listB = new List<string>();
                List<string> listC = new List<string>();
                List<string> listD = new List<string>();
                List<string> listE = new List<string>();
                List<string> listF = new List<string>();


                loopCount = 0;
                loop = 0;
                smao = "";

                while (!reader.EndOfStream)
                {
                    var lineRead = reader.ReadLine();
                    var values = lineRead.Split(',');

                    listA.Add(values[0]);       //  PN          Part Number
                    listB.Add(values[1]);       //  Desc        Description
                    listC.Add(values[2]);       //  Price       Our Cost
                    listD.Add(values[3]);       //  Stock       Number of pieces in stock
                    listE.Add(values[4]);       //  Cost        Customer Cost (Price * 1.3)
                    listF.Add(values[5]);       //  Index       Item Index #

                    sma = listA[loopCount].ToUpper();
                    smb = listB[loopCount].ToUpper();
                    smc = listC[loopCount].ToUpper();
                    smd = listD[loopCount].ToUpper();
                    sme = listE[loopCount].ToUpper();
                    smao = listF[loopCount].ToUpper();

                    switch (sma.Length)
                    {
                        case 7:
                            sma += "\t";
                            break;
                        case 8:
                            sma += "\t";
                            break;
                        case 9:
                            sma += "\t";
                            break;
                        case 10:
                            sma += "\t";
                            break;
                        case 11:
                            sma += "\t";
                            break;
                    }
                    smc1 = Convert.ToDecimal(smc);
                    sme1 = Convert.ToDecimal(sme);
                    switch (smc1.ToString("C2").Length)
                    {
                        case 1:
                            spacer = "   ";
                            break;
                        case 2:
                            spacer = "   ";
                            break;
                        case 3:
                            spacer = "   ";
                            break;
                        case 4:
                            spacer = "   ";
                            break;
                        case 5:
                            spacer = "   ";
                            break;
                        case 6:
                            spacer = "   ";
                            break;
                        case 7:
                            spacer = "   ";
                            break;
                        case 8:
                            spacer = "   ";
                            break;
                        default:
                            spacer = "   ";
                            break;
                    }


                    if (locate == false)
                    {
                        if (smb.Contains(searchtext) || sma.Contains(searchtext))
                        {
                            richTextBox1.Text = richTextBox1.Text + smao + "\t\t" + sma + "\t" + spacer + smc1.ToString("C2") + "\t" + smd + "\t" + spacer + sme1.ToString("C2") + "\t\t" + smb + "\n";
                            loop++;
                            label18.Text = "Found: " + loop.ToString();
                        }
                    }
                    if (locate == true)
                    {
                        if (smao == SelectedText)
                        {
                            richTextBox2.Text = richTextBox2.Text + smao + "\t\t" + sma + "\t" + spacer + smc1.ToString("C2") + "\t" + smd + "\t" + spacer + sme1.ToString("C2") + "\t\t" + smb + "\n";
                            loop++;
                            locate = false;
                            Ssma = sma;
                            Ssmb = smb;
                            Ssmc = smc;
                            Ssmd = smd;
                            Ssme = sme;
                            Ssmf = smao;
                        }
                    }

                    loopCount++;

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

                MessageBox.Show("Error 479: Sorry an error has occured: " + ex.Message);
            }
        }

        public void SearchMatch()
        {
            try
            {
                StreamReader reader = new StreamReader(Search, Encoding.GetEncoding("Windows-1252"));
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

                loopCount = 0;
                loop = 0;

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
                    listAO.Add(values[40]);     //  Index Number
                    

                    sma = listA[loopCount].ToUpper();
                    smb = listB[loopCount].ToUpper();
                    smc = listC[loopCount].ToUpper();
                    smd = listD[loopCount].ToUpper();
                    sme = listE[loopCount].ToUpper();
                    smf = listF[loopCount].ToUpper();
                    smg = listG[loopCount].ToUpper();
                    smh = listH[loopCount].ToUpper();
                    smi = listI[loopCount].ToUpper();
                    smj = listJ[loopCount].ToUpper();
                    smk = listK[loopCount].ToUpper();
                    sml = listL[loopCount].ToUpper();
                    smm = listM[loopCount].ToUpper();
                    smn = listN[loopCount].ToUpper();
                    smo = listO[loopCount].ToUpper();
                    smp = listP[loopCount].ToUpper();
                    smq = listQ[loopCount].ToUpper();
                    smr = listR[loopCount].ToUpper();
                    sms = listS[loopCount].ToUpper();
                    smt = listT[loopCount].ToUpper();
                    smu = listU[loopCount].ToUpper();
                    smv = listV[loopCount].ToUpper();
                    smw = listW[loopCount].ToUpper();
                    smx = listX[loopCount].ToUpper();
                    smy = listY[loopCount].ToUpper();
                    smz = listZ[loopCount].ToUpper();
                    smaa = listAA[loopCount].ToUpper();
                    smab = listAB[loopCount].ToUpper();
                    smac = listAC[loopCount].ToUpper();
                    smad = listAD[loopCount].ToUpper();
                    smae = listAE[loopCount].ToUpper();
                    smaf = listAF[loopCount].ToUpper();
                    smag = listAG[loopCount].ToUpper();
                    smah = listAH[loopCount].ToUpper();
                    smai = listAI[loopCount].ToUpper();
                    smaj = listAJ[loopCount].ToUpper();
                    smak = listAK[loopCount].ToUpper();
                    smal = listAL[loopCount].ToUpper();
                    smam = listAM[loopCount].ToUpper();
                    sman = listAN[loopCount].ToUpper();
                    smao = listAO[loopCount].ToUpper();

                    if (locate == false)
                    {
                        if (smb.Contains(searchtext))
                        {
                            richTextBox1.Text = richTextBox1.Text + smao + "\t" + sma + "\t" + smb + "\n";
                            loop++;
                            label18.Text = "Found: " + loop.ToString();
                        }
                    }
                    if (locate == true)
                    {
                        if (SelectedText == smao)
                        {
                            richTextBox2.Text = richTextBox2.Text + smao + "\t" + sma + "\t" + smb + "\n";
                            locate = false;
                        }    
                    }
                    loopCount++;

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

                MessageBox.Show("Error 667: Sorry an error has occured: " + ex.Message);
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

                    if (claimno.Length >= 7)   // Convert new claim# to Remove the "A" prefix
                    {
                        claimno = claimno.Substring(claimno.Length - 6, 6);
                        mTheNewClaimNum = claimno;
                    }

                    if (claimno.Length == 6 || searchtext == "")
                    {
                        if (mb == claimno || mb == (yeardigit + mb))
                        {
                            label2.Text = "Claim#: " + mb;
                            label10.Text = md + " " + me;
                            label11.Text = mf;
                            label12.Text = mg;
                            label13.Text = mh;
                            label14.Text = mi;
                            label15.Text = mj;
                            label16.Text = mk;
                            label20.Text = mm;
                            label22.Text = mo;
                            label24.Text = mp;
                        }
                    }
                    loopCount++;
                    loop++;
                }
                reader.Close(); // Close the open file
                textBox2.Select();
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

                MessageBox.Show("Error 974: Sorry an error has occured: " + ex.Message);
            }
        }
    }
}
