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
using System.Windows.Forms;

namespace WizServ
{
    public partial class ByINT_CLM_Number : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        private readonly string file = @"I:\\Datafile\\Control\\Database.CSV";
        private string claim_no;    // fname, lname, addr, city, state, zip, hphone, wphone;
        //private bool war_prd;
        //private DateTime datein;
        public int loopCount, loop;
        public string Lines = "-------------------------------------------------------------------------------------";

        public ByINT_CLM_Number()
        {
            InitializeComponent();
            Icon = image100;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
            claim_no = Version.Claim;
            label7.Text = "Double-Click on Claim # to select that claim.";
            Text = "Retrieve Claim by Last Name";
            label1.Text = "Searching for: " + claim_no;
            GetData();
        }

        private void richTextBox1_DoubleClick(object sender, EventArgs e)
        {
            var SelectedText = richTextBox1.SelectedText;
            Version.Claim = SelectedText.Trim();
            claim_no = SelectedText.Trim();
            if (SelectedText.Length <= 5)
            {
                return;
            }
            if (SelectedText.Length >= 7)
            {
                return;
            }
            Hide();
            ByClaimNum f2 = new ByClaimNum();
            f2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            RetrieveMenu f2 = new RetrieveMenu();
            f2.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
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
            catch (Exception)
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
                if (ex.ToString().Contains("You can't copy 'nothing', select some text first."))
                {
                    // Ignore nothing selected
                }
                else
                {
                    MessageBox.Show("Sorry an exception has occured. Line 169\n" + ex);
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

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            NewClaim f2 = new NewClaim();
            f2.Show();
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


                loopCount = 0;
                loop = 0;

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

                    if (listD[loopCount].Contains(claim_no))
                    {
                        var mwidth = listD[loopCount] + ", " + listE[loopCount];
                        var TheWidth = mwidth.Length;
                        switch (TheWidth)
                        {
                            case 26:
                                richTextBox1.Text = richTextBox1.Text + listB[loopCount] + "\t" + listE[loopCount] + ", " + listD[loopCount] + "\t\t\t\t" + listG[loopCount] + "\t" + listH[loopCount] + "\t" + listBB[loopCount] + "\n" + Lines + "\n";
                                break;
                            case 29:
                                richTextBox1.Text = richTextBox1.Text + listB[loopCount] + "\t" + listE[loopCount] + ", " + listD[loopCount] + "\t\t\t" + listG[loopCount] + "\t" + listH[loopCount] + "\t" + listBB[loopCount] + "\n" + Lines + "\n";
                                break;
                            case 30:
                                richTextBox1.Text = richTextBox1.Text + listB[loopCount] + "\t" + listE[loopCount] + ", " + listD[loopCount] + "\t\t\t" + listG[loopCount] + "\t" + listH[loopCount] + "\t" + listBB[loopCount] + "\n" + Lines + "\n";
                                break;
                            case 31:
                                richTextBox1.Text = richTextBox1.Text + listB[loopCount] + "\t" + listE[loopCount] + ", " + listD[loopCount] + "\t\t" + listG[loopCount] + "\t" + listH[loopCount] + "\t" + listBB[loopCount] + "\n" + Lines + "\n";
                                break;
                            case 39:
                                richTextBox1.Text = richTextBox1.Text + listB[loopCount] + "\t" + listE[loopCount] + ", " + listD[loopCount] + "\t" + listG[loopCount] + "\t" + listH[loopCount] + "\t" + listBB[loopCount] + "\n" + Lines + "\n";
                                break;
                            default:
                                richTextBox1.Text = richTextBox1.Text + listB[loopCount] + "\t" + listE[loopCount] + ", " + listD[loopCount] + "\t\t" + listG[loopCount] + "\t" + listH[loopCount] + "\t" + listBB[loopCount] + "\n" + Lines + "\n";
                                break;
                        }
                        loop++;
                    }
                    if (listE[loopCount].Contains(claim_no))
                    {
                        var mwidth = listD[loopCount] + ", " + listE[loopCount];
                        var TheWidth = mwidth.Length;
                        switch (TheWidth)
                        {
                            case 26:
                                richTextBox1.Text = richTextBox1.Text + listB[loopCount] + "\t" + listE[loopCount] + ", " + listD[loopCount] + "\t\t\t\t" + listG[loopCount] + "\t" + listH[loopCount] + "\t" + listBB[loopCount] + "\n" + Lines + "\n";
                                break;
                            case 29:
                                richTextBox1.Text = richTextBox1.Text + listB[loopCount] + "\t" + listE[loopCount] + ", " + listD[loopCount] + "\t\t\t" + listG[loopCount] + "\t" + listH[loopCount] + "\t" + listBB[loopCount] + "\n" + Lines + "\n";
                                break;
                            case 30:
                                richTextBox1.Text = richTextBox1.Text + listB[loopCount] + "\t" + listE[loopCount] + ", " + listD[loopCount] + "\t\t\t" + listG[loopCount] + "\t" + listH[loopCount] + "\t" + listBB[loopCount] + "\n" + Lines + "\n";
                                break;
                            case 31:
                                richTextBox1.Text = richTextBox1.Text + listB[loopCount] + "\t" + listE[loopCount] + ", " + listD[loopCount] + "\t\t" + listG[loopCount] + "\t" + listH[loopCount] + "\t" + listBB[loopCount] + "\n" + Lines + "\n";
                                break;
                            case 39:
                                richTextBox1.Text = richTextBox1.Text + listB[loopCount] + "\t" + listE[loopCount] + ", " + listD[loopCount] + "\t" + listG[loopCount] + "\t" + listH[loopCount] + "\t" + listBB[loopCount] + "\n" + Lines + "\n";
                                break;
                            default:
                                richTextBox1.Text = richTextBox1.Text + listB[loopCount] + "\t" + listE[loopCount] + ", " + listD[loopCount] + "\t\t" + listG[loopCount] + "\t" + listH[loopCount] + "\t" + listBB[loopCount] + "\n" + Lines + "\n";
                                break;
                        }
                        loop++;
                    }
                    loopCount++;
                }
                reader.Close(); // Close the open file

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 239: Sorry an error has occured: " + ex.Message);
            }
        }


    }
}
