using Assets.Helpers;
using Assets.Scripts.BaseCellInterfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour, IHaveInforamtion
{
    public void ShowButtonForAction()
    {
        CoreObjectHelper.GetMainController().SetInfoText("Это монетка. Её можно подобрать");
    }
}
