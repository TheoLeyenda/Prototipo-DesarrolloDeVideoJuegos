using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototipo_2
{
    public class GeneradorDeEnemigos : MonoBehaviour
    {
        // Start is called before the first frame update
        //FIJARSE COMO PROGRAME EL GENERADOR DE ENEMIGOS EN TheDudgeBall Y LUEGO REPLICARLO ACA.
        public enum TypeGeneration
        {
            DeadthEnemy,
        }
        public Pool poolEnemy;
        public GameObject Generador;
        public GameObject pointOfCombat;
        public GameObject pointOfInit;
        private GameManager gm;
        private PoolObject poolObject;
        private Enemy enemigoActual;
        public List<EnumsEnemy.TiposDeEnemigo> TypeEnemiesLevel;
        public List<EnumsEnemy.TiposDeJefe> TypeBossLevel;
        private int idListEnemy;
        public bool InitGenerate;
        public TypeGeneration typeGeneration;
        private void Awake()
        {
            if (GameManager.instanceGameManager != null)
            {
                gm = GameManager.instanceGameManager;
            }
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
                if (InitGenerate)
                {
                    Generate();
                }
                gm.levelManager.ObjectiveOfPassLevel = TypeBossLevel.Count+1;
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
                        if (enemigoActual.life <= 0)
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
                enemigoActual.pointOfCombat = pointOfCombat.transform.position;
                enemigoActual.pointOfDeath = pointOfInit.transform.position;
                enemigoActual.enumsEnemy.SetMovement(EnumsEnemy.Movimiento.MoveToPointCombat);
            }
            else if (gm.enumsGameManager.modoDeJuego == EnumsGameManager.ModosDeJuego.Historia)
            {
                Debug.Log("ENTRE");
                enemigoActual.ENEMY.transform.position = pointOfCombat.transform.position;
                enemigoActual.enumsEnemy.SetMovement(EnumsEnemy.Movimiento.Nulo);
            }
        }
        public void GenerateEnemy()
        {
            GameObject go = poolEnemy.GetObject();
            Enemy enemy = go.GetComponentInChildren<Enemy>();
            go.transform.position = Generador.transform.position;
            go.transform.rotation = Generador.transform.rotation;
            if (enemy != null)
            {
                enemigoActual = enemy;
                enemigoActual.InitialPosition = go.transform.position = Generador.transform.position;
                enemigoActual.ResetEnemy();
            }
            
            switch (gm.enumsGameManager.modoDeJuego)
            {
                case EnumsGameManager.ModosDeJuego.Supervivencia:
                    enemy.OnEnemySurvival();
                    break;
                case EnumsGameManager.ModosDeJuego.Historia:
                    if (idListEnemy < TypeEnemiesLevel.Count)
                    {
                        enemy.OnEnemyHistory(TypeEnemiesLevel[idListEnemy], TypeBossLevel[idListEnemy]);
                        if (idListEnemy == 0)
                        {
                            gm.levelManager.SetInDialog(true);
                        }
                        idListEnemy++;
                    }
                    else
                    {
                        gm.levelManager.ObjectiveOfPassLevel = 0;
                    }
                    break;
            }
            if (poolEnemy.count <= 0)
            {
                return;
            }
        }
    }
}