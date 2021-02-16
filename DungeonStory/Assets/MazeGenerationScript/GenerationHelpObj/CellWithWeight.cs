using Assets.Maze.Cell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.MazeGenerationScript.GenerationHelpObj
{
    public class CellWithWeight
    {
        public ICell Cell { get; set; }
        public float Weight { get; set; }
        public bool IsReadyToBreak { get; set; } = false;

        public override string ToString()
        {
            var t = Cell is Wall ? "W" : "G";
            return $"[{Cell.X}, {Cell.Z}: {t}: {Weight}: {(IsReadyToBreak ? '+' : '-')}]";
        }
    }
}
