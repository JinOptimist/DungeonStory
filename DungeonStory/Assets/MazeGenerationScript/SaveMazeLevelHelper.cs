using Assets.Helpers;
using Assets.Maze;
using Assets.Maze.Cell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.MazeGenerationScript
{
    public class SaveMazeLevelHelper
    {
        public MazeLevelBusinessObject Save(MazeGeneratorLogicScript currentLevel)
        {
            var main = CoreObjectHelper.GetMainController();
            var savedLevel = main.Levels[main.CurrentLevelIndex];
            var heroCell = CoreObjectHelper.GetHeroGameObject().GetComponentInChildren<BaseCellScript>();
            savedLevel.Player.X = heroCell.X;
            savedLevel.Player.Z = heroCell.Z;

            UpdateLandscape(savedLevel, currentLevel);

            //main.Levels[main.CurrentLevelIndex] = savedLevel;

            return savedLevel;
        }

        private void UpdateLandscape(MazeLevelBusinessObject savedLevel, MazeGeneratorLogicScript currentLevel)
        {
            foreach (var cellGameObject in currentLevel.Landscape)
            {
                ICell cell = null;

                var wallGameObject = cellGameObject.GetComponent<WallScript>();
                if (wallGameObject != null)
                {
                    cell = new Wall(0, 0, null);
                }

                var coinGameObj = cellGameObject.GetComponent<CoinScript>();
                if (coinGameObj != null)
                {
                    cell = new Coin(0, 0, null);
                }

                var groundGameObj = cellGameObject.GetComponent<GroundScript>();
                if (groundGameObj != null)
                {
                    cell = new Ground(0, 0, null);
                }

                var stairDownGameObj = cellGameObject.GetComponent<StairDownScript>();
                if (stairDownGameObj != null)
                {
                    cell = new StairToDown(0, 0, null);
                }

                var stairUpGameObj = cellGameObject.GetComponent<StairUpScript>();
                if (stairUpGameObj != null)
                {
                    cell = new StairToUp(0, 0, null);
                }

                var fountainGameObj = cellGameObject.GetComponent<FountainScript>();
                if (fountainGameObj != null)
                {
                    cell = new Fountain(0, 0, null);
                }

                if (cell == null)
                {
                    var a = 12223;
                }

                var baseCell = cellGameObject.GetComponentInChildren<BaseCellScript>();
                cell.X = baseCell.X;
                cell.Z = baseCell.Z;
                savedLevel.ReplaceCell(cell);
            }
        }
    }
}
