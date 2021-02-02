using Assets.Maze;
using Assets.Maze.Cell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.MazeGenerationScript.Cell
{
    public class Enemy : BaseCell
    {
        public int Hp { get; set; }

        public Enemy(int x, int y, IMazeLevelBusinessObject maze) : base(x, y, maze) { }
    }
}
