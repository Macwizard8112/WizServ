using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Media;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WizServ
{
    public partial class DoNotRepairSelect : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        private readonly string file4 = @"I:\\Datafile\\Control\\DNR\\Brand_DNR.csv";
        private readonly string file5 = @"I:\\Datafile\\Control\\DNR\\DoNotRepair.csv";
        private string file6;
        public string fname, lname, addr, city, state, zip, hphone, wphone, NextClaimNum;
        private bool war_prd, ready;
        private int loopCount, loop;
        public string mSelected;
        public string pass;
        public string from = Version.From;
        public bool wasSelected;

        public DoNotRepairSelect()
        {
            InitializeComponent();
            Icon = image100;
            from = Version.From;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
            pass = "START";
            GetProduct();
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            if (wasSelected != true)
            {
                MessageBox.Show("You must select a pull down item.");
            }
            if (wasSelected == true)
            {
                if (from == "ENTERSERVICECUSTMENU")
                {
                    Hide();
                    Warranty f2 = new Warranty();
                    f2.Show();
                }
                else
                {
                    Hide();
                    MainMenu f2 = new MainMenu();
                    f2.Show();
                }
            }
        }

        private void DoNotRepairSelect_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void DoNotRepairSelect_FormClosed(object sender, FormClosedEventArgs e)
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }

        private void ComboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            var Sel = comboBox1.SelectedItem;
            var Selec = comboBox1.Text;

            if (Selec.Length != null)
            {
                wasSelected = true;
            }
            try
            {
                label2.Text = Sel.ToString();
            }
            catch (Exception)
            {
                label2.Text = "P";
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            wasSelected = true;
            var jj = comboBox1.Text.Length;
            if (jj > 0)
            {
                richTextBox1.Text = "";
                var Sel = comboBox1.SelectedItem;
                try
                {
                    label2.Text = "Selected: " + Sel.ToString();
                }
                catch (Exception)
                {
                    label2.Text = "P";
                }
                switch (Sel)
                {
                    case "AMPEG":
                        file6 = @"I:\\Datafile\\Control\\DNR\\AMPEG_DNR.csv";
                        pass = "AMPEG";
                        GetProduct();
                        break;
                    case "BARCUS BERRY":
                        file6 = @"I:\\Datafile\\Control\\DNR\\BARCUS_DNR.csv";
                        pass = "BARCUS BERRY";
                        GetProduct();
                        break;
                    case "BEHRINGER/BUGERA":
                        file6 = @"I:\\Datafile\\Control\\DNR\\BEHRINGER_DNR.csv";
                        pass = "BEHRINGER";
                        GetProduct();
                        break;
                    case "BENJAMIN ADAMS":
                        file6 = @"I:\\Datafile\\Control\\DNR\\BENJAMINADAMS_DNR.csv";
                        pass = "BENJAMIN ADAMS";
                        GetProduct();
                        break;
                    case "BOSE":
                        file6 = @"I:\\Datafile\\CONTROL\\DNR\\BOSE_DNR.csv";
                        pass = "BOSE";
                        GetProduct();
                        break;
                    case "BUGERA":
                        file6 = @"I:\\Datafile\\Control\\DNR\\BUGERA.csv";
                        pass = "BEHRINGER";
                        GetProduct();
                        break;
                    case "ELECTRO-VOICE -BOSCH":
                        file6 = @"I:\\Datafile\\Control\\DNR\\EVB_DNR.csv";
                        pass = "ELECTRO-VOICE BOSCH";
                        GetProduct();
                        break;
                    case "PRESONUS":
                        file6 = @"I:\\Datafile\\Control\\DNR\\DoNotRepair.csv";
                        pass = "PRESONUS";
                        GetProduct();
                        break;
                    case "FENDER MUSICAL INSTR":
                        file6 = @"I:\\Datafile\\Control\\DNR\\FENDER_DNR.csv";
                        pass = "FENDER";
                        GetProduct();
                        break;
                    case "GEMINI":
                        file6 = @"I:\\Datafile\\Control\\DNR\\GEMINI.csv";
                        pass = "GEMINI";
                        GetProduct();
                        break;
                    case "GIBSON":
                        file6 = @"I:\\Datafile\\Control\\DNR\\GIBSON_DNR.csv";
                        pass = "GIBSON";
                        GetProduct();
                        break;
                    case "ROLAND":
                        file6 = @"I:\\Datafile\\CONTROL\\DNR\\ROLAND_DNR.csv";
                        pass = "ROLAND";
                        GetProduct();
                        break;
                    case "KRK":
                        file6 = @"I:\Datafile\\Control\\DNR\\KRK_DNR.csv";
                        pass = "KRK";
                        GetProduct();
                        break;
                    case "LAB GRUPPEN":
                        file6 = @"I:\\Datafile\\Control\\DNR\\LAB_GRUPPEN_DNR.csv";
                        pass = "LAB GRUPPEN";
                        GetProduct();
                        break;
                    case "MACKIE DESIGNS":
                        file6 = @"I:\\Datafile\\CONTROL\\DNR\\MACKIE_DNR.csv";
                        pass = "ROLAND";
                        GetProduct();
                        break;
                    case "LOUD AUDIO LLC.":
                        file6 = @"I:\\Datafile\\CONTROL\\DNR\\MACKIE_DNR.csv";
                        pass = "ROLAND";
                        GetProduct();
                        break;
                    case "JBL PROFESSIONAL":
                        file6 = @"I:\\Datafile\\CONTROL\\DNR\\JBL_DNR.csv";
                        pass = "JBL";
                        GetProduct();
                        break;
                    case "KORG/VOX":
                        file6 = @"I:\\Datafile\\CONTROL\\DNR\\KORG_DNR.csv";
                        pass = "KORG";
                        GetProduct();
                        break;
                    case "NUMARK":
                        file6 = @"I:\\Datafile\\CONTROL\\DNR\\NUMARK_DNR.csv";
                        pass = "NUMARK";
                        GetProduct();
                        break;
                    case "ONKYO":
                        file6 = @"I:\\Datafile\\CONTROL\\DNR\\ONKYO_DNR.csv";
                        pass = "ONKYO";
                        GetProduct();
                        break;
                    case "PIONEER":
                        file6 = @"I:\\Datafile\\CONTROL\\DNR\\PIONEER_DNR.csv";
                        pass = "PIONEER";
                        GetProduct();
                        break;
                    case "SONY":
                        file6 = @"I:\\Datafile\\CONTROL\\DNR\\SONY_DNR.csv";
                        pass = "SONY";
                        GetProduct();
                        break;
                    case "SUNFIRE":
                        file6 = @"I:\\Datafile\\CONTROL\\DNR\\SUNFIRE_DNR.csv";
                        pass = "SUNFIRE";
                        GetProduct();
                        break;
                    case "T-REX":
                        file6 = @"I:\\Datafile\\CONTROL\\DNR\\T-REX_DNR.csv";
                        pass = "T-REX";
                        GetProduct();
                        break;
                    case "VELODYNE":
                        file6 = @"I:\\Datafile\\CONTROL\\DNR\\VELODYNE_DNR.csv";
                        pass = "VELODYNE";
                        GetProduct();
                        break;
                    case "VOX":
                        file6 = @"I:\\Datafile\\CONTROL\\DNR\\VOX_DNR.csv";
                        pass = "KORG";
                        GetProduct();
                        break;
                    case "YAMAHA":
                        file6 = @"I:\\Datafile\\CONTROL\\DNR\\YAMAHA_DNR.csv";
                        pass = "YAMAHA";
                        GetProduct();
                        break;
                    case "WHARFEDALE PRO":
                        file6 = @"I:\\Datafile\\CONTROL\\DNR\\WARFDALE_DNR.csv";
                        pass = "WHARFEDALE PRO";
                        GetProduct();
                        break;
                    default:
                        richTextBox1.Text = "\n Nothing Listed.";
                        break;
                }
            }
        }
        private void RichTextBox1_MouseUp(object sender, MouseEventArgs e)
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



        public void GetProduct()                // Populate Product Lit pulldown
        {
            richTextBox1.Text = "";
            if (pass == "START")
            {
                file6 = file4;
            }
            try
            {
                StreamReader reader = new StreamReader(file6, Encoding.GetEncoding("Windows-1252"));
                String line = reader.ReadLine();

                List<string> listA = new List<string>();

                loopCount = 0;
                while (!reader.EndOfStream)
                {
                    var lineRead = reader.ReadLine();
                    var values = lineRead.Split(',');

                    listA.Add(values[0]);       //  war_prd

                    if (pass == "START")
                    {
                        comboBox1.Items.Add(listA[loopCount]);
                    }
                    if (pass != "START")
                    {
                        richTextBox1.Text = richTextBox1.Text + listA[loopCount] + Environment.NewLine;
                    }
                   
                    loop++;
                    loopCount++;
                }
                reader.Close(); // Close the open file
                //comboBox1.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 326: Sorry an error has occured: " + ex.Message);
            }
        }
    }
}
