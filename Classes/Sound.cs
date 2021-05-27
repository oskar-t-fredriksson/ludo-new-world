using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;
using LudoNewWorld.Classes;

namespace LudoNewWorld
{
    class Sound
    {
        static Windows.Storage.StorageFile RequestedMusic;
        public static async Task SoundPlay()
        {
            Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets\\Sounds");
            Windows.Storage.StorageFile music1 = await folder.GetFileAsync(@"placeholder.mp3");
            MainPage.mPlayer.AutoPlay = false;
            RequestedMusic = music1;

            MainPage.mPlayer.Source = MediaSource.CreateFromStorageFile(RequestedMusic);
            MainPage.mPlayer.Play();
        }
    }
}
