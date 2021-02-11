using Assets.Scripts.SpecialCell;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    //Prefab for UI
    public GameObject abilityTemplate;

    //UI
    public GameObject UIInfoBlockPanel { get; private set; }
    public GameObject UIInfoCellText { get; private set; }
    public GameObject UIInfoBlockText { get; private set; }
    public GameObject CellActionGroup { get; private set; }

    //UI
    public const string UIInfoBlockPanelName = "UIInfoBlockPanel";
    public const string UIInfoBlockTextName = "UIInfoBlockText";
    public const string UIInfoCellTextName = "UIInfoCellText";
    public const string CellActionGroupName = "CellActionGroup";

    // Start is called before the first frame update
    void Start()
    {
        UIInfoBlockPanel = GameObject.Find(UIInfoBlockPanelName);
        UIInfoCellText = GameObject.Find(UIInfoCellTextName);
        UIInfoBlockText = GameObject.Find(UIInfoBlockTextName);

        CellActionGroup = GameObject.Find(CellActionGroupName);

        //UIInfoBlockMain.SetActive(false);
        //UIInfoBlockText.SetActive(false);
    }

    public void SetInfoText(string infoText)
    {
        //if (string.IsNullOrEmpty(infoText))
        //{
        //    return;
        //}
        UIInfoCellText.GetComponent<Text>().text = infoText;
    }

    public void ShowAbilityForActivecell(IFinalCell finalCell)
    {
        foreach (Transform child in CellActionGroup.transform)
        {
            Destroy(child.gameObject);
        }

        for (var i = 0; i < finalCell?.Abilities?.Count; i++)
        {
            var ability = finalCell.Abilities[i];
            var uiAbility = Instantiate(abilityTemplate);
            //uiAbility.transform.parent = CellActionGroup.GetComponent<Transform>();
            uiAbility.transform.SetParent(CellActionGroup.GetComponent<Transform>());

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
}
