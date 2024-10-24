using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WizServ
{
    public partial class NextClaimNum : Form
    {
        public string path = @"c:\\Control\\NextClaim.CSV";
        public int nextClaimNumber;
        public string nextClaimNo, yearis;
        public string dateis = DateTime.Now.ToShortDateString();
        public string lastofyear;

        public NextClaimNum()
        {
            InitializeComponent();
            SetTheYear();
            ReadYear();
            GenerateNewClaimNum();
            if (Version.From == "EnterServiceCustMenu")
            {
                //DoExit();
            }
        }

        private void DoExit()
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }

        private void SetTheYear()
        {
            var length = dateis.Length;
            lastofyear = dateis.Substring(length - 4, 4);
            string path = @"I:\\Datafile\\Control\\Year.CSV";
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.WriteLine(lastofyear.ToString() + Environment.NewLine);
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error occured: Line 58: \n" + ex);
            }
        }

        private void ReadYear()                          
        {
            var length = dateis.Length;
            lastofyear = dateis.Substring(length-2, 2);
            label2.Text = lastofyear;

            string path = @"I:\\Datafile\\Control\\Year.CSV";

            using (var reader = new StreamReader(path))
            {
                List<string> listA = new List<string>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    listA.Add(values[0]);
                    yearis = listA[0];
                    label1.Text = yearis;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            EnterServiceCustMenu f2 = new EnterServiceCustMenu();
            f2.Show();
        }

        private void GenerateNewClaimNum()                          // DOUBLE-CHECK THIS NUMBER BEFORE USING - GO LIVE !
        {                                                           // **************************************************
            string path = @"I:\\Datafile\\Control\\NextClaim.CSV";

            using (var reader = new StreamReader(path))
            {
                List<string> listA = new List<string>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    listA.Add(values[0]);
                    nextClaimNo = listA[0];
                    label3.Text = nextClaimNo;
                }
            }
            switch (lastofyear)
            {
                case "21":
                    nextClaimNo = nextClaimNo;
                    break;
                case "22":
                    var x = nextClaimNo;
                    var y = x.Substring(2, (x.Length-2));
                    nextClaimNo = lastofyear + y;
                    break;
                case "23":
                    var x1 = nextClaimNo;
                    var y1 = x1.Substring(2, (x1.Length - 2));
                    nextClaimNo = lastofyear + y1;
                    break;
                case "24":
                    var x2 = nextClaimNo;
                    var y2 = x2.Substring(2, (x2.Length - 2));
                    nextClaimNo = lastofyear + y2;
                    break;
                case "25":
                    var x3 = nextClaimNo;
                    var y3 = x3.Substring(2, (x3.Length - 2));
                    nextClaimNo = lastofyear + y3;
                    break;
                case "26":
                    var x4 = nextClaimNo;
                    var y4 = x4.Substring(2, (x4.Length - 2));
                    nextClaimNo = lastofyear + y4;
                    break;
                case "27":
                    var x5 = nextClaimNo;
                    var y5 = x5.Substring(2, (x5.Length - 2));
                    nextClaimNo = lastofyear + y5;
                    break;
                case "28":
                    var x6 = nextClaimNo;
                    var y6 = x6.Substring(2, (x6.Length - 2));
                    nextClaimNo = lastofyear + y6;
                    break;
                case "29":
                    var x7 = nextClaimNo;
                    var y7 = x7.Substring(2, (x7.Length - 2));
                    nextClaimNo = lastofyear + y7;
                    break;
                case "30":
                    var x8 = nextClaimNo;
                    var y8 = x8.Substring(2, (x8.Length - 2));
                    nextClaimNo = lastofyear + y8;
                    break;
                case "31":
                    var x9 = nextClaimNo;
                    var y9 = x9.Substring(2, (x9.Length - 2));
                    nextClaimNo = lastofyear + y9;
                    break;
                case "32":
                    var x10 = nextClaimNo;
                    var y10 = x10.Substring(2, (x10.Length - 2));
                    nextClaimNo = lastofyear + y10;
                    break;
                case "33":
                    var x11 = nextClaimNo;
                    var y11 = x11.Substring(2, (x11.Length - 2));
                    nextClaimNo = lastofyear + y11;
                    break;
                case "34":
                    var x12 = nextClaimNo;
                    var y12 = x12.Substring(2, (x12.Length - 2));
                    nextClaimNo = lastofyear + y12;
                    break;
                case "35":
                    var x13 = nextClaimNo;
                    var y13 = x13.Substring(2, (x13.Length - 2));
                    nextClaimNo = lastofyear + y13;
                    break;
                case "36":
                    var x14 = nextClaimNo;
                    var y14 = x14.Substring(2, (x14.Length - 2));
                    nextClaimNo = lastofyear + y14;
                    break;
                default:
                    MessageBox.Show("Sorry, I was written to last from 2021 to 2036\nUpdate 'NextClaimNum.cs' file to continue.");
                    break;
            }

            nextClaimNumber = Int32.Parse(nextClaimNo);
            nextClaimNumber++;
            label4.Text = nextClaimNumber.ToString();

            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.WriteLine(nextClaimNumber.ToString() + Environment.NewLine);
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error occured: Line 57: \n" + ex);
            }
        }
    }
}
