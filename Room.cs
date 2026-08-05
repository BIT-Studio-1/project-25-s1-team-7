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
        public bool isLocked { get; set; }
        public bool isEscaped { get; set; } = false;
        public string scenePath { get; set; }
        public string mapPath { get; set; }

        public Room(string name, string description, string ScenePath, string MapPath)
        {
            Name = name;
            Description = description;
            scenePath = ScenePath;
            mapPath = MapPath;
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