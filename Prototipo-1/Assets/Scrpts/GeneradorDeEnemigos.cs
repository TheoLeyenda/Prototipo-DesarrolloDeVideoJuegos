using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorDeEnemigos : MonoBehaviour
{
    //FIJARSE COMO PROGRAME EL GENERADOR DE ENEMIGOS EN TheDudgeBall Y LUEGO REPLICARLO ACA.
    public Pool poolEnemy;
    public GameObject Generador;
    public GameManager gm;
    private PoolObject poolObject;
    private Enemy.TiposDeEnemigo TypeEmemy;
    private Enemy.TiposDeJefe TypeBoss;

    private void Start()
    {
        if (GameManager.instanceGameManager != null)
        {
            gm = GameManager.instanceGameManager;
        }
    }
    public void GenerateEnemy()
    {

    }
    public void CheckGenerate()
    {

    }
}
