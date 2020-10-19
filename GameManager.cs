using System;
using System.Threading;

namespace Snake
{
    public class GameManager
    {
        public Player player;
        public Food food;
        public Board gameBoard;

        public bool enableWalls;

        public GameManager()
        {
            Restart();
        }

        public void Restart()
        {
            Console.Clear();

            Random rnd = new Random();
            enableWalls = rnd.Next(0, 2) == 1;

            gameBoard = new Board(30, 30);
            if (enableWalls) { gameBoard.DrawBoard(); }

            player = new Player(this);
            SpawnFood();
        }

        public void Execute()
        {
            player.ReadInput();
            player.Move();
            Thread.Sleep(75);
        }

        public void SpawnFood()
        {
            food = new Food(this);
        }
    }
}
