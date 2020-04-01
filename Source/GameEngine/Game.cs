using System;

namespace GameEngine
{
    public class Game
    {

        public int RollDice()
        {
            Random rnd = new Random();
            int dice = rnd.Next(1,7);
            return dice;
        }


    }
}
