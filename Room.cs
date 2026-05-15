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

        public Room(string name, string description, int difficulty)
        {
            Name = name;
            Description = description;
            Difficulty = difficulty;
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
    }
}
