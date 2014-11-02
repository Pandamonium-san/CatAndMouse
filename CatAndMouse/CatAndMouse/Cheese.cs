using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CatAndMouse
{
    class Cheese:GameObject
    {

        public Cheese(Texture2D texture, Vector2 pos):base(texture, pos)
        {
            scale = 0.7f;
        }

        public override void Update(GameTime gameTime, Tile[,] tiles)
        {
        }

    }
}
