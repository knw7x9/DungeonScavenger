using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deliverable7 {
    /// <summary>
    /// MapCell.cs
    /// CS 1182
    /// Written By: Katherine Wilsdon
    /// 16 April 2019
    /// Jon Holmes
    /// Description - Locations where an actor or item can be on the game board
    /// </summary>
    [Serializable]
    public class MapCell {
        #region Attributes       
        private bool _IsDiscovered = false;
        private Monster _Monster;
        private Item _Item;
        private DoorKey _DoorKey;
        private Door _Door;
        #endregion

        #region Properties
        /// <summary>
        /// Get whether the map cell has been discovered
        /// </summary>
        public bool IsDiscovered {
            get { return _IsDiscovered; }
            set { _IsDiscovered = value; }
        }       
        
        /// <summary>
        /// Get whether the map cell has a door key
        /// </summary>
        public bool HasDoorKey {
            get {
                if (_DoorKey != null) {
                    return true;
                } else {
                    return false;
                }
            }
        }
        /// <summary>
        /// Get whether the map cell has a door
        /// </summary>
        public bool HasDoor {
            get {
                if (_Door != null) {
                    return true;
                } else {
                    return false;
                }
            }
        }

        /// <summary>
        /// Get and set the monster of the map cell
        /// </summary>
        public Monster Monster {
            get { return _Monster; }
            set {
                if(_Item == null && _DoorKey == null && _Door == null) {
                    _Monster = value;
                }                
            }
        }

        /// <summary>
        /// Get and set the item of the map cell
        /// </summary>
        public Item Item {
            get { return _Item; }
            set {
                if (_Monster == null && _DoorKey == null && _Door == null) {
                    _Item = value;
                }
            }
        }

        /// <summary>
        /// Get and set a a door key in the map cell
        /// </summary>
        public DoorKey DoorKey {
            get { return _DoorKey; }
            set {
                if (_Monster == null && _Item == null && _Door == null) {
                    _DoorKey = value;
                }
            }
        }

        /// <summary>
        /// Get and set a door in the map cell
        /// </summary>
        public Door Door {
            get { return _Door; }
            set {
                if (_Monster == null && _Item == null && _DoorKey == null) {
                    _Door = value;
                }
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public MapCell() {
            _IsDiscovered = false;
            _Monster = null;
            _Item = null;
        }      
        #endregion
    }
}
