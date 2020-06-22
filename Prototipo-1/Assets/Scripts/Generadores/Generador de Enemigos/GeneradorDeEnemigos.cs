using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GeneradorDeEnemigos : MonoBehaviour
{
    // Start is called before the first frame update
    //FIJARSE COMO PROGRAME EL GENERADOR DE ENEMIGOS EN TheDudgeBall Y LUEGO REPLICARLO ACA.
    public enum TypeGeneration
    {
        DeadthEnemy,
    }
    public enum GeneratorForGameMode
    {
        Supervivencia,
        Historia,
        Nulo,
    }
    [System.Serializable]
    public class EnemiesDataGeneration
    {
        public Pool poolEnemy;
        public EnumsEnemy.TiposDeEnemigo typeEnemy;
        public EnumsEnemy.TiposDeJefe typeBoss;

    }
    [System.Serializable]
    public class BossesDataGeneration
    {
        public Pool poolBoss;
        public EnumsEnemy.TiposDeJefe typeBoss;
    }
    public List<EnemiesDataGeneration> ListEnemyGenerate;
    public List<BossesDataGeneration> ListBossGenerate;
    public LevelManager levelManager;
    public int RondasPorJefe;
    private int randomEnemyGenerate;
    public GameObject Generador;
    public GameObject pointOfCombat;
    public GameObject pointOfInit;
    private GameManager gm;
    private PoolObject poolObject;
    private Enemy enemigoActual;
    private int idListEnemy;
    public bool InitGenerate;
    public TypeGeneration typeGeneration;
    public GeneratorForGameMode GeneradorDelModoDeJuego;
    public bool generarJefes;
    private int countEnemysGenerate = 0;
    private void Awake()
    {
    }
    private void Start()
    {
        idListEnemy = 0;
        if (GameManager.instanceGameManager != null)
        {
            gm = GameManager.instanceGameManager;
        }
        if (gm != null)
        {
            switch (GeneradorDelModoDeJuego)
            {
                case GeneratorForGameMode.Supervivencia:
                    gm.enumsGameManager.modoDeJuego = EnumsGameManager.ModosDeJuego.Supervivencia;
                    break;
                case GeneratorForGameMode.Historia:
                    gm.enumsGameManager.modoDeJuego = EnumsGameManager.ModosDeJuego.Historia;
                    break;
            }
            if (InitGenerate)
            {
                Generate();
            }
            if (gm.enumsGameManager.modoDeJuego == EnumsGameManager.ModosDeJuego.Historia)
            {
                levelManager.ObjectiveOfPassLevel = ListEnemyGenerate.Count;
            }
        }
    }
    private void Update()
    {
        CheckGeneration();
    }
    public void CheckGeneration()
    {
        switch (typeGeneration)
        {
            case TypeGeneration.DeadthEnemy:
                if (enemigoActual != null)
                {
                    if (enemigoActual.life <= 0 && enemigoActual.enumsEnemy.GetStateEnemy() == EnumsEnemy.EstadoEnemigo.muerto || !enemigoActual.enemyPrefab.gameObject.activeSelf)
                    {
                        Generate();
                    }
                }
                break;
        }
    }
    public void Generate()
    {
        GenerateEnemy();
        if (gm.enumsGameManager.modoDeJuego == EnumsGameManager.ModosDeJuego.Supervivencia)
        {
            if (countEnemysGenerate == 1)
            {
                enemigoActual.enemyPrefab.transform.position = pointOfCombat.transform.position;
                enemigoActual.enumsEnemy.SetMovement(EnumsEnemy.Movimiento.Nulo);
                enemigoActual.enableMovement = false;
                //Debug.Log("ENTRE");
            }
            else
            {
                enemigoActual.enemyPrefab.transform.position = Generador.transform.position;
                //enemigoActual.gridEnemy.gameObject.transform.position = new Vector3(0, enemigoActual.gridEnemy.gameObject.transform.position.y, 0);
                enemigoActual.transform.localPosition = new Vector3(0, enemigoActual.transform.localPosition.y, 0);
                enemigoActual.pointOfCombat = pointOfCombat.transform.position;
                enemigoActual.pointOfDeath = pointOfInit.transform.position;
                enemigoActual.enumsEnemy.SetMovement(EnumsEnemy.Movimiento.MoveToPointCombat);
                enemigoActual.enableMovement = true;
                enemigoActual.SetDelaySelectMovement(1);
            }
        }
        else if (gm.enumsGameManager.modoDeJuego == EnumsGameManager.ModosDeJuego.Historia)
        {
            if (idListEnemy == 1)
            {
                enemigoActual.enemyPrefab.transform.position = pointOfCombat.transform.position;
                enemigoActual.enumsEnemy.SetMovement(EnumsEnemy.Movimiento.Nulo);
                enemigoActual.enableMovement = false;
            }
            else
            {
                enemigoActual.enemyPrefab.transform.position = Generador.transform.position;
                //enemigoActual.gridEnemy.gameObject.transform.position = new Vector3(0, enemigoActual.gridEnemy.gameObject.transform.position.y, 0);
                enemigoActual.transform.localPosition = new Vector3(0, enemigoActual.transform.localPosition.y, 0);
                enemigoActual.pointOfCombat = pointOfCombat.transform.position;
                enemigoActual.pointOfDeath = pointOfInit.transform.position;
                enemigoActual.enumsEnemy.SetMovement(EnumsEnemy.Movimiento.MoveToPointCombat);
                enemigoActual.SetDelaySelectMovement(1);
                enemigoActual.enableMovement = true;
            }
        }
    }
    public void GenerateEnemy()
    {
        GameObject go = null;
        Enemy enemy = null;
        Pool pool = null;
        switch (gm.enumsGameManager.modoDeJuego)
        {
            case EnumsGameManager.ModosDeJuego.Supervivencia:
                if (gm.countEnemysDead % RondasPorJefe == 0 && gm.countEnemysDead > 0 && generarJefes)
                {
                    randomEnemyGenerate = Random.Range(0, ListBossGenerate.Count);
                    go = ListBossGenerate[randomEnemyGenerate].poolBoss.GetObject();
                    pool = ListBossGenerate[randomEnemyGenerate].poolBoss;
                }
                else
                {
                    randomEnemyGenerate = Random.Range(0, ListEnemyGenerate.Count);
                    //randomEnemyGenerate = ListEnemyGenerate.Count - 3;
                    go = ListEnemyGenerate[randomEnemyGenerate].poolEnemy.GetObject();
                    pool = ListEnemyGenerate[randomEnemyGenerate].poolEnemy;
                }
                enemy = go.GetComponentInChildren<Enemy>();
                enemy.pool = pool;
                go.transform.position = Generador.transform.position;
                go.transform.rotation = Generador.transform.rotation;
                if (enemy != null)
                {
                    enemigoActual = enemy;
                    enemigoActual.enemyPrefab.transform.position = Generador.transform.position;
                    enemigoActual.InitialPosition = enemigoActual.transform.position;
                    //Debug.Log(enemigoActual.InitialPosition);
                    enemigoActual.ResetEnemy();
                }
                enemigoActual.xpActual = 0;
                enemy.OnEnemy();
                countEnemysGenerate++;
                break;
            case EnumsGameManager.ModosDeJuego.Historia:
                if (idListEnemy < ListEnemyGenerate.Count)
                {
                    go = ListEnemyGenerate[idListEnemy].poolEnemy.GetObject();
                    pool = ListEnemyGenerate[idListEnemy].poolEnemy;
                    if (go != null)
                    {
                        enemy = go.GetComponentInChildren<Enemy>();
                        enemy.pool = pool;
                        go.transform.position = Generador.transform.position;
                        go.transform.rotation = Generador.transform.rotation;
                        if (enemy != null)
                        {
                            enemigoActual = enemy;
                            enemigoActual.enemyPrefab.transform.position = Generador.transform.position;
                            enemigoActual.InitialPosition = enemigoActual.transform.position;
                            enemigoActual.ResetEnemy();
                        }
                        enemigoActual.xpActual = 0;
                        enemy.OnEnemy();
                        if (idListEnemy == 0)
                        {
                            levelManager.SetInDialog(true);
                        }
                        idListEnemy++;
                    }
                }
                else
                {
                    levelManager.ObjectiveOfPassLevel = 0;
                }
                break;
        }
    }
}