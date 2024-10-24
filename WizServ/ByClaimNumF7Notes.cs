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

namespace WizServ
{
    public partial class ByClaimNumF7Notes : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        private readonly string file = @"I:\\Datafile\\Control\\Database.CSV";
        private readonly string file2, file3;
        private readonly string Lab = "F7 Notes for WizServ";
        public string claim_no = Version.Claim;
        public string from = Version.From;
        private Font printFont;
        private StreamReader streamToPrint;
        static string filePath;

        public string SAVEDDATA { get; private set; }

        public ByClaimNumF7Notes()
        {
            InitializeComponent();
            Icon = image100;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
            file =  @"I:\Datafile\Control\F7Notes\" + claim_no.ToString() + "ClaimNotes.rtf";
            file2 = @"I:\Datafile\Control\F7Notes\" + claim_no.ToString() + "ClaimNotesBU.rtf";
            file3 = @"I:\Datafile\Control\F7Notes\" + claim_no.ToString() + "ClaimNotesMASTER.rtf";
            label1.Text = Lab + " for Claim #: " + claim_no;
            label2.Text = file.ToString();
            LoadRTFfile();
            richTextBox1.Select();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Version.From == "Retrieve")
            {
                Hide();
                RetrieveMenu f2 = new RetrieveMenu();
                f2.Show();
            }
            else
            {
                Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.SaveFile(file, RichTextBoxStreamType.RichText);
            File.Copy(file, file2, true);
            try
            {
                if (Version.From == "Retrieve")
                {
                    Hide();
                    RetrieveMenu f2 = new RetrieveMenu();
                    f2.Show();
                }
                else
                {
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex);
            }
        }

        // The PrintPage event is raised for each page to be printed.
        private void pd_PrintPage(object sender, PrintPageEventArgs ev)
        {
            float linesPerPage = 0;
            float yPos = 0;
            int count = 0;
            float leftMargin = ev.MarginBounds.Left;
            float topMargin = ev.MarginBounds.Top;
            String line = null;

            // Calculate the number of lines per page.
            linesPerPage = ev.MarginBounds.Height /
               printFont.GetHeight(ev.Graphics);

            // Iterate over the file, printing each line.
            while (count < linesPerPage &&
               ((line = streamToPrint.ReadLine()) != null))
            {
                yPos = topMargin + (count * printFont.GetHeight(ev.Graphics));
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

        public void Printing()
        {
            try
            {
                streamToPrint = new StreamReader(filePath);
                try
                {
                    printFont = new Font("Arial", 10);
                    PrintDocument pd = new PrintDocument();
                    pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
                    // Print the document.
                    pd.Print();
                }
                finally
                {
                    streamToPrint.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {

            SAVEDDATA = richTextBox1.Text;
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
            else
            {
                printDocument1.Print();
            }
            richTextBox1.Text = "";
            richTextBox1.Text = SAVEDDATA;
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
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
                if (ex.ToString().Contains("You can't copy 'nothing', select some text first."))
                {
                    // Ignore nothing selected
                }
                else
                {
                    MessageBox.Show("Sorry an exception has occured. Line 169\n" + ex);
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

        private void button11_Click(object sender, EventArgs e)
        {
            richTextBox1.Select(richTextBox1.TextLength, 0);
            // Scroll to the control cursor
            richTextBox1.ScrollToCaret();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            richTextBox1.Select(0, 0);
            // Scroll to the control cursor
            richTextBox1.ScrollToCaret();
            //richTextBox1.AppendText(" X ");
        }

        private void changeFontsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog1 = new FontDialog
            {
                ShowColor = true,
                Font = richTextBox1.Font,
                Color = richTextBox1.ForeColor
            };

            if (fontDialog1.ShowDialog() != DialogResult.Cancel)
            {
                richTextBox1.Font = fontDialog1.Font;
                richTextBox1.ForeColor = fontDialog1.Color;
            }
            richTextBox1.ShortcutsEnabled = true;
            richTextBox1.AcceptsTab = true;
            richTextBox1.DeselectAll();
        }

        private void LoadRTFfile()
        {
            if (File.Exists(file))
            {
                richTextBox1.LoadFile(file);    // Load contents of RTF file
            }
            richTextBox1.SaveFile(file, RichTextBoxStreamType.RichText);
            richTextBox1.SaveFile(file3, RichTextBoxStreamType.RichText);
            var textLength = richTextBox1.TextLength;
            if (textLength >= 0)
            {
                if (File.Exists(file2))     // Upon RTF load, save a backup copy BEFORE any changes.
                {
                    //File.Delete(file2);
                }
                //File.Move(file, file2); // Rename the oldFileName into newFileName
                //File.Copy(file2, file);
            }
        }
    }
}
