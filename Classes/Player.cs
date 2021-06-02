using System;
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
        public bool IsHuman { get; }
        public bool isMyTurn = false;
        public int rowBoatOut = 4;

        public static List<Player> playerList = new List<Player>();
        public List<RowBoat> rowBoats = new List<RowBoat>();

        public Player(Faction playerFaction, bool isHuman)
        {
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
            }
        }
        public void MoveRowBoat()
        {
            var ship = GameEngine.lastPressedBoat;
            var tile = GameEngine.lastPressedGameTile;
            var tileIndex = GraphicHandler.orderedTiles.IndexOf(tile);

            var diceRoll = GameEngine.lastDiceRoll;
            if (!ship.active)
            {

                if(ship.Faction == Faction.Britain)
                {
                    ship.CurrentTile = 0;
                    ship.active = true;
                }
                else if(ship.Faction == Faction.Dutch)
                {
                    ship.CurrentTile = 10;
                    ship.active = true;
                }
                else if (ship.Faction == Faction.Spain)
                {
                    ship.CurrentTile = 21;
                    ship.active = true;
                }
                else if (ship.Faction == Faction.France)
                {
                    ship.CurrentTile = 32;
                    ship.active = true;
                }
            }
            if (ship.CurrentTile < tileIndex && tileIndex - ship.CurrentTile == diceRoll)
            {
                float shipX = tile.GameTileVector.X - 10;
                float shipY = tile.GameTileVector.Y - 25;
                ship.Vector = new Vector2(shipX, shipY);
                ship.CurrentTile += diceRoll;
                GraphicHandler.orderedTiles[ship.CurrentTile].IsPlayerOnTile = false;
                tile.IsPlayerOnTile = true;                
            }
        }
        public bool CheckIfMovable(Player.RowBoat ship, int dicenr)
        {
            if (GameEngine.gameActive)
            {
                Debug.WriteLine("=================================");
                Debug.WriteLine($"Loop for ship {ship.Id} started: ");
                for (int i = ship.CurrentTile + 1; i < ship.CurrentTile + dicenr + 1; i++)
                {   
                    Debug.Write("Tile: " + i);

                    if (GraphicHandler.orderedTiles[i].IsPlayerOnTile)
                    {
                        foreach (var targetShip in GraphicHandler.rowBoatList)
                        {
                            if (ship.Id != targetShip.Id && i == targetShip.CurrentTile && ship.Faction == targetShip.Faction)
                            {
                                Debug.WriteLine("Found own ship, cant move!");
                                return false;
                            }
                            else if (i == targetShip.CurrentTile && i + 1 == dicenr)
                            {
                                Debug.WriteLine($" Found {targetShip.Faction} ship on last tile!");
                                
                            }
                            else if (i == targetShip.CurrentTile)
                            {
                                Debug.WriteLine($" Found {targetShip.Faction} ship!");
                            }
                        }
                    }
                    else
                    {
                        Debug.WriteLine($" Found zero ship!");
                    }                   
                }               
            }
            ship.targetable = true;
            return true;
        }       
    }
}
