using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prototipo_2;
public class Grid : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Cuadrilla> cuadrilla;
    public void RestartCuadrillas()
    {
        for (int i = 0; i < cuadrilla.Count; i++)
        {
            cuadrilla[i].ResetCuadrilla();
        }
    }
}
