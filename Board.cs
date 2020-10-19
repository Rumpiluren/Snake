using System;

namespace Snake
{
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
}
