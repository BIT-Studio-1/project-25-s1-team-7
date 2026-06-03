using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

using static ConsoleApp1.GraphicUtils;
using static ConsoleApp1.Player;

namespace ConsoleApp1
{
    internal class World
    {
        //Initialises world map in a 3x3 2D array. Player location determined by row and column.
        //Map and player location encapsulated to prevent player input modifying these values.
        private Room[,] _mapGrid;
        //Player starts in bottom middle square of 3x3 grid.
        private int _playerRow = 0;
        private int _playerCol = 1;
        private bool _riddlePuzzleSolved = false;
        private bool _statePuzzleSolved = false;
        private bool _cauldronPuzzleSolved = false;
        private bool _waterAdded = false;
        private bool _herbsAdded = false;



        //Call this to display current room via World class ie - "Current room: {world.CurrentRoom}";
        public Room CurrentRoom => _mapGrid[_playerRow, _playerCol];

        public World()
        {
            _mapGrid = new Room[3, 3];

            //Last integer value is room difficulty. Unsure if this is a good idea to implement though, just in terms of keeping things simple.
            //Add more rooms and room details here. Rooms hardcoded for ease of use. Populating world grid dynamically not necessary.

            //Section 1
            _mapGrid[0, 0] = new Room("Entrance Hall", "A heavy door slams shut behind you. The air is cold and stale.", 1, GameConfig.PathAssets + "\\walls\\north_west_corner.txt");
            _mapGrid[0, 1] = new Room("Stone Cell", "Damp walls surround you. Scratch marks cover the stone floor.", 1, GameConfig.PathAssets + "\\walls\\north_wall.txt");
            _mapGrid[0, 2] = new Room("Dusty Library", "Shelves of rotting books line the walls. Something feels off.", 2, GameConfig.PathAssets + "\\walls\\north_east_corner.txt");
            //Section 2
            _mapGrid[1, 0] = new Room("Flooded Basement", "Ankle deep water covers the floor. A faint dripping echoes.", 2, GameConfig.PathAssets + "\\walls\\west_wall.txt");
            _mapGrid[1, 1] = new Room("Guard's Quarters", "An empty cot and rusted armour stand in the corner.", 3, GameConfig.PathAssets + "\\map\\main_map.txt");
            _mapGrid[1, 2] = new Room("Candlelit Chapel", "Candles flicker despite no wind. The exit door is ahead.", 3, GameConfig.PathAssets + "\\walls\\east_wall.txt");
            //Section 3
            _mapGrid[2, 0] = new Room("Kitchen", "A cold hearth and empty pots. Something smells rotten.", 2, GameConfig.PathAssets + "\\walls\\south_west_corner.txt");
            _mapGrid[2, 1] = new Room("Dining Hall", "A long table set for a feast that never happened.", 2, GameConfig.PathAssets + "\\walls\\south_wall.txt");
            _mapGrid[2, 2] = new Room("Trophy Room", "Hunting trophies stare down at you from the walls.", 3, GameConfig.PathAssets + "\\walls\\south_east_corner.txt");

            //Example of a locked room. Can swap bool value of any room to false to unlock if logic conditions in main script met.
            _mapGrid[0, 0].isLocked = true; // Exit room, needs all 4 keys
            _mapGrid[1, 0].isLocked = true; // Flooded basement locked until player finds key in guard's quarters. Can add more locked rooms and keys as needed.

            //Add items to rooms here. Items hardcoded for ease of use. Populating rooms with items dynamically not necessary.

            // Item Section 1
            _mapGrid[0, 0].Items.Add(new Item("Bookshelf", "Holds plenty of books, among them, 'Book on being wicked', 'brewing up broth - a cooking guide' which holds notes about mixing herbs and water in a lit", false));
            _mapGrid[0, 0].Items.Add(new Item("Book on being wicked", "contains text that says 'Mwahahahahahaha' and that's it.", true));
            _mapGrid[0, 0].Items.Add(new Item("Brewing up Broth", "a cooking guide - contains many soupy recipes", true));
            _mapGrid[0, 0].Items.Add(new Item("Journal", "holds random notes and a bookmarked page that reads \"the answer is the third number, minus the first, times the second, plus the first\"\r\n", true));

            // Item Section 2

            //_mapGrid[0, 1].Items.Add(new Item("")); - Exit door Issue

            // Item Section 3

            //_mapGrid[0, 2].Items.Add(new Item("")); - NPC (or sign - more realistic for the timeframe)
            _mapGrid[0, 2].Items.Add(new Item("Sign", "The sign reads: \"I have these 3 numbers: 4, 2, and 7. You need to use them to result in the answer somehow...\"", false));

            // Item Section 4
            _mapGrid[1, 0].Items.Add(new Item("Chest", "unlocked and has heaps of turnips, not useful, but there seems to be a sword in there too, weird place to keep it but hey.", false));
            _mapGrid[1, 0].Items.Add(new Item("Sword", "Very pointy, ouch.", true));

            // Item Section 5
            _mapGrid[1, 1].Items.Add(new Item("Table", "Shifty looking table that is somehow still standing. Found in the centre of the room, on it, is the matchbox and a bottle of water.", false));
            _mapGrid[1, 1].Items.Add(new Item("Matchbox", "Found on the table, can be used to light the torch.", true));
            _mapGrid[1, 1].Items.Add(new Item("Bottle of Water", "might be helpful for making some soup, wonder if there are any recipes around....", true));

            // Item Section 6
            _mapGrid[1, 2].Items.Add(new Item("Torch", "Can be lit to reveal things you may have missed.", true));
            //_mapGrid[1, 2].Items.Add(new Item("Key 1", "A mysterious blue key, the first step to getting out.", true));

            // Item Section 7
            _mapGrid[2, 0].Items.Add(new Item("Cauldron", "A large cauldron, bubbling with unknown contents.", false));

            // Item Section 8
            _mapGrid[2, 1].Items.Add(new Item("Statue", "Holding Key 2, it's eyes seem to follow you around the room.", false));
            //_mapGrid[2, 1].Items.Add(new Item("Key 2", "A glowing red key that could help you open that door.", true));
            // Item Section 9
            _mapGrid[2, 2].Items.Add(new Item("Crate", "Sealed shut, might need something to open it.", false));
            _mapGrid[2, 2].Items.Add(new Item("Unknown Herbs", "Strange herbs, not sure what they do.", true));
        }


