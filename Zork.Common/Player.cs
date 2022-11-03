using System;
using System.Collections.Generic;

namespace Zork.Common
{
    public class Player
    {
        public event EventHandler<int> MovesChanged;

        public Room CurrentRoom
        {
            get => _currentRoom;
            set => _currentRoom = value;
        }

        public List<Item> Inventory { get; set; }
        
        public int Moves 
        { 
            get
            {
                return _moves;
            }
            set
            {
                if (_moves != value)
                {
                    _moves = value;
                    MovesChanged?.Invoke(this, _moves); 
                }             
            }
        }

        public Player(World world, string startingLocation)
        {
            _world = world;

            if (_world.RoomsByName.TryGetValue(startingLocation, out _currentRoom) == false)
            {
                throw new Exception($"Invalid starting location: {startingLocation}");
            }

            Inventory = new List<Item>();
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
        public void Take(Game game, string itemName)
        {
            Item itemToTake = null;
            foreach (Item item in _world.Items)
            {
                if (string.Compare(item.Name, itemName, ignoreCase: true) == 0)
                {
                    itemToTake = item;
                    break;
                }
            }

            if (itemToTake == null)
            {
                game.Output.WriteLine("No such item exists. \n");
            }

            else
            {
                bool itemIsInRoomInventory = false;
                foreach (Item item in _currentRoom.Inventory)
                {
                    if (item == itemToTake)
                    {
                        itemIsInRoomInventory = true;
                        break;
                    }                   
                }

                if (itemIsInRoomInventory == false)
                {
                    game.Output.WriteLine("You can see no such thing. \n");
                }
                else
                {
                    game.Output.WriteLine("Taken \n");
                    Inventory.Add(itemToTake);
                    _currentRoom.Inventory.Remove(itemToTake);
                }
            }          
        }

        public void Drop(Game game, string itemName)
        {
            Item itemToDrop = null;
            foreach (Item item in _world.Items)
            {
                if (string.Compare(item.Name, itemName, ignoreCase: true) == 0)
                {
                    itemToDrop = item;
                    break;
                }
            }

            if (itemToDrop == null)
            {
                game.Output.WriteLine("No such item exists. \n");
            }

            else
            {
                bool itemIsInRoomInventory = false;
                foreach (Item item in _currentRoom.Inventory)
                {
                    if (item == itemToDrop)
                    {
                        itemIsInRoomInventory = false;
                        break;
                    }
                }

                if (itemIsInRoomInventory == true)
                {
                    game.Output.WriteLine("You can see no such thing. \n");
                }
                else
                {
                    game.Output.WriteLine("Dropped \n");
                    Inventory.Remove(itemToDrop);
                    _currentRoom.Inventory.Add(itemToDrop);
                }
            }
        }

        private World _world;
        private Room _currentRoom;
        private int _moves; 
    }
}
