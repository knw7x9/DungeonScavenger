using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deliverable7 {
    /// <summary>
    /// Item.cs
    /// CS 1182
    /// Written By: Katherine Wilsdon
    /// 16 April 2019
    /// Jon Holmes
    /// Description - Items can heal or damage actors within the game
    /// </summary>
    [Serializable]
    abstract public class Item {

        #region Attributes
        private string _Name;
        private int _AffectValue;        
        #endregion

        #region Properties
        /// <summary>
        /// Get and set the name of the item
        /// </summary>
        public string Name {
            get { return _Name; } 
            set {               
                _Name = TitleCase(value);
            }
        }

        /// <summary>
        /// Get and set the affect value of the item
        /// </summary>
        public int AffectValue {
            get { return _AffectValue; }
            set { _AffectValue = value; }
        }      
        #endregion

        #region Constructors
        /// <summary>
        /// Creates an Item
        /// </summary>
        /// <param name="name">name of the item</param>
        /// <param name="value">affect value of the item</param>
        public Item(string name, int affectValue) {
            Name = name;
            AffectValue = affectValue;
        }     
        #endregion

        #region Methods
        /// <summary>
        /// Capitalize the first letter in the word
        /// </summary>
        /// <param name="word">the word to be capitalized</param>
        /// <returns>the first letter capitalized and the rest of the word lowercase</returns>
        private string CapitalizeWord(string word) {            
            string capitalized = word.ToUpper();
            return capitalized[0] + word.Substring(1).ToLower();
        }

        /// <summary>
        /// Capitalize the first letter in each word
        /// </summary>
        /// <param name="phrase">the name of the item</param>
        /// <returns></returns>
        private string TitleCase(string phrase) {
            string[] wordArray = phrase.Split(' ');
            for (int i = 0; i < wordArray.Length; i++) {
                wordArray[i] = CapitalizeWord(wordArray[i]);
            }
            return string.Join(" ", wordArray);
        }
        #endregion

    }
}
