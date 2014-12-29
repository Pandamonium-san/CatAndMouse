using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CatAndMouse
{
    class GameObject
    {
        protected Texture2D texture;
        public Vector2 pos, vectorOrigin, dir;
        protected Rectangle spriteRec;
        public Rectangle hitbox;
        protected Color color;
        protected float scale = 1f;
        protected int offset;

        protected int frameWidth, frameHeight;
        protected int frame = 0, maxFrames = 4;
        protected double frameInterval = 500, frameTime = 0;

        protected float speed = 4;
        protected double moveTime = 0;
        protected double maxMoveTime = 8;

        public bool moving = false, teleported = false;
        public bool upPossible, downPossible, leftPossible, rightPossible;
        public int gridPosX, gridPosY;

        public Direction spriteDirection;

        public enum Direction
        {
            Down,
            Left,
            Right,
            Up,
        }

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
        }

        public virtual void Update(GameTime gameTime, Tile[,] tiles)
        {
            this.hitbox = new Rectangle(
                (int)pos.X + offset - spriteRec.Width/2,
                (int)pos.Y + offset - spriteRec.Height/2,
                spriteRec.Width - offset * 2,
                spriteRec.Height - offset * 2);
            
            //Animate sprite
            if (frameTime > frameInterval)
            {
                frame++;
                frameTime = 0;
                if (frame == maxFrames)
                    frame = 0;
            }

            if (moving)
            {
                frameTime += gameTime.ElapsedGameTime.TotalMilliseconds;
                moveTime++;
                pos += speed * dir;
                if (moveTime >= maxMoveTime)
                {
                    StopMoving(tiles);
                }
            }
        }

        public virtual void StopMoving(Tile[,] tiles)
        {
            moving = false;
            teleported = false;
            CheckValidDirections(tiles);
            moveTime = 0;
        }

        public void UpdateGridPos()
        {
            gridPosX = (int)(ObjectManager.mapOffset + pos.X) / 32;
            gridPosY = (int)(ObjectManager.mapOffset + pos.Y) / 32;
        }

        public virtual void CheckValidDirections(Tile[,] tiles)
        {
            if (!(this is Cheese))
            {
                pos.X = (pos.X - pos.X % 32)+16;    //Snap to grid to make sure objects stay aligned
                pos.Y = (pos.Y - pos.Y % 32)+16;
                UpdateGridPos();
                rightPossible = false;
                leftPossible = false;
                downPossible = false;
                upPossible = false;
                if (gridPosX + 1 <= tiles.GetLength(0) && !tiles[gridPosX + 1, gridPosY].isSolid)
                    rightPossible = true;
                if (gridPosX - 1 >= 0 && !tiles[gridPosX - 1, gridPosY].isSolid)
                    leftPossible = true;
                if (gridPosY + 1 <= tiles.GetLength(1) && !tiles[gridPosX, gridPosY + 1].isSolid)
                    downPossible = true;
                if (gridPosY - 1 >= 0 && !tiles[gridPosX, gridPosY - 1].isSolid)
                    upPossible = true;
            }
        }

        public void Move(Direction direction)
        {
            spriteDirection = direction;
            switch(direction)
            {
                case Direction.Down:
                    dir = new Vector2(0, 1);
                    if (downPossible)
                        moving = true;
                    break;
                case Direction.Left:
                    dir = new Vector2(-1, 0);
                    if (leftPossible)
                        moving = true;
                    break;
                case Direction.Right:
                    dir = new Vector2(1, 0);
                    if (rightPossible)
                        moving = true;
                    break;
                case Direction.Up:
                    dir = new Vector2(0, -1);
                    if (upPossible)
                        moving = true;
                    break;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, pos, spriteRec, color, 0f, vectorOrigin, scale, SpriteEffects.None, 0f);
            //spriteBatch.Draw(texture, hitbox, Color.Red);
        }

    }
}
