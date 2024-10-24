using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace WizServ
{
    public partial class Info : Form
    {
        public Icon image100 = Properties.Resources.WizServ;
        public string versionX, computerDescription;
        public bool TheOSis;

        public Info()
        {
            InitializeComponent();
            Icon = image100;
            MaximizeBox = false;
            MinimizeBox = true;
            ControlBox = true;
            CheckOSEnv();
            GAssembLyInfo();
            PullMachineInfo();
            SetLinkLabelText();
        }

        private void SetLinkLabelText()
        {
            //this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Text = "Parts@wizardelectronics.com";

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.linkLabel1.LinkVisited = true;
            this.linkLabel1.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            Process.Start("mailto:Parts@wizardelectronics.com?subject=Tech_Support_for_TechServ&body=" + "Need technical support for WizServ. " + computerDescription + " computer." +  Environment.NewLine);
            this.linkLabel1.LinkVisited = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            MainMenu f2 = new MainMenu();
            f2.Show();
        }

        public void CheckOSEnv()
        {
            TheOSis = Environment.Is64BitOperatingSystem;
        }

        public void PullMachineInfo()            // To find the Computer Name / Description
        {
            string key = @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\lanmanserver\parameters";
            computerDescription = (string)Registry.GetValue(key, "srvcomment", null);
            if (TheOSis == true)
            {
                label4.Text = "Computer : " + computerDescription + ", 64Bit OS";
            }
            else
            {
                label4.Text = "Computer : " + computerDescription + ", 32bit OS";
            }
        }

        public void GAssembLyInfo()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            versionX = fvi.FileVersion;
            label6.Text = "Version: " + versionX + "  © 2021, 2022, 2023";
            Text = "Information   v" + versionX;      // Set Menu Title, Get full Version infomation
        }
    }
}
