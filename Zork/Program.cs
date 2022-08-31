using System;

namespace Zork
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork!");

            string inputString = System.Console.ReadLine();
            Commands command = ToCommand(inputString.Trim().ToUpper());
            Console.WriteLine(command);  
            
            if (command == Commands.QUIT)
            {
                Console.WriteLine("Thank you for playing.");
            }

            else if (command == Commands.LOOK)
            {
                Console.WriteLine("This is an open field west of a white house, with a boarded front door.\nA rubber mat saying 'Welcome to Zork!' lies by the door.");
            }

            else if (command == Commands.NORTH)
            {
                Console.WriteLine("You moved North.");
            }

            else if (command == Commands.SOUTH)
            {
                Console.WriteLine("You moved South.");
            }

            else if (command == Commands.EAST)
            {
                Console.WriteLine("You moved East.");
            }

            else if (command == Commands.WEST)
            {
                Console.WriteLine("You moved West.");
            }

            else
            {

                Console.WriteLine($"Unknown command:" + inputString);
            }
        }

        private static Commands ToCommand(string commandString)
        {                  
            return Enum.TryParse(commandString,true,out Commands result) ? result : Commands.UNKNOWN;
        }
    }
}