using Assets.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletKillScript : MonoBehaviour
{
    private bool _alreadyHit = false;

    void OnCollisionEnter(Collision collision)
    {
        var enemy = collision.gameObject.GetComponentInParent<EnemyScript>();
        var needToRemove = 
            collision.gameObject.name == CoreObjectHelper.GroundName
            || enemy != null;
        
        if (!_alreadyHit && enemy != null)
        {
            enemy.HitEnemy();
        }

        if (needToRemove)
        {
            var bullet = GetComponentInParent<BulletScript>();
            Destroy(bullet.gameObject);
            _alreadyHit = true;
        }
    }
}
