using Assets.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CoinCollectScript : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Destroy(CoreObjectHelper.GetMainController().ActiveObject);
    }
}
