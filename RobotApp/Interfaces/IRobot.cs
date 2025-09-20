using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RobotApp.Model;

namespace RobotApp.Interfaces
{
    public interface IRobot
    {
        Position Position { get; }
        void Place(int x, int y, Direction facing);
        void Move();
        void Left();
        void Right();
        string Report();
        bool IsPlaced { get; }
    }
}
