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
using System.Diagnostics;
using Windows.Storage;
using Windows.Media.Playback;
using Windows.Media.Core;
using Windows.UI.Xaml.Media.Imaging;
using System.ServiceModel.Channels;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LudoNewWorld
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static Rect bounds = ApplicationView.GetForCurrentView().VisibleBounds;
        public static float scaleWidth, scaleHeight;
        public static float DesignWidth = 1920;
        public static float DesignHeight = 1080;
        public static int gameState = 0;
        public static Faction playerFaction;
        public static MediaPlayer mPlayer = new MediaPlayer();
        public static MediaPlayer mPlayerr = new MediaPlayer();
        public static double currentVolume = 0.5;
        public static int volumeLevel = 5;
        public static bool volumeMute = false;
        public static bool nextRoundAvailable = true;
        private static bool debugMenuActive = false;
        private static int gameTickCounter = 0;
        public Vector3 scaleVector3Variable = new Vector3(DesignWidth, DesignHeight, 1);

        GameEngine gameEngine = new GameEngine();

        Dice dice = new Dice();
        
        public MainPage()
        {
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size { Width = 1280, Height = 720 });
            ApplicationView.PreferredLaunchViewSize = new Size(1920, 1080);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            this.InitializeComponent();
            Window.Current.SizeChanged += Current_SizeChanged;
            Scaler.SetScale();
            Sound.SoundPlay();
            GraphicHandler.LoadResources();            
        }
        private void Current_SizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            bounds = ApplicationView.GetForCurrentView().VisibleBounds;
            Scaler.SetScale();

            scaleVector3Variable.X = (float)mainGrid.ActualWidth / DesignWidth;
            scaleVector3Variable.Y = (float)mainGrid.ActualHeight / DesignHeight;

            var xMargin = mainGrid.ActualWidth - DesignWidth;
            var yMargin = mainGrid.ActualHeight - DesignHeight;

            // Scale assets based on the true size of the game window
            MenuField.Scale = scaleVector3Variable;
            FactionField.Scale = scaleVector3Variable;
            Dice.Scale = scaleVector3Variable;
            PlayingMenu.Scale = scaleVector3Variable;

            //Scale margin of assets based on the actual window size
            MenuField.Margin = new Thickness(1, 1, xMargin, yMargin);
            FactionField.Margin = new Thickness(1, 1, xMargin, yMargin);
            Dice.Margin = new Thickness(1, 1, xMargin, yMargin);
            PlayingMenu.Margin = new Thickness(1, 1, xMargin, yMargin);
        }

        private void GameCanvas_CreateResources(CanvasAnimatedControl sender, CanvasCreateResourcesEventArgs args)
        {
            GraphicHandler.CreateResources(sender, args);
        }

        private void GameCanvas_Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            GraphicHandler.Draw(sender, args);
        }
        private void GameCanvas_Update(ICanvasAnimatedControl sender, CanvasAnimatedUpdateEventArgs args)
        {
            gameTickCounter++;
            GameEngine.moveAITick++;
            if(GameEngine.GetGameActive())
            {
                if (gameTickCounter >= 60)
                {
                    if(debugMenuActive)
                    {
                        SetDebugMenu();
                    }
                    TriggerRollButton();
                    gameTickCounter = 0;
                }
                if(nextRoundAvailable)
                {
                    gameEngine.NextRound();
                }
            }
            if (GameEngine.moveAITick > 200) GameEngine.moveAITick = 0;
        }
        private void GameCanvas_Loaded(object sender, RoutedEventArgs e) { }

        private void GameCanvas_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Vector2 cords = new Vector2((float)e.GetCurrentPoint(GameCanvas).Position.X, (float)e.GetCurrentPoint(GameCanvas).Position.Y);
            Vector2 scaledVector = Scaler.ClickCords(cords);
            gameEngine.boat = gameEngine.CheckForShipsOnMousePressed(scaledVector);
            gameEngine.tile = gameEngine.CheckForTileOnMousePressed(scaledVector);
            if (GameEngine.GetGameActive())
            {
                if(gameEngine.boat != null)
                {
                }
            }
        }

        /// <summary>
        /// Will enable or disable the Roll button in the game based on <see cref="GameEngine.PlayerTurn"/> has as value
        /// Anything else than 1 will disable the button
        /// </summary>
        private async void TriggerRollButton()
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                if(GameEngine.diceRolled)
                {
                    Dice.Visibility = Visibility.Collapsed;
                }
                else if (GameEngine.PlayerTurn != 1 && Dice.Visibility == Visibility.Visible)
                {
                    Dice.Visibility = Visibility.Collapsed;
                }
                else if (GameEngine.PlayerTurn == 1 && !GameEngine.diceRolled)
                {
                    Dice.Visibility = Visibility.Visible;
                }
            }).AsTask();
        }

        private void btnRoll_Click(object sender, RoutedEventArgs e)
        {
            Sound.DiceSound();
            GameEngine.LastDiceRoll = GraphicHandler.scrambleDice(GameEngine.PlayerTurn);
            GameEngine.diceRolled = true;
            Dice.Visibility = Visibility.Collapsed;
        }

        private async void btnStart_Click(object sender, RoutedEventArgs e)
        {
            MenuField.Visibility = Visibility.Collapsed;
            FactionField.Visibility = Visibility.Visible;
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            MenuField.Visibility = Visibility.Collapsed;
            QuitConfirm_Popup.IsOpen = true;
        }

        private void btnBritain_Click(object sender, RoutedEventArgs e)
        {
            gameEngine.StartGame(Faction.Britain);
            FactionField.Visibility = Visibility.Collapsed;
            Dice.Visibility = Visibility.Visible;
            gameState = 1;
            Thickness margin = new Thickness(30, 179, 0, 0);
            btnRoll.Margin = margin;
        }

        private void btnDutch_Click(object sender, RoutedEventArgs e)
        {
            gameEngine.StartGame(Faction.Dutch);
            FactionField.Visibility = Visibility.Collapsed;
            Dice.Visibility = Visibility.Visible;
            gameState = 1;
            Thickness margin = new Thickness(1773, 179, 0, 0);
            btnRoll.Margin = margin;
        }

        private void btnFrance_Click(object sender, RoutedEventArgs e)
        {
            gameEngine.StartGame(Faction.France);
            FactionField.Visibility = Visibility.Collapsed;
            Dice.Visibility = Visibility.Visible;
            gameState = 1;
            Thickness margin = new Thickness(30, 851, 0, 0);
            btnRoll.Margin = margin;
        }
 
        private void btnSpain_Click(object sender, RoutedEventArgs e)
        {
            gameEngine.StartGame(Faction.Spain);
            FactionField.Visibility = Visibility.Collapsed;
            Dice.Visibility = Visibility.Visible;
            gameState = 1;
            Thickness margin = new Thickness(1773, 851, 0, 0);
            btnRoll.Margin = margin;
        }
        private void BtnMuteVolume_Click(object sender, RoutedEventArgs e)
        {
            if (!volumeMute && volumeLevel > 0)
            {
                Debug.WriteLine("Volume muted");
                mPlayer.Volume = 0;
                volumeMute = true;
                muteButtonChange.Source = new BitmapImage(new Uri(base.BaseUri, @"/Assets/Images/Menu/volumemute.png"));
            }
            else if (volumeMute && volumeLevel > 0)
            {
                Debug.WriteLine("Volume enabled");
                mPlayer.Volume = currentVolume;
                volumeMute = false;
                muteButtonChange.Source = new BitmapImage(new Uri(base.BaseUri, @"/Assets/Images/Menu/volumeunmute.png"));
            }
        }
        private void BtnLowerVolume_Click(object sender, RoutedEventArgs e)
        {
            if (!volumeMute && volumeLevel > 0)
            {
                currentVolume -= 0.1;
                mPlayer.Volume -= 0.1;
                volumeLevel--;
                volumeSlider.Source = new BitmapImage(new Uri(base.BaseUri, @"/Assets/Images/Menu/menu" + volumeLevel + ".png"));
                if(volumeLevel == 0)
                {
                    muteButtonChange.Source = new BitmapImage(new Uri(base.BaseUri, @"/Assets/Images/Menu/volumemute.png"));
                }
            }
            Debug.WriteLine("Volume: " + mPlayer.Volume);
        }

        private void BtnRaiseVolume_Click(object sender, RoutedEventArgs e)
        {
            if(!volumeMute && volumeLevel < 10)
            {
                currentVolume += 0.1;
                mPlayer.Volume += 0.1;
                volumeLevel++;
                volumeSlider.Source = new BitmapImage(new Uri(base.BaseUri, @"/Assets/Images/Menu/menu" + volumeLevel + ".png"));
                if (volumeLevel > 0)
                {
                    muteButtonChange.Source = new BitmapImage(new Uri(base.BaseUri, @"/Assets/Images/Menu/volumeunmute.png"));
                }
            }
            Debug.WriteLine("Volume: " + mPlayer.Volume);
        }

        private void Help_Quit_Click(object sender, RoutedEventArgs e)
        {
            Popup2.IsOpen = false;
            ExitMenuConfirm_Popup.IsOpen = true;
            
        }

        private void BtnDebug_Click(object sender, RoutedEventArgs e)
        {
            if(!debugMenuActive)
            {
                DebugMenu.Visibility = Visibility.Visible;
                debugMenuActive = true;
            }
            else
            {
                DebugMenu.Visibility = Visibility.Collapsed;
                debugMenuActive = false;
            }
        }

        private void Instruct_btn_Click(object sender, RoutedEventArgs e)
        {

            Popup2.IsOpen = false;
            MyPopup.IsOpen = false;
            InstructPopup.IsOpen = true;
     
        }

        private void Credit_btn_Click(object sender, RoutedEventArgs e)
        {
            Popup2.IsOpen = false;
            MyPopup.IsOpen = false;
            CreditPopup.IsOpen = true;
            Sound.CrediSound(CreditPopup.IsOpen);

        }

        private void instruct_return_Click(object sender, RoutedEventArgs e)
        {
            InstructPopup.IsOpen = false;
            MyPopup.IsOpen = true;
            Popup2.IsOpen = true;
        }

        private void credit_return_Click(object sender, RoutedEventArgs e)
        {
            CreditPopup.IsOpen = false;
            MyPopup.IsOpen = true;
            Popup2.IsOpen = true;
            Sound.CrediSound(CreditPopup.IsOpen);
        }

        private void QuitConfirm_yes_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        private void QuitConfirm_no_Click(object sender, RoutedEventArgs e)
        {
            MenuField.Visibility = Visibility.Visible;
            QuitConfirm_Popup.IsOpen = false;
        }

        private void ExitConfirm_yes_Click(object sender, RoutedEventArgs e)
        {
            gameState = 2;

            if (gameState == 2)
            {
                ParentPopup.IsOpen = false;
                ExitMenuConfirm_Popup.IsOpen = false;

                MenuField.Visibility = Visibility.Visible;
                Dice.Visibility = Visibility.Collapsed;
            }
        }

        private void ExitConfirm_no_Click(object sender, RoutedEventArgs e)
        {
            ExitMenuConfirm_Popup.IsOpen = false;
            Popup2.IsOpen = true;
        }

        private void BtnMenuHelp_Click(object sender, RoutedEventArgs e)
        {
             
            if (gameState == 1)
            {
                ParentPopup.IsOpen = true;

                if (ParentPopup.IsOpen == true)
                {
                    MyPopup.IsOpen = true;
                    Popup2.IsOpen = true;
                }
                else if (ParentPopup.IsOpen == false)
                {
                    MyPopup.IsOpen = false;
                    Popup2.IsOpen = false;
                }
            }
        }
        private async void SetDebugMenu()
        {
            if(GameEngine.GetGameActive())
            {
                await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    debugMenuTextLeft.Text =
                    $"\nPlayer 1 Faction : {gameEngine.p1.playerFaction}\n" +
                    $"Player 1 boats alive: {gameEngine.p1.rowBoats.Count}\n" +
                    $"-- Boat 1 current tile: {gameEngine.p1.rowBoats[0].CurrentTile}\n" +
                    $"-- Boat 2 current tile: {gameEngine.p1.rowBoats[1].CurrentTile}\n" +
                    $"-- Boat 3 current tile: {gameEngine.p1.rowBoats[2].CurrentTile}\n" +
                    $"-- Boat 4 current tile: {gameEngine.p1.rowBoats[3].CurrentTile}\n" +

                    $"-- Boat 1 active: {gameEngine.p1.rowBoats[0].active}\n" +
                    $"-- Boat 2 active: {gameEngine.p1.rowBoats[1].active}\n" +
                    $"-- Boat 3 active: {gameEngine.p1.rowBoats[2].active}\n" +
                    $"-- Boat 4 active: {gameEngine.p1.rowBoats[3].active}\n" +

                    $"-- Boat 1 targetable: {gameEngine.p1.rowBoats[0].targetable}\n" +
                    $"-- Boat 2 targetable: {gameEngine.p1.rowBoats[1].targetable}\n" +
                    $"-- Boat 3 targetable: {gameEngine.p1.rowBoats[2].targetable}\n" +
                    $"-- Boat 4 targetable: {gameEngine.p1.rowBoats[3].targetable}\n" +

                    $"\nPlayer 3 Faction : {gameEngine.p3.playerFaction}\n" +
                    $"Player 3 boats alive: {gameEngine.p3.rowBoats.Count}\n" +
                    $"-- Boat 1 current tile: {gameEngine.p3.rowBoats[0].CurrentTile}\n" +
                    $"-- Boat 2 current tile: {gameEngine.p3.rowBoats[1].CurrentTile}\n" +
                    $"-- Boat 3 current tile: {gameEngine.p3.rowBoats[2].CurrentTile}\n" +
                    $"-- Boat 4 current tile: {gameEngine.p3.rowBoats[3].CurrentTile}\n" +

                    $"-- Boat 1 active: {gameEngine.p3.rowBoats[0].active}\n" +
                    $"-- Boat 2 active: {gameEngine.p3.rowBoats[1].active}\n" +
                    $"-- Boat 3 active: {gameEngine.p3.rowBoats[2].active}\n" +
                    $"-- Boat 4 active: {gameEngine.p3.rowBoats[3].active}\n" +

                    $"-- Boat 1 targetable: {gameEngine.p3.rowBoats[0].targetable}\n" +
                    $"-- Boat 2 targetable: {gameEngine.p3.rowBoats[1].targetable}\n" +
                    $"-- Boat 3 targetable: {gameEngine.p3.rowBoats[2].targetable}\n" +
                    $"-- Boat 4 targetable: {gameEngine.p3.rowBoats[3].targetable}\n" +

                    $"\nTargetableRowBoats count: {Player.targetableRowBoats.Count}" +
                    $"\nMove confirmed?: {GameEngine.moveConfirmed}";

                    debugMenuTextRight.Text =
                    $"\nPlayer 2 Faction : {gameEngine.p2.playerFaction}\n" +
                    $"Player 2 boats alive: {gameEngine.p2.rowBoats.Count}\n" +
                    $"-- Boat 1 current tile: {gameEngine.p2.rowBoats[0].CurrentTile}\n" +
                    $"-- Boat 2 current tile: {gameEngine.p2.rowBoats[1].CurrentTile}\n" +
                    $"-- Boat 3 current tile: {gameEngine.p2.rowBoats[2].CurrentTile}\n" +
                    $"-- Boat 4 current tile: {gameEngine.p2.rowBoats[3].CurrentTile}\n" +

                    $"-- Boat 1 active: {gameEngine.p2.rowBoats[0].active}\n" +
                    $"-- Boat 2 active: {gameEngine.p2.rowBoats[1].active}\n" +
                    $"-- Boat 3 active: {gameEngine.p2.rowBoats[2].active}\n" +
                    $"-- Boat 4 active: {gameEngine.p2.rowBoats[3].active}\n" +

                    $"-- Boat 1 targetable: {gameEngine.p2.rowBoats[0].targetable}\n" +
                    $"-- Boat 2 targetable: {gameEngine.p2.rowBoats[1].targetable}\n" +
                    $"-- Boat 3 targetable: {gameEngine.p2.rowBoats[2].targetable}\n" +
                    $"-- Boat 4 targetable: {gameEngine.p2.rowBoats[3].targetable}\n" +

                    $"\nPlayer 4 Faction : {gameEngine.p4.playerFaction}\n" +
                    $"Player 4 boats alive: {gameEngine.p4.rowBoats.Count}\n" +
                    $"-- Boat 1 current tile: {gameEngine.p4.rowBoats[0].CurrentTile}\n" +
                    $"-- Boat 2 current tile: {gameEngine.p4.rowBoats[1].CurrentTile}\n" +
                    $"-- Boat 3 current tile: {gameEngine.p4.rowBoats[2].CurrentTile}\n" +
                    $"-- Boat 4 current tile: {gameEngine.p4.rowBoats[2].CurrentTile}\n" +

                    $"-- Boat 1 active: {gameEngine.p4.rowBoats[0].active}\n" +
                    $"-- Boat 2 active: {gameEngine.p4.rowBoats[1].active}\n" +
                    $"-- Boat 3 active: {gameEngine.p4.rowBoats[2].active}\n" +
                    $"-- Boat 4 active: {gameEngine.p4.rowBoats[3].active}\n" +

                    $"-- Boat 1 targetable: {gameEngine.p4.rowBoats[0].targetable}\n" +
                    $"-- Boat 2 targetable: {gameEngine.p4.rowBoats[1].targetable}\n" +
                    $"-- Boat 3 targetable: {gameEngine.p4.rowBoats[2].targetable}\n" +
                    $"-- Boat 4 targetable: {gameEngine.p4.rowBoats[3].targetable}\n";

                    warningTextArea.Text = "This menu will cause fps and game lag!";
                }).AsTask();
            }
        }

       
    }
}
