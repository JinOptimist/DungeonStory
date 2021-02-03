using Assets.Helpers;
using Assets.Scripts.BaseCellInterfaces;
using Assets.Scripts.SpecialCell;
using Assets.Scripts.SpecialCell.AbilityStuff;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour, IHaveInforamtion, IFinalCell
{
    public string InfoText => "Это стена. Её можно сломать. Но это долго";

    public List<Ability> Abilities { get; set; } = new List<Ability>();
    
    public Ability DefaultAbility { get; set; }

    public void Awake()
    {
        Abilities.Add(new Ability(
           new Action(HitWall),
           "Бум",
           "Бум",
           true));

        Abilities.Add(new Ability(
           new Action(BreakWall),
           "Сломать",
           "Сломать",
           true));
    }

    public void HitWall()
    {
        Debug.Log("We hit the wall");
    }

    public void BreakWall()
    {
        Debug.Log("We BreakWall the wall");
    }
}
