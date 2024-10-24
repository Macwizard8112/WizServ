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
    public partial class ByClaimNumF5Notes : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        private readonly string file = @"I:\\Datafile\\Control\\Database.CSV";
        private readonly string file2, file3;
        private readonly string Lab = "F5 Notes for WizServ";
        public string claim_no = Version.Claim;
        public string from = Version.From;

        public ByClaimNumF5Notes()
        {
            InitializeComponent();
            Icon = image100;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
            file = @"I:\Datafile\Control\Notes\" + claim_no.ToString() + "ClaimNotes.rtf";
            file2 = @"I:\Datafile\Control\Notes\" + claim_no.ToString() + "ClaimNotesBU.rtf";
            file3 = @"I:\Datafile\Control\Notes\" + claim_no.ToString() + "ClaimNotesMASTER.rtf";
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

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void printDocument1_BeginPrint(object sender, PrintEventArgs e)
        {
            SAVEDDATA = richTextBox1.Text;
            richTextBox1.Text = "              F5 Notes - " + DateTime.Now.ToShortDateString() + "   " + claim_no + "\n\n" + richTextBox1.Text;

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
        private int linesPrinted;
        private string[] lines;
        private string SAVEDDATA;

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
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

        private void button3_Click(object sender, EventArgs e)
        {
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
    }
}
