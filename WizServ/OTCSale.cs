using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WizServ
{
    public partial class OTCSale : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        private int loopCount;
        string path = @"I:\Datafile\Control\StateAbbr.csv";
        public string mSTATE, mName, mAddr, mCity, mState, mZip, mPhone, mEmail;
        public int Qty1, Qty2, Qty3, Qty4, Qty5, Qty6, Qty7, Qty8, Qty9, Qty10, initial;
        public decimal Pr1, Pr2, Pr3, Pr4, Pr5, Pr6, Pr7, Pr8, Pr9, Pr10;
        public decimal Tt1, Tt2, Tt3, Tt4, Tt5, Tt6, Tt7, Tt8, Tt9, Tt10;
        public decimal Gt1, Gt2, Gt3, Gt4, Gt5, Gt6, Gt7, Gt8, Gt9, Gt10, SubTotal, GATax, GrandTotal;
        public bool instock;

        public OTCSale()
        {
            InitializeComponent();
            Icon = image100;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
            textBox1.Select();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }


        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox1.TextLength == 0)
                {
                    MessageBox.Show("Name can't be blank.");
                    textBox1.Select();
                }
                else
                {
                    mName = textBox1.Text;
                    mName.Replace(",", "");
                    textBox2.Select();
                }
            }
            
        }


        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                mAddr = textBox2.Text;
                mAddr.Replace(",", "");
                textBox3.Select();
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                mCity = textBox3.Text;
                comboBox1.Select();
            }
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox5.Select();
            }
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                mSTATE = comboBox1.Text;
                textBox4.Select();
            }
            if (e.KeyCode == Keys.Enter)
            {
                mSTATE = comboBox1.Text;
                textBox4.Select();
            }
            textBox4.Select();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            Qty1 = Convert.ToInt32(comboBox2.Text);
            textBox6.Select();

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            Qty2 = Convert.ToInt32(comboBox3.Text);
            textBox11.Select();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            Qty3 = Convert.ToInt32(comboBox4.Text);
            textBox14.Select();
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            Qty4 = Convert.ToInt32(comboBox5.Text);
            textBox17.Select();
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            Qty5 = Convert.ToInt32(comboBox6.Text);
            textBox20.Select();
        }

        private void comboBox2_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;
            if (comboBox2.Text  != "0")
            {
                Qty1 = Convert.ToInt32(comboBox2.Text);
                textBox6.Select();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (initial == 0)
            {
                return;
            }
            initial++;
            mState = comboBox1.Text;

        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            Qty6 = Convert.ToInt32(comboBox7.Text);
            textBox23.Select();
        }

        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            Qty7 = Convert.ToInt32(comboBox8.Text);
            textBox26.Select();
        }

        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            Qty8 = Convert.ToInt32(comboBox9.Text);
            textBox29.Select();
        }

        private void comboBox10_SelectedIndexChanged(object sender, EventArgs e)
        {
            Qty9 = Convert.ToInt32(comboBox10.Text);
            textBox32.Select();
        }

        private void comboBox11_SelectedIndexChanged(object sender, EventArgs e)
        {
            Qty10 = Convert.ToInt32(comboBox11.Text);
            textBox35.Select();
        }


        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)    // Enter
            {
                mSTATE = comboBox1.Text;
                textBox4.Select();
            }
            if (e.KeyChar == 9) // Tab
            {
                mSTATE = comboBox1.Text;
                textBox4.Select();
            }
        }


        private void OTCSale_Load(object sender, EventArgs e)
        {
            string message = "Do we have ALL parts in-stock?";
            string title = "Parts Check";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {
            }
            else
            {
                MessageBox.Show("Create a new Claim instead !!");
                instock = false;
                try
                {
                    this.Close();
                    Hide();
                    MainMenu f2 = new MainMenu();
                    f2.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception \n" + ex);
                }
            }

            comboBox1.SelectedIndex = 10;

            for (int i = 0; i < 11; i++)   // ComboBox 2 - Quantity
            {
                comboBox2.Items.Add(i);
                comboBox3.Items.Add(i);
                comboBox4.Items.Add(i);
                comboBox5.Items.Add(i);
                comboBox6.Items.Add(i);
                comboBox7.Items.Add(i);
                comboBox8.Items.Add(i);
                comboBox9.Items.Add(i);
                comboBox10.Items.Add(i);
                comboBox11.Items.Add(i);
            }
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
            comboBox5.SelectedIndex = 0;
            comboBox6.SelectedIndex = 0;
            comboBox7.SelectedIndex = 0;
            comboBox8.SelectedIndex = 0;
            comboBox9.SelectedIndex = 0;
            comboBox10.SelectedIndex = 0;
            comboBox11.SelectedIndex = 0;
            Setup();
            textBox1.Select();
        }

        private void Setup()
        {
            textBox6.Text = "0.00";
            textBox11.Text = "0.00";
            textBox14.Text = "0.00";
            textBox17.Text = "0.00";
            textBox20.Text = "0.00";
            textBox23.Text = "0.00";
            textBox26.Text = "0.00";
            textBox29.Text = "0.00";
            textBox32.Text = "0.00";
            textBox35.Text = "0.00";
        }

        private void textBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Tab)
            {
                e.IsInputKey = true;
                if (textBox1.TextLength == 0)
                {
                    MessageBox.Show("Name can't be blank.");
                    textBox1.Select();
                }
                else
                {
                    mName = textBox1.Text;
                    textBox2.Select();
                }
            }
        }

        private void textBox2_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Tab)
            {
                e.IsInputKey = true;
                if (textBox2.TextLength == 0)
                {
                    string caption = "Error:";
                    MessageBox.Show("Addr can't be blank." , caption);
                    textBox2.Select();
                }
                else
                {
                    mAddr = textBox2.Text;
                    textBox3.Select();
                }
            }
        }

        private void textBox3_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Tab)
            {
                e.IsInputKey = true;
                mCity = textBox3.Text;
                comboBox1.Select();
            }
        }

        private void comboBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Tab)
            {
                e.IsInputKey = true;
                textBox4.Select();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            initial = 0;
            button3.PerformClick();
            button2.BackColor = Color.Gold;
        }

        private void textBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pr1 = Convert.ToDecimal(textBox6.Text);
                textBox6.Text = Pr1.ToString("0.00");
                textBox7.Select();
            }
        }

        private void textBox6_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Tab)
            {
                e.IsInputKey = true;
                Pr1 = Convert.ToDecimal(textBox6.Text);
                textBox6.Text = Pr1.ToString("0.00");
                textBox7.Select();
            }
        }

        private void textBox11_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pr2 = Convert.ToDecimal(textBox11.Text);
                textBox11.Text = Pr2.ToString("0.00");
                textBox10.Select();
            }
        }

        private void textBox11_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Tab)
            {
                e.IsInputKey = true;
                Pr2 = Convert.ToDecimal(textBox11.Text);
                textBox11.Text = Pr2.ToString("0.00");
                textBox10.Select();
            }
        }

        private void textBox14_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pr3 = Convert.ToDecimal(textBox14.Text);
                textBox14.Text = Pr3.ToString("0.00");
                textBox13.Select();
            }
        }

        private void textBox14_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Tab)
            {
                e.IsInputKey = true;
                Pr3 = Convert.ToDecimal(textBox14.Text);
                textBox14.Text = Pr3.ToString("0.00");
                textBox13.Select();
            }
        }

        private void textBox17_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pr4 = Convert.ToDecimal(textBox17.Text);
                textBox17.Text = Pr4.ToString("0.00");
                textBox16.Select();
            }
        }

        private void textBox17_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Tab)
            {
                e.IsInputKey = true;
                Pr4 = Convert.ToDecimal(textBox17.Text);
                textBox17.Text = Pr4.ToString("0.00");
                textBox16.Select();
            }
        }

        private void textBox20_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pr5 = Convert.ToDecimal(textBox20.Text);
                textBox20.Text = Pr5.ToString("0.00");
                textBox19.Select();
            }
        }

        private void textBox20_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Tab)
            {
                e.IsInputKey = true;
                Pr5 = Convert.ToDecimal(textBox20.Text);
                textBox20.Text = Pr5.ToString("0.00");
                textBox19.Select();
            }
        }

        private void textBox23_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pr6 = Convert.ToDecimal(textBox23.Text);
                textBox23.Text = Pr6.ToString("0.00");
                textBox22.Select();
            }
        }

        private void textBox23_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Tab)
            {
                e.IsInputKey = true;
                Pr6 = Convert.ToDecimal(textBox23.Text);
                textBox23.Text = Pr6.ToString("0.00");
                textBox22.Select();
            }
        }

        private void textBox26_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pr7 = Convert.ToDecimal(textBox26.Text);
                textBox26.Text = Pr7.ToString("0.00");
                textBox25.Select();
            }
        }

        private void textBox26_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Tab)
            {
                e.IsInputKey = true;
                Pr7 = Convert.ToDecimal(textBox26.Text);
                textBox26.Text = Pr7.ToString("0.00");
                textBox25.Select();
            }
        }

        private void textBox29_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pr8 = Convert.ToDecimal(textBox29.Text);
                textBox29.Text = Pr8.ToString("0.00");
                textBox28.Select();
            }
        }

        private void textBox29_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Tab)
            {
                e.IsInputKey = true;
                Pr8 = Convert.ToDecimal(textBox29.Text);
                textBox29.Text = Pr8.ToString("0.00");
                textBox28.Select();
            }
        }

        private void textBox32_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pr9 = Convert.ToDecimal(textBox32.Text);
                textBox32.Text = Pr9.ToString("0.00");
                textBox31.Select();
            }
        }

        private void textBox32_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Tab)
            {
                e.IsInputKey = true;
                Pr9 = Convert.ToDecimal(textBox32.Text);
                textBox32.Text = Pr9.ToString("0.00");
                textBox31.Select();
            }
        }
        private void textBox35_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pr10 = Convert.ToDecimal(textBox35.Text);
                textBox35.Text = Pr10.ToString("0.00");
                textBox34.Select();
            }
        }


        private void textBox35_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Tab)
            {
                e.IsInputKey = true;
                Pr10 = Convert.ToDecimal(textBox35.Text);
                textBox35.Text = Pr10.ToString("0.00");
                textBox34.Select();
            }
        }

        private void CheckBut3()
        {
            if (button3.Focused == true)
            {
                button3.BackColor = Color.Green;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            initial = 0;
            Qty1 = Convert.ToInt32(comboBox2.Text);
            Pr1 = Convert.ToDecimal(textBox6.Text);
            Tt1 = Qty1 * Pr1;
            Gt1 = Tt1;
            textBox8.Text = Tt1.ToString("0.00");

            Qty2 = Convert.ToInt32(comboBox3.Text);
            Pr2 = Convert.ToDecimal(textBox11.Text);
            Tt2 = Qty2 * Pr2;
            Gt2 = Tt1 + Tt2;
            textBox9.Text = Tt2.ToString("0.00");

            Qty3 = Convert.ToInt32(comboBox4.Text);
            Pr3 = Convert.ToDecimal(textBox14.Text);
            Tt3 = Qty3 * Pr3;
            Gt3 = Tt1 + Tt2 + Tt3;
            textBox12.Text = Tt3.ToString("0.00");

            Qty4 = Convert.ToInt32(comboBox5.Text);
            Pr4 = Convert.ToDecimal(textBox17.Text);
            Tt4 = Qty4 * Pr4;
            Gt4 = Tt1 + Tt2 + Tt3 + Tt4;
            textBox15.Text = Tt4.ToString("0.00");

            Qty5 = Convert.ToInt32(comboBox6.Text);
            Pr5 = Convert.ToDecimal(textBox20.Text);
            Tt5 = Qty5 * Pr5;
            Gt5 = Tt1 + Tt2 + Tt3 + Tt4 + Tt5;
            textBox18.Text = Tt5.ToString("0.00");

            Qty6 = Convert.ToInt32(comboBox7.Text);
            Pr6 = Convert.ToDecimal(textBox23.Text);
            Tt6 = Qty6 * Pr6;
            Gt6 = Tt1 + Tt2 + Tt3 + Tt4 + Tt5 + Tt6;
            textBox21.Text = Tt6.ToString("0.00");

            Qty7 = Convert.ToInt32(comboBox8.Text);
            Pr7 = Convert.ToDecimal(textBox26.Text);
            Tt7 = Qty7 * Pr7;
            Gt7 = Tt1 + Tt2 + Tt3 + Tt4 + Tt5 + Tt6 + Tt7;
            textBox24.Text = Tt7.ToString("0.00");

            Qty8 = Convert.ToInt32(comboBox9.Text);
            Pr8 = Convert.ToDecimal(textBox29.Text);
            Tt8 = Qty8 * Pr8;
            Gt8 = Tt1 + Tt2 + Tt3 + Tt4 + Tt5 + Tt6 + Tt7 + Tt8;
            textBox27.Text = Tt8.ToString("0.00");

            Qty9 = Convert.ToInt32(comboBox10.Text);
            Pr9 = Convert.ToDecimal(textBox32.Text);
            Tt9 = Qty9 * Pr9;
            Gt9 = Tt1 + Tt2 + Tt3 + Tt4 + Tt5 + Tt6 + Tt7 + Tt8 + Tt9;
            textBox30.Text = Tt9.ToString("0.00");

            Qty10 = Convert.ToInt32(comboBox11.Text);
            Pr10 = Convert.ToDecimal(textBox35.Text);
            Tt10 = Qty10 * Pr10;
            Gt10 = Tt1 + Tt2 + Tt3 + Tt4 + Tt5 + Tt6 + Tt7 + Tt8 + Tt9 + Tt10;
            textBox33.Text = Tt10.ToString("0.00");

            SubTotal = Tt1 + Tt2 + Tt3 + Tt4 + Tt5 + Tt6 + Tt7 + Tt8 + Tt9 + Tt10;
            textBox36.Text = SubTotal.ToString("0.00");
            GATax = SubTotal * .0895m;
            textBox37.Text = GATax.ToString("0.00");
            GrandTotal = SubTotal + GATax;
            textBox38.Text = GrandTotal.ToString("C2");

            button3.BackColor = Color.Gold;
            button2.BackColor = Color.Green;
            button2.Select();
        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox39.Focus();
                textBox39.Select();
            }
        }

        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Qty1 = Convert.ToInt32(comboBox2.Text);
                Pr1 = Convert.ToDecimal(textBox6.Text);
                Tt1 = Qty1 * Pr1;
                Gt1 = Tt1;
                textBox8.Text = Tt1.ToString("0.00");
                textBox36.Text = Gt1.ToString("0.00");
                comboBox3.Select();
            }
        }

        private void textBox10_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Qty2 = Convert.ToInt32(comboBox3.Text);
                Pr2 = Convert.ToDecimal(textBox11.Text);
                Tt2 = Qty2 * Pr2;
                Gt2 = Tt1 + Tt2;
                textBox9.Text = Tt2.ToString("0.00");
                textBox36.Text = Gt2.ToString("0.00");
                comboBox4.Select();
            }
        }

        private void textBox13_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Qty3 = Convert.ToInt32(comboBox4.Text);
                Pr3 = Convert.ToDecimal(textBox14.Text);
                Tt3 = Qty3 * Pr3;
                Gt3 = Tt1 + Tt2 + Tt3;
                textBox12.Text = Tt3.ToString("0.00");
                textBox36.Text = Gt3.ToString("0.00");
                comboBox5.Select();
            }
        }

        private void textBox16_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Qty4 = Convert.ToInt32(comboBox5.Text);
                Pr4 = Convert.ToDecimal(textBox17.Text);
                Tt4 = Qty4 * Pr4;
                Gt4 = Tt1 + Tt2 + Tt3 + Tt4;
                textBox15.Text = Tt4.ToString("0.00");
                textBox36.Text = Gt4.ToString("0.00");
                comboBox6.Select();
            }
        }

        private void textBox19_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Qty5 = Convert.ToInt32(comboBox6.Text);
                Pr5 = Convert.ToDecimal(textBox20.Text);
                Tt5 = Qty5 * Pr5;
                Gt5 = Tt1 + Tt2 + Tt3 + Tt4 + Tt5;
                textBox18.Text = Tt5.ToString("0.00");
                textBox36.Text = Gt5.ToString("0.00");
                comboBox7.Select();
            }
        }

        private void textBox22_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Qty6 = Convert.ToInt32(comboBox7.Text);
                Pr6 = Convert.ToDecimal(textBox23.Text);
                Tt6 = Qty6 * Pr6;
                Gt6 = Tt1 + Tt2 + Tt3 + Tt4 + Tt5 + Tt6;
                textBox21.Text = Tt6.ToString("0.00");
                textBox36.Text = Gt6.ToString("0.00");
                comboBox8.Select();
            }
        }

        private void textBox25_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Qty7 = Convert.ToInt32(comboBox8.Text);
                Pr7 = Convert.ToDecimal(textBox26.Text);
                Tt7 = Qty7 * Pr7;
                Gt7 = Tt1 + Tt2 + Tt3 + Tt4 + Tt5 + Tt6 + Tt7;
                textBox24.Text = Tt7.ToString("0.00");
                textBox36.Text = Gt7.ToString("0.00");
                comboBox9.Select();
            }
        }

        private void textBox28_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Qty8 = Convert.ToInt32(comboBox9.Text);
                Pr8 = Convert.ToDecimal(textBox29.Text);
                Tt8 = Qty8 * Pr8;
                Gt8 = Tt1 + Tt2 + Tt3 + Tt4 + Tt5 + Tt6 + Tt7 + Tt8;
                textBox27.Text = Tt8.ToString("0.00");
                textBox36.Text = Gt8.ToString("0.00");
                comboBox10.Select();
            }
        }

        private void textBox31_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Qty9 = Convert.ToInt32(comboBox10.Text);
                Pr9 = Convert.ToDecimal(textBox32.Text);
                Tt9 = Qty9 * Pr9;
                Gt9 = Tt1 + Tt2 + Tt3 + Tt4 + Tt5 + Tt6 + Tt7 + Tt8 + Tt9;
                textBox30.Text = Tt9.ToString("0.00");
                textBox36.Text = Gt9.ToString("0.00");
                comboBox11.Select();
            }
        }

        private void textBox34_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Qty10 = Convert.ToInt32(comboBox11.Text);
                Pr10 = Convert.ToDecimal(textBox35.Text);
                Tt10 = Qty10 * Pr10;
                Gt10 = Tt1 + Tt2 + Tt3 + Tt4 + Tt5 + Tt6 + Tt7 + Tt8 + Tt9 + Tt10;
                textBox33.Text = Tt10.ToString("0.00");
                textBox36.Text = Gt10.ToString("0.00");

                SubTotal = Tt1 + Tt2 + Tt3 + Tt4 + Tt5 + Tt6 + Tt7 + Tt8 + Tt9 + Tt10;
                textBox36.Text = SubTotal.ToString("0.00");
                GATax = SubTotal * .0895m;
                textBox37.Text = GATax.ToString("0.00");
                GrandTotal = SubTotal + GATax;
                textBox38.Text = GrandTotal.ToString("C2");
                button3.Select();
                CheckBut3();
            }
        }
    }
}
    
