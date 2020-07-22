using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Text;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace UWPGame
{
    public sealed partial class GameStage : Page
    {

        class Entity
        {
            public Entity(int x, int y)
            {
                X = x;
                Y = y;
            }

            public int X;
            public int Y;
        }

        public static int gameDifficulty;

        List<Entity> Projectiles = new List<Entity>();
        List<Entity> Enemies = new List<Entity>();

        int score = 0;

        CanvasBitmap shipTexture, scoreScene, timerBG, projText, enemyText;

        public static int spaceShip, gameLevel, GameState = 0, gameTime = 0;

        double width, height;
        int shipWidth, shipHeight;

        public static DispatcherTimer RoundTimer = new DispatcherTimer();
        public static DispatcherTimer TimeTimer = new DispatcherTimer();
        Random rnd = new Random();
        int spawnPoint;
        int countdown = 10;
        bool RoundEnded = false;

        public GameStage()
        {
            this.InitializeComponent();

            RoundTimer.Tick += RoundTimer_Tick;
            RoundTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            RoundTimer.Start();

            TimeTimer.Tick += TimeTimer_Tick;
            TimeTimer.Interval = new TimeSpan(0, 0, 1);
            TimeTimer.Start();
        }

        private void Righty_Click(object sender, RoutedEventArgs e)
        {
            if (shipWidth < width - 360)
            {
                shipWidth += 10;
            }
        }

        private void Lefty_Click(object sender, RoutedEventArgs e)
        {
            if (shipWidth > 50)
            {
                shipWidth -= 10;
            }
        }

        private void TimeTimer_Tick(object sender, object e)
        {
            countdown -= 1;
            
            if (countdown == 0)
            {
                RoundTimer.Stop();
                TimeTimer.Stop();
                RoundEnded = true;
                gameTime = 0;

                if (HighScores.Scores[0] < score)
                {
                    HighScores.Scores[2] = HighScores.Scores[1];
                    HighScores.Scores[1] = HighScores.Scores[0];
                    HighScores.Scores[0] = score;
                }
                else if (HighScores.Scores[1] < score)
                {
                    HighScores.Scores[2] = HighScores.Scores[1];
                    HighScores.Scores[1] = score;
                }
                else if (HighScores.Scores[2] < score)
                {
                    HighScores.Scores[2] = score;
                }
            }
        }

        private void RoundTimer_Tick(object sender, object e)
        {

            if (!RoundEnded)
            {
                ++gameTime;
            }

            if (gameTime % 5 == 0)
            {
                var bullet = new Entity(shipWidth + 140, shipHeight - 10);

                Projectiles.Add(bullet);
            }
            
            if (gameTime % gameDifficulty == 0)
            {
                spawnPoint = rnd.Next(50, (int)(width) - 350);

                var enemy = new Entity(spawnPoint, -300);

                Enemies.Add(enemy);
            }

            for (int i = 0; i < Projectiles.Count; ++i)
            {
                if (Projectiles[i].Y < 0)
                {
                    Projectiles.RemoveAt(i);
                }else
                {
                    Projectiles[i].Y -= 10;
                }
            }

            for (int i = 0; i < Enemies.Count; ++i)
            {
                if (Enemies[i].Y > height)
                {
                    Enemies.RemoveAt(i);
                    _ = score > 50? score -= 50 : score = 0;
                }
                else
                {
                    Enemies[i].Y += 10;
                }
            }

            for (int i = Projectiles.Count-1; i >= 0; --i)
            {
                for (int j = Enemies.Count-1; j >= 0; --j)
                {
                    if (Projectiles[i].X > Enemies[j].X && Projectiles[i].X < Enemies[j].X + 300)
                    {
                        if (Projectiles[i].Y < Enemies[j].Y + 300 && Projectiles[i].Y > Enemies[j].Y)
                        {
                            score += 20;
                            Projectiles.RemoveAt(i);
                            Enemies.RemoveAt(j);
                            break;
                        }
                    }
                }
            }
        }

        private void GameCanvas_CreateResources(CanvasControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
        {
            args.TrackAsyncAction(CreateResourcesAsync(sender).AsAsyncAction());
            width = GameCanvas.ActualWidth;
            height = GameCanvas.ActualHeight;

            shipWidth = (int)(width / 2 - 150);
            shipHeight = (int)(height - 425);
        }

        async Task CreateResourcesAsync(CanvasControl sender)
        {
            switch (spaceShip)
            {
                case 1:
                    shipTexture = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Textures/urhajo1.png"));
                    break;
                case 2:
                    shipTexture = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Textures/urhajo2.png"));
                    break;
                case 3:
                    shipTexture = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Textures/urhajo3.png"));
                    break;
                case 4:
                    shipTexture = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Textures/urhajo4.png"));
                    break;
                case 5:
                    shipTexture = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Textures/urhajo5.png"));
                    break;
                default:
                    shipTexture = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Textures/urhajo1.png"));
                    break;
            }
            
            projText = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Textures/projectile.png"));
            enemyText = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Textures/enemy.png"));

            scoreScene = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Textures/score.png"));
            timerBG = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Textures/timer.png"));

        }

        private void GameCanvas_Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            args.DrawingSession.DrawImage(shipTexture, shipWidth, shipHeight);

            for (int i = 0; i < Projectiles.Count; ++i)
            {
                args.DrawingSession.DrawImage(projText, Projectiles[i].X, Projectiles[i].Y);
            }

            for (int i = 0; i < Enemies.Count; ++i)
            {
                args.DrawingSession.DrawImage(enemyText, Enemies[i].X, Enemies[i].Y);
            }

            if (RoundEnded)
            {
                args.DrawingSession.DrawImage(scoreScene, (int)(width / 2 - 150), (int)(height / 2 - 200));
                args.DrawingSession.DrawText(score.ToString(), (int)(width / 2 - 12), (int)(height / 2 - 12), Colors.Yellow);
                Righty.IsEnabled = false;
                Lefty.IsEnabled = false;
            }
            else
            {
                args.DrawingSession.DrawImage(timerBG, 25, 25);
                args.DrawingSession.DrawText(countdown.ToString(), 42, 35, Colors.Black);
            }

            GameCanvas.Invalidate();
        }

        private void GameCanvas_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (RoundEnded)
            {
                this.Frame.Navigate(typeof(MainPage));
            }
        }
    }
}
