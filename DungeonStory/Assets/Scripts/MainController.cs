using Assets.Helpers;
using Assets.Scripts.BaseCellInterfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
    public GameObject ActiveObject { get; private set; }

    public GameObject InfoBlockUIMain { get; private set; }
    public GameObject InfoBlockUIText { get; private set; }

    public const string IsCubeActive = "IsActive";

    public const string InfoBlockMainName = "InfoBlock";
    public const string InfoBlockTextName = "CellInfoUIText";

    void Start()
    {
        InfoBlockUIMain = GameObject.Find(InfoBlockMainName);
        InfoBlockUIText = GameObject.Find(InfoBlockTextName);

        InfoBlockUIMain.SetActive(false);
    }

    public void ActivateGameObject(GameObject gameObject)
    {
        if (ActiveObject != null)
        {
            //Deactivate old active obj
            ActiveObject.GetComponentInParent<Animator>()
                .SetBool(IsCubeActive, false);
        }

        ActiveObject = gameObject;
        ActiveObject
            .GetComponentInParent<Animator>()
            .SetBool(IsCubeActive, true);

        //HideButton();
        var inforamtion = ActiveObject.GetComponentInParent<IHaveInforamtion>();
        if (inforamtion != null)
        {
            ActiveObject.GetComponentInParent<IHaveInforamtion>()?.ShowButtonForAction();
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

        InfoBlockUIText.GetComponent<Text>().text = infoText;
    }
}
