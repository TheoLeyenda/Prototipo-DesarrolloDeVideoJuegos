using System.Collections.Generic;
using UnityEngine;

public class SelectedPlayers : MonoBehaviour
{
    // ESTE SCRIPT DEBE COMUNICAR AL STRUCT DEL GAME MANAGER LAS SELECCIONES DE LOS JUGADORES (tanto player1 como player2)
    struct CursorMatriz
    {
        public int x;
        public int y;
    }
    public List<string> nameOptions;
    public string[,] grillaDeSeleccion;
    public int filas;
    public int columnas;
    private int idOption;
    private CursorMatriz cursorPlayer1;
    private CursorMatriz cursorPlayer2;
    private string namePlayerSelected;
    private void Start()
    {
        idOption = 0;
        cursorPlayer1.x = 0;
        cursorPlayer1.y = 0;
        if (filas > 0 && columnas > 0)
        {
            grillaDeSeleccion = new string[filas, columnas];
            if (grillaDeSeleccion != null)
            {
                for (int i = 0; i < filas; i++)
                {
                    for (int j = 0; j < columnas; j++)
                    {
                        if (idOption < nameOptions.Count)
                        {
                            grillaDeSeleccion[i, j] = nameOptions[idOption];
                        }
                        idOption++;
                    }
                }
            }
        }
        idOption = 0;
    }
    public void MoveCursor()
    {

    }
    public string GetNamePlayerSelected()
    {
        return namePlayerSelected;
    }
    // ESTE SCRIPT DEBE COMUNICAR AL STRUCT DEL GAME MANAGER LAS SELECCIONES DE LOS JUGADORES (tanto player1 como player2)
}
