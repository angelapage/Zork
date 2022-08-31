﻿using System;

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
        }

        private static Commands ToCommand(string commandString)
        {
            if (commandString == "QUIT")
            {
                Console.WriteLine("Thank you for playing.");
            }

            else if (commandString == "LOOK")
            {
                Console.WriteLine("This is an open field west of a white house, with a boarded front door.\nA rubber mat saying 'Welcome to Zork!' lies by the door.");
            }

            else if (commandString == "NORTH")
            {
                Console.WriteLine("You moved North.");
            }

            else if (commandString == "SOUTH")
            {
                Console.WriteLine("You moved South.");
            }

            else if (commandString == "EAST")
            {
                Console.WriteLine("You moved East.");
            }

            else if (commandString == "WEST")
            {
                Console.WriteLine("You moved West.");
            }

            else
            {

                Console.WriteLine($"Unknown command:" + commandString);
            }

            return Enum.TryParse(commandString,true,out Commands result) ? result : Commands.UNKNOWN;
        }
    }
}