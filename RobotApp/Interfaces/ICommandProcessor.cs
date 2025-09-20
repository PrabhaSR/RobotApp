using RobotApp.Model;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;

namespace RobotApp.Interfaces
{
    public interface ICommandProcessor
    {
        Task ProcessCommandsAsync(string filePath, CancellationToken cancellationToken);
    }
}
