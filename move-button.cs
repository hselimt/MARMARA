using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MoveButtonApp
{
    public partial class MoveButton : Form
    {
        // A nested class to hold the buttons and their target positions
        private class ButtonData
        {
            public Button Button { get; set; }
            public Point TargetCorner { get; set; }
            public Point StartCenter { get; set; }
        }

        private readonly List<ButtonData> buttonList = new List<ButtonData>();
        private const int ButtonSize = 75;
        private bool shouldAnimate = true;

        public MoveButton()
        {
            InitializeComponent();
            // We bind the method that will run when the form is loaded here.
            this.Load += new EventHandler(MoveButton_Load);
        }

        private void MoveButton_Load(object sender, EventArgs e)
        {
            this.SizeChanged += new EventHandler(MoveButton_SizeChanged);
            this.Text = "Button Animation (with Task)";
            this.DoubleBuffered = true; // Prevents flickering for smooth movement

            // Create and position the buttons
            CreateAndPositionButtons();

            // Start the animation
            StartAnimationTask();
        }

        private void CreateAndPositionButtons()
        {
            // Calculate the center of the form
            Point formCenter = new Point(this.ClientSize.Width / 2, this.ClientSize.Height / 2);

            // The starting position of the buttons (from the center)
            Point centerStart = new Point(formCenter.X - ButtonSize / 2, formCenter.Y - ButtonSize / 2);

            // 4 Corner Targets: (TopLeft, TopRight, BottomLeft, BottomRight)
            Point[] targetCorners = new Point[]
            {
                new Point(10, 10), // Top Left
                new Point(this.ClientSize.Width - ButtonSize - 10, 10), // Top Right
                new Point(10, this.ClientSize.Height - ButtonSize - 10), // Bottom Left
                new Point(this.ClientSize.Width - ButtonSize - 10, this.ClientSize.Height - ButtonSize - 10) // Bottom Right
            };

            for (int i = 0; i < 4; i++)
            {
                Button btn = new Button
                {
                    Text = $"Button {i + 1}",
                    Size = new Size(ButtonSize, ButtonSize),
                    Location = centerStart // Initially, all are at the center
                };
                this.Controls.Add(btn);

                buttonList.Add(new ButtonData
                {
                    Button = btn,
                    TargetCorner = targetCorners[i],
                    StartCenter = centerStart
                });
            }
        }

        // If the form size changes, recalculate the button targets
        private void MoveButton_SizeChanged(object sender, EventArgs e)
        {
            Point formCenter = new Point(this.ClientSize.Width / 2, this.ClientSize.Height / 2);
            Point centerStart = new Point(formCenter.X - ButtonSize / 2, formCenter.Y - ButtonSize / 2);

            // New Corner Targets
            if (buttonList.Count == 4)
            {
                buttonList[0].TargetCorner = new Point(10, 10); // Top Left
                buttonList[1].TargetCorner = new Point(this.ClientSize.Width - ButtonSize - 10, 10); // Top Right
                buttonList[2].TargetCorner = new Point(10, this.ClientSize.Height - ButtonSize - 10); // Bottom Left
                buttonList[3].TargetCorner = new Point(this.ClientSize.Width - ButtonSize - 10, this.ClientSize.Height - ButtonSize - 10); // Bottom Right
            }

            // Also, update the starting center position
            foreach (var data in buttonList)
            {
                data.StartCenter = centerStart;
            }
        }

        private void StartAnimationTask()
        {
            // Add an event to stop the animation when the form is closing
            this.FormClosing += (sender, e) => { shouldAnimate = false; };

            // Start a Task to run the animation in the background
            Task.Run(async () =>
            {
                float speed = 0.06f; // Movement speed (0.01 is slow, 0.1 is fast)
                float t = 0; // Animation progress (between 0.0 and 1.0)
                bool movingToCorner = true; // Is it moving to the corner or the center?

                // Main animation loop
                while (shouldAnimate)
                {
                    t += speed;

                    // If the animation time is up (reached 1 from 0)
                    if (t > 1.0f)
                    {
                        t = 0; // Reset
                        movingToCorner = !movingToCorner; // Change target (Corner <-> Center)
                        await Task.Delay(500); // Wait for half a second before changing direction
                    }

                    // Use Invoke for UI updates (safe access to UI from a different thread)
                    this.Invoke((MethodInvoker)delegate
                    {
                        UpdateButtonPositions(t, movingToCorner);
                    });

                    // Slow down the loop (simulating approximately 60 FPS)
                    await Task.Delay(16);
                }
            });
        }

        private void UpdateButtonPositions(float t, bool toCorner)
        {
            // Calculate the new position for each button
            foreach (var data in buttonList)
            {
                Point startPoint, endPoint;

                if (toCorner)
                {
                    // Movement from Center to Corner
                    startPoint = data.StartCenter;
                    endPoint = data.TargetCorner;
                }
                else
                {
                    // Movement from Corner to Center
                    startPoint = data.TargetCorner;
                    endPoint = data.StartCenter;
                }

                // Position calculation using the *Linear Interpolation (Lerp)* formula
                int newX = (int)(startPoint.X + (endPoint.X - startPoint.X) * t);
                int newY = (int)(startPoint.Y + (endPoint.Y - startPoint.Y) * t);

                data.Button.Location = new Point(newX, newY);
            }
        }
    }
}
