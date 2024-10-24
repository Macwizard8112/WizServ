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
    public partial class EstimateReports : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        public readonly string file2 = @"I:\\Datafile\\Control\\Notified.CSV";
        private int loopCount;

        public EstimateReports()
        {
            InitializeComponent();
            Icon = image100;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
            timer1.Enabled = true;
            timer1.Interval = 5000; // 5000 = 5 seconds, (1000 millaseconds per second)
            timer1.Start();
            GetEstimatesSaved();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            ClaimsMGTMenu f2 = new ClaimsMGTMenu();
            f2.Show();
        }

        public void GetEstimatesSaved()
        {
            try
            {
                StreamReader reader = new StreamReader(file2, Encoding.GetEncoding("Windows-1252"));
                String line = reader.ReadLine();

                List<string> claim = new List<string>();
                List<string> date = new List<string>();
                List<string> time = new List<string>();
                List<string> CloseD = new List<string>();
                List<string> CloseT = new List<string>();
                List<string> Appr = new List<string>();

                loopCount = 0;
                richTextBox1.Text = richTextBox1.Text + "\t\tEstimates Already Approved / Declined: " + DateTime.Now.ToShortDateString() + "\n\n";
                richTextBox1.Text = richTextBox1.Text + "Claim #     Date        Time        SSN   Who   Status\n\n";
                while (!reader.EndOfStream)
                {
                    var lineRead = reader.ReadLine();
                    var values = lineRead.Split(',');

                    claim.Add(values[0]);      //  Claim #         Claim Number
                    date.Add(values[1]);       //  Date            Date
                    time.Add(values[2]);       //  Time            Time
                    CloseD.Add(values[3]);     //  Who Approved
                    CloseT.Add(values[4]);     //  SSN
                    Appr.Add(values[5]);       //  Approved 

                    var cclaim = claim[loopCount];
                    var cdate = date[loopCount];
                    var ctime = time[loopCount];
                    var cWho = CloseD[loopCount];
                    var cSsn = CloseT[loopCount];
                    var cAppr = Appr[loopCount];

                    if (cAppr == "A")
                    {
                        richTextBox1.Text = richTextBox1.Text + cclaim + "\t" + cdate + "\t" + ctime + "\t" + cSsn + "\t" + cWho + "\t" + "Approved" + "\n";
                    }
                    if (cAppr == "_")
                    {
                        richTextBox1.Text = richTextBox1.Text + cclaim + "\t" + cdate + "\t" + ctime + "\t" + cSsn + "\t" + cWho + "\t" + "Declined" + "\n";
                    }

                    loopCount++;
                }
                reader.Close(); // Close the open file
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: (Line 297) \n" + ex);
            }
        }

        private void EstimateReports_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void EstimateReports_FormClosed(object sender, FormClosedEventArgs e)
        {
            Hide();
            ClaimsMGTMenu f2 = new ClaimsMGTMenu();
            f2.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
            richTextBox1.Text = "";
            GetEstimatesSaved();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.SaveFile(@"I:\\Datafile\\Doc\\Estimate3.rtf", RichTextBoxStreamType.RichText);
            TextWriter txt = new StreamWriter("I:\\Datafile\\Doc\\Estimate3.rtf");
            txt.Write(richTextBox1.Text);
            txt.Close();
            var fileToOpen = "I:\\Datafile\\Doc\\Estimate3.rtf";
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
            richTextBox1.SaveFile(@"I:\\Datafile\\Doc\\Estimate3.rtf", RichTextBoxStreamType.RichText);
            TextWriter rtf = new StreamWriter("I:\\Datafile\\Doc\\Estimat3.rtf");
            rtf.Write(richTextBox1.Text);
            rtf.Close();
            Process.Start("wordpad.exe", "/p I:\\Datafile\\Doc\\Estimate3.rtf");
        }
    }
}
