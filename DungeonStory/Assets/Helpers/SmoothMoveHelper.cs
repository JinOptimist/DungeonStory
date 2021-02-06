using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Helpers
{
    public static class SmoothMoveHelper
    {
        public static void SmartRotationY(Transform targetTransform, float angle, float animationSpeed, 
            bool fixToZeroXandZ = false)
        {
            var finalRotation = targetTransform.eulerAngles;
            finalRotation.y = angle;
            //finalRotation.x = 0;
            //finalRotation.z = 0;

            var currentAngles = targetTransform.eulerAngles;
            if (Mathf.Abs(currentAngles.y - finalRotation.y) >= 180)
            {
                if (currentAngles.y - finalRotation.y > 0)
                {
                    finalRotation.y += 360;
                }
                else
                {
                    finalRotation.y -= 360;
                }
            }

            var lerpRotation = Vector3.Lerp(currentAngles, finalRotation, Time.deltaTime * animationSpeed);
            if (fixToZeroXandZ)
            {
                lerpRotation.x = 0;
                lerpRotation.z = 0;
            }
            targetTransform.eulerAngles = lerpRotation;
        }
    }
}
