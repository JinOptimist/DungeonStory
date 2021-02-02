using Assets.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMoveScript : MonoBehaviour
{
    public float moveAnimationSpeed;
    // Update is called once per frame
    void Update()
    {
        KeyboardMove();

        var baseCell = GetComponentInChildren<BaseCellScript>();
        var finalPosition = CoreObjectHelper.GetPositionByCoordinate(baseCell.X, baseCell.Z);

        var lerp = Vector3.Lerp(transform.position, finalPosition, Time.deltaTime * moveAnimationSpeed);
        transform.position = lerp;
    }

    public void KeyboardMove()
    {
        var baseCell = GetComponentInChildren<BaseCellScript>();
        if (Input.GetKeyUp(KeyCode.W))
        {
            baseCell.Z++;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            baseCell.Z--;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            baseCell.X--;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            baseCell.X++;
        }
    }
}
