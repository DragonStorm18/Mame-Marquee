using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MameSacanner2
{
    public partial class frmSettings : Form
    {
        public frmSettings()
        {
            InitializeComponent();
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog pics = new FolderBrowserDialog();
            pics.ShowDialog();

            string loc = pics.SelectedPath;

            textBox1.Text = loc;
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            StreamWriter setting = new StreamWriter(Application.StartupPath + "\\" + "settings.txt");

            if (File.Exists(Application.StartupPath + "\\" + "settings.txt"))
            {
                setting.WriteLine(textBox1.Text);
                setting.Close();
            }
            else
            {
                File.Create(Application.StartupPath + "\\" + "settings.txt");
            }

           
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            StreamReader set = new StreamReader(Application.StartupPath + "\\" + "settings.txt");

            textBox1.Text = set.ReadLine();
            set.Close();

            if (File.Exists(Application.StartupPath + "\\" + "settings.txt"))
            {
                frmMain begin = new frmMain();
                begin.Show();
                this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;
            }
        }
    }
}