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
    /// frmMonster.xaml.cs
    /// CS 1182
    /// Written By: Katherine Wilsdon
    /// 16 April 2019
    /// Jon Holmes
    /// Description - Displays a fight with a monster. The hero can attack or run away.
    /// </summary>
    public partial class frmMonster : Window {
        public frmMonster() {
            InitializeComponent();
            MapCell currentLocationOfHero = Game.Map.CurrentPositionOfHero(Game.Map.Adventurer);
            tbOutputMonster.Text = "You encountered a " + currentLocationOfHero.Monster.Name + ".";
            // Displays the correct monster image
            if (currentLocationOfHero.Monster.Name == "Skeleton") {
                imgMonster.Source = new BitmapImage(new Uri("Images/Skeleton.png", UriKind.RelativeOrAbsolute));
            } else if (currentLocationOfHero.Monster.Name == "Giant") {
                imgMonster.Source = new BitmapImage(new Uri("Images/Giant.png", UriKind.RelativeOrAbsolute));
            } else if (currentLocationOfHero.Monster.Name == "Dragon") {
                imgMonster.Source = new BitmapImage(new Uri("Images/Dragon.png", UriKind.RelativeOrAbsolute));
            } else if (currentLocationOfHero.Monster.Name == "Slug") {
                imgMonster.Source = new BitmapImage(new Uri("Images/Slug.png", UriKind.RelativeOrAbsolute));
            } else if (currentLocationOfHero.Monster.Name == "Goblin") {
                imgMonster.Source = new BitmapImage(new Uri("Images/Goblin.png", UriKind.RelativeOrAbsolute));
            }
            // Displays the hero image and statistics
            imgHero.Source = new BitmapImage(new Uri("Images/Hero.png", UriKind.RelativeOrAbsolute));
            tbOutputMonster.Text = "What would you like to do?";
            tbOutputHeroStatistics.Text = Game.Map.Adventurer.HitPoints + "/" + Game.Map.Adventurer.MaxHitPoints;
            tbOutputMonsterStatistics.Text = currentLocationOfHero.Monster.HitPoints + "/" + currentLocationOfHero.Monster.MaxHitPoints;
            ShowDialog();
        }    
        
        /// <summary>
        /// Set the GameState to Lost
        /// </summary>
        private void HeroLoses() {
            foreach (Window window in Application.Current.Windows) {
                if (window.GetType() == typeof(MainWindow)) {
                    (window as MainWindow).CurrentGameState(Game.GameState.Lost);
                }
            }
        }

        /// <summary>
        /// The hero attacks the monster until either the monster or hero dies
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAttack_Click(object sender, RoutedEventArgs e) {
            MapCell currentLocationOfHero = Game.Map.CurrentPositionOfHero(Game.Map.Adventurer);
            bool isHeroALive = Game.Map.Adventurer + currentLocationOfHero.Monster;
            bool isMonsterAlive = currentLocationOfHero.Monster.IsAlive;
            // if both the monster and hero are alive, update the statistics
            if (isHeroALive && isMonsterAlive) {
                tbOutputMonster.Text = "What would you like to do?";
                tbOutputHeroStatistics.Text = Game.Map.Adventurer.HitPoints + "/" + Game.Map.Adventurer.MaxHitPoints;
                tbOutputMonsterStatistics.Text = currentLocationOfHero.Monster.HitPoints + "/" + currentLocationOfHero.Monster.MaxHitPoints;
            } // if the monster dies, delete the monster and close the form 
            else if (isMonsterAlive == false) {
                currentLocationOfHero.Monster = null;
                this.Close();
            } // if the hero dies, set the GameState to Lost and close the form
            else if (isHeroALive == false){
                this.Close();
                HeroLoses();
            }
        }

        /// <summary>
        /// The hero runs away from the monster
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRunAway_Click(object sender, RoutedEventArgs e) {
            MapCell currentLocationOfHero = Game.Map.CurrentPositionOfHero(Game.Map.Adventurer);
            Game.Map.Adventurer.IsRunningAway = true;
            bool isHeroALive = Game.Map.Adventurer + currentLocationOfHero.Monster;
            Game.Map.Adventurer.IsRunningAway = false;
            // if the hero dies, set the GameState to Lost and close the form
            if (isHeroALive == false) {
                this.Close();
                HeroLoses();
            }
            this.Close();
        }
    }
}
