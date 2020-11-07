using Assets.Maze;
using Assets.Maze.Cell;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MazeGeneratorLogicScript : MonoBehaviour
{
    public GameObject wallBrickTemplate;

    public int width;
    public int heigth;
    public double cointChance;
    public int fontaineCount;

    // Start is called before the first frame update
    void Start()
    {
        var mazeBuilder = new MazeBuilder();
        var maze = mazeBuilder.Generate(width, heigth, cointChance, fontaineCount);

        foreach (var cell in maze.CellsWithPlayer.OfType<Wall>())
        {
            var wallGameObject = Instantiate(wallBrickTemplate);
            wallGameObject.GetComponent<Transform>().position
                = new Vector3(cell.X, 1, cell.Z);
        }

        for (int i = -1; i < width + 1; i++)
        {
            var wallGameObjectBottom = Instantiate(wallBrickTemplate);
            wallGameObjectBottom.GetComponent<Transform>().position = new Vector3(i, 1, -1);

            var wallGameObjectTop = Instantiate(wallBrickTemplate);
            wallGameObjectTop.GetComponent<Transform>().position = new Vector3(i, 1, heigth);
        }

        for (int i = 0; i < heigth; i++)
        {
            var wallGameObjectLeft= Instantiate(wallBrickTemplate);
            wallGameObjectLeft.GetComponent<Transform>().position = new Vector3(-1, 1, i);

            var wallGameObjectRight= Instantiate(wallBrickTemplate);
            wallGameObjectRight.GetComponent<Transform>().position = new Vector3(width, 1, i);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
