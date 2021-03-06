﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatAndMouse
{
  /// <summary>
  /// Always goes the shortest path to the player, can turn around at intersections
  /// </summary>
    class GeniusCat : SmartCat
    {
        protected int targetX, targetY;
        protected TileStack path = null;

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
                {
                    canReverseDirection = false;
                    MoveTowardsTarget(playerMice);
                }
            }
            base.Update(gameTime, tiles);
        }

        protected virtual void SelectTarget()
        {
            targetX = playerMice[0].gridX;
            targetY = playerMice[0].gridY;
        }

        protected virtual void FindPath(Tile[,] tiles)
        {
            canReverseDirection = true;
            playerMice[0].UpdateGridPos();
            SelectTarget();
            if (ObjectManager.IndexIsNotOutOfRange(targetX, targetY, tiles))
                path = PathFinder.FindPath(tiles[gridX, gridY], tiles[targetX, targetY], tiles);
            FollowPath(path);
        }

        protected void FollowPath(TileStack path)
        {
            if (path == null || path.count == 0)
            {
                MoveTowardsTarget(playerMice);
                return;
            }
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

            AvoidDirectionReversal();
            PickDirection();
        }
    }
}
