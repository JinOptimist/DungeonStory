using Assets.Maze.Cell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Maze
{
    public class MazeBuilder
    {
        private MazeLevelBusinessObject _maze;
        private Random _random = new Random();

        public MazeLevelBusinessObject Generate(int width, int height, 
            double chanceOfCoin = 0.1, int maxCountOfFontaine = 3)
        {
            if (width < 3)
            {
                width = 3;
            }
            if (height < 3)
            {
                height = 3;
            }

            _maze = new MazeLevelBusinessObject(width, height);

            GenerateWall();

            GenerateGround();

            GenerateWell(maxCountOfFontaine);

            GenerateCoins(chanceOfCoin);

            return _maze;
        }

        private void GenerateWall()
        {
            for (int y = 0; y < _maze.Height; y++)
            {
                for (int x = 0; x < _maze.Width; x++)
                {
                    var wall = new Wall(x, y, _maze);
                    _maze.Cells.Add(wall);
                }
            }
        }

        private void GenerateGround()
        {
            var keyCell = _maze.Cells.First();
            var greenWall = new List<ICell>();
            do
            {
                greenWall.Remove(keyCell);

                var ground = new Ground(keyCell.X, keyCell.Z, _maze);
                _maze.ReplaceCell(ground);

                var nearWall = GetNearCells<Wall>(keyCell);

                greenWall.AddRange(nearWall);
                greenWall = greenWall
                    .Where(wall => GetNearCells<Ground>(wall).Count() <= 1)
                    .ToList();

                keyCell = GetRandom(greenWall);
            } while (greenWall.Any());
        }

        private void GenerateCoins(double chanceOfCoin)
        {
            foreach (var ground in _maze.Cells.OfType<Ground>().ToList())
            {
                if (_random.NextDouble() <= chanceOfCoin)
                {
                    var coin = new Coin(ground.X, ground.Z, _maze);
                    _maze.ReplaceCell(coin);
                }
            }
        }

        private List<T> GetNearCells<T>(ICell keyCell) where T : BaseCell
        {
            return _maze.Cells.Where(
                cell => cell.X == keyCell.X && cell.Z == keyCell.Z - 1
                || cell.X == keyCell.X && cell.Z == keyCell.Z + 1
                || cell.X == keyCell.X + 1 && cell.Z == keyCell.Z
                || cell.X == keyCell.X - 1 && cell.Z == keyCell.Z)
                .OfType<T>()
                .ToList();
        }

        private ICell GetRandom(IEnumerable<ICell> cells)
        {
            if (!cells.Any())
            {
                return null;
            }

            var index = _random.Next(cells.Count());
            return cells.ToList()[index];
        }

        private void GenerateWell(int maxCountOfWell)
        {
            var groundDeadEnd = _maze.Cells.OfType<Ground>()
                .Where(x => GetNearCells<Wall>(x).Count() == 3);

            for (int i = 0; i < Math.Min(maxCountOfWell, groundDeadEnd.Count()); i++)
            {
                var cell = GetRandom(groundDeadEnd);
                var xWell = cell.X;
                var yWell = cell.Z;
                var well = new Fountain(xWell, yWell, _maze);
                _maze.ReplaceCell(well);
            }
        }
    }
}
