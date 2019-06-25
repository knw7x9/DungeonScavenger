using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Deliverable7 {
    /// <summary>
    /// Actor.cs
    /// CS 1182
    /// Written By: Katherine Wilsdon
    /// 16 April 2019
    /// Jon Holmes
    /// Description - Actors are characters that can be attacked or can attack other actors
    /// </summary>
    [Serializable]
    abstract public class Actor {

        #region Attributes
        private string _Name;
        private string _Title;
        private int _AttackSpeed;
        private int _HitPoints;
        private int _MaxHitPoints;
        private int _XCoordinate = -1;
        private int _YCoordinate = -1;
        #endregion

        #region Enumerator
        public enum Direction { Left, Right, Up, Down};
        #endregion

        #region Properties
        /// <summary>
        /// Get and set the name of the actor
        /// </summary>
        public string Name {
            get { return _Name; }
            set {
                _Name = CapitalizeWord(value);
            }
        }

        /// <summary>
        /// Get and set the title of the actor
        /// </summary>
        public string Title {
            get { return _Title; } 
            set {
                _Title = _Name + " " + TitleCase(value);
            }
        }

        /// <summary>
        /// Get and set the attack speed
        /// </summary>
        public virtual int AttackSpeed {
            get { return _AttackSpeed; }
            set { _AttackSpeed = value; }
        }

        /// <summary>
        /// Get hit points
        /// </summary>
        public int HitPoints {
            get { return _HitPoints; }            
        }

        /// <summary>
        /// Get and set maximum hit points 
        /// </summary>
        public int MaxHitPoints {
            get { return _MaxHitPoints; }
            set { _MaxHitPoints = value; }

        }
        /// <summary>
        /// Get whether the actor is alive
        /// </summary>
        public bool IsAlive {
            get {
                if (HitPoints > 0) {
                    return true;
                } else {
                    return false;
                }
            }
        }

        /// <summary>
        /// Get the x position of the actor
        /// </summary>
        public int XCoordinate {
            get { return _XCoordinate; }
        }

        /// <summary>
        /// Get the y position of the actor
        /// </summary>
        public int YCoordinate {
            get { return _YCoordinate; }
        }
        #endregion

        #region Constructors
        
        /// <summary>
        /// Creates an actor
        /// </summary>
        /// <param name="name">the actor's name</param>
        /// <param name="title">the actor's title</param>
        /// <param name="attackSpeed">the speed that the actor can attack at</param>
        /// <param name="hitPoints">the actor's hit points that determine whether the actor is alive or dead</param>
        /// <param name="xCoordinate">the x position of the actor</param>
        /// <param name="yCoordinate">the y position of the actor</param>
        public Actor(string name, string title, int attackSpeed, int hitPoints, int xCoordinate, int yCoordinate) {
            Name = name;
            Title = title;
            _AttackSpeed = attackSpeed;
            _HitPoints = hitPoints;
            _XCoordinate = xCoordinate;
            _YCoordinate = yCoordinate;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Capitalizes the first letter in the word
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        private string CapitalizeWord(string word) {
            string capitalized = word.ToUpper();
            return capitalized[0] + word.Substring(1).ToLower();
        }

        /// <summary>
        /// Converts the title into title case
        /// </summary>
        /// <remarks>
        /// Try Catch statement in a class: https://stackoverflow.com/questions/11236559/try-catch-in-every-method-of-every-class
        /// Title Case word list: The title case word list is according to APA guidline of 4 letter words or less https://www.grammarcheck.net/capitalization-in-titles-101/
        /// Join: https://stackoverflow.com/questions/4841401/convert-string-array-to-string and 
        ///       https://docs.microsoft.com/en-us/dotnet/api/system.string.join?view=netframework-4.7.2#System_String_Join_System_String_System_String___
        /// </remarks>
        /// <param name="phrase">the phase to convert into title case</param>
        /// <returns>the phase in title case</returns>
        private string TitleCase(string phrase) {
            // Split the phrase into an array containing each word
            string[] wordArray = phrase.Split(' ');
            
            string[] titleCaseWordList;
            // Create the word list
            try {
                titleCaseWordList = File.ReadAllLines("../../TitleCaseWords.txt");
            }
            catch (Exception e) {
                throw new Exception(e.Message);
            }
            finally { }

            // Check whether the word in the word array is in the word list or not
            for (int i = 0; i < wordArray.Length; i++) {
                bool isInWordList = false;
                for (int j = 0; j < titleCaseWordList.Length; j++) {
                    // if the word is in the word list, make lower case
                    if (wordArray[i].ToLower() == titleCaseWordList[j]) {
                        wordArray[i] = wordArray[i].ToLower();
                        isInWordList = true;
                        break;
                    }
                }
                // if the word is not in the wordlist, capitalize the first letter in the word
                if (isInWordList == false) {
                    wordArray[i] = CapitalizeWord(wordArray[i]);
                }
            }
            return string.Join(" ", wordArray);
        }

        /// <summary>
        /// Damage the actor by a given number of points
        /// </summary>
        /// <param name="damagePoints">the points that the actor's hit points  will decrease by</param>
        public void Damaged(int damagePoints) {
            if (HitPoints - damagePoints > 0) {
                _HitPoints -= damagePoints;
            } else {
                _HitPoints = 0;
            }

        }

        /// <summary>
        /// Heal the actor by a given number of points
        /// </summary>
        /// <param name="affectValue">the points that the actor's hit points  will increase by</param>
        public void Healed(int affectValue) {
            if (HitPoints + affectValue <= MaxHitPoints && HitPoints != 0) {
                _HitPoints += affectValue;
            } else if (HitPoints + affectValue > MaxHitPoints) {
                _HitPoints = MaxHitPoints;
            }
        }

        /// <summary>
        /// Increments the x or y coordinate by 1
        /// </summary>
        /// <param name="movement">a direction enumerator</param>
        public virtual void Move(Direction movement) { 
            // Move Left
            if (movement == Direction.Left) {
                _YCoordinate--;               
            } 
            // Move Right
            if (movement == Direction.Right) {               
                _YCoordinate++;
                
            }
            // Move Up
            if (movement == Direction.Up) {              
                _XCoordinate--;
            }
            // Move Down
            if (movement == Direction.Down) {
                _XCoordinate++;
            }
        }
    }
    #endregion
}

