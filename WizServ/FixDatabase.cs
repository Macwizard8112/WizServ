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
    public partial class FixDatabase : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        public int rowIndex, mReplaceCount;
        private string mTxt = "This program validates the Datafile.csv file,\nLooking for blank spaces (missed columns).\nReplace '&&' with '+'.";
        private string oldName, newName;
        public Bitmap image1 = Properties.Resources.GreenBox;
        public Bitmap image2 = Properties.Resources.RedBox;

        public FixDatabase()
        {
            InitializeComponent();
            label1.Text = "";
            label2.Text = "";
            label3.Text = "";
            label1.Text = mTxt;
            label1.BackColor = Color.White;
            Icon = image100;
            MaximizeBox = false;
            MinimizeBox = false;
            ControlBox = false;
            SendMSG();
            CheckDB();
            RemoveAmpersand();
            RenameDB();
        }

        private void RenameDB()
        {
            oldName = @"I:\Datafile\Control\Database.csv";
            newName = @"I:\Datafile\Control\DatabaseBU.csv";
            if (File.Exists(oldName))
            {
                File.Copy(oldName, newName, true);
                File.Delete(oldName);
            }
            oldName = @"I:\Datafile\Control\DatabaseFixed.csv";
            newName = @"I:\Datafile\Control\Database.csv";
            if (File.Exists(oldName))
            {
                File.Copy(oldName, newName, true);
                File.Delete(oldName);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Hide();
            MainMenu f3 = new MainMenu();
            f3.Show();
        }

        private void SendMSG()
        {
            string message = "MAKE SURE EVERYONE IS AT\nMAIN SCREEN BEFORE USING !";
            string title = "WARNING !";
            MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
            DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                //this.Close();
            }
            else
            {

            }
        }

        private void RemoveAmpersand()
        {
            mReplaceCount = 0;
            // Open the input file
            string inputFilePath = @"I:\Datafile\Control\Database.csv";

            // Check if a file was selected
            if (string.IsNullOrEmpty(inputFilePath))
            {
                MessageBox.Show("Please select an input file.");
                return;
            }

            // Read the input file line by line
            using (StreamReader reader = new StreamReader(inputFilePath))
            {
                // Open the output file
                string outputFilePath = @"I:\Datafile\Control\DatabaseFixed.csv";

                // Check if an output file was selected
                if (string.IsNullOrEmpty(outputFilePath))
                {
                    MessageBox.Show("Please select an output file.");
                    return;
                }
                try
                {
                    using (StreamWriter writer = new StreamWriter(outputFilePath))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            // Replace "&" with "+"
                            string modifiedLine = line.Replace("&", "+");
                            mReplaceCount++;
                            // Write the modified line to the output file
                            writer.WriteLine(modifiedLine);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Replacement Failed.\n" + ex);
                }
            }
            label2.Text = mReplaceCount.ToString() + " replacements made.";
            label3.Text = "Replacement completed successfully.";
        }

        private void CheckDB()
        {
            listBox1.Items.Clear();
            string filePath = @"I:\Datafile\Control\Database.csv";
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    int rowIndex = 0;

                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] values = line.Split(',');

                        for (int colIndex = 0; colIndex < values.Length; colIndex++)
                        {
                            if (string.IsNullOrWhiteSpace(values[colIndex]))
                            {
                                rowIndex++;
                                listBox1.Items.Add("Empty cell found at Row " + rowIndex.ToString() + ",  Column " + colIndex.ToString());
                                pictureBox2.Image = image2;
                                rowIndex--;
                                return;
                            }
                        }
                        rowIndex++;
                    }
                    listBox1.Items.Add("No empty cells found.");
                    pictureBox2.Image = image1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading file:" + ex.Message);
            }
        }
    }
}
