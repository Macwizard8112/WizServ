using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using Microsoft.Win32;
using WizServ.Properties;
using WizServ.Resources;

namespace WizServ
{
    public partial class ByClaimNum : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        private string IsSelected = Version.IsSelected;
        private static readonly string Database = @"I:\\Datafile\\Control\\Database.CSV";
        private static readonly string Notes = @"I:\Datafile\Control\Notes\Claim_Notes.csv";
        public string ClaimNotes = "ClaimNotes.rtf";
        private static readonly string PartsUsed1 = @"I:\\Datafile\\Control\\Partsused.CSV";  // This is Read only CSV
        public string FileLocking = @"I:\\Datafile\\Control\\FileLocking.csv";
        //private readonly string Related = @"I:\\Datafile\\Control\\Related.CSV";
        static readonly string key = @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\lanmanserver\parameters";
        public readonly string computerDescription = (string)Registry.GetValue(key, "srvcomment", null);
        private string claim_no;
        private int loopCount, loop;
        private string IsClosed = "";
        public bool Found = false, ServiceBulletin, ServiceManual;
        public string Mex, TheFileIs, TheFileNameIs, yeardigit;
        private SoundPlayer Player = new SoundPlayer();
        private readonly StreamReader streamToPrint;
        private readonly Font printFont;
        public string FROM = Version.From, kkk, mModel, mBrand, mSerial, yModel, yBrand, ySerial;
        public string SELECTEDTEXT = Version.SELECTEDTEXT, SAVEDDATA;
        public decimal d4, d5, kkkShip;
        private decimal bBench;
        private decimal sShopFee;
        private decimal ddd;
        public string ptu1, ptu2, ptu3, ptu4, ptu5, ptu6, ptu7, ptu8, ptu9, ptu10;
        public string ppn1, ppn2, ppn3, ppn4, ppn5, ppn6, ppn7, ppn8, ppn9, ppn10;
        public string ppd1, ppd2, ppd3, ppd4, ppd5, ppd6, ppd7, ppd8, ppd9, ppd10;
        public int len1, len2, len3, len4, len5, len6, len7, len8, len9, len10;
        public int pploop;
        private int linesPrinted;
        private string[] lines;
        private decimal mRushFee;
        public string[] Claims1;

        public ByClaimNum()
        {
            InitializeComponent();
            //Version.IsSelected = " ";
            if (computerDescription.Contains("PARTS"))
            {
                this.Size = new Size(820, 790);
            }
            button6.Visible = false;
            button7.Visible = false;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GetClaimPrefix();
            SetupParts();
            this.Player.LoadCompleted += new AsyncCompletedEventHandler(Player_LoadCompleted);
            this.BackColor = Color.LightSeaGreen;
            panel4.BackColor = Color.LightSeaGreen;
            Icon = image100;
            label57.Text = "";
            button5.Visible = false;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
            claim_no = Version.Claim;
            if (IsSelected == null)
            {
                claim_no = Version.Claim;
            }
            else
            {
                claim_no = IsSelected;
                Version.Claim = IsSelected;
                Version.IsSelected = null;
            }
            
            Text = "Retrieve Claim by Claim Number - Page 1";
            ClearButLabels();
            DoLookup();
            GetData();
            CheckFileClosedStatus();
            if (Found == false)
            {
                string message = "Claim " + yeardigit + claim_no + " not found.";
                string title = "Not Found:";
                MessageBox.Show(message, title);
                Hide();
                RetrieveMenu f2 = new RetrieveMenu();
                f2.Show();
                return;
            }
            timer1.Interval = 1000;
            timer1.Enabled = true;
            label32.Visible = false;
            //IsFileLocked(FileInfo file);
            SetBackColor();
            CheckforNotes();
            CheckforF7Notes();
            CheckFileClosedStatus();
        }

