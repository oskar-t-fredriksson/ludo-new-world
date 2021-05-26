using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;

namespace LudoNewWorld.Classes
{
    class Sound
    {
        public static  async Task SoundPlay()
        {
            Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");

            Windows.Storage.StorageFile file = await folder.GetFileAsync(@"01 - Franklin City.mp3");
            MainPage.player.AutoPlay = false;
            MainPage.player.Source = MediaSource.CreateFromStorageFile(file);
            MainPage.player.Play();
        }
    }
}
