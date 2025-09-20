using RobotApp.Interfaces;
using RobotApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotApp.Services
{
    public class Robot : IRobot
    {
        private readonly ISurface _surface;
        private Position _position;
        public Position Position => _position;
        public bool IsPlaced { get; private set; }

        public Robot(ISurface table)
        {
            _surface = table;
        }

        public void Place(int x, int y, Direction facing)
        {
            if (_surface.IsValidPosition(x, y))
            {
                _position = new Position { X = x, Y = y, Facing = facing };
                IsPlaced = true;
            }
        }

        public void Move()
        {
            if (!IsPlaced) return;

            int x = _position.X;
            int y = _position.Y;

            switch (_position.Facing)
            {
                case Direction.NORTH: y++; break;
                case Direction.SOUTH: y--; break;
                case Direction.EAST: x++; break;
                case Direction.WEST: x--; break;
            }

            if (_surface.IsValidPosition(x, y))
            {
                _position.X = x;
                _position.Y = y;
            }
        }

        public void Left()
        {
            if (!IsPlaced) return;

            _position.Facing = (Direction)(((int)_position.Facing + 3) % 4);
        }

        public void Right()
        {
            if (!IsPlaced) return;

            _position.Facing = (Direction)(((int)_position.Facing + 1) % 4);
        }

        public string Report()
        {
            return IsPlaced ? _position.ToString() : "Robot not yet placed.";
        }
    }
}
