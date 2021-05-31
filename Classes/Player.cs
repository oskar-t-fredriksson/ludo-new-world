using System;
using System.Collections.Generic;
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

        public List<RowBoat> rowBoats = new List<RowBoat>();

        public Player(Faction playerFaction, bool isHuman)
        {
            this.playerFaction = playerFaction;
            this.IsHuman = isHuman;
            
            switch (playerFaction)
            {
                case Faction.Britain:
                    RowBoat rowBoat1 = new RowBoat(1, new Vector2(190 - 10, 150 - 25), Faction.Britain);
                    RowBoat rowBoat2 = new RowBoat(2, new Vector2(190 - 10, 230 - 25), Faction.Britain);
                    RowBoat rowBoat3 = new RowBoat(3, new Vector2(270 - 10, 150 - 25), Faction.Britain);
                    RowBoat rowBoat4 = new RowBoat(4, new Vector2(270 - 10, 230 - 25), Faction.Britain);

                    GraphicHandler.rowBoatList.Add(rowBoat1);
                    rowBoats.Add(rowBoat1);
                    GraphicHandler.rowBoatList.Add(rowBoat2);
                    rowBoats.Add(rowBoat2);
                    GraphicHandler.rowBoatList.Add(rowBoat3);
                    rowBoats.Add(rowBoat3);
                    GraphicHandler.rowBoatList.Add(rowBoat3);
                    rowBoats.Add(rowBoat3);
                    GraphicHandler.rowBoatList.Add(rowBoat4);
                    rowBoats.Add(rowBoat4);
                    break;
                case Faction.Spain:
                    break;
                case Faction.Dutch:
                    break;
                case Faction.France:
                    break;
                case Faction.FactionNull:
                    break;
                default:
                    break;
            }
        }
        public class RowBoat
        {
            public Vector2 Vector { get; set; }
            public Faction Faction { get; }
            public int Id { get; }
            public bool active = false;
            public bool targetable = false;

            public RowBoat(int id, Vector2 vector, Faction faction)
            {
                this.Vector = vector;
                this.Faction = faction;
                this.Id = id;
            }
        }
        public void MoveRowBoat()
        {

        }
    }
}
