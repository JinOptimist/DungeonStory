using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Helpers
{
    public class CoreObjectHelper
    {
        public const string MainControllerTag = "MainController";
        public const string MainHeroTag = "MainHero";

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
    }
}
