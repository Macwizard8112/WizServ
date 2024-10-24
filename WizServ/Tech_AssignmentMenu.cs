using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Printing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.Windows.Forms;

namespace WizServ
{
    public partial class Tech_AssignmentMenu : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        private int loopCount, loop;
        private readonly string DATABASE = @"I:\\Datafile\\Control\\Database.CSV";
        private readonly string PREVIOUS = @"I:\\Datafile\\Control\\Prev_Tech_Assign.CSV";
        private readonly string TECHS = @"I:\\Datafile\\Control\\Tech_Names.CSV";
        public string claim_no = Version.Claim;
        public bool FOUND = false;
        public string PCLAIM, PCURRENT, PPREV1, PPREV2, PPREV3, PPREV4, PPREV5, PDATE, SELECTED;
        public bool PFOUND = false, blnReturn = false;

        public Tech_AssignmentMenu()
        {
            InitializeComponent();
            button7.Visible = true;
            Icon = image100;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
            label3.Visible = false;
            textBox2.Visible = false;
           // HideButtons();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Hide();
            Reassign f2 = new Reassign();
            f2.Show();
        }

        private void HideButtons()
        {
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            button5.Visible = false;
        }
        private void ShowButtons()
        {
            button1.Visible = true;
            button2.Visible = true;
            button3.Visible = true;
            button4.Visible = true;
            button5.Visible = true;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox1.Text == "911")
                {
                    label2.Visible = false;
                    textBox1.Visible = false;
                    ShowButtons();
                }
                else
                {
                    textBox1.Text = "";
                    textBox1.Select();
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            EstimateApprovalRPT f2 = new EstimateApprovalRPT();
            f2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            GenerateEstimateReport f2 = new GenerateEstimateReport();
            f2.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label3.Visible = true;
            textBox2.Visible = true;
            textBox2.Select();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            Hide();
            TechAssignment f2 = new TechAssignment();
            //OpenTechAssignments f2 = new OpenTechAssignments();
            f2.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Hide();
            ClaimStatusReport f2 = new ClaimStatusReport();
            f2.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox2.TextLength == 6)
                {
                    Version.Claim = textBox2.Text;
                    Hide();
                    TechAssignment f2 = new TechAssignment();
                    f2.Show();
                }
            }
        }
    }
}
