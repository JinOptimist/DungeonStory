using Assets.Scripts;
using Assets.Scripts.BaseCellInterfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraClickTriggerScript : MonoBehaviour
{
    private GameObject _oldHoveredObject;
    
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
        var newHoverCell = gameObject?.GetComponentInParent<IHovered>();
        //Check do we look at new cell and also wo we have old cell
        // Check "_oldHoveredObject != null" is nessary to be sure that we doesn't destroy old cell
        if (newHoverCell != null && _oldHoveredObject != null)
        {
            _oldHoveredObject?.GetComponentInParent<IHovered>()?.OnMouseOut();
        }
        
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

        if (newHoverCell != null)
        {
            newHoverCell.OnMouseIn();
            _oldHoveredObject = gameObject;
        }
        
    }
}
