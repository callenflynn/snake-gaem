using System;

namespace SnakeGame
{
    public class Game
    {
        private bool isRunning;
        private Snake snake;
        private Food food;

        public Game()
        {
            snake = new Snake(new Point(5, 5)); // Provide a starting position
            food = new Food(new Point(10, 10)); // Provide a starting position
        }

        public void Start()
        {
            isRunning = true;
            while (isRunning)
            {
                Update();
                Render();
            }
        }

        private void Update()
        {
            snake.Move();
            if (snake.CheckCollision(food.Position))
            {
                snake.Grow();
                food.Spawn();
            }
        }

        private void Render()
        {
            Console.Clear();
            var snakeHead = snake.Head;
            Console.WriteLine("Snake Position: " + snake.Head); 
            Console.WriteLine("Food Position: " + food.Position);
        }

        public void Stop()
        {
            isRunning = false;
        }
    }
}