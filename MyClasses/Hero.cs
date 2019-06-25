using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deliverable7 {
    /// <summary>
    /// Hero.cs
    /// CS 1182
    /// Written By: Katherine Wilsdon
    /// 16 April 2019
    /// Jon Holmes
    /// Description - A hero is an actor that can have a weapon to defend against an attack 
    /// </summary>
    [Serializable]
    public class Hero : Actor, ICombat {

        #region Attributes
        private Weapon _EquippedWeapon;
        private bool _IsRunningAway;
        private int _AttackDamage;
        private DoorKey _EquippedDoorKey;
        #endregion

        #region Properties
        /// <summary>
        /// Get the hero's equipped a weapon
        /// </summary>
        public Weapon EquippedWeapon {
            get { return _EquippedWeapon; }   
            private set { _EquippedWeapon = value; }
        }

        /// <summary>
        /// Gets whether the hero has a weapon
        /// </summary>
        public bool HasWeapon {
            get {
                if (EquippedWeapon != null) {
                    return true;
                } else {
                    return false;
                }
            }
        }

        /// <summary>
        /// Subtacts the hero's speed by the weapons attack speed modifier
        /// </summary>
        /// <remarks>
        /// Referring to base (base.Speed): https://stackoverflow.com/questions/5108319/c-sharp-can-a-base-class-property-be-invoked-from-derived-class
        /// </remarks>
        public override int AttackSpeed {
            get {
                if (EquippedWeapon != null) {
                    return base.AttackSpeed - EquippedWeapon.AttackSpeed;
                } else {
                    return base.AttackSpeed;
                }
            }           
        }

        /// <summary>
        /// Get and set if the hero is running away from a monster
        /// </summary>
        public bool IsRunningAway {
            get { return _IsRunningAway; }
            set { _IsRunningAway = value; }
        }

        /// <summary>
        /// Read-only property to get the the Attack Damage of the hero depending on whether he/she is equipped with a weapon or not
        /// </summary>
        public int AttackDamage {
            get {
                if (HasWeapon == true) {
                    return EquippedWeapon.AffectValue;
                } else {
                    return 1;
                }
            }
        }

        /// <summary>
        /// Whether the hero has a key or not
        /// </summary>
        public bool HasKey {
            get {
                if (EquippedDoorKey != null) {
                    return true;
                } else {
                   return false;
                }
            }
           
        }

        /// <summary>
        /// Get whether the hero has equipped a door key
        /// </summary>
        public DoorKey EquippedDoorKey {
            get { return _EquippedDoorKey; }
            private set { _EquippedDoorKey = value; }
        }
        #endregion

        #region Constuctors
        /// <summary>
        /// Creates a hero
        /// </summary>
        /// <param name="name">the hero's name</param>
        /// <param name="title">the hero's title</param>
        /// <param name="attackSpeed">the speed that the hero can attack at</param>
        /// <param name="hitPoints">the hero's hit points that determine whether the actor is alive or dead</param>
        /// <param name="xCoordinate">the x position of the hero</param>
        /// <param name="yCoordinate">the y position of the hero</param>
        public Hero(string name, string title, int attackSpeed, int hitPoints, int xCoordinate, int yCoordinate):
            base(name, title, attackSpeed, hitPoints, xCoordinate, yCoordinate){
            MaxHitPoints = hitPoints;
        }

        /// <summary>
        /// When a hero and monster meet, the monster and/or the hero attack each other and returns whether the hero survived or died
        /// </summary>
        /// <param name="hero">the hero meeting the monster</param>
        /// <param name="monster">the monster meeting the hero</param>
        /// <returns>whether the hero survived or died</returns>
        public static bool operator +(Hero hero, Monster monster) {
            bool isHeroAlive = true;
            bool isMonsterAlive = true;
            //If the Hero’s AttackSpeed is greater and the Hero is running away,then the Monster does no damage and the Hero gets away undamaged.
            if (hero.IsRunningAway && (monster.AttackSpeed > hero.AttackSpeed)) {
                return isHeroAlive;
            } //If the Monster’s AttackSpeed is equal to or greater than the Hero’s and the Hero is running away, then the Monster damages the Hero.
            else if (hero.IsRunningAway && (monster.AttackSpeed >= hero.AttackSpeed)) {
                isHeroAlive = monster.Attack(hero);
                return isHeroAlive;
            } //If the Hero’s AttackSpeed is greater, then the Hero will damage the Monster. If the Monster is still alive, 
            //the Monster will damage the Hero.
            else if (hero.AttackSpeed > monster.AttackSpeed) {
                isMonsterAlive = hero.Attack(monster);
                if (isMonsterAlive) {
                    isHeroAlive = monster.Attack(hero);

                }
                return isHeroAlive;
            } //If the Monsters’ AttackSpeed is greater, then the Monster will damage the Hero. If the Hero is still alive, 
            //the Hero will damage the Monster. 
            else if (monster.AttackSpeed > hero.AttackSpeed) {
                isHeroAlive = monster.Attack(hero);
                if (isHeroAlive) {
                    isMonsterAlive = hero.Attack(monster);
                }
                return isHeroAlive;
            } // If the AttackSpeed of both the Hero and the Monster are equal, they will both damage each other.
            else {
                isMonsterAlive = hero.Attack(monster);
                isHeroAlive = monster.Attack(hero);
                return isHeroAlive;
            }
        }

        #endregion

        #region Methods
        // <summary>
        ///  Damages the actor's hit points by the hero's attack value
        /// </summary>
        /// <param name="actor">the actor that is being attacked</param>
        /// <returns>whether the actor is alive or dead</returns>
        public bool Attack(Actor actor) {
            actor.Damaged(AttackDamage);
            return IsAlive;
        }

        /// <summary>
        /// A hero applies an item 
        /// </summary>
        /// <param name="item">an item the hero obtained</param>
        /// <returns>an item or no item</returns>
        public Item HeroAppliesItem(Item item) {
            // If a potion, the hero is healed and an item is not returned. 
            if (item.GetType() == typeof(Potion)) {
                this.Healed(item.AffectValue);
                return null;
            } // if a weapon, equip the hero with the weapon and drop the previous weapon
            else if (item.GetType() == typeof(Weapon)) {
                Weapon temp = EquippedWeapon;
                EquippedWeapon = (Weapon)item;
                if (temp == null) {                    
                    return null;
                } else {
                    return temp;
                }
            } // if a door key, equip the hero with a door key and dorm the previous key 
            else if (item.GetType() == typeof(DoorKey)) {
                DoorKey temp = EquippedDoorKey;
                EquippedDoorKey = (DoorKey)item;
                if (temp == null) {
                    return null;
                } else {
                    return temp;
                }
            } // if any other type, return the item
            else {
                return item;
            }            
        }
        #endregion

    }
}
