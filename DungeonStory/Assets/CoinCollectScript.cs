using Assets.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollectScript : MonoBehaviour
{
    public void CollectCoin()
    {
        Destroy(CoreObjectHelper.GetMainController().ActiveObject);
    }
}
