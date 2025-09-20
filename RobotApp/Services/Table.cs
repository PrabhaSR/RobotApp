using RobotApp.Interfaces;

namespace RobotApp.Services
{
    public class Table : ISurface
    {
        private readonly int _width;
        private readonly int _height;

        public Table(int width = 5, int height = 5)
        {
            _width = width;
            _height = height;
        }

        public bool IsValidPosition(int x, int y)
        {
            return x >= 0 && x < _width && y >= 0 && y < _height;
        }
    }
}

