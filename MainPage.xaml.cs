using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas;
using System.Threading.Tasks;
using Windows.UI.ViewManagement;
using Windows.UI.Core;
using LudoNewWorld.Classes;
using System.Threading;
using System.Numerics;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LudoNewWorld
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static CanvasBitmap background, gameBackground, menuBackground ,dice1,dice2, dice3, dice4, dice5, dice6;
        public static Rect bounds = ApplicationView.GetForCurrentView().VisibleBounds;
        public Vector3 scaleVector3Variable = new Vector3(DesignWidth, DesignHeight, 1);

        public static float DesignWidth = 1920;
        public static float DesignHeight = 1080;
        public static float scaleWidth, scaleHeight;
        public static int gameState = 0;
        public static string playerFaction;
                
        public MainPage()
        {
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size { Width = 1280, Height = 720 });
            ApplicationView.PreferredLaunchViewSize = new Size(1920, 1080);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            this.InitializeComponent();
            Window.Current.SizeChanged += Current_SizeChanged;
            Scaler.SetScale();
        }
        private void Current_SizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            bounds = ApplicationView.GetForCurrentView().VisibleBounds;
            Scaler.SetScale();

            scaleVector3Variable.X = (float)mainGrid.ActualWidth / DesignWidth;
            scaleVector3Variable.Y = (float)mainGrid.ActualHeight / DesignHeight;

            // Scale assets based on the true size of the game window
            MenuField.Scale = scaleVector3Variable;
            FactionField.Scale = scaleVector3Variable;

            //Scale margin of assets based on the actual window size
            var xMargin = (mainGrid.ActualWidth - DesignWidth);
            var yMargin = (mainGrid.ActualHeight - DesignHeight);
            FactionField.Margin = new Thickness(1, 1, xMargin, yMargin);
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            MenuField.Visibility = Visibility.Collapsed;
            FactionField.Visibility = Visibility.Visible;
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        private void btnBritain_Click(object sender, RoutedEventArgs e)
        {
            playerFaction = "Britain";
            FactionField.Visibility = Visibility.Collapsed;
            gameState = 1;
        }

        private void btnFrance_Click(object sender, RoutedEventArgs e)
        {
            playerFaction = "France";
            FactionField.Visibility = Visibility.Collapsed;
            
            gameState = 1;
        }

        private void btnDutch_Click(object sender, RoutedEventArgs e)
        {
            playerFaction = "Dutch";
            FactionField.Visibility = Visibility.Collapsed;
            gameState = 1;
        }

        private void btnSpain_Click(object sender, RoutedEventArgs e)
        {
            playerFaction = "Spain";
            FactionField.Visibility = Visibility.Collapsed;
            gameState = 1;
        }

        private void GameCanvas_CreateResources(CanvasAnimatedControl sender, CanvasCreateResourcesEventArgs args)
        {
            args.TrackAsyncAction(CreateResourceAsync(sender).AsAsyncAction());
        }

        async Task CreateResourceAsync(CanvasAnimatedControl sender)
        {
            gameBackground = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/bg.png"));
            menuBackground = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/menuBackground.png"));
            dice1 = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Dices/dice1.png"));
            dice2 = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Dices/dice2.png"));
            dice3 = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Dices/dice3.png"));
            dice4 = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Dices/dice4.png"));
            dice5 = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Dices/dice5.png"));
            dice6 = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Dices/dice6.png"));

            GameTiles.BritainTile = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/GameTiles/BritainTile.png"));
            GameTiles.DutchTile = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/GameTiles/DutchTile.png"));
            GameTiles.SpainTile = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/GameTiles/SpainTile.png"));
            GameTiles.FranceTile = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/GameTiles/FranceTile.png"));

            Dice.Dicepictures.Add(dice1); Dice.Dicepictures.Add(dice2); Dice.Dicepictures.Add(dice3); Dice.Dicepictures.Add(dice4);
            Dice.Dicepictures.Add(dice5); Dice.Dicepictures.Add(dice6);
            await DicePics(sender);
        }

        private static async Task DicePics(CanvasAnimatedControl sender)
        {
            
            Dice.DiceRoll();

            switch (Dice.diceValue)
            {
                case 1:
                    Dice.dice = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Dices/dice1.png"));
                    break;
                case 2:
                    Dice.dice = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Dices/dice2.png"));
                    break;
                case 3:
                    Dice.dice = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Dices/dice3.png"));
                    break;
                case 4:
                    Dice.dice = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Dices/dice4.png"));
                    break;
                case 5:
                    Dice.dice = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Dices/dice5.png"));
                    break;
                case 6:
                    Dice.dice = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Dices/dice6.png"));
                    break;
                default:
                    break;
            }
        }

        private void GameCanvas_Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            GameStateManager.BackgroundManager();

            args.DrawingSession.DrawImage(Scaler.Fit(background));
            if (gameState == 1)
            {
                args.DrawingSession.DrawImage(Scaler.Fit(GameTiles.BritainTile), 300, 400);
                args.DrawingSession.DrawImage(Scaler.Fit(GameTiles.FranceTile), 500, 600);
                args.DrawingSession.DrawImage(Scaler.Fit(GameTiles.DutchTile), 600, 700);
                args.DrawingSession.DrawImage(Scaler.Fit(GameTiles.SpainTile), 400, 500);

                for (int i = 0; i < Dice.Dicepictures.Count; i++)
                {
                    args.DrawingSession.DrawImage(Scaler.Fit(Dice.Dicepictures[i]));
                }
            }
        }
    }
}
