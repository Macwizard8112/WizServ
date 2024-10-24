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

namespace WizServ
{
    public partial class GoldCustMenu : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        private readonly string file = @"I:\\Datafile\\Control\\Gold.CSV";
        private DateTime datein;
        private int loopCount, loop;

        public GoldCustMenu()
        {
            InitializeComponent();
            BackColor = Color.FromArgb(0, 132, 129);
            Icon = image100;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
            GetData();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Hide();
            ClaimsMGTMenu f2 = new ClaimsMGTMenu();
            f2.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Hide();
            Gold_Print f2 = new Gold_Print();
            f2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            Gold_Edit f2 = new Gold_Edit();
            f2.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Hide();
            DeleteGoldMember f2 = new DeleteGoldMember();
            f2.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Hide();
            UndeleteGoldMember f2 = new UndeleteGoldMember();
            f2.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Hide();
            Gold_Edit f2 = new Gold_Edit();
            f2.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Hide();
            Gold_Print f2 = new Gold_Print();
            f2.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Hide();
            DeleteGoldMember f2 = new DeleteGoldMember();
            f2.Show();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Hide();
            UndeleteGoldMember f2 = new UndeleteGoldMember();
            f2.Show();
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            Hide();
            ClaimsMGTMenu f2 = new ClaimsMGTMenu();
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

                    if (listG[loopCount] == "Y")
                    {
                        loop++;
                        label4.ForeColor = Color.Yellow;
                        label4.Text = loop.ToString();
                    }
                    loopCount++;

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
