using Assets.Helpers;
using Assets.Scripts.BaseCellInterfaces;
using Assets.Scripts.SpecialCell;
using Assets.Scripts.SpecialCell.AbilityStuff;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairUpScript : MonoBehaviour, IHaveInforamtion, IFinalCell
{
    public string InfoText => "Лестница вверх. Путь на выход";

    public List<Ability> Abilities { get; set; } = new List<Ability>();

    public Ability DefaultAbility { get; set; }

    public void Awake()
    {
        var step = new Ability(
           new Action(Step),
           "Шануть",
           "Шануть",
           true);
        DefaultAbility = step;
        Abilities.Add(step);

        Abilities.Add(new Ability(
           new Action(GoUp),
           "Подняться",
           "Подняться",
           true));
    }

    public void GoUp()
    {
        CoreObjectHelper.GetMainController().GoOneLevelUp();
    }

    public void Step()
    {
        CoreObjectHelper.GetMainController().MoveHeroToCell();
    }

}
