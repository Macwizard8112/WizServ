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
using System.Threading;
using System.Media;
using System.Drawing.Printing;

namespace WizServ
{
    public partial class ListOpenClaims2 : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        public string claim_no = Version.Claim;
        private readonly string file = @"I:\Datafile\Control\Database.CSV";
        private string fname, lname, addr, city, state, zip, hphone, wphone, Lines;
        private bool war_prd;
        private DateTime datein;
        private int loopCount, loop, zBench, zService, zPartsOrd, zAssign, zCheck, zCompleted, zPartsBack, zSent, zWait;
        public string calledfrom, tScreen;
        public string lab, TodaysDate, dt1, dt2, dt3, dt4, dt5, dt6, dt8, dt9, DateWOZeros;
        public int mtotal, dt7;
        public int indexToText = 0;
        private int linesPrinted;
        private string[] lines;

        public ListOpenClaims2()
        {
            InitializeComponent();
            Icon = image100;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
            GetTodaysDate();
            GetData();
        }

        public void GetTodaysDate()
        {
            try
            {
                // Generate todays date
                dt2 = DateTime.Now.Month.ToString("00");    // Month as 2 digits
                if (int.Parse(dt2) <= 9)
                {
                    dt1 = DateTime.Now.Month.ToString("0");
                }
                else
                {
                    dt1 = DateTime.Now.Month.ToString("00");
                }
                dt4 = DateTime.Now.Day.ToString("00");      // Day as 2 digits
                if (int.Parse(dt4) <= 9)
                {
                    dt5 = DateTime.Now.Day.ToString("0");
                }
                else
                {
                    dt5 = DateTime.Now.Day.ToString("00");
                }
                dt8 = DateTime.Now.Year.ToString("0000");   // Year as 4 digits
                dt3 = dt1 + "/" + dt5 + "/" + dt8;
                DateWOZeros = dt3;
                dt9 = dt2 + "/" + dt4 + "/" + dt8;          // Add back in seperators "/"
                TodaysDate = dt9;                           // Store Todays Date in Public variable
                // End generate todays date
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 77, Error\n" + ex);
            }
        }

