using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public GameObject ActiveObject { get; private set; }

    public const string IsCubeActive = "IsActive";

    public void ActivateGameObject(GameObject gameObject)
    {
        if (ActiveObject != null)
        {
            //Deactivate old active obj
            ActiveObject.GetComponentInParent<Animator>()
                .SetBool(IsCubeActive, false);
        }

        ActiveObject = gameObject;
        ActiveObject.GetComponentInParent<Animator>()
            .SetBool(IsCubeActive, true);
    }
}
