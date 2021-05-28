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
        
        public Faction PlayerFaction;
        public bool IsHuman { get; set; }
        public bool IsMyTurn = false;
        public int RowBoatOut = 4;

        public List<RowBoat> rowBoats = new List<RowBoat>();

        public Player(Faction playerFaction, bool isHuman)
        {
            PlayerFaction = playerFaction;
            IsHuman = isHuman;
            
            
            switch (PlayerFaction)
            {
                case Faction.Britain:
                    RowBoat rowBoat1 = new RowBoat(new Vector2(190 - 10, 150 - 25), Faction.Britain);
                    RowBoat rowBoat2 = new RowBoat(new Vector2(190 - 10, 230 - 25), Faction.Britain);
                    RowBoat rowBoat3 = new RowBoat(new Vector2(270 - 10, 150 - 25), Faction.Britain);
                    RowBoat rowBoat4 = new RowBoat(new Vector2(270 - 10, 230 - 25), Faction.Britain);
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

        public void MoveRowBoat()
        {

        }
        public class RowBoat
        {
            public Vector2 SmallShipVector { get; set; }
            public Faction FactionShip { get; set; }
            //public Faction RowBoatFaction;
            public bool Active = false;

            public RowBoat(Vector2 smallShipVector, Faction factionShip)
            {
                SmallShipVector = smallShipVector;
                FactionShip = factionShip;
            }
        }

    }
}
