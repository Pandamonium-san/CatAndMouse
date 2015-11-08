using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CatAndMouse
{
    public class Tile : GameObject
    {
        public static int tileSize = 32;

        //Editor variables
        public enum TileType { mouse, dumbcat, smartcat, intelligentcat, geniuscat, cheese, wall, floor, teleporter }
        public Nullable<TileType> type = TileType.wall;

        //Pathfinder variables
        public List<Tile> neighbors;
        public bool open, closed;
        public float G, H;    //G is cumulative cost, H is heuristic, F is the sum of G and H
        public float F
        {
            get { return G + H; }
        }
        public Tile parent;
        public bool isForkTile, isSolid;

        public int teleporterId;

        public Tile(Texture2D texture, Vector2 pos)
            : base(texture, pos)
        {
            this.vectorOrigin = Vector2.Zero;
            this.hitbox = new Rectangle((int)pos.X, (int)pos.Y, 32, 32);
        }

        public void FindNeighbors(Tile[,] tiles)
        {
            neighbors = new List<Tile>();

            if (gridX + 1 < tiles.GetLength(0))
                neighbors.Add(tiles[gridX + 1, gridY]);
            if (gridX - 1 >= 0)
                neighbors.Add(tiles[gridX - 1, gridY]);
            if (gridY + 1 < tiles.GetLength(1))
                neighbors.Add(tiles[gridX, gridY + 1]);
            if (gridY - 1 >= 0)
                neighbors.Add(tiles[gridX, gridY - 1]);

            neighbors.RemoveAll(n => n.isSolid);
            if (neighbors.Count >= 3)
                isForkTile = true;
        }

        public void ResetValues()
        {
            closed = false;
            open = false;
            G = 0;
            H = 0;
            parent = null;
        }
    }
}
