using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHoverCell : MonoBehaviour
{
    public const string IsHover = "IsHover";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseEnter()
    {
        OnMouseIn();
    }

    void OnMouseExit()
    {
        OnMouseOut();
    }

    public void OnMouseIn()
    {
        gameObject.GetComponentInParent<Animator>().SetBool(IsHover, true);
    }

    public void OnMouseOut()
    {
        gameObject.GetComponentInParent<Animator>().SetBool(IsHover, false);
    }
}
