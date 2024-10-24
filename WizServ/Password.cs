using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WizServ
{
    public partial class Password : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        public string from, answer, line, pwis;

        public Password()
        {
            InitializeComponent();
            Icon = image100;
            from = Version.From;
            ReadPassword();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Hide();
            RetrieveMenu f2 = new RetrieveMenu();
            f2.Show();
        }

        private void ReadPassword()
        {
            try
            {
                string text = File.ReadAllText(@"I:\\Datafile\\Control\\Info.txt", Encoding.UTF8);
                pwis = text;                // Read in 6545
                var t = Int32.Parse(pwis);  // Convert string to int
                var y = 9999 - t;           // Subtract 9999 - 6545 = 3454
                pwis = y.ToString();        // Convert 3454 to string - this is the password to check against.
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            Version.PWSelected = "Yes";
            if (e.KeyCode == Keys.Enter)
            {
                answer = textBox1.Text;
                if (answer == "3454" || answer == "911" || answer == pwis)
                {
                    switch (from)
                    {
                        case "PARTSUSED":
                            Version.Valid = true;
                            break;
                        case "CLAIMASSIGN":
                            Version.PWSelected = "No";
                            Hide();
                            AssignClaim f6 = new AssignClaim();
                            f6.Show();
                            return;
                            break;
                        case "ServiceUtility":
                            Version.PWSelected = "No";
                            Hide();
                            UtilityService f2 = new UtilityService();
                            f2.Show();
                            break;
                        case "CalimsMGTMenu":
                            if (Version.Estimate == "Y")
                            {
                                Version.PWSelected = "No";
                                Hide();
                                CreateEstimate f1 = new CreateEstimate();
                                f1.Show();
                            }
                            else
                            {
                                Version.PWSelected = "No";
                                Hide();
                                OpenClaimsMenu f1 = new OpenClaimsMenu();
                                f1.Show();
                            }
                            break;
                        case "MAINMENU":
                            Version.PWSelected = "No";
                            Hide();
                            MainUtilitiesMenu f0 = new MainUtilitiesMenu();
                            f0.Show();
                            break;
                        default:
                            Hide();
                            MainMenu f3 = new MainMenu();
                            f3.Show();
                            break;
                    }
                }
                else
                {
                    Hide();
                    MainMenu f1 = new MainMenu();
                    f1.Show();
                }
            }
        }
    }
}
