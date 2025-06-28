using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SnakeGame
{
    public partial class Form1 : Form
    {
        private Timer timer = new Timer();
        private List<Point> snake = new List<Point>();
        private Point direction = new Point(1, 0);
        private Point food;
        private int gridSize = 20;
        private int cellSize;
        private Random rand = new Random();
        private int score = 0;
        private bool started = false;

        public Form1()
        {
            InitializeComponent();
            this.Text = "Cal's Snake Gaem";
            this.DoubleBuffered = true;
            this.ClientSize = new Size(400, 400);
            UpdateCellSize();
            StartGame();

            timer.Interval = 100;
            timer.Tick += Timer_Tick;

            this.KeyDown += Form1_KeyDown;
            this.ClientSizeChanged += Form1_ClientSizeChanged;
        }

        private void Form1_ClientSizeChanged(object sender, EventArgs e)
        {
            UpdateCellSize();
            Invalidate();
        }

        private void UpdateCellSize()
        {
            cellSize = Math.Min(ClientSize.Width, ClientSize.Height) / gridSize;
        }

        private void StartGame()
        {
            snake.Clear();
            snake.Add(new Point(gridSize / 2, gridSize / 2));
            direction = new Point(1, 0);
            score = 0;
            started = false;
            timer.Stop();
            SpawnFood();
            Invalidate();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            MoveSnake();
            Invalidate();
        }

        private void MoveSnake()
        {
            Point newHead = new Point(snake[0].X + direction.X, snake[0].Y + direction.Y);

            if (newHead.X < 0 || newHead.Y < 0 || newHead.X >= gridSize || newHead.Y >= gridSize || snake.Contains(newHead))
            {
                StartGame();
                return;
            }

            snake.Insert(0, newHead);

            if (newHead.Equals(food))
            {
                score++;
                if (snake.Count >= gridSize * gridSize * 0.9)
                {
                    timer.Stop();
                    MessageBox.Show("You win!");
                    StartGame();
                    return;
                }
                SpawnFood();
            }
            else
            {
                snake.RemoveAt(snake.Count - 1);
            }
        }

        private void SpawnFood()
        {
            do
            {
                food = new Point(rand.Next(0, gridSize), rand.Next(0, gridSize));
            } while (snake.Contains(food));
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!started)
            {
                if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
                {
                    started = true;
                    timer.Start();
                }
            }

            if (e.KeyCode == Keys.Up && direction.Y != 1) direction = new Point(0, -1);
            else if (e.KeyCode == Keys.Down && direction.Y != -1) direction = new Point(0, 1);
            else if (e.KeyCode == Keys.Left && direction.X != 1) direction = new Point(-1, 0);
            else if (e.KeyCode == Keys.Right && direction.X != -1) direction = new Point(1, 0);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.Clear(Color.White);

            using (Pen borderPen = new Pen(Color.Black, 3))
            {
                e.Graphics.DrawRectangle(
                    borderPen,
                    0,
                    0,
                    gridSize * cellSize,
                    gridSize * cellSize
                );
            }

            foreach (var p in snake)
            {
                e.Graphics.FillRectangle(Brushes.Green, p.X * cellSize, p.Y * cellSize, cellSize, cellSize);
            }

            e.Graphics.FillRectangle(Brushes.Red, food.X * cellSize, food.Y * cellSize, cellSize, cellSize);
        }
    }
}