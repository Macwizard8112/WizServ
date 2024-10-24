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
    public partial class EditExstingPO : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        private static readonly string Company = @"I:\\Datafile\\Control\\VP10\\Company.CSV";  // This is Read only CSV
        private int loopCount, foundcount2;
        public string claimno;
        public string SelectedText;
        public string mVNo, mVSN, mVCo, mVContact, mVTitle, mVAddr1, mVAddr2, mVCity, mVState, mVZip, mVCountry, mVPhone, mVFax, mVEmail, mVBuyer, mVPmnt;
        private int keyCount;
        public string ans;

        public EditExstingPO()
        {
            InitializeComponent();
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
            Icon = image100;
            button3.Visible = false;
            label15.Visible = true;
            HideLabels();
            PartsOrdered();
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (textBox1.TextLength == 0)
            {
                MessageBox.Show("Vendor shortname can't be empty.");
                textBox1.Select();
                textBox1.Focus();
                textBox1.SelectionStart = textBox1.Text.Length;
            }
            if (textBox1.TextLength > 0)
            {
                textBox2.Select();
                textBox2.DeselectAll();
                textBox2.Focus();
                textBox2.SelectionStart = textBox2.Text.Length;
            }
            if (e.KeyCode == Keys.Enter)
            {
                textBox2.Select();
                textBox2.DeselectAll();
                textBox2.Focus();
                textBox2.SelectionStart = textBox2.Text.Length;
            }
            if (e.KeyCode == Keys.Tab)
            {
                textBox2.Select();
                textBox2.DeselectAll();
                textBox2.Focus();
                textBox2.SelectionStart = textBox2.Text.Length;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (textBox1.TextLength == 0)
                {
                    MessageBox.Show("Vendor shortname can't be empty.");
                    textBox1.Select();
                    textBox1.Focus();
                    textBox1.SelectionStart = textBox1.Text.Length;
                }
            }
            if (e.KeyChar == (char)Keys.Tab)
            {
                if (textBox1.TextLength == 0)
                {
                    MessageBox.Show("Vendor shortname can't be empty.");
                    textBox1.Select();
                    textBox1.Focus();
                    textBox1.SelectionStart = textBox1.Text.Length;
                }
            }
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (textBox1.TextLength > 0)
                {
                    textBox2.Select();
                    textBox2.DeselectAll();
                    textBox2.Focus();
                    textBox2.SelectionStart = textBox2.Text.Length;
                }
            }
            if (e.KeyChar == (char)Keys.Tab)
            {
                if (textBox1.TextLength > 0)
                {
                    textBox2.Select();
                    textBox2.DeselectAll();
                    textBox2.Focus();
                    textBox2.SelectionStart = textBox2.Text.Length;
                }
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (textBox2.TextLength == 0)
            {
                MessageBox.Show("Vendor shortname can't be empty.");
                textBox2.Select();
                textBox2.DeselectAll();
                textBox2.Focus();
                textBox2.SelectionStart = textBox2.Text.Length;
            }
            if (textBox2.TextLength > 0)
            {
                textBox3.Select();
                textBox3.DeselectAll();
                textBox3.Focus();
                textBox3.SelectionStart = textBox3.Text.Length;
            }
            if (e.KeyCode == Keys.Enter)
            {
                textBox3.Select();
                textBox3.DeselectAll();
                textBox3.Focus();
                textBox3.SelectionStart = textBox3.Text.Length;
            }
            if (e.KeyCode == Keys.Tab)
            {
                textBox3.Select();
                textBox3.DeselectAll();
                textBox3.Focus();
                textBox3.SelectionStart = textBox3.Text.Length;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (textBox2.TextLength == 0)
                {
                    MessageBox.Show("Vendor Name can't be empty.");
                    textBox2.Select();
                    textBox2.DeselectAll();
                    textBox2.Focus();
                    textBox2.SelectionStart = textBox2.Text.Length;
                }
            }
            if (e.KeyChar == (char)Keys.Tab)
            {
                if (textBox2.TextLength == 0)
                {
                    MessageBox.Show("Vendor Name can't be empty.");
                    textBox2.Select();
                    textBox2.DeselectAll();
                    textBox2.Focus();
                    textBox2.SelectionStart = textBox2.Text.Length;
                }
            }
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (textBox2.TextLength > 0)
                {
                    textBox3.Select();
                    textBox3.DeselectAll();
                    textBox3.Focus();
                    textBox3.SelectionStart = textBox3.Text.Length;
                }
            }
            if (e.KeyChar == (char)Keys.Tab)
            {
                if (textBox2.TextLength > 0)
                {
                    textBox3.Select();
                    textBox3.DeselectAll();
                    textBox3.Focus();
                    textBox3.SelectionStart = textBox3.Text.Length;
                }
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (textBox3.TextLength == 0)
                {
                    MessageBox.Show("Contact name can't be empty.");
                    textBox3.Select();
                    textBox3.DeselectAll();
                    textBox3.Focus();
                    textBox3.SelectionStart = textBox3.Text.Length;
                }
            }
            if (e.KeyChar == (char)Keys.Tab)
            {
                if (textBox3.TextLength == 0)
                {
                    MessageBox.Show("Contact name can't be empty.");
                    textBox3.Select();
                    textBox3.DeselectAll();
                    textBox3.Focus();
                    textBox3.SelectionStart = textBox3.Text.Length;
                }
            }
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (textBox3.TextLength > 0)
                {
                    textBox4.Select();
                    textBox4.DeselectAll();
                    textBox4.Focus();
                    textBox4.SelectionStart = textBox4.Text.Length;
                }
            }
            if (e.KeyChar == (char)Keys.Tab)
            {
                if (textBox2.TextLength > 0)
                {
                    textBox4.Select();
                    textBox4.DeselectAll();
                    textBox4.Focus();
                    textBox4.SelectionStart = textBox4.Text.Length;
                }
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (textBox3.TextLength == 0)
            {
                MessageBox.Show("Contact name can't be empty.");
                textBox3.Select();
            }
            if (textBox3.TextLength > 0)
            {
                textBox4.Select();
                textBox4.DeselectAll();
                textBox4.Focus();
                textBox4.SelectionStart = textBox4.Text.Length;
            }
            if (e.KeyCode == Keys.Enter)
            {
                textBox4.Select();
                textBox4.DeselectAll();
                textBox4.Focus();
                textBox4.SelectionStart = textBox4.Text.Length;
            }
            if (e.KeyCode == Keys.Tab)
            {
                textBox4.Select();
                textBox4.DeselectAll();
                textBox4.Focus();
                textBox4.SelectionStart = textBox4.Text.Length;
            }
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (textBox4.TextLength == 0)
            {
                MessageBox.Show("Title can't be empty.");
                textBox4.Select();
                textBox4.DeselectAll();
                textBox4.Focus();
                textBox4.SelectionStart = textBox4.Text.Length;
            }
            if (textBox4.TextLength > 0)
            {
                textBox5.Select();
                textBox5.DeselectAll();
                textBox5.Focus();
                textBox5.SelectionStart = textBox5.Text.Length;
            }
            if (e.KeyCode == Keys.Enter)
            {
                textBox5.Select();
                textBox5.DeselectAll();
                textBox5.Focus();
                textBox5.SelectionStart = textBox5.Text.Length;
            }
            if (e.KeyCode == Keys.Tab)
            {
                textBox5.Select();
                textBox5.DeselectAll();
                textBox5.Focus();
                textBox5.SelectionStart = textBox5.Text.Length;
            }
        }


        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (textBox4.TextLength == 0)
                {
                    MessageBox.Show("Title can't be empty.");
                    textBox4.Select();
                    textBox4.DeselectAll();
                    textBox4.Focus();
                    textBox4.SelectionStart = textBox4.Text.Length;
                }
            }
            if (e.KeyChar == (char)Keys.Tab)
            {
                if (textBox4.TextLength == 0)
                {
                    MessageBox.Show("Title can't be empty.");
                    textBox4.Select();
                    textBox4.DeselectAll();
                    textBox4.Focus();
                    textBox4.SelectionStart = textBox4.Text.Length;
                }
            }
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (textBox4.TextLength > 0)
                {
                    textBox5.Select();
                    textBox5.DeselectAll();
                    textBox5.Focus();
                    textBox5.SelectionStart = textBox5.Text.Length;
                }
            }
            if (e.KeyChar == (char)Keys.Tab)
            {
                if (textBox4.TextLength > 0)
                {
                    textBox5.Select();
                    textBox5.DeselectAll();
                    textBox5.Focus();
                    textBox5.SelectionStart = textBox5.Text.Length;
                }
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            keyCount = textBox5.TextLength;
            keyCount++;
            label20.Text = keyCount.ToString();
            label21.Text = "Characters left: " + (69 - keyCount).ToString();
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (textBox5.TextLength == 0)
                {
                    MessageBox.Show("Address 1 can't be empty.");
                    textBox5.Select();
                    textBox5.DeselectAll();
                    textBox5.Focus();
                    textBox5.SelectionStart = textBox5.Text.Length;
                }
            }
            if (e.KeyChar == (char)Keys.Tab)
            {
                if (textBox5.TextLength == 0)
                {
                    MessageBox.Show("Address 1 can't be empty.");
                    textBox5.Select();
                    textBox5.DeselectAll();
                    textBox5.Focus();
                    textBox5.SelectionStart = textBox5.Text.Length;
                }
            }
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (textBox5.TextLength > 0)
                {
                    textBox6.Select();
                    textBox6.DeselectAll();
                    textBox6.Focus();
                    textBox6.SelectionStart = textBox6.Text.Length;
                }
            }

            if (keyCount >= 69)
            {
                textBox6.Select();
                textBox6.DeselectAll();
                textBox6.Focus();
                textBox6.SelectionStart = textBox6.Text.Length;
                keyCount = 0;
            }    
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            keyCount = textBox6.TextLength;
            keyCount++;
            label20.Text = keyCount.ToString();
            label22.Text = "Characters left: " + (69 - keyCount).ToString();
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (textBox6.TextLength == 0)
                {
                    MessageBox.Show("Address 2 can't be empty.");
                    textBox6.Select();
                    textBox6.DeselectAll();
                    textBox6.Focus();
                    textBox6.SelectionStart = textBox6.Text.Length;
                }
            }
            if (e.KeyChar == (char)Keys.Tab)
            {
                if (textBox6.TextLength == 0)
                {
                    MessageBox.Show("Address 2 can't be empty.");
                    textBox6.Select();
                    textBox6.DeselectAll();
                    textBox6.Focus();
                    textBox6.SelectionStart = textBox6.Text.Length;
                }
            }
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (textBox6.TextLength > 0)
                {
                    textBox7.Select();
                    textBox7.DeselectAll();
                    textBox7.Focus();
                    textBox7.SelectionStart = textBox7.Text.Length;
                }
            }

            if (keyCount >= 69)
            {
                textBox7.Select();
                textBox7.DeselectAll();
                textBox7.Focus();
                textBox7.SelectionStart = textBox7.Text.Length;
                keyCount = 0;
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)Keys.Enter)
            {
                if (textBox7.TextLength == 0)
                {
                    MessageBox.Show("City can't be empty.");
                    textBox7.Select();
                    textBox7.DeselectAll();
                    textBox7.Focus();
                    textBox7.SelectionStart = textBox7.Text.Length;
                }
            }
            if (e.KeyChar == (char)Keys.Tab)
            {
                if (textBox7.TextLength == 0)
                {
                    MessageBox.Show("City can't be empty.");
                    textBox7.Select();
                    textBox7.DeselectAll();
                    textBox7.Focus();
                    textBox7.SelectionStart = textBox7.Text.Length;
                }
            }
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (textBox7.TextLength > 0)
                {
                    textBox8.Select();
                    textBox8.DeselectAll();
                    textBox8.Focus();
                    textBox8.SelectionStart = textBox8.Text.Length;
                }
            }
            if (e.KeyChar == (char)Keys.Tab)
            {
                if (textBox7.TextLength > 0)
                {
                    textBox8.Select();
                    textBox8.DeselectAll();
                    textBox8.Focus();
                    textBox8.SelectionStart = textBox8.Text.Length;
                }
            }
        }

        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if (textBox7.TextLength == 0)
            {
                MessageBox.Show("Title can't be empty.");
                textBox7.Select();
                textBox7.DeselectAll();
                textBox7.Focus();
                textBox7.SelectionStart = textBox7.Text.Length;
            }
            if (textBox4.TextLength > 0)
            {
                textBox8.Select();
                textBox8.DeselectAll();
                textBox8.Focus();
                textBox8.SelectionStart = textBox8.Text.Length;
            }
            if (e.KeyCode == Keys.Enter)
            {
                textBox8.Select();
                textBox8.DeselectAll();
                textBox8.Focus();
                textBox8.SelectionStart = textBox8.Text.Length;
            }
            if (e.KeyCode == Keys.Tab)
            {
                textBox8.Select();
                textBox8.DeselectAll();
                textBox8.Focus();
                textBox8.SelectionStart = textBox8.Text.Length;
            }
        }

        private void textBox8_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (textBox8.TextLength == 0)
                {
                    MessageBox.Show("City can't be empty.");
                    textBox8.Select();
                    textBox8.DeselectAll();
                    textBox8.Focus();
                    textBox8.SelectionStart = textBox8.Text.Length;
                }
            }
            if (e.KeyChar == (char)Keys.Tab)
            {
                if (textBox8.TextLength == 0)
                {
                    MessageBox.Show("City can't be empty.");
                    textBox8.Select();
                    textBox8.DeselectAll();
                    textBox8.Focus();
                    textBox8.SelectionStart = textBox8.Text.Length;
                }
            }
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (textBox8.TextLength > 0)
                {
                    textBox9.Select();
                    textBox9.DeselectAll();
                    textBox9.Focus();
                    textBox9.SelectionStart = textBox9.Text.Length;
                }
            }
            if (e.KeyChar == (char)Keys.Tab)
            {
                if (textBox8.TextLength > 0)
                {
                    textBox9.Select();
                    textBox9.DeselectAll();
                    textBox9.Focus();
                    textBox9.SelectionStart = textBox9.Text.Length;
                }
            }
        }

        private void textBox9_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox10_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox11_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox12_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void textBox12_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox13_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                label24.Visible = true;
                var pass = 0;
                string s = textBox13.Text;

                string[] subs = s.Split(' ');

                foreach (var sub in subs)
                {
                    pass++;
                    //label23.Text = label23.Text + ($"{sub} ");
                    if (pass == 1)
                    {
                        ans = sub;
                    }
                }
                label24.Text = ans.ToString();
            }

        }

        private void textBox13_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox14_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void textBox14_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox15_KeyDown(object sender, KeyEventArgs e)
        {

        }
        private void textBox15_KeyPress(object sender, KeyPressEventArgs e)
        {

        }


        private void button2_Click(object sender, EventArgs e)  // Main Menu
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show(); 
        }

        private void button1_Click(object sender, EventArgs e)  // Back to Claims Management Menu
        {
            Hide();
            ClaimsMGTMenu f2 = new ClaimsMGTMenu();
            f2.Show();
        }

        private void button3_Click(object sender, EventArgs e)  // Save Data
        {

        }

        private void HideLabels()                               // Hide all textboxes & labels
        {
            foreach (var lbl in Controls.OfType<Label>())
                lbl.Hide();
            foreach (var textbox in Controls.OfType<TextBox>())
                textbox.Hide();
            label15.Visible = true;
            label17.Visible = true;
            label1.Visible = true;
            label2.Visible = true;
        }

        private void ShowLabels()                               // Show all textboxes & labels
        {
            foreach (var lbl in Controls.OfType<Label>())
                lbl.Show();
            foreach (var textbox in Controls.OfType<TextBox>())
                textbox.Show();
        }

        private void richTextBox1_DoubleClick(object sender, EventArgs e)
        {

            SelectedText = richTextBox1.SelectedText;
            claimno = SelectedText.Trim();
            richTextBox1.Visible = false;
            ShowLabels();
            PullData();
            label1.Visible = true;
            label2.Visible = false;
            label17.Visible = false;
            label18.Visible = true;
            button3.Visible = true;
            label15.Visible = false;
            pictureBox1.Visible = false;
            label2.Visible = false;
            label17.Visible = false;
            label21.Visible = true;
            label22.Visible = true;
            label16.Text = "Vendor Number: " + mVNo;
            textBox1.Text = mVSN;
            textBox2.Text = mVCo;
            textBox3.Text = mVContact;
            textBox4.Text = mVTitle;
            textBox5.Text = mVAddr1;
            textBox6.Text = mVAddr2;
            textBox7.Text = mVCity;
            textBox8.Text = mVState;
            textBox9.Text = mVZip;
            textBox10.Text = mVCountry;
            textBox11.Text = mVPhone;
            textBox12.Text = mVEmail;
            textBox13.Text = mVBuyer;
            textBox14.Text = mVPmnt;
            textBox15.Text = mVFax;
            textBox1.Select();
            textBox1.Focus();
            textBox1.SelectionStart = textBox1.Text.Length;
        }

        private void PullData()
        {
            try
            {
                StreamReader reader = new StreamReader(Company);
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

                    listA.Add(values[0]);   // Vendor Number ( 1 thru 1050)
                    listB.Add(values[1]);   // Vendor Short Name
                    listC.Add(values[2]);   // Vendor Company
                    listD.Add(values[3]);   // Contact
                    listE.Add(values[4]);   // Title
                    listF.Add(values[5]);   // Address1
                    listG.Add(values[6]);   // Address2
                    listH.Add(values[7]);   // City
                    listI.Add(values[8]);   // State
                    listJ.Add(values[9]);   // Zip
                    listK.Add(values[10]);  // Country
                    listL.Add(values[11]);  // Phone
                    listM.Add(values[12]);  // Fax Number
                    listN.Add(values[13]);  // Email Address
                    listO.Add(values[14]);  // Buyer Initials
                    listP.Add(values[15]);  // Payment Terms

                    if (listA[loopCount] == SelectedText)
                    {
                        mVNo = listA[loopCount];
                        mVSN = listB[loopCount];
                        mVCo = listC[loopCount];
                        mVContact = listD[loopCount];
                        mVTitle = listE[loopCount];
                        mVAddr1 = listF[loopCount];
                        mVAddr2 = listG[loopCount];
                        mVCity = listH[loopCount];
                        mVState = listI[loopCount];
                        mVZip = listJ[loopCount];
                        mVCountry = listK[loopCount];
                        mVPhone = listL[loopCount];
                        mVFax = listM[loopCount];
                        mVEmail = listN[loopCount];
                        mVBuyer = listO[loopCount];
                        mVPmnt = listP[loopCount];
                    }
                    loopCount++;
                }
                reader.Close(); // Close the open file
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 820: Sorry an error has occured: " + ex.Message);
            }
        }

        private void PartsOrdered()
        {
            try
            {
                StreamReader reader = new StreamReader(Company);
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

                    listA.Add(values[0]);   // Vendor Number ( 1 thru 1050)
                    listB.Add(values[1]);   // Vendor Short Name
                    listC.Add(values[2]);   // Vendor Company
                    listD.Add(values[3]);   // Contact
                    listE.Add(values[4]);   // Title
                    listF.Add(values[5]);   // Address1
                    listG.Add(values[6]);   // Address2
                    listH.Add(values[7]);   // City
                    listI.Add(values[8]);   // State
                    listJ.Add(values[9]);   // Zip
                    listK.Add(values[10]);  // Country
                    listL.Add(values[11]);  // Phone
                    listM.Add(values[12]);  // Fax Number
                    listN.Add(values[13]);  // Email Address
                    listO.Add(values[14]);  // Buyer Initials
                    listP.Add(values[15]);  // Payment Terms

                    var name = listB[loopCount];
                    name += "\t";
                    switch (name.Length)
                    {
                        case 1:
                            name += "\t\t";
                            break;
                        case 2:
                            name += "\t";
                            break;
                        case 3:
                            name += "      ";
                            break;
                        case 4:
                            name += "      ";
                            break;
                        case 5:
                            name += "      ";
                            break;
                        case 6:
                            break;
                        case 7:
                            break;
                        case 8:
                            break;
                        case 9:
                            break;
                    }

                    if (listA[loopCount] != SelectedText)
                    {
                        richTextBox1.Text = richTextBox1.Text + listA[loopCount] + "\t" + name + "\t" + listC[loopCount] + "\n";
                    }
                    else
                    {
                        mVNo = listA[loopCount];
                        mVSN = listB[loopCount];
                        mVCo = listC[loopCount];
                        mVContact = listD[loopCount];
                        mVTitle = listE[loopCount];
                        mVAddr1 = listF[loopCount];
                        mVAddr2 = listG[loopCount];
                        mVCity = listH[loopCount];
                        mVState = listI[loopCount];
                        mVZip = listJ[loopCount];
                        mVCountry = listK[loopCount];
                        mVPhone = listL[loopCount];
                        mVFax = listM[loopCount];
                        mVEmail = listN[loopCount];
                        mVBuyer = listO[loopCount];
                        mVPmnt = listP[loopCount];
                    }
                    loopCount++;
                }
                reader.Close(); // Close the open file
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 930: Sorry an error has occured: " + ex.Message);
            }
        }
    }
}
