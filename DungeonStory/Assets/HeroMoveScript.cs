using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMoveScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.W))
        {
            gameObject.transform.position = gameObject.transform.position + new Vector3(0, 0, 1);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            gameObject.transform.position = gameObject.transform.position + new Vector3(0, 0, -1);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            gameObject.transform.position = gameObject.transform.position + new Vector3(-1, 0, 0);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            gameObject.transform.position = gameObject.transform.position + new Vector3(1, 0, 0);
        }
        
    }
}
