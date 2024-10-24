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
using System.Diagnostics;
using System.Collections;
using System.Collections.ObjectModel;
using System.Runtime;
using System.Media;

namespace WizServ
{
    public partial class ViewEstimates : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        private readonly string Estimates = @"I:\\Datafile\\Mel\\Estimates.CSV";         // This is Read only CSV
        private readonly string Est_Appr = @"I:\\Datafile\\Control\\Est_Approved.CSV";   // This is Read only CSV
        private readonly string Database = @"I:\\Datafile\\Control\\Database.CSV";       // This is Read only CSV
        private int ndx0, ndx1, ndx2, loopCount;
        public string select, name, NAME, PARTS, LABOR, SHOP, SHIPPING, TAXES, TOTAL, DOWNP, SENT, APPROVED, RUSH;
        public string NAME1, NAME2, NAME3, PARTS1, LABOR1, SHOP1, SHIPPING1, TAXES1, TOTAL1, DOWNP1, NDX, SENT1, APPROVED1, RUSH1;
        private readonly string msg = "Double-click on Claim Number to edit.";
        StringFormat strFormat;                     //Used to format the grid rows.
        ArrayList arrColumnLefts = new ArrayList(); //Used to save left coordinates of columns
        ArrayList arrColumnWidths = new ArrayList();//Used to save column widths
        int iCellHeight = 0;                        //Used to get/set the datagridview cell height
        int iTotalWidth = 0;                        //Used to get/set the datagridview cell width
        private int RowIndex;
        int iRow = 0;                               //Used as counter
        bool bFirstPage = false;                    //Used to check whether we are printing first page
        bool bNewPage = false;                      // Used to check whether we are printing a new page
        int iHeaderHeight = 0;                      //Used for the header height
        public static string the_day_is = "";
        private string xMsg = "";
        public int IsStarted = 0;
        public int MClock, mClock3;
        private Button sender;
        public event DataGridViewCellEventHandler CellDoubleClick;

