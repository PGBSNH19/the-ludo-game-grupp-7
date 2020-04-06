using System;

namespace LudoGameEngine
{
    public class Game
    {
        public bool IsFinished { get; set; }
        public int GameID { get; set; }
        public Player Winner { get; set; }
        public DateTime TimeOfGame { get; set; }


        public int RollDice()
        {
            Random rnd = new Random();
            int dice = rnd.Next(1,7);
            return dice;
        }


    }
}
