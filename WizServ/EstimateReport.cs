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
    public partial class EstimateReport : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        private readonly string file = @"I:\\Datafile\\Control\\Estimates.CSV";
        private string claim_no, fname, lname, addr, city, state, zip, hphone, wphone;
        private bool war_prd;
        private DateTime datein;
        private int loopCount, loop;
        private string IsClosed = "";
        private string Spacer;

        public EstimateReport()
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(0, 132, 129);
            Icon = image100;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
            GetData();
        }

        public void GetData()
        {
            var dt = DateTime.Now.ToString();
            richTextBox1.Text = richTextBox1.Text + "OPEN ESTIMATE CLAIMS\t" + dt +"\n\n";
            richTextBox1.Text = richTextBox1.Text + "Claim #\tDate In\tName\n\n";
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

                loopCount = 0;

                while (!reader.EndOfStream)
                {
                    var lineRead = reader.ReadLine();
                    var values = lineRead.Split(',');

                    listA.Add(values[0]);       //  Claim No
                    listB.Add(values[1]);       //  Date In
                    listC.Add(values[2]);       //  First Name
                    listD.Add(values[3]);       //  Last Name
                    listE.Add(values[4]);       //  Brand
                    listF.Add(values[5]);       //  Model
                    listG.Add(values[6]);       //  Estimate

                    var Datein = listB[loopCount];
                    var mName = listC[loopCount] + " " + listD[loopCount];
                    if (Datein.Length == 8)
                    {
                        Spacer = "\t";
                    }
                    if (Datein.Length == 9)
                    {
                        Spacer = "\t";
                    }

                    if (listG[loopCount] == "ESTIMATE")
                    {
                        richTextBox1.Text = richTextBox1.Text + listA[loopCount] + "\t" + Datein + Spacer + mName + "\n";
                        loop++;
                        
                    }
                    loopCount++;

                }
            reader.Close(); // Close the open file
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 93: Sorry an error has occured: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            ClaimsMGTMenu f2 = new ClaimsMGTMenu();
            f2.Show();
        }

        private void button4_Click(object sender, EventArgs e)  // Page Setup
        {
            TextWriter txt = new StreamWriter("I:\\Datafile\\Doc\\Estimates.txt");
            txt.Write(richTextBox1.Text);
            txt.Close();
            var fileToOpen = "I:\\Datafile\\Doc\\Estimates.txt";
            if (!File.Exists(fileToOpen))
            {
                button1.PerformClick();
            }
            var process = new Process();
            process.StartInfo = new ProcessStartInfo()
            {
                UseShellExecute = true,
                FileName = fileToOpen
            };

            process.Start();
            process.WaitForExit();
        }

        private void button6_Click(object sender, EventArgs e)  // Print
        {
            TextWriter txt = new StreamWriter("I:\\Datafile\\Doc\\Estimates.txt");
            txt.Write(richTextBox1.Text);
            txt.Close();
            Process.Start("notepad.exe", "/p I:\\Datafile\\Doc\\Estimates.txt");
        }
    }
}
