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
    public partial class OpenClaimsMenu : Form
    {
        public OpenClaimsMenu()
        {
            InitializeComponent();
        }

        private void Button5_Click(object sender, EventArgs e)  // Return
        {
            Hide();
            ClaimsMGTMenu f2 = new ClaimsMGTMenu();
            f2.Show();
        }

        private void Button4_Click(object sender, EventArgs e)  // Main Menu
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }

        private void Button1_Click(object sender, EventArgs e)  // List / Print Open Claims
        {
            Hide();
            ListOpenClaims f2 = new ListOpenClaims();
            f2.Show();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Hide();
            ListOpenClaims2 f2 = new ListOpenClaims2();
            f2.Show();
        }

        private void button8_Click(object sender, EventArgs e)  // Daily report by technician
        {
            Hide();
            DailyReportByTech f2 = new DailyReportByTech();
            f2.Show();
        }

        private void button7_Click(object sender, EventArgs e)  // Monthly Statistics Information
        {

        }

        private void button6_Click(object sender, EventArgs e)  // Monthly to Date Operation Report
        {

        }

        private void button11_Click(object sender, EventArgs e) // Year to Date Operation Report
        {

        }

        private void button10_Click(object sender, EventArgs e) // Client/Dealer Billing Statements Report
        {

        }

        private void button9_Click(object sender, EventArgs e)  // Accounts Receivable Menu
        {

        }
    }
}
