using System;
using System.Collections.Generic;

namespace LudoGameEngine
{
    public class Game
    {
        public int GameID { get; set; }
        public string? WinnerPlayerName { get; set; }
        public bool HasFinished { get; set; }

        public List<Player> Players { get; set; }

        public Game()
        {
            HasFinished = false;
            Players = new List<Player>();
        }
    }
}
