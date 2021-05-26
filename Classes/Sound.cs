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
        public static Windows.Storage.StorageFile RequestedMusic;
        public static bool Playing = false;

        public static  async Task SoundPlay()
        {
            Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");

            Windows.Storage.StorageFile music1 = await folder.GetFileAsync(@"Pirates Of The Caribbean Theme Song.mp3");
            Windows.Storage.StorageFile music2 = await folder.GetFileAsync(@"01 - Franklin City.mp3");
            MainPage.player.AutoPlay = false;
            

            switch (MainPage.soundState)
            {
                case 1:
                    RequestedMusic = music1;
                    break;
                
                case 2:
                    RequestedMusic = music2;
                    break;

                default:
                    break;
            }
           
            MainPage.player.Source = MediaSource.CreateFromStorageFile(RequestedMusic);
            if (Playing==false)
            {
                MainPage.player.Source = null;
            }
            else
            {
                MainPage.player.Play();

            }



          
        }
    }
}
