using System.Text;

// Utilities
using static ConsoleApp1.GraphicUtils;
using static ConsoleApp1.SoundUtils;

// Game classes
using static ConsoleApp1.Item;
using static ConsoleApp1.Room;
using static ConsoleApp1.Player;
using static ConsoleApp1.World;

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

            Console.Write("Enter your name: ");
            string playerName = Console.ReadLine();
            Player player = new(playerName);

            //TODO: Load World
            World world = new World(); // maybe new World(PathAssets + "world.txt");

            world.DisplayCurrentRoom();
            bool running = true;
            while (running) // Game loop, will continue until player types 'quit'
            {
                Console.WriteLine("What do you want to do? (type 'help' for commands)");
                string command = Console.ReadLine().Trim().ToLower();
                switch (command)
                {
                    case "help":
                        Console.WriteLine("Available commands: look, move, inventory, quit");
                        break;
                    case "look":
                        world.DisplayCurrentRoom();
                        break;
                    case "move":
                        Console.Write("Enter direction (north, south, east, west): ");
                        string direction = Console.ReadLine().Trim().ToLower();
                        world.MovePlayer(direction);
                        break;
                    case "inventory":
                        player.showInventory();
                        break;
                    case "quit":
                        running = false;
                        Console.WriteLine("Thanks for playing!");
                        break;
                    default:
                        Console.WriteLine("Unknown command. Type 'help' for a list of commands.");
                        break;
                }
            }
        }
    }
}
