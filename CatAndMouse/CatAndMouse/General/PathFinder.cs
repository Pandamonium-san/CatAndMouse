using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatAndMouse
{
    static class PathFinder
    {
        public static List<Tile> openList;
        public static Tile currentTile;
        public static Tile targetTile;

        public static int maxIterations = 1000;

        static float G;

        public static TileStack FindPath(Tile startTile, Tile destinationTile, Tile[,] tiles)
        {
            openList = new List<Tile>();
            foreach (Tile n in tiles)
                n.ResetValues();

            targetTile = destinationTile;
            currentTile = startTile;
            currentTile.open = true;

            for (int i = 0; i < maxIterations; i++)
            {
                AddAdjacentOpenTiles(currentTile);
                currentTile = FindBestTile();
                if (currentTile == destinationTile)
                    return Path(startTile, destinationTile);
                if (openList.Count <= 0)
                    return null;
            }
            return null;
        }

        public static TileStack FindPathForwardOnly(Tile startTile, Tile destinationTile, Tile[,] tiles, Cat cat)
        {
            openList = new List<Tile>();
            foreach (Tile n in tiles)
                n.ResetValues();

            targetTile = destinationTile;
            currentTile = startTile;
            currentTile.open = true;

            if(ObjectManager.IndexIsNotOutOfRange(currentTile.gridX - (int)cat.dir.X, currentTile.gridY - (int)cat.dir.Y, tiles))
                tiles[currentTile.gridX - (int)cat.dir.X, currentTile.gridY - (int)cat.dir.Y].closed = true;

            for (int i = 0; i < maxIterations; i++)
            {
                AddAdjacentOpenTiles(currentTile);
                currentTile = FindBestTile();
                if (currentTile == destinationTile)
                    return Path(startTile, destinationTile);
                if (openList.Count <= 0)
                    return null;
            }
            return null;
        }

        static Tile FindBestTile()
        {
            Tile lowestFTile = currentTile;
            float lowestF = -1;

            foreach (Tile n in openList)
            {
                if (n.open)
                    if (lowestF < 0 || n.F < lowestF)
                    {
                        lowestF = n.F;
                        lowestFTile = n;
                    }
            }
            return lowestFTile;
        }

        static void AddAdjacentOpenTiles(Tile t)
        {
            foreach (Tile neighbor in t.neighbors)
            {
                G = t.G + 1;

                if ((!neighbor.closed && !neighbor.open) || neighbor.G >= G)
                {
                    neighbor.G = G;
                    neighbor.H = Heuristic(neighbor);
                    neighbor.parent = t;
                }
                if (!neighbor.closed && !neighbor.open)
                {
                    neighbor.open = true;
                    openList.Add(neighbor);
                }
            }
            CloseTile(t);
        }

        static float Heuristic(Tile t)
        {
            //float H;

            //H = Math.Abs(targetTile.gridX - t.gridX) + Math.Abs(targetTile.gridY - t.gridY);    //Manhattan distance

            //return H + (targetTile.pos - t.pos).Length() / 1000f; //tiebreaker Euclidean distance

            return 0;   //Not using a heuristic is more effective for this game since maps are relatively small and pathfinding through teleporters is more accurate.
        }

        static void CloseTile(Tile t)
        {
            openList.Remove(t);
            t.open = false;
            t.closed = true;
        }

        static TileStack Path(Tile startTile, Tile destinationTile)
        {
            TileStack path = new TileStack();
            Tile parentTile = destinationTile;
            while(parentTile != startTile)
            {
                if (parentTile == startTile)
                    return path;
                path.Push(parentTile);
                parentTile = parentTile.parent;
            }
            return path;
        }
    }
}
