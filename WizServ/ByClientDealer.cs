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
    public partial class ByClientDealer : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        private readonly string file = @"I:\\Datafile\\Control\Dealers.CSV";            // Dealers file
        //private readonly string file2 = @"I:\\Datafile\\Control\Dealers_Number.CSV";    // Next # in Dealers file
        private string claim_no;
        private int loopCount, loop;
        public string calledfrom;

        public ByClientDealer()
        {
            InitializeComponent(); 
            Icon = image100;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
            Text = "By Client Dealer";
            claim_no = Version.Claim;
            calledfrom = Version.From;
            button5.Visible = false;
            label17.Visible = false;
            label18.Visible = false;
            label19.Visible = false;
            pictureBox1.Visible = true;
            GetData();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            TextWriter txt = new StreamWriter("I:\\Datafile\\Doc\\Dealers.txt");
            txt.Write(richTextBox1.Text);
            txt.Close(); 
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            var fileToOpen = "I:\\Datafile\\Doc\\Dealers.txt";
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

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox1.TextLength >= 4)
                {
                    return;
                }
                Version.Claim = textBox1.Text.Trim();
                claim_no = textBox1.Text.Trim();
                switch (calledfrom)
                {
                    case "Retrieve":
                        Hide();
                        ByClientDealer2 f2 = new ByClientDealer2();
                        f2.Show();
                        break;
                    case "Custstatus":
                        Hide();
                        ByClientDealer3 f1 = new ByClientDealer3();
                        f1.Show();
                        break;
                }
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Hide();
            RetrieveMenu f2 = new RetrieveMenu();
            f2.Show();
        }

        private void RichTextBox1_MouseUp(object sender, MouseEventArgs e)
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
                if (ex.ToString().Contains("Value cannot be null."))
                {
                    // Ignore nothing selected
                }
                else
                {
                    MessageBox.Show("Sorry an exception has occured.\n" + ex);
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

        private void CreateNewDealer()                          // Create a new dealer, add to CSV file
        {
            Text = "Add a new dealer"; 
            button1.Visible = false;
            button4.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            richTextBox1.Visible = false;
            textBox1.Visible = false;
            button2.Enabled = false;
            button3.Enabled = true;
            button5.Visible = true;
            label17.Visible = true;
            label18.Visible = true;
            label19.Visible = true;
            pictureBox1.Visible = false; ;
            textBox2.Select();

            string path = file;

            string[] lines = File.ReadAllLines(path);
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                if (line.Contains(","))
                {
                    var split = line.Split(',');
                    if (split[1].Contains("Add a Dealer"))
                    {
                        split[1] = "100";
                        line = string.Join(",", split);
                    }
                }
            }
            File.WriteAllLines(@"C:\\CSV.txt", lines);
        }


        private void button5_Click(object sender, EventArgs e)      // Save new Dealer info
        {
            Text = "By Client Dealer";
        }

        private void RichTextBox1_DoubleClick(object sender, EventArgs e)
        {
            var SelectedText = richTextBox1.SelectedText;
            Version.Claim = SelectedText.Trim();
            if (SelectedText == "0")
            {
                CreateNewDealer();
                //GetData();
                return;
            }
            claim_no = SelectedText.Trim();
            if (SelectedText.Length >= 4)
            {
                return;
            }
            switch (calledfrom)
            {
                case "Retrieve":
                    Hide();
                    ByClientDealer2 f2 = new ByClientDealer2();
                    f2.Show();
                    break;
                case "Custstatus":
                    Hide();
                    ByClientDealer3 f1 = new ByClientDealer3();
                    f1.Show();
                    break;
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                if (textBox2.TextLength == 0)
                {
                    textBox2.Text = "N/A";
                }
                textBox3.Select();
                if (textBox2.TextLength == 0)
                {
                    textBox2.Text = "N/A";
                }
                if (e.KeyCode == Keys.Enter)
                {
                    if (textBox2.TextLength == 0)
                    {
                        textBox2.Text = "N/A";
                    }
                }
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (textBox2.TextLength == 0)
                {
                    textBox2.Text = "N/A";
                }
            }
            if (e.KeyChar == 13)
            {
                textBox3.Select();
            }
            if (e.KeyChar == (char)Keys.Enter)
            {
                textBox3.Select();
            }
            if (e.KeyChar == 13)
            {
                textBox3.Select();
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (textBox2.TextLength == 0)
            {
                textBox2.Text = "N/A";
            }
            if (e.KeyChar == (char)Keys.Enter)
            {
                textBox4.Select();
            }
            if (e.KeyChar == 13)
            {
                textBox4.Select();
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.PageUp)
            {
                textBox2.Select();
            }
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox3.TextLength == 0)
                {
                    MessageBox.Show("Dealer name can't be blank !");
                    textBox3.Select();
                }
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                textBox5.Select();
            }
            if (e.KeyChar == 13)
            {
                textBox5.Select();
            }
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.PageUp)
            {
                textBox3.Select();
            }
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox4.TextLength == 0)
                {
                    MessageBox.Show("Address name can't be blank !");
                    textBox4.Select();
                }
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                textBox6.Select();
            }
            if (e.KeyChar == 13)
            {
                textBox6.Select();
            }
        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.PageUp)
            {
                textBox4.Select();
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                textBox7.Select();
            }
            if (e.KeyChar == 13)
            {
                textBox7.Select();
            }
        }

        private void textBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.PageUp)
            {
                textBox5.Select();
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                textBox8.Select();
            }
            if (e.KeyChar == 13)
            {
                textBox8.Select();
            }
        }

        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.PageUp)
            {
                textBox6.Select();
            }
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                textBox9.Select();
            }
            if (e.KeyChar == 13)
            {
                textBox9.Select();
            }
        }

        private void textBox8_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.PageUp)
            {
                textBox7.Select();
            }
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                textBox10.Select();
            }
            if (e.KeyChar == 13)
            {
                textBox10.Select();
            }
        }

        private void textBox9_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.PageUp)
            {
                textBox8.Select();
            }
        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                textBox11.Select();
            }
            if (e.KeyChar == 13)
            {
                textBox11.Select();
            }
        }

        private void textBox10_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.PageUp)
            {
                textBox9.Select();
            }
        }

        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                textBox12.Select();
            }
            if (e.KeyChar == 13)
            {
                textBox12.Select();
            }
        }

        private void textBox11_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.PageUp)
            {
                textBox10.Select();
            }
        }

        private void textBox12_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                textBox13.Select();
            }
            if (e.KeyChar == 13)
            {
                textBox13.Select();
            }
        }

        private void textBox12_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.PageUp)
            {
                textBox11.Select();
            }
        }

        private void textBox13_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                textBox14.Select();
            }
            if (e.KeyChar == 13)
            {
                textBox14.Select();
            }
        }

        private void textBox13_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.PageUp)
            {
                textBox12.Select();
            }
        }

        private void textBox14_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button5.Select();
            }
            if (e.KeyChar == 13)
            {
                button5.Select();
            }
        }

        private void textBox14_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.PageUp)
            {
                textBox13.Select();
            }
        }

        private void button5_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void button5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.PageUp)
            {
                textBox14.Select();
            }
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

                    var Name = listB[loopCount];
                    switch (Name.Length)
                    {
                        case 2:
                            Name += "                  ";
                            break;
                        case 3:
                            Name += "                 ";
                            break;
                        case 4:
                            Name += "                ";
                            break;
                        case 5:
                            Name += "               ";
                            break;
                        case 6:
                            Name += "              ";
                            break;
                        case 7:
                            Name += "             ";
                            break;
                        case 8:
                            Name += "            ";
                            break;
                        case 9:
                            Name += "           ";
                            break;
                        case 10:
                            Name += "          ";
                            break;
                        case 11:
                            Name += "         ";
                            break;
                        case 12:
                            Name += "        ";
                            break;
                        case 13:
                            Name += "       ";
                            break;
                        case 14:
                            Name += "      ";
                            break;
                        case 15:
                            Name += "     ";
                            break;
                        case 16:
                            Name += "    ";
                            break;
                        case 17:
                            Name += "   ";
                            break;
                        case 18:
                            Name += "  ";
                            break;
                        case 19:
                            Name += " ";
                            break;
                    }

                    var City = listD[loopCount];
                    switch (City.Length)
                    {
                        case 3:
                            City += "             ";
                            break;
                        case 4:
                            City += "            ";
                            break;
                        case 5:
                            City += "           ";
                            break;
                        case 6:
                            City += "          ";
                            break;
                        case 7:
                            City += "         ";
                            break;
                        case 8:
                            City += "        ";
                            break;
                        case 9:
                            City += "       ";
                            break;
                        case 10:
                            City += "      ";
                            break;
                        case 11:
                            City += "     ";
                            break;
                        case 12:
                            City += "    ";
                            break;
                        case 13:
                            City += "   ";
                            break;
                        case 14:
                            City += "  ";
                            break;
                        case 15:
                            City += " ";
                            break;
                    }
                    var TheCount = loopCount.ToString();
                    switch (TheCount.Length)
                    {
                        case 1:
                            TheCount += "  ";
                            break;
                        case 2:
                            TheCount += " ";
                            break;
                    }
                    richTextBox1.Text = richTextBox1.Text + listP[loopCount] + "\t\t" + Name + "\t" + City + "\t" + listE[loopCount] + ", " + listG[loopCount] + "\n";
                    loop++;
                    
                    loopCount++;
                }
                reader.Close(); // Close the open file

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 198: Sorry an error has occured: " + ex.Message);
            }
        }
    }
}
