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
        public const string MainControllerName = "MainController";
        public const string UiControllerName = "UiController";
        public const string MainHeroTag = "MainHero";
        public const string MazeGeneratorName = "MazeGenerator";
        public const string DirectionalLightName = "MainLight";

        public const string GroundName = "Ground";

        public const int blockSize = 1;

        private static MainController _mainController;
        public static MainController GetMainController()
        {
            if (_mainController == null)
            {
                _mainController = GameObject
                    .Find(MainControllerName)
                    .GetComponent<MainController>();
            }

            return _mainController;
        }

        private static UiController _uiController;
        public static UiController GetUiController()
        {
            if (_uiController == null)
            {
                _uiController = GameObject
                    .Find(UiControllerName)
                    .GetComponent<UiController>();
            }

            return _uiController;
        }

        public static MazeGeneratorLogicScript GetMazeGenerator()
        {
            return GameObject
                .Find(MazeGeneratorName)
                .GetComponent<MazeGeneratorLogicScript>();
        }

        public static GameObject GetHeroGameObject()
        {
            return GameObject
                .FindGameObjectWithTag(MainHeroTag);
        }

        public static HeroMoveScript GetHeroMoveScript()
        {
            return GetHeroGameObject().GetComponent<HeroMoveScript>();
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
    
        public static LightScript GetLightScript()
        {
            return GameObject.Find(DirectionalLightName).GetComponent<LightScript>();
        }
    }
}
