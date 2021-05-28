﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        dice1, dice2, dice3, dice4, dice5, dice6, britishDisplayDice, dutchDisplayDice, spainDisplayDice, franceDisplayDice,
        dice1Inactive, dice2Inactive, dice3Inactive, dice4Inactive, dice5Inactive, dice6Inactive,
        NeutralTile, BritainTile, SpainTile, DutchTile, FranceTile, NegativeTile, PositiveTile, RandomTile, 
        BritainGoalTile, DutchGoalTile, FranceGoalTile, SpainGoalTile;

        // List
        static List<GameTile> gameTiles = new List<GameTile>();
        static List<CanvasBitmap> diceBitmapList = new List<CanvasBitmap>();
        public static List<CanvasBitmap> diceInactiveBitmapList = new List<CanvasBitmap>();

        // Objects
        private readonly Random _random = new Random();

        public static int scrambleDice(int playerID)
        {
            Dice dice = new Dice();
            int trueNumber = dice.Roll();
            Debug.WriteLine("Dice rolled: " + trueNumber);
            switch (playerID)
            {
                case 1:
                    for (int i = 0; i < 10000000; i++)
                    {
                        switch (dice.Roll())
                        {
                            case 1: britishDisplayDice = diceBitmapList[0]; break;
                            case 2: britishDisplayDice = diceBitmapList[1]; break;
                            case 3: britishDisplayDice = diceBitmapList[2]; break;
                            case 4: britishDisplayDice = diceBitmapList[3]; break;
                            case 5: britishDisplayDice = diceBitmapList[4]; break;
                            case 6: britishDisplayDice = diceBitmapList[5]; break;
                            default: break;
                        }
                    }
                    britishDisplayDice = diceBitmapList[trueNumber - 1]; break;
                case 2:
                    for (int i = 0; i < 10000000; i++)
                    {
                        switch (dice.Roll())
                        {
                            case 1: dutchDisplayDice = diceBitmapList[0]; break;
                            case 2: dutchDisplayDice = diceBitmapList[1]; break;
                            case 3: dutchDisplayDice = diceBitmapList[2]; break;
                            case 4: dutchDisplayDice = diceBitmapList[3]; break;
                            case 5: dutchDisplayDice = diceBitmapList[4]; break;
                            case 6: dutchDisplayDice = diceBitmapList[5]; break;
                            default: break;
                        }
                    }
                    dutchDisplayDice = diceBitmapList[trueNumber - 1]; break;
                case 3:
                    for (int i = 0; i < 10000000; i++)
                    {
                        switch (dice.Roll())
                        {
                            case 1: spainDisplayDice = diceBitmapList[0]; break;
                            case 2: spainDisplayDice = diceBitmapList[1]; break;
                            case 3: spainDisplayDice = diceBitmapList[2]; break;
                            case 4: spainDisplayDice = diceBitmapList[3]; break;
                            case 5: spainDisplayDice = diceBitmapList[4]; break;
                            case 6: spainDisplayDice = diceBitmapList[5]; break;
                            default: break;
                        }
                    }
                    spainDisplayDice = diceBitmapList[trueNumber - 1]; break;
                case 4:
                    for (int i = 0; i < 10000000; i++)
                    {
                        switch (dice.Roll())
                        {
                            case 1: franceDisplayDice = diceBitmapList[0]; break;
                            case 2: franceDisplayDice = diceBitmapList[1]; break;
                            case 3: franceDisplayDice = diceBitmapList[2]; break;
                            case 4: franceDisplayDice = diceBitmapList[3]; break;
                            case 5: franceDisplayDice = diceBitmapList[4]; break;
                            case 6: franceDisplayDice = diceBitmapList[5]; break;
                            default: break;
                        }
                    }
                    franceDisplayDice = diceBitmapList[trueNumber - 1]; break;
                default: break;
            }
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
            BritainGoalTile = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/GameTiles/BritainGoalTile.png"));
            DutchGoalTile = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/GameTiles/DutchGoalTile.png"));
            FranceGoalTile = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/GameTiles/FranceGoalTile.png"));
            SpainGoalTile = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/GameTiles/SpainGoalTile.png"));

            // Dice
            diceBitmapList.Add(dice1 = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Dices/dice1.png")));
            diceBitmapList.Add(dice2 = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Dices/dice2.png")));
            diceBitmapList.Add(dice3 = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Dices/dice3.png")));
            diceBitmapList.Add(dice4 = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Dices/dice4.png")));
            diceBitmapList.Add(dice5 = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Dices/dice5.png")));
            diceBitmapList.Add(dice6 = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Dices/dice6.png")));

            // Dice inactive
            diceInactiveBitmapList.Add(dice1Inactive = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Dices/dice1.png")));
            diceInactiveBitmapList.Add(dice2Inactive = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Dices/dice2.png")));
            diceInactiveBitmapList.Add(dice3Inactive = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Dices/dice3.png")));
            diceInactiveBitmapList.Add(dice4Inactive = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Dices/dice4.png")));
            diceInactiveBitmapList.Add(dice5Inactive = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Dices/dice5.png")));
            diceInactiveBitmapList.Add(dice6Inactive = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Dices/dice6.png")));

            // Dice Locations per faction
            britishDisplayDice = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Dices/dice0.png"));
            dutchDisplayDice = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Dices/dice0_inactive.png"));
            spainDisplayDice = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Dices/dice0_inactive.png"));
            franceDisplayDice = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Dices/dice0_inactive.png"));
        }

        public static void Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            GameStateManager.BackgroundManager();
            args.DrawingSession.DrawImage(Scaler.Fit(background));
            if (MainPage.gameState == 1)
            {
                foreach (var tile in gameTiles)
                {
                    if (tile.FactionType != FactionTile.FactionNull && tile.TileType == Tile.GoalTile || tile.FactionType != FactionTile.FactionNull && tile.TileType == Tile.StartTile || tile.FactionType != FactionTile.FactionNull && tile.TileType == Tile.BaseTile)
                    {
                        switch (tile.FactionType)
                        {
                            case FactionTile.BritainTile:
                                args.DrawingSession.DrawImage(Scaler.Fit(BritainTile), Scaler.Cords(tile.GameTileVector));
                                break;
                            case FactionTile.SpainTile:
                                args.DrawingSession.DrawImage(Scaler.Fit(SpainTile), Scaler.Cords(tile.GameTileVector));
                                break;
                            case FactionTile.DutchTile:
                                args.DrawingSession.DrawImage(Scaler.Fit(DutchTile), Scaler.Cords(tile.GameTileVector));
                                break;
                            case FactionTile.FranceTile:
                                args.DrawingSession.DrawImage(Scaler.Fit(FranceTile), Scaler.Cords(tile.GameTileVector));
                                break;
                            default:
                                args.DrawingSession.DrawImage(Scaler.Fit(NeutralTile), Scaler.Cords(tile.GameTileVector));
                                break;
                        }
                    }
                    else if (tile.FactionType != FactionTile.FactionNull && tile.TileType == Tile.NeutralTile)
                    {
                        switch (tile.FactionType)
                        {
                            case FactionTile.BritainTile:
                                args.DrawingSession.DrawImage(Scaler.Fit(BritainGoalTile), Scaler.Cords(tile.GameTileVector));
                                break;
                            case FactionTile.SpainTile:
                                args.DrawingSession.DrawImage(Scaler.Fit(SpainGoalTile), Scaler.Cords(tile.GameTileVector));
                                break;
                            case FactionTile.DutchTile:
                                args.DrawingSession.DrawImage(Scaler.Fit(DutchGoalTile), Scaler.Cords(tile.GameTileVector));
                                break;
                            case FactionTile.FranceTile:
                                args.DrawingSession.DrawImage(Scaler.Fit(FranceGoalTile), Scaler.Cords(tile.GameTileVector));
                                break;
                            default:
                                args.DrawingSession.DrawImage(Scaler.Fit(NeutralTile), Scaler.Cords(tile.GameTileVector));
                                break;
                        }
                    }
                    else if(tile.TileType == Tile.NegativeTile || tile.TileType == Tile.PositiveTile || tile.TileType == Tile.RandomTile)
                    {
                        switch (tile.TileType)
                        {
                            case Tile.NegativeTile:
                                args.DrawingSession.DrawImage(Scaler.Fit(NegativeTile), Scaler.Cords(tile.GameTileVector));
                                break;
                            case Tile.PositiveTile:
                                args.DrawingSession.DrawImage(Scaler.Fit(PositiveTile), Scaler.Cords(tile.GameTileVector));
                                break;
                            case Tile.RandomTile:
                                args.DrawingSession.DrawImage(Scaler.Fit(RandomTile), Scaler.Cords(tile.GameTileVector));
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        args.DrawingSession.DrawImage(Scaler.Fit(NeutralTile), Scaler.Cords(tile.GameTileVector));
                    }
                }
                // Draw 4 x dice on the map
                args.DrawingSession.DrawImage(Scaler.Fit(britishDisplayDice), Scaler.Cords(new Vector2(362, 184)));
                args.DrawingSession.DrawImage(Scaler.Fit(dutchDisplayDice), Scaler.Cords(new Vector2(1558, 184)));
                args.DrawingSession.DrawImage(Scaler.Fit(spainDisplayDice), Scaler.Cords(new Vector2(1558, 896)));
                args.DrawingSession.DrawImage(Scaler.Fit(franceDisplayDice), Scaler.Cords(new Vector2(362, 896)));
            }
        }
        private static void CreateTileObjects()
        {

            //Faction Tiles
            //Britain Base Tile
            gameTiles.Add(new GameTile(Tile.BaseTile, FactionTile.BritainTile, new Vector2(190, 150)));
            gameTiles.Add(new GameTile(Tile.BaseTile, FactionTile.BritainTile, new Vector2(190, 230)));
            gameTiles.Add(new GameTile(Tile.BaseTile, FactionTile.BritainTile, new Vector2(270, 150)));
            gameTiles.Add(new GameTile(Tile.BaseTile, FactionTile.BritainTile, new Vector2(270, 230)));

            //Dutch Base Tile
            gameTiles.Add(new GameTile(Tile.BaseTile, FactionTile.DutchTile, new Vector2(1730, 150)));
            gameTiles.Add(new GameTile(Tile.BaseTile, FactionTile.DutchTile, new Vector2(1730, 230)));
            gameTiles.Add(new GameTile(Tile.BaseTile, FactionTile.DutchTile, new Vector2(1650, 150)));
            gameTiles.Add(new GameTile(Tile.BaseTile, FactionTile.DutchTile, new Vector2(1650, 230)));

            //France Base Tile
            gameTiles.Add(new GameTile(Tile.BaseTile, FactionTile.FranceTile, new Vector2(190, 930)));
            gameTiles.Add(new GameTile(Tile.BaseTile, FactionTile.FranceTile, new Vector2(190, 850)));
            gameTiles.Add(new GameTile(Tile.BaseTile, FactionTile.FranceTile, new Vector2(270, 930)));
            gameTiles.Add(new GameTile(Tile.BaseTile, FactionTile.FranceTile, new Vector2(270, 850)));

            //Spain Base Tile
            gameTiles.Add(new GameTile(Tile.BaseTile, FactionTile.SpainTile, new Vector2(1730, 930)));
            gameTiles.Add(new GameTile(Tile.BaseTile, FactionTile.SpainTile, new Vector2(1730, 850)));
            gameTiles.Add(new GameTile(Tile.BaseTile, FactionTile.SpainTile, new Vector2(1650, 930)));
            gameTiles.Add(new GameTile(Tile.BaseTile, FactionTile.SpainTile, new Vector2(1650, 850)));

            //Britain Goal Tile 
            gameTiles.Add(new GameTile(Tile.GoalTile, FactionTile.BritainTile, new Vector2(420, 540)));
            gameTiles.Add(new GameTile(Tile.GoalTile, FactionTile.BritainTile, new Vector2(500, 540)));
            gameTiles.Add(new GameTile(Tile.GoalTile, FactionTile.BritainTile, new Vector2(580, 540)));
            gameTiles.Add(new GameTile(Tile.GoalTile, FactionTile.BritainTile, new Vector2(660, 540)));
            gameTiles.Add(new GameTile(Tile.NeutralTile, FactionTile.BritainTile, new Vector2(340, 540)));            

            //Dutch Goal Tile 
            gameTiles.Add(new GameTile(Tile.GoalTile, FactionTile.DutchTile, new Vector2(960, 150)));
            gameTiles.Add(new GameTile(Tile.GoalTile, FactionTile.DutchTile, new Vector2(960, 230)));
            gameTiles.Add(new GameTile(Tile.GoalTile, FactionTile.DutchTile, new Vector2(960, 310)));
            gameTiles.Add(new GameTile(Tile.GoalTile, FactionTile.DutchTile, new Vector2(960, 390)));
            gameTiles.Add(new GameTile(Tile.NeutralTile, FactionTile.DutchTile, new Vector2(960, 70)));
            
            //France Goal Tile 
            gameTiles.Add(new GameTile(Tile.GoalTile, FactionTile.FranceTile, new Vector2(960, 930)));
            gameTiles.Add(new GameTile(Tile.GoalTile, FactionTile.FranceTile, new Vector2(960, 850)));
            gameTiles.Add(new GameTile(Tile.GoalTile, FactionTile.FranceTile, new Vector2(960, 770)));
            gameTiles.Add(new GameTile(Tile.GoalTile, FactionTile.FranceTile, new Vector2(960, 690)));
            gameTiles.Add(new GameTile(Tile.NeutralTile, FactionTile.FranceTile, new Vector2(960, 1010)));

            //Spain Goal Tile 
            gameTiles.Add(new GameTile(Tile.GoalTile, FactionTile.SpainTile, new Vector2(1500, 540)));
            gameTiles.Add(new GameTile(Tile.GoalTile, FactionTile.SpainTile, new Vector2(1420, 540)));
            gameTiles.Add(new GameTile(Tile.GoalTile, FactionTile.SpainTile, new Vector2(1340, 540)));
            gameTiles.Add(new GameTile(Tile.GoalTile, FactionTile.SpainTile, new Vector2(1260, 540)));
            gameTiles.Add(new GameTile(Tile.NeutralTile, FactionTile.SpainTile, new Vector2(1580, 540)));            

            //Britain Start Tile
            gameTiles.Add(new GameTile(Tile.GoalTile, FactionTile.BritainTile, new Vector2(340, 460)));

            //Dutch Start Tile
            gameTiles.Add(new GameTile(Tile.GoalTile, FactionTile.DutchTile, new Vector2(1040, 70)));

            //France Start Tile
            gameTiles.Add(new GameTile(Tile.GoalTile, FactionTile.FranceTile, new Vector2(880, 1010)));

            //Spain Start Tile
            gameTiles.Add(new GameTile(Tile.GoalTile, FactionTile.SpainTile, new Vector2(1580, 620)));

            //Neutral Tile Britain side
            gameTiles.Add(new GameTile(Tile.NeutralTile, new Vector2(420, 420)));
            gameTiles.Add(new GameTile(Tile.NeutralTile, new Vector2(540, 300)));
            gameTiles.Add(new GameTile(Tile.NeutralTile, new Vector2(590, 140)));
            gameTiles.Add(new GameTile(Tile.NeutralTile, new Vector2(720, 70)));
            gameTiles.Add(new GameTile(Tile.NeutralTile, new Vector2(800, 70)));
            gameTiles.Add(new GameTile(Tile.NeutralTile, new Vector2(880, 70)));            

            //Neutral Tile Dutch side
            gameTiles.Add(new GameTile(Tile.NeutralTile, new Vector2(1120, 70)));
            gameTiles.Add(new GameTile(Tile.NeutralTile, new Vector2(1280, 80)));
            gameTiles.Add(new GameTile(Tile.NeutralTile, new Vector2(1360, 220)));
            gameTiles.Add(new GameTile(Tile.NeutralTile, new Vector2(1420, 380)));
            gameTiles.Add(new GameTile(Tile.NeutralTile, new Vector2(1500, 420)));
            gameTiles.Add(new GameTile(Tile.NeutralTile, new Vector2(1580, 460)));           

            //Neutral Tile Spain side
            gameTiles.Add(new GameTile(Tile.NeutralTile, new Vector2(1500, 660)));
            gameTiles.Add(new GameTile(Tile.NeutralTile, new Vector2(1380, 780)));
            gameTiles.Add(new GameTile(Tile.NeutralTile, new Vector2(1330, 940)));
            gameTiles.Add(new GameTile(Tile.NeutralTile, new Vector2(1200, 1010)));
            gameTiles.Add(new GameTile(Tile.NeutralTile, new Vector2(1120, 1010)));
            gameTiles.Add(new GameTile(Tile.NeutralTile, new Vector2(1040, 1010)));

            //Neutral Tile France side
            gameTiles.Add(new GameTile(Tile.NeutralTile, new Vector2(800, 1010)));
            gameTiles.Add(new GameTile(Tile.NeutralTile, new Vector2(640, 1000)));
            gameTiles.Add(new GameTile(Tile.NeutralTile, new Vector2(560, 860)));
            gameTiles.Add(new GameTile(Tile.NeutralTile, new Vector2(500, 700)));
            gameTiles.Add(new GameTile(Tile.NeutralTile, new Vector2(420, 660)));
            gameTiles.Add(new GameTile(Tile.NeutralTile, new Vector2(340, 620)));

            //Britain side Effects Tile
            gameTiles.Add(new GameTile(Tile.PositiveTile, new Vector2(500, 380)));
            gameTiles.Add(new GameTile(Tile.RandomTile, new Vector2(560, 220)));
            gameTiles.Add(new GameTile(Tile.NegativeTile, new Vector2(640, 80)));

            //Dutch side Effects Tile
            gameTiles.Add(new GameTile(Tile.PositiveTile, new Vector2(1200, 70)));
            gameTiles.Add(new GameTile(Tile.RandomTile, new Vector2(1330, 140)));
            gameTiles.Add(new GameTile(Tile.NegativeTile, new Vector2(1380, 300)));

            //Spain side Effects Tile
            gameTiles.Add(new GameTile(Tile.PositiveTile, new Vector2(1420, 700)));
            gameTiles.Add(new GameTile(Tile.RandomTile, new Vector2(1360, 860)));
            gameTiles.Add(new GameTile(Tile.NegativeTile, new Vector2(1280, 1000)));

            //France side Effects Tile
            gameTiles.Add(new GameTile(Tile.PositiveTile, new Vector2(720, 1010)));
            gameTiles.Add(new GameTile(Tile.RandomTile, new Vector2(590, 940)));
            gameTiles.Add(new GameTile(Tile.NegativeTile, new Vector2(540, 780)));
        }
    }
}
