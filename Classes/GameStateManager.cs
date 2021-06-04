using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LudoNewWorld.Classes;

namespace LudoNewWorld
{
    class GameStateManager
    {
        public static void BackgroundManager()
        {
            if (MainPage.gameState == 0)
            {
                GraphicHandler.background = GraphicHandler.menuBackground;
            }
            else if (MainPage.gameState == 1)
            {
                GraphicHandler.background = GraphicHandler.gameBackground;
            }
            else if (MainPage.gameState == 2)
            {
                Player.playerList.Clear();
                Debug.WriteLine("listan raderades");
                Debug.WriteLine(Player.playerList.Count);
                GameEngine.factionList.Clear();
                Debug.WriteLine(GameEngine.factionList.Count);
                GraphicHandler.rowBoatList.Clear();
                Debug.WriteLine($"listan raderades och det finns {GraphicHandler.rowBoatList.Count}");
                MainPage.gameState = 0;
                

            }
        }
    }
}
