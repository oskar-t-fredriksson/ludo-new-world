using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;

namespace LudoNewWorld.Classes
{
    public enum Tile
    {
        NeutralTile,
        GoalTile,
        StartTile,
        NegativeTile,
        PositiveTile,
        RandomTile    
    }
    public enum FactionTile
    {
        BritainTile,
        SpainTile,
        DutchTile,
        FranceTile
    }
    class GameTiles
    {
        public static CanvasBitmap NeutralTile, BritainTile, SpainTile, DutchTile, FranceTile, NegativeTile, PositiveTile, RandomTile;
        public static bool IsPlayerOnTile = false;

        public GameTiles(Tile tileType, FactionTile factionType)
        {

        }
    }
}
