using System.Collections.Generic;

namespace Zork
{
    public class Room
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Dictionary<Directions, string > NeighborNames { get; set; }
        public Dictionary<Directions, Room> Neighbors { get; set; }

        public bool HasBeenVisted { get; set; }

        public Room(string name, string description, Dictionary<Directions, string> neighborNames)
        {
            Name = name;
            Description = description;
            NeighborNames = neighborNames ?? new Dictionary<Directions, string>();
        }

        public void UpdateNeighbors(World world)
        {
            Neighbors = new Dictionary<Directions, Room>();
           foreach (KeyValuePair<Directions, string> neighborName in NeighborNames)
            {
                Neighbors.Add(neighborName.Key, world.RoomsByName[neighborName.Value]);
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}