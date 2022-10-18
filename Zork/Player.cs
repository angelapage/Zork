namespace Zork
{
    public class Player
    {
        public Room CurrentRoom
        {
            get => _currentRoom;
            set => _currentRoom = value;
            
        }

        public int Score { get; }
        public int Moves { get; }

        public Player (World world, string startingLocation)
        {
            _world = world;
            
            if (_world.RoomsByName.TryGetValue(startingLocation, out _currentRoom) == false)
            {
                throw new System.Exception($"Invalid starting location: {startingLocation}");
            }          
        }

        public bool Move(Directions direction)
        {
            bool didMove = _currentRoom.Neighbors.TryGetValue(direction, out Room neighbor);
            if (didMove)
            {
                CurrentRoom = neighbor;
            }           
         
            return didMove;
        }

        private World _world;

        private Room _currentRoom;

        private (int Row, int Column) _location;
    }
}
