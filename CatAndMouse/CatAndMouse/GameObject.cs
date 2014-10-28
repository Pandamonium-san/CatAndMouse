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
        public Vector2 pos, vectorOrigin;
        protected Rectangle spriteRec;
        public Rectangle hitbox;
        protected float scale = 1f;
        protected int offset;

        protected int frameWidth, frameHeight;
        protected int frameInterval = 10, frameTime = 0, frame = 0, maxFrames = 4;

        protected float speed = 4;
        protected float moveTime = 0;
        protected float maxMoveTime = 8;

        public bool moving = false;
        public bool up, down, left, right;
        protected int gridPosX, gridPosY;

        public Direction direction;

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
            this.hitbox = new Rectangle((int)pos.X + offset - spriteRec.Width / 2, (int)pos.Y + offset - spriteRec.Height / 2, spriteRec.Width - offset * 2, spriteRec.Height - offset * 2);
            this.vectorOrigin = new Vector2(spriteRec.Width / 2, spriteRec.Height / 2);
            CheckValidDirections();
        }

        public virtual void Update()
        {
            this.hitbox = new Rectangle((int)pos.X + offset - spriteRec.Width/2, (int)pos.Y + offset - spriteRec.Height/2, spriteRec.Width - offset * 2, spriteRec.Height - offset * 2);
            if (frameTime > frameInterval)
            {
                frame++;
                frameTime = 0;
                if (frame == maxFrames)
                    frame = 0;
            }

            if (moving)
            {
                frameTime++;
                moveTime++;
                switch (direction)
                {
                    case Direction.Down:
                        pos.Y += speed;
                        break;

                    case Direction.Left:
                        pos.X -= speed;
                        break;

                    case Direction.Right:
                        pos.X += speed;
                        break;

                    case Direction.Up:
                        pos.Y -= speed;
                        break;
                }
                if (moveTime >= maxMoveTime)
                {
                    moving = false;
                    CheckValidDirections();
                    moveTime = 0;
                }
            }
        }
        public void GetGridPos()
        {
            gridPosX = (int)(ObjectManager.mapOffset+pos.X) / 32;
            gridPosY = (int)(ObjectManager.mapOffset+pos.Y) / 32;
        }

        public virtual void CheckValidDirections()
        {
            GetGridPos();
            right = false;
            left = false;
            down = false;
            up = false;
            if (ObjectManager.tiles[gridPosX + 1, gridPosY] is FloorTile)
                right = true;
            if (ObjectManager.tiles[gridPosX - 1, gridPosY] is FloorTile)
                left = true;
            if (ObjectManager.tiles[gridPosX, gridPosY + 1] is FloorTile)
                down = true;
            if (ObjectManager.tiles[gridPosX, gridPosY - 1] is FloorTile)
                up = true;
        }

        public virtual void Move(Direction direction)
        {
            moving = true;
            this.direction = direction;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, pos, spriteRec, Color.White, 0f, vectorOrigin, scale, SpriteEffects.None, 0f);
        }

    }
}
