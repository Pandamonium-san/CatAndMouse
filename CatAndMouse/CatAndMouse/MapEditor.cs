using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatAndMouse
{
    class MapEditor
    {
        public Tile[,] tiles;

        Rectangle spriteRec = new Rectangle(0, 0, 32, 32);
        int tileSize, mapOffset;
        int arrayX, arrayY;

        public MapEditor(int x, int y)
        {
            arrayX = x + 3;
            arrayY = y + 3;
            tileSize = 32;
            mapOffset = tileSize * 2;   
            tiles = new Tile[arrayX, arrayY];
            for (int i = 0; i < arrayX; i++)
            {
                for (int j = 0; j < arrayY; j++)
                {
                    tiles[j, i] = new WallTile(ObjectManager.tileTexture, new Vector2(j * tileSize - mapOffset, i * tileSize - mapOffset));
                }
            }
        }

        public void Update()
        {
  
            for (int i = 0; i < arrayX; i++)
			{
                for (int j = 0; j < arrayY; j++)
                {
                    if (tiles[i, j].hitbox.Contains(KeyMouseReader.LeftClickPos))
                    {
                        if (tiles[i, j].tileID < 4)
                            ++tiles[i, j].tileID;
                        else
                            tiles[i, j].tileID = 0;
                        switch (tiles[i, j].tileID)
                        {
                            case 0:
                                tiles[i, j].type = Tile.TileType.wall;
                                break;
                            case 1:
                                tiles[i, j].type = Tile.TileType.floor;
                                break;
                            case 2:
                                tiles[i, j].type = Tile.TileType.cat;
                                break;
                            case 3:
                                tiles[i, j].type = Tile.TileType.cheese;
                                break;
                            case 4:
                                tiles[i, j].type = Tile.TileType.mouse;
                                break;
                        }
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tile t in tiles)
            {
                spriteBatch.Draw(ObjectManager.tileTexture, t.pos, new Rectangle(32, 0, 32, 32), Color.White);
                switch(t.type)
                {
                    case Tile.TileType.wall:
                        spriteBatch.Draw(ObjectManager.tileTexture, t.pos, spriteRec, Color.White);
                        break;
                    case Tile.TileType.mouse:
                        spriteBatch.Draw(ObjectManager.mouseTexture, t.pos, spriteRec, Color.White);
                        break;
                    case Tile.TileType.cat:
                        spriteBatch.Draw(ObjectManager.catTexture, t.pos, spriteRec, Color.White);
                        break;
                    case Tile.TileType.cheese:
                        spriteBatch.Draw(ObjectManager.cheeseTexture, t.pos + new Vector2(5,5), spriteRec, Color.White, 0f, Vector2.Zero, 0.7f,SpriteEffects.None, 0f);
                        break;
                }
            }

        }
    }
}
