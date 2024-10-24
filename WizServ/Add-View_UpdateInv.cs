using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WizServ
{
    public partial class Add_View_UpdateInv : Form
    {
        public Icon image100 = Properties.Resources.WizServ;

        public Add_View_UpdateInv()
        {
            InitializeComponent();
            Icon = image100;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Hide();
            InventoryMenu f2 = new InventoryMenu();
            f2.Show();
        }
    }
}
