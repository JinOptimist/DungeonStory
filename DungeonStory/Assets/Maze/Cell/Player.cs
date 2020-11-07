using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Maze.Cell
{
    public class Player : BaseCell
    {
        public int Money { get; set; }
        
        public Player(int x, int y, IMazeLevelBusinessObject maze) : base(x, y, maze) { }
    }
}
