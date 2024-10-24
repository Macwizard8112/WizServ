using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WizServ
{
    public partial class EstimateEditScreen : Form
    {
        private readonly string Estimates = @"I:\\Datafile\\Mel\\Estimates.CSV";         // This is Read only CSV
        public string Index = Version.IndexNum;
        public int loop, loopCount;
        public string A, B, C, D, E, F, G, H, I, J, K, L, M, N;


        public EstimateEditScreen()
        {
            InitializeComponent();
            timer1.Interval = 2000;
            timer1.Stop();
            label7.Visible = false;
            PullCustomerData();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label7.Visible = false;
            timer1.Stop();
        }

        private void button1_Click(object sender, EventArgs e)  // Save new data
        {
            label7.Visible = true;
            timer1.Start();
            List<String> lines = new List<String>();

            if (File.Exists(Estimates))
            {
                using (StreamReader reader = new StreamReader(Estimates))
                {
                    String line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Contains(","))
                        {
                            String[] split = line.Split(',');

                            if (split[0].Contains(label2.Text.Trim()))
                            {
                                split[1] = textBox1.Text;                   // First Name
                                split[2] = textBox2.Text;                   // Last  Name
                                split[3] = textBox6.Text;                   // Parts Cost
                                split[4] = textBox7.Text;                   // Labor Cost
                                split[5] = textBox8.Text;                   // Shop Supplies
                                split[6] = textBox9.Text;                   // Shipping Costs
                                split[7] = textBox10.Text;                  // Tax
                                split[8] = textBox11.Text;                  // Total
                                split[9] = textBox12.Text;                  // Sent Date
                                split[11] = textBox3.Text;                  // Approval Date
                                split[12] = textBox4.Text;                  // Paid Down
                                split[13] = textBox5.Text;                  // Rush
                                line = String.Join(",", split);
                            }
                        }

                        lines.Add(line);
                    }
                }

                using (StreamWriter writer = new StreamWriter(Estimates, false))
                {
                    foreach (String line in lines)
                        writer.WriteLine(line);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            MainUtilitiesMenu f2 = new MainUtilitiesMenu();
            f2.Show();
        }

        public void PullCustomerData()
        {
            try
            {
                StreamReader reader = new StreamReader(Estimates, Encoding.GetEncoding("Windows-1252"));
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

                loopCount = 0;

                while (!reader.EndOfStream)
                {
                    var lineRead = reader.ReadLine();
                    var values = lineRead.Split(',');

                    listA.Add(values[0]);       //  Claim
                    listB.Add(values[1]);       //  First Name
                    listC.Add(values[2]);       //  Last Name
                    listD.Add(values[3]);       //  Parts Cost $
                    listE.Add(values[4]);       //  Labor Cost $
                    listF.Add(values[5]);       //  Shop Cost $
                    listG.Add(values[6]);       //  Shipping $
                    listH.Add(values[7]);       //  Tax $
                    listI.Add(values[8]);       //  Total $
                    listJ.Add(values[9]);       //  Sent Date
                    listK.Add(values[10]);      //  Index #
                    listL.Add(values[11]);      //  Approved Date
                    listM.Add(values[12]);      //  Paid Down
                    listN.Add(values[13]);      //  Rush

                   if (listK[loop] == Index)
                    {
                        A = listA[loop];
                        B = listB[loop];
                        C = listC[loop];
                        D = listD[loop];
                        E = listE[loop];
                        F = listF[loop];
                        G = listG[loop];
                        H = listH[loop];
                        I = listI[loop];
                        J = listJ[loop];
                        K = listK[loop];
                        L = listL[loop];
                        M = listM[loop];
                        N = listN[loop];
                        label2.Text = A;
                        textBox1.Text = B;
                        textBox2.Text = C;
                        textBox3.Text = L;
                        var money = Convert.ToDecimal(M);
                        var money2 = money.ToString("0.00");
                        M = money2;
                        textBox4.Text = M;
                        textBox5.Text = N;
                        textBox6.Text = D;
                        textBox7.Text = E;
                        textBox8.Text = F;
                        textBox9.Text = G;
                        textBox10.Text = H;
                        textBox11.Text = I;
                        textBox12.Text = J;
                    }
                    loop++;

                    loopCount++;
                }
                reader.Close(); // Close the open file

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 123: Sorry an error has occured: " + ex.Message);
            }
        }
    }
}
