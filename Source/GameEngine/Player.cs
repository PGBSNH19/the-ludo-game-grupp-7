using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LudoGameEngine
{
    public class Player
    {
        public int PlayerID { get; set; }
        public Color Color { get; }
        public string Name { get; }
        public int Score { get; set; }
        public int GameID { get; set; }
        public List<GamePiece> GamePieces = new List<GamePiece>();
        
        [NotMapped]
        public int StartingDice { get; set; }

        public Player(Color color, string name)
        {
            Color = color;
            Name = name;
            Score = 0;
            GamePieces.Add(new GamePiece() { GamePieceID = 1, positionType = PositionType.StartingPosition });
            GamePieces.Add(new GamePiece() { GamePieceID = 2, positionType = PositionType.StartingPosition });
            GamePieces.Add(new GamePiece() { GamePieceID = 3, positionType = PositionType.StartingPosition });
            GamePieces.Add(new GamePiece() { GamePieceID = 4, positionType = PositionType.StartingPosition });
        }
        public Player()
        {

        }
    }
}
