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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WizServ
{
    public partial class PartsUsed : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        private static readonly string PartsUsed1 = @"I:\\Datafile\\Control\\Partsused.CSV";  // This is Read only CSV
        private static readonly string Ordered = @"I:\\Datafile\\Control\\Ordered.CSV";     // This is Read only CSV
        private int loopCount, foundcount, foundcount2;
        public string claimno = Version.Claim;
        public string from = Version.From;
        public string mTab = "\t";
        public string mTab2 = "\t\t";
        public decimal decimalRounded, decRounded;
        public string t = "0";
        public decimal t_price, mBOCost, partstotal, totRounded;
        private string dd, mTab3;
        public string FROM = Version.From;
        public string SELECTEDTEXT = Version.SELECTEDTEXT;  // Pass along from Password PRG to enable editing

        public PartsUsed()
        {
            InitializeComponent();
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
            Icon = image100;
            label20.Text = SELECTEDTEXT;
            if (Version.From == "EditServices")
            {
                button3.Visible = false;
            }
            claimno = Version.Claim;
            from = Version.From;
            DoLookup();
            PartsOrdered();

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (SELECTEDTEXT == "MAINUTILITIESMENU")
            {
                Hide();
                MainUtilitiesMenu f2 = new MainUtilitiesMenu();
                f2.Show();
            }
            if (from == "Retrieve1" && SELECTEDTEXT != "MAINUTILITIESMENU")
            {
                Hide();
                ByClaimNum f2 = new ByClaimNum();
                f2.Show();
            }
            if (from == "EditServices")
            {
                Hide();
                EditServices f2 = new EditServices();
                f2.Show();
            }
            if (from == "Retrieve2")
            {
                Hide();
                ByClaimNumPg2 f2 = new ByClaimNumPg2();
                f2.Show();
            }
        }

        static void GetValidPW()
        {
            Version.From = "PARTSUSED";
            Password f2 = new Password();
            f2.Show();
        }

        private void richTextBox2_DoubleClick(object sender, EventArgs e)
        {
            if (SELECTEDTEXT == "MAINUTILITIESMENU")
            {
                MovePartsIntoClaim();
            }
            //Task thread1 = Task.Factory.StartNew(() => GetValidPW());
            //Task.WaitAll(thread1);

        }

        private void MovePartsIntoClaim()
        {
            string message = "Selected: " + richTextBox2.SelectedText;
            string title = "MAINUTILITIESMENU";
            MessageBox.Show(message, title);
        }


        private void Button2_Click(object sender, EventArgs e)
        {
            if (Version.From == "EditServices")
            {
                Hide();
                EditServices f2 = new EditServices();
                f2.Show();
            }
            else
            {
                Hide();
                ByClaimNum f2 = new ByClaimNum();
                f2.Show();
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Hide();
            ByClaimNumPg2 f2 = new ByClaimNumPg2();
            f2.Show();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Hide();
            RetrieveMenu f2 = new RetrieveMenu();
            f2.Show();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }

        private void RichTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
                Hide();
                ByClaimNum f2 = new ByClaimNum();
                f2.Show();   
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
                    
                    if (listC[loopCount] == claimno)
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
                        if (listJ[loopCount] != "Y")
                        {
                            richTextBox2.Text = richTextBox2.Text + listA[loopCount] + "\t" + xPN + "\t" + xPrice2 + "\t" + dd + "\t\t" + mTab3 + listK[loopCount] + "\t\t" + listL[loopCount] + "\n";
                        }
                        foundcount2++;
                        label16.Text = "Found: " + foundcount2.ToString();
                        mBOCost = mBOCost + xPrice;
                        label17.Text = mBOCost.ToString("C2");
                    }
                    loopCount++;
                }
                reader.Close(); // Close the open file
                partstotal = mBOCost + totRounded;
                label18.Text = "Claim Parts Total: " + partstotal.ToString("C2");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 275: Sorry an error has occured: " + ex.Message);
            }
        }

        private void DoLookup()
        {
            try
            {
                StreamReader reader = new StreamReader(PartsUsed1);
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

                loopCount = 0;

                while (!reader.EndOfStream)
                {
                    var lineRead = reader.ReadLine();
                    var values = lineRead.Split(',');

                    listA.Add(values[0]);   // Qty
                    listB.Add(values[1]);   // Part_no
                    listC.Add(values[2]);   // Ref_no
                    listD.Add(values[3]);   // Description
                    listE.Add(values[4]);   // Price
                    listF.Add(values[5]);   // Claim_no
                    listG.Add(values[6]);   // Cost
                    listH.Add(values[7]);   // Part_Date
                    listI.Add(values[8]);   // Ppurch Date
                    listJ.Add(values[9]);   // Part in Claim
                    listK.Add(values[10]);  // Index

                    var Apn = listB[loopCount];
                    var Apn2 = Apn.Length;

                    switch (Apn2)
                    {
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            Apn += "            ";
                            break;
                        case 4:
                            Apn += "           ";
                            break;
                        case 5:
                            Apn += "          ";
                            break;
                        case 6:
                            Apn += "         \t";
                            break;
                        case 7:
                            Apn += "       \t";
                            break;
                        case 8:
                            Apn += "       \t";
                            break;
                        case 9:
                            Apn += "      ";
                            break;
                        case 10:
                            Apn += "     ";
                            break;
                        case 11:
                            Apn += "    ";
                            break;
                        case 12:
                            Apn += "   ";
                            break;
                        case 13:
                            Apn += "  ";
                            break;
                        case 14:
                            Apn += " ";
                            break;
                        case 15:
                            break;
                        case 16:
                            break;
                        case 17:
                            break;
                        case 18:
                            break;
                        case 19:
                            break;
                        case 20:
                            break;
                    }
                    if (Apn == "2")
                    {
                        Apn = "0002\t\t";
                    }
                    if (Apn == "3")
                    {
                        Apn = "0003\t\t";
                    }
                    if (Apn == "4")
                    {
                        Apn = "0004\t\t";
                    }
                    if (Apn == "5")
                    {
                        Apn = "0005\t\t";
                    }
                    if (Apn == "6")
                    {
                        Apn = "0006\t\t";
                    }
                    if (Apn == "7")
                    {
                        Apn = "0007\t\t";
                    }
                    if (Apn == "8")
                    {
                        Apn = "0008\t\t";
                    }
                    if (Apn == "9")
                    {
                        Apn = "0009\t\t";
                    }

                    if (values[5] == claimno && listJ[loopCount] == "Y")    // Parts in Claim
                    {
                        if (listB[loopCount].Contains("FREIGHT"))
                        {
                            return;
                        }
                    }
                    if (values[5] == claimno && listJ[loopCount] == "N")    // Parts in Claim
                    {
                        if (listB[loopCount].Contains("FREIGHT"))
                        {
                            return;
                        }
                    }

                    if (values[5] == claimno && listJ[loopCount] == "Y")    // Parts in Claim
                    {
                        var c_price = decimal.Parse((listE[loopCount]));
                        decimal decimalRounded = Decimal.Parse(c_price.ToString("0.00"));
                        t_price = t_price + c_price;
                        totRounded = Decimal.Parse(t_price.ToString("0.00"));
                        var w_price = decimal.Parse((listG[loopCount]));
                        decimal decRounded = Decimal.Parse(w_price.ToString("0.00"));
                        if (decimalRounded.ToString().Length == 4)
                        {
                            t = "  ";
                        }
                        else
                        {
                            t = "";
                        }
                        foundcount++;
                        richTextBox1.Text = richTextBox1.Text + listA[loopCount] + "\t" + Apn + "\t" + t + decimalRounded + "\t\t"  + listD[loopCount] + "\n";
                        label6.Text = "Found: " + foundcount.ToString() + " items,  Total $ " + totRounded;
                    }
                    if (values[5] == claimno && listJ[loopCount] == "N")    // Parts on Order for Claim
                    {
                        var c_price = decimal.Parse((listE[loopCount]));
                        decimal decimalRounded = Decimal.Parse(c_price.ToString("0.00"));
                        t_price = t_price + c_price;
                        decimal totRounded = Decimal.Parse(t_price.ToString("0.00"));
                        var w_price = decimal.Parse((listG[loopCount]));
                        decimal decRounded = Decimal.Parse(w_price.ToString("0.00"));
                        if (decimalRounded.ToString().Length == 4)
                        {
                            t = "  ";
                        }
                        else
                        {
                            t = "";
                        }
                        foundcount++;
                        richTextBox2.Text = richTextBox2.Text + listA[loopCount] + "\t" + Apn + "\t" + t + decimalRounded + "\t\t" + listD[loopCount] + "\t\t" + listK[loopCount] + "\n";
                        label6.Text = "Found: " + foundcount.ToString() + " items,  Total $ " + totRounded;
                    }
                    loopCount++;
                }
                reader.Close(); // Close the open file
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 469: Sorry an error has occured: " + ex.Message);
            }
        }
    }
}
