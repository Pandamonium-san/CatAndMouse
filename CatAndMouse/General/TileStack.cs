using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatAndMouse
{
    public class TileStack
    {
        TileNode head;
        public int count;

        public TileStack()
        {
            count = 0;
        }

        public void Push(Tile tile)
        {
            TileNode tn = new TileNode(tile, null);
            if (head == null)
                head = tn;
            else
            {
                tn.next = head;
                head = tn;
            }
            count++;
        }

        public Tile Pop()
        {
            if (count == 0)
                return null;
            Tile temp = head.tile;
            head = head.next;
            count--;
            return temp;
        }
    }

    class TileNode
    {
        public Tile tile;
        public TileNode next;

        public TileNode(Tile tile, TileNode next)
        {
            this.tile = tile;
            this.next = next;
        }
    }
}
