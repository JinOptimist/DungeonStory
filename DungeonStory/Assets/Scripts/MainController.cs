using Assets.Helpers;
using Assets.Scripts.BaseCellInterfaces;
using Assets.Scripts.SpecialCell;
using System.Collections;
using System.Collections.Generic;
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

    //Prefab for UI
    public GameObject abilityTemplate;

    //UI
    public GameObject UIInfoBlockMain { get; private set; }
    public GameObject UIInfoCellText { get; private set; }
    public GameObject UIInfoBlockText { get; private set; }
    public GameObject CellActionGroup { get; private set; }

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
    }

    public void ActivateGameObject(GameObject gameObject)
    {
        if (ActiveObject != null)
        {
            //Deactivate old active obj
            ActiveObject.GetComponentInChildren<Animator>()
                .SetBool(IsCubeActive, false);
        }

        var finalCell = gameObject.GetComponentInParent<IFinalCell>();
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

        for (var i = 0; i < finalCell.Abilities.Count; i++)
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

    public GameObject ReplaceToGround(GameObject gameObject)
    {
        var baseCell = gameObject.GetComponentInChildren<BaseCellScript>();

        var finalCell = ((MonoBehaviour)gameObject.GetComponentInParent<IFinalCell>()).gameObject;
        Destroy(finalCell);

        var ground = Instantiate(groundTemplate);
        CoreObjectHelper.MoveCellToPosition(ground, baseCell.X, baseCell.Z);
        return ground;
    }

    public void MoveHeroToCell(GameObject gameObject)
    {
        var hero = CoreObjectHelper.GetHeroGameObject();
        var cell = gameObject.GetComponentInChildren<BaseCellScript>();
        CoreObjectHelper.MoveCellToPosition(hero, cell.X, cell.Z);
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
}
