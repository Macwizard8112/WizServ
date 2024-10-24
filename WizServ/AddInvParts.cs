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
    public partial class AddInvParts : Form
    {
        public AddInvParts()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Hide();
            InventoryMenu f2 = new InventoryMenu();
            f2.Show();
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Hide();
                InventoryMenu f2 = new InventoryMenu();
                f2.Show();
            }
        }

        private void AddInvParts_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void AddInvParts_FormClosed(object sender, FormClosedEventArgs e)
        {
            Hide();
            InventoryMenu f2 = new InventoryMenu();
            f2.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {

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

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            Hide();
            InventoryMenu f2 = new InventoryMenu();
            f2.Show();
        }
    }
}
