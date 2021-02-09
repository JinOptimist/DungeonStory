using Assets.Helpers;
using Assets.Scripts.BaseCellInterfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseCellScript : MonoBehaviour, IHovered, IClicked
{
    public int X;
    public int Z;

    public const string IsHover = "IsHover";

    public void OnMouseIn()
    {
        gameObject.GetComponentInParent<Animator>().SetBool(IsHover, true);
    }

    public void OnMouseOut()
    {
        gameObject.GetComponentInParent<Animator>().SetBool(IsHover, false);
    }

    public void OnLeftMouseClick()
    {
        CoreObjectHelper.GetMainController().PickGameObject(gameObject);
    }

    public void OnRightMouseClick()
    {
        CoreObjectHelper.GetMainController().DefaultAction(gameObject);
    }
}
