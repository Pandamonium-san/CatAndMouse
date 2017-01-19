using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatAndMouse
{
  class Agent
  {
    Random rnd;
    List<Vector2Int> directions;
    List<int> cats;

    public Vector2Int pos;
    Vector2Int dir;
    int steps = 0;
    int totalSteps;
    double turnProbability;
    bool gridRestricted;

    List<Vector2Int> possibleDirs;
    MapEditor me;
    bool[,] visited;

    public Agent(MapEditor me, int totalSteps, double turnProbability, bool gridRestricted, int seed)
    {
      rnd = new Random(seed);
      directions = new List<Vector2Int>();
      directions.Add(new Vector2Int(0, -1));
      directions.Add(new Vector2Int(0, 1));
      directions.Add(new Vector2Int(-1, 0));
      directions.Add(new Vector2Int(1, 0));

      // 0 = dumb, 1 = smart, 2 = intelligent, 3 = genius
      cats = new List<int>();
      for (int i = 0; i < 4; i++)
        cats.Add(i);

      this.me = me;
      this.turnProbability = turnProbability;
      this.totalSteps = totalSteps;
      this.gridRestricted = gridRestricted;
      visited = new bool[me.arrayX, me.arrayY];
      possibleDirs = new List<Vector2Int>(directions);

      int x = rnd.Next(me.arrayX - 2) + 1;
      int y = rnd.Next(me.arrayY - 2) + 1;
      if (gridRestricted)
      {
        while (x % 2 != 1 || y % 2 != 1)
        {
          x = rnd.Next(me.arrayX - 2) + 1;
          y = rnd.Next(me.arrayY - 2) + 1;
        }
      }

      pos = new Vector2Int(x, y);
      dir = new Vector2Int(0, 0);
      ChangeDirection();
    }

    public void Execute()
    {
      for (int i = 0; i < totalSteps; i++)
      {
        Step();
      }
    }
    public void Step()
    {
      ++steps;
      if (rnd.NextDouble() < turnProbability && (!gridRestricted || steps % 2 == 1))
        ChangeDirection();

      // turn if facing map boundary
      if (!IsValidDirection(dir))
        ChangeDirection();

      pos = pos + dir;

      Tile t = me.tiles[pos.X, pos.Y];
      if (t.type == Tile.TileType.wall ||
          t.type == Tile.TileType.cheese)
      {
        if(steps == 1)
          SetTile(Tile.TileType.teleporter);
        else if (steps == 2)
          SetTile(Tile.TileType.mouse);
        else if (steps == totalSteps)
          SetTile(Tile.TileType.teleporter);

        else if (steps == totalSteps / 2) // guarantees at least 1 cat
          AddCat();
        else if (steps % 50 == 0 && rnd.NextDouble() < 0.8) // chance to add more cats
          AddCat();
        else if (steps % 100 == 0 && rnd.NextDouble() < 0.6) // chance to add teleporter
          SetTile(Tile.TileType.teleporter);

        else
          SetTile(Tile.TileType.cheese);
      }
    }

    private void ChangeDirection()
    {
      possibleDirs = new List<Vector2Int>(directions);
      int index = rnd.Next(possibleDirs.Count());

      // can't go backwards or forwards, must turn
      while ((!IsValidDirection(possibleDirs[index]) || possibleDirs[index] == dir || possibleDirs[index] == dir * -1) && possibleDirs.Count() > 0)
      {
        possibleDirs.RemoveAt(index);
        index = rnd.Next(possibleDirs.Count());
      }
      dir = possibleDirs[index];
    }

    private bool IsValidDirection(Vector2Int dir)
    {
      Vector2Int newPos = pos + dir;
      if ((newPos.X >= me.arrayX - 1 || newPos.X < 1 || newPos.Y >= me.arrayY - 1 || newPos.Y < 1))
        return false;
      return true;
    }

    private void AddCat()
    {
      if (cats.Count() == 0)
      {
        SetTile(Tile.TileType.cheese);
        return;
      }
      int index = rnd.Next(cats.Count());
      switch (cats[index])
      {
        case 0:
          SetTile(Tile.TileType.dumbcat);
          break;
        case 1:
          SetTile(Tile.TileType.smartcat);
          break;
        case 2:
          SetTile(Tile.TileType.intelligentcat);
          break;
        case 3:
          SetTile(Tile.TileType.geniuscat);
          break;
        default:
          SetTile(Tile.TileType.cheese);
          break;
      }
      cats.RemoveAt(index);
    }

    public void SetTile(Tile.TileType type)
    {
      me.SetTile(pos.X, pos.Y, type);
      visited[pos.X, pos.Y] = true;
    }

  }
}
