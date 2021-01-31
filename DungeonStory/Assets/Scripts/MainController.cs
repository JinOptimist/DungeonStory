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

    public GameObject InfoBlockUIMain { get; private set; }
    public GameObject CellInfoUIText { get; private set; }
    public GameObject InfoTextBlock { get; private set; }

    public const string IsCubeActive = "IsActive";

    public const string InfoBlockMainName = "InfoBlockUIMain";
    public const string CellInfoUITextName = "CellInfoUIText";

    public const string InfoTextBlockName = "InfoTextBlock";

    void Start()
    {
        InfoBlockUIMain = GameObject.Find(InfoBlockMainName);
        CellInfoUIText = GameObject.Find(CellInfoUITextName);

        InfoTextBlock = GameObject.Find(InfoTextBlockName);

        InfoBlockUIMain.SetActive(false);
        InfoTextBlock.SetActive(false);
    }

    public void ActivateGameObject(GameObject gameObject)
    {
        if (ActiveObject != null)
        {
            //Deactivate old active obj
            ActiveObject.GetComponentInChildren<Animator>()
                .SetBool(IsCubeActive, false);
        }

        ActiveObject = ((MonoBehaviour)gameObject.GetComponentInParent<IFinalCell>()).gameObject;
        ActiveObject
            .GetComponentInChildren<Animator>()
            .SetBool(IsCubeActive, true);

        //HideButton();
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
            InfoBlockUIMain.SetActive(false);
            return;
        }

        InfoBlockUIMain.SetActive(true);
        CellInfoUIText.GetComponent<Text>().text = infoText;
    }
}
