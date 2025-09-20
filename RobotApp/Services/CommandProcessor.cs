using Microsoft.Extensions.Logging;
using RobotApp.Interfaces;
using RobotApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RobotApp.Services
{
    public class CommandProcessor : ICommandProcessor
    {
        private readonly IRobot _robot;
        private readonly ILogger<CommandProcessor> _logger;

        public CommandProcessor(IRobot robot, ILogger<CommandProcessor> logger)
        {
            _robot = robot;
            _logger = logger;
        }

        public async Task ProcessCommandsAsync(string filePath, CancellationToken cancellationToken)
        {
            try
            {
                var lines = await File.ReadAllLinesAsync(filePath, cancellationToken);

                foreach (var rawCommand in lines)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    var line = rawCommand.Trim().ToUpper();

                    if (line.StartsWith("PLACE"))
                    {
                        var match = Regex.Match(line, @"PLACE\s+(\d+),(\d+),(\w+)");
                        if (match.Success && Enum.TryParse(match.Groups[3].Value, out Direction facing))
                        {
                            int x = int.Parse(match.Groups[1].Value);
                            int y = int.Parse(match.Groups[2].Value);
                            _robot.Place(x, y, facing);
                        }
                        else
                        {
                            _logger.LogWarning("Invalid PLACE command format: {Command}", line);
                        }
                    }
                    else if (!_robot.IsPlaced)
                    {
                        continue;
                    }
                    else if (line == "MOVE") _robot.Move();
                    else if (line == "LEFT") _robot.Left();
                    else if (line == "RIGHT") _robot.Right();
                    else if (line == "REPORT") Console.WriteLine(_robot.Report());
                    else
                    {
                        _logger.LogWarning("Unknown command: {Command}", line);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Command processing was cancelled.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing commands from {FilePath}", filePath);
                throw;
            }
        }
    }
}
