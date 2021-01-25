using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMoveScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        var moveVector = new Vector3(0, 0, 0);
        if (Input.GetKeyUp(KeyCode.W))
        {
            moveVector = new Vector3(0, 0, 1);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            moveVector = new Vector3(0, 0, -1);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            moveVector = new Vector3(-1, 0, 0);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            moveVector = new Vector3(1, 0, 0);
        }

        gameObject.transform.position = gameObject.transform.position + moveVector;
        //float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y + distToPlayer, ref velocity.y, smoothTimeY);
        //if (Input.GetMouseButton(0) || player.transform.position.y > transform.position.y - 2.6)
        //{
        //    transform.position = new Vector3(transform.position.x, posY, transform.position.z);
        //}

    }
}
