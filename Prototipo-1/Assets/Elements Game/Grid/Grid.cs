using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prototipo_2;
public class Grid : MonoBehaviour
{
    public Cuadrilla[] cuadrilla;
    public List<List<Cuadrilla>> matrizCuadrilla;
    private int cuadrilla_columnas = 3;
    private int cuadrilla_filas = 3;
    private void Start()
    {
        matrizCuadrilla = new List<List<Cuadrilla>>();
        InitGrid();
        InitMatrizCuadrilla();
    }
    public void InitGrid()
    {
        for (int i = 0; i < matrizCuadrilla.Count; i++)
        {
            matrizCuadrilla.Add(new List<Cuadrilla>());
            for (int j = 0; j < matrizCuadrilla[i].Count; j++)
            {
                matrizCuadrilla[i].Add(null);
            }
        }
    }
    public void RestartCuadrillas()
    {
        for (int i = 0; i < matrizCuadrilla.Count; i++)
        {
            for (int j = 0; j < matrizCuadrilla[i].Count; j++)
            {
                matrizCuadrilla[i][j].ResetCuadrilla();
            }
        }
    }
    public void InitMatrizCuadrilla()
    {
        for (int i = 0; i < cuadrilla_filas; i++)
        {
            for (int j = 0; j < cuadrilla_columnas; j++)
            {
                matrizCuadrilla[i][j] = cuadrilla[i];
            }
        }
    }
}
