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
        public enum GeneratorForGameMode
        {
            Supervivencia,
            Historia,
            Nulo,
        }
        public LevelManager levelManager;
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
        public GeneratorForGameMode GeneradorDelModoDeJuego;
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
                    levelManager.ObjectiveOfPassLevel = TypeBossLevel.Count + 1;
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
            if (gm.enumsGameManager.modoDeJuego == EnumsGameManager.ModosDeJuego.Supervivencia)
            {
                enemigoActual.pointOfCombat = pointOfCombat.transform.position;
                enemigoActual.pointOfDeath = pointOfInit.transform.position;
                enemigoActual.enumsEnemy.SetMovement(EnumsEnemy.Movimiento.MoveToPointCombat);
            }
            else if (gm.enumsGameManager.modoDeJuego == EnumsGameManager.ModosDeJuego.Historia)
            {
                if (idListEnemy == 1)
                {
                    enemigoActual.ENEMY.transform.position = pointOfCombat.transform.position;
                    enemigoActual.enumsEnemy.SetMovement(EnumsEnemy.Movimiento.Nulo);
                }
                else
                {
                    enemigoActual.pointOfCombat = pointOfCombat.transform.position;
                    enemigoActual.pointOfDeath = pointOfInit.transform.position;
                    enemigoActual.enumsEnemy.SetMovement(EnumsEnemy.Movimiento.MoveToPointCombat);
                }
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
                            levelManager.SetInDialog(true);
                        }
                        idListEnemy++;
                    }
                    else
                    {
                        levelManager.ObjectiveOfPassLevel = 0;
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