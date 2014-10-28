using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CatAndMouse
{
    static class ObjectManager
    {
        public static Texture2D tileTexture, mouseTexture, catTexture, cheeseTexture;
        public static List<String> mapData;
        public static Tile[,] tiles;
        public static int tileSize = 32;
        public static int mapOffset = tileSize * 2; //Adds two layers of tiles outside screen to avoid null error when checking valid directions

        public static Mouse playerMouse;
        public static List<Cat> cats;
        public static List<Cheese> cheeseList;

        public static void LoadContent(ContentManager Content)
        {
            tileTexture = Content.Load<Texture2D>(@"tileSpritesheet");
            mouseTexture = Content.Load<Texture2D>(@"mouse");
            catTexture = Content.Load<Texture2D>(@"cat");
            cheeseTexture = Content.Load<Texture2D>(@"cheese");
        }

        public static void Initialize()
        {
            CreateMap(@"Content\test.txt");
            playerMouse.CheckValidDirections();
            foreach (Cat c in cats)
                c.CheckValidDirections();
        }

        public static void CreateMap(String path)
        {
            mapData = MapHandler.GetMapFromText(path);
            cats = new List<Cat>();
            cheeseList = new List<Cheese>();

            tiles = new Tile[mapData[0].Length,mapData.Count];

            for (int i = 0; i < mapData.Count; i++)
            {
                for (int j = 0; j < mapData[i].Length; j++)
                {
                    if (mapData[i][j] == 'W')
                        tiles[j,i] = new WallTile(tileTexture, new Vector2(j * tileSize-mapOffset, i * tileSize-mapOffset));
                    else if (mapData[i][j] == '_')
                        tiles[j,i] = new FloorTile(tileTexture, new Vector2(j * tileSize-mapOffset, i * tileSize-mapOffset));
                    else if (mapData[i][j] == 'C')
                    {
                        tiles[j, i] = new FloorTile(tileTexture, new Vector2(j * tileSize - mapOffset, i * tileSize - mapOffset));
                        cheeseList.Add(new Cheese(cheeseTexture, new Vector2(j * tileSize - mapOffset + 16, i * tileSize - mapOffset + 16)));
                    }
                    else if (mapData[i][j] == 'M')
                    {
                        tiles[j, i] = new FloorTile(tileTexture, new Vector2(j * tileSize - mapOffset, i * tileSize - mapOffset));
                        playerMouse = new Mouse(mouseTexture, new Vector2(j * tileSize - mapOffset + 16, i * tileSize - mapOffset + 16));
                    }
                    else if (mapData[i][j] == 'E')
                    {
                        tiles[j, i] = new FloorTile(tileTexture, new Vector2(j * tileSize - mapOffset, i * tileSize - mapOffset));
                        cats.Add(new Cat(catTexture, new Vector2(j * tileSize - mapOffset + 16, i * tileSize - mapOffset + 16)));
                    }
                }
            }
        }

        public static void Update()
        {
            playerMouse.Update();
            foreach (Cat cat in cats)
            {
                cat.Update();
                if (playerMouse.hitbox.Intersects(cat.hitbox))
                    Initialize();
            }
            foreach(Cheese c in cheeseList)
            {
                if (playerMouse.hitbox.Intersects(c.hitbox))
                {
                    cheeseList.Remove(c);
                    break;
                }
            }
        }

        public static void Draw(SpriteBatch spritebatch)
        {
            foreach (Tile t in tiles)
            {
                t.Draw(spritebatch);
            }
            foreach(Cheese c in cheeseList)
            {
                c.Draw(spritebatch);
            }
            foreach(Cat cat in cats)
            {
                cat.Draw(spritebatch);
            }
            playerMouse.Draw(spritebatch);

        }

    }
}
