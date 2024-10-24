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
    public partial class RawSearch : Form
    {
        private string filePath = @"I:\datafile\Control\Database.csv";
        private string one, two, three, four, five, six, IsSelected;

        public RawSearch()
        {
            InitializeComponent();
            LoadCsvToListBox(filePath);
        }
        
        private void LoadCsvToListBox(string filePath)
        {
            listBoxResults.Items.Clear();

            var lines = File.ReadAllLines(filePath);
            var selectedColumnsIndices = new int[] { 1, 2, 3, 4, 12, 14 }; // specify the indices of the 6 columns you need

            foreach (var line in lines)
            {
                var columns = line.Split(',');
                var selectedColumns = selectedColumnsIndices.Select(index => columns[index]);
                one = columns[1];       // Claim #
                two = columns[2];       // Date In
                three = columns[3];     // First Name
                four = columns[4];      // Last Name
                five = columns[12];     // Manufacturer
                six = columns[14];      // Model
                //listBoxResults.Items.Add(string.Join(", ", selectedColumns));
                FixSpaces();
                if (one != "1")
                {
                    listBox1.Items.Add(one + "    " + two + "    " + three + "    " + four + "    " + five + "    " + six);
                }
            }
        }


        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            IsSelected = listBox1.SelectedItem.ToString();
            IsSelected = IsSelected.Substring(0, 6).Trim();
            Version.IsSelected = IsSelected;
            label8.Text += IsSelected;
            Hide();
            ByClaimNum f2 = new ByClaimNum();
            f2.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            RetrieveMenu f2 = new RetrieveMenu();
            f2.Show();
        }

        private void FixSpaces()
        {
            switch (two.Length)
            {
                case 8:
                    two += "  ";
                    break;
                case 9:
                    two += " ";
                    break;
            }
            switch (three.Length)
            {
                case 1:
                    three += "                    ";
                    break;
                case 2:
                    three += "                   ";
                    break;
                case 3:
                    three += "                  ";
                    break;
                case 4:
                    three += "                 ";
                    break;
                case 5:
                    three += "                ";
                    break;
                case 6:
                    three += "               ";
                    break;
                case 7:
                    three += "              ";
                    break;
                case 8:
                    three += "             ";
                    break;
                case 9:
                    three += "            ";
                    break;
                case 10:
                    three += "           ";
                    break;
                case 11:
                    three += "          ";
                    break;
                case 12:
                    three += "         ";
                    break;
                case 13:
                    three += "        ";
                    break;
                case 14:
                    three += "       ";
                    break;
                case 15:
                    three += "      ";
                    break;
                case 16:
                    three += "     ";
                    break;
                case 17:
                    three += "    ";
                    break;
                case 18:
                    three += "   ";
                    break;
                case 19:
                    three += "  ";
                    break;
                case 20:
                    three += " ";
                    break;
            }
            switch (four.Length)
            {
                case 1:
                    four += "                    ";
                    break;
                case 2:
                    four += "                   ";
                    break;
                case 3:
                    four += "                  ";
                    break;
                case 4:
                    four += "                 ";
                    break;
                case 5:
                    four += "                ";
                    break;
                case 6:
                    four += "               ";
                    break;
                case 7:
                    four += "              ";
                    break;
                case 8:
                    four += "             ";
                    break;
                case 9:
                    four += "            ";
                    break;
                case 10:
                    four += "           ";
                    break;
                case 11:
                    four += "          ";
                    break;
                case 12:
                    four += "         ";
                    break;
                case 13:
                    four += "        ";
                    break;
                case 14:
                    four += "       ";
                    break;
                case 15:
                    four += "      ";
                    break;
                case 16:
                    four += "     ";
                    break;
                case 17:
                    four += "    ";
                    break;
                case 18:
                    four += "   ";
                    break;
                case 19:
                    four += "  ";
                    break;
                case 20:
                    four += " ";
                    break;
            }
            switch (five.Length)
            {
                case 1:
                    five += "                    ";
                    break;
                case 2:
                    five += "                   ";
                    break;
                case 3:
                    five += "                  ";
                    break;
                case 4:
                    five += "                 ";
                    break;
                case 5:
                    five += "                ";
                    break;
                case 6:
                    five += "               ";
                    break;
                case 7:
                    five += "              ";
                    break;
                case 8:
                    five += "             ";
                    break;
                case 9:
                    five += "            ";
                    break;
                case 10:
                    five += "           ";
                    break;
                case 11:
                    five += "          ";
                    break;
                case 12:
                    five += "         ";
                    break;
                case 13:
                    five += "        ";
                    break;
                case 14:
                    five += "       ";
                    break;
                case 15:
                    five += "      ";
                    break;
                case 16:
                    five += "     ";
                    break;
                case 17:
                    five += "    ";
                    break;
                case 18:
                    five += "   ";
                    break;
                case 19:
                    five += "  ";
                    break;
                case 20:
                    five += " ";
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }
    }
}
