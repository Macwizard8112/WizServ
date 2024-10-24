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
    public partial class ChartsMenu : Form
    {
        public ChartsMenu()
        {
            InitializeComponent();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Hide();
            MainUtilitiesMenu f0 = new MainUtilitiesMenu();
            f0.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            ChartByTech f0 = new ChartByTech();
            f0.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            MainMenu f0 = new MainMenu();
            f0.Show();
        }
    }
}
