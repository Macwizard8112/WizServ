using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Media;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace WizServ
{
    public partial class Warranty : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        public string from, warranty;
        public bool iswarr, assurion;
        private readonly string Related = @"I:\\Datafile\\Control\\Related.CSV";
        static readonly string key = @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\lanmanserver\parameters";
        public readonly string computerDescription = (string)Registry.GetValue(key, "srvcomment", null);
        private string msg = "    Wizard Electronics\nEnter New Claim Menu.";

        public Warranty()
        {
            InitializeComponent();
            label1.Text = msg;
            Icon = image100;
            from = Version.From;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            iswarr = true;
            warranty = "Yes";
            assurion = false;
            Version.Warranty = warranty;
            Version.IsWarr = iswarr;
            Hide();
            NameLookup f2 = new NameLookup();
            f2.Show();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            iswarr = false;
            warranty = "No";
            Version.Warranty = warranty;
            Version.IsWarr = iswarr;
            Hide();
            NameLookupChars f2 = new NameLookupChars();
            f2.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            iswarr = true;
            warranty = "Yes";
            assurion = true;
            Version.Assurion = assurion;
            Version.Warranty = warranty;
            Version.IsWarr = iswarr;
            Hide();
            NameLookup f2 = new NameLookup();
            f2.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }
    }
}
