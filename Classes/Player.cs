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
        public void MoveRowBoat(int diceValue)
        {            
            foreach (var player in playerList)
            {
                if (player.isMyTurn == true)
                {
                    foreach (var rowBoat in GraphicHandler.rowBoatList)
                    {
                        float rowBoatX = GraphicHandler.orderedTiles[rowBoat.CurrentTile + diceValue - 1].GameTileVector.X - 10;
                        float rowBoatY = GraphicHandler.orderedTiles[rowBoat.CurrentTile + diceValue - 1].GameTileVector.Y - 25;
                        if (rowBoat.pressedByMouse == true)
                        {
                            rowBoat.Vector = new Vector2(rowBoatX, rowBoatY);
                            rowBoat.CurrentTile += diceValue;
                        }
                    }
                }
                player.isMyTurn = false;
            }
        }
        public bool CheckIfMovable(Player.RowBoat ship, Player targetPlayer, int dicenr)
        {
            if(GameEngine.gameActive)
            {
                GameEngine gameEngine = new GameEngine();
                targetPlayer.rowBoats[0].CurrentTile = 0;

                switch (ship.Faction)
                {
                    case Faction.Britain:
                        Debug.Write($"Loop for ship {ship.Id} started: ");
                        for (int i = 0; i < dicenr; i++)
                        {
                            ship.CurrentTile = GraphicHandler.orderedTiles.IndexOf(GraphicHandler.orderedTiles[i]);

                            if (GraphicHandler.orderedTiles[ship.CurrentTile].IsPlayerOnTile)
                            {
                                foreach (var targetShip in GraphicHandler.rowBoatList)
                                {
                                    if (ship.CurrentTile == targetShip.CurrentTile && ship.Faction == targetShip.Faction)
                                    {
                                        return false;
                                    }
                                    else if (ship.CurrentTile == targetShip.CurrentTile)
                                    {
                                        Debug.WriteLine("Found p2 ship");
                                    }
                                }
                            }
                            // Om det redan står en pjäs på rutan
                            // Är pjäsen motståndare eller inte
                        }
                        ship.CurrentTile = GraphicHandler.orderedTiles.IndexOf(GraphicHandler.orderedTiles[0]);
                        break;
                    case Faction.Dutch:
                        break;
                    case Faction.Spain:
                        break;
                    case Faction.France:
                        break;
                    case Faction.FactionNull:
                        break;
                    default:
                        break;
                }
            }
        return true;
        }
    }
}
