using System;
using Microsoft.Win32;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace WizServ
{
    public partial class TechAssignment : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        public string t;
        public static readonly string Database = @"I:\\Datafile\\Control\\Database.CSV";
        public readonly string TechNames = @"I:\\Datafile\\Control\\Technician_Names.csv";
        private readonly string PREVIOUS = @"I:\\Datafile\\Control\\Prev_Tech_Assign.CSV";
        public static readonly string TechAssign = @"I:\\Datafile\\Control\\Tech_Assign.csv";
        public static readonly string TechAssign2 = @"I:\\Datafile\\Control\\Tech_Assign2.csv";
        public static readonly string TechAssign3 = @"I:\\Datafile\\Control\\Tech_Assign3.csv";
        public static readonly string TechAssign24 = @"I:\\Datafile\\Control\\Tech_Assign24.csv";
        static readonly string key = @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\lanmanserver\parameters";
        public readonly string computerDescription = (string)Registry.GetValue(key, "srvcomment", null);
        public bool PFOUND = false, blnReturn = false;
        public string TechicianNames1, TechicianNames2, TechicianNames3, TechicianNames4, TechicianNames5;
        public string claim_no, WriteString1, Header, Header2, SELECTED, clientDetails, ThePickedTech;
        public int loopCount, loop, loopCount1, loop1, DisplayLoopCount, UpdatePrevious, UnassignedCount;
        public bool Found, IsNumber;
        public string Mex, TheFileIs, TheFileNameIs, yeardigit, All, OnBench, WriteString2, TechName;
        public string kkk, SAVEDDATA, butPressed = "1";
        public string XTECHID, XTECHNAME, XTECHNO;
        public decimal d4, d5, kkkShip;
        public int pploop, loopArray, index, pass;
        public string[] lines;
        public string[] bytech, DBbyclaim;
        public string byline, DBbyline, byLineAlso;
        public bool IsChecked, IsChecked2, dbFileIsLocked, XFOUND;
        public static string mWarr, mClaim_NO, mDate_IN, mFname, mLname, mAddr, mCity, mState, mZip, mHphone, mWPhone;
        public string mProblem, mBrand, mServNo, mModel, mSerial, mq, mr, ms, mt, mu, mv, mw, mx, my, mz;
        public static string maa, mab, mac, mad, mFthr_exp1, mFthr_exp2, mag, mah, mai, maj, mak, mal, mam, man, mao;
        public static string map, maq, mar, mas, mWar_Note, mau, mav, maw, max, may, maz;
        public static string mba, mbb, mbc, mbd, mbe, mbf, mbg, mbh, mbi, mbj, mbk, mWarranty, mbm, mbn, mbo, mbp;
        public string mbq, mbr, mbs, mbt;
        public static string mbu, mbv, mbw, mbx, mby, mbz, mca, mcb, TheTechis, mused;
        public string c = ",", TechNumber, TechID, TodayString, SelectedText;
        public string mmProduct, mmDate_IN, mmFname, mmLname, mmRecall, mmRush, mmESTYN, mmShelf, mmWar_Note;
        public string PCLAIM, PCURRENT, PPREV1, PPREV2, PPREV3, PPREV4, PPREV5, PDATE;

        public TechAssignment()
        {
            InitializeComponent();
            TodayString = DateTime.Today.ToString("MM/dd/yyyy");
            Icon = image100;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SELECTED = comboBox1.Text;
                ChangeTech();
            }
        }

        private void CheckDBFile()
        {
            dbFileIsLocked = FileIsLocked(@"I:\\Datafile\\Control\\Database.CSV");
            if (dbFileIsLocked == true)
            {
                MessageBox.Show("Error line 80:\nDatabase is in use.\nDatabase.\nWiat 1 minute and try again.");
                return;
            }
        }

        private void AssignNames()  // This loads all tech names into ComboBox1
        {
            try
            {
                StreamReader reader = new StreamReader(TechNames, Encoding.GetEncoding("Windows-1252"));
                String line = reader.ReadLine();

                List<string> listA = new List<string>();

                loopCount = 0;

                while (!reader.EndOfStream)
                {
                    var lineRead = reader.ReadLine();
                    var values = lineRead.Split(',');

                    listA.Add(values[loopCount]);       //  Tech Name

                    comboBox1.Items.Add(listA[0]);
                    comboBox1.Items.Add(values[1]);
                    comboBox1.Items.Add(values[2]);
                    comboBox1.Items.Add(values[3]);
                    comboBox1.Items.Add(values[4]);
                    comboBox1.Items.Add(values[5]);
                    comboBox1.Items.Add(values[6]);
                    loopCount++;
                }
                comboBox1.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 115: Please try again\nTechNames DB in use.");
            }
        }

        public void SetTechNumber()
        {
            switch (TechName)
            {
                case "COLE":
                    TechNumber = "1";
                    TechName = "COLE";
                    TechID = "CH";
                    break;
                case "WALTER":
                    TechNumber = "2";
                    TechName = "WALTER";
                    TechID = "WK";
                    break;
                case "WILLIAM":
                    TechNumber = "6";
                    TechName = "WILLIAM";
                    TechID = "WB";
                    break;
                case "DEREK":
                    TechNumber = "3";
                    TechName = "DEREK";
                    TechID = "DN";
                    break;
                case "BILLY":
                    TechNumber = "4";
                    TechName = "BILLY";
                    TechID = "BS";
                    break;
                case "NOEL":
                    TechNumber = "5";
                    TechName = "NOEL";
                    TechID = "NA";
                    break;
                case "ANGELO":
                    TechNumber = "5";
                    TechName = "ANGELO";
                    TechID = "AA";
                    break;
                case "CONSIGN":
                    TechNumber = "9";
                    TechName = "CONSIGN";
                    TechID = "CS";
                    break;
                case "PARTS":
                    TechNumber = "8";
                    TechName = "PARTS";
                    TechID = "PA";
                    break;
            }
        }

        public bool FileIsLocked(string strFullFileName)
        {
            bool blnReturn = false;
            System.IO.FileStream fs;
            try
            {
                fs = System.IO.File.Open(strFullFileName, FileMode.OpenOrCreate, FileAccess.Read, FileShare.None);
                fs.Close();
            }
            catch (IOException ex)
            {
                blnReturn = true;
            }
            return blnReturn;
        }

        private void CopyTechAssign()
        {
            if (Version.PauseDBAccess == true)
            {
                Thread.Sleep(5000);
            }
            CheckDBFile();
            string sourceFile = TechAssign;
            string destinationFile = TechAssign2;
            try
            {
                File.Copy(sourceFile, destinationFile, true);
            }
            catch (IOException iox)
            {
                MessageBox.Show("Error 197 occured during copy\n " + iox.Message);
            }
        }

        public void EditTechAssign()
        {
            try
            {
                if (Version.PauseDBAccess == true)
                {
                    Thread.Sleep(5000);
                }
                CheckDBFile();
                dbFileIsLocked = FileIsLocked(@"I:\\Datafile\\Control\\Tech_Assign.csv");

                if (dbFileIsLocked == true)
                {
                    MessageBox.Show("Error 214: File in use.\nTech Assign.\nRetrying...");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 220: File in use\nTech Assign DB");
            }
            try
            {
                string clientDetails = claim_no + "," + mmDate_IN + "," + mmWar_Note + "," + TechName + "," + "BENCH ON BENCH - " + TodayString + "," + mmShelf + "," + mmWar_Note + "," + mmESTYN + "," + mmRush + "," + mmRecall + Environment.NewLine;

                if (!File.Exists(TechAssign))
                {
                    string clientHeader = "CLAIM_NO" + "," + "DATEIN" + "," + "WARR_NOTE" + "," + "TECH" + "," + "STATUS" + "," + "WH_LOC" + "," + "REFB_CODE" + "," + "ESST_YN" + "," + "RUSH" + "," + "RECSLL" + Environment.NewLine;

                    File.WriteAllText(TechAssign, clientHeader);
                }

                File.AppendAllText(TechAssign, clientDetails);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 230: Please try again\nTech Assignment file in use.");
            }

            SetupBox();
            GetData();
        }

        public void EditCSVDatabase()
        {
            try
            {
                if (Version.PauseDBAccess == true)
                {
                    Thread.Sleep(5000);
                }
                CheckDBFile();
                SetTechNumber();    // Setup the Tech Initials, Tech # & Tech ID number.

                dbFileIsLocked = FileIsLocked(@"I:\\Datafile\\Control\\Database.CSV");
                if (dbFileIsLocked == true)
                {
                    MessageBox.Show("Database is in use.\nDatabase.");
                    return;
                }
                if (Version.PauseDBAccess == true)
                {
                    Thread.Sleep(5000);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 261:Please try again\nDatabase file in use.");
            }

            List<String> lines = new List<String>();

            if (File.Exists(Database))
            {
                try
                {
                    using (StreamReader reader = new StreamReader(Database))
                    {
                        String line;

                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line.Contains(","))
                            {
                                String[] split = line.Split(',');

                                if (split[1].Contains(claim_no))
                                {
                                    split[0] = "REDRUM";
                                    split[1] = claim_no;    // Claim #
                                    split[2] = mmDate_IN;
                                    split[3] = mmFname;
                                    split[4] = mmLname;
                                    split[50] = TechID;
                                    split[51] = TechName;
                                    split[52] = TechNumber;
                                    split[55] = "BENCH ON BENCH - " + TodayString;
                                    split[62] = TechName;
                                    line = String.Join(",", split);
                                }
                            }

                            lines.Add(line);
                        }
                        reader.Close();
                    }

                    using (StreamWriter writer = new StreamWriter(Database, false))
                    {
                        if (Version.PauseDBAccess == true)
                        {
                            Thread.Sleep(5000);
                        }
                        foreach (String line in lines)
                            writer.WriteLine(line);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error 292: Please try again\nDatbase is in use.");
                }
            }
            EditTechAssign();
            if (dbFileIsLocked == true)
            {
                dbFileIsLocked = false;
                EditTechAssign();
            }
            //SetupBox();
            //GetData();
        }

        private void TechAssignment_Load(object sender, EventArgs e)
        {
            SelectedText = " ";
            comboBox1.Visible = false;
            AssignNames();
            GetTech();
            button3.Visible = false;
            IsChecked = true;
            claim_no = "";
            GetData();
        }

        private void TurnOnLabels()
        {
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            label6.Visible = true;
            label7.Visible = true;
            label8.Visible = true;
            label12.Visible = true;
            label17.Visible = true;
            label20.Visible = true;
            label22.Visible = true;
            label24.Visible = true;
            label26.Visible = true;
            label30.Visible = true;
            label32.Visible = true;
            label34.Visible = true;
            label36.Visible = true;
            ShowLabels();
        }
        private void SetupBox()
        {
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label12.Visible = false;
            label17.Visible = false;
            label20.Visible = false;
            label22.Visible = false;
            label24.Visible = false;
            label26.Visible = false;
            label30.Visible = false;
            label32.Visible = false;
            label34.Visible = false;
            label36.Visible = false;
        }

        private void HideLabels()
        {
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
        }

        private void ShowLabels()
        {
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
        }

        public void GetTech()
        {
            if (SelectedText != "")
            {
                claim_no = SelectedText;
            }
            try
            {
                StreamReader reader = new StreamReader(PREVIOUS, Encoding.GetEncoding("Windows-1252"));
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

                    listA.Add(values[0]);       //  claim_no
                    listB.Add(values[1]);       //  Current Tech Assignment
                    listC.Add(values[2]);       //  Previous 1
                    listD.Add(values[3]);       //  Previous 2
                    listE.Add(values[4]);       //  Previous 3
                    listF.Add(values[5]);       //  Previous 4
                    listG.Add(values[6]);       //  Previous 5
                    listH.Add(values[7]);       //  Date

                    if (claim_no == listA[loopCount])
                    {
                        ShowLabels();
                        PFOUND = true;
                        PCLAIM = listA[loopCount];
                        label21.Text = PCLAIM;
                        PCURRENT = listB[loopCount];
                        label35.Text = PCURRENT;
                        PPREV1 = listC[loopCount];
                        if (PPREV1 != ".")
                        {
                            label29.Text = PPREV1;
                        }
                        else
                        {
                            label23.Visible = false;
                            label29.Visible = false;
                        }
                        PPREV2 = listD[loopCount];
                        if (PPREV2 != ".")
                        {
                            label30.Text = PPREV2;
                        }
                        else
                        {
                            label24.Visible = false;
                            label30.Visible = false;
                        }
                        PPREV3 = listE[loopCount];
                        if (PPREV3 != ".")
                        {
                            label31.Text = PPREV3;
                        }
                        else
                        {
                            label25.Visible = false;
                            label31.Visible = false;
                        }
                        PPREV4 = listF[loopCount];
                        if (PPREV4 != ".")
                        {
                            label32.Text = PPREV4;
                        }
                        else
                        {
                            label26.Visible = false;
                            label32.Visible = false;
                        }
                        PPREV5 = listG[loopCount];
                        if (PPREV5 != ".")
                        {
                            label33.Text = PPREV5;
                        }
                        else
                        {
                            label27.Visible = false;
                            label33.Visible = false;
                        }
                        PDATE = listH[loopCount];
                        //label41.Text = PDATE;

                        if (listB[loopCount] != ".")
                        {
                            //label40.Text = "Claim is in " + listB[loopCount] + "'s que.";
                        }
                        if (listC[loopCount] != ".")
                        {
                            //label40.Text = "Claim started in " + listC[loopCount] + "'s que, then " + listB[loopCount] + "'s";
                        }
                        if (listD[loopCount] != ".")
                        {
                            // label40.Text = "Claim started in " + listD[loopCount] + "'s que, then " + listC[loopCount] + "'s then " + listB[loopCount] + "'s";
                        }
                        if (listE[loopCount] != ".")
                        {
                            //label40.Text = "Claim started in " + listE[loopCount] + "'s que, then " + listD[loopCount] + "'s then " + listC[loopCount] + "'s then " + listB[loopCount] + "'s";
                        }
                        if (listF[loopCount] != ".")
                        {
                            // label40.Text = "Claim started in " + listF[loopCount] + "'s que, then " + listE[loopCount] + "'s then " + listD[loopCount] + "'s then " + listC[loopCount] + "'s then " + listB[loopCount] + "'s";
                        }
                        if (listG[loopCount] != ".")
                        {
                            //label40.Text = "Claim started in " + listG[loopCount] + "'s que, then " + listF[loopCount] + "'s then " + listE[loopCount] + "'s then " + listD[loopCount] + "'s then " + listC[loopCount] + "'s then " + listB[loopCount] + "'s";
                        }
                    }
                    loopCount++;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 495: Sorry an error has occured: " + ex.Message);
            }
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            button3.Visible = true;

            if (comboBox1.Text.Length > 1)  // Once it closed value is set back to "", so below keeps vars valid throughout.
            {
                ThePickedTech = comboBox1.Text;
                SELECTED = comboBox1.Text;
                SelectedText = comboBox1.Text;
            }
            switch (comboBox1.SelectedItem)
            {
                case "COLE":
                    TechNumber = "1";
                    TechName = "COLE";
                    TechID = "CH";
                    break;
                case "WALTER":
                    TechNumber = "2";
                    TechName = "WALTER";
                    TechID = "WK";
                    break;
                case "WILLIAM":
                    TechNumber = "6";
                    TechName = "WILLIAM";
                    TechID = "WB";
                    break;
                case "DEREK":
                    TechNumber = "3";
                    TechName = "DEREK";
                    TechID = "DN";
                    break;
                case "DREW":
                    TechNumber = "4";
                    TechName = "DREW";
                    TechID = "DL";
                    break;
                case "NOEL":
                    TechNumber = "5";
                    TechName = "NOEL";
                    TechID = "NA";
                    break;
                case "ANGELO":
                    TechNumber = "5";
                    TechName = "ANGELO";
                    TechID = "AA";
                    break;
                case "CONSIGN":
                    TechNumber = "9";
                    TechName = "CONSIGN";
                    TechID = "CS";
                    break;
                case "PARTS":
                    TechNumber = "8";
                    TechName = "PARTS";
                    TechID = "PA";
                    break;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CheckDBFile();
            // After saving, hide text & this button until another claim is selected.
            pass = 0;
            EditCSVDatabase();
            comboBox1.SelectedIndex = -1;
            button3.Visible = false;
            comboBox1.Visible = false;
            CopyTechAssign();
            AddNewLine();
            CopyTechAssign3();
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                var t = listBox1.SelectedItem.ToString();
                if (t == "–––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––")
                {
                    return;
                }
                if (t.StartsWith("CLAIM"))
                {
                    return;
                }
                var h = t.Substring(0, 7).Trim();
                SelectedText = h;
                GetTech();
                TurnOnLabels();
                if (PFOUND == false)
                {
                    HideLabels();
                    MessageBox.Show("Claim Not Found in\nPrevious Tech Database.\n\nIt will be added.\nAfter clicking SAVE button.");
                }
                if (SelectedText.Length <= 5)
                {
                    return;
                }
                if (SelectedText.Length >= 7)
                {
                    return;
                }
                if (Regex.IsMatch(SelectedText, @"^\d"))
                {
                    IsNumber = true;
                }
                if (SelectedText.Length == 6)
                {
                    if (IsNumber == true)
                    {
                        TurnOnLabels();
                        comboBox1.Visible = true;
                        claim_no = SelectedText.Trim();
                        label1.Text = claim_no;
                        GetData();
                    }
                    IsNumber = false;
                }
                else
                {
                    return;
                }
                CheckIfMoreClaims();
            }
            catch (Exception ex)
            {
                if (ex.ToString().Contains("Object reference not set to an instance of an object."))
                {
                    return;
                }
            }
        }

        private void CopyTechAssign3()
        {
            if (Version.PauseDBAccess == true)
            {
                Thread.Sleep(5000);
            }
            CheckDBFile();
            string sourceFile = TechAssign3;
            string destinationFile = TechAssign;
            try
            {
                File.Copy(sourceFile, destinationFile, true);
            }
            catch (IOException iox)
            {
                MessageBox.Show("Error 717 occured during copy\n " + iox.Message);
            }
        }

        private void ChangeTechinDatabase()
        {
            if (Version.PauseDBAccess == true)
            {
                Thread.Sleep(5000);
            }
            if (PFOUND == true)
            {
                ShowLabels();
                PCURRENT = SELECTED;
                switch (PCURRENT)
                {
                    case "COLE":
                        XTECHID = "CH";
                        XTECHNAME = "COLE";
                        XTECHNO = "1";
                        break;
                    case "WALTER":
                        XTECHID = "WK";
                        XTECHNAME = "WALTER";
                        XTECHNO = "2";
                        break;
                    case "DAVID":
                        XTECHID = "DS";
                        XTECHNAME = "DAVID";
                        XTECHNO = "3";
                        break;
                    case "BILLY":
                        XTECHID = "BS";
                        XTECHNAME = "BILLY";
                        XTECHNO = "4";
                        break;
                    case "DEREK":
                        XTECHID = "DN";
                        XTECHNAME = "DEREK";
                        XTECHNO = "3";
                        break;
                    case "WILLIAM":
                        XTECHID = "WB";
                        XTECHNAME = "WILLIAM";
                        XTECHNO = "6";
                        break;
                    case "NOEL":
                        XTECHID = "NA";
                        XTECHNAME = "NOEL";
                        XTECHNO = "5";
                        break;
                    case "ANGELO":
                        XTECHID = "AA";
                        XTECHNAME = "ANGELO";
                        XTECHNO = "5";
                        break;
                    case "BRETT":
                        XTECHID = "BE";
                        XTECHNAME = "BRETT";
                        XTECHNO = "4";
                        break;
                    case "CONNOR":
                        XTECHID = "CD";
                        XTECHNAME = "CONNOR";
                        XTECHNO = "5";
                        break;
                    case "PARTS":
                        XTECHID = "DR";
                        XTECHNAME = "PARTS";
                        XTECHNO = "0";
                        break;
                    case "CONSIGN":
                        XTECHID = "CON";
                        XTECHNAME = "CONSIGN";
                        XTECHNO = "9";
                        break;
                }

                List<String> lines = new List<String>();
                CheckDBFile();
                if (File.Exists(Database))
                {
                    try
                    {
                        using (StreamReader reader = new StreamReader(Database))
                        {
                            String line;

                            while ((line = reader.ReadLine()) != null)
                            {
                                if (line.Contains(","))
                                {
                                    String[] split = line.Split(',');
                                    try
                                    {
                                        if (split[1].Contains(claim_no))
                                        {
                                            XFOUND = true;
                                            split[50] = XTECHID;
                                            split[51] = XTECHNAME;
                                            split[52] = XTECHNO;
                                            split[55] = "BENCH ON BENCH - " + TodayString;
                                            split[62] = XTECHNAME;
                                            PFOUND = true;
                                            line = String.Join(",", split);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("Error: Line 826\n" + ex);
                                    }
                                }
                                lines.Add(line);
                            }
                            reader.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error 836: Please try again\nDatabase is in use.");
                    }

                    using (StreamWriter writer = new StreamWriter(Database, false))
                    {
                        foreach (String line in lines)
                            writer.WriteLine(line);
                    }

                }
                label39.Visible = true;
                label39.Text = "Database Data Saved";
                label39.ForeColor = Color.White;
                label39.BackColor = Color.Red;
            }

            if (PFOUND == false)
            {
                label39.Visible = true;
                label39.Text = "Database Data NOT Saved";
                label39.ForeColor = Color.White;
                label39.BackColor = Color.Red;
                UpdatePrevious = 0;
                UpdateThePreviousDB();
            }
        }

        private void UpdateThePreviousDB()
        {
            PDATE = DateTime.Now.ToShortDateString();
            if (Version.PauseDBAccess == true)
            {
                Thread.Sleep(5000);
            }

            List<String> lines = new List<String>();

            if (File.Exists(PREVIOUS))
            {
                using (StreamReader reader = new StreamReader(PREVIOUS))
                {
                    String line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Contains(","))
                        {
                            String[] split = line.Split(',');
                            try
                            {
                                if (split[0].Contains(claim_no))
                                {
                                    split[1] = comboBox1.Text;
                                    split[2] = ".";
                                    split[3] = ".";
                                    split[4] = ".";
                                    split[5] = ".";
                                    split[6] = ".";
                                    split[7] = PDATE;
                                    PFOUND = true;
                                    line = String.Join(",", split);
                                }
                                else
                                {
                                    if (UpdatePrevious == 0)
                                    {
                                        split[1] = SelectedText;
                                        split[2] = ".";
                                        split[3] = ".";
                                        split[4] = ".";
                                        split[5] = ".";
                                        split[6] = ".";
                                        split[7] = PDATE;
                                        PFOUND = true;
                                        line = String.Join(",", split);
                                    }
                                    UpdatePrevious++;
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error: Line 917\n" + ex);
                            }
                        }
                        lines.Add(line);
                    }
                    reader.Close();
                }

                using (StreamWriter writer = new StreamWriter(PREVIOUS, false))
                {
                    foreach (String line in lines)
                        writer.WriteLine(line);
                }
            }
            label38.Visible = true;
            label38.Text = "Data Saved";
            label38.ForeColor = Color.White;
            label38.BackColor = Color.Red;
            GetTech();                                  // Update bottom panel after change
        }



        private void ChangeTech()
        {
            if (PFOUND == true)
            {
                //ShowLabels();
                PPREV5 = PPREV4;
                PPREV4 = PPREV3;
                PPREV3 = PPREV2;
                PPREV2 = PPREV1;
                PPREV1 = PCURRENT;
                PCURRENT = SELECTED;
                ChangeTechinDatabase();
                if (XFOUND == true)
                {
                    label39.Visible = true;
                    label39.Text = "Database Data Saved";
                    label39.ForeColor = Color.White;
                    label39.BackColor = Color.Red;
                }
                else
                {
                    label39.Visible = true;
                    label39.Text = "Database Data NOT Saved";
                    label39.ForeColor = Color.White;
                    label39.BackColor = Color.Red;
                }
                PDATE = DateTime.Now.ToShortDateString();

                List<String> lines = new List<String>();

                if (File.Exists(PREVIOUS))
                {
                    using (StreamReader reader = new StreamReader(PREVIOUS))
                    {
                        String line;

                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line.Contains(","))
                            {
                                String[] split = line.Split(',');
                                try
                                {
                                    if (split[0].Contains(claim_no))
                                    {
                                        split[1] = PCURRENT;
                                        split[2] = PPREV1;
                                        split[3] = PPREV2;
                                        split[4] = PPREV3;
                                        split[5] = PPREV4;
                                        split[6] = PPREV5;
                                        split[7] = PDATE;
                                        PFOUND = true;
                                        line = String.Join(",", split);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Error: Line 998\n" + ex);
                                }
                            }
                            lines.Add(line);
                        }
                        reader.Close();
                    }

                    using (StreamWriter writer = new StreamWriter(PREVIOUS, false))
                    {
                        foreach (String line in lines)
                            writer.WriteLine(line);
                    }

                }
                label38.Visible = true;
                label38.Text = "Data Saved";
                label38.ForeColor = Color.White;
                label38.BackColor = Color.Red;
                GetTech();                                  // Update bottom panel after change
            }

            if (PFOUND == false)
            {
                label38.Visible = true;
                label38.Text = "Data NOT Saved";
                label38.ForeColor = Color.White;
                label38.BackColor = Color.Red;
                AddNewLine();
            }
        }

        private void AddNewLine()
        {
            {
                var csv = new StringBuilder();
                string com = ",";
                string first = claim_no;
                //string second = SelectedText;
                string second = ThePickedTech;
                string third = ".";
                string fourth = ".";
                string fifth = ".";
                string sixth = ".";
                string seventh = ".";
                string eigth = DateTime.Now.ToShortDateString();
                var newLine = first + com + second + com + third + com + fourth + com + fifth + com + sixth + com + seventh + com + eigth + Environment.NewLine;
                clientDetails = newLine;
            }
            if (!File.Exists(PREVIOUS))
            {
                var csv = new StringBuilder();
                string com = ",";
                string first = claim_no;
                // string second = TechName;
                string second = ThePickedTech;
                string third = ".";
                string fourth = ".";
                string fifth = ".";
                string sixth = ".";
                string seventh = ".";
                string eigth = DateTime.Now.ToShortDateString();
                var newLine = first + com + second + com + third + com + fourth + com + fifth + com + sixth + com + seventh + com + eigth + Environment.NewLine;
                clientDetails = newLine;
                try
                {
                    string PrevHeader = "ClaimNo" + "," + "CURRENT" + "," + "PREV1" + "," + "PREV2" + "<" + "PREV3" + "," + "PREV4" + "," + "PREV5" + "," + "DATE";
                    File.WriteAllText(PREVIOUS, PrevHeader);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception line 936\n" + ex);
                }
            }
            try
            {
                File.AppendAllText(PREVIOUS, clientDetails);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception line 1078\n" + ex);
            }

            label38.Visible = true;
            label38.Text = "Previous DB Updated";
            label38.ForeColor = Color.White;
            label38.BackColor = Color.Red;
            GetTech();
        }

        private void CheckIfMoreClaims()
        {
            if (listBox1.Text.Length == 124)
            {
                listBox1.Items.Add(" No more claims to list");
            }
        }

        private void button1_Click(object sender, EventArgs e)  // Start
        {
            IsChecked = true;
            claim_no = "";
            GetData();
        }

        private void button2_Click(object sender, EventArgs e)  // return to menu
        {
            this.Close();
            Version.PauseDBAccess = false;
            Hide();
            MainMenu f0 = new MainMenu();
            f0.Show();
        }

        public void GetData()
        {
            if (SelectedText.Length == 6)
            {
                claim_no = SelectedText;
            }

            listBox1.Items.Clear();
            if (Version.PauseDBAccess == true)
            {
                Thread.Sleep(5000);
            }
            loopCount = 0;
            UnassignedCount = 0;
            try
            {
                StreamReader reader = new StreamReader(Database, Encoding.GetEncoding("Windows-1252"));
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
                List<string> listBY = new List<string>();
                List<string> listBZ = new List<string>();
                List<String> listCA = new List<string>();
                List<String> listCB = new List<string>();

                listBox1.Items.Add("CLAIM   Product           Manufacturer          Model");
                listBox1.Items.Add("–––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––");

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
                    listBY.Add(values[76]);     //  Used
                    listBZ.Add(values[77]);     //  Estimate Deposit
                    listCA.Add(values[78]);     //  Closed
                    listCB.Add(values[79]);     //  Picked up

                    mWarr = listA[loopCount];
                    mClaim_NO = listB[loopCount];
                    mDate_IN = listC[loopCount];
                    mFname = listD[loopCount];
                    mLname = listE[loopCount];
                    mAddr = listF[loopCount];
                    mCity = listG[loopCount];
                    mState = listH[loopCount];
                    mZip = listI[loopCount];
                    mHphone = listJ[loopCount];
                    mWPhone = listK[loopCount];
                    mProblem = listL[loopCount];
                    mBrand = listM[loopCount];
                    mServNo = listN[loopCount];
                    mModel = listO[loopCount];
                    mSerial = listP[loopCount];
                    mq = listQ[loopCount];
                    mr = listR[loopCount];
                    ms = listS[loopCount];
                    mt = listT[loopCount];
                    mu = listU[loopCount];
                    mv = listV[loopCount];
                    mw = listW[loopCount];
                    mx = listX[loopCount];
                    my = listY[loopCount];
                    mz = listZ[loopCount];
                    maa = listAA[loopCount];
                    mab = listAB[loopCount];
                    mac = listAC[loopCount];
                    mad = listAD[loopCount];
                    mFthr_exp1 = listAE[loopCount];
                    mFthr_exp2 = listAF[loopCount];
                    mag = listAG[loopCount];
                    mah = listAH[loopCount];
                    mai = listAI[loopCount];
                    maj = listAJ[loopCount];
                    mak = listAK[loopCount];
                    mal = listAL[loopCount];
                    mam = listAM[loopCount];
                    man = listAN[loopCount];
                    mao = listAO[loopCount];
                    map = listAP[loopCount];
                    maq = listAQ[loopCount];
                    mar = listAR[loopCount];
                    mas = listAS[loopCount];
                    mWar_Note = listAT[loopCount];
                    mbi = listBI[loopCount];
                    mbe = listBE[loopCount];


                    var mTS1 = listAU[loopCount];
                    var mTS2 = listAV[loopCount];
                    var mTS3 = listAW[loopCount];
                    var mts4 = listAX[loopCount];
                    var mTechNum = listBA[loopCount];
                    var mTech = listBC[loopCount];
                    var mStatus = listBD[loopCount];
                    var mProduct = listBJ[loopCount];
                    mWarranty = listBL[loopCount];
                    var mTheNewClaimNum = listBQ[loopCount];
                    var mIsWarr = listBL[loopCount];
                    var mEmail = listBP[loopCount];

                    if (listBT[loopCount] != "NONE")
                    {
                        mEmail += ", " + listBT[loopCount];
                    }
                    var mEstimate = listBU[loopCount];
                    var mRush = listBX[loopCount];

                    if (mTheNewClaimNum.Length >= 7)   // Convert new claim# to Remove the "A" prefix
                    {
                        var tt = mTheNewClaimNum;
                        var yy = mTheNewClaimNum.Length;
                        yy--;
                        var uu = tt.Substring(1, yy);
                        mTheNewClaimNum = uu;
                    }

                    switch (mBrand.Length)
                    {
                        case 3:
                            mBrand += "                 ";
                            break;
                        case 4:
                            mBrand += "                ";
                            break;
                        case 5:
                            mBrand += "               ";
                            break;
                        case 6:
                            mBrand += "              ";
                            break;
                        case 7:
                            mBrand += "             ";
                            break;
                        case 8:
                            mBrand += "            ";
                            break;
                        case 9:
                            mBrand += "           ";
                            break;
                        case 10:
                            mBrand += "          ";
                            break;
                        case 11:
                            mBrand += "         ";
                            break;
                        case 12:
                            mBrand += "        ";
                            break;
                        case 13:
                            mBrand += "       ";
                            break;
                        case 14:
                            mBrand += "      ";
                            break;
                        case 15:
                            mBrand += "     ";
                            break;
                        case 16:
                            mBrand += "    ";
                            break;
                        case 17:
                            mBrand += "   ";
                            break;
                        case 18:
                            mBrand += "  ";
                            break;
                        case 19:
                            mBrand += " ";
                            break;

                    }

                    {
                        if (claim_no.Length >= 3)
                        {
                            if (claim_no == mClaim_NO)
                            {
                                if (mStatus == "ASSIGNED")
                                {
                                    label1.Text = mClaim_NO;
                                    label2.Text = mProduct;
                                    mmProduct = mProduct;
                                    mmDate_IN = mDate_IN;
                                    label3.Text = mBrand;
                                    label49.Text = claim_no;
                                    label47.Text = mBrand;
                                    label46.Text = mModel;
                                    label17.Text = mSerial;
                                    mmFname = mFname;
                                    mmLname = mLname;
                                    label44.Text = mProblem;
                                    label43.Text = mFthr_exp1;
                                    label42.Text = mFthr_exp2;
                                    label45.Text = mProblem;
                                    label45.Text = mFname + " " + mLname; 
                                    label7.Text = mFthr_exp1;
                                    label8.Text = mFthr_exp2;
                                    label12.Text = mHphone;
                                    label20.Text = mDate_IN;
                                    label22.Text = DateTime.Today.ToString("MM/dd/yyyy");
                                    TodayString = label22.Text;
                                    label24.Text = mStatus;
                                    mmWar_Note = mWar_Note;
                                    mmWar_Note = mWar_Note;
                                    label26.Text = mmWar_Note;
                                    if (mbi.Contains("RECALL"))
                                    {
                                        mmRecall = "Y";
                                        label32.Text = mmRecall;
                                    }
                                    else
                                    {
                                        mmRecall = "N";
                                        label32.Text = mmRecall;
                                    }
                                    if (mLname.Contains("RUSH"))
                                    {
                                        mmRush = "Y";
                                        label34.Text = mRush;
                                    }
                                    else
                                    {
                                        mmRush = "N";
                                        label34.Text = mRush;
                                    }
                                    if (mWarranty.Contains("NON"))
                                    {
                                        mmESTYN = "Y";
                                        label30.Text = mmESTYN;
                                    }
                                    else
                                    {
                                        mmESTYN = "N";
                                        label30.Text = mmESTYN;
                                    }
                                    mmShelf = mbe;
                                    label36.Text = mmShelf;
                                    mBrand = mBrand.Trim();
                                }

                            }
                        }
                    }
                    if (IsChecked == true)
                    {
                        if (mStatus == "ASSIGNED")
                        {
                            listBox1.Items.Add(mClaim_NO + "\t" + mProduct + "\t" + mBrand + "\t" + mModel);
                            UnassignedCount++;
                            label50.Text = "Unassigned: " + UnassignedCount.ToString();
                        }

                    }
                    loopCount++;
                }
                reader.Close(); // Close the open file
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 1520: Please try again\nDatabase in use");
            }
            
            CheckIfMoreClaims();
        }
    }
}
