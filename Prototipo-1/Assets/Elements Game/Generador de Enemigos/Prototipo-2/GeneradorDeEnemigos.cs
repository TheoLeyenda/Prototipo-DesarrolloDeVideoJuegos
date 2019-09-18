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
            enemigoActual.pointOfCombat = pointOfCombat.transform.position;
            enemigoActual.pointOfDeath = pointOfInit.transform.position;
            enemigoActual.enumsEnemy.SetMovement(EnumsEnemy.Movimiento.MoveToPointCombat);
        }
        public void GenerateEnemy()
        {
            GameObject go = poolEnemy.GetObject();
            Enemy enemy = go.GetComponentInChildren<Enemy>();
            if (enemy != null)
            {
                enemigoActual = enemy;
                enemigoActual.life = enemigoActual.maxLife;
            }
            go.transform.position = Generador.transform.position;
            go.transform.rotation = Generador.transform.rotation;
            switch (gm.enumsGameManager.modoDeJuego)
            {
                case EnumsGameManager.ModosDeJuego.Supervivencia:
                    enemy.OnEnemySurvival();
                    break;
                case EnumsGameManager.ModosDeJuego.Historia:
                    enemy.OnEnemyHistory();
                    break;
            }
            if (poolEnemy.count <= 0)
            {
                return;
            }
            //go.transform.rotation = transform.rotation;
        }
    }
}