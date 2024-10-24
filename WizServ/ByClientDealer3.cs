using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WizServ
{
    public partial class ByClientDealer3 : Form
    {
        public string claim_no = Version.Claim;
        public string calledfrom = Version.From;

        public ByClientDealer3()
        {
            InitializeComponent();
            label1.Text = "Dealer # " + claim_no;
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Hide();
            ByClientDealer4 f2 = new ByClientDealer4();
            f2.Show();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Hide();
            ByClientDealer4 f2 = new ByClientDealer4();
            f2.Show();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Hide();
            ByClientDealer f2 = new ByClientDealer();
            f2.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Hide();
            ByClientDealer4 f2 = new ByClientDealer4();
            f2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Hide();
            ByClientDealer4 f2 = new ByClientDealer4();
            f2.Show();
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            Hide();
            ByClientDealer f2 = new ByClientDealer();
            f2.Show();
        }
    }
}
