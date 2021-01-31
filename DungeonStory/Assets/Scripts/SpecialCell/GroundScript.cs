using Assets.Scripts.BaseCellInterfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.SpecialCell;
using Assets.Scripts.SpecialCell.AbilityStuff;
using System;
using Assets.Helpers;

public class GroundScript : MonoBehaviour, IHaveInforamtion, IFinalCell
{
    public string InfoText => "Просто земля. На неё можно наступить";

    public List<Ability> Abilities { get; set; } = new List<Ability>();

    public void Awake()
    {
        Abilities.Add(new Ability(
           new Action(StepToGround),
           "Шагнуть",
           "Шагнуть",
           true));
    }

    public void StepToGround()
    {
        var mainController = CoreObjectHelper.GetMainController();
        var activeObject = mainController.ActiveObject;
        mainController.MoveHeroToCell(activeObject);
    }
}
