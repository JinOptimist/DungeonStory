using Assets.Scripts.BaseCellInterfaces;
using Assets.Scripts.SpecialCell;
using Assets.Scripts.SpecialCell.AbilityStuff;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveScript : MonoBehaviour, IHaveInforamtion, IFinalCell
{
    public string InfoText => "Это противник. Бей или беги";

    public List<Ability> Abilities { get; set; } = new List<Ability>();

    public Ability DefaultAbility { get; set; }

    public void Awake()
    {
        Abilities.Add(new Ability(
           new Action(HitEnemy),
           "Ударить",
           "Ударить",
           true));
    }

    public void HitEnemy()
    {
        Debug.Log("Enemy was hitted");
    }
}
