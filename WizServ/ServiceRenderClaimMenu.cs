using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;

namespace WizServ
{
    public partial class ServiceRenderClaimMenu : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        private readonly string file = @"I:\\Datafile\\Control\\Database.CSV";
        private readonly string file2 = @"I:\\Datafile\\Control\\RecallDB.CSV";
        public string claimno, newClaimNo, TheNewClaimNum;
        public int loopCount, loop;

        public ServiceRenderClaimMenu() 
        {
            InitializeComponent();
            this.BackColor = Color.LightSeaGreen;
            Icon = image100;
            claimno = Version.Claim;
            label3.Text = claimno;
            label17.Text = "Old Claim #";
            label18.Text = "New Claim #";
            GetData();
        }

        public void GetData()
        {
            label20.Text = "";

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


                loopCount = 0;

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
                    listBO.Add(values[66]);
                    listBP.Add(values[67]);
                    listBQ.Add(values[68]);
                    listBR.Add(values[69]);
                    listBS.Add(values[70]);
                    listBT.Add(values[71]);


                    var mWarr = listA[loopCount];
                    var mClaim_NO = listB[loopCount];
                    var mDate_IN = listC[loopCount];
                    var mFname = listD[loopCount];
                    var mLname = listE[loopCount];
                    var mAddr = listF[loopCount];
                    var mCity = listG[loopCount];
                    var mState = listH[loopCount];
                    var mZip = listI[loopCount];
                    var mHphone = listJ[loopCount];
                    var mWPhone = listK[loopCount];
                    var mProblem = listL[loopCount];
                    var mBrand = listM[loopCount];
                    var mServNo = listN[loopCount];
                    var mModel = listO[loopCount];
                    var mSerial = listP[loopCount];
                    var mWarranty = listBL[loopCount];
                    var mFthr_exp1 = listAE[loopCount];
                    var mFthr_exp2 = listAF[loopCount];
                    var mTS1 = listAT[loopCount];
                    var mTS2 = listAV[loopCount];
                    var mTS3 = listAW[loopCount];
                    var mts4 = listAX[loopCount];
                    var mTech = listBC[loopCount];
                    var mBench = listBD[loopCount];
                    var mTheTech = listAZ[loopCount];
                    var mTheNewClaimNum = listBQ[loopCount];

                    var tt1 = mTheNewClaimNum;
                    var yy1 = tt1.Length;
                    var uu1 = yy1 - 1;
                    var k11 = tt1.Substring(1, uu1);
                    TheNewClaimNum = k11;

                    if (mClaim_NO == claimno)
                    {
                        label4.Text =  "First, Last:     " + mFname + ", " + mLname;
                        label5.Text =  "Address:         " + mAddr;
                        label6.Text =  "City, ST, Zip    " + mCity + ", " + mState + " " + mZip;
                        label11.Text = "Dealer:          " + listAI[loopCount];
                        label14.Text = "Date in:         " + listC[loopCount];
                        label15.Text = "Technician:      " + listBK[loopCount];
                        label21.ForeColor = Color.Red;
                        label19.Text = "Claim Status:    ";
                            label21.Text = listBD[loopCount];
                        if (listBI[loopCount] == "- - - ")
                        {
                            label19.Text = "Claim Status:";
                            label21.Text = listBL[loopCount];
                        }
                        if (listBI[loopCount].Contains("RECALL"))
                            {
                            button5.BackColor = Color.Firebrick;
                            button5.ForeColor = Color.White;
                            label20.Text = "Render as Parts Charge Only !";
                            TurnOffButtons2();
                            }
                        if (listBD[loopCount].Contains("RENDERED"))
                        {
                            TurnOffButtons();
                            label21.Text = label21.Text + " - CLOSED CLAIM.";
                        }
                        label7.Text =  "Product: " + listBJ[loopCount];
                        label10.Text = "Brand:   " + listM[loopCount];
                        label12.Text = "Model:   " + listO[loopCount];
                        label13.Text = "Serial#: " + listP[loopCount];
                        {
                            var tt = listBQ[loopCount];
                            var yy = tt.Length;
                            var uu = yy - 1;
                            newClaimNo = tt.Substring(1, uu);
                        }
                        label16.Text = newClaimNo;
                        if (listBE[loopCount] == "FC")
                        {
                            listBE[loopCount] = "Front Counter";
                        }
                        label9.Text = "WH Loc:  " + listBE[loopCount];

                        loop++;
                    }
                    else
                    {
                        if (TheNewClaimNum == claimno)
                        {
                            label3.Text = listB[loopCount];
                            label4.Text = "Name First, Last: " + mFname + " " + mLname;
                            label5.Text = "Address:         " + mAddr;
                            label6.Text = "City, ST, Zip    " + mCity + ", " + mState + " " + mZip;
                            label11.Text = "Dealer:          " + listAI[loopCount];
                            label14.Text = "Date in:         " + listC[loopCount];
                            label15.Text = "Technician:      " + listBK[loopCount];
                            label7.Text = "Product: " + listBJ[loopCount];
                            label10.Text = "Brand:   " + listM[loopCount];
                            label12.Text = "Model:   " + listO[loopCount];
                            label13.Text = "Serial#: " + listP[loopCount];
                            {
                                var tt = listBQ[loopCount];
                                var yy = tt.Length;
                                var uu = yy - 1;
                                newClaimNo = tt.Substring(1, uu);
                            }
                            label16.Text = newClaimNo;
                            if (listBE[loopCount] == "FC")
                            {
                                listBE[loopCount] = "Front Counter";
                            }
                            label9.Text = "WH Loc:  " + listBE[loopCount];

                            loop++;
                        }
                    }
                    loopCount++;
                }
                reader.Close(); // Close the open file

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 310: Sorry an error has occured: " + ex.Message);
            }
        }

        private void TurnOffButtons()
        {
            MessageBox.Show("Looks like already rendered.");
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            button8.Enabled = false;
            button9.Enabled = false;
            button10.Enabled = false;
            button11.Enabled = false;
            button12.Enabled = false;
            button1.ForeColor = Color.Gray;
            button2.ForeColor = Color.Gray;
            button3.ForeColor = Color.Gray;
            button4.ForeColor = Color.Gray;
            button5.ForeColor = Color.Gray;
            button6.ForeColor = Color.Gray;
            button7.ForeColor = Color.Gray;
            button8.ForeColor = Color.Gray;
            button9.ForeColor = Color.Gray;
            button10.ForeColor = Color.Gray;
            button11.ForeColor = Color.Gray;
            button12.ForeColor = Color.Gray;
            button1.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            button2.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            button3.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            button4.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            button5.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            button6.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            button7.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            button8.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            button9.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            button10.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            button11.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            button12.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void TurnOffButtons2()
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = true;
            button6.Enabled = false;
            button7.Enabled = false;
            button8.Enabled = false;
            button9.Enabled = false;
            button10.Enabled = false;
            button11.Enabled = false;
            button12.Enabled = false;
            button1.ForeColor = Color.Gray;
            button2.ForeColor = Color.Gray;
            button3.ForeColor = Color.Gray;
            button4.ForeColor = Color.Gray;
            button5.ForeColor = Color.Black;
            button6.ForeColor = Color.Gray;
            button7.ForeColor = Color.Gray;
            button8.ForeColor = Color.Gray;
            button9.ForeColor = Color.Gray;
            button10.ForeColor = Color.Gray;
            button11.ForeColor = Color.Gray;
            button12.ForeColor = Color.Gray;
            button1.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            button2.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            button3.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            button4.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            button5.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            button6.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            button7.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            button8.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            button9.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            button10.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            button11.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            button12.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Hide();
            SRNONWARR f2 = new SRNONWARR();
            f2.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void Button6_Click(object sender, EventArgs e)  // Move unit to "On Bench"
        {
            Hide();
            UnitOnBechForService f2 = new UnitOnBechForService();
            f2.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {

        }

        private void Button14_Click(object sender, EventArgs e)
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }

        private void button16_Click(object sender, EventArgs e)
        {

        }

        private void utilitiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Version.From = "ServiceUtility";
            Hide();
            Password f2 = new Password();
            f2.Show();
        }

        private void mainMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }

        private void laborWarrantyOnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void partsWarrantyOnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void partsWarrantyOnlyToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void nonWarrantyToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void recallNoChargeOnLaborToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void unitOnBenchForServiceToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void sendToOutsideContractorToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void waitingOnEstimateApprovalToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void orderServiceManualToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void unitHasBackorderedPartsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void partsOrderedByDescriptionToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void checkingPartsCostsAvailabilityToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {
            Hide();
            _Render f2 = new _Render();
            f2.Show();
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Hide();
                MainMenu f2 = new MainMenu();
                f2.Show();
            }
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            Version.From = "ServiceUtility";
            Hide();
            Password f2 = new Password();
            f2.Show();
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            Version.From = "ServiceUtility";
            Hide();
            Password f2 = new Password();
            f2.Show();
        }
    }
}
