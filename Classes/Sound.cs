using System;
using System.Threading.Tasks;
using Windows.Media.Core;

namespace LudoNewWorld
{
    class Sound
    {
        static Windows.Storage.StorageFile RequestedMusic;
        static Windows.Storage.StorageFile requestedMusic;


        /// <summary>
        /// Play the background music of the entire game, currently disabled
        /// </summary>
        public static async Task SoundPlay()
        {
            //******************************************************************
            //This is disabled as we don't have any copyright free music yet
            //******************************************************************

            Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets\\Sounds");
            Windows.Storage.StorageFile music1 = await folder.GetFileAsync(@""); // Change when implementing new background sound
            RequestedMusic = music1;

            MainPage.mPlayer.AutoPlay = false;
            MainPage.mPlayer.Source = MediaSource.CreateFromStorageFile(RequestedMusic);
            MainPage.mPlayer.Volume = MainPage.currentVolume;
            MainPage.mPlayer.IsLoopingEnabled = true;
            MainPage.mPlayer.Play();
        }


        /// <summary>
        /// The music of the credit in the menu
        /// </summary>
        /// <param name="play"></param>
        /// <returns></returns>
        public static async Task CrediSound(bool play)
        {
            Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets\\Sounds");
            Windows.Storage.StorageFile music1 = await folder.GetFileAsync(@"SHEESH SOUND EFFECT.mp3");
            requestedMusic = music1;

            MainPage.mPlayerr.AutoPlay = false;
            MainPage.mPlayerr.Source = MediaSource.CreateFromStorageFile(requestedMusic);
            MainPage.mPlayerr.Volume = MainPage.currentVolume;

            if (play == false)
            { MainPage.mPlayerr.Source = null;}
            else
            {  MainPage.mPlayerr.Play();}
        }


        /// <summary>
        /// Sound effect of a dice roll
        /// </summary>
        /// <returns></returns>
        public static async Task DiceSound()
        {
            Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets\\Sounds");
            Windows.Storage.StorageFile music1 = await folder.GetFileAsync(@"Rolling Dice - Sound Effect (HD).mp3");
            requestedMusic = music1;

            MainPage.mPlayerr.AutoPlay = false;
            MainPage.mPlayerr.Source = MediaSource.CreateFromStorageFile(requestedMusic);
            MainPage.mPlayerr.Volume = MainPage.currentVolume;
            MainPage.mPlayerr.Play();
        }


        /// <summary>
        /// Sound effect when a boat moves
        /// </summary>
        public static async Task MoveBoat()
        {
            Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets\\Sounds");
            Windows.Storage.StorageFile music1 = await folder.GetFileAsync(@"");
            requestedMusic = music1;

            MainPage.mPlayerr.AutoPlay = false;
            MainPage.mPlayerr.Source = MediaSource.CreateFromStorageFile(requestedMusic);
            MainPage.mPlayerr.Volume = MainPage.currentVolume;
            MainPage.mPlayerr.Play();
        }


        /// <summary>
        /// The postive effect sound on a special tile if the player step on a positive tile. 
        /// </summary>
        /// <param name="IsPlayerOnTile"></param>
        /// <returns></returns>
        public static async Task PositiveSound(bool IsPlayerOnTile)
        {
            Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets\\Sounds");
            Windows.Storage.StorageFile music1 = await folder.GetFileAsync(@"");
            requestedMusic = music1;

            MainPage.mPlayerr.AutoPlay = false;
            MainPage.mPlayerr.Source = MediaSource.CreateFromStorageFile(requestedMusic);
            MainPage.mPlayerr.Volume = MainPage.currentVolume;
            
            if (IsPlayerOnTile == false)
            { MainPage.mPlayerr.Source = null;}
            else
            { MainPage.mPlayerr.Play();}
        }


        /// <summary>
        /// The negative effect sound on a special tile if the player step on a negative tile. 
        /// </summary>
        /// <param name="IsPlayerOnTile"></param>
        /// <returns></returns>
        public static async Task NegativeSound(bool IsPlayerOnTile)
        {
            Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets\\Sounds");
            Windows.Storage.StorageFile music1 = await folder.GetFileAsync(@"");
            requestedMusic = music1;


            MainPage.mPlayerr.AutoPlay = false;
            MainPage.mPlayerr.Source = MediaSource.CreateFromStorageFile(requestedMusic);
            MainPage.mPlayerr.Volume = MainPage.currentVolume;
            if (IsPlayerOnTile == false)
            { MainPage.mPlayerr.Source = null;}
            else
            {MainPage.mPlayerr.Play();}
        }
    }
}
