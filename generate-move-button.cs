using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GenerateMoveButtonApp
{
    public partial class GenerateMoveButton : Form
    {
        private class ButtonData
        {
            public Button Button { get; set; }
            public Point TargetCorner { get; set; }
            public Point StartCenter { get; set; }
        }

        private readonly List<ButtonData> buttonList = new List<ButtonData>();
        private Random random = new Random();
        private bool shouldAnimate = true;

        public GenerateMoveButton()
        {
            InitializeComponent();
            this.Load += new EventHandler(GenerateMoveButton_Load);
        }

        private void GenerateMoveButton_Load(object sender, EventArgs e)
        {
            this.SizeChanged += new EventHandler(GenerateMoveButton_SizeChanged);
            this.Text = "Button Animation (with Random Sizes)";
            this.DoubleBuffered = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreateAndPositionButtons();
            StartAnimationTask();
            this.Controls.Remove(button1);
        }

        private void CreateAndPositionButtons()
        {
            Point formCenter = new Point(this.ClientSize.Width / 2, this.ClientSize.Height / 2);
            Point centerStart = new Point(formCenter.X - 50 / 2, formCenter.Y - 50 / 2);

            Point[] targetCorners = new Point[]
            {
                new Point(10, 10),
                new Point(this.ClientSize.Width - 50 - 10, 10),
                new Point(10, this.ClientSize.Height - 50 - 10),
                new Point(this.ClientSize.Width - 50 - 10, this.ClientSize.Height - 50 - 10)
            };

            for (int i = 0; i < 5; i++)
            {
                Button btn = new Button
                {
                    Text = $"Button {i + 1}",
                    Size = new Size(random.Next(50, 101), random.Next(50, 101)),
                    Location = centerStart
                };
                this.Controls.Add(btn);

                buttonList.Add(new ButtonData
                {
                    Button = btn,
                    TargetCorner = targetCorners[i % 4],
                    StartCenter = centerStart
                });
            }
        }

        private void GenerateMoveButton_SizeChanged(object sender, EventArgs e)
        {
            Point formCenter = new Point(this.ClientSize.Width / 2, this.ClientSize.Height / 2);
            Point centerStart = new Point(formCenter.X - 50 / 2, formCenter.Y - 50 / 2);

            if (buttonList.Count == 5)
            {
                buttonList[0].TargetCorner = new Point(10, 10);
                buttonList[1].TargetCorner = new Point(this.ClientSize.Width - 50 - 10, 10);
                buttonList[2].TargetCorner = new Point(10, this.ClientSize.Height - 50 - 10);
                buttonList[3].TargetCorner = new Point(this.ClientSize.Width - 50 - 10, this.ClientSize.Height - 50 - 10);
            }

            foreach (var data in buttonList)
            {
                data.StartCenter = centerStart;
            }
        }

        private void StartAnimationTask()
        {
            this.FormClosing += (sender, e) => { shouldAnimate = false; };

            Task.Run(async () =>
            {
                float speed = 0.06f;
                float t = 0;
                bool movingToCorner = true;

                while (shouldAnimate)
                {
                    t += speed;
                    if (t > 1.0f)
                    {
                        t = 0;
                        movingToCorner = !movingToCorner;
                        await Task.Delay(500);
                    }

                    this.Invoke((MethodInvoker)delegate
                    {
                        UpdateButtonPositions(t, movingToCorner);
                    });

                    await Task.Delay(16);
                }
            });
        }

        private void UpdateButtonPositions(float t, bool toCorner)
        {
            foreach (var data in buttonList)
            {
                Point startPoint, endPoint;

                if (toCorner)
                {
                    startPoint = data.StartCenter;
                    endPoint = data.TargetCorner;
                }
                else
                {
                    startPoint = data.TargetCorner;
                    endPoint = data.StartCenter;
                }

                int newX = (int)(startPoint.X + (endPoint.X - startPoint.X) * t);
                int newY = (int)(startPoint.Y + (endPoint.Y - startPoint.Y) * t);

                data.Button.Location = new Point(newX, newY);
            }
        }
    }
}
