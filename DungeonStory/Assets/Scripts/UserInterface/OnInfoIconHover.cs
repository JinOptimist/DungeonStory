using Assets.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnInfoIconHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        CoreObjectHelper.GetUiController().UIInfoBlockText.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CoreObjectHelper.GetUiController().UIInfoBlockText.SetActive(false);
    }
}
