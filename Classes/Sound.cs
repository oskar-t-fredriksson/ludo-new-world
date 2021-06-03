using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;
using LudoNewWorld.Classes;
using Windows.UI.Xaml;

namespace LudoNewWorld
{
    class Sound
    {
        static Windows.Storage.StorageFile RequestedMusic;
        static Windows.Storage.StorageFile requestedMusic;
      
        public static async Task SoundPlay()
        {
            Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets\\Sounds");
            Windows.Storage.StorageFile music1 = await folder.GetFileAsync(@"placeholder.mp3");
            RequestedMusic = music1;

            MainPage.mPlayer.AutoPlay = false;
            MainPage.mPlayer.Source = MediaSource.CreateFromStorageFile(RequestedMusic);
            MainPage.mPlayer.Volume = MainPage.currentVolume;
            MainPage.mPlayer.IsLoopingEnabled = true;
            MainPage.mPlayer.Play();
           


        }
        public static async Task PositivEffect()
        {
            Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets\\Sounds");
            Windows.Storage.StorageFile music1 = await folder.GetFileAsync(@"SHEESH SOUND EFFECT.mp3");
            requestedMusic = music1;


            MainPage.mPlayerr.AutoPlay = false;
            MainPage.mPlayerr.Source = MediaSource.CreateFromStorageFile(requestedMusic);
            MainPage.mPlayerr.Volume = MainPage.currentVolume;
            //MainPage.mPlayerr.IsLoopingEnabled = true;
            MainPage.mPlayerr.Play();

        }
        public static async Task DiceSound()
        {
            Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets\\Sounds");
            Windows.Storage.StorageFile music1 = await folder.GetFileAsync(@"Rolling Dice - Sound Effect (HD).mp3");
            requestedMusic = music1;


            MainPage.mPlayerr.AutoPlay = false;
            MainPage.mPlayerr.Source = MediaSource.CreateFromStorageFile(requestedMusic);
            MainPage.mPlayerr.Volume = MainPage.currentVolume;
            //MainPage.mPlayerr.IsLoopingEnabled = true;
            MainPage.mPlayerr.Play();

        }
    }
}
