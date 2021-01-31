using Assets.Scripts.BaseCellInterfaces;
using Assets.Scripts.SpecialCell;
using Assets.Scripts.SpecialCell.AbilityStuff;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FountainScript : MonoBehaviour, IHaveInforamtion, IFinalCell
{
    public string InfoText => "Это фонтан. Тут можно попить воды";

    public List<Ability> Abilities { get; set; } = new List<Ability>();

    public void Awake()
    {
        Abilities.Add(new Ability(
           new Action(HitWall),
           "Пить",
           "Пить",
           true));
    }

    public void HitWall()
    {
        Debug.Log("We drink from a fontain");
    }
}
