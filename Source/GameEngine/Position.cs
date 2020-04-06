using System;
using System.Collections.Generic;
using System.Text;

namespace LudoGameEngine
{
    public class Position
    {
        public PositionType positionType;
        public int BoardPosition { get; set; }
        public bool Occupied { get; set; }
    }
}
