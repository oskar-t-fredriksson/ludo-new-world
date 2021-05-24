using System;
using System.Collections.Generic;
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
                MainPage.background = MainPage.menuBackground;
            }
            else if (MainPage.gameState == 1)
            {
                MainPage.background = MainPage.gameBackground;
            }
        }
    }
}
