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
using System.Diagnostics;
using System.Drawing.Printing;

namespace WizServ
{
    public partial class Gold_Print : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        private readonly string file = @"I:\\Datafile\\Control\\Gold.CSV";
        private DateTime datein;
        private int loopCount, loop;

        public Gold_Print()
        {
            InitializeComponent();
            BackColor = Color.FromArgb(0, 132, 129);
            Icon = image100;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
            GetData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            GoldCustMenu f2 = new GoldCustMenu();
            f2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TextWriter txt = new StreamWriter("I:\\Datafile\\Doc\\Gold_Print.txt");
            txt.Write(richTextBox1.Text);
            txt.Close();
            var fileToOpen = "I:\\Datafile\\Doc\\Gold_Print.txt";
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

        private void button3_Click(object sender, EventArgs e)
        {
            TextWriter txt = new StreamWriter("I:\\Datafile\\Doc\\Gold_Print.txt");
            txt.Write(richTextBox1.Text);
            txt.Close();
            Process.Start("notepad.exe", "/p I:\\Datafile\\Doc\\Gold_Print.txt");
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            char[] param = { '\n' };

            if (printDialog1.PrinterSettings.PrintRange == PrintRange.Selection)
            {
                lines = richTextBox1.SelectedText.Split(param);
            }
            else
            {
                lines = richTextBox1.Text.Split(param);
            }

            int i = 0;
            char[] trimParam = { '\r' };
            foreach (string s in lines)
            {
                lines[i++] = s.TrimEnd(trimParam);
            }
        }

        private int linesPrinted;
        private string[] lines;
        private void OnPrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;
            Brush brush = new SolidBrush(richTextBox1.ForeColor);

            while (linesPrinted < lines.Length)
            {
                e.Graphics.DrawString(lines[linesPrinted++],
                    richTextBox1.Font, brush, x, y);
                y += 15;
                if (y >= e.MarginBounds.Bottom)
                {
                    e.HasMorePages = true;
                    return;
                }
            }

            linesPrinted = 0;
            e.HasMorePages = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
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

                loopCount = 0;

                while (!reader.EndOfStream)
                {
                    var lineRead = reader.ReadLine();
                    var values = lineRead.Split(',');

                    listA.Add(values[0]);       //  First Name
                    listB.Add(values[1]);       //  Last Name
                    listC.Add(values[2]);       //  Address
                    listD.Add(values[3]);       //  City
                    listE.Add(values[4]);       //  State
                    listF.Add(values[5]);       //  Zip Code
                    listG.Add(values[6]);       //  Display Y or N
                    listH.Add(values[7]);       //  Number

                    var fname = listA[loopCount];
                    var lname = listB[loopCount];
                    var addr = listC[loopCount];
                    var city = listD[loopCount];
                    var state = listE[loopCount];
                    var zip = listF[loopCount];
                    var number = listH[loopCount];
                    var combName = fname + " " + lname;

                    if (zip.Length == 4)
                    {
                        zip = "0" + zip;
                    }
                    if (listG[loopCount] == "Y")
                    {
                        switch (combName.Length)
                        {
                            case 8:
                                richTextBox1.Text = richTextBox1.Text + number + "\t" + combName + "\t\t" + city + "\t\t\t" + state + "\t" + zip + "\t  " + addr + "\n";
                                break;
                            case 9:
                                richTextBox1.Text = richTextBox1.Text + number + "\t" + combName + "\t\t" + city + "\t\t\t" + state + "\t" + zip + "\t  " + addr + "\n";
                                break;
                            case 10:
                                if (city.Length == 6)
                                {
                                    //richTextBox1.Text = richTextBox1.Text + number + "\t" + combName + "\t\t" + city + "\t\t\t\t" + state + "\t" + zip + "\t  " + addr + "\n";
                                }
                                if (combName != "VAN MILLER")
                                {
                                    if (city.Length == 6)
                                    {
                                        richTextBox1.Text = richTextBox1.Text + number + "\t" + combName + "\t\t" + city + "\t\t\t\t" + state + "\t" + zip + "\t  " + addr + "\n";

                                    }
                                    if (city.Length != 6)
                                    {
                                        richTextBox1.Text = richTextBox1.Text + number + "\t" + combName + "\t\t" + city + "\t\t\t" + state + "\t" + zip + "\t  " + addr + "\n";

                                    }
                                }
                                if (combName == "VAN MILLER")
                                {
                                    richTextBox1.Text = richTextBox1.Text + number + "\t" + combName + "\t\t" + city + "\t \t" + state + "\t" + zip + "\t  " + addr + "\n";
                                }
                                break;
                            case 11:
                                richTextBox1.Text = richTextBox1.Text + number + "\t" + combName + "\t\t" + city + "\t\t\t" + state + "\t" + zip + "\t  " + addr + "\n";
                                break;
                            case 12:
                                if (city.Length == 6)
                                {
                                    richTextBox1.Text = richTextBox1.Text + number + "\t" + combName + "\t\t" + city + "\t\t\t\t" + state + "\t" + zip + "\t  " + addr + "\n";

                                }
                                else
                                {
                                    richTextBox1.Text = richTextBox1.Text + number + "\t" + combName + "\t\t" + city + "\t\t\t" + state + "\t" + zip + "\t  " + addr + "\n";
                                }
                                break;
                            case 13:
                                if (city.Length == 6)
                                {
                                    richTextBox1.Text = richTextBox1.Text + number + "\t" + combName + "\t\t" + city + "\t\t\t\t" + state + "\t" + zip + "\t  " + addr + "\n";

                                }
                                else
                                {
                                    richTextBox1.Text = richTextBox1.Text + number + "\t" + combName + "\t\t" + city + "\t\t\t" + state + "\t" + zip + "\t  " + addr + "\n";

                                }
                                break;
                            case 14:
                                richTextBox1.Text = richTextBox1.Text + number + "\t" + combName + "\t" + city + "\t\t\t" + state + "\t" + zip + "\t  " + addr + "\n";
                                break;
                            case 15:
                                richTextBox1.Text = richTextBox1.Text + number + "\t" + combName + "\t" + city + "\t\t\t" + state + "\t" + zip + "\t  " + addr + "\n";
                                break;
                            case 16:
                                richTextBox1.Text = richTextBox1.Text + number + "\t" + combName + "\t" + city + "\t\t\t\t" + state + "\t" + zip + "\t  " + addr + "\n";
                                break;
                            case 17:
                                richTextBox1.Text = richTextBox1.Text + number + "\t" + combName + "\t" + city + "\t\t\t" + state + "\t" + zip + "\t  " + addr + "\n";
                                break;
                            case 18:
                                richTextBox1.Text = richTextBox1.Text + number + "\t" + combName + "\t" + city + "\t\t\t" + state + "\t" + zip + "\t  " + addr + "\n";
                                break;
                            case 19:
                                richTextBox1.Text = richTextBox1.Text + number + "\t" + combName + "\t" + city + "\t\t\t" + state + "\t" + zip + "\t  " + addr + "\n";
                                break;
                            case 20:
                                richTextBox1.Text = richTextBox1.Text + number + "\t" + combName + "\t" + city + "\t\t\t" + state + "\t" + zip + "\t  " + addr + "\n";
                                break;
                            case 21:
                                richTextBox1.Text = richTextBox1.Text + number + "\t" + combName + "\t" + city + "\t\t\t" + state + "\t" + zip + "\t  " + addr + "\n";
                                break;
                        }
                        
                        loop++;
                    }
                    loopCount++;
                    richTextBox1.SelectAll();
                    richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
                    richTextBox1.DeselectAll();
                }
                reader.Close(); // Close the open file
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 82: Sorry an error has occured: " + ex.Message);
            }
        }


    }
}
