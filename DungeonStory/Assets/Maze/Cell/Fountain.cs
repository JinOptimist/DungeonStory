using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Maze.Cell
{
    public class Fountain : BaseCell
    {
        public int Durability { get; set; }

        public Fountain(int x, int y, IMazeLevelBusinessObject maze) : base(x, y, maze) { }
    }
}
