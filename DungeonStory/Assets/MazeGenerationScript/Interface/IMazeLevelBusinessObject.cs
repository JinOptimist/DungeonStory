using Assets.Maze.Cell;
using System.Collections.Generic;

namespace Assets.Maze
{
    public interface IMazeLevelBusinessObject
    {
        int Width { get; set; }
        int Height { get; set; }
        Player Player { get; set; }

        List<ICell> Cells { get; set; }
        List<ICell> CellsWithCharacters { get; }

        void ReplaceCell(ICell cell);
        void ReplaceCell(List<ICell> cells, ICell cell);
    }
}