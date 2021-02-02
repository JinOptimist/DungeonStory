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

    public float blockSize;

    // Start is called before the first frame update
    void Start()
    {
        var mazeBuilder = new MazeBuilder();
        var maze = mazeBuilder.Generate(width, heigth, enterStairsX, enterStairsZ, cointChance, fontaineCount);
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

            if (cell is Ground || cell is Player || cell is Enemy)
            {
                gameObject = Instantiate(mainController.groundTemplate);
            }

            if (cell is Fountain)
            {
                gameObject = Instantiate(mainController.fountainTemplate);
            }

            CoreObjectHelper.MoveCellToPosition(gameObject, cell.X, cell.Z);
        }

        var player = maze.Player;
        CoreObjectHelper.MoveCellToPosition(Instantiate(mainController.heroTemplate), player.X, player.Z);

        maze.Enemies.ForEach(enemy =>
            CoreObjectHelper.MoveCellToPosition(Instantiate(mainController.enemyTemplate), enemy.X, enemy.Z)
        );

        //Draw border wall
        for (int i = -1; i < width + 1; i++)
        {
            CoreObjectHelper.MoveCellToPosition(Instantiate(mainController.wallBrickTemplate), i, -1);
            CoreObjectHelper.MoveCellToPosition(Instantiate(mainController.wallBrickTemplate), i, heigth);
        }

        for (int i = 0; i < heigth; i++)
        {
            CoreObjectHelper.MoveCellToPosition(Instantiate(mainController.wallBrickTemplate), -1, i);
            CoreObjectHelper.MoveCellToPosition(Instantiate(mainController.wallBrickTemplate), width, i);
        }
    }


}
