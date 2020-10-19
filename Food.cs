namespace Snake
{
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
}
