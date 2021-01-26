using Assets.Helpers;
using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerClickAndActivate : MonoBehaviour, ITriggerClick
{
    public void OnTriggerClick()
    {
        var mainController = CoreObjectHelper.GetMainController();
        mainController.ActivateGameObject(gameObject);
    }
}
