using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public int LifeTimeSecond;

    private DateTime _burnTime;

    void Start()
    {
        _burnTime = DateTime.Now;
    }

    void Update()
    {
        if (_burnTime.AddSeconds(LifeTimeSecond) < DateTime.Now)
        {
            Destroy(gameObject);
        }
    }
}
