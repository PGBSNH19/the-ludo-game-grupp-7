using System;
using System.Collections.Generic;
using System.Text;

namespace LudoGameEngine
{
    public class GameEngine
    {
        private int turn = 0;
        private List<Player> players = new List<Player>();

        public void AddPlayer(string name)
        {
            if (players.Count == 0)
                players.Add(new Player(Color.red, name));

            else if (players.Count == 1)
                players.Add(new Player(Color.blue, name));

            else if (players.Count == 2)
                players.Add(new Player(Color.yellow, name));

            else if (players.Count == 3)
                players.Add(new Player(Color.green, name));
        }
        public int RollDice()
        {
            Random rnd = new Random();
            return rnd.Next(1, 7);
        }

        public GamePiece MoveGamePiece(GamePiece gamePiece)
        {
            var dice = RollDice();

            gamePiece.Position.BoardPosition = gamePiece.Position.BoardPosition + dice;

            return gamePiece;
        }

        public void StepCounter()
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
