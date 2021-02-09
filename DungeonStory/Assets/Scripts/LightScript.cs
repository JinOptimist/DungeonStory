using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    [Tooltip("Set level where light will be total off")]
    public int LevelWithoutLight;
    public float animationSpeed;
    private Color _finalColor;

    void Update()
    {
        var light = GetComponent<Light>();
        light.color = Color.Lerp(light.color, _finalColor, Time.deltaTime * animationSpeed);
    }

    public void SetBrightnessByLevel(int level)
    {
        var c = 1 - 1f * level / LevelWithoutLight;
        if (c < 0)
        {
            c = 0;
        }

        _finalColor = new Color(c, c, c * 0.9f);
    }
}
