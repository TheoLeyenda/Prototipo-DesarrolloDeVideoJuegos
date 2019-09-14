using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace Prototipo_2
{
    public class Enemy : MonoBehaviour
    {
        public GameObject enemyPrefab;
        public Grid gridEnemy;
        public EnumsEnemy enumsEnemy;
        public StructsEnemys structsEnemys;
        public SpriteRenderer SpriteRendererEnemigoBalanceado;
        public SpriteRenderer SpriteRendererEnemigoAgresivo;
        public SpriteRenderer SpriteRendererEnemigoDefensivo;
        public SpriteRenderer SpriteRendererJefeProfeAnatomia;
        public GameObject BARRA_DE_VIDA;
        private float auxLife;
        private Animator animator;
        public bool InPool;
        private PoolObject poolObjectEnemy;
        public float life;
        public float maxLife;
        public Image ImageHP;
        public Pool poolObjectAttack;
        public float SpeedJump;
        private Rigidbody2D rg2D;
        private GameManager gm;
        public List<GameObject> generadoresProyectiles;
        public List<GameObject> generadorProyectilesAgachado;
        private float DeffensePorcentage;
        private float AttackPorcentage;
        private float DodgePorcentage;
        private float AttackJumpPorcentage;
        private float AttackDuckPorcentage;
        private float AttackIdlePorcentage;
        private float AttackParabolaPorcentage;
        private float DefenceJumpPorcentage;
        private float DefenceDuckPorcentage;
        private float DefenceIdlePorcentage;
        private float MovePorcentage;
        private float MoveForwardPorcentage;
        private float MoveBackPorcentage;
        private float JumpPorcentage;
        private float DuckPorcentage;
        private float MinRangeRandom = 0;
        private float MaxRangeRandom = 100;
        private float TypeRandom = 3;
        private float delaySelectMovement;
        private bool doubleDamage;
        private bool isDuck;
        void Start()
        {
            delaySelectMovement = 0;
            auxLife = life;
            poolObjectEnemy = GetComponent<PoolObject>();
            animator = GetComponent<Animator>();
            //DisableShild();
            if (GameManager.instanceGameManager != null)
            {
                gm = GameManager.instanceGameManager;
            }
            rg2D = GetComponent<Rigidbody2D>();
            SetPorcentageMovements();
            CheckInitialSprite();
        }
        private void Update()
        {
            CheckLifeBar();
            CheckDead();
        }
        public void CheckInitialSprite()
        {
            if (enumsEnemy.typeEnemy != EnumsEnemy.TiposDeEnemigo.Jefe)
            {
                switch (enumsEnemy.typeEnemy)
                {
                    case EnumsEnemy.TiposDeEnemigo.Agresivo:
                        structsEnemys.dataEnemy.CantCasillasOcupadas_X = 1;
                        structsEnemys.dataEnemy.CantCasillasOcupadas_Y = 2;
                        structsEnemys.dataEnemy.columnaActual = 1;
                        SpriteRendererEnemigoAgresivo.gameObject.SetActive(true);
                        SpriteRendererEnemigoBalanceado.gameObject.SetActive(false);
                        SpriteRendererEnemigoDefensivo.gameObject.SetActive(false);
                        SpriteRendererJefeProfeAnatomia.gameObject.SetActive(false);
                        gridEnemy.CheckCuadrillaOcupada(structsEnemys.dataEnemy.columnaActual, structsEnemys.dataEnemy.CantCasillasOcupadas_X, structsEnemys.dataEnemy.CantCasillasOcupadas_Y);
                        break;
                    case EnumsEnemy.TiposDeEnemigo.Balanceado:
                        structsEnemys.dataEnemy.CantCasillasOcupadas_X = 1;
                        structsEnemys.dataEnemy.CantCasillasOcupadas_Y = 2;
                        structsEnemys.dataEnemy.columnaActual = 1;
                        SpriteRendererEnemigoBalanceado.gameObject.SetActive(true);
                        SpriteRendererEnemigoAgresivo.gameObject.SetActive(false);
                        SpriteRendererEnemigoDefensivo.gameObject.SetActive(false);
                        SpriteRendererJefeProfeAnatomia.gameObject.SetActive(false);
                        gridEnemy.CheckCuadrillaOcupada(structsEnemys.dataEnemy.columnaActual, structsEnemys.dataEnemy.CantCasillasOcupadas_X, structsEnemys.dataEnemy.CantCasillasOcupadas_Y);
                        break;
                    case EnumsEnemy.TiposDeEnemigo.Defensivo:
                        structsEnemys.dataEnemy.CantCasillasOcupadas_X = 1;
                        structsEnemys.dataEnemy.CantCasillasOcupadas_Y = 2;
                        structsEnemys.dataEnemy.columnaActual = 1;
                        SpriteRendererEnemigoDefensivo.gameObject.SetActive(true);
                        SpriteRendererEnemigoBalanceado.gameObject.SetActive(false);
                        SpriteRendererEnemigoAgresivo.gameObject.SetActive(false);
                        SpriteRendererJefeProfeAnatomia.gameObject.SetActive(false);
                        gridEnemy.CheckCuadrillaOcupada(structsEnemys.dataEnemy.columnaActual, structsEnemys.dataEnemy.CantCasillasOcupadas_X, structsEnemys.dataEnemy.CantCasillasOcupadas_Y);
                        break;
                }
            }
            else
            {
                switch (enumsEnemy.typeBoss)
                {
                    case EnumsEnemy.TiposDeJefe.ProfeAnatomia:
                        SpriteRendererEnemigoDefensivo.gameObject.SetActive(false);
                        SpriteRendererEnemigoBalanceado.gameObject.SetActive(false);
                        SpriteRendererEnemigoAgresivo.gameObject.SetActive(false);
                        SpriteRendererJefeProfeAnatomia.gameObject.SetActive(true);
                        break;
                }
            }
        }
        public void SetPorcentageMovements()
        {
            switch (enumsEnemy.typeEnemy)
            {
                //PANEL DE CONFIGURACION DE PORCENTAJES
                case EnumsEnemy.TiposDeEnemigo.Balanceado:
                    //----Movimiento----//
                    AttackPorcentage = 45;
                    DeffensePorcentage = 45;
                    DodgePorcentage = 10;
                    //----Objetivo Atacar----//
                    AttackJumpPorcentage = 25f;
                    AttackIdlePorcentage = 25f;
                    AttackDuckPorcentage = 25f;
                    AttackParabolaPorcentage = 25f;
                    //----Objetivo Defender----//
                    DefenceJumpPorcentage = 33.3f;
                    DefenceIdlePorcentage = 33.4f;
                    DefenceDuckPorcentage = 33.3f;
                    //----Esquivar Arriba/Abajo----//
                    JumpPorcentage = 50;
                    DuckPorcentage = 50;
                    break;
                case EnumsEnemy.TiposDeEnemigo.Agresivo:
                    //----Movimiento----//
                    AttackPorcentage = 80;
                    DeffensePorcentage = 20;
                    DodgePorcentage = 0;
                    //----Objetivo Atacar----//
                    AttackJumpPorcentage = 0;
                    AttackIdlePorcentage = 20;
                    AttackDuckPorcentage = 70;
                    AttackParabolaPorcentage = 10;
                    //----Objetivo Defender----//
                    DefenceJumpPorcentage = 33.3f;
                    DefenceIdlePorcentage = 33.4f;
                    DefenceDuckPorcentage = 33.3f;
                    //----Esquivar Arriba/Abajo----//
                    JumpPorcentage = 0;
                    DuckPorcentage = 0;
                    break;
                case EnumsEnemy.TiposDeEnemigo.Defensivo:
                    //---Movimiento---//
                    AttackPorcentage = 40;
                    DeffensePorcentage = 60;
                    DodgePorcentage = 0;
                    //----Objetivo Atacar----//
                    AttackJumpPorcentage = 100;
                    AttackIdlePorcentage = 0;
                    AttackDuckPorcentage = 0;
                    AttackParabolaPorcentage = 0;
                    //----Objetivo Defender----//
                    DefenceJumpPorcentage = 33.3f;
                    DefenceIdlePorcentage = 33.4f;
                    DefenceDuckPorcentage = 33.3f;
                    //----Esquivar Arriba/Abajo----//
                    JumpPorcentage = 0;
                    DuckPorcentage = 0;
                    break;
            }
        }
        public void OnEnemyHistory()
        {
            CheckInitialSprite();
        }
        public void OnEnemySurvival()
        {

            enumsEnemy.SetStateEnemy(EnumsEnemy.EstadoEnemigo.vivo);
            poolObjectEnemy = GetComponent<PoolObject>();
            float opcion = Random.Range(MinRangeRandom, TypeRandom);
            if (GameManager.instanceGameManager != null)
            {
                gm = GameManager.instanceGameManager;
            }
            Debug.Log("Enemigos Abatidos:" + gm.countEnemysDead);
            if ((gm.countEnemysDead % gm.RondasPorJefe) != 0 || gm.countEnemysDead == 0)
            {
                switch ((int)opcion)
                {
                    case 0:
                        //Debug.Log("ENTRE BALANCEADO");
                        //Cambiar el sprite del enemigo Balanceado.
                        enumsEnemy.typeEnemy = EnumsEnemy.TiposDeEnemigo.Balanceado;
                        break;
                    case 1:
                        //Debug.Log("ENTRE AGRESIVO");
                        //Cambiar el sprite del enemigo Agresivo.
                        enumsEnemy.typeEnemy = EnumsEnemy.TiposDeEnemigo.Agresivo;
                        break;
                    case 2:
                        //Debug.Log("ENTRE DEFENSIVO");
                        //Cambiar el sprite del enemigo Defensivo.
                        enumsEnemy.typeEnemy = EnumsEnemy.TiposDeEnemigo.Defensivo;
                        break;
                }
            }
            else if ((gm.countEnemysDead % gm.RondasPorJefe) == 0 && gm.countEnemysDead > 1)
            {
                //Cambiar el sprite del jefe correspondiente

                //PROGRAMAR UN RANDOM ENTRE LOS DISTINTOS JEFES
                Debug.Log("Soy tremendo jefe");
                enumsEnemy.typeEnemy = EnumsEnemy.TiposDeEnemigo.Jefe;
                enumsEnemy.typeBoss = EnumsEnemy.TiposDeJefe.ProfeAnatomia;
            }
            CheckInitialSprite();
            SetPorcentageMovements();
        }
        public void IA()
        {
            CheckMovement();
        }
        
        
        public void CheckLifeBar()
        {
            if (life <= maxLife)
            {
                ImageHP.fillAmount = life / maxLife;
            }
            else if (life > maxLife)
            {
                life = maxLife;
            }
            else if (life < 0)
            {
                life = 0;
            }
        }
        //INTERACTUA CON GAME MANAGER
        public void CheckDead()
        {
            if (!InPool)
            {
                if (life <= 0)
                {
                    // SI SU VIDA ES IGUAL A 0 POS MUERE DESACTIVADO
                    enumsEnemy.SetStateEnemy(EnumsEnemy.EstadoEnemigo.muerto);
                    enemyPrefab.gameObject.SetActive(false);
                    gm.countEnemysDead++;
                    gm.ResetRoundCombat(false);
                }
            }
            else if (InPool)
            {
                switch (gm.enumsGameManager.modoDeJuego)
                {
                    case EnumsGameManager.ModosDeJuego.Supervivencia:
                        if (life <= 0)
                        {
                            life = auxLife;
                            gm.generateEnemy = true;
                            poolObjectEnemy.Recycle();
                            enumsEnemy.SetStateEnemy(EnumsEnemy.EstadoEnemigo.muerto);
                            gm.countEnemysDead++;
                            gm.ResetRoundCombat(false);
                        }
                        break;
                    case EnumsGameManager.ModosDeJuego.Historia:
                        if (life <= 0)
                        {
                            life = auxLife;
                            gm.generateEnemy = true;
                            poolObjectEnemy.Recycle();
                            enumsEnemy.SetStateEnemy(EnumsEnemy.EstadoEnemigo.muerto);
                            gm.countEnemysDead++;
                            gm.ResetRoundCombat(false);
                        }
                        break;
                    case EnumsGameManager.ModosDeJuego.Nulo:
                        if (life <= 0)
                        {
                            enumsEnemy.SetStateEnemy(EnumsEnemy.EstadoEnemigo.muerto);
                            gameObject.SetActive(false);
                            gm.countEnemysDead++;
                            gm.ResetRoundCombat(false);
                        }
                        break;
                }
            }
        }


        public void CheckMovement()
        {

            //CHEKEA EL MOVIMIENTO DEL ENEMIGO

        }
        public void ResetEnemy()
        {
            //RESETEA TODO EL ENEMIGO
        }
        public void Attack()
        {
            string nombreGenerador = "NADA XD";
            GameObject generador = null;
            GameObject go = poolObjectAttack.GetObject();
            Proyectil proyectil = go.GetComponent<Proyectil>();
            proyectil.SetDobleDamage(doubleDamage);
            if (doubleDamage)
            {
                proyectil.damage = proyectil.damage * 2;
            }
            if (!isDuck)
            {
                if (enumsEnemy.typeEnemy != EnumsEnemy.TiposDeEnemigo.Jefe)
                {
                    switch (enumsEnemy.typeEnemy)
                    {
                        case EnumsEnemy.TiposDeEnemigo.Balanceado:
                            nombreGenerador = "GeneradorPelotasBalanceado";
                            break;
                        case EnumsEnemy.TiposDeEnemigo.Defensivo:
                            nombreGenerador = "GeneradorPelotasDefensivo";
                            break;
                        case EnumsEnemy.TiposDeEnemigo.Agresivo:
                            nombreGenerador = "GeneradorPelotasAgresivo";
                            break;
                    }
                }
                else
                {
                    switch (enumsEnemy.typeBoss)
                    {
                        case EnumsEnemy.TiposDeJefe.ProfeAnatomia:
                            nombreGenerador = "GeneradorPelotasProfeAnatomia";
                            break;
                        case EnumsEnemy.TiposDeJefe.ProfeHistoria:
                            nombreGenerador = "GeneradorPelotasProfeHistoria";
                            break;
                        case EnumsEnemy.TiposDeJefe.ProfeEducacionFisica:
                            nombreGenerador = "GeneradorPelotasProfeEducacionFisica";
                            break;
                        case EnumsEnemy.TiposDeJefe.ProfeArte:
                            nombreGenerador = "GeneradorPelotasProfeArte";
                            break;
                        case EnumsEnemy.TiposDeJefe.ProfeMatematica:
                            nombreGenerador = "GeneradorPelotasProfeMatematica";
                            break;
                        case EnumsEnemy.TiposDeJefe.ProfeQuimica:
                            nombreGenerador = "GeneradorPelotasProfeQuimica";
                            break;
                        case EnumsEnemy.TiposDeJefe.ProfeProgramacion:
                            nombreGenerador = "GeneradorPelotasProfeProgramacion";
                            break;
                        case EnumsEnemy.TiposDeJefe.ProfeBaretto:
                            nombreGenerador = "GeneradorPelotasProfeBaretto";
                            break;
                        case EnumsEnemy.TiposDeJefe.ProfeLautarito:
                            nombreGenerador = "GeneradorPelotasProfeLautaro";
                            break;

                            //UNA VEZ INCORPORADA LA PARTE DE LOS BOSESS INCORPORAR ESTA PARTE EN BASE A LA PARTE DE ARRIBA.
                    }
                }
                for (int i = 0; i < generadoresProyectiles.Count; i++)
                {
                    if (generadoresProyectiles[i].name == nombreGenerador)
                    {
                        generador = generadoresProyectiles[i];
                    }
                }
                if (generador != null)
                {
                    go.transform.position = generador.transform.position;
                }
            }
            else
            {
                switch (enumsEnemy.typeEnemy)
                {

                }
                switch (enumsEnemy.typeBoss)
                {
                    //UNA VEZ INCORPORADA LA PARTE DE LOS BOSESS INCORPORAR ESTA PARTE EN BASE A LA PARTE DE ARRIBA.
                }
            }
            proyectil.On();
            proyectil.ShootForward();
        }
        public void AttackParabola()
        {
            //ATACA A UN PUNTO DE ARRIBA DE LA CUADRILLA
        }
        public void CounterAttack(bool dobleDamage)
        {

            //DisableShild();
        }
        public void Deffense()
        {
            
        }
        public void Jump()
        {
           

        }
        public void Duck()
        {
            
        }
    }
}
