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

    //Prefab for UI
    public GameObject abilityTemplate;

    //UI
    public GameObject UIInfoBlockMain { get; private set; }

    public GameObject UIInfoCellText { get; private set; }

    public GameObject UIInfoBlockText { get; private set; }

    public GameObject CellActionGroup { get; private set; }

    public List<GameObject> Enemies { get; private set; } = new List<GameObject>();
    public List<GameObject> Landscape { get; private set; } = new List<GameObject>();

    public List<GameObject> BorderWall { get; private set; } = new List<GameObject>();

    private int _currentLevelIndex;
    public List<MazeLevelBusinessObject> Levels { get; private set; } = new List<MazeLevelBusinessObject>();

    //UI
    public const string UIInfoBlockMainName = "UIInfoBlockMain";
    public const string UIInfoBlockTextName = "UIInfoBlockText";
    public const string UIInfoCellTextName = "UIInfoCellText";
    public const string CellActionGroupName = "CellActionGroup";

    //Animation
    public const string IsCubeActive = "IsActive";

    void Start()
    {
        UIInfoBlockMain = GameObject.Find(UIInfoBlockMainName);
        UIInfoCellText = GameObject.Find(UIInfoCellTextName);
        UIInfoBlockText = GameObject.Find(UIInfoBlockTextName);

        CellActionGroup = GameObject.Find(CellActionGroupName);

        UIInfoBlockMain.SetActive(false);
        UIInfoBlockText.SetActive(false);

        _currentLevelIndex = -1;
        GoOneLevelDown(1, 1);
    }

    public void EndTurn()
    {
        foreach (var enemyGameObject in Enemies)
        {
            enemyGameObject.GetComponent<AiEndTurnScript>()?.EndTurn();
        }

        Landscape
            .Select(x => x.GetComponentInChildren<IEndTurn>())
            .Where(x => x != null)
            .ToList()
            .ForEach(x => x.EndTurn());
    }

    public void PickGameObject(GameObject gameObject)
    {
        if (gameObject == null)
        {
            SetInfoText("");
            ShowAbilityForActivecell(null);
            return;
        }

        if (ActiveObject != null)
        {
            //Deactivate old active obj
            ActiveObject.GetComponentInChildren<Animator>()
                .SetBool(IsCubeActive, false);
        }

        var activeEnemy = GetEnemyByGround(gameObject);

        var finalCell = activeEnemy != null
            ? activeEnemy.GetComponentInParent<IFinalCell>()
            : gameObject.GetComponentInParent<IFinalCell>();

        ActiveObject = ((MonoBehaviour)finalCell).gameObject;

        RunActiveAnimation(ActiveObject);

        ShowAbilityForActivecell(finalCell);

        var inforamtion = ActiveObject.GetComponentInChildren<IHaveInforamtion>();
        if (inforamtion != null)
        {
            var infoText = ActiveObject.GetComponentInChildren<IHaveInforamtion>()?.InfoText;
            CoreObjectHelper.GetMainController().SetInfoText(infoText);
        }
        else
        {
            SetInfoText("");
        }
    }

    public void GoOneLevelDown(int x, int z)
    {
        SaveLevelChenges();
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

    private void ClearMaze()
    {
        Enemies.ForEach(Destroy);
        Enemies = new List<GameObject>();

        Landscape.ForEach(Destroy);
        Landscape = new List<GameObject>();

        BorderWall.ForEach(Destroy);
        BorderWall = new List<GameObject>();

        PickGameObject(null);
    }

    private void DrawCurrentMazeLevel()
    {
        ClearMaze();
        var mazeGenerator = CoreObjectHelper.GetMazeGenerator();
        var mazeLevel = Levels[_currentLevelIndex];
        mazeGenerator.DrawMaze(mazeLevel, _currentLevelIndex);
    }

    private void SaveLevelChenges()
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

    public void MoveHeroToCell()
    {
        var cell = ActiveObject.GetComponentInChildren<BaseCellScript>();
        MoveHeroToCell(cell.X, cell.Z);
    }

    public void MoveHeroToCell(int x, int z)
    {
        var hero = CoreObjectHelper.GetHeroGameObject();
        hero.GetComponent<BaseCellScript>().X = x;
        hero.GetComponent<BaseCellScript>().Z = z;
    }

    public void SetInfoText(string infoText)
    {
        if (string.IsNullOrEmpty(infoText))
        {
            UIInfoBlockMain.SetActive(false);
            return;
        }

        UIInfoBlockMain.SetActive(true);
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
        Enemies.Remove(enemy);
        var ground = GetGroundByEnemy(enemy);
        PickGameObject(ground);
        Destroy(enemy);
    }

    public GameObject GetEnemyByGround(GameObject ground)
    {
        return GetEnemyByGround(ground.GetComponentInChildren<BaseCellScript>());
    }

    public GameObject GetEnemyByGround(BaseCellScript cell)
    {
        return GetCell(Enemies, cell);
    }

    public GameObject GetGroundByEnemy(GameObject enemy)
    {
        return GetCell(Landscape, enemy.GetComponentInChildren<BaseCellScript>());
    }

    private GameObject GetCell(List<GameObject> gameObjects, BaseCellScript cell)
    {
        return gameObjects.FirstOrDefault(gameObj =>
        {
            var gameObjCell = gameObj.GetComponentInChildren<BaseCellScript>();
            return gameObjCell.X == cell.X && gameObjCell.Z == cell.Z;
        });
    }
}
