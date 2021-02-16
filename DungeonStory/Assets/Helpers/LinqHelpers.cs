using Assets.Maze.Cell;
using Assets.MazeGenerationScript.GenerationHelpObj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Helpers
{
    public static class LinqHelpers
    {
        private static Random _random = new Random();

        public static T GetRandom<T>(this IEnumerable<T> array)
        {
            if (!array.Any())
            {
                return default(T);
            }

            var index = _random.Next(array.Count());
            return array.ToList()[index];
        }

        public static CellWithWeight GetRandomByWeight(this IEnumerable<CellWithWeight> array)
        {
            if (!array.Any())
            {
                return null;
            }

            var goodItems = array
                .Where(x => x.IsReadyToBreak)
                .Where(x => x.Weight > 0)
                .OrderBy(x => x.Weight);

            if (!goodItems.Any())
            {
                return null;
            }

            var sum = goodItems.Sum(x => x.Weight);
            var randomFloat = _random.NextDouble() * 1f;

            var currentSum = 0f;
            foreach (var item in goodItems)
            {
                currentSum += item.Weight / sum;
                if (randomFloat < currentSum)
                {
                    return item;
                }
            }

            return goodItems.Last();
        }


        public static List<ICell> ReplaceCell(this List<ICell> cells, ICell cell)
        {
            var oldCell = cells
                .Single(currentCell => currentCell.X == cell.X && currentCell.Z == cell.Z);
            cells.Remove(oldCell);
            cells.Add(cell);

            return cells;
        }

        public static List<CellWithWeight> ReplaceCell(this List<CellWithWeight> cells,
            CellWithWeight cellWithWeight)
        {
            var oldCell = cells
                .Single(currentCell =>
                currentCell.Cell.X == cellWithWeight.Cell.X
                && currentCell.Cell.Z == cellWithWeight.Cell.Z);
            cells.Remove(oldCell);
            cells.Add(cellWithWeight);

            return cells;
        }

        public static void RemoveCell(this IEnumerable<CellWithWeight> array, ICell cell)
        {
            if (!array.Any())
            {
                return;
            }

            var list = array.ToList();
            var item = list.SingleOrDefault(x => x.Cell == cell);
            list.Remove(item);
        }
    }
}
