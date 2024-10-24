using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Media;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace WizServ
{
public partial class EditEmailSuffixs : Form
{
        private readonly string EmailS = @"I:\\Datafile\\Control\\Email_Suffix.CSV";
        static readonly string key = @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\lanmanserver\parameters";
        public readonly string computerDescription = (string)Registry.GetValue(key, "srvcomment", null);
        public Icon image100 = Properties.Resources.WizServ;
        string msg = "Double-Click on Email to Edit:";
        string msg2 = "Enter NEW Email Suffix:";
        private int loopCount, loop;
        public string SelectedText, emailsuffix, TheText;

        public EditEmailSuffixs()
        {
            InitializeComponent();
            Icon = image100;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
            label1.Text = msg;
            label2.Text = msg2;
            label7.Visible = false;
            GetData();
        }

        public void GetSuffix()
        {
            try
            {
                StreamReader reader = new StreamReader(EmailS, Encoding.GetEncoding("Windows-1252"));
                String line = reader.ReadLine();

                List<string> listA = new List<string>();

                loopCount = 0;

                while (!reader.EndOfStream)
                {
                    var lineRead = reader.ReadLine();
                    var values = lineRead.Split(',');

                    listA.Add(values[0]);       //  war_prd         Unused

                    if (listA[loopCount].Contains(SelectedText))
                    {
                        emailsuffix = listA[loopCount].ToUpper();

                    }
                    loopCount++;
                }
                reader.Close(); // Close the open file
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 70: Sorry an error has occured: " + ex.Message);
            }
        }


        public void GetData()
        {
            richTextBox1.Text = "";
        try
        {
            StreamReader reader = new StreamReader(EmailS, Encoding.GetEncoding("Windows-1252"));
            String line = reader.ReadLine();

            List<string> listA = new List<string>();

            loopCount = 0;

            while (!reader.EndOfStream)
            {
                var lineRead = reader.ReadLine();
                var values = lineRead.Split(',');

                listA.Add(values[0]);       //  Dealer_czx

                richTextBox1.Text = richTextBox1.Text + listA[loopCount] + "\n";
                loop++;
                loopCount++;
            }
            reader.Close(); // Close the open file
        }
        catch (Exception ex)
            {
                MessageBox.Show("Error line 102: \n" + ex);
            }
        }

        private void richTextBox1_DoubleClick(object sender, EventArgs e)
        {
            SelectedText = richTextBox1.SelectedText;
            TheText = SelectedText;
            GetSuffix();
            SelectedText = emailsuffix;
            textBox2.Text = SelectedText;
        }

            private void richTextBox1_MouseUp(object sender, MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Right)
                {   //click event
                    ContextMenu contextMenu = new ContextMenu();
                    MenuItem menuItem = new MenuItem("Cut       Ctrl+X");
                    menuItem.Click += new EventHandler(CutAction);
                    contextMenu.MenuItems.Add(menuItem);
                    menuItem = new MenuItem("Copy    Ctrl+C");
                    menuItem.Click += new EventHandler(CopyAction);
                    contextMenu.MenuItems.Add(menuItem);
                    menuItem = new MenuItem("Paste    Ctrl+V");
                    menuItem.Click += new EventHandler(PasteAction);
                    contextMenu.MenuItems.Add(menuItem);

                    richTextBox1.ContextMenu = contextMenu;
                }
            }
            void CutAction(object sender, EventArgs e)
            {
                try
                {
                    richTextBox1.Cut();
                }
                catch (Exception)
                {
                    //
                }
            }

            void CopyAction(object sender, EventArgs e)
            {
                try
                {
                    Clipboard.SetText(richTextBox1.SelectedText);
                }
                catch (Exception ex)
                {
                    if (ex.ToString().Contains("Value cannot be null."))
                    {
                        // Ignore nothing selected
                    }
                    else
                    {
                        MessageBox.Show("Sorry an exception has occured.\n" + ex);
                    }
                }

            }

            void PasteAction(object sender, EventArgs e)
            {
                if (Clipboard.ContainsText())
                {
                    richTextBox1.Text += Clipboard.GetText(TextDataFormat.Text).ToString();
                }
            }

        public void UpdateSuffix()
        {
            SelectedText = textBox2.Text;
            string path = EmailS;
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
                                if (split[0] == emailsuffix)
                                {
                                    split[0] = SelectedText;
                                    if (emailsuffix.Length == 0)
                                    {
                                        split[1] = "AOL";
                                    }
                                    else
                                    {
                                        split[1] = emailsuffix;
                                    }
                                    split[2] = loopCount.ToString();
                                    line = String.Join(",", split);
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error: \n" + ex);
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
                    MessageBox.Show("Error line 213: \n" + ex);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var mre = new ManualResetEvent(false);
            ThreadPool.QueueUserWorkItem(_ => {
                ResortData();
                mre.Set();
            });
            mre.WaitOne();

            //ResortData();
            GetData();

        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                UpdateSuffix();
                GetData();
                textBox2.Text = "";
            }
        }

        public void ResortData()                    // Resort CSV file
        {
            { 
                // Create the IEnumerable data source  
                string[] lines = File.ReadAllLines(@"I:\\Datafile\\Control\\Email_Suffix.CSV");

                // Create the query. Put field 2 first, then  
                // reverse and combine fields 0 and 1 from the old field  
                IEnumerable<string> query =
                    from line in lines
                    let x = line.Split(',')
                    orderby x[0]                                // Sort on First Column x[0]
                    select x[0] + ", " + (x[1] + "," + x[2]);

                // Execute the query and write out the new file. Note that WriteAllLines  
                // takes a string[], so ToArray is called on the query.  
                File.WriteAllLines(@"I:\\Datafile\\Control\\Email_Suffix2.CSV", query.ToArray());
            }
        File.Delete(@"I:\\Datafile\\Control\\Email_Suffix.CSV");
        var filePath = @"I:\\Datafile\\Control\\Email_Suffix2.CSV";
        var newPath = @"I:\\Datafile\\Control\\Email_Suffix.CSV";
        File.Copy(filePath, newPath, true);
        }

        public void button4_Click(object sender, EventArgs e)
        {
            var mre = new ManualResetEvent(false);
            ThreadPool.QueueUserWorkItem(_ => {
                ResortData();
                mre.Set();
            });
            mre.WaitOne();

            //ResortData();
            GetData();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            label7.Visible = false;
            if (e.KeyCode == Keys.Enter)
            {
                var rows = new List<string>();
                rows.Add(textBox1.Text + ",");
                if (emailsuffix == null)
                {
                    rows.Add("AOL" + ",");
                }
                else
                {
                    rows.Add(emailsuffix + ",");
                }
                rows.Add(loopCount.ToString() + Environment.NewLine);
                // create the rows you need to append
                StringBuilder sb = new StringBuilder();
                foreach (string row in rows)
                    sb.AppendFormat("{0}", row);

                // flush all rows once time.
                File.AppendAllText(EmailS, sb.ToString(), Encoding.Default);
                textBox1.Text = "";
                Refesh();
                var mre = new ManualResetEvent(false);
                ThreadPool.QueueUserWorkItem(_ => {
                    ResortData();
                    mre.Set();
                });
                mre.WaitOne();

                //ResortData();
                GetData();
                label7.Visible = true;
            }
        }

        private void Refesh()
        {
            GetData();
        }

        private void button2_Click(object sender, EventArgs e)      // Add new row to csv file
        {
            var rows = new List<string>();
            rows.Add(textBox1.Text + ",");
            if (emailsuffix == null)
            {
                rows.Add("AOL" + ",");
            }
            else
            {
                rows.Add(emailsuffix + ",");
            }
            rows.Add(loopCount.ToString() + Environment.NewLine);
            // create the rows you need to append
            StringBuilder sb = new StringBuilder();
            foreach (string row in rows)
                sb.AppendFormat("{0}", row);

            // flush all rows once time.
            File.AppendAllText(EmailS, sb.ToString(), Encoding.Default);
            textBox1.Text = "";
            Refesh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            MainUtilitiesMenu f2 = new MainUtilitiesMenu();
            f2.Show();
        }
    }
}
