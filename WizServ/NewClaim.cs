using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Media;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using WizServ.Properties;
using WizServ.Resources;

namespace WizServ
{
    public partial class NewClaim : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        public string claim_no;
        public string mString = "Missing required data\nPlease recheck all fields.";
        public string path = @"I:\\Control\\NextClaim.CSV";
        private readonly string EquipWorth = @"I:\\Datafile\\Control\\EquipWorth.CSV";
        private readonly string database = @"I:\\Datafile\\Control\\Database.CSV";
        private readonly string Dealers = @"I:\\Datafile\\Control\\Dealers.CSV";
        private readonly string Product = @"I:\\Datafile\\Control\\Product.CSV";
        private readonly string file4 = @"I:\\Datafile\\Control\\DNR\\DoNotRepair.csv";
        private readonly string Brand_DNR = @"I:\\Datafile\\Control\\DNR\\Brand_DNR.csv";
        private readonly string file6 = @"I:\\Datafile\\Control\\DNR\\Master_DNR_List.csv";
        private readonly string file7 = @"I:\\Datafile\\COntrol\\Email_Suffix.csv";
        private readonly string Related = @"I:\\Datafile\\Control\\Related.CSV";
        private readonly string BlackList = @"I:\\Datafile\\Control\\BlackList.CSV";
        static readonly string key = @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\lanmanserver\parameters";
        public readonly string computerDescription = (string)Registry.GetValue(key, "srvcomment", null);
        public static string ma, mb, mc, md, me, mf, mg, mh, mi, mj, mk, ml, mm, mn, mo, mp, mq, mr, ms, mt, mu, mv, mw, mx, my, mz;
        public static string maa, mab, mac, mad, mae, maf, mag, mah, mai, maj, mak, mal, mam, man, mao, map, maq, mar, mas, mat, mau, mav, maw, max, may, maz;
        public static string mba, mbb, mbc, mbd, mbe, mbf, mbg, mbh, mbi, mbj, mbk, mbl, mbm, mbn, mbo, mbp, mbq, mbr, mbs, mbt;
        public static string mbu, mbv, mbw, mbx, TheTechis, mused;
        public string TheFileIs;
        public string who, ssn;
        public string fname, lname, addr, city, state, zip, hphone, wphone, from;
        public static string NextClaimNum;
        private bool war_prd, ready, isTrue;
        public static bool but2press;
        private DateTime datein;
        private int loopCount, loop;
        public string mSelected, yy, zz, nextClaimNo;
        public int nextClaimNumber;
        public string strTextBox;
        public TextBox textBox26;
        public string warranty = Version.Warranty;
        public bool iswarr = Version.IsWarr;
        public string email_p1, email_p2, po;
        public string make = Version.Make;
        public string model = Version.Model;
        public string serial = Version.Serial;
        public string c = ",";
        public string dealer_number, theEmailaddr;
        public string temp, temp2;
        private readonly string msg = "You must install new AC Board Assembly 534-WP200015-02 - Service Bulletin 7";
        public string answer, seconddigit, thirddigit, fourthdigit, fifthdigit, Month, Year, Week;
        public bool TF;
        private bool IsMsg, IsMsg1;


        public NewClaim()
        {
            InitializeComponent();
            label52.Visible = false;
            comboBox4.Text = Version.Make;
            comboBox7.Text = Version.Model;
            textBox13.Text = Version.Serial;
            HidePage2();
            ShowPage1();
            label31.Visible = true;
            label33.Visible = true;
            Icon = image100;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
            email_p1 = Version.Email_P1;
            email_p2 = Version.Email_P2;
            textBox10.Text = email_p1;
            comboBox5.Text = email_p2;
            textBox17.Text = email_p1 + email_p2;
            claim_no = Version.Claim;
            from = Version.Claim;
            label25.Text = "Claim #: " + claim_no;
            Text = "New Claim # " + claim_no;
            textBox14.Text = "00/00/0000";
            textBox15.Text = DateTime.Now.ToShortDateString();
            if (but2press == false)
            {
                GenerateNewClaimNum();                              // Generate a new claim number
                NextClaimNum = nextClaimNumber.ToString();
            }

            GetBrand();
            LoadComBx();
            LoadComBx6();
            GetDealers();
            comboBox1.SelectedIndex = 1;
            if (from == "A00000")
            {
                if (Version.Warranty == "No")
                {
                    textBox1.Select();
                }
                else
                {
                    comboBox4.Select();
                }
            }
            else
            {
                comboBox4.Select();
            }
            GetProduct();
            //ReadNextClaimNum();
            GetData();
            ReadDontRepair();
            EmailSuffix();
            //label30.Text = "Warranty: " + Version.Warranty + ", " + Version.IsWarr.ToString();
            warranty = Version.Warranty;
            
            iswarr = Version.IsWarr;
            comboBox4.Text = Version.Make;
            comboBox7.Text = Version.Model;
            if (comboBox7.Text.Length <= 0)
            {
                comboBox7.Text = Version.Model2;
            }
            textBox13.Text = Version.Serial;
            textBox1.Select();
        }

        private void LoadComBx()
        {
            comboBox3.Items.Add("Yes");
            comboBox3.Items.Add("No");
            comboBox3.SelectedIndex = 0;
        }

        private void LoadComBx6()
        {
            comboBox6.Items.Add("No");
            comboBox6.Items.Add("Yes");
            comboBox6.SelectedIndex = 0;
        }

