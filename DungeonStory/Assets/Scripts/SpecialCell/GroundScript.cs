using Assets.Scripts.BaseCellInterfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.SpecialCell;

public class GroundScript : MonoBehaviour, IHaveInforamtion, IFinalCell
{
    public string InfoText => "Просто земля. На неё можно наступить";
}
