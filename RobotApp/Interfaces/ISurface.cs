using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotApp.Interfaces
{
    public interface ISurface
    {
        bool IsValidPosition(int x, int y);
    }
}
