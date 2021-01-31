using Assets.Scripts.BaseCellInterfaces;
using Assets.Scripts.SpecialCell;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FountainScript : MonoBehaviour, IHaveInforamtion, IFinalCell
{
    public string InfoText => "Это фонтан. Тут можно попить воды";
}
