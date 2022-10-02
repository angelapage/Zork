using Newtonsoft.Json;
using System;
using System.IO;

namespace Zork
{
    class Program
    {
        private static Room CurrentRoom
        {
            get
            {
                return _rooms[_currentRow, _currentColumn];
            }
        }
        static void Main(string[] args)
        {
            string roomsFilename = "Content/Rooms.json";
            InitalizeRoomDescriptions(roomsFilename);

            Console.WriteLine("Welcome to Zork!");

            Room previousRoom = null;

            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine(CurrentRoom);

                if (previousRoom != CurrentRoom && CurrentRoom.HasBeenVisted == false)
                {
                    Console.WriteLine(CurrentRoom.Description);
                    previousRoom = CurrentRoom;
                    CurrentRoom.HasBeenVisted = true;
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
                        outputString = CurrentRoom.Description;
                        break;

                    case Commands.NORTH:
                    case Commands.SOUTH:
                    case Commands.EAST:
                    case Commands.WEST:
                        if (Move(command))
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

        private static bool Move(Commands command)
        {
            bool didMove = false;

            switch (command)
            {
                case Commands.NORTH when _currentRow < _rooms.GetLength(0) - 1:
                    _currentRow++;
                    didMove = true;
                    break;

                case Commands.SOUTH when _currentRow > 0:
                    _currentRow--;
                    didMove = true;
                    break;

                case Commands.EAST when _currentColumn < _rooms.GetLength(1) - 1:
                    _currentColumn++;
                    didMove = true;
                    break;

                case Commands.WEST when _currentColumn > 0:
                    _currentColumn--;
                    didMove = true;
                    break;
            }

            return didMove;
        }

        private static void InitalizeRoomDescriptions(string roomsFilename) =>
            _rooms = JsonConvert.DeserializeObject<Room[,]>(File.ReadAllText(roomsFilename));

        private static Room[,] _rooms =
            {
                {new Room("Rocky Trail"), new Room("South of House"), new Room("Canyon View") },
                {new Room("Forest"), new Room("West of House"), new Room("Behind House") },
                {new Room("Dense Woods"), new Room("North of House"), new Room("Clearing") }
            };

        private static int _currentRow = 1;
        private static int _currentColumn = 1;
    }
}