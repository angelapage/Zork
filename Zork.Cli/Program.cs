﻿using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Zork.Common;

namespace Zork.Cli
{
    class Program
    {
        static void Main(string[] args)
        {           
            const string defaultRoomsFilename = @"Content\Game.json";
            string gameFilename = (args.Length > 0 ? args[(int)CommandLineArguments.GameFilename] : defaultRoomsFilename);
            Game game = JsonConvert.DeserializeObject<Game>(File.ReadAllText(gameFilename));

            var input = new ConsoleInputService();         
            var output = new ConsoleOutputService();

            game.Player.MovesChanged += Player_MovesChanged;

            Console.WriteLine("Welcome to Zork!");
            game.Run(output);
        }

        private static void Player_MovesChanged (object sender, int moves)
        {
            Console.WriteLine($"You've made {moves} moves.");
        }
        private enum CommandLineArguments
        {
            GameFilename = 0
        }
    }
}