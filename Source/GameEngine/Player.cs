﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LudoGameEngine
{
    public class Player
    {
        public Color Color { get; }
        public string Name { get; }
        public int Turn = 0;
        public int Score { get; set; }

        public int StartingDice { get; set; }

        public List<GamePiece> GamePieces = new List<GamePiece>();

        public Player(Color color, string name)
        {
            Color = color;
            Name = name;
            Score = 0;
            GamePieces.Add(new GamePiece() { position = new Position() { BoardPosition = 0, positionType = PositionType.StartingPosition } });
            GamePieces.Add(new GamePiece() { position = new Position() { BoardPosition = 0, positionType = PositionType.StartingPosition } });
            GamePieces.Add(new GamePiece() { position = new Position() { BoardPosition = 0, positionType = PositionType.StartingPosition } });
            GamePieces.Add(new GamePiece() { position = new Position() { BoardPosition = 0, positionType = PositionType.StartingPosition } });
        }
    }
}
