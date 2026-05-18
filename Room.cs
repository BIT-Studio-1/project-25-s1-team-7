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
            Difficulty = difficulty;
            scenePath = ScenePath;
        }
        public void displayRoom()
        {
            //rough example.
            Console.WriteLine($"You are in {Name}.");
            Console.WriteLine(Description);
            if (Items.Count > 0)
            {
                Console.WriteLine("You see the following items:");
                foreach (var item in Items)
                {
                    Console.WriteLine($"- {item.Name}: {item.Description}");
                }
            }
            else
            {
                Console.WriteLine("There are no items in this room.");
            }


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
    }
}
