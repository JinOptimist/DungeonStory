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

        Debug.Log($"Я бот. [{myCell.X}, {myCell.Z}]. Конец хода");

        var mainController = CoreObjectHelper.GetMainController();
        var heroCell = CoreObjectHelper.GetHeroGameObject().GetComponentInChildren<BaseCellScript>();
        var randomCell = mainController
            .Landscape
            .Where(x => x.GetComponent<GroundScript>() != null)//Get ground
            .Select(x => x.GetComponentInChildren<BaseCellScript>())//Get Cell
            .Where(x => x.X == myCell.X && x.Z == myCell.Z + 1
                || x.X == myCell.X && x.Z == myCell.Z - 1
                || x.X == myCell.X + 1 && x.Z == myCell.Z
                || x.X == myCell.X - 1 && x.Z == myCell.Z)//GetNearCell
            .Where(cell => cell.X != heroCell.X || cell.Z != heroCell.Z)//Get cell without hero
            .Where(x => mainController.GetEnemyByGround(x) == null)//Get ground without enemy
            .GetRandom();

        myCell.X = randomCell?.X ?? myCell.X;
        myCell.Z = randomCell?.Z ?? myCell.Z;
    }
}
