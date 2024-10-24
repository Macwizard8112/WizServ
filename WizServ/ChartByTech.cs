using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Win32;
using System.Threading;
using System.Windows.Forms;

namespace WizServ
{
    public partial class ChartByTech : Form
    {
        private readonly string Database = @"I:\\Datafile\\Control\\Database.CSV";       // This is Read only CSV
        public int loopCount, loop;
        public string IndexNo;

        public ChartByTech()
        {
            InitializeComponent();
        }

        private void ChartByTech_Load(object sender, EventArgs e)
        {
            BarExample(); //Show bar chart
            //SplineChartExample();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            ChartsMenu f0 = new ChartsMenu();
            f0.Show();
        }

        public void BarExample()
        {
        }
    }
}
