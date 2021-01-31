using Assets.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CoinCollectScript : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        var activeObject = CoreObjectHelper.GetMainController().ActiveObject;
        CoreObjectHelper.GetMainController().ReplaceToGround(activeObject);
    }
}
