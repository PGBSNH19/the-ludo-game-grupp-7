using System;
using System.Collections.Generic;
using System.Text;

namespace LudoGameEngine
{
    class Position
    {
        public PositionType positionType;
        public int GamePieceID { get; set; }
        public int StartingZoneID { get; set; }

        public int BoardPositionID { get; set; }


    }
}
