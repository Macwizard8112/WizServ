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
    public partial class frmChild : Form
    {
        public frmChild()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            EnterServiceCustMenu f2 = new EnterServiceCustMenu();
            f2.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
