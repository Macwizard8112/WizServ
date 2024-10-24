using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace WizServ
{
    public partial class ByManuf : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        public string claim_no = Version.Claim;
        private readonly string file = @"I:\\Datafile\\Control\\Database.CSV";
        //private string fname, lname, addr, city, state, zip, hphone, wphone;
        //private bool war_prd;
        //private DateTime datein;
        private int loopCount;  // loop;
        public string calledfrom;

        public ByManuf()
        {
            InitializeComponent();
            Icon = image100;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
            GetData();
        }

        private void RichTextBox1_DoubleClick(object sender, EventArgs e)
        {
            var SelectedText = richTextBox1.SelectedText;
            Version.Claim = SelectedText.Trim();
            claim_no = SelectedText.Trim();
            Hide();
            ByClaimNum f2 = new ByClaimNum();
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

        private void Button1_Click(object sender, EventArgs e)
        {
            TextWriter txt = new StreamWriter("C:\\Datafile\\Doc\\Manuf.txt");
            txt.Write(richTextBox1.Text);
            txt.Close();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            var fileToOpen = "I:\\Datafile\\Doc\\Manuf.txt";
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

        private void Button2_Click(object sender, EventArgs e)
        {
            Hide();
            RetrieveMenu f2 = new RetrieveMenu();
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

                    if (listM[loopCount].Length <= 15)
                    {
                        listM[loopCount] += "\t\t";
                    }

                    if (listM[loopCount].Contains(claim_no))
                    {
                        var name = listD[loopCount] + " " + listE[loopCount];
                        var model = listO[loopCount];
                        if (model.Length <= 6)
                        {
                            model += "\t";
                        }
                        if (model.Length <= 15)
                        {
                            model += "\t";
                        }
                        if (model.Length <= 25)
                        {
                            model += "\t\t";
                        }
                        if (listO[loopCount].Contains("EON ONE COMPACT"))
                        {
                            model = listO[loopCount] + "\t\t";
                            richTextBox1.Text = richTextBox1.Text + listB[loopCount] + "\t" + listM[loopCount] + " " + model + "\t" + name + "\n";
                        }
                        if (listO[loopCount].Contains("EON ONE PRO-B"))
                        {
                            model = listO[loopCount] + "\t\t";
                            richTextBox1.Text = richTextBox1.Text + listB[loopCount] + "\t" + listM[loopCount] + " " + model + "\t" + name + "\n";
                        }
                        else
                        {
                            richTextBox1.Text = richTextBox1.Text + listB[loopCount] + "\t" + listM[loopCount] + " " + model + "\t" + name + "\n";
                        }
                            //loop++;
                    }
                    loopCount++;
                }
                reader.Close(); // Close the open file

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 237: Sorry an error has occured: " + ex.Message);
            }
        }
    }
}
