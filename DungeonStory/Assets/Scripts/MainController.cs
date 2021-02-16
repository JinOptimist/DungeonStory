using Assets.Helpers;
using Assets.Maze;
using Assets.Scripts.BaseCellInterfaces;
using Assets.Scripts.SpecialCell;
using Assets.Scripts.SpecialCell.CellInterface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
    public GameObject ActiveObject { get; private set; }

    public int CurrentLevelIndex { get; private set; }

    public List<MazeLevelBusinessObject> Levels { get; private set; } = new List<MazeLevelBusinessObject>();
    
    //Animation
    public const string IsCubeActive = "IsActive";

    void Start()
    {
        CurrentLevelIndex = -1;
        GoOneLevelDown(1, 1);
    }

    public void EndTurn()
    {
        var mazeGenerator = CoreObjectHelper.GetMazeGenerator();
        foreach (var enemyGameObject in mazeGenerator.Enemies)
        {
            enemyGameObject.GetComponent<AiEndTurnScript>()?.EndTurn();
        }

        mazeGenerator.Landscape
            .Select(x => x.GetComponentInChildren<IEndTurn>())
            .Where(x => x != null)
            .ToList()
            .ForEach(x => x.EndTurn());
    }

    public void GoOneLevelDown(int x, int z)
    {
        var mazeGerate = CoreObjectHelper.GetMazeGenerator();
        mazeGerate.SaveLevelChanges();
        CurrentLevelIndex++;

        var mazeGenerator = CoreObjectHelper.GetMazeGenerator();
        if (CurrentLevelIndex >= Levels.Count)
        {
            mazeGenerator.enterStairsX = x;
            mazeGenerator.enterStairsZ = z;
            mazeGenerator.width++;
            mazeGenerator.height++;
            var mazeLevel = mazeGenerator.GenerateMaze();
            Levels.Add(mazeLevel);
        }

        DrawCurrentMazeLevel();
    }

    public void GoOneLevelUp()
    {
        var mazeGenerator = CoreObjectHelper.GetMazeGenerator();
        mazeGenerator.SaveLevelChanges();
        CurrentLevelIndex--;
        DrawCurrentMazeLevel();
    }

    public void PickGameObject(GameObject pickedGameObject)
    {
        var uiController = CoreObjectHelper.GetUiController();
        uiController.SetInfoText("");
        if (pickedGameObject == null)
        {
            uiController.ShowAbilityForActivecell(null);
            return;
        }

        if (ActiveObject != null)
        {
            //Deactivate old active obj
            ActiveObject.GetComponentInChildren<Animator>()
                .SetBool(IsCubeActive, false);
        }

        var activeEnemy = CoreObjectHelper.GetMazeGenerator().GetEnemy(pickedGameObject);

        var finalCell = activeEnemy != null
            ? activeEnemy.GetComponentInParent<IFinalCell>()
            : pickedGameObject.GetComponentInParent<IFinalCell>();

        ActiveObject = ((MonoBehaviour)finalCell).gameObject;

        RunActiveAnimation(ActiveObject);

        uiController.ShowAbilityForActivecell(finalCell);

        var inforamtion = ActiveObject.GetComponentInChildren<IHaveInforamtion>();
        if (inforamtion != null)
        {
            var infoText = ActiveObject.GetComponentInChildren<IHaveInforamtion>()?.InfoText;
            uiController.SetInfoText(infoText);
        }
    }
    
    

    public void DefaultAction(GameObject gameObject)
    {
        PickGameObject(gameObject);
        ActiveObject
            .GetComponentInParent<IFinalCell>()
            ?.DefaultAbility
            ?.RunAction();
    }

    public void KillEnemy(GameObject enemy)
    {
        var mazeGenerator = CoreObjectHelper.GetMazeGenerator();
        mazeGenerator.Enemies.Remove(enemy);
        var ground = mazeGenerator.GetGroundByEnemy(enemy);
        PickGameObject(ground);
        Destroy(enemy);
    }

    private void RunActiveAnimation(GameObject gameObject)
    {
        ActiveObject
            .GetComponentInChildren<Animator>()
            .SetBool(IsCubeActive, true);
    }

    private void DrawCurrentMazeLevel()
    {
        var mazeGenerator = CoreObjectHelper.GetMazeGenerator();
        mazeGenerator.ClearMaze();
        var mazeLevel = Levels[CurrentLevelIndex];
        var startDraw = DateTime.Now;
        mazeGenerator.DrawMaze(mazeLevel, CurrentLevelIndex);
        var endDraw = DateTime.Now;
        Debug.Log($"DRAW TIME {(endDraw - startDraw).TotalSeconds} s");
    }
}
