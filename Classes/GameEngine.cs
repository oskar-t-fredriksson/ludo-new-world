using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoNewWorld.Classes
{
    class GameEngine
    {
        public static int tick = 0;
        public static bool GoalAchieved = false;
        public static int playerturn = 1;
        private static bool gameActive = false;
        private static readonly Random _random = new Random();

        public Player p1, p2, p3, p4;

        public List<Player> playerList = new List<Player>();     
        public List<Faction> factionList = new List<Faction>();

        public void CreatePlayers(Faction faction)
        {
            factionList.Add(Faction.Britain);
            factionList.Add(Faction.Dutch);
            factionList.Add(Faction.Spain);
            factionList.Add(Faction.France);
            factionList.Remove(faction);
            p1 = new Player(faction, true);
            p2 = new Player(factionList[0], false);
            p3 = new Player(factionList[1], false);
            p4 = new Player(factionList[2], false);

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
                int n = _random.Next(0, 4);
                int m = _random.Next(0, 4);

                p1.rowBoats[n].targetable = true;
                p1.rowBoats[m].targetable = false;
            }
        }
    }
}
