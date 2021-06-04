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
        /// <summary>
        /// Changes background view depending on the current game state
        /// </summary>
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
                ResetGame();
            }
        }
        /// <summary>
        /// Clears all objects on the board
        /// </summary>
        private static void ResetGame()
        {
            Player.playerList.Clear();
            GameEngine.factionList.Clear();
            GraphicHandler.rowBoatList.Clear();
            MainPage.gameState = 0;
        }
    }
}
