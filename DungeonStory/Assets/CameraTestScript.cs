using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hit);
        if (Input.GetMouseButtonDown(0))
        {
            var iTriggerClick = hit.collider.gameObject
                .GetComponentsInParent<ITriggerClick>();
            if (iTriggerClick.Length > 0)
            {
                iTriggerClick[0]?.OnTriggerClick();
            }
        }
    }
}
