using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deliverable7 {
    /// <summary>
    /// Door.cs
    /// CS 1182
    /// Written By: Katherine Wilsdon
    /// 16 April 2019
    /// Jon Holmes
    /// Description - A door that the hero can unlock with the correct key
    /// </summary>
    [Serializable]
    public class Door : Item {
        #region Attributes
        private string _Code;
        #endregion

        #region Properties
        /// <summary>
        /// Get the code to match a door key
        /// </summary>
        public string Code {
            get { return _Code; }
        }
        #endregion

        #region Overload Constructor
        /// <summary>
        /// Creates a door
        /// </summary>
        /// <param name="name">the door's name</param>
        /// <param name="affectValue">the affect value of the door</param>
        /// <param name="code">the code that matches the door key</param>
        public Door(string name, int affectValue, string code) : base(name, affectValue){
            _Code = code;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Determine whether the door key matches the door
        /// </summary>
        /// <param name="key">the door key that may match the door</param>
        /// <returns>whether the door key and door have the same code</returns>
        public bool DoesDoorKeyMatch (DoorKey key) {
            if (key.Code == Code) {
                return true;
            } else {
                return false;
            }
        }
        #endregion
    }
}
