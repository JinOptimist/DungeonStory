using Assets.Helpers;
using Assets.Maze.Cell;
using Assets.MazeGenerationScript.Cell;
using Assets.MazeGenerationScript.GenerationHelpObj;
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
        public const float NearCellChance = .3f;
        public const float NearCrossCellChance = .1f;

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

            GenerateFullWall();

            //GenerateGroundV1(enterStairsX, enterStairsZ);
            GenerateGroundV2(enterStairsX, enterStairsZ);

            GenerateStairs(enterStairsX, enterStairsZ);

            GenerateFountain(maxCountOfFontaine);

            GenerateCoins(chanceOfCoin);

            GeneratePlayer(enterStairsX, enterStairsZ);

            GenerateEnemies(countOfEnemies);

            return _maze;
        }

        private void GenerateStairs(int enterStairsX, int enterStairsZ)
        {
            var stairToUp = new StairToUp(enterStairsX, enterStairsZ, _maze);
            _maze.ReplaceCell(stairToUp);

            var randomGroundDeadEnd = GetDeadEnd().GetRandom();
            if (randomGroundDeadEnd == null)
            {
                randomGroundDeadEnd = _maze.Cells.OfType<Ground>().GetRandom();
            }

            var stairToDown = new StairToDown(randomGroundDeadEnd.X, randomGroundDeadEnd.Z, _maze);
            _maze.ReplaceCell(stairToDown);
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
            //var stairs = _maze.Cells.Single(c => c.X == enterStairsX && c.Z == enterStairsZ);
            //var nearCells = GetNearCells<BaseCell>(stairs)
            //    .Where(x => !(x is Wall))
            //    .GetRandom();
            //var randomGround = nearCells;
            //if (randomGround == null)
            //{
            //    Debug.LogError("There is no ground near stairs");
            //    Debug.LogError($"Stairs [{stairs.X},{stairs.Z}]");
            //}

            //var player = new Player(randomGround.X, randomGround.Z, _maze);
            //_maze.Player = player;

            _maze.Player = new Player(enterStairsX, enterStairsZ, _maze);
        }

        private void GenerateFullWall()
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

        private void GenerateGroundV1(int enterStairsX, int enterStairsZ)
        {
            var keyCell = _maze.Cells.Single(c => c.X == enterStairsX && c.Z == enterStairsZ);
            var greenWall = new List<ICell>();
            do
            {
                greenWall.Remove(keyCell);

                var ground = new Ground(keyCell.X, keyCell.Z, _maze);
                _maze.ReplaceCell(ground);

                var nearWall = GetNear4Cells<Wall>(keyCell);

                greenWall.AddRange(nearWall);
                greenWall = greenWall
                    .Where(wall => GetNear4Cells<Ground>(wall).Count() <= 1)
                    .ToList();

                keyCell = greenWall.GetRandom();
            } while (greenWall.Any());
        }

        private void GenerateGroundV2(int enterStairsX, int enterStairsZ)
        {
            var mazeWithWeight = _maze.Cells.Select(x => new CellWithWeight
            {
                Cell = x,
                Weight = 0
            }).ToList();

            var keyCell = mazeWithWeight.Single(c => c.Cell.X == enterStairsX && c.Cell.Z == enterStairsZ);
            var sumWeight = 0f;
            do
            {
                var groundCellWithWeight = new CellWithWeight
                {
                    Cell = new Ground(keyCell.Cell.X, keyCell.Cell.Z, _maze),
                    Weight = 0
                };
                mazeWithWeight.ReplaceCell(groundCellWithWeight);

                UpdateWeight(mazeWithWeight, keyCell);

                keyCell = mazeWithWeight.GetRandomByWeight();
                sumWeight = mazeWithWeight.Sum(x => x.Weight);
            } while (sumWeight > 0);

            _maze.Cells = mazeWithWeight.Select(x => x.Cell).ToList();
        }

        private void UpdateWeight(List<CellWithWeight> mazeWithWeight, CellWithWeight keyCell)
        {
            foreach (var cellWithWeight in mazeWithWeight.Where(x => x.Cell is Wall))
            {
                if (IsNear(cellWithWeight.Cell, keyCell.Cell))
                {
                    if (!cellWithWeight.IsReadyToBreak)
                    {
                        cellWithWeight.IsReadyToBreak = true;
                        cellWithWeight.Weight = 1f;
                    }

                    cellWithWeight.Weight -= NearCellChance;
                }
                else if (IsNearCross(cellWithWeight.Cell, keyCell.Cell))
                {
                    if (!cellWithWeight.IsReadyToBreak)
                    {
                        continue;
                    }

                    //if (!cellWithWeight.IsReadyToBreak)
                    //{
                    //    cellWithWeight.IsReadyToBreak = true;
                    //    cellWithWeight.Weight = 1f;
                    //}

                    cellWithWeight.Weight -= NearCrossCellChance;
                }

                if (cellWithWeight.Weight < 0)
                {
                    cellWithWeight.Weight = 0;
                }
            }
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

        private List<T> GetNear4Cells<T>(ICell keyCell) where T : BaseCell
        {
            return _maze.Cells.Where(cell => IsNear(cell, keyCell))
                .OfType<T>()
                .ToList();
        }

        private bool IsNear(ICell cellOne, ICell cellTwo)
        {
            return cellOne.X == cellTwo.X && cellOne.Z == cellTwo.Z - 1
                || cellOne.X == cellTwo.X && cellOne.Z == cellTwo.Z + 1
                || cellOne.X == cellTwo.X + 1 && cellOne.Z == cellTwo.Z
                || cellOne.X == cellTwo.X - 1 && cellOne.Z == cellTwo.Z;
        }

        private bool IsNearCross(ICell cellOne, ICell cellTwo)
        {
            return cellOne.X == cellTwo.X + 1 && cellOne.Z == cellTwo.Z + 1
                || cellOne.X == cellTwo.X + 1 && cellOne.Z == cellTwo.Z - 1
                || cellOne.X == cellTwo.X - 1 && cellOne.Z == cellTwo.Z + 1
                || cellOne.X == cellTwo.X - 1 && cellOne.Z == cellTwo.Z - 1;
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

        private void GenerateFountain(int maxCountOfWell)
        {
            var groundDeadEnd = GetDeadEnd();

            for (int i = 0; i < Math.Min(maxCountOfWell, groundDeadEnd.Count()); i++)
            {
                var cell = groundDeadEnd.GetRandom();
                var xWell = cell.X;
                var yWell = cell.Z;
                var well = new Fountain(xWell, yWell, _maze);
                _maze.ReplaceCell(well);
            }
        }

        private IEnumerable<Ground> GetDeadEnd()
        {
            return _maze.Cells.OfType<Ground>()
                .Where(x =>
                    GetNear4Cells<BaseCell>(x).Count() - GetNear4Cells<Wall>(x).Count() == 1);
        }
    }
}
