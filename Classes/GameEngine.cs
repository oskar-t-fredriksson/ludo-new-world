using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoNewWorld.Classes
{
    class GameEngine
    {
        public static bool GoalAchieved = false;
        public static int playerturn = 1;

        public List<Player> playerList = new List<Player>();     
        public List<Faction> factionList = new List<Faction>();

        public void CreatePlayers(Faction faction)
        {
            factionList.Add(Faction.Britain);
            factionList.Add(Faction.Dutch);
            factionList.Add(Faction.Spain);
            factionList.Add(Faction.France);
            Player player1 = new Player(faction, true);
        }              

        public void StartGame(Faction faction)
        {
            CreatePlayers(faction);
            while (GoalAchieved!=false)
            {
                
            }
        }
    }
}
