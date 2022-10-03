using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Zork
{
    class Game
    {
        public World World { get; set; }
        
        public Player Player { get; set; }

        
        public void Run()
        {
            Console.WriteLine("Welcome to Zork!");

            Room previousRoom = null;

            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine(Player.CurrentRoom.ToString());

                if (previousRoom != Player.CurrentRoom && Player.CurrentRoom.HasBeenVisted == false)
                {
                    Console.WriteLine(Player.CurrentRoom.Description);
                    previousRoom = Player.CurrentRoom;
                    Player.CurrentRoom.HasBeenVisted = true;
                }

                Console.Write("> ");

                string inputString = System.Console.ReadLine();
                Commands command = ToCommand(inputString.Trim());

                string outputString;
                switch (command)
                {
                    case Commands.QUIT:
                        isRunning = false;
                        outputString = "Thank you for playing!.";
                        break;

                    case Commands.LOOK:
                        outputString = Player.CurrentRoom.Description;
                        break;

                    case Commands.NORTH:
                    case Commands.SOUTH:
                    case Commands.EAST:
                    case Commands.WEST:
                        if (Player.Move(command))
                        {
                            outputString = $"You moved {command}";
                        }

                        else
                        {
                            outputString = "The way is shut!";
                        }

                        break;

                    default:
                        outputString = "Unknown command.";
                        break;
                }

                Console.WriteLine(outputString);
            }
        }

        private static Commands ToCommand(string commandString)
        {
            return Enum.TryParse(commandString, true, out Commands result) ? result : Commands.UNKNOWN;
        }

        public static Game Load(string roomsFilename)
        {
            Game game = JsonConvert.DeserializeObject<Game>(File.ReadAllText(roomsFilename));
            game.Player = new Player(game.World);

            return game;
        }
    }
}
