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
using System.Windows.Forms;
using System.Media;
using System.Threading;
using System.Drawing.Printing;

namespace WizServ
{
    public partial class ThisTechAssign : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        private readonly string TECHASSIGN = @"I:\\Datafile\\Control\\Tech_Assign.csv";
        private readonly string TECHASSIGN2 = @"I:\\Datafile\\Control\\Tech_Assign2.csv";
        private readonly string DATABASE = @"I:\\Datafile\\Control\\Database.CSV";
        public readonly string Notified = @"I:\\Datafile\\Control\\Notified.CSV";
        public readonly string filePath = @"I:\\Datafile\\Control\\NOEL.CSV";
        public readonly string filePath2 = @"I:\\Datafile\\Control\\NOELSORTED.CSV";
        public readonly string filePath3 = @"I:\\Datafile\\Control\\NOELSORTED2.CSV";
        public readonly string TechNames = @"I:\\Datafile\\Control\\Technician_Names.csv";
        static readonly string key = @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\lanmanserver\parameters";
        public readonly string computerDescription = (string)Registry.GetValue(key, "srvcomment", null);
        private int loopCount, loop, loopCount1, loop1;
        public int Tech1TotalClaims, Tech3TotalClaims, cconnor, Tech5TotalClaims, Tech4TotalClaims, Tech2TotalClaims, tc2 = 30;
        public string recall, WriteString, NoelReadString, PCNAME;
        private string msg = "Claims in Que";
        public bool hide = false;
        private int linesPrinted, tk1, tk2;
        private string[] lines;
        public int pass;
        private object newLine;
        public string TechicianNames1, TechicianNames2, TechicianNames3, TechicianNames4, TechicianNames5;
        private PrintDocument docToPrint;
        private string stringToPrint, rtftext, rtftext2, rtftext3, rtftext4;