        public void DisplayCurrentRoom()
        {
            // Only render ASCII art if this room has a scene file
            //if (!string.IsNullOrEmpty(CurrentRoom.scenePath) && File.Exists(CurrentRoom.scenePath)) // Check if the file exists to avoid errors
            //{
            //    return;
            //}

            Renderer.Render(CurrentRoom.scenePath);

            Console.WriteLine($"Current room: {CurrentRoom.Name}");
            Console.WriteLine(CurrentRoom.Description);
            if (CurrentRoom.Items.Count > 0)
            {
                Console.WriteLine("You see the following items:");
                foreach (var item in CurrentRoom.Items)
                {
                    Console.WriteLine($"- {item.Name}: {item.Description}");
                }
            }
            else
            {
                Console.WriteLine("There are no items in this room.");
            }

            DisplayAvailableDirections();
        }

        //Returns list of strings of available directions to travel. 
        //Works out if the player is on the border of the map. If they are, that direction is NOT added to the list of available directions to move.
        public List<string> GetAvailableDirections()
        {
            var directions = new List<string>();

            if (_playerRow > 0) directions.Add("north");  // room exists above
            if (_playerRow < 2) directions.Add("south");  // room exists below
            if (_playerCol > 0) directions.Add("west");   // room exists to the left
            if (_playerCol < 2) directions.Add("east");   // room exists to the right
            return directions;
        }

