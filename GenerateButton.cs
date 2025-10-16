using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GenerateButton
{
    public partial class GenerateButton : Form
    {
        public GenerateButton()
        {
            InitializeComponent();
            timer.Interval = 1000;
            random = new Random();
            timer.Start();
        }

        private Button button2, button3, button4, button5, button6;
        private List<Button> buttons;
        private Random random;

        private void button1_Click(object sender, EventArgs e)
        {
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();

            buttons = new List<Button> { button2, button3, button4, button5, button6 };

            this.Controls.Add(button2);
            this.Controls.Add(button3);
            this.Controls.Add(button4);
            this.Controls.Add(button5);
            this.Controls.Add(button6);

            this.Controls.Remove(button1);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            foreach (Button button in buttons)
            {
                button.Left = random.Next(0, this.ClientSize.Width - button.Width);
                button.Top = random.Next(0, this.ClientSize.Width - button.Width);
            }
        }
    }
}
