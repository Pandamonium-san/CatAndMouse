using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CatAndMouse
{
    class Mouse:GameObject
    {

        public Mouse(Texture2D texture, Vector2 pos) : base(texture, pos)
        {
            hitbox = new Rectangle((int)pos.X, (int)pos.Y, spriteRec.Width, spriteRec.Height);
            offset = 10;

            speed = 4; //tileSize must be a multiple of speed
            maxMoveTime = ObjectManager.tileSize/speed;

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
                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    direction = Direction.Down;
                    if(down)
                        Move(Direction.Down);
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    direction = Direction.Left;
                    if (left)
                    Move(Direction.Left);
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    direction = Direction.Right;
                    if (right)
                    Move(Direction.Right);
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    direction = Direction.Up;
                    if (up)
                    Move(Direction.Up);
                }
            }

            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteRec = new Rectangle(1 + frameWidth * frame, 1 + frameHeight * (int)direction, frameWidth, frameHeight);

            base.Draw(spriteBatch);
            //spriteBatch.Draw(texture, hitbox, Color.Red);
        }

    }
}
