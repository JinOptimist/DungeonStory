using Assets.Helpers;
using Assets.Scripts.BaseCellInterfaces;
using Assets.Scripts.SpecialCell;
using Assets.Scripts.SpecialCell.AbilityStuff;
using Assets.Scripts.SpecialCell.CellInterface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerScript : MonoBehaviour, IHaveInforamtion, IFinalCell, IEndTurn
{
    public GameObject bulletTemplate;

    public string InfoText => "Башенка. Стреляет";
    public List<Ability> Abilities { get; set; } = new List<Ability>();
    public Ability DefaultAbility { get; set; }

    private List<GameObject> _bullets = new List<GameObject>();

    public void Awake()
    {
        Abilities.Add(new Ability(
           new Action(HitWall),
           "Пить",
           "Пить",
           true));
    }

    public void EndTurn()
    {
        var cell = GetComponentInChildren<BaseCellScript>();
        var enemies = CoreObjectHelper.GetMazeGenerator().GetNearEnemy(cell);
        if (enemies.Any())
        {
            Fire(enemies.GetRandom());
        }
    }

    public void Fire(GameObject enemy)
    {
        var bullet = Instantiate(bulletTemplate);
        var bulletPosition = gameObject.transform.position;
        bulletPosition.y += 2;
        bullet.transform.position = bulletPosition;

        var enemyX = enemy.transform.position.x - transform.position.x;
        var enemyZ = enemy.transform.position.z - transform.position.z;
        var fireImpulse = new Vector3(enemyX * 2, 0, enemyZ * 2);

        bullet.GetComponentInChildren<Rigidbody>().AddForce(fireImpulse, ForceMode.Impulse);
        _bullets.Add(bullet);
    }

    private void OnDestroy()
    {
        _bullets.ForEach(x => Destroy(x));
    }

    public void HitWall()
    {
        Debug.Log("We drink from a fontain");
    }
}
