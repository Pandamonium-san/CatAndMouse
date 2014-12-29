using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CatAndMouse
{
    class TeleporterTile:Tile
    {
        public TeleporterTile(Texture2D texture, Vector2 pos):base(texture,pos)
        {
            spriteRec = new Rectangle(64, 0, 32, 32);
            isSolid = false;
        }
    }
}
