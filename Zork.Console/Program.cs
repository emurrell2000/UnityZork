using System.IO;
using Newtonsoft.Json;

namespace Zork
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string defaultGameFilename = "Zork.json";
            string gameFilename = args.Length > 0 ? args[(int)CommandLineArguments.GameFilename] : defaultGameFilename;

            // Game game = JsonConvert.DeserializeObject<Game>(File.ReadAllText(gameFilename));

            ConsoleInputService input = new ConsoleInputService();
            ConsoleOutputService output = new ConsoleOutputService();

            Game.StartFromFile(gameFilename, input, output);

            while (Game.Instance.IsRunning)
            {
                output.Write("\n> ");
                input.ProcessInput();
            }

            // output.WriteLine(game.WelcomeMessage);

            output.WriteLine(Game.Instance.ExitMessage);
        }

        private enum CommandLineArguments
        {
            GameFilename = 0
        }
    }
}