using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Prototipo_1;

public class GeneradorDeEnemigos : MonoBehaviour
{
    //FIJARSE COMO PROGRAME EL GENERADOR DE ENEMIGOS EN TheDudgeBall Y LUEGO REPLICARLO ACA.
    public Pool poolEnemy;
    public GameObject Generador;
    private GameManager gm;
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
        GameObject go = poolEnemy.GetObject();
        Enemy enemy = go.GetComponent<Enemy>();
        enemy.transform.position = Generador.transform.position;
        enemy.alturaPredeterminada = Generador.transform.position.y;
        switch (gm.GetGameMode())
        {
            case EnumsGameManager.ModosDeJuego.Supervivencia:      
                enemy.OnEnemySurvival();
                Debug.Log("ENTRE");
                break;
            case EnumsGameManager.ModosDeJuego.Historia:
                enemy.OnEnemySurvival();
                break;
        }
        if (poolEnemy.count <= 0)
        {
            return;
        }
        
        //go.transform.rotation = transform.rotation;

    }
}
