using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;

namespace WizServ
{
    public partial class StopPhoneCalls : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        private int loopCount;
        private readonly string PhoneCalls = @"I:\Datafile\Control\PhoneCalls.CSV";
        public string A, B, C, D, E, F, G, H, I, J, K, L, claimNo, path;
        public string A1, B1, C1, D1, E1, F1, G1, H1, I1, J1, K1, L1;
        public string selected;

        public int Pass { get; private set; }
        public int Pass2 { get; private set; }

        public StopPhoneCalls()
        {
            InitializeComponent();
            Icon = image100;
            label12.Visible = false;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
            LoadCB2();
            GetProduct();
            label14.ForeColor = Color.DarkBlue;
            label14.BackColor = Color.White;
            label14.Text = " Select Y or N above. ";
            label6.Visible = false;
            timer1.Stop();
            timer1.Interval = 3000; // 2 seconds
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label6.Text = "";
            timer1.Stop();
            label12.Visible = true;
            label12.ForeColor = Color.White;
            label12.Text = "Select another claim to edit or RETURN to exit.";
        }

        private void LoadCB2()
        {
            comboBox2.Items.Add("Y");
            comboBox2.Items.Add("N");
            comboBox2.SelectedIndex = 0;
            label12.ForeColor = Color.Black;
            label12.Text = "Claim has been updated.";
        }

        private void SetNewMsg()
        {
            label12.ForeColor = Color.Black;
            label12.Text = "Claim has been updated.";
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            label6.Visible = true;
            var t = comboBox2.SelectedIndex;
            var msg = C1 + " " + D1 + " " + E1 + " " + F1;
            if (t == 0)
            {
                label6.Text = claimNo + " " + msg + " will no longer receive calls.";
            }
            if (t == 1)
            {
                label6.Text = claimNo + " " + msg + " will still receive calls.";
            }
            label6.ForeColor = Color.Yellow;
            if (Pass2 == 1)
            {
                EditCSV();
                timer1.Start();
                label12.Visible = true;
            }
            Pass2++;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            ClaimsMGTMenu f2 = new ClaimsMGTMenu();
            f2.Show();
        }

        private void EditCSV()
        {
            path = PhoneCalls;
            List<String> lines = new List<String>();

            if (File.Exists(path)) 
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    String line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Contains(","))
                        {
                            String[] split = line.Split(',');

                            if (split[1].Contains(claimNo))
                            {
                                if (comboBox2.Text == "Y")
                                {
                                    split[0] = "Y";
                                }
                                if (comboBox2.Text == "N")
                                {
                                    split[0] = "N";
                                }
                                line = String.Join(",", split);
                            }
                        }

                        lines.Add(line);
                    }
                }

                using (StreamWriter writer = new StreamWriter(path, false))
                {
                    foreach (String line in lines)
                        writer.WriteLine(line);
                }
            }
            Pass2 = 0;
        }

        public void GetProduct()                // Populate Product List pulldown
        {
            try
            {
                StreamReader reader = new StreamReader(PhoneCalls, Encoding.GetEncoding("Windows-1252"));
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

                loopCount = 0;

                while (!reader.EndOfStream)
                {
                    var lineRead = reader.ReadLine();
                    var values = lineRead.Split(',');

                    listA.Add(values[0]);       //  Cancel Phone, Call Y = call, N = Don't call
                    listB.Add(values[1]);       //  claim_no
                    listC.Add(values[2]);       //  Date In
                    listD.Add(values[3]);       //  First Name
                    listE.Add(values[4]);       //  Last Name
                    listF.Add(values[5]);       //  Address
                    listG.Add(values[6]);       //  City
                    listH.Add(values[7]);       //  State
                    listI.Add(values[8]);       //  Zip Code
                    listJ.Add(values[9]);       //  Home Phone
                    listK.Add(values[10]);      //  Work Phone
                    listL.Add(values[11]);      //  Email Address

                    A = listA[loopCount];
                    B = listB[loopCount];
                    C = listC[loopCount];
                    D = listD[loopCount];
                    E = listE[loopCount];
                    F = listF[loopCount];
                    G = listG[loopCount];
                    H = listH[loopCount];
                    I = listI[loopCount];
                    J = listJ[loopCount];
                    K = listK[loopCount];
                    L = listL[loopCount];

                    comboBox1.Items.Add(B + " " + J + " " + D + " " + E);   // Claim#, Home Phone, First, Last Name
                    loopCount++;
                }
                reader.Close(); // Close the open file
                comboBox1.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 170: Sorry an error has occured: " + ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label7.Text = "";
            SetNewMsg();
            selected = comboBox1.Text;
            label1.Text = "Whole String: " + selected;
            var s = comboBox1.SelectedIndex;    // 0 = 1st item
            string s1 = selected;
            string[] subs = s1.Split(' ');

            foreach (var sub in subs)
            {
                if (Pass == 0)
                {
                    label4.Text = "Claim: " + ($"{sub}");
                    A1 = ($"{sub}");
                    claimNo = A1;       // Claim Number
                }
                if (Pass == 1)
                {
                    //label7.Text += " " + ($"{sub}");
                    B1 = ($"{sub}");    // Phone Number
                }
                if (Pass == 2)
                {
                    label7.Text += " " + ($"{sub}");
                    C1 = ($"{sub}");    // Name
                }
                if (Pass == 3)
                {
                    label7.Text += " " + ($"{sub}");
                    D1 = ($"{sub}");    // Name
                }
                if (Pass == 4)
                {
                    label7.Text += " " + ($"{sub}");
                    E1 = ($"{sub}");    // Name
                }
                if (Pass == 5)
                {
                    label7.Text += " " + ($"{sub}");
                    F1 = ($"{sub}");    // Name
                }
                Pass++;
            }
            Pass = 0;
        }
    }
}
