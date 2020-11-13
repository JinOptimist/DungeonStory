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

    public int width;
    public int heigth;
    public double cointChance;
    public int fontaineCount;

    public float blockSize;

    // Start is called before the first frame update
    void Start()
    {
        var mazeBuilder = new MazeBuilder();
        var maze = mazeBuilder.Generate(width, heigth, cointChance, fontaineCount);

        DrawMaze(maze);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void DrawMaze(MazeLevelBusinessObject maze)
    {
        foreach (var cell in maze.CellsWithPlayer)
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

            TrasformCell(gameObject, cell.X, cell.Z);
        }

        //foreach (var cell in maze.CellsWithPlayer.OfType<Wall>())
        //{
        //    var wallGameObject = Instantiate(wallBrickTemplate);
        //    wallGameObject.GetComponent<Transform>().position
        //        = new Vector3(cell.X, 1, cell.Z);
        //}

        //Draw border wall
        for (int i = -1; i < width + 1; i++)
        {
            TrasformCell(Instantiate(wallBrickTemplate), i, -1);
            TrasformCell(Instantiate(wallBrickTemplate), i, heigth);
        }

        for (int i = 0; i < heigth; i++)
        {
            TrasformCell(Instantiate(wallBrickTemplate), -1, i);
            TrasformCell(Instantiate(wallBrickTemplate), width, i);
        }
    }

    private void TrasformCell(GameObject gameObject, int x, int z)
    {
        if (gameObject != null)
        {
            var cellTransform = gameObject.GetComponent<Transform>();
            cellTransform.position = new Vector3(x * blockSize, 1, z * blockSize);
            cellTransform.localScale = new Vector3(blockSize, 1, blockSize);
        }
    }
}
