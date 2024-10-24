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
    public partial class CustStatusMenu : Form
    {
        public Icon image100 = Properties.Resources.WizServ;

        public CustStatusMenu()
        {
            InitializeComponent();
            Icon = image100;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = false;
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            Version.From = "CustStatus";
            Hide();
            ClientDealerReports f2 = new ClientDealerReports();
            f2.Show();
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Hide();
            OpenClaimsByTech f2 = new OpenClaimsByTech();
            f2.Show();
        }
    }
}
