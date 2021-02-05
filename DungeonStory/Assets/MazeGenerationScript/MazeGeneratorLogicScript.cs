using Assets.Helpers;
using Assets.Maze;
using Assets.Maze.Cell;
using Assets.MazeGenerationScript.Cell;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MazeGeneratorLogicScript : MonoBehaviour
{
    public int enterStairsX;
    public int enterStairsZ;

    public int width;
    public int heigth;
    public double cointChance;
    public int fontaineCount;
    public int countOfEnemies;

    public float blockSize;

    //int width, int heigth, int enterStairsX, int enterStairsZ, double cointChance, int fontaineCount, int countOfEnemies
    public void GenerateMaze()
    {
        var mazeBuilder = new MazeBuilder();
        var maze = mazeBuilder.Generate(
            width, 
            heigth, 
            enterStairsX, 
            enterStairsZ, 
            cointChance, 
            fontaineCount, 
            countOfEnemies);
        DrawMaze(maze);
    }

    private void DrawMaze(MazeLevelBusinessObject maze)
    {
        var mainController = CoreObjectHelper.GetMainController();
        foreach (var cell in maze.Cells)
        {
            GameObject gameObject = null;
            if (cell is Wall)
            {
                gameObject = Instantiate(mainController.wallBrickTemplate);
            }

            if (cell is Coin)
            {
                gameObject = Instantiate(mainController.coinTemplate);
            }

            if (cell is Ground)
            {
                gameObject = Instantiate(mainController.groundTemplate);
            }

            if (cell is Fountain)
            {
                gameObject = Instantiate(mainController.fountainTemplate);
            }

            if (cell is StairToUp)
            {
                gameObject = Instantiate(mainController.stairsUpTemplate);
            }

            if (cell is StairToDown)
            {
                gameObject = Instantiate(mainController.stairsDownTemplate);
            }

            CoreObjectHelper.MoveCellToPosition(gameObject, cell.X, cell.Z);

            mainController.Landscape.Add(gameObject);
        }

        mainController.MoveHeroToCell(maze.Player.X, maze.Player.Z);

        foreach (var enemy in maze.Enemies)
        {
            var enemyGameObject = Instantiate(mainController.enemyTemplate);
            CoreObjectHelper.MoveCellToPosition(enemyGameObject, enemy.X, enemy.Z);
            mainController.Enemies.Add(enemyGameObject);
        }

        //Draw border wall
        for (int i = -1; i < width + 1; i++)
        {
            AddBorderWall(i, -1);
            AddBorderWall(i, heigth);
        }

        for (int i = 0; i < heigth; i++)
        {
            AddBorderWall(-1, i);
            AddBorderWall(width, i);
        }
    }

    private void AddBorderWall(int x, int z)
    {
        var mainController = CoreObjectHelper.GetMainController();
        var borderWall = Instantiate(mainController.wallBrickTemplate);
        mainController.BorderWall.Add(borderWall);
        CoreObjectHelper.MoveCellToPosition(borderWall, x, z);
    }
}
