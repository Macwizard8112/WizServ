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
    public partial class VP10Menu : Form
    {
        public Icon image100 = Properties.Resources.WizServ;

        public VP10Menu()
        {
            InitializeComponent();
            Icon = image100;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
        }

        private void button1_Click(object sender, EventArgs e)  // Enter New Vendor
        {

        }

        private void button2_Click(object sender, EventArgs e)  // Edit Existing Vendor
        {
            Hide();
            EditExstingPO f2 = new EditExstingPO();
            f2.Show();
        }

        private void button3_Click(object sender, EventArgs e)  // Print Vendir List
        {

        }

        private void button4_Click(object sender, EventArgs e)  // Create New PO
        {

        }

        private void button5_Click(object sender, EventArgs e)  // Edit Existing PO
        {

        }

        private void button6_Click(object sender, EventArgs e)  // Print / RePrint PO
        {

        }

        private void button7_Click(object sender, EventArgs e)  // Return
        {
            Hide();
            ClaimsMGTMenu f2 = new ClaimsMGTMenu();
            f2.Show();
        }

        private void button8_Click(object sender, EventArgs e)  // Main Menu
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }
    }
}
