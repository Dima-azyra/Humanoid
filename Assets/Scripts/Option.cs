using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option : MonoBehaviour
{
    private void Awake()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        Application.targetFrameRate = 60;
    }
}
