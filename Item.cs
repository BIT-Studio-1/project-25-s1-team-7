using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{ // Kelvin:- I created an Item class with two properties, Name and Description, to represent items in the game.
  // The constructor initializes these properties when an Item object is created.
    internal class Item 
    {
        public string Name { get; set; } 
        public string Description { get; set; } 
        public bool CanPickup { get; set; }
        public Item(string name, string description, bool canPickup)
        {
            Name = name;
            Description = description; 
            CanPickup = canPickup;
        }
    }
}
