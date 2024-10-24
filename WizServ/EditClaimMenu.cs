using System;
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
    public partial class EditClaimMenu : Form
    {
        public string claim_no;

        public EditClaimMenu()
        {
            InitializeComponent();
            claim_no = Version.Claim;
            label7.Text = "Claim: " + claim_no;
        }

        private void editCustomerInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void editDealerInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void editProblemServicesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void returnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            ClaimsMGTMenu f2 = new ClaimsMGTMenu();
            f2.Show();
        }

        private void mainMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            EditCustInfo f2 = new EditCustInfo();
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
            Hide();
            ClaimsMGTMenu f2 = new ClaimsMGTMenu();
            f2.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.Handled = true;
                Hide();
                ClaimsMGTMenu f2 = new ClaimsMGTMenu();
                f2.Show();
            }
        }


        private void EditClaimMenu_Load(object sender, EventArgs e)
        {

        }
    }
}