        public void PlaySimpleSound()
        {
            //SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.ChurchBell);
            SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.Alarm10);
            simpleSound.Play();
        }

        private void restoreClaimifCancelled()                      // subtract 1 so cancelled claims are not skipped.
        {
            string path = @"I:\\Datafile\\Control\\NextClaim.CSV";

            using (var reader = new StreamReader(path))
            {
                List<string> listA = new List<string>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    listA.Add(values[0]);
                    nextClaimNo = listA[0];
                }
            }
            nextClaimNumber = Int32.Parse(nextClaimNo);
            nextClaimNumber--;                              
                try
                {
                    using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(fs))
                        {
                            sw.WriteLine(nextClaimNumber.ToString() + Environment.NewLine);
                        }
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Error occured: Line 189: \n" + ex);
                }
        }

        public void Button2_Click(object sender, EventArgs e)       // Cancelled pressed
        {
            CheckFileClosedStatus();                                // Close Open files
            restoreClaimifCancelled();                              // Adjust Next Claim # for we don't skip any claim #'s
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }

        private void ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            //textBox10.Text = comboBox4.SelectedItem.ToString();
            if (comboBox4.Text.Length > 0)
            //if (comboBox4.SelectedItem.ToString() != null)
            {
                po = comboBox4.SelectedItem.ToString();         // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                if (po.Contains("BEHRINGER"))                   //  >>>>> DON'T TAKE IN ANY BEHRINGER <<<<<
                {                                               // <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    MessageBox.Show("We no longer take in ANY Behringer equipment.");
                    Button2_Click(null, null);
                }
            }
           
            Version.Model = comboBox4.SelectedText.ToString();
            switch (po)
            {
                case "BARCUS BERRY":
                    string message1 = "We do not take Warranty repairs.";
                    string title1 = "BARCUS BERRY waring:";
                    MessageBox.Show(message1, title1);
                    break;
                case "BEHRINGER/BUGERA":
                    string message2 = "Behringer\nDo NOT take any WARRANTY products !\nBugera\nANY model - No Support Available\nBoth - Limited parts available!";
                    string title2 = "BEHRINGER/BUGERA Warning:";
                    MessageBox.Show(message2, title2);
                    break;
                case "BENJAMIN ADAMS":
                    string message3 = "We do not take ANY repairs.";
                    string title3 = "BENJAMIN ADAMS waring:";
                    MessageBox.Show(message3, title3);
                    break;
                case "BOSE":
                    string message4 = "We do not take ANY repairs.";
                    string title4 = "BOSE waring:";
                    MessageBox.Show(message4, title4);
                    break;
                case "EV - BOSCH":
                    string message5 = "EVOLVE-50 Factory Service ONLY.";
                    string title5 = "ELECTRO-VOICE BOSCH waring:";
                    MessageBox.Show(message5, title5);
                    break;

            }
            comboBox2.Select();
            comboBox7.Select();
        }

        private void comboBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadModels();
            }
        }

        private void comboBox4_MouseLeave(object sender, EventArgs e)
        {
            LoadModels();
        }

        private void comboBox4_SelectedValueChanged(object sender, EventArgs e)
        {
            LoadModels();
        }

        private void LoadModels()
        {
            string model_info = @"I:\\Datafile\\Control\\Model_Info.csv";
            try
            {
                StreamReader reader = new StreamReader(model_info, Encoding.GetEncoding("Windows-1252"));
                String line = reader.ReadLine();

                List<string> listA = new List<string>();
                List<string> listB = new List<string>();

                loopCount = 0;
                while (!reader.EndOfStream)
                {
                    var lineRead = reader.ReadLine();
                    var values = lineRead.Split(',');

                    listA.Add(values[0]);       //  Make
                    listB.Add(values[1]);

                    if (listA[loopCount] == comboBox4.Text)
                    {
                        comboBox7.Items.Add(listB[loopCount]);
                    }

                    loop++;
                    loopCount++;
                }
                reader.Close();                 // Close the open reader file
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 300: Sorry an error has occured: " + ex.Message);
            }
        }
            

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                temp2 = textBox1.Text.ToUpper();
                textBox2.Select();
            }
            if (e.KeyData == Keys.Tab)
            {
                textBox2.Select();
            }
        }

        private void TextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                temp = textBox2.Text.ToUpper();
                textBox3.Select();
            }
            if (e.KeyData == Keys.Tab)
            {
                textBox3.Select();
            }
        }

        private void TextBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (textBox3.Text.ToUpper().Contains("114 REDFORD") && temp == "OLIVER" && temp2 == "JEROME")
                {
                    MessageBox.Show("Jerome Oliver is Blacklisted.\nDo NOT take any equipment in from him.");
                }
                textBox16.Select();
            }
            if (e.KeyData == Keys.Tab)
            {
                textBox16.Select();
            }
        }

        private void TextBox16_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                textBox4.Select();
            }
            if (e.KeyData == Keys.Tab)
            {
                textBox4.Select();
            }
        }

        private void TextBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                textBox5.Select();
            }
            if (e.KeyData == Keys.Tab)
            {
                textBox5.Select();
            }
        }

        private void TextBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                textBox7.Select();
            }
            if (e.KeyData == Keys.Tab)
            {
                textBox7.Select();
            }
        }

        private void TextBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                textBox6.Select();
            }
            if (e.KeyData == Keys.Tab)
            {
                textBox6.Select();
            }
        }

        private void TextBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                textBox8.Select();
            }
            if (e.KeyData == Keys.Tab)
            {
                textBox8.Select();
            }
        }

        private void TextBox8_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                textBox9.Select();
            }
            if (e.KeyData == Keys.Tab)
            {
                textBox9.Select();
            }
        }

        private void TextBox9_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                textBox10.Select();
            }
            if (e.KeyData == Keys.Tab)
            {
                textBox10.Select();
            }
        }

        private void TextBox10_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                comboBox4.Select();
            }
            if (e.KeyData == Keys.Tab)
            {
                comboBox4.Select();
            }
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox7.Select();
        }

        private void TextBox13_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                comboBox1.Select();
                if (comboBox4.Text == "QSC")
                {
                    if (comboBox7.Text == "K12.2")
                    { 
                        label52.Text = msg;
                        DisassembleSN();
                    }
                }
            }
            if (e.KeyData == Keys.Tab)
            {
                comboBox1.Select();
            }
        }

        private void DisassembleSN()
        {
            answer = textBox13.Text.ToUpper();
            ClearVars();
            seconddigit = answer.Substring(1, 1);
            TF = seconddigit.All(Char.IsLetter);
            thirddigit = answer.Substring(2, 1);
            fourthdigit = answer.Substring(3, 1);
            fifthdigit = answer.Substring(4, 1);
            if (TF == true)
            {
                Format1();
                Format2();
                SetupMsg();
            }
            else
            {
                Format3();
                Format4();
                SetupMsg();
            }
        }

        private void ClearVars()
        {
            seconddigit = "";
            thirddigit = "";
            fourthdigit = "";
            fifthdigit = "";
        }

        private void SetupMsg()
        {
            if (seconddigit == "A" && IsMsg == true)
            {
                label52.Visible = true;
                label52.Text = " " + msg + " ";
            }
            if (seconddigit == "B" && IsMsg == true)
            {
                label52.Visible = true;
                label52.Text = " " + msg + " ";
            }
            if (seconddigit == "C" && IsMsg == true)
            {
                label52.Visible = true;
                label52.Text = " " + msg + " ";
            }
            if (seconddigit == "D" && IsMsg == true)
            {
                label52.Visible = true;
                label52.Text = " " + msg + " ";
            }
            if (seconddigit == "E" && IsMsg == true)
            {
                label52.Visible = true;
                label52.Text = " " + msg + " ";
            }
            if (seconddigit == "F" && IsMsg == true)
            {
                label52.Visible = true;
                label52.Text = " " + msg + " ";
            }

            var d = seconddigit.All(Char.IsLetter);
            if (d == false)
            {
                var t = seconddigit + thirddigit;
                int result = Int32.Parse(t);
                if (result <= 30 && IsMsg == true)
                {
                    label4.Text = " " + msg + " ";
                }
                if (IsMsg == true)
                {
                    label4.Text = " " + msg + " ";
                }
            }

        }

        private void Format1()
        {
            switch (seconddigit)
            {
                case "A":
                    Month = "January ";
                    break;
                case "B":
                    Month = "February ";
                    break;
                case "C":
                    Month = "March ";
                    break;
                case "D":
                    Month = "April ";
                    break;
                case "E":
                    Month = "May ";
                    break;
                case "F":
                    Month = "June ";
                    break;
                case "G":
                    Month = "July ";
                    break;
                case "H":
                    Month = "August ";
                    break;
                case "I":
                    Month = "September ";
                    break;
                case "J":
                    Month = "October ";
                    break;
                case "K":
                    Month = "November ";
                    break;
                case "L":
                    Month = "December ";
                    break;
            }
        }

        private void Format2()
        {
            switch (thirddigit)
            {
                case "G":
                    Year = "2016 ";
                    IsMsg = true;
                    break;
                case "H":
                    Year = "2017 ";
                    IsMsg = true;
                    break;
                case "I":
                    Year = "2018 ";
                    IsMsg = true;
                    break;
                case "J":
                    Year = "2019 ";
                    IsMsg = true;
                    break;
                case "K":
                    Year = "2020 ";
                    IsMsg = true;
                    break;
                case "L":
                    Year = "2021 ";
                    IsMsg = true;
                    break;
                case "M":
                    Year = "2022 ";
                    IsMsg = true;
                    break;
                case "N":
                    Year = "2023 ";
                    IsMsg = false;
                    break;
                case "O":
                    Year = "2024 ";
                    IsMsg = false;
                    break;
                case "P":
                    Year = "2025 ";
                    IsMsg = false;
                    break;
                case "Q":
                    Year = "2026 ";
                    IsMsg = false;
                    break;
                case "R":
                    Year = "2027 ";
                    IsMsg = false;
                    break;
                case "S":
                    Year = "2028 ";
                    IsMsg = false;
                    break;
                case "T":
                    Year = "2029 ";
                    IsMsg = false;
                    break;
                case "U":
                    Year = "2030 ";
                    IsMsg = false;
                    break;
                case "V":
                    Year = "2031 ";
                    IsMsg = false;
                    break;
                case "W":
                    Year = "2032 ";
                    IsMsg = false;
                    break;
                case "X":
                    Year = "2033 ";
                    IsMsg = false;
                    break;
                case "Y":
                    Year = "2034 ";
                    IsMsg = false;
                    break;
                case "Z":
                    Year = "2035 ";
                    IsMsg = false;
                    break;
            }
            label2.Text = "Manufactured Month / Year: " + Month + Year;
        }

        private void Format3()
        {
            Week = seconddigit + thirddigit;
            label2.Text = "Manufactured Week " + Week;
        }

        private void Format4()
        {
            var t = fourthdigit + fifthdigit;
            switch (t)
            {
                case "16":
                    Year = " of 2016";
                    IsMsg = true;
                    break;
                case "17":
                    Year = " of 2017";
                    IsMsg = true;
                    break;
                case "18":
                    Year = " of 2018";
                    IsMsg = true;
                    break;
                case "19":
                    Year = " of 2019";
                    IsMsg = true;
                    break;
                case "20":
                    Year = " of 2020";
                    IsMsg = true;
                    break;
                case "21":
                    Year = " of 2021";
                    IsMsg = true;
                    break;
                case "22":
                    Year = " of 2022";
                    IsMsg = true;
                    break;
                case "23":
                    Year = " of 2023";
                    IsMsg = false;
                    break;
                case "24":
                    Year = " of 2024";
                    IsMsg = false;
                    break;
                case "25":
                    Year = " of 2025";
                    IsMsg = false;
                    break;
                case "26":
                    Year = " of 2026";
                    IsMsg = false;
                    break;
                case "27":
                    Year = " of 2027";
                    IsMsg = false;
                    break;
                case "28":
                    Year = " of 2028";
                    IsMsg = false;
                    break;
                case "29":
                    Year = " of 2029";
                    IsMsg = false;
                    break;
                case "30":
                    Year = " of 2030";
                    IsMsg = false;
                    break;
                case "31":
                    Year = " of 2031";
                    IsMsg = false;
                    break;
                case "32":
                    Year = " of 2032";
                    IsMsg = false;
                    break;
                case "33":
                    Year = " of 2033";
                    IsMsg = false;
                    break;
                case "34":
                    Year = " of 2034";
                    IsMsg = false;
                    break;
                case "35":
                    Year = " of 2035";
                    IsMsg = false;
                    break;
            }
            label2.Text = "Manufactured Week " + Week + Year;
        }

        private void TextBox14_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                textBox19.Select();
            }
            if (e.KeyData == Keys.Tab)
            {
                textBox19.Select();
            }
        }

        private void TextBox19_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                textBox20.Select();
            }
            if (e.KeyData == Keys.Tab)
            {
                textBox20.Select();
            }
        }

        private void TextBox20_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                textBox21.Select();
            }
            if (e.KeyData == Keys.Tab)
            {
                textBox21.Select();
            }
        }

        private void TextBox21_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                textBox22.Select();
            }
            if (e.KeyData == Keys.Tab)
            {
                textBox22.Select();
            }
        }

        private void TextBox22_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                textBox23.Select();
            }
            if (e.KeyData == Keys.Tab)
            {
                textBox23.Select();
            }
        }

        private void TextBox23_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                textBox11.Select();
            }
            if (e.KeyData == Keys.Tab)
            {
                textBox11.Select();
            }
        }

        private void TextBox11_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                textBox24.Select();
            }
            if (e.KeyData == Keys.Tab)
            {
                textBox24.Select();
            }
        }

        private void TextBox24_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                textBox25.Select();
            }
            if (e.KeyData == Keys.Tab)
            {
                textBox25.Select();
            }
        }

        private void TextBox25_KeyDown(object sender, KeyEventArgs e)
        {
            button1.Select();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex == 0)
            {
                label32.Text = "No estimate requested";
            }
            if (comboBox3.SelectedIndex == 1)
            {
                label32.Text = "Estimate Requested";
            }
        }

        private void comboBox7_TextChanged(object sender, EventArgs e)
        {
            Version.Model = comboBox7.Text;
            Version.Model2 = comboBox7.Text;
            if (comboBox7.Text == "LSR4328P")
            {
                MessageBox.Show("OBSOLETE !\nDO NOT TAKE IN");
            }
            if (comboBox7.Text == "LSR2328P")
            {
                MessageBox.Show("OBSOLETE !\nDO NOT TAKE IN");
            }
            if (comboBox7.Text == "K18.2")
            {
                MessageBox.Show("Have tech check Service Bulletins!");
            }
            if (comboBox7.Text == "K10.2")
            {
                MessageBox.Show("Have tech check Service Bulletins!");
            }
            if (comboBox7.Text == "K12.2")
            {
                MessageBox.Show("Have tech check Service Bulletins!");
            }
            CheckEquipWorth();
        }

        private void CheckEquipWorth()
        {
            loopCount = 0;
            using (var reader = new StreamReader(EquipWorth))
            {
                List<string> Brand = new List<string>();
                List<string> Model = new List<string>();
                List<string> Used = new List<string>();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    Brand.Add(values[0]);
                    Model.Add(values[1]);
                    Used.Add(values[2]);

                    var make = Brand[loopCount];
                                           
                    if (comboBox4.Text.Contains(Brand[loopCount]))
                    {
                        if (comboBox7.Text == Model[loopCount])
                        {
                            textBox12.Text = Used[loopCount];
                            var s = Decimal.Parse(textBox18.Text);
                            var d = decimal.Parse(textBox12.Text);
                            var f = 340m;                               // 2 hours Labor ($120/Hour) + 100 in parts
                            if (d < f)
                            {
                                MessageBox.Show("Might not be cost effective to fix !\nCollect $65 diagnostic.\nLookup used price on internet.");
                            }
                            if (s < f)
                            {
                                if (s > 0)
                                {
                                    MessageBox.Show("Might not be cost effective to fix !\nCollect $65 diagnostic.\nLookup used price on internet.");
                                }
                            }
                            if (s > d)
                            {
                                MessageBox.Show("Customer wants to spend more than it's worth !\nWorth $ " + textBox12.Text + "\nTheir Max repair price is: $" + textBox18.Text + "\nCollect $65 diagnostic.");


                            }
                            isTrue = true;
                            if (Model[loopCount] == comboBox7.Text)
                            {
                                label53.Text = " Worth $" + Used[loopCount] + " ";
                            }
                            else
                            {
                                label53.Visible = false;
                            }
                        }
                        else
                        {
                            if (isTrue != true)
                            {
                                textBox12.Text = "0.00";
                            }
                        }
                    }
                    loopCount++;
                }
            }
        }

                                                                    // **************************************************
        private void GenerateNewClaimNum()                          // DOUBLE-CHECK THIS NUMBER BEFORE USING - GO LIVE !
        {                                                           // **************************************************
            string path = @"I:\\Datafile\\Control\\NextClaim.CSV";

            using (var reader = new StreamReader(path))
            {
                List<string> listA = new List<string>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    listA.Add(values[0]);
                    nextClaimNo = listA[0];
                }
            }
            nextClaimNumber = Int32.Parse(nextClaimNo);
            nextClaimNumber++;
            label25.Text = "New Claim # " + nextClaimNumber.ToString();
            try
                    {
                        using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                        {
                            using (StreamWriter sw = new StreamWriter(fs))
                            {
                                sw.WriteLine(nextClaimNumber.ToString() + Environment.NewLine);
                            }
                        }
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show("Error occured: Line 1016: \n" + ex);
                    }
        }

        public void StoreDataToVars()
        {
            ma = "REDRUM";
            mb = nextClaimNumber.ToString();
            mc = textBox15.Text;
            md = textBox1.Text;
            me = textBox2.Text;
            mf = textBox3.Text;
            mg = textBox4.Text;
            mh = textBox5.Text;
            mi = textBox6.Text;
            mj = textBox7.Text;
            mk = textBox8.Text;
            ml = ".";
            mm = comboBox4.Text;
            mn = ".";
            mo = comboBox7.Text;
            mp = textBox13.Text;
            mq = "0.00";
            mr = "0.00";
            ms = "0.00";
            mt = "0.00";
            mu = "0.00";
            mv = DateTime.Now.ToShortDateString();
            mw = "FALSE";
            mx = "0.00";
            my = "0.00";
            mz = "0.00";
            maa = "0.00";
            mab = "0.00";
            mused = textBox12.Text;
            mac = "NO";         // remember to check if this is a warranty claim & change to YES !
            mad = textBox14.Text;
            mae = ".";
            maf = ".";
            mag = ".";
            mah = ".";          // remember to update when delivery status changes on page 2 (CUSTOMER PICKUP, SHIP TO CUSTOMER)
            mai = comboBox1.Text;
            if (mai == "Add a new Dealer")
            {
                mai = "NONE";
            }

            if (mai.Length == 0)
            {
                mai = ".";
            }
            maj = textBox19.Text.ToUpper();
            if (maj.Length == 0)
            {
                maj = ".";
            }
            mak = textBox20.Text.ToUpper();
            if (mak.Length == 0)
            {
                mak = ".";
            }
            mal = textBox21.Text;
            if (mal.Length == 0)
            {
                mal = "GA";
            }
            mam = textBox22.Text;
            if (mam.Length == 0)
            {
                mam = "00000";
            }
            man = textBox23.Text;
            if (man.Length == 0)
            {
                man = "000-000-0000";
            }
            mao = "0";
            map = "0";
            maq = "0";
            mar = "0";
            mas = "0";
            mat = "NON-WARRANTY";             // Remember to change based on if Warr or Non-Warr
            mau = Version.msg;      // Tech_serv1 note
            mav = ".";              // Tech_serv2 note
            maw = ".";              // Tech_serv3 note
            max = ".";              // Tech_serv4 note
            may = ".";              // tech ID
            maz = ".";              // Tech - Cole, David, etc.
            mba = ".";              // Tech_no - Tech Number (1,2,3,4,5, etc)
            mbb = "00/00/0000";     // Dte_compl - Date Claim Completed
            mbc = "00/00/0000";     // Dte_closed - Date Claim Closed
            mbd = "ASSIGNED";       // Status - ASSIGNED for new claims till given to Tech
            mbe = textBox25.Text;   // comment - Shelf Location
            mbf = textBox24.Text;   // dela_no - dealer claim / inventory number
            mbg = "N-";             // narda - Claim Priority P-, PS, etc
            mbh = ".";              // dist_name
            mbi = ".";              // dist_code - FREIGHT, RECALL, etc
            mbj = comboBox2.Text;   // Product Catagory, Mixer, Powered-Speaker, DDJ, etc.
            mbk = ".";              // auth_code - Technician Name (Cole, David, etc)
            mbl = "NON-WARRANTY";   // refb_code - Warranty or Non-Warranty                     Change based on which screen picked
            mbm = textBox15.Text;   // microwave - date in shop second time
            if (comboBox3.Text == "Yes")
            {
                mbn = "ESTIMATE";   // ESTIMATE - ESTIMATE or NONE
            }
            if (comboBox3.Text != "Yes")
            {
                mbn = "NONE";       // ESTIMATE - ESTIMATE or NONE
            }
            mbo = dealer_number;    // Dealer_Num - pull from combobox1
            if (mbo.Length == 0)
            { 
                mbo = "999";
            }
            mbp = textBox17.Text;
            var kl = DateTime.Now.ToShortDateString();
            var kl1 = kl.Length;
            var kl2 = kl.Substring((kl1-2), 1);
            mbq = "A" + kl2 + nextClaimNumber.ToString();   // New claim # is "A" + 21 (last 2 of year) + claim #
            mbr = textBox16.Text;
            var kj1 = kl.Substring((kl1 - 4), 4);   // year
            var kj2 = kl.Substring((kl1 - 7), 2);   // Month
            var kj3 = kl.Substring(0, 2);         // day
            var KJ4 = nextClaimNumber.ToString();
            var kj5 = KJ4.Length;
            var kj6 = KJ4.Substring(kj5 - 4, 4);
            var kj4 = "A" + kj1 + kj3 + kj2 + kj6;
            mbs = kj4;          // Real Claim # (YYYYMMDDcccc) Year, Month, Day, last 4 of claim #
            mbt = textBox17.Text;
            if (comboBox3.Text == "Yes")
            {
                mbu = "Y";   // ESTIMATE - ESTIMATE or NONE
            }
            if (comboBox3.Text != "Yes")
            {
                mbu = "N";       // ESTIMATE - ESTIMATE or NONE
            }
            mbv = "0.00";
            mbw = "0.00";
            mbx = "N";

            WriteNewDatatoCSV();
        }

        private void WriteNewDatatoCSV()
        {
            // c = "," comma
            // x = Environment.Newline (Carradge return)

            var x = Environment.NewLine;
            try
            {
                using (FileStream fs = new FileStream(database, FileMode.Append, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs))      // Save new data to Databse.csv
                    {
                        sw.WriteLine(ma + c + mb + c + mc + c + md + c + me + c + mf + c + mg + c + mh + c + mi + c + mj
                            + c + mk + c + ml + c + mm + c + mn + c + mo + c + mp + c + mq + c + mr + c + ms + c + mt
                            + c + mu + c + mv + c + mw + c + mx + c + my + c + mz + c + maa + c + mab + c + mac + c + mad
                            + c + mae + c + maf + c + mag + c + mah + c + mai + c + maj + c + mak + c + mal + c + mam
                            + c + man + c + mao + c + map + c + maq + c + mar + c + mas + c + mat + c + mau + c + mav
                            + c + maw + c + max + c + may + c + maz + c + mba + c + mbb + c + mbc + c + mbd + c + mbe
                            + c + mbf + c + mbg + c + mbh + c + mbi + c + mbj + c + mbk + c + mbl + c + mbm + c + mbn
                            + c + mbo + c + mbp + c + mbq + c + mbr + c + mbs + c + mbt + c + mbu + c + mbv + c + mbw 
                            + c + mbx + c + mused);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured: Line 1186\n" + ex);
            } 
        }

        private void SHowPG2()
        {
            label29.Visible = true;
            label31.Visible = true;
            label32.Visible = true;
            label33.Visible = true;
            label34.Visible = true;
            label35.Visible = true;
            label36.Visible = true;
            label37.Visible = true;
            label38.Visible = true;
            label39.Visible = true;
            label40.Visible = true;
            label41.Visible = true;
            label42.Visible = true;
            label43.Visible = true;
            label44.Visible = true;
            label45.Visible = true;
            label46.Visible = true;
            label47.Visible = true;
            label36.Text = md;
            label37.Text = me;
            label38.Text = mf;
            label39.Text = mg;
            label40.Text = mh;
            label41.Text = mi;
            label42.Text = mj;
        }

        private void Button1_Click(object sender, EventArgs e)      // Create next Claim #
        {
            Text = "New Claim # Page 2" + claim_no;
            but2press = true;
            Version.Make = comboBox4.Text;
            Version.Model = comboBox7.Text;
            Version.Model2 = comboBox7.Text;
            Version.Serial = textBox13.Text;
            Version.msg = msg;
            CheckUserEnteredData(); // Verify data entered is valid
            ZeroOutData();          // clear out old data
            StoreDataToVars();      // Save new data to Databse
            SHowPG2();              // Show Page 2
            if (textBox14.Text.Length <= 0)
            {
                textBox14.Text = DateTime.Now.ToShortDateString();
            }
            if (ready == true)
            {
                //Hide();
                //NewClaimsPg2 f2 = new NewClaimsPg2();
                //f2.Show();
            }
            else
            {
                MessageBox.Show(mString);
                return;
            }
        }

        private void HidePage2()
        {
            for (int i = 29; i <= 42; i++)
            {
                foreach (var lbl in Controls.OfType<Label>())
                    lbl.Hide();
            }
        }

        private void ShowPage1()
        {
            for (int i = 0; i <= 28; i++)
            {
                foreach (var lbl in Controls.OfType<Label>())
                    lbl.Show();
            }
            label29.Visible = false;
            label31.Visible = false;
            label32.Visible = false;
            label33.Visible = false;
            label34.Visible = false;
            label35.Visible = false;
            label36.Visible = false;
            label37.Visible = false;
            label38.Visible = false;
            label39.Visible = false;
            label40.Visible = false;
            label41.Visible = false;
            label42.Visible = false;
            label43.Visible = false;
            label44.Visible = false;
            label45.Visible = false;
            label46.Visible = false;
            label47.Visible = false;
        }

        public void ZeroOutData()                               // Hide everything on screen, prep for 2nd page
        {
            for (int i = 0; i <= 28; i++)
            {
                foreach (var lbl in Controls.OfType<Label>())
                lbl.Hide(); 
            }
            foreach (var tb in this.Controls.OfType<TextBox>()) 
                tb.Visible = false;
            comboBox1.Visible = false;
            comboBox2.Visible = false;
            comboBox3.Visible = false;
            comboBox4.Visible = false;
            comboBox5.Visible = false;
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox17.Text = textBox10.Text + comboBox5.SelectedItem.ToString();
            Version.Email_P1 = textBox10.Text;
            Version.Email_P2 = comboBox5.SelectedItem.ToString();
        }

        private void comboBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                yy = comboBox5.Text;
                textBox17.Text = textBox10.Text + yy;
                Version.Email_P1 = textBox10.Text;
                Version.Email_P2 = yy;
            }
        }

        public void ReadDontRepair()
        {
            var column1 = new List<string>();
            var column2 = new List<string>();
            using (var rd = new StreamReader(file4))
            {
                while (!rd.EndOfStream)
                {
                    var splits = rd.ReadLine().Split(',');
                    column1.Add(splits[0]);

                }
            }
            // print column1
            Console.WriteLine("Column 1:");
            foreach (var element in column1)
                Console.WriteLine(element);

            if (Version.Warranty == null)
            {
                Version.Warranty = "No";
                string message = "Is this a WARRANTY claim?";
                string caption = "Error Detected in Input";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;
                result = MessageBox.Show(message, caption, buttons);
                if (result == DialogResult.Yes)
                {
                    // Closes the parent form.
                    Version.Warranty = "Y";
                    warranty = "Yes";
                        label18.ForeColor = Color.Black;
                        label14.ForeColor = Color.Black;
                        label19.ForeColor = Color.Black;
                        label20.ForeColor = Color.Black;
                        label21.ForeColor = Color.Black;
                        label23.ForeColor = Color.Black;
                        label27.ForeColor = Color.Black;
                        label17.ForeColor = Color.Black;
                        textBox14.Enabled = true;
                        label17.Visible = true;
                        label17.Enabled = true;
                        textBox11.Visible = true;
                        textBox11.Enabled = true;
                        textBox19.Enabled = true;
                        textBox20.Enabled = true;
                        textBox21.Enabled = true;
                        textBox22.Enabled = true;
                        textBox23.Enabled = true;
                        comboBox1.Enabled = true;
                    }
                else
                {
                    Version.Warranty = "N";
                    warranty = "N";
                }
                textBox1.Select();
                
            }
            if (Version.Warranty.Contains("N"))
            {
                textBox1.Select();
            }
            else
            {
                comboBox4.Select();
            }
        }

        private void CheckUserEnteredData()
        { 

            if (comboBox7.Text.Length <= 0)
            {
                ready = false;
                label10.ForeColor = Color.Black;
                label10.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Regular);
                label11.ForeColor = Color.Black;
                label11.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Regular);
                mString = "Missing Model field\nPlease check and enter Manufacturer Model.";
                label12.ForeColor = Color.Red;
                label12.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
                comboBox7.Select();
                return;
            }
            else
            {

                ready = true;
            }

            if (textBox13.Text.Length <= 0)
            {
                ready = false;
                label10.ForeColor = Color.Black;
                label10.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Regular);
                label11.ForeColor = Color.Black;
                label11.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Regular);
                label12.ForeColor = Color.Black;
                label12.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Regular);
                mString = "Missing Serial field\nPlease check and enter Manufacturer Serial.\nUse N/A if unreadable.";
                label13.ForeColor = Color.Red;
                label13.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
                textBox13.Select();
                return;
            }

            var yy = textBox14.Text;
            int length = textBox14.Text.Length;
            string yy2 = yy.Substring(1, length - 1);
            string zz = textBox14.Text;
            if (yy2 == "0/00/0000")
            {
                label14.ForeColor = Color.Red;
                label14.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
                textBox14.Select();
                return;
            }

            if (comboBox1.SelectedItem.ToString() == "Add a new Dealer")
            {
                label18.Text = "Client/Dealer Name:";
                label18.ForeColor = Color.Red;
                label18.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
                comboBox1.Select();
                return;
            }

            if (textBox13.Text.Length >= 1)
            {
                ready = true;
                return;
            }
        }

        public void CheckFileOpenStatus()
        {
            String path = @"I:\\Datafile\\Control\\FileLocking.csv";
            List<String> lines = new List<String>();

            if (File.Exists(path)) 
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    String line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Contains(","))
                        {
                            String[] split = line.Split(',');

                            if (split[0].Contains("Brand_DNR"))
                            {
                                split[1] = "Open";
                                line = String.Join(",", split);
                                TheFileIs = "Open";
                            }
                            if (split[0].Contains("Database"))
                            {
                                split[1] = "Open";
                                line = String.Join(",", split);
                                TheFileIs = "Open";
                            }
                            if (split[0].Contains("Dealers"))
                            {
                                split[1] = "Open";
                                line = String.Join(",", split);
                                TheFileIs = "Open";
                            }
                            if (split[0].Contains("Product"))
                            {
                                split[1] = "Open";
                                line = String.Join(",", split);
                                TheFileIs = "Open";
                            }
                            if (split[0].Contains("NextClaim"))
                            {
                                split[1] = "Open";
                                line = String.Join(",", split);
                                TheFileIs = "Open";
                            }
                            if (split[0].Contains("Related"))
                            {
                                split[1] = "Open";
                                line = String.Join(",", split);
                                TheFileIs = "Open";
                            }
                            if (split[0].Contains("Dealers_Number"))
                            {
                                split[1] = "Open";
                                line = String.Join(",", split);
                                TheFileIs = "Open";
                            }
                            if (split[0].Contains("Estimates"))
                            {
                                split[1] = "Open";
                                line = String.Join(",", split);
                                TheFileIs = "Open";
                            }
                            if (split[0].Contains("Gold"))
                            {
                                split[1] = "Open";
                                line = String.Join(",", split);
                                TheFileIs = "Open";
                            }
                        }
                        lines.Add(line);
                    }
                }

                using (StreamWriter writer = new StreamWriter(path, false))
                {
                    foreach (String line in lines)
                        writer.WriteLine(line);
                }
            }
        }

        public void CheckFileClosedStatus()
        {
            String path = @"I:\\Datafile\\Control\\FileLocking.csv";
            List<String> lines = new List<String>();

            if (File.Exists(path))
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    String line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Contains(","))
                        {
                            String[] split = line.Split(',');

                            if (split[0].Contains("Brand_DNR"))
                            {
                                split[1] = "Closed";
                                line = String.Join(",", split);
                                TheFileIs = "Closed";
                            }
                            if (split[0].Contains("Database"))
                            {
                                split[1] = "Closed";
                                line = String.Join(",", split);
                                TheFileIs = "Closed";
                            }
                            if (split[0].Contains("Dealers"))
                            {
                                split[1] = "Closed";
                                line = String.Join(",", split);
                                TheFileIs = "Closed";
                            }
                            if (split[0].Contains("Product"))
                            {
                                split[1] = "Closed";
                                line = String.Join(",", split);
                                TheFileIs = "Closed";
                            }
                            if (split[0].Contains("NextClaim"))
                            {
                                split[1] = "Closed";
                                line = String.Join(",", split);
                                TheFileIs = "Closed";
                            }
                            if (split[0].Contains("Related"))
                            {
                                split[1] = "Closed";
                                line = String.Join(",", split);
                                TheFileIs = "Closed";
                            }
                            if (split[0].Contains("Dealers_Number"))
                            {
                                split[1] = "Closed";
                                line = String.Join(",", split);
                                TheFileIs = "Closed";
                            }
                            if (split[0].Contains("Estimates"))
                            {
                                split[1] = "Closed";
                                line = String.Join(",", split);
                                TheFileIs = "Closed";
                            }
                            if (split[0].Contains("Gold"))
                            {
                                split[1] = "Closed";
                                line = String.Join(",", split);
                                TheFileIs = "Closed";
                            }
                        }

                        lines.Add(line);
                    }
                }

                using (StreamWriter writer = new StreamWriter(path, false))
                {
                    foreach (String line in lines)
                        writer.WriteLine(line);
                }
            }
        }


        public void GetBrand()                // Populate Product Lit pulldown
        {
            CheckFileOpenStatus();          // Check if file is Open or CLosed
            //if (TheFileIs == "OPEN")
            {
                try
                {
                    StreamReader reader = new StreamReader(Brand_DNR, Encoding.GetEncoding("Windows-1252"));
                    String line = reader.ReadLine();

                    List<string> listA = new List<string>();

                    loopCount = 0;
                    while (!reader.EndOfStream)
                    {
                        var lineRead = reader.ReadLine();
                        var values = lineRead.Split(',');

                        listA.Add(values[0]);       //  war_prd


                        comboBox4.Items.Add(listA[loopCount]);

                        loop++;
                        loopCount++;
                    }
                    reader.Close();                 // Close the open reader file
                    CheckFileClosedStatus();        // Close after using file
                    comboBox4.SelectedIndex = 1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error 1656: Sorry an error has occured: " + ex.Message);
                }
            }
            if (TheFileIs != "Open")
            {
                //MessageBox.Show("Sorry File is already Open\nRetry in a few seconds.");
                CheckFileClosedStatus();
                return;
            }
            
        }

        public void GetProduct()                // Populate Product List pulldown
        {
            try
            {
                StreamReader reader = new StreamReader(Product, Encoding.GetEncoding("Windows-1252"));
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

                    listA.Add(values[0]);       //  war_prd
                    listB.Add(values[1]);       //  claim_no

                    comboBox2.Items.Add(listB[loopCount]);
                    loop++;
                    loopCount++;
                }
                reader.Close(); // Close the open file
                CheckFileClosedStatus();
                comboBox2.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 1708: Sorry an error has occured: " + ex.Message);
            }
        }

        public void EmailSuffix()                // Populate Email Suffix
        {
            try
            {
                StreamReader reader = new StreamReader(file7, Encoding.GetEncoding("Windows-1252"));
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

                    listA.Add(values[0]);       // Email Suffix

                    comboBox5.Items.Add(listA[loopCount]);
                    loop++;
                    loopCount++;
                }
                reader.Close(); // Close the open file
                CheckFileClosedStatus();
                comboBox2.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 1751: Sorry an error has occured: " + ex.Message);
            }
        }

        public void GetDealers()        // Populate ComboBox 1
        {
            try
            {
                StreamReader reader = new StreamReader(Dealers, Encoding.GetEncoding("Windows-1252"));
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
                    listP.Add(values[15]);      // Dealer Number

                    comboBox1.Items.Add(listB[loopCount]);
                    loop++;

                }
                reader.Close(); // Close the open file
                Version.Warranty = "No";
                string message = "Is this a WARRANTY claim?";
                string caption = "Error Detected in Input";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;
                result = MessageBox.Show(message, caption, buttons);
                if (result == DialogResult.Yes)
                {
                    warranty = "Yes";
                    label18.ForeColor = Color.Black;
                    label14.ForeColor = Color.Black;
                    label19.ForeColor = Color.Black;
                    label20.ForeColor = Color.Black;
                    label21.ForeColor = Color.Black;
                    label23.ForeColor = Color.Black;
                    label27.ForeColor = Color.Black;
                    label17.ForeColor = Color.Black;
                    textBox14.Enabled = true;
                    label17.Visible = true;
                    label17.Enabled = true;
                    textBox11.Visible = true;
                    textBox11.Enabled = true;
                    textBox19.Enabled = true;
                    textBox20.Enabled = true;
                    textBox21.Enabled = true;
                    textBox22.Enabled = true;
                    textBox23.Enabled = true;
                    comboBox1.Enabled = true;
                    dealer_number = listP[loopCount];
                    loopCount++;
                }
                    else
                {
                    warranty = "No";
                    textBox11.Enabled = false; 
                    textBox14.Enabled = false;
                    textBox19.Enabled = false;
                    textBox20.Enabled = false;
                    textBox21.Enabled = false;
                    textBox22.Enabled = false;
                    textBox23.Enabled = false;
                    comboBox1.Enabled = false;
                    label18.Visible = false;
                    label14.Visible = false;
                    label17.Visible = false;
                    label19.Visible = false;
                    label20.Visible = false;
                    label21.Visible = false;
                    label23.Visible = false;
                    label27.Visible = false;
                    label27.Visible = false;
                    textBox11.Visible = false;
                    textBox14.Visible = false;
                    textBox19.Visible = false;
                    textBox20.Visible = false;
                    textBox21.Visible = false;
                    textBox22.Visible = false;
                    textBox23.Visible = false;
                    comboBox1.Visible = false;
                    dealer_number = listP[loopCount];
                    loopCount++;
                }

                reader.Close(); // Close the open file
                CheckFileClosedStatus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 1876: Sorry an error has occured: " + ex.Message);
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            mSelected = comboBox1.Text;
            //GetSelectedDealer();
            textBox14.Select();
        }

        public void GetSelectedDealer()         // Populate Client Dealer address info
        {
            try
            {
                StreamReader reader = new StreamReader(Dealers, Encoding.GetEncoding("Windows-1252"));
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

                    if (mSelected.Contains(listB[loopCount]))
                    {
                        textBox19.Text = listC[loopCount]; // Client Address
                        textBox20.Text = listD[loopCount]; // Client City
                        textBox21.Text = listE[loopCount]; // Client State
                        if (listF[loopCount].Length == 4)
                        {
                            textBox22.Text = "0" + listF[loopCount]; // Client Zip Code
                        }
                        else
                        {
                            textBox22.Text = listF[loopCount]; // Client Zip Code
                        }
                        textBox23.Text = listG[loopCount]; // Client Phone
                        
                    }
                    loop++;
                    loopCount++;
                }
                reader.Close(); // Close the open file
                CheckFileClosedStatus();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 1958: Sorry an error has occured: " + ex.Message);
            }
        }


        public void GetData()
        {
            try
            {
                StreamReader reader = new StreamReader(database, Encoding.GetEncoding("Windows-1252"));
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

                    if (listB[loopCount].Contains(claim_no))
                    {
                        textBox1.Text = listD[loopCount];
                        textBox2.Text = listE[loopCount];
                        textBox3.Text = listF[loopCount];
                        textBox4.Text = listG[loopCount];
                        textBox5.Text = listH[loopCount];
                        textBox6.Text = listI[loopCount];
                        textBox7.Text = listJ[loopCount];
                        textBox8.Text = listK[loopCount];
                        textBox9.Text = listBR[loopCount];
                        var d = listBP[loopCount];
                        var j = "";
                        try
                        {
                            for (int i = 0; i < d.Length; i++)
                            {
                                if (i == 0)
                                {

                                }
                                if (i > 0 && d.Substring(i - 1, i) != "@")
                                {
                                    j = d.Substring(0, i);
                                }
                                if (d.Substring(1 - 1, i) == "@")
                                {
                                    i = d.Length;
                                }
                                textBox10.Text = j;
                            }
                        }
                        catch (Exception)
                        {

                        }
                        try
                        {
                            theEmailaddr = d;
                            string[] words = theEmailaddr.Split('@');
                            comboBox5.Text = "@" + words[1];
                        }
                        catch (Exception)
                        {

                        }
                        textBox17.Text = listBP[loopCount];
                        textBox16.Text = listBR[loopCount];
                        //textBox10.Text = listM[loopCount];
                        //textBox11.Text = listBJ[loopCount];
                        //textBox12.Text = listO[loopCount];
                        //textBox13.Text = listP[loopCount];
                        //textBox14.Text = listAD[loopCount];  // Date Purchased
                        textBox15.Text = DateTime.Now.ToShortDateString();
                        //textBox15.Text = listC[loopCount];
                        //textBox16.Text = listBM[loopCount];
                        //textBox16.Text = "00/00/0000";
                        //textBox17.Text = listBC[loopCount];
                        //textBox18.Text = listAI[loopCount];
                        textBox24.Text = listBF[loopCount]; // Client Claim / INVentory #
                        textBox25.Text = "FC"; // Shelf Location

                        loop++;
                        if (claim_no == listB[loopCount])
                        {
                            Text = "New Claim # " + NextClaimNum +  "   Old Claim #: " + claim_no + ",  " + listBQ[loopCount];
                        }
                    }
                    loopCount++;
                }
                reader.Close(); // Close the open file
                CheckFileClosedStatus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 2197: Sorry an error has occured: " + ex.Message);
            }
        }

        public void GetDealerNames()
        {
            try
            {
                StreamReader reader = new StreamReader(database, Encoding.GetEncoding("Windows-1252"));
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

                    var TheCount = loopCount.ToString();
                    //comboBox1.Items.Add(listB[loopCount]);
                    loop++;
                    loopCount++;
                }
                reader.Close(); // Close the open file
                CheckFileClosedStatus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 2259: Sorry an error has occured: " + ex.Message);
            }
        }

    }
}
