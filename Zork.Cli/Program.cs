using System;
using System.IO;
using Newtonsoft.Json;
using Zork.Common;

namespace Zork.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            const string defaultGameFilename = @"Content\Game.json";
            string gameFilename = (args.Length > 0 ? args[(int)CommandLineArguments.GameFilename] : defaultGameFilename);
            Game game = JsonConvert.DeserializeObject<Game>(File.ReadAllText(gameFilename));

            var output = new ConsoleOutputService();
            var input = new ConsoleInputService();

            game.Player.MovesChanged += Player_MovesChanged;

            game.Run(input, output);

            static void Player_MovesChanged(object sender, int moves)
            {
                Console.WriteLine($"You've made {moves} moves.");
            }

            while (game.IsRunning)
            {
                game.Output.Write("> ");
                input.ProcessInput();
            }

            output.WriteLine("Thank you for playing!");
        }

        private enum CommandLineArguments
        {
            GameFilename = 0
        }
    }
}