using System;
using System.Drawing;
using System.Windows.Forms;

namespace EventsTestApp
{
    public partial class EventsForm : Form
    {
        bool isDragging = false;
        Point dragStartPoint;
        int currentFontSize = 0;

        public EventsForm()
        {
            InitializeComponent();
        }

        // Ctrl+Q to close form
        private void EventsForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Q)
            {
                this.Close();
            }
        }

        // Letter filtering for TextBox
        private void numbersOnlyTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
                e.Handled = true;
            else
                e.Handled = false;
        }

        // Font size increase on button click
        private void increaseFontButton_MouseDown(object sender, MouseEventArgs e)
        {
            currentFontSize = Convert.ToInt32(Label.Font.Size);
            currentFontSize += 5;
            Label.Font = new Font("Arial", currentFontSize);
        }

        // Change labeb color with buttons
        private void changeBackgroundButton_MouseMove(object sender, MouseEventArgs e)
        {
            Label.BackColor = Color.Blue;
        }

        private void changeTextColorButton_MouseHover(object sender, EventArgs e)
        {
            Label.ForeColor = Color.Yellow;
        }

        private void changeTextColorButton_MouseLeave(object sender, EventArgs e)
        {
            Label.ForeColor = Color.Black;
        }

        // Random button movement
        private void randomMoveButton_MouseMove(object sender, MouseEventArgs e)
        {
            Random random = new Random();
            randomMoveButton.Left = random.Next(0, 100);
            randomMoveButton.Top = random.Next(0, 100);
        }

        private void randomMoveButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("BUTTON CLICKED");
        }

        // Block 5: Draggable button
        private void draggableButton_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            dragStartPoint = e.Location;
        }

        private void draggableButton_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                draggableButton.Left += e.X - dragStartPoint.X;
                draggableButton.Top += e.Y - dragStartPoint.Y;
            }
        }

        private void draggableButton_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }
    }
}
