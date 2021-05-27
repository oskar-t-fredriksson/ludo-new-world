using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using LudoNewWorld.Classes;

namespace LudoNewWorld
{
    public enum Tile
    {
        NeutralTile,
        GoalTile,
        BaseTile,
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
        FranceTile,
        FactionNull
    }
    public class GameTile
    {
        public CanvasBitmap TileImage { get; set; }
        public Vector2 GameTileVector { get; set; }
        public FactionTile FactionType { get; set; }
        public Tile TileType { get; set; }
        public bool IsPlayerOnTile = false;

        public GameTile(Tile tileType, FactionTile factionType, Vector2 gameTileVector)
        {
            TileType = tileType;
            FactionType = factionType;
            GameTileVector = gameTileVector;
        }
        public GameTile(Tile tileType, Vector2 gameTileVector)
        {
            TileType = tileType;
            GameTileVector = gameTileVector;
            FactionType = FactionTile.FactionNull;
        }
    }
}
