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
        /// <para>Will change background with highlighted bases based on the current <see cref="GameEngine.PlayerTurn"/></para>
        /// </summary>
        public static void BackgroundManager()
        {
            if (MainPage.gameState == 0)
            {
                GraphicHandler.background = GraphicHandler.menuBackground;
            }
            else if (MainPage.gameState == 1)
            {
                if(GameEngine.GetGameActive())
                {
                    switch (GameEngine.PlayerTurn)
                    {
                        case 1: GraphicHandler.background = GraphicHandler.backgroundBritainActive; break;
                        case 2: GraphicHandler.background = GraphicHandler.backgroundDutchActive; break;
                        case 3: GraphicHandler.background = GraphicHandler.backgroundSpainActive; break;
                        case 4: GraphicHandler.background = GraphicHandler.backgroundFranceActive; break;
                    }
                }
                else
                {
                    GraphicHandler.background = GraphicHandler.gameBackground;
                }
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
            MainPage.showDice = false;
            Player.playerList.Clear();
            GameEngine.factionList.Clear();
            GraphicHandler.rowBoatList.Clear();
            foreach (var tile in GraphicHandler.GetOrderedTiles())
            {
                tile.IsPlayerOnTile = false;
            }            
            MainPage.gameState = 0;
        }
    }
}
