using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraFollowScript : MonoBehaviour
{
    public float offsetY;
    public float offsetZ;

    public float speed;

    private void Update()
    {
        var scrollWhell = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWhell > 0f) // forward
        {
            offsetY /= 1.1f;
            offsetZ /= 1.1f;
        }
        else if (scrollWhell < 0f) // backwards
        {
            offsetY *= 1.1f;
            offsetZ *= 1.1f;
        }
    }

    void LateUpdate()
    {
        var player = GameObject.FindGameObjectWithTag("MainHero");
        if (player != null)
        {
            var finalPosition = player.transform.position + new Vector3(0, offsetY, offsetZ);
            var lerp = Vector3.Lerp(transform.position, finalPosition, Time.deltaTime * speed);
            gameObject.GetComponent<Transform>().position = lerp;
        }
    }
}
