using System;
using System.Collections.Generic;

namespace LudoGameEngine
{
    public class Game
    {
        public int GameID { get; set; }
        public string? WinnerPlayerName { get; set; }
        public DateTime EndTimeOfGame { get; set; }
        public int? NextPlayerID { get; set; }

        public List<Player> Players = new List<Player>();
    }
}
