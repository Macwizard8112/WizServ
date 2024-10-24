using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WizServ
{
    public partial class EditPO : Form
    {
        public Image WizLogo4 = Properties.Resources.WizLogo4;
        public Image Box2 = Properties.Resources.Invoice_Templete;
        public Image VOID = Properties.Resources.Void2;
        public Icon image100 = Properties.Resources.WizServ;
        private string PurchOrder = @"I:\\Datafile\\Control\\PurchOrder.CSV";   
        public string CLAIM = Version.Claim;
        public string CLAIM2, CLAIM3;
        public decimal RUNNINGTOTAL;
        private int loopCount, PASS, BUTPASS;
        public string MANF, ADDR, ADDR2, ADDR3, CITY, CSZ, CODE;
        public string PONUM, PARTS, DATEIN, BUYER, BUYER2, ONBO, DATEORD, TECH, WARR, TAXABLE, PHONE, DATEENT, QTY, PARTNUM, COSTEA, EXTENDED;
        public decimal LINE1, LINE2, LINE3, LINE4, LINE5, LINE6, LINE7, LINE8, LINE9, LINE10, LINE11, LINE12, LINE13, LINE14, LINE15;
        public string Index1, Index2, Index3, Index4, Index5, Index6, Index7, Index8, Index9, Index10, Index11, Index12, Index13, Index14, Index15;
        public int Index, i, Tpass;
        public string TECHNICIAN, WARRANTYSTATUS, VOIDED;
        public bool FOUND;

        public EditPO()
        {
            InitializeComponent();
            Icon = image100;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
            pictureBox4.Visible = false;
            timer1.Stop();
            timer1.Interval = 2000;     // 2 Seconds
            label36.Visible = false;
            pictureBox3.Image = VOID;
            pictureBox3.Visible = false;
            CheckPONum();
            label1.Text = "Purchase Oder: " + CLAIM2;
            GetMFG();
            if (FOUND == true)
            {
                LoadData();
                label37.Text = CLAIM + ", " + textBox13.Text + ", Date In: " + DATEIN;
            }
            else
            {
                MessageBox.Show("Claim " + CLAIM + " not found.");
                button1.Visible = false;
                button3.Visible = false;
                button2.PerformClick();
                
            }
        }

        private void CheckPONum()
        {
            var h = CLAIM.Length;
            var j = CLAIM.Substring(h - 3, 3);
            var k = Math.Truncate(Convert.ToDecimal(CLAIM));
            var f = Convert.ToString(k);
            var n = f.Length;
            var a = h - n;
            if (a == 2)
            {
                CLAIM3 = CLAIM;
                CLAIM2 = CLAIM;
            }
        }

        private void LoadData()
        {
            label30.Text = MANF;
            label31.Text = ADDR;
            label32.Text = ADDR2;
            label33.Text = ADDR3;
            label34.Text = CSZ;
            label35.Text = PHONE;
            var z = DateTime.Parse(DATEENT);
            textBox6.Text = z.ToShortDateString();
            textBox6.TextAlign = HorizontalAlignment.Center;
            textBox7.Text = "UPS";
            textBox7.TextAlign = HorizontalAlignment.Center;
            textBox8.Text = "Origin";
            textBox8.TextAlign = HorizontalAlignment.Center;
            textBox9.Text = "OA";
            textBox9.TextAlign = HorizontalAlignment.Center;
            textBox10.Text = BUYER2;
            textBox10.TextAlign = HorizontalAlignment.Center;
            var dateTime = DateTime.Parse(DATEENT);
            textBox11.Text = dateTime.ToShortDateString();
            textBox1.Text = DateTime.Parse(DATEORD).ToShortDateString();
            textBox1.TextAlign = HorizontalAlignment.Center;
            textBox11.TextAlign = HorizontalAlignment.Center;
            textBox12.Text = BUYER;
            textBox12.TextAlign = HorizontalAlignment.Center;
            textBox13.Text = WARR + "-" + TECH;
            textBox13.TextAlign = HorizontalAlignment.Center;
            textBox14.Text = TAXABLE;
            textBox14.TextAlign = HorizontalAlignment.Center;

            label29.Text = "Vendor #: " + CODE;
            if (textBox14.Text.Contains("N"))
            {
                var TOT = LINE1 + LINE2 + LINE3 + LINE4 + LINE5 + LINE6 + LINE7 + LINE8 + LINE9 + LINE10 + LINE11 + LINE12 + LINE13 + LINE14 + LINE15;
                if (TOT.ToString("N").Length == 4)
                {
                    textBox105.Text = "    " + TOT.ToString("N");
                }
                if (TOT.ToString("N").Length == 5)
                {
                    textBox105.Text = "   " + TOT.ToString("N");
                }
                if (TOT.ToString("N").Length == 6)
                {
                    textBox105.Text = "  " + TOT.ToString("N");
                }
                if (TOT.ToString("N").Length == 7)
                {
                    textBox105.Text = " " + TOT.ToString("N");
                }
                if (TOT.ToString("N").Length >= 8)
                {
                    textBox105.Text = TOT.ToString("N");
                }
                textBox106.Text = "0.00";
                if (textBox106.Text == "0.00")
                {
                    textBox106.Text = "    " + "0.00";
                }
                textBox107.Text = "0.00";
                if (textBox107.Text == "0.00")
                {
                    textBox107.Text = "    " + "0.00";
                }
                if (RUNNINGTOTAL.ToString("N").Length == 4)
                {
                    textBox108.Text = "    " + RUNNINGTOTAL.ToString("N");
                }
                if (RUNNINGTOTAL.ToString("N").Length == 5)
                {
                    textBox108.Text = "   " + RUNNINGTOTAL.ToString("N");
                }
                if (RUNNINGTOTAL.ToString("N").Length == 6)
                {
                    textBox108.Text = "  " + RUNNINGTOTAL.ToString("N");
                }
                if (RUNNINGTOTAL.ToString("N").Length == 7)
                {
                    textBox108.Text = " " + RUNNINGTOTAL.ToString("N");
                }
                if (RUNNINGTOTAL.ToString("N").Length >= 8)
                {
                    textBox108.Text = RUNNINGTOTAL.ToString("N");
                }
            }
            if (textBox14.Text.Contains("Y"))
            {
                var TOT = LINE1 + LINE2 + LINE3 + LINE4 + LINE5 + LINE6 + LINE7 + LINE8 + LINE9 + LINE10 + LINE11 + LINE12 + LINE13 + LINE14 + LINE15;
                if (TOT.ToString("N").Length == 4)
                {
                    textBox106.Text = "    " + TOT.ToString("N");
                }
                if (TOT.ToString("N").Length == 5)
                {
                    textBox106.Text = "   " + TOT.ToString("N");
                }
                if (TOT.ToString("N").Length == 6)
                {
                    textBox106.Text = "  " + TOT.ToString("N");
                }
                if (TOT.ToString("N").Length == 7)
                {
                    textBox106.Text = " " + TOT.ToString("N");
                }
                if (TOT.ToString("N").Length >= 8)
                {
                    textBox106.Text = TOT.ToString("N");
                }
                textBox107.Text = (RUNNINGTOTAL * .0895m).ToString("N");
                var h = RUNNINGTOTAL * .0895m;
                textBox108.Text = RUNNINGTOTAL + h.ToString("N");
            }
        }

        public static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        private void EditPO_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = resizeImage(WizLogo4, new Size(155, 141));
        }

        public void GetMFG()
        {
            PASS = 0;
            try
            {
                StreamReader reader = new StreamReader(PurchOrder, Encoding.GetEncoding("Windows-1252"));
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

                loopCount = 0;
                RUNNINGTOTAL = 0.00m;

                while (!reader.EndOfStream)
                {
                    var lineRead = reader.ReadLine();
                    var values = lineRead.Split(',');

                    listA.Add(values[0]);       //  MFG             Manufacturer
                    listB.Add(values[1]);       //  Addr            Address, City, State
                    listC.Add(values[2]);       //  Date IN
                    listD.Add(values[3]);       //  
                    listE.Add(values[4]);       //  CSZ             City, State, Zip
                    listF.Add(values[5]);       //  
                    listG.Add(values[6]);       //  Tech            Tech Name
                    listH.Add(values[7]);       // 
                    listI.Add(values[8]);       //  
                    listJ.Add(values[9]);       //  
                    listK.Add(values[10]);      //  
                    listL.Add(values[11]);      //  
                    listM.Add(values[12]);      //  
                    listN.Add(values[13]);      //  
                    listO.Add(values[14]);      //  
                    listP.Add(values[15]);      //  
                    listQ.Add(values[16]);      //  
                    listR.Add(values[17]);      //  
                    listS.Add(values[18]);      //  
                    listT.Add(values[19]);      //
                    listU.Add(values[20]);      //  Index Number

                    var itms = listA[loopCount];
                    var index_no = listU[loopCount];
                    TECHNICIAN = listG[loopCount];
                    WARRANTYSTATUS = listH[loopCount];

                    if (listA[loopCount].Contains(CLAIM))
                    {
                        if (listA[loopCount].Contains("V"))
                        {
                            VOIDED = "VOID";
                            label38.Text = VOIDED;
                            pictureBox3.Visible = true;
                            pictureBox3.Image = VOID;
                            CLAIM3 = CLAIM + "V";
                            label1.Text = "Purchase Oder: " + CLAIM;
                            label36.Visible = true;
                            label36.Text = "VOID VOID VOID VOID VOID VOID VOID VOID";
                            label37.Text = CLAIM + ", " + textBox13.Text + ", Date In: " + DATEIN;
                        }
                        FOUND = true;
                        CLAIM3 = CLAIM;
                        PONUM = listA[loopCount];
                        PARTS = listB[loopCount];
                        DATEIN = listC[loopCount];
                        BUYER = listD[loopCount];
                        if (BUYER == "David Reynics")
                        {
                            BUYER2 = "DBR";
                        }
                        if (BUYER.Contains("Cole"))
                        {
                            BUYER2 = "CH";
                        }
                        if (BUYER.Contains("Lyn"))
                        {
                            BUYER2 = "LW";
                        }
                        ONBO = listE[loopCount];
                        DATEORD = listF[loopCount];
                        TECH = listG[loopCount];
                        WARR = listH[loopCount];
                        TAXABLE = listI[loopCount];
                        MANF = listJ[loopCount];
                        ADDR = listK[loopCount];
                        ADDR2 = listL[loopCount];
                        ADDR3 = listM[loopCount];
                        //CITY = listN[loopCount];
                        CSZ = listN[loopCount];
                        CODE = listO[loopCount];
                        PHONE = listP[loopCount];
                        DATEENT = listQ[loopCount];
                        QTY = listR[loopCount];
                        PARTNUM = listS[loopCount];
                        COSTEA = listT[loopCount];

                        PASS++;
                        if (PASS == 1)
                        {
                            Index++;
                            Index1 = index_no;
                            textBox15.Text = QTY;
                            textBox16.Text = PARTNUM;
                            textBox17.Text = PARTS;
                            var s0 = Convert.ToDecimal(COSTEA);
                            if (s0.ToString("N").Length == 4)
                            {
                                textBox18.Text = "   " + s0.ToString("N");
                            }
                            if (s0.ToString("N").Length == 5)
                            {
                                textBox18.Text = "  " + s0.ToString("N");
                            }
                            if (s0.ToString("N").Length == 6)
                            {
                                textBox18.Text = "  " + s0.ToString("N");
                            }
                            if (s0.ToString("N").Length == 7)
                            {
                                textBox18.Text = " " + s0.ToString("N");
                            }
                            textBox19.Text = ONBO;
                            textBox19.TextAlign = HorizontalAlignment.Center;
                            var d0 = Convert.ToDecimal(QTY);
                            var d1 = Convert.ToDecimal(COSTEA);
                            RUNNINGTOTAL = RUNNINGTOTAL + (d0 * d1);
                            LINE1 = d0 * d1;
                            EXTENDED = (d0 * d1).ToString("N");
                            if (EXTENDED.Length == 4)
                            {
                                textBox20.Text = "    " + EXTENDED;
                            }
                            if (EXTENDED.Length == 5)
                            {
                                textBox20.Text = "   " + EXTENDED;
                            }
                            if (EXTENDED.Length == 6)
                            {
                                textBox20.Text = "  " + EXTENDED;
                            }
                            if (EXTENDED.Length == 7)
                            {
                                textBox20.Text = " " + EXTENDED;
                            }
                            if (EXTENDED.Length >= 8)
                            {
                                textBox20.Text = EXTENDED;
                            }
                        }
                        if (PASS == 2)
                        {
                            Index++;
                            Index2 = index_no;
                            textBox26.Text = QTY;
                            textBox24.Text = PARTS;
                            var s0 = Convert.ToDecimal(COSTEA);
                            if (s0.ToString("N").Length == 4)
                            {
                                textBox23.Text = "   " + s0.ToString("N");
                            }
                            if (s0.ToString("N").Length == 5)
                            {
                                textBox23.Text = "  " + s0.ToString("N");
                            }
                            if (s0.ToString("N").Length == 6)
                            {
                                textBox23.Text = "  " + s0.ToString("N");
                            }
                            if (s0.ToString("N").Length == 7)
                            {
                                textBox23.Text = " " + s0.ToString("N");
                            }
                            textBox25.Text = PARTNUM;
                            textBox22.Text = ONBO;
                            textBox22.TextAlign = HorizontalAlignment.Center;
                            var d0 = Convert.ToDecimal(QTY);
                            var d1 = Convert.ToDecimal(COSTEA);
                            RUNNINGTOTAL = RUNNINGTOTAL + (d0 * d1);
                            LINE2 = d0 * d1;
                            EXTENDED = (d0 * d1).ToString("N");
                            if (EXTENDED.Length == 4)
                            {
                                textBox21.Text = "    " + EXTENDED;
                            }
                            if (EXTENDED.Length == 5)
                            {
                                textBox21.Text = "   " + EXTENDED;
                            }
                            if (EXTENDED.Length == 6)
                            {
                                textBox21.Text = "  " + EXTENDED;
                            }
                            if (EXTENDED.Length == 7)
                            {
                                textBox21.Text = " " + EXTENDED;
                            }
                            if (EXTENDED.Length >= 8)
                            {
                                textBox21.Text = EXTENDED;
                            }
                        }
                        if (PASS == 3)
                        {
                            Index++;
                            Index3 = index_no;
                            textBox32.Text = QTY;
                            textBox30.Text = PARTS;
                            var s0 = Convert.ToDecimal(COSTEA);
                            if (s0.ToString("N").Length == 4)
                            {
                                textBox29.Text = "   " + s0.ToString("N");
                            }
                            if (s0.ToString("N").Length == 5)
                            {
                                textBox29.Text = "  " + s0.ToString("N");
                            }
                            if (s0.ToString("N").Length == 6)
                            {
                                textBox29.Text = " " + s0.ToString("N");
                            }
                            if (s0.ToString("N").Length == 7)
                            {
                                textBox29.Text = "" + s0.ToString("N");
                            }
                            textBox31.Text = PARTNUM;
                            textBox28.Text = ONBO;
                            textBox28.TextAlign = HorizontalAlignment.Center;
                            var d0 = Convert.ToDecimal(QTY);
                            var d1 = Convert.ToDecimal(COSTEA);
                            RUNNINGTOTAL = RUNNINGTOTAL + (d0 * d1);
                            LINE3 = d0 * d1;
                            EXTENDED = (d0 * d1).ToString("N");
                            if (EXTENDED.Length == 4)
                            {
                                textBox27.Text = "    " + s0.ToString("N");
                            }
                            if (EXTENDED.Length == 5)
                            {
                                textBox27.Text = "   " + s0.ToString("N");
                            }
                            if (EXTENDED.Length == 6)
                            {
                                textBox27.Text = "  " + EXTENDED;
                            }
                            if (EXTENDED.Length == 7)
                            {
                                textBox27.Text = " " + EXTENDED;
                            }
                            if (EXTENDED.Length >= 8)
                            {
                                textBox27.Text = EXTENDED;
                            }
                        }
                        if (PASS == 4)
                        {
                            Index++;
                            Index4 = index_no;
                            textBox38.Text = QTY;
                            textBox36.Text = PARTS;
                            var s0 = Convert.ToDecimal(COSTEA);
                            if (s0.ToString("N").Length == 4)
                            {
                                textBox35.Text = "   " + s0.ToString("N");
                            }
                            if (s0.ToString("N").Length == 5)
                            {
                                textBox35.Text = "  " + s0.ToString("N");
                            }
                            if (s0.ToString("N").Length == 6)
                            {
                                textBox35.Text = "  " + s0.ToString("N");
                            }
                            if (s0.ToString("N").Length == 7)
                            {
                                textBox35.Text = " " + s0.ToString("N");
                            }
                            textBox37.Text = PARTNUM;
                            textBox34.Text = ONBO;
                            textBox34.TextAlign = HorizontalAlignment.Center;
                            var d0 = Convert.ToDecimal(QTY);
                            var d1 = Convert.ToDecimal(COSTEA);
                            RUNNINGTOTAL = RUNNINGTOTAL + (d0 * d1);
                            LINE4 = d0 * d1;
                            EXTENDED = (d0 * d1).ToString("N");
                            if (EXTENDED.Length == 4)
                            {
                                textBox33.Text = "    " + EXTENDED;
                            }
                            if (EXTENDED.Length == 5)
                            {
                                textBox33.Text = "   " + EXTENDED;
                            }
                            if (EXTENDED.Length == 6)
                            {
                                textBox33.Text = "  " + EXTENDED;
                            }
                            if (EXTENDED.Length == 7)
                            {
                                textBox33.Text = " " + EXTENDED;
                            }
                            if (EXTENDED.Length >= 8)
                            {
                                textBox33.Text = EXTENDED;
                            }
                        }
                        if (PASS == 5)
                        {
                            Index++;
                            Index5 = index_no;
                            textBox44.Text = QTY;
                            textBox43.Text = PARTNUM;
                            textBox42.Text = PARTS;
                            var s0 = Convert.ToDecimal(COSTEA);
                            if (s0.ToString("N").Length == 4)
                            {
                                textBox41.Text = "   " + s0.ToString("N");
                            }
                            if (s0.ToString("N").Length == 5)
                            {
                                textBox41.Text = "  " + s0.ToString("N");
                            }
                            if (s0.ToString("N").Length == 6)
                            {
                                textBox41.Text = "  " + s0.ToString("N");
                            }
                            if (s0.ToString("N").Length == 7)
                            {
                                textBox41.Text = " " + s0.ToString("N");
                            }
                            textBox40.Text = ONBO;
                            textBox40.TextAlign = HorizontalAlignment.Center;
                            var d0 = Convert.ToDecimal(QTY);
                            var d1 = Convert.ToDecimal(COSTEA);
                            RUNNINGTOTAL = RUNNINGTOTAL + (d0 * d1);
                            LINE5 = d0 * d1;
                            EXTENDED = (d0 * d1).ToString("N");
                            if (EXTENDED.Length == 4)
                            {
                                textBox39.Text = "    " + EXTENDED;
                            }
                            if (EXTENDED.Length == 5)
                            {
                                textBox39.Text = "   " + EXTENDED;
                            }
                            if (EXTENDED.Length == 6)
                            {
                                textBox39.Text = "  " + EXTENDED;
                            }
                            if (EXTENDED.Length == 7)
                            {
                                textBox39.Text = " " + EXTENDED;
                            }
                            if (EXTENDED.Length >= 8)
                            {
                                textBox39.Text = EXTENDED;
                            }
                        }
                        if (PASS == 6)
                        {
                            Index++;
                            Index6 = index_no;
                            textBox50.Text = QTY;
                            textBox49.Text = PARTNUM;
                            textBox48.Text = PARTS;
                            var s0 = Convert.ToDecimal(COSTEA);
                            if (s0.ToString("N").Length == 4)
                            {
                                textBox47.Text = "   " + s0.ToString("N");
                            }
                            if (s0.ToString("N").Length == 5)
                            {
                                textBox47.Text = "  " + s0.ToString("N");
                            }
                            if (s0.ToString("N").Length == 6)
                            {
                                textBox47.Text = "  " + s0.ToString("N");
                            }
                            if (s0.ToString("N").Length == 7)
                            {
                                textBox47.Text = " " + s0.ToString("N");
                            }
                            textBox46.Text = ONBO;
                            textBox46.TextAlign = HorizontalAlignment.Center;
                            var d0 = Convert.ToDecimal(QTY);
                            var d1 = Convert.ToDecimal(COSTEA);
                            RUNNINGTOTAL = RUNNINGTOTAL + (d0 * d1);
                            LINE6 = d0 * d1;
                            EXTENDED = (d0 * d1).ToString("N");
                            if (EXTENDED.Length == 4)
                            {
                                textBox45.Text = "    " + EXTENDED;
                            }
                            if (EXTENDED.Length == 5)
                            {
                                textBox45.Text = "   " + EXTENDED;
                            }
                            if (EXTENDED.Length == 6)
                            {
                                textBox45.Text = "  " + EXTENDED;
                            }
                            if (EXTENDED.Length == 7)
                            {
                                textBox45.Text = " " + EXTENDED;
                            }
                            if (EXTENDED.Length >= 8)
                            {
                                textBox45.Text = EXTENDED;
                            }
                        }
                        if (PASS == 7)
                        {
                            Index++;
                            Index7 = index_no;
                            textBox56.Text = QTY;
                            textBox55.Text = PARTNUM;
                            textBox54.Text = PARTS;
                            var s0 = Convert.ToDecimal(COSTEA);
                            if (s0.ToString("N").Length == 4)
                            {
                                textBox53.Text = "   " + s0.ToString("N");
                            }
                            if (s0.ToString("N").Length == 5)
                            {
                                textBox53.Text = "  " + s0.ToString("N");
                            }
                            if (s0.ToString("N").Length == 6)
                            {
                                textBox53.Text = "  " + s0.ToString("N");
                            }
                            if (s0.ToString("N").Length == 7)
                            {
                                textBox53.Text = " " + s0.ToString("N");
                            }
                            textBox52.Text = ONBO;
                            textBox52.TextAlign = HorizontalAlignment.Center;
                            var d0 = Convert.ToDecimal(QTY);
                            var d1 = Convert.ToDecimal(COSTEA);
                            RUNNINGTOTAL = RUNNINGTOTAL + (d0 * d1);
                            LINE7 = d0 * d1;
                            EXTENDED = (d0 * d1).ToString("N");
                            if (EXTENDED.Length == 4)
                            {
                                textBox51.Text = "    " + EXTENDED;
                            }
                            if (EXTENDED.Length == 5)
                            {
                                textBox51.Text = "   " + EXTENDED;
                            }
                            if (EXTENDED.Length == 6)
                            {
                                textBox51.Text = "  " + EXTENDED;
                            }
                            if (EXTENDED.Length == 7)
                            {
                                textBox51.Text = " " + EXTENDED;
                            }
                            if (EXTENDED.Length >= 8)
                            {
                                textBox51.Text = EXTENDED;
                            }
                        }
                        if (PASS == 8)
                        {
                            Index++;
                            Index8 = index_no;
                            textBox62.Text = QTY;
                            textBox61.Text = PARTNUM;
                            textBox60.Text = PARTS;
                            var s0 = Convert.ToDecimal(COSTEA);
                            if (s0.ToString("N").Length == 4)
                            {
                                textBox59.Text = "   " + s0.ToString("N");
                            }
                            if (s0.ToString("N").Length == 5)
                            {
                                textBox59.Text = "  " + s0.ToString("N");
                            }
                            if (s0.ToString("N").Length == 6)
                            {
                                textBox59.Text = "  " + s0.ToString("N");
                            }
                            if (s0.ToString("N").Length == 7)
                            {
                                textBox59.Text = " " + s0.ToString("N");
                            }
                            textBox58.Text = ONBO;
                            textBox58.TextAlign = HorizontalAlignment.Center;
                            var d0 = Convert.ToDecimal(QTY);
                            var d1 = Convert.ToDecimal(COSTEA);
                            RUNNINGTOTAL = RUNNINGTOTAL + (d0 * d1);
                            LINE8 = d0 * d1;
                            EXTENDED = (d0 * d1).ToString("N");
                            if (EXTENDED.Length == 4)
                            {
                                textBox57.Text = "    " + EXTENDED;
                            }
                            if (EXTENDED.Length == 5)
                            {
                                textBox57.Text = "   " + EXTENDED;
                            }
                            if (EXTENDED.Length == 6)
                            {
                                textBox57.Text = "  " + EXTENDED;
                            }
                            if (EXTENDED.Length == 7)
                            {
                                textBox57.Text = " " + EXTENDED;
                            }
                            if (EXTENDED.Length >= 8)
                            {
                                textBox57.Text = EXTENDED;
                            }
                        }
                        if (PASS == 9)
                        {
                            Index++;
                            Index9 = index_no;
                            textBox68.Text = QTY;
                            textBox67.Text = PARTNUM;
                            textBox66.Text = PARTS;
                            var s0 = Convert.ToDecimal(COSTEA);
                            if (s0.ToString("N").Length == 4)
                            {
                                textBox65.Text = "   " + s0.ToString("N");
                            }
                            if (s0.ToString("N").Length == 5)
                            {
                                textBox65.Text = "  " + s0.ToString("N");
                            }
                            if (s0.ToString("N").Length == 6)
                            {
                                textBox65.Text = "  " + s0.ToString("N");
                            }
                            if (s0.ToString("N").Length == 7)
                            {
                                textBox65.Text = " " + s0.ToString("N");
                            }
                            textBox64.Text = ONBO;
                            textBox64.TextAlign = HorizontalAlignment.Center;
                            var d0 = Convert.ToDecimal(QTY);
                            var d1 = Convert.ToDecimal(COSTEA);
                            RUNNINGTOTAL = RUNNINGTOTAL + (d0 * d1);
                            LINE9 = d0 * d1;
                            EXTENDED = (d0 * d1).ToString("N");
                            if (EXTENDED.Length == 4)
                            {
                                textBox63.Text = "    " + EXTENDED;
                            }
                            if (EXTENDED.Length == 5)
                            {
                                textBox63.Text = "   " + EXTENDED;
                            }
                            if (EXTENDED.Length == 6)
                            {
                                textBox63.Text = "  " + EXTENDED;
                            }
                            if (EXTENDED.Length == 7)
                            {
                                textBox63.Text = " " + EXTENDED;
                            }
                            if (EXTENDED.Length >= 8)
                            {
                                textBox63.Text = EXTENDED;
                            }
                        }
                        if (PASS == 10)
                        {
                            Index++;
                            Index10 = index_no;
                            textBox74.Text = QTY;
                            textBox73.Text = PARTNUM;
                            textBox72.Text = PARTS;
                            var s0 = Convert.ToDecimal(COSTEA);
                            if (s0.ToString("N").Length == 4)
                            {
                                textBox71.Text = "   " + s0.ToString("N");
                            }
                            if (s0.ToString("N").Length == 5)
                            {
                                textBox71.Text = "  " + s0.ToString("N");
                            }
                            if (s0.ToString("N").Length == 6)
                            {
                                textBox71.Text = "  " + s0.ToString("N");
                            }
                            if (s0.ToString("N").Length == 7)
                            {
                                textBox71.Text = " " + s0.ToString("N");
                            }
                            textBox70.Text = ONBO;
                            textBox70.TextAlign = HorizontalAlignment.Center;
                            var d0 = Convert.ToDecimal(QTY);
                            var d1 = Convert.ToDecimal(COSTEA);
                            RUNNINGTOTAL = RUNNINGTOTAL + (d0 * d1);
                            LINE10 = d0 * d1;
                            EXTENDED = (d0 * d1).ToString("N");
                            if (EXTENDED.Length == 4)
                            {
                                textBox69.Text = "    " + EXTENDED;
                            }
                            if (EXTENDED.Length == 5)
                            {
                                textBox69.Text = "   " + EXTENDED;
                            }
                            if (EXTENDED.Length == 6)
                            {
                                textBox69.Text = "  " + EXTENDED;
                            }
                            if (EXTENDED.Length == 7)
                            {
                                textBox69.Text = " " + EXTENDED;
                            }
                            if (EXTENDED.Length >= 8)
                            {
                                textBox69.Text = EXTENDED;
                            }
                        }
                        if (PASS == 11)
                        {
                            Index++;
                            Index11 = index_no;
                            textBox80.Text = QTY;
                            textBox79.Text = PARTNUM;
                            textBox78.Text = PARTS;
                            var s0 = Convert.ToDecimal(COSTEA);
                            if (s0.ToString("N").Length == 4)
                            {
                                textBox77.Text = "   " + s0.ToString("N");
                            }
                            if (s0.ToString("N").Length == 5)
                            {
                                textBox77.Text = "  " + s0.ToString("N");
                            }
                            if (s0.ToString("N").Length == 6)
                            {
                                textBox77.Text = "  " + s0.ToString("N");
                            }
                            if (s0.ToString("N").Length == 7)
                            {
                                textBox77.Text = " " + s0.ToString("N");
                            }
                            textBox76.Text = ONBO;
                            textBox76.TextAlign = HorizontalAlignment.Center;
                            var d0 = Convert.ToDecimal(QTY);
                            var d1 = Convert.ToDecimal(COSTEA);
                            RUNNINGTOTAL = RUNNINGTOTAL + (d0 * d1);
                            LINE11 = d0 * d1;
                            EXTENDED = (d0 * d1).ToString("N");
                            if (EXTENDED.Length == 4)
                            {
                                textBox75.Text = "    " + EXTENDED;
                            }
                            if (EXTENDED.Length == 5)
                            {
                                textBox75.Text = "   " + EXTENDED;
                            }
                            if (EXTENDED.Length == 6)
                            {
                                textBox75.Text = "  " + EXTENDED;
                            }
                            if (EXTENDED.Length == 7)
                            {
                                textBox75.Text = " " + EXTENDED;
                            }
                            if (EXTENDED.Length >= 8)
                            {
                                textBox75.Text = EXTENDED;
                            }
                        }
                        if (PASS == 12)
                        {
                            Index++;
                            Index12 = index_no;
                            textBox92.Text = QTY;
                            textBox91.Text = PARTNUM;
                            textBox90.Text = PARTS;
                            var s0 = Convert.ToDecimal(COSTEA);
                            if (s0.ToString("N").Length == 4)
                            {
                                textBox89.Text = "   " + s0.ToString("N");
                            }
                            if (s0.ToString("N").Length == 5)
                            {
                                textBox89.Text = "  " + s0.ToString("N");
                            }
                            if (s0.ToString("N").Length == 6)
                            {
                                textBox89.Text = "  " + s0.ToString("N");
                            }
                            if (s0.ToString("N").Length == 7)
                            {
                                textBox89.Text = " " + s0.ToString("N");
                            }
                            textBox88.Text = ONBO;
                            textBox88.TextAlign = HorizontalAlignment.Center;
                            var d0 = Convert.ToDecimal(QTY);
                            var d1 = Convert.ToDecimal(COSTEA);
                            RUNNINGTOTAL = RUNNINGTOTAL + (d0 * d1);
                            LINE12 = d0 * d1;
                            EXTENDED = (d0 * d1).ToString("N");
                            if (EXTENDED.Length == 4)
                            {
                                textBox87.Text = "    " + EXTENDED;
                            }
                            if (EXTENDED.Length == 5)
                            {
                                textBox87.Text = "   " + EXTENDED;
                            }
                            if (EXTENDED.Length == 6)
                            {
                                textBox87.Text = "  " + EXTENDED;
                            }
                            if (EXTENDED.Length == 7)
                            {
                                textBox87.Text = " " + EXTENDED;
                            }
                            if (EXTENDED.Length >= 8)
                            {
                                textBox87.Text = EXTENDED;
                            }
                        }
                        if (PASS == 13)
                        {
                            Index++;
                            Index13 = index_no;
                            textBox86.Text = QTY;
                            textBox85.Text = PARTNUM;
                            textBox84.Text = PARTS;
                            var s0 = Convert.ToDecimal(COSTEA);
                            if (s0.ToString("N").Length == 4)
                            {
                                textBox83.Text = "   " + s0.ToString("N");
                            }
                            if (s0.ToString("N").Length == 5)
                            {
                                textBox83.Text = "  " + s0.ToString("N");
                            }
                            if (s0.ToString("N").Length == 6)
                            {
                                textBox83.Text = "  " + s0.ToString("N");
                            }
                            if (s0.ToString("N").Length == 7)
                            {
                                textBox83.Text = " " + s0.ToString("N");
                            }
                            textBox82.Text = ONBO;
                            textBox82.TextAlign = HorizontalAlignment.Center;
                            var d0 = Convert.ToDecimal(QTY);
                            var d1 = Convert.ToDecimal(COSTEA);
                            RUNNINGTOTAL = RUNNINGTOTAL + (d0 * d1);
                            LINE13 = d0 * d1;
                            EXTENDED = (d0 * d1).ToString("N");
                            if (EXTENDED.Length == 4)
                            {
                                textBox81.Text = "    " + EXTENDED;
                            }
                            if (EXTENDED.Length == 5)
                            {
                                textBox81.Text = "   " + EXTENDED;
                            }
                            if (EXTENDED.Length == 6)
                            {
                                textBox81.Text = "  " + EXTENDED;
                            }
                            if (EXTENDED.Length == 7)
                            {
                                textBox81.Text = " " + EXTENDED;
                            }
                            if (EXTENDED.Length >= 8)
                            {
                                textBox81.Text = EXTENDED;
                            }
                        }
                        if (PASS == 14)
                        {
                            Index++;
                            Index14 = index_no;
                            textBox104.Text = QTY;
                            textBox103.Text = PARTNUM;
                            textBox102.Text = PARTS;
                            var s0 = Convert.ToDecimal(COSTEA);
                            if (s0.ToString("N").Length == 4)
                            {
                                textBox101.Text = "   " + s0.ToString("N");
                            }
                            if (s0.ToString("N").Length == 5)
                            {
                                textBox101.Text = "  " + s0.ToString("N");
                            }
                            if (s0.ToString("N").Length == 6)
                            {
                                textBox101.Text = "  " + s0.ToString("N");
                            }
                            if (s0.ToString("N").Length == 7)
                            {
                                textBox101.Text = " " + s0.ToString("N");
                            }
                            textBox100.Text = ONBO;
                            textBox100.TextAlign = HorizontalAlignment.Center;
                            var d0 = Convert.ToDecimal(QTY);
                            var d1 = Convert.ToDecimal(COSTEA);
                            RUNNINGTOTAL = RUNNINGTOTAL + (d0 * d1);
                            LINE14 = d0 * d1;
                            EXTENDED = (d0 * d1).ToString("N");
                            if (EXTENDED.Length == 4)
                            {
                                textBox99.Text = "    " + EXTENDED;
                            }
                            if (EXTENDED.Length == 5)
                            {
                                textBox99.Text = "   " + EXTENDED;
                            }
                            if (EXTENDED.Length == 6)
                            {
                                textBox99.Text = "  " + EXTENDED;
                            }
                            if (EXTENDED.Length == 7)
                            {
                                textBox99.Text = " " + EXTENDED;
                            }
                            if (EXTENDED.Length >= 8)
                            {
                                textBox99.Text = EXTENDED;
                            }
                        }
                        if (PASS == 15)
                        {
                            Index++;
                            Index15 = index_no;
                            textBox98.Text = QTY;
                            textBox97.Text = PARTNUM;
                            textBox96.Text = PARTS;
                            var s0 = Convert.ToDecimal(COSTEA);
                            if (s0.ToString("N").Length == 4)
                            {
                                textBox95.Text = "   " + s0.ToString("N");
                            }
                            if (s0.ToString("N").Length == 5)
                            {
                                textBox95.Text = "  " + s0.ToString("N");
                            }
                            if (s0.ToString("N").Length == 6)
                            {
                                textBox95.Text = "  " + s0.ToString("N");
                            }
                            if (s0.ToString("N").Length == 7)
                            {
                                textBox95.Text = " " + s0.ToString("N");
                            }
                            textBox94.Text = ONBO;
                            textBox94.TextAlign = HorizontalAlignment.Center;
                            var d0 = Convert.ToDecimal(QTY);
                            var d1 = Convert.ToDecimal(COSTEA);
                            RUNNINGTOTAL = RUNNINGTOTAL + (d0 * d1);
                            LINE15 = d0 * d1;
                            EXTENDED = (d0 * d1).ToString("N");
                            if (EXTENDED.Length == 4)
                            {
                                textBox93.Text = "    " + EXTENDED;
                            }
                            if (EXTENDED.Length == 5)
                            {
                                textBox93.Text = "   " + EXTENDED;
                            }
                            if (EXTENDED.Length == 6)
                            {
                                textBox93.Text = "  " + EXTENDED;
                            }
                            if (EXTENDED.Length == 7)
                            {
                                textBox93.Text = " " + EXTENDED;
                            }
                            if (EXTENDED.Length >= 8)
                            {
                                textBox93.Text = EXTENDED;
                            }
                        }
                    }
                    loopCount++;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error line 1057: \n" + ex);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            MainUtilitiesMenu f0 = new MainUtilitiesMenu();
            f0.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            BUTPASS++;
            if (BUTPASS == 1)
            {
                pictureBox3.Visible = true;
                pictureBox3.Image = VOID;
                CLAIM += "V";
                label1.Text = "Purchase Oder: " + CLAIM;
                label36.Visible = true;
                label36.Text = "VOID VOID VOID VOID VOID VOID VOID VOID";
                label37.Text = CLAIM + ", " + textBox13.Text + ", Date In: " + DATEIN;
            }
            if (BUTPASS == 2)
            {
                pictureBox3.Visible = false;
                pictureBox3.Image = VOID;
                CLAIM = CLAIM3;
                label1.Text = "Purchase Oder: " + CLAIM;
                label36.Visible = false;
                label36.Text = ".";
                BUTPASS = 0;
            }
        }

        private void textBox98_KeyDown(object sender, KeyEventArgs e)   // Line 15
        {
            if (e.KeyCode == Keys.Enter)
            {
                var d0 = Convert.ToDecimal(textBox98.Text);
                var d1 = Convert.ToDecimal(textBox95.Text);
                EXTENDED = (d0 * d1).ToString("N");
                LINE15 = d0 * d1;
                if (EXTENDED.Length == 4)
                {
                    textBox93.Text = "     " + EXTENDED;
                }
                if (EXTENDED.Length == 5)
                {
                    textBox93.Text = "   " + EXTENDED;
                }
                if (EXTENDED.Length == 6)
                {
                    textBox93.Text = "  " + EXTENDED;
                }
                if (EXTENDED.Length == 7)
                {
                    textBox93.Text = " " + EXTENDED;
                }
                if (EXTENDED.Length >= 8)
                {
                    textBox93.Text = EXTENDED;
                }

                if (textBox14.Text.Contains("N"))
                {
                    var TOT = LINE1 + LINE2 + LINE3 + LINE4 + LINE5 + LINE6 + LINE7 + LINE8 + LINE9 + LINE10 + LINE11 + LINE12 + LINE13 + LINE14 + LINE15;
                    if (TOT.ToString("N").Length == 4)
                    {
                        textBox105.Text = "    " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 5)
                    {
                        textBox105.Text = "   " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 6)
                    {
                        textBox105.Text = "  " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 7)
                    {
                        textBox105.Text = " " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length >= 8)
                    {
                        textBox105.Text = TOT.ToString("N");
                    }
                    textBox106.Text = "0.00";
                    if (textBox106.Text == "0.00")
                    {
                        textBox106.Text = "    " + "0.00";
                    }
                    textBox107.Text = "0.00";
                    if (textBox107.Text == "0.00")
                    {
                        textBox107.Text = "    " + "0.00";
                    }
                    textBox108.Text = TOT.ToString("N");
                }
                if (textBox14.Text.Contains("Y"))
                {
                    var TOT = LINE1 + LINE2 + LINE3 + LINE4 + LINE5 + LINE6 + LINE7 + LINE8 + LINE9 + LINE10 + LINE11 + LINE12 + LINE13 + LINE14 + LINE15;
                    if (TOT.ToString("N").Length == 4)
                    {
                        textBox106.Text = "    " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 5)
                    {
                        textBox106.Text = "   " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 6)
                    {
                        textBox106.Text = "  " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 7)
                    {
                        textBox106.Text = " " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length >= 8)
                    {
                        textBox106.Text = TOT.ToString("N");
                    }
                    textBox107.Text = (TOT * .0895m).ToString("N");
                    var h = TOT * .0895m;
                    textBox108.Text = TOT + h.ToString("N");
                }
                textBox97.Select();
            }
        }

        private void textBox104_KeyDown(object sender, KeyEventArgs e)  // Line 14
        {
            if (e.KeyCode == Keys.Enter)
            {
                var d0 = Convert.ToDecimal(textBox104.Text);
                var d1 = Convert.ToDecimal(textBox101.Text);
                EXTENDED = (d0 * d1).ToString("N");
                LINE14 = d0 * d1;
                if (EXTENDED.Length == 4)
                {
                    textBox99.Text = "     " + EXTENDED;
                }
                if (EXTENDED.Length == 5)
                {
                    textBox99.Text = "   " + EXTENDED;
                }
                if (EXTENDED.Length == 6)
                {
                    textBox99.Text = "  " + EXTENDED;
                }
                if (EXTENDED.Length == 7)
                {
                    textBox99.Text = " " + EXTENDED;
                }
                if (EXTENDED.Length >= 8)
                {
                    textBox99.Text = EXTENDED;
                }

                if (textBox14.Text.Contains("N"))
                {
                    var TOT = LINE1 + LINE2 + LINE3 + LINE4 + LINE5 + LINE6 + LINE7 + LINE8 + LINE9 + LINE10 + LINE11 + LINE12 + LINE13 + LINE14 + LINE15;
                    if (TOT.ToString("N").Length == 4)
                    {
                        textBox105.Text = "    " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 5)
                    {
                        textBox105.Text = "   " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 6)
                    {
                        textBox105.Text = "  " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 7)
                    {
                        textBox105.Text = " " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length >= 8)
                    {
                        textBox105.Text = TOT.ToString("N");
                    }
                    textBox106.Text = "0.00";
                    if (textBox106.Text == "0.00")
                    {
                        textBox106.Text = "    " + "0.00";
                    }
                    textBox107.Text = "0.00";
                    if (textBox107.Text == "0.00")
                    {
                        textBox107.Text = "    " + "0.00";
                    }
                    textBox108.Text = TOT.ToString("N");
                }
                if (textBox14.Text.Contains("Y"))
                {
                    var TOT = LINE1 + LINE2 + LINE3 + LINE4 + LINE5 + LINE6 + LINE7 + LINE8 + LINE9 + LINE10 + LINE11 + LINE12 + LINE13 + LINE14 + LINE15;
                    if (TOT.ToString("N").Length == 4)
                    {
                        textBox106.Text = "    " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 5)
                    {
                        textBox106.Text = "   " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 6)
                    {
                        textBox106.Text = "  " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 7)
                    {
                        textBox106.Text = " " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length >= 8)
                    {
                        textBox106.Text = TOT.ToString("N");
                    }
                    textBox107.Text = (TOT * .0895m).ToString("N");
                    var h = TOT * .0895m;
                    textBox108.Text = TOT + h.ToString("N");
                }
                textBox103.Select();
            }
        }

        private void textBox86_KeyDown(object sender, KeyEventArgs e)   // Line 13
        {
            if (e.KeyCode == Keys.Enter)
            {
                var d0 = Convert.ToDecimal(textBox86.Text);
                var d1 = Convert.ToDecimal(textBox83.Text);
                EXTENDED = (d0 * d1).ToString("N");
                LINE13 = d0 * d1;
                if (EXTENDED.Length == 4)
                {
                    textBox81.Text = "     " + EXTENDED;
                }
                if (EXTENDED.Length == 5)
                {
                    textBox81.Text = "   " + EXTENDED;
                }
                if (EXTENDED.Length == 6)
                {
                    textBox81.Text = "  " + EXTENDED;
                }
                if (EXTENDED.Length == 7)
                {
                    textBox81.Text = " " + EXTENDED;
                }
                if (EXTENDED.Length >= 8)
                {
                    textBox81.Text = EXTENDED;
                }

                if (textBox14.Text.Contains("N"))
                {
                    var TOT = LINE1 + LINE2 + LINE3 + LINE4 + LINE5 + LINE6 + LINE7 + LINE8 + LINE9 + LINE10 + LINE11 + LINE12 + LINE13 + LINE14 + LINE15;
                    if (TOT.ToString("N").Length == 4)
                    {
                        textBox105.Text = "    " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 5)
                    {
                        textBox105.Text = "   " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 6)
                    {
                        textBox105.Text = "  " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 7)
                    {
                        textBox105.Text = " " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length >= 8)
                    {
                        textBox105.Text = TOT.ToString("N");
                    }
                    textBox106.Text = "0.00";
                    if (textBox106.Text == "0.00")
                    {
                        textBox106.Text = "    " + "0.00";
                    }
                    textBox107.Text = "0.00";
                    if (textBox107.Text == "0.00")
                    {
                        textBox107.Text = "    " + "0.00";
                    }
                    textBox108.Text = TOT.ToString("N");
                }
                if (textBox14.Text.Contains("Y"))
                {
                    var TOT = LINE1 + LINE2 + LINE3 + LINE4 + LINE5 + LINE6 + LINE7 + LINE8 + LINE9 + LINE10 + LINE11 + LINE12 + LINE13 + LINE14 + LINE15;
                    if (TOT.ToString("N").Length == 4)
                    {
                        textBox106.Text = "    " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 5)
                    {
                        textBox106.Text = "   " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 6)
                    {
                        textBox106.Text = "  " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 7)
                    {
                        textBox106.Text = " " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length >= 8)
                    {
                        textBox106.Text = TOT.ToString("N");
                    }
                    textBox107.Text = (TOT * .0895m).ToString("N");
                    var h = TOT * .0895m;
                    textBox108.Text = TOT + h.ToString("N");
                }
                textBox85.Select();
            }
        }

        private void textBox92_KeyDown(object sender, KeyEventArgs e)   // Line 12
        {
            if (e.KeyCode == Keys.Enter)
            {
                var d0 = Convert.ToDecimal(textBox92.Text);
                var d1 = Convert.ToDecimal(textBox89.Text);
                EXTENDED = (d0 * d1).ToString("N");
                LINE12 = d0 * d1;
                if (EXTENDED.Length == 4)
                {
                    textBox87.Text = "     " + EXTENDED;
                }
                if (EXTENDED.Length == 5)
                {
                    textBox87.Text = "   " + EXTENDED;
                }
                if (EXTENDED.Length == 6)
                {
                    textBox87.Text = "  " + EXTENDED;
                }
                if (EXTENDED.Length == 7)
                {
                    textBox87.Text = " " + EXTENDED;
                }
                if (EXTENDED.Length >= 8)
                {
                    textBox87.Text = EXTENDED;
                }

                if (textBox14.Text.Contains("N"))
                {
                    var TOT = LINE1 + LINE2 + LINE3 + LINE4 + LINE5 + LINE6 + LINE7 + LINE8 + LINE9 + LINE10 + LINE11 + LINE12 + LINE13 + LINE14 + LINE15;
                    if (TOT.ToString("N").Length == 4)
                    {
                        textBox105.Text = "    " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 5)
                    {
                        textBox105.Text = "   " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 6)
                    {
                        textBox105.Text = "  " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 7)
                    {
                        textBox105.Text = " " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length >= 8)
                    {
                        textBox105.Text = TOT.ToString("N");
                    }
                    textBox106.Text = "0.00";
                    if (textBox106.Text == "0.00")
                    {
                        textBox106.Text = "    " + "0.00";
                    }
                    textBox107.Text = "0.00";
                    if (textBox107.Text == "0.00")
                    {
                        textBox107.Text = "    " + "0.00";
                    }
                    textBox108.Text = TOT.ToString("N");
                }
                if (textBox14.Text.Contains("Y"))
                {
                    var TOT = LINE1 + LINE2 + LINE3 + LINE4 + LINE5 + LINE6 + LINE7 + LINE8 + LINE9 + LINE10 + LINE11 + LINE12 + LINE13 + LINE14 + LINE15;
                    if (TOT.ToString("N").Length == 4)
                    {
                        textBox106.Text = "    " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 5)
                    {
                        textBox106.Text = "   " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 6)
                    {
                        textBox106.Text = "  " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 7)
                    {
                        textBox106.Text = " " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length >= 8)
                    {
                        textBox106.Text = TOT.ToString("N");
                    }
                    textBox107.Text = (TOT * .0895m).ToString("N");
                    var h = TOT * .0895m;
                    textBox108.Text = TOT + h.ToString("N");
                }
                textBox91.Select();
            }
        }

        private void textBox80_KeyDown(object sender, KeyEventArgs e)   // Line 11
        {
            if (e.KeyCode == Keys.Enter)
            {
                var d0 = Convert.ToDecimal(textBox80.Text);
                var d1 = Convert.ToDecimal(textBox77.Text);
                EXTENDED = (d0 * d1).ToString("N");
                LINE11 = d0 * d1;
                if (EXTENDED.Length == 4)
                {
                    textBox75.Text = "     " + EXTENDED;
                }
                if (EXTENDED.Length == 5)
                {
                    textBox75.Text = "   " + EXTENDED;
                }
                if (EXTENDED.Length == 6)
                {
                    textBox75.Text = "  " + EXTENDED;
                }
                if (EXTENDED.Length == 7)
                {
                    textBox75.Text = " " + EXTENDED;
                }
                if (EXTENDED.Length >= 8)
                {
                    textBox75.Text = EXTENDED;
                }

                if (textBox14.Text.Contains("N"))
                {
                    var TOT = LINE1 + LINE2 + LINE3 + LINE4 + LINE5 + LINE6 + LINE7 + LINE8 + LINE9 + LINE10 + LINE11 + LINE12 + LINE13 + LINE14 + LINE15;
                    if (TOT.ToString("N").Length == 4)
                    {
                        textBox105.Text = "    " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 5)
                    {
                        textBox105.Text = "   " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 6)
                    {
                        textBox105.Text = "  " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 7)
                    {
                        textBox105.Text = " " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length >= 8)
                    {
                        textBox105.Text = TOT.ToString("N");
                    }
                    textBox106.Text = "0.00";
                    if (textBox106.Text == "0.00")
                    {
                        textBox106.Text = "    " + "0.00";
                    }
                    textBox107.Text = "0.00";
                    if (textBox107.Text == "0.00")
                    {
                        textBox107.Text = "    " + "0.00";
                    }
                    textBox108.Text = TOT.ToString("N");
                }
                if (textBox14.Text.Contains("Y"))
                {
                    var TOT = LINE1 + LINE2 + LINE3 + LINE4 + LINE5 + LINE6 + LINE7 + LINE8 + LINE9 + LINE10 + LINE11 + LINE12 + LINE13 + LINE14 + LINE15;
                    if (TOT.ToString("N").Length == 4)
                    {
                        textBox106.Text = "    " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 5)
                    {
                        textBox106.Text = "   " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 6)
                    {
                        textBox106.Text = "  " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 7)
                    {
                        textBox106.Text = " " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length >= 8)
                    {
                        textBox106.Text = TOT.ToString("N");
                    }
                    textBox107.Text = (TOT * .0895m).ToString("N");
                    var h = TOT * .0895m;
                    textBox108.Text = TOT + h.ToString("N");
                }
                textBox79.Select();
            }
        }

        private void textBox74_KeyDown(object sender, KeyEventArgs e)   // Line 10
        {
            if (e.KeyCode == Keys.Enter)
            {
                var d0 = Convert.ToDecimal(textBox74.Text);
                var d1 = Convert.ToDecimal(textBox71.Text);
                EXTENDED = (d0 * d1).ToString("N");
                LINE10 = d0 * d1;
                if (EXTENDED.Length == 4)
                {
                    textBox69.Text = "     " + EXTENDED;
                }
                if (EXTENDED.Length == 5)
                {
                    textBox69.Text = "   " + EXTENDED;
                }
                if (EXTENDED.Length == 6)
                {
                    textBox69.Text = "  " + EXTENDED;
                }
                if (EXTENDED.Length == 7)
                {
                    textBox69.Text = " " + EXTENDED;
                }
                if (EXTENDED.Length >= 8)
                {
                    textBox69.Text = EXTENDED;
                }

                if (textBox14.Text.Contains("N"))
                {
                    var TOT = LINE1 + LINE2 + LINE3 + LINE4 + LINE5 + LINE6 + LINE7 + LINE8 + LINE9 + LINE10 + LINE11 + LINE12 + LINE13 + LINE14 + LINE15;
                    if (TOT.ToString("N").Length == 4)
                    {
                        textBox105.Text = "    " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 5)
                    {
                        textBox105.Text = "   " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 6)
                    {
                        textBox105.Text = "  " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 7)
                    {
                        textBox105.Text = " " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length >= 8)
                    {
                        textBox105.Text = TOT.ToString("N");
                    }
                    textBox106.Text = "0.00";
                    if (textBox106.Text == "0.00")
                    {
                        textBox106.Text = "    " + "0.00";
                    }
                    textBox107.Text = "0.00";
                    if (textBox107.Text == "0.00")
                    {
                        textBox107.Text = "    " + "0.00";
                    }
                    textBox108.Text = TOT.ToString("N");
                }
                if (textBox14.Text.Contains("Y"))
                {
                    var TOT = LINE1 + LINE2 + LINE3 + LINE4 + LINE5 + LINE6 + LINE7 + LINE8 + LINE9 + LINE10 + LINE11 + LINE12 + LINE13 + LINE14 + LINE15;
                    if (TOT.ToString("N").Length == 4)
                    {
                        textBox106.Text = "    " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 5)
                    {
                        textBox106.Text = "   " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 6)
                    {
                        textBox106.Text = "  " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 7)
                    {
                        textBox106.Text = " " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length >= 8)
                    {
                        textBox106.Text = TOT.ToString("N");
                    }
                    textBox107.Text = (TOT * .0895m).ToString("N");
                    var h = TOT * .0895m;
                    textBox108.Text = TOT + h.ToString("N");
                }
                textBox73.Select();
            }
        }

        private void textBox68_KeyDown(object sender, KeyEventArgs e)   // Line 9
        {
            if (e.KeyCode == Keys.Enter)
            {
                var d0 = Convert.ToDecimal(textBox68.Text);
                var d1 = Convert.ToDecimal(textBox65.Text);
                EXTENDED = (d0 * d1).ToString("N");
                LINE9 = d0 * d1;
                if (EXTENDED.Length == 4)
                {
                    textBox63.Text = "     " + EXTENDED;
                }
                if (EXTENDED.Length == 5)
                {
                    textBox63.Text = "   " + EXTENDED;
                }
                if (EXTENDED.Length == 6)
                {
                    textBox63.Text = "  " + EXTENDED;
                }
                if (EXTENDED.Length == 7)
                {
                    textBox63.Text = " " + EXTENDED;
                }
                if (EXTENDED.Length >= 8)
                {
                    textBox63.Text = EXTENDED;
                }

                if (textBox14.Text.Contains("N"))
                {
                    var TOT = LINE1 + LINE2 + LINE3 + LINE4 + LINE5 + LINE6 + LINE7 + LINE8 + LINE9 + LINE10 + LINE11 + LINE12 + LINE13 + LINE14 + LINE15;
                    if (TOT.ToString("N").Length == 4)
                    {
                        textBox105.Text = "    " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 5)
                    {
                        textBox105.Text = "   " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 6)
                    {
                        textBox105.Text = "  " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 7)
                    {
                        textBox105.Text = " " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length >= 8)
                    {
                        textBox105.Text = TOT.ToString("N");
                    }
                    textBox106.Text = "0.00";
                    if (textBox106.Text == "0.00")
                    {
                        textBox106.Text = "    " + "0.00";
                    }
                    textBox107.Text = "0.00";
                    if (textBox107.Text == "0.00")
                    {
                        textBox107.Text = "    " + "0.00";
                    }
                    textBox108.Text = TOT.ToString("N");
                }
                if (textBox14.Text.Contains("Y"))
                {
                    var TOT = LINE1 + LINE2 + LINE3 + LINE4 + LINE5 + LINE6 + LINE7 + LINE8 + LINE9 + LINE10 + LINE11 + LINE12 + LINE13 + LINE14 + LINE15;
                    if (TOT.ToString("N").Length == 4)
                    {
                        textBox106.Text = "    " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 5)
                    {
                        textBox106.Text = "   " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 6)
                    {
                        textBox106.Text = "  " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 7)
                    {
                        textBox106.Text = " " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length >= 8)
                    {
                        textBox106.Text = TOT.ToString("N");
                    }
                    textBox107.Text = (TOT * .0895m).ToString("N");
                    var h = TOT * .0895m;
                    textBox108.Text = TOT + h.ToString("N");
                }
                textBox67.Select();
            }
        }

        private void textBox62_KeyDown(object sender, KeyEventArgs e)   // Line 8
        {
            if (e.KeyCode == Keys.Enter)
            {

                var d0 = Convert.ToDecimal(textBox62.Text);
                var d1 = Convert.ToDecimal(textBox59.Text);
                EXTENDED = (d0 * d1).ToString("N");
                LINE8 = d0 * d1;
                if (EXTENDED.Length == 4)
                {
                    textBox57.Text = "     " + EXTENDED;
                }
                if (EXTENDED.Length == 5)
                {
                    textBox57.Text = "   " + EXTENDED;
                }
                if (EXTENDED.Length == 6)
                {
                    textBox57.Text = "  " + EXTENDED;
                }
                if (EXTENDED.Length == 7)
                {
                    textBox57.Text = " " + EXTENDED;
                }
                if (EXTENDED.Length >= 8)
                {
                    textBox57.Text = EXTENDED;
                }

                if (textBox14.Text.Contains("N"))
                {
                    var TOT = LINE1 + LINE2 + LINE3 + LINE4 + LINE5 + LINE6 + LINE7 + LINE8 + LINE9 + LINE10 + LINE11 + LINE12 + LINE13 + LINE14 + LINE15;
                    if (TOT.ToString("N").Length == 4)
                    {
                        textBox105.Text = "    " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 5)
                    {
                        textBox105.Text = "   " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 6)
                    {
                        textBox105.Text = "  " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 7)
                    {
                        textBox105.Text = " " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length >= 8)
                    {
                        textBox105.Text = TOT.ToString("N");
                    }
                    textBox106.Text = "0.00";
                    if (textBox106.Text == "0.00")
                    {
                        textBox106.Text = "    " + "0.00";
                    }
                    textBox107.Text = "0.00";
                    if (textBox107.Text == "0.00")
                    {
                        textBox107.Text = "    " + "0.00";
                    }
                    textBox108.Text = TOT.ToString("N");
                }
                if (textBox14.Text.Contains("Y"))
                {
                    var TOT = LINE1 + LINE2 + LINE3 + LINE4 + LINE5 + LINE6 + LINE7 + LINE8 + LINE9 + LINE10 + LINE11 + LINE12 + LINE13 + LINE14 + LINE15;
                    if (TOT.ToString("N").Length == 4)
                    {
                        textBox106.Text = "    " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 5)
                    {
                        textBox106.Text = "   " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 6)
                    {
                        textBox106.Text = "  " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 7)
                    {
                        textBox106.Text = " " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length >= 8)
                    {
                        textBox106.Text = TOT.ToString("N");
                    }
                    textBox107.Text = (TOT * .0895m).ToString("N");
                    var h = TOT * .0895m;
                    textBox108.Text = TOT + h.ToString("N");
                }
                textBox61.Select();
            }
        }

        private void textBox56_KeyDown(object sender, KeyEventArgs e)   // Line 7
        {
            if (e.KeyCode == Keys.Enter)
            {

                var d0 = Convert.ToDecimal(textBox56.Text);
                var d1 = Convert.ToDecimal(textBox53.Text);
                EXTENDED = (d0 * d1).ToString("N");
                LINE7 = d0 * d1;
                if (EXTENDED.Length == 4)
                {
                    textBox51.Text = "     " + EXTENDED;
                }
                if (EXTENDED.Length == 5)
                {
                    textBox51.Text = "   " + EXTENDED;
                }
                if (EXTENDED.Length == 6)
                {
                    textBox51.Text = "  " + EXTENDED;
                }
                if (EXTENDED.Length == 7)
                {
                    textBox51.Text = " " + EXTENDED;
                }
                if (EXTENDED.Length >= 8)
                {
                    textBox51.Text = EXTENDED;
                }

                if (textBox14.Text.Contains("N"))
                {
                    var TOT = LINE1 + LINE2 + LINE3 + LINE4 + LINE5 + LINE6 + LINE7 + LINE8 + LINE9 + LINE10 + LINE11 + LINE12 + LINE13 + LINE14 + LINE15;
                    if (TOT.ToString("N").Length == 4)
                    {
                        textBox105.Text = "    " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 5)
                    {
                        textBox105.Text = "   " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 6)
                    {
                        textBox105.Text = "  " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 7)
                    {
                        textBox105.Text = " " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length >= 8)
                    {
                        textBox105.Text = TOT.ToString("N");
                    }
                    textBox106.Text = "0.00";
                    if (textBox106.Text == "0.00")
                    {
                        textBox106.Text = "    " + "0.00";
                    }
                    textBox107.Text = "0.00";
                    if (textBox107.Text == "0.00")
                    {
                        textBox107.Text = "    " + "0.00";
                    }
                    textBox108.Text = TOT.ToString("N");
                }
                if (textBox14.Text.Contains("Y"))
                {
                    var TOT = LINE1 + LINE2 + LINE3 + LINE4 + LINE5 + LINE6 + LINE7 + LINE8 + LINE9 + LINE10 + LINE11 + LINE12 + LINE13 + LINE14 + LINE15;
                    if (TOT.ToString("N").Length == 4)
                    {
                        textBox106.Text = "    " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 5)
                    {
                        textBox106.Text = "   " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 6)
                    {
                        textBox106.Text = "  " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 7)
                    {
                        textBox106.Text = " " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length >= 8)
                    {
                        textBox106.Text = TOT.ToString("N");
                    }
                    textBox107.Text = (TOT * .0895m).ToString("N");
                    var h = TOT * .0895m;
                    textBox108.Text = TOT + h.ToString("N");
                }
                textBox55.Select();
            }
        }

        private void textBox50_KeyDown(object sender, KeyEventArgs e)   // Line 6
        {
            if (e.KeyCode == Keys.Enter)
            {

                var d0 = Convert.ToDecimal(textBox50.Text);
                var d1 = Convert.ToDecimal(textBox47.Text);
                EXTENDED = (d0 * d1).ToString("N");
                LINE6 = d0 * d1;
                if (EXTENDED.Length == 4)
                {
                    textBox45.Text = "     " + EXTENDED;
                }
                if (EXTENDED.Length == 5)
                {
                    textBox45.Text = "   " + EXTENDED;
                }
                if (EXTENDED.Length == 6)
                {
                    textBox45.Text = "  " + EXTENDED;
                }
                if (EXTENDED.Length == 7)
                {
                    textBox45.Text = " " + EXTENDED;
                }
                if (EXTENDED.Length >= 8)
                {
                    textBox45.Text = EXTENDED;
                }

                if (textBox14.Text.Contains("N"))
                {
                    var TOT = LINE1 + LINE2 + LINE3 + LINE4 + LINE5 + LINE6 + LINE7 + LINE8 + LINE9 + LINE10 + LINE11 + LINE12 + LINE13 + LINE14 + LINE15;
                    if (TOT.ToString("N").Length == 4)
                    {
                        textBox105.Text = "    " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 5)
                    {
                        textBox105.Text = "   " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 6)
                    {
                        textBox105.Text = "  " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 7)
                    {
                        textBox105.Text = " " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length >= 8)
                    {
                        textBox105.Text = TOT.ToString("N");
                    }
                    textBox106.Text = "0.00";
                    if (textBox106.Text == "0.00")
                    {
                        textBox106.Text = "    " + "0.00";
                    }
                    textBox107.Text = "0.00";
                    if (textBox107.Text == "0.00")
                    {
                        textBox107.Text = "    " + "0.00";
                    }
                    textBox108.Text = TOT.ToString("N");
                }
                if (textBox14.Text.Contains("Y"))
                {
                    var TOT = LINE1 + LINE2 + LINE3 + LINE4 + LINE5 + LINE6 + LINE7 + LINE8 + LINE9 + LINE10 + LINE11 + LINE12 + LINE13 + LINE14 + LINE15;
                    if (TOT.ToString("N").Length == 4)
                    {
                        textBox106.Text = "    " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 5)
                    {
                        textBox106.Text = "   " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 6)
                    {
                        textBox106.Text = "  " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 7)
                    {
                        textBox106.Text = " " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length >= 8)
                    {
                        textBox106.Text = TOT.ToString("N");
                    }
                    textBox107.Text = (TOT * .0895m).ToString("N");
                    var h = TOT * .0895m;
                    textBox108.Text = TOT + h.ToString("N");
                }
                textBox49.Select();
            }
        }

        private void textBox44_KeyDown(object sender, KeyEventArgs e)   // Line 5
        {
            if (e.KeyCode == Keys.Enter)
            {

                var d0 = Convert.ToDecimal(textBox44.Text);
                var d1 = Convert.ToDecimal(textBox41.Text);
                EXTENDED = (d0 * d1).ToString("N");
                LINE5 = d0 * d1;
                if (EXTENDED.Length == 4)
                {
                    textBox39.Text = "     " + EXTENDED;
                }
                if (EXTENDED.Length == 5)
                {
                    textBox39.Text = "   " + EXTENDED;
                }
                if (EXTENDED.Length == 6)
                {
                    textBox39.Text = "  " + EXTENDED;
                }
                if (EXTENDED.Length == 7)
                {
                    textBox39.Text = " " + EXTENDED;
                }
                if (EXTENDED.Length >= 8)
                {
                    textBox39.Text = EXTENDED;
                }

                if (textBox14.Text.Contains("N"))
                {
                    var TOT = LINE1 + LINE2 + LINE3 + LINE4 + LINE5 + LINE6 + LINE7 + LINE8 + LINE9 + LINE10 + LINE11 + LINE12 + LINE13 + LINE14 + LINE15;
                    if (TOT.ToString("N").Length == 4)
                    {
                        textBox105.Text = "    " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 5)
                    {
                        textBox105.Text = "   " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 6)
                    {
                        textBox105.Text = "  " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 7)
                    {
                        textBox105.Text = " " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length >= 8)
                    {
                        textBox105.Text = TOT.ToString("N");
                    }
                    textBox106.Text = "0.00";
                    if (textBox106.Text == "0.00")
                    {
                        textBox106.Text = "    " + "0.00";
                    }
                    textBox107.Text = "0.00";
                    if (textBox107.Text == "0.00")
                    {
                        textBox107.Text = "    " + "0.00";
                    }
                    textBox108.Text = TOT.ToString("N");
                }
                if (textBox14.Text.Contains("Y"))
                {
                    var TOT = LINE1 + LINE2 + LINE3 + LINE4 + LINE5 + LINE6 + LINE7 + LINE8 + LINE9 + LINE10 + LINE11 + LINE12 + LINE13 + LINE14 + LINE15;
                    if (TOT.ToString("N").Length == 4)
                    {
                        textBox106.Text = "    " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 5)
                    {
                        textBox106.Text = "   " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 6)
                    {
                        textBox106.Text = "  " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 7)
                    {
                        textBox106.Text = " " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length >= 8)
                    {
                        textBox106.Text = TOT.ToString("N");
                    }
                    textBox107.Text = (TOT * .0895m).ToString("N");
                    var h = TOT * .0895m;
                    textBox108.Text = TOT + h.ToString("N");
                }
                textBox43.Select();
            }
        }

        private void textBox38_KeyDown(object sender, KeyEventArgs e)   // Line 4
        {
            if (e.KeyCode == Keys.Enter)
            {

                var d0 = Convert.ToDecimal(textBox38.Text);
                var d1 = Convert.ToDecimal(textBox35.Text);
                EXTENDED = (d0 * d1).ToString("N");
                LINE4 = d0 * d1;
                if (EXTENDED.Length == 4)
                {
                    textBox33.Text = "    " + EXTENDED;
                }
                if (EXTENDED.Length == 5)
                {
                    textBox33.Text = "   " + EXTENDED;
                }
                if (EXTENDED.Length == 6)
                {
                    textBox33.Text = "  " + EXTENDED;
                }
                if (EXTENDED.Length == 7)
                {
                    textBox33.Text = " " + EXTENDED;
                }
                if (EXTENDED.Length >= 8)
                {
                    textBox33.Text = EXTENDED;
                }

                if (textBox14.Text.Contains("N"))
                {
                    var TOT = LINE1 + LINE2 + LINE3 + LINE4 + LINE5 + LINE6 + LINE7 + LINE8 + LINE9 + LINE10 + LINE11 + LINE12 + LINE13 + LINE14 + LINE15;
                    if (TOT.ToString("N").Length == 4)
                    {
                        textBox105.Text = "    " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 5)
                    {
                        textBox105.Text = "   " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 6)
                    {
                        textBox105.Text = "  " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 7)
                    {
                        textBox105.Text = " " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length >= 8)
                    {
                        textBox105.Text = TOT.ToString("N");
                    }
                    textBox106.Text = "0.00";
                    if (textBox106.Text == "0.00")
                    {
                        textBox106.Text = "    " + "0.00";
                    }
                    textBox107.Text = "0.00";
                    if (textBox107.Text == "0.00")
                    {
                        textBox107.Text = "    " + "0.00";
                    }
                    textBox108.Text = TOT.ToString("N");
                }
                if (textBox14.Text.Contains("Y"))
                {
                    var TOT = LINE1 + LINE2 + LINE3 + LINE4 + LINE5 + LINE6 + LINE7 + LINE8 + LINE9 + LINE10 + LINE11 + LINE12 + LINE13 + LINE14 + LINE15;
                    if (TOT.ToString("N").Length == 4)
                    {
                        textBox106.Text = "    " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 5)
                    {
                        textBox106.Text = "   " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 6)
                    {
                        textBox106.Text = "  " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 7)
                    {
                        textBox106.Text = " " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length >= 8)
                    {
                        textBox106.Text = TOT.ToString("N");
                    }
                    textBox107.Text = (TOT * .0895m).ToString("N");
                    var h = TOT * .0895m;
                    textBox108.Text = TOT + h.ToString("N");
                }
                textBox37.Select();
            }
        }

        private void textBox32_KeyDown(object sender, KeyEventArgs e)   // Line 3
        {
            if (e.KeyCode == Keys.Enter)
            {

                var d0 = Convert.ToDecimal(textBox32.Text);
                var d1 = Convert.ToDecimal(textBox29.Text);
                EXTENDED = (d0 * d1).ToString("N");
                LINE3 = d0 * d1;
                if (EXTENDED.Length == 4)
                {
                    textBox27.Text = "    " + EXTENDED;
                }
                if (EXTENDED.Length == 5)
                {
                    textBox27.Text = "   " + EXTENDED;
                }
                if (EXTENDED.Length == 6)
                {
                    textBox27.Text = "  " + EXTENDED;
                }
                if (EXTENDED.Length == 7)
                {
                    textBox27.Text = " " + EXTENDED;
                }
                if (EXTENDED.Length >= 8)
                {
                    textBox27.Text = EXTENDED;
                }

                if (textBox14.Text.Contains("N"))
                {
                    var TOT = LINE1 + LINE2 + LINE3 + LINE4 + LINE5 + LINE6 + LINE7 + LINE8 + LINE9 + LINE10 + LINE11 + LINE12 + LINE13 + LINE14 + LINE15;
                    if (TOT.ToString("N").Length == 4)
                    {
                        textBox105.Text = "    " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 5)
                    {
                        textBox105.Text = "   " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 6)
                    {
                        textBox105.Text = "  " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 7)
                    {
                        textBox105.Text = " " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length >= 8)
                    {
                        textBox105.Text = TOT.ToString("N");
                    }
                    textBox106.Text = "0.00";
                    if (textBox106.Text == "0.00")
                    {
                        textBox106.Text = "    " + "0.00";
                    }
                    textBox107.Text = "0.00";
                    if (textBox107.Text == "0.00")
                    {
                        textBox107.Text = "    " + "0.00";
                    }
                    textBox108.Text = TOT.ToString("N");
                }
                if (textBox14.Text.Contains("Y"))
                {
                    var TOT = LINE1 + LINE2 + LINE3 + LINE4 + LINE5 + LINE6 + LINE7 + LINE8 + LINE9 + LINE10 + LINE11 + LINE12 + LINE13 + LINE14 + LINE15;
                    if (TOT.ToString("N").Length == 4)
                    {
                        textBox106.Text = "    " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 5)
                    {
                        textBox106.Text = "   " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 6)
                    {
                        textBox106.Text = "  " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 7)
                    {
                        textBox106.Text = " " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length >= 8)
                    {
                        textBox106.Text = TOT.ToString("N");
                    }
                    textBox107.Text = (TOT * .0895m).ToString("N");
                    var h = TOT * .0895m;
                    textBox108.Text = TOT + h.ToString("N");
                }
                textBox31.Select();
            }
        }

        private void textBox26_KeyDown(object sender, KeyEventArgs e)   // Line 2
        {
            if (e.KeyCode == Keys.Enter)
            {

                var d0 = Convert.ToDecimal(textBox26.Text);
                var d1 = Convert.ToDecimal(textBox23.Text);
                EXTENDED = (d0 * d1).ToString("N");
                LINE2 = d0 * d1;
                if (EXTENDED.Length == 4)
                {
                    textBox21.Text = "    " + EXTENDED;
                }
                if (EXTENDED.Length == 5)
                {
                    textBox21.Text = "   " + EXTENDED;
                }
                if (EXTENDED.Length == 6)
                {
                    textBox21.Text = "  " + EXTENDED;
                }
                if (EXTENDED.Length == 7)
                {
                    textBox21.Text = " " + EXTENDED;
                }
                if (EXTENDED.Length >= 8)
                {
                    textBox21.Text = EXTENDED;
                }

                if (textBox14.Text.Contains("N"))
                {
                    var TOT = LINE1 + LINE2 + LINE3 + LINE4 + LINE5 + LINE6 + LINE7 + LINE8 + LINE9 + LINE10 + LINE11 + LINE12 + LINE13 + LINE14 + LINE15;
                    if (TOT.ToString("N").Length == 4)
                    {
                        textBox105.Text = "    " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 5)
                    {
                        textBox105.Text = "   " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 6)
                    {
                        textBox105.Text = "  " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 7)
                    {
                        textBox105.Text = " " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length >= 8)
                    {
                        textBox105.Text = TOT.ToString("N");
                    }
                    textBox106.Text = "0.00";
                    if (textBox106.Text == "0.00")
                    {
                        textBox106.Text = "    " + "0.00";
                    }
                    textBox107.Text = "0.00";
                    if (textBox107.Text == "0.00")
                    {
                        textBox107.Text = "    " + "0.00";
                    }
                    textBox108.Text = TOT.ToString("N");
                }
                if (textBox14.Text.Contains("Y"))
                {
                    var TOT = LINE1 + LINE2 + LINE3 + LINE4 + LINE5 + LINE6 + LINE7 + LINE8 + LINE9 + LINE10 + LINE11 + LINE12 + LINE13 + LINE14 + LINE15;
                    if (TOT.ToString("N").Length == 4)
                    {
                        textBox106.Text = "    " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 5)
                    {
                        textBox106.Text = "   " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 6)
                    {
                        textBox106.Text = "  " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 7)
                    {
                        textBox106.Text = " " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length >= 8)
                    {
                        textBox106.Text = TOT.ToString("N");
                    }
                    textBox107.Text = (TOT * .0895m).ToString("N");
                    var h = TOT * .0895m;
                    textBox108.Text = TOT + h.ToString("N");
                }
                textBox25.Select();
            }
        }

        private void textBox15_KeyDown(object sender, KeyEventArgs e)       // LINE1
        {
            if (e.KeyCode == Keys.Enter)
            {
                var d0 = Convert.ToDecimal(textBox15.Text);
                var d1 = Convert.ToDecimal(textBox18.Text);
                EXTENDED = (d0 * d1).ToString("N");
                LINE1 = d0 * d1;
                if (EXTENDED.Length == 4)
                {
                    textBox20.Text = "    " + EXTENDED;
                }
                if (EXTENDED.Length == 5)
                {
                    textBox20.Text = "   " + EXTENDED;
                }
                if (EXTENDED.Length == 6)
                {
                    textBox20.Text = "  " + EXTENDED;
                }
                if (EXTENDED.Length == 7)
                {
                    textBox20.Text = " " + EXTENDED;
                }
                if (EXTENDED.Length >= 8)
                {
                    textBox20.Text = EXTENDED;
                }

                if (textBox14.Text.Contains("N"))
                {
                    var TOT = LINE1 + LINE2 + LINE3 + LINE4 + LINE5 + LINE6 + LINE7 + LINE8 + LINE9 + LINE10 + LINE11 + LINE12 + LINE13 + LINE14 + LINE15;
                    if (TOT.ToString("N").Length == 4)
                    {
                        textBox105.Text = "    " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 5)
                    {
                        textBox105.Text = "   " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 6)
                    {
                        textBox105.Text = "  " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 7)
                    {
                        textBox105.Text = " " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length >= 8)
                    {
                        textBox105.Text = TOT.ToString("N");
                    }
                    textBox106.Text = "0.00";
                    if (textBox106.Text == "0.00")
                    {
                        textBox106.Text = "    " + "0.00";
                    }
                    textBox107.Text = "0.00";
                    if (textBox107.Text == "0.00")
                    {
                        textBox107.Text = "    " + "0.00";
                    }
                    textBox108.Text = TOT.ToString("N");
                }
                if (textBox14.Text.Contains("Y"))
                {
                    var TOT = LINE1 + LINE2 + LINE3 + LINE4 + LINE5 + LINE6 + LINE7 + LINE8 + LINE9 + LINE10 + LINE11 + LINE12 + LINE13 + LINE14 + LINE15;
                    if (TOT.ToString("N").Length == 4)
                    {
                        textBox106.Text = "    " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 5)
                    {
                        textBox106.Text = "   " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 6)
                    {
                        textBox106.Text = "  " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length == 7)
                    {
                        textBox106.Text = " " + TOT.ToString("N");
                    }
                    if (TOT.ToString("N").Length >= 8)
                    {
                        textBox106.Text = TOT.ToString("N");
                    }
                    textBox107.Text = (TOT * .0895m).ToString("N");
                    var h = TOT * .0895m;
                    textBox108.Text = TOT + h.ToString("N");
                }
                textBox16.Select();
            }
        }

        private void button1_Click(object sender, EventArgs e)                                   // Save new data
        {
            timer1.Start();
            List<String> lines = new List<String>();

            if (File.Exists(PurchOrder))
            {
                using (StreamReader reader = new StreamReader(PurchOrder))
                {
                    String line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Contains(","))
                        {
                            String[] split = line.Split(',');
                            try
                            {
                                if (split[0].Contains(CLAIM3))
                                {
                                    if (split[20] == "1")
                                    {
                                        split[0] = CLAIM;                                        // PO Number
                                        split[1] = textBox17.Text;                               // Parts Description
                                        split[2] = textBox6.Text;                                // Date In
                                        split[3] = textBox12.Text;                               // Buyer
                                        split[4] = textBox19.Text;                               // On Backorder
                                        split[5] = textBox11.Text;                               // Date Ordered
                                        split[6] = TECHNICIAN;                                   // Technician Name
                                        split[7] = WARRANTYSTATUS;                               // Warranty Status
                                        split[8] = textBox14.Text;                               // Taxable
                                        split[9] = label30.Text.ToUpper();                       // Company
                                        split[10] = ADDR;                                        // Address 1
                                        split[11] = ADDR2;                                       // Address 2
                                        split[12] = ADDR3;                                       // Address 3
                                        split[13] = CSZ;                                         // City State Zip
                                        split[14] = CODE;                                        // Code - JBL, QSC, etc
                                        split[15] = PHONE;                                       // Company Phone #
                                        split[16] = textBox11.Text;                              // Date Entered
                                        split[17] = textBox15.Text;                              // Quantity
                                        split[18] = textBox16.Text;                              // Part Number
                                        split[19] = textBox18.Text;                              // Cost
                                    }
                                    if (split[20] == "2")
                                    {
                                        split[0] = CLAIM;                                        // PO Number
                                        split[1] = textBox24.Text;                               // Parts Description
                                        split[2] = textBox6.Text;                                // Date In
                                        split[3] = textBox12.Text;                               // Buyer
                                        split[4] = textBox22.Text;                               // On Backorder
                                        split[5] = textBox11.Text;                               // Date Ordered
                                        split[6] = TECHNICIAN;                                   // Technician Name
                                        split[7] = WARRANTYSTATUS;                               // Warranty Status
                                        split[8] = textBox14.Text;                               // Taxable
                                        split[9] = label30.Text.ToUpper();                       // Company
                                        split[10] = ADDR;                                        // Address 1
                                        split[11] = ADDR2;                                       // Address 2
                                        split[12] = ADDR3;                                       // Address 3
                                        split[13] = CSZ;                                         // City State Zip
                                        split[14] = CODE;                                        // Code - JBL, QSC, etc
                                        split[15] = PHONE;                                       // Company Phone #
                                        split[16] = textBox11.Text;                              // Date Entered
                                        split[17] = textBox26.Text;                              // Quantity
                                        split[18] = textBox25.Text;                              // Part Number
                                        split[19] = textBox23.Text;                              // Cost
                                    }
                                    if (split[20] == "3")
                                    {
                                        split[0] = CLAIM;                                        // PO Number
                                        split[1] = textBox30.Text;                               // Parts Description
                                        split[2] = textBox6.Text;                                // Date In
                                        split[3] = textBox12.Text;                               // Buyer
                                        split[4] = textBox28.Text;                               // On Backorder
                                        split[5] = textBox11.Text;                               // Date Ordered
                                        split[6] = TECHNICIAN;                                   // Technician Name
                                        split[7] = WARRANTYSTATUS;                               // Warranty Status
                                        split[8] = textBox14.Text;                               // Taxable
                                        split[9] = label30.Text.ToUpper();                       // Company
                                        split[10] = ADDR;                                        // Address 1
                                        split[11] = ADDR2;                                       // Address 2
                                        split[12] = ADDR3;                                       // Address 3
                                        split[13] = CSZ;                                         // City State Zip
                                        split[14] = CODE;                                        // Code - JBL, QSC, etc
                                        split[15] = PHONE;                                       // Company Phone #
                                        split[16] = textBox11.Text;                              // Date Entered
                                        split[17] = textBox32.Text;                              // Quantity
                                        split[18] = textBox31.Text;                              // Part Number
                                        split[19] = textBox29.Text;                              // Cost
                                    }
                                    if (split[20] == "4")
                                    {
                                        split[0] = CLAIM;                                        // PO Number
                                        split[1] = textBox36.Text;                               // Parts Description
                                        split[2] = textBox6.Text;                                // Date In
                                        split[3] = textBox12.Text;                               // Buyer
                                        split[4] = textBox34.Text;                               // On Backorder
                                        split[5] = textBox11.Text;                               // Date Ordered
                                        split[6] = TECHNICIAN;                                   // Technician Name
                                        split[7] = WARRANTYSTATUS;                               // Warranty Status
                                        split[8] = textBox14.Text;                               // Taxable
                                        split[9] = label30.Text.ToUpper();                       // Company
                                        split[10] = ADDR;                                        // Address 1
                                        split[11] = ADDR2;                                       // Address 2
                                        split[12] = ADDR3;                                       // Address 3
                                        split[13] = CSZ;                                         // City State Zip
                                        split[14] = CODE;                                        // Code - JBL, QSC, etc
                                        split[15] = PHONE;                                       // Company Phone #
                                        split[16] = textBox11.Text;                              // Date Entered
                                        split[17] = textBox38.Text;                              // Quantity
                                        split[18] = textBox37.Text;                              // Part Number
                                        split[19] = textBox35.Text;                              // Cost
                                    }
                                    if (split[20] == "5")
                                    {
                                        split[0] = CLAIM;                                        // PO Number
                                        split[1] = textBox42.Text;                               // Parts Description
                                        split[2] = textBox6.Text;                                // Date In
                                        split[3] = textBox12.Text;                               // Buyer
                                        split[4] = textBox40.Text;                               // On Backorder
                                        split[5] = textBox11.Text;                               // Date Ordered
                                        split[6] = TECHNICIAN;                                   // Technician Name
                                        split[7] = WARRANTYSTATUS;                               // Warranty Status
                                        split[8] = textBox14.Text;                               // Taxable
                                        split[9] = label30.Text.ToUpper();                       // Company
                                        split[10] = ADDR;                                        // Address 1
                                        split[11] = ADDR2;                                       // Address 2
                                        split[12] = ADDR3;                                       // Address 3
                                        split[13] = CSZ;                                         // City State Zip
                                        split[14] = CODE;                                        // Code - JBL, QSC, etc
                                        split[15] = PHONE;                                       // Company Phone #
                                        split[16] = textBox11.Text;                              // Date Entered
                                        split[17] = textBox44.Text;                              // Quantity
                                        split[18] = textBox43.Text;                              // Part Number
                                        split[19] = textBox41.Text;                              // Cost
                                    }
                                    if (split[20] == "6")
                                    {
                                        split[0] = CLAIM;                                        // PO Number
                                        split[1] = textBox48.Text;                               // Parts Description
                                        split[2] = textBox6.Text;                                // Date In
                                        split[3] = textBox12.Text;                               // Buyer
                                        split[4] = textBox46.Text;                               // On Backorder
                                        split[5] = textBox11.Text;                               // Date Ordered
                                        split[6] = TECHNICIAN;                                   // Technician Name
                                        split[7] = WARRANTYSTATUS;                               // Warranty Status
                                        split[8] = textBox14.Text;                               // Taxable
                                        split[9] = label30.Text.ToUpper();                       // Company
                                        split[10] = ADDR;                                        // Address 1
                                        split[11] = ADDR2;                                       // Address 2
                                        split[12] = ADDR3;                                       // Address 3
                                        split[13] = CSZ;                                         // City State Zip
                                        split[14] = CODE;                                        // Code - JBL, QSC, etc
                                        split[15] = PHONE;                                       // Company Phone #
                                        split[16] = textBox11.Text;                              // Date Entered
                                        split[17] = textBox50.Text;                              // Quantity
                                        split[18] = textBox49.Text;                              // Part Number
                                        split[19] = textBox47.Text;                              // Cost
                                    }
                                    if (split[20] == "7")
                                    {
                                        split[0] = CLAIM;                                        // PO Number
                                        split[1] = textBox54.Text;                               // Parts Description
                                        split[2] = textBox6.Text;                                // Date In
                                        split[3] = textBox12.Text;                               // Buyer
                                        split[4] = textBox52.Text;                               // On Backorder
                                        split[5] = textBox11.Text;                               // Date Ordered
                                        split[6] = TECHNICIAN;                                   // Technician Name
                                        split[7] = WARRANTYSTATUS;                               // Warranty Status
                                        split[8] = textBox14.Text;                               // Taxable
                                        split[9] = label30.Text.ToUpper();                       // Company
                                        split[10] = ADDR;                                        // Address 1
                                        split[11] = ADDR2;                                       // Address 2
                                        split[12] = ADDR3;                                       // Address 3
                                        split[13] = CSZ;                                         // City State Zip
                                        split[14] = CODE;                                        // Code - JBL, QSC, etc
                                        split[15] = PHONE;                                       // Company Phone #
                                        split[16] = textBox11.Text;                              // Date Entered
                                        split[17] = textBox56.Text;                              // Quantity
                                        split[18] = textBox55.Text;                              // Part Number
                                        split[19] = textBox53.Text;                              // Cost
                                    }
                                    if (split[20] == "8")
                                    {
                                        split[0] = CLAIM;                                        // PO Number
                                        split[1] = textBox60.Text;                               // Parts Description
                                        split[2] = textBox6.Text;                                // Date In
                                        split[3] = textBox12.Text;                               // Buyer
                                        split[4] = textBox58.Text;                               // On Backorder
                                        split[5] = textBox11.Text;                               // Date Ordered
                                        split[6] = TECHNICIAN;                                   // Technician Name
                                        split[7] = WARRANTYSTATUS;                               // Warranty Status
                                        split[8] = textBox14.Text;                               // Taxable
                                        split[9] = label30.Text.ToUpper();                       // Company
                                        split[10] = ADDR;                                        // Address 1
                                        split[11] = ADDR2;                                       // Address 2
                                        split[12] = ADDR3;                                       // Address 3
                                        split[13] = CSZ;                                         // City State Zip
                                        split[14] = CODE;                                        // Code - JBL, QSC, etc
                                        split[15] = PHONE;                                       // Company Phone #
                                        split[16] = textBox11.Text;                              // Date Entered
                                        split[17] = textBox62.Text;                              // Quantity
                                        split[18] = textBox61.Text;                              // Part Number
                                        split[19] = textBox59.Text;                              // Cost
                                    }
                                    if (split[20] == "9")
                                    {
                                        split[0] = CLAIM;                                        // PO Number
                                        split[1] = textBox66.Text;                               // Parts Description
                                        split[2] = textBox6.Text;                                // Date In
                                        split[3] = textBox12.Text;                               // Buyer
                                        split[4] = textBox64.Text;                               // On Backorder
                                        split[5] = textBox11.Text;                               // Date Ordered
                                        split[6] = TECHNICIAN;                                   // Technician Name
                                        split[7] = WARRANTYSTATUS;                               // Warranty Status
                                        split[8] = textBox14.Text;                               // Taxable
                                        split[9] = label30.Text.ToUpper();                       // Company
                                        split[10] = ADDR;                                        // Address 1
                                        split[11] = ADDR2;                                       // Address 2
                                        split[12] = ADDR3;                                       // Address 3
                                        split[13] = CSZ;                                         // City State Zip
                                        split[14] = CODE;                                        // Code - JBL, QSC, etc
                                        split[15] = PHONE;                                       // Company Phone #
                                        split[16] = textBox11.Text;                              // Date Entered
                                        split[17] = textBox68.Text;                              // Quantity
                                        split[18] = textBox67.Text;                              // Part Number
                                        split[19] = textBox65.Text;                              // Cost
                                    }
                                    if (split[20] == "10")
                                    {
                                        split[0] = CLAIM;                                        // PO Number
                                        split[1] = textBox72.Text;                               // Parts Description
                                        split[2] = textBox6.Text;                                // Date In
                                        split[3] = textBox12.Text;                               // Buyer
                                        split[4] = textBox70.Text;                               // On Backorder
                                        split[5] = textBox11.Text;                               // Date Ordered
                                        split[6] = TECHNICIAN;                                   // Technician Name
                                        split[7] = WARRANTYSTATUS;                               // Warranty Status
                                        split[8] = textBox14.Text;                               // Taxable
                                        split[9] = label30.Text.ToUpper();                       // Company
                                        split[10] = ADDR;                                        // Address 1
                                        split[11] = ADDR2;                                       // Address 2
                                        split[12] = ADDR3;                                       // Address 3
                                        split[13] = CSZ;                                         // City State Zip
                                        split[14] = CODE;                                        // Code - JBL, QSC, etc
                                        split[15] = PHONE;                                       // Company Phone #
                                        split[16] = textBox11.Text;                              // Date Entered
                                        split[17] = textBox74.Text;                              // Quantity
                                        split[18] = textBox73.Text;                              // Part Number
                                        split[19] = textBox71.Text;                              // Cost
                                    }
                                    if (split[20] == "11")
                                    {
                                        split[0] = CLAIM;                                        // PO Number
                                        split[1] = textBox78.Text;                               // Parts Description
                                        split[2] = textBox6.Text;                                // Date In
                                        split[3] = textBox12.Text;                               // Buyer
                                        split[4] = textBox76.Text;                               // On Backorder
                                        split[5] = textBox11.Text;                               // Date Ordered
                                        split[6] = TECHNICIAN;                                   // Technician Name
                                        split[7] = WARRANTYSTATUS;                               // Warranty Status
                                        split[8] = textBox14.Text;                               // Taxable
                                        split[9] = label30.Text.ToUpper();                       // Company
                                        split[10] = ADDR;                                        // Address 1
                                        split[11] = ADDR2;                                       // Address 2
                                        split[12] = ADDR3;                                       // Address 3
                                        split[13] = CSZ;                                         // City State Zip
                                        split[14] = CODE;                                        // Code - JBL, QSC, etc
                                        split[15] = PHONE;                                       // Company Phone #
                                        split[16] = textBox11.Text;                              // Date Entered
                                        split[17] = textBox80.Text;                              // Quantity
                                        split[18] = textBox79.Text;                              // Part Number
                                        split[19] = textBox77.Text;                              // Cost
                                    }
                                    if (split[20] == "12")
                                    {
                                        split[0] = CLAIM;                                        // PO Number
                                        split[1] = textBox90.Text;                               // Parts Description
                                        split[2] = textBox6.Text;                                // Date In
                                        split[3] = textBox12.Text;                               // Buyer
                                        split[4] = textBox88.Text;                               // On Backorder
                                        split[5] = textBox11.Text;                               // Date Ordered
                                        split[6] = TECHNICIAN;                                   // Technician Name
                                        split[7] = WARRANTYSTATUS;                               // Warranty Status
                                        split[8] = textBox14.Text;                               // Taxable
                                        split[9] = label30.Text.ToUpper();                       // Company
                                        split[10] = ADDR;                                        // Address 1
                                        split[11] = ADDR2;                                       // Address 2
                                        split[12] = ADDR3;                                       // Address 3
                                        split[13] = CSZ;                                         // City State Zip
                                        split[14] = CODE;                                        // Code - JBL, QSC, etc
                                        split[15] = PHONE;                                       // Company Phone #
                                        split[16] = textBox11.Text;                              // Date Entered
                                        split[17] = textBox92.Text;                              // Quantity
                                        split[18] = textBox91.Text;                              // Part Number
                                        split[19] = textBox89.Text;                              // Cost
                                    }
                                    if (split[20] == "13")
                                    {
                                        split[0] = CLAIM;                                        // PO Number
                                        split[1] = textBox84.Text;                               // Parts Description
                                        split[2] = textBox6.Text;                                // Date In
                                        split[3] = textBox12.Text;                               // Buyer
                                        split[4] = textBox82.Text;                               // On Backorder
                                        split[5] = textBox11.Text;                               // Date Ordered
                                        split[6] = TECHNICIAN;                                   // Technician Name
                                        split[7] = WARRANTYSTATUS;                               // Warranty Status
                                        split[8] = textBox14.Text;                               // Taxable
                                        split[9] = label30.Text.ToUpper();                       // Company
                                        split[10] = ADDR;                                        // Address 1
                                        split[11] = ADDR2;                                       // Address 2
                                        split[12] = ADDR3;                                       // Address 3
                                        split[13] = CSZ;                                         // City State Zip
                                        split[14] = CODE;                                        // Code - JBL, QSC, etc
                                        split[15] = PHONE;                                       // Company Phone #
                                        split[16] = textBox11.Text;                              // Date Entered
                                        split[17] = textBox86.Text;                              // Quantity
                                        split[18] = textBox85.Text;                              // Part Number
                                        split[19] = textBox83.Text;                              // Cost
                                    }
                                    if (split[20] == "14")
                                    {
                                        split[0] = CLAIM;                                        // PO Number
                                        split[1] = textBox102.Text;                              // Parts Description
                                        split[2] = textBox6.Text;                                // Date In
                                        split[3] = textBox12.Text;                               // Buyer
                                        split[4] = textBox100.Text;                              // On Backorder
                                        split[5] = textBox11.Text;                               // Date Ordered
                                        split[6] = TECHNICIAN;                                   // Technician Name
                                        split[7] = WARRANTYSTATUS;                               // Warranty Status
                                        split[8] = textBox14.Text;                               // Taxable
                                        split[9] = label30.Text.ToUpper();                       // Company
                                        split[10] = ADDR;                                        // Address 1
                                        split[11] = ADDR2;                                       // Address 2
                                        split[12] = ADDR3;                                       // Address 3
                                        split[13] = CSZ;                                         // City State Zip
                                        split[14] = CODE;                                        // Code - JBL, QSC, etc
                                        split[15] = PHONE;                                       // Company Phone #
                                        split[16] = textBox11.Text;                              // Date Entered
                                        split[17] = textBox104.Text;                             // Quantity
                                        split[18] = textBox103.Text;                             // Part Number
                                        split[19] = textBox101.Text;                             // Cost
                                    }
                                    if (split[20] == "15")
                                    {
                                        split[0] = CLAIM;                                        // PO Number
                                        split[1] = textBox96.Text;                               // Parts Description
                                        split[2] = textBox6.Text;                                // Date In
                                        split[3] = textBox12.Text;                               // Buyer
                                        split[4] = textBox94.Text;                               // On Backorder
                                        split[5] = textBox11.Text;                               // Date Ordered
                                        split[6] = TECHNICIAN;                                   // Technician Name
                                        split[7] = WARRANTYSTATUS;                               // Warranty Status
                                        split[8] = textBox14.Text;                               // Taxable
                                        split[9] = label30.Text.ToUpper();                       // Company
                                        split[10] = ADDR;                                        // Address 1
                                        split[11] = ADDR2;                                       // Address 2
                                        split[12] = ADDR3;                                       // Address 3
                                        split[13] = CSZ;                                         // City State Zip
                                        split[14] = CODE;                                        // Code - JBL, QSC, etc
                                        split[15] = PHONE;                                       // Company Phone #
                                        split[16] = textBox11.Text;                              // Date Entered
                                        split[17] = textBox98.Text;                              // Quantity
                                        split[18] = textBox97.Text;                              // Part Number
                                        split[19] = textBox95.Text;                              // Cost
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

                using (StreamWriter writer = new StreamWriter(PurchOrder, false))
                {
                    foreach (String line in lines)
                        writer.WriteLine(line);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Tpass++;
            if (Tpass == 1)
            {
                pictureBox4.Visible = true;
            }
            if (Tpass > 1)
            {
                pictureBox4.Visible = false;
                timer1.Stop();
                Tpass = 0;
            }
        }
    }
}
    

