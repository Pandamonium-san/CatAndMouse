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
        public static String test = @"Content\test.txt";
        public static String lvl1 = @"Content\lvl1.txt";
        public static String lvl2 = @"Content\lvl2.txt";
        public static String lvl3 = @"Content\lvl3.txt";

        public static List<String> GetMapFromText(String mapPath)
        {
            if (!File.Exists(mapPath))
                return null;
            sr = new StreamReader(mapPath);
            mapData = new List<String>();
            while (!sr.EndOfStream)
            {
                mapData.Add(sr.ReadLine());
            }
            sr.Close();

            return mapData;
        }

        public static void SaveMapToText(Tile[,] tiles, string path)
        {
            mapData = new List<String>();
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                String currentLine = "";
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    if (tiles[i, j].type == Tile.TileType.wall)
                        currentLine += "W";
                    else if (tiles[i, j].type == Tile.TileType.floor)
                        currentLine += "_";
                    else if (tiles[i, j].type == Tile.TileType.dumbcat)
                        currentLine += "E";
                    else if (tiles[i, j].type == Tile.TileType.cheese)
                        currentLine += "C";
                    else if (tiles[i, j].type == Tile.TileType.mouse)
                        currentLine += "M";
                    else if (tiles[i, j].type == Tile.TileType.smartcat)
                        currentLine += "S";
                    else if (tiles[i, j].type == Tile.TileType.intelligentcat)
                        currentLine += "I";
                    else if (tiles[i, j].type == Tile.TileType.geniuscat)
                        currentLine += "G";
                    else if (tiles[i, j].type == Tile.TileType.teleporter)
                        currentLine += "T";
                    if(j==tiles.GetLength(1)-1)
                    {
                        mapData.Add(currentLine);
                    }
                }
            }
            File.WriteAllLines(path, mapData);
        }

        public static List<String> GenerateDefaultMap()
        {
            mapData = new List<String>();
            mapData.Add("WWWWWWWWWWWWWWWWWWWWWWWWW");
            mapData.Add("WWWWWWWWWWWWWWWWWWWWWWWWW");
            mapData.Add("WWWWWWWWWWWWWWWWWWWWWWWWW");
            mapData.Add("WWWCCCCCCCCCCCCCCCCCCSWWW");
            mapData.Add("WWWGWWWWWCWWWWCWCWWWWCWWW");
            mapData.Add("WWWCWWTWCCCCCWCWCCCCCCWWW");
            mapData.Add("WWWCWWCWWCWWCWCWWCWWWCWWW");
            mapData.Add("WWWCCCCWWCWWCWCWWTWCCCWWW");
            mapData.Add("WWWCWWCCWCWWCCCWWWWCWWWWW");
            mapData.Add("WWWCWWWCCMWWWWCCWCCCWCWWW");
            mapData.Add("WWWCWTWWWCWWWWWCWWWCWCWWW");
            mapData.Add("WWWCCCCWCCCWCCCCCCCCCCWWW");
            mapData.Add("WWWCWCWWWWCWCWWWWCWWWCWWW");
            mapData.Add("WWWCWCCCWWCCCCCCWCCCCCWWW");
            mapData.Add("WWWCWWWCCWWWWWWCWWCWWCWWW");
            mapData.Add("WWWCWWWWCWWWTWWCWWCWWCWWW");
            mapData.Add("WWWECCCCCCCCCICCCCCCCCWWW");
            mapData.Add("WWWWWWWWWWWWWWWWWWWWWWWWW");
            mapData.Add("WWWWWWWWWWWWWWWWWWWWWWWWW");
            mapData.Add("WWWWWWWWWWWWWWWWWWWWWWWWW");
            return mapData;
        }

    }
}
