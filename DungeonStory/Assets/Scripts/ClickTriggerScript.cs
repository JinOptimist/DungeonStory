using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickTriggerScript : MonoBehaviour
{
    private OnHoverCell _oldHoveredObject;
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
        if (Input.GetMouseButtonDown(0))
        {
            var iTriggerClick = gameObject
                .GetComponentsInParent<ITriggerClick>();
            if (iTriggerClick.Length > 0)
            {
                iTriggerClick[0]?.OnTriggerClick();
            }
        }

        var newHoverCell = gameObject?.GetComponentInParent<OnHoverCell>();
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
