using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deliverable7 {
    /// <summary>
    /// DoorKey.cs
    /// CS 1182
    /// Written By: Katherine Wilsdon
    /// 16 April 2019
    /// Jon Holmes
    /// Description - A door key that can be use to unlock a door by a hero
    /// </summary>
    [Serializable]
    public class DoorKey : Item {
        #region Attributes
        private string _Code;
        #endregion

        #region Properties
        /// <summary>
        /// Get the code to match a door
        /// </summary>
        public string Code {
            get { return _Code; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a door key
        /// </summary>
        /// <param name="name">the name of the door key</param>
        /// <param name="affectValue">affect value of the door key</param>
        /// <param name="code">the code to match a door</param>
        public DoorKey (string name, int affectValue, string code) : base(name,affectValue){
            _Code = code;
        }

        #endregion
    }
}
