using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UWPGame
{

    public sealed partial class NewGame : Page
    {

        public NewGame()
        {
            this.InitializeComponent();
        }

        private void Level1_Click(object sender, RoutedEventArgs e)
        {
            GameStage.gameDifficulty = 70;
            this.Frame.Navigate(typeof(GameStage));
        }

        private void Level2_Click(object sender, RoutedEventArgs e)
        {
            GameStage.gameDifficulty = 50;
            this.Frame.Navigate(typeof(GameStage));
        }

        private void Level3_Click(object sender, RoutedEventArgs e)
        {
            GameStage.gameDifficulty = 30;
            this.Frame.Navigate(typeof(GameStage));
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
