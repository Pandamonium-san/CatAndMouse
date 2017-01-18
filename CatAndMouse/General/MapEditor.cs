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
    public EditorHUD hud;

    Rectangle defaultRec = new Rectangle(0, 0, 32, 32);
    public Rectangle mapRec;
    public int arrayX, arrayY;
    enum Place { Mouse, DumbCat, SmartCat, IntelligentCat, GeniusCat, Cheese, Wall, Floor, Teleporter }
    Place placeTool = Place.Wall;

    Agent agent;

    public MapEditor(int x, int y)
    {
      arrayY = x;
      arrayX = y;
      tiles = new Tile[arrayX, arrayY];
      mapRec = new Rectangle(0, 0, (tiles.GetLength(1)) * Tile.tileSize, (tiles.GetLength(0)) * Tile.tileSize);
      hud = new EditorHUD(mapRec.Width, mapRec.Height + EditorHUD.hudHeight);

      for (int i = 0; i < arrayX; i++)
      {
        for (int j = 0; j < arrayY; j++)
        {
          tiles[i, j] = new WallTile(ObjectManager.tileTexture, new Vector2(j * Tile.tileSize, i * Tile.tileSize));
        }
      }
    }

    public void LoadMap(String mapPath)
    {
      List<String> mapData = MapHandler.GetMapFromText(mapPath);

      if (mapData == null)
      {
        //mapData = MapHandler.GenerateDefaultMap();
        //mapRec = new Rectangle(0, 0, (tiles.GetLength(1) - 4) * 32, (tiles.GetLength(0) - 4) * 32);
        //hud = new EditorHUD(mapRec.Width, mapRec.Height + 2 * 32);

        CreateRandomMap();
        return;
      }

      arrayX = mapData.Count;
      arrayY = mapData[0].Length;
      tiles = new Tile[arrayX, arrayY];
      mapRec = new Rectangle(0, 0, (tiles.GetLength(1)) * Tile.tileSize, (tiles.GetLength(0)) * Tile.tileSize);
      hud = new EditorHUD(mapRec.Width, mapRec.Height + EditorHUD.hudHeight);

      for (int i = 0; i < mapData.Count; i++)
      {
        for (int j = 0; j < mapData[i].Length; j++)
        {
          tiles[i, j] = new WallTile(ObjectManager.tileTexture, new Vector2(j * Tile.tileSize, i * Tile.tileSize));
          if (mapData[i][j] == 'W')
            tiles[i, j].type = Tile.TileType.wall;
          else if (mapData[i][j] == '_')
            tiles[i, j].type = Tile.TileType.floor;
          else if (mapData[i][j] == 'C')
          {
            tiles[i, j].type = Tile.TileType.cheese;
          }
          else if (mapData[i][j] == 'M')
          {
            tiles[i, j].type = Tile.TileType.mouse;
          }
          else if (mapData[i][j] == 'E')
          {
            tiles[i, j].type = Tile.TileType.dumbcat;
          }
          else if (mapData[i][j] == 'S')
          {
            tiles[i, j].type = Tile.TileType.smartcat;
          }
          else if (mapData[i][j] == 'I')
          {
            tiles[i, j].type = Tile.TileType.intelligentcat;
          }
          else if (mapData[i][j] == 'G')
          {
            tiles[i, j].type = Tile.TileType.geniuscat;
          }
          else if (mapData[i][j] == 'T')
          {
            tiles[i, j].type = Tile.TileType.teleporter;
          }
        }
      }
    }

    public void SetTile(int x, int y, Tile.TileType type)
    {
      tiles[x, y].type = type;
    }

    protected void CreateRandomMap()
    {
      // Initialize
      for (int i = 0; i < arrayX; i++)
      {
        for (int j = 0; j < arrayY; j++)
        {
          tiles[i, j] = new WallTile(ObjectManager.tileTexture, new Vector2(j * Tile.tileSize, i * Tile.tileSize));
        }
      }

      // Create map agent
      agent = new Agent(this, 300, 0.3, true, Game1.rnd.Next());
      agent.Execute();
    }

    public void Update(GameWindow window)
    {
      hud.Update();
      foreach (EditorButton b in hud.buttons)
      {
        if (b.ButtonClicked())
          placeTool = (Place)b.type;
      }
      if (KeyMouseReader.KeyPressed(Keys.W))
      {
        ++placeTool;
        if ((int)placeTool > 8)
          placeTool = (Place)0;
      }
      if (KeyMouseReader.KeyPressed(Keys.Q))
      {
        --placeTool;
        if ((int)placeTool < 0)
          placeTool = (Place)8;
      }
      if (KeyMouseReader.keyState.IsKeyDown(Keys.N) && agent != null)
      {
        agent.Step();
      }
      if (hud.btnRandom.ButtonClicked())
      {
        CreateRandomMap();
      }
      foreach (Tile tile in tiles)
      {
        if (tile.hitbox.Contains(KeyMouseReader.mousePos) && KeyMouseReader.mouseState.LeftButton == ButtonState.Pressed && mapRec.Contains(KeyMouseReader.mousePos))
        {
          tile.type = (Tile.TileType)placeTool;
        }
      }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      foreach (Tile t in tiles)
      {
        spriteBatch.Draw(ObjectManager.tileTexture, t.pos, new Rectangle(32, 0, 32, 32), Color.White);  //Draw floor texture first, then draw stuff over it if needed
        switch (t.type)
        {
          case Tile.TileType.wall:
            spriteBatch.Draw(ObjectManager.tileTexture, t.pos, defaultRec, Color.White);
            break;
          case Tile.TileType.teleporter:
            spriteBatch.Draw(ObjectManager.tileTexture, t.pos, new Rectangle(64, 0, 32, 32), Color.White);
            break;
          case Tile.TileType.mouse:
            spriteBatch.Draw(ObjectManager.mouseTexture, t.pos, defaultRec, Color.White);
            break;
          case Tile.TileType.cheese:
            spriteBatch.Draw(ObjectManager.cheeseTexture, t.pos + new Vector2(5, 5), defaultRec, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
            break;
          case Tile.TileType.dumbcat:
            spriteBatch.Draw(ObjectManager.catTexture, t.pos, new Rectangle(0, 128, 32, 32), Color.White);
            break;
          case Tile.TileType.smartcat:
            spriteBatch.Draw(ObjectManager.catTexture, t.pos, new Rectangle(0, 0, 32, 32), Color.White);
            break;
          case Tile.TileType.intelligentcat:
            spriteBatch.Draw(ObjectManager.catTexture, t.pos, new Rectangle(128, 0, 32, 32), Color.White);
            break;
          case Tile.TileType.geniuscat:
            spriteBatch.Draw(ObjectManager.catTexture, t.pos, new Rectangle(128, 128, 32, 32), Color.White);
            break;
        }
      }
      spriteBatch.DrawString(Game1.font, placeTool.ToString(), Vector2.Zero, Color.White);
      hud.Draw(spriteBatch, (int)placeTool);

      // Draw agent
      if (agent != null)
        spriteBatch.Draw(ObjectManager.tileTexture, tiles[agent.pos.X, agent.pos.Y].pos, new Rectangle(32, 0, 32, 32), Color.Red* 0.5f);
    }

  }
}
