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
            this.vectorOrigin = new Vector2(spriteRec.Width / 2, spriteRec.Height / 2);
            this.scale = 0.5f;
        }
    }
}
