using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CatAndMouse
{
    class Tile : GameObject
    {
        public static int tileSize = 32;

        //Editor variables
        public enum TileType { mouse, dumbcat, smartcat, intelligentcat, geniuscat, cheese, wall, floor, teleporter }
        public Nullable<TileType> type = TileType.wall;

        //Pathfinder variables
        public bool visited;
        public enum PDir { up, down, left, right }
        public Nullable<PDir> pDir = null;      //parent tile direction
        public bool isForkTile, isSolid;

        public int teleporterId;

        public Tile(Texture2D texture, Vector2 pos)
            : base(texture, pos)
        {
            this.vectorOrigin = Vector2.Zero;
            this.hitbox = new Rectangle((int)pos.X, (int)pos.Y, 32, 32);
        }

        public virtual void CheckSetForkTile(List<String> mapData, Tile[,] tiles)
        {
            UpdateGridPos();
            int connectingTiles = 0;
            if (pos.X > 0 && pos.X < (mapData[0].Length - 4) * 32 && pos.Y > 0 && pos.Y < (mapData.Count - 4) * 32 && this is FloorTile)
            {
                if (tiles[gridPosX + 1, gridPosY] is FloorTile)
                    connectingTiles++;
                if (tiles[gridPosX - 1, gridPosY] is FloorTile)
                    connectingTiles++;
                if (tiles[gridPosX, gridPosY + 1] is FloorTile)
                    connectingTiles++;
                if (tiles[gridPosX, gridPosY - 1] is FloorTile)
                    connectingTiles++;
            }
            if (connectingTiles >= 3)
                isForkTile = true;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            //DRAW PATHFINDER PATH
            //if (pDir == PDir.down)
            //    spriteBatch.DrawString(Game1.hudFont, "v", pos + new Vector2(12, 16), Color.Red);

            //else if (pDir == PDir.right)
            //    spriteBatch.DrawString(Game1.hudFont, ">", pos + new Vector2(20, 8), Color.Red);

            //else if (pDir == PDir.left)
            //    spriteBatch.DrawString(Game1.hudFont, "<", pos + new Vector2(4, 8), Color.Red);

            //else if (pDir == PDir.up)
            //    spriteBatch.DrawString(Game1.hudFont, "^", pos + new Vector2(12, 0), Color.Red);
        }
    }
}
