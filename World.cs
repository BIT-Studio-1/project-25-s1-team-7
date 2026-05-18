using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static ConsoleApp1.GraphicUtils;

namespace ConsoleApp1
{
    internal class World
    {
        //Initialises world map in a 4 by 4 2D array. Player location determined by row and column.
        //Map and player location encapsulated to prevent player input modifying these values.
        private Room[,] _mapGrid;
        private int _playerRow;
        private int _playerCol;

        //Call this to display current room via World class ie - "Current room: {world.CurrentRoom}";
        public Room CurrentRoom => _mapGrid[_playerRow, _playerCol];

        public World()
        {
            _mapGrid = new Room[4, 4];

            //Last integer value is room difficulty. Unsure if this is a good idea to implement though, just in terms of keeping things simple.
            //Add more rooms and room details here. Rooms hardcoded for ease of use. Populating world grid dynamically not necessary.

            //_mapGrid[0, 0] = new Room("Room name here", "Room description here", 1);
            //_mapGrid[0, 1] = new Room("Room name here", "Room description here", 1);
            //_mapGrid[0, 2] = new Room("Room name here", "Room description here", 1);

            //_mapGrid[1, 0] = new Room("Room name here", "Room description here", 1);
            //_mapGrid[1, 1] = new Room("Room name here", "Room description here", 1);
            //_mapGrid[1, 2] = new Room("Room name here", "Room description here", 1);

            _mapGrid[0, 0] = new Room("Entrance Hall", "A heavy door slams shut behind you. The air is cold and stale.", 1, "C:\\Users\\Ashton Scott\\Visual Studio\\source\\repos\\project-25-s1-team-7\\assets\\entrance_hall.txt");
            _mapGrid[0, 1] = new Room("Stone Cell", "Damp walls surround you. Scratch marks cover the stone floor.", 1, "");
            _mapGrid[0, 2] = new Room("Dusty Library", "Shelves of rotting books line the walls. Something feels off.", 2, "");

            _mapGrid[1, 0] = new Room("Flooded Basement", "Ankle deep water covers the floor. A faint dripping echoes.", 2, "");
            _mapGrid[1, 1] = new Room("Guard's Quarters", "An empty cot and rusted armour stand in the corner.", 3, "");
            _mapGrid[1, 2] = new Room("Candlelit Chapel", "Candles flicker despite no wind. The exit door is ahead.", 3, "");
            _mapGrid[0, 3] = new Room("Armoury", "Weapon racks line the walls, all empty.", 2, "");

            _mapGrid[1, 3] = new Room("Torture Chamber", "You don't want to spend long in here.", 3, "");

            _mapGrid[2, 0] = new Room("Kitchen", "A cold hearth and empty pots. Something smells rotten.", 2, "");
            _mapGrid[2, 1] = new Room("Dining Hall", "A long table set for a feast that never happened.", 2, "");
            _mapGrid[2, 2] = new Room("Trophy Room", "Hunting trophies stare down at you from the walls.", 3, "");
            _mapGrid[2, 3] = new Room("Observatory", "A telescope points at a sky you can't see.", 3, "");

            _mapGrid[3, 0] = new Room("Catacombs", "Bones line the walls. The air is thick and heavy.", 4, "");
            _mapGrid[3, 1] = new Room("Throne Room", "An imposing throne sits empty. The exit is so close.", 4, "");
            _mapGrid[3, 2] = new Room("Secret Passage", "A narrow corridor hidden behind a bookshelf.", 4, "");
            _mapGrid[3, 3] = new Room("Final Chamber", "This is it. The last room stands between you and freedom.", 4, "");
        }

        public void DisplayCurrentRoom() //simple room display method. Can be called in main script to show player their current location and room description.
        {
            Console.Clear();
            RenderFrame(CurrentRoom.scenePath, 0);

            Console.WriteLine($"Current room: {CurrentRoom.Name}");
            Console.WriteLine(CurrentRoom.Description);

            //Example of a locked room. Can swap bool value of any room to false to unlock if logic conditions in main script met.
            _mapGrid[1, 0].isLocked = true;

            //Player starting position.
            _playerCol = 0;
            _playerRow = 0;
        }

        //Returns list of strings of available directions to travel. 
        //Works out if the player is on the border of the map. If they are, that direction is NOT added to the list of available directions to move.
        public List<string> GetAvailableDirections()
        {
            var directions = new List<string>();
            //if (_playerRow >= 0)
            //{
            //    directions.Add("North");
            //}

            //if (_playerRow <= 4)
            //{
            //    directions.Add("South");
            //}

            //if (_playerCol >= 0)
            //{
            //    directions.Add("West");
            //}

            //if (_playerCol <= 4)
            //{
            //    directions.Add("East");

            //Fixed the above code. Was adding directions to list even when player was on the border of the map. Now checks if player is on the border before adding direction to list.
            if (_playerRow > 0) directions.Add("North");  // room exists above
            if (_playerRow < 3) directions.Add("South");  // room exists below
            if (_playerCol > 0) directions.Add("West");   // room exists to the left
            if (_playerCol < 3) directions.Add("East");   // room exists to the right

            return directions;
        }

        public bool MovePlayer(string direction)
        {
            //Checks if the direction the player wants to move in is in the list of available directions. If it is, moves player in that direction and returns true. If not, returns false.
            var availableDirections = GetAvailableDirections();
            if (availableDirections.Contains(direction))
            {
                switch (direction)
                {
                    case "North":
                        _playerRow--;
                        break;
                    case "South":
                        _playerRow++;
                        break;
                    case "West":
                        _playerCol--;
                        break;
                    case "East":
                        _playerCol++;
                        break;
                }
                return true;
            }
            return false;
        } // Will need to create a sensitivity check for player input in main script to ensure they are entering directions in the correct format.
          // Could also add a method here to convert player input to the correct format if they enter it in a different way.
    }
}
