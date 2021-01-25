using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Maze.Cell
{
    public abstract class BaseCell : ICell
    {
        public int X { get; set; }
        public int Z { get; set; }
        public IMazeLevelBusinessObject Maze { get; set; }

        public BaseCell() { }

        public BaseCell(int x, int z, IMazeLevelBusinessObject maze)
        {
            X = x;
            Z = z;
            Maze = maze;
        }
    }
}
