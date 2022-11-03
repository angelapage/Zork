using System;

namespace Zork.Common
{
    public class Game
    {
        public World World { get; }

        public Player Player { get; }

        public Item Item { get; }

        public IOutputService Output { get; private set; }

        public Game(World world, Item item, string startingLocation)
        {
            World = world;
            Item = item;
            Player = new Player(World, startingLocation);
        }

        public void Run(IOutputService output)
        {
            Output = output ?? throw new ArgumentNullException(nameof(output));

            Room previousRoom = null;
            bool isRunning = true;
            while (isRunning)
            {
                Output.WriteLine(Player.CurrentRoom.ToString());
                if (previousRoom != Player.CurrentRoom && Player.CurrentRoom.HasBeenVisted == false)
                {
                    Output.WriteLine(Player.CurrentRoom.Description);
                  
                    previousRoom = Player.CurrentRoom;
                    Player.CurrentRoom.HasBeenVisted = true;

                    foreach (Item item in Player.CurrentRoom.Inventory)
                    {
                        Output.WriteLine (item.Description);
                    }
                }

                Output.Write("> ");

                string inputString = Console.ReadLine().Trim();
         
                char separator = ' ';
                string[] commandTokens = inputString.Split(separator);

                string verb = null;
                string subject = null;
             
                if (commandTokens.Length == 0)
                {
                    continue;
                }
                else if (commandTokens.Length == 1)
                {
                    verb = commandTokens[0];

                }
                else
                {
                    verb = commandTokens[0];
                    subject = commandTokens[1];
                }

                Commands command = ToCommand(verb);
                string outputString = null;
                switch (command)
                {
                    case Commands.QUIT:
                        isRunning = false;
                        outputString = "Thank you for playing!";
                        break;

                    case Commands.LOOK:
                        outputString = $"{Player.CurrentRoom.Description}\n";   
                        foreach (Item item in Player.CurrentRoom.Inventory)
                        {
                            outputString += $"{item.Description}\n";
                        }
                        break;

                    case Commands.NORTH:
                    case Commands.SOUTH:
                    case Commands.EAST:
                    case Commands.WEST:
                        Directions direction = (Directions)command;
                        if (Player.Move(direction))
                        {
                            outputString = $"You moved {direction}.";
                        }
                        else
                        {
                            outputString = "The way is shut!";
                        }
                        break;

                    case Commands.TAKE:
                        if (string.IsNullOrEmpty(subject))
                        {
                            outputString = "This command requires a subject. \n";
                        }
                        else
                        {
                            Player.Take(this, subject);                                                                       
                        }
                        break;

                    case Commands.DROP:
                        if (string.IsNullOrEmpty(subject))
                        {
                            outputString = "This command requires a subject. \n";
                        }
                        else
                        {
                            Player.Drop(this, subject);                                                     
                        }
                        break;

                    case Commands.INVENTORY:
                        if (Player.Inventory.Count == 0)
                        {
                            Output.WriteLine("You're emptyhanded. \n");
                        }
                        else if (Player.Inventory.Count >= 1)
                        {
                            foreach (Item item in Player.Inventory)
                            {
                                Output.WriteLine(item.Description);
                            }
                        }                       
                        break;

                    default:
                        outputString = "Unknown command.";
                        break;
                }
                if (command != Commands.UNKNOWN)
                {
                    Player.Moves++;
                }
                if (outputString != null)
                {
                    Output.WriteLine(outputString);
                }
            }
        }

        private static Commands ToCommand(string commandString) => Enum.TryParse(commandString, true, out Commands result) ? result : Commands.UNKNOWN;
    }
}
