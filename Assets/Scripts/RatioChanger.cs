using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RatioChanger : MonoBehaviour
{
    public CanvasScaler[] game;
    public float fixedRatioStart = 16 / 9, fixedRatioEnd = 16 / 9;

    public bool isPauseActive;
    public Pause pauseScript;

    // private void OnApplicationFocus(bool focusStatus)
    // {
    //     if (isPauseActive)
    //     {
    //         if (focusStatus)
    //         {
    //             pauseScript.ResumeGame();
    //         }
    //         else
    //         {
    //             pauseScript.PauseGame();
    //         }
    //     }
    // }

    void Update()
    {
        float ratio = (float)Screen.width / (float)Screen.height;
        if (fixedRatioStart > ratio)
        {
            for (int i = 0; i < game.Length; i++)
            {
                game[i].matchWidthOrHeight = 0;
            }
        }
        else if (fixedRatioEnd < ratio)
        {
            for (int i = 0; i < game.Length; i++)
            {
                game[i].matchWidthOrHeight = 1;
            }
        }
    }
}
