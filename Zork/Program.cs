using System;

namespace Zork
{
    class Program
    {
        static void Main(string[] args)
        {
            string gameFilename = "Content/Game.json";

            Game game = Game.Load(gameFilename);
            game.Run();
        }
    }
}