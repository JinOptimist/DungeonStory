using Assets.Maze.Cell;
using Assets.MazeGenerationScript.Cell;
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

        public List<Enemy> Enemies { get; set; }

        /// <summary>
        /// Только ландшафт без героя
        /// </summary>
        public List<ICell> Cells { get; set; }
        public List<ICell> CellsWithCharacters
        {
            get
            {
                var copyCells = Cells.Select(x => x).ToList();
                ReplaceCell(copyCells, Player);
                if (Enemies.Any())
                {
                    Enemies.ForEach(x => ReplaceCell(copyCells, x));
                }
                return copyCells;
            }
        }

        public ICell this[int x, int z]
        {
            get
            {
                return Cells.SingleOrDefault(cell => cell.X == x && cell.Z == z);
            }
            set
            {
                ReplaceCell(value);
            }
        }

        public MazeLevelBusinessObject(int width, int height)
        {
            Width = width;
            Height = height;
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
