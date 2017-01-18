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

    public Vector2Int pos;
    Vector2Int dir;
    Vector2Int bounds;
    int steps = 0;

    List<Vector2Int> possibleDirs;
    MapEditor me;
    bool[,] visited;

    public Agent(int arrayX, int arrayY, MapEditor me)
    {
      rnd = new Random();
      directions = new List<Vector2Int>();

      directions.Add(new Vector2Int(0, -1));
      directions.Add(new Vector2Int(0, 1));
      directions.Add(new Vector2Int(-1, 0));
      directions.Add(new Vector2Int(1, 0));

      visited = new bool[arrayX, arrayY];
      possibleDirs = new List<Vector2Int>(directions);

      pos = new Vector2Int(rnd.Next(arrayX - 2), rnd.Next(arrayY - 2));
      dir = new Vector2Int(0, 0);
      bounds = new Vector2Int(arrayX - 2, arrayY - 2);

      this.me = me;
      PickDirection();
    }

    public void Step()
    {
      ++steps;
      if (rnd.Next(100) < 30 && steps % 2 == 0)
        PickDirection();
      if (IsValidDirection(dir))
        pos = pos + dir;
      else
      {
        PickDirection();
        pos = pos + dir;
      }
      //if (!visited[pos.X, pos.Y])
      {
        Tile t = me.tiles[pos.X + 1, pos.Y + 1];
        if (steps == 1)
          SetTile(Tile.TileType.mouse);
        else if (steps == 75)
          SetTile(Tile.TileType.dumbcat);
        else if (steps == 150)
          SetTile(Tile.TileType.smartcat);
        else if (steps == 225)
          SetTile(Tile.TileType.intelligentcat);
        else if (steps == 300)
          SetTile(Tile.TileType.geniuscat);
        else if (t.type != Tile.TileType.mouse &&
          t.type != Tile.TileType.dumbcat &&
          t.type != Tile.TileType.smartcat &&
          t.type != Tile.TileType.intelligentcat &&
          t.type != Tile.TileType.geniuscat)
          SetTile(Tile.TileType.cheese);
      }
    }

    private void PickDirection()
    {
      possibleDirs = new List<Vector2Int>(directions);
      int index = rnd.Next(possibleDirs.Count());

      // can't go backwards or forwards, must turn
      while ((!IsValidDirection(possibleDirs[index]) || possibleDirs[index] == dir || possibleDirs[index] == dir * -1) && possibleDirs.Count() != 0)
      {
        possibleDirs.RemoveAt(index);
        index = rnd.Next(possibleDirs.Count());
      }
      dir = possibleDirs[index];
    }

    // can't face outside bounds
    private bool IsValidDirection(Vector2Int dir)
    {
      Vector2Int newPos = pos + dir;
      if ((newPos.X >= bounds.X || newPos.X < 0 || newPos.Y >= bounds.Y || newPos.Y < 0))
        return false;
      return true;
    }

    public void SetTile(Tile.TileType type)
    {
      me.SetTile(pos.X + 1, pos.Y + 1, type);
      visited[pos.X, pos.Y] = true;
    }

    public void ChangeDirection(Vector2Int dir)
    {
      this.dir = dir;
    }
  }
}
