using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CatAndMouse
{
    class SmartCat:Cat
    {
        public SmartCat(Texture2D texture, Vector2 pos):base(texture, pos)
        {
            spriteOriginX = 0;
            spriteOriginY = 0;
        }

        public void MoveTowardsTarget(List<Mouse> mice)
        {
            Vector2 targetDir = GetClosestTarget(mice);

            if (targetDir.X > 0 && rightPossible)
                possibleDirections.Add(Direction.Right);
            else if (targetDir.X < 0 && leftPossible)
                possibleDirections.Add(Direction.Left);
            if (targetDir.Y > 0 && downPossible)
                possibleDirections.Add(Direction.Down);
            else if (targetDir.Y < 0 && upPossible)
                possibleDirections.Add(Direction.Up);

            AvoidDirectionReversal();
            if (possibleDirections.Count == 0)
            {
                AddPossibleDirections();
                AvoidDirectionReversal();
            }
            PickDirection();
        }

        public override void Update(GameTime gameTime, Tile[,] tiles)
        {
            if (!moving)
            {
                UpdateGridPos();
                if (tiles[gridPosX, gridPosY].isForkTile)
                    MoveTowardsTarget(playerMice);
                else
                    MoveRandomly();
            }
            base.Update(gameTime, tiles);
        }

    }
}
