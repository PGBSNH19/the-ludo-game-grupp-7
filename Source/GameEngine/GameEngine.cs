using System;
using System.Collections.Generic;
using System.Text;

namespace LudoGameEngine
{
    public class GameEngine
    {
        public int PlayerScore { get; set; }
        public int Dice { get; set; }



        public void MoveGamePiece()
        {
        }

        public void ChangeTurn()
        {

        }

        public void ChooseStaringPlayer()
        {

        }

        public GamePiece SelectGamePiece()
        {

            Console.WriteLine("Choose the game piece you want to move: ");
            Console.ReadLine();
            return null;
        }
    }
}
