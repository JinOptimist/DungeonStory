using Assets.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMoveScript : MonoBehaviour
{
    public float moveAnimationSpeed;
    public float rotationYAngle;
    
    // Update is called once per frame
    void Update()
    {
        KeyboardMoveAndRotation();

        var baseCell = GetComponentInChildren<BaseCellScript>();
        var finalPosition = CoreObjectHelper.GetPositionByCoordinate(baseCell.X, baseCell.Z);

        var lerp = Vector3.Lerp(transform.position, finalPosition, Time.deltaTime * moveAnimationSpeed);
        transform.position = lerp;

        var finalRotation = new Vector3(transform.eulerAngles.x, rotationYAngle, transform.eulerAngles.z);
        var lerpRotation = Vector3.Lerp(transform.eulerAngles, finalRotation, Time.deltaTime * moveAnimationSpeed);
        transform.eulerAngles = lerpRotation;
    }

    public void KeyboardMoveAndRotation()
    {
        //var baseCell = GetComponentInChildren<BaseCellScript>();
        //var angelInRadian = rotationYAngle * Mathf.Deg2Rad;
        //if (Input.GetKeyUp(KeyCode.W))
        //{
        //    baseCell.X += Mathf.RoundToInt(Mathf.Sin(angelInRadian));
        //    baseCell.Z += Mathf.RoundToInt(Mathf.Cos(angelInRadian));
        //}
        //if (Input.GetKeyUp(KeyCode.S))
        //{
        //    baseCell.X -= Mathf.RoundToInt(Mathf.Sin(angelInRadian));
        //    baseCell.Z -= Mathf.RoundToInt(Mathf.Cos(angelInRadian));
        //}
        //if (Input.GetKeyUp(KeyCode.A))
        //{
        //    baseCell.X -= Mathf.RoundToInt(Mathf.Cos(angelInRadian));
        //    baseCell.Z += Mathf.RoundToInt(Mathf.Sin(angelInRadian));
        //}
        //if (Input.GetKeyUp(KeyCode.D))
        //{
        //    baseCell.X += Mathf.RoundToInt(Mathf.Cos(angelInRadian));
        //    baseCell.Z -= Mathf.RoundToInt(Mathf.Sin(angelInRadian));
        //}

        if (Input.GetKeyUp(KeyCode.Q))
        {
            rotationYAngle += 90;
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            rotationYAngle -= 90;
        }

        if (Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.E))
        {
            rotationYAngle = rotationYAngle % 360;
            if (rotationYAngle < 0)
            {
                rotationYAngle += 360;
            }

            Debug.Log($"rotationYAngle = {rotationYAngle}");
        }
    }
}
