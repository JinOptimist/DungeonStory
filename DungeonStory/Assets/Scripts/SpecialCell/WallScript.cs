using Assets.Helpers;
using Assets.Scripts.BaseCellInterfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour, IHaveInforamtion
{
    public void ShowButtonForAction()
    {
        CoreObjectHelper.GetMainController().SetInfoText("Это стена. Её можно сломать. Но это долго");
    }
}
