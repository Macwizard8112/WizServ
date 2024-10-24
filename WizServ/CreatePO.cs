using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Printing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.Windows.Forms;

namespace WizServ
{
    public partial class CreatePO : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        public Image Box2 = Properties.Resources.Box2;
        public Image Box3 = Properties.Resources.Box3;
        public Image WizLogo2 = Properties.Resources.WizLogo2;
        public Image WizLogo3 = Properties.Resources.WizLogo3;
        public Image Line1 = Properties.Resources.Line1;
        private readonly string MFG = @"I:\\Datafile\\Control\\MFG.CSV";                 // This is Read only CSV
        private readonly string PO = @"I:\\Datafile\\Control\\PO.CSV";                   // This is Read only CSV
        private readonly string Ordered = @"I:\\Datafile\\Control\\Ordered.CSV";         // This is Read only CSV
        private readonly string Database = @"I:\\Datafile\\Control\\Database.CSV";       // This is Read only CSV
        private readonly string PRI = @"I:\\Datafile\\Control\\PRI.CSV";                 // This is Read only CSV
        private readonly string PARTSUSED = @"I:\\Datafile\\Control\\PartsUsed.CSV";     // This is Read only CSV
        private readonly string POPEEPS = @"I:\\Datafile\\Control\\PO_PEEPS.CSV";        // This is Read Only CSV    
        private readonly string TechTypes = @"I:\\Datafile\\Control\\TechTypes.CSV";     // This is Read Only CSV
        private readonly string Data = @"I:\\Datafile\\Doc\\Data.CSV";                   // Ordered Status DB
        private readonly string PurchOrder = @"I:\\Datafile\\Control\\PurchOrder.CSV";   // This is Read Only CSV
        public int butpress, loopCount, INDEXCOUNT;

        public PrintPageEventHandler printDoc_PrintPage { get; private set; }

        public string drawString, selectedText, dateend, po_num;
        public string MANF, ADDR, ADDR2, ADDR3, CITY, CSZ, CODE, TERMS, BUYER, TO, REMARKS, TAX, claim_no, SHIPPER, SearchTerm;
        public int foundcount2, PrintLine, TheIndexIs;
        public string mTab = "\t", mTab2 = "\t\t";
        public decimal decimalRounded, decRounded;
        public string t = "0", DATEIN, BUYERPULL;
        public decimal t_price, mBOCost, partstotal, totRounded;
        private string dd, mTab3;
        public string mPART, mDesc, mOur_Cost, mStock, mCust_Cost, mIndex, mVendPN, mVendor;
        public string One, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Eleven, Twelve;
        public string aOne, aTwo, aThree, aFour, aFive, aSix, aSeven, aEight, aNine, aTen, aEleven, aTwelve;    // Line 1
        public string bOne, bTwo, bThree, bFour, bFive, bSix, bSeven, bEight, bNine, bTen, bEleven, bTwelve;    // Line 2
        public string cOne, cTwo, cThree, cFour, cFive, cSix, cSeven, cEight, cNine, cTen, cEleven, cTwelve;    // Line 3
        public string dOne, dTwo, dThree, dFour, dFive, dSix, dSeven, dEight, dNine, dTen, dEleven, dTwelve;    // Line 4
        public string eOne, eTwo, eThree, eFour, eFive, eSix, eSeven, eEight, eNine, eTen, eEleven, eTwelve;    // Line 5
        public string fOne, fTwo, fThree, fFour, fFive, fSix, fSeven, fEight, fNine, fTen, fEleven, fTwelve;    // Line 6
        public string gOne, gTwo, gThree, gFour, gFive, gSix, gSeven, gEight, gNine, gTen, gEleven, gTwelve;    // Line 7
        public string hOne, hTwo, hThree, hFour, hFive, hSix, hSeven, hEight, hNine, hTen, hEleven, hTwelve;    // Line 8
        public string iOne, iTwo, iThree, iFour, iFive, iSix, iSeven, iEight, iNine, iTen, iEleven, iTwelve;    // Line 9
        public string jOne, jTwo, jThree, jFour, jFive, jSix, jSeven, jEight, jNine, jTen, jEleven, jTwelve;    // Line 10
        public string kOne, kTwo, kThree, kFour, kFive, kSix, kSeven, kEight, kNine, kTen, kEleven, kTwelve;    // Line 11
        public string lOne, lTwo, lThree, lFour, lFive, lSix, lSeven, lEight, lNine, lTen, lEleven, lTwelve;    // Line 12
        public string mOne, mTwo, mThree, mFour, mFive, mSix, mSeven, mEight, mNine, mTen, mEleven, mTwelve;    // Line 13
        public string nOne, nTwo, nThree, nFour, nFive, nSix, nSeven, nEight, nNine, nTen, nEleven, nTwelve;    // Line 14
        public string oOne, oTwo, oThree, oFour, oFive, oSix, oSeven, oEight, oNine, oTen, oEleven, oTwelve;    // Line 15
        public string pOne, pTwo, pThree, pFour, pFive, pSix, pSeven, pEight, pNine, pTen, pEleven, pTwelve;    // Line 16
        public decimal aTotal, bTotal, cTotal, dTotal, eTotal, fTotal, gTotal, hTotal, iTotal, jTotal, kTotal, lTotal, mTotal, nTotal, oTotal, pTotal;
        public int PASS, ItemTotalCount;
        public bool CLAIMFOUND = false;
        public string space = "                                        ", WARRANTYSTATUS, TECHNICIAN, ENTRYDATE;

        public CreatePO()
        {
            InitializeComponent();
            Icon = image100;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
            label13.Visible = false;
            ENTRYDATE = DateTime.Now.ToShortDateString();
            Copies();
            SetWidthDesc();
            LoadCombox2();
            textBox1.Select();
            GetDateInfo();
        }

        private void Copies()                       // Number of copies to print
        {
            comboBox8.Items.Add("1");   // Will print "Part Managers Copy" on printout
            comboBox8.Items.Add("2");   // Will print "Lynnette's Copy" on printout
            comboBox8.Items.Add("3");
            comboBox8.Items.Add("4");
            comboBox8.Items.Add("5");
            comboBox8.SelectedIndex = 0;
        }
        
