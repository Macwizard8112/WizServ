using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WizServ
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainMenu());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sorry an unknown error has occured\nContact DOC to fix.\n" + ex);
            }
        }
    }
}
