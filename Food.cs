using System;

namespace SnakeGame
{
    public class Food
    {
        public Point Position { get; set; }
        private static Random random = new Random();

        public Food(Point startPosition)
        {
            Position = startPosition;
        }

        public void Spawn()
        {
            Position = new Point(random.Next(0, 20), random.Next(0, 20));
        }
    }
}