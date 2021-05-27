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
                GraphicHandler.background = GraphicHandler.menuBackground;
            }
            else if (MainPage.gameState == 1)
            {
                GraphicHandler.background = GraphicHandler.gameBackground;
            }
        }
    }
}
