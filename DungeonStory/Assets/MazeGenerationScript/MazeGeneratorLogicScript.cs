using Assets.Helpers;
using Assets.Maze;
using Assets.Maze.Cell;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MazeGeneratorLogicScript : MonoBehaviour
{
    public GameObject wallBrickTemplate;
    public GameObject coinTemplate;
    public GameObject heroTemplate;
    public GameObject groundTemplate;
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

    // Update is called once per frame
    void Update()
    {

    }

    private void DrawMaze(MazeLevelBusinessObject maze)
    {
        foreach (var cell in maze.Cells)
        {
            GameObject gameObject = null;
            if (cell is Wall)
            {
                gameObject = Instantiate(wallBrickTemplate);
            }
            if (cell is Coin)
            {
                gameObject = Instantiate(coinTemplate);
            }

            if (cell is Ground || cell is Player)
            {
                gameObject = Instantiate(groundTemplate);
            }

            CoreObjectHelper.MoveCellToPosition(gameObject, cell.X, cell.Z);
        }

        var player = maze.Player;
        CoreObjectHelper.MoveCellToPosition(Instantiate(heroTemplate), player.X, player.Z);

        //Draw border wall
        for (int i = -1; i < width + 1; i++)
        {
            CoreObjectHelper.MoveCellToPosition(Instantiate(wallBrickTemplate), i, -1);
            CoreObjectHelper.MoveCellToPosition(Instantiate(wallBrickTemplate), i, heigth);
        }

        for (int i = 0; i < heigth; i++)
        {
            CoreObjectHelper.MoveCellToPosition(Instantiate(wallBrickTemplate), -1, i);
            CoreObjectHelper.MoveCellToPosition(Instantiate(wallBrickTemplate), width, i);
        }
    }


}
