using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotApp.Model
{
    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Direction Facing { get; set; }
        public override string ToString() => $"{X},{Y},{Facing}";
    }
}
