using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCameraFollowScript : MonoBehaviour
{
    public int cameraY;
    // Update is called once per frame
    void LateUpdate()
    {
        var player = GameObject.FindGameObjectWithTag("MainHero");
        if (player != null)
        {
            gameObject.GetComponent<Transform>().position =
                player.transform.position + new Vector3(0, cameraY, 0);
        }
    }
}
