using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resolucion : MonoBehaviour
{
    public float Resolucion_X;
    public float Resolucion_Y;
    public float MinResolution_X = 640;
    public float MinResolution_Y = 360;
    private float OriginalResolution_X;
    private float OriginalResolution_Y;
    public bool fullScreen;
    public bool automaticRescaledResolution;
    void Start()
    {
        OriginalResolution_X = Resolucion_X;
        OriginalResolution_Y = Resolucion_Y;

        Screen.SetResolution((int)Resolucion_X, (int)Resolucion_Y, fullScreen);
        if (automaticRescaledResolution)
        {
            if (Screen.width < Resolucion_X || Screen.height < Resolucion_Y)
            {
                fullScreen = false;
                Resolucion_X = MinResolution_X;
                Resolucion_Y = MinResolution_Y;
                Screen.SetResolution((int)Resolucion_X, (int)Resolucion_Y, fullScreen);
            }
        }
    }
}
