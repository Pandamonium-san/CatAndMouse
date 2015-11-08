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
        TeleporterTile other;

        public TeleporterTile(Texture2D texture, Vector2 pos):base(texture,pos)
        {
            spriteRec = new Rectangle(64, 0, 32, 32);
            isSolid = false;
        }

        public void Teleport(Actor a)
        {
            a.pos = other.pos;
        }

        public void SetExit(TeleporterTile other)
        {
            this.other = other;
        }

        public void AddExitToNeighbors()
        {
            neighbors.Add(other);
            if (neighbors.Count >= 3)
                isForkTile = true;
        }
    }
}
