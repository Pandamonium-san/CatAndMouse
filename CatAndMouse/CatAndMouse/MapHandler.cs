using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CatAndMouse
{
    static class MapHandler
    {
        public static StreamReader sr;
        public static List<String> mapData;

        public static List<String> GetMapFromText(String relativeMapLocation)
        {
            sr = new StreamReader(relativeMapLocation);
            mapData = new List<String>();
            while (!sr.EndOfStream)
            {
                mapData.Add(sr.ReadLine());
            }
            sr.Close();

            return mapData;
        }

        public static void CreateMapFile(List<String> mapData, string path)
        {
            File.WriteAllLines(path, mapData);
        }

        public static void SaveMapToText(Tile[,] tiles)
        {
            mapData = new List<String>();
            for (int i = 0; i < tiles.GetLength(1); i++)
            {
                String currentLine = "";
                for (int j = 0; j < tiles.GetLength(0); j++)
                {
                    if (tiles[j, i].type == Tile.TileType.wall)
                        currentLine += "W";
                    else if (tiles[j, i].type == Tile.TileType.floor)
                        currentLine += "_";
                    else if (tiles[j, i].type == Tile.TileType.cat)
                        currentLine += "E";
                    else if (tiles[j, i].type == Tile.TileType.cheese)
                        currentLine += "C";
                    else if (tiles[j, i].type == Tile.TileType.mouse)
                        currentLine += "M";
                    if(j==tiles.GetLength(0)-1)
                    {
                        mapData.Add(currentLine);
                    }
                }
            }
            File.WriteAllLines(@"Content\test.txt", mapData);
        }



    }
}
