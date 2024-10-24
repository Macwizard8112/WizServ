using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WizServ
{
    public partial class NameLookup : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        private readonly string HistoryDB = @"I:\Datafile\Control\HistoryDB.csv";
        private string claim_no, fname, lname, addr, city, state, zip, hphone, wphone;
        private bool war_prd;
        private DateTime datein;
        private int loopCount, loop;
        public string ADDRESS, newLine, AllorPart, IsSelected, fName, lName, Name, City, StateZip, IsSel2;

        public NameLookup()
        {
            InitializeComponent();
            Icon = image100;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
            claim_no = Version.Claim;
            label4.Text = "Double-Click on Claim # to select that claim.";
            Text = "Retrieve Claim by Last Name";
            label1.Text = "Searching for: " + claim_no;
            //GetData();
            textBox1.Select();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            IsSel2 = listBox1.SelectedItem.ToString();
            IsSel2 = IsSel2.Substring(0, 7).Trim();
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            IsSelected = listBox1.SelectedItem.ToString();
            IsSelected = IsSelected.Substring(0, 7).Trim();
            MessageBox.Show("Selected Claim #: " + IsSelected);
            var SelectedText = IsSelected;
            Version.Claim = SelectedText.Trim();
            claim_no = SelectedText.Trim();
            Version.Claim = claim_no;
            Version.From = claim_no;
            Hide();
            NewClaim f2 = new NewClaim();
            f2.Show();
            
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AllorPart = textBox1.Text;
                GetData();
            }
        }

        private void RichTextBox1_DoubleClick(object sender, EventArgs e)
        {
            var SelectedText = richTextBox1.SelectedText;
            Version.Claim = SelectedText.Trim();
            claim_no = SelectedText.Trim();
            Version.Claim = claim_no;
            Version.From = claim_no;
            Hide();
            NewClaim f2 = new NewClaim();
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

        private void NameLookup_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void NameLookup_FormClosed(object sender, FormClosedEventArgs e)
        {
            Hide();
            EnterServiceCustMenu f2 = new EnterServiceCustMenu();
            f2.Show();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Hide();
            EnterServiceCustMenu f2 = new EnterServiceCustMenu();
            f2.Show();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }

        public void GetData()
        {
            listBox1.Items.Clear();
            try
            {
                StreamReader reader = new StreamReader(HistoryDB, Encoding.GetEncoding("Windows-1252"));
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

                    listA.Add(values[0]);       //  Claim Number
                    listB.Add(values[1]);       //  First Name
                    listC.Add(values[2]);       //  Last Name
                    listD.Add(values[3]);       //  Addr
                    listE.Add(values[4]);       //  City
                    listF.Add(values[5]);       //  State
                    listG.Add(values[6]);       //  Zip
                    listH.Add(values[7]);       //  Home Phone
                    listI.Add(values[8]);       //  Work Phone #

                    var first = listA[loopCount]; 
                    var second = listB[loopCount];
                    var third = listC[loopCount];
                    var fourth = listD[loopCount];
                    var fifth = listE[loopCount];
                    var sixth = listF[loopCount];
                    var seventh = listG[loopCount];
                    var eighth = listH[loopCount];
                    var ninth = listI[loopCount];

                    var Index = listA[loopCount];
                    Name = listC[loopCount] + " , " + listB[loopCount];
                    fName = listB[loopCount];
                    lName = listC[loopCount];
                    var Addr = listD[loopCount];
                    City = listE[loopCount];
                    StateZip = listF[loopCount] + ", " + listG[loopCount];
                    var NameLen = Name.Length;
                    if (City.StartsWith("-") ^ City.StartsWith("."))
                    {
                        City = "Unknown " + City;
                    }
                    FixNames();
                    FixCity();
                    if (third.StartsWith(AllorPart))
                    {
                        richTextBox1.Text = richTextBox1.Text + Index + "\t" + Name + "\t" + City + "\t" + Addr + "\n";
                        listBox1.Items.Add(Index + "\t" + Name + "\t" + City + "\t" + Addr + "\n");
                        newLine = newLine + first + com + second + com + third + com + fourth + com +
                            fifth + com + sixth + com + seventh + com + eighth + com + ninth + Environment.NewLine;
                        //csv.AppendLine(newLine);
                    }
                        loop++;
                        loopCount++;
                    
                }
                label1.Text = loopCount.ToString();
                reader.Close();                                         // Close the open file
                string filePath = @"I:\\Datafile\\Control\\ClientsList2.csv";
                csv.AppendLine("CLAIM" + "," + "First" + "," + "Last" + "," + "Address" + "," + "City" + "," + "State" + "," + "Zip" + "," + "Home PH" + "," + "Work PH");
                csv.AppendLine(newLine);
                File.WriteAllText(filePath, csv.ToString());            // Create CSV file  ClientList2.csv

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 248: Sorry an error has occured: " + ex.Message);
            }
        }

        public void FixCity()
        {
            switch(City.Length)
            {
                case 2:
                    City = City + "                         ";
                    break;
                case 3:
                    City = City + "                        ";
                    break;
                case 4:
                    City = City + "                       ";
                    break;
                case 5:
                    City = City + "                      ";
                    break;
                case 6:
                    City = City + "                     ";
                    break;
                case 7:
                    City = City + "                    ";
                    break;
                case 8:
                    City = City + "                   ";
                    break;
                case 9:
                    City = City + "                  ";
                    break;
                case 10:
                    City = City + "                 ";
                    break;
                case 11:
                    City = City + "                ";
                    break;
                case 12:
                    City = City + "               ";
                    break;
                case 13:
                    City = City + "              ";
                    break;
                case 14: 
                    City = City + "             ";
                    break;
                case 15:
                    City = City + "            ";
                    break;
                case 16:
                    City = City + "           ";
                    break;
                case 17:
                    City = City + "          ";
                    break;
                case 18:
                    City = City + "         ";
                    break;
                case 19:
                    City = City + "        ";
                    break;
                case 20:
                    City = City + "       ";
                    break;
                case 21:
                    City = City + "      ";
                    break;
                case 22:
                    City = City + "     ";
                    break;
                case 23:
                    City = City + "    ";
                    break;
                case 24:
                    City = City + "   ";
                    break;
                case 25:
                    City = City + "  ";
                    break;
                case 26:
                    City = City + " ";
                    break;
                case 27:
                    City = City + "";
                    break;
            }
            City += " " + StateZip;
        }

        public void FixNames()
        {
            switch(Name.Length)
            {
                case 1:
                    Name += "                                     ";
                    break;
                case 2:
                    Name += "                                    ";
                    break;
                case 3:
                    Name += "                                   ";
                    break;
                case 4:
                    Name += "                                  ";
                    break;
                case 5:
                    Name += "                                 ";
                    break;
                case 6:
                    Name += "                                ";
                    break;
                case 7:
                    Name += "                               ";
                    break;
                case 8:
                    Name += "                              ";
                    break;
                case 9:
                    Name += "                             ";
                    break;
                case 10:
                    Name += "                            ";
                    break;
                case 11:
                    Name += "                           ";
                    break;
                case 12:
                    Name += "                          ";
                    break;
                case 13:
                    Name += "                         ";
                    break;
                case 14:
                    Name += "                        ";
                    break;
                case 15:
                    Name += "                       ";
                    break;
                case 16:
                    Name += "                      ";
                    break;
                case 17:
                    Name += "                     ";
                    break;
                case 18:
                    Name += "                    ";
                    break;
                case 19:
                    Name += "                   ";
                    break;
                case 20:
                    Name += "                  ";
                    break;
                case 21:
                    Name += "                 ";
                    break;
                case 22:
                    Name += "                ";
                    break;
                case 23:
                    Name += "               ";
                    break;
                case 24:
                    Name += "              ";
                    break;
                case 26:
                    Name += "             ";
                    break;
                case 27:
                    Name += "           ";
                    break;
                case 28:
                    Name += "          ";
                    break;
                case 29:
                    Name += "         ";
                    break;
                case 30:
                    Name += "        ";
                    break;
                case 31:
                    Name += "       ";
                    break;
                case 32:
                    Name += "      ";
                    break;
                case 33:
                    Name += "     ";
                    break;
                case 34:
                    Name += "    ";
                    break;
                case 35:
                    Name += "   ";
                    break;
                case 36:
                    Name += "  ";
                    break;
                case 37:
                    Name += " ";
                    break;

            }
        }
    }
}
