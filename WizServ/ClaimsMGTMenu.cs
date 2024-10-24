using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Media;
using Microsoft.Win32;

namespace WizServ
{
    public partial class ClaimsMGTMenu : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        public string butpress;
        //string msg = "2.50 hours Disassembled, Diagnosed xxx";
        //private string ans;
        //private readonly string file = @"I:\\Datafile\\Control\\Database.CSV";
        //private readonly string Related = @"I:\\Datafile\\Control\\Related.CSV";
        static readonly string key = @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\lanmanserver\parameters";
        public readonly string computerDescription = (string)Registry.GetValue(key, "srvcomment", null);
        private readonly string claim_no;        // fname, lname, addr, city, state, zip, hphone, wphone;
        //private bool war_prd;
       // private DateTime datein;
        //private int loopCount, loop;
        //private string IsClosed = "";
        public bool Found = false;
        private readonly SoundPlayer Player = new SoundPlayer();
        public string IsWarranty, TheBrand, s;
        public bool IsError = false;

        public ClaimsMGTMenu()
        {
            InitializeComponent();
            claim_no = Version.Claim;
            label1.Text = claim_no;
            label6.Visible = false;
            textBox1.Visible = false;
            Icon = image100;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
            label2.Visible = false;
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            Hide();
            RePrint f2 = new RePrint();
            f2.Show();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            label6.Visible = true;
            textBox1.Visible = true;
            textBox1.Select();
            butpress = "5";
        }

        private void Button4_Click(object sender, EventArgs e)
        {

        }

        private void Button3_Click(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            label6.Visible = true;
            textBox1.Visible = true;
            textBox1.Select();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            Version.From = "ClaimsMGTMenu";
            Hide();
            Warehouse f2 = new Warehouse();
            f2.Show();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            Version.From = "ClaimsMGTMenu";
            Hide();
            EstimateRanges f2 = new EstimateRanges();
            f2.Show();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            Version.From = "ClaimsMGTMenu";
            Hide();
            StopPhoneCalls f2 = new StopPhoneCalls();
            f2.Show();
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (butpress == "17")
                {
                    Version.Claim = textBox1.Text;
                    Hide();
                    EditServices f2 = new EditServices();
                    f2.Show();
                    return;
                }
                if (butpress == "5")
                {
                    Version.Claim = textBox1.Text;
                    Hide();
                    ReprintClaim f0 = new ReprintClaim();
                    f0.Show();
                    return;
                }
                if (butpress == "15")
                {
                    Version.Claim = textBox1.Text;
                    Hide();
                    _Render f2 = new _Render();
                    f2.Show();
                    return;
                }
                label1.Text = textBox1.Text;
                if (textBox1.TextLength <= 5)
                {
                    return;
                }
                if (textBox1.TextLength >= 8)
                {
                    return;
                }
                if (textBox1.Text.StartsWith("R"))
                {
                    Version.Claim = textBox1.Text;
                    Hide();
                    _Render f2 = new _Render();
                    f2.Show();
                }
                else
                {
                    Version.Claim = textBox1.Text;
                    Hide();
                    EditClaimMenu f2 = new EditClaimMenu();
                    f2.Show();
                }
            }
        }

        private void Button8_Click(object sender, EventArgs e)  // List all Open Claims
        {
            label6.Visible = false;
            textBox1.Visible = false;
            Version.From = "CalimsMGTMenu";
            Hide();
            Password f2 = new Password();
            f2.Show();
        }

        private void button9_Click(object sender, EventArgs e)  // By Client/Dealer Report
        {
            Hide();
            ClientDealerReport f2 = new ClientDealerReport();
            f2.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Hide();
            EstimateReport f2 = new EstimateReport();
            f2.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Hide();
            RenderReport f2 = new RenderReport();
            f2.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Hide();
            VP10Menu f2 = new VP10Menu();
            f2.Show();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Version.Estimate = "Y";
            Version.From = "CalimsMGTMenu";
            Hide();
            Password f2 = new Password();
            f2.Show();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Hide();
            EstimateReports f2 = new EstimateReports();
            f2.Show();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            butpress = "15";
            label6.Visible = true;
            label2.Visible = true;
            textBox1.Visible = true;
            textBox1.Select();
            Version.Claim = textBox1.Text;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            label6.Visible = true;
            textBox1.Visible = true;
            textBox1.Select();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            label6.Visible = true;
            textBox1.Visible = true;
            textBox1.Select();
            butpress = "5";
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Hide();
            RePrint f2 = new RePrint();
            f2.Show();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Hide();
            EstimateReports f2 = new EstimateReports();
            f2.Show();
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            label6.Visible = true;
            textBox1.Visible = true;
            textBox1.Select();
            Version.Claim = textBox1.Text;
            butpress = "17";
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            butpress = "15";
            label6.Visible = true;
            label2.Visible = true;
            textBox1.Visible = true;
            textBox1.Select();
            Version.Claim = textBox1.Text;
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            Version.From = "ClaimsMGTMenu";
            Hide();
            EstimateRanges f2 = new EstimateRanges();
            f2.Show();
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            label6.Visible = false;
            textBox1.Visible = false;
            Version.From = "CalimsMGTMenu";
            Hide();
            Password f2 = new Password();
            f2.Show();
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            Hide();
            ClientDealerReport f2 = new ClientDealerReport();
            f2.Show();
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            Hide();
            EstimateReport f2 = new EstimateReport();
            f2.Show();
        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {
            Hide();
            RenderReport f2 = new RenderReport();
            f2.Show();
        }

        private void pictureBox17_Click(object sender, EventArgs e)
        {
            Hide();
            VP10Menu f2 = new VP10Menu();
            f2.Show();
        }

        private void pictureBox18_Click(object sender, EventArgs e)
        {
            Version.Estimate = "Y";
            Version.From = "CalimsMGTMenu";
            Hide();
            Password f2 = new Password();
            f2.Show();
        }

        private void pictureBox19_Click(object sender, EventArgs e)
        {
            Hide();
            GoldCustMenu f2 = new GoldCustMenu();
            f2.Show();
        }

        private void pictureBox20_Click(object sender, EventArgs e)
        {
            Version.From = "ClaimsMGTMenu";
            Hide();
            Warehouse f2 = new Warehouse();
            f2.Show();
        }

        private void pictureBox21_Click(object sender, EventArgs e)
        {
            Version.From = "ClaimsMGTMenu";
            Hide();
            StopPhoneCalls f2 = new StopPhoneCalls();
            f2.Show();
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Hide();
            GoldCustMenu f2 = new GoldCustMenu();
            f2.Show();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            label6.Visible = true;
            textBox1.Visible = true;
            textBox1.Select();
            Version.Claim = textBox1.Text;
            butpress = "17";
        }
    }
}
