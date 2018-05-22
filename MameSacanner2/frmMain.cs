using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Timers;


namespace MameSacanner2
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            try
            {
                this.Location = Screen.AllScreens[1].WorkingArea.Location;
            }
            catch
            {
                this.Location = Screen.AllScreens[0].WorkingArea.Location;
            }
        }

        private int scrll { get; set; }

        public void GetPic()
        {
            var collection = new List<string>();

            User32.EnumDelegate filter = delegate(IntPtr hWnd, int lParam)
            {
                StringBuilder strbTitle = new StringBuilder(255);
                int nLength = User32.GetWindowText(hWnd, strbTitle, strbTitle.Capacity + 1);
                string strTitle = strbTitle.ToString();

                if (User32.IsWindowVisible(hWnd) && string.IsNullOrEmpty(strTitle) == false)
                {
                    collection.Add(strTitle);
                }
                return true;
            };

            if (User32.EnumDesktopWindows(IntPtr.Zero, filter, IntPtr.Zero))
            {
                
                foreach (var item in collection)
                {
                    if (item.Contains("[") && item.StartsWith("MAME:"))
                    {
                        string output = item.Substring(item.IndexOf('[') + 1);
                        string value2 = output;
                        string romname = value2;
                        //string line;
                        string currentLine = string.Empty;
                        string previousLine = string.Empty;

                        int index1 = value2.IndexOf("]");

                        if (index1 != -1)
                        {
                            romname = value2.Remove(index1, 1); // Use integer from IndexOf.
                        }
                        pictureBox1.ImageLocation = (Application.StartupPath + "\\romname" + "\\" + romname + ".png");
                        
                    }
                }
            }
        }
                   
        private void Form1_Load(object sender, EventArgs e)
        {
            GetPic();
            timer1.Start();

            var aTimer = new System.Timers.Timer();

            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 500;
            aTimer.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
           GetPic();   
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            timer1.Stop();

            frmScore Score = new frmScore();
            Score.Show();   
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
   }
