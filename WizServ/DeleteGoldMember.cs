using System;
using System.IO;
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
    public partial class DeleteGoldMember : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        private readonly string file = @"I:\\Datafile\\Control\\Gold.CSV";
        private DateTime datein;
        private int loopCount, loop;
        public string Selected;
        public int MaxNumber;
        public string fname, lname, addr, city, state, zip, display, number, combName;

        public DeleteGoldMember()
        {
            InitializeComponent();
            BackColor = Color.FromArgb(0, 132, 129);
            Icon = image100;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
            pictureBox1.Visible = true;
            label10.Visible = false;
            label11.Visible = false;
            label12.Text = "";
            label13.Text = "Members found: ";
            GetData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == "Yes")
            {
                label12.Text = "Yes, Is marked for deletion.\nPLEASE PRESS RETURN BUTTON.";
                UpdateGold();
                label10.Visible = true;
                loop -= 1;
                label13.ForeColor = Color.White;
                label13.Text = "Members found: " + loop.ToString();
            }
            if (comboBox1.SelectedItem == "No")
            {
                label12.Text = "No, Not marked for deletion.\nPLEASE PRESS RETURN BUTTON.";
                UpdateGold();
                label10.Visible = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            GoldCustMenu f2 = new GoldCustMenu();
            f2.Show();
        }
        private void richTextBox1_DoubleClick(object sender, EventArgs e)
        {
            Selected = richTextBox1.SelectedText;

            richTextBox1.Visible = false;
            pictureBox1.Visible = false;
            label1.Visible = false;
            comboBox1.Items.Add("Yes");
            comboBox1.Items.Add("No");
            GetEditData();
        }

        public void UpdateGold()
        {
            string path = file;
            List<String> lines = new List<String>();

            if (File.Exists(path))
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    String line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Contains(","))
                        {
                            String[] split = line.Split(',');
                            try
                            {
                                if (split[7] == number)
                                {
                                    if (comboBox1.SelectedItem == "Yes")
                                    {
                                        split[0] = fname;
                                        split[1] = lname;
                                        split[2] = addr;
                                        split[3] = city;
                                        split[4] = state;
                                        split[5] = zip;
                                        split[6] = "N";
                                        label10.ForeColor = Color.Yellow;
                                        loop -= 1;
                                        label13.Text = "Members found: " + loop.ToString();
                                    }
                                    if (comboBox1.SelectedItem == "No")
                                    {
                                        split[0] = fname;
                                        split[1] = lname;
                                        split[2] = addr;
                                        split[3] = city;
                                        split[4] = state;
                                        split[5] = zip;
                                        split[6] = "Y";
                                        label10.ForeColor = Color.White;
                                        label13.Text = "Members found: " + loop.ToString();
                                    }
                                    line = String.Join(",", split);
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error 189: \n" + ex);
                            }
                        }
                        lines.Add(line);
                    }
                }
                try
                {
                    using (StreamWriter writer = new StreamWriter(path, false))
                    {
                        foreach (String line in lines)
                            writer.WriteLine(line);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error line 137: \n" + ex);
                }
            }
        }

        public void GetEditData()
        {
            try
            {
                StreamReader reader = new StreamReader(file, Encoding.GetEncoding("Windows-1252"));
                String line = reader.ReadLine();

                List<string> listA = new List<string>();
                List<string> listB = new List<string>();
                List<string> listC = new List<string>();
                List<string> listD = new List<string>();
                List<string> listE = new List<string>();
                List<string> listF = new List<string>();
                List<string> listG = new List<string>();
                List<string> listH = new List<string>();

                loopCount = 0;
                label11.Visible = true;
                while (!reader.EndOfStream)
                {
                    var lineRead = reader.ReadLine();
                    var values = lineRead.Split(',');

                    listA.Add(values[0]);       //  First Name
                    listB.Add(values[1]);       //  Last Name
                    listC.Add(values[2]);       //  Address
                    listD.Add(values[3]);       //  City
                    listE.Add(values[4]);       //  State
                    listF.Add(values[5]);       //  Zip Code
                    listG.Add(values[6]);       //  Display Y or N
                    listH.Add(values[7]);       //  Number


                    if (listH[loopCount] == Selected)
                    {
                        fname = listA[loopCount];
                        lname = listB[loopCount];
                        addr = listC[loopCount];
                        city = listD[loopCount];
                        state = listE[loopCount];
                        zip = listF[loopCount];
                        display = listG[loopCount];
                        number = listH[loopCount];
                        combName = fname + " " + lname;

                        if (zip.Length == 4)
                        {
                            zip = "0" + zip;
                        }

                        label2.Text = "First Name: " + fname;
                        label3.Text = "Last Name:  " + lname;
                        label4.Text = "Address:    " + addr;
                        label5.Text = "City:       " + city;
                        label6.Text = "State:      " + state;
                        label7.Text = "Zip Code: " + zip;
                        label11.Text = "ID Number: " + number;
                        loop++;
                    }
                    loopCount++;
                    richTextBox1.SelectAll();
                    richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
                    richTextBox1.DeselectAll();
                }
                reader.Close(); // Close the open file
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 210: Sorry an error has occured: " + ex.Message);
            }
        }


        public void GetData()
        {
            try
            {
                StreamReader reader = new StreamReader(file, Encoding.GetEncoding("Windows-1252"));
                String line = reader.ReadLine();

                List<string> listA = new List<string>();
                List<string> listB = new List<string>();
                List<string> listC = new List<string>();
                List<string> listD = new List<string>();
                List<string> listE = new List<string>();
                List<string> listF = new List<string>();
                List<string> listG = new List<string>();
                List<string> listH = new List<string>();

                loopCount = 0;
                richTextBox1.Text = "#     Name                 City                 ST    Zip    Address\n\n";

                while (!reader.EndOfStream)
                {
                    var lineRead = reader.ReadLine();
                    var values = lineRead.Split(',');

                    listA.Add(values[0]);       //  First Name
                    listB.Add(values[1]);       //  Last Name
                    listC.Add(values[2]);       //  Address
                    listD.Add(values[3]);       //  City
                    listE.Add(values[4]);       //  State
                    listF.Add(values[5]);       //  Zip Code
                    listG.Add(values[6]);       //  Display Y or N
                    listH.Add(values[7]);       //  Number

                    var fname = listA[loopCount];
                    var lname = listB[loopCount];
                    var addr = listC[loopCount];
                    var city = listD[loopCount];
                    var state = listE[loopCount];
                    var zip = listF[loopCount];
                    var combName = fname + " " + lname;

                    if (zip.Length == 4)
                    {
                        zip = "0" + zip;
                    }
                    if (listG[loopCount] == "Y")
                    {
                        switch (combName.Length)
                        {
                            case 8:
                                richTextBox1.Text = richTextBox1.Text + listH[loopCount].ToString() + "\t" + combName + " \t\t\t" + city + "\t\t\t" + state + "\t" + zip + "\t  " + addr + "\n";
                                break;
                            case 9:
                                richTextBox1.Text = richTextBox1.Text + listH[loopCount].ToString() + "\t" + combName + " \t\t\t" + city + "\t\t\t" + state + "\t" + zip + "\t  " + addr + "\n";
                                break;
                            case 10:
                                if (city.Contains("BROOKHAV"))
                                {
                                    richTextBox1.Text = richTextBox1.Text + listH[loopCount].ToString() + "\t" + combName + " \t\t" + city + "\t\t" + state + "\t" + zip + "\t  " + addr + "\n";
                                }
                                if (city.Contains("AVON"))
                                {
                                    richTextBox1.Text = richTextBox1.Text + listH[loopCount].ToString() + "\t" + combName + " \t\t" + city + "\t" + state + "\t" + zip + "\t  " + addr + "\n";
                                }
                                else
                                {
                                    richTextBox1.Text = richTextBox1.Text + listH[loopCount].ToString() + "\t" + combName + " \t\t" + city + "\t\t\t" + state + "\t" + zip + "\t  " + addr + "\n";
                                }
                                break;
                            case 11:
                                if (city.Contains("GUNTER"))
                                {
                                    richTextBox1.Text = richTextBox1.Text + listH[loopCount].ToString() + "\t" + combName + " \t\t" + city + "\t\t" + state + "\t" + zip + "\t  " + addr + "\n";
                                }
                                else
                                {
                                    richTextBox1.Text = richTextBox1.Text + listH[loopCount].ToString() + "\t" + combName + " \t\t" + city + "\t\t\t" + state + "\t" + zip + "\t  " + addr + "\n";
                                }
                                break;
                            case 12:
                                if (city.Contains("GUNTER"))
                                {
                                    richTextBox1.Text = richTextBox1.Text + listH[loopCount].ToString() + "\t" + combName + " \t\t" + city + "\t\t" + state + "\t" + zip + "\t  " + addr + "\n";
                                }
                                else
                                {
                                    richTextBox1.Text = richTextBox1.Text + listH[loopCount].ToString() + "\t" + combName + " \t\t" + city + "\t\t\t" + state + "\t" + zip + "\t  " + addr + "\n";
                                }
                                break;
                            case 13:
                                richTextBox1.Text = richTextBox1.Text + listH[loopCount].ToString() + "\t" + combName + " \t\t" + city + "\t\t\t" + state + "\t" + zip + "\t  " + addr + "\n";
                                break;
                            case 14:
                                richTextBox1.Text = richTextBox1.Text + listH[loopCount].ToString() + "\t" + combName + " \t\t" + city + "\t\t\t" + state + "\t" + zip + "\t  " + addr + "\n";
                                break;
                            case 15:
                                richTextBox1.Text = richTextBox1.Text + listH[loopCount].ToString() + "\t" + combName + " \t" + city + "\t\t\t" + state + "\t" + zip + "\t  " + addr + "\n";
                                break;
                            case 16:
                                if (city.Length == 17)
                                {
                                    richTextBox1.Text = richTextBox1.Text + listH[loopCount].ToString() + "\t" + combName + "\t " + city + " \t\t" + state + "\t" + zip + "\t  " + addr + "\n";
                                }
                                if (city.Length == 12)
                                {
                                    richTextBox1.Text = richTextBox1.Text + listH[loopCount].ToString() + "\t" + combName + "\t " + city + " \t\t" + state + "\t" + zip + "\t  " + addr + "\n";
                                }
                                else
                                {
                                    richTextBox1.Text = richTextBox1.Text + listH[loopCount].ToString() + "\t" + combName + "\t " + city + " \t\t\t" + state + "\t" + zip + "\t  " + addr + "\n";
                                }
                                break;
                            case 17:
                                richTextBox1.Text = richTextBox1.Text + listH[loopCount].ToString() + "\t" + combName + " \t" + city + "\t\t\t" + state + "\t" + zip + "\t  " + addr + "\n";
                                break;
                            case 18:
                                richTextBox1.Text = richTextBox1.Text + listH[loopCount].ToString() + "\t" + combName + " \t" + city + "\t\t\t" + state + "\t" + zip + "\t  " + addr + "\n";
                                break;
                            case 19:
                                richTextBox1.Text = richTextBox1.Text + listH[loopCount].ToString() + "\t" + combName + " \t" + city + "\t\t\t" + state + "\t" + zip + "\t  " + addr + "\n";
                                break;
                            case 20:
                                richTextBox1.Text = richTextBox1.Text + listH[loopCount].ToString() + "\t" + combName + " \t" + city + "\t\t\t" + state + "\t" + zip + "\t  " + addr + "\n";
                                break;
                            case 21:
                                richTextBox1.Text = richTextBox1.Text + listH[loopCount].ToString() + "\t" + combName + " \t" + city + "\t\t\t" + state + "\t" + zip + "\t  " + addr + "\n";
                                break;
                        }

                        loop++;
                    }
                    loopCount++;
                }
                reader.Close(); // Close the open file
                MaxNumber = loopCount;                  // store maximum number
                label13.Text = "Members found: " + loop.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 353: Sorry an error has occured: " + ex.Message);
            }
        }
    }
}
