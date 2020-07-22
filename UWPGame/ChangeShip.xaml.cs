using Windows.UI.Xaml.Controls;

namespace UWPGame
{

    public sealed partial class ChangeShip : Page
    {
        public ChangeShip()
        {
            this.InitializeComponent();
        }

        private void Ship1_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            GameStage.spaceShip = 1;
            this.Frame.Navigate(typeof(Settings));
        }

        private void Ship2_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            GameStage.spaceShip = 2;
            this.Frame.Navigate(typeof(Settings));
        }

        private void Ship3_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            GameStage.spaceShip = 3;
            this.Frame.Navigate(typeof(Settings));
        }

        private void Ship4_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            GameStage.spaceShip = 4;
            this.Frame.Navigate(typeof(Settings));
        }

        private void Ship5_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            GameStage.spaceShip = 5;
            this.Frame.Navigate(typeof(Settings));
        }
    }
}