        public void MovePlayer(string direction)
        {
            direction = NormalizeDirection(direction);

            //Checks if the direction the player wants to move in is in the list of available directions. If it is, moves player in that direction and returns true. If not, returns false.
            var availableDirections = GetAvailableDirections();
            if (availableDirections.Contains(direction))
            {
                switch (direction)
                {
                    case "north":
                        _playerRow--;
                        break;
                    case "south":
                        _playerRow++;
                        break;
                    case "west":
                        _playerCol--;
                        break;
                    case "east":
                        _playerCol++;
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please try north, south, east or west.");
                        return;
                }
                Console.WriteLine($"You move {direction} into the {CurrentRoom.Name}.");
                DisplayAvailableDirections();
            }
            else
            {
                // I reckon it would be pretty funny if this hurt the player, maybe they take damage from walking into the wall?
                Console.WriteLine("You walk into a solid stone wall.");
                return;
            }
        } // Will need to create a sensitivity check for player input in main script to ensure they are entering directions in the correct format.
          // Could also add a method here to convert player input to the correct format if they enter it in a different way.
        public void DisplayAvailableDirections()
        {
            var directions = GetAvailableDirections();
            Console.WriteLine("Available directions to move: " + string.Join(", ", directions));
        }

        private string NormalizeDirection(string direction)
        {
            return direction switch
            {
                "n" => "north",
                "s" => "south",
                "e" => "east",
                "w" => "west",
                _ => direction
            };
        }

        // set up puzzle methods
        public void UseItem(Item item, string target, Player player)
        {
            // Dispatch to the right puzzle based on current room
            if (CurrentRoom == _mapGrid[1, 2]) // Section 6 - Torch room
            {
                TorchPuzzle(item, target, player);
            }
            else
            {
                Console.WriteLine($"You used {item.Name} on {target}. Nothing happened.");
            }
        }

        public void TorchPuzzle(Item item, string target, Player player)
        {
            if (item.Name.ToLower() == "matchbox" && target == "torch")
            {
                // Check player has the torch too
                Item torch = player.Inventory.Find(i => i.Name.ToLower() == "torch");
                if (torch != null)
                {
                    Console.WriteLine("You light the torch! The room brightens and you notice a blue key hanging on the wall.");
                    // Add Key 1 to the room for player to pick up
                    CurrentRoom.Items.Add(new Item("blue key", "A mysterious blue key, the first step to getting out.", true));
                }
                else
                {
                    Console.WriteLine("You'd need a torch to light first.");
                }
            }
            else
            {
                Console.WriteLine("Nothing happens.");
            }
        }

        public bool StatuePuzzle(Player player)
        {
            bool isInDiningHall = _playerRow == 2 && _playerCol == 1;
            Item? statue = CurrentRoom.Items.Find(item => item.Name.Equals("Statue", StringComparison.OrdinalIgnoreCase));
            Item? key = CurrentRoom.Items.Find(item => item.Name.Equals("red key", StringComparison.OrdinalIgnoreCase));

            if (!isInDiningHall || statue == null)
            {
                Console.WriteLine("There is no statue here to inspect");
                return false;
            }

            if (_statePuzzleSolved)
            {
                Console.WriteLine("The statue has already given you the red key.");
                return true;
            }

            Console.WriteLine("The statue's stone eyes glow as it asks:");
            Console.WriteLine("\"What is the binary answer for 20 - 14?\"");
            string answer = Console.ReadLine() ?? "";

            if (answer.Trim() != "0110")
            {
                Console.WriteLine("The statue goes still. That answer must not have been correct.");
                return false;
            }

            _statePuzzleSolved = true;
            if (key != null)
            {
                CurrentRoom.Items.Remove(key);
                key.CanPickup = true;
                player.PickUp(key);
            }

            statue.Description = "The statue stands peacefully now, no longer clutching the red key.";
            Console.WriteLine("The statue nods and drops the red key");
            return true;
        }

        public bool InspectJournal(Player player)
        {
            Item? journal = CurrentRoom.Items.Find(item => item.Name.Equals("Journal", StringComparison.OrdinalIgnoreCase));
            journal ??= player.Inventory.Find(item => item.Name.Equals("Journal", StringComparison.OrdinalIgnoreCase));

            if (journal == null)
            {
                Console.WriteLine("There is no journal here to inspect.");
                return false;
            }

            Console.WriteLine("The journal holds random notes and a bookmarked page that reads:");
            Console.WriteLine("\"The answer is (the third number minus the first) times the second, plus the first.\"");
            return true;
        }

