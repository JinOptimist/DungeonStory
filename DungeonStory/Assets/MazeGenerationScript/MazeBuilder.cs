using Assets.Helpers;
using Assets.Maze.Cell;
using Assets.MazeGenerationScript.Cell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Maze
{
    public class MazeBuilder
    {
        private MazeLevelBusinessObject _maze;
        private System.Random _random = new System.Random();

        public MazeLevelBusinessObject Generate(int width, int height,
            int enterStairsX, int enterStairsZ,
            double chanceOfCoin = 0.1, int maxCountOfFontaine = 3, int countOfEnemies = 3)
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

            GenerateGround(enterStairsX, enterStairsZ);

            GenerateWell(maxCountOfFontaine);

            GenerateCoins(chanceOfCoin);

            GeneratePlayer(enterStairsX, enterStairsZ);

            GenerateEnemies(countOfEnemies);

            return _maze;
        }

        /// <summary>
        /// Run only after generate Player
        /// </summary>
        /// <param name="countOfEnemies"></param>
        private void GenerateEnemies(int countOfEnemies)
        {
            _maze.Enemies = new List<Enemy>();
            var grounds = _maze.Cells
                .OfType<Ground>()
                .Where(x => x.X != _maze.Player.X || x.Z != _maze.Player.Z)
                .ToList();
            if (countOfEnemies > grounds.Count)
            {
                countOfEnemies = grounds.Count;
            }

            for (int i = 0; i < countOfEnemies; i++)
            {
                var ground = grounds.GetRandom();
                var enemy = new Enemy(ground.X, ground.Z, _maze);
                grounds.Remove(ground as Ground);
                _maze.Enemies.Add(enemy);
            }
        }

        private void GeneratePlayer(int enterStairsX, int enterStairsZ)
        {
            var stairs = _maze.Cells.Single(c => c.X == enterStairsX && c.Z == enterStairsZ);
            var nearCells = GetNearCells<BaseCell>(stairs)
                .Where(x => !(x is Wall))
                .GetRandom();
            var randomGround = nearCells;
            if (randomGround == null)
            {
                Debug.LogError("There is no ground near stairs");
                Debug.LogError($"Stairs [{stairs.X},{stairs.Z}]");
            }
            var player = new Player(randomGround.X, randomGround.Z, _maze);
            _maze.Player = player;
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

        private void GenerateGround(int enterStairsX, int enterStairsZ)
        {
            var keyCell = _maze.Cells.Single(c => c.X == enterStairsX && c.Z == enterStairsZ);
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

                keyCell = greenWall.GetRandom();
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

        //private ICell GetRandom(IEnumerable<ICell> cells)
        //{
        //    if (!cells.Any())
        //    {
        //        return null;
        //    }

        //    var index = _random.Next(cells.Count());
        //    return cells.ToList()[index];
        //}

        private void GenerateWell(int maxCountOfWell)
        {
            var groundDeadEnd = _maze.Cells.OfType<Ground>()
                .Where(x => GetNearCells<Wall>(x).Count() == 3);

            for (int i = 0; i < Math.Min(maxCountOfWell, groundDeadEnd.Count()); i++)
            {
                var cell = groundDeadEnd.GetRandom();
                var xWell = cell.X;
                var yWell = cell.Z;
                var well = new Fountain(xWell, yWell, _maze);
                _maze.ReplaceCell(well);
            }
        }
    }
}
