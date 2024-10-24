using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WizServ
{
    public partial class NameLookupChars : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        private readonly string Datbase = @"I:\\Datafile\\Control\\Database.csv";
        private readonly string Blacklist = @"I:\\Datafile\\Control\\Blacklist.csv";
        private string claim_no, fname, lname, addr, city, state, zip, hphone, wphone;
        private bool war_prd, found;
        private DateTime datein;
        private int loopCount, loop;
        public string ADDRESS, newLine, theNameis;
        public bool BlockCust, IsBlocked;
        private string[] csvLines;

        public NameLookupChars()
        {
            InitializeComponent();
            textBox1.TextChanged += new EventHandler(TextBoxSearch_TextChanged);
            IsBlocked = false;
            Icon = image100;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
            claim_no = Version.Claim;
            //label4.Text = "Double-Click on Claim # to select that claim.";
            Text = "Retrieve Claim by Last Name";
        }
        private void TextBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBox1.Text.ToLower();
            listBoxResults.Items.Clear();
            

            foreach (var line in csvLines)
            {
                var fields = line.Split(',');
                if (fields[4].ToLower().Contains(searchText))
                {
                    string field3Padded = fields[3].PadRight(21); // Pad field 3 to be between 1 and 21 characters long
                    string field4Padded = fields[4].PadRight(21);
                    string field12Padded = fields[12].PadRight(20);
                    string displayText = $"{fields[1]}\t{fields[2]}\t{field3Padded}\t{field4Padded}\t{field12Padded}\t{fields[14]}";
                    listBoxResults.Items.Add(displayText);
                }
            }
        }


        private void textBox1_Click(object sender, EventArgs e)
        {
            label1.Text = "Enter first 3 characters of last name";
            richTextBox1.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            Warranty f2 = new Warranty();
            f2.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var SelectedText = "000000";
            Version.Claim = SelectedText.Trim();
            claim_no = SelectedText.Trim();
            Version.Claim = claim_no;
            Version.From = claim_no;
            Hide();
            NewClaim f2 = new NewClaim();
            f2.Show();
        }

        private void NotFound()
        {
            var SelectedText = "000000";
            Version.Claim = SelectedText.Trim();
            claim_no = SelectedText.Trim();
            Version.Claim = claim_no;
            Version.From = claim_no;
            Hide();
            NewClaim f2 = new NewClaim();
            f2.Show();
        }

        private void richTextBox1_DoubleClick(object sender, EventArgs e)
        {
            var SelectedText = richTextBox1.SelectedText;
            if (SelectedText == "200475")
            {
                BlockCust = true;
            }
            else
            {
                BlockCust = false;
                IsBlocked = false;
            }
            /*
            if (IsBlocked = true)
            {
                MessageBox.Show("This customer is Blacklisted.\nDo NOT take any equipment in\nfrom him.");
                button1.Enabled = false;
                button1.Visible = false;
            }
            */
            if (BlockCust == false)
            {
                Version.Claim = SelectedText.Trim();
                claim_no = SelectedText.Trim();
                Version.Claim = claim_no;
                Version.From = claim_no;
                Hide();
                NewClaim f2 = new NewClaim();
                f2.Show();
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                theNameis = textBox1.Text.ToUpper();
                label1.Text = "Searching for: " + theNameis;
                GetData();
                if (found != true)
                {
                    NotFound();
                }
            }
        }

        public void GetBlacklist()                // Populate Product List pulldown
        {
            try
            {
                StreamReader reader = new StreamReader(Blacklist, Encoding.GetEncoding("Windows-1252"));
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

                    listA.Add(values[0]);       //  First Name
                    listB.Add(values[1]);       //  Last Name
                    listC.Add(values[2]);       //  Address
                    listD.Add(values[2]);       //  City
                    listE.Add(values[2]);       //  State
                    listF.Add(values[2]);       //  Zip
                    listG.Add(values[2]);       //  Phone
                    listH.Add(values[2]);       //  Email


                    loop++;
                    loopCount++;
                }
                reader.Close(); // Close the open file
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 141: Sorry an error has occured: " + ex.Message);
            }
        }

        public void GetData()
        {
            try
            {
                StreamReader reader = new StreamReader(Datbase, Encoding.GetEncoding("Windows-1252"));
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

                loopCount = 0;
                var com = ",";

                var csv = new StringBuilder();                  // Get ready to save a new CSV file

                while (!reader.EndOfStream)
                {
                    var lineRead = reader.ReadLine();
                    var values = lineRead.Split(',');

                    listA.Add(values[0]);       //  False
                    listB.Add(values[1]);       //  Claim Number
                    listC.Add(values[2]);       //  Enter Date
                    listD.Add(values[3]);       //  First Name
                    listE.Add(values[4]);       //  Last Name
                    listF.Add(values[5]);       //  State
                    listG.Add(values[6]);       //  Zip
                    listH.Add(values[7]);       //  Home Phone
                    listI.Add(values[8]);       //  Work Phone #

                    var first = listA[loopCount];
                    var claimnumber = listB[loopCount];
                    var recddate = listC[loopCount];
                    var firstname = listD[loopCount].Trim();
                    var lastname = listE[loopCount].Trim();
                    var address = listF[loopCount].Trim();
                    var city = listG[loopCount];
                    var state = listH[loopCount];
                    var zipcode = listI[loopCount];

                    if (lastname.Contains(theNameis.ToUpper()))
                    {
                        richTextBox1.Text = richTextBox1.Text + claimnumber + "\t" + lastname + ",\t" + firstname + "\t" + address + " " + city + " " + state + " " + zipcode + "\n";
                        found = true;
                    }
                    if (firstname.Contains(theNameis.ToUpper())) 
                    {
                        richTextBox1.Text = richTextBox1.Text + claimnumber + "\t" + lastname + ",\t" + firstname + "\t" + address + " " + city + " " + state + " " + zipcode + "\n";
                        found = true;
                    }
                    loop++;
                    loopCount++;
                }
                label1.Text = label1.Text + " Found: " + loopCount.ToString();
                reader.Close();                                         // Close the open file
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 119: Sorry an error has occured: " + ex.Message);
            }
        }
    
}
}
