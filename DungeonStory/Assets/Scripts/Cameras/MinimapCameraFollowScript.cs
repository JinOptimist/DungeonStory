using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCameraFollowScript : MonoBehaviour
{
    public int cameraY;
    public float speed;

    private void Update()
    {
        var scrollWhell = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWhell > 0f) // forward
        {
            cameraY--;
        }
        else if (scrollWhell < 0f) // backwards
        {
            cameraY++;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        var player = GameObject.FindGameObjectWithTag("MainHero");
        if (player != null)
        {
            var finalPosition = player.transform.position + new Vector3(0, cameraY, 0);
            var lerp = Vector3.Lerp(transform.position, finalPosition, Time.deltaTime * speed);
            gameObject.GetComponent<Transform>().position = lerp;
        }
    }
}
