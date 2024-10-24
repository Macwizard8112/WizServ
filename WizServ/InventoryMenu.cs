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
    public partial class InventoryMenu : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        public int parts = Version.totParts;
        public decimal partscost = Version.totPartsCost;
        public decimal sellcost = Version.totSellCost;

        public InventoryMenu()
        {
            InitializeComponent();
            Icon = image100;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
            SetButtonText();
            CheckOnValues();
        }

        private void SetButtonText()
        {
            // Parts Used for this Year
            var tt = DateTime.Now;
            var rr = tt.Year;
            var ss = rr - 1;
            button10.Text = "Parts Used for this Year " + rr.ToString();
            button17.Text = "Parts Used for Last Year " + ss.ToString();
        }

        private void CheckOnValues()
        {
            if (parts <= 0)
            {
                label5.Visible = false;
                label6.Visible = false;
                label7.Visible = false;
                label8.Visible = false;
                label9.Visible = false;
                label10.Visible = false;
            }
            else
            {
                label5.Visible = true;
                label6.Visible = true;
                label7.Visible = true;
                label8.Visible = true;
                label9.Visible = true;
                label10.Visible = true;
                label8.Text = parts.ToString();
                label9.Text = partscost.ToString("C2");
                label10.Text = sellcost.ToString("C2");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            AddInvParts f2 = new AddInvParts();
            f2.Show();
        }

        private void button15_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Hide();
            Full_INV_Report f2 = new Full_INV_Report();
            f2.Show();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Hide();
                MainMenu f2 = new MainMenu();
                f2.Show();
            }
        }

        private void InventoryMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void InventoryMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Hide();
            PartsOnOrder f2 = new PartsOnOrder();
            f2.Show();
        }

        private void addViewUpdateInventoryPartsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            AddInvParts f2 = new AddInvParts();
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

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }

        private void fullInventoryListTotalValueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            Full_INV_Report f2 = new Full_INV_Report();
            f2.Show();
        }

        private void allPartsCurrentlyOnOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            PartsOnOrder f2 = new PartsOnOrder();
            f2.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Hide();
            PartsUsedThisYear f2 = new PartsUsedThisYear();
            f2.Show();
        }

        private void returnToMainMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }

        private void partsUsedForThisYearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            PartsUsedThisYear f2 = new PartsUsedThisYear();
            f2.Show();
        }

        private void partsUsedForLastYearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            PartsUsedLastYear f2 = new PartsUsedLastYear();
            f2.Show();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            Hide();
            PartsUsedLastYear f2 = new PartsUsedLastYear();
            f2.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Hide();
            InvLowLimit f2 = new InvLowLimit();
            f2.Show();
        }

        private void listPartsAtorBelowLowLimitsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            InvLowLimit f2 = new InvLowLimit();
            f2.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Hide();
            InvPriSecRpt f2 = new InvPriSecRpt();
            f2.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }
    }
}
