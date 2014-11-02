using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatAndMouse
{
    class PathFinder
    {
        public List<Tile> openTiles;
        public int tileCount;
        public int tileCounter;
        public bool baseTileAdded, tilesCounted;

        public PathFinder()
        {
            openTiles = new List<Tile>();
            tileCount = 0;
            tileCounter = 0;
            baseTileAdded = false;
            tilesCounted = false;
        }

        public void ExpandFrontier(Tile[,] tiles, Tile tile)
        {
            if (tile.gridPosX + 2 <= tiles.GetLength(0) && !tiles[tile.gridPosX + 1, tile.gridPosY].isSolid && !tiles[tile.gridPosX + 1, tile.gridPosY].visited)    //if(not out of range && not solid && not visited)
            {
                openTiles.Add(tiles[tile.gridPosX + 1, tile.gridPosY]);
                tiles[tile.gridPosX + 1, tile.gridPosY].pDir = Tile.PDir.left;
            }
            if (tile.gridPosX - 1 >= 0 && !tiles[tile.gridPosX - 1, tile.gridPosY].isSolid && !tiles[tile.gridPosX - 1, tile.gridPosY].visited)
            {
                openTiles.Add(tiles[tile.gridPosX - 1, tile.gridPosY]);
                tiles[tile.gridPosX - 1, tile.gridPosY].pDir = Tile.PDir.right;
            }
            if (tile.gridPosY + 2 <= tiles.GetLength(1) && !tiles[tile.gridPosX, tile.gridPosY + 1].isSolid && !tiles[tile.gridPosX, tile.gridPosY + 1].visited)
            {
                openTiles.Add(tiles[tile.gridPosX, tile.gridPosY + 1]);
                tiles[tile.gridPosX, tile.gridPosY + 1].pDir = Tile.PDir.up;
            }
            if (tile.gridPosY - 1 >= 0 && !tiles[tile.gridPosX, tile.gridPosY - 1].isSolid && !tiles[tile.gridPosX, tile.gridPosY - 1].visited)
            {
                openTiles.Add(tiles[tile.gridPosX, tile.gridPosY - 1]);
                tiles[tile.gridPosX, tile.gridPosY - 1].pDir = Tile.PDir.down;
            }
            tile.visited = true;
        }

        public void BreadthFirstSearch(Tile[,] tiles, Tile t, Cat cat)  //Starts at mouses's position and scans outwards
        {
            if (!tilesCounted)
            {
                foreach (Tile tile in tiles)
                {
                    if (tile is FloorTile)
                        tileCount++;
                }
                tilesCounted = true;
            }

            if (!baseTileAdded)
            {
                openTiles.Add(t);
                baseTileAdded = true;
            }
            
            for (int j = 0; j < tileCount; j++)
            {
                foreach (Tile tile in openTiles)
                {
                    if (tile.hitbox.Intersects(cat.hitbox) && tile.visited)     //Ends search when target is found
                    {
                        EndSearch();
                        return;
                    } 
                    if (!tile.visited)
                    {
                        ExpandFrontier(tiles, tile);
                        break;
                    }
                }
            }
            EndSearch();
        }

        public void EndSearch()
        {
            foreach (Tile ti in openTiles)
                ti.visited = false;
            openTiles = new List<Tile>();
            baseTileAdded = false;
        }

    }
}
