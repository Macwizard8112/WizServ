using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace WizServ
{
    public partial class BySerialNumber : Form
    {
        public string claim_no = Version.Claim;
        public Icon image100 = Properties.Resources.WizServ;
        private readonly string file = @"I:\\Datafile\\Control\\Database.CSV";
        //private string fname, lname, addr, city, state, zip, hphone, wphone;
        //private bool war_prd;
        //private DateTime datein;
        public string calledfrom;

        public BySerialNumber()
        {
            InitializeComponent();
            label1.Text = "Serial starts with: " + claim_no;
            Icon = image100;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
            calledfrom = Version.From;
            GetData();
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

        private int loopCount, loop;

        private void RichTextBox1_DoubleClick(object sender, EventArgs e)
        {
            var SelectedText = richTextBox1.SelectedText;
            Version.Claim = SelectedText.Trim();
            claim_no = SelectedText.Trim();
            Hide();
            ByClaimNum f2 = new ByClaimNum();
            f2.Show();
        }

        private void Button2_Click(object sender, EventArgs e)  // Retrieve Menu
        {
            Hide();
            RetrieveMenu f2 = new RetrieveMenu();
            f2.Show();
        }

        private void Button3_Click(object sender, EventArgs e)  // Main Menu
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }

        private void Button4_Click(object sender, EventArgs e)  // Print
        {
            var fileToOpen = "I:\\Datafile\\Doc\\Serials.txt";
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

        private void Button1_Click(object sender, EventArgs e)  // Save
        {
            TextWriter txt = new StreamWriter("I:\\Datafile\\Doc\\Serials.txt");
            txt.Write(richTextBox1.Text);
            txt.Close();    // Close open file
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
                    listB.Add(values[1]);       //  Claim_No
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
                    listP.Add(values[15]);      //  Serial Number

                    var Name = listE[loopCount] + ", " + listD[loopCount];
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


                    if (listP[loopCount].StartsWith(claim_no))
                    {
                        var MakeModel = listM[loopCount] + ", " + listO[loopCount];
                        switch (MakeModel.Length)
                        {
                            case 36:
                                MakeModel += "\t";
                                break;
                            case 35:
                                MakeModel += "\t";
                                break;
                            case 34:
                                MakeModel += "\t";
                                break;
                            case 33:
                                MakeModel += "\t";
                                break;
                            case 32:
                                MakeModel += "";
                                break;
                            case 31:
                                MakeModel += "";
                                break;
                            case 30:
                                MakeModel += "";
                                break;
                            case 29:
                                MakeModel += "\t";
                                break;
                            case 28:
                                MakeModel += "\t";
                                break;
                            case 27:
                                MakeModel += "\t";
                                break;
                            case 26:
                                MakeModel += "\t";
                                break;
                            case 25:
                                MakeModel += "\t";
                                break;
                            case 24:
                                MakeModel += "\t";
                                break;
                            case 23:
                                MakeModel += "\t\t";
                                break;
                            case 22:
                                MakeModel += "\t\t";
                                break;
                            case 21:
                                MakeModel += "\t\t";
                                break;
                            case 20:
                                MakeModel += "\t\t";
                                break;
                            case 19:
                                MakeModel += "\t\t";
                                break;
                            case 18:
                                MakeModel += "\t\t";
                                break;
                            case 17:
                                MakeModel += "\t\t";
                                break;
                            case 16:
                                MakeModel += "\t\t\t";
                                break;
                            case 15: 
                                MakeModel += "\t\t\t\t";
                                break;
                            case 14:
                                MakeModel += "\t\t\t\t";
                                break;
                            case 10:
                                MakeModel += "\t\t\t\t\t"; 
                                break;

                        }

                        if (listP[loopCount].Length <= 5)
                        {
                            listP[loopCount] += "\t";
                        }
                        richTextBox1.Text = richTextBox1.Text + listP[loopCount] + "\t" + listB[loopCount] + "\t" + MakeModel + "\t" + Name + "\t" + listF[loopCount] + "\n";
                        loop++;
                    }
                    loopCount++;
                }
                reader.Close(); // Close the open file

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 346: Sorry an error has occured: " + ex.Message);
            }
        }
    }
}
