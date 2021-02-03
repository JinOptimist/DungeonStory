using Assets.Helpers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AiEndTurnScript : MonoBehaviour
{
    public void EndTurn()
    {
        var myCell = gameObject.GetComponentInChildren<BaseCellScript>();
        //myCell.X++;

        Debug.Log($"Я бот. [{myCell.X}, {myCell.Z}]. Конец хода");

        var randomCell = CoreObjectHelper.GetMainController()
            .Landscape
            .Where(x => x.GetComponent<GroundScript>() != null)
            .Select(x => x.GetComponentInChildren<BaseCellScript>())
            .Where(x => x.X == myCell.X && x.Z == myCell.Z + 1
                || x.X == myCell.X && x.Z == myCell.Z - 1
                || x.X == myCell.X + 1 && x.Z == myCell.Z
                || x.X == myCell.X - 1 && x.Z == myCell.Z)
            .GetRandom();
        
        myCell.X = randomCell?.X ?? myCell.X; 
        myCell.Z = randomCell?.Z ?? myCell.Z;
    }
}
