using Assets.Maze.Cell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Maze
{
    public class MazeLevelBusinessObject : IMazeLevelBusinessObject
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public Player Player { get; set; }

        /// <summary>
        /// Только ландшафт без героя
        /// </summary>
        public List<ICell> Cells { get; set; }
        public List<ICell> CellsWithPlayer
        {
            get
            {
                var copyCells = Cells.Select(x => x).ToList();
                ReplaceCell(copyCells, Player);
                return copyCells;
            }
        }

        public MazeLevelBusinessObject(int width, int height)
        {
            Width = width;
            Height = height;
            Player = new Player(0, 0, this);
            Cells = new List<ICell>();
        }

        /// <summary>
        /// Новая ячейка будет помещена в список Cells
        /// </summary>
        /// <param name="cell"></param>
        public void ReplaceCell(ICell cell)
        {
            ReplaceCell(Cells, cell);
        }

        public void ReplaceCell(List<ICell> cells, ICell cell)
        {
            var oldCell = cells
                .Single(currentCell => currentCell.X == cell.X && currentCell.Z == cell.Z);
            cells.Remove(oldCell);
            cells.Add(cell);
        }
    }
}
