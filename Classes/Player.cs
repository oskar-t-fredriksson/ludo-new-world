using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;

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
        public static IDictionary<Player.RowBoat, Vector2> rowboatVector = new Dictionary<Player.RowBoat, Vector2>();
        public List<RowBoat> rowBoats = new List<RowBoat>();

        /// <summary>
        /// Constructor for a <see cref="Player"/> object
        /// Represent a player of the game, will generate four <see cref="Player.RowBoat"/> objects per <see cref="Player"/>
        /// </summary>
        /// <param name="ID">ID between 1-4</param>
        /// <param name="playerFaction">What <see cref="Faction"/> is the player</param>
        /// <param name="isHuman">Human or AI</param>
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
            foreach (var boat in rowBoats)
            {
                rowboatVector.Add(boat, boat.Vector);                
            }
        }

        /// <summary>
        /// <see cref="Player.RowBoat"/> class
        /// </summary>
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
            public bool isOnGoalTile = false;


            /// <summary>
            /// Constructor for a <see cref="Player.RowBoat"/> object
            /// </summary>
            /// <param name="id">ID between 1-4</param>
            /// <param name="vector"><see cref="Vector2"/> X/Y coordinates</param>
            /// <param name="faction">What <see cref="Faction"/> is the boat part off</param>
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
                // We move the ship by changing the X and Y coordinates to the new tile
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
                GraphicHandler.GetOrderTile(ship.CurrentTile).IsPlayerOnTile = false;
                tile.IsPlayerOnTile = true;
                DestroyRowBoat(ship, tile);
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
                int stepsTaken = 1;

                // Detect if ship is currently standing on a goal tile
                if (ship.isOnGoalTile && ship.active && ship.CurrentTile <= 4 && ship.Faction == GraphicHandler.GetGoalTileByShipFactor(ship).FactionType &&
                    GraphicHandler.GetGoalTileByShipFactor(ship).TileType == Tile.GoalTile)
                {
                    if (GameEngine.LastDiceRoll + ship.CurrentTile > 4) return false;
                    else if (GameEngine.LastDiceRoll + ship.CurrentTile == 4)
                    {
                        return true;
                    }
                    else
                    {
                        for (int i = ship.CurrentTile + 1; i < 4; i++)
                        {
                            if(GraphicHandler.GetGoalTileByShipFactor(ship,i).IsPlayerOnTile)
                            {
                                return false;
                            }
                        }
                        return true;
                    }
                }
                for (int i = shipTileI + 1; i < forLoopShipTileLengthI + dicenr + 1; i++)
                {   
                    if(!ship.active && GameEngine.LastDiceRoll != 1 && GameEngine.LastDiceRoll != 6)
                    {
                        return false;
                    }
                    if (i <= 43)
                    {
                        // Detect if the method are passing a "faction arrow" that indicate that the boat should turn into the goal tiles
                        if(GraphicHandler.GetOrderTile(i).FactionType == ship.Faction && GraphicHandler.GetOrderTile(i).TileType == Tile.NeutralTile)
                        {
                            // How many steps there are left into the goal tiles
                            int RemaingStepsToTake = GameEngine.LastDiceRoll - stepsTaken;
                            if (RemaingStepsToTake > 3) return false;
                            else
                            {
                                switch (ship.Faction)
                                {
                                    case Faction.Britain:
                                        var britishList = GraphicHandler.GetBritainGoalTiles();
                                        for (int j = 0; j < RemaingStepsToTake; j++)
                                        {
                                            if (britishList[j].IsPlayerOnTile && ship.Faction == britishList[j].FactionType)
                                            {
                                                return false;
                                            }
                                        }
                                        break;
                                    case Faction.Dutch:
                                        var dutchList = GraphicHandler.GetDutchGoalTiles();
                                        for (int j = 0; j < RemaingStepsToTake; j++)
                                        {
                                            if (dutchList[j].IsPlayerOnTile && ship.Faction == dutchList[j].FactionType)
                                            {
                                                return false;
                                            }
                                        }
                                        break;
                                    case Faction.Spain:
                                        var spainList = GraphicHandler.GetSpainGoalTiles();
                                        for (int j = 0; j < RemaingStepsToTake; j++)
                                        {
                                            if (spainList[j].IsPlayerOnTile && ship.Faction == spainList[j].FactionType)
                                            {
                                                return false;
                                            }
                                        }
                                        break;
                                    case Faction.France:
                                        var franceList = GraphicHandler.GetFranceGoalTiles();
                                        for (int j = 0; j < RemaingStepsToTake; j++)
                                        {
                                            if (franceList[j].IsPlayerOnTile && ship.Faction == franceList[j].FactionType)
                                            {
                                                return false;
                                            }
                                        }
                                        break;
                                }
                            }
                        }

                        // We check if there is a ship of the same nation in the way based on the last dice roll,
                        // if so, we return false 
                        if (GraphicHandler.GetOrderTile(i).IsPlayerOnTile)
                        {
                            foreach (var targetShip in GraphicHandler.rowBoatList)
                            {                                
                                if (ship.Id != targetShip.Id && i == targetShip.CurrentTile && ship.Faction == targetShip.Faction)
                                {
                                    return false;
                                }                                                              
                            }
                        }
                    }
                    else if(ship.Faction != Faction.Britain)
                    {
                        i = -1;
                        forLoopShipTileLengthI = shipTileI - 43 - 1;
                    }
                    stepsTaken++;
                }                
            } 
            // If a ship isnt getting stuck by any of our false checks, it ends up here
            // We set the targetable bool value on the ship to true so the player can press it
            // Also fills a list containting all these targetable ships.
            ship.targetable = true;
            if(targetableRowBoats.Count < 4) targetableRowBoats.Add(ship);
            return true;
        }


        /// <summary>
        /// Resets vector of Rowboat on selected tile if player choose to move his own Rowboat to that tile
        /// </summary>
        /// <param name="ship"></param>
        /// <param name="tile"></param>
        public static void DestroyRowBoat(Player.RowBoat ship, GameTile tile)
        {
            foreach (var targetShip in GraphicHandler.rowBoatList)
            {
                foreach (var item in rowboatVector)
                {
                    if (item.Key.Id == targetShip.Id && item.Key.Faction == targetShip.Faction && targetShip.CurrentTile == GraphicHandler.GetOrderedTiles().IndexOf(tile))
                    {
                        if (targetShip.Faction != ship.Faction && targetShip.Faction == Faction.Britain 
                            || ship.Faction == Faction.Britain && GraphicHandler.GetOrderTile(ship.CurrentTile).TileType == Tile.NegativeTile)
                        {
                            targetShip.CurrentTile = -1;
                            targetShip.Vector = new Vector2(item.Value.X, item.Value.Y);
                            targetShip.active = false;
                        }
                        else if (targetShip.Faction != ship.Faction && targetShip.Faction == Faction.Dutch
                            || ship.Faction == Faction.Dutch && GraphicHandler.GetOrderTile(ship.CurrentTile).TileType == Tile.NegativeTile)
                        {
                            targetShip.CurrentTile = 10;
                            targetShip.Vector = new Vector2(item.Value.X, item.Value.Y);
                            targetShip.active = false;
                        }
                        else if (targetShip.Faction != ship.Faction && targetShip.Faction == Faction.Spain
                            || ship.Faction == Faction.Spain && GraphicHandler.GetOrderTile(ship.CurrentTile).TileType == Tile.NegativeTile)
                        {
                            targetShip.CurrentTile = 21;
                            targetShip.Vector = new Vector2(item.Value.X, item.Value.Y);
                            targetShip.active = false;
                        }
                        else if (targetShip.Faction != ship.Faction && targetShip.Faction == Faction.France
                            || ship.Faction == Faction.France && GraphicHandler.GetOrderTile(ship.CurrentTile).TileType == Tile.NegativeTile)
                        {
                            targetShip.CurrentTile = 32;
                            targetShip.Vector = new Vector2(item.Value.X, item.Value.Y);
                            targetShip.active = false;
                        }
                    }
                }
            }           
        }


        /// <summary>
        /// Makes the player or AI able to roll the dice another time
        /// </summary>
        public static void PositiveTileEffect()
        {
            if(GameEngine.GetActivePlayer().IsHuman)
            {
                GameEngine.diceRolled = false;
                MainPage.showDice = true;
            }
            else
            {
                GameEngine.diceRolled = false;
            }
        }


        /// <summary>
        /// Upon walking on random tile this negative effect might happen, reset vector to start position and set IsPlayerOnTile to false
        /// </summary>
        /// <param name="ship"></param>
        public static void RandomNegativeTile(Player.RowBoat ship)
        {
            foreach (var item in rowboatVector)
            {
                if (item.Key.Id == ship.Id && item.Key.Faction == ship.Faction && ship.CurrentTile >= 0)
                {
                    if (ship.Faction == Faction.Britain && GraphicHandler.GetOrderTile(ship.CurrentTile).TileType == Tile.RandomTile)
                    {
                        GraphicHandler.GetOrderTile(ship.CurrentTile).IsPlayerOnTile = false;
                        ship.CurrentTile = -1;
                        ship.Vector = new Vector2(item.Value.X, item.Value.Y);
                        ship.active = false;
                    }
                    else if (ship.Faction == Faction.Dutch && GraphicHandler.GetOrderTile(ship.CurrentTile).TileType == Tile.RandomTile)
                    {
                        GraphicHandler.GetOrderTile(ship.CurrentTile).IsPlayerOnTile = false;
                        ship.CurrentTile = 10;
                        ship.Vector = new Vector2(item.Value.X, item.Value.Y);
                        ship.active = false;
                    }
                    else if (ship.Faction == Faction.Spain && GraphicHandler.GetOrderTile(ship.CurrentTile).TileType == Tile.RandomTile)
                    {
                        GraphicHandler.GetOrderTile(ship.CurrentTile).IsPlayerOnTile = false;
                        ship.CurrentTile = 21;
                        ship.Vector = new Vector2(item.Value.X, item.Value.Y);
                        ship.active = false;
                    }
                    else if (ship.Faction == Faction.France && GraphicHandler.GetOrderTile(ship.CurrentTile).TileType == Tile.RandomTile)
                    {
                        GraphicHandler.GetOrderTile(ship.CurrentTile).IsPlayerOnTile = false;
                        ship.CurrentTile = 32;
                        ship.Vector = new Vector2(item.Value.X, item.Value.Y);
                        ship.active = false;
                    }
                }
            }
        }
    }
}
