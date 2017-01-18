using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CatAndMouse
{
    public class GameObject
    {

        public Vector2 pos, vectorOrigin;
        public Rectangle hitbox;
        protected Texture2D texture;
        protected Rectangle spriteRec;  //Rectangle from which to take the sprite. Default uses entire texture.
        protected Color color;
        protected float scale = 1f;
        protected int offset; //lowers size of hitbox by pixels on all sides

        public int gridX, gridY;

        public GameObject(Texture2D texture, Vector2 pos)
        {
            this.texture = texture;
            this.pos = pos;
            this.spriteRec = new Rectangle(0, 0, texture.Width, texture.Height);
            this.vectorOrigin = new Vector2(spriteRec.Width / 2, spriteRec.Height / 2);
            this.color = Color.White;

            this.hitbox = new Rectangle(
                (int)pos.X + offset - spriteRec.Width / 2,
                (int)pos.Y + offset - spriteRec.Height / 2,
                spriteRec.Width - offset * 2,
                spriteRec.Height - offset * 2);

            UpdateGridPos();
        }

        public void UpdateGridPos()
        {
            gridX = (int)(pos.X) / 32;
            gridY = (int)(pos.Y) / 32;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, pos, spriteRec, color, 0f, vectorOrigin, scale, SpriteEffects.None, 0f);

        }
    }
}
