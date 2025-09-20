using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RobotApp.Interfaces;
using RobotApp.Services;

namespace ToyRobotSimulator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
            .AddLogging(config =>
            {
                config.ClearProviders();
             })
            .AddSingleton<ISurface, Table>()
            .AddSingleton<IRobot, Robot>()
            .AddSingleton<ICommandProcessor, CommandProcessor>()
            .BuildServiceProvider();

            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            var processor = serviceProvider.GetRequiredService<ICommandProcessor>();

            logger.LogInformation("===== Toy Robot Simulator =====");

            try
            {
                string fileName = args.Length > 0 ? args[0] : "commands.txt";

                if (!File.Exists(fileName))
                {
                    string altPath = Path.Combine(AppContext.BaseDirectory, fileName);
                    if (!File.Exists(altPath))
                    {
                        logger.LogError("File '{FileName}' not found in current or base directory.", fileName);
                        return;
                    }

                    fileName = altPath;
                }

                logger.LogInformation("Using command file: {FileName}", Path.GetFullPath(fileName));
                using var cts = new CancellationTokenSource();
                Console.CancelKeyPress += (sender, eventArgs) =>
                {
                    eventArgs.Cancel = true;
                    cts.Cancel();
                    logger.LogWarning("Cancellation requested...");
                };
                await processor.ProcessCommandsAsync(fileName, cts.Token);
            }
            catch (OperationCanceledException)
            {
                logger.LogWarning("Operation was canceled by the user.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unhandled exception during robot simulation.");
            }
        }
    }
}
