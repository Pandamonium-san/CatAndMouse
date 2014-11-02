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
        public Vector2 startingPos;

        public bool invulnerable;
        public double invulnerableTime, invulnerableInterval = 2000;

        public Mouse(Texture2D texture, Vector2 pos) : base(texture, pos)
        {
            offset = 10;
            startingPos = pos;
            speed = 3;
            maxMoveTime = Tile.tileSize/speed;

            frameWidth = 32;
            frameHeight = 32;
            frameInterval = 120;
            spriteRec = new Rectangle(0, 0, frameWidth, frameHeight);
            vectorOrigin = new Vector2(spriteRec.Width / 2, spriteRec.Height / 2);
        }

        public override void Update(GameTime gameTime, Tile[,] tiles)
        {
            if (!moving)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                    Move(Direction.Down);
                else if (Keyboard.GetState().IsKeyDown(Keys.Left))
                    Move(Direction.Left);
                else if (Keyboard.GetState().IsKeyDown(Keys.Right))
                    Move(Direction.Right);
                else if (Keyboard.GetState().IsKeyDown(Keys.Up))
                    Move(Direction.Up);
            }

            if(invulnerable)
            {
                color = Color.CornflowerBlue * 0.8f;
                invulnerableTime += gameTime.ElapsedGameTime.TotalMilliseconds;
                if (invulnerableTime >= invulnerableInterval)
                {
                    invulnerable = false;
                    invulnerableTime = 0;
                    color = Color.White;
                }
            }

            base.Update(gameTime, tiles);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteRec = new Rectangle(1 + frameWidth * frame, 1 + frameHeight * (int)spriteDirection, frameWidth, frameHeight);
            base.Draw(spriteBatch);
        }
    }
}
