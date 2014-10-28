using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CatAndMouse
{
    class Cat:GameObject
    {
        Random rand = new Random();
        public Cat(Texture2D texture, Vector2 pos):base(texture, pos)
        {
            hitbox = new Rectangle((int)pos.X, (int)pos.Y, spriteRec.Width, spriteRec.Height);
            offset = 10;
            scale = 1f;
            speed = 2;
            maxMoveTime = 16;
            frameWidth = 32;
            frameHeight = 32;
            frameInterval = 10;
            spriteRec = new Rectangle(0, 0, frameWidth, frameHeight);
            vectorOrigin = new Vector2(spriteRec.Width / 2, spriteRec.Height / 2);
        }

        public override void Update()
        {
            if (!moving)
            {
                switch ((GameObject.Direction)rand.Next(0, 5))
                {
                    case Direction.Down:
                        if (down)
                            Move(Direction.Down);
                        break;
                    case Direction.Left:
                        if (left)
                            Move(Direction.Left);
                        break;
                    case Direction.Right:
                        if (right)
                            Move(Direction.Right);
                        break;
                    case Direction.Up:
                        if (up)
                            Move(Direction.Up);
                        break;
                }
            }
            frameTime++;
            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteRec = new Rectangle(frameWidth * frame, frameHeight * (int)direction, frameWidth, frameHeight);
            
            base.Draw(spriteBatch);
            //spriteBatch.Draw(texture, hitbox, Color.Red);
        }
    }
}
