﻿using Assets.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnWallClick : MonoBehaviour
{
    void OnMouseDown()
    {
        var mainController = CoreObjectHelper.GetMainController();
        mainController.ActivateGameObject(gameObject);
    }
}