        public ViewEstimates()
        {
            InitializeComponent();
            Icon = image100;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
            label15.Text = msg;
            timer1.Interval = 100;     // 1 seconds
            timer1.Enabled = true;
            timer1.Start();
            label17.Visible = false;
            label29.Visible = false;
            label30.Visible = false;
            //GetIndexNumber();
            dataGridView1.Enabled = true;
            dataGridView1.ScrollBars.Equals(true);
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 12);
            dataGridView1.DefaultCellStyle.ForeColor = Color.White;
            dataGridView1.DefaultCellStyle.BackColor = Color.Black;
            button8.PerformClick();
            //CellDoubleClick = new DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e);
        }

        private void DataGridView1_CellDoubleClick(Object sender, DataGridViewCellEventArgs e)
        {
            System.Text.StringBuilder messageBoxCS = new StringBuilder();
            messageBoxCS.AppendFormat("{0} = {1}", "ColumnIndex", e.ColumnIndex);
            messageBoxCS.AppendLine();
            messageBoxCS.AppendFormat("{0} = {1}", "RowIndex", e.RowIndex);
            messageBoxCS.AppendLine();
            MessageBox.Show(messageBoxCS.ToString(), "CellDoubleClick Event");
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
           
        }
        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var f = e.RowIndex.ToString();
            var s = e.ColumnIndex.ToString();
            MessageBox.Show("RowIndex " + f + "\n" + "Column Index " + s);
        }


        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox8.Text = DateTime.Now.ToShortDateString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            button8.PerformClick();
            timer1.Stop();
        }

        private void CheckUserData()
        {
            if (NAME1.Contains(","))
            {
                NAME1.Replace(',', ' ');
            }
            if (NAME2.Contains(","))
            {
                NAME2.Replace(',', ' ');
            }
            if (textBox1.Text.Contains(","))
            {
                textBox1.Text.Replace(',', ' ');
            }
            if (textBox2.Text.Contains(","))
            {
                textBox2.Text.Replace(',', ' ');
            }
            if (textBox3.Text.Contains(","))
            {
                textBox3.Text.Replace(',', ' ');
            }
            if (textBox4.Text.Contains(","))
            {
                textBox4.Text.Replace(',', ' ');
            }
            if (textBox5.Text.Contains(","))
            {
                textBox5.Text.Replace(',', ' ');
            }
            if (textBox6.Text.Contains(","))
            {
                textBox6.Text.Replace(',', ' ');
            }
            if (textBox7.Text.Contains(","))
            {
                textBox7.Text.Replace(',', ' ');
            }
            if (textBox8.Text.Contains(","))
            {
                textBox8.Text.Replace(',', ' ');
            }
            if (textBox9.Text.Contains(","))
            {
                textBox9.Text.Replace(',', ' ');
            }
            if (textBox10.Text.Contains(","))
            {
                textBox10.Text.Replace(',', ' ');
            }
        }

        private void WithRetry()
        {
            MessageBox.Show("Source file busy.\nWill retry in 30 seconds\nCall Doc if this re-occurs.");
            Thread.Sleep(2500);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            label11.Text = "Updating DB";
            //DoHeavyStuf();
            MClock = 0;
            label9.Text = "115";
            try
            {
                using (var stream = File.Open(Estimates, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
                {
                    label7.Text = "OK";
                }
            }
            catch (Exception)
            {
                label7.Text = "Busy";
                WithRetry();
            }
            if (label7.Text != "OK")
            {
                Thread.Sleep(5000);
            }

            try
            {
                var btn = sender as Button;
                btn.Enabled = false;
                button2.Enabled = false;
                progressBar1.Value = 100;
                progressBar1.Style = ProgressBarStyle.Marquee;
                progressBar1.ForeColor = Color.Yellow;
                progressBar1.BackColor = Color.Black;
                dataGridView1.Rows.Clear();
                int counter = 0;
                var thread = new Thread(() =>
                {
                    //using (Stream reader = new FileStream(fileName, FileMode.Open))
                    using (var reader = new StreamReader(Estimates))
                    {
                        try
                        {
                            while (!reader.EndOfStream)
                            {
                                var line = reader.ReadLine();
                                var values = line.Split(',');
                                if (!string.IsNullOrEmpty(line))
                                {
                                    dataGridView1.Invoke((MethodInvoker)delegate
                                    {
                                        dataGridView1.Rows.Add(values);
                                    });
                                    label1.Invoke((MethodInvoker)delegate
                                    {
                                        label1.Text = $"Found:{counter++}";
                                    });
                                }
                            }
                            progressBar1.Invoke((MethodInvoker)delegate
                            {
                                if (progressBar1.Value <= 99)
                                {
                                    progressBar1.Value = progressBar1.Value + 10;
                                    progressBar1.ForeColor = Color.Yellow;
                                }
                                if (progressBar1.Value > 100)
                                {
                                    progressBar1.Value = 100;
                                }
                                progressBar1.Style = ProgressBarStyle.Blocks;
                                progressBar1.ForeColor = Color.Yellow;
                            });
                            btn.Invoke((MethodInvoker)delegate
                            {
                                btn.Enabled = true;
                            });
                            button2.Invoke((MethodInvoker)delegate
                            {
                                button2.Enabled = true;
                            });
                            reader.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                });
                thread.Start();
                button3.Enabled = true;
                label11.Text = "";
            }

            catch (Exception ex)
            {
                if (ex.Message.Contains("The process cannot access I:\\Datafile\\Data.csv"))
                {
                    MessageBox.Show(" ");
                }
                else
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            button4.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CheckUserData();                                                // Make sure a Comma ',' was not typed into fields
            List<String> lines = new List<String>();

            if (File.Exists(Estimates))
            {
                using (StreamReader reader = new StreamReader(Estimates))   // Update record in CSV file (Append record)
                {
                    String line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Contains(","))
                        {
                            String[] split = line.Split(',');

                            if (split[0].Contains(select))
                            {
                                split[1] = NAME1;
                                split[2] = NAME2;
                                split[3] = textBox10.Text;
                                split[4] = textBox1.Text;
                                split[5] = textBox2.Text;
                                split[6] = textBox3.Text;
                                split[7] = textBox4.Text;
                                split[8] = textBox5.Text;
                                split[9] = textBox7.Text;
                                split[10] = NDX;
                                split[11] = textBox8.Text;
                                split[12] = textBox6.Text;
                                split[13] = textBox9.Text;

                                line = String.Join(",", split);
                            }
                        }

                        lines.Add(line);
                    }
                }
                try
                {
                    using (StreamWriter writer = new StreamWriter(Estimates, false))
                    {
                        foreach (String line in lines)
                            writer.WriteLine(line);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Line: 148\n" + ex);
                }

                label17.Visible = true;
            }
            UpdateEstApproved();
            UpdateDatabase();
        }

        private void UpdateDatabase()
        {
            List<String> lines = new List<String>();

            if (File.Exists(Database))
            {
                using (StreamReader reader = new StreamReader(Database))   // Update record in CSV file (Append record)
                {
                    String line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Contains(","))
                        {
                            String[] split = line.Split(',');

                            if (split[1].Contains(select.Trim()))
                            {
                                split[16] = textBox5.Text;
                                split[17] = textBox1.Text;
                                split[18] = textBox10.Text;
                                split[21] = textBox8.Text;
                                split[73] = textBox5.Text;
                                split[74] = textBox10.Text;
                                split[77] = textBox6.Text;

                                line = String.Join(",", split);
                            }
                        }
                        lines.Add(line);
                    }
                }
                try
                {
                    using (StreamWriter writer = new StreamWriter(Database, false))
                    {
                        foreach (String line in lines)
                            writer.WriteLine(line);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Line: 199\n" + ex);
                }
                label30.ForeColor = Color.Purple;
                label30.Visible = true;
            }
        }

        private void UpdateEstApproved()
        {
            if (File.Exists(Est_Appr))
            {
                label11.Visible = true;
                var csv = new StringBuilder();
                var comma = ",";

                var newLine = (select + comma + NAME1 + comma + NAME2 + comma + textBox10.Text + comma + textBox1.Text + comma
                    + textBox2.Text + comma + textBox3.Text + comma + textBox4.Text + comma + textBox5.Text + comma
                    + textBox7.Text + comma + NDX + comma + textBox8.Text + comma + textBox6.Text + comma + textBox9.Text + Environment.NewLine);
                csv.Append(newLine);
                try
                {
                    File.AppendAllText(Est_Appr, csv.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error line 226\n" + ex);
                }
              
            }
            label29.ForeColor = Color.Blue;
            label29.Visible = true;
        }

        private void richTextBox1_DoubleClick(object sender, EventArgs e)
        {
            select = richTextBox1.SelectedText.Trim();
            pictureBox1.Visible = false;
            label15.Visible = false;
            richTextBox1.Visible = false;
            HideLabels();
            GetUserData();
            StartUpdate();
        }

        private void StartUpdate()
        {
            label28.Text = "Claim: " + select;
            label27.Text = "Name:  " + NAME3;
            textBox1.Text = LABOR1.Trim();
            textBox2.Text = SHOP1.Trim();
            textBox3.Text = SHIPPING1.Trim();
            var d = decimal.Parse(TAXES1);
            textBox4.Text = decimal.Round(d, 2).ToString();
            TAXES1 = decimal.Round(d, 2).ToString();
            var t = decimal.Parse(TOTAL1);
            textBox5.Text = decimal.Round(t, 2).ToString();
            TOTAL1 = decimal.Round(t, 2).ToString();
            textBox6.Text = DOWNP1.Trim();
            textBox7.Text = SENT1.Trim();
            textBox8.Text = APPROVED1.Trim();
            textBox9.Text = RUSH1.Trim();
            textBox10.Text = PARTS1.Trim();
        }

        private void HideLabels()
        {
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            //label12.Visible = false;
            label13.Visible = false;
            //label14.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            MainUtilitiesMenu f1 = new MainUtilitiesMenu();
            f1.Show();
        }

        private void GetIndexNumber()                                   // Get next Index # in sequence
        {
            try
            {
                StreamReader reader = new StreamReader(Estimates);
                String line = reader.ReadLine();

                List<string> listA = new List<string>();
                List<string> listB = new List<string>();
                List<string> listC = new List<string>();
                List<string> listD = new List<string>();
                List<string> listE = new List<string>();
                List<string> listF = new List<string>();
                List<string> listG = new List<string>();
                List<string> listH = new List<string>();
                List<string> listI = new List<string>();
                List<string> listJ = new List<string>();
                List<string> listK = new List<string>();
                List<string> listL = new List<string>();
                List<string> listM = new List<string>();
                List<string> listN = new List<string>();

                ndx0 = 0;
                ndx1 = 0;
                ndx2 = 0;
                loopCount = 0;

                while (!reader.EndOfStream)
                {
                    var lineRead = reader.ReadLine();
                    var values = lineRead.Split(',');

                    listA.Add(values[0]);       //  Claim_No
                    listB.Add(values[1]);       //  First Name
                    listC.Add(values[2]);       //  Last Name
                    listD.Add(values[3]);       //  Parts $
                    listE.Add(values[4]);       //  Labor $
                    listF.Add(values[5]);       //  Shop $
                    listG.Add(values[6]);       //  Ship $
                    listH.Add(values[7]);       //  Tax $
                    listI.Add(values[8]);       //  Total $
                    listJ.Add(values[9]);       //  Sent_Date 00/00/0000
                    listK.Add(values[10]);      //  Index Number
                    listL.Add(values[11]);      //  Approved Date 00/00/0000
                    listM.Add(values[12]);      //  Paid Down $
                    listN.Add(values[13]);      //  Rush $

                    var claim = listA[loopCount];
                    var name = listB[loopCount] + " " + listC[loopCount];
                    var part = listD[loopCount];
                    var labo = listE[loopCount];
                    var sho = listF[loopCount];
                    var shi = listG[loopCount];
                    var tax = listH[loopCount];
                    var tota = listI[loopCount];
                    var sent = listJ[loopCount];
                    var inde = listK[loopCount];
                    var appr = listL[loopCount];
                    var down = listM[loopCount];
                    var rush = listN[loopCount];

                    switch (name.Length)
                    {
                        case 7:
                            name += "               ";
                            break;
                        case 8:
                            name += "              ";
                            break;
                        case 9:
                            name += "             ";
                            break;
                        case 10:
                            name += "            ";
                            break;
                        case 11:
                            name += "           ";
                            break;
                        case 12:
                            name += "          ";
                            break;
                        case 13:
                            name += "         ";
                            break;
                        case 14:
                            name += "        ";
                            break;
                        case 15:
                            name += "       ";
                            break;
                        case 16:
                            name += "      ";
                            break;
                        case 17:
                            name += "     ";
                            break;
                        case 18:
                            name += "    ";
                            break;
                        case 19:
                            name += "   ";
                            break;
                        case 20:
                            name += "  ";
                            break;
                        case 21:
                            name += " ";
                            break;
                        case 22:
                            name += "";
                            break;
                    }
                    var u = Convert.ToDecimal(part);
                    PARTS = u.ToString("C2");
                   switch (part.Length)
                    {
                        case 1:
                            PARTS = "  " + PARTS;
                            break;
                        case 2:
                            PARTS = "  " + PARTS;
                            break;
                        case 3:
                            PARTS = "  " + PARTS;
                            break;
                        case 4:
                            PARTS = "    " + PARTS;
                            break;
                        case 5:
                            PARTS = "   " + PARTS;
                            break;
                        case 6:
                            PARTS = "  " + PARTS;
                            break;
                        case 7:
                            PARTS = " " + PARTS;
                            break;
                        case 8:
                            PARTS = "" + PARTS;
                            break;
                    }
                    var la = Convert.ToDecimal(labo);
                    LABOR = la.ToString("C2");
                    switch (labo.Length)
                    {
                        case 1:
                            LABOR = "  " + 1;
                            break;
                        case 2:
                            LABOR = "  " + LABOR;
                            break;
                        case 3:
                            LABOR = "  " + LABOR;
                            break;
                        case 4:
                            LABOR = "    " + LABOR;
                            break;
                        case 5:
                            LABOR = "   " + LABOR;
                            break;
                        case 6:
                            LABOR = "  " + LABOR;
                            break;
                        case 7:
                            LABOR = " " + LABOR;
                            break;
                        case 8:
                            LABOR = "" + LABOR;
                            break;
                    }
                    var sh = Convert.ToDecimal(sho);
                    SHOP = sh.ToString("C2");
                    switch (sho.Length)
                    {
                        case 1:
                            SHOP = "  " + SHOP;
                            break;
                        case 2:
                            SHOP = "  " + SHOP;
                            break;
                        case 3:
                            SHOP = "  " + SHOP;
                            break;
                        case 4:
                            SHOP = "    " + SHOP;
                            break;
                        case 5:
                            SHOP = "   " + SHOP;
                            break;
                        case 6:
                            SHOP = "  " + SHOP;
                            break;
                        case 7:
                            SHOP = " " + SHOP;
                            break;
                        case 8:
                            SHOP = "" + SHOP;
                            break;
                    }
                    var shi1 = Convert.ToDecimal(shi);
                    SHIPPING = shi1.ToString("C2");
                    switch (shi.Length)
                    {
                        case 1:
                            SHIPPING = "  " + SHIPPING;
                            break;
                        case 2:
                            SHIPPING = "  " + SHIPPING;
                            break;
                        case 3:
                            SHIPPING = "  " + SHIPPING;
                            break;
                        case 4:
                            SHIPPING = "    " + SHIPPING;
                            break;
                        case 5:
                            SHIPPING = "   " + SHIPPING;
                            break;
                        case 6:
                            SHIPPING = "  " + SHIPPING;
                            break;
                        case 7:
                            SHIPPING = " " + SHIPPING;
                            break;
                        case 8:
                            SHIPPING = "" + SHIPPING;
                            break;
                    }
                    var tax1 = Convert.ToDecimal(tax);
                    TAXES = tax1.ToString("C2");
                    switch (tax.Length)
                    {
                        case 1:
                            TAXES = "  " + TAXES;
                            break;
                        case 2:
                            TAXES = "  " + TAXES;
                            break;
                        case 3:
                            TAXES = "  " + TAXES;
                            break;
                        case 4:
                            TAXES = "    " + TAXES;
                            break;
                        case 5:
                            TAXES = "   " + TAXES;
                            break;
                        case 6:
                            TAXES = "  " + TAXES;
                            break;
                        case 7:
                            TAXES = " " + TAXES;
                            break;
                        case 8:
                            TAXES = "" + TAXES;
                            break;
                    }
                    var total1 = Convert.ToDecimal(tota);
                    TOTAL = total1.ToString("C2");
                    switch (tota.Length)
                    {
                        case 1:
                            TOTAL = "  " + TOTAL;
                            break;
                        case 2:
                            TOTAL = "  " + TOTAL;
                            break;
                        case 3:
                            TOTAL = "  " + TOTAL;
                            break;
                        case 4:
                            TOTAL = "    " + TOTAL;
                            break;
                        case 5:
                            TOTAL = "   " + TOTAL;
                            break;
                        case 6:
                            TOTAL = "  " + TOTAL;
                            break;
                        case 7:
                            TOTAL = " " + TOTAL;
                            break;
                        case 8:
                            TOTAL = "" + TOTAL;
                            break;
                    }
                    var v = Convert.ToDecimal(down);
                    DOWNP = v.ToString("C2");
                    switch (down.Length)
                    {
                        case 1:
                            DOWNP = "  " + DOWNP;
                            break;
                        case 2:
                            DOWNP = "  " + DOWNP;
                            break;
                        case 3:
                            DOWNP = "  " + DOWNP;
                            break;
                        case 4:
                            DOWNP = "    " + DOWNP;
                            break;
                        case 5:
                            DOWNP = "   " + DOWNP;
                            break;
                        case 6:
                            DOWNP = "  " + DOWNP;
                            break;
                        case 7:
                            DOWNP = " " + DOWNP;
                            break;
                        case 8:
                            DOWNP = "" + DOWNP;
                            break;
                    }
                    switch (appr.Length)
                    {
                        case 8:
                            APPROVED = "  " + appr;
                            break;
                        case 9:
                            APPROVED = " " + appr;
                            break;
                        default:
                            APPROVED = "" + appr;
                            break;
                    }
                    switch (sent.Length)
                    {
                        case 8:
                            SENT = "  " + sent;
                            break;
                        case 9:
                            SENT = " " + sent;
                            break;
                        default:
                            SENT = "" + sent;
                            break;
                    }
                    var mm = Convert.ToDecimal(rush);
                    RUSH = mm.ToString("C2");
                    switch (rush.Length)
                    {
                        case 1:
                            RUSH = "  " + rush;
                            break;
                        case 2:
                            RUSH = "  " + rush;
                            break;
                        case 3:
                            RUSH = "  " + rush;
                            break;
                        case 4:
                            RUSH = "    " + rush;
                            break;
                        case 5:
                            RUSH = "   " + rush;
                            break;
                        case 6:
                            RUSH = "  " + rush;
                            break;
                        case 7:
                            RUSH = " " + rush;
                            break;
                        case 8:
                            RUSH = "" + rush;
                            break;
                    }
                    richTextBox1.Text = richTextBox1.Text + claim + " " + name + " " + PARTS + " " + LABOR + " " + SHOP + " " + SHIPPING + " " + TAXES + " " + TOTAL + " " + DOWNP + " " + SENT + " " + APPROVED + " " + " " + RUSH + "\n";
                    loopCount++;
                }
                reader.Close(); // Close the open file
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Line 665:\n Sorry an error has occured: " + ex.Message);
            }
        }

        private void GetUserData()                                   // Get User Data that was selected Claim #
        {
            try
            {
                StreamReader reader = new StreamReader(Estimates);
                String line = reader.ReadLine();

                List<string> listA = new List<string>();
                List<string> listB = new List<string>();
                List<string> listC = new List<string>();
                List<string> listD = new List<string>();
                List<string> listE = new List<string>();
                List<string> listF = new List<string>();
                List<string> listG = new List<string>();
                List<string> listH = new List<string>();
                List<string> listI = new List<string>();
                List<string> listJ = new List<string>();
                List<string> listK = new List<string>();
                List<string> listL = new List<string>();
                List<string> listM = new List<string>();
                List<string> listN = new List<string>();

                ndx0 = 0;
                ndx1 = 0;
                ndx2 = 0;
                loopCount = 0;

                while (!reader.EndOfStream)
                {
                    var lineRead = reader.ReadLine();
                    var values = lineRead.Split(',');

                    listA.Add(values[0]);       //  Claim_No
                    listB.Add(values[1]);       //  First Name
                    listC.Add(values[2]);       //  Last Name
                    listD.Add(values[3]);       //  Parts $
                    listE.Add(values[4]);       //  Labor $
                    listF.Add(values[5]);       //  Shop $
                    listG.Add(values[6]);       //  Ship $
                    listH.Add(values[7]);       //  Tax $
                    listI.Add(values[8]);       //  Total $
                    listJ.Add(values[9]);       //  Sent_Date 00/00/0000
                    listK.Add(values[10]);      //  Index Number
                    listL.Add(values[11]);      //  Approved Date 00/00/0000
                    listM.Add(values[12]);      //  Paid Down $
                    listN.Add(values[13]);      //  Rush $

                    var claim = listA[loopCount];
                    var name = listB[loopCount] + " " + listC[loopCount];
                    var part = listD[loopCount];
                    var labo = listE[loopCount];
                    var sho = listF[loopCount];
                    var shi = listG[loopCount];
                    var tax = listH[loopCount];
                    var tota = listI[loopCount];
                    var sent = listJ[loopCount];
                    var ind = listK[loopCount];
                    var appr = listL[loopCount];
                    var down = listM[loopCount];
                    var rush = listN[loopCount];

                    if (select.Trim() == claim)
                    {
                        NAME1 = listB[loopCount];
                        NAME2 = listC[loopCount];
                        NAME3 = NAME1 + " " + NAME2;
                        PARTS1 = part;
                        LABOR1 = labo;
                        SHOP1 = sho;
                        SHIPPING1 = shi;
                        TAXES1 = tax;
                        TOTAL1 = tota;
                        DOWNP1 = down;
                        SENT1 = sent;
                        NDX = ind;
                        APPROVED1 = appr;
                        RUSH1 = rush;
                    }
                    loopCount++;
                }
                reader.Close(); // Close the open file
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Line 753:\n Sorry an error has occured: " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox6.Text = textBox10.Text;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox6.Text = textBox5.Text;
        }

        private void button6_Click(object sender, EventArgs e)  // $ 50 Rush fee
        {
            textBox9.Text = "50.00";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox9.Text = "100.00";
        }
    }

    internal class DataGridView1_CellDoubleClick
    {
        private object @object;
        private Button sender;
        private DataGridViewCellEventArgs dataGridViewCellEventArgs;
        private object e;

        public DataGridView1_CellDoubleClick()
        {
        }

        public DataGridView1_CellDoubleClick(object @object, Button sender, DataGridViewCellEventArgs dataGridViewCellEventArgs, object e)
        {
            this.@object = @object;
            this.sender = sender;
            this.dataGridViewCellEventArgs = dataGridViewCellEventArgs;
            this.e = e;
        }
    }
}
