using Assets.Scripts;
using Assets.Scripts.BaseCellInterfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraClickTriggerScript : MonoBehaviour
{
    private IHovered _oldHoveredObject;
    
    // Update is called once per frame
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hit);
        if (hit.Equals(default(RaycastHit)))
        {
            return;
        }

        var gameObject = hit.collider?.gameObject;
        //Left mouse
        if (Input.GetMouseButtonDown(0))
        {
            var iTriggerClick = gameObject
                .GetComponentsInParent<IClicked>();
            if (iTriggerClick.Length > 0)
            {
                iTriggerClick[0]?.OnLeftMouseClick();
            }
        }
        //Right mouse
        if (Input.GetMouseButtonDown(1))
        {
            var iTriggerClick = gameObject
                .GetComponentsInParent<IClicked>();
            if (iTriggerClick.Length > 0)
            {
                iTriggerClick[0]?.OnRightMouseClick();
            }
        }

        var newHoverCell = gameObject?.GetComponentInParent<IHovered>();
        if (newHoverCell != null)
        {
            if (_oldHoveredObject != null)
            {
                _oldHoveredObject.OnMouseOut();
            }

            _oldHoveredObject = newHoverCell;
            _oldHoveredObject.OnMouseIn();
        }
    }
}
