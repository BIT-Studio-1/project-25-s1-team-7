using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Room
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();
        public int Difficulty { get; set; }
        public bool isLocked { get; set; }
        public bool isEscaped { get; set; } = false;
        public string scenePath { get; set; }

        public Room(string name, string description, int difficulty, string ScenePath)
        {
            Name = name;
            Description = description;
            Difficulty = difficulty; //Unused currently
            scenePath = ScenePath;
        }

        public bool AttemptEscape(Player player)
        {
            if (isLocked) // will need to flesh out the logic for unlocking the door, this is just a placeholder.
            {
                Console.WriteLine("The door is locked. You need to find a way to unlock it.");
                return false;
            }
            else
            {
                Console.WriteLine("You have escaped the room! Congratulations!");
                isEscaped = true;
                return true;
            }  
        }
        public void UseItem(Player player, string target, Item item)
        {
            if (Items.Contains(item))
            {
                Console.WriteLine($"You used {item.Name} on {target}.");
                // Implement logic for using the item on the target
                // For example, if the item is a key and the target is a door, you could unlock the door
            }
            else
            {
                Console.WriteLine($"You don't have {item.Name} in this room.");
            }
        }
    }
}
