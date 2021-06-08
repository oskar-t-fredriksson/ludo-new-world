﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace LudoNewWorld.Classes
{
    class Player
    {
        
        public Faction playerFaction;
        public int ID { get; }
        public bool IsHuman { get; }
        public bool isMyTurn = false;
        public int rowBoatOut = 4;

        public static List<Player> playerList = new List<Player>();
        public static List<Player.RowBoat> targetableRowBoats = new List<Player.RowBoat>();
        public List<RowBoat> rowBoats = new List<RowBoat>();

        public Player(int ID, Faction playerFaction, bool isHuman)
        {
            this.ID = ID;
            this.playerFaction = playerFaction;
            this.IsHuman = isHuman;
            
            switch (playerFaction)
            {
                case Faction.Britain:
                    RowBoat britainRowBoat1 = new RowBoat(1, new Vector2(190 - 10, 150 - 25), Faction.Britain);
                    RowBoat britainRowBoat2 = new RowBoat(2, new Vector2(190 - 10, 230 - 25), Faction.Britain);
                    RowBoat britainRowBoat3 = new RowBoat(3, new Vector2(270 - 10, 150 - 25), Faction.Britain);
                    RowBoat britainRowBoat4 = new RowBoat(4, new Vector2(270 - 10, 230 - 25), Faction.Britain);

                    GraphicHandler.rowBoatList.Add(britainRowBoat1);
                    rowBoats.Add(britainRowBoat1);
                    GraphicHandler.rowBoatList.Add(britainRowBoat2);
                    rowBoats.Add(britainRowBoat2);
                    GraphicHandler.rowBoatList.Add(britainRowBoat3);
                    rowBoats.Add(britainRowBoat3);
                    GraphicHandler.rowBoatList.Add(britainRowBoat4);
                    rowBoats.Add(britainRowBoat4);
                    break;
                case Faction.Dutch:
                    RowBoat dutchRowBoat1 = new RowBoat(1, new Vector2(1730 - 10, 150 - 25), Faction.Dutch);
                    RowBoat dutchRowBoat2 = new RowBoat(2, new Vector2(1730 - 10, 230 - 25), Faction.Dutch);
                    RowBoat dutchRowBoat3 = new RowBoat(3, new Vector2(1650 - 10, 150 - 25), Faction.Dutch);
                    RowBoat dutchRowBoat4 = new RowBoat(4, new Vector2(1650 - 10, 230 - 25), Faction.Dutch);

                    GraphicHandler.rowBoatList.Add(dutchRowBoat1);
                    rowBoats.Add(dutchRowBoat1);
                    GraphicHandler.rowBoatList.Add(dutchRowBoat2);
                    rowBoats.Add(dutchRowBoat2);
                    GraphicHandler.rowBoatList.Add(dutchRowBoat3);
                    rowBoats.Add(dutchRowBoat3);
                    GraphicHandler.rowBoatList.Add(dutchRowBoat4);
                    rowBoats.Add(dutchRowBoat4);
                    break;
                case Faction.Spain:
                    RowBoat spainRowBoat1 = new RowBoat(1, new Vector2(1730 - 10, 930 - 25), Faction.Spain);
                    RowBoat spainRowBoat2 = new RowBoat(2, new Vector2(1730 - 10, 850 - 25), Faction.Spain);
                    RowBoat spainRowBoat3 = new RowBoat(3, new Vector2(1650 - 10, 930 - 25), Faction.Spain);
                    RowBoat spainRowBoat4 = new RowBoat(4, new Vector2(1650 - 10, 850 - 25), Faction.Spain);

                    GraphicHandler.rowBoatList.Add(spainRowBoat1);
                    rowBoats.Add(spainRowBoat1);
                    GraphicHandler.rowBoatList.Add(spainRowBoat2);
                    rowBoats.Add(spainRowBoat2);
                    GraphicHandler.rowBoatList.Add(spainRowBoat3);
                    rowBoats.Add(spainRowBoat3);
                    GraphicHandler.rowBoatList.Add(spainRowBoat4);
                    rowBoats.Add(spainRowBoat4);
                    break;
                case Faction.France:
                    RowBoat franceRowBoat1 = new RowBoat(1, new Vector2(190 - 10, 930 - 25), Faction.France);
                    RowBoat franceRowBoat2 = new RowBoat(2, new Vector2(190 - 10, 850 - 25), Faction.France);
                    RowBoat franceRowBoat3 = new RowBoat(3, new Vector2(270 - 10, 930 - 25), Faction.France);
                    RowBoat franceRowBoat4 = new RowBoat(4, new Vector2(270 - 10, 850 - 25), Faction.France);

                    GraphicHandler.rowBoatList.Add(franceRowBoat1);
                    rowBoats.Add(franceRowBoat1);
                    GraphicHandler.rowBoatList.Add(franceRowBoat2);
                    rowBoats.Add(franceRowBoat2);
                    GraphicHandler.rowBoatList.Add(franceRowBoat3);
                    rowBoats.Add(franceRowBoat3);
                    GraphicHandler.rowBoatList.Add(franceRowBoat4);
                    rowBoats.Add(franceRowBoat4);
                    break;
                default:
                    break;
            }
        }
        public class RowBoat
        {
            public Vector2 Vector { get; set; }
            public Vector2 scaledVector { get; set; }
            public int CurrentTile = -1;
            public Faction Faction { get; }
            public int Id { get; }
            public bool active = false;
            public bool targetable = false;
            public bool pressedByMouse = false;

            public RowBoat(int id, Vector2 vector, Faction faction)
            {
                this.Vector = vector;
                this.Faction = faction;
                this.Id = id;
                switch (faction)
                {   
                    case Faction.Britain: this.CurrentTile = -1; break;
                    case Faction.Dutch: this.CurrentTile = 10; break;
                    case Faction.Spain: this.CurrentTile = 21; break;
                    case Faction.France: this.CurrentTile = 32; break;
                }
            }
        }
        /// <summary>
        /// Move lastPressedBoat and updates lastPressBoat.CurrentTile value
        /// </summary>
        public void MoveRowBoat()
        {
            var ship = GameEngine.LastPressedBoat;
            var tile = GameEngine.LastPressedGameTile;
            var diceRoll = GameEngine.LastDiceRoll;

            if (!ship.active) ship.active = true;

            if(GameEngine.LastPressedGameTile == null || GameEngine.LastPressedGameTile.GameTileVector != tile.GameTileVector)
            {
                // If a player clicks on the wrong area when picking the tile to move (not a highlighted one) its should set all
                // the targetable ships of the faction back to targetable = true again so the round doesnt end
                foreach (var rowBoat in targetableRowBoats)
                {
                    rowBoat.targetable = true;
                }
            }
            else
            {
                float shipX = tile.GameTileVector.X - 10;
                float shipY = tile.GameTileVector.Y - 25;
                ship.Vector = new Vector2(shipX, shipY);
                if (ship.CurrentTile + diceRoll > 43)
                {
                    ship.CurrentTile = ship.CurrentTile - 43 + diceRoll - 1;
                }
                else
                {
                    ship.CurrentTile += diceRoll;
                }
                
                GraphicHandler.GetTile(ship.CurrentTile).IsPlayerOnTile = false;
                tile.IsPlayerOnTile = true;
                GameEngine.moveConfirmed = false;
            }
        }

        /// <summary>
        /// Return true or false if a ship can move or not on the board 
        /// <para>Based the latest dice roll and what other ships are already placed in the way</para>
        /// <para>A ship of the same faction in the way will return false and break</para>
        /// </summary>
        /// <param name="ship">Ship object</param>
        /// <param name="dicenr"></param>
        /// <returns>True or False</returns>
        public bool CheckIfMovable(Player.RowBoat ship, int dicenr)
        {
            if (GameEngine.GetGameActive())
            {
                var shipTileI = ship.CurrentTile;
                var forLoopShipTileLengthI = ship.CurrentTile;
                Debug.WriteLine("=================================");
                Debug.WriteLine($"Loop for ship {ship.Id} started: ");
                for (int i = shipTile + 1; i < shipTile + dicenr + 1; i++)
                {   
                    Debug.Write("Tile: " + i);
                    if (i <= 43)
                    {
                        if (GraphicHandler.GetTile(i).IsPlayerOnTile)
                        {
                            foreach (var targetShip in GraphicHandler.rowBoatList)
                            {                                
                                if (ship.Id != targetShip.Id && i == targetShip.CurrentTile && ship.Faction == targetShip.Faction)
                                {
                                    Debug.WriteLine(" Found own ship in the way, cant move!");
                                    return false;
                                }
                                else if (i == targetShip.CurrentTile && i + 1 == dicenr)
                                {
                                    Debug.WriteLine($" Found {targetShip.Faction} ship on last tile!, Should destroy!");

                                }
                                else if (i == targetShip.CurrentTile)
                                {
                                    Debug.WriteLine($" Found {targetShip.Faction} ship!");
                                }                                                                
                            }
                        }
                        else
                        {
                            Debug.WriteLine($" Found zero ship on tile!");
                        }
                    }
                    else
                    {
                        i = -1;
                        forLoopShipTileLengthI = shipTileI - 43 - 1;
                    }  
                }                
            }            
            ship.targetable = true;
            if(targetableRowBoats.Count < 4) targetableRowBoats.Add(ship);
            return true;
        }       
    }
}
