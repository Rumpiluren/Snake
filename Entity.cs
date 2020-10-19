using System;

namespace Snake
{
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
}