        public bool RiddlePuzzle(Player player)
        {
            bool isInLibrary = _playerRow == 0 && _playerCol == 2;
            Item? sign = CurrentRoom.Items.Find(item => item.Name.Equals("Sign", StringComparison.OrdinalIgnoreCase));

            if (!isInLibrary || sign == null)
            {
                Console.WriteLine("There is no riddle sign here to inspect.");
                return false;
            }

            Console.WriteLine(sign.Description);

            if (_riddlePuzzleSolved)
            {
                Console.WriteLine("The riddle has already been solved, and Key 3 has been taken.");
                return true;
            }

            Console.WriteLine("The journal's note may help you arrange the numbers.");
            Console.Write("Answer: ");
            string answer = Console.ReadLine() ?? "";

            if (answer.Trim() != "10")
            {
                Console.WriteLine("The sign remains silent. That answer was not correct.");
                return false;
            }

            _riddlePuzzleSolved = true;
            Item key = new("Key 3", "A small green key decorated with etchings of vines, it should work on the third lock on the door.");
            player.PickUp(key);
            Console.WriteLine("The sign clicks, and the green key drops into your hands.");
            return true;
        }

        public bool CauldronPuzzle(Player player)
        {
            bool isInCauldronRoom = _playerRow == 2 && _playerCol == 0;

            Item? cauldron = CurrentRoom.Items.Find(item =>
                item.Name.Equals("Cauldron", StringComparison.OrdinalIgnoreCase));

            Item? key = CurrentRoom.Items.Find(item =>
                item.Name.Equals("Key 4", StringComparison.OrdinalIgnoreCase));

            if (!isInCauldronRoom || cauldron == null)
            {
                Console.WriteLine("There is no cauldron here.");
                return false;
            }

            if (_cauldronPuzzleSolved)
            {
                Console.WriteLine("The cauldron sits empty. You already claimed the key.");
                return true;
            }

            Item? waterBottle = player.Inventory.Find(item =>
                item.Name.Equals("Water Bottle", StringComparison.OrdinalIgnoreCase));

            Item? herbs = player.Inventory.Find(item =>
                item.Name.Equals("Unknown Herbs", StringComparison.OrdinalIgnoreCase));

            if (waterBottle == null || herbs == null)
            {
                Console.WriteLine("The cauldron seems to need both water and herbs.");
                return false;
            }

            Console.WriteLine("You pour the water into the cauldron.");
            Console.WriteLine("You add the unknown herbs.");
            Console.WriteLine();
            Console.WriteLine("The mixture begins to bubble violently...");
            Console.WriteLine("Steam fills the room.");
            Console.WriteLine("As the liquid evaporates, something gleams at the bottom.");
            Console.WriteLine();

            player.Inventory.Remove(waterBottle);
            player.Inventory.Remove(herbs);

            _cauldronPuzzleSolved = true;

            if (key != null)
            {
                CurrentRoom.Items.Remove(key);
                key.CanPickup = true;
                player.PickUp(key);
            }

            cauldron.Description =
                "The cauldron is dry and empty. Whatever magic it held has long since faded.";

            Console.WriteLine("You found Key 4!");
            return true;
        }
        public void FinalDoorPuzzle(Player player)
        {
            if (CurrentRoom == _mapGrid[0, 0]) // Section 1 - Exit room
            {
                // Check player has all 4 keys
                bool hasKey1 = player.Inventory.Exists(i => i.Name.ToLower() == "blue key");
                bool hasKey2 = player.Inventory.Exists(i => i.Name.ToLower() == "key 2");
                bool hasKey3 = player.Inventory.Exists(i => i.Name.ToLower() == "key 3");
                bool hasKey4 = player.Inventory.Exists(i => i.Name.ToLower() == "key 4");
                if (hasKey1 && hasKey2 && hasKey3 && hasKey4)
                {
                    Console.WriteLine("You use all four keys to unlock the door. It creaks open, revealing your escape route. Congratulations, you've escaped!");
                    CurrentRoom.isEscaped = true;
                }
                else
                {
                    Console.WriteLine("The door is locked. You need to find all four keys to escape.");
                }
            }
            else
            {
                Console.WriteLine("There is no door to escape here.");
            }
        }
    }
}
