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
        public Player.RowBoat boat;
        public GameTile tile;
        public Player p1, p2, p3, p4;
        public List<Faction> factionList = new List<Faction>();

        private static readonly Random _random = new Random();


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
        public void NextRound()
        {
            if(gameActive)
            {
                if(diceRolled)
                {
                    foreach (var boat in p1.rowBoats)
                    {
                        Debug.WriteLine(p1.CheckIfMovable(boat, lastDiceRoll));
                        Debug.WriteLine("RowBoat tile: " + boat.CurrentTile);
                    }
                    diceRolled = false;
                }
                if (lastPressedBoat == null)
                {
                    GraphicHandler.highlighter.GameTileVector = new Vector2(2000, 2000);
                }
                if (lastPressedBoat != null)
                {
                    var tileIndex = lastPressedBoat.CurrentTile + lastDiceRoll;
                    Debug.WriteLine("lastPressedBoat tile: " + lastPressedBoat.CurrentTile);
                    Vector2 highlightoffset = new Vector2(GraphicHandler.orderedTiles[tileIndex].GameTileVector.X - 12, GraphicHandler.orderedTiles[tileIndex].GameTileVector.Y - 12);
                    GraphicHandler.highlighter.GameTileVector = highlightoffset;                    
                }
                if(lastPressedBoat != null && lastPressedGameTile != null)
                {
                    p1.MoveRowBoat();
                    GraphicHandler.highlighter.GameTileVector = new Vector2(2000, 2000);
                }
            }
        }
        public Player.RowBoat CheckForShipsOnMousePressed(Vector2 clickCords)
        {
            if(gameActive)
            {
                if(lastPressedBoat != null)
                {
                    lastPressedBoat.pressedByMouse = false;
                    lastPressedBoat.targetable = true;
                }
                foreach (var ship in p1.rowBoats)
                {
                    if(ship.targetable)
                    {
                        if (clickCords.X >= ship.scaledVector.X - 30 && clickCords.X <= ship.scaledVector.X + 30
                        && clickCords.Y >= ship.scaledVector.Y - 30 && clickCords.Y <= ship.scaledVector.Y + 30)
                        {
                            ship.targetable = false;
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
                            return ship;
                        }
                    }
                }
            }
            return null;
        }
        public GameTile CheckForTileOnMousePressed(Vector2 clickCords)
        {
            if (gameActive)
            {
                foreach (var tile in GraphicHandler.orderedTiles)
                {
                    if(tile.TileType != Tile.BaseTile)
                    {
                        if (clickCords.X >= tile.ScaledVector.X - 30 && clickCords.X <= tile.ScaledVector.X + 30
                        && clickCords.Y >= tile.ScaledVector.Y - 30 && clickCords.Y <= tile.ScaledVector.Y + 30)
                        {
                            Debug.WriteLine("Found tile " + tile.TileType);
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
