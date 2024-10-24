using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WizServ
{
    public partial class MainUtilitiesMenu : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        private readonly BackgroundWorker worker;
        public int butnum;

        public MainUtilitiesMenu()
        {
            InitializeComponent();
            label3.Visible = false;
            textBox1.Visible = false;
            Icon = image100;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += StartCounting;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            label2.Visible = false;
        }

        public void PlaySimpleSound()
        {
            //SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.ChurchBell);
            SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.Magic);
            simpleSound.Play();
        }

        private void MainUtilitiesMenu_Load(object sender, EventArgs e)
        {
            PlaySimpleSound();
            //Playaudio(); // calling the function
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            Hide();
            NegInvRpt f0 = new NegInvRpt();
            f0.Show();
        }

        private void Playaudio() // defining the function
        {
            //SoundPlayer audio = new SoundPlayer(Properties.Resources.ChurchBell);
            //audio.Play();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            Hide();
            Tech_AssignmentMenu f3 = new Tech_AssignmentMenu();
            f3.Show();
        }

        private void StartCounting(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bgWorker = (BackgroundWorker)sender;
            for (var i = 0; i <= 11; i++)
            {
                bgWorker.ReportProgress(i);
                switch (i)
                {
                    case 0:
                        PlaySimpleSound();
                        break;
                    case 1:
                        PlaySimpleSound();
                        break;
                }
                PlaySimpleSound();
            }
            
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            PlaySimpleSound();
        }

        void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            PlaySimpleSound();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void DoHeavyStuf()
        {
            //Heavy work (simulated by thread.sleep)
            string Path = @"C:\\Windows\\System32\\";
            string sourcePath = @"I:\\_CSV_BACKUP_BU\\";
            var countDirectories = Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories).Count();
            Process proc = new Process();
            proc.StartInfo.UseShellExecute = true;
            proc.StartInfo.FileName = "xcopy.exe";
            proc.StartInfo.Arguments = @"I:\Datafile\Control I:\_CSV_BACKUP_BU\Backup /E /I /F /Y /H";
            proc.Start();
            string Answer = "Files Backed up to B/U Directory\n" + countDirectories.ToString() + " Directories copied.";
            int fileCount = Directory.EnumerateFiles(sourcePath, "*.*", SearchOption.AllDirectories).Count();
            int total = fileCount;
            label2.Visible = true;
            label2.Text = Answer + "Files Copied: " + fileCount.ToString();
            MessageBox.Show("Backup Completed!");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var currentSyncContext = SynchronizationContext.Current;
            Task.Factory.StartNew(() =>
            {
                string Path = @"I:\Datafile\Control\";
                string sourcePath = @"I:\_CSV_BACKUP\";
                var countDirectories = Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories).Count();
                Process proc = new Process();
                proc.StartInfo.UseShellExecute = true;
                proc.StartInfo.FileName = "xcopy.exe";
                proc.StartInfo.Arguments = @"I:\Datafile\ I:\\_CSV_BACKUP\\Backup /E /I /F /Y /H";
                proc.Start();
                string Answer = "Files Backed up to B/U Directory\n" + countDirectories.ToString() + " Directories copied.";
                int fileCount = Directory.EnumerateFiles(sourcePath, "*.*", SearchOption.AllDirectories).Count();
                int total = fileCount;

                currentSyncContext.Send(new SendOrPostCallback((arg) =>
                {
                    label2.Visible = true;
                    label2.Text = Answer + "\nFiles Copied: " + fileCount.ToString();
                }), "your current status");
                //do some work
            });
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DoHeavyStuf();
        }

        private void button5_Click(object sender, EventArgs e)  // Edit Email Suffix file
        {
            Hide();
            Priority f0 = new Priority();
            f0.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            EnterPartsIntoClaim f0 = new EnterPartsIntoClaim();
            f0.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            label3.Visible = true;
            textBox1.Visible = true;
            textBox1.Select();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Hide();
            ExportPartsDB f0 = new ExportPartsDB();
            f0.Show();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Version.Claim = textBox1.Text;
                Version.From = "MAINUTILITIESMENU";
                Version.SELECTEDTEXT = "MAINUTILITIESMENU";
                if (butnum == 11)
                {
                    Hide();
                    EditPO f0 = new EditPO();
                    f0.Show();
                }
                if (butnum == 17)
                {
                    Version.Claim = textBox1.Text;
                    Hide();
                    Tech_AssignmentMenu f0 = new Tech_AssignmentMenu();
                    f0.Show();
                }
                else
                {
                    Hide();
                    ByClaimNum f0 = new ByClaimNum();
                    f0.Show();
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Hide();
            CreatePO f0 = new CreatePO();
            f0.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            label3.Visible = true;
            textBox1.Visible = true;
            butnum = 11;
            textBox1.Select();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Hide();
            MainMenu f0 = new MainMenu();
            f0.Show();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Hide();
            CreateEstimate f0 = new CreateEstimate();
            f0.Show();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Hide();
            ViewEstimatesTwo f0 = new ViewEstimatesTwo();
            f0.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {
            Hide();
            EstimateApprovalRPT f0 = new EstimateApprovalRPT();
            f0.Show();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            butnum = 17;
            label3.Visible = true;
            textBox1.Visible = true;
            textBox1.Select();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            Hide();
            FixDatabase f2 = new FixDatabase();
            f2.Show();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            Hide();
            ChartsMenu f2 = new ChartsMenu();
            f2.Show();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            Application.ExitThread();
            Close();
        }
    }
}
