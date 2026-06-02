using System;
using System.Collections.Generic;
using System.Linq;
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
            _mapGrid[0, 0].Items.Add(new Item("Journal", "holds random notes and a bookmarked page that reads \"the answer is the third number, minus the first, times the second, plus the first\"\r\n",true));

            // Item Section 2

            //_mapGrid[0, 1].Items.Add(new Item("")); - Exit door Issue

            // Item Section 3

            //_mapGrid[0, 2].Items.Add(new Item("")); - NPC (or sign - more realistic for the timeframe)
            _mapGrid[0, 2].Items.Add(new Item("Key 3", "A small green key decorated with etchings of vines, it should work on the third lock on the door.", true));

            // Item Section 4
            _mapGrid[1, 0].Items.Add(new Item("Chest", "unlocked and has heaps of turnips, not useful, but there seems to be a sword in there too, weird place to keep it but hey.", false));
            _mapGrid[1, 0].Items.Add(new Item("Sword", "Very pointy, ouch.", true));

            // Item Section 5
            _mapGrid[1, 1].Items.Add(new Item("Table", "Shifty looking table that is somehow still standing. Found in the centre of the room, on it, is the matchbox and a bottle of water.", false));
            _mapGrid[1, 1].Items.Add(new Item("Matchbox", "Found on the table, can be used to light the torch.", true));
            _mapGrid[1, 1].Items.Add(new Item("Bottle of Water", "might be helpful for making some soup, wonder if there are any recipes around....", true));

            // Item Section 6
            _mapGrid[1, 2].Items.Add(new Item("Torch", "Can be lit to reveal things you may have missed.", true));
            _mapGrid[1, 2].Items.Add(new Item("Key 1", "A mysterious blue key, the first step to getting out.", true));

            // Item Section 7
            _mapGrid[2, 0].Items.Add(new Item("Cauldron", "A large cauldron, bubbling with unknown contents.", false));

            // Item Section 8
            _mapGrid[2, 1].Items.Add(new Item("Statue", "Holding Key 2, it's eyes seem to follow you around the room.", false));
            _mapGrid[2, 1].Items.Add(new Item("Key 2", "A glowing red key that could help you open that door.", true));
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

        // set up puzzle methods
        public void TorchPuzzle()
        {

        }

        public void StatuePuzzle()
        {

        }

        public void RiddlePuzzle()
        {

        }

        public void CauldronPuzzle()
        {

        }
    }
}
