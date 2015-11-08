using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatAndMouse
{
    class GeniusCat : IntelligentCat
    {

        public GeniusCat(Texture2D texture, Vector2 pos)
            : base(texture, pos)
        {
            spriteOriginX = 128;
            spriteOriginY = 128;
            intelligent = false;
        }

        public override void FindPath(Tile[,] tiles)
        {
            playerMice[0].UpdateGridPos();
            targetX = playerMice[0].gridPosX;
            targetY = playerMice[0].gridPosY;
            base.FindPath(tiles);
        }
    }
}
