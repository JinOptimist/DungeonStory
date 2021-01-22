﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraFollowScript : MonoBehaviour
{
    public float offsetY;
    public float offsetZ;

    void LateUpdate()
    {
        var player = GameObject.FindGameObjectWithTag("MainHero");
        if (player != null)
        {
            gameObject.GetComponent<Transform>().position = 
                player.transform.position + new Vector3(0, offsetY, offsetZ);
        }
    }
}