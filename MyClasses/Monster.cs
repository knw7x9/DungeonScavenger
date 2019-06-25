using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deliverable7 {
    /// <summary>
    /// Monster.cs
    /// CS 1182
    /// Written By: Katherine Wilsdon
    /// 16 April 2019
    /// Jon Holmes
    /// Description - A monster is an actor that can attack other actors
    /// </summary>
    [Serializable]
    public class Monster : Actor, IRepeatable<Monster>, ICombat {
        #region Attributes
        private int _AttackValue;
        #endregion

        #region Properties
        /// <summary>
        /// Gets and sets the attack value of the monster
        /// </summary>
        public int AttackValue {
            get { return _AttackValue; }
            set { _AttackValue = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a monster with
        /// </summary>
        /// <param name="name">the monster's name</param>
        /// <param name="title">the monster's title</param>
        /// <param name="attackSpeed">the speed that the monster can attack at</param>
        /// <param name="hitPoints">the monster's hit points that determine whether the actor is alive or dead</param>
        /// <param name="xCoordinate">the x position of the monster</param>
        /// <param name="yCoordinate">the y position of the monster</param>
        /// <param name="attackValue">how many points of damage the monster can inflict on a hero</param>
        public Monster(string name, string title, int attackSpeed, int hitPoints, int xCoordinate, int yCoordinate, int attackValue) : 
            base(name, title, attackSpeed, hitPoints, xCoordinate, yCoordinate) {
            AttackValue = attackValue;
            MaxHitPoints = hitPoints;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Create a deep copy of a monster
        /// </summary>
        /// <param name="monster">the monster to be copied</param>
        /// <returns>a copy of the monster</returns>
        public Monster CreateCopy() {
            Monster aMonster = new Monster(this.Name, this.Title, this.AttackSpeed, this.HitPoints, this.XCoordinate, this.YCoordinate, this.AttackValue);
            return aMonster;
        }

        /// <summary>
        /// Damages the actor's hit points by the monster's attack value
        /// </summary>
        /// <param name="actor">the actor that is being attacked</param>
        /// <returns>whether the actor is alive or dead</returns>
        public bool Attack(Actor actor) {
            actor.Damaged(AttackValue);
            return actor.IsAlive;
        }
        #endregion

    }
}
