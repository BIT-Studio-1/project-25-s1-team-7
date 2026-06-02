using System.Text;

// Utilities
using static ConsoleApp1.GraphicUtils;
using static ConsoleApp1.SoundUtils;
using static ConsoleApp1.Titlescreen;

// Game classes

namespace ConsoleApp1
{
    /// <summary>
    /// Handles information we want to access globally i.e. file paths.
    /// </summary>
    internal class GameConfig
    {
        public static readonly string PathAssets = @"..\..\..\assets\";
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            PlayTitleScreen();

            Console.OutputEncoding = Encoding.UTF8;

            Teleprinter("""
                Hello! Before you start, please full screen the console for the best experience.

                If you don't, things might break!

                Press any key to continue...

                """, 5);

            Console.ReadKey(true);

            ConsoleFormatter.Clear();

            Console.Write("Enter your name: ");
            string playerName = Console.ReadLine() ?? "";
            Player player = new(playerName);

            ConsoleFormatter.Clear();

            //TODO: Load World
            World world = new World(); // maybe new World(PathAssets + "world.txt");
            bool running = true;
            Teleprinter("""
                You slowly regain consciousness - you realize you are imprisoned. 
                                
                Available commands: look, move, pickup, use, inventory, escape, quit 
                """, 5);
            Thread.Sleep(1000);
            while (running) // Game loop, will continue until player types 'quit'
            {
                Console.Write("> ");
                string command = Console.ReadLine() ?? ""
                    .Trim().ToLower();

                switch (command)
                {
                    case "help":
                        Console.WriteLine("Available commands: look, move, pickup, use, inventory, escape, quit");
                        break;

                    case "look":
                        world.DisplayCurrentRoom();
                        break;

                    case "move":
                        Console.Write("Enter direction (north, south, east, west): ");
                        string direction = Console.ReadLine() ?? ""
                            .Trim().ToLower();

                        world.MovePlayer(direction);
                        break;

                    case "test":
                        Renderer.Render(GameConfig.PathAssets + "entrance_hall.txt");
                        ConsoleFormatter.Clear();
                        break;

                    case "pickup":
                        
                        Teleprinter("What do you want to pick up? ", 5);
                        string itemName = Console.ReadLine() ?? "";
                        itemName = itemName.Trim().ToLower();

                        Item foundItem = null;

                        foreach (Item item in world.CurrentRoom.Items)
                        {
                            if (item.Name.ToLower() == itemName)
                            {
                                foundItem = item;
                                break;
                            }
                        }
                        if (foundItem == null)
                        {
                            Console.WriteLine($"There is no {itemName} here.");
                        }
                        else if (!foundItem.CanPickup)
                        {
                            Console.WriteLine($"You cannot pick up {foundItem.Name}.");
                        }
                        else
                        {
                            player.PickUp(foundItem);
                            world.CurrentRoom.Items.Remove(foundItem);
                            Console.WriteLine($"You picked up: {foundItem.Name}");
                        }
                        break;
                    //Console.Write("What do you want to pick up? ");
                    //string itemName = Console.ReadLine() ?? ""
                    //    .Trim().ToLower();

                            //Item foundItem = world.CurrentRoom.Items.Find(i => i.Name.ToLower() == itemName); //Will simplify
                            //if (foundItem != null)
                            //{
                            //    player.PickUp(foundItem);
                            //    world.CurrentRoom.Items.Remove(foundItem);
                            //    Console.WriteLine($"You picked up: {foundItem.Name}");
                            //}
                            //else
                            //{
                            //    Console.WriteLine("That item is not here.");
                            //}
                            //break;
                    case "use":
                        Teleprinter("What do you want to use? ", 5);
                        Console.Write("> ");
                        string useItemName = (Console.ReadLine() ?? "").Trim().ToLower();
                        Teleprinter("What do you want to use it on? ", 5);
                        Console.Write("> ");
                        string targetName = (Console.ReadLine() ?? "").Trim().ToLower();
                        Item useItem = player.Inventory.Find(i => i.Name.ToLower() == useItemName);
                        if (useItem != null)
                        {
                            world.CurrentRoom.UseItem(player, targetName, useItem);
                        }
                        else
                        {
                            Console.WriteLine("You don't have that item.");
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
                        ConsoleFormatter.Clear();
                        break;

                    default:
                        Console.WriteLine("Unknown command. Type 'help' for a list of commands.");
                        break;
                }
            }
        }
    }
}
