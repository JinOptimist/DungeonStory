using Assets.Helpers;
using Assets.Maze;
using Assets.Scripts.BaseCellInterfaces;
using Assets.Scripts.SpecialCell;
using Assets.Scripts.SpecialCell.CellInterface;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
    public GameObject ActiveObject { get; private set; }

    //Prefab for UI
    public GameObject abilityTemplate;

    //UI
    public GameObject UIInfoBlockPanel { get; private set; }
    public GameObject UIInfoCellText { get; private set; }
    public GameObject UIInfoBlockText { get; private set; }
    public GameObject CellActionGroup { get; private set; }

    private int _currentLevelIndex;
    public List<MazeLevelBusinessObject> Levels { get; private set; } = new List<MazeLevelBusinessObject>();

    //UI
    public const string UIInfoBlockPanelName = "UIInfoBlockPanel";
    public const string UIInfoBlockTextName = "UIInfoBlockText";
    public const string UIInfoCellTextName = "UIInfoCellText";
    public const string CellActionGroupName = "CellActionGroup";

    //Animation
    public const string IsCubeActive = "IsActive";

    void Start()
    {
        UIInfoBlockPanel = GameObject.Find(UIInfoBlockPanelName);
        UIInfoCellText = GameObject.Find(UIInfoCellTextName);
        UIInfoBlockText = GameObject.Find(UIInfoBlockTextName);

        CellActionGroup = GameObject.Find(CellActionGroupName);

        //UIInfoBlockMain.SetActive(false);
        //UIInfoBlockText.SetActive(false);

        _currentLevelIndex = -1;
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
        var mainController = CoreObjectHelper.GetMainController();
        mainController.SaveLevelChenges();
        _currentLevelIndex++;

        var mazeGenerator = CoreObjectHelper.GetMazeGenerator();
        if (_currentLevelIndex >= Levels.Count)
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
        SaveLevelChenges();
        _currentLevelIndex--;
        DrawCurrentMazeLevel();
    }


    public void PickGameObject(GameObject pickedGameObject)
    {
        SetInfoText("");
        if (pickedGameObject == null)
        {
            ShowAbilityForActivecell(null);
            return;
        }

        if (ActiveObject != null)
        {
            //Deactivate old active obj
            ActiveObject.GetComponentInChildren<Animator>()
                .SetBool(IsCubeActive, false);
        }

        var activeEnemy = CoreObjectHelper.GetMazeGenerator().GetEnemyByGround(pickedGameObject);

        var finalCell = activeEnemy != null
            ? activeEnemy.GetComponentInParent<IFinalCell>()
            : pickedGameObject.GetComponentInParent<IFinalCell>();

        ActiveObject = ((MonoBehaviour)finalCell).gameObject;

        RunActiveAnimation(ActiveObject);

        ShowAbilityForActivecell(finalCell);

        var inforamtion = ActiveObject.GetComponentInChildren<IHaveInforamtion>();
        if (inforamtion != null)
        {
            var infoText = ActiveObject.GetComponentInChildren<IHaveInforamtion>()?.InfoText;
            CoreObjectHelper.GetMainController().SetInfoText(infoText);
        }
    }

    
    public void SaveLevelChenges()
    {
        if (_currentLevelIndex < 0)
        {
            return;
        }

        var heroCell = CoreObjectHelper.GetHeroGameObject().GetComponentInChildren<BaseCellScript>();
        Levels[_currentLevelIndex].Player.X = heroCell.X;
        Levels[_currentLevelIndex].Player.Z = heroCell.Z;
    }

    private void RunActiveAnimation(GameObject gameObject)
    {
        ActiveObject
            .GetComponentInChildren<Animator>()
            .SetBool(IsCubeActive, true);
    }

    private void ShowAbilityForActivecell(IFinalCell finalCell)
    {
        foreach (Transform child in CellActionGroup.transform)
        {
            Destroy(child.gameObject);
        }

        for (var i = 0; i < finalCell?.Abilities?.Count; i++)
        {
            var ability = finalCell.Abilities[i];
            var uiAbility = Instantiate(abilityTemplate);
            uiAbility.transform.parent = CellActionGroup.GetComponent<Transform>();

            var rectTransform = uiAbility.GetComponent<RectTransform>();
            var width = rectTransform.rect.width;
            rectTransform.localPosition = new Vector3(width * i, 0, 0);

            uiAbility.GetComponent<AbilityUIClickScript>().Ability = ability;

            uiAbility.GetComponentInChildren<Text>().text = ability.Name;

            if (!ability.Abailable)
            {
                uiAbility.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
            }
        }
    }

    
    public void SetInfoText(string infoText)
    {
        if (string.IsNullOrEmpty(infoText))
        {
            return;
        }

        UIInfoCellText.GetComponent<Text>().text = infoText;
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

    private void DrawCurrentMazeLevel()
    {
        var mazeGenerator = CoreObjectHelper.GetMazeGenerator();
        mazeGenerator.ClearMaze();
        var mazeLevel = Levels[_currentLevelIndex];
        mazeGenerator.DrawMaze(mazeLevel, _currentLevelIndex);
    }
}
