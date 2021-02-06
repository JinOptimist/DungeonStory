using Assets.Helpers;
using Assets.Scripts.BaseCellInterfaces;
using Assets.Scripts.SpecialCell;
using Assets.Scripts.SpecialCell.AbilityStuff;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairDownScript : MonoBehaviour, IHaveInforamtion, IFinalCell
{
    public int heightOfDrop;
    public string InfoText => "Лестница вниз. Путь в глубину подземелья";

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
           new Action(GoDown),
           "Спуститься",
           "Спуститься",
           true));
    }

    public void GoDown()
    {
        var hero = CoreObjectHelper.GetHeroGameObject();
        hero.transform.position = new Vector3(hero.transform.position.x, heightOfDrop, hero.transform.position.z);

        var cameraPosition = Camera.main.transform.position;
        Camera.main.transform.position = new Vector3(cameraPosition.x, cameraPosition.y + heightOfDrop, cameraPosition.z);

        hero.transform.position = new Vector3(hero.transform.position.x, heightOfDrop, hero.transform.position.z);

        CoreObjectHelper.GetMainController().GenerateMaze(gameObject);
    }

    public void Step()
    {
        CoreObjectHelper.GetMainController().MoveHeroToCell();
    }
}
