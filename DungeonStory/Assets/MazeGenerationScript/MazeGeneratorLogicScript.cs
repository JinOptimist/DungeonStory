using Assets.Helpers;
using Assets.Maze;
using Assets.Maze.Cell;
using Assets.MazeGenerationScript;
using Assets.MazeGenerationScript.Cell;
using Assets.Scripts.SpecialCell;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MazeGeneratorLogicScript : MonoBehaviour
{
    public int enterStairsX;
    public int enterStairsZ;

    public int width;
    public int height;
    public double cointChance;
    public int fontaineCount;
    public int countOfEnemies;

    public float blockSize;

    //Prefab for cell
    public GameObject wallBrickTemplate;
    public GameObject coinTemplate;
    public GameObject heroTemplate;
    public GameObject groundTemplate;
    public GameObject fountainTemplate;
    public GameObject stairsUpTemplate;
    public GameObject stairsDownTemplate;

    public GameObject enemyTemplate;
    public GameObject towerTemplate;

    public List<GameObject> Enemies { get; private set; } = new List<GameObject>();
    public List<GameObject> Landscape { get; private set; } = new List<GameObject>();
    public List<GameObject> BorderWall { get; private set; } = new List<GameObject>();

    //int width, int heigth, int enterStairsX, int enterStairsZ, double cointChance, int fontaineCount, int countOfEnemies
    public MazeLevelBusinessObject GenerateMaze()
    {
        var mazeBuilder = new MazeBuilder();
        var startMazeGenerate = DateTime.Now;
        var maze = mazeBuilder.Generate(
            width,
            height,
            enterStairsX,
            enterStairsZ,
            cointChance,
            fontaineCount,
            countOfEnemies);
        var endMazeGenerate = DateTime.Now;
        Debug.Log($"GENERATION TIME {(endMazeGenerate - startMazeGenerate).TotalSeconds} s");
        return maze;
    }

    public void DrawMaze(MazeLevelBusinessObject maze, int currentLevelIndex)
    {
        var mainController = CoreObjectHelper.GetMainController();
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

            if (cell is Ground)
            {
                gameObject = Instantiate(groundTemplate);
            }

            if (cell is Fountain)
            {
                gameObject = Instantiate(fountainTemplate);
            }

            if (cell is StairToUp)
            {
                gameObject = Instantiate(stairsUpTemplate);
            }

            if (cell is StairToDown)
            {
                gameObject = Instantiate(stairsDownTemplate);
            }

            CoreObjectHelper.MoveCellToPosition(gameObject, cell.X, cell.Z);

            Landscape.Add(gameObject);
        }

        CoreObjectHelper.GetHeroMoveScript().MoveHeroToCell(maze.Player.X, maze.Player.Z);

        foreach (var enemy in maze.Enemies)
        {
            var enemyGameObject = Instantiate(enemyTemplate);
            CoreObjectHelper.MoveCellToPosition(enemyGameObject, enemy.X, enemy.Z);
            Enemies.Add(enemyGameObject);
        }

        //Draw border wall
        for (int i = -1; i < maze.Width + 1; i++)
        {
            AddBorderWall(i, -1);
            AddBorderWall(i, maze.Height);
        }

        for (int i = 0; i < maze.Height; i++)
        {
            AddBorderWall(-1, i);
            AddBorderWall(maze.Width, i);
        }

        CoreObjectHelper.GetLightScript().SetBrightnessByLevel(currentLevelIndex);
    }

    public void SaveLevelChanges()
    {
        var main = CoreObjectHelper.GetMainController();
        var level = main.CurrentLevelIndex;
        if (level < 0)
        {
            return;
        }
        
        main.Levels[level] = new SaveMazeLevelHelper().Save(this);
    }

    public GameObject ReplaceToGround(GameObject from)
    {
        var ground = Instantiate(groundTemplate);
        return ReplaceCell(from, ground);
    }

    public GameObject ReplaceToTower(GameObject from)
    {
        var tower = Instantiate(towerTemplate);
        return ReplaceCell(from, tower);
    }

    public GameObject ReplaceCell(GameObject from, GameObject to)
    {
        Landscape.Remove(from);

        var baseCell = from.GetComponentInChildren<BaseCellScript>();

        var finalCell = ((MonoBehaviour)from.GetComponentInParent<IFinalCell>()).gameObject;
        Destroy(finalCell);

        CoreObjectHelper.MoveCellToPosition(to, baseCell.X, baseCell.Z);

        Landscape.Add(to);

        return to;
    }

    public GameObject GetEnemy(GameObject ground)
    {
        return FindCell(Enemies, ground.GetComponentInChildren<BaseCellScript>());
    }

    public GameObject GetEnemy(BaseCellScript cell)
    {
        return FindCell(Enemies, cell);
    }

    public GameObject GetGroundByEnemy(GameObject enemy)
    {
        return FindCell(Landscape, enemy.GetComponentInChildren<BaseCellScript>());
    }

    public GameObject GetEnemy(int x, int z)
    {
        return FindCell(Enemies, x, z);
    }

    public void ClearMaze()
    {

        Enemies.ForEach(Destroy);
        Enemies.Clear();

        Landscape.ForEach(Destroy);
        Landscape.Clear();

        BorderWall.ForEach(Destroy);
        BorderWall.Clear();

        CoreObjectHelper.GetMainController().PickGameObject(null);
    }

    public List<GameObject> GetNearEnemy(BaseCellScript cell)
    {
        var x = cell.X;
        var z = cell.Z;
        var nearEnemies = new List<GameObject>() {
            GetEnemy(x + 1, z),
            GetEnemy(x - 1, z),
            GetEnemy(x, z + 1),
            GetEnemy(x, z - 1),

            GetEnemy(x + 1, z - 1),
            GetEnemy(x + 1, z + 1),
            GetEnemy(x - 1, z + 1),
            GetEnemy(x - 1, z - 1),
        };
        return nearEnemies.Where(enemy => enemy != null).ToList();
    }

    private void AddBorderWall(int x, int z)
    {
        var borderWall = Instantiate(wallBrickTemplate);
        BorderWall.Add(borderWall);
        CoreObjectHelper.MoveCellToPosition(borderWall, x, z);
    }

    private GameObject FindCell(List<GameObject> gameObjects, BaseCellScript cell)
    {
        return FindCell(gameObjects, cell.X, cell.Z);
    }

    private GameObject FindCell(List<GameObject> gameObjects, int x, int z)
    {
        return gameObjects.FirstOrDefault(gameObj =>
        {
            var gameObjCell = gameObj.GetComponentInChildren<BaseCellScript>();
            return gameObjCell.X == x && gameObjCell.Z == z;
        });
    }
}
