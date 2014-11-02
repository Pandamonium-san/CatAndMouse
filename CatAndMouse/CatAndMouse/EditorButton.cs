using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatAndMouse
{
    class EditorButton:Button
    {
        public enum ButtonType { Mouse, DumbCat, SmartCat, Cheese, Wall, Floor }
        public ButtonType type = ButtonType.Wall;
        Texture2D texture;
        Nullable<Rectangle> spriteRec = new Rectangle(0,0,32,32);

        public EditorButton(Texture2D texture, Nullable<Rectangle> spriteRec, Vector2 pos, int type):base(pos, 32, 32)
        {
            this.texture = texture;
            this.spriteRec = spriteRec;
            this.type = (ButtonType)type;
        }

        public override void Update()
        {
            if (rec.Contains(KeyMouseReader.mousePos))
                alpha = 1f;
            else
                alpha = 0.5F;
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            base.Draw(spritebatch);
            spritebatch.Draw(texture, rec, spriteRec, Color.White * alpha);
        }
    }
}
