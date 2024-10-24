using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WizServ
{
    public partial class SearchOldClaims : Form
    {
        private string IsFound;
        private string[] csvLines;
        private Stopwatch timer;

        public SearchOldClaims()
        {
            InitializeComponent();
            InitializeMyComponents();
        }

        private void InitializeMyComponents()
        {
            timer = new Stopwatch();
            timer.Start();
            label2.Text = "";
            label3.Text = "";
            textBoxSearch.TextChanged += new EventHandler(TextBoxSearch_TextChanged);
            this.Controls.Add(textBoxSearch);
            this.Controls.Add(listBoxResults);
            textBoxSearch.Select();
            try
            {
                csvLines = File.ReadAllLines(@"I:\datafile\control\Database.csv");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception:\n" + ex.Message);
            }
        }

        private void TextBoxSearch_TextChanged(object sender, EventArgs e)
        {
            label3.Text = "";
            string searchText = textBoxSearch.Text.ToLower();
            listBoxResults.Items.Clear();

            foreach (var line in csvLines)
            {
                var fields = line.Split(',');
                if (fields[4].ToLower().Contains(searchText))
                {
                    string field3Padded = fields[3].PadRight(21); // Pad field 3 to be between 1 and 21 characters long
                    string field4Padded = fields[4].PadRight(21);
                    string field12Padded = fields[12].PadRight(20);
                    string displayText = $"{fields[1]}\t{fields[2]}\t{field3Padded}\t{field4Padded}\t{field12Padded}\t{fields[14]}";
                    listBoxResults.Items.Add(displayText);
                }
            }
            timer.Stop();
            TimeSpan timeTaken = timer.Elapsed;
            string foo = "Time taken: " + timeTaken.ToString(@"m\:ss\.fff");
            label3.Text = foo;
        }


        private void listBoxResults_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxResults.SelectedItem == null) return;

            
            string selectedItem = listBoxResults.SelectedItem.ToString();
            var selectedFields = selectedItem.Split('\t');

            // Assuming you have variables to store the selected fields
            string column1 = selectedFields[0].Trim();
            string column2 = selectedFields[1].Trim();
            string column3 = selectedFields[2].Trim();
            string column4 = selectedFields[3].Trim();
            string column5 = selectedFields[4].Trim();
            string column6 = selectedFields[5].Trim();

            // Now you can use these variables as needed
            MessageBox.Show($"Column 1: {column1}\nColumn 2: {column2}\nColumn 3: {column3}\nColumn 4: {column4}\nColumn 5: {column5}\nColumn 6: {column6}");

            // Update label to show selected item
            IsFound = column1;
            label2.Text = "Selected: " + IsFound;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            EnterServiceCustMenu f2 = new EnterServiceCustMenu();
            f2.Show();
        }
    }
}
