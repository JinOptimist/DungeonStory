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
    public int hp;

    public string InfoText => "Это противник. Бей или беги";

    public List<Ability> Abilities { get; set; } = new List<Ability>();

    public Ability DefaultAbility { get; set; }

    public void Awake()
    {
        var hit = new Ability(
           new Action(HitEnemy),
           "Ударить",
           "Ударить",
           true);

        DefaultAbility = hit;

        Abilities.Add(hit);
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
        hp--;
        if (hp <= 0)
        {
            CoreObjectHelper.GetMainController().KillEnemy(gameObject);
            return;
        }

        var hpBar = gameObject.transform.Find("HpBar");
        hpBar.transform.localScale = new Vector3(0.1f, 0.1f * hp, 0.1f);
    }
}
