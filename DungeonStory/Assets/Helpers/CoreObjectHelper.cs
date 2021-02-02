﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Helpers
{
    public class CoreObjectHelper
    {
        public const string MainControllerTag = "MainController";
        public const string MainHeroTag = "MainHero";

        public const int blockSize = 1;

        public static MainController GetMainController()
        {
            return GameObject
                .FindGameObjectWithTag(MainControllerTag)
                .GetComponent<MainController>();
        }

        public static GameObject GetHeroGameObject()
        {
            return GameObject
                .FindGameObjectWithTag(MainHeroTag);
        }

        public static void MoveCellToPosition(GameObject gameObject, int x, int z)
        {
            if (gameObject == null)
            {
                return;
            }

            var cell = gameObject.GetComponentInChildren<BaseCellScript>();
            if (cell == null)
            {
                Debug.LogWarning("We try to move object which doesn't have BaseCellScript");
            }
            cell.X = x;
            cell.Z = z;

            var cellTransform = gameObject.GetComponent<Transform>();
            cellTransform.position = GetPositionByCoordinate(x, z);

            //var scale = resizeBlock ? blockSize : 1;
            //cellTransform.localScale = new Vector3(scale, scale, scale);
        }

        public static Vector3 GetPositionByCoordinate(int x, int z)
        {
            return new Vector3(x * blockSize, 0, z * blockSize);
        }
    }
}
