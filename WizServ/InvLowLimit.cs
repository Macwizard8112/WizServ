using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Media;

namespace WizServ
{
    public partial class InvLowLimit : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        private readonly string PrimaryParts = @"I:\\datafile\\Control\\part_pri.csv";
        private int loopCount, loop, items, onorder, printcount;
        private int ONHAND, LOWLIMIT, pagecount;
        private decimal cost;

        public InvLowLimit()
        {
            InitializeComponent();
            Icon = image100;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
            label12.Visible = false;
            GetData();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            printcount++;
            if (printcount == 1)
            {
                richTextBox1.Text = "Part #\t\t\t\tLast Used\t\tQty Ord.\tDate Ord.\t\tCost EA.\n\n" + richTextBox1.Text;
            }
            string saveFile1 = @"C:\\_WizServ_Reports\\LowLimit.rtf";
            string path = @"C:\\_WizServ_Reports";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            richTextBox1.SelectAll();
            richTextBox1.SelectionFont = new Font("Lucida Sans Unicode", 10, FontStyle.Regular);
            richTextBox1.SaveFile(saveFile1, RichTextBoxStreamType.RichText);
            label12.Visible = true;
            label12.Text = "File Saved";
            richTextBox1.DeselectAll();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            InventoryMenu f2 = new InventoryMenu();
            f2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            printcount++;
            if (printcount == 1)
            {
                richTextBox1.Text = "Part #\t\t\t\tLast Used\t\tQty Ord.\tDate Ord.\t\tCost EA.\n\n" + richTextBox1.Text;
            }
            TextWriter txt = new StreamWriter("I:\\Datafile\\Doc\\LowLimit.txt");
            txt.Write(richTextBox1.Text);
            txt.Close();
            var fileToOpen = "I:\\Datafile\\Doc\\LowLimit.txt";
            if (!File.Exists(fileToOpen))
            {
                button1.PerformClick();
            }
            var process = new Process
            {
                StartInfo = new ProcessStartInfo()
                {
                    UseShellExecute = true,
                    FileName = fileToOpen
                }
            };
            process.Start();
            process.WaitForExit();
        }

        public void GetData()
        {
            pagecount = 0;
            try
            {
                StreamReader reader = new StreamReader(PrimaryParts, Encoding.GetEncoding("Windows-1252"));
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
                    listAC.Add(values[28]);     //  
                    listAD.Add(values[29]);     //  
                    listAE.Add(values[30]);     //  
                    listAF.Add(values[31]);     //  
                    listAG.Add(values[32]);     //  
                    listAH.Add(values[33]);     //  
                    listAI.Add(values[34]);     //  
                    listAJ.Add(values[35]);     //  
                    listAK.Add(values[36]);     //  
                    listAL.Add(values[37]);     //  
                    listAM.Add(values[38]);
                    listAN.Add(values[39]);

                    var Part_Num = listA[loopCount];
                    var desc = listB[loopCount];
                    var TheOnHand = listC[loopCount];
                    var Thelowlimit = listD[loopCount];
                    ONHAND = int.Parse(TheOnHand);
                    LOWLIMIT = int.Parse(Thelowlimit);
                    var TheCost = listE[loopCount];
                    var TheRetail = listF[loopCount];
                    var Last_Ordered = listJ[loopCount];

                    if (Part_Num == "2")
                    {
                        Part_Num = "0002";
                    }
                    if (Part_Num == "3")
                    {
                        Part_Num = "0003";
                    }
                    if (Part_Num == "4")
                    {
                        Part_Num = "0004";
                    }
                    if (Part_Num == "5")
                    {
                        Part_Num = "0005";
                    }
                    if (Part_Num == "6")
                    {
                        Part_Num = "0006";
                    }
                    if (Part_Num == "8")
                    {
                        Part_Num = "0008";
                    }
                    switch (Part_Num.Length)
                    {
                        case 2:
                            Part_Num += "                  ";
                            break;
                        case 3:
                            Part_Num += "                   ";
                            break;
                        case 4:
                            Part_Num += "\t\t";
                            break;
                        case 5:
                            Part_Num += "               ";
                            break;
                        case 6:
                            Part_Num += "              ";
                            break;
                        case 7:
                            Part_Num += "             ";
                            break;
                        case 8:
                            Part_Num += "            ";
                            break;
                        case 9:
                            Part_Num += "           ";
                            break;
                        case 10:
                            Part_Num += "          ";
                            break;
                        case 11:
                            Part_Num += "         ";
                            break;
                        case 12:
                            Part_Num += "        ";
                            break;
                        case 13:
                            Part_Num += "       ";
                            break;
                        case 14:
                            Part_Num += "   ";
                            break;
                        case 15:
                            Part_Num += "  ";
                            break;
                        case 16:
                            Part_Num += "  ";
                            break;
                        case 17:
                            Part_Num += "   ";
                            break;
                        case 18:
                            Part_Num += "  ";
                            break;
                        case 19:
                            Part_Num += " ";
                            break;
                    }
                    if (desc.Length <= 14)
                    {
                        desc += "\t";
                    }

                    if (ONHAND <= LOWLIMIT)
                    {
                        // Convert stored date to formatted string - Last Used Date
                        var JJ = listJ[loopCount];
                        var KK = Convert.ToDateTime(JJ);
                        string strFormat = "MM/dd/yyyy";
                        var last_used_date = KK.ToString(strFormat);
                        // Convert stored Ordered Date to formatted string - Ordered Date
                        var TT = listAN[loopCount];
                        var LL = Convert.ToDateTime(TT);
                        var Ordered_date = LL.ToString(strFormat);

                        richTextBox1.Text = richTextBox1.Text + Part_Num + "\t\t" + last_used_date + "\t\t" + listK[loopCount] + "\t\t" + Ordered_date + "\t\t" + Convert.ToDecimal(listE[loopCount]).ToString("C2") + "\n";
                        loop++;
                        items += Convert.ToInt32(listK[loopCount]);
                        onorder += Convert.ToInt32(listK[loopCount]);
                        cost += Convert.ToDecimal(listE[loopCount]) * Convert.ToDecimal(listK[loopCount]);
                    }
                    loopCount++;
                }
                reader.Close(); // Close the open file
                label1.Text = "Count: " + loop.ToString() + ", Items on Order: " + items.ToString();
                label2.Text = "Total Cost: " + cost.ToString("C2");
                pagecount = (loopCount / 52);
                label11.Text = "Printed Page: " + pagecount.ToString() + " pages";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 299: Sorry an error has occured: " + ex.Message);
            }
        }
    }
}
