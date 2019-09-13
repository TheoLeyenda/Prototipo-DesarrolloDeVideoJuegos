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
    public void CheckCuadrillaOcupada(int columnaActual, int CasillasBaseOcupadas,int CasillasAltasOcupadas)
    {
        if (columnaActual < matrizCuadrilla.Count && columnaActual >= 0 && CasillasBaseOcupadas > 0 && CasillasBaseOcupadas <= matrizCuadrilla.Count)
        {
            int casillaBase = 2;
            RestartCuadrillas();
            matrizCuadrilla[columnaActual][casillaBase].SetStateCuadrilla(Cuadrilla.StateCuadrilla.Ocupado);
            switch (CasillasBaseOcupadas)
            {
                case 1:

                    switch (CasillasAltasOcupadas)
                    {
                        case 2:
                            matrizCuadrilla[columnaActual][casillaBase - 1].SetStateCuadrilla(Cuadrilla.StateCuadrilla.Ocupado);
                            break;
                        case 3:
                            matrizCuadrilla[columnaActual][casillaBase - 1].SetStateCuadrilla(Cuadrilla.StateCuadrilla.Ocupado);
                            matrizCuadrilla[columnaActual][casillaBase - 2].SetStateCuadrilla(Cuadrilla.StateCuadrilla.Ocupado);
                            break;
                    }
                    break;

                case 2:
                    switch (columnaActual)
                    {
                        case 0:

                            matrizCuadrilla[columnaActual + 1][casillaBase].SetStateCuadrilla(Cuadrilla.StateCuadrilla.Ocupado);
                            switch (CasillasAltasOcupadas)
                            {
                                case 2:
                                    matrizCuadrilla[columnaActual][casillaBase - 1].SetStateCuadrilla(Cuadrilla.StateCuadrilla.Ocupado);
                                    matrizCuadrilla[columnaActual + 1][casillaBase - 1].SetStateCuadrilla(Cuadrilla.StateCuadrilla.Ocupado);
                                    break;
                                case 3:
                                    matrizCuadrilla[columnaActual][casillaBase - 1].SetStateCuadrilla(Cuadrilla.StateCuadrilla.Ocupado);
                                    matrizCuadrilla[columnaActual + 1][casillaBase - 1].SetStateCuadrilla(Cuadrilla.StateCuadrilla.Ocupado);
                                    matrizCuadrilla[columnaActual][casillaBase - 2].SetStateCuadrilla(Cuadrilla.StateCuadrilla.Ocupado);
                                    matrizCuadrilla[columnaActual + 1][casillaBase - 2].SetStateCuadrilla(Cuadrilla.StateCuadrilla.Ocupado);
                                    break;
                            }

                            break;
                        case 1:
                            matrizCuadrilla[columnaActual + 1][casillaBase].SetStateCuadrilla(Cuadrilla.StateCuadrilla.Ocupado);
                            switch (CasillasAltasOcupadas)
                            {
                                case 2:
                                    matrizCuadrilla[columnaActual][casillaBase - 1].SetStateCuadrilla(Cuadrilla.StateCuadrilla.Ocupado);
                                    matrizCuadrilla[columnaActual + 1][casillaBase - 1].SetStateCuadrilla(Cuadrilla.StateCuadrilla.Ocupado);
                                    break;
                                case 3:
                                    matrizCuadrilla[columnaActual][casillaBase - 1].SetStateCuadrilla(Cuadrilla.StateCuadrilla.Ocupado);
                                    matrizCuadrilla[columnaActual + 1][casillaBase - 1].SetStateCuadrilla(Cuadrilla.StateCuadrilla.Ocupado);
                                    matrizCuadrilla[columnaActual][casillaBase - 2].SetStateCuadrilla(Cuadrilla.StateCuadrilla.Ocupado);
                                    matrizCuadrilla[columnaActual + 1][casillaBase - 2].SetStateCuadrilla(Cuadrilla.StateCuadrilla.Ocupado);
                                    break;
                            }
                            break;
                    }
                    break;
                case 3:
                    matrizCuadrilla[columnaActual + 1][casillaBase].SetStateCuadrilla(Cuadrilla.StateCuadrilla.Ocupado);
                    matrizCuadrilla[columnaActual + 2][casillaBase].SetStateCuadrilla(Cuadrilla.StateCuadrilla.Ocupado);
                    switch (CasillasAltasOcupadas)
                    {
                        case 2:
                            matrizCuadrilla[columnaActual][casillaBase + 1].SetStateCuadrilla(Cuadrilla.StateCuadrilla.Ocupado);
                            matrizCuadrilla[columnaActual + 1][casillaBase+1].SetStateCuadrilla(Cuadrilla.StateCuadrilla.Ocupado);
                            matrizCuadrilla[columnaActual + 2][casillaBase+1].SetStateCuadrilla(Cuadrilla.StateCuadrilla.Ocupado);
                            break;
                        case 3:
                            matrizCuadrilla[columnaActual][casillaBase + 1].SetStateCuadrilla(Cuadrilla.StateCuadrilla.Ocupado);
                            matrizCuadrilla[columnaActual + 1][casillaBase + 1].SetStateCuadrilla(Cuadrilla.StateCuadrilla.Ocupado);
                            matrizCuadrilla[columnaActual + 2][casillaBase + 1].SetStateCuadrilla(Cuadrilla.StateCuadrilla.Ocupado);
                            matrizCuadrilla[columnaActual][casillaBase + 2].SetStateCuadrilla(Cuadrilla.StateCuadrilla.Ocupado);
                            matrizCuadrilla[columnaActual + 1][casillaBase + 2].SetStateCuadrilla(Cuadrilla.StateCuadrilla.Ocupado);
                            matrizCuadrilla[columnaActual + 2][casillaBase + 2].SetStateCuadrilla(Cuadrilla.StateCuadrilla.Ocupado);
                            break;
                    }
                    break;
            }
        }
    }
}
