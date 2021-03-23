using System;
using System.Threading;
using System.Collections.Generic;

namespace Snake
{
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
                        Environment.Exit(0);
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
}
