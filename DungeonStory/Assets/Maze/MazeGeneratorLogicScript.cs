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
        foreach (var cell in maze.CellsWithPlayer)
        {
            GameObject gameObject = null;
            var isResize = false;
            if (cell is Wall)
            {
                gameObject = Instantiate(wallBrickTemplate);
                isResize = true;
            }
            if (cell is Coin)
            {
                gameObject = Instantiate(coinTemplate);
            }
            if (cell is Player)
            {
                gameObject = Instantiate(heroTemplate);
                isResize = true;
            }

            TrasformCell(gameObject, cell.X, cell.Z, isResize);
        }

        //Draw border wall
        for (int i = -1; i < width + 1; i++)
        {
            TrasformCell(Instantiate(wallBrickTemplate), i, -1, true);
            TrasformCell(Instantiate(wallBrickTemplate), i, heigth, true);
        }

        for (int i = 0; i < heigth; i++)
        {
            TrasformCell(Instantiate(wallBrickTemplate), -1, i, true);
            TrasformCell(Instantiate(wallBrickTemplate), width, i, true);
        }
    }

    private void TrasformCell(GameObject gameObject, int x, int z, bool resizeBlock = false)
    {
        if (gameObject != null)
        {
            var cellTransform = gameObject.GetComponent<Transform>();
            cellTransform.position = new Vector3(x * blockSize, 0, z * blockSize);

            var scale = resizeBlock ? blockSize : 1;
            cellTransform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
