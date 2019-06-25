using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deliverable7 {
    /// <summary>
    /// Potion.cs
    /// CS 1182
    /// Written By: Katherine Wilsdon
    /// 16 April 2019
    /// Jon Holmes
    /// Description - A potion can heal a hero
    /// </summary>
    [Serializable]
    public class Potion : Item, IRepeatable<Potion> {
        #region Attributes
        private Color _Color;
        #endregion

        #region Enumerator
        /// <summary>
        /// Color of the potion
        /// </summary>
        public enum Color { Yellow, Green, Blue, Purple };
        #endregion

        #region Properties
        /// <summary>
        /// Get and set the color of the potion
        /// </summary>
        public Color AColor {
            get { return _Color; }
            set { _Color = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ceates a potion
        /// </summary>
        /// <param name="name">the potion's name</param>
        /// <param name="value">the potion's affect value that can heal the hero</param>
        /// <param name="color">the potion's color</param>
        public Potion(string name, int affectValue, Color color) : base(name, affectValue) {
            AColor = color;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates a deep copy of the potion
        /// </summary>
        /// <remarks>
        /// Deep clone: https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone?view=netframework-4.7.2
        /// </remarks>
        /// <returns>a copy of the object</returns>
        public Potion CreateCopy() {
            Potion aPotion = new Potion(this.Name, this.AffectValue, this.AColor);
            return aPotion;
        }
        #endregion
    }
}
