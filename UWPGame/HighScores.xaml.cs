using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UWPGame
{
    public sealed partial class HighScores : Page
    {

        CanvasBitmap highScore;
        double width, height;
        int scoreWidth, scoreHight;

        public static int[] Scores = { 0, 0, 0 };

        public HighScores()
        {
            this.InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void HighScoreCanvas_CreateResources(Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
        {
            args.TrackAsyncAction(CreateResourcesAsync(sender).AsAsyncAction());
        }

        async Task CreateResourcesAsync(CanvasControl sender)
        {
            highScore = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Textures/highscores.png"));

            width = HighScoreCanvas.ActualWidth;
            height = HighScoreCanvas.ActualHeight;
            scoreWidth = (int)(width / 2 - 200);
            scoreHight = (int)(height / 2 - 400);
        }

        private void HighScoreCanvas_Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            args.DrawingSession.DrawImage(highScore, scoreWidth, scoreHight);
            args.DrawingSession.DrawText(Scores[0].ToString(), scoreWidth + 195, scoreHight + 120, Colors.Yellow);
            args.DrawingSession.DrawText(Scores[1].ToString(), scoreWidth + 195, scoreHight + 250, Colors.Yellow);
            args.DrawingSession.DrawText(Scores[2].ToString(), scoreWidth + 195, scoreHight + 400, Colors.Yellow);
            HighScoreCanvas.Invalidate();
        }
    }
}
