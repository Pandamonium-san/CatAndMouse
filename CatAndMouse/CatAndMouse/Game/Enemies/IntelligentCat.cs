using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatAndMouse
{
    class IntelligentCat : GeniusCat
    {
        public IntelligentCat(Texture2D texture, Vector2 pos)
            : base(texture, pos)
        {
            spriteOriginX = 128;
            spriteOriginY = 0;
            canReverseDirection = false;
        }
        protected override void SelectTarget()
        {
            targetX = playerMice[0].gridX + (int)playerMice[0].dir.X * 4;
            targetY = playerMice[0].gridY + (int)playerMice[0].dir.Y * 4;
        }

        protected override void FindPath(Tile[,] tiles)
        {
            playerMice[0].UpdateGridPos();
            SelectTarget();
            if (ObjectManager.IndexIsNotOutOfRange(targetX, targetY, tiles))
                path = PathFinder.FindPathForwardOnly(tiles[gridX, gridY], tiles[targetX, targetY], tiles, this);
            FollowPath(path);
        }

    }
}
