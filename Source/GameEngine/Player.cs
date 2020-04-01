using System;
using System.Collections.Generic;
using System.Text;

namespace LudoGameEngine
{
    public class Player
    {
        public Color Color { get; set; }
        public int Score { get; set; }

        public List<GamePiece> GamePieces = new List<GamePiece>();

        public Player(Color _color)
        {
            Color = _color;
            Score = 0;
            GamePieces = GenerateGamePieces();
        }

        public List<GamePiece> GenerateGamePieces()
        {
            for (int i = 1; i < 5; i++)
            {
                GamePieces.Add(new GamePiece{ GamePieceID = i, IsSelected = false, PositionID = 0 });
            }

            return GamePieces;

        }
    }
}
