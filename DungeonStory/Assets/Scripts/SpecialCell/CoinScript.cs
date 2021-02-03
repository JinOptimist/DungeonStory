using Assets.Helpers;
using Assets.Scripts.BaseCellInterfaces;
using Assets.Scripts.SpecialCell;
using Assets.Scripts.SpecialCell.AbilityStuff;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour, IHaveInforamtion, IFinalCell
{
    public string InfoText => "Это монетка. Её можно подобрать";

    public List<Ability> Abilities { get; set; } = new List<Ability>();
    public Ability DefaultAbility { get; set; }

    public void Awake()
    {
        var grabAbility = new Ability(
            new Action(GrabCoin),
            "Схватить",
            "Схватить",
            true);
        
        DefaultAbility = grabAbility;

        Abilities.Add(grabAbility);
        

        Abilities.Add(new Ability(
            new Action(KickCoin),
            "Пнуть монетку",
            "Пнуть монетку",
            false));
    }

    public void GrabCoin()
    {
        var mainController = CoreObjectHelper.GetMainController();
        var activeObject = mainController.ActiveObject;
        var ground = mainController.ReplaceToGround(activeObject);
        mainController.ActivateGameObject(ground);
    }

    public void KickCoin()
    {
        Debug.Log("Мы пнули монетку");
    }
}
