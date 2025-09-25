using System;
using System.Windows.Forms;

namespace TimerApplication
{
    public partial class TimerForm : Form
    {
        bool isCountingDown = false;
        
        int milliseconds = 0;
        int seconds = 0;
        int minutes = 0;
        int hours = 0; 
        int days = 0;
        
        int systemHours = DateTime.Now.Hour;
        int systemMinutes = DateTime.Now.Minute;
        int systemSeconds = DateTime.Now.Second;
        
        public TimerForm()
        {
            InitializeComponent();
        }
        
        private void StartButton_Click(object sender, EventArgs e)
        {
            CountdownHoursLabel.Text = string.Empty;
            CountdownMinutesLabel.Text = string.Empty;
            CountdownSecondsLabel.Text = string.Empty;
            CountdownDaysLabel.Text = string.Empty;
            CountdownMillisecondsLabel.Text = string.Empty;
            
            StopwatchSecondsLabel.Text = seconds.ToString();
            StopwatchMinutesLabel.Text = minutes.ToString();
            StopwatchHoursLabel.Text = hours.ToString();
            StopwatchMillisecondsLabel.Text = milliseconds.ToString();
            StopwatchDaysLabel.Text = days.ToString();
            
            MainTimer.Interval = 10;
            MainTimer.Start();
            StartButton.Enabled = false;
            
            if(StartButton.BackColor == Color.Green)
            {
                Environment.Exit(0);
            }
        }
        
        private void MainTimer_Tick(object sender, EventArgs e)
        {
            if(!isCountingDown)
            {
                milliseconds++;
                if (milliseconds == 100)
                {
                    seconds++;
                    milliseconds = 0;
                }
                if (seconds == 60)
                {
                    minutes++;
                    seconds = 0;
                }
                if (minutes == 60)
                {
                    hours++;
                    minutes = 0;
                }
                if (hours == 24)
                {
                    days++;
                    hours = 0;
                }
                
                StopwatchSecondsLabel.Text = seconds.ToString();
                StopwatchMinutesLabel.Text = minutes.ToString();
                StopwatchHoursLabel.Text = hours.ToString();
                StopwatchMillisecondsLabel.Text = milliseconds.ToString();
                StopwatchDaysLabel.Text = days.ToString();
            }
            else
            {
                StopwatchSecondsLabel.Text = string.Empty;
                StopwatchMinutesLabel.Text = string.Empty;
                StopwatchHoursLabel.Text = string.Empty;
                StopwatchMillisecondsLabel.Text = string.Empty;
                StopwatchDaysLabel.Text = string.Empty;
                
                StartButton.Enabled = true;
                StartButton.BackColor = Color.Green;
                
                CountdownMillisecondsLabel.Text = milliseconds.ToString();
                CountdownSecondsLabel.Text = seconds.ToString();
                CountdownMinutesLabel.Text = minutes.ToString();
                CountdownHoursLabel.Text = hours.ToString();
                CountdownDaysLabel.Text = days.ToString();
                
                milliseconds--;
                if (milliseconds < 0)
                {
                    milliseconds = 99;
                    seconds--;
                }
                if (seconds < 0)
                {
                    seconds = 59;
                    minutes--;
                }
                if (minutes < 0)
                {
                    minutes = 59;
                    hours--;
                }
                if (hours < 0)
                {
                    hours = 23;
                    days--;
                }
            }
            
            if (hours == systemHours && minutes == systemMinutes && seconds == systemSeconds && !isCountingDown)
            {
                MainTimer.Stop();
                isCountingDown = true;
                StartButton.Enabled = true;
            }
        }
    }
}
