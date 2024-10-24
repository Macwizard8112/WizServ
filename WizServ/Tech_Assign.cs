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
using Microsoft.Win32;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Media;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing.Printing;

namespace WizServ
{
    public partial class Tech_Assign : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        private readonly string TECHASSIGN = @"I:\\Datafile\\Control\\Tech_Assign3.csv";
        private readonly string DATABASE = @"I:\\Datafile\\Control\\Database.CSV";
        public readonly string Tech5 = @"I:\\Datafile\\Control\\NOEL.CSV";
        public readonly string Tech5Sorted = @"I:\\Datafile\\Control\\NOELSORTED.CSV";
        public readonly string Tech5Sorted2nd = @"I:\\Datafile\\Control\\NOELSORTED2.CSV";
        public readonly string Tech1 = @"I:\\Datafile\\Control\\COLE.CSV";
        public readonly string Tech1Sorted = @"I:\\Datafile\\Control\\COLESORTED.CSV";
        public readonly string Tech1Sorted2nd = @"I:\\Datafile\\Control\\COLESORTED2.CSV";
        public readonly string Tech3 = @"I:\\Datafile\\Control\\DEREK.CSV";
        public readonly string Tech3Sorted = @"I:\\Datafile\\Control\\DEREKSORTED.CSV";
        public readonly string Tech3Sorted2nd = @"I:\\Datafile\\Control\\DEREKSORTED2.CSV";
        private int loopCount, loop;
        public int Tech1TotalClaims, Tech3TotalClaims, Tech5TotalClaims;
        private int Tech3ClaimCount;
        public int Tech4TotalClaims, Tech2TotalClaims, tc2 = 30;
        public string recall, PCNAME;
        public string Tech1ReadString, Tech2ReadString, Tech3ReadString, Tech4ReadString, Tech5ReadString;
        private string msg = "Wizard Technician by Ordered Claim Assignments";
        public bool hide = false, ticked = false;
        public int pass;
        private object newLine;
        private PrintDocument docToPrint;
        private string stringToPrint, rtftext, rtftext2, rtftext3, rtftext4;
        public bool HasMorePages { get; private set; }

        public Tech_Assign()
        {
            InitializeComponent();
            PCNAME = Version.PCNAME;
            this.docToPrint = new PrintDocument();
            label7.Text = msg;
            timer1.Enabled = true;
            timer1.Start();
            timer1.Interval = (1000 * 60);  // 1000 * 60 = 60 seconds - refresh rate
            timer2.Enabled = true;
            timer2.Start();
            timer2.Interval = 1000;
            Icon = image100;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = false;
            this.Visible = true;
            this.Focus();
            if (WindowState == FormWindowState.Minimized)
                WindowState = FormWindowState.Normal;
            else
            {
                TopMost = true;
                Focus();
                BringToFront();
            }
            textBox1.Text = "Approved Claims  " + DateTime.Now.ToString() + "\n";
            textBox1.Text = textBox1.Text + "Start ordering Parts" + "\n";
            textBox1.Text = textBox1.Text + "Claim #" + "\t" + "Appr/Decl" + "\t" + "Date" + "\t\t" + "Time" + "\t\t" + "Technician" + "\n\n";
            GetData();
        }

        private void docToPrintCustom(object sender, PrintPageEventArgs e)
        {
            Font PrintFont = this.richTextBox1.Font;
            SolidBrush PrintBrush = new SolidBrush(Color.Black);

            int LinesPerPage = 0;
            int charactersOnPage = 0;

            e.Graphics.MeasureString(stringToPrint, PrintFont, e.MarginBounds.Size, StringFormat.GenericTypographic,
                out charactersOnPage, out LinesPerPage);

            e.Graphics.DrawString(stringToPrint, PrintFont, PrintBrush, e.MarginBounds, StringFormat.GenericTypographic);

            stringToPrint = stringToPrint.Substring(charactersOnPage);

            e.HasMorePages = (stringToPrint.Length > 0);

            PrintBrush.Dispose();
        }

