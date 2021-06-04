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
        public static int tick = 0;
        public static bool GoalAchieved = false;
        public static int playerturn = 0;
        public static bool gameActive = false;
        public static Player.RowBoat lastPressedBoat = null;
        public static GameTile lastPressedGameTile = null;
        public static int lastDiceRoll = 0;
        public static bool diceRolled = false;
        public static bool playerCanMove = false;
        public static bool playerRoundCompleted = false;
        public static bool moveConfirmed = false;
        public Player.RowBoat boat;
        public GameTile tile;
        public Player p1, p2, p3, p4;

        public List<Faction> factionList = new List<Faction>();

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

            Player.playerList.Add(p1 = new Player(faction, true));
            Player.playerList.Add(p2 = new Player(factionList[0], false));
            Player.playerList.Add(p3 = new Player(factionList[1], false));
            Player.playerList.Add(p4 = new Player(factionList[2], false));
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
                    playerturn = 1; break;
                case Faction.Dutch:
                    playerturn = 2; break;
                case Faction.Spain:
                    playerturn = 3; break;
                case Faction.France:
                    playerturn = 4; break;
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
            if (gameActive)
            {
                if (diceRolled)
                {
                    foreach (var boat in p1.rowBoats)
                    {
                        Debug.WriteLine(p1.CheckIfMovable(boat, lastDiceRoll));
                    }
                    diceRolled = false;
                }
                if (lastPressedBoat == null)
                {
                    GraphicHandler.highlighter.GameTileVector = new Vector2(2000, 2000);
                }
                if (lastPressedBoat != null)
                {
                    playerCanMove = true;
                    var tileIndex = lastPressedBoat.CurrentTile;
                    if (!lastPressedBoat.active)
                    {
                        switch (lastPressedBoat.Faction)
                        {
                            case Faction.Britain: break;
                            case Faction.Dutch: tileIndex = 10; break;
                            case Faction.Spain: tileIndex = 21; break;
                            case Faction.France: tileIndex = 32; break;
                            default: break;
                        }
                    }
                    tileIndex += lastDiceRoll;
                    Vector2 highlightoffset = new Vector2(GraphicHandler.orderedTiles[tileIndex].GameTileVector.X - 12, GraphicHandler.orderedTiles[tileIndex].GameTileVector.Y - 12);
                    GraphicHandler.highlighter.GameTileVector = highlightoffset;
                }
                if (lastPressedBoat != null && lastPressedBoat.targetable)
                {
                    GraphicHandler.highlighter.GameTileVector = new Vector2(2000, 2000);
                    if (GraphicHandler.orderedTiles.IndexOf(lastPressedGameTile) == lastPressedBoat.CurrentTile + lastDiceRoll)
                    {
                        Debug.WriteLine("Right tile was clicked, calling to move tile");
                        moveConfirmed = true;
                    }
                    if(moveConfirmed)
                    {
                        p1.MoveRowBoat();
                        GraphicHandler.highlighter.GameTileVector = new Vector2(2000, 2000);
                        playerRoundCompleted = true;
                        lastPressedBoat = null;
                        lastPressedGameTile = null;
                        playerCanMove = false;
                        foreach (var boat in Player.targetableRowBoats)
                        {
                            boat.targetable = false;
                        }
                        Debug.WriteLine("Moved ship to new tile, ending round");
                    }
                    else
                    {
                        Debug.WriteLine("Waiting for user to press the correct tile!");
                    }
                }
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
                if (lastPressedBoat != null)
                {
                    lastPressedBoat.pressedByMouse = false;
                    lastPressedBoat.targetable = true;
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
                                lastPressedBoat = ship;
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
                foreach (var tile in GraphicHandler.orderedTiles)
                {
                    if (tile.TileType != Tile.BaseTile)
                    {
                        if (clickCords.X >= tile.ScaledVector.X - 50 && clickCords.X <= tile.ScaledVector.X
                        && clickCords.Y >= tile.ScaledVector.Y - 50 && clickCords.Y <= tile.ScaledVector.Y)
                        {
                            lastPressedGameTile = tile;
                            return tile;
                        }
                    }
                }
            }
            return null;
        }
    }
}
