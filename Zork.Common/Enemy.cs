using System;

namespace Zork.Common
{
    public class Enemy
    {
        public event EventHandler<int> ChangeHealth;

        public int _health = 5;


        private Room _currentRoom;

        public string Name { get; }

        public string Description { get; }

        public Enemy(string name, string description)
        {
            Name = name;
            Description = description;          
        }

        public Room CurrentRoom
        {
            get => _currentRoom;
            set
            {
                if (_currentRoom != value)
                {
                    _currentRoom = value;
                }
            }
        }

        public int Health
        {
            get
            {
                return _health;
            }
            set
            {
                if (_health != value)
                {
                    _health = value;
                    ChangeHealth?.Invoke(this, _health);
                }
            }
        }
        public override string ToString() => Name;
    }
}
