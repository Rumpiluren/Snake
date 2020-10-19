using System;
using System.Threading;

namespace Snake
{
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
}
