using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Grid : MonoBehaviour
{
    public enum IdPlataforma
    {
        PlataformaAnatomia,
        PlataformaHistoria,
        PlataformaEducacionFisica,
        PlataformaArte,
        PlataformaMatematica,
        PlataformaQuimica,
        PlataformaProgramacion,
        PlataformaTESIS,
        PlatafomaCafeteria,
        Count,

    }
    private IdPlataforma idPlataforma;
    public List<GameObject> Gm_Plataformas;
    private int cuadrilla_columnas = 3;
    private int cuadrilla_filas = 3;
    public int baseGrild = 2;
    private GameManager gm;
    public Plataformas[] plataformas;
    private void Awake()
    {
        if (GameManager.instanceGameManager != null)
        {
            gm = GameManager.instanceGameManager;
        }
    }
    private void Start()
    {
        ActivatePlataforma();
        
    }
    public void ActivatePlataforma()
    {
        if (Gm_Plataformas.Count > 0)
        {
            for (int i = 0; i < Gm_Plataformas.Count; i++)
            {
                if (Gm_Plataformas[i] != null)
                {
                    Gm_Plataformas[i].SetActive(false);
                }
            }
            if (SceneManager.GetActiveScene().name != "PvP")
            {
                //CUANDO AYA MAS NIVELES AGREGARLOS ACA PARA ACTIVAR LAS PLATAFORMAS DEPENDIENDO DEL NIVEL
                switch (SceneManager.GetActiveScene().name)
                {
                    case "Nivel 1":
                        idPlataforma = IdPlataforma.PlataformaAnatomia;
                        break;
                    case "Nivel 2":
                        idPlataforma = IdPlataforma.PlataformaHistoria;
                        break;
                    case "Nivel 3":
                        idPlataforma = IdPlataforma.PlataformaEducacionFisica;
                        break;
                    case "Supervivencia":
                        idPlataforma = IdPlataforma.PlatafomaCafeteria;
                        break;
                }
                Gm_Plataformas[(int)idPlataforma].SetActive(true);
            }
            else if (SceneManager.GetActiveScene().name == "PvP")
            {
                if (gm != null)
                {
                    Gm_Plataformas[(int)gm.structGameManager.gm_dataCombatPvP.level_selected].SetActive(true);
                }
            }
        }
    }
    
    public int GetCuadrilla_columnas()
    {
        return cuadrilla_columnas;
    }
}
