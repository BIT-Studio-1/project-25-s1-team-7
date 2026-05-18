using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Player
    {
        public string Name { get; set; } // could just be 'Player' or 'you', could be used later for more meaningful stuff.
        public int Health { get; set; } // maybe? Stamina? Energy? Depends on the game mechanics.
        public int Time { get; set; } // Time to complete, could be implemented as a stopwatch or timer in the actual game, plus add a method to start and stop the timer.
        public List<Item> Inventory { get; set; } = new List<Item>();

        public Player(string name)
        {
            Name = name;
            Health = 100; // Default health, can be adjusted based on game mechanics.
            Time = 0; // Initial time, can be updated as the player progresses through the game.
        }

        public void PickUp(Item item)
        {
            //TODO: maube add some logic to check if the item can be picked up (e.g., inventory limit, item weight, etc.)
            Inventory.Add(item);
            Console.WriteLine($"{Name} picked up {item.Name}.");
        }
        public void Drop(Item item)
        {
            if (Inventory.Contains(item))
            {
                Inventory.Remove(item);
                Console.WriteLine($"{Name} dropped {item.Name}.");
            }
            else
            {
                Console.WriteLine($"{Name} does not have {item.Name} in the inventory.");
            }
        } // Drop method is just an idea, could be used for puzzles that require dropping items, or for managing inventory space.
        public void UseItem(Item item)
        {

        }
        //TD and Heal methods are just an idea, could be used for boss fights, or wrong interactions that cause consequences.
        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health < 0) Health = 0; // Prevent health from going negative.
            Console.WriteLine($"{Name} took {damage} damage. Current health: {Health}"); // just for testing, can be removed or replaced with a more sophisticated health management system.
        }
        public void Heal(int amount)
        {
            Health += amount;
            if (Health > 100) Health = 100; // Prevent health from exceeding maximum.
            //TODO: maybe add some logic to check if the player has a healing item in the inventory before allowing them to heal, or implement a cooldown system for healing.
            Console.WriteLine($"{Name} healed {amount} health. Current health: {Health}");
        }
        public void showInventory()
        {
            Console.WriteLine($"{Name}'s Inventory:");
            if (Inventory.Count == 0)
            {
                Console.WriteLine("Inventory is empty.");
            }
            else
            {
                foreach (var item in Inventory)
                {
                    Console.WriteLine($"- {item.Name}: {item.Description}"); // basic inventory display, can be enhanced with item stats, quantity, etc.
                }
            }
        }
    }
}