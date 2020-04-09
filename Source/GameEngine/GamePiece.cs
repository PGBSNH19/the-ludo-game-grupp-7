using System;
using System.Collections.Generic;
using System.Text;

namespace LudoGameEngine
{
    public class GamePiece
    {
        public int GamePieceID { get; set; }
        public int StepCounter { get; set; }
        public int BoardPosition { get; set; }
        public PositionType positionType { get; set; }
        public int PlayerID { get; set; }
        public Player Player { get; set; }
    }
}
