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
        public Texture2D texture;
        public Vector2 pos;
        public Rectangle spriteRec;
        public Rectangle hitbox;

        public int tileID;
        public bool isSolid;
        public enum TileType { mouse, cat, cheese, wall, floor }
        public Nullable<TileType> type = TileType.wall;

        public Tile(Texture2D texture, Vector2 pos)
        {
            this.texture = texture;
            this.pos = pos;
        }

        public virtual void Update()
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, pos, spriteRec, Color.White);

        }

    }
}
