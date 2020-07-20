using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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
    public bool inBoss;

    public static event Action<Grid, Vector3> OnSettingTitileo;
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
    private void OnEnable()
    {
        ProfesorAnatomia.OnInitTrowSpecialAttackTerremoto += TitiledGrid;
    }
    private void OnDisable()
    {
        ProfesorAnatomia.OnInitTrowSpecialAttackTerremoto -= TitiledGrid;
    }
    public void TitiledGrid(ProfesorAnatomia profesorAnatomia, float delayTitileo, int numCasilla,bool AllCasillas)
    {
        if (!inBoss)
        {
            if (profesorAnatomia != null)
            {
                Plataformas currentPlataforma = null;
                Vector3 cuadrillaPosition = Vector3.zero;
                Cuadrilla cuadrilla = null;
                float substractVector = 2.5f;
                for (int i = 0; i < plataformas.Length; i++)
                {
                    if (plataformas[i].gameObject.activeSelf)
                    {
                        currentPlataforma = plataformas[i];
                    }
                }
                if (!AllCasillas)
                {
                    switch (numCasilla)
                    {
                        case 1:
                            cuadrillaPosition = currentPlataforma.plataformaIzquierda.transform.position;
                            cuadrilla = currentPlataforma.cuadrillaPlataformaIzquierda;
                            cuadrilla.SetDelayTitileo(delayTitileo);
                            break;
                        case 2:
                            cuadrillaPosition = currentPlataforma.plataformaCentral.transform.position;
                            cuadrilla = currentPlataforma.cuadrillaPlataformaCentral;
                            cuadrilla.SetDelayTitileo(delayTitileo);
                            break;
                        case 3:
                            cuadrillaPosition = currentPlataforma.plataformaDerecha.transform.position;
                            cuadrilla = currentPlataforma.cuadrillaPlataformaDerecha;
                            cuadrilla.SetDelayTitileo(delayTitileo);
                            break;
                    }
                    if (OnSettingTitileo != null)
                    {
                        OnSettingTitileo(this, cuadrilla.transform.position + new Vector3(0, -substractVector, 0));
                        //profesorAnatomia.GeneratorSpecialAttack.transform.position = cuadrilla.transform.position;
                        //Debug.Log("ENTRE");
                    }
                }
                else
                {
                    cuadrillaPosition = currentPlataforma.plataformaIzquierda.transform.position;
                    cuadrilla = currentPlataforma.cuadrillaPlataformaIzquierda;
                    cuadrilla.SetDelayTitileo(delayTitileo);

                    cuadrillaPosition = currentPlataforma.plataformaCentral.transform.position;
                    cuadrilla = currentPlataforma.cuadrillaPlataformaCentral;
                    cuadrilla.SetDelayTitileo(delayTitileo);

                    cuadrillaPosition = currentPlataforma.plataformaDerecha.transform.position;
                    cuadrilla = currentPlataforma.cuadrillaPlataformaDerecha;
                    cuadrilla.SetDelayTitileo(delayTitileo);

                    if (OnSettingTitileo != null)
                    {
                        OnSettingTitileo(this, currentPlataforma.transform.position + new Vector3(0, -substractVector, 0));
                        //profesorAnatomia.GeneratorSpecialAttack.transform.position = cuadrilla.transform.position;
                        //Debug.Log("ENTRE");
                    }
                }
            }
        }
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
