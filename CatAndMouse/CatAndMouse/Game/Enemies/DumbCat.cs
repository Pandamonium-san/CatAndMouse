using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CatAndMouse
{
    class DumbCat : Cat
    {
        public DumbCat(Texture2D texture, Vector2 pos):base(texture, pos)
        {
            spriteOriginX = 0;
            spriteOriginY = 128;
            this.speed = 2.3f;
            maxMoveTime = Tile.tileSize / speed;
        }

        public override void Update(GameTime gameTime, Tile[,] tiles)
        {
            if (!moving)
                MoveRandomly();
            base.Update(gameTime, tiles);
        }

        public override void StopMoving(Tile[,] tiles)
        {
            base.StopMoving(tiles);
            MoveRandomly();
        }

    }
}
