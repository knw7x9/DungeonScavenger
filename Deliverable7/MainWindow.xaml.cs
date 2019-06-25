using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;

namespace Deliverable7 {
    /// <summary>
    /// MainWindow.xaml.cs
    /// CS 1182
    /// Written By: Katherine Wilsdon
    /// 16 April 2019
    /// Jon Holmes
    /// Description - The hero navigates an unknown dungeon where he can discover weapons, potions, monsters, the key, or the door.
    /// Once the hero finds the key, the hero can exit through a door to win the game. The game can be saved or reset, and the user
    ///  can load a saved game.
    ///  Wow: Added images of the items and monsters to the location on the grid once discovered
    /// </summary>
    /// <remarks>
    /// Image Citations
    /// Skeleton: http://www.getcoloringpages.com/skeleton-coloring-pages 
    /// Slug: https://www.shareicon.net/cartoon-slug-225915 
    /// Dragon: https://all-free-download.com/free-vector/free-cartoon-dragon-vector.html 
    /// Giant: https://www.shutterstock.com/search/giant 
    /// Goblin: https://www.123rf.com/stock-photo/goblin.html?sti=nyha1t5nkszrwgajci| 
    /// Small Potion, Medium Potion, Large Potion: http://clipart-library.com/science-beaker-cliparts.html 
    /// Extreme Potion: https://www.storyblocks.com/images/cartoon%20magic%20potion 
    /// Dagger: https://www.dreamstime.com/stock-illustration-dagger-cartoon-vector-illustration-white-background-image44093505 
    /// Club: https://www.svgimages.com/jpg/wooden-club-cartoon.html 
    /// Sword: http://www.clker.com/clipart-25535.html
    /// Key: http://www.clker.com/clipart-olde-key.html 
    /// Door: https://www.shutterstock.com/search/door+cartoon 
    /// Door Open: https://www.shutterstock.com/search/door+cartoon 
    /// GameOver: https://www.iconfinder.com/icons/3386483/disappointment_sound_visual_game_over_game_over_comic_bubble_game_over_speech_bubble_hopelessness_pop_art_icon
    /// </remarks>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            Game.ResetGame(10, 10);
            CreateGrid();
            DrawMap();
            UpdateStatistics();
        }

        /// <summary>
        /// Creates the columns and rows of the grid
        /// </summary>
        public void CreateGrid() {
            // Create rows in grid            
            for (int i = 0; i < Game.Map.GameBoard.GetLength(0); i++) {
                grdMap.RowDefinitions.Add(new RowDefinition());
            }

            // Create columns in grid
            for (int i = 0; i < Game.Map.GameBoard.GetLength(1); i++) {
                grdMap.ColumnDefinitions.Add(new ColumnDefinition());                
            }
            
            
        }

        /// <summary>
        /// Draws the Map on the GUI
        /// </summary>
        public void DrawMap() {
            for (int i = 0; i < Game.Map.GameBoard.GetLength(0); i++) {
                for (int j = 0; j < Game.Map.GameBoard.GetLength(1); j++) {
                    // Create label and color the grid black
                    Label lblMapCell = new Label();
                    SolidColorBrush white = new SolidColorBrush(Colors.White);
                    SolidColorBrush black = new SolidColorBrush(Colors.Black);                  
                    Image image = new Image();                    
                    grdMap.Background = Brushes.Black;

                    // if the hero is located in the map cell, show the hero picture
                    if (Game.Map.Adventurer.XCoordinate == i && Game.Map.Adventurer.YCoordinate == j) {
                        lblMapCell.Background = white;
                        lblMapCell.HorizontalContentAlignment = HorizontalAlignment.Center;
                        lblMapCell.VerticalContentAlignment = VerticalAlignment.Center;
                        image.Source = new BitmapImage(new Uri("Images/Hero.png", UriKind.RelativeOrAbsolute));
                    } // if the map cell has a item, show the appropiate picture when discovered
                    else if (Game.Map.GameBoard[i, j].Item != null) {
                        if (Game.Map.GameBoard[i, j].Item.Name == "Battle Axe") {
                            CheckIfMapCellVisible("Images/BattleAxe.png", Game.Map.GameBoard[i, j], lblMapCell, image);                            
                        } else if (Game.Map.GameBoard[i, j].Item.Name == "Club") {
                            CheckIfMapCellVisible("Images/Club.png", Game.Map.GameBoard[i, j], lblMapCell, image);
                        } else if (Game.Map.GameBoard[i, j].Item.Name == "Sword") {
                            CheckIfMapCellVisible("Images/Sword.png", Game.Map.GameBoard[i, j], lblMapCell, image);
                        } else if (Game.Map.GameBoard[i, j].Item.Name == "Dagger") {
                            CheckIfMapCellVisible("Images/Dagger.png", Game.Map.GameBoard[i, j], lblMapCell, image);
                        } else if (Game.Map.GameBoard[i, j].Item.Name == "Small Healing Potion") {
                            CheckIfMapCellVisible("Images/SmallPotion.jpg", Game.Map.GameBoard[i, j], lblMapCell, image);
                        } else if (Game.Map.GameBoard[i, j].Item.Name == "Medium Healing Potion") {
                            CheckIfMapCellVisible("Images/MediumPotion.png", Game.Map.GameBoard[i, j], lblMapCell, image);
                        } else if (Game.Map.GameBoard[i, j].Item.Name == "Large Healing Potion") {
                            CheckIfMapCellVisible("Images/LargePotion.png", Game.Map.GameBoard[i, j], lblMapCell, image);
                        } else if (Game.Map.GameBoard[i, j].Item.Name == "Extreme Healing Potion") {
                            CheckIfMapCellVisible("Images/ExtremePotion.png", Game.Map.GameBoard[i, j], lblMapCell, image);
                        }
                    } // // if the map cell has a door, show the door picture when discovered 
                    else if (Game.Map.GameBoard[i, j].Door != null) {
                        CheckIfMapCellVisible("Images/Door.png", Game.Map.GameBoard[i, j], lblMapCell, image);
                    } // if the map cell has a door key, show the door key picture when discovered 
                    else if (Game.Map.GameBoard[i, j].DoorKey != null) {
                        lblMapCell.Background = white;
                        CheckIfMapCellVisible("Images/Key.png", Game.Map.GameBoard[i, j], lblMapCell, image);
                    } // if the map cell has a monster, show the appropiate picture when discovered
                    else if (Game.Map.GameBoard[i, j].Monster != null) {
                        lblMapCell.Background = white;
                        if (Game.Map.GameBoard[i, j].Monster.Name == "Skeleton") {
                            CheckIfMapCellVisible("Images/Skeleton.png", Game.Map.GameBoard[i, j], lblMapCell, image);
                        } else if (Game.Map.GameBoard[i, j].Monster.Name == "Giant") {
                            CheckIfMapCellVisible("Images/Giant.png", Game.Map.GameBoard[i, j], lblMapCell, image);
                        } else if (Game.Map.GameBoard[i, j].Monster.Name == "Dragon") {
                            CheckIfMapCellVisible("Images/Dragon.png", Game.Map.GameBoard[i, j], lblMapCell, image);
                        } else if (Game.Map.GameBoard[i, j].Monster.Name == "Slug") {
                            CheckIfMapCellVisible("Images/Slug.png", Game.Map.GameBoard[i, j], lblMapCell, image);
                        } else if (Game.Map.GameBoard[i, j].Monster.Name == "Goblin") {
                            CheckIfMapCellVisible("Images/Goblin.png", Game.Map.GameBoard[i, j], lblMapCell, image);
                        }
                    } else {
                        // if discovered, background is white. if not, background is black
                        if (Game.Map.GameBoard[i, j].IsDiscovered) {
                            lblMapCell.Background = white;
                        } else {
                            lblMapCell.Background = black;
                        }                        
                    }                    
                    // Add the label to the grid
                    Grid.SetColumn(lblMapCell, j);
                    Grid.SetRow(lblMapCell, i);
                    lblMapCell.Content = image;
                    grdMap.Children.Add(lblMapCell);
                }
            }
        }

        /// <summary>
        /// Checks if the map cell is visible and colors the background accordingly
        /// </summary>
        /// <param name="imagePath"></param>
        /// <param name="mapCell"></param>
        /// <param name="label"></param>
        /// <param name="image"></param>
        private void CheckIfMapCellVisible(string imagePath, MapCell mapCell, Label label, Image image) {
            SolidColorBrush white = new SolidColorBrush(Colors.White);
            SolidColorBrush black = new SolidColorBrush(Colors.Black);
            // if discovered and has an something in the map cell, show the appropriate picture and color the background white
            if (mapCell.IsDiscovered) {
                image.Source = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));
                 label.Background = white;
                label.HorizontalContentAlignment = HorizontalAlignment.Center;
                label.VerticalContentAlignment = VerticalAlignment.Center;
            } // if not discovered, keep background black
            else {
                label.Background = black;
                label.HorizontalContentAlignment = HorizontalAlignment.Center;
                label.VerticalContentAlignment = VerticalAlignment.Center;
            }
            
        }

        /// <summary>
        /// Displays either the monster, item, or game over form depending on what was encountered
        /// </summary>
        /// <remarks>
        /// Display an image using a BitmapImage: https://stackoverflow.com/questions/6503424/how-to-programmatically-set-the-image-source
        /// </remarks>
        private void DisplayForm() {
            // Gets the hero's position
            MapCell currentLocationOfHero = Game.Map.CurrentPositionOfHero(Game.Map.Adventurer);
            // Displays the item form when an item is found
            if (currentLocationOfHero.Item != null || currentLocationOfHero.DoorKey != null) {
                frmItem itemForm = new frmItem();   
            } // Displays the monster form when encountering a monster
            else if (currentLocationOfHero.Monster != null) {
                frmMonster monsterForm = new frmMonster();                
            } // Displays the game over form when encountering a door
            else if (currentLocationOfHero.Door != null) {
                if (Game.Map.Adventurer.HasKey) {
                    CurrentGameState(Game.GameState.Won);
                }
                frmGameOver gameOver = new frmGameOver();               
            }             
        }


        /// <summary>
        /// Update the GUI statistics
        /// </summary>
        public void UpdateStatistics() {
            tbOutputName.Text = Game.Map.Adventurer.Title;
            imgHero.Source = new BitmapImage(new Uri("Images/Hero.png", UriKind.RelativeOrAbsolute));
            tbOutputHitPoints.Text = Game.Map.Adventurer.HitPoints + "/" + Game.Map.Adventurer.MaxHitPoints;
            // if the player has a weapon display the name
            if (Game.Map.Adventurer.HasWeapon) {
                tbOutputWeapon.Text = Game.Map.Adventurer.EquippedWeapon.Name;
            } else {
                tbOutputWeapon.Text = "None";
            }
            // if the player has a key display the key name
            if (Game.Map.Adventurer.HasKey) {
                tbOutputKey.Text = Game.Map.Adventurer.EquippedDoorKey.Name;
            } else {
                tbOutputKey.Text = "None";
            }

        }

        /// <summary>
        /// Handles the current game state
        /// </summary>
        /// <param name="gameState"></param>
        public void CurrentGameState(Game.GameState gameState) {
            Game.TheGameState = gameState;
            if (gameState == Game.GameState.Lost) {
                frmGameOver gameOver = new frmGameOver();
            }
        }

        /// <summary>
        /// Allows the user to save their game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveGame_Click(object sender, RoutedEventArgs e) {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "map file(*.map)|*.map";
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fs = null;

            try {
                if (sfd.ShowDialog() == true) {
                    string fileName = sfd.FileName;
                    fs = File.Create(fileName);                    
                }
                formatter.Serialize(fs, Game.Map);

            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            } 
            finally {
                fs.Close(); 
            }
            
        }

        /// <summary>
        /// Allows the player to load a previous game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoadGame_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "map file(*.map)|*.map";
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fs = null;
            try {
                if (ofd.ShowDialog() == true) {
                    fs = File.OpenRead(ofd.FileName);
                    Game.Map = (Map)formatter.Deserialize(fs);
                    DrawMap();
                    UpdateStatistics();
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            finally {
                if (fs != null) {
                    fs.Close();
                }
            }
        }

        /// <summary>
        /// Allows the actor to move up and updates the map on GUI.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUp_Click(object sender, RoutedEventArgs e) {
            bool hasToAct = Game.Map.Move(Actor.Direction.Up, Game.Map.Adventurer);
            DrawMap();
            if (hasToAct) {
                DisplayForm();
            }
            UpdateStatistics();
        }

        /// <summary>
        /// Allows the actor to move down and updates the map on GUI.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDown_Click(object sender, RoutedEventArgs e) {
            bool hasToAct = Game.Map.Move(Actor.Direction.Down, Game.Map.Adventurer);
            DrawMap();
            if (hasToAct) {
                DisplayForm();
            }
            UpdateStatistics();
        }

        /// <summary>
        /// Allows the actor to move left and updates the map on GUI.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLeft_Click(object sender, RoutedEventArgs e) {
            bool hasToAct = Game.Map.Move(Actor.Direction.Left, Game.Map.Adventurer);
            DrawMap();
            if (hasToAct) {
                DisplayForm();
            }
            UpdateStatistics();
        }

        /// <summary>
        /// Allows the actor to move right and updates the map on GUI.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRight_Click(object sender, RoutedEventArgs e) {
            bool hasToAct = Game.Map.Move(Actor.Direction.Right, Game.Map.Adventurer);
            DrawMap();
            if (hasToAct) {
                DisplayForm();
            }
            UpdateStatistics();
        }

        /// <summary>
        /// A form level event listener that listens for and handles the keypress event for the up, down, left, and right arrow keys
        /// </summary>
        /// <remarks>
        /// Window level keyboard events: https://www.dotnetperls.com/keydown-wpf
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyUp(object sender, KeyEventArgs e) {
            // if the left arrow key is pressed, call the left button click event
            if (e.Key == Key.Left) {
                btnLeft_Click(this, null);
            } // if the right arrow key is pressed, call the right button click event
            else if (e.Key == Key.Up) {
                btnUp_Click(this, null);
            } // if the down arrow key is pressed, call the down button click event
            else if (e.Key == Key.Down) {
                btnDown_Click(this, null);
            } // if the right arrow key is pressed, call the right button click event
            else if (e.Key == Key.Right) {
                btnRight_Click(this, null);
            }
        }
    }
}
