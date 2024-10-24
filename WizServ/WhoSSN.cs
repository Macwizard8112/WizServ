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
    public partial class WhoSSN : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        public readonly string file2 = @"I:\\Datafile\\Control\\Notified.CSV";
        public readonly string tech_assign = @"I:\\Datafile\\Control\\Tech_Assign.CSV";
        private readonly string database = @"I:\\Datafile\\Control\\Database.CSV";
        public bool PASS1 = false;
        public bool PASS2 = false;
        public string who = Version.WHO;
        public string ssn = Version.SSN;
        public string tech = Version.TECH;
        public string TheSelectedText, approved;
        public string zClaim_NO = GenerateEstimateReport.zClaim_NO;
        public string zDate_IN = GenerateEstimateReport.zDate_IN;
        public string zWar_Note = GenerateEstimateReport.zWar_Note;
        public string zBench = GenerateEstimateReport.zBench;
        public string zWHLoc = GenerateEstimateReport.zWHLoc;
        public string zTheTech = GenerateEstimateReport.zTheTech;
        public string zIsWarr = GenerateEstimateReport.zIsWarr;
        public string zEstimate = GenerateEstimateReport.zEstimate;
        public string zRush = GenerateEstimateReport.zRush;
        public string claim_no;
        public bool Found, hasrun;
        public static int loop;

        public WhoSSN()
        {
            InitializeComponent();
            TheSelectedText = Version.SELECTEDTEXT;
            claim_no = TheSelectedText;
            approved = Version.APPROVED;
            Icon = image100;
            MaximizeBox = false;
            MinimizeBox = false;
            ControlBox = true;
            textBox1.Select();
        }

        public void AddNewLine()
        {
            hasrun = true;
            loop++;
            string zClaim_NO = GenerateEstimateReport.zClaim_NO;
            string zDate_IN = GenerateEstimateReport.zDate_IN;
            string zWar_Note = GenerateEstimateReport.zWar_Note;
            string zBench = GenerateEstimateReport.zBench;
            string zWHLoc = GenerateEstimateReport.zWHLoc;
            string zTheTech = GenerateEstimateReport.zTheTech;
            string zIsWarr = GenerateEstimateReport.zIsWarr;
            string zEstimate = GenerateEstimateReport.zEstimate;
            string zRush = GenerateEstimateReport.zRush;
            string ninth = zRush;
            if (loop == 1)
            {
                try
                {
                    using (FileStream fs = new FileStream(tech_assign, FileMode.Append, FileAccess.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(fs))
                        {
                            sw.WriteLine(zClaim_NO + "," + zDate_IN + "," + zWar_Note + "," + zTheTech + "," + zBench + "," + zWHLoc + "," + zIsWarr + "," + zEstimate + "," + ninth);
                        }
                    }
                }
                
                catch (Exception ex)
                {
                    MessageBox.Show("Error occured: Line 78: \n" + ex);
                }
                loop++;
            }
        }

        public void RecordEstimates()              // Edit CSV File - Mark as Customer Notified of Estimate / Collected Cash / Card
        {
            string path = file2;
            string theDate = DateTime.Now.ToString("MM/dd/yyyy");
            string TheTime = DateTime.Now.ToString("HH:mm:ss");
            try
            {
                who = textBox1.Text;
                ssn = textBox2.Text;
                using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.WriteLine(TheSelectedText + "," + theDate + "," + TheTime + "," + who + "," + ssn + "," + approved + "," + Version.TECH);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured: Line 103: \n" + ex);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            hasrun = false;
            if (textBox1.Text.Length == 0)
            {
                MessageBox.Show("Sorry, Name can not be blank.");
                textBox1.Select();
                PASS1 = false;
            }
            if (textBox2.Text.Length == 0)
            {
                MessageBox.Show("Sorry, SSN can not be blank.");
                textBox1.Select();
                PASS2 = false;
            }
            if (textBox1.Text.Length >0)
            {
                PASS1 = true;
            }
            if (textBox2.Text.Length > 0)
            {
                PASS2 = true;
            }
            if (PASS1 == true || PASS2 == true)
            {
                hasrun = false;
                Version.WHO = textBox1.Text.Substring(0,4);
                Version.SSN = textBox2.Text.Substring(0,4);
                RecordEstimates();
                AddNewLine();
                Version.CLOSED = true;
                this.Close();
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Version.WHO = textBox1.Text;
                if (textBox1.Text.Length > 0)
                {
                    PASS1 = true;
                    Version.WHO = textBox1.Text;
                    textBox2.Select();
                }
                if (textBox1.Text.Length == 0)
                {
                    MessageBox.Show("Sorry, Name can not be blank.");
                    textBox1.Select();
                }
            }
            if (e.KeyCode == Keys.Tab)
            {
                Version.WHO = textBox1.Text;
                if (textBox1.Text.Length > 0)
                {
                    PASS1 = true;
                    Version.WHO = textBox1.Text;
                    textBox2.Select();
                }
                if (textBox1.Text.Length == 0)
                {
                    MessageBox.Show("Sorry, Name can not be blank.");
                    textBox1.Select();
                }
            }
        }

        private void WhoSSN_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Version.WHO = textBox1.Text;
                if (textBox2.Text.Length > 0)
                {
                    PASS1 = true;
                    Version.SSN = textBox2.Text;
                    button1.Select();
                }
                if (textBox2.Text.Length == 0)
                {
                    MessageBox.Show("Sorry, SSN can not be blank.");
                    textBox2.Select();
                }
                button1.Select();
            }
            if (e.KeyCode == Keys.Tab)
            {
                Version.WHO = textBox1.Text;
                if (textBox2.Text.Length > 0)
                {
                    PASS1 = true;
                    Version.SSN = textBox2.Text;
                    button1.Select();
                }
                if (textBox2.Text.Length == 0)
                {
                    MessageBox.Show("Sorry, SSN can not be blank.");
                    textBox2.Select();
                }
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Version.WHO = textBox1.Text;
                textBox2.Select();
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            string st = "0123456789" + (char)8 + (char)13;
            if (st.IndexOf(e.KeyChar) == -1)
            {
                MessageBox.Show("please enter digits only");
                e.Handled = true;
            }
            if (e.KeyChar == 13)
            {
                Version.WHO = textBox1.Text;
                Version.SSN = textBox2.Text;
                button1.Select();
            }
        }
    }
}
