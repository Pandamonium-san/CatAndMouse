using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatAndMouse
{
    class IntelligentCat:Cat
    {
        protected PathFinder pf;
        protected List<Tile> openTiles;
        protected int targetX, targetY;
        protected bool intelligent;

        public IntelligentCat(Texture2D texture, Vector2 pos):base(texture, pos)
        {
            spriteOriginX = 128;
            spriteOriginY = 0;
            pf = new PathFinder();
            openTiles = new List<Tile>();
            intelligent = true;
        }

        public override void Update(GameTime gameTime, Tile[,] tiles)
        {
            if (!moving)
                FindPath(tiles);
            base.Update(gameTime, tiles);
        }

        public virtual void FindPath(Tile[,] tiles)
        {
            if (intelligent)
            {
                playerMice[0].UpdateGridPos();
                targetX = playerMice[0].gridPosX + (int)playerMice[0].dir.X * 4;
                targetY = playerMice[0].gridPosY + (int)playerMice[0].dir.Y * 4;
            }
            if(targetX >= 0 && targetY >= 0 && targetX <= tiles.GetLength(0)-1 && targetY <= tiles.GetLength(1)-1)  //Dodge the null reference errors
            pf.BreadthFirstSearch(tiles, tiles[targetX, targetY], this);
            switch (tiles[gridPosX, gridPosY].pDir)
            {
                case Tile.PDir.down:
                    if (downPossible)
                        possibleDirections.Add(Direction.Down);
                    break;
                case Tile.PDir.up:
                    if (upPossible)
                        possibleDirections.Add(Direction.Up);
                    break;
                case Tile.PDir.left:
                    if (leftPossible)
                        possibleDirections.Add(Direction.Left);
                    break;
                case Tile.PDir.right:
                    if (rightPossible)
                        possibleDirections.Add(Direction.Right);
                    break;
                case null:
                    MoveRandomly();
                    break;
            }
            AvoidDirectionReversal();
            PickDirection();
        }

    }
}
