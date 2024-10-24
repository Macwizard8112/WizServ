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
using System.Windows.Forms;
using System.Media;
using Microsoft.Win32;

namespace WizServ
{
    public partial class NegInvRpt : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        private readonly string Locate = @"I:\\Datafile\\Control\\Pri.CSV";
        private int loopCount, loop;
        // Get Data Variables
        public string ma, mb, mc, md, me, mf, mg, mh, mi, mj, mk, ml, mm, mn, mo, mp, mq, mr, ms, mt, mu, mv, mw, mx, my, mz;
        //private System.ComponentModel.Container components;
        private System.Windows.Forms.Button printButton;
        private Font printFont;
        private StreamReader streamToPrint;
        public string maa, mab, mac, mad, mae, maf, mag, mah, mai, maj, mak, mal, mam, man, mao, map, maq, mar, mas, mat, mau, mav, maw, max, may, maz;
        public string mba, mbb, mbc, mbd, mbe, mbf, mbg, mbh, mbi, mbj, mbk, mbl, mbm, mbn, mbo, mbp, mbq, mbr, mbs, mbt;
        public string mbu, mbv, mbw, mbx;
        // Search String
        public string searchtext;
        // Search String Variables
        public string sma, smb, smc, smd, sme, smf, smg, smh, smi, smj, smk, sml, smm, smn, smo, smp, smq, smr, sms, smt, smu, smv, smw, smx, smy, smz;
        public string smaa, smab, smac, smad, smae, smaf, smag, smah, smai, smaj, smak, smal, smam, sman, smao;
        //
        public string Ssma, Ssmb, Ssmc, Ssmd, Ssme, Ssmf, Ssmg, Ssmh;
        public bool locate;
        public string SelectedText, Mex;
        private decimal smc1, ourcost, custcost;
        private decimal sme1;
        private string spacer;
        private int smd1, pass;
        public string DblClickedText;
      
        public bool Landscape { get; set; }

        public NegInvRpt()
        {
            InitializeComponent();
            label9.Visible = false;
            Icon = image100;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
            LocateInfo();
            label8.Text = "Double-Click on Index #, Edit Data, then click on 'SAVE NEW DATA' button.";
            Start();
        }

