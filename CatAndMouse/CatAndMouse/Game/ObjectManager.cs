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
    class ObjectManager
    {
        public static Texture2D tileTexture, mouseTexture, catTexture, cheeseTexture;
        public static int mapOffset = Tile.tileSize * 2; //Adds two layers of tiles outside screen to avoid null error when checking valid directions

        public List<String> mapData;
        public HUD hud;
        public Tile[,] tiles;
        public List<Tile> teleporters;
        public Rectangle mapRec;

        public List<Mouse> playerMice;
        public List<Cat> cats;
        public List<Cheese> cheeseList;

        public bool lose, victory;
        public int playerLives;
        public int cheeseEaten;


        public void LoadContent(ContentManager Content)
        {
            tileTexture = Content.Load<Texture2D>(@"tileSpritesheet");
            mouseTexture = Content.Load<Texture2D>(@"mouse");
            catTexture = Content.Load<Texture2D>(@"catsprites");
            cheeseTexture = Content.Load<Texture2D>(@"cheese");
        }

        public void Start(string mapPath)    //Initialize
        {
            CreateLevel(mapPath);
            foreach (Mouse m in playerMice)
            {
                m.CheckValidDirections(tiles);
            }
            foreach (Cat c in cats)
                c.CheckValidDirections(tiles);

            playerLives = 3;
            cheeseEaten = 0;
            lose = false;
            victory = false;
        }

        //Loads the level from path
        public void CreateLevel(String path)
        {
            mapData = MapHandler.GetMapFromText(path);
            if (mapData == null)
                mapData = MapHandler.GenerateDefaultMap();
            playerMice = new List<Mouse>();
            cats = new List<Cat>();
            cheeseList = new List<Cheese>();
            tiles = new Tile[mapData[0].Length, mapData.Count];

            for (int i = 0; i < mapData.Count; i++)
            {
                for (int j = 0; j < mapData[i].Length; j++)
                {
                    Vector2 position = new Vector2(j * Tile.tileSize - mapOffset, i * Tile.tileSize - mapOffset);

                    if (mapData[i][j] == 'W')
                        tiles[j, i] = new WallTile(tileTexture, position);
                    else if (mapData[i][j] == 'T')
                    {
                        tiles[j, i] = new TeleporterTile(tileTexture, position);
                    }
                    else
                    {
                        tiles[j, i] = new FloorTile(tileTexture, position);
                        position = new Vector2(j * Tile.tileSize - mapOffset + 16, i * Tile.tileSize - mapOffset + 16);
                    }

                    if (mapData[i][j] == '_')
                        continue;
                    else if (mapData[i][j] == 'C')
                    {
                        cheeseList.Add(new Cheese(cheeseTexture, position));
                    }
                    else if (mapData[i][j] == 'M')
                    {
                        playerMice.Add(new Mouse(mouseTexture, position));
                    }
                    else if (mapData[i][j] == 'E')
                    {
                        cats.Add(new DumbCat(catTexture, position));
                    }
                    else if (mapData[i][j] == 'S')
                    {
                        cats.Add(new SmartCat(catTexture, position));
                    }
                    else if (mapData[i][j] == 'I')
                    {
                        cats.Add(new IntelligentCat(catTexture, position));
                    }
                    else if (mapData[i][j] == 'G')
                    {
                        cats.Add(new GeniusCat(catTexture, position));
                    }
                }
            }

            teleporters = new List<Tile>();
            foreach (Tile tile in tiles)
            {
                if (!(tile is TeleporterTile))
                    continue;
                teleporters.Add(tile);
            }
            for (int i = 0; i < teleporters.Count; i++)
            {
                teleporters[i].teleporterId = i;
            }

            mapRec = new Rectangle(0, 0, (tiles.GetLength(0) - 4) * 32, (tiles.GetLength(1) - 2) * 32);
            hud = new HUD((tiles.GetLength(0) - 4) * 32, (tiles.GetLength(1) - 2) * 32);

            foreach (Tile tile in tiles)
            {
                tile.CheckSetForkTile(mapData, tiles);
            }
        }

        public List<Mouse> GetMouseList()
        {
            return this.playerMice;
        }

        //Teleporter logic
        public void TeleportUpdate(Actor go)
        {
            int targetID = 0;
            foreach (TeleporterTile tp in teleporters)
            {
                if (tp.hitbox.Contains(go.hitbox) && !go.teleported)
                {
                    targetID = tp.teleporterId + 1;
                    if (targetID > teleporters.Count - 1)
                        targetID = 0;
                    go.pos = teleporters[targetID].pos;
                    go.StopMoving(tiles);
                    go.teleported = true;
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            hud.Update(cheeseEaten, playerLives, cheeseList.Count);
            foreach (Mouse m in playerMice)
            {
                m.Update(gameTime, tiles);
                TeleportUpdate(m);
            }
            for (int i = 0; i < cats.Count; i++)
			{
                if (cats[i] is SmartCat || cats[i] is IntelligentCat)
                    cats[i].GetTargetList(playerMice);
            }
            foreach (Cat cat in cats)
            {
                cat.Update(gameTime, tiles);
                TeleportUpdate(cat);
                foreach(Mouse m in playerMice)
                    if (m.hitbox.Intersects(cat.hitbox) && !m.invulnerable)
                    {
                        --playerLives;
                        m.pos = m.startingPos;
                        m.StopMoving(tiles);
                        m.invulnerable = true;
                    }
            }
            foreach(Cheese c in cheeseList)
            {
                foreach (Mouse m in playerMice)
                if (m.hitbox.Intersects(c.hitbox))
                {
                    cheeseList.Remove(c);
                    ++cheeseEaten;
                    return;
                }
            }
            if (playerLives <= 0)
                lose = true;
            if (cheeseList.Count <= 0)
                victory = true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tile t in tiles)
            {
                t.Draw(spriteBatch);
            }
            foreach(Cheese c in cheeseList)
            {
                c.Draw(spriteBatch);
            }
            foreach(Cat cat in cats)
            {
                cat.Draw(spriteBatch);
            }
            foreach (Mouse m in playerMice)
            {
                m.Draw(spriteBatch);
            }
            hud.Draw(spriteBatch);
        }

    }
}
