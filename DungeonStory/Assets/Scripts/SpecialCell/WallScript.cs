using Assets.Helpers;
using Assets.Scripts.BaseCellInterfaces;
using Assets.Scripts.SpecialCell;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour, IHaveInforamtion, IFinalCell
{
    public string InfoText => "Это стена. Её можно сломать. Но это долго";
}
