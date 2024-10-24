using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WizServ
{
    public partial class ClientDealerReport : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        private readonly string file = @"I:\\Datafile\\Control\\Database.CSV";
        private string claim_no, fname, lname, addr, city, state, zip, hphone, wphone;
        private bool war_prd;
        private DateTime datein;
        private int loopCount, loop;
        public string Lines = "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━";

        public ClientDealerReport()
        {
            InitializeComponent();
            Icon = image100;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            ClaimsMGTMenu f2 = new ClaimsMGTMenu();
            f2.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }
    }
}
