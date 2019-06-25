using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Deliverable7 {
    /// <summary>
    /// Game.cs
    /// CS 1182
    /// Written By: Katherine Wilsdon
    /// 16 April 2019
    /// Jon Holmes
    /// Description - Keeps track of the game
    /// </summary>
    [Serializable]
    public static class Game {

        #region Attributes
        private static GameState _GameState;
        private static Map _Map;
        private static int _Height = 10;
        private static int _Width = 10;
        #endregion

        #region Enumerator
        /// <summary>
        /// What the state of the game is
        /// </summary>
        public enum GameState { Running, Lost, Won };
        #endregion

        #region Properties
        /// <summary>
        /// Get and set the game state of the game
        /// </summary>
        public static GameState TheGameState {
            get {
                if (Game.Map.Adventurer.IsAlive == false) {
                    _GameState = GameState.Lost;
                    return _GameState;
                } else {
                    return _GameState;
                }
                
            }
            set { _GameState = value; }
        }

        /// <summary>
        /// Get the Map of the game
        /// </summary>
        public static Map Map {
            get { return _Map; }
            set { _Map = value; }
        }

        /// <summary>
        /// Get and set height of the game board
        /// </summary>
        public static int Height {
            get { return _Height; }
            set { _Height = value; }
        }

        /// <summary>
        /// Get and set the width of the game board
        /// </summary>
        public static int Width {
            get { return _Width; }
            set { _Width = value; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Resets the game by creating a new map, a new hero, and set the game state to running
        /// </summary>
        /// <param name="rows">the number of rows in the map</param>
        /// <param name="columns">the number of columns in the map</param>
        public static void ResetGame(int rows, int columns) {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            TheGameState = GameState.Running;
            _Map = new Map(rows, columns);

            // Randomly places a hero in the map cell where there is not an item, door, or door key
            int randomRow;
            int randomColumn;
            do {
                randomRow = random.Next(_Map.GameBoard.GetLength(0));
                randomColumn = random.Next(_Map.GameBoard.GetLength(1));
                // if the map cell does not have an item, door, or door key, create a hero located in this map cell
                if (_Map.GameBoard[randomRow, randomColumn].Item == null && _Map.GameBoard[randomRow, randomColumn].Door == null && 
                    _Map.GameBoard[randomRow, randomColumn].DoorKey == null && _Map.GameBoard[randomRow, randomColumn].Monster == null) {
                    Game.Map.Adventurer = new Hero("kevin", "the magnificent", 10, 100, randomRow, randomColumn);
                }
            } // repeat until a map cell that does not contain a item, door, or door key is found 
            while (_Map.GameBoard[randomRow, randomColumn].Item != null || _Map.GameBoard[randomRow, randomColumn].Door != null || 
                Map.GameBoard[randomRow, randomColumn].DoorKey != null || Map.GameBoard[randomRow, randomColumn].Monster != null);           
        }

        /// <summary>
        /// Uses the preset height and width of on the game class
        /// </summary>
        public static void ResetGame() {
            ResetGame(Height, Width);
        }
            #endregion
        }
}
