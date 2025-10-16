using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GenerateButton
{
    public partial class GenerateButton : Form
    {
        public GenerateButton()
        {
            InitializeComponent();
            timer1.Interval = 2000;
            random = new Random();
            timer1.Start();
        }

        private Button button2, button3, button4, button5, button6, button7, button8, button9, button10, button11;
        private List<Button> buttons;
        private Random random;
        int number = 0;
        bool go = true;

        private void endBtn_Click(object sender, EventArgs e)
        {
            timer1.Stop();

            for (int i = 0; i < numberLb.Items.Count - 1; i++)
            {
                int current = int.Parse(numberLb.Items[i].ToString());
                int next = int.Parse(numberLb.Items[i + 1].ToString());

                if (current > next || current % 2 == 1)
                {
                    go = false;
                    break;
                }
            }

            if (go)
            {
                MessageBox.Show("Success");
            }
            else
            {
                MessageBox.Show("Failure");
            }
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            startBtn.Enabled = false;

            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            button7 = new Button();
            button8 = new Button();
            button9 = new Button();
            button10 = new Button();
            button11 = new Button();

            buttons = new List<Button> { button2, button3, button4, button5, button6, button7, button8, button9, button10, button11 };

            foreach (Button button in buttons)
            {
                button.Click += Button_Click;
                buttonPnl.Controls.Add(button);
            }
        }

        private void GenerateButton_Load(object sender, EventArgs e)
        {
            this.ClientSize = new Size(1000, 500);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (Button button in buttons)
            {
                number = random.Next(0, 100);
                button.Text = number.ToString();
                button.Size = new Size(75, 30);
                button.BackColor = Color.AliceBlue;
                button.Left = random.Next(0, buttonPnl.ClientSize.Width - button.Width);
                button.Top = random.Next(0, buttonPnl.ClientSize.Height - button.Height);
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                numberLb.Items.Add(clickedButton.Text);
                buttonPnl.Controls.Remove(clickedButton);
                buttons.Remove(clickedButton);
            }
        }
    }
}
