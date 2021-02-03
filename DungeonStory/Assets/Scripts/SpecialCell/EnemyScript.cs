using Assets.Helpers;
using Assets.Scripts.BaseCellInterfaces;
using Assets.Scripts.SpecialCell;
using Assets.Scripts.SpecialCell.AbilityStuff;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour, IHaveInforamtion, IFinalCell
{
    public float moveAnimationSpeed;

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

    private void Update()
    {
        var baseCell = GetComponentInChildren<BaseCellScript>();
        var finalPosition = CoreObjectHelper.GetPositionByCoordinate(baseCell.X, baseCell.Z);
        var lerp = Vector3.Lerp(transform.position, finalPosition, Time.deltaTime * moveAnimationSpeed);
        transform.position = lerp;
    }

    public void HitEnemy()
    {
        Debug.Log("Enemy was hitted");
    }
}
