using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Timers;
using System.Drawing.Text;

namespace MameSacanner2
{
    public partial class frmScore : Form
    {
        public frmScore()
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
           // label1.Text = strLabel;
        }


        

        // private int scrll { get; set; }

        private int scrll { get; set; }

         public void GetScore()
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
                     if (item.Contains("["))
                     {
                         string output = item.Substring(item.IndexOf('[') + 1);
                         string value2 = output;
                         string romname = value2;
                          
                         string currentLine = string.Empty;
                         string previousLine = string.Empty;

                         int index1 = value2.IndexOf("]");

                         if (index1 != -1)
                         {
                             romname = value2.Remove(index1, 1); // Use integer from IndexOf.
                         }
                         
                         string[] lines = File.ReadAllLines(Application.StartupPath + "\\" + "HighScores.txt");
                         int i = 0;
                      
                         var Scoreitems = from line in lines
                                          where i++ != -1
                                          //let words = line.Split('|') 
                                          select new
                                          {
                                              rom = line.Split('|')[0],
                                              date = line.Split('|')[1],
                                              name = line.Split('|')[2],
                                              score = line.Split('|')[3]
                                          };
                         foreach (var line in Scoreitems)
                         {                                
                            //if (line.rom.Contains(romname))
                              if (line.rom == romname)
                                try
                                {
                    
                                   // lblScore.Text = line.score.ToString();
                                   
                                    lblScore.Invoke((Action)(() => lblScore.Text = line.score.ToString()));
                                    //label2.Text = line.rom.ToString();
                                    label2.Invoke((Action)(() => label2.Text = line.rom.ToString()));
                                    //lblDate.Text = line.date.ToString();
                                    lblDate.Invoke((Action)(() => lblDate.Text = line.date.ToString()));
                                    //lblName.Text = line.name.ToString();
                                    lblName.Invoke((Action)(() => lblName.Text = line.name.ToString()));
                                    //lblNotFound.Text = " ";
                                    lblNotFound.Invoke((Action)(() => lblNotFound.Text = " "));
                                }
                                catch (InvalidOperationException e)
                                {
                                    MessageBox.Show(e.Message);
                                }
                                catch (Win32Exception e)
                                {
                                    //    // computer keeps throwing errors ill just keep catching them
                                    MessageBox.Show(e.Message);
                                }
                                catch (TimeoutException e)
                                {
                                    MessageBox.Show(e.Message);
                                }
                            
                         }   
                     }
                 }
             }
         }
        
        public void frmScore_Load(object sender, EventArgs e)
        {
            GetScore();
            timer1.Start();

            var ScoreTimer = new System.Timers.Timer();
            //var aTimer = new System.Timers.Timer();

            ScoreTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent2);
            ScoreTimer.Interval = 700;
            ScoreTimer.Enabled = true;

        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            timer1.Stop();
            this.Hide();
            frmMain main = new frmMain();
            main.timer1.Start();
        
        }

        private void OnTimedEvent2(object source, ElapsedEventArgs e)
        {
            if (lblScore.ForeColor == Color.Red)
                lblScore.ForeColor = Color.Black;
            else
                lblScore.ForeColor = Color.Red;

            
        }

        private void frmScore_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Application.Exit();
            }
        }

        private void lblScore_Click(object sender, EventArgs e)
        {

        }
    }
}
