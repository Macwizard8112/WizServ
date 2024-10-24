using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WizServ
{
    public partial class NightlyMenu : Form
    {
        public Icon image100 = Properties.Resources.WizServ;

        public NightlyMenu()
        {
            InitializeComponent();
            Icon = image100;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            Brands f2 = new Brands();
            f2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
