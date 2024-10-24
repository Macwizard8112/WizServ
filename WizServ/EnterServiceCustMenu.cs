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
    public partial class EnterServiceCustMenu : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        public string from;
        private readonly string NextClaim = @"I:\\Datafile\\Control\\NextClaim.CSV";
        private int loopCount;
        public string A, nextClaim, yeardigit;

        public EnterServiceCustMenu()
        {
            InitializeComponent();
            Icon = image100;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
            from = Version.From;
            GetNextClaim();
        }

        public void GetNextClaim()                // Get / SHow next Claim # on screen
        {
            var date = DateTime.Now.ToShortDateString();
            var len = date.Length;
            var year = date.Substring((len - 2), 2);
            yeardigit = year;
            try
            {
                var lines = File.ReadLines(NextClaim);

                foreach (string line in lines)
                {
                    if (line != "")
                    {
                        nextClaim = line;
                        var yy = line;
                        var tt = yy.Substring(1, (yy.Length)-1);
                        nextClaim = tt;
                        nextClaimToolStripMenuItem.Text = "Last Claim #: " + yeardigit + nextClaim; 
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 49: Sorry an error has occured: " + ex.Message);
            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }

        public void Button1_Click(object sender, EventArgs e)  // Out of Warranty
        {
            from = "ENTERSERVICECUSTMENU";
            Version.From = from;
            Hide();
            DoNotRepairSelect f2 = new DoNotRepairSelect();
            f2.Show();
            //Hide();
            //NameLookup f2 = new NameLookup();
            //f2.Show();
        }

        private void Button2_Click(object sender, EventArgs e)  // Under Manufacturer Warranty
        {
            Hide();
            NameLookup f2 = new NameLookup();
            f2.Show();
        }

        private void Button3_Click(object sender, EventArgs e)  // Recall for Work Performed
        {
            Hide();
            SearchOldClaims f2 = new SearchOldClaims();
            f2.Show();
        }

        private void Button4_Click(object sender, EventArgs e)  // Lookup Customer
        {

        }

        private void Button5_Click(object sender, EventArgs e)  // Future
        {
            Hide();
            DoNotRepairSelect f2 = new DoNotRepairSelect();
            f2.Show();
        }

        private void Button6_Click(object sender, EventArgs e)  // Future
        {

        }

        private void EnterServiceCustMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void EnterServiceCustMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }

        private void Button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                Close();
            }
            if (e.KeyData == Keys.A)
            {
                from = "ENTERSERVICECUSTMENU";
                Version.From = from;
                Hide();
                DoNotRepairSelect f2 = new DoNotRepairSelect();
                f2.Show();
            }
            if (e.KeyData == Keys.B)
            {
                Hide();
                NameLookup f2 = new NameLookup();
                f2.Show();
            }
            if (e.KeyData == Keys.C)
            {
                Hide();
                NameLookup f2 = new NameLookup();
                f2.Show();
            }
            if (e.KeyData == Keys.D)
            {
                Hide();
                NameLookup f2 = new NameLookup();
                f2.Show();
            }
            if (e.KeyData == Keys.E)
            {
                Hide();
                DoNotRepairSelect f2 = new DoNotRepairSelect();
                f2.Show();
            }
            if (e.KeyData == Keys.F)
            {

            }
            if (e.KeyData == Keys.Q)
            {
                Hide();
                MainMenu f2 = new MainMenu();
                f2.Show();
            }
        }

        private void createNewClaimToolStripMenuItem_Click(object sender, EventArgs e)
        {
            from = "ENTERSERVICECUSTMENU";
            Version.From = from;
            Hide();
            DoNotRepairSelect f2 = new DoNotRepairSelect();
            f2.Show();
        }

        private void underManufacturersWarrantyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            NameLookup f2 = new NameLookup();
            f2.Show();
        }

        private void recallForWorkPerformedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            NameLookup f2 = new NameLookup();
            f2.Show();
        }

        private void lookupCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            NameLookup f2 = new NameLookup();
            f2.Show();
        }

        private void dONOTREPAIRLISTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            DoNotRepairSelect f2 = new DoNotRepairSelect();
            f2.Show();
        }

        private void futureToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            Version.From = "EnterServiceCustMenu";
            Hide();
            NextClaimNum f2 = new NextClaimNum();
            f2.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            from = "ENTERSERVICECUSTMENU";
            Version.From = from;
            Hide();
            DoNotRepairSelect f2 = new DoNotRepairSelect();
            f2.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Hide();
            NameLookup f2 = new NameLookup();
            f2.Show();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Hide();
            DoNotRepairSelect f2 = new DoNotRepairSelect();
            f2.Show();
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }

        private void returnToMainMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }

        private void nextClaimToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nextClaimToolStripMenuItem.Text = "Last Claim #: " + yeardigit + nextClaim;
        }
    }
}
