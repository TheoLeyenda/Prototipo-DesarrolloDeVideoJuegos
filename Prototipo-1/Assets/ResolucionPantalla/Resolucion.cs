using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resolucion : MonoBehaviour
{
    // Start is called before the first frame update
    public int Resolucion_X;
    public int Resolucion_Y;
    public bool fullScreen;
    void Start()
    {
        Screen.SetResolution(Resolucion_X, Resolucion_Y, fullScreen);
    }
}
