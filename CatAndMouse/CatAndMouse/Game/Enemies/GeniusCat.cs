using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatAndMouse
{
    class GeniusCat : SmartCat
    {
        protected int targetX, targetY;

        public GeniusCat(Texture2D texture, Vector2 pos)
            : base(texture, pos)
        {
            spriteOriginX = 128;
            spriteOriginY = 128;
            canReverseDirection = true;
        }

        public override void Update(GameTime gameTime, Tile[,] tiles)
        {
            if (!moving)
            {
                UpdateGridPos();
                if (tiles[gridX, gridY].isForkTile)
                    FindPath(tiles);
                else
                    MoveRandomly();
            }
            base.Update(gameTime, tiles);
        }

        protected virtual void SelectTarget()
        {
            targetX = playerMice[0].gridX;
            targetY = playerMice[0].gridY;
        }

        public void FindPath(Tile[,] tiles)
        {
            playerMice[0].UpdateGridPos();
            SelectTarget();
            TileStack path = null;
            if (ObjectManager.IndexIsNotOutOfRange(targetX, targetY, tiles))
                path = PathFinder.FindPath(tiles[gridX, gridY], tiles[targetX, targetY], tiles);
            if (path == null)
                MoveTowardsTarget(playerMice);
            else
                FollowPath(path);
            AvoidDirectionReversal();
            PickDirection();
        }

        protected void FollowPath(TileStack path)
        {
            Tile t = path.Pop();
            int X = t.gridX - gridX;
            int Y = t.gridY - gridY;
            if (X == 1)
                possibleDirections.Add(Direction.Right);
            else if (X == -1)
                possibleDirections.Add(Direction.Left);
            else if (Y == 1)
                possibleDirections.Add(Direction.Down);
            else if (Y == -1)
                possibleDirections.Add(Direction.Up);
        }
    }
}
