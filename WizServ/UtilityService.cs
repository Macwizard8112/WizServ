using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WizServ
{
    public partial class UtilityService : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        private readonly string file = @"I:\\Datafile\\Control\\Estimates.CSV";
        private string claim_no, fname, lname, addr, city, state, zip, hphone, wphone;
        private bool war_prd;
        private DateTime datein;
        private int loopCount, loop;
        private string IsClosed = "";
        private string Spacer;
        private string ShowText = "Double-click on Claim # to Edit:";
        public string SelectedText;

        public UtilityService()
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(0, 132, 129);
            label9.BackColor = Color.FromArgb(0, 132, 129);
            Icon = image100;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
            label9.Visible = false;
            GetData();
            label2.Text = ShowText;
        }

        private void richTextBox1_DoubleClick(object sender, EventArgs e)
        {
            SelectedText = richTextBox1.SelectedText;
            Version.Claim = SelectedText.Trim();
            claim_no = SelectedText.Trim();
            if (SelectedText.Length <= 5)
            {
                return;
            }
            if (SelectedText.Length >= 7)
            {
                return;
            }
            // do something here with selected data
            label1.Text = SelectedText;
            EditClaim();
            GetData2();
        }

        public void EditClaim()
        {
            richTextBox1.Visible = false;
            label2.Text = SelectedText;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            WriteNewData2();
        }

        public void GetData2()
        {
            var dt = DateTime.Now.ToString();
            richTextBox1.Text = richTextBox1.Text + "OPEN ESTIMATE CLAIMS\t" + dt + "\n\n";
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

                    if (listA[loopCount] == SelectedText)
                    {
                        textBox1.Text = listB[loopCount];
                        textBox2.Text = listC[loopCount];
                        textBox3.Text = listD[loopCount];
                        textBox4.Text = listE[loopCount];
                        textBox5.Text = listF[loopCount];
                        textBox6.Text = listG[loopCount];
                        loop++;
                    }
                    loopCount++;

                }
                reader.Close(); // Close the open file
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 164: Sorry an error has occured: " + ex.Message);
            }
        }

        public void WriteNewData2()
        {

            string newFileName = @"I:\Datafile\Control\Estimates.CSV";

            string clientDetails = SelectedText + "," + textBox1.Text + "," + textBox2.Text + "," + textBox3.Text + "," + textBox4.Text + "," + textBox5.Text + "," + textBox6.Text;


            if (!File.Exists(newFileName))
            {
                string clientHeader = "claim_no" + "," + "datein" + "," + "fname" + "," + "lname" + "," + "model" + "," + "Estimate" + Environment.NewLine;

                File.WriteAllText(newFileName, clientHeader);
            }
            try
            {
                File.AppendAllText(newFileName, clientDetails);
                label9.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("");
            }
        }


        public void GetData()
        {
            var dt = DateTime.Now.ToString();
            richTextBox1.Text = richTextBox1.Text + "OPEN ESTIMATE CLAIMS\t" + dt + "\n\n";
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
                MessageBox.Show("Error 164: Sorry an error has occured: " + ex.Message);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Hide();
            ServiceRenderClaimMenu f2 = new ServiceRenderClaimMenu();
            f2.Show();
        }
    }
}
