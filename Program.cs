using System;

namespace Snake
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.WindowHeight = 31;
            Console.WindowWidth = 31;
            Console.BufferHeight = 31;
            Console.BufferWidth = 31;

            Menu menu = new Menu();
            menu.GetLogo();

            GameManager gameManager = new GameManager();

            while (true)
            {
                gameManager.Execute();
            }
        }
    }
}
