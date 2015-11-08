using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CatAndMouse
{
    class Actor : GameObject
    {
        //Animation variables
        protected int frameWidth, frameHeight;
        protected int frame = 0, maxFrames = 4;
        protected double frameInterval = 500, frameTime = 0;

        //Movement variables
        public Vector2 dir;
        protected float speed = 4;
        protected double moveTime = 0;
        protected double maxMoveTime = 8;
        public bool moving = false, teleported = false;

        //Pathing variables
        public bool upPossible, downPossible, leftPossible, rightPossible;
        public Direction spriteDirection;
        public enum Direction
        {
            Down,
            Left,
            Right,
            Up,
        }

        public Actor(Texture2D texture, Vector2 pos)
            : base(texture, pos)
        {
            this.vectorOrigin = new Vector2(spriteRec.Width / 2, spriteRec.Height / 2);
        }

        public virtual void Update(GameTime gameTime, Tile[,] tiles)
        {
            UpdateHitbox();
            AnimateSprite();
            Moving(gameTime, tiles);
        }

        void UpdateHitbox()
        {
            this.hitbox = new Rectangle(
                (int)pos.X + offset - spriteRec.Width / 2,
                (int)pos.Y + offset - spriteRec.Height / 2,
                spriteRec.Width - offset * 2,
                spriteRec.Height - offset * 2);
        }

        protected virtual void Moving(GameTime gameTime, Tile[,] tiles)
        {
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

        protected virtual void AnimateSprite()
        {
            if (frameTime > frameInterval)
            {
                frame++;
                frameTime = 0;
                if (frame == maxFrames)
                    frame = 0;
            }
        }

        public virtual void StopMoving(Tile[,] tiles)
        {
            moving = false;
            teleported = false;
            pos.X = (pos.X - pos.X % 32) + 16;    //Snap to grid to make sure objects stay aligned
            pos.Y = (pos.Y - pos.Y % 32) + 16;
            CheckValidDirections(tiles);
            moveTime = 0;
        }

        public virtual void CheckValidDirections(Tile[,] tiles)
        {
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

        public void Move(Direction direction)
        {
            spriteDirection = direction;
            switch (direction)
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

    }
}
