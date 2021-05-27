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
        dice1, dice2, dice3, dice4, dice5, dice6, displayDice,
        NeutralTile, BritainTile, SpainTile, DutchTile, FranceTile, NegativeTile, PositiveTile, RandomTile;

        // List
        static List<GameTile> gameTiles = new List<GameTile>();
        static List<CanvasBitmap> diceBitmapList = new List<CanvasBitmap>();

        // Objects
        private readonly Random _random = new Random();

        public static int scrambleDice()
        {
            Dice dice = new Dice();
            int trueNumber = dice.Roll();
            for (int i = 0; i < 10000000; i++)
            {
                switch (dice.Roll())
                {
                    case 1: displayDice = diceBitmapList[1]; break;
                    case 2: displayDice = diceBitmapList[2]; break;
                    case 3: displayDice = diceBitmapList[3]; break;
                    case 4: displayDice = diceBitmapList[4]; break;
                    case 5: displayDice = diceBitmapList[5]; break;
                    case 6: displayDice = diceBitmapList[6]; break;
                    default: break;
                }
            }
            displayDice = diceBitmapList[trueNumber];
            return trueNumber;
        }

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

            // Dice
            diceBitmapList.Add(displayDice = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Dices/dice0.png")));
            diceBitmapList.Add(dice1 = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Dices/dice1.png")));
            diceBitmapList.Add(dice2 = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Dices/dice2.png")));
            diceBitmapList.Add(dice3 = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Dices/dice3.png")));
            diceBitmapList.Add(dice4 = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Dices/dice4.png")));
            diceBitmapList.Add(dice5 = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Dices/dice5.png")));
            diceBitmapList.Add(dice6 = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Dices/dice6.png")));
        }

        public static void Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            GameStateManager.BackgroundManager();
            args.DrawingSession.DrawImage(Scaler.Fit(background));
            if (MainPage.gameState == 1)
            {
                args.DrawingSession.DrawImage(Scaler.Fit(GraphicHandler.displayDice), 0, 0);
                args.DrawingSession.DrawImage(Scaler.Fit(BritainTile), Scaler.Cords(gameTiles[0].GameTileVector));
                args.DrawingSession.DrawImage(Scaler.Fit(NeutralTile), Scaler.Cords(gameTiles[1].GameTileVector));
                args.DrawingSession.DrawImage(Scaler.Fit(NeutralTile), Scaler.Cords(gameTiles[2].GameTileVector));
                args.DrawingSession.DrawImage(Scaler.Fit(NeutralTile), Scaler.Cords(gameTiles[3].GameTileVector));
                args.DrawingSession.DrawImage(Scaler.Fit(NegativeTile), Scaler.Cords(gameTiles[4].GameTileVector));
                args.DrawingSession.DrawImage(Scaler.Fit(PositiveTile), Scaler.Cords(gameTiles[5].GameTileVector));
                args.DrawingSession.DrawImage(Scaler.Fit(RandomTile), Scaler.Cords(gameTiles[6].GameTileVector));
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
