namespace MoveButton
{
    public partial class MoveButton : Form
    {
        public MoveButton()
        {
            InitializeComponent();
        }

        bool isDragging = false;
        Point initialPoint;

        private void button_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            initialPoint = e.Location;
        }

        private void button_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                button.Left = e.X + button.Left - initialPoint.X;
                button.Top = e.Y + button.Top - initialPoint.Y;
            }
        }

        private void button_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }
    }
}
