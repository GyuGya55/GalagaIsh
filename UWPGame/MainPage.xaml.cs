using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Windows.ApplicationModel.Core;

namespace UWPGame
{

    public sealed partial class MainPage : Page
    { 

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(NewGame));
        }

        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Settings));
        }

        private void HighScores_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(HighScores));
        }

        private void QuitGame_Click(object sender, RoutedEventArgs e)
        {
            CoreApplication.Exit();
        }
    }
}
