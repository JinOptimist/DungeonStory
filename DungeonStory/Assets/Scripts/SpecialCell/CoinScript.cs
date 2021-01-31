using Assets.Helpers;
using Assets.Scripts.BaseCellInterfaces;
using Assets.Scripts.SpecialCell;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour, IHaveInforamtion, IFinalCell
{
    public string InfoText => "Это монетка. Её можно подобрать";
}
