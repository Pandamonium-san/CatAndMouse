using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CatAndMouse
{
    class FloorTile : Tile
    {
        public FloorTile(Texture2D texture, Vector2 pos): base(texture, pos)
        {
            this.spriteRec = new Rectangle(32, 0, 32, 32);
            this.isSolid = false;
            this.hitbox = new Rectangle((int)pos.X, (int)pos.Y, spriteRec.Width, spriteRec.Height);
        }
    }
}