        private int linesPrinted;
        private string[] lines;

        private void OnPrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;
            Brush brush = new SolidBrush(richTextBox1.ForeColor);

            while (linesPrinted < lines.Length)
            {
                e.Graphics.DrawString(lines[linesPrinted++],
                    richTextBox1.Font, brush, x, y);
                y += 15;
                if (y >= e.MarginBounds.Bottom)
                {
                    e.HasMorePages = true;
                    return;
                }
            }

            linesPrinted = 0;
            e.HasMorePages = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Version.From == "MainMenu")
            {
                this.Close();
                Hide();
                MainMenu f2 = new MainMenu();
                f2.Show();
            }
            else
            {
                this.Close();
            }
        }

        public void GetData()
        {
            richTextBox1.Text = ""; // Tech 1 Cole
            richTextBox2.Text = ""; // Tech 2
            richTextBox3.Text = ""; // Tech 3
            richTextBox4.Text = ""; // Tech 5
            richTextBox5.Text = ""; // Tech 4

            try
            {
                StreamReader reader = new StreamReader(TECHASSIGN, Encoding.GetEncoding("Windows-1252"));
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

                loopCount = 0;
                loop = 0;
                Tech5ReadString = "";
                Tech3ClaimCount = 0;

                var csvTech1 = new StringBuilder();
                var csvTech2 = new StringBuilder();
                var csvTech3 = new StringBuilder();
                var csvTech4 = new StringBuilder();
                var csvTech5 = new StringBuilder();


                while (!reader.EndOfStream)
                {
                    var lineRead = reader.ReadLine();
                    var values = lineRead.Split(',');

                    listA.Add(values[0]);       //  Claim_No
                    listB.Add(values[1]);       //  Date_IN
                    listC.Add(values[2]);       //  War_Note
                    listD.Add(values[3]);       //  Tech
                    listE.Add(values[4]);       //  Status
                    listF.Add(values[5]);       //  WH Location
                    listG.Add(values[6]);       //  Refb_Code
                    listH.Add(values[7]);       //  EST
                    listI.Add(values[8]);       //  Rush
                    listJ.Add(values[8]);       //  Recall

                    var isrecall = listC[loopCount];
                    var tech = listD[loopCount].ToUpper();
                    var loc = listF[loopCount].ToUpper();
                    var priority = listI[loopCount];
                    var approved = listH[loopCount];
                    var recall = listJ[loopCount];

                    if (priority == "N")    // Normal Priority
                    {
                        priority = "--";
                        hide = false;
                    }
                    if (priority == "NP")    // Normal Priority
                    {
                        priority = "NP";
                        hide = false;
                    }
                    if (priority == "Y")    // High Priority
                    {
                        priority = "P-";
                        hide = false;
                    }
                    if (priority == "R")    // Recall Claim
                    {
                        priority = "R-";
                        hide = false;
                    }
                    if (priority == "A")   // Parts Arrived
                    {
                        priority = "A-";
                        hide = false;
                    }
                    
                    if (isrecall.Contains("RECALL"))
                    {
                        priority = "RC";
                        hide = false;
                    }

                    if (recall == "R")  // Recall Claim
                    {
                        priority = "R-";
                        hide = false;
                    }
                    
                    if (loc == ".") // Warehouse location
                    {
                        loc = "FC";
                    }

                    if (tech == "COLE")
                    {
                        richTextBox1.Text = richTextBox1.Text + priority + " " + listA[loopCount] + "  " + loc + "\n";
                        Tech1TotalClaims++;
                    }
                    if (tech == "COLE" && hide == false)
                    {
                        Tech1ReadString += priority + " " + listA[loopCount] + "  " + loc + "," + "\n";
                        var first = Tech1ReadString[0].ToString();
                        newLine = string.Format("{0}", first);
                        csvTech1.AppendLine(Tech1ReadString);
                    }
                    if (tech == "DEREK")
                    {
                        if (hide == true)
                        {
                            if (priority == "RC")
                            {
                                richTextBox2.Text = richTextBox2.Text + priority + " " + listA[loopCount] + "  " + loc + "\n";
                                Tech3TotalClaims++;
                            }
                        }
                        else
                        {
                            if (hide != true)
                            {
                                richTextBox2.Text = richTextBox2.Text + priority + " " + listA[loopCount] + "  " + loc + "\n";
                                Tech3TotalClaims++;
                            }
                        }
                        if (tech == "DEREK" && hide == false)
                        {
                            Tech3ReadString += priority + " " + listA[loopCount] + "  " + loc + "," + "\n";
                            var first = Tech3ReadString[0].ToString();
                            newLine = string.Format("{0}", first);
                            csvTech3.AppendLine(Tech3ReadString);
                        }
                    }
                    if (tech == "WILLIAM")
                    {
                        if (hide == true)
                        {
                            if (priority == "RC")
                            {
                                richTextBox3.Text = richTextBox3.Text + priority + " " + listA[loopCount] + "  " + loc + "\n";
                                Tech2TotalClaims++;
                            }
                        }
                        if (hide != true)
                        {
                            richTextBox3.Text = richTextBox3.Text + priority + " " + listA[loopCount] + "  " + loc + "\n";
                            Tech2TotalClaims++;
                        }
                    }
                    if (tech == "NOEL")
                    {
                        if (hide == true)
                        {
                            if (priority == "RC")
                            {
                                richTextBox4.Text = richTextBox4.Text + priority + " " + listA[loopCount] + "  " + loc + "\n";
                                Tech5TotalClaims++;
                            }
                        }
                        if (hide == false)
                        {
                            richTextBox4.Text = richTextBox4.Text + priority + " " + listA[loopCount] + "  " + loc + "\n";
                            Tech5TotalClaims++;
                        }
                        if (tech == "NOEL" && hide == false)
                        {
                            Tech5ReadString += priority + " " + listA[loopCount] + "  " + loc + "," + "\n";
                            var first = Tech5ReadString[0].ToString();
                            newLine = string.Format("{0}", first);
                            csvTech5.AppendLine(Tech5ReadString);
                        }
                    
                    }
                    if (tech == "BILLY")
                    {
                        richTextBox5.Text = richTextBox5.Text + priority + " " + listA[loopCount] + "  " + loc + "\n";
                        Tech4TotalClaims++;
                    }
                    loopCount++;
                    loop++;
                }
                reader.Close();
                label8.Text = "Claims: " + Tech1TotalClaims.ToString();
                label9.Text = "Claims: " + Tech3TotalClaims.ToString();
                label10.Text = "Claims: " + Tech2TotalClaims.ToString();
                label11.Text = "Claims: " + Tech5TotalClaims.ToString();
                label12.Text = "Claims: " + Tech4TotalClaims.ToString();
                label13.Text = " Total Claims: " + (Tech1TotalClaims + Tech3TotalClaims + Tech2TotalClaims + Tech5TotalClaims + Tech4TotalClaims).ToString() + " ";
                Tech1TotalClaims = 0;
                Tech3TotalClaims = 0;
                Tech2TotalClaims = 0;
                Tech5TotalClaims = 0;
                Tech4TotalClaims = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 341: Sorry an error has occured: " + ex.Message);
            }
            if (ticked == true)
            {
                richTextBox1.Text = "";
            }
            
            File.WriteAllText(Tech3, Tech3ReadString.ToString());
            WriteSortedCSVFileTech3();
            ShowCSVTech3();

            File.WriteAllText(Tech5, Tech5ReadString.ToString());
            WriteSortedCSVFileTech5();
            ShowCSV();

            File.WriteAllText(Tech1, Tech1ReadString.ToString());
            WriteSortedCSVFIleTech1();
        }

        private void WriteSortedCSVFIleTech1()
        {
            // Create the IEnumerable data source  
            string[] lines = File.ReadAllLines(@"I:\\Datafile\\Control\\COLE.CSV");
            // Create the query. Put field 0 first, then  
            // reverse and combine fields 0 and 1 from the old field  
            IEnumerable<string> query =
                from line in lines
                let x = line.Split(',')
                orderby x[0] descending
                select x[0];

            // Execute the query and write out the new file. Note that WriteAllLines  
            // takes a string[], so ToArray is called on the query.  
            File.WriteAllLines(@"I:\\Datafile\\Control\\COLESORTED.CSV", query.ToArray());
            richTextBox1.Text = "";
            WriteSortedCSVFileTech12();
            ShowCSVTech1();
        }

        private void WriteSortedCSVFileTech12()
        {
            // Create the IEnumerable data source  
            string[] lines = System.IO.File.ReadAllLines(@"I:\\Datafile\\Control\\COLE.CSV");
            // Create the query. Put field 0 first, then  
            // reverse and combine fields 0 and 1 from the old field  
            IEnumerable<string> query =
                from line in lines
                let x = line.Split(',')
                orderby x[0] ascending
                select x[0];

            // Execute the query and write out the new file. Note that WriteAllLines  
            // takes a string[], so ToArray is called on the query.  
            File.WriteAllLines(@"I:\\Datafile\\Control\\COLESORTED2.CSV", query.ToArray());
            richTextBox1.Text = "";
        }

        private void WriteSortedCSVFileTech5()
            {
                // Create the IEnumerable data source  
                string[] lines = System.IO.File.ReadAllLines(@"I:\\Datafile\\Control\\NOEL.CSV");
                // Create the query. Put field 0 first, then  
                // reverse and combine fields 0 and 1 from the old field  
                IEnumerable<string> query =
                    from line in lines
                    let x = line.Split(',')
                    orderby x[0] descending
                    select x[0];

                // Execute the query and write out the new file. Note that WriteAllLines  
                // takes a string[], so ToArray is called on the query.  
                File.WriteAllLines(@"I:\\Datafile\\Control\\NOELSORTED.CSV", query.ToArray());
            richTextBox4.Text = "";
            WriteSortedCSVFileTech52();
            ShowCSV2Tech12();
            }

        private void WriteSortedCSVFileTech52()
        {
            // Create the IEnumerable data source  
            string[] lines = System.IO.File.ReadAllLines(@"I:\\Datafile\\Control\\NOEL.CSV");
            // Create the query. Put field 0 first, then  
            // reverse and combine fields 0 and 1 from the old field  
            IEnumerable<string> query =
                from line in lines
                let x = line.Split(',')
                orderby x[0] ascending
                select x[0];

            // Execute the query and write out the new file. Note that WriteAllLines  
            // takes a string[], so ToArray is called on the query.  
            File.WriteAllLines(@"I:\\Datafile\\Control\\NOELSORTED2.CSV", query.ToArray());
            richTextBox4.Text = "";
        }

        private void WriteSortedCSVFileTech3()
        {
            // Create the IEnumerable data source  
            string[] lines = System.IO.File.ReadAllLines(@"I:\\Datafile\\Control\\DEREK.CSV");
            // Create the query. Put field 0 first, then  
            // reverse and combine fields 0 and 1 from the old field  
            IEnumerable<string> query =
                from line in lines
                let x = line.Split(',')
                orderby x[0] descending
                select x[0];

            // Execute the query and write out the new file. Note that WriteAllLines  
            // takes a string[], so ToArray is called on the query.  
            File.WriteAllLines(@"I:\\Datafile\\Control\\DEREKSORTED.CSV", query.ToArray());
            richTextBox4.Text = "";
            WriteSortedCSVFileTech32();
            ShowCSV2Tech32();
        }

        private void WriteSortedCSVFileTech32()
        {
            // Create the IEnumerable data source  
            string[] lines = System.IO.File.ReadAllLines(@"I:\\Datafile\\Control\\NOEL.CSV");
            // Create the query. Put field 0 first, then  
            // reverse and combine fields 0 and 1 from the old field  
            IEnumerable<string> query =
                from line in lines
                let x = line.Split(',')
                orderby x[0] ascending
                select x[0];

            // Execute the query and write out the new file. Note that WriteAllLines  
            // takes a string[], so ToArray is called on the query.  
            File.WriteAllLines(@"I:\\Datafile\\Control\\NOELSORTED2.CSV", query.ToArray());
            richTextBox4.Text = "";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                checkBox1.Text = "Show all";
                ShowCSV2();
            }
            if (checkBox1.Checked == false)
            {
                checkBox1.Text = "Normal";
                richTextBox4.Text = "";
                ShowCSV();
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox2.Checked == true)
            {
                checkBox2.Text = "Show all";
                //ShowCSV2Tech12();
                ShowCSVTech1Clicked();
            }
            if (checkBox2.Checked == false)
            {
                checkBox2.Text = "Normal";
                richTextBox1.Text = "";
                ShowCSV2Tech12();
                ShowCSVTech1Clicked();
            }
        }

        private void ShowCSV()
        {
            // Open the file using a StreamReader
            using (var reader = new StreamReader(Tech5Sorted))    // Sorted CSV file
            {
                // Read the rest of the file
                while (!reader.EndOfStream)
                {
                    // Read the first line of the file
                    var line = reader.ReadLine();
                    // Split the data line into an array of values
                    var values = line.Split(',');
                    foreach (var value in values)
                    {
                        if (checkBox1.Checked == true)
                        {
                            richTextBox4.Text += (value + "\n");
                        }
                        else
                        {
                            if (value.Substring(0, 1) == "P")
                            {
                                richTextBox4.Text += (value + "\n");
                            }
                            if (value.Substring(0, 1) == "R")
                            {
                                richTextBox4.Text += (value + "\n");
                            }
                            if (value.Substring(0, 1) == "A")
                            {
                                richTextBox4.Text += (value + "\n");
                            }
                        }
                    }
                }
            }
        }



        private void ShowCSV2()
        {
            // Open the file using a StreamReader
            using (var reader = new StreamReader(Tech5Sorted2nd))    // Sorted CSV file
            {
                // Read the rest of the file
                while (!reader.EndOfStream)
                {
                    // Read the first line of the file
                    var line = reader.ReadLine();
                    // Split the data line into an array of values
                    var values = line.Split(',');
                    foreach (var value in values)
                    {
                        if (checkBox1.Checked == true)  // Show remaining claims sorted by Claim #
                        {
                            if (value.StartsWith("-"))
                            {
                                richTextBox4.Text += (value + "\n");
                            }
                        }
                    }
                }
            }
        }

        private void ShowCSV2Tech12()
        {
            if (checkBox1.Checked == true)
            {
                //var t = richTextBox1.Text;
                richTextBox1.Text = "";
                //richTextBox1.Text = t;
            }
            // Open the file using a StreamReader
            using (var reader = new StreamReader(Tech1Sorted2nd))    // Sorted CSV file
            {
                // Read the rest of the file
                while (!reader.EndOfStream)
                {
                    // Read the first line of the file
                    var line = reader.ReadLine();
                    // Split the data line into an array of values
                    var values = line.Split(',');
                    foreach (var value in values)
                    {
                        if (checkBox1.Checked == true)  // Show remaining claims sorted by Claim #
                        {
                            if (value.StartsWith("-"))
                            {
                                richTextBox1.Text += (value + "\n");
                            }
                        }
                    }
                }
            }
        }

        private void ShowCSVTech1()
        {
            // Open the file using a StreamReader
            using (var reader = new StreamReader(Tech1Sorted))    // Sorted CSV file
            {
                // Read the rest of the file
                while (!reader.EndOfStream)
                {
                    // Read the first line of the file
                    var line = reader.ReadLine();
                    // Split the data line into an array of values
                    var values = line.Split(',');
                    foreach (var value in values)
                    {
                        if (checkBox2.Checked == true)
                        {
                            richTextBox1.Text += (value + "\n");
                        }
                        else
                        {
                            if (value.Substring(0, 1) == "P")
                            {
                                richTextBox1.Text += (value + "\n");
                            }
                            if (value.Substring(0, 1) == "R")
                            {
                                richTextBox1.Text += (value + "\n");
                            }
                            if (value.Substring(0, 1) == "A")
                            {
                                richTextBox1.Text += (value + "\n");
                            }
                        }
                    }
                }
            }         
        }

        private void ShowCSVTech1Clicked()
        {
            var t = richTextBox1.Text;
            richTextBox1.Text = "";
            // Open the file using a StreamReader
            using (var reader = new StreamReader(Tech1Sorted))    // Sorted CSV file
            {
                // Read the rest of the file
                while (!reader.EndOfStream)
                {
                    // Read the first line of the file
                    var line = reader.ReadLine();
                    // Split the data line into an array of values
                    var values = line.Split(',');
                    foreach (var value in values)
                    {
                        if (checkBox2.Checked == true)
                        {
                            richTextBox1.Text += (value + "\n");
                        }
                        else
                        {
                            if (value.Substring(0, 1) == "P")
                            {
                                richTextBox1.Text += (value + "\n");
                            }
                            if (value.Substring(0, 1) == "R")
                            {
                                richTextBox1.Text += (value + "\n");
                            }
                            if (value.Substring(0, 1) == "A")
                            {
                                richTextBox1.Text += (value + "\n");
                            }
                        }
                    }
                }
            }
        }

        private void ShowCSV2Tech32()
        {
            // Open the file using a StreamReader
            using (var reader = new StreamReader(Tech1Sorted2nd))    // Sorted CSV file
            {
                // Read the rest of the file
                while (!reader.EndOfStream)
                {
                    // Read the first line of the file
                    var line = reader.ReadLine();
                    // Split the data line into an array of values
                    var values = line.Split(',');
                    foreach (var value in values)
                    {
                        if (checkBox1.Checked == true)  // Show remaining claims sorted by Claim #
                        {
                            if (value.StartsWith("-"))
                            {
                                richTextBox1.Text += (value + "\n");
                            }
                        }
                    }
                }
            }
        }

        private void ShowCSVTech3()
        {
            // Open the file using a StreamReader
            using (var reader = new StreamReader(Tech3Sorted))    // Sorted CSV file
            {
                // Read the rest of the file
                while (!reader.EndOfStream)
                {
                    // Read the first line of the file
                    var line = reader.ReadLine();
                    // Split the data line into an array of values
                    var values = line.Split(',');
                    foreach (var value in values)
                    {
                        if (checkBox1.Checked == true)
                        {
                            richTextBox2.Text += (value + "\n");
                        }
                        else
                        {
                            if (value.Substring(0, 1) == "P")
                            {
                                richTextBox2.Text += (value + "\n");
                            }
                            if (value.Substring(0, 1) == "R")
                            {
                                richTextBox2.Text += (value + "\n");
                            }
                            if (value.Substring(0, 1) == "A")
                            {
                                richTextBox2.Text += (value + "\n");
                            }
                        }
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            richTextBox2.Text = "";
            richTextBox3.Text = "";
            richTextBox4.Text = "";
            richTextBox5.Text = "";
            GetData();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (Version.PauseDBAccess == true)
            {
                Thread.Sleep(5000);
            }
            label6.Text = "Refresh: " + tc2.ToString() + " seconds.";
            tc2 = (tc2 - 1);
            if (tc2 == 1)
            {
                richTextBox1.Text = "";
                richTextBox2.Text = "";
                richTextBox3.Text = "";
                richTextBox4.Text = "";
                richTextBox5.Text = "";
                tc2 = 30;
                ticked = true;
                GetData();
            }
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (Version.From == "MainMenu")
                {
                    this.Close();
                    Hide();
                    MainMenu f2 = new MainMenu();
                    f2.Show();
                }
                else
                {
                    this.Close();
                }
            }
        }

        public void SetupPrintJob()
        {
            richTextBox1.SelectAll();
            richTextBox1.Font = new Font("Lucida Sans Unicode", 10);
            this.richTextBox1.SelectAll();
            richTextBox1.Font = new Font("Lucida Sans Unicode", 10);
        }

        public void SetupPrintJob2()
        {
            richTextBox1.SelectAll();
            richTextBox1.Font = new Font("Lucida Sans Unicode", 22);
            this.richTextBox1.SelectAll();
            richTextBox1.Font = new Font("Lucida Sans Unicode", 22);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            rtftext = "";
            // RichtextBox7.Text = all added up
            var Ttemp = richTextBox1.Text;
            rtftext4 = "All Technicians  Claims in que:\n" + "Priority, Claim#, WH Location:\n\n";
            rtftext2 = label1.Text + "\n" + richTextBox1.Text + "\n" + label2.Text + "\n" + richTextBox2.Text + "\n" +
                label3.Text + "\n" + richTextBox3.Text + "\n\n" + label5.Text + "\n" + richTextBox4.Text + "\n" +
                label4.Text + "\n" + richTextBox5.Text + "\n";
            richTextBox7.Text = rtftext2;

            rtftext3 = rtftext + rtftext4 + rtftext2;
            richTextBox1.Text = "";
            richTextBox1.Text = rtftext3;
            SetupPrintJob();
            PrintDialog myPrintDialog = new PrintDialog();
            myPrintDialog.AllowCurrentPage = true;
            myPrintDialog.AllowSelection = true;
            myPrintDialog.AllowSomePages = true;
            myPrintDialog.Document = docToPrint;
            if (myPrintDialog.ShowDialog() == DialogResult.OK)
            {
                StringReader reader = new StringReader(this.richTextBox1.Text);
                stringToPrint = reader.ReadToEnd();
                try
                {
                    this.docToPrint.PrintPage += new PrintPageEventHandler(this.docToPrintCustom);
                    this.docToPrint.Print();
                    richTextBox1.Text = "";
                    richTextBox1.Text = Ttemp;
                    richTextBox1.Text = "";
                    richTextBox1.Text = Ttemp;
                    SetupPrintJob2();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception Line 521:\n" + ex);
                }
            }
            else
            {
                richTextBox1.Text = "";
                richTextBox1.SelectAll();
                richTextBox1.Font = new Font("Lucida Console", 12);
                richTextBox1.Text = Ttemp;
            }
        }

        private void PrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //e.Graphics.DrawString(richTextBox1.Text, new Font(richTextBox1.Font.ToString(), richTextBox1.Font.Size), System.Drawing.Brushes.Black, 66, 50);
            e.Graphics.DrawString(richTextBox6.Text, new Font("Courier New", 10), Brushes.Black, 66, 50);
            e.HasMorePages = true;
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            char[] param = { '\n' };

            if (printDialog1.PrinterSettings.PrintRange == PrintRange.Selection)
            {
                lines = richTextBox6.SelectedText.Split(param);
            }
            else
            {
                lines = richTextBox6.Text.Split(param);
            }

            int i = 0;
            char[] trimParam = { '\r' };
            foreach (string s in lines)
            {
                lines[i++] = s.TrimEnd(trimParam);
            }
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                MessageBox.Show("Escape Pressed");
            }
        }
    }
}
