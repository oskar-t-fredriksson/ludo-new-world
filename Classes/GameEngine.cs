﻿using System;
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
        public static int playerturn = 1;
        public static bool gameActive = false;
        private static readonly Random _random = new Random();
        private static Player.RowBoat lastPressed = null;
        private static GameTile lastPressedGameTile = null;
        public Player.RowBoat boat;
        public GameTile tile;

        public Player p1, p2, p3, p4;

        public List<Faction> factionList = new List<Faction>();

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
            gameActive = true;
        }
        public void Update()
        {
            if(gameActive)
            {
                switch (playerturn)
                {
                    case 1: break;
                    case 2: break;
                    case 3: break;
                    case 4: break;
                    default: break;
                }

                //Debug.WriteLine("Scaled vector: " + string.Format("{0:0.00}", p1.rowBoats[0].scaledVector) + " Was: " + p1.rowBoats[0].Vector);
                //int n = _random.Next(0, 4);
                //int m = _random.Next(0, 4);
                //p1.rowBoats[n].targetable = true;
                //p3.rowBoats[n].targetable = true;
                //p1.rowBoats[m].targetable = false;
            }
        }
        public Player.RowBoat CheckForShipsOnMousePressed(Vector2 clickCords)
        {
            if(gameActive)
            {
                if(lastPressed != null)
                {
                    lastPressed.pressedByMouse = false;
                    lastPressed.targetable = true;
                }
                foreach (var ship in p1.rowBoats)
                {
                    ship.targetable = true;
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
                                lastPressed = ship;
                            }
                            Debug.WriteLine("Found ship" + ship.Faction + " " + ship.Id + " " + ship.scaledVector);
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
                foreach (var tile in GraphicHandler.gameTiles)
                {
                    if(tile.TileType != Tile.BaseTile)
                    {
                        if (clickCords.X >= tile.ScaledVector.X - 30 && clickCords.X <= tile.ScaledVector.X + 30
                        && clickCords.Y >= tile.ScaledVector.Y - 30 && clickCords.Y <= tile.ScaledVector.Y + 30)
                        {
                            Debug.WriteLine("Found tile " + tile.TileType);
                            return tile;
                        }
                    }
                }
            }
            return null;
        }
    }
}
