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
    public partial class NewClaimsPg2 : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        public string make = Version.Make;
        public string model = Version.Model;
        public string serial = Version.Serial;
        public static bool but2press;
        public static string NextClaimNum;

        public NewClaimsPg2()
        {
            InitializeComponent();
            Icon = image100;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
            but2press = NewClaim.but2press;
            NextClaimNum = NewClaim.NextClaimNum;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Hide();
            NewClaim f2 = new NewClaim();
            f2.Show();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Hide();
            EnterServiceCustMenu f2 = new EnterServiceCustMenu();
            f2.Show();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }

        private void NewClaimsPg2_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void NewClaimsPg2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Hide();
            NewClaim f2 = new NewClaim();
            f2.Show();
        }
    }
}
