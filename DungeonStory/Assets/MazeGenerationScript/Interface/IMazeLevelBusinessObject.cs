using Assets.Maze.Cell;
using System.Collections.Generic;

namespace Assets.Maze
{
    public interface IMazeLevelBusinessObject
    {
        List<ICell> Cells { get; set; }
        List<ICell> CellsWithPlayer { get; }
        int Height { get; set; }
        Player Player { get; set; }
        int Width { get; set; }

        void ReplaceCell(ICell cell);
        void ReplaceCell(List<ICell> cells, ICell cell);
    }
}