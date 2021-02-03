using Assets.Helpers;
using Assets.Scripts.SpecialCell.AbilityStuff;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AbilityUIClickScript : MonoBehaviour, IPointerClickHandler
{
    public Ability Ability;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Ability.Abailable)
        {
            Ability.RunAction();
        }
    }
}
