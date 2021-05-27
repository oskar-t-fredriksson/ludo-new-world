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
        public static Rect bounds = ApplicationView.GetForCurrentView().VisibleBounds;
        public static Vector3 scaleVector3Variable = new Vector3(DesignWidth, DesignHeight, 1);
        public static MediaPlayer mPlayer = new MediaPlayer();
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
            Sound.SoundPlay();
            GraphicHandler.LoadResources();
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

        private void GameCanvas_CreateResources(CanvasAnimatedControl sender, CanvasCreateResourcesEventArgs args)
        {
            GraphicHandler.CreateResources(sender, args);
        }

        private void GameCanvas_Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            GraphicHandler.Draw(sender, args);
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
    }
}
