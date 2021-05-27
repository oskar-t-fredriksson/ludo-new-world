using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using LudoNewWorld.Classes;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;

namespace LudoNewWorld
{
    class GraphicHandler
    {
        public static CanvasBitmap background, gameBackground, menuBackground,
        dice1, dice2, dice3, dice4, dice5, dice6,
        NeutralTile, BritainTile, SpainTile, DutchTile, FranceTile, NegativeTile, PositiveTile, RandomTile;
        public static List<GameTile> gameTiles = new List<GameTile>();

        public static void LoadResources()
        {
            CreateTileObjects();
        }

        public static void CreateResources(CanvasAnimatedControl sender, CanvasCreateResourcesEventArgs args)
        {
            args.TrackAsyncAction(CreateResourceAsync(sender).AsAsyncAction());
        }

        private static async Task CreateResourceAsync(CanvasAnimatedControl sender)
        {
            //Background
            gameBackground = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/bg.png"));
            menuBackground = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/menuBackground.png"));

            //Tiles
            BritainTile = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/GameTiles/BritainTile.png"));
            DutchTile = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/GameTiles/DutchTile.png"));
            SpainTile = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/GameTiles/SpainTile.png"));
            FranceTile = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/GameTiles/FranceTile.png"));
            NeutralTile = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/GameTiles/NeutralTile.png"));
            NegativeTile = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/GameTiles/NegativeTile.png"));
            PositiveTile = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/GameTiles/PositiveTile.png"));
            RandomTile = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/GameTiles/RandomTile.png"));
        }

        public static void Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            GameStateManager.BackgroundManager();
            args.DrawingSession.DrawImage(Scaler.Fit(background));
            if (MainPage.gameState == 1)
            {
                args.DrawingSession.DrawImage(Scaler.Fit(BritainTile), gameTiles[0].GameTileVector);
                args.DrawingSession.DrawImage(Scaler.Fit(NeutralTile), gameTiles[1].GameTileVector);
                args.DrawingSession.DrawImage(Scaler.Fit(NeutralTile), gameTiles[2].GameTileVector);
                args.DrawingSession.DrawImage(Scaler.Fit(NeutralTile), gameTiles[3].GameTileVector);
                args.DrawingSession.DrawImage(Scaler.Fit(NegativeTile), gameTiles[4].GameTileVector);
                args.DrawingSession.DrawImage(Scaler.Fit(PositiveTile), gameTiles[5].GameTileVector);
                args.DrawingSession.DrawImage(Scaler.Fit(RandomTile), gameTiles[6].GameTileVector);
            }
        }
        private static void CreateTileObjects()
        {
            gameTiles.Add(new GameTile(Tile.StartTile, FactionTile.BritainTile, new Vector2(300, 400)));
            gameTiles.Add(new GameTile(Tile.NeutralTile, new Vector2(400, 400)));
            gameTiles.Add(new GameTile(Tile.NeutralTile, new Vector2(500, 400)));
            gameTiles.Add(new GameTile(Tile.NeutralTile, new Vector2(600, 400)));
            gameTiles.Add(new GameTile(Tile.NegativeTile, new Vector2(600, 500)));
            gameTiles.Add(new GameTile(Tile.PositiveTile, new Vector2(600, 600)));
            gameTiles.Add(new GameTile(Tile.RandomTile, new Vector2(600, 700)));
        }
    }
}
