using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Deliverable7 {
    /// <summary>
    /// frmItem.xaml.cs
    /// CS 1182
    /// Written By: Katherine Wilsdon
    /// 16 April 2019
    /// Jon Holmes
    /// Description - Asks the user if they want the item in the map cell
    /// </summary>
    public partial class frmItem : Window {
        public frmItem() {
            InitializeComponent();
            MapCell currentLocationOfHero = Game.Map.CurrentPositionOfHero(Game.Map.Adventurer);
            // Display the correct weapon image
            if (currentLocationOfHero.Item != null) {
                tbOutputItem.Text = " Do you want a " + currentLocationOfHero.Item.Name + "?";
                if (currentLocationOfHero.Item.GetType() == typeof(Weapon)) {
                    if (currentLocationOfHero.Item.Name == "Battle Axe") {
                        imgItem.Source = new BitmapImage(new Uri("Images/BattleAxe.png", UriKind.RelativeOrAbsolute));
                    } else if (currentLocationOfHero.Item.Name == "Club") {
                        imgItem.Source = new BitmapImage(new Uri("Images/Club.png", UriKind.RelativeOrAbsolute));
                    } else if (currentLocationOfHero.Item.Name == "Sword") {
                        imgItem.Source = new BitmapImage(new Uri("Images/Sword.png", UriKind.RelativeOrAbsolute));
                    } else if (currentLocationOfHero.Item.Name == "Dagger") {
                        imgItem.Source = new BitmapImage(new Uri("Images/Dagger.png", UriKind.RelativeOrAbsolute));
                    }
                }
            // Display the correct potion image
                if (currentLocationOfHero.Item.GetType() == typeof(Potion)) {
                    if (currentLocationOfHero.Item.Name == "Small Healing Potion") {
                        imgItem.Source = new BitmapImage(new Uri("Images/SmallPotion.jpg", UriKind.RelativeOrAbsolute));
                    } else if (currentLocationOfHero.Item.Name == "Medium Healing Potion") {
                        imgItem.Source = new BitmapImage(new Uri("Images/MediumPotion.png", UriKind.RelativeOrAbsolute));
                    } else if (currentLocationOfHero.Item.Name == "Large Healing Potion") {
                        imgItem.Source = new BitmapImage(new Uri("Images/LargePotion.png", UriKind.RelativeOrAbsolute));
                    } else if (currentLocationOfHero.Item.Name == "Extreme Healing Potion") {
                        imgItem.Source = new BitmapImage(new Uri("Images/ExtremePotion.png", UriKind.RelativeOrAbsolute));
                    }
                }
            } //if MapCell has a door key
            else if (currentLocationOfHero.DoorKey != null) {
                tbOutputItem.Text = " Do you want a " + currentLocationOfHero.DoorKey.Name + "?";
                imgItem.Source = new BitmapImage(new Uri("Images/Key.png", UriKind.RelativeOrAbsolute));                
            }
            ShowDialog();
        }

        /// <summary>
        /// Applies the item to the hero, drops previous item in current location, and closes the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnYes_Click(object sender, RoutedEventArgs e) {
            MapCell currentLocationOfHero = Game.Map.CurrentPositionOfHero(Game.Map.Adventurer); 
            // Hero applies item and drops previous item
            if (currentLocationOfHero.Item != null) {
                Item item = Game.Map.Adventurer.HeroAppliesItem(currentLocationOfHero.Item);
                currentLocationOfHero.Item = item;
            } // Hero equips the doorkey
            else if (currentLocationOfHero.DoorKey != null) {
                DoorKey doorKey = (DoorKey)Game.Map.Adventurer.HeroAppliesItem(currentLocationOfHero.DoorKey);
                currentLocationOfHero.DoorKey = doorKey;
            }
            
            this.Close();
        }

        /// <summary>
        /// Close the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNo_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}
