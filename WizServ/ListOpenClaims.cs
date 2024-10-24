using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Media;

namespace WizServ
{
    public partial class ListOpenClaims : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        public string claim_no = Version.Claim;
        private readonly string file = @"I:\\Datafile\\Control\\Database.CSV";
        private string fname, lname, addr, city, state, zip, hphone, wphone, Lines;
        private bool war_prd;
        private DateTime datein;
        private int loopCount, loop, zBench, zService, zPartsOrd, zAssign, zCheck, zCompleted, zPartsBack, zSent, zWait;
        private int consign;
        public string calledfrom;
        public string lab;
        public int mtotal;
        private int linesPrinted;
        private string[] lines;

        public ListOpenClaims()
        {
            InitializeComponent();
            Icon = image100;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
            GetData();
            lab = "You can double-click on Claim # to open any claim in list.";
            label1.Font = new Font("Arial", 13, FontStyle.Regular);
            label1.Text = lab;
        }

        private void ListOpenClaims_FormClosed(object sender, FormClosedEventArgs e)
        {
            Hide();
            ClaimsMGTMenu f2 = new ClaimsMGTMenu();
            f2.Show();
        }

        private void ListOpenClaims_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Hide();
            OpenClaimsMenu f2 = new OpenClaimsMenu();
            f2.Show();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            TextWriter txt = new StreamWriter(@"C:\Datafile\OpenClaims.txt");
            txt.Write(richTextBox1.Text);
            txt.Close();
            MessageBox.Show("Saved in:\nDrive C  Datafile Folder\n Filename: OpenClaims.txt");
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Length > 0)
            {
                richTextBox1.SelectAll();
                var tScreen = richTextBox1.Text;
                DateTime date = DateTime.Now;
                var shortDate = date.ToString("MM-dd-yyyy");
                var tMessage = shortDate + "\t" + Version.PCNAME + " Claims by Technician " + "\n\n";
                var tHeader = "Claim Manufacturer              Model                     Warr Status  Disposition\n";
                richTextBox2.Text = "";
                richTextBox2.Text = tMessage + tHeader + tScreen;
                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.Print();
                }
            }
            else
            {
                if (richTextBox1.Text.Length <= 0)
                {
                    richTextBox1.Text = "\nClick buttons above to list / Print first.";
                    return;
                }
            }
        }
        private void richTextBox3_DoubleClick(object sender, EventArgs e)
        {
            var SelectedText = richTextBox3.SelectedText;
            Version.Claim = SelectedText.Trim();
            claim_no = SelectedText.Trim();
            Hide();
            ByClaimNum f2 = new ByClaimNum();
            f2.Show();
        }


        private void printDocument1_BeginPrint(object sender, PrintEventArgs e)
        {
            printDocument1.DefaultPageSettings.Landscape = true;
            char[] param = { '\n' };

            if (printDialog1.PrinterSettings.PrintRange == PrintRange.Selection)
            {
                lines = richTextBox2.SelectedText.Split(param);
            }
            else
            {
                lines = richTextBox2.Text.Split(param);
            }

            int i = 0;
            char[] trimParam = { '\r' };
            foreach (string s in lines)
            {
                lines[i++] = s.TrimEnd(trimParam);
            }
        }

        private void OnPrintPage(object sender, PrintPageEventArgs e)
        {
            printDocument1.DefaultPageSettings.Landscape = true;
            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;
            Brush brush = new SolidBrush(richTextBox1.ForeColor);

            while (linesPrinted < lines.Length)
            {
                e.Graphics.DrawString(lines[linesPrinted++],
                    richTextBox1.Font, brush, x, y);
                y += 15;
                if (y >= e.MarginBounds.Bottom)
                {
                    e.HasMorePages = true;
                    return;
                }
            }

            linesPrinted = 0;
            e.HasMorePages = false;
        }

        public void PlaySimpleSound()
        {
            SoundPlayer simpleSound = new SoundPlayer(@"c:\Windows\Media\chimes.wav");
            simpleSound.Play();
        }

        private void RichTextBox1_DoubleClick(object sender, EventArgs e)
        {
            var SelectedText = richTextBox1.SelectedText;
            Version.Claim = SelectedText.Trim();
            claim_no = SelectedText.Trim();
            Hide();
            ByClaimNum f2 = new ByClaimNum();
            f2.Show();
        }

        public void GetData()
        {
            zBench = 0;
            zService = 0;
            zPartsOrd = 0;
            zAssign = 0;
            zCheck = 0;
            zCompleted = 0;
            zPartsBack = 0;
            zSent = 0;
            zWait = 0;

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


                loopCount = 0;
                loop = 0;
                consign = 0;

                while (!reader.EndOfStream)
                {
                    var lineRead = reader.ReadLine();
                    var values = lineRead.Split(',');

                    listA.Add(values[0]);       //  war_prd
                    listB.Add(values[1]);       //  claim_no
                    listC.Add(values[2]);       //  datein
                    listD.Add(values[3]);       //  fname
                    listE.Add(values[4]);       //  lname
                    listF.Add(values[5]);       //  addr
                    listG.Add(values[6]);       //  city
                    listH.Add(values[7]);       //  state
                    listI.Add(values[8]);       //  zip
                    listJ.Add(values[9]);       //  hphone          Home Phone #
                    listK.Add(values[10]);      //  wphone          Work Phone #
                    listL.Add(values[11]);      //  prob_compl      Problem Complaint
                    listM.Add(values[12]);      //  brand           Manuf Brand
                    listN.Add(values[13]);      //  serv_no
                    listO.Add(values[14]);
                    listP.Add(values[15]);
                    listQ.Add(values[16]);
                    listR.Add(values[17]);
                    listS.Add(values[18]);
                    listT.Add(values[19]);
                    listU.Add(values[20]);
                    listV.Add(values[21]);
                    listW.Add(values[22]);
                    listX.Add(values[23]);
                    listY.Add(values[24]);
                    listZ.Add(values[25]);
                    listAA.Add(values[26]);
                    listAB.Add(values[27]);
                    listAC.Add(values[28]);     //  war_stat         Warranty Status
                    listAD.Add(values[29]);     //  purch_date       Purchase Date for Warranty Claim
                    listAE.Add(values[30]);     //  fthr_exp1        Further Explination C/C line 2
                    listAF.Add(values[31]);     //  frth_exp2        Further Explination C/C line 3
                    listAG.Add(values[32]);
                    listAH.Add(values[33]);
                    listAI.Add(values[34]);
                    listAJ.Add(values[35]);
                    listAK.Add(values[36]);
                    listAL.Add(values[37]);
                    listAM.Add(values[38]);
                    listAN.Add(values[39]);
                    listAO.Add(values[40]);
                    listAP.Add(values[41]);
                    listAQ.Add(values[42]);
                    listAR.Add(values[43]);
                    listAS.Add(values[44]);
                    listAT.Add(values[45]);
                    listAU.Add(values[46]);
                    listAV.Add(values[47]);
                    listAW.Add(values[48]);
                    listAX.Add(values[49]);
                    listAY.Add(values[50]);
                    listAZ.Add(values[51]);
                    listBA.Add(values[52]);
                    listBB.Add(values[53]);
                    listBC.Add(values[54]);
                    listBD.Add(values[55]);
                    listBE.Add(values[56]);
                    listBF.Add(values[57]);
                    listBG.Add(values[58]);
                    listBH.Add(values[59]);
                    listBI.Add(values[60]);
                    listBJ.Add(values[61]);
                    listBK.Add(values[62]);
                    listBL.Add(values[63]);
                    listBM.Add(values[64]);
                    listBN.Add(values[65]);

                    //if (!listBD[loopCount].StartsWith("SERVICE RENDERED"))
                    {
                        var name = listE[loopCount] + ", " + listD[loopCount];  // Last, First name
                        var model = listM[loopCount] + " " + listO[loopCount] + " " + listP[loopCount];
                        var NameLen = name.Length;
                        name = name.Trim();
                        switch (NameLen)
                        {
                            case 5:
                                name += "\t\t\t\t\t\t";
                                break;
                            case 6:
                                name += "\t\t\t\t\t\t";
                                break;
                            case 7:
                                name += "\t\t\t\t\t\t";
                                break;
                            case 8:
                                name += "\t\t\t\t\t\t\t";
                                break;
                            case 9:
                                name += "\t\t\t\t\t\t\t";
                                break;
                            case 10:
                                name += "\t\t\t\t\t\t";
                                break;
                            case 11:
                                name += "\t\t\t\t\t\t";
                                break;
                            case 12:
                                name += "\t\t\t\t\t\t";
                                break;
                            case 13:
                                name += "\t\t\t\t\t\t";
                                break;
                            case 14:
                                name += "\t\t\t\t\t\t";
                                break;
                            case 15:
                                name += "\t\t\t\t\t";
                                break;
                            case 16:
                                name += "\t\t\t\t\t";
                                break;
                            case 17:
                                name += "\t\t\t\t\t";
                                break;
                            case 18:
                                name += "\t\t\t\t\t";
                                break;
                            case 19:
                                name += "\t\t\t\t\t";
                                break;
                            case 20:
                                name += "\t\t\t\t";
                                break;
                            case 21:
                                name += "\t\t\t\t";
                                break;
                            case 22:
                                name += "\t\t\t\t";
                                break;
                            case 23:
                                name += "\t\t\t\t";
                                break;
                            case 24:
                                name += "\t\t\t";
                                break;
                            case 25:
                                name += "\t\t\t";
                                break;
                            case 26:
                                name += "\t\t\t";
                                break;
                            case 27:
                                name += "\t\t\t";
                                break;
                            case 28:
                                name += "\t\t\t";
                                break;
                            case 29:
                                name += "\t\t";
                                break;
                            case 30:
                                name += "\t\t";
                                break;
                            case 31:
                                name += "\t\t";
                                break;
                            case 32:
                                name += "\t\t";
                                break;
                            case 33:
                                name += "\t\t";
                                break;
                            case 34:
                                name += "\t";
                                break;
                            case 35:
                                name += "\t";
                                break;
                            case 36:
                                name += "\t";
                                break;
                            case 37:
                                name += "\t";
                                break;
                            case 38:
                                name += "\t";
                                break;
                            case 39:
                                name += "";
                                break;
                            case 40:
                                name += "";
                                break;
                            case 42:
                                name += "";
                                break;

                        }
                        if (!name.Contains("CONSIGN"))
                        {
                            richTextBox1.Text = richTextBox1.Text + listB[loopCount] + "\t" + name + "\t" + model + "\n";

                        }
                        if (listD[loopCount].Contains("CONSIGN"))
                        {
                            richTextBox3.Text = richTextBox3.Text + listB[loopCount] + "\t" + name + "\t" + model + "\n";
                            consign++;
                        }
                        if (listE[loopCount].Contains("CONSIGN"))
                        {
                            richTextBox3.Text = richTextBox3.Text + listB[loopCount] + "\t" + name + "\t" + model + "\n";
                            consign++;
                        }
                        loop++;
                        label5.Text = "Found: \n" + loop.ToString() + " Claims\n" + consign.ToString() + " Consignment\n\n" + "Claims-Consign\n= " + (loop-consign).ToString();
                        if (listBD[loopCount].StartsWith("BENCH"))
                        {
                            zBench++;
                        }
                        if (listBD[loopCount].Contains("SERVICE RENDERED  XX"))
                        {
                            zService++;
                        }
                        if (listBD[loopCount].StartsWith("PARTS ORDERED"))
                        {
                            zPartsOrd++;
                        }
                        if (listBD[loopCount].StartsWith("ASSIGN"))
                        {
                            zAssign++;
                        }
                        if (listBD[loopCount].StartsWith("CHECK"))
                        {
                            zCheck++;
                        }
                        if (listBD[loopCount].StartsWith("COMPLETED"))
                        {
                            zCompleted++;
                        }
                        if (listBD[loopCount].StartsWith("PARTS ARE"))
                        {
                            zPartsBack++;
                        }
                        if (listBD[loopCount].StartsWith("SENT TO"))
                        {
                            zSent++;
                        }
                        if (listBD[loopCount].StartsWith("WAITING"))
                        {
                            zWait++;
                        }
                    }
                    loopCount++;
                }
                reader.Close(); // Close the open file
                label6.Text = "On Bench: " + zBench.ToString();
                label8.Text = "Service Rendered : " + zService.ToString();
                label9.Text = "Parts Ordered: " + zPartsOrd.ToString();
                label7.Text = "Assigned: " + zAssign.ToString();
                label10.Text = "Checking Parts: " + zCheck.ToString();
                label11.Text = "Completed     : " + zCompleted.ToString();
                label12.Text = "Back ordered Parts:   " + zPartsBack.ToString();
                label13.Text = "Sent to Manufacturer: " + zSent.ToString();
                label14.Text = "Waiting Estimate: " + zWait.ToString();
                label15.Text = "Consignment: " + consign;
                richTextBox1.Text = richTextBox1.Text + "\n\n";
                richTextBox1.Text = richTextBox1.Text + "**********  SUMMARY  **********\n\n";
                richTextBox1.Text = richTextBox1.Text + "On Bench: " + zBench.ToString() + "\n";
                richTextBox1.Text = richTextBox1.Text + "Assigned: " + zAssign.ToString() + "\n";
                richTextBox1.Text = richTextBox1.Text + "Completed: " + zCompleted.ToString() + "\n";
                richTextBox1.Text = richTextBox1.Text + "Consignment: " + consign.ToString() + "\n";
                richTextBox1.Text = richTextBox1.Text + "Parts Ordered: " + zPartsOrd.ToString() + "\n";
                richTextBox1.Text = richTextBox1.Text + "Checking Parts: " + zCheck.ToString() + "\n";
                richTextBox1.Text = richTextBox1.Text + "Waiting Estimate: " + zWait.ToString() + "\n";
                richTextBox1.Text = richTextBox1.Text + "Service Rendered: " + zService.ToString() + "\n";
                richTextBox1.Text = richTextBox1.Text + "Back ordered Parts: " + zPartsBack.ToString() + "\n";
                richTextBox1.Text = richTextBox1.Text + "Sent to Manufacturer: " + zSent.ToString() + "\n";
                richTextBox1.Text = richTextBox1.Text + "\n\n";
                richTextBox1.Text = richTextBox1.Text + "**********  SUMMARY  **********\n";
                var mGrandTotal = (zBench + zPartsOrd + zAssign + zCheck + zPartsBack + zSent + zWait);
                richTextBox1.Text = richTextBox1.Text + "Total Claims: " + loopCount + "\n";
                richTextBox1.Text = richTextBox1.Text + "Open Claims:  " + mGrandTotal.ToString() + "\n";  
                var mClosedClaims = zCompleted + zService;
                richTextBox1.Text = richTextBox1.Text + "Closed/Rendered Claims: " + mClosedClaims.ToString() + "\n";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 470: Sorry an error has occured: " + ex.Message);
            }
        }
    }
}
