using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deliverable7 {
    /// <summary>
    /// Map.cs
    /// CS 1182
    /// Written By: Katherine Wilsdon
    /// 16 April 2019
    /// Jon Holmes
    /// Description - A map contains all the map cells for the game
    /// </summary>
    [Serializable]
    public class Map {
        #region Attributes
        private MapCell[,] _GameBoard;
        private List<Item> _Items;
        private List<Monster> _Monsters;
        private Hero _Adventurer;
        // field variables for FillMap
        private Random random;
        int[,] randNumArray;
        int randNum;
        #endregion

        #region Properties
        /// <summary>
        /// Gets all the MapCells that make up the game board
        /// </summary>
        public MapCell[,] GameBoard {
            get { return _GameBoard; }
        }

        /// <summary>
        /// Gets all possible potions and weapons in the game
        /// </summary>
        private List<Item> Items {
            get { return _Items;}
        }

        /// <summary>
        /// Gets all possible Monsters in the game
        /// </summary>
        private List<Monster> Monsters {
            get { return _Monsters; }
        }

        /// <summary>
        /// Get the hero in the game
        /// </summary>
        public Hero Adventurer {
            get { return _Adventurer; }
            set { _Adventurer = value; }
        }

        /// <summary>
        /// Returns the hit points of the monster who has the most in the list of monsters
        /// </summary>
        public int MostHitPointsInMonsters {
            get {
                int mostHitPoints = _Monsters.Max(monster => monster.HitPoints);
                return mostHitPoints;
            }
        }

        /// <summary>
        /// Returns the least affect value of the weapons in the items list
        /// </summary>
        /// <remarks>
        /// Using ToList: https://stackoverflow.com/questions/4256329/cannot-implicitly-convert-type-system-collections-generic-ienumerableanonymous
        /// </remarks>
        public int LeastDamagingWeapon {
            get {
                List<Item> weapons = new List<Item>();
                weapons = (from item in _Items where item.GetType() == typeof(Weapon) select item).ToList();
                int leastAffectValue = weapons.Min(weapon => weapon.AffectValue);
                return leastAffectValue;
            }
        }

        /// <summary>
        /// Returns the average affect value of the potions in the items list
        /// </summary>
        public double AverageHealingofPotions {
            get {
                List<Item> potions = new List<Item>();
                potions = (from item in _Items where item.GetType() == typeof(Potion) select item).ToList();
                double avgAffectValue = potions.Average(potion => potion.AffectValue);
                return avgAffectValue;
            }
        }

        /// <summary>
        /// Counts the number of monsters on the game board
        /// </summary>
        public int CountOfMonstersInMap {
            get {
                int count = 0;
                foreach (MapCell mapCell in _GameBoard) {
                    if (mapCell.Monster != null) {
                        count++;
                    }
                }
                return count;
            }
        }

        /// <summary>
        /// Counts the number of items on the game board
        /// </summary>
        public int CountOfItemsInMap {
            get {
                int count = 0;
                foreach (MapCell mapCell in _GameBoard) {
                    if (mapCell.Item != null) {
                        count++;
                    }
                }
                return count;
            }
        }

        /// <summary>
        /// Returns the percentage of how much of the map has been discovered
        /// </summary>
        public double PercentOfDiscoveredMapCells {
            get {
                int count = 0;
                for (int i = 0; i < _GameBoard.GetLength(0); i++) {
                    for (int j = 0; j < _GameBoard.GetLength(1); j++) {
                        if (_GameBoard[i,j].IsDiscovered == true) {
                            count++;
                        }
                    }                   
                }
                return (double)count/_GameBoard.Length;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a game board with the given rows and columns, and adds monsters, items, and map cells to the game board
        /// </summary>
        /// <param name="numOfRows">the number of rows containing MapCells for the game board</param>
        /// <param name="numOfColumns">the number of columns containing MapCells for the game board</param>
        public Map(int numOfRows, int numOfColumns) {
            _GameBoard = new MapCell[numOfRows, numOfColumns];
            FillMonsters();
            FillItems();
            FillMap();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Create the monsters in the monsters array
        /// </summary>
        private void FillMonsters() {
            _Monsters = new List<Monster>();   
            _Monsters.Add(new Monster("Skeleton", "your ancestor", 5, 15, 50, 50, 5));            
            _Monsters.Add(new Monster("Giant", "with one eye", 10, 100, 30, 30, 8));
            _Monsters.Add(new Monster("Dragon", "with fire breath", 7, 50, 20, 20, 7));
            _Monsters.Add(new Monster("Slug", "the slowest", 3, 3, 40, 40, 2));
            _Monsters.Add(new Monster("Goblin", "the malicious", 4, 10, 10, 10, 4));           
        }

        /// <summary>
        /// Creates the items in the items array
        /// </summary>
        private void FillItems() {
            _Items = new List<Item>();
            FillPotions();
            FillWeapons();
        }

        /// <summary>
        /// Creates the potions in the items array
        /// </summary>
        private void FillPotions() {
            _Items.Add(new Potion("Small Healing Potion", 5, Potion.Color.Yellow));
            _Items.Add(new Potion("Medium Healing Potion", 10, Potion.Color.Green));
            _Items.Add(new Potion("Large Healing Potion", 20, Potion.Color.Blue));
            _Items.Add(new Potion("Extreme Healing Potion", 30, Potion.Color.Purple));
        }

        /// <summary>
        /// Creates the weapons in the items array
        /// </summary>
        private void FillWeapons() {
            _Items.Add(new Weapon("Battle Axe", 25, 5));
            _Items.Add(new Weapon("Club", 15, 3));
            _Items.Add(new Weapon("Sword", 30, 7));
            _Items.Add(new Weapon("Dagger", 20, 4));
        }

        /// <summary>
        /// Creates the map cells containing an item, door, or doorkey in the gameboard array
        /// </summary>
        /// <remarks>
        /// Seed generator: https://stackoverflow.com/questions/1785744/how-do-i-seed-a-random-class-to-avoid-getting-duplicate-random-values
        /// </remarks>
        private void FillMap() {
            // A random number generator
            random = new Random(Guid.NewGuid().GetHashCode());
            // A random number array for each cell in the game board
            randNumArray = new int[_GameBoard.GetLength(0), _GameBoard.GetLength(1)];

            CreateMapCellsInGameBoard();
            PopulateMapCellsWithItems();
            PopulateMaCellsWithMonsters();
            InsertDoorAndDoorKey();
        }

        /// <summary>
        ///  Creates a map cell for each cell in the game board and populates the random number array to determine which cells have an item and a monster
        /// </summary>
        private void CreateMapCellsInGameBoard() {
            for (int i = 0; i < _GameBoard.GetLength(0); i++) {
                for (int j = 0; j < _GameBoard.GetLength(1); j++) {
                    // Create a map cell for each cell in the game board
                    _GameBoard[i, j] = new MapCell();
                    // Generate a random number between 0 and 8 and populate the random number array with the generated number
                    // Items and Monsters will have a 1/9th probability (11.11%) of populating the map cell 
                    randNum = random.Next(9);
                    randNumArray[i, j] = randNum;
                }
            }
        }

        /// <summary>
        /// Choose item randomly when the random number array equal 0, and populate the map cell with that item
        /// </summary>
        private void PopulateMapCellsWithItems() {
            for (int i = 0; i < _GameBoard.GetLength(0); i++) {
                for (int j = 0; j < _GameBoard.GetLength(1); j++) {
                    // if the random number equals zero, an item is chosen randomly
                    if (randNumArray[i, j] == 0) {
                        randNum = random.Next(8);
                        // if the item is a potion, set the map cell to have a deep copy of the potion
                        if (_Items[randNum].GetType() == typeof(Potion)) {
                            Potion aPotion = (Potion)_Items[randNum];
                            _GameBoard[i, j].Item = (Item)aPotion.CreateCopy();
                        } // // if the item is a weapon, set the map cell to have a deep copy of the weapon
                        else if (_Items[randNum].GetType() == typeof(Weapon)) {
                            Weapon aWeapon = (Weapon)_Items[randNum];
                            _GameBoard[i, j].Item = (Item)aWeapon.CreateCopy();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// If the random number equals one, an monster is chosen randomly to populate the map cell
        /// </summary>
        private void PopulateMaCellsWithMonsters() {
            for (int i = 0; i < _GameBoard.GetLength(0); i++) {
                for (int j = 0; j < _GameBoard.GetLength(1); j++) {
                    //when the random number equals one, an monster is chosen randomly
                    if (randNumArray[i, j] == 1) {
                        randNum = random.Next(_Monsters.Count);
                        Monster aMonster = _Monsters[randNum];
                        _GameBoard[i, j].Monster = (Monster)aMonster.CreateCopy();
                    }
                }
            }
        }

        /// <summary>
        /// Finds a location for a door and door key
        /// </summary>
        private void InsertDoorAndDoorKey() {
            int randomRow;
            int randomColumn;
            // Randomly places a single door key in the map cell where there is not an item
            do {
                randomRow = random.Next(_GameBoard.GetLength(0));
                randomColumn = random.Next(_GameBoard.GetLength(1));
                // if the map cell does not have an item, door, or monster, create a door key 
                if (_GameBoard[randomRow, randomColumn].Item == null && _GameBoard[randomRow, randomColumn].Door == null &&
                    _GameBoard[randomRow, randomColumn].Monster == null) {
                    _GameBoard[randomRow, randomColumn].DoorKey = new DoorKey("Golden Key", 20, "12345");
                }
            } // repeat until a map cell that does not contain a item, door, or monster is found            
            while (_GameBoard[randomRow, randomColumn].Item != null || _GameBoard[randomRow, randomColumn].Door != null ||
                _GameBoard[randomRow, randomColumn].Monster != null);

            // Randomly places a door in the map cell where there is not an item or a door
            do {
                randomRow = random.Next(_GameBoard.GetLength(0));
                randomColumn = random.Next(_GameBoard.GetLength(1));
                // if the map cell does not have an item, door key, or monster, create a door 
                if (_GameBoard[randomRow, randomColumn].Item == null && _GameBoard[randomRow, randomColumn].DoorKey == null &&
                    _GameBoard[randomRow, randomColumn].Monster == null) {
                    _GameBoard[randomRow, randomColumn].Door = new Door("Brown Door", 20, "12345");
                }
            } // repeat until a map cell that does not contain a item, door key, or monster is found 
            while (_GameBoard[randomRow, randomColumn].Item != null || _GameBoard[randomRow, randomColumn].DoorKey != null ||
                _GameBoard[randomRow, randomColumn].Monster != null);
        }

        /// <summary>
        /// Checks the location of the hero and returns a MapCell of the location
        /// </summary>
        /// <param name="hero">the hero whose location is being checked</param>
        /// <returns>Returns the MapCell where the hero is located or a new MapCell if the hero is not on the board</returns>
        public MapCell CurrentPositionOfHero(Hero hero) {
            // If a hero has been initialized, return the MapCell at the hero's position
            if (hero.XCoordinate != -1 && hero.YCoordinate != -1) {
                return _GameBoard[hero.XCoordinate, hero.YCoordinate];
            } // If a hero is not on the board, return a new MapCell 
            else {
                return new MapCell();
            }
        }

        /// <summary>
        /// Moves the actor in a direction without going off the board and returns true if that map cell has an item or a monster
        /// </summary>
        /// <param name="movement">direction the actor wants to move</param>
        /// <param name="actor">the actor that wants to move</param>
        /// <returns></returns>
        public bool Move(Actor.Direction movement, Actor actor) {
            int maxRow = _GameBoard.GetUpperBound(0);
            int maxCol = _GameBoard.GetUpperBound(1);

            // Move Left
            if(movement == Actor.Direction.Left) {
                if( actor.YCoordinate - 1 >= 0) {
                    actor.Move(Actor.Direction.Left);
                    _GameBoard[actor.XCoordinate, actor.YCoordinate + 1].IsDiscovered = true;
                }
            }
            // Move Right
            if (movement == Actor.Direction.Right) {
                if (actor.YCoordinate + 1 <= maxCol) {
                    actor.Move(Actor.Direction.Right);
                    _GameBoard[actor.XCoordinate, actor.YCoordinate - 1].IsDiscovered = true;
                }
            }
            // Move Up
            if (movement == Actor.Direction.Up) {
                if (actor.XCoordinate - 1 >= 0) {
                    actor.Move(Actor.Direction.Up);
                    _GameBoard[actor.XCoordinate + 1, actor.YCoordinate].IsDiscovered = true;
                }
            }
            // Move Down
            if (movement == Actor.Direction.Down) {
                if (actor.XCoordinate + 1 <= maxRow) {
                    actor.Move(Actor.Direction.Down);
                    _GameBoard[actor.XCoordinate - 1, actor.YCoordinate].IsDiscovered = true;
                }
            }
            // Checks if the new location requires the hero to act if the location contains a Item, Monster, DoorKey, or Door
            if (_GameBoard[actor.XCoordinate, actor.YCoordinate].Item != null ||
                _GameBoard[actor.XCoordinate, actor.YCoordinate].Monster != null ||
                _GameBoard[actor.XCoordinate, actor.YCoordinate].DoorKey != null ||
                _GameBoard[actor.XCoordinate, actor.YCoordinate].Door != null) {
                return true;
            } else {
                return false;
            }
        }
        #endregion
    }
}
