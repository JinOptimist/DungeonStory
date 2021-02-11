using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletKillScript : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        var enemy = collision.gameObject.GetComponentInParent<EnemyScript>();
        if (enemy != null)
        {
            enemy.HitEnemy();
            var bullet = GetComponentInParent<BulletScript>();
            Destroy(bullet.gameObject);
        }
    }
}
