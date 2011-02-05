using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CalendarScreenSaver.Properties;

namespace CalendarScreenSaver
{
    public partial class SettingsForm : Form
    {
        private Settings _Settings = new Settings();

        public SettingsForm()
        {
            InitializeComponent();
            tbURI.Text = _Settings.CalendarURI;
            tbUserName.Text = _Settings.Username;
            tbPassword.Text = _Settings.Password;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _Settings.CalendarURI = tbURI.Text;
            _Settings.Username = tbUserName.Text;
            _Settings.Password = tbPassword.Text;

            _Settings.Save();
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
