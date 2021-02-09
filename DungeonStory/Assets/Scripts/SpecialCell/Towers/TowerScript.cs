using Assets.Scripts.BaseCellInterfaces;
using Assets.Scripts.SpecialCell;
using Assets.Scripts.SpecialCell.AbilityStuff;
using Assets.Scripts.SpecialCell.CellInterface;
using System;
using System.Collections;
using System.Collections.Generic;
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
        var bullet = Instantiate(bulletTemplate);
        var bulletPosition = gameObject.transform.position;
        bulletPosition.y += 2;
        bullet.transform.position = bulletPosition;
        bullet.GetComponentInChildren<Rigidbody>().AddForce(new Vector3(3, 2, 5), ForceMode.Impulse);
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
