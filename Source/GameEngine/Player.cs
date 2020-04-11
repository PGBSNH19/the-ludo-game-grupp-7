using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LudoGameEngine
{
    public class Player
    {
        public int ID { get; set; }
        public Color Color { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public bool MyTurn { get; set; }
        public int GameID { get; set; }
        public Game Game { get; set; }
        public List<GamePiece> GamePieces { get; set; }
        
        [NotMapped]
        public int StartingDice { get; set; }

        public Player(Color color, string name)
        {
            Color = color;
            Name = name;
            Score = 0;
            GamePieces = new List<GamePiece>();
            GamePieces.Add(new GamePiece() { PlayerGamePiece = 1, PositionType = PositionType.StartingPosition });
            GamePieces.Add(new GamePiece() { PlayerGamePiece = 2, PositionType = PositionType.StartingPosition });
            GamePieces.Add(new GamePiece() { PlayerGamePiece = 3, PositionType = PositionType.StartingPosition });
            GamePieces.Add(new GamePiece() { PlayerGamePiece = 4, PositionType = PositionType.StartingPosition });
        }
        public Player()
        {
            GamePieces = new List<GamePiece>();
        }
    }
}
