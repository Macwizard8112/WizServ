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
    public partial class ARMenu : Form
    {
        public Icon image100 = Properties.Resources.WizServ;

        public ARMenu()
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(0, 132, 129);
            Icon = image100;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Hide();
            OpenClaimsMenu f2 = new OpenClaimsMenu();
            f2.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }

        private void MenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void LISTPRINTOPENCLAIMSToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                MessageBox.Show("'A' pressed.");
            }

            if (e.KeyCode == Keys.G)
            {
                Hide();
                MainMenu f2 = new MainMenu();
                f2.Show();
            }
            if (e.KeyCode == Keys.Q)
            {
                Hide();
                OpenClaimsMenu f2 = new OpenClaimsMenu();
                f2.Show();
            }
            if (e.KeyCode == Keys.Escape)
            {
                Hide();
                MainMenu f2 = new MainMenu();
                f2.Show();
            }
        }

        private void GotoMainMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }

        private void ReturnToPreviousMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            OpenClaimsMenu f2 = new OpenClaimsMenu();
            f2.Show();
        }

        private void ARMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void ARMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }

        private void openClaimsSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void openClaimsSummaryToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void dailyProgressReportByTechnicianToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void monthlyStatisticsInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void monthlyToDateOperationReportToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void yearToDateOperationReportToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            Hide();
            OpenClaimsMenu f2 = new OpenClaimsMenu();
            f2.Show();
        }
    }
}
