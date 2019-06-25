using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deliverable7 {
    /// <summary>
    /// Weapon.cs
    /// CS 1182
    /// Written By: Katherine Wilsdon
    /// 16 April 2019
    /// Jon Holmes
    /// Description - A weapon is carried by a hero
    /// </summary>
    [Serializable]
    public class Weapon : Item, IRepeatable<Weapon> {
        #region Attributes
        private int _AttackSpeed;
        #endregion

        #region Properties
        /// <summary>
        /// Gets how much the attack speed of the hero will decrease by when equipping the weapon
        /// </summary>
        public int AttackSpeed {
            get { return _AttackSpeed; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a weapon
        /// </summary>
        /// <param name="name">the name of the item</param>
        /// <param name="affectValue">the affect value on the actor's hit points</param>
        /// <param name="attackSpeed">the amount the hero's attack speed will decrease by when equipping the weapon</param>
        public Weapon(string name, int affectValue, int attackSpeed) : base (name, affectValue) {
            _AttackSpeed = attackSpeed;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Creates a deep copy of the weapon
        /// </summary>
        /// <param name="weapon">the weapon to copy</param>
        /// <returns>a copy of the weapon</returns>
        public Weapon CreateCopy() {
            Weapon aWeapon = new Weapon(this.Name,this.AffectValue, this.AttackSpeed);
            return aWeapon;
        }
        #endregion
    }
}
