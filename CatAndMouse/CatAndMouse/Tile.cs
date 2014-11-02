using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CatAndMouse
{
    class Tile
    {
        public static int tileSize = 32;

        public Texture2D texture;
        public Vector2 pos;
        public Rectangle spriteRec;
        public Rectangle hitbox;

        //Editor variables
        public enum TileType { mouse, dumbcat, smartcat, intelligentcat, geniuscat, cheese, wall, floor }
        public Nullable<TileType> type = TileType.wall;


        //Pathfinder variables
        public bool visited;
        public enum PDir { up, down, left, right }
        public Nullable<PDir> pDir = null;      //parent tile direction

        public int gridPosX, gridPosY;
        public bool isForkTile;
        public bool isSolid;

        public Tile(Texture2D texture, Vector2 pos)
        {
            this.texture = texture;
            this.pos = pos;
            UpdateGridPos();
        }

        public virtual void Update(){}

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, pos, spriteRec, Color.White);

            //DRAW PATHFINDER PATH
            //if (pDir == PDir.down)
            //    spriteBatch.DrawString(Game1.hudFont, "v", pos + toleft + new Vector2(12, 16), Color.Red);

            //else if (pDir == PDir.right)
            //    spriteBatch.DrawString(Game1.hudFont, ">", pos + toleft + new Vector2(20, 8), Color.Red);

            //else if (pDir == PDir.left)
            //    spriteBatch.DrawString(Game1.hudFont, "<", pos + toleft + new Vector2(4, 8), Color.Red);

            //else if (pDir == PDir.up)
            //    spriteBatch.DrawString(Game1.hudFont, "^", pos + toleft + new Vector2(12, 0), Color.Red);
        }

        public void UpdateGridPos()
        {
            gridPosX = (int)(ObjectManager.mapOffset + pos.X) / 32;
            gridPosY = (int)(ObjectManager.mapOffset + pos.Y) / 32;
        }

        public virtual void CheckSetForkTile(List<String> mapData, Tile[,] tiles)
        {
            UpdateGridPos();
            int connectingTiles = 0;
            if (pos.X > 0 && pos.X < (mapData[0].Length-4) * 32 && pos.Y > 0 && pos.Y < (mapData.Count-4) * 32 && this is FloorTile)
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
    }
}