        private void NegInvRpt_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void NegInvRpt_FormClosed(object sender, FormClosedEventArgs e)
        {
            Hide();
            MainUtilitiesMenu f0 = new MainUtilitiesMenu();
            f0.Show();
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button3.PerformClick();
            }
        }

        // The Windows Forms Designer requires the following procedure.
        private void Start()
        {
            return;
            //this.components = new Container();
            this.printButton = new Button();
            //this.ClientSize = new System.Drawing.Size(504, 381);
            this.ClientSize = new System.Drawing.Size(820, 620);
            this.Text = "Print Example";

            printButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            printButton.Location = new System.Drawing.Point(32, 110);
            printButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            printButton.TabIndex = 0;
            printButton.Text = "Print the file.";
            printButton.Size = new System.Drawing.Size(136, 40);
            printButton.Click += new System.EventHandler(button5_Click);

            this.Controls.Add(printButton);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                pass++;
                // Create output file
                if (pass == 1)
                {
                    richTextBox2.Text = richTextBox2.Text + "\n\n" + "Total Our Cost: " + ourcost.ToString("C2") + "\n" + "Total Sell Cost: " + custcost.ToString("C2") + "\n";
                    richTextBox3.Text = richTextBox3.Text + "\n\n" + "Total Our Cost: " + ourcost.ToString("C2") + "\n" + "Total Sell Cost: " + custcost.ToString("C2") + "\n";
                }
                TextWriter txt = new StreamWriter("I:\\Datafile\\Doc\\NegInvRpt.txt");
                txt.Write(richTextBox2.Text);
                txt.Close();
                // Print output file
                streamToPrint = new StreamReader ("I:\\Datafile\\Doc\\NegInvRpt.txt");
                try
                {
                    printFont = new Font("Lucida Sans Unicode", 10);
                    PrintDocument pd = new PrintDocument();
                    pd.DefaultPageSettings.Landscape = true;                    // Set to Landscape, False = Portrait
                    pd.PrintPage += new PrintPageEventHandler
                       (this.pd_PrintPage);
                    pd.Print();
                }
                finally
                {
                    streamToPrint.Close();
                }
                PrintRTB3();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PrintRTB3()
        {
            try
            {
                TextWriter txt = new StreamWriter("I:\\Datafile\\Doc\\OrderRpt.txt");
                txt.Write(richTextBox3.Text);
                txt.Close();
                // Print output file
                streamToPrint = new StreamReader("I:\\Datafile\\Doc\\OrderRpt.txt");
                try
                {
                    printFont = new Font("Lucida Sans Unicode", 10);
                    PrintDocument pd = new PrintDocument();
                    pd.DefaultPageSettings.Landscape = false;                    // Set to Landscape, False = Portrait
                    pd.PrintPage += new PrintPageEventHandler
                       (this.pd_PrintPage);
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

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            MainUtilitiesMenu f0 = new MainUtilitiesMenu();
            f0.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            LocateInfo();
        }

        private void richTextBox1_DoubleClick(object sender, EventArgs e)
        {
            label9.Visible = false;
            DblClickedText = richTextBox1.SelectedText;
            DisplayData();
            textBox1.Text = Ssma;   // Part Num
            textBox2.Text = Ssmb;   // Description
            textBox3.Text = Ssmc;   // Our Cost
            textBox4.Text = Ssmd;   // Quantity
            textBox5.Text = Ssme;   // Sell to Customer
            label13.Text = Ssmf;    // Index
        }

        private void button3_Click(object sender, EventArgs e)
        {
            EditFile();
            label9.Visible = true;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            label13.Text = "";
            button2.PerformClick();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pass++;
            if (pass == 1)
            {
                richTextBox2.Text = richTextBox2.Text + "\n\n" + "Total Our Cost: " + ourcost.ToString("C2") + "\n" + "Total Sell Cost: " + custcost.ToString("C2") + "\n";
            }
            TextWriter txt = new StreamWriter("I:\\Datafile\\Doc\\NegInvRpt.txt");
            txt.Write(richTextBox2.Text);
            txt.Close();
            var fileToOpen = "I:\\Datafile\\Doc\\NegInvRpt.txt";
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
        }


        public void DisplayData()
        {
            try
            {
                StreamReader reader = new StreamReader(Locate, Encoding.GetEncoding("Windows-1252"));
                String line = reader.ReadLine();

                List<string> listA = new List<string>();
                List<string> listB = new List<string>();
                List<string> listC = new List<string>();
                List<string> listD = new List<string>();
                List<string> listE = new List<string>();
                List<string> listF = new List<string>();
                List<string> listG = new List<string>();
                List<string> listH = new List<string>();


                loopCount = 0;
                loop = 0;
                smao = "";

                while (!reader.EndOfStream)
                {
                    var lineRead = reader.ReadLine();
                    var values = lineRead.Split(',');

                    listA.Add(values[0]);       //  PN          Part Number
                    listB.Add(values[1]);       //  Desc        Description
                    listC.Add(values[2]);       //  Price       Our Cost
                    listD.Add(values[3]);       //  Stock       Number of pieces in stock
                    listE.Add(values[4]);       //  Cost        Customer Cost (Price * 1.3)
                    listF.Add(values[5]);       //  Index       Item Index #
                    listG.Add(values[6]);       //  Vendor PN   Vendor Part Number
                    listH.Add(values[7]);       //  Vendor      Vendor

                    sma = listA[loopCount].ToUpper();
                    smb = listB[loopCount].ToUpper();
                    smc = listC[loopCount].ToUpper();
                    smd = listD[loopCount].ToUpper();
                    sme = listE[loopCount].ToUpper();
                    smao = listF[loopCount].ToUpper();

                    smd1 = Convert.ToInt32(smd);

                    switch (sma.Length)
                    {
                        case 7:
                            sma = sma + "\t";
                            break;
                        case 8:
                            sma = sma + "\t";
                            break;
                        case 9:
                            sma = sma + "\t";
                            break;
                        case 10:
                            sma = sma + "\t";
                            break;
                        case 11:
                            sma = sma + "\t";
                            break;
                    }
                    smc1 = Convert.ToDecimal(smc);
                    sme1 = Convert.ToDecimal(sme);
                    switch (smc1.ToString("C2").Length)
                    {
                        case 1:
                            spacer = "   ";
                            break;
                        case 2:
                            spacer = "   ";
                            break;
                        case 3:
                            spacer = "   ";
                            break;
                        case 4:
                            spacer = "   ";
                            break;
                        case 5:
                            spacer = "   ";
                            break;
                        case 6:
                            spacer = "   ";
                            break;
                        case 7:
                            spacer = "   ";
                            break;
                        case 8:
                            spacer = "   ";
                            break;
                        default:
                            spacer = "   ";
                            break;
                    }
                    if (smao == DblClickedText)
                    {
                        //richTextBox1.Text = richTextBox1.Text + smao + "\t\t" + sma + "\t" + spacer + smc1.ToString("C2") + "\t" + smd + "\t" + spacer + sme1.ToString("C2") + "\t\t" + smb + "\n";
                        loop++;
                        label1.Text = "Found: " + loop.ToString();
                        Ssma = sma;
                        Ssmb = smb;
                        Ssmc = smc;
                        Ssmd = smd;
                        Ssme = sme;
                        Ssmf = smao;
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

                MessageBox.Show("Error 1329: Sorry an error has occured: " + ex.Message);
            }
        }

        public void LocateInfo()
        {
            richTextBox2.Text = "\t\t                                        Negative Inventory Report " + DateTime.Now.ToShortDateString() + "\n";
            richTextBox2.Text = richTextBox2.Text + "PART NUMBER         COST       SELL      Item Description                    VENDOR PN    VENDOR" + "\n\r";
            richTextBox3.Text = "\t\t                                        Parts Ordering Report " + DateTime.Now.ToShortDateString() + "\n";
            richTextBox3.Text = richTextBox3.Text + "PART NUMBER         COST       Item Description                   VENDOR PN     VENDOR" + "\n\r";
            try
            {
                StreamReader reader = new StreamReader(Locate, Encoding.GetEncoding("Windows-1252"));
                String line = reader.ReadLine();

                List<string> listA = new List<string>();
                List<string> listB = new List<string>();
                List<string> listC = new List<string>();
                List<string> listD = new List<string>();
                List<string> listE = new List<string>();
                List<string> listF = new List<string>();
                List<string> listG = new List<string>();
                List<string> listH = new List<string>();

                loopCount = 0;
                loop = 0;
                smao = "";

                while (!reader.EndOfStream)
                {
                    var lineRead = reader.ReadLine();
                    var values = lineRead.Split(',');


                    listA.Add(values[0]);       //  PN          Part Number
                    listB.Add(values[1]);       //  Desc        Description
                    listC.Add(values[2]);       //  Price       Our Cost
                    listD.Add(values[3]);       //  Stock       Number of pieces in stock
                    listE.Add(values[4]);       //  Cost        Customer Cost (Price * 1.3)
                    listF.Add(values[5]);       //  Index       Item Index #
                    listG.Add(values[6]);       //  Vendor PN   Vendor Part Number
                    listH.Add(values[7]);       //  Vendor      Vendor

                    sma = listA[loopCount].ToUpper();
                    smb = listB[loopCount].ToUpper();
                    smc = listC[loopCount].ToUpper();
                    smd = listD[loopCount].ToUpper();
                    sme = listE[loopCount].ToUpper();
                    smao = listF[loopCount].ToUpper();
                    smg = listG[loopCount].ToUpper();
                    smh = listH[loopCount].ToUpper();

                    smd1 = Convert.ToInt32(smd);

                    switch (sma.Length)
                    {
                        case 7:
                            sma = sma + "\t";
                            break;
                        case 8:
                            sma = sma + "\t";
                            break;
                        case 9:
                            sma = sma + "\t";
                            break;
                        case 10:
                            sma = sma + "\t";
                            break;
                        case 11:
                            sma = sma + "\t";
                            break;
                    }
                    smc1 = Convert.ToDecimal(smc);
                    sme1 = Convert.ToDecimal(sme);
                    switch (smc1.ToString("C2").Length)
                    {
                        case 1:
                            spacer = "   ";
                            break;
                        case 2:
                            spacer = "   ";
                            break;
                        case 3:
                            spacer = "   ";
                            break;
                        case 4:
                            spacer = "   ";
                            break;
                        case 5:
                            spacer = "   ";
                            break;
                        case 6:
                            spacer = "   ";
                            break;
                        case 7:
                            spacer = "   ";
                            break;
                        case 8:
                            spacer = "   ";
                            break;
                        default:
                            spacer = "   ";
                            break;
                    }
                    if (smd1 < 0)
                    {
                        richTextBox1.Text = richTextBox1.Text + smao + "\t\t" + sma + "\t" + spacer + smc1.ToString("C2") + "\t" + smd + "\t" + spacer + sme1.ToString("C2") + "\t\t" + smb + "\n";

                        if (smg != ".")
                        {
                            richTextBox2.Text = richTextBox2.Text + sma + "\t" + spacer + smc1.ToString("C2") + " \t" + "\t" + spacer + sme1.ToString("C2") + "\t" + smb + "  " + smg + "\t" + smh + "\n";
                            richTextBox3.Text = richTextBox3.Text + sma + "\t" + spacer + smc1.ToString("C2") + " \t" + "\t" + spacer + "\t" + smb + "  " + smg + "\t " + smh + "\n";

                        }
                        if (smg == ".")
                        {
                            richTextBox2.Text = richTextBox2.Text + sma + "\t" + spacer + smc1.ToString("C2") + " \t" + smd + "\t" + spacer + sme1.ToString("C2") + "\t" + smb + "\n";
                        }
                        loop++;
                        label1.Text = "Found: " + loop.ToString();
                        Ssma = sma;
                        Ssmb = smb;
                        Ssmc = smc;
                        Ssmd = smd;
                        Ssme = sme;
                        Ssmf = smao;
                        Ssmg = smg;
                        ourcost = ourcost + Convert.ToDecimal(smc);
                        custcost = custcost + Convert.ToDecimal(sme);
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

                MessageBox.Show("Error 1329: Sorry an error has occured: " + ex.Message);
            }
        }

        private void EditFile()
        {
            Ssma = textBox1.Text;
            Ssmb = textBox2.Text;
            Ssmc = textBox3.Text;
            Ssmd = textBox4.Text;
            Ssme = textBox5.Text;

            string path = Locate;
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
                                if (split[5] == Ssmf)
                                {
                                    split[0] = Ssma;
                                    split[1] = Ssmb;
                                    split[2] = Ssmc;
                                    split[3] = Ssmd;
                                    split[4] = Ssme;
                                    split[5] = Ssmf;

                                    line = String.Join(",", split);
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error Line 572: \n" + ex);
                            }
                        }

                        lines.Add(line);
                    }
                }

                using (StreamWriter writer = new StreamWriter(path, false))
                {
                    foreach (String line in lines)
                        writer.WriteLine(line);
                }
            }
        }
    }
}
