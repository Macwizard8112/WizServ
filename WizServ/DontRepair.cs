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


namespace WizServ
{
    public partial class DontRepair : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        private readonly string file4 = @"I:\\Datafile\\Control\\DNR\\DoNotRepair.csv";
        public string fname, lname, addr, city, state, zip, hphone, wphone, NextClaimNum, from;
        private bool war_prd, ready;
        private DateTime datein;
        private int loopCount, loop;
        public string mSelected;

        public DontRepair()
        {
            InitializeComponent();
            Icon = image100;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
            GetProduct();
        }

        private void DontRepair_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
        }

        private void DontRepair_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void GetProduct()                // Populate Product Lit pulldown
        {
            try
            {
                StreamReader reader = new StreamReader(file4, Encoding.GetEncoding("Windows-1252"));
                String line = reader.ReadLine();

                List<string> listA = new List<string>();

                loopCount = 0;
                //Font myfont = new Font("Times New Roman", 12.0f);
                //textBox1.Font = myfont;
                while (!reader.EndOfStream)
                {
                    var lineRead = reader.ReadLine();
                    var values = lineRead.Split(',');

                    listA.Add(values[0]);       //  war_prd


                    textBox1.Text = textBox1.Text + listA[loopCount] + Environment.NewLine;
                    loop++;
                    loopCount++;
                }
                reader.Close(); // Close the open file
                textBox1.DeselectAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 187: Sorry an error has occured: " + ex.Message);
            }
        }
    }
}
