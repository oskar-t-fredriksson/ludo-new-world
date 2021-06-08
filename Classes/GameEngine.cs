using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace LudoNewWorld.Classes
{
    class GameEngine
    {
        public static Player.RowBoat LastPressedBoat { get; set; }
        public static GameTile LastPressedGameTile { get; set; }

        private Player ActivePlayer { get; set; }
        public static int LastDiceRoll { get; set; }
        public static int Tick { get; set; }
        public static int moveAITick { get; set; }
        public static int PlayerTurn { get; set; }

        private static bool gameActive = false;



        public static bool diceRolled = false;
        public static bool playerCanMove = false;
        public static bool playerRoundCompleted = false;
        public static bool moveConfirmed = false;
        public Player.RowBoat boat;
        public GameTile tile;
        public Player p1, p2, p3, p4;

        public static List<Faction> factionList = new List<Faction>();

        private static readonly Random _random = new Random();

        /// <summary>
        /// Creates four Player objects and places them into
        /// <see cref="Player.playerList"/>
        /// </summary>
        /// <param name="faction"></param>
        public void CreatePlayers(Faction faction)
        {
            factionList.Add(Faction.Britain);
            factionList.Add(Faction.Dutch);
            factionList.Add(Faction.Spain);
            factionList.Add(Faction.France);
            factionList.Remove(faction);

            Player.playerList.Add(p1 = new Player(1,faction, true));
            Player.playerList.Add(p2 = new Player(2,factionList[0], false));
            Player.playerList.Add(p3 = new Player(3,factionList[1], false));
            Player.playerList.Add(p4 = new Player(4,factionList[2], false));            
        }

        /// <summary>
        /// <para>Creates all player and rowboat objects needed to play the game</para>
        /// <para>Takes in what <see cref="Faction"/> the player picked in menu</para>
        /// </summary>
        /// <param name="faction">What faction are the player picking in the start menu</param>
        public void StartGame(Faction faction)
        {
            CreatePlayers(faction);
            switch (faction)
            {
                case Faction.Britain:
                    PlayerTurn = 1; break;
                case Faction.Dutch:
                    PlayerTurn = 2; break;
                case Faction.Spain:
                    PlayerTurn = 3; break;
                case Faction.France:
                    PlayerTurn = 4; break;
                default: break;
            }
            gameActive = true;
            NextRound();
        }

        /// <summary>
        /// Start of a new round of the game
        /// <para><see cref="gameActive"/> needs to be set True</para>
        /// </summary>
        public void NextRound()
        {
            switch (PlayerTurn)
            {
                case 1: ActivePlayer = p1; break;
                case 2: ActivePlayer = p2; break;
                case 3: ActivePlayer = p3; break;
                case 4: ActivePlayer = p4; break;
            }
            if(gameActive && !ActivePlayer.IsHuman)
            {
                if(!diceRolled) LastDiceRoll = GraphicHandler.scrambleDice(ActivePlayer.ID);
                diceRolled = true;
                MoveAI();
            }
            else if(gameActive && ActivePlayer.IsHuman)
            {
                if (diceRolled)
                {
                    foreach (var boat in ActivePlayer.rowBoats)
                    {
                        Debug.WriteLine(ActivePlayer.CheckIfMovable(boat, LastDiceRoll));
                    }
                    diceRolled = false;
                }
                if (LastPressedBoat == null)
                {
                    GraphicHandler.highlighter.GameTileVector = new Vector2(2000, 2000);
                }
                if (LastPressedBoat != null)
                {
                    playerCanMove = true;
                    var tileIndex = LastPressedBoat.CurrentTile;
                    if (LastPressedBoat.CurrentTile + LastDiceRoll > 43)
                    {
                        tileIndex = tileIndex - 43 + LastDiceRoll - 1;
                    }
                    else
                    {
                        tileIndex += LastDiceRoll;
                    }
                    Vector2 highlightoffset = new Vector2(GraphicHandler.GetTile(tileIndex).GameTileVector.X - 12, GraphicHandler.GetTile(tileIndex).GameTileVector.Y - 12);
                    GraphicHandler.highlighter.GameTileVector = highlightoffset;
                }
                if (LastPressedBoat != null && LastPressedBoat.targetable)
                {
                    GraphicHandler.highlighter.GameTileVector = new Vector2(2000, 2000);
                    if (GraphicHandler.GetOrderedTiles().IndexOf(LastPressedGameTile) == LastPressedBoat.CurrentTile + LastDiceRoll)
                    {
                        Debug.WriteLine("Right tile was clicked, calling to move tile");
                        moveConfirmed = true;
                    }
                    else if ((LastPressedBoat.CurrentTile + LastDiceRoll - 1) >= 43)
                    {
                        Debug.WriteLine("Right tile was clicked, calling to move tile");
                        Debug.WriteLine("Test else if moveConfirmed");
                        moveConfirmed = true;
                    }
                    if (moveConfirmed)
                    {
                        ActivePlayer.MoveRowBoat();
                        GraphicHandler.highlighter.GameTileVector = new Vector2(2000, 2000);
                        playerRoundCompleted = true;
                        LastPressedBoat = null;
                        LastPressedGameTile = null;
                        playerCanMove = false;
                        foreach (var boat in Player.targetableRowBoats)
                        {
                            boat.targetable = false;
                        }
                        Debug.WriteLine("Moved ship to new tile, ending round");
                        PlayerTurn = 2;
                        Player.targetableRowBoats.Clear();
                    }
                    else
                    {
                        // In the case a user press the incorrect tile/anywhere else on the map that isnt the correct tile
                        LastPressedBoat = null;
                        Debug.WriteLine("Waiting for user to press the correct tile!");
                    }
                }
            }
        }

        private void MoveAI()
        {
            if(moveAITick >= 80)
            {
                foreach (var ship in ActivePlayer.rowBoats)
                {
                    ActivePlayer.CheckIfMovable(ship, LastDiceRoll);
                }
                foreach (var ship in Player.targetableRowBoats)
                {
                    ship.targetable = true;
                }
                // Generate a random number between 0 and the amount of ships in the player objects rowboat list
                int boatNumber = _random.Next(0, Player.targetableRowBoats.Count);
                Player.RowBoat boat = Player.targetableRowBoats[boatNumber];

                // Get the GameTile index of the ship we just generated a random index for
                int currentTileIndex = boat.CurrentTile;

                // Now we need to get the target game tile index & game tile object based on currentTileIndex + dice roll
                int targetTileIndex = GraphicHandler.GetOrderedTiles().IndexOf(GraphicHandler.GetTile(currentTileIndex + LastDiceRoll));
                GameTile targetTile = GraphicHandler.GetTile(targetTileIndex);

                // Move ship to target tile
                float shipX = targetTile.GameTileVector.X - 10;
                float shipY = targetTile.GameTileVector.Y - 25;
                boat.Vector = new Vector2(shipX, shipY);
                boat.active = true;
                boat.CurrentTile = targetTileIndex;
                targetTile.IsPlayerOnTile = true;
                Player.DestroyRowBoat(boat, targetTile);
                Debug.WriteLine($"Moved ship {boat.Faction} {boat.Id} to tile {boat.CurrentTile}");
                foreach (var ship in Player.targetableRowBoats)
                {
                    ship.targetable = false;
                }
                diceRolled = false;
                Player.targetableRowBoats.Clear();
                if(moveAITick >= 60) PlayerTurn = 1;
            }
        }

        /// <summary>
        /// Takes in the latest mouse click coordinates in the form of a Vector2 value
        /// <para>Check if any ship is on that position of the screen</para>
        /// </summary>
        /// <param name="clickCords">X/Y coordinates of the latest mouse click on the game screen</param>
        /// <returns>Player.RowBoat object or null</returns>
        public Player.RowBoat CheckForShipsOnMousePressed(Vector2 clickCords)
        {
            if (gameActive)
            {
                if (LastPressedBoat != null)
                {
                    LastPressedBoat.pressedByMouse = false;
                    LastPressedBoat.targetable = true;
                }
                foreach (var ship in p1.rowBoats)
                {
                    if (ship.targetable)
                    {
                        if (clickCords.X >= ship.scaledVector.X - 30 && clickCords.X <= ship.scaledVector.X + 30
                        && clickCords.Y >= ship.scaledVector.Y - 30 && clickCords.Y <= ship.scaledVector.Y + 30)
                        {
                            if (ship.pressedByMouse)
                            {
                                ship.targetable = true;
                                ship.pressedByMouse = false;
                            }
                            else
                            {
                                ship.targetable = false;
                                ship.pressedByMouse = true;
                                LastPressedBoat = ship;
                            }
                            Debug.WriteLine("User has pressed a targetable click, waiting for user to click a tile!");
                            return ship;
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Takes in the latest mouse click coordinates in the form of a Vector2 value
        /// <para>Check if any game tile is on that position of the screen</para>
        /// </summary>
        /// <param name="clickCords">X/Y coordinates of the latest mouse click on the game screen </param>
        /// <returns>GameTile object or null</returns>
        public GameTile CheckForTileOnMousePressed(Vector2 clickCords)
        {
            if (gameActive && playerCanMove)
            {
                foreach (var tile in GraphicHandler.GetOrderedTiles())
                {
                    if (tile.TileType != Tile.BaseTile)
                    {
                        if (clickCords.X >= tile.ScaledVector.X - 50 && clickCords.X <= tile.ScaledVector.X
                        && clickCords.Y >= tile.ScaledVector.Y - 50 && clickCords.Y <= tile.ScaledVector.Y)
                        {
                            LastPressedGameTile = tile;
                            return tile;
                        }
                    }
                }
            }
            return null;
        }

        public static bool GetGameActive()
        {
            return gameActive;
        }
        public static void CheckWin()
        {
            foreach (var player in Player.playerList)
            {
                if (player.rowBoats.Count == 0)
                {
                   
                    Debug.WriteLine($"{player} won the game uuuuuuuuuuuuuuu");
                }
                else
                {
                    Debug.WriteLine("no player has won the game yet");
                }
            }
        }
    }
}
