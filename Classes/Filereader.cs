using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using LudoNewWorld.Classes;
using Newtonsoft.Json;

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
                string DirectoryPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\..\")) + @"Assets\Data\";
                string fullPath = DirectoryPath + filename;
                using (StreamReader r = new StreamReader(fullPath))
                {
                    string json = r.ReadToEnd();
                    items = JsonConvert.DeserializeObject<List<Filereader>>(json);
                    return items;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return items;
        }
        
    }
}