        public ThisTechAssign()
        {
            InitializeComponent();
            GetTechnicianNames();
            this.docToPrint = new PrintDocument();
            label12.Text = "Claims are ALWAYS to be\ncompleted from oldest to\nnewest !";
            label14.Text = "Remember to move claims\nto Checking Parts Costs \nand Availability for:\nEstimates,\nParts Orders,\nEtc.";
            computerDescription = computerDescription.ToUpper();
            SetPCNames();
            label7.Text = PCNAME + "s " + msg;
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
            richTextBox1.Text = "";
            GetData();
            WriteSortedCSVFile();
            WriteSortedCSVFile2();
            checkBox1.Text = "Show all";
            ShowCSV2();
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            rtftext = DateTime.Today.ToString("dd/MM/yyyy" + "\n");
            rtftext4 = PCNAME + "s  Claims in que:\n" + "Priority, Claim#, WH Location:\n\n";
            rtftext2 = richTextBox1.Text;
            rtftext3 = rtftext + rtftext4 + rtftext2;
            richTextBox1.Text = "";
            richTextBox1.Text = rtftext3;
            SetupPrintJob();
            PrintDialog myPrintDialog = new PrintDialog();
            try
            {
                myPrintDialog.AllowCurrentPage = true;
                myPrintDialog.AllowSelection = true;
                myPrintDialog.AllowSomePages = true;
                myPrintDialog.Document = docToPrint;
                if (myPrintDialog.ShowDialog() == DialogResult.OK)
                {
                    StringReader reader = new StringReader(this.richTextBox1.Text);
                    stringToPrint = reader.ReadToEnd();
                    this.docToPrint.PrintPage += new PrintPageEventHandler(this.docToPrintCustom);
                    this.docToPrint.Print();
                    richTextBox1.Text = "";
                    richTextBox1.Text = rtftext2;
                    richTextBox1.Text = "";
                    richTextBox1.Text = rtftext2;
                    SetupPrintJob2();
                }
                else
                {
                    richTextBox1.Text = "";
                    richTextBox1.SelectAll();
                    richTextBox1.Font = new Font("Lucida Sans Unicode", 21);    // Set same as default font (Properties)
                    richTextBox1.Text = rtftext2;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: \n" + ex);
            }
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

        public void SetupPrintJob()
        {
            richTextBox1.SelectAll();
            richTextBox1.Font = new Font("Lucida Sans Unicode", 14);    // Set size to print
            this.richTextBox1.SelectAll();
            richTextBox1.Font = new Font("Lucida Sans Unicode", 14);
        }

        public void SetupPrintJob2()
        {
            richTextBox1.SelectAll();
            richTextBox1.Font = new Font("Lucida Sans Unicode", 21);    // Set size to display on screen
            this.richTextBox1.SelectAll();
            richTextBox1.Font = new Font("Lucida Sans Unicode", 21);
        }

        private void ThisTechAssign_Load(object sender, EventArgs e)
        {
            checkBox1.Text = "Show all";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                richTextBox1.Text = "";
                checkBox1.Text = "Show all";
                ShowCSV2();
            }
            if (checkBox1.Checked == false)
            {
                checkBox1.Text = "Normal";
                ShowCSV();
            }
        }

        public void GetTechnicianNames()
        {
            try
            {
                StreamReader reader = new StreamReader(TechNames, Encoding.GetEncoding("Windows-1252"));
                String line = reader.ReadLine();

                List<string> listA = new List<string>();
                List<string> listB = new List<string>();
                List<string> listC = new List<string>();
                List<string> listD = new List<string>();
                List<string> listE = new List<string>();

                loopCount1 = 0;
                loop1 = 0;

                while (!reader.EndOfStream)
                {
                    var lineRead = reader.ReadLine();
                    var values = lineRead.Split(',');

                    listA.Add(values[0]);       
                    listB.Add(values[1]);       
                    listC.Add(values[2]);       
                    listD.Add(values[3]);       
                    listE.Add(values[4]);       

                    TechicianNames1 = listA[loopCount1];    //  Tech 1  Cole
                    TechicianNames2 = listB[loopCount1];    //  Tech 2  William
                    TechicianNames3 = listC[loopCount1];    //  Tech 3  Derek
                    TechicianNames4 = listD[loopCount1];    //  Tech 4  Billy
                    TechicianNames5 = listE[loopCount1];    //  Tech 5  Noel

                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 265: Sorry an error has occured: " + ex.Message);
            }
        }

        public void SetPCNames()
        {
            switch (computerDescription)
            {
                case "TECH5":
                    PCNAME = TechicianNames5;
                    break;
                case "TECH4":
                    PCNAME = TechicianNames4;
                    break;
                case "WIZTECH3":
                    PCNAME = TechicianNames3;
                    break;
                case "WIZTECH2":
                    PCNAME = TechicianNames2;
                    break;
                case "WIZTECH1":
                    PCNAME = TechicianNames1;
                    break;
                case "PARTS2":
                    PCNAME = TechicianNames3;
                    break;
                default:
                    PCNAME = "PARTS1";
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //CopyTechAssignBack();
            tk1++;
            label15.Text = tk1.ToString();
            richTextBox1.Text = "";
            GetData();
            
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            label6.Text = "Refresh: " + tc2.ToString() + " seconds.";
            tc2 = tc2 - 1;
            if (tc2 == 1)
            {
                tk2++;
                label16.Text = tk2.ToString();
                richTextBox1.Text = "";
                tc2 = 30;
                CopyTechAssignBack();
                GetData();
            }
        }

        private void CopyTechAssign()
        {
            if (Version.PauseDBAccess == true)
            {
                Thread.Sleep(5000);
            }
            string sourceFile = TECHASSIGN;
            string destinationFile = TECHASSIGN2;
            try
            {
                File.Copy(sourceFile, destinationFile, true);
            }
            catch (IOException iox)
            {
                MessageBox.Show("Error occured during copy\n " + iox.Message);
            }
        }

        private void CopyTechAssignBack()
        {
            if (Version.PauseDBAccess == true)
            {
                Thread.Sleep(5000);
            }
            string sourceFile = TECHASSIGN2;
            string destinationFile = TECHASSIGN;
            try
            {
                File.Copy(sourceFile, destinationFile, true);
            }
            catch (IOException iox)
            {
                MessageBox.Show("Error occured during copy\n " + iox.Message);
            }
        }

        public void GetData()
        {
            CopyTechAssign();
            if (Version.PauseDBAccess == true)
            {
                Thread.Sleep(5000);
            }
            try
            {
                StreamReader reader = new StreamReader(TECHASSIGN2, Encoding.GetEncoding("Windows-1252"));
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

                loopCount = 0;
                loop = 0;
                WriteString = "";
                NoelReadString = "";
                //richTextBox1.Text = "";

                var csv = new StringBuilder();
                String[] foos = new String[] { NoelReadString };

                while (!reader.EndOfStream)
                {
                    var lineRead = reader.ReadLine();
                    var values = lineRead.Split(',');

                    listA.Add(values[0]);       //  Claim_No
                    listB.Add(values[1]);       //  Date_IN
                    listC.Add(values[2]);       //  War_Note
                    listD.Add(values[3]);       //  Tech
                    listE.Add(values[4]);       //  Status
                    listF.Add(values[5]);       //  Comment / WH Location
                    listG.Add(values[6]);       //  Refb_Code
                    listH.Add(values[7]);       //  EST
                    listI.Add(values[8]);       //  Rush

                    var isrecall = listC[loopCount];
                    var tech = listD[loopCount].ToUpper();
                    var loc = listF[loopCount].ToUpper();
                    var priority = listI[loopCount];
                    var approved = listH[loopCount];

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
                    if (priority == "AR")   // Parts Arrived
                    {
                        priority = "A-";
                        hide = false;
                    }

                    if (isrecall.Contains("RECALL"))
                    {
                        priority = "RC";
                        hide = true;
                    }

                    if (loc == ".")
                    {
                        loc = "FC";
                    }

                    if (tech == PCNAME)
                    {
                        if (hide == true)
                        {
                            if (priority == "RC")
                            {
                                richTextBox1.Text = richTextBox1.Text + priority + " " + listA[loopCount] + "  " + loc + "\n";
                                Tech1TotalClaims++;
                            }
                        }
                        else
                        {
                            if (hide == false)
                            {
                                richTextBox1.Text = richTextBox1.Text + priority + " " + listA[loopCount] + "  " + loc + "\n";
                                Tech1TotalClaims++;
                                NoelReadString += priority + " " + listA[loopCount] + "  " + loc + "," + "\n";
                                var first = NoelReadString[0].ToString();
                                newLine = string.Format("{0}", first);
                                csv.AppendLine(NoelReadString);
                            }
                        }
                    }
                    loopCount++;
                    loop++;
                }
                reader.Close();
                label8.Text = "Total Claims: " + Tech1TotalClaims.ToString();
                label13.Text = " Total Claims: " + (Tech1TotalClaims.ToString()) + " ";
                Tech1TotalClaims = 0;
                Tech3TotalClaims = 0;
                Tech2TotalClaims = 0;
                Tech5TotalClaims = 0;
                Tech4TotalClaims = 0;
            }
            
            catch (Exception ex)
            {
                MessageBox.Show("Error 482: Sorry an error has occured: " + ex.Message);
            }
            WriteString += richTextBox1.Text;
            File.WriteAllText(filePath, NoelReadString.ToString());
            WriteSortedCSVFile();
        }

        private void WriteSortedCSVFile()
        {
            // Create the IEnumerable data source  
            string[] lines = System.IO.File.ReadAllLines(@"I:\\Datafile\\Control\\NOEL.CSV");
            // Create the query. Put field 2 first, then  
            // reverse and combine fields 0 and 1 from the old field  
            IEnumerable<string> query =
                from line in lines
                let x = line.Split(',')
                orderby x[0] descending
                select x[0];

            // Execute the query and write out the new file. Note that WriteAllLines  
            // takes a string[], so ToArray is called on the query.  
            File.WriteAllLines(@"I:\\Datafile\\Control\\NOELSORTED.CSV", query.ToArray());
            //richTextBox1.Text = "";
            WriteSortedCSVFile2();
            ShowCSV();
        }

        private void WriteSortedCSVFile2()
        {
            // Create the IEnumerable data source  
            string[] lines = System.IO.File.ReadAllLines(@"I:\\Datafile\\Control\\NOEL.CSV");
            // Create the query. Put field 2 first, then  
            // reverse and combine fields 0 and 1 from the old field  
            IEnumerable<string> query =
                from line in lines
                let x = line.Split(',')
                orderby x[0] ascending
                select x[0];

            // Execute the query and write out the new file. Note that WriteAllLines  
            // takes a string[], so ToArray is called on the query.  
            File.WriteAllLines(@"I:\\Datafile\\Control\\NOELSORTED2.CSV", query.ToArray());
            //richTextBox1.Text = "";
        }

        private void ShowCSV()
        {
            if (Version.PauseDBAccess == true)
            {
                Thread.Sleep(5000);
            }
            // Open the file using a StreamReader
            using (var reader = new StreamReader(filePath2))    // Sorted CSV file
            {
                richTextBox1.Text = "";
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
                reader.Close();
            }
            if (richTextBox1.Text.Length <= 0)
            {
                richTextBox1.Text = "No Priority Claims\nClick Show All.";
                checkBox1.Visible = true;
                //checkBox1.Checked = true;
            }
            if (richTextBox1.Text.Length >= 1)
            {
                checkBox1.Visible = false;
                if (richTextBox1.Text == "No Priority Claims\nClick Show All.")
                {
                    checkBox1.Visible = true;
                    
                }
            }
        }

        private void ShowCSV2()
        {
            if (Version.PauseDBAccess == true)
            {
                Thread.Sleep(5000);
            }
            // Open the file using a StreamReader
            using (var reader = new StreamReader(filePath3))    // Sorted CSV file
            {
               // richTextBox1.Text = "";
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
                reader.Close();
            }
        }
    }
}
