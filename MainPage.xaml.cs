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
using Newtonsoft.Json;
using System.Diagnostics;
using Windows.Storage;
using Windows.Media.Playback;
using Windows.Media.Core;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LudoNewWorld
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public static CanvasBitmap background, gameBackground, menuBackground,
            dice1, dice2, dice3, dice4, dice5, dice6, 
            NeutralTile, BritainTile, SpainTile, DutchTile, FranceTile, NegativeTile, PositiveTile, RandomTile;
        public static MediaPlayer player;
        public static Rect bounds = ApplicationView.GetForCurrentView().VisibleBounds;
        public Vector3 scaleVector3Variable = new Vector3(DesignWidth, DesignHeight, 1);
        public static MediaPlayer mPlayer = new MediaPlayer();

        public static float DesignWidth = 1920;
        public static float DesignHeight = 1080;
        public static float scaleWidth, scaleHeight;
        public static int gameState = 0;
        public static string playerFaction;
        
        List<GameTile> gameTiles = new List<GameTile>();
                
        public MainPage()
        {
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size { Width = 1280, Height = 720 });
            ApplicationView.PreferredLaunchViewSize = new Size(1920, 1080);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            this.InitializeComponent();
            Window.Current.SizeChanged += Current_SizeChanged;
            Scaler.SetScale();
            Sound.SoundPlay();
        }
        public void AddObjectToTileList()
        {
            gameTiles.Add(new GameTile(Tile.StartTile, FactionTile.BritainTile, new Vector2(300, 400)));
            gameTiles.Add(new GameTile(Tile.NeutralTile, new Vector2(400, 400)));
            gameTiles.Add(new GameTile(Tile.NeutralTile, new Vector2(500, 400)));
            gameTiles.Add(new GameTile(Tile.NeutralTile, new Vector2(600, 400)));
            gameTiles.Add(new GameTile(Tile.NegativeTile, new Vector2(600, 500)));
            gameTiles.Add(new GameTile(Tile.PositiveTile, new Vector2(600, 600)));
            gameTiles.Add(new GameTile(Tile.RandomTile, new Vector2(600, 700)));
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

        private async void btnStart_Click(object sender, RoutedEventArgs e)
        {
            MenuField.Visibility = Visibility.Collapsed;
            FactionField.Visibility = Visibility.Visible;
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        private async void btnBritain_Click(object sender, RoutedEventArgs e)
        {
            playerFaction = "Britain";
            FactionField.Visibility = Visibility.Collapsed;
            gameState = 1;
        }

        private async void btnFrance_Click(object sender, RoutedEventArgs e)
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
            //Background
            gameBackground = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/bg.png"));
            menuBackground = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/menuBackground.png"));

            //Dices
            dice1 = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Dices/dice1.png"));
            dice2 = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Dices/dice2.png"));
            dice3 = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Dices/dice3.png"));
            dice4 = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Dices/dice4.png"));
            dice5 = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Dices/dice5.png"));
            dice6 = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Dices/dice6.png"));
            Dice.Dicepictures.Add(dice1); Dice.Dicepictures.Add(dice2); Dice.Dicepictures.Add(dice3); Dice.Dicepictures.Add(dice4);
            Dice.Dicepictures.Add(dice5); Dice.Dicepictures.Add(dice6);
            await DicePics(sender);

            //Tiles
            BritainTile = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/GameTiles/BritainTile.png"));
            DutchTile = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/GameTiles/DutchTile.png"));
            SpainTile = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/GameTiles/SpainTile.png"));
            FranceTile = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/GameTiles/FranceTile.png"));
            NeutralTile = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/GameTiles/NeutralTile.png"));
            NegativeTile = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/GameTiles/NegativeTile.png"));
            PositiveTile = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/GameTiles/PositiveTile.png"));
            RandomTile = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/GameTiles/RandomTile.png"));

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
            AddObjectToTileList();
            args.DrawingSession.DrawImage(Scaler.Fit(background));
            if (gameState == 1)
            {

                args.DrawingSession.DrawImage(Scaler.Fit(BritainTile), gameTiles[0].GameTileVector);
                args.DrawingSession.DrawImage(Scaler.Fit(NeutralTile), gameTiles[1].GameTileVector);
                args.DrawingSession.DrawImage(Scaler.Fit(NeutralTile), gameTiles[2].GameTileVector);
                args.DrawingSession.DrawImage(Scaler.Fit(NeutralTile), gameTiles[3].GameTileVector);
                args.DrawingSession.DrawImage(Scaler.Fit(NegativeTile), gameTiles[4].GameTileVector);
                args.DrawingSession.DrawImage(Scaler.Fit(PositiveTile), gameTiles[5].GameTileVector);
                args.DrawingSession.DrawImage(Scaler.Fit(RandomTile), gameTiles[6].GameTileVector);


                for (int i = 0; i < Dice.Dicepictures.Count; i++)
                {
                    args.DrawingSession.DrawImage(Scaler.Fit(Dice.Dicepictures[i]));
                }
            }
        }
    }
}
