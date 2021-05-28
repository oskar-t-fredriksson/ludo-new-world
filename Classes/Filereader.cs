using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using LudoNewWorld.Classes;

namespace LudoNewWorld
{
    public class Filereader
    {
        public Tile tile;
        public FactionTile factionTile;
        public Vector2 vector;

        public List<Filereader> LoadJson(string filename)
        {
            List<Filereader> items = new List<Filereader>();
            try
            {
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return items;
        }
        
    }
}