        public async void PlayChurchBell()
        {
            await Task.Delay(500);
            SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.ChurchBell);
            simpleSound.Play();
        }

        public void SetupParts()
        {
            ptu1 = "";
            ptu2 = "";
            ptu3 = "";
            ptu4 = "";
            ptu5 = "";
            ptu6 = "";
            ptu7 = "";
            ptu8 = "";
            ptu9 = "";
            ptu10 = "";
            ppn1 = "";
            ppn2 = "";
            ppn3 = "";
            ppn4 = "";
            ppn5 = "";
            ppn6 = "";
            ppn7 = "";
            ppn8 = "";
            ppn9 = "";
            ppn10 = "";
            ppd1 = "";
            ppd2 = "";
            ppd3 = "";
            ppd4 = "";
            ppd5 = "";
            ppd6 = "";
            ppd7 = "";
            ppd8 = "";
            ppd9 = "";
            ppd10 = "";
        }

        public void GetClaimPrefix()
        {
            var date = DateTime.Now.ToShortDateString();
            var len = date.Length;
            var year = date.Substring((len - 2), 2);
            yeardigit = year.Substring(0, 1);
        }

        public void GetNotesInfo()          // Finish writing this part
        {
            var CN = claim_no;
            var CN1 = Notes + CN + ClaimNotes;
            StreamReader reader = new StreamReader(Notes, Encoding.GetEncoding("Windows-1252"));
            String line = reader.ReadLine();

            List<string> listA = new List<string>();
            List<string> listB = new List<string>();
            List<string> listC = new List<string>();

            loopCount = 0;

            while (!reader.EndOfStream)
            {
                var lineRead = reader.ReadLine();
                var values = lineRead.Split(',');

                listA.Add(values[0]);       //  claim_no        Claim Number
                listB.Add(values[1]);       //  Date            Date of Note
                listC.Add(values[2]);       //  Date            Date of Note
            }
        }

        public void CheckforNotes()
        {
            string f5file = @"I:\Datafile\Control\Notes\" + claim_no.ToString() + "ClaimNotes.rtf";
            TheFileNameIs = f5file;
            if (File.Exists(f5file))
            {
                TheFileNameIs = "Notes";
                TheFileIs = "Notes";
                label32.Visible = true;
                label32.Text = "*F5 Notes*";
                PlayAlarmSound();
                GetNotesInfo();
            }
        }

        private void Button6_Click_1(object sender, EventArgs e)
        {
            ServiceBulletinsAvail();
        }

        private async void Button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (yBrand.Contains("AB INT"))
                {
                    if (yModel.Contains("900A"))
                    {
                        Version.Serial = ySerial;
                        Version.Make = yBrand;
                        Version.Model = yModel;
                        Process.Start(new ProcessStartInfo()
                        {
                            FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\Schematics A\AB INTERNATIONAL",
                            UseShellExecute = true,
                            Verb = "open"
                        });
                    }
                    if (yModel.Contains("922A"))
                    {
                        Version.Serial = ySerial;
                        Version.Make = yBrand;
                        Version.Model = yModel;
                        Process.Start(new ProcessStartInfo()
                        {
                            FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\Schematics A\AB INTERNATIONAL",
                            UseShellExecute = true,
                            Verb = "open"
                        });
                    }
                    if (yModel.Contains("1100A"))
                    {
                        Version.Serial = ySerial;
                        Version.Make = yBrand;
                        Version.Model = yModel;
                        Process.Start(new ProcessStartInfo()
                        {
                            FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\Schematics A\AB INTERNATIONAL",
                            UseShellExecute = true,
                            Verb = "open"
                        });
                    }
                    if (yModel.Contains("9420A"))
                    {
                        Version.Serial = ySerial;
                        Version.Make = yBrand;
                        Version.Model = yModel;
                        Process.Start(new ProcessStartInfo()
                        {
                            FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\Schematics A\AB INTERNATIONAL",
                            UseShellExecute = true,
                            Verb = "open"
                        });
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 223 Exception:\n" + ex);
            }
            if (yBrand.Contains("ACCUPHASE"))
            {
                if (yModel.Contains("DP-77"))
                {
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\Schematics A\Accuphase\DP-77",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (yModel.Contains("DP 77"))
                {
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\Schematics A\Accuphase\DP-77",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                if (yModel.Contains("DP77"))
                {
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = @"\\NEWCLOUD\Cloud_D\Wizard\GoogleDrive\WizardElect\Schematics\Schematics A\Accuphase\DP-77",
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
            }
            }

        private void button8_Click(object sender, EventArgs e)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            Application.ExitThread();
            Close();
        }
        public void CheckforF7Notes()
        {
            switch (computerDescription.ToUpper())
            {
                case "PARTS2":
                    OpenF7Notes();
                    break;
                case "PARTS3":
                    OpenF7Notes();
                    break;
                case "COUNTER1":
                    OpenF7Notes();
                    break;
                case "COUNTER2":
                    OpenF7Notes();
                    break;
                case "COUNTER3":
                    OpenF7Notes();
                    break;
                case "GWENCOUNTER":
                    OpenF7Notes();
                    break;
            }
        }

        private void OpenF7Notes()
        {
            string f5file = @"I:\Datafile\Control\F7Notes\" + claim_no.ToString() + "ClaimNotes.rtf";
            TheFileNameIs = f5file;
            if (File.Exists(f5file))
            {
                TheFileNameIs = "Notes";
                TheFileIs = "Notes";
                label32.Visible = true;
                label32.Text = "*F7 Notes*";
                PlayAlarmSound();
            }
        }

        public void PlayAlarmSound()
        {
            this.LoadAsyncSound();      // Play sound Async
        }

        public void LoadAsyncSound()
        {
            try
            {
               this.Player.SoundLocation = "c:\\windows\\media\\Magic.wav";
               this.Player.LoadAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error loading sound Line 213");
            }
        }

        // This is the event handler for the LoadCompleted event.
        private void Player_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (Player.IsLoadCompleted)
            {
                try
                {
                    this.Player.Play();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error playing sound");
                }
            }
        }

        public void CheckFileOpenStatus()
        {
            List<String> lines = new List<String>();

            if (File.Exists(FileLocking))
            {
                using (StreamReader reader = new StreamReader(FileLocking))
                {
                    String line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Contains(","))
                        {
                            String[] split = line.Split(',');

                            if (split[0].Contains("Brand_DNR"))
                            {
                                split[1] = "Open";
                                line = String.Join(",", split);
                                TheFileIs = "Open";
                            }
                            if (split[0].Contains("Database"))
                            {
                                split[1] = "Open";
                                line = String.Join(",", split);
                                TheFileIs = "Open";
                            }
                            if (split[0].Contains("Dealers"))
                            {
                                split[1] = "Open";
                                line = String.Join(",", split);
                                TheFileIs = "Open";
                            }
                            if (split[0].Contains("Product"))
                            {
                                split[1] = "Open";
                                line = String.Join(",", split);
                                TheFileIs = "Open";
                            }
                            if (split[0].Contains("NextClaim"))
                            {
                                split[1] = "Open";
                                line = String.Join(",", split);
                                TheFileIs = "Open";
                            }
                            if (split[0].Contains("Related"))
                            {
                                split[1] = "Open";
                                line = String.Join(",", split);
                                TheFileIs = "Open";
                            }
                            if (split[0].Contains("Dealers_Number"))
                            {
                                split[1] = "Open";
                                line = String.Join(",", split);
                                TheFileIs = "Open";
                            }
                            if (split[0].Contains("Estimates"))
                            {
                                split[1] = "Open";
                                line = String.Join(",", split);
                                TheFileIs = "Open";
                            }
                            if (split[0].Contains("Gold"))
                            {
                                split[1] = "Open";
                                line = String.Join(",", split);
                                TheFileIs = "Open";
                            }
                            if (split[0].Contains("Notes"))
                            {
                                split[1] = "Open";
                                line = String.Join(",", split);
                                TheFileIs = "Open";
                            }
                        }
                        lines.Add(line);
                    }
                reader.Close();
                }
                using (StreamWriter writer = new StreamWriter(FileLocking, false))
                {
                    foreach (String line in lines)
                        writer.WriteLine(line);
                }
            }
        }

        public void CheckFileClosedStatus()
        {
            List<String> lines = new List<String>();

            if (File.Exists(FileLocking))
            {
                using (StreamReader reader = new StreamReader(FileLocking))
                {
                    String line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Contains(","))
                        {
                            String[] split = line.Split(',');

                            if (split[0].Contains("Brand_DNR"))
                            {
                                split[1] = "Closed";
                                line = String.Join(",", split);
                                TheFileIs = "Closed";
                            }
                            if (split[0].Contains("Database"))
                            {
                                split[1] = "Closed";
                                line = String.Join(",", split);
                                TheFileIs = "Closed";
                            }
                            if (split[0].Contains("Dealers"))
                            {
                                split[1] = "Closed";
                                line = String.Join(",", split);
                                TheFileIs = "Closed";
                            }
                            if (split[0].Contains("Product"))
                            {
                                split[1] = "Closed";
                                line = String.Join(",", split);
                                TheFileIs = "Closed";
                            }
                            if (split[0].Contains("NextClaim"))
                            {
                                split[1] = "Closed";
                                line = String.Join(",", split);
                                TheFileIs = "Closed";
                            }
                            if (split[0].Contains("Related"))
                            {
                                split[1] = "Closed";
                                line = String.Join(",", split);
                                TheFileIs = "Closed";
                            }
                            if (split[0].Contains("Dealers_Number"))
                            {
                                split[1] = "Closed";
                                line = String.Join(",", split);
                                TheFileIs = "Closed";
                            }
                            if (split[0].Contains("Estimates"))
                            {
                                split[1] = "Closed";
                                line = String.Join(",", split);
                                TheFileIs = "Closed";
                            }
                            if (split[0].Contains("Gold"))
                            {
                                split[1] = "Closed";
                                line = String.Join(",", split);
                                TheFileIs = "Closed";
                            }
                            if (split[0].Contains("Notes"))
                            {
                                split[1] = "Closed";
                                line = String.Join(",", split);
                                TheFileIs = "Closed";
                            }
                        }
                        lines.Add(line);
                    }
                reader.Close();
                }
                using (StreamWriter writer = new StreamWriter(FileLocking, false))
                {
                    foreach (String line in lines)
                        writer.WriteLine(line);
                }
            }
        }

        private void SetBackColor()
        {
            label15.BackColor = Color.LightSeaGreen;
            label16.BackColor = Color.LightSeaGreen;
            label17.BackColor = Color.LightSeaGreen;
            label18.BackColor = Color.LightSeaGreen;
            label19.BackColor = Color.LightSeaGreen;
            label20.BackColor = Color.LightSeaGreen;
            label21.BackColor = Color.LightSeaGreen;
            label22.BackColor = Color.LightSeaGreen;
            label23.BackColor = Color.LightSeaGreen;
            label24.BackColor = Color.LightSeaGreen;
            label25.BackColor = Color.LightSeaGreen;
            label26.BackColor = Color.LightSeaGreen;
            label27.BackColor = Color.LightSeaGreen;
            label28.BackColor = Color.LightSeaGreen;
            label29.BackColor = Color.LightSeaGreen;
            label30.BackColor = Color.LightSeaGreen;
            label31.BackColor = Color.LightSeaGreen;
            label33.BackColor = Color.LightSeaGreen;
            label34.BackColor = Color.LightSeaGreen;
            label35.BackColor = Color.LightSeaGreen;
            label36.BackColor = Color.LightSeaGreen;
            label37.BackColor = Color.LightSeaGreen;
            label39.BackColor = Color.LightSeaGreen;
            label40.BackColor = Color.LightSeaGreen;
            label45.BackColor = Color.LightSeaGreen;
        }

        private void ClearButLabels()
        {
            label6.Text = "";
            label7.Text = "";
            label8.Text = "";
            label9.Text = "";
            label10.Text = "";
            label22.Text = "";
            label23.Text = "";
            label24.Text = "";
            label25.Text = "";
            label26.Text = "";
            label27.Text = "";
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }

        private void Button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Hide();
                RetrieveMenu f2 = new RetrieveMenu();
                f2.Show();
            }
            if (e.KeyCode == Keys.F2)   // For PC keyboards
            {
                Hide();
                Version.From = "Retrieve1";
                PartsUsed f2 = new PartsUsed();
                f2.Show();
            }
            if (e.KeyCode == Keys.F6)   // For Macintosh Keyboards
            {
                Hide();
                Version.From = "Retrieve1";
                PartsUsed f9 = new PartsUsed();
                f9.Show();
            }
            if (e.KeyCode == Keys.F5)
            {
                Version.Claim = claim_no;
                Version.From = "Retrieve1";
                ByClaimNumF5Notes f3 = new ByClaimNumF5Notes();
                f3.Show();
            }
            if (e.KeyCode == Keys.F7)
            {
                switch (computerDescription.ToUpper())
                {
                    case "PARTS3":
                        Version.Claim = claim_no;
                        Version.From = "Retrieve1";
                        ByClaimNumF7Notes f4 = new ByClaimNumF7Notes();
                        f4.Show();
                        break;
                    case "COUNTER1":
                        Version.Claim = claim_no;
                        Version.From = "Retrieve1";
                        ByClaimNumF7Notes f5 = new ByClaimNumF7Notes();
                        f5.Show();
                        break;
                    case "COUNTER2":
                        Version.Claim = claim_no;
                        Version.From = "Retrieve1";
                        ByClaimNumF7Notes f6 = new ByClaimNumF7Notes();
                        f6.Show();
                        break;
                    case "COUNTER3":
                        Version.Claim = claim_no;
                        Version.From = "Retrieve1";
                        ByClaimNumF7Notes f7 = new ByClaimNumF7Notes();
                        f7.Show();
                        break;
                    case "GWENCOUNTER":
                        Version.Claim = claim_no;
                        Version.From = "Retrieve1";
                        ByClaimNumF7Notes f8 = new ByClaimNumF7Notes();
                        f8.Show();
                        break;
                }
                
            }
        }

        public void PlaySimpleSound()
        {
            SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.chimes);
            simpleSound.Play();
        }

        private void Button4_Click(object sender, EventArgs e)  // Page Setup Button - Print Claim
        {
            TextWriter txt = new StreamWriter("I:\\Datafile\\Doc\\ClaimNum.txt");
            txt.Write(richTextBox1.Text);
            txt.Close();
            var fileToOpen = "I:\\Datafile\\Doc\\ClaimNum.txt";
            if (!File.Exists(fileToOpen))
            {
                button1.PerformClick();
            }
            var process = new Process
            {
                StartInfo = new ProcessStartInfo()
                {
                    UseShellExecute = true,
                    FileName = fileToOpen
                }
            };

            process.Start();
            process.WaitForExit();
            txt.Close();
        }

        private void Button5_Click(object sender, EventArgs e)      // Tech Support Button
        {
            //Hide();
            JBLTechSupport f2 = new JBLTechSupport();
            f2.Show();
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            char[] param = { '\n' };

            if (printDialog1.PrinterSettings.PrintRange == PrintRange.Selection)
            {
                lines = richTextBox1.SelectedText.Split(param);
            }
            else
            {
                lines = richTextBox1.Text.Split(param);
            }

            int i = 0;
            char[] trimParam = { '\r' };
            foreach (string s in lines)
            {
                lines[i++] = s.TrimEnd(trimParam);
            }
        }


        private void OnPrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;
            Brush brush = new SolidBrush(richTextBox1.ForeColor);
            lines[0] = "200";
            if (lines.Length == null)
            {
                var t = 0;
            }
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

        private void pd_PrintPage(object sender, PrintPageEventArgs ev)
        {
            float linesPerPage = 0;
            float yPos = 0;
            int count = 0;
            float leftMargin = ev.MarginBounds.Left;
            float topMargin = ev.MarginBounds.Top;
            string line = null;

            // Calculate the number of lines per page.
            linesPerPage = ev.MarginBounds.Height /
               printFont.GetHeight(ev.Graphics);

            // Print each line of the file.
            while (count < linesPerPage &&
               ((line = streamToPrint.ReadLine()) != null))
            {
                yPos = topMargin + (count *
                   printFont.GetHeight(ev.Graphics));
                ev.Graphics.DrawString(line, printFont, Brushes.Black,
                   leftMargin, yPos, new StringFormat());
                count++;
            }

            // If more lines exist, print another page.
            if (line != null)
                ev.HasMorePages = true;
            else
                ev.HasMorePages = false;
        }


        private void pageSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextWriter txt = new StreamWriter("I:\\Datafile\\Doc\\ClaimNum.txt");
            txt.Write(richTextBox1.Text);
            txt.Close();
            var fileToOpen = "I:\\Datafile\\Doc\\ClaimNum.txt";
            if (!File.Exists(fileToOpen))
            {
                button1.PerformClick();
            }
            var process = new Process
            {
                StartInfo = new ProcessStartInfo()
                {
                    UseShellExecute = true,
                    FileName = fileToOpen
                }
            };

            process.Start();
            process.WaitForExit();
            txt.Close();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextWriter txt = new StreamWriter("I:\\Datafile\\Doc\\ClaimNum.txt");
            txt.Write(richTextBox1.Text);
            txt.Close();
            Process.Start("notepad.exe", "/p I:\\Datafile\\Doc\\ClaimNum.txt");
            PlaySimpleSound();
            txt.Close();
        }

        private void retrieveMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            ByClaimNumPg2 f2 = new ByClaimNumPg2();
            f2.Show();
        }

        private void mainMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            RetrieveMenu f2 = new RetrieveMenu();
            f2.Show();
        }

        private void mainMenuToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }

        private void getTechSupportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            JBLTechSupport f2 = new JBLTechSupport();
            f2.Show();
        }

        private void f5NotesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Version.Claim = claim_no;
            Version.From = "Retrieve1";
            ByClaimNumF5Notes f3 = new ByClaimNumF5Notes();
            f3.Show();
        }

        private void f2PartsListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            Version.From = "Retrieve1";
            PartsUsed f2 = new PartsUsed();
            f2.Show();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Hide();
            RetrieveMenu f2 = new RetrieveMenu();
            f2.Show();
        }

        private void ByClaimNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Hide();
                MainMenu f2 = new MainMenu();
                f2.Show();
            }
            if (e.KeyCode == Keys.F2)
            {
                Hide();
                Version.From = "Retrieve1";
                PartsUsed f2 = new PartsUsed();
                f2.Show();
            }
        }

        private void ByClaimNum_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void PrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //e.Graphics.DrawString(richTextBox1.Text, new Font(richTextBox1.Font.ToString(), richTextBox1.Font.Size), System.Drawing.Brushes.Black, 66, 50);
            e.Graphics.DrawString(richTextBox1.Text, new Font("Courier New", 10), Brushes.Black, 66, 50);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            PrintDocument printDocument1 = new PrintDocument();
            //printDocument1.DefaultPageSettings.PaperSize = new PaperSize("Custom", 500, 500);
            printDocument1.PrinterSettings = printDocument1.PrinterSettings;
            printDocument1.PrintPage += new PrintPageEventHandler(this.PrintDocument_PrintPage);
            PrintPreviewDialog printPreviewDialog1 = new PrintPreviewDialog
            {
                Document = printDocument1
            };
            DialogResult result = printPreviewDialog1.ShowDialog();
            if (result == DialogResult.OK)
                printDocument1.Print();
        }

        private void ByClaimNum_FormClosed(object sender, FormClosedEventArgs e)
        {
            Hide();
            RetrieveMenu f2 = new RetrieveMenu();
            f2.Show();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Hide();
            ByClaimNumPg2 f2 = new ByClaimNumPg2();
            f2.Show();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (label36.Text.Contains("REASSEMBLE"))
            {
                label36.ForeColor = Color.White;
                label36.Font = new Font("Ariel", 13, FontStyle.Bold);
            }
        }


        protected virtual bool IsFileLocked(FileInfo file)
        {
            try
            {
                using (FileStream stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    stream.Close();
                }
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }

            //file is not locked
            return false;
        }

        private void ByClaimNum_MouseUp(object sender, MouseEventArgs e)
        {
            Hide();
            ByClaimNumPg2 f2 = new ByClaimNumPg2();
            f2.Show();
        }

        private void DoLookup()
        {
            pploop = 1;
            loopCount = 0;
            try
            {
                StreamReader reader = new StreamReader(PartsUsed1);
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

                while (!reader.EndOfStream)
                {
                    var lineRead = reader.ReadLine();
                    var values = lineRead.Split(',');

                    listA.Add(values[0]);   // Qty
                    listB.Add(values[1]);   // Part_no
                    listC.Add(values[2]);   // Claim
                    listD.Add(values[3]);   // Description
                    listE.Add(values[4]);   // Price
                    listF.Add(values[5]);   // Claim_no
                    listG.Add(values[6]);   // Cost
                    listH.Add(values[7]);   // Part_Date
                    listI.Add(values[8]);   // Ppurch Date
                    listJ.Add(values[9]);   // Part in Claim

                    if (claim_no == listC[loopCount])
                    {
                        switch (pploop)
                        {
                            case 1:
                                ptu1 = listA[loopCount];
                                if (listB[loopCount].Length == 1)
                                {
                                    ppn1 = "000" + listB[loopCount];
                                }
                                else
                                {
                                    ppn1 = listB[loopCount];
                                }
                                if (listD[loopCount].Length <= 22)
                                {
                                    ppd1 = listD[loopCount];
                                }
                                else
                                {
                                    ppd1 = listD[loopCount].Substring(0, 23);
                                }
                                len1 = ptu1.Length + ppn1.Length + ppd1.Length;
                                pploop++;
                                break;
                            case 2:
                                ptu2 = listA[loopCount];
                                if (listB[loopCount].Length == 1)
                                {
                                    ppn2 = "000" + listB[loopCount];
                                }
                                else
                                {
                                    ppn2 = listB[loopCount];
                                }
                                if (listD[loopCount].Length <= 22)
                                {
                                    ppd2 = listD[loopCount];
                                }
                                else
                                {
                                    ppd2 = listD[loopCount].Substring(0, 23);
                                }
                                len2 = ptu2.Length + ppn2.Length + ppd2.Length;
                                pploop++;
                                break;
                            case 3:
                                ptu3 = listA[loopCount];
                                if (listB[loopCount].Length == 1)
                                {
                                    ppn3 = "000" + listB[loopCount];
                                }
                                else
                                {
                                    ppn3 = listB[loopCount];
                                }
                                if (listD[loopCount].Length <= 22)
                                {
                                    ppd3 = listD[loopCount];
                                }
                                else
                                {
                                    ppd3 = listD[loopCount].Substring(0, 23);
                                }
                                len3 = ptu3.Length + ppn3.Length + ppd3.Length;
                                pploop++;
                                break;
                            case 4:
                                ptu4 = listA[loopCount];
                                ppn4 = listB[loopCount];
                                ppd4 = listD[loopCount];
                                len4 = ptu4.Length + ppn4.Length + ppd4.Length;
                                pploop++;
                                break;
                            case 5:
                                ptu5 = listA[loopCount];
                                ppn5 = listB[loopCount];
                                ppd5 = listD[loopCount];
                                len5 = ptu5.Length + ppn5.Length + ppd5.Length;
                                pploop++;
                                break;
                            case 6:
                                ptu6 = listA[loopCount];
                                ppn6 = listB[loopCount];
                                ppd6 = listD[loopCount];
                                len6 = ptu6.Length + ppn6.Length + ppd6.Length;
                                pploop++;
                                break;
                            case 7:
                                ptu7 = listA[loopCount];
                                ppn7 = listB[loopCount];
                                ppd7 = listD[loopCount];
                                len7 = ptu7.Length + ppn7.Length + ppd7.Length;
                                pploop++;
                                break;
                            case 8:
                                ptu8 = listA[loopCount];
                                ppn8 = listB[loopCount];
                                ppd8 = listD[loopCount];
                                len8 = ptu8.Length + ppn8.Length + ppd8.Length;
                                pploop++;
                                break;
                            case 9:
                                ptu9 = listA[loopCount];
                                ppn9 = listB[loopCount];
                                ppd9 = listD[loopCount];
                                len9 = ptu9.Length + ppn9.Length + ppd9.Length;
                                pploop++;
                                break;
                            case 10:
                                ptu10 = listA[loopCount];
                                ppn10 = listB[loopCount];
                                ppd10 = listD[loopCount];
                                len10 = ptu10.Length + ppn10.Length + ppd10.Length;
                                pploop++;
                                break;
                            default:
                                break;
                        }

                    }
                    loopCount++;
                }
                reader.Close(); // Close the open file
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 1075: Sorry an error has occured: " + ex.Message);
            }
        }

        public void ServiceBulletinsAvail()
        {
            if (yBrand.Contains("AKAI"))
            {
                if (yModel.Contains("MPC1000"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();

                }
                if (yModel.Contains("MPC 1000"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("MPCLIVE!"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("MPC LIVE"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("MPC 2KXL"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("MPC2KXL"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("MPC200XL"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("MPC 200XL"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("MPC 3000"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("MPC3000"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("MPC 4000"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("MPC4000"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("MPC 5000"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("MPC5000"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("DPS12"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("DPS 12"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("DPS-12"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
            }
            if (yBrand.Contains("ALESIS"))
            {
                if (yModel.Contains("A6"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("A6 Andromeda"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("DM10"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("DM 10"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
            }
            if (yBrand.StartsWith("ALLEN"))
            {
                if (yModel.Contains("QU16"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("QU 16"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("QU-16"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("QU-24"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("QU24"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("QU 24"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("RPS11"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("RPS 11"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("RPS-11"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("S3000"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("S 3000"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("S-3000"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("S-5000"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("S5000"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("S 5000"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("S7000"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("S 7000"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("S-7000"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("SQ6"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("SQ 6"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("SQ-6"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("ZED428"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("ZED 428"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("ZED-428"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
            }
            if (yBrand.StartsWith("AMPEG"))
            {
                if (yModel.Contains("B5R"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("BA108-V2"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("BA108 V2"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("BA108V2"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("BA110-V2"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("BA110 V2"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("BA110V2"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("BA112-V2"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("BA112 V2"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("BA112V2"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel ==("BA-115"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel ==("BA 115"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel ==("BA115"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("BA-115 V2"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("BA 115 V2"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("BA115 V2"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("BA115-V2"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("BA-210 V2"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("BA 210 V2"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("BA210 V2"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("BA210-V2"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("LIQUIFIER"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("MICROCL"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("MICRO-CL"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("MICROVR"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("MICRO-VR"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("MICRO VR"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("OPTOCOMP"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("OPTO-COMP"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("OPTO COMP"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("PF500"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("PF 500"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("PF-500"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("SCRD1"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("SCR D1"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("SCR-D1"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("SVPPRO"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("SVP PRO"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("SVP-PRO"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("SVT2PRO"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("SVT2 PRO"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("SVT2-PRO"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("SVT3PRO"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("SVT3 PRO"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("SVT3-PRO"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("SVT4PRO"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("SVT4 PRO"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("SVT4-PRO"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("SVTCL"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("SVT CL"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("SVT-CL"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
            }
            if (yBrand == "B52")
            {
                if (yModel.Contains("AT-100"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("AT 100"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("AT100"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("AT212"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("AT 212"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("AT-212"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
            }
            if (yBrand.Contains("BLACKSTAR"))
            {
                if (yModel.Contains("HT-5"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("HT 5"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("HT5"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
            }
            if (yBrand.Contains("BOSS"))
            {
                if (yModel.Contains("SP-303"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("SP 303"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("SP303"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
            }
            if (yBrand.Contains("AVALON"))
            {
                if (yModel.Contains("AD-2022"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("AD 2022"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("AD2022"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("VT-737"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("VT 737"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("VT737"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
            }
            if (yBrand.Contains("CASIO"))
            {
                if (yModel.Contains("AP-200"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("AP 200"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("AP200"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("AP-620"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("AP 620"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("AP620"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
            }
            if (yBrand.Contains("ASHLEY"))
            {
                if (yModel.Contains("MX508"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("MX 508"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
                if (yModel.Contains("MX-508"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AKAI f2 = new AKAI();
                    f2.Show();
                }
            }
            if (yBrand.Contains("ADAMS AUDIO"))
            {
                if (yModel.Contains("AX7"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AdamAudio f2 = new AdamAudio();
                    f2.Show();
                }
                if (yModel.Contains("SUB"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AdamAudio f2 = new AdamAudio();
                    f2.Show();
                }
            }
            if (yBrand.Contains("ADAM AUDIO"))
            {
                if (yModel.Contains("AX7"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AdamAudio f2 = new AdamAudio();
                    f2.Show();
                }
                if (yModel.Contains("SUB"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AdamAudio f2 = new AdamAudio();
                    f2.Show();
                }
            }
            if (yBrand.Contains("AKAI"))
            {
                if (yModel.Contains("DPS12"))
                {
                    ServiceBulletin = true;
                    button6.Enabled = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AdamAudio f2 = new AdamAudio();
                    f2.Show();
                }
                if (yModel.Contains("DPS 12"))
                {
                    ServiceBulletin = true;
                    button6.Enabled = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AdamAudio f2 = new AdamAudio();
                    f2.Show();
                }
                if (yModel.Contains("DPS-12"))
                {
                    ServiceBulletin = true;
                    button6.Enabled = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    AdamAudio f2 = new AdamAudio();
                    f2.Show();
                }
            }
                if (yBrand.StartsWith("QSC"))
            {
                if (yModel.Contains("K8.2"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    QSCDecoder f2 = new QSCDecoder();
                    f2.Show();
                }
                if (yModel.Contains("K10.2"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    QSCDecoder f2 = new QSCDecoder();
                    f2.Show();
                }
                if (yModel.Contains("K12.2"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    QSCDecoder f2 = new QSCDecoder();
                    f2.Show();
                }
            }
            if (yBrand.StartsWith("YAMAHA"))
            {
                if (yModel.Contains("MOD"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    Yamaha f2 = new Yamaha();
                    f2.Show();
                }
                if (mModel.Contains("MONTAGE"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    Yamaha f2 = new Yamaha();
                    f2.Show();
                }
                if (mModel.Contains("MOTIF"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    Yamaha f2 = new Yamaha();
                    f2.Show();
                }
                if (yModel.Contains("P71"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    Yamaha f2 = new Yamaha();
                    f2.Show();
                }
                if (yModel.Contains("P-71"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    Yamaha f2 = new Yamaha();
                    f2.Show();
                }
                if (yModel.Contains("P 71"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    Yamaha f2 = new Yamaha();
                    f2.Show();
                }
                if (yModel.Contains("P 515"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    Yamaha f2 = new Yamaha();
                    f2.Show();
                }
                if (yModel.Contains("P-515"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    Yamaha f2 = new Yamaha();
                    f2.Show();
                }
                if (yModel.Contains("P515"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    Yamaha f2 = new Yamaha();
                    f2.Show();
                }
                if (yModel.Contains("PF70"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    Yamaha f2 = new Yamaha();
                    f2.Show();
                }
                if (yModel.Contains("PF 70"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    Yamaha f2 = new Yamaha();
                    f2.Show();
                }
                if (yModel.Contains("PF-70"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    Yamaha f2 = new Yamaha();
                    f2.Show();
                }
                if (yModel.Contains("PF80"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    Yamaha f2 = new Yamaha();
                    f2.Show();
                }
                if (yModel.Contains("PF 80"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    Yamaha f2 = new Yamaha();
                    f2.Show();
                }
                if (yModel.Contains("PF-80"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    Yamaha f2 = new Yamaha();
                    f2.Show();
                }
                if (yModel.Contains("PSR600"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    Yamaha f2 = new Yamaha();
                    f2.Show();
                }
                if (yModel.Contains("PSR 600"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    Yamaha f2 = new Yamaha();
                    f2.Show();
                }
                if (yModel.Contains("PSR-600"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    Yamaha f2 = new Yamaha();
                    f2.Show();
                }
                if (yModel.Contains("PSR2700"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    Yamaha f2 = new Yamaha();
                    f2.Show();
                }
                if (yModel.Contains("PSR 2700"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    Yamaha f2 = new Yamaha();
                    f2.Show();
                }
                if (yModel.Contains("PSR-2700"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    Yamaha f2 = new Yamaha();
                    f2.Show();
                }
                if (yModel.StartsWith("TF"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    Yamaha f2 = new Yamaha();
                    f2.Show();
                }
                if (yModel.StartsWith("THR"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    Yamaha f2 = new Yamaha();
                    f2.Show();
                }
                if (yModel.StartsWith("TIO"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    Yamaha f2 = new Yamaha();
                    f2.Show();
                }
                if (yModel.Contains("TYROS5"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    Yamaha f2 = new Yamaha();
                    f2.Show();
                }
                if (yModel.Contains("TYROS 5"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    Yamaha f2 = new Yamaha();
                    f2.Show();
                }
                if (yModel.Contains("TYROS-5"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    Yamaha f2 = new Yamaha();
                    f2.Show();
                }
                if (yModel.Contains("YDP-142"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    Yamaha f2 = new Yamaha();
                    f2.Show();
                }
                if (yModel.Contains("YDP 142"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    Yamaha f2 = new Yamaha();
                    f2.Show();
                }
                if (yModel.Contains("YDP142"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    Yamaha f2 = new Yamaha();
                    f2.Show();
                }
                if (yModel.Contains("YDP184"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    Yamaha f2 = new Yamaha();
                    f2.Show();
                }
                if (yModel.Contains("YDP 184"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    Yamaha f2 = new Yamaha();
                    f2.Show();
                }
                if (yModel.Contains("YDP-184"))
                {
                    ServiceBulletin = true;
                    Version.Serial = ySerial;
                    Version.Make = yBrand;
                    Version.Model = yModel;
                    Yamaha f2 = new Yamaha();
                    f2.Show();
                }
            }
        }

        public async void GetData()
        {
            CheckFileOpenStatus();
            try
            {
                ServiceBulletin = false;
                if (Version.DatabaseIsLocked == true)
                {
                    MessageBox.Show("Database in use, please wait a few seconds.");
                }
            }
            catch (Exception)
            {
                Thread.Sleep(4000);
            }
            try
            {
                StreamReader reader = new StreamReader(Database, Encoding.GetEncoding("Windows-1252"));
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
                List<string> listO = new List<string>();
                List<string> listP = new List<string>();
                List<string> listQ = new List<string>();
                List<string> listR = new List<string>();
                List<string> listS = new List<string>();
                List<string> listT = new List<string>();
                List<string> listU = new List<string>();
                List<string> listV = new List<string>();
                List<string> listW = new List<string>();
                List<string> listX = new List<string>();
                List<string> listY = new List<string>();
                List<string> listZ = new List<string>();
                List<string> listAA = new List<string>();
                List<string> listAB = new List<string>();
                List<string> listAC = new List<string>();
                List<string> listAD = new List<string>();
                List<string> listAE = new List<string>();
                List<string> listAF = new List<string>();
                List<string> listAG = new List<string>();
                List<string> listAH = new List<string>();
                List<string> listAI = new List<string>();
                List<string> listAJ = new List<string>();
                List<string> listAK = new List<string>();
                List<string> listAL = new List<string>();
                List<string> listAM = new List<string>();
                List<string> listAN = new List<string>();
                List<string> listAO = new List<string>();
                List<string> listAP = new List<string>();
                List<string> listAQ = new List<string>();
                List<string> listAR = new List<string>();
                List<string> listAS = new List<string>();
                List<string> listAT = new List<string>();
                List<string> listAU = new List<string>();
                List<string> listAV = new List<string>();
                List<string> listAW = new List<string>();
                List<string> listAX = new List<string>();
                List<string> listAY = new List<string>();
                List<string> listAZ = new List<string>();
                List<string> listBA = new List<string>();
                List<string> listBB = new List<string>();
                List<string> listBC = new List<string>();
                List<string> listBD = new List<string>();
                List<string> listBE = new List<string>();
                List<string> listBF = new List<string>();
                List<string> listBG = new List<string>();
                List<string> listBH = new List<string>();
                List<string> listBI = new List<string>();
                List<string> listBJ = new List<string>();
                List<string> listBK = new List<string>();
                List<string> listBL = new List<string>();
                List<string> listBM = new List<string>();
                List<string> listBN = new List<string>();
                List<string> listBO = new List<string>();
                List<string> listBP = new List<string>();
                List<string> listBQ = new List<string>();
                List<string> listBR = new List<string>();
                List<string> listBS = new List<string>();
                List<string> listBT = new List<string>();
                List<string> listBU = new List<string>();
                List<string> listBV = new List<string>();
                List<string> listBW = new List<string>();
                List<string> listBX = new List<string>();
                List<string> listBY = new List<string>();
                List<string> listBZ = new List<string>();
                List<String> listCA = new List<string>();
                List<String> listCB = new List<string>();

                loopCount = 0;

                while (!reader.EndOfStream)
                {
                    var lineRead = reader.ReadLine();
                    var values = lineRead.Split(',');

                    listA.Add(values[0]);       //  war_prd         Unused
                    listB.Add(values[1]);       //  claim_no        Claim Number
                    listC.Add(values[2]);       //  datein          Equipment Entry Date
                    listD.Add(values[3]);       //  fname           Customer First Name
                    listE.Add(values[4]);       //  lname           Customer Last Name
                    listF.Add(values[5]);       //  addr            Customer Address
                    listG.Add(values[6]);       //  city            Customer City
                    listH.Add(values[7]);       //  state           Customer State (2 char)
                    listI.Add(values[8]);       //  zip             Customer Zip Code XXXXX-XXXX
                    listJ.Add(values[9]);       //  hphone          Home Phone #
                    listK.Add(values[10]);      //  wphone          Work Phone #
                    listL.Add(values[11]);      //  prob_compl      Problem Complaint
                    listM.Add(values[12]);      //  brand           Manuf Brand
                    listN.Add(values[13]);      //  serv_no
                    listO.Add(values[14]);      //  model
                    listP.Add(values[15]);      //  Serial_no
                    listQ.Add(values[16]);      //  Total Estimate  $ $
                    listR.Add(values[17]);      //  Lab_Est         $ $
                    listS.Add(values[18]);      //  Part Estimate   $ $
                    listT.Add(values[19]);      //  Actual Cost     $ $
                    listU.Add(values[20]);      //  Deposit         $ $
                    listV.Add(values[21]);      //  Deposit Date
                    listW.Add(values[22]);      //  Postcard
                    listX.Add(values[23]);      //  Part_Prof       $ $
                    listY.Add(values[24]);      //  Profit          $ $
                    listZ.Add(values[25]);      //  Other Info
                    listAA.Add(values[26]);     //  Other Estimate  $ $
                    listAB.Add(values[27]);     //  Tax             $ $
                    listAC.Add(values[28]);     //  war_stat                Warranty Status
                    listAD.Add(values[29]);     //  purch_date              Purchase Date for Warranty Claim
                    listAE.Add(values[30]);     //  fthr_exp1               Further Explination C/C line 2
                    listAF.Add(values[31]);     //  frth_exp2               Further Explination C/C line 3
                    listAG.Add(values[32]);     //  Access                  Paid by Cash or Card
                    listAH.Add(values[33]);     //  DLV_Stat                Cust Pickup or OnSite Service
                    listAI.Add(values[34]);     //  Dname                   Dealer Name
                    listAJ.Add(values[35]);     //  Daddr                   Dealer Address
                    listAK.Add(values[36]);     //  DCity                   Dealer City
                    listAL.Add(values[37]);     //  DState                  Dealer State
                    listAM.Add(values[38]);     //  DZip                    Dealer Zip Code
                    listAN.Add(values[39]);     //  Dphone                  Dealer Phone Number
                    listAO.Add(values[40]);     //  TVorStereo              Skip
                    listAP.Add(values[41]);     //  Repr_cat                Number ? Column
                    listAQ.Add(values[42]);     //  Serv_Perf               Number ? Column
                    listAR.Add(values[43]);     //  Service                 Number ? Column
                    listAS.Add(values[44]);     //  Toj_Total               Claim Repair in Hours
                    listAT.Add(values[45]);     //  War_Note                Claim Status (Warr,Non-Warr, Parts Ordered, etc)
                    listAU.Add(values[46]);     //  Tech_Serv1              4 lines of what was reapired
                    listAV.Add(values[47]);     //  Tech_Serv2              4 lines of what was reapired
                    listAW.Add(values[48]);     //  Tech_Serv3              4 lines of what was reapired
                    listAX.Add(values[49]);     //  Tech_Serv4              4 lines of what was reapired
                    listAY.Add(values[50]);     //  Tech_ID                 2 letters of Tech Name
                    listAZ.Add(values[51]);     //  Tech                    Tech Name (COLE, DAVID, CONNER, etc)
                    listBA.Add(values[52]);     //  Tech_NO                 Tech ID Num (1 = Cole, 3 = David, etc)
                    listBB.Add(values[53]);     //  DTE_Compl               Date Complete
                    listBC.Add(values[54]);     //  DTE_Closed              Service Render Date
                    listBD.Add(values[55]);     //  Status                  On Bench, Parts Ordered, etc
                    listBE.Add(values[56]);     //  Comment                 Warehouse Location (A1, F2, G3, etc)
                    listBF.Add(values[57]);     //  Deal_No                 Dealer Name, School Name, etc
                    listBG.Add(values[58]);     //  Narda                   P or '.' - Ask Cole
                    listBH.Add(values[59]);     //  Distname                . XX XXX or Ship Date (We shipped unit)
                    listBI.Add(values[60]);     //  Distcode                Freight, Estimate, Recall or '.'
                    listBJ.Add(values[61]);     //  Product                 List of Model Types (Mixer, Powered Spkr, etc)
                    listBK.Add(values[62]);     //  Auth_Code               Tech Name (Cole, David, etc)
                    listBL.Add(values[63]);     //  Refb_Code               Warranty, Non-Warranty
                    listBM.Add(values[64]);     //  Microwave               Unknown Date - Ask Cole
                    listBN.Add(values[65]);     //  Estimate                ESTIMATE or NONE if requested Estimate
                    listBO.Add(values[66]);     //  Dealer_Num              Dealer Number or 999
                    listBP.Add(values[67]);     //  Cust_Extn               Unknown - Ask Cole
                    listBQ.Add(values[68]);     //  Claim_Num               'A' Claim Number A210403
                    listBR.Add(values[69]);     //  Company                 Company Name or N/A
                    listBS.Add(values[70]);     //  Real_Claim              Unused (Old new claim #)
                    listBT.Add(values[71]);     //  Email                   Customer/Dealer Email Address
                    listBU.Add(values[72]);     //  EST_YN                  Estimate Yes / No
                    listBV.Add(values[73]);     //  EST_TOTAL               Estimate Total $
                    listBW.Add(values[74]);     //  EST_PARTS               Estimate Parts $
                    listBX.Add(values[75]);     //  Rush                    Rush Y or N
                    listBY.Add(values[76]);
                    listBZ.Add(values[77]);
                    listCA.Add(values[78]);
                    listCB.Add(values[79]);

                    var mWarr = listA[loopCount];
                    var mClaim_NO = listB[loopCount];
                    var mDate_IN = listC[loopCount];
                    var mFname = listD[loopCount];
                    var mLname = listE[loopCount];
                    var mAddr = listF[loopCount];
                    var mCity = listG[loopCount];
                    var mState = listH[loopCount];
                    var mZip = listI[loopCount];
                    var mHphone = listJ[loopCount];
                    var mWPhone = listK[loopCount];
                    var mProblem = listL[loopCount];
                        mBrand = listM[loopCount];
                    var mServNo = listN[loopCount];
                        mModel = listO[loopCount];
                        mSerial = listP[loopCount];
                    var mWarranty = listBN[loopCount] + " " + listBL[loopCount];
                    var mFthr_exp1 = listAE[loopCount];
                    var mFthr_exp2 = listAF[loopCount];
                    var t = listAU[loopCount];
                    var mTS1 = listAU[loopCount];
                    var mTS2 = listAV[loopCount];
                    var mTS3 = listAW[loopCount];
                    var mts4 = listAX[loopCount];
                    var mTechNum = listBA[loopCount];
                    var mTech = listBC[loopCount];
                    var mBench = listBD[loopCount];
                    var mTheTech = listAZ[loopCount];
                    var COMPLETED = listBC[loopCount];
                    var mTheNewClaimNum = listBR[loopCount];
                    var mIsWarr = listBL[loopCount];
                    var mEmail = listBP[loopCount];
                    if (listBT[loopCount] != "NONE")
                    {
                        mEmail += ", " + listBT[loopCount];
                    }
                    var mEstimate = listBU[loopCount];
                    var mRush = listBX[loopCount];
                    var Est_Total = listBV[loopCount];
                    var Est_Parts = listBW[loopCount];
                    var CLOSED = listCA[loopCount];
                    var PICKUP = listCB[loopCount];

                    if (mTheNewClaimNum.Length >= 7)   // Convert new claim# to Remove the "A" prefix
                    {
                        var tt = mTheNewClaimNum;
                        var yy = mTheNewClaimNum.Length;
                        yy--;
                        var uu = tt.Substring(1, yy);
                        mTheNewClaimNum = uu;
                    }

                    if (claim_no.Length == 6)
                    {
                        if (mClaim_NO == claim_no || mClaim_NO == (yeardigit + mClaim_NO))
                        {
                            Found = true;

                            Version.Serial = mSerial;

                            label6.Text = mFname + " " + mLname;
                            label7.Text = mAddr;
                            label8.Text = mCity + ", " + mState + " " + mZip;
                            label9.Text = yeardigit + claim_no;
                            label10.Text = listBK[loopCount];
                            label11.Text = mHphone;
                            label14.Text = mWPhone;
                            label22.Text = mBrand;
                            label26.Text = COMPLETED;
                            if (listBU[loopCount] != "B")
                            {
                                try
                                {
                                    d4 = decimal.Parse(Est_Total);
                                    d5 = decimal.Parse(Est_Parts);
                                }
                                catch (Exception)
                                {
                                    MessageBox.Show("Database is damaged, notify Doc or Cole.\nThere is a comma in database.");
                                }
                                if (computerDescription.Contains("TECH"))
                                {
                                    if (mEstimate == "Y")
                                    {
                                        label54.Text = "Estimate: ";    // Convert text to decimal w/ $
                                        label57.Text = " Pending ";
                                    }
                                    if (mEstimate == "A")
                                    {
                                        label54.Text = "Estimate: ";     // Convert text to decimal w/ $
                                        label57.Text = " Approved ";
                                    }
                                    if (mEstimate == "N")
                                    {
                                        label54.Text = "Estimate: " + "Not Requested";   // Convert text to decimal w/ $
                                        label57.Text = " Not Needed ";
                                    }
                                    if (mEstimate == "_")
                                    {
                                        label54.Text = "Estimate: " + "DECLINED-REASSEMBLE";    // Convert text to decimal w/ $
                                        label57.Text = " DECLINED ";
                                    }
                                }
                                else
                                {
                                    if (mEstimate == "Y")
                                    {
                                        label54.Text = "Estimate: " + d4.ToString("C2") + " Parts: " + d5.ToString("C2");    // Convert text to decimal w/ $
                                        label57.Text = " Pending ";
                                    }
                                    if (mEstimate == "A")
                                    {
                                        label54.Text = "Estimate: " + d4.ToString("C2") + " Parts: " + d5.ToString("C2");    // Convert text to decimal w/ $
                                        label57.Text = " Approved ";
                                    }
                                    if (mEstimate == "N")
                                    {
                                        label54.Text = "Estimate: Not Requested";    // Convert text to decimal w/ $
                                        label57.Text = " Not Needed ";
                                    }
                                    if (mEstimate == "_")
                                    {
                                        label54.Text = "Estimate: " + d4.ToString("C2") + " Parts: " + d5.ToString("C2");    // Convert text to decimal w/ $
                                        label57.Text = " DECLINED ";
                                    }
                                   
                                }
                            }
                            else
                            {
                                label54.Text = "";
                            }
                            if (mBrand.Contains("AB INT"))      // Start Service Manual Add
                            {
                                if (mModel == "900A")
                                {
                                    button7.Visible = true;
                                    ServiceManual = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                }
                                if (mModel == "900A")
                                {
                                    button7.Visible = true;
                                    ServiceManual = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                }
                                if (mModel == "900A")
                                {
                                    button7.Visible = true;
                                    ServiceManual = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                }
                                if (mModel == "900A")
                                {
                                    button7.Visible = true;
                                    ServiceManual = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                }
                            }

                            if (mBrand.Contains("ACCUPHASE"))      // Start Service Manual Add
                            {
                                if (mModel == "DP-77")
                                {
                                    button7.Visible = true;
                                    ServiceManual = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                }
                                if (mModel == "DP 77")
                                {
                                    button7.Visible = true;
                                    ServiceManual = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                }
                                if (mModel == "DP77")
                                {
                                    button7.Visible = true;
                                    ServiceManual = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                }
                            }
                            if (ServiceManual == true)
                            {
                                PlayChurchBell();
                                await Task.Delay(700);
                                button7.Visible = true;
                            }


                            // End adding of new Service Manuals

                            if (mBrand.StartsWith("JBL"))
                            {
                                button5.Visible = true;
                                ySerial = mSerial;
                                yModel = mModel;
                                yBrand = mBrand;
                            }
                            if (mBrand.StartsWith("ALLEN"))
                            {
                                button5.Visible = true;
                                ySerial = mSerial;
                                yModel = mModel;
                                yBrand = mBrand;
                            }
                            if (mBrand.StartsWith("GALL"))
                            {
                                button5.Visible = true;
                                ySerial = mSerial;
                                yModel = mModel;
                                yBrand = mBrand;
                            }
                            if (mBrand.StartsWith("KRK"))
                            {
                                button5.Visible = true;
                                ySerial = mSerial;
                                yModel = mModel;
                                yBrand = mBrand;
                            }
                            if (mBrand.StartsWith("LINE6"))
                            {
                                button5.Visible = true;
                                ySerial = mSerial;
                                yModel = mModel;
                                yBrand = mBrand;
                            }
                            if (mBrand.StartsWith("CROWN"))
                            {
                                button5.Visible = true;
                                ySerial = mSerial;
                                yModel = mModel;
                                yBrand = mBrand;
                            }
                            if (mBrand.StartsWith("DBX"))
                            {
                                button5.Visible = true;
                                ySerial = mSerial;
                                yModel = mModel;
                                yBrand = mBrand;
                            }
                            if (mBrand.StartsWith("BIAMP"))
                            {
                                button5.Visible = true;
                                ySerial = mSerial;
                                yModel = mModel;
                                yBrand = mBrand;
                            }
                            if (mBrand.StartsWith("LEXICON"))
                            {
                                button5.Visible = true;
                                ySerial = mSerial;
                                yModel = mModel;
                                yBrand = mBrand;
                            }
                            if (mBrand.StartsWith("SOUNDCRAFT"))
                            {
                                button5.Visible = true;
                                ySerial = mSerial;
                                yModel = mModel;
                                yBrand = mBrand;
                            }
                            if (mBrand.StartsWith("MARTIN"))
                            {
                                button5.Visible = true;
                                ySerial = mSerial;
                                yModel = mModel;
                                yBrand = mBrand;
                            }
                            if (mBrand.StartsWith("AKG"))
                            {
                                button5.Visible = true;
                                ySerial = mSerial;
                                yModel = mModel;
                                yBrand = mBrand;
                            }
                            if (mBrand.StartsWith("DIGITECH"))
                            {
                                button5.Visible = true;
                                ySerial = mSerial;
                                yModel = mModel;
                                yBrand = mBrand;
                            }
                            if (mBrand.StartsWith("AMX"))
                            {
                                button5.Visible = true;
                                ySerial = mSerial;
                                yModel = mModel;
                                yBrand = mBrand;
                            }
                            if (mBrand.StartsWith("BSS"))
                            {
                                button5.Visible = true;
                                ySerial = mSerial;
                                yModel = mModel;
                                yBrand = mBrand;
                            }
                            if (mBrand.StartsWith("YAMAHA"))
                            {
                                button5.Visible = true;
                                ySerial = mSerial;
                                yModel = mModel;
                                yBrand = mBrand;
                            }
                            if (mBrand.StartsWith("MACKIE"))
                            {
                                button5.Visible = true;
                                ySerial = mSerial;
                                yModel = mModel;
                                yBrand = mBrand;
                            }
                            label23.Text = mModel;
                            label24.Text = mSerial;
                            Version.Serial = mSerial;
                            Version.Make = mBrand;
                            Version.Model = mModel;
                            if (mBrand.StartsWith("ALLEN"))
                            {
                                if (mModel.Contains("QU16"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("QU 16"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("QU-16"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("QU-24"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("QU24"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("QU 24"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("RPS11"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("RPS 11"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("RPS-11"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("S3000"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("S 3000"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("S-3000"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("S-5000"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("S5000"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("S 5000"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("S 7000"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("S-7000"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("S7000"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("SQ6"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("SQ 6"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("SQ-6"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("ZED-428"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("ZED428"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("ZED 428"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                            }
                            if (mBrand.StartsWith("AMPEG"))
                            {
                                if (mModel.Contains("B5R"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("BA108-V2"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("BA108 V2"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("BA108V2"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("BA110V2"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("BA110 V2"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("BA110-V2"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("BA112V2"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("BA112 V2"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("BA112-V2"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("BA115"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("BA 115"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("BA-115"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("BA115 V2"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("BA115-V2"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("BA 115 V2"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("BA-115 V2"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("BA210 V2"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("BA210-V2"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("BA 210 V2"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("BA-210 V2"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("LIQUIFIER"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("MICROCL"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("MICRO CL"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("MICRO-CL"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("MICROVR"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("MICRO VR"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("MICRO-VR"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("OPTOCOMP"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("OPTO COMP"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("OPTO-COMP"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("PF500"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("PF 500"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("PF-500"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("SCRD1"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("SCR D1"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("SCR-D1"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("SVPPRO"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("SVP PRO"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("SVP-PRO"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("SVT2PRO"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("SVT2 PRO"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("SVT2-PRO"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("SVT3PRO"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("SVT3 PRO"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("SVT3-PRO"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("SVT4PRO"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("SVT4 PRO"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("SVT4-PRO"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("SVTCL"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("SVT CL"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("SVT-CL"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                            }
                            if (mBrand.Contains("ASHLEY"))
                            {
                                if (mModel.Contains("MX508"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("MX 508"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("MX-508"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                            }
                            if (mBrand.Contains("AVALON"))
                            {
                                if (mModel.Contains("AD-2022"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("AD 2022"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("AD2022"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("VT-737"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("VT 737"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("VT737"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                            }
                            if (mBrand.Contains("CASIO"))
                            {
                                if (mModel == "AP-200")
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel == "AP 200")
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel == "AP200")
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel == "AP-620")
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel == "AP 620")
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel == "AP620")
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                            }
                            if (mBrand.Contains("BOSS"))
                            {
                                if (mModel == "SP-303")
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel == "SP 303")
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel == "SP303")
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                            }
                            if (mBrand.Contains("B52"))
                            {
                                if (mModel.Contains("AT-100"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("AT100"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("AT 100"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("AT-212"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("AT212"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("AT 212"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                            }
                            if (mBrand.Contains("BLACKSTAR"))
                            {
                                if (mModel.Contains("HT-5"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("HT 5"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("HT5"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    button6.Visible = true;
                                    PlayChurchBell();
                                }
                            }
                            if (mBrand.Contains("AKAI"))
                            {
                                if (mModel.Contains("MPC1000"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                }
                                if (mModel.Contains("MPC 1000"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                }
                                if (mModel.Contains("MPCLIVE!"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                }
                                if (mModel.Contains("MPC LIVE"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                }
                                if (mModel.Contains("MPC 2KXL"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                }
                                if (mModel.Contains("MPC2KXL"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                }
                                if (mModel.Contains("DPS12"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("DPS 12"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    PlayChurchBell();
                                }
                                if (mModel.Contains("DPS-12"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                    Version.Serial = ySerial;
                                    Version.Make = yBrand;
                                    Version.Model = yModel;
                                    PlayChurchBell();
                                }
                                 if (mModel.Contains("MPC200XL"))
                                {
                                    ServiceBulletin = true;
                                }
                                if (mModel.Contains("MPC 200XL"))
                                {
                                    ServiceBulletin = true;
                                }
                                if (mModel.Contains("MPC 3000"))
                                {
                                    ServiceBulletin = true;
                                }
                                if (mModel.Contains("MPC3000"))
                                {
                                    ServiceBulletin = true;
                                }
                                if (mModel.Contains("MPC 4000"))
                                {
                                    ServiceBulletin = true;
                                }
                                if (mModel.Contains("MPC4000"))
                                {
                                    ServiceBulletin = true;
                                }
                                if (mModel.Contains("MPC 5000"))
                                {
                                    ServiceBulletin = true;
                                }
                                if (mModel.Contains("MPC5000"))
                                {
                                    ServiceBulletin = true;
                                }
                            }
                            if (mBrand.Contains("ALESIS"))
                            {
                                if (mModel.Contains("A6"))
                                {
                                    ServiceBulletin = true;
                                }
                                if (mModel.Contains("A6 Andromeda"))
                                {
                                    ServiceBulletin = true;
                                }
                                if (mModel.Contains("DM10"))
                                {
                                    ServiceBulletin = true;
                                }
                                if (mModel.Contains("DM 10"))
                                {
                                    ServiceBulletin = true;
                                }
                            }
                            if (mBrand.Contains("ADAMS AUDIO"))
                            {
                                if (mModel.Contains("AX7"))
                                {
                                    ServiceBulletin = true;
                                }
                                if (mModel.Contains("SUB"))
                                {
                                    ServiceBulletin = true;
                                }
                            }
                            if (mBrand.StartsWith("QSC"))
                            {
                                if (mModel.Contains("K8.2"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                }
                                if (mModel.Contains("K10.2"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                }
                                if (mModel.Contains("K12.2"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                }
                            }
                            if (mBrand.StartsWith("YAMAHA"))
                            {
                                if (mModel.Contains("MOD"))
                                {
                                    ServiceBulletin = true;
                                }
                                if (mModel.Contains("MONTAGE"))
                                {
                                    ServiceBulletin = true;
                                }
                                if (mModel.Contains("MOTIF"))
                                {
                                    ServiceBulletin = true;
                                }
                                if (mModel.Contains("P71"))
                                {
                                    ServiceBulletin = true;
                                }
                                if (mModel.Contains("P-71"))
                                {
                                    ServiceBulletin = true;
                                }
                                if (mModel.Contains("P 71"))
                                {
                                    ServiceBulletin = true;
                                }
                                if (mModel.Contains("P 515"))
                                {
                                    ServiceBulletin = true;
                                }
                                if (mModel.Contains("P-515"))
                                {
                                    ServiceBulletin = true;
                                }
                                if (mModel.Contains("P515"))
                                {
                                    ServiceBulletin = true;
                                }
                                if (mModel.Contains("PF70"))
                                {
                                    ServiceBulletin = true;
                                }
                                if (mModel.Contains("PF 70"))
                                {
                                    ServiceBulletin = true;
                                }
                                if (mModel.Contains("PF-70"))
                                {
                                    ServiceBulletin = true;
                                }
                                if (mModel.Contains("PF80"))
                                {
                                    ServiceBulletin = true;
                                }
                                if (mModel.Contains("PF 80"))
                                {
                                    ServiceBulletin = true;
                                }
                                if (mModel.Contains("PF-80"))
                                {
                                    ServiceBulletin = true;
                                }
                                if (mModel.Contains("PSR600"))
                                {
                                    ServiceBulletin = true;
                                }
                                if (mModel.Contains("PSR 600"))
                                {
                                    ServiceBulletin = true;
                                }
                                if (mModel.Contains("PSR-600"))
                                {
                                    ServiceBulletin = true;
                                }
                                if (mModel.Contains("PSR2700"))
                                {
                                    ServiceBulletin = true;
                                }
                                if (mModel.Contains("PSR 2700"))
                                {
                                    ServiceBulletin = true;
                                }
                                if (mModel.Contains("PSR-2700"))
                                {
                                    ServiceBulletin = true;
                                }
                                if (mModel.StartsWith("TF"))
                                {
                                    ServiceBulletin = true;
                                }
                                if (mModel.StartsWith("THR"))
                                {
                                    ServiceBulletin = true;
                                }
                                if (mModel.StartsWith("TIO"))
                                {
                                    ServiceBulletin = true;
                                }
                                if (mModel.Contains("TYROS5"))
                                {
                                    ServiceBulletin = true;
                                }
                                if (mModel.Contains("TYROS 5"))
                                {
                                    ServiceBulletin = true;
                                }
                                if (mModel.Contains("TYROS-5"))
                                {
                                    ServiceBulletin = true;
                                }
                                if (mModel.Contains("YDP-142"))
                                {
                                    ServiceBulletin = true;
                                }
                                if (mModel.Contains("YDP 142"))
                                {
                                    ServiceBulletin = true;
                                }
                                if (mModel.Contains("YDP142"))
                                {
                                    ServiceBulletin = true;
                                }
                                if (mModel.Contains("YDP184"))
                                {
                                    ServiceBulletin = true;
                                }
                                if (mModel.Contains("YDP 184"))
                                {
                                    ServiceBulletin = true;
                                }
                                if (mModel.Contains("YDP-184"))
                                {
                                    ServiceBulletin = true;
                                    ySerial = mSerial;
                                    yModel = mModel;
                                    yBrand = mBrand;
                                }
                            }
                            if (ServiceBulletin == true)
                            {
                                button6.Visible = true;
                                PlayChurchBell();

                            }

                            Version.MMS = mBrand + " Model: " + mModel + " Serial: " + mSerial;
                            Version.Make = mBrand;
                            Version.Model = mModel;
                            Version.Serial = mSerial;
                            label25.Text = mDate_IN;
                            label28.Text = mWarranty + ", " + mProblem;
                            label29.Text = mFthr_exp1;
                            label55.Text = mFthr_exp2; 
                            label30.Text = "Email: ";
                            textBox1.Text = mEmail;
                            //label43.Text = mEmail;
                            //label48.Text = mEmail;
                            if (mEmail == ".")
                            {
                                //label43.Text = listBP[loopCount];
                                textBox1.Text = listBP[loopCount];
                            }
                            label33.Text = mTS1;
                            label34.Text = mTS2;
                            label35.Text = mTS3;
                            label36.Text = mts4;
                            if (mWarranty.Contains("WAR"))
                            {
                                label38.BackColor = Color.White;
                            }
                            if (mWarranty.Contains("NON"))
                            {
                                label38.ForeColor = Color.Green;
                            }
                            else
                            {
                                label38.ForeColor = Color.Red;
                            }
                            label38.Text = mWarranty;
                            var mEstimate2 = "";
                            if (mEstimate == "Y")
                            {
                                mEstimate2 = "Yes";
                            }
                            else
                            {
                                mEstimate2 = "No";
                            }
                            label50.Text = "Estimate: ";
                            if (mEstimate.Contains("Y") || mEstimate.Contains("A"))
                            {
                                label52.ForeColor = Color.Red;
                                label52.BackColor = Color.White;
                            }
                            else
                            {
                                label52.ForeColor = Color.White;
                                label52.BackColor = Color.Black;
                            }
                            if (mEstimate == "Y")
                            {
                                label52.Text = "Yes";
                            }
                            if (mEstimate == "A")
                            {
                                label52.Text = "Yes";
                            }
                            if (mEstimate == "_")
                            {
                                label52.Text = "Dec";
                            }
                            if (mEstimate == "N")
                            {
                                label52.Text = "No";
                            }

                            var mRush2 = "";
                            if (mRush == "Y")
                            {
                                mRush2 = "Yes";
                            }
                            else
                            {
                                mRush2 = "No";
                            }
                            label51.Text = "Rush Claim: ";
                            if (mRush2 == "Yes")
                            {
                                label53.ForeColor = Color.Red;
                                label53.BackColor = Color.White;
                            }
                            else
                            {
                                label53.ForeColor = Color.White;
                                label53.BackColor = Color.Black;
                            }
                            label53.Text = mRush2;
                            if (mWarranty.Contains("RECALL") || mIsWarr == "WARRANTY")
                            {
                                label49.Text = " RECALL ";
                            }
                            if (mIsWarr.Contains("RECALL"))  
                            {
                                label49.Text += "PARTS ONLY No Labor";
                            }
                            label39.Text = mBench;

                            //label48.Text = mTheNewClaimNum.ToString();
                            if (mBench.Contains("SERVICE RENDERED"))
                            {
                                label47.ForeColor = Color.Red;
                                label47.Text = "CLOSED";
                                Text += "  CLOSED CLAIM !";
                                //label49.Text = "CLOSED CLAIM";
                            }
                            else
                            {
                                label47.ForeColor = Color.Green;
                                label47.Text = "Active Claim";
                            }
                            if (listBN[loopCount] == "ESTIMATE")
                            {
                                label45.Text = "Estimate:"; // Yes
                                label56.Text = " Yes ";
                                label56.BackColor = Color.Red;
                                label56.ForeColor = Color.White;
                            }
                            else
                            {
                                label45.Text = "Estimate:";  // No
                                label56.Text = " No ";
                                label56.BackColor = Color.Black;
                                label56.ForeColor = Color.White;
                            }
                            //label45.Text = "Estimate: " + listBN[loopCount];
                            label40.Text = "Technician: " + mTheTech;
                            if (listBF[loopCount] == "FC")
                            {
                                listBF[loopCount] = "Front Counter";
                            }
                            label41.Text = listBF[loopCount];
                            if (label36.Text.Contains("PARTS") || label35.Text.Contains("PARTS"))
                            {
                                label46.BackColor = Color.White;
                                label46.ForeColor = Color.Green;
                                label46.Text = " Parts Ordered ";
                                label46.BackColor = Color.White;
                            }
                            else
                            {
                                label46.Text = "No Parts Ordered";
                            }
                            //label43.Text = mEmail;
                            textBox1.Text = mEmail;
                            if (mEmail == ".")
                            {
                                //label43.Text = listBP[loopCount];
                                textBox1.Text = listBP[loopCount];
                            }
                            var mxRush = "";
                            if (mRush == "Y")
                            {
                                mxRush = "Yes";
                            }
                            else
                            {
                                mxRush = "No";
                            }
                            Found = true;
                            if (listBE[loopCount].ToUpper().Contains("FRONT"))
                            {
                                listBE[loopCount] = "FC. ";
                            }
                            if (listAI[loopCount] == "NO DEALER")
                            {
                                listAI[loopCount] = "      ";
                            }
                            if (listAN[loopCount] == "000-00-0000")
                            {
                                listAN[loopCount] = "     ";
                            }
                            if (listAJ[loopCount] == "N/A")
                            {
                                listAJ[loopCount] = "     ";
                            }
                            if (listAK[loopCount] == "N/A")
                            {
                                listAK[loopCount] = "     ";
                            }
                            if (listAM[loopCount] == "0")
                            {
                                listAM[loopCount] = "     ";
                            }
                            if (CLOSED == "Y")
                            {
                                IsClosed = "This is a Closed Claim";
                                if (PICKUP == "Y")
                                {
                                    IsClosed += ", PICKED UP.";
                                }
                                else
                                {
                                    IsClosed += ", NOT PICKED UP YET.";
                                }
                            }
                            else
                            {
                                IsClosed = "This is an Open Claim";
                            }

                            richTextBox1.Text += "╔══════════════════════════════════╗\t    Date In: " + mDate_IN + "    CLAIM # " + mClaim_NO + "\n";
                            richTextBox1.Text += "║ Wizard Electronics, Inc.         ║\t    Product: " + listBJ[loopCount] + "\n";
                            richTextBox1.Text += "║ 554 Deering Road Northwest       ║\t    Brand:   " + mBrand + "\n";
                            richTextBox1.Text += "║ Atlanta, GA 30309-2267           ║\t    Model:   " + mModel + "\n";
                            richTextBox1.Text += "║ (404)325-4891 Fax (404)325-4175  ║\t    Serial#: " + mSerial + "\n";
                            richTextBox1.Text += "╚══════════════════════════════════╝\t    Shelf Location: " + listBE[loopCount] + " Rush Claim: " + mxRush + "\n";
                            richTextBox1.Text += "\n";
                            richTextBox1.Text += "Customer Name:    " + mFname + ", " + mLname + "\t\t\t" + listAH[loopCount] + "\n";
                            richTextBox1.Text += "Customer Address: " + mAddr + "\n";
                            richTextBox1.Text += "City, State, Zip: " + mCity + ", " + mState + " " + mZip + " " + "Home : " + mHphone + " Work: " + mWPhone + "\n";
                            richTextBox1.Text += "Email:   " + mEmail + "\n";
                            richTextBox1.Text += "══════════════════════════════════════════════════════════════════════════════════════\n";
                            richTextBox1.Text += "Client/Dealer name: " + listAI[loopCount] + "\t\tPhone: " + listAN[loopCount] + "\n";
                            richTextBox1.Text += "Address:            " + listAJ[loopCount] + "\t\t Invoice/Claim # " + listBF[loopCount] + "\n";
                            richTextBox1.Text += "City, State, Zip:   " + listAK[loopCount] + " " + listAL[loopCount] + "  " + listAM[loopCount] + "\n";
                            richTextBox1.Text += "══════════════════════════════════════════════════════════════════════════════════════\n";
                            Version.Warranty = mWarranty;
                            richTextBox1.Text += "PROBLEM / CUSTOMER COMPLAINT:                     Unit Status is: " + listBL[loopCount] + "\n";
                            richTextBox1.Text += "Problem: " + mProblem + "\n"; ;
                            richTextBox1.Text += "Problem: " + mFthr_exp1 + "\n";
                            richTextBox1.Text += "Problem: " + mFthr_exp2 + "\n";
                            richTextBox1.Text += "══════════════════════════════════════════════════════════════════════════════════════\n";

                            var d = Convert.ToDouble(listQ[loopCount]);
                            var f = Convert.ToDouble(listS[loopCount]);
                            var total = d + f;
                            var tax = total * .095;
                            var newtotal = total + tax;
                            var k = 0.00m;
                            var lkl = listU[loopCount];
                            if (lkl == "65")
                            {
                                var lkl1 = Convert.ToDecimal(lkl);
                            }
                            kkk = listAA[loopCount];
                            if (kkk == "80")
                            {
                                kkkShip = 15.00m;
                                bBench = 65.00m;
                                sShopFee = 11.00m;
                                ddd = Convert.ToDecimal(d);
                                ddd -= 15.00m;
                                ddd -= sShopFee;
                                if (mRush == "Y")
                                {
                                    mRushFee = 50.00m;
                                }
                                else
                                {
                                    mRushFee = 0.00m;
                                }
                            }

                            richTextBox1.Text += "Technical Services Rendered:\n";
                            richTextBox1.Text += listAU[loopCount] + "\n";
                            richTextBox1.Text += listAV[loopCount] + "\n";
                            richTextBox1.Text += listAW[loopCount] + "\n";
                            richTextBox1.Text += listAX[loopCount] + "\n";
                            richTextBox1.Text += "══════════════════════════════════════════════════════════════════════════════════════\n";
                            richTextBox1.Text += "YOUR TECHNICIAN: " + mTheTech + "       TECHNICIAN #: " + mTechNum + "        Date Completed: " + COMPLETED + "\n";
                            richTextBox1.Text += "════════════════════════════════════════════════════╦═════════════════════════════════\n";
                            richTextBox1.Text += "Materials Used:                                     ║\n";
                            richTextBox1.Text += "                                                    ║    Totals:\n";
                            richTextBox1.Text += "QTY    Part Number          Description             ║      " + "\n";
                            
                            richTextBox1.Text += " " + ptu1 + "     " + ppn1 + "                 " + ppd1 + "               ║    Services       $ " + ddd.ToString("0.##") + "\n";
                            richTextBox1.Text += " " + ptu2 + "     " + ppn2 + "                 " + ppd2 + "             ║    Expendables    $  " + sShopFee.ToString("0.00") + "\n";
                            richTextBox1.Text += " " + ptu3 + "     " + ppn3 + "    " + ppd3 + " ║    Parts          $  " + f.ToString("0.##") + "\n";
                            richTextBox1.Text += " " + ptu4 + "     " + ppn4 + "    " + ppd4 + "                                          ║    Parts Shipping $  " + kkkShip.ToString("0.00") + "\n";
                            richTextBox1.Text += " " + ptu5 + "     " + ppn5 + "    " + ppd5 + "                                          ║    Tax            $  " + tax.ToString("0.##") + "\n";

                            if (mRush == "Y")
                            {
                                richTextBox1.Text += " " + ptu6 + "     " + ppn6 + "    " + ppd6 + "                                          ║    Rush Fee         $  " + mRushFee.ToString("0.00") + "\n";
                            }
                            else
                            {
                                richTextBox1.Text += " " + ptu6 + "     " + ppn6 + "    " + ppd6 + "                                          ║    \n";
                            }
                            richTextBox1.Text += " " + ptu7 + "     " + ppn7 + "    " + ppd7 + "                                          ║    \n";
                            richTextBox1.Text += " " + ptu8 + "     " + ppn8 + "    " + ppd8 + "                                          ║    ═══════════════════════════════════\n";
                            richTextBox1.Text += " " + ptu9 + "     " + ppn9 + "    " + ppd9 + "                                          ║    Grand Total    $  " + newtotal.ToString("0.##") + "\n";
                            richTextBox1.Text += " " + ptu10 + "     " + ppn10 + "    " + ppd10 + "                                          ║ \n";
                            richTextBox1.Text += "════════════════════════════════════════════════════╩══════════════════════════════════\n";
                            richTextBox1.Text += "For 45 days after the date listed below, We will cover parts and labor original\n";
                            richTextBox1.Text += "installed or performed that is listed, which proves to be defective, provided\n";
                            richTextBox1.Text += "unit was used within guidelines of Manufacturer or standard industry practices. \n";
                            richTextBox1.Text += "We are not responsible for any damage, incidental, or consequential, due to abuse, \n";
                            richTextBox1.Text += "misuse or act of god, and we make no guarantee for components not listed on invoice. \n";
                            richTextBox1.Text += "Their replacement, will be chargeable work. Signature acknowledges receipt of the \n";
                            richTextBox1.Text += "equipment in working order, as listed above.\n\n";
                            richTextBox1.Text += "                     Thank you for choosing Wizard Electronics, Inc.\n\n\n";
                            richTextBox1.Text += "Signed:______________________________________   \tDate: " + DateTime.Now.ToShortDateString() + "\n\n";
                            richTextBox1.Text += IsClosed + "\n";
                            loop++;
                        }
                        //loopCount++;
                    }

                    //reader.Close(); // Close the open file
                    if (mTheNewClaimNum == claim_no)
                    {
                        Found = true;
                        label6.Text = mFname + " " + mLname;
                        label7.Text = mAddr;
                        label8.Text = mCity + ", " + mState + " " + mZip;
                        label9.Text = yeardigit + claim_no;
                        label10.Text = listBK[loopCount];
                        label11.Text = mHphone;
                        label14.Text = mWPhone;
                        label22.Text = mBrand;
                        if (mBrand.StartsWith("JBL"))
                        {
                            button5.Visible = true;
                        }
                        if (mBrand.StartsWith("GALL"))
                        {
                            button5.Visible = true;
                        }
                        if (mBrand.StartsWith("CROWN"))
                        {
                            button5.Visible = true;
                        }
                        if (mBrand.StartsWith("DBX"))
                        {
                            button5.Visible = true;
                        }
                        if (mBrand.StartsWith("LEXICON"))
                        {
                            button5.Visible = true;
                        }
                        if (mBrand.StartsWith("SOUNDCRAFT"))
                        {
                            button5.Visible = true;
                        }
                        if (mBrand.StartsWith("MARTIN"))
                        {
                            button5.Visible = true;
                        }
                        if (mBrand.StartsWith("AKG"))
                        {
                            button5.Visible = true;
                        }
                        if (mBrand.StartsWith("DIGITECH"))
                        {
                            button5.Visible = true;
                        }
                        if (mBrand.StartsWith("AMX"))
                        {
                            button5.Visible = true;
                        }
                        if (mBrand.StartsWith("BSS"))
                        {
                            button5.Visible = true;
                        }
                        if (mBrand.StartsWith("YAMAHA"))
                        {
                            button5.Visible = true;
                        }
                        if (mBrand.StartsWith("MACKIE"))
                        {
                            button5.Visible = true;
                        }
                        label23.Text = mModel;
                        label24.Text = mSerial;
                        Version.MMS = mBrand + " Model: " + mModel + " Serial: " + mSerial;
                        Version.Make = mBrand;
                        Version.Model = mModel;
                        Version.Serial = mSerial;
                        label25.Text = mDate_IN;
                        label28.Text = listBN[loopCount] + ", " + mProblem;
                        label29.Text = mFthr_exp1;
                        label30.Text = "Email: ";
                        //label43.Text = "&" + mFthr_exp2;
                        textBox1.Text = "&" + mFthr_exp2;
                        if (mEmail == ".")
                        {
                            //label43.Text = listBP[loopCount];
                            //label43.Text = listBP[loopCount];
                        }
                        label33.Text = mTS1;
                        label34.Text = mTS2;
                        label35.Text = mTS3;
                        label36.Text = mts4;
                        label38.Text = mWarranty;
                        if (mWarranty.Contains("RECALL") || mIsWarr == "WARRANTY")
                        {
                            label49.Text = " RECALL ";
                        }
                        if (mIsWarr.Contains("RECALL"))
                        {
                            label49.Text += "PARTS ONLY No Labor";
                        }
                        label39.Text = mBench;
                        //label48.Text = mTheNewClaimNum.ToString();
                        if (mBench.Contains("SERVICE RENDERED"))
                        {
                            label47.ForeColor = Color.Red;
                            label47.Text = "CLOSED";
                            Text += "  CLOSED CLAIM !";
                            label49.Text = "CLOSED CLAIM";
                        }
                        else
                        {
                            label47.ForeColor = Color.Green;
                            label47.Text = "Open Claim";
                        }
                        //label45.Text = "Estimate: " + listBN[loopCount];
                        label40.Text = "Technician: " + mTheTech;
                        if (listBE[loopCount] == "FC")
                        {
                            listBE[loopCount] = "Front Counter";
                        }
                        label41.Text = listBE[loopCount];
                        if (label36.Text.Contains("PARTS"))
                        {
                            label46.BackColor = Color.White;
                            label46.ForeColor = Color.Green;
                            label46.Text = " Parts Ordered ";
                            label46.BackColor = Color.White;
                        }
                        else
                        {
                            label46.Text = "No Parts Ordered";
                        }
                        richTextBox1.Text += "************************************\tDate In: " + mDate_IN + "    CLAIM # " + yeardigit + mClaim_NO + "\n";
                        richTextBox1.Text += "* Wizard Electronics, Inc.         *\tProduct: " + listBJ[loopCount] + "\n";
                        richTextBox1.Text += "* 554 Deering Road Northwest       *\tBrand:   " + mBrand + "\n";
                        richTextBox1.Text += "* Atlanta, GA 30309                *\tModel:   " + mModel + "\n";
                        richTextBox1.Text += "* (404)325-4891 Fax (404)325-4175  *\tSerial#: " + mSerial + "\n";
                        if (listBE[loopCount].ToUpper().Contains("FRONT"))
                        {
                            listBE[loopCount] = "FC. ";
                        }
                        richTextBox1.Text += "************************************\tShelf Location: " + listBE[loopCount] + "\n";
                        richTextBox1.Text += "\n";
                        richTextBox1.Text += "Customer Name:    " + mFname + ", " + mLname + "\t\t" + listAH[loopCount] + "\n";
                        richTextBox1.Text += "Customer Address: " + mAddr + "\n";
                        richTextBox1.Text += "City, State, Zip: " + mCity + ", " + mState + " " + mZip + " " + "Home : " + mHphone + " Work: " + mWPhone + "\n";
                        richTextBox1.Text += "════════════════════════════════════════════════════════════════════════════════\n";
                        richTextBox1.Text += "Client/Dealer name: " + listAI[loopCount] + "\tPhone: " + listAN[loopCount] + "\n";
                        richTextBox1.Text += "Address:            " + listAJ[loopCount] + "\t\tInvoice/Claim # " + listBF[loopCount] + "\n";
                        richTextBox1.Text += "City, State, Zip:   " + listAK[loopCount] + " " + listAL[loopCount] + "  " + listAM[loopCount] + "\n";
                        richTextBox1.Text += "════════════════════════════════════════════════════════════════════════════════\n";
                        richTextBox1.Text += "Unit Status is: " + listBL[loopCount] + "\n";
                        Version.Warranty = mWarranty;
                        if (listQ[loopCount].Length <= 6)
                        {
                            richTextBox1.Text += "\tTechnical Services: $  " + listQ[loopCount] + "\n";
                        }
                        else
                        {
                            richTextBox1.Text += "\tTechnical Services: $ " + listQ[loopCount] + "\n";
                        }
                        richTextBox1.Text += "\tTechnical Services: $ " + listQ[loopCount] + "\n";
                        richTextBox1.Text += "\t             Parts: $ " + listS[loopCount] + "\n";
                        richTextBox1.Text += "\t------------------------------------" + "\n";
                        var d = Convert.ToDouble(listQ[loopCount]);
                        var f = Convert.ToDouble(listS[loopCount]);
                        var total = d + f;
                        var tax = total * .095;
                        var newtotal = total + tax;
                        var k = 0.00m;
                        if (newtotal > 0)
                        {
                            IsClosed = "This is a Closed Claim";
                        }
                        else
                        {
                            IsClosed = "This is an Open Claim";
                        }
                        richTextBox1.Text += "\t             Total: $ " + total.ToString() + "\n\n";
                        richTextBox1.Text += "No warranty repairs W/O Sales Receipt/RA# at drop off. If NOT warranty,";
                        richTextBox1.Text += "EST Diagnostic Fee will apply if repair declined. Items left over 10 days,";
                        richTextBox1.Text += "add $ 1.00/Day storage fee.\n";
                        richTextBox1.Text += "════════════════════════════════════════════════════════════════════════════════\n";
                        richTextBox1.Text += "Problem: " + mProblem + "\n"; ;
                        richTextBox1.Text += "Problem: " + mFthr_exp1 + "\n";
                        richTextBox1.Text += "Problem: " + mFthr_exp2 + "\n";
                        richTextBox1.Text += "Email:   " + mEmail + "\n";
                        richTextBox1.Text += "════════════════════════════════════════════════════════════════════════════════\n";
                        richTextBox1.Text += "Technical Services Rendered:\n";
                        richTextBox1.Text += listAU[loopCount] + "\n";
                        richTextBox1.Text += listAV[loopCount] + "\n";
                        richTextBox1.Text += listAW[loopCount] + "\n";
                        richTextBox1.Text += listAX[loopCount] + "\n";
                        richTextBox1.Text += "════════════════════════════════════════════════════════════════════════════════\n";
                        richTextBox1.Text += "Materials Used:                               *\n";
                        richTextBox1.Text += "                                              *    Totals:\n";
                        richTextBox1.Text += "QTY    Part Number     Description            *    \n";
                        richTextBox1.Text += "_____________________________________________ *    Services      $ " + d.ToString("0.##") + "\n";
                        richTextBox1.Text += "_____________________________________________ *    Parts         $  " + f.ToString("0.##") + "\n";
                        richTextBox1.Text += "_____________________________________________ *    Other         $   " + k.ToString("0.##") + "\n";
                        richTextBox1.Text += "_____________________________________________ *    Tax           $  " + tax.ToString("0.##") + "\n";
                        richTextBox1.Text += "_____________________________________________ *    Down Payment  $  65.00\n";
                        richTextBox1.Text += "_____________________________________________ *    ========================\n";
                        richTextBox1.Text += "_____________________________________________ *    Grand Total   $ " + newtotal.ToString("0.##") + "\n";
                        richTextBox1.Text += "════════════════════════════════════════════════════════════════════════════════\n";
                        richTextBox1.Text += "Items left over 45 days will be sold. I authorize service as specified.\n";
                        richTextBox1.Text += "Rush Charge is $ 50.00 in addition to repair charges. \n";
                        richTextBox1.Text += "Payment must be Cash/Bank Card\n\n";
                        richTextBox1.Text += "Signed:______________________________________   \tDate: " + DateTime.Now.ToShortDateString() + "\n\n";
                        richTextBox1.Text += IsClosed + "\n";
                        loop++;
                    }
                loopCount++;
                }
                reader.Close(); // Close the open file
            }
            catch (Exception ex)
            {
                Mex = ex.ToString();
                if (Mex.Contains("AccessViolationException"))
                {
                    MessageBox.Show("AccessViolationException");
                }
                if (Mex.Contains("AggregateException"))
                {
                    MessageBox.Show("AggregateException");
                }
                if (Mex.Contains("FileFormatException"))
                {
                    MessageBox.Show("FileFormatException");
                }
                if (Mex.Contains("IndexOutOfRangeException"))
                {
                    MessageBox.Show("IndexOutOfRangeException");
                }
                if (Mex.Contains("StackOverflowException"))
                {
                    MessageBox.Show("StackOverflowException");
                }
                if (Mex.Contains("Input string was not in a correct format."))
                {

                }
                else
                {
                    MessageBox.Show("Error 5295: Sorry an error has occured: " + ex.Message);
                }
            }
        }
            
            /*

AppDomainUnloadedException
ApplicationException
ArgumentException
ArgumentNullException
ArgumentOutOfRangeException
ArithmeticException
ArrayTypeMismatchException
BadImageFormatException
CannotUnloadAppDomainException
ContextMarshalException
DataMisalignedException
DivideByZeroException
DllNotFoundException
DuplicateWaitObjectException
EntryPointNotFoundException
ExecutionEngineException
FieldAccessException
FormatException
IndexOutOfRangeException
InsufficientMemoryException
InvalidCastException
InvalidOperationException
InvalidProgramException
InvalidTimeZoneException
MemberAccessException
MethodAccessException
MissingFieldException
MissingMemberException
MissingMethodException
MulticastNotSupportedException
NotCancelableException
NotFiniteNumberException
NotImplementedException
NotSupportedException
NullReferenceException
ObjectDisposedException
OperationCanceledException
OutOfMemoryException
OverflowException
PlatformNotSupportedException
RankException
StackOverflowException
SystemException
TimeoutException
TimeZoneNotFoundException
TypeAccessException
TypeInitializationException
TypeLoadException
TypeUnloadedException
UnauthorizedAccessException
UriFormatException
ConstraintException
DataException
DBConcurrencyException
DeleteRowInaccessibleException
DuplicateNameException
EvaluateException
InRowChangingEventException
InvalidConstraintException
InvalidExpressionException
MissingPrimaryKeyException
NoNullAllowedException
OperationAbortedException
ReadOnlyException
RowNotInTableException
StrongTypingException
SyntaxErrorException
TypedDataSetGeneratorException
VersionNotFoundException
DirectoryNotFoundException
DriveNotFoundException
EndOfStreamException

FileLoadException
FileNotFoundException
InternalBufferOverflowException
InvalidDataException
IOException
PathTooLongException
PipeException
            */

        
    }
}
