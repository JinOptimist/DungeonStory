using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Maze.Cell
{
    public class Wall : BaseCell
    {
        public int Durability { get; set; }

        public Wall(int x, int y, IMazeLevelBusinessObject maze) : base(x, y, maze) { }
    }
}
