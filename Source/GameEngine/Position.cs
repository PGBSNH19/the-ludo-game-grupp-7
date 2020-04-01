using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine
{
    class Position
    {
        public PositionType positionType;
        public int GamePieceID { get; set; }
        public int StartingZoneID { get; set; }
    }
}
