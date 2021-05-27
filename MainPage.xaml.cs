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
using Windows.UI.Xaml.Media.Imaging;


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
        public static string playerFaction;
        public static MediaPlayer mPlayer = new MediaPlayer();
        public static double currentVolume = 0.5;
        public static int volumeLevel = 5;
        public static bool volumeMute = false;
        public Vector3 scaleVector3Variable = new Vector3(DesignWidth, DesignHeight, 1);
        
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

        public  void btnRoll_Click(object sender, RoutedEventArgs e)
        {
            GraphicHandler.scrambleDice();
        }
        private void GameCanvas_Loaded(object sender, RoutedEventArgs e){}

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
            Dice.Visibility = Visibility.Visible;
            gameState = 1;
        }

        private async void btnFrance_Click(object sender, RoutedEventArgs e)
        {
            playerFaction = "France";
            FactionField.Visibility = Visibility.Collapsed;
            Dice.Visibility = Visibility.Visible;
            gameState = 1;
        }
 
        private void btnDutch_Click(object sender, RoutedEventArgs e)
        {
            playerFaction = "Dutch";
            FactionField.Visibility = Visibility.Collapsed;
            Dice.Visibility = Visibility.Visible;
            gameState = 1;
        }
        private void btnSpain_Click(object sender, RoutedEventArgs e)
        {
            playerFaction = "Spain";
            FactionField.Visibility = Visibility.Collapsed;
            Dice.Visibility = Visibility.Visible;
            gameState = 1;
        }
        private void BtnMuteVolume_Click(object sender, RoutedEventArgs e)
        {
            if (!volumeMute)
            {
                Debug.WriteLine("Volume muted");
                mPlayer.Volume = 0;
                volumeMute = true;
                muteButtonChange.Source = new BitmapImage(new Uri(base.BaseUri, @"/Assets/Images/Menu/volumemute.png"));
            }
            else if (volumeMute)
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
            }
            Debug.WriteLine("Volume: " + mPlayer.Volume);
        }
        private void BtnMenuHelp_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