        private void richTextBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {   //click event
                ContextMenu contextMenu = new ContextMenu();
                MenuItem menuItem = new MenuItem("Cut       Ctrl+X");
                menuItem.Click += new EventHandler(CutAction);
                contextMenu.MenuItems.Add(menuItem);
                menuItem = new MenuItem("Copy    Ctrl+C");
                menuItem.Click += new EventHandler(CopyAction);
                contextMenu.MenuItems.Add(menuItem);
                menuItem = new MenuItem("Paste    Ctrl+V");
                menuItem.Click += new EventHandler(PasteAction);
                contextMenu.MenuItems.Add(menuItem);

                richTextBox1.ContextMenu = contextMenu;
            }
        }
        void CutAction(object sender, EventArgs e)
        {
            try
            {
                richTextBox1.Cut();
            }
            catch (Exception ex)
            {
                //
            }
        }

        void CopyAction(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(richTextBox1.SelectedText);
            }
            catch (Exception ex)
            {
                if (ex.ToString().Contains("Value cannot be null."))
                {
                    // Ignore nothing selected
                }
                else
                {
                    MessageBox.Show("Error 125, Sorry an exception has occured.\n" + ex);
                }
            }
        }
        void PasteAction(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                richTextBox1.Text += Clipboard.GetText(TextDataFormat.Text).ToString();
            }
        }

        private void ListOpenClaims2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Hide();
            ClaimsMGTMenu f2 = new ClaimsMGTMenu();
            f2.Show();
        }

        private void ListOpenClaims2_FormClosing(object sender, FormClosingEventArgs e)
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

        private void Button5_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Length > 0)
            {
                richTextBox1.SelectAll();
                tScreen = richTextBox1.Text;
                DateTime date = DateTime.Now;
                var tMessage = TodaysDate + "\t" + "\n\n";
                var tMsg = TodaysDate + " Todays\n" + "Claims in database" + ":\n\n";
                var tHeader = "   \n";
                richTextBox1.Text = "";
                richTextBox1.Text = tScreen;
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
            richTextBox1.Text = "";
            richTextBox1.Text = tScreen;
            indexToText = 0;
            indexToText = richTextBox1.Find("Open and Closed Claims Summary Report", 0, RichTextBoxFinds.MatchCase);
            richTextBox1.SelectedText.Equals("Open and Closed Claims Summary Report");
            richTextBox1.SelectionFont = new Font("Courier New", 16, FontStyle.Bold);
            richTextBox1.SelectionColor = Color.Blue;
            richTextBox1.DeselectAll();
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            printDocument1.DefaultPageSettings.Landscape = false;
            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;
            Brush brush = new SolidBrush(richTextBox1.ForeColor);
            char[] param = { '\n' };
            lines = richTextBox1.Text.Split(param);
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

        public void GetData()
        {
            try
            {
                if (Version.DatabaseIsLocked == true)
                {
                    MessageBox.Show("Database in use. Retryning...");
                    Thread.Sleep(3000);
                }
                Version.DatabaseIsLocked = true;
            }
            catch (Exception)
            {
                GetData();
            }
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
              
                richTextBox1.Text = richTextBox1.Text + "\t\tOpen and Closed Claims Summary Report\t\t" + DateTime.Now.ToShortDateString() + "\n\n";

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

                        loop++;
                        label5.Text = "Found: " + loop.ToString();
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
                Version.DatabaseIsLocked = false;

                label6.Text = "On Bench: " + zBench.ToString();
                label8.Text = "Service Rendered : " + zService.ToString();
                label9.Text = "Parts Ordered: " + zPartsOrd.ToString();
                label7.Text = "Assigned: " + zAssign.ToString();
                label10.Text = "Checking Parts: " + zCheck.ToString();
                label11.Text = "Completed     : " + zCompleted.ToString();
                label12.Text = "Back ordered Parts:   " + zPartsBack.ToString();
                label13.Text = "Sent to Manufacturer: " + zSent.ToString();
                label14.Text = "Waiting Estimate: " + zWait.ToString();
                richTextBox1.Text = richTextBox1.Text + "\n\n";
                richTextBox1.Text = richTextBox1.Text + "**********  SUMMARY  **********\n\n";
                if (zBench <= 9)
                {
                    richTextBox1.Text = richTextBox1.Text + "On Bench:                 " + zBench.ToString() + "\n";
                }
                if (zBench >= 10 && zBench <= 99)
                {
                    richTextBox1.Text = richTextBox1.Text + "On Bench:                " + zBench.ToString() + "\n";
                }
               if (zBench >= 100)
                {
                    richTextBox1.Text = richTextBox1.Text + "On Bench:               " + zBench.ToString() + "\n";
                }
                if (zAssign <= 9)
                {
                    richTextBox1.Text = richTextBox1.Text + "Assigned:                 " + zAssign.ToString() + "\n";
                }
                if (zAssign >= 10 && zAssign <= 99)
                {
                    richTextBox1.Text = richTextBox1.Text + "Assigned:                " + zAssign.ToString() + "\n";
                }
                if (zAssign >= 100)
                {
                    richTextBox1.Text = richTextBox1.Text + "Assigned:               " + zAssign.ToString() + "\n";
                }
                if (zCompleted <= 9)
                {
                    richTextBox1.Text = richTextBox1.Text + "Completed:                " + zCompleted.ToString() + "\n";
                }
                if (zCompleted >= 10 && zCompleted <= 99)
                {
                    richTextBox1.Text = richTextBox1.Text + "Completed:               " + zCompleted.ToString() + "\n";
                }
                if (zCompleted >= 100)
                {
                    richTextBox1.Text = richTextBox1.Text + "Completed:              " + zCompleted.ToString() + "\n";
                }
                if (zPartsOrd <= 9)
                {
                    richTextBox1.Text = richTextBox1.Text + "Parts Ordered:            " + zPartsOrd.ToString() + "\n";
                }
                if (zPartsOrd >= 10 && zPartsOrd <= 99)
                {
                    richTextBox1.Text = richTextBox1.Text + "Parts Ordered:           " + zPartsOrd.ToString() + "\n";
                }
                if (zPartsOrd >= 100)
                {
                    richTextBox1.Text = richTextBox1.Text + "Parts Ordered:          " + zPartsOrd.ToString() + "\n";
                }
                if (zCheck <= 9)
                {
                    richTextBox1.Text = richTextBox1.Text + "Checking Parts:           " + zCheck.ToString() + "\n";
                }
                if (zCheck >= 10 && zCheck <= 99)
                {
                    richTextBox1.Text = richTextBox1.Text + "Checking Parts:          " + zCheck.ToString() + "\n";
                }
                if (zCheck >= 100)
                {
                    richTextBox1.Text = richTextBox1.Text + "Checking Parts:         " + zCheck.ToString() + "\n";
                }
                if (zWait <= 9)
                {
                    richTextBox1.Text = richTextBox1.Text + "Waiting Estimate:         " + zWait.ToString() + "\n";
                }
                if (zWait >= 10 && zWait <= 99)
                {
                    richTextBox1.Text = richTextBox1.Text + "Waiting Estimate:        " + zWait.ToString() + "\n";
                }
                if (zWait >= 100)
                {
                    richTextBox1.Text = richTextBox1.Text + "Waiting Estimate:       " + zWait.ToString() + "\n";
                }
                if (zService <= 9)
                {
                    richTextBox1.Text = richTextBox1.Text + "Service Rendered:         " + zService.ToString() + "\n";
                }
                if (zService >= 10 && zService <= 99)
                {
                    richTextBox1.Text = richTextBox1.Text + "Service Rendered:        " + zService.ToString() + "\n";
                }
                if (zService >= 100)
                {
                    richTextBox1.Text = richTextBox1.Text + "Service Rendered:       " + zService.ToString() + "\n";
                }
                if (zPartsBack <= 9)
                {
                    richTextBox1.Text = richTextBox1.Text + "Back ordered Parts:       " + zPartsBack.ToString() + "\n";
                }
                if (zPartsBack >= 10 && zPartsBack <= 99)
                {
                    richTextBox1.Text = richTextBox1.Text + "Back ordered Parts:      " + zPartsBack.ToString() + "\n";
                }
                if (zPartsBack >= 100)
                {
                    richTextBox1.Text = richTextBox1.Text + "Back ordered Parts:     " + zPartsBack.ToString() + "\n";
                }
                if (zSent <= 9)
                {
                    richTextBox1.Text = richTextBox1.Text + "Sent to Manufacturer:     " + zSent.ToString() + "\n";
                }
                if (zSent >= 10 && zSent <= 99)
                {
                    richTextBox1.Text = richTextBox1.Text + "Sent to Manufacturer:    " + zSent.ToString() + "\n";
                }
                if (zSent >= 100)
                {
                    richTextBox1.Text = richTextBox1.Text + "Sent to Manufacturer:   " + zSent.ToString() + "\n";
                }
                richTextBox1.Text = richTextBox1.Text + "\n\n";
                richTextBox1.Text = richTextBox1.Text + "******* TOTALS SUMMARY  *******\n\n";
                var mGrandTotal = (zBench + zPartsOrd + zAssign + zCheck + zPartsBack + zSent + zWait);
                richTextBox1.Text = richTextBox1.Text + "Total Claims:           " + loopCount + "\n";
                richTextBox1.Text = richTextBox1.Text + "Open Claims:            " + mGrandTotal.ToString() + "\n";
                var mClosedClaims = zCompleted + zService;
                richTextBox1.Text = richTextBox1.Text + "Closed/Rendered Claims: " + mClosedClaims.ToString() + "\n";

                indexToText = 0;
                indexToText = richTextBox1.Find("Open and Closed Claims Summary Report", 0, RichTextBoxFinds.MatchCase);
                richTextBox1.SelectedText.Equals("Open and Closed Claims Summary Report");
                richTextBox1.SelectionFont = new Font("Courier New", 16, FontStyle.Bold);
                richTextBox1.SelectionColor = Color.Blue;
                richTextBox1.DeselectAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 523: Sorry an error has occured: " + ex.Message);
            }
        }
    }
}