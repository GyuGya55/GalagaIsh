using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Threading.Tasks;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UWPGame
{

    public sealed partial class Settings : Page
    {

        public Settings()
        {
            this.InitializeComponent();
        }

        private void Fullscreen_Click(object sender, RoutedEventArgs e)
        {
            var view = ApplicationView.GetForCurrentView();
            if (view.IsFullScreenMode)
            {
                view.ExitFullScreenMode();
                ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.Auto;
            }
            else
            {
                if (view.TryEnterFullScreenMode())
                {
                    ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.FullScreen;
                }
            }
        }


        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void ChangeShip_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ChangeShip));
        }
    }
}
