using System;
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
        public static CanvasBitmap background, backgroundBritainActive, backgroundDutchActive, backgroundSpainActive, backgroundFranceActive ,gameBackground, menuBackground,
        dice1, dice2, dice3, dice4, dice5, dice6, britishDisplayDice, dutchDisplayDice, spainDisplayDice, franceDisplayDice,
        dice1Inactive, dice2Inactive, dice3Inactive, dice4Inactive, dice5Inactive, dice6Inactive,
        NeutralTile, BritainTile, SpainTile, DutchTile, FranceTile, NegativeTile, PositiveTile, RandomTile,
        BritainGoalTile, DutchGoalTile, FranceGoalTile, SpainGoalTile,
        BritainSmallShip, BritainSmallShipActive, BritainSmallShipTarget, DutchSmallShip, DutchSmallShipActive, DutchSmallShipTarget,
        SpainSmallShip, SpainSmallShipActive, SpainSmallShipTarget, FranceSmallShip, FranceSmallShipActive, FranceSmallShipTarget,
        helpmenuBackground, instructionsBackground, creditsBackground, MoveableTile;

        // List GameTile
        private static List<GameTile> orderedTiles = new List<GameTile>();
        private static List<GameTile> britainGoalTiles = new List<GameTile>();
        private static List<GameTile> dutchGoalTiles = new List<GameTile>();
        private static List<GameTile> spainGoalTiles = new List<GameTile>();
        private static List<GameTile> franceGoalTiles = new List<GameTile>();
        private static List<List<GameTile>> allGoalTiles = new List<List<GameTile>>();
        private static List<GameTile> britainBaseTiles = new List<GameTile>();
        private static List<GameTile> dutchBaseTiles = new List<GameTile>();
        private static List<GameTile> spainBaseTiles = new List<GameTile>();
        private static List<GameTile> franceBaseTiles = new List<GameTile>();
        private static List<List<GameTile>> allBaseTiles = new List<List<GameTile>>();

        //List Images
        static List<CanvasBitmap> diceBitmapList = new List<CanvasBitmap>();
        public static List<CanvasBitmap> diceInactiveBitmapList = new List<CanvasBitmap>();

        //List rowBoat obejects
        public static List<Player.RowBoat> rowBoatList = new List<Player.RowBoat>();

        // Objects
        private readonly Random _random = new Random();
        public static GameTile highlighter = new GameTile(Tile.HighlightTile, new Vector2(2000, 2000));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="playerID"></param>
        /// <returns></returns>
        public static int scrambleDice(int playerID)
        {
            //// TESTING PURPOSE.
            // Player.playerList[0].rowBoats.Clear();
            //Debug.WriteLine($"{Player.playerList[0].rowBoats.Count()} finns i listan nu");
            //GameEngine.CheckWin();

            Sound.DiceSound();
            Dice dice = new Dice();
            int trueNumber = dice.Roll();
            Debug.WriteLine(GameEngine.GetActivePlayer().playerFaction + " rolled: " + trueNumber);
           
           
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

        /// <summary>
        /// Adds all tile objects to appointed lists
        /// </summary>
        public static void LoadResources()
        {
            AddBoardTilesOrdered();
            BritainGoalTilesToList();
            DutchGoalTilesToList();
            SpainGoalTilesToList();
            FranceGoalTilesToList();
            BritainBaseTilesToList();
            DutchBaseTilesToList();
            SpainBaseTilesToList();
            FranceBaseTilesToList();
        }

        public static void CreateResources(CanvasAnimatedControl sender, CanvasCreateResourcesEventArgs args)
        {
            args.TrackAsyncAction(CreateResourceAsync(sender).AsAsyncAction());
        }

        private static async Task CreateResourceAsync(CanvasAnimatedControl sender)
        {
            //Background
            gameBackground = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Background/bg.png"));
            backgroundBritainActive = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Background/bg_britain_active.png"));
            backgroundDutchActive = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Background/bg_dutch_active.png"));
            backgroundSpainActive = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Background/bg_spain_active.png"));
            backgroundFranceActive = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Background/bg_france_active.png"));
            menuBackground = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Background/menuBackground.png"));
            helpmenuBackground = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/helpMenuNew.png"));
            instructionsBackground = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/instruct_placeholder.png"));
            creditsBackground = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Credits.png"));

            //Tiles
            MoveableTile = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/GameTiles/MovableTilePlaceholder.png"));
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
          
            //Player ships
            BritainSmallShip = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Ships/BritainSmallShip.png"));
            BritainSmallShipActive = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Ships/BritainSmallShipActive.png"));
            BritainSmallShipTarget = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Ships/BritainSmallShipTarget.png"));
            DutchSmallShip = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Ships/DutchSmallShip.png"));
            DutchSmallShipActive = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Ships/DutchSmallShipActive.png"));
            DutchSmallShipTarget = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Ships/DutchSmallShipTarget.png"));
            SpainSmallShip = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Ships/SpainSmallShip.png"));
            SpainSmallShipActive = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Ships/SpainSmallShipActive.png"));
            SpainSmallShipTarget = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Ships/SpainSmallShipTarget.png"));
            FranceSmallShip = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Ships/FranceSmallShip.png"));
            FranceSmallShipActive = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Ships/FranceSmallShipActive.png"));
            FranceSmallShipTarget = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Assets/Images/Ships/FranceSmallShipTarget.png"));
        }

        public static void Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            GameEngine.Tick++;
            GameStateManager.BackgroundManager();
            args.DrawingSession.DrawImage(Scaler.Fit(background));
            args.DrawingSession.DrawImage(Scaler.Fit(MoveableTile), Scaler.Cords(highlighter.GameTileVector));

            if (MainPage.gameState == 1)
            {
                foreach (var tile in orderedTiles)
                {
                    tile.ScaledVector = Scaler.TileCords(tile.GameTileVector);
                    DrawPlayboardOutsideCircle(args, tile);
                }
                foreach (var list in allBaseTiles)
                {
                    DrawBaseTiles(args, list);
                }

                foreach (var list in allGoalTiles)
                {
                    DrawGoalTiles(args, list);
                }
                
                // Draw 4 x dice on the map
                DrawDices(args);

                // Draw all ship objects created by the GameEngine
                foreach (var ship in rowBoatList)
                {
                    ship.scaledVector = Scaler.Cords(ship.Vector);
                    switch (ship.Faction)
                    {
                        case Faction.Britain:
                            DrawShipStates(args, ship, BritainSmallShipActive, BritainSmallShipTarget, BritainSmallShip); break;
                        case Faction.Dutch:
                            DrawShipStates(args, ship, DutchSmallShipActive, DutchSmallShipTarget, DutchSmallShip); break;
                        case Faction.Spain:
                            DrawShipStates(args, ship, SpainSmallShipActive, SpainSmallShipTarget, SpainSmallShip); break;
                        case Faction.France:
                            DrawShipStates(args, ship, FranceSmallShipActive, FranceSmallShipTarget, FranceSmallShip); break;
                    }
                }
            }
        }

        /// <summary>
        /// Draws the different states of the ship
        /// </summary>
        /// <param name="args"></param>
        /// <param name="ship"></param>
        /// <param name="shipActive">The version of the ship when clickable</param>
        /// <param name="shipTarget">The version of the ship when user press a specific ship</param>
        /// <param name="shipSmallShip">Standard version of the ship</param>
        private static void DrawShipStates(CanvasAnimatedDrawEventArgs args, Player.RowBoat ship, CanvasBitmap shipActive, CanvasBitmap shipTarget, CanvasBitmap shipSmallShip)
        {
            if (ship.targetable)
            {
                args.DrawingSession.DrawImage(Scaler.Fit(shipActive), Scaler.Cords(ship.Vector));
            }
            else if (ship.pressedByMouse)
            {
                args.DrawingSession.DrawImage(Scaler.Fit(shipTarget), Scaler.Cords(ship.Vector));
            }
            else
            {
                args.DrawingSession.DrawImage(Scaler.Fit(shipSmallShip), Scaler.Cords(ship.Vector));
            }
        }

        private static void DrawDices(CanvasAnimatedDrawEventArgs args)
        {
            args.DrawingSession.DrawImage(Scaler.Fit(britishDisplayDice), Scaler.Cords(new Vector2(362, 184)));
            args.DrawingSession.DrawImage(Scaler.Fit(dutchDisplayDice), Scaler.Cords(new Vector2(1552, 184)));
            args.DrawingSession.DrawImage(Scaler.Fit(spainDisplayDice), Scaler.Cords(new Vector2(1552, 885)));
            args.DrawingSession.DrawImage(Scaler.Fit(franceDisplayDice), Scaler.Cords(new Vector2(362, 885)));
        }

        /// <summary>
        /// Draws the 4 last tiles before finish
        /// </summary>
        /// <param name="args"></param>
        /// <param name="list"></param>
        private static void DrawGoalTiles(CanvasAnimatedDrawEventArgs args, List<GameTile> list)
        {
            foreach (var tile in list)
            {
                tile.ScaledVector = Scaler.TileCords(tile.GameTileVector);
                switch (tile.FactionType)
                {
                    case Faction.Britain:
                        args.DrawingSession.DrawImage(Scaler.Fit(BritainTile), Scaler.Cords(tile.GameTileVector));
                        break;
                    case Faction.Dutch:
                        args.DrawingSession.DrawImage(Scaler.Fit(DutchTile), Scaler.Cords(tile.GameTileVector));
                        break;
                    case Faction.Spain:
                        args.DrawingSession.DrawImage(Scaler.Fit(SpainTile), Scaler.Cords(tile.GameTileVector));
                        break;
                    case Faction.France:
                        args.DrawingSession.DrawImage(Scaler.Fit(FranceTile), Scaler.Cords(tile.GameTileVector));
                        break;
                    case Faction.FactionNull:
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Draws all base tiles where each player Rowboat nest
        /// </summary>
        /// <param name="args"></param>
        /// <param name="list"></param>
        private static void DrawBaseTiles(CanvasAnimatedDrawEventArgs args, List<GameTile> list)
        {
            foreach (var tile in list)
            {
                tile.ScaledVector = Scaler.TileCords(tile.GameTileVector);
                switch (tile.FactionType)
                {
                    case Faction.Britain:
                        args.DrawingSession.DrawImage(Scaler.Fit(BritainTile), Scaler.Cords(tile.GameTileVector));
                        break;
                    case Faction.Dutch:
                        args.DrawingSession.DrawImage(Scaler.Fit(DutchTile), Scaler.Cords(tile.GameTileVector));
                        break;
                    case Faction.Spain:
                        args.DrawingSession.DrawImage(Scaler.Fit(SpainTile), Scaler.Cords(tile.GameTileVector));
                        break;
                    case Faction.France:
                        args.DrawingSession.DrawImage(Scaler.Fit(FranceTile), Scaler.Cords(tile.GameTileVector));
                        break;
                    case Faction.FactionNull:
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Draws all the game tiles to form the circle around the goal tiles
        /// </summary>
        /// <param name="args"></param>
        /// <param name="tile"></param>
        private static void DrawPlayboardOutsideCircle(CanvasAnimatedDrawEventArgs args, GameTile tile)
        {
            if (tile.FactionType != Faction.FactionNull && tile.TileType == Tile.StartTile)
            {
                switch (tile.FactionType)
                {
                    case Faction.Britain:
                        args.DrawingSession.DrawImage(Scaler.Fit(BritainTile), Scaler.Cords(tile.GameTileVector));
                        break;
                    case Faction.Spain:
                        args.DrawingSession.DrawImage(Scaler.Fit(SpainTile), Scaler.Cords(tile.GameTileVector));
                        break;
                    case Faction.Dutch:
                        args.DrawingSession.DrawImage(Scaler.Fit(DutchTile), Scaler.Cords(tile.GameTileVector));
                        break;
                    case Faction.France:
                        args.DrawingSession.DrawImage(Scaler.Fit(FranceTile), Scaler.Cords(tile.GameTileVector));
                        break;
                    default:
                        args.DrawingSession.DrawImage(Scaler.Fit(NeutralTile), Scaler.Cords(tile.GameTileVector));
                        break;
                }
            }
            else if (tile.FactionType != Faction.FactionNull && tile.TileType == Tile.NeutralTile)
            {
                switch (tile.FactionType)
                {
                    case Faction.Britain:
                        args.DrawingSession.DrawImage(Scaler.Fit(BritainGoalTile), Scaler.Cords(tile.GameTileVector));
                        break;
                    case Faction.Spain:
                        args.DrawingSession.DrawImage(Scaler.Fit(SpainGoalTile), Scaler.Cords(tile.GameTileVector));
                        break;
                    case Faction.Dutch:
                        args.DrawingSession.DrawImage(Scaler.Fit(DutchGoalTile), Scaler.Cords(tile.GameTileVector));
                        break;
                    case Faction.France:
                        args.DrawingSession.DrawImage(Scaler.Fit(FranceGoalTile), Scaler.Cords(tile.GameTileVector));
                        break;
                    default:
                        args.DrawingSession.DrawImage(Scaler.Fit(NeutralTile), Scaler.Cords(tile.GameTileVector));
                        break;
                }
            }
            else if (tile.TileType == Tile.NegativeTile || tile.TileType == Tile.PositiveTile || tile.TileType == Tile.RandomTile)
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

        /// <summary>
        /// Adds all tile objects in correct order to List<GameTiles> orderTiles
        /// </summary>
        private static void AddBoardTilesOrdered()
        {
            //Neutral Tile Britain side
            orderedTiles.Add(new GameTile(Tile.StartTile, Faction.Britain, new Vector2(340, 460)));
            orderedTiles.Add(new GameTile(Tile.NeutralTile, new Vector2(420, 420)));
            orderedTiles.Add(new GameTile(Tile.PositiveTile, new Vector2(500, 380)));
            orderedTiles.Add(new GameTile(Tile.NeutralTile, new Vector2(540, 300)));
            orderedTiles.Add(new GameTile(Tile.RandomTile, new Vector2(560, 220)));
            orderedTiles.Add(new GameTile(Tile.NeutralTile, new Vector2(590, 140)));
            orderedTiles.Add(new GameTile(Tile.NegativeTile, new Vector2(640, 80)));
            orderedTiles.Add(new GameTile(Tile.NeutralTile, new Vector2(720, 70)));
            orderedTiles.Add(new GameTile(Tile.NeutralTile, new Vector2(800, 70)));
            orderedTiles.Add(new GameTile(Tile.NeutralTile, new Vector2(880, 70)));

            //Dutch last tile
            orderedTiles.Add(new GameTile(Tile.NeutralTile, Faction.Dutch, new Vector2(960, 70)));

            //Neutral Tile Dutch side
            orderedTiles.Add(new GameTile(Tile.StartTile, Faction.Dutch, new Vector2(1040, 70)));
            orderedTiles.Add(new GameTile(Tile.NeutralTile, new Vector2(1120, 70)));
            orderedTiles.Add(new GameTile(Tile.PositiveTile, new Vector2(1200, 70)));
            orderedTiles.Add(new GameTile(Tile.NeutralTile, new Vector2(1280, 80)));
            orderedTiles.Add(new GameTile(Tile.RandomTile, new Vector2(1330, 140)));
            orderedTiles.Add(new GameTile(Tile.NeutralTile, new Vector2(1360, 220)));
            orderedTiles.Add(new GameTile(Tile.NegativeTile, new Vector2(1380, 300)));
            orderedTiles.Add(new GameTile(Tile.NeutralTile, new Vector2(1420, 380)));
            orderedTiles.Add(new GameTile(Tile.NeutralTile, new Vector2(1500, 420)));
            orderedTiles.Add(new GameTile(Tile.NeutralTile, new Vector2(1580, 460)));

            //Spain last tile
            orderedTiles.Add(new GameTile(Tile.NeutralTile, Faction.Spain, new Vector2(1580, 540)));

            //Neutral Tile Spain side
            orderedTiles.Add(new GameTile(Tile.StartTile, Faction.Spain, new Vector2(1580, 620)));
            orderedTiles.Add(new GameTile(Tile.NeutralTile, new Vector2(1500, 660)));
            orderedTiles.Add(new GameTile(Tile.PositiveTile, new Vector2(1420, 700)));
            orderedTiles.Add(new GameTile(Tile.NeutralTile, new Vector2(1380, 780)));
            orderedTiles.Add(new GameTile(Tile.RandomTile, new Vector2(1360, 860)));
            orderedTiles.Add(new GameTile(Tile.NeutralTile, new Vector2(1330, 940)));
            orderedTiles.Add(new GameTile(Tile.NegativeTile, new Vector2(1280, 1000)));
            orderedTiles.Add(new GameTile(Tile.NeutralTile, new Vector2(1200, 1010)));
            orderedTiles.Add(new GameTile(Tile.NeutralTile, new Vector2(1120, 1010)));
            orderedTiles.Add(new GameTile(Tile.NeutralTile, new Vector2(1040, 1010)));

            //France last tile
            orderedTiles.Add(new GameTile(Tile.NeutralTile, Faction.France, new Vector2(960, 1010)));

            //Neutral Tile France side
            orderedTiles.Add(new GameTile(Tile.StartTile, Faction.France, new Vector2(880, 1010)));
            orderedTiles.Add(new GameTile(Tile.NeutralTile, new Vector2(800, 1010)));
            orderedTiles.Add(new GameTile(Tile.PositiveTile, new Vector2(720, 1010)));
            orderedTiles.Add(new GameTile(Tile.NeutralTile, new Vector2(640, 1000)));
            orderedTiles.Add(new GameTile(Tile.RandomTile, new Vector2(590, 940)));
            orderedTiles.Add(new GameTile(Tile.NeutralTile, new Vector2(560, 860)));
            orderedTiles.Add(new GameTile(Tile.NegativeTile, new Vector2(540, 780)));
            orderedTiles.Add(new GameTile(Tile.NeutralTile, new Vector2(500, 700)));
            orderedTiles.Add(new GameTile(Tile.NeutralTile, new Vector2(420, 660)));
            orderedTiles.Add(new GameTile(Tile.NeutralTile, new Vector2(340, 620)));

            //Britain last tile 
            orderedTiles.Add(new GameTile(Tile.NeutralTile, Faction.Britain, new Vector2(340, 540)));
        }
        /// <summary>
        /// Adds all goal tiles to respective faction goal tile list
        /// </summary>
        private static void BritainGoalTilesToList()
        {
            britainGoalTiles.Add(new GameTile(Tile.GoalTile, Faction.Britain, new Vector2(420, 540)));
            britainGoalTiles.Add(new GameTile(Tile.GoalTile, Faction.Britain, new Vector2(500, 540)));
            britainGoalTiles.Add(new GameTile(Tile.GoalTile, Faction.Britain, new Vector2(580, 540)));
            britainGoalTiles.Add(new GameTile(Tile.GoalTile, Faction.Britain, new Vector2(660, 540)));
            allGoalTiles.Add(britainGoalTiles);
        }
        private static void DutchGoalTilesToList()
        {
            dutchGoalTiles.Add(new GameTile(Tile.GoalTile, Faction.Dutch, new Vector2(960, 150)));
            dutchGoalTiles.Add(new GameTile(Tile.GoalTile, Faction.Dutch, new Vector2(960, 230)));
            dutchGoalTiles.Add(new GameTile(Tile.GoalTile, Faction.Dutch, new Vector2(960, 310)));
            dutchGoalTiles.Add(new GameTile(Tile.GoalTile, Faction.Dutch, new Vector2(960, 390)));
            allGoalTiles.Add(dutchGoalTiles);
        }
        private static void SpainGoalTilesToList()
        {
            spainGoalTiles.Add(new GameTile(Tile.GoalTile, Faction.Spain, new Vector2(1500, 540)));
            spainGoalTiles.Add(new GameTile(Tile.GoalTile, Faction.Spain, new Vector2(1420, 540)));
            spainGoalTiles.Add(new GameTile(Tile.GoalTile, Faction.Spain, new Vector2(1340, 540)));
            spainGoalTiles.Add(new GameTile(Tile.GoalTile, Faction.Spain, new Vector2(1260, 540)));
            allGoalTiles.Add(spainGoalTiles);
        }
        private static void FranceGoalTilesToList()
        {
            franceGoalTiles.Add(new GameTile(Tile.GoalTile, Faction.France, new Vector2(960, 930)));
            franceGoalTiles.Add(new GameTile(Tile.GoalTile, Faction.France, new Vector2(960, 850)));
            franceGoalTiles.Add(new GameTile(Tile.GoalTile, Faction.France, new Vector2(960, 770)));
            franceGoalTiles.Add(new GameTile(Tile.GoalTile, Faction.France, new Vector2(960, 690)));
            allGoalTiles.Add(franceGoalTiles);
        }
        /// <summary>
        /// Adds all base tiles to respective faction base tile list
        /// </summary>
        private static void BritainBaseTilesToList()
        {
            britainBaseTiles.Add(new GameTile(Tile.BaseTile, Faction.Britain, new Vector2(190, 150)));
            britainBaseTiles.Add(new GameTile(Tile.BaseTile, Faction.Britain, new Vector2(190, 230)));
            britainBaseTiles.Add(new GameTile(Tile.BaseTile, Faction.Britain, new Vector2(270, 150)));
            britainBaseTiles.Add(new GameTile(Tile.BaseTile, Faction.Britain, new Vector2(270, 230)));
            allBaseTiles.Add(britainBaseTiles);
        }
        private static void DutchBaseTilesToList()
        {
            dutchBaseTiles.Add(new GameTile(Tile.BaseTile, Faction.Dutch, new Vector2(1730, 150)));
            dutchBaseTiles.Add(new GameTile(Tile.BaseTile, Faction.Dutch, new Vector2(1730, 230)));
            dutchBaseTiles.Add(new GameTile(Tile.BaseTile, Faction.Dutch, new Vector2(1650, 150)));
            dutchBaseTiles.Add(new GameTile(Tile.BaseTile, Faction.Dutch, new Vector2(1650, 230)));
            allBaseTiles.Add(dutchBaseTiles);
        }
        private static void SpainBaseTilesToList()
        {
            spainBaseTiles.Add(new GameTile(Tile.BaseTile, Faction.Spain, new Vector2(1730, 930)));
            spainBaseTiles.Add(new GameTile(Tile.BaseTile, Faction.Spain, new Vector2(1730, 850)));
            spainBaseTiles.Add(new GameTile(Tile.BaseTile, Faction.Spain, new Vector2(1650, 930)));
            spainBaseTiles.Add(new GameTile(Tile.BaseTile, Faction.Spain, new Vector2(1650, 850)));
            allBaseTiles.Add(spainBaseTiles);
        }
        private static void FranceBaseTilesToList()
        {
            franceBaseTiles.Add(new GameTile(Tile.BaseTile, Faction.France, new Vector2(190, 930)));
            franceBaseTiles.Add(new GameTile(Tile.BaseTile, Faction.France, new Vector2(190, 850)));
            franceBaseTiles.Add(new GameTile(Tile.BaseTile, Faction.France, new Vector2(270, 930)));
            franceBaseTiles.Add(new GameTile(Tile.BaseTile, Faction.France, new Vector2(270, 850)));
            allBaseTiles.Add(franceBaseTiles);
        }
        public static List<GameTile> GetOrderedTiles()
        {
            return orderedTiles;
        }
        public static GameTile GetOrderTile(int index)
        {
            try
            {
                if (index > 43)
                {
                    index = 0;
                }
                return orderedTiles[index];
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw e;
            }
        }
        public static GameTile GetGoalTileByShipFactor(Player.RowBoat ship)
        {
            switch (ship.Faction)
            {
                case Faction.Britain: return britainGoalTiles[ship.CurrentTile];
                case Faction.Dutch: return dutchGoalTiles[ship.CurrentTile];
                case Faction.Spain: return spainGoalTiles[ship.CurrentTile];
                case Faction.France: return franceGoalTiles[ship.CurrentTile];
            }
            return null;
        }
        public static GameTile GetGoalTileByShipFactor(Player.RowBoat ship, int index)
        {
            switch (ship.Faction)
            {
                case Faction.Britain: return britainGoalTiles[index];
                case Faction.Dutch: return dutchGoalTiles[index];
                case Faction.Spain: return spainGoalTiles[index];
                case Faction.France: return franceGoalTiles[index];
            }
            return null;
        }
        public static GameTile GetBritainGoalTile(int index)
        {
            return britainGoalTiles[index];
        }
        public static List<GameTile> GetBritainGoalTiles()
        {
            return britainGoalTiles;
        }
        public static GameTile GetDutchGoalTile(int index)
        {
            return dutchGoalTiles[index];
        }
        public static List<GameTile> GetDutchGoalTiles()
        {
            return dutchGoalTiles;
        }
        public static GameTile GetSpainGoalTile(int index)
        {
            return spainGoalTiles[index];
        }
        public static List<GameTile> GetSpainGoalTiles()
        {
            return spainGoalTiles;
        }
        public static GameTile GetFranceGoalTile(int index)
        {
            return franceGoalTiles[index];
        }
        public static List<GameTile> GetFranceGoalTiles()
        {
            return franceGoalTiles;
        }
    }
}
