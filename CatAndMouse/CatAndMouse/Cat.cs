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
        protected List<Mouse> playerMice;
        protected List<Direction> possibleDirections;
        protected int spriteOriginX, spriteOriginY;
        protected int newDir;

        public Cat(Texture2D texture, Vector2 pos):base(texture, pos)
        {
            hitbox = new Rectangle((int)pos.X, (int)pos.Y, spriteRec.Width, spriteRec.Height);
            offset = 10;
            scale = 1f;
            speed = 2;
            maxMoveTime = 16;
            frameWidth = 32;
            frameHeight = 32;
            frameInterval = 150;
            spriteRec = new Rectangle(0, 0, frameWidth, frameHeight);
            vectorOrigin = new Vector2(spriteRec.Width / 2, spriteRec.Height / 2);
        }

        public void AvoidDirectionReversal()
        {
                switch (spriteDirection)        //Removes all instances of reverse direction from list of possible directions
                {
                    case Direction.Down:
                        possibleDirections.RemoveAll(d => d == Direction.Up);
                        break;
                    case Direction.Up:
                        possibleDirections.RemoveAll(d => d == Direction.Down);
                        break;
                    case Direction.Left:
                        possibleDirections.RemoveAll(d => d == Direction.Right);
                        break;
                    case Direction.Right:
                        possibleDirections.RemoveAll(d => d == Direction.Left);
                        break;
                }
                if(possibleDirections.Count==0)
                {
                    AddPossibleDirections();
                    if (possibleDirections.Count == 1)
                    {
                        Move(possibleDirections[0]);
                        return;
                    }
                    else
                        AvoidDirectionReversal();
                }
        }

        public void AddPossibleDirections()
        {
            if (downPossible)
                possibleDirections.Add(Direction.Down);
            if (upPossible)
                possibleDirections.Add(Direction.Up);
            if (leftPossible)
                possibleDirections.Add(Direction.Left);
            if (rightPossible)
                possibleDirections.Add(Direction.Right);
        }

        public void PickDirection()
        {
            if (!moving)
            {
                if (possibleDirections.Count != 0)
                {
                    newDir = Game1.rnd.Next(0, possibleDirections.Count);
                    Move(possibleDirections[newDir]);
                }
                else
                    MoveRandomly();
            }
        }

        public void MoveRandomly()  //Select random direction but avoid walking backwards unless it's the only option
        {
            AddPossibleDirections();
            AvoidDirectionReversal();
            PickDirection();
        }

        public Vector2 GetClosestTarget(List<Mouse> mice)
        {
            Vector2 closestMouse = new Vector2(5000, 5000), currentMouse;
            foreach (Mouse m in mice)
            {
                currentMouse = m.pos - this.pos;
                if (currentMouse.Length() < closestMouse.Length())
                    closestMouse = currentMouse;

            }
            closestMouse.Normalize();
            return closestMouse;
        }

        public void GetTargetList(List<Mouse> playerMice)
        {
            this.playerMice = playerMice;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteRec = new Rectangle(spriteOriginX + frameWidth * frame, spriteOriginY + frameHeight * (int)spriteDirection, frameWidth, frameHeight);
            
            base.Draw(spriteBatch);
            //spriteBatch.Draw(texture, hitbox, Color.Red);
        }

        public override void CheckValidDirections(Tile [,] tiles)
        {
            base.CheckValidDirections(tiles);
            possibleDirections = new List<Direction>();
        }
    }
}
