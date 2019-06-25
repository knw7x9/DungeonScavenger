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
    /// Interaction logic for frmGameOver.xaml
    /// </summary>
    public partial class frmGameOver : Window {
        public frmGameOver() {
            InitializeComponent();
            // When the hero dies, display Lost game form
            if (Game.TheGameState == Game.GameState.Lost) {
                tbOutputGameState.Text = "You Lose!";
                SolidColorBrush red = new SolidColorBrush(Colors.Red);
                tbOutputGameState.Foreground = red;
                Title = "Opps!";
                imgGameOver.Source = new BitmapImage(new Uri("Images/GameOver.png", UriKind.RelativeOrAbsolute));
                btnRestart.Visibility = Visibility.Visible;
                btnExit.Visibility = Visibility.Visible;
                btnOk.Visibility = Visibility.Collapsed;
            } // When the hero wins, display the Won form 
            else if (Game.TheGameState == Game.GameState.Won) {
                tbOutputGameState.Text = "Congratulations!";
                imgGameOver.Source = new BitmapImage(new Uri("Images/OpenDoor.png", UriKind.RelativeOrAbsolute));
                SolidColorBrush green = new SolidColorBrush(Colors.Green);
                tbOutputGameState.Foreground = green;
                tbOutputInformation.Text = "You Won!";
                Title = "Winner";
                btnRestart.Visibility = Visibility.Visible;
                btnExit.Visibility = Visibility.Visible;
                btnOk.Visibility = Visibility.Collapsed;
            } // When the hero finds the door, but does not have the door key, display the Keep Searching form 
            else if (Game.TheGameState == Game.GameState.Running) {
                tbOutputGameState.Text = "You found the door!";
                tbOutputInformation.Text = "But you do not have the key.";
                tbWishToDo.Text = "Keep Looking.";
                imgGameOver.Source = new BitmapImage(new Uri("Images/Door.png", UriKind.RelativeOrAbsolute));
                SolidColorBrush orange = new SolidColorBrush(Colors.Orange);
                tbOutputGameState.Foreground = orange;
                Title = "Continue Searching";
                btnRestart.Visibility = Visibility.Collapsed;
                btnExit.Visibility = Visibility.Collapsed;
                btnOk.Visibility = Visibility.Visible;
            }
            ShowDialog();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        /// <summary>
        /// Restart the MainWindow
        /// </summary>
        /// How to restart the MainWindow: https://stackoverflow.com/questions/13644114/how-can-i-access-a-control-in-wpf-from-another-class-or-window
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRestart_Click(object sender, RoutedEventArgs e) {
            Game.ResetGame();
            this.Close();
            foreach (Window window in Application.Current.Windows) {
                if (window.GetType() == typeof(MainWindow)) {
                    (window as MainWindow).DrawMap();
                    (window as MainWindow).UpdateStatistics();
                }
            }                 
        }

        /// <summary>
        /// Exit the MainWindow and the game over form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, RoutedEventArgs e) {
            this.Close();
            this.Close();
            foreach (Window window in Application.Current.Windows)
                if (window.GetType() == typeof(MainWindow)) {
                    (window as MainWindow).Close();
                }
        }
    }
}