        public void SetWidthDesc()
        {
            aFour += space;
            bFour += space;
            cFour += space;
            dFour += space;
            eFour += space;
            fFour += space;
            gFour += space;
            hFour += space;
            iFour += space;
            jFour += space;
            kFour += space;
            lFour += space;
            mFour += space;
            nFour += space;
            oFour += space;
            pFour += space;
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            TO = comboBox5.Text;
            comboBox6.Select();
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchTerm = textBox3.Text;
                if (SearchTerm == "*")
                {
                    GetPartsInfo();
                }
                if (SearchTerm.Length <= 1)
                {
                    textBox3.Select();
                }
                if (SearchTerm.Length >= 2)
                {
                    //PartsOrdered();
                    GetPartsInfo();
                }
                GetPartsInfo();
            }
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            TAX = comboBox7.Text;
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            REMARKS = comboBox6.Text;
            comboBox6.Select();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SHIPPER = comboBox2.Text;
            comboBox3.Select();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            BUYER = comboBox4.Text;
            comboBox5.Select();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            TERMS = comboBox3.Text;
            if (TERMS == "CC")
            {
                comboBox8.Text = "2";
            }
            comboBox4.Select();
        }

        private void comboBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SHIPPER = comboBox2.Text;
                comboBox3.Select();
            }
        }

        private void LoadCombox2()
        {
            comboBox2.Items.Add("UPS");
            comboBox2.Items.Add("FedEx");
            comboBox2.Items.Add("DHL");
            comboBox2.Items.Add("U.S. Mail");
            comboBox2.Items.Add("Courier");
            comboBox2.Items.Add("Other");
            comboBox2.SelectedIndex = 0;
            comboBox3.Items.Add("OA");
            comboBox3.Items.Add("CC");
            comboBox3.SelectedIndex = 0;
            comboBox7.Items.Add("No");
            comboBox7.Items.Add("Yes");
            comboBox7.SelectedIndex = 0;
            GetPeople();
            GetTechTypes();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox1.TextLength <= 5)
                {
                    textBox1.Select();
                }
                claim_no = textBox1.Text;
                GetPONum();
                PullFromDB();
                if (CLAIMFOUND == false)
                {
                    MessageBox.Show("WARNING\nThis Claim " + textBox1.Text + " is NOT in Database.csv");
                }
                richTextBox2.Text = "";
                PartsOrdered();
                comboBox1.Select();
            }
        }

        public void GetDateInfo()
        {
            var da = DateTime.Now.ToShortDateString();
            var ds = da.Length;
            dateend = da.Substring(ds - 2, 2);
            textBox1.Text = dateend;
        }


        private void CreatePO_Load(object sender, EventArgs e)
        {
            GetData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedText = comboBox1.Text;
            GetMFG();
            comboBox2.Select();
        }

        public void GetData()
        {
            try
            {
                //var fs = new FileStream(MFG, FileMode.Open, (FileAccess)(FileShare.ReadWrite | FileShare.Delete));
                //var reader = new StreamReader(fs);
                StreamReader reader = new StreamReader(MFG, Encoding.GetEncoding("Windows-1252"));
                String line = reader.ReadLine();

                List<string> listA = new List<string>();

                loopCount = 0;

                while (!reader.EndOfStream)
                {
                    var lineRead = reader.ReadLine();
                    var values = lineRead.Split(',');

                    listA.Add(values[0]);       //  war_prd         Unused

                    var itms = listA[loopCount];

                    comboBox1.Items.Add(itms);
                    loopCount++;
                }

                }
            catch (Exception ex)
            {
                MessageBox.Show("Error line 273: \n" + ex);
            }
        }

        public void GetPONum()
        {
            List<string> list = new List<string>();

            try
            {
                StreamReader reader = new StreamReader(PO, Encoding.GetEncoding("Windows-1252"));
                String line = reader.ReadLine();

                List<string> listA = new List<string>();

                loopCount = 0;

                while (!reader.EndOfStream)
                {
                    var lineRead = reader.ReadLine();
                    var values = lineRead.Split(',');

                    listA.Add(values[0]);       //  Claim Number
                    
                    var itms = listA[loopCount];
                    if (itms.Contains(claim_no))
                    {
                        list.Add(itms);
                        comboBox1.Items.Add(itms);
                    }
                    loopCount++;
                }
                string[] array = list.ToArray();
                reader.Close();
                var k = -1;
                var p = "";
                foreach (string i in array)
                {
                    p = i;
                    k++;
                }
                var eos = p.Length;
                if (eos == 6)
                {
                    p += ".00";
                    eos = p.Length;
                }
                var i1 = p.Substring(eos - 3,3);
                if (i1.Contains("."))
                {
                    var m = p.Substring(eos - 1, 1);
                    var j = p.Substring(eos - 2, 1);
                    var x = j + m;
                    if (x.Contains("."))
                    {

                    }
                    else
                    {
                        if (x.Length >= 2)
                        {
                            if (x.Substring(0, 1) == "0")
                            {
                                var e = Convert.ToInt32(x);
                                e++;
                                switch (e)
                                {
                                    case 10:
                                        j = e.ToString();
                                        break;
                                    case 20:
                                        j = e.ToString();
                                        break;
                                    case 30:
                                        j = e.ToString();
                                        break;
                                    default:
                                        j = "0" + e.ToString();
                                        break;
                                }
                                
                                m = j;
                            }
                            else
                            {
                                var e = Convert.ToInt32(x);
                                e++;
                                j = e.ToString();
                              
                            }
                        }
                    }

                    switch (m)
                    {
                        case "0":
                            m = j;
                            break;
                        case "1":
                            var f0 = Convert.ToInt32(m);
                            f0++;
                            j = f0.ToString();
                            break;
                        case "2":
                            var f1 = Convert.ToInt32(m);
                            f1++;
                            j = f1.ToString();
                            break;
                        case "3":
                            var f2 = Convert.ToInt32(m);
                            f2++;
                            j = f2.ToString();
                            break;
                        case "03":
                            p = p.Substring(0, 6) + m;
                            break;
                        case "4":
                            var f3 = Convert.ToInt32(m);
                            f3++;
                            j = f3.ToString();
                            break;
                        case "5":
                            var f4 = Convert.ToInt32(m);
                            f4++;
                            j = f4.ToString();
                            break;
                        case "6":
                            var f5 = Convert.ToInt32(m);
                            f5++;
                            j = f5.ToString();
                            break;
                        case "7":
                            var f6 = Convert.ToInt32(m);
                            f6++;
                            j = f6.ToString();
                            break;
                        case "8":
                            var f7 = Convert.ToInt32(m);
                            f7++;
                            j = f7.ToString();
                            break;
                        case "9":
                            var f8 = Convert.ToInt32(m);
                            f8++;
                            j = f8.ToString();
                            break;
                        default:

                            break;
                    }
                    if (j == null || j == "")
                    {

                    }
                    else
                    {
                        m = p.Substring(0, 6) + "." + j;
                        if (m.Length <= 9)
                        {

                        }
                        else
                        {
                            m += "0";
                        }
                        textBox2.Text = m;
                        po_num = m;
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error line 446: \n" + ex);
            }
        }

        public void GetPeople()
        {
            try
            {
                StreamReader reader = new StreamReader(POPEEPS, Encoding.GetEncoding("Windows-1252"));
                String line = reader.ReadLine();

                List<string> listA = new List<string>();
                List<string> listB = new List<string>();

                loopCount = 0;

                while (!reader.EndOfStream)
                {
                    var lineRead = reader.ReadLine();
                    var values = lineRead.Split(',');

                    listA.Add(values[0]);       //  Name
                    listB.Add(values[1]);       //  Abbr

                    comboBox4.Items.Add(listA[loopCount]);
                    comboBox5.Items.Add(listA[loopCount]);

                    loopCount++;
                }
                comboBox4.SelectedIndex = 0;
                comboBox5.SelectedIndex = 0;
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error line 481: \n" + ex);
            }
        }

        public void GetTechTypes()              // Tech Name & Warranty Status
        {
            try
            {
                StreamReader reader = new StreamReader(TechTypes, Encoding.GetEncoding("Windows-1252"));
                String line = reader.ReadLine();

                List<string> listA = new List<string>();

                loopCount = 0;

                while (!reader.EndOfStream)
                {
                    var lineRead = reader.ReadLine();
                    var values = lineRead.Split(',');

                    listA.Add(values[0]);       //  Tech Name & Warranty Status

                    comboBox6.Items.Add(listA[loopCount]);

                    loopCount++;
                }
                comboBox6.SelectedIndex = 0;
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error line 512: \n" + ex);
            }
        }

        public void GetMFG()
        {
            try
            {
                StreamReader reader = new StreamReader(MFG, Encoding.GetEncoding("Windows-1252"));
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

                loopCount = 0;

                while (!reader.EndOfStream)
                {
                    var lineRead = reader.ReadLine();
                    var values = lineRead.Split(',');

                    listA.Add(values[0]);       //  MFG             Manufacturer
                    listB.Add(values[1]);       //  Addr            Address, City, State
                    listC.Add(values[2]);       //  
                    listD.Add(values[3]);       //  
                    listE.Add(values[4]);       //  CSZ             City, State, Zip
                    listF.Add(values[5]);       //  
                    listG.Add(values[6]);       //  MFG_Code        Dhort name for MFG
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
                    listS.Add(values[18]);      //  Phone

                    var itms = listA[loopCount];

                    if (selectedText == listA[loopCount])
                    {
                        MANF = listA[loopCount];
                        ADDR = listB[loopCount];
                        ADDR2 = listC[loopCount];
                        ADDR3 = listD[loopCount];
                        CITY = listE[loopCount];
                        CSZ = listI[loopCount];
                        CODE = listS[loopCount];
                    }
                    loopCount++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error line 587: \n" + ex);
            }
        }

        private void UpdatePOdb()
        {
            var csv = new StringBuilder();
            try
            {
                string first = po_num;
                var newLine = first + Environment.NewLine;
                csv.Append(newLine);

                File.AppendAllText(PO, csv.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error line 604 " + ex);
            }
        }

        private void button1_Click(object sender, EventArgs e)      // Print
        {
            UpdatePOdb();       //  Update PO database
            var zcount = Convert.ToInt32(comboBox8.Text);
            for (int i = 0; i < zcount; i++)
            {
                if (i == 0)
                {
                    PrintLine = 1;
                }
                if (i == 1)
                {
                    PrintLine = 2;
                }
                try
                {
                    PrintDocument pd = new PrintDocument();     // Print Purchase Order
                    pd.PrintPage += PrintPage;
                    pd.Print();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Line 630\nSorry a print error occured\n" + ex);
                }
            }
        }

        private void PartsOrdered()
        {
            try
            {
                StreamReader reader = new StreamReader(Ordered);
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

                loopCount = 0;

                while (!reader.EndOfStream)
                {
                    var lineRead = reader.ReadLine();
                    var values = lineRead.Split(',');

                    listA.Add(values[0]);   // Qty Used
                    listB.Add(values[1]);   // Part_no
                    listC.Add(values[2]);   // Claim_Number
                    listD.Add(values[3]);   // Description
                    listE.Add(values[4]);   // Price
                    listF.Add(values[5]);   // Claim_no
                    listG.Add(values[6]);   // Cost
                    listH.Add(values[7]);   // Part_Date
                    listI.Add(values[8]);   // Ppurch Date
                    listJ.Add(values[9]);   // Part in Claim
                    listK.Add(values[10]);  // Back Ordered
                    listL.Add(values[11]);  // Index

                    if (listD[loopCount] == "SHOP SUPPLIES")
                    {
                        listD[loopCount] += "\t\t    ";
                    }
                    var xPN = listB[loopCount];
                    if (xPN == "2")
                    {
                        xPN = "0002\t\t";
                    }
                    if (xPN.Length >= 12)
                    {
                        xPN += "\t";
                    }
                    if (xPN.Length == 11)
                    {
                        xPN += "\t";
                    }
                    if (xPN.Length <= 10)
                    {
                        xPN += "\t";
                    }

                    var xPrice = decimal.Parse(listE[loopCount]);
                    var xPrice2 = xPrice.ToString("C2");

                    if (listF[loopCount] == claim_no)
                    {
                        switch (xPrice2.Length)
                        {
                            case 1:
                                xPrice2 += "    ";
                                break;
                            case 2:
                                xPrice2 += "    ";
                                break;
                            case 3:
                                xPrice2 += "    ";
                                break;
                            case 4:
                                xPrice2 += "    ";
                                break;
                            case 5:
                                xPrice2 = "    " + xPrice2 + "\t";
                                break;
                            case 6:
                                xPrice2 = "  " + xPrice2 + "\t";
                                break;
                            case 7:
                                xPrice2 = "" + xPrice2 + "\t";
                                break;
                            case 8:
                                xPrice2 += "    ";
                                break;
                            case 9:
                                xPrice2 += "    ";
                                break;
                            case 10:
                                xPrice2 += "    ";
                                break;
                        }
                        if (listD[loopCount].Length >= 22)
                        {
                            dd = listD[loopCount].Substring(0, 22);
                            mTab3 = "";
                        }
                        else
                        {
                            dd = listD[loopCount];
                            mTab3 = "\t";
                        }
                        richTextBox2.Text = richTextBox2.Text + listA[loopCount] + "\t" + xPN + "\t" + xPrice2 + "\t" + dd + "\t\t" + mTab3 + listK[loopCount] + "\t\t" + listL[loopCount] + "\n";
                        foundcount2++;
                        mBOCost += xPrice;
                    }
                    loopCount++;
                }
                reader.Close(); // Close the open file
                partstotal = mBOCost + totRounded;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Line 756:\n Sorry an error has occured: " + ex.Message);
            }
        }

        private void PullFromDB()
        {
            try
            {
                StreamReader reader = new StreamReader(Database);
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

                    if (listB[loopCount] == claim_no)
                    {
                        DATEIN = listC[loopCount];
                        CLAIMFOUND = true;
                        WARRANTYSTATUS = listAT[loopCount];
                        TECHNICIAN = listAZ[loopCount];
                    }
                    loopCount++;
                }
                reader.Close(); // Close the open file
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Line 941:\n Sorry an error has occured: " + ex.Message);
            }
        }

        public void GetPartsInfo()
        { 
            richTextBox1.Text = "";
            try
            {
                StreamReader reader = new StreamReader(PRI);
                String line = reader.ReadLine();

                List<string> listA = new List<string>();
                List<string> listB = new List<string>();
                List<string> listC = new List<string>();
                List<string> listD = new List<string>();
                List<string> listE = new List<string>();
                List<string> listF = new List<string>();
                List<string> listG = new List<string>();
                List<string> listH = new List<string>();

                loopCount = 0;
                if (SearchTerm == "*")
                {
                    MessageBox.Show("This will take between a few\nseconds to a minute depending\nupon computer speed.\nPlease wait.");
                }
                while (!reader.EndOfStream)
                {
                    var lineRead = reader.ReadLine();
                    var values = lineRead.Split(',');

                    listA.Add(values[0]);   // Part_Num
                    listB.Add(values[1]);   // Description
                    listC.Add(values[2]);   // Our Cost $
                    listD.Add(values[3]);   // Stock (Qty in-house)
                    listE.Add(values[4]);   // Cust_Cost
                    listF.Add(values[5]);   // Index Number
                    listG.Add(values[6]);   // Vendor_PN
                    listH.Add(values[7]);   // Vendor

                    if (SearchTerm != "*")
                    {
                        if (listB[loopCount].Contains(SearchTerm) || listA[loopCount].Contains(SearchTerm))
                        {
                            decimal d = Convert.ToDecimal(listC[loopCount]);
                            string e = d.ToString("C2");
                            richTextBox1.Text = richTextBox1.Text + listF[loopCount] + "\t" + e + "\t" + listA[loopCount] + "\t" + listB[loopCount] + "\n";
                        }
                    }
                    if (SearchTerm == "*")
                    {
                        label13.Visible = true;
                        decimal d = Convert.ToDecimal(listC[loopCount]);
                        string e = d.ToString("C2");
                        richTextBox1.Text = richTextBox1.Text + listF[loopCount] + "\t" + e + "\t" + listA[loopCount] + "\t" + listB[loopCount] + "\n";
                    }
                    loopCount++;
                }
                reader.Close(); // Close the open file
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Line 1003:\nSorry an error has occured: " + ex.Message);
            }
        }

        private void GetDetailedInfo()
        {
            richTextBox1.Text = "";
            try
            {
                StreamReader reader = new StreamReader(PRI);
                String line = reader.ReadLine();

                List<string> listA = new List<string>();
                List<string> listB = new List<string>();
                List<string> listC = new List<string>();
                List<string> listD = new List<string>();
                List<string> listE = new List<string>();
                List<string> listF = new List<string>();
                List<string> listG = new List<string>();
                List<string> listH = new List<string>();

                loopCount = 0;

                while (!reader.EndOfStream)
                {
                    var lineRead = reader.ReadLine();
                    var values = lineRead.Split(',');

                    listA.Add(values[0]);   // Part_Num
                    listB.Add(values[1]);   // Description
                    listC.Add(values[2]);   // Our Cost $
                    listD.Add(values[3]);   // Stock (Qty in-house)
                    listE.Add(values[4]);   // Cust_Cost
                    listF.Add(values[5]);   // Index Number
                    listG.Add(values[6]);   // Vendor_PN
                    listH.Add(values[7]);   // Vendor

                    if (listF[loopCount] == selectedText)
                    {
                        mPART = listA[loopCount];
                        mDesc = listB[loopCount];
                        mOur_Cost = listC[loopCount];
                        mStock = listD[loopCount];
                        mCust_Cost = listE[loopCount];
                        mIndex = listF[loopCount];
                        mVendPN = listG[loopCount];
                        mVendor = listH[loopCount];
                        textBox5.Text = mPART;
                        textBox6.Text = mOur_Cost;
                        textBox7.Text = mDesc;
                        textBox9.Text = mCust_Cost;
                        textBox10.Text = mVendor;
                        textBox11.Text = mVendPN;
                    }
                    loopCount++;
                }
                reader.Close(); // Close the open file
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Line: 1063\nSorry an error has occured: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ItemTotalCount++;
            label13.Visible = false;
            if (ItemTotalCount >= 16)
            {
                MessageBox.Show("16 Maximum items per PO.");
            }
            OrderedIndex();                     // Get Next Index Number
            var csv = new StringBuilder();
            var comma = ",";

            One = textBox4.Text;                             // Qty
            Two = textBox5.Text;                             // Part Number
            Three = textBox2.Text;                           // Purchase Order Number
            Four = textBox7.Text;                            // Description
            Five = textBox6.Text;                            // Our Price / Cost
            Six = textBox1.Text;                             // Claim #
            Seven = textBox9.Text;                           // Customer Cost
            Eight = DateTime.Now.ToShortDateString();        // Part Date
            Nine = DateTime.Now.ToShortDateString();         // Purchase Date
            Ten = "Y";                                       // In Claim (Y/N)
            Eleven = textBox8.Text.ToUpper();                // Back Ordered
            Twelve = TheIndexIs.ToString();                  // Index Number

            var newLine = (One + comma + Two + comma + Three + comma + Four + comma + Five + comma + Six + comma + Seven 
                + comma + Eight + comma + Nine + comma + Ten + comma + Eleven + comma + Twelve + Environment.NewLine);
            csv.Append(newLine);
            try
            {
                File.AppendAllText(Ordered, csv.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error line 1101\n" + ex);
            }
            UpdatePurchOrderDB();                           // Update Master Copy of PO Database
            UpdatePartsUsed();                              // Update Parts Used DB
            richTextBox2.Text = "";
            PartsOrdered();                                 // Refresh Parts in Claim
            PASS++;
            label10.Text = "Line Count: " + PASS.ToString();
            GetAllVariables();                              // Store all variables for multiple items
            UpdateDateDB();
            ClearTextBoxes();
        }

        private void UpdateDateDB()
        {
            var csv = new StringBuilder();
            var comma = ",";
            var mThree = "No";

            DateTime today = DateTime.Now;
            DateTime answer = today.AddDays(35);
            //if (Four.Contains("FREIGHT"))
            //{
            //    return;
            //}
            var newLine = (Three + comma + Four + comma + mThree + comma + Eleven + comma + answer + comma + TECHNICIAN + comma + WARRANTYSTATUS + Environment.NewLine);
            csv.Append(newLine);
            try
            {
                File.AppendAllText(Data, csv.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error line 1136\n" + ex);
            }
        }

        private void UpdatePurchOrderDB()
        {
            INDEXCOUNT++;
            var csv = new StringBuilder();
            var comma = ",";
            var mThree = "No";

            DateTime today = DateTime.Now;
            DateTime answer = today.AddDays(35);
            //if (Four.Contains("FREIGHT"))
            //{
            //    return;
            //}
            var newLine = textBox2.Text + comma + Four + comma + DATEIN + comma + BUYER + comma + Eleven + comma + answer + comma + TECHNICIAN 
                + comma + WARRANTYSTATUS + comma + TAX + comma + MANF + comma + ADDR + comma + ADDR2 + comma + ADDR3 + comma + CITY + comma 
                + CSZ + comma + CODE + comma + ENTRYDATE + comma +  One + comma + Two + comma + Five + comma + INDEXCOUNT.ToString() + Environment.NewLine;
            csv.Append(newLine);
            try
            {
                File.AppendAllText(PurchOrder, csv.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error line 1160\n" + ex);
            }
        }

        private void ClearTextBoxes()
        {
            textBox4.Text = "1";
            textBox5.Text = ".";
            textBox6.Text = ".";
            textBox7.Text = ".";
            textBox8.Text = "N";
            textBox9.Text = ".";
            textBox10.Text = ".";
            textBox11.Text = ".";
        }

        private void UpdatePartsUsed()
        {
            var csv = new StringBuilder();
            var comma = ",";

            //if (Four.Contains("FREIGHT"))       // Do not wite to PartsUsed.csv if description is "FREIGHT_IN
            //{
            //    return;
            //}
            One = textBox4.Text;                             // Qty
            Two = textBox5.Text;                             // Part Number
            Three = textBox2.Text;                           // Purchase Order Number
            Four = textBox7.Text;                            // Description
            Five = textBox6.Text;                            // Our Price / Cost
            Six = textBox1.Text;                             // Claim #
            Seven = textBox9.Text;                           // Customer Cost
            Eight = DateTime.Now.ToShortDateString();        // Part Date
            Nine = DateTime.Now.ToShortDateString();         // Purchase Date
            Ten = "Y";                                       // In Claim (Y/N)
            Eleven = textBox8.Text.ToUpper();                // Back Ordered
            Twelve = TheIndexIs.ToString();                  // Index Number
            if (One == "")
            {
                One = "1";
            }
            var newLine = (One + comma + Two + comma + Six + comma + Four + comma + Seven + comma + Six + comma + Five
                + comma + Eight + comma + Nine + comma + Ten + Environment.NewLine);
            csv.Append(newLine);
            try
            {
                File.AppendAllText(PARTSUSED, csv.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error line 1210\n " + ex);
            }
        }

        public void GetAllVariables()
        {
            switch (PASS)
            {
                case 1:
                    aOne = One;
                    aTwo = Two;
                    aThree = Three;
                    aFour = Four + "                                           ";
                    aFive = Five;
                    aSix = Six;
                    aSeven = Seven;
                    aEight = Eight;
                    aNine = Nine;
                    aTen = Ten;
                    aEleven = Eleven;
                    aTwelve = Twelve;
                    break;
                case 2:
                    bOne = One;
                    bTwo = Two;
                    bThree = Three;
                    bFour = Four + "                                           ";
                    bFive = Five;
                    bSix = Six;
                    bSeven = Seven;
                    bEight = Eight;
                    bNine = Nine;
                    bTen = Ten;
                    bEleven = Eleven;
                    bTwelve = Twelve;
                    break;
                case 3:
                    cOne = One;
                    cTwo = Two;
                    cThree = Three;
                    cFour = Four + "                                           ";
                    cFive = Five;
                    cSix = Six;
                    cSeven = Seven;
                    cEight = Eight;
                    cNine = Nine;
                    cTen = Ten;
                    cEleven = Eleven;
                    cTwelve = Twelve;
                    break;
                case 4:
                    dOne = One;
                    dTwo = Two;
                    dThree = Three;
                    dFour = Four + "                                           ";
                    dFive = Five;
                    dSix = Six;
                    dSeven = Seven;
                    dEight = Eight;
                    dNine = Nine;
                    dTen = Ten;
                    dEleven = Eleven;
                    dTwelve = Twelve;
                    break;
                case 5:
                    eOne = One;
                    eTwo = Two;
                    eThree = Three;
                    eFour = Four + "                                           ";
                    eFive = Five;
                    eSix = Six;
                    eSeven = Seven;
                    eEight = Eight;
                    eNine = Nine;
                    eTen = Ten;
                    eEleven = Eleven;
                    eTwelve = Twelve;
                    break;
                case 6:
                    fOne = One;
                    fTwo = Two;
                    fThree = Three;
                    fFour = Four + "                                             ";
                    fFive = Five;
                    fSix = Six;
                    fSeven = Seven;
                    fEight = Eight;
                    fNine = Nine;
                    fTen = Ten;
                    fEleven = Eleven;
                    fTwelve = Twelve;
                    break;
                case 7:
                    gOne = One;
                    gTwo = Two;
                    gThree = Three;
                    gFour = Four + "                                           ";
                    gFive = Five;
                    gSix = Six;
                    gSeven = Seven;
                    gEight = Eight;
                    gNine = Nine;
                    gTen = Ten;
                    gEleven = Eleven;
                    gTwelve = Twelve;
                    break;
                case 8:
                    hOne = One;
                    hTwo = Two;
                    hThree = Three;
                    hFour = Four + "                                           ";
                    hFive = Five;
                    hSix = Six;
                    hSeven = Seven;
                    hEight = Eight;
                    hNine = Nine;
                    hTen = Ten;
                    hEleven = Eleven;
                    hTwelve = Twelve;
                    break;
                case 9:
                    iOne = One;
                    iTwo = Two;
                    iThree = Three;
                    iFour = Four + "                                           ";
                    iFive = Five;
                    iSix = Six;
                    iSeven = Seven;
                    iEight = Eight;
                    iNine = Nine;
                    iTen = Ten;
                    iEleven = Eleven;
                    iTwelve = Twelve;
                    break;
                case 10:
                    jOne = One;
                    jTwo = Two;
                    jThree = Three;
                    jFour = Four + "                                           ";
                    jFive = Five;
                    jSix = Six;
                    jSeven = Seven;
                    jEight = Eight;
                    jNine = Nine;
                    jTen = Ten;
                    jEleven = Eleven;
                    jTwelve = Twelve;
                    break;
                case 11:
                    kOne = One;
                    kTwo = Two;
                    kThree = Three;
                    kFour = Four + "                                           ";
                    kFive = Five;
                    kSix = Six;
                    kSeven = Seven;
                    kEight = Eight;
                    kNine = Nine;
                    kTen = Ten;
                    kEleven = Eleven;
                    kTwelve = Twelve;
                    break;
                case 12:
                    lOne = One;
                    lTwo = Two;
                    lThree = Three;
                    lFour = Four + "                                           ";
                    lFive = Five;
                    lSix = Six;
                    lSeven = Seven;
                    lEight = Eight;
                    lNine = Nine;
                    lTen = Ten;
                    lEleven = Eleven;
                    lTwelve = Twelve;
                    break;
                case 13:
                    mOne = One;
                    mTwo = Two;
                    mThree = Three;
                    mFour = Four + "                                           ";
                    mFive = Five;
                    mSix = Six;
                    mSeven = Seven;
                    mEight = Eight;
                    mNine = Nine;
                    mTen = Ten;
                    mEleven = Eleven;
                    mTwelve = Twelve;
                    break;
                case 14:
                    nOne = One;
                    nTwo = Two;
                    nThree = Three;
                    nFour = Four + "                                           ";
                    nFive = Five;
                    nSix = Six;
                    nSeven = Seven;
                    nEight = Eight;
                    nNine = Nine;
                    nTen = Ten;
                    nEleven = Eleven;
                    nTwelve = Twelve;
                    break;
                case 15:
                    oOne = One;
                    oTwo = Two;
                    oThree = Three;
                    oFour = Four + "                                           ";
                    oFive = Five;
                    oSix = Six;
                    oSeven = Seven;
                    oEight = Eight;
                    oNine = Nine;
                    oTen = Ten;
                    oEleven = Eleven;
                    oTwelve = Twelve;
                    break;
                case 16:
                    MessageBox.Show("15 Max Per PO, create a new PO for 16 thru...");
                    break;
            }
        }

        private void richTextBox1_DoubleClick(object sender, EventArgs e)
        {
            selectedText = richTextBox1.SelectedText;
            GetDetailedInfo();
        }

        private void OrderedIndex()
        {
            var tt = 0;
            try
            {
                StreamReader reader = new StreamReader(Ordered);
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

                loopCount = 0;

                while (!reader.EndOfStream)
                {
                    var lineRead = reader.ReadLine();
                    var values = lineRead.Split(',');

                    listA.Add(values[0]);   // Qty Used
                    listB.Add(values[1]);   // Part_no
                    listC.Add(values[2]);   // Cliam_Number
                    listD.Add(values[3]);   // Description
                    listE.Add(values[4]);   // Price
                    listF.Add(values[5]);   // Claim_no
                    listG.Add(values[6]);   // Cost
                    listH.Add(values[7]);   // Part_Date
                    listI.Add(values[8]);   // Ppurch Date
                    listJ.Add(values[9]);   // Part in Claim
                    listK.Add(values[10]);  // Back Ordered
                    listL.Add(values[11]);  // Index


                    var nn = Convert.ToInt32(listL[loopCount]);
                    if (nn > tt)
                    {
                        tt = nn;
                        TheIndexIs = tt;
                    }
                    loopCount++;
                }
                reader.Close(); // Close the open file
                TheIndexIs++;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 1495: Sorry an error has occured: " + ex.Message);
            }
        }

        private void PrintPage(object o, PrintPageEventArgs e)
        {
            oTotal = 0;
            try
            {
                {

                    drawString = "Purchase Order: " + po_num;                           // Create string to draw.
                    Font drawFont = new Font("Arial", 16, FontStyle.Bold);              // Create font and brush.
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(500.0F, 10.0F);                       // Create point for upper-left corner of drawing.
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);  // Draw string to screen.
                }
                {
                    drawString = "Vendor #: " + CSZ;
                    Font drawFont = new Font("Arial", 12, FontStyle.Bold);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(500.0F, 40.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    drawString = "                  Wizard Electronics, Inc.";
                    Font drawFont = new Font("Arial", 10, FontStyle.Bold);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(160.0F, 80.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);

                }
                {
                    drawString = "                   554 Deering Road NW\n                   Atlanta, GA 30309\n                   Phone: 404-325-4891\n                   www.wizardelectronics.com";
                    Font drawFont = new Font("Arial", 10);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(160.0F, 100.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    drawString = "To:";
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(90.0F, 220.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    drawString = "Ship To:";
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(500.0F, 220.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    drawString = MANF + "\n" + ADDR + "\n" + ADDR2 + "\n" + ADDR3 + "\n" + CITY + "\n" + CODE;
                    Font drawFont = new Font("Arial", 10, FontStyle.Bold);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(100.0F, 240.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    drawString = "Wizard Electronics, Inc.\n554 Deering Road N.W.\nAtlanta, GA 30309\nUSA";
                    Font drawFont = new Font("Arial", 10, FontStyle.Bold);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(510.0F, 240.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    Image img = WizLogo3;                                   // Draw Wizard Logo (embedded)
                    Point loc = new Point(5, 5);
                    e.Graphics.DrawImage(img, loc);
                }
                {
                    Image img = Box2;                                       // Draw Frame (embedded)
                    Point loc = new Point(0, 370);
                    e.Graphics.DrawImage(img, loc);
                }
                {
                    drawString = "P.O. Date";
                    Font drawFont = new Font("Arial", 9, FontStyle.Bold);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(20.0F, 378.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    drawString = "Ship Via";
                    Font drawFont = new Font("Arial", 9, FontStyle.Bold);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(190.0F, 378.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    drawString = "F.O.B.";
                    Font drawFont = new Font("Arial", 9, FontStyle.Bold);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(430.0F, 378.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    drawString = "Terms";
                    Font drawFont = new Font("Arial", 9, FontStyle.Bold);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(660.0F, 378.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    drawString = DateTime.Now.ToShortDateString();
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(35.0F, 400.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    drawString = SHIPPER;
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(195.0F, 400.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    drawString = "          Origin";
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(390.0F, 400.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    drawString = TERMS;
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(665.0F, 400.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    drawString = "Buyer";
                    Font drawFont = new Font("Arial", 9, FontStyle.Bold);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(10.0F, 423.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    drawString = "Freight";
                    Font drawFont = new Font("Arial", 9, FontStyle.Bold);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(105.0F, 423.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    drawString = "Req. Date";
                    Font drawFont = new Font("Arial", 9, FontStyle.Bold);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(235.0F, 423.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    drawString = "Confirming To";
                    Font drawFont = new Font("Arial", 9, FontStyle.Bold);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(375.0F, 423.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    drawString = "Remarks";
                    Font drawFont = new Font("Arial", 9, FontStyle.Bold);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(615.0F, 423.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    drawString = "Tax";
                    Font drawFont = new Font("Arial", 9, FontStyle.Bold);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(758.0F, 423.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    if (BUYER.Contains("David"))
                    {
                        BUYER = "DBR";
                    }
                    if (BUYER.Contains("Cole"))
                    {
                        BUYER = "CH";
                    }
                    if (BUYER.Contains("Lynnette"))
                    {
                        BUYER = "LW";
                    }
                    drawString = BUYER;
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(15.0F, 445.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    drawString = aEight;
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(240.0F, 445.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    drawString = TO;
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(375.0F, 445.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    drawString = REMARKS;
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(550.0F, 445.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    if (TAX == "No")
                    {
                        TAX = "N";
                    }
                    else
                    {
                        TAX = "Y";
                    }
                    drawString = TAX;
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(765.0F, 445.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    drawString = "Qty.";
                    Font drawFont = new Font("Arial", 9, FontStyle.Bold);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(20.0F, 470.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    drawString = "Item Number";
                    Font drawFont = new Font("Arial", 9, FontStyle.Bold);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(80.0F, 470.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    drawString = "Description";
                    Font drawFont = new Font("Arial", 9, FontStyle.Bold);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(325.0F, 470.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    drawString = "Unit Cost";
                    Font drawFont = new Font("Arial", 9, FontStyle.Bold);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(550.0F, 470.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    drawString = "B/O";
                    Font drawFont = new Font("Arial", 9, FontStyle.Bold);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(632.0F, 470.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    drawString = "Extended Cost";
                    Font drawFont = new Font("Arial", 9, FontStyle.Bold);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(675.0F, 470.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                ///////////////////////////////////    LINE 1   ////////////////////////////////////////// 
                {
                    drawString = aOne;                                                              // Quantity
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(20.0F, 495.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    drawString = aTwo;                                                              // Item Number
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(70.0F, 495.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    var des = aFour.Substring(0, 39);
                    drawString = des;                                                               // Description
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(210.0F, 495.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    var s = Convert.ToDecimal(aFive);
                    aTotal = s;
                    if (aTotal == 0)    // move spacing if 0.00 over to the right
                    {
                        drawString = aTotal.ToString("C2");                                         // Unit Cost
                        Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                        SolidBrush drawBrush = new SolidBrush(Color.Black);
                        PointF drawPoint = new PointF(558.0F, 495.0F);
                        e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                    }
                    else
                    {
                        var s1 = Convert.ToDecimal(aFive);
                        aTotal = s1;
                        drawString = aTotal.ToString("C2");                                         // Unit Cost
                        Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                        SolidBrush drawBrush = new SolidBrush(Color.Black);
                        PointF drawPoint = new PointF(550.0F, 495.0F);
                        e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                    }
                }
                { 
                drawString = aEleven;                                                              // Back Order Status
                Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                SolidBrush drawBrush = new SolidBrush(Color.Black);
                PointF drawPoint = new PointF(635.0F, 495.0F);
                e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    var x = Convert.ToDecimal(aOne);
                    var s = Convert.ToDecimal(aFive);
                    aTotal = x * s;
                    drawString = aTotal.ToString("C2");                                             // Extended Cost
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(700.0F, 495.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                ///////////////////////////////////    LINE 2   ////////////////////////////////////////// 
                {
                    drawString = bOne;
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(20.0F, 515.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    drawString = bTwo;
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(70.0F, 515.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    var des = bFour.Substring(0, 39);
                    drawString = des;
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(210.0F, 515.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    var s = Convert.ToDecimal(bFive);
                    bTotal = s;
                    if (bTotal == 0)
                    {
                        drawString = bTotal.ToString("C2");
                        Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                        SolidBrush drawBrush = new SolidBrush(Color.Black);
                        PointF drawPoint = new PointF(558.0F, 515.0F);
                        e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                    }
                    else
                    {
                        var s1 = Convert.ToDecimal(bFive);
                        bTotal = s1;
                        drawString = bTotal.ToString("C2");
                        Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                        SolidBrush drawBrush = new SolidBrush(Color.Black);
                        PointF drawPoint = new PointF(550.0F, 515.0F);
                        e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                    }
                }
                {
                    drawString = bEleven;                                                              // Back Order Status
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(635.0F, 515.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    var x = Convert.ToDecimal(bOne);
                    var s = Convert.ToDecimal(bFive);
                    bTotal = x * s;
                    drawString = bTotal.ToString("C2");
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(700.0F, 515.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                ///////////////////////////////////    LINE 3   ////////////////////////////////////////// 
                {
                    drawString = cOne;
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(20.0F, 535.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    drawString = cTwo;
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(70.0F, 535.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    var desc = cFour.Substring(0, 39);
                    drawString = desc;
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(210.0F, 535.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    var s = Convert.ToDecimal(cFive);
                    cTotal = s;
                    if (cTotal == 0)
                    {
                        drawString = cTotal.ToString("C2");
                        Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                        SolidBrush drawBrush = new SolidBrush(Color.Black);
                        PointF drawPoint = new PointF(558.0F, 535.0F);
                        e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                    }
                    else
                    {
                        var s1 = Convert.ToDecimal(cFive);
                        cTotal = s1;
                        drawString = cTotal.ToString("C2");
                        Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                        SolidBrush drawBrush = new SolidBrush(Color.Black);
                        PointF drawPoint = new PointF(550.0F, 535.0F);
                        e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                    }
                    
                }
                {
                    drawString = cEleven;                                                              // Back Order Status
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(635.0F, 535.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    var x = Convert.ToDecimal(cOne);
                    var s = Convert.ToDecimal(cFive);
                    cTotal = x * s;
                    drawString = cTotal.ToString("C2");
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(700.0F, 535.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                ///////////////////////////////////    LINE 4   ////////////////////////////////////////// 
                {
                    drawString = dOne;
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(20.0F, 555.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    drawString = dTwo;
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(70.0F, 555.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    var desc = dFour.Substring(0, 39);
                    drawString = desc;
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(210.0F, 555.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    var s = Convert.ToDecimal(dFive);
                    dTotal = s;
                    if (dTotal == 0)
                    {
                        drawString = dTotal.ToString("C2");
                        Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                        SolidBrush drawBrush = new SolidBrush(Color.Black);
                        PointF drawPoint = new PointF(558.0F, 555.0F);
                        e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                    }
                    else
                    {
                        var s1 = Convert.ToDecimal(dFive);
                        dTotal = s1;
                        drawString = dTotal.ToString("C2");
                        Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                        SolidBrush drawBrush = new SolidBrush(Color.Black);
                        PointF drawPoint = new PointF(550.0F, 555.0F);
                        e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                    }
                }
                {
                    drawString = dEleven;                                                              // Back Order Status
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(635.0F, 555.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    var x = Convert.ToDecimal(dOne);
                    var s = Convert.ToDecimal(dFive);
                    dTotal = x * s;
                    drawString = dTotal.ToString("C2");
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(700.0F, 555.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                ///////////////////////////////////    LINE 5   ////////////////////////////////////////// 
                {
                    drawString = eOne;
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(20.0F, 575.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    drawString = eTwo;
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(70.0F, 575.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    var desc = eFour.Substring(0, 39);
                    drawString = desc;
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(210.0F, 575.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    var s = Convert.ToDecimal(eFive);
                    eTotal = s;
                    if (eTotal == 0)
                    {
                        drawString = eTotal.ToString("C2");
                        Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                        SolidBrush drawBrush = new SolidBrush(Color.Black);
                        PointF drawPoint = new PointF(558.0F, 575.0F);
                        e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                    }
                    else
                    {
                        var s1 = Convert.ToDecimal(eFive);
                        eTotal = s1;
                        drawString = eTotal.ToString("C2");
                        Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                        SolidBrush drawBrush = new SolidBrush(Color.Black);
                        PointF drawPoint = new PointF(550.0F, 575.0F);
                        e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                    }
                }
                {
                    drawString = eEleven;                                                              // Back Order Status
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(635.0F, 575.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    var x = Convert.ToDecimal(eOne);
                    var s = Convert.ToDecimal(eFive);
                    eTotal = x * s;
                    drawString = eTotal.ToString("C2");
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(700.0F, 575.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                ///////////////////////////////////    LINE 6   ////////////////////////////////////////// 
                {
                    drawString = fOne;
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(20.0F, 595.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    drawString = fTwo;
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(70.0F, 595.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    var desc = fFour.Substring(0, 39);
                    drawString = desc;
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(210.0F, 595.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    var s = Convert.ToDecimal(fFive);
                    fTotal = s;
                    if (fTotal == 0)
                    {
                        drawString = fTotal.ToString("C2");
                        Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                        SolidBrush drawBrush = new SolidBrush(Color.Black);
                        PointF drawPoint = new PointF(558.0F, 595.0F);
                        e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                    }
                    else
                    {
                        var s1 = Convert.ToDecimal(fFive);
                        fTotal = s1;
                        drawString = fTotal.ToString("C2");
                        Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                        SolidBrush drawBrush = new SolidBrush(Color.Black);
                        PointF drawPoint = new PointF(550.0F, 595.0F);
                        e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                    }
                }
                {
                    drawString = fEleven;                                                              // Back Order Status
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(635.0F, 595.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    var x = Convert.ToDecimal(fOne);
                    var s = Convert.ToDecimal(fFive);
                    fTotal = x * s;
                    drawString = fTotal.ToString("C2");
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(700.0F, 595.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                ///////////////////////////////////    LINE 7   ////////////////////////////////////////// 
                {
                    drawString = gOne;
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(20.0F, 615.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    drawString = gTwo;
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(70.0F, 615.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    var desc = gFour.Substring(0, 39);
                    drawString = desc;
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(210.0F, 615.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    var s = Convert.ToDecimal(gFive);
                    gTotal = s;
                    if (gTotal == 0)
                    {
                        drawString = gTotal.ToString("C2");
                        Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                        SolidBrush drawBrush = new SolidBrush(Color.Black);
                        PointF drawPoint = new PointF(558.0F, 615.0F);
                        e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                    }
                    else
                    {
                        var s1 = Convert.ToDecimal(gFive);
                        gTotal = s1;
                        drawString = gTotal.ToString("C2");
                        Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                        SolidBrush drawBrush = new SolidBrush(Color.Black);
                        PointF drawPoint = new PointF(550.0F, 615.0F);
                        e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                    }
                }
                {
                    drawString = gEleven;                                                              // Back Order Status
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(635.0F, 615.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    var x = Convert.ToDecimal(gOne);
                    var s = Convert.ToDecimal(gFive);
                    gTotal = x * s;
                    drawString = gTotal.ToString("C2");
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(700.0F, 615.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                ///////////////////////////////////    LINE 8   ////////////////////////////////////////// 
                {
                    drawString = hOne;
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(20.0F, 635.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    drawString = hTwo;
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(70.0F, 635.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    var desc = hFour.Substring(0, 39);
                    drawString = desc;
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(210.0F, 635.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    var s = Convert.ToDecimal(hFive);
                    hTotal = s;
                    if (hTotal == 0)
                    {
                        drawString = hTotal.ToString("C2");
                        Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                        SolidBrush drawBrush = new SolidBrush(Color.Black);
                        PointF drawPoint = new PointF(558.0F, 635.0F);
                        e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                    }
                    else
                    {
                        var s1 = Convert.ToDecimal(hFive);
                        hTotal = s1;
                        drawString = hTotal.ToString("C2");
                        Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                        SolidBrush drawBrush = new SolidBrush(Color.Black);
                        PointF drawPoint = new PointF(550.0F, 635.0F);
                        e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                    }
                }
                {
                    drawString = hEleven;                                                              // Back Order Status
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(635.0F, 635.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    var x = Convert.ToDecimal(hOne);
                    var s = Convert.ToDecimal(hFive);
                    hTotal = x * s;
                    drawString = hTotal.ToString("C2");
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(700.0F, 635.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                ///////////////////////////////////    LINE 9   ////////////////////////////////////////// 
                {
                    drawString = iOne;
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(20.0F, 655.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    drawString = iTwo;
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(70.0F, 655.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    var desc = iFour.Substring(0, 39);
                    drawString = desc;
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(210.0F, 655.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    var s = Convert.ToDecimal(iFive);
                    iTotal = s;
                    if (iTotal == 0)
                    {
                        drawString = iTotal.ToString("C2");
                        Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                        SolidBrush drawBrush = new SolidBrush(Color.Black);
                        PointF drawPoint = new PointF(558.0F, 655.0F);
                        e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                    }
                    else
                    {
                        var s1 = Convert.ToDecimal(iFive);
                        iTotal = s1;
                        drawString = iTotal.ToString("C2");
                        Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                        SolidBrush drawBrush = new SolidBrush(Color.Black);
                        PointF drawPoint = new PointF(550.0F, 655.0F);
                        e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                    }
                }
                {
                    drawString = iEleven;                                                              // Back Order Status
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(635.0F, 655.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    var x = Convert.ToDecimal(iOne);
                    var s = Convert.ToDecimal(iFive);
                    iTotal = x * s;
                    drawString = iTotal.ToString("C2");
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(700.0F, 655.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                ///////////////////////////////////    LINE 10   ////////////////////////////////////////// 
                {
                    drawString = jOne;
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(20.0F, 675.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    drawString = jTwo;
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(70.0F, 675.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    var desc = jFour.Substring(0, 39);
                    drawString = desc;
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(210.0F, 675.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    var s = Convert.ToDecimal(jFive);
                    jTotal = s;
                    if (jTotal == 0)
                    {
                        drawString = jTotal.ToString("C2");
                        Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                        SolidBrush drawBrush = new SolidBrush(Color.Black);
                        PointF drawPoint = new PointF(558.0F, 675.0F);
                        e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                    }
                    else
                    {
                        var s1 = Convert.ToDecimal(jFive);
                        jTotal = s1;
                        drawString = jTotal.ToString("C2");
                        Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                        SolidBrush drawBrush = new SolidBrush(Color.Black);
                        PointF drawPoint = new PointF(550.0F, 675.0F);
                        e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                    }
                }
                {
                    drawString = jEleven;                                                              // Back Order Status
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(635.0F, 675.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    var x = Convert.ToDecimal(jOne);
                    var s = Convert.ToDecimal(jFive);
                    jTotal = x * s;
                    drawString = jTotal.ToString("C2");
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(700.0F, 675.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                ///////////////////////////////////    LINE 11   ////////////////////////////////////////// 
                {
                    drawString = kOne;
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(20.0F, 695.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    drawString = kTwo;
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(70.0F, 695.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    var desc = kFour.Substring(0, 39);
                    drawString = desc;
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(210.0F, 695.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    var s = Convert.ToDecimal(kFive);
                    kTotal = s;
                    if (kTotal == 0)
                    {
                        drawString = kTotal.ToString("C2");
                        Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                        SolidBrush drawBrush = new SolidBrush(Color.Black);
                        PointF drawPoint = new PointF(558.0F, 695.0F);
                        e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                    }
                    else
                    {
                        var s1 = Convert.ToDecimal(kFive);
                        kTotal = s1;
                        drawString = kTotal.ToString("C2");
                        Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                        SolidBrush drawBrush = new SolidBrush(Color.Black);
                        PointF drawPoint = new PointF(550.0F, 695.0F);
                        e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                    }
                }
                {
                    drawString = kEleven;                                                              // Back Order Status
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(635.0F, 695.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    var x = Convert.ToDecimal(kOne);
                    var s = Convert.ToDecimal(kFive);
                    kTotal = x * s;
                    drawString = kTotal.ToString("C2");
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(700.0F, 695.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                ///////////////////////////////////    LINE 12   ////////////////////////////////////////// 
                {
                    drawString = lOne;
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(20.0F, 715.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    drawString = lTwo;
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(70.0F, 715.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    var desc = lFour.Substring(0, 39);
                    drawString = desc;
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(210.0F, 715.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    var s = Convert.ToDecimal(lFive);
                    lTotal = s;
                    if (oTotal == 0)
                    {
                        drawString = lTotal.ToString("C2");
                        Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                        SolidBrush drawBrush = new SolidBrush(Color.Black);
                        PointF drawPoint = new PointF(558.0F, 715.0F);
                        e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                    }
                    else
                    {
                        var s1 = Convert.ToDecimal(lFive);
                        lTotal = s1;
                        drawString = lTotal.ToString("C2");
                        Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                        SolidBrush drawBrush = new SolidBrush(Color.Black);
                        PointF drawPoint = new PointF(550.0F, 715.0F);
                        e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                    }
                }
                {
                    drawString = lEleven;                                                              // Back Order Status
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(635.0F, 715.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    var x = Convert.ToDecimal(lOne);
                    var s = Convert.ToDecimal(lFive);
                    lTotal = x * s;
                    drawString = lTotal.ToString("C2");
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(700.0F, 715.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                ///////////////////////////////////    LINE 13   ////////////////////////////////////////// 
                {
                    drawString = mOne;
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(20.0F, 735.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    drawString = mTwo;
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(70.0F, 735.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    var desc = mFour.Substring(0, 39);
                    drawString = desc;
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(210.0F, 735.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    var s = Convert.ToDecimal(mFive);
                    mTotal = s;
                    if (mTotal == 0)
                    {
                        drawString = mTotal.ToString("C2");
                        Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                        SolidBrush drawBrush = new SolidBrush(Color.Black);
                        PointF drawPoint = new PointF(558.0F, 735.0F);
                        e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                    }
                    else
                    {
                        var s1 = Convert.ToDecimal(mFive);
                        mTotal = s1;
                        drawString = mTotal.ToString("C2");
                        Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                        SolidBrush drawBrush = new SolidBrush(Color.Black);
                        PointF drawPoint = new PointF(550.0F, 735.0F);
                        e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                    }
                }
                {
                    drawString = mEleven;                                                              // Back Order Status
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(635.0F, 735.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    var x = Convert.ToDecimal(mOne);
                    var s = Convert.ToDecimal(mFive);
                    mTotal = x * s;
                    drawString = mTotal.ToString("C2");
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(700.0F, 735.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                ///////////////////////////////////    LINE 14   ////////////////////////////////////////// 
                {
                    drawString = nOne;
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(20.0F, 755.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    drawString = nTwo;
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(70.0F, 755.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    var desc = nFour.Substring(0, 39);
                    drawString = desc;
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(210.0F, 755.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    var s = Convert.ToDecimal(nFive);
                    nTotal = s;
                    if (nTotal == 0)
                    {
                        drawString = nTotal.ToString("C2");
                        Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                        SolidBrush drawBrush = new SolidBrush(Color.Black);
                        PointF drawPoint = new PointF(558.0F, 755.0F);
                        e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                    }
                    else
                    {
                        var s1 = Convert.ToDecimal(nFive);
                        nTotal = s1;
                        drawString = nTotal.ToString("C2");
                        Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                        SolidBrush drawBrush = new SolidBrush(Color.Black);
                        PointF drawPoint = new PointF(550.0F, 755.0F);
                        e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                    }
                }
                {
                    drawString = nEleven;                                                              // Back Order Status
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(635.0F, 755.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    var x = Convert.ToDecimal(nOne);
                    var s = Convert.ToDecimal(nFive);
                    nTotal = x * s;
                    drawString = nTotal.ToString("C2");
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(700.0F, 755.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                ///////////////////////////////////    LINE 15   ////////////////////////////////////////// 
                {
                    drawString = oOne;
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(20.0F, 775.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    drawString = oTwo;
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(70.0F, 775.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    var desc = oFour.Substring(0, 39);
                    drawString = desc;
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(210.0F, 775.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    var s = Convert.ToDecimal(oFive);
                    oTotal = s;
                    if (oTotal == 0)
                    {
                        drawString = oTotal.ToString("C2");
                        Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                        SolidBrush drawBrush = new SolidBrush(Color.Black);
                        PointF drawPoint = new PointF(558.0F, 775.0F);
                        e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                    }
                    else
                    {
                        var s1 = Convert.ToDecimal(oFive);
                        oTotal = s1;
                        drawString = oTotal.ToString("C2");
                        Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                        SolidBrush drawBrush = new SolidBrush(Color.Black);
                        PointF drawPoint = new PointF(550.0F, 775.0F);
                        e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                    }
                }
                {
                    drawString = oEleven;                                                              // Back Order Status
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(635.0F, 775.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    var x = Convert.ToDecimal(oOne);
                    var s = Convert.ToDecimal(oFive);
                    oTotal = x * s;
                    drawString = oTotal.ToString("C2");
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(700.0F, 775.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                ///////////////////////////////////    Line 16   ////////////////////////////////////////// 
                {
                    drawString = pOne;
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(20.0F, 795.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    drawString = pTwo;
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(70.0F, 795.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    var desc = pFour.Substring(0, 39);
                    drawString = desc;
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(210.0F, 795.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    var s = Convert.ToDecimal(pFive);
                    pTotal = s;
                    if (pTotal == 0)
                    {
                        drawString = pTotal.ToString("C2");
                        Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                        SolidBrush drawBrush = new SolidBrush(Color.Black);
                        PointF drawPoint = new PointF(558.0F, 795.0F);
                        e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                    }
                    else
                    {
                        var s1 = Convert.ToDecimal(pFive);
                        pTotal = s1;
                        drawString = pTotal.ToString("C2");
                        Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                        SolidBrush drawBrush = new SolidBrush(Color.Black);
                        PointF drawPoint = new PointF(550.0F, 795.0F);
                        e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                    }
                }
                {
                    drawString = pEleven;                                                              // Back Order Status
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(635.0F, 795.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    var x = Convert.ToDecimal(pOne);
                    var s = Convert.ToDecimal(pFive);
                    pTotal = x * s;
                    drawString = pTotal.ToString("C2");
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(700.0F, 795.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                ///////////////////////////////////    End Lines   ////////////////////////////////////////// 

                {
                    drawString = po_num;
                    Font drawFont = new Font("Arial", 11, FontStyle.Bold);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(10.0F, 830.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    drawString = REMARKS + ", Received Date: " + DATEIN;
                    Font drawFont = new Font("Arial", 11, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(90.0F, 830.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                if (PrintLine == 1)
                {
                    {
                        Image img = Box3;                                   // Draw Parts Manager Box (embedded)
                        Point loc = new Point(5, 860);
                        e.Graphics.DrawImage(img, loc);

                        drawString = "   PARTS MANAGER COPY";
                        Font drawFont = new Font("Arial", 14, FontStyle.Bold);
                        SolidBrush drawBrush = new SolidBrush(Color.Black);
                        PointF drawPoint = new PointF(10.0F, 870.0F);
                        e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                    }
                }
                if (PrintLine == 2)
                {
                    {
                        Image img = Box3;                                   // Draw Lynnette Box (embedded)
                        Point loc = new Point(5, 860);
                        e.Graphics.DrawImage(img, loc);

                        drawString = "LYNNETTE'S COPY";
                        Font drawFont = new Font("Arial", 14, FontStyle.Bold);
                        SolidBrush drawBrush = new SolidBrush(Color.Black);
                        PointF drawPoint = new PointF(10.0F, 870.0F);
                        e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                    }
                }

                {
                    drawString = "Non Taxable Subtotal";
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(500.0F, 896.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    decimal d = aTotal + bTotal + cTotal + dTotal + eTotal + fTotal + gTotal + hTotal + iTotal + jTotal + kTotal + lTotal + mTotal + nTotal + oTotal + pTotal; 
                    drawString = d.ToString("C2");
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(725.0F, 896.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    drawString = "Taxable Subtotal";
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(500.0F, 916.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    drawString = "0.00";
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(733.0F, 916.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    drawString = "Tax";
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(500.0F, 936.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    drawString = "0.00";
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(733.0F, 936.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    drawString = "Total";
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(500.0F, 956.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    decimal d1 = aTotal + bTotal + cTotal + dTotal + eTotal + fTotal + gTotal + hTotal + iTotal + jTotal + kTotal + lTotal + mTotal + nTotal + oTotal + pTotal;
                    drawString = d1.ToString("C2");
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(725.0F, 957.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }

                {
                    drawString = "Page 1";
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(10.0F, 985.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    drawString = "Vendor Original";
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(300.0F, 1015.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    drawString = "Authorized Signature";
                    Font drawFont = new Font("Arial", 10, FontStyle.Regular);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    PointF drawPoint = new PointF(530.0F, 1035.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }
                {
                    Image img = Line1;                                   // Draw Sign here line  (embedded)
                    Point loc = new Point(530, 1025);
                    e.Graphics.DrawImage(img, loc);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error line 2893 \n" + ex);
            }
        }

        private void comboBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                comboBox7.Select();
            }
        }

        private void comboBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox3.Select();
            }
        }

        private void comboBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                comboBox6.Select();
            }
        }

        private void comboBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                comboBox5.Select();
            }
        }

        private void comboBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                comboBox4.Select();
                if (comboBox3.Text == "CC")
                {
                    comboBox8.Text = "2";
                }
            }
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                comboBox2.Select();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            MainUtilitiesMenu f0 = new MainUtilitiesMenu();
            f0.Show();
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var qty = textBox4.Text;
                var price = textBox6.Text;
                var qty1 = Convert.ToDecimal(qty);
                var price2 = Convert.ToDecimal(price);
                var total = qty1 * price2;
                textBox9.Text = total.ToString();

            }
        }
    }
}
