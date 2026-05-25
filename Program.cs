using System.Text;

// Utilities
using static ConsoleApp1.GraphicUtils;
using static ConsoleApp1.SoundUtils;

// Game classes

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            // Assets Path
            string PathAssets = @"..\..\..\assets\";

            /* Do not change this path, it should
               work on any machine. */

            Teleprinter("""
                Hello! Before you start, please full screen the console for the best experience.

                If you don't, things might break!

                Press any key to continue...

                """, 5);

            Console.ReadKey(true);

            Console.Clear();

            Console.Write("Enter your name: ");
            string playerName = Console.ReadLine() ?? "";
            Player player = new(playerName);

            Console.Clear();

            //TODO: Load World
            World world = new World(); // maybe new World(PathAssets + "world.txt");
            world.DisplayCurrentRoom();
            bool running = true;
            while (running) // Game loop, will continue until player types 'quit'
            {
                Console.Write("> ");
                string command = Console.ReadLine().Trim().ToLower();
                switch (command)
                {
                    case "help":
                        Console.WriteLine("Available commands: look, move, pickup, inventory, escape, quit");
                        break;
                    case "look":
                        world.DisplayCurrentRoom();
                        break;
                    case "move":
                        Console.Write("Enter direction (north, south, east, west): ");
                        string direction = Console.ReadLine().Trim().ToLower();
                        world.MovePlayer(direction);
                        world.DisplayCurrentRoom();
                        break;
                    case "pickup":
                        Console.Write("What do you want to pick up? ");
                        string itemName = Console.ReadLine().Trim().ToLower();
                        Item foundItem = world.CurrentRoom.Items.Find(i => i.Name.ToLower() == itemName);
                        if (foundItem != null)
                        {
                            player.PickUp(foundItem);
                            world.CurrentRoom.Items.Remove(foundItem);
                            Console.WriteLine($"You picked up: {foundItem.Name}");
                        }
                        else
                        {
                            Console.WriteLine("That item is not here.");
                        }
                        break;
                    case "inventory":
                        player.showInventory();
                        break;
                    case "escape":
                        if (world.CurrentRoom.AttemptEscape(player))
                        {
                            running = false;
                        }
                        break;
                    case "quit":
                    case "exit":
                    case "q":
                        running = false;
                        Console.WriteLine("Thanks for playing!");
                        break;
                    case "cls":
                    case "clear":
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Unknown command. Type 'help' for a list of commands.");
                        break;
                }
            }
        }
    }
}
