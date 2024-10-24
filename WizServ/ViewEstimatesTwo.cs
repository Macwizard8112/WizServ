using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WizServ
{
    public partial class ViewEstimatesTwo : Form
    {
        private readonly string Estimates = @"I:\\Datafile\\Mel\\Estimates.CSV";         // This is Read only CSV
        private readonly string Est_Appr = @"I:\\Datafile\\Control\\Est_Approved.CSV";   // This is Read only CSV
        private readonly string Database = @"I:\\Datafile\\Control\\Database.CSV";       // This is Read only CSV
        public int loopCount, loop;
        public string IndexNo, IsSelected;

        public ViewEstimatesTwo()
        {
            InitializeComponent();
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
            ShowEstimates();
            textBox1.SelectAll();
            textBox1.Select();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            MainUtilitiesMenu f2 = new MainUtilitiesMenu();
            f2.Show();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                IndexNo = textBox1.Text;
                var t = int.Parse(IndexNo);
                var h = loop;
                if (t > h)
                {
                    IndexNo = "";
                    textBox1.Text = "";
                    textBox1.Select();
                    MessageBox.Show("Index number can't be larger than displayed index numbers.");
                }
                else
                {
                    label14.Text = "Index # = " + IndexNo;
                    Version.IndexNum = IndexNo.ToString();
                    Hide();
                    EstimateEditScreen f2 = new EstimateEditScreen();
                    f2.Show();
                }
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            IsSelected = listBox1.SelectedItem.ToString();
            IsSelected = IsSelected.Substring(0, 5).Trim();
            textBox1.Text = IsSelected;
            IndexNo = IsSelected;
            var t = int.Parse(IndexNo);
            var h = loop;
            if (t > h)
            {
                IndexNo = "";
                textBox1.Text = "";
                textBox1.Select();
                MessageBox.Show("Index number can't be larger than displayed index numbers.");
            }
            else
            {
                label14.Text = "Index # = " + IndexNo;
                Version.IndexNum = IndexNo.ToString();
                Hide();
                EstimateEditScreen f2 = new EstimateEditScreen();
                f2.Show();
            }
        }

        public void ShowEstimates()
        {
            try
            {
                StreamReader reader = new StreamReader(Estimates, Encoding.GetEncoding("Windows-1252"));
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

                loopCount = 0;

                while (!reader.EndOfStream)
                {
                    var lineRead = reader.ReadLine();
                    var values = lineRead.Split(',');

                    listA.Add(values[0]);       //  Claim
                    listB.Add(values[1]);       //  First Name
                    listC.Add(values[2]);       //  Last Name
                    listD.Add(values[3]);       //  Parts Cost $
                    listE.Add(values[4]);       //  Labor Cost $
                    listF.Add(values[5]);       //  Shop Cost $
                    listG.Add(values[6]);       //  Shipping $
                    listH.Add(values[7]);       //  Tax $
                    listI.Add(values[8]);       //  Total $
                    listJ.Add(values[9]);       //  Sent Date
                    listK.Add(values[10]);      //  Index #
                    listL.Add(values[11]);      //  Approved Date
                    listM.Add(values[12]);      //  Paid Down
                    listN.Add(values[13]);      //  Rush

                    var TheCount = loopCount.ToString();
                    var fnameL = listB[loop];
                    var fname = listB[loop];
                    var lnameL = listC[loop];
                    var lname = listC[loop];
                    switch (fnameL.Length)
                    {
                        case 1:
                            fname += "                     ";
                            break;
                        case 2:
                            fname += "                    ";
                            break;
                        case 3:
                            fname += "                   ";
                            break;
                        case 4:
                            fname += "                  ";
                            break;
                        case 5:
                            fname += "                 ";
                            break;
                        case 6:
                            fname += "                ";
                            break;
                        case 7:
                            fname += "               ";
                            break;
                        case 8:
                            fname += "              ";
                            break;
                        case 9:
                            fname += "             ";
                            break;
                        case 10:
                            fname += "            ";
                            break;
                        case 11:
                            fname += "           ";
                            break;
                        case 12:
                            fname += "          ";
                            break;
                        case 13:
                            fname += "         ";
                            break;
                        case 14:
                            fname += "        ";
                            break;
                        case 15:
                            fname += "       ";
                            break;
                        case 16:
                            fname += "      ";
                            break;
                        case 17:
                            fname += "     ";
                            break;
                        case 18:
                            fname += "    ";
                            break;
                        case 19:
                            fname += "   ";
                            break;
                        case 20:
                            fname += "  ";
                            break;
                        case 21:
                            fname += " ";
                            break;
                    }
                    switch (lnameL.Length)
                    {
                        case 1:
                            lname += "                     ";
                            break;
                        case 2:
                            lname += "                    ";
                            break;
                        case 3:
                            lname += "                   ";
                            break;
                        case 4:
                            lname += "                  ";
                            break;
                        case 5:
                            lname += "                 ";
                            break;
                        case 6:
                            lname += "                ";
                            break;
                        case 7:
                            lname += "               ";
                            break;
                        case 8:
                            lname += "              ";
                            break;
                        case 9:
                            lname += "             ";
                            break;
                        case 10:
                            lname += "            ";
                            break;
                        case 11:
                            lname += "           ";
                            break;
                        case 12:
                            lname += "          ";
                            break;
                        case 13:
                            lname += "         ";
                            break;
                        case 14:
                            lname += "        ";
                            break;
                        case 15:
                            lname += "       ";
                            break;
                        case 16:
                            lname += "      ";
                            break;
                        case 17:
                            lname += "     ";
                            break;
                        case 18:
                            lname += "    ";
                            break;
                        case 19:
                            lname += "   ";
                            break;
                        case 20:
                            lname += "  ";
                            break;
                        case 21:
                            lname += " ";
                            break;
                    }
                    var Parts = listD[loop].ToString();
                    var PartsL = Parts.Length;
                    switch (PartsL)
                    {
                        case 4:
                            Parts = "   " + Parts;
                            break;
                        case 5:
                            Parts = "  " + Parts;
                            break;
                        case 6:
                            Parts = " " + Parts;
                            break;
                        case 7:
                            Parts = "" + Parts;
                            break;
                    }
                    var Labor = listE[loop].ToString();
                    var lb = Convert.ToDecimal(Labor);
                    Labor = lb.ToString("0.00");
                    var LaborL = Labor.Length;
                    switch (LaborL)
                    {
                        case 4:
                            Labor = "   " + Labor;
                            break;
                        case 5:
                            Labor = "  " + Labor;
                            break;
                        case 6:
                            Labor = " " + Labor;
                            break;
                        case 7:
                            Labor = "" + Labor;
                            break;
                    }
                    var Shop = listF[loop].ToString();
                    var ShopL = Shop.Length;
                    switch (ShopL)
                    {
                        case 4:
                            Shop = "   " + Shop;
                            break;
                        case 5:
                            Shop = "  " + Shop;
                            break;
                        case 6:
                            Shop = " " + Shop;
                            break;
                        case 7:
                            Shop = "" + Shop;
                            break;
                    }
                    var Shipping = listG[loop].ToString();
                    var ShippingL = Shipping.Length;
                    switch (ShippingL)
                    {
                        case 4:
                            Shipping = "   " + Shipping;
                            break;
                        case 5:
                            Shipping = "  " + Shipping;
                            break;
                        case 6:
                            Shipping = " " + Shipping;
                            break;
                        case 7:
                            Shipping = "" + Shipping;
                            break;
                    }
                    var Tax = listH[loop].ToString();
                    var TaxL = Tax.Length;
                    switch (TaxL)
                    {
                        case 4:
                            Tax = "   " + Tax;
                            break;
                        case 5:
                            Tax = "  " + Tax;
                            break;
                        case 6:
                            Tax = " " + Tax;
                            break;
                        case 7:
                            Tax = "" + Tax;
                            break;
                    }
                    var Total = listI[loop].ToString();
                    var T1 = Convert.ToDecimal(Total);
                    Total = T1.ToString("0.00");
                    var TotalL = Total.Length;
                    switch (TotalL)
                    {
                        case 4:
                            Total = "   " + Total;
                            break;
                        case 5:
                            Total = "  " + Total;
                            break;
                        case 6:
                            Total = " " + Total;
                            break;
                        case 7:
                            Total = "" + Total;
                            break;
                    }
                    var SentDate = listJ[loop].ToString();
                    var SD1 = SentDate.Substring(0, 2);
                    if (SD1.Contains("/"))
                    {
                        SentDate = "0" + SentDate;
                    }
                    var SD2 = SentDate.Substring(2, 2);
                    if (SD2.Contains("/"))
                    {
                        var dmt1 = SentDate.Substring(0, 2);
                        var dmt2 = "0" + SentDate.Substring(3, 1);
                        var dmt3 = SentDate.Length;
                        var dmt4 = SentDate.Substring(dmt3 - 4, 4);
                        var dmt5 = dmt1 + "/" + dmt2 + "/" + dmt4;
                        SentDate = dmt5;
                    }
                    var SendDateL = SentDate.Length;
                    switch (SendDateL)
                    {
                        case 4:
                            SentDate = "       " + SentDate;
                            break;
                        case 5:
                            SentDate = "      " + SentDate;
                            break;
                        case 6:
                            SentDate = "     " + SentDate;
                            break;
                        case 7:
                            SentDate = "    " + SentDate;
                            break;
                        case 8:
                            SentDate = "   " + SentDate;
                            break;
                        case 9:
                            SentDate = "  " + SentDate;
                            break;
                    }
                    var Rush = listN[loop].ToString();
                    if (Rush == "0")
                    {
                        Rush = "No";
                    }
                    else
                    {
                        Rush = "Yes";
                    }
                    var RushL = Rush.Length;
                    switch (RushL)
                    {
                        case 4:
                            Rush = "   " + Rush;
                            break;
                        case 5:
                            Rush = "  " + Rush;
                            break;
                        case 6:
                            Rush = " " + Rush;
                            break;
                        case 7:
                            Rush = "" + Rush;
                            break;
                    }
                    var Index = listK[loop].ToString();
                    var index2 = Convert.ToInt32(Index);
                    Index = index2.ToString();
                    var IndexL = Index.Length;
                    switch (IndexL)
                    {
                        case 1:
                            Index = "   " + Index;
                            break;
                        case 2:
                            Index = "  " + Index;
                            break;
                        case 3:
                            Index = " " + Index;
                            break;
                    }
                    var p1 = Parts;
                    var p2 = Convert.ToDecimal(p1);
                    var p3 = p2.ToString("0.00");
                    var tx = Convert.ToDecimal(Tax);
                    Tax = tx.ToString("0.00");
                    switch(Tax.Length)
                    {
                        case 4:
                            Tax = "   " + Tax;
                            break;
                        case 5:
                            Tax = "  " + Tax;
                            break;
                        case 6:
                            Tax = " " + Tax;
                            break;
                    }
                    if (listL[loop].StartsWith("00/"))
                    {
                        listL[loop] = "-";
                    }
                    if (Labor.Length <= 4)
                    {
                        listBox1.Items.Add(Index + "\t" + listA[loop] + "  " + fname + "\t" + lname + "\t" + p3 + "\t\t\t" + Labor + "\t" + Shop + "\t" + Shipping + "\t   " + Tax + "\t" + Total + "\t\t" + SentDate + "\t" + Rush + "\t" + listL[loop]);
                    }
                    else
                    {
                        listBox1.Items.Add(Index + "\t" + listA[loop] + "  " + fname + "\t" + lname + "\t" + p3 + "\t\t" + Labor + "\t\t" + Shop + "\t" + Shipping + "\t   " + Tax + "\t" + Total + "\t\t" + SentDate + "\t" + Rush + "\t" + listL[loop]);
                    }
                    loop++;

                    loopCount++;
                }
                reader.Close(); // Close the open file
                label18.Text = "Found: " + loopCount.ToString();
            }
            catch (Exception ex)
            {
                if (ex.ToString().Contains("Input string was not in a correct format."))
                {
                   
                }
                else
                {
                    MessageBox.Show("Error 420: Sorry an error has occured: " + ex.Message);
                }
            }
        }
    }
}
