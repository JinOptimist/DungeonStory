using Assets.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraFollowScript : MonoBehaviour
{
    public float offsetUp;
    public float offsetBack;

    public float speed;

    private void Update()
    {
        var scrollWhell = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWhell > 0f) // forward
        {
            offsetUp /= 1.1f;
            offsetBack /= 1.1f;
        }
        else if (scrollWhell < 0f) // backwards
        {
            offsetUp *= 1.1f;
            offsetBack *= 1.1f;
        }
    }

    void LateUpdate()
    {
        var player = GameObject.FindGameObjectWithTag("MainHero");
        if (player != null)
        {
            var h = player.GetComponent<HeroMoveScript>();
            
            var x = offsetBack * Mathf.Sin(h.rotationYAngle * Mathf.Deg2Rad);
            var z = offsetBack * Mathf.Cos(h.rotationYAngle * Mathf.Deg2Rad);

            var finalPosition = player.transform.position + new Vector3(x, offsetUp, z);
            var lerp = Vector3.Lerp(transform.position, finalPosition, Time.deltaTime * speed);
            transform.position = lerp;

            SmoothMoveHelper.SmartRotationY(transform, h.rotationYAngle, speed);
        }
    }
}
