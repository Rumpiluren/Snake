using System;
using System.Threading;
using System.Collections.Generic;

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

    public class Menu
    {
        public void GetLogo()
        {
            int logoHeight = 12;
            int logoWidth = 25;
            bool[,] logoArray = new bool[logoWidth, logoHeight];

            ////////
            logoArray[3, 0] = true;
            logoArray[4, 0] = true;
            logoArray[5, 0] = true;
            logoArray[7, 0] = true;
            logoArray[10, 0] = true;
            logoArray[13, 0] = true;
            logoArray[16, 0] = true;
            logoArray[19, 0] = true;
            logoArray[21, 0] = true;
            logoArray[22, 0] = true;
            logoArray[23, 0] = true;

            logoArray[3, 1] = true;
            logoArray[7, 1] = true;
            logoArray[8, 1] = true;
            logoArray[10, 1] = true;
            logoArray[12, 1] = true;
            logoArray[14, 1] = true;
            logoArray[16, 1] = true;
            logoArray[18, 1] = true;
            logoArray[21, 1] = true;

            logoArray[3, 2] = true;
            logoArray[4, 2] = true;
            logoArray[5, 2] = true;
            logoArray[7, 2] = true;
            logoArray[9, 2] = true;
            logoArray[10, 2] = true;
            logoArray[12, 2] = true;
            logoArray[13, 2] = true;
            logoArray[14, 2] = true;
            logoArray[16, 2] = true;
            logoArray[17, 2] = true;
            logoArray[21, 2] = true;
            logoArray[22, 2] = true;

            logoArray[5, 3] = true;
            logoArray[7, 3] = true;
            logoArray[10, 3] = true;
            logoArray[12, 3] = true;
            logoArray[14, 3] = true;
            logoArray[16, 3] = true;
            logoArray[18, 3] = true;
            logoArray[21, 3] = true;

            logoArray[3, 4] = true;
            logoArray[4, 4] = true;
            logoArray[5, 4] = true;
            logoArray[7, 4] = true;
            logoArray[10, 4] = true;
            logoArray[12, 4] = true;
            logoArray[14, 4] = true;
            logoArray[16, 4] = true;
            logoArray[19, 4] = true;
            logoArray[21, 4] = true;
            logoArray[22, 4] = true;
            logoArray[23, 4] = true;

            ////////



            for (int x = 0; x < logoWidth; x++)
            {
                for (int y = 0; y < logoHeight; y++)
                {
                    if (logoArray[x, y])
                    {
                        Console.SetCursorPosition(x + 2, y + 10);
                        Console.Write("█");
                    }
                }
            }

            do
            {
                AnimatedMenu();
            } while (Console.KeyAvailable != true);
        }

        public void AnimatedMenu()
        {
            Thread.Sleep(500);

            Console.SetCursorPosition(8, 20);
            Console.Write("PRESS ANY BUTTON");

            Thread.Sleep(500);

            Console.SetCursorPosition(8, 20);
            Console.Write("                                   ");
        }
    }

    public class Entity
    {
        public Coordinates position;
        Random rnd = new Random();
        protected GameManager owner;

        public void Draw(char symbol, Coordinates drawPosition)
        {
            Console.SetCursorPosition(drawPosition.x, drawPosition.y);
            Console.Write($"{symbol}\b");
            Console.SetCursorPosition(1, 1);
        }

        protected void GetRandomPosition()
        {
            bool isOverlapping;
            do
            {
                isOverlapping = false;

                position.x = rnd.Next(1, owner.gameBoard.size.x - 1);
                position.y = rnd.Next(1, owner.gameBoard.size.y - 1);

                if (owner.player != null)
                {
                    if (owner.player.position.y == position.y && owner.player.position.x == position.x)
                    {
                        isOverlapping = true;
                    }
                    foreach (var item in owner.player.tail)
                    {
                        if (item.y == position.y && item.x == position.x)
                        {
                            isOverlapping = true;
                        }
                    }
                }

            } while (isOverlapping);
        }
    }

    public class Player : Entity
    {
        public List<Coordinates> tail;
        public Coordinates heading;

        public Player(GameManager newOwner)
        {
            owner = newOwner;
            GetRandomPosition();
            tail?.Clear();
            tail = new List<Coordinates>();
            heading = new Coordinates();
        }

        public void ReadInput()
        {
            //This here is where we end up after each button press.
            //We read the user input,
            //and if the input equals any of the arrow keys, we store the movement.
            if (Console.KeyAvailable)
            {
                var input = Console.ReadKey(false).Key;
                switch (input)
                {
                    case ConsoleKey.UpArrow:
                        if (heading.y != 1)
                        {
                            heading.y = -1;
                            heading.x = 0;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (heading.y != -1)
                        {
                            heading.y = 1;
                            heading.x = 0;
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (heading.x != 1)
                        {
                            heading.x = -1;
                            heading.y = 0;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (heading.x != -1)
                        {
                            heading.x = 1;
                            heading.y = 0;
                        }
                        break;
                    case ConsoleKey.Escape:
                        //Environment.Exit(0);
                        Eat();
                        break;
                }
            }
        }

        public void Move()
        {
            //Draw(' ', position);
            //foreach (var item in tail)
            //{
            //    Draw(' ', item);
            //}

            if (tail.Count > 0)
            {
                Draw(' ', tail[tail.Count - 1]);
            }
            else
            {
                Draw(' ', position);
            }

            for (int i = tail.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    tail[0] = position;
                }
                else if (i > 0)
                {
                    tail[i] = tail[i - 1];
                }
            }

            if (owner.enableWalls != true)
            {
                if (position.x + heading.x < 0)
                {
                    position.x = owner.gameBoard.size.x;
                }
                else if (position.x + heading.x > owner.gameBoard.size.x)
                {
                    position.x = 0;
                }

                if (position.y + heading.y < 0)
                {
                    position.y = owner.gameBoard.size.y;
                }
                else if (position.y + heading.y > owner.gameBoard.size.y)
                {
                    position.y = 0;
                }
            }

            position.x = position.x + heading.x;
            position.y = position.y + heading.y;

            Draw('█', position);
            foreach (var item in tail)
            {
                Draw('█', item);
            }

            CheckCollision();
        }

        public void Eat()
        {
            tail.Add(new Coordinates(position.x, position.y));
            owner.SpawnFood();
        }

        void CheckCollision()
        {
            foreach (var item in tail)
            {
                if (item.x == position.x && item.y == position.y)
                {
                    Death();
                }
            }

            if (position.x == owner.food.position.x && position.y == owner.food.position.y)
            {
                Eat();
            }

            if (owner.enableWalls)
            {
                if (position.x <= 0 || position.x >= owner.gameBoard.size.x - 1 || position.y <= 0 || position.y >= owner.gameBoard.size.y - 1)
                {
                    Death();
                }
            }
        }

        void Death()
        {
            for (int i = 0; i < 4; i++)
            {
                Thread.Sleep(100);

                Draw(' ', position);
                foreach (var item in tail)
                {
                    Draw(' ', item);
                }

                Thread.Sleep(100);

                Draw('█', position);
                foreach (var item in tail)
                {
                    Draw('█', item);
                }
            }
            owner.Restart();
        }
    }

    public class Food : Entity
    {
        char symbol = '@';

        public Food(GameManager newOwner)
        {
            owner = newOwner;
            GetRandomPosition();
            Draw(symbol, position);
        }
    }

    public class Board
    {
        public Coordinates size;
        public Board(int sizeX, int sizeY)
        {
            size.x = sizeX;
            size.y = sizeY;
        }

        public void DrawBoard()
        {
            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    if (i == 0 || j == 0 || i == size.x - 1 || j == size.y - 1)
                    {
                        //This loop ends up here if we are at the edges of the map.
                        //Every position out here is a wall, so we set the value at this position in the array to true.
                        Console.SetCursorPosition(i, j);
                        Console.Write("█");
                    }
                    else
                    {
                        Console.SetCursorPosition(i, j);
                        Console.Write(" ");
                    }
                }
            }
        }
    }

    public struct Coordinates
    {
        public int x;
        public int y;

        public Coordinates(int positionX, int positionY)
        {
            x = positionX;
            y = positionY;
        }
    }
}
