namespace Zork
{
    internal class Player
    {
        public Room CurrentRoom
        {
            get
            {
                return _world.Rooms[_currentRow, _currentColumn];
            }
        }

        public int Score { get; }
        public int Moves { get; }

        public Player (World world)
        {
            _world = world;
        }

        public bool Move(Commands command)
        {
            bool didMove = false;

            switch (command)
            {
                case Commands.NORTH when _currentRow < _world.Rooms.GetLength(0) - 1:
                    _currentRow++;
                    didMove = true;
                    break;

                case Commands.SOUTH when _currentRow > 0:
                    _currentRow--;
                    didMove = true;
                    break;

                case Commands.EAST when _currentColumn < _world.Rooms.GetLength(1) - 1:
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

        private World _world;
        private static int _currentRow = 1;
        private static int _currentColumn = 1;
    }
}
