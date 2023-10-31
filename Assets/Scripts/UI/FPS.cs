using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPS : MonoBehaviour
{
    [SerializeField]
    private TMP_Text fpsText;

    int intFPS;

    void Update()
    {
        float fps = 1 / Time.unscaledDeltaTime;
        intFPS = (int)fps;
        fpsText.text = intFPS.ToString();
    }
}
