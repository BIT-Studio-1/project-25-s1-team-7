using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            _mapGrid[0, 0] = new Room("Room name here", "Room description here", 1);
            _mapGrid[0, 1] = new Room("Room name here", "Room description here", 1);
            _mapGrid[0, 2] = new Room("Room name here", "Room description here", 1);

            _mapGrid[1, 0] = new Room("Room name here", "Room description here", 1);
            _mapGrid[1, 1] = new Room("Room name here", "Room description here", 1);
            _mapGrid[1, 2] = new Room("Room name here", "Room description here", 1);

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
            if (_playerRow >= 0)
            {
                directions.Add("North");
            }

            if (_playerRow <= 4)
            {
                directions.Add("South");
            }

            if (_playerCol >= 0)
            {
                directions.Add("West");
            }

            if (_playerCol <= 4)
            {
                directions.Add("East");
            } 
            return directions;
        }
    }
}
