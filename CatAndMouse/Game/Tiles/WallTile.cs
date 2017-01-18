using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CatAndMouse
{
    class WallTile:Tile
    {

        public WallTile(Texture2D texture, Vector2 pos):base(texture, pos)
        {
            this.spriteRec = new Rectangle(0, 0, 32, 32);
            this.isSolid = true;
            this.hitbox = new Rectangle((int)pos.X, (int)pos.Y, spriteRec.Width, spriteRec.Height);
        }
    }
}
