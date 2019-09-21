using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace Prototipo_2
{
    public class Enemy : MonoBehaviour
    {
        public List<SpriteEnemy> spriteEnemys;
        [HideInInspector]
        public SpriteEnemy spriteEnemyActual;
        public GameObject ENEMY;
        public GameObject enemyPrefab;
        public Grid gridEnemy;
        public EnumsEnemy enumsEnemy;
        public StructsEnemys structsEnemys;
        public SpriteRenderer SpriteRendererEnemigoBalanceado;
        public SpriteRenderer SpriteRendererEnemigoAgresivo;
        public SpriteRenderer SpriteRendererEnemigoDefensivo;
        public SpriteRenderer SpriteRendererJefeProfeAnatomia;
        public SpriteRenderer SpriteRendererJefeProfeHistoria;
        public SpriteRenderer SpriteRendererJefeProfeEducacionFisica;
        public SpriteRenderer SpriteRendererJefeProfeArte;
        public SpriteRenderer SpriteRendererJefeProfeMatematica;
        public SpriteRenderer SpriteRendererJefeProfeQuimica;
        public SpriteRenderer SpriteRendererJefeProfeProfeProgramacion;
        public SpriteRenderer SpriteRendererJefeProfeBaretto;
        public SpriteRenderer SpriteRendererJefeProfeLautarito;
        public SpecialAttackEnemyController specialAttackEnemyController;
        public GameObject BARRA_DE_VIDA;
        private float auxLife;
        private Animator animator;
        public bool InPool;
        private PoolObject poolObjectEnemy;
        public float life;
        public float maxLife;
        public Image ImageHP;
        public Pool poolObjectAttack;
        private Rigidbody2D rg2D;
        private GameManager gm;
        public List<GameObject> generadoresProyectiles;
        public List<GameObject> generadorProyectilesAgachado;
        public List<GameObject> generadorProyectilParabola;
        public List<GameObject> generadorProyectilParabolaAgachado;
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
        public float maxRandomDelayMovement;
        public float minRandomDelayMovement;
        public float delayAttack;
        public float pointsDeffence;
        private float auxDelayAttack;
        private bool doubleDamage;
        private bool isDuck;
        private bool isDeffended;
        public float anguloAtaqueSalto;
        public float Speed;
        public float SpeedJump;
        private float auxSpeedJump;
        public float Resistace;
        public float Gravity;
        public float delayAttackJumping;
        private bool isJamping;
        public List<Collider2D> collidersSprites;
        [HideInInspector]
        public Vector3 InitialPosition;
        [HideInInspector]
        public Vector3 pointOfDeath;
        [HideInInspector]
        public Vector3 pointOfCombat;
        //private float sumarAlturaInicial = 0.2f;
        void Start()
        {
            auxSpeedJump = SpeedJump;
            InitialPosition = transform.position;
            auxDelayAttack = delayAttack;
            delaySelectMovement = 0;
            auxLife = life;
            poolObjectEnemy = GetComponent<PoolObject>();
            animator = GetComponent<Animator>();
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
            IA();
            //Debug.Log(delaySelectMovement);
        }
        public void CheckInitialSprite()
        {
            //transform.position = new Vector3(transform.position.x, InitialPosition.y, transform.position.z);
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
                        SpriteRendererJefeProfeHistoria.gameObject.SetActive(false);
                        SpriteRendererJefeProfeEducacionFisica.gameObject.SetActive(false);
                        SpriteRendererJefeProfeArte.gameObject.SetActive(false);
                        SpriteRendererJefeProfeMatematica.gameObject.SetActive(false);
                        SpriteRendererJefeProfeQuimica.gameObject.SetActive(false);
                        SpriteRendererJefeProfeProfeProgramacion.gameObject.SetActive(false);
                        SpriteRendererJefeProfeBaretto.gameObject.SetActive(false);
                        SpriteRendererJefeProfeLautarito.gameObject.SetActive(false);

                        gridEnemy.CheckCuadrillaOcupada(structsEnemys.dataEnemy.columnaActual, structsEnemys.dataEnemy.CantCasillasOcupadas_X, structsEnemys.dataEnemy.CantCasillasOcupadas_Y);
                        break;
                    case EnumsEnemy.TiposDeEnemigo.Balanceado:
                        structsEnemys.dataEnemy.CantCasillasOcupadas_X = 1;
                        structsEnemys.dataEnemy.CantCasillasOcupadas_Y = 2;
                        structsEnemys.dataEnemy.columnaActual = 1;
                        SpriteRendererEnemigoAgresivo.gameObject.SetActive(false);
                        SpriteRendererEnemigoBalanceado.gameObject.SetActive(true);
                        SpriteRendererEnemigoDefensivo.gameObject.SetActive(false);
                        SpriteRendererJefeProfeAnatomia.gameObject.SetActive(false);
                        SpriteRendererJefeProfeHistoria.gameObject.SetActive(false);
                        SpriteRendererJefeProfeEducacionFisica.gameObject.SetActive(false);
                        SpriteRendererJefeProfeArte.gameObject.SetActive(false);
                        SpriteRendererJefeProfeMatematica.gameObject.SetActive(false);
                        SpriteRendererJefeProfeQuimica.gameObject.SetActive(false);
                        SpriteRendererJefeProfeProfeProgramacion.gameObject.SetActive(false);
                        SpriteRendererJefeProfeBaretto.gameObject.SetActive(false);
                        SpriteRendererJefeProfeLautarito.gameObject.SetActive(false);

                        gridEnemy.CheckCuadrillaOcupada(structsEnemys.dataEnemy.columnaActual, structsEnemys.dataEnemy.CantCasillasOcupadas_X, structsEnemys.dataEnemy.CantCasillasOcupadas_Y);
                        break;
                    case EnumsEnemy.TiposDeEnemigo.Defensivo:
                        structsEnemys.dataEnemy.CantCasillasOcupadas_X = 1;
                        structsEnemys.dataEnemy.CantCasillasOcupadas_Y = 2;
                        structsEnemys.dataEnemy.columnaActual = 1;
                        SpriteRendererEnemigoAgresivo.gameObject.SetActive(false);
                        SpriteRendererEnemigoBalanceado.gameObject.SetActive(false);
                        SpriteRendererEnemigoDefensivo.gameObject.SetActive(true);
                        SpriteRendererJefeProfeAnatomia.gameObject.SetActive(false);
                        SpriteRendererJefeProfeHistoria.gameObject.SetActive(false);
                        SpriteRendererJefeProfeEducacionFisica.gameObject.SetActive(false);
                        SpriteRendererJefeProfeArte.gameObject.SetActive(false);
                        SpriteRendererJefeProfeMatematica.gameObject.SetActive(false);
                        SpriteRendererJefeProfeQuimica.gameObject.SetActive(false);
                        SpriteRendererJefeProfeProfeProgramacion.gameObject.SetActive(false);
                        SpriteRendererJefeProfeBaretto.gameObject.SetActive(false);
                        SpriteRendererJefeProfeLautarito.gameObject.SetActive(false);

                        gridEnemy.CheckCuadrillaOcupada(structsEnemys.dataEnemy.columnaActual, structsEnemys.dataEnemy.CantCasillasOcupadas_X, structsEnemys.dataEnemy.CantCasillasOcupadas_Y);
                        break;
                }
            }
            else
            {
                switch (enumsEnemy.typeBoss)
                {
                    case EnumsEnemy.TiposDeJefe.ProfeAnatomia:
                        structsEnemys.dataEnemy.CantCasillasOcupadas_X = 2;
                        structsEnemys.dataEnemy.CantCasillasOcupadas_Y = 3;
                        structsEnemys.dataEnemy.columnaActual = 1;

                        SpriteRendererEnemigoAgresivo.gameObject.SetActive(false);
                        SpriteRendererEnemigoBalanceado.gameObject.SetActive(false);
                        SpriteRendererEnemigoDefensivo.gameObject.SetActive(false);
                        SpriteRendererJefeProfeAnatomia.gameObject.SetActive(true);
                        SpriteRendererJefeProfeHistoria.gameObject.SetActive(false);
                        SpriteRendererJefeProfeEducacionFisica.gameObject.SetActive(false);
                        SpriteRendererJefeProfeArte.gameObject.SetActive(false);
                        SpriteRendererJefeProfeMatematica.gameObject.SetActive(false);
                        SpriteRendererJefeProfeQuimica.gameObject.SetActive(false);
                        SpriteRendererJefeProfeProfeProgramacion.gameObject.SetActive(false);
                        SpriteRendererJefeProfeBaretto.gameObject.SetActive(false);
                        SpriteRendererJefeProfeLautarito.gameObject.SetActive(false);
                        gridEnemy.CheckCuadrillaOcupada(structsEnemys.dataEnemy.columnaActual, structsEnemys.dataEnemy.CantCasillasOcupadas_X, structsEnemys.dataEnemy.CantCasillasOcupadas_Y);
                        break;
                    case EnumsEnemy.TiposDeJefe.ProfeHistoria:
                        structsEnemys.dataEnemy.CantCasillasOcupadas_X = 2;
                        structsEnemys.dataEnemy.CantCasillasOcupadas_Y = 3;
                        structsEnemys.dataEnemy.columnaActual = 1;

                        SpriteRendererEnemigoAgresivo.gameObject.SetActive(false);
                        SpriteRendererEnemigoBalanceado.gameObject.SetActive(false);
                        SpriteRendererEnemigoDefensivo.gameObject.SetActive(false);
                        SpriteRendererJefeProfeAnatomia.gameObject.SetActive(false);
                        SpriteRendererJefeProfeHistoria.gameObject.SetActive(true);
                        SpriteRendererJefeProfeEducacionFisica.gameObject.SetActive(false);
                        SpriteRendererJefeProfeArte.gameObject.SetActive(false);
                        SpriteRendererJefeProfeMatematica.gameObject.SetActive(false);
                        SpriteRendererJefeProfeQuimica.gameObject.SetActive(false);
                        SpriteRendererJefeProfeProfeProgramacion.gameObject.SetActive(false);
                        SpriteRendererJefeProfeBaretto.gameObject.SetActive(false);
                        SpriteRendererJefeProfeLautarito.gameObject.SetActive(false);
                        gridEnemy.CheckCuadrillaOcupada(structsEnemys.dataEnemy.columnaActual, structsEnemys.dataEnemy.CantCasillasOcupadas_X, structsEnemys.dataEnemy.CantCasillasOcupadas_Y);
                        break;
                    case EnumsEnemy.TiposDeJefe.ProfeEducacionFisica:
                        structsEnemys.dataEnemy.CantCasillasOcupadas_X = 2;
                        structsEnemys.dataEnemy.CantCasillasOcupadas_Y = 3;
                        structsEnemys.dataEnemy.columnaActual = 1;

                        SpriteRendererEnemigoAgresivo.gameObject.SetActive(false);
                        SpriteRendererEnemigoBalanceado.gameObject.SetActive(false);
                        SpriteRendererEnemigoDefensivo.gameObject.SetActive(false);
                        SpriteRendererJefeProfeAnatomia.gameObject.SetActive(false);
                        SpriteRendererJefeProfeHistoria.gameObject.SetActive(false);
                        SpriteRendererJefeProfeEducacionFisica.gameObject.SetActive(true);
                        SpriteRendererJefeProfeArte.gameObject.SetActive(false);
                        SpriteRendererJefeProfeMatematica.gameObject.SetActive(false);
                        SpriteRendererJefeProfeQuimica.gameObject.SetActive(false);
                        SpriteRendererJefeProfeProfeProgramacion.gameObject.SetActive(false);
                        SpriteRendererJefeProfeBaretto.gameObject.SetActive(false);
                        SpriteRendererJefeProfeLautarito.gameObject.SetActive(false);
                        gridEnemy.CheckCuadrillaOcupada(structsEnemys.dataEnemy.columnaActual, structsEnemys.dataEnemy.CantCasillasOcupadas_X, structsEnemys.dataEnemy.CantCasillasOcupadas_Y);
                        break;
                    case EnumsEnemy.TiposDeJefe.ProfeArte:
                        structsEnemys.dataEnemy.CantCasillasOcupadas_X = 2;
                        structsEnemys.dataEnemy.CantCasillasOcupadas_Y = 3;
                        structsEnemys.dataEnemy.columnaActual = 1;

                        SpriteRendererEnemigoAgresivo.gameObject.SetActive(false);
                        SpriteRendererEnemigoBalanceado.gameObject.SetActive(false);
                        SpriteRendererEnemigoDefensivo.gameObject.SetActive(false);
                        SpriteRendererJefeProfeAnatomia.gameObject.SetActive(false);
                        SpriteRendererJefeProfeHistoria.gameObject.SetActive(false);
                        SpriteRendererJefeProfeEducacionFisica.gameObject.SetActive(false);
                        SpriteRendererJefeProfeArte.gameObject.SetActive(true);
                        SpriteRendererJefeProfeMatematica.gameObject.SetActive(false);
                        SpriteRendererJefeProfeQuimica.gameObject.SetActive(false);
                        SpriteRendererJefeProfeProfeProgramacion.gameObject.SetActive(false);
                        SpriteRendererJefeProfeBaretto.gameObject.SetActive(false);
                        SpriteRendererJefeProfeLautarito.gameObject.SetActive(false);
                        gridEnemy.CheckCuadrillaOcupada(structsEnemys.dataEnemy.columnaActual, structsEnemys.dataEnemy.CantCasillasOcupadas_X, structsEnemys.dataEnemy.CantCasillasOcupadas_Y);
                        break;
                    case EnumsEnemy.TiposDeJefe.ProfeMatematica:
                        structsEnemys.dataEnemy.CantCasillasOcupadas_X = 2;
                        structsEnemys.dataEnemy.CantCasillasOcupadas_Y = 3;
                        structsEnemys.dataEnemy.columnaActual = 1;

                        SpriteRendererEnemigoAgresivo.gameObject.SetActive(false);
                        SpriteRendererEnemigoBalanceado.gameObject.SetActive(false);
                        SpriteRendererEnemigoDefensivo.gameObject.SetActive(false);
                        SpriteRendererJefeProfeAnatomia.gameObject.SetActive(false);
                        SpriteRendererJefeProfeHistoria.gameObject.SetActive(false);
                        SpriteRendererJefeProfeEducacionFisica.gameObject.SetActive(false);
                        SpriteRendererJefeProfeArte.gameObject.SetActive(false);
                        SpriteRendererJefeProfeMatematica.gameObject.SetActive(true);
                        SpriteRendererJefeProfeQuimica.gameObject.SetActive(false);
                        SpriteRendererJefeProfeProfeProgramacion.gameObject.SetActive(false);
                        SpriteRendererJefeProfeBaretto.gameObject.SetActive(false);
                        SpriteRendererJefeProfeLautarito.gameObject.SetActive(false);
                        gridEnemy.CheckCuadrillaOcupada(structsEnemys.dataEnemy.columnaActual, structsEnemys.dataEnemy.CantCasillasOcupadas_X, structsEnemys.dataEnemy.CantCasillasOcupadas_Y);
                        break;
                    case EnumsEnemy.TiposDeJefe.ProfeQuimica:
                        structsEnemys.dataEnemy.CantCasillasOcupadas_X = 2;
                        structsEnemys.dataEnemy.CantCasillasOcupadas_Y = 3;
                        structsEnemys.dataEnemy.columnaActual = 1;

                        SpriteRendererEnemigoAgresivo.gameObject.SetActive(false);
                        SpriteRendererEnemigoBalanceado.gameObject.SetActive(false);
                        SpriteRendererEnemigoDefensivo.gameObject.SetActive(false);
                        SpriteRendererJefeProfeAnatomia.gameObject.SetActive(false);
                        SpriteRendererJefeProfeHistoria.gameObject.SetActive(false);
                        SpriteRendererJefeProfeEducacionFisica.gameObject.SetActive(false);
                        SpriteRendererJefeProfeArte.gameObject.SetActive(false);
                        SpriteRendererJefeProfeMatematica.gameObject.SetActive(false);
                        SpriteRendererJefeProfeQuimica.gameObject.SetActive(true);
                        SpriteRendererJefeProfeProfeProgramacion.gameObject.SetActive(false);
                        SpriteRendererJefeProfeBaretto.gameObject.SetActive(false);
                        SpriteRendererJefeProfeLautarito.gameObject.SetActive(false);
                        gridEnemy.CheckCuadrillaOcupada(structsEnemys.dataEnemy.columnaActual, structsEnemys.dataEnemy.CantCasillasOcupadas_X, structsEnemys.dataEnemy.CantCasillasOcupadas_Y);
                        break;
                    case EnumsEnemy.TiposDeJefe.ProfeProgramacion:
                        structsEnemys.dataEnemy.CantCasillasOcupadas_X = 2;
                        structsEnemys.dataEnemy.CantCasillasOcupadas_Y = 3;
                        structsEnemys.dataEnemy.columnaActual = 1;

                        SpriteRendererEnemigoAgresivo.gameObject.SetActive(false);
                        SpriteRendererEnemigoBalanceado.gameObject.SetActive(false);
                        SpriteRendererEnemigoDefensivo.gameObject.SetActive(false);
                        SpriteRendererJefeProfeAnatomia.gameObject.SetActive(false);
                        SpriteRendererJefeProfeHistoria.gameObject.SetActive(false);
                        SpriteRendererJefeProfeEducacionFisica.gameObject.SetActive(false);
                        SpriteRendererJefeProfeArte.gameObject.SetActive(false);
                        SpriteRendererJefeProfeMatematica.gameObject.SetActive(false);
                        SpriteRendererJefeProfeQuimica.gameObject.SetActive(false);
                        SpriteRendererJefeProfeProfeProgramacion.gameObject.SetActive(true);
                        SpriteRendererJefeProfeBaretto.gameObject.SetActive(false);
                        SpriteRendererJefeProfeLautarito.gameObject.SetActive(false);
                        gridEnemy.CheckCuadrillaOcupada(structsEnemys.dataEnemy.columnaActual, structsEnemys.dataEnemy.CantCasillasOcupadas_X, structsEnemys.dataEnemy.CantCasillasOcupadas_Y);
                        break;
                    case EnumsEnemy.TiposDeJefe.ProfeBaretto:
                        structsEnemys.dataEnemy.CantCasillasOcupadas_X = 2;
                        structsEnemys.dataEnemy.CantCasillasOcupadas_Y = 3;
                        structsEnemys.dataEnemy.columnaActual = 1;

                        SpriteRendererEnemigoAgresivo.gameObject.SetActive(false);
                        SpriteRendererEnemigoBalanceado.gameObject.SetActive(false);
                        SpriteRendererEnemigoDefensivo.gameObject.SetActive(false);
                        SpriteRendererJefeProfeAnatomia.gameObject.SetActive(false);
                        SpriteRendererJefeProfeHistoria.gameObject.SetActive(false);
                        SpriteRendererJefeProfeEducacionFisica.gameObject.SetActive(false);
                        SpriteRendererJefeProfeArte.gameObject.SetActive(false);
                        SpriteRendererJefeProfeMatematica.gameObject.SetActive(false);
                        SpriteRendererJefeProfeQuimica.gameObject.SetActive(false);
                        SpriteRendererJefeProfeProfeProgramacion.gameObject.SetActive(false);
                        SpriteRendererJefeProfeBaretto.gameObject.SetActive(true);
                        SpriteRendererJefeProfeLautarito.gameObject.SetActive(false);
                        gridEnemy.CheckCuadrillaOcupada(structsEnemys.dataEnemy.columnaActual, structsEnemys.dataEnemy.CantCasillasOcupadas_X, structsEnemys.dataEnemy.CantCasillasOcupadas_Y);
                        break;
                    case EnumsEnemy.TiposDeJefe.ProfeLautarito:
                        structsEnemys.dataEnemy.CantCasillasOcupadas_X = 2;
                        structsEnemys.dataEnemy.CantCasillasOcupadas_Y = 3;
                        structsEnemys.dataEnemy.columnaActual = 1;

                        SpriteRendererEnemigoAgresivo.gameObject.SetActive(false);
                        SpriteRendererEnemigoBalanceado.gameObject.SetActive(false);
                        SpriteRendererEnemigoDefensivo.gameObject.SetActive(false);
                        SpriteRendererJefeProfeAnatomia.gameObject.SetActive(false);
                        SpriteRendererJefeProfeHistoria.gameObject.SetActive(false);
                        SpriteRendererJefeProfeEducacionFisica.gameObject.SetActive(false);
                        SpriteRendererJefeProfeArte.gameObject.SetActive(false);
                        SpriteRendererJefeProfeMatematica.gameObject.SetActive(false);
                        SpriteRendererJefeProfeQuimica.gameObject.SetActive(false);
                        SpriteRendererJefeProfeProfeProgramacion.gameObject.SetActive(false);
                        SpriteRendererJefeProfeBaretto.gameObject.SetActive(false);
                        SpriteRendererJefeProfeLautarito.gameObject.SetActive(true);
                        gridEnemy.CheckCuadrillaOcupada(structsEnemys.dataEnemy.columnaActual, structsEnemys.dataEnemy.CantCasillasOcupadas_X, structsEnemys.dataEnemy.CantCasillasOcupadas_Y);
                        break;
                }
            }
            CheckSpriteEnemyActual();
        }
        public void CheckSpriteEnemyActual()
        {
            for (int i = 0; i < spriteEnemys.Count; i++)
            {
                if (spriteEnemys[i].gameObject.activeSelf)
                {
                    spriteEnemyActual = spriteEnemys[i];
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
                    enumsEnemy.typeBoss = EnumsEnemy.TiposDeJefe.Nulo;
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
                    enumsEnemy.typeBoss = EnumsEnemy.TiposDeJefe.Nulo;
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
                    enumsEnemy.typeBoss = EnumsEnemy.TiposDeJefe.Nulo;
                    break;
            }
        }
        public void OnEnemyHistory(EnumsEnemy.TiposDeEnemigo typeEnemy, EnumsEnemy.TiposDeJefe typeBoss)
        {
            CheckSpriteEnemyActual();
            enumsEnemy.typeEnemy = typeEnemy;
            enumsEnemy.typeBoss = typeBoss;
            CheckInitialSprite();
            poolObjectEnemy = GetComponent<PoolObject>();
            if (GameManager.instanceGameManager != null)
            {
                gm = GameManager.instanceGameManager;
            }
            SetPorcentageMovements();

        }
        public void CheckOutLimit()
        {
            if (transform.position.y < InitialPosition.y && !isJamping)
            {
                //Debug.Log("ENTRE A LA INICIAL POSICION");
                transform.position = new Vector3(transform.position.x, InitialPosition.y, transform.position.z);
                delaySelectMovement = 0;
                enumsEnemy.SetMovement(EnumsEnemy.Movimiento.Nulo);
                gridEnemy.CheckCuadrillaOcupada(structsEnemys.dataEnemy.columnaActual, structsEnemys.dataEnemy.CantCasillasOcupadas_X, structsEnemys.dataEnemy.CantCasillasOcupadas_Y);
                SpeedJump = auxSpeedJump;
            }
        }
        public void OnEnemySurvival()
        {
            CheckSpriteEnemyActual();
            enumsEnemy.SetStateEnemy(EnumsEnemy.EstadoEnemigo.vivo);
            poolObjectEnemy = GetComponent<PoolObject>();
            float opcion = Random.Range(MinRangeRandom, TypeRandom);
            if (GameManager.instanceGameManager != null)
            {
                gm = GameManager.instanceGameManager;
            }
            //Debug.Log("Enemigos Abatidos:" + gm.countEnemysDead);
            if ((gm.countEnemysDead % gm.RondasPorJefe) != 0 || gm.countEnemysDead == 0)
            {
                switch ((int)opcion)
                {
                    case 0:
                        //Debug.Log("ENTRE BALANCEADO");
                        enumsEnemy.typeEnemy = EnumsEnemy.TiposDeEnemigo.Balanceado;
                        break;
                    case 1:
                        //Debug.Log("ENTRE AGRESIVO");
                        enumsEnemy.typeEnemy = EnumsEnemy.TiposDeEnemigo.Agresivo;
                        break;
                    case 2:
                        //Debug.Log("ENTRE DEFENSIVO");
                        enumsEnemy.typeEnemy = EnumsEnemy.TiposDeEnemigo.Defensivo;
                        break;
                }
            }
            else if ((gm.countEnemysDead % gm.RondasPorJefe) == 0 && gm.countEnemysDead > 1)
            {
                //Cambiar el sprite del jefe correspondiente
                //PROGRAMAR UN RANDOM ENTRE LOS DISTINTOS JEFES
                enumsEnemy.typeEnemy = EnumsEnemy.TiposDeEnemigo.Jefe;
                opcion = (int)Random.Range(0, (int)EnumsEnemy.TiposDeJefe.Count - 1);
                switch ((EnumsEnemy.TiposDeJefe)opcion)
                {
                    case EnumsEnemy.TiposDeJefe.ProfeAnatomia:
                        enumsEnemy.typeBoss = EnumsEnemy.TiposDeJefe.ProfeAnatomia;
                        break;
                    case EnumsEnemy.TiposDeJefe.ProfeHistoria:
                        enumsEnemy.typeBoss = EnumsEnemy.TiposDeJefe.ProfeHistoria;
                        break;
                    case EnumsEnemy.TiposDeJefe.ProfeEducacionFisica:
                        enumsEnemy.typeBoss = EnumsEnemy.TiposDeJefe.ProfeEducacionFisica;
                        break;
                    case EnumsEnemy.TiposDeJefe.ProfeArte:
                        enumsEnemy.typeBoss = EnumsEnemy.TiposDeJefe.ProfeArte;
                        break;
                    case EnumsEnemy.TiposDeJefe.ProfeMatematica:
                        enumsEnemy.typeBoss = EnumsEnemy.TiposDeJefe.ProfeMatematica;
                        break;
                    case EnumsEnemy.TiposDeJefe.ProfeQuimica:
                        enumsEnemy.typeBoss = EnumsEnemy.TiposDeJefe.ProfeQuimica;
                        break;
                    case EnumsEnemy.TiposDeJefe.ProfeProgramacion:
                        enumsEnemy.typeBoss = EnumsEnemy.TiposDeJefe.ProfeProgramacion;
                        break;
                    case EnumsEnemy.TiposDeJefe.ProfeBaretto:
                        enumsEnemy.typeBoss = EnumsEnemy.TiposDeJefe.ProfeBaretto;
                        break;
                    case EnumsEnemy.TiposDeJefe.ProfeLautarito:
                        enumsEnemy.typeBoss = EnumsEnemy.TiposDeJefe.ProfeLautarito;
                        break;
                }
                Debug.Log("Soy tremendo jefe");
            }
            CheckInitialSprite();
            SetPorcentageMovements();
        }
        public void IA()
        {
            if (life > 0)
            {
                if (delaySelectMovement <= 0 && (enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.Saltar || enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.SaltoAtaque))
                {
                    EnumsEnemy.Movimiento movimiento;
                    gridEnemy.CheckCuadrillaOcupada(structsEnemys.dataEnemy.columnaActual, structsEnemys.dataEnemy.CantCasillasOcupadas_X, structsEnemys.dataEnemy.CantCasillasOcupadas_Y);
                    int min = (int)EnumsEnemy.Movimiento.Nulo + 1;
                    int max = (int)EnumsEnemy.Movimiento.Count - 3;
                    if (enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.MoveToPointCombat && enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.MoveToPointDeath)
                    {
                        movimiento = (EnumsEnemy.Movimiento)Random.Range(min, max);
                    }
                    else
                    {
                        movimiento = enumsEnemy.GetMovement();
                    }
                    delaySelectMovement = Random.Range(minRandomDelayMovement, maxRandomDelayMovement);
                    enumsEnemy.SetMovement(movimiento);
                    Debug.Log(movimiento.ToString());
                    if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.SaltoAtaque || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AtacarEnParabolaSaltando)
                    {
                        delayAttack = delayAttackJumping;
                    }
                    if (enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.SaltoDefensa || enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.DefensaEnElLugar)
                    {
                        isDeffended = false;
                    }
                }
                if (delaySelectMovement > 0)
                {
                    CheckMovement();
                    delaySelectMovement = delaySelectMovement - Time.deltaTime;
                }
            }
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
        public void MoveToPoint(Vector3 pointCombat)
        {
            if (CheckMove(pointCombat))
            {
                delaySelectMovement = 999;
                if (pointCombat.x < transform.position.x)
                {
                    ENEMY.transform.Translate(Vector3.left * Speed * Time.deltaTime);
                }
                else if (pointCombat.x > transform.position.x)
                {
                    ENEMY.transform.Translate(Vector3.right * Speed * Time.deltaTime);
                }
            }
            else
            {
                delaySelectMovement = 0;
                enumsEnemy.SetMovement(EnumsEnemy.Movimiento.Nulo);
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
                    gm.countEnemysDead++;
                    gm.ResetRoundCombat(false);
                    ResetEnemy();
                    enemyPrefab.gameObject.SetActive(false);

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
                            enumsEnemy.SetStateEnemy(EnumsEnemy.EstadoEnemigo.muerto);
                            gm.countEnemysDead++;
                            gm.ResetRoundCombat(false);
                            ResetEnemy();
                            poolObjectEnemy.Recycle();
                        }
                        break;
                    case EnumsGameManager.ModosDeJuego.Historia:
                        if (life <= 0)
                        {
                            life = auxLife;
                            gm.generateEnemy = true;
                            enumsEnemy.SetStateEnemy(EnumsEnemy.EstadoEnemigo.muerto);
                            gm.countEnemysDead++;
                            gm.ResetRoundCombat(false);
                            ResetEnemy();
                            poolObjectEnemy.Recycle();
                        }
                        break;
                    case EnumsGameManager.ModosDeJuego.Nulo:
                        if (life <= 0)
                        {
                            gm.countEnemysDead++;
                            gm.ResetRoundCombat(false);
                            ResetEnemy();
                            enumsEnemy.SetStateEnemy(EnumsEnemy.EstadoEnemigo.muerto);
                            gameObject.SetActive(false);
                        }
                        break;
                }
            }
        }
        public void CheckMovement()
        {
            switch (enumsEnemy.GetMovement())
            {
                case EnumsEnemy.Movimiento.AtacarEnElLugar:
                    CheckDelayAttack(false);
                    break;
                case EnumsEnemy.Movimiento.AgacharseAtaque:
                    Duck(structsEnemys.dataEnemy.CantCasillasOcupadas_Y);
                    CheckDelayAttack(false);
                    break;
                case EnumsEnemy.Movimiento.SaltoAtaque:
                    CheckDelayAttack(false);
                    isJamping = true;
                    Jump(gridEnemy.matrizCuadrilla[0][structsEnemys.dataEnemy.columnaActual].transform.position);
                    break;
                case EnumsEnemy.Movimiento.MoverAtras:
                    if (structsEnemys.dataEnemy.columnaActual < gridEnemy.GetCuadrilla_columnas() - 1)
                    {
                        MoveRight(gridEnemy.matrizCuadrilla[gridEnemy.baseGrild][structsEnemys.dataEnemy.columnaActual + 1].transform.position);
                    }
                    else
                    {
                        delaySelectMovement = 0;
                    }
                    break;
                case EnumsEnemy.Movimiento.MoverAdelante:
                    if (structsEnemys.dataEnemy.columnaActual > 0)
                    {
                        MoveLeft(gridEnemy.matrizCuadrilla[gridEnemy.baseGrild][structsEnemys.dataEnemy.columnaActual - 1].transform.position);
                    }
                    else
                    {
                        delaySelectMovement = 0;
                    }
                    break;
                case EnumsEnemy.Movimiento.Saltar:
                    isJamping = true;
                    Jump(gridEnemy.matrizCuadrilla[0][structsEnemys.dataEnemy.columnaActual].transform.position);
                    break;
                case EnumsEnemy.Movimiento.DefensaEnElLugar:
                    Deffence();
                    break;
                case EnumsEnemy.Movimiento.SaltoDefensa:
                    isJamping = true;
                    Jump(gridEnemy.matrizCuadrilla[0][structsEnemys.dataEnemy.columnaActual].transform.position);
                    Deffence();
                    break;
                case EnumsEnemy.Movimiento.AgacheDefensa:
                    Duck(structsEnemys.dataEnemy.CantCasillasOcupadas_Y);
                    Deffence();
                    break;
                case EnumsEnemy.Movimiento.Agacharse:
                    Duck(structsEnemys.dataEnemy.CantCasillasOcupadas_Y);
                    break;
                case EnumsEnemy.Movimiento.AtacarEnParabola:
                    CheckDelayAttack(true);
                    break;
                case EnumsEnemy.Movimiento.AtacarEnParabolaAgachado:
                    Duck(structsEnemys.dataEnemy.CantCasillasOcupadas_Y);
                    CheckDelayAttack(true);
                    break;
                case EnumsEnemy.Movimiento.AtacarEnParabolaSaltando:
                    CheckDelayAttack(true);
                    isJamping = true;
                    Jump(gridEnemy.matrizCuadrilla[0][structsEnemys.dataEnemy.columnaActual].transform.position);
                    break;
                case EnumsEnemy.Movimiento.MoveToPointCombat:
                    MoveToPoint(pointOfCombat);
                    break;
                case EnumsEnemy.Movimiento.MoveToPointDeath:
                    MoveToPoint(pointOfDeath);
                    break;
            }
            if (enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.AgacharseAtaque)
            {
                for (int i = 0; i < collidersSprites.Count; i++)
                {
                    collidersSprites[i].enabled = true;
                }
                isDuck = false;
            }
            //CHEKEA EL MOVIMIENTO DEL ENEMIGO
        }
        public void Deffence()
        {
            isDeffended = true;
            for (int i = 0; i < gridEnemy.matrizCuadrilla.Count; i++)
            {
                for (int j = 0; j < gridEnemy.matrizCuadrilla[i].Count; j++)
                {
                    if (gridEnemy.matrizCuadrilla[i][j].GetStateCuadrilla() == Cuadrilla.StateCuadrilla.Ocupado)
                    {
                        gridEnemy.matrizCuadrilla[i][j].SetStateCuadrilla(Cuadrilla.StateCuadrilla.Defendido);
                    }
                }
            }
        }
        public void CheckDelayAttack(bool specialAttack)
        {
            if (delayAttack > 0)
            {
                delayAttack = delayAttack - Time.deltaTime;
            }
            else if (delayAttack <= 0)
            {
                delayAttack = auxDelayAttack;
                if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.SaltoAtaque || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.Nulo || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AtacarEnParabolaSaltando)
                {
                    delayAttack = delayAttackJumping;
                    Attack(true, specialAttack,false);
                }
                else
                {
                    Attack(false, specialAttack,false);
                }
            }
        }
        public void ResetEnemy()
        {
            life = maxLife;
            transform.position = InitialPosition;
            ENEMY.transform.position = InitialPosition;
        }
        public void MoveLeft(Vector3 cuadrillaDestino)
        {
            if (CheckMove(new Vector3(gridEnemy.leftCuadrilla.transform.position.x, transform.position.y, transform.position.z)) && transform.position.x > cuadrillaDestino.x)
            {
                Move(Vector3.left);
                enumsEnemy.SetMovement(EnumsEnemy.Movimiento.MoverAdelante);
            }
            else if (enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.Nulo)
            {
                structsEnemys.dataEnemy.columnaActual--;
                enumsEnemy.SetMovement(EnumsEnemy.Movimiento.Nulo);
                gridEnemy.CheckCuadrillaOcupada(structsEnemys.dataEnemy.columnaActual, structsEnemys.dataEnemy.CantCasillasOcupadas_X, structsEnemys.dataEnemy.CantCasillasOcupadas_Y);
                delaySelectMovement = 0;
            }
        }
        public void MoveRight(Vector3 cuadrillaDestino)
        {
            if (CheckMove(new Vector3(gridEnemy.rightCuadrilla.transform.position.x, transform.position.y, transform.position.z)) && transform.position.x < cuadrillaDestino.x)
            {
                Move(Vector3.right);
                enumsEnemy.SetMovement(EnumsEnemy.Movimiento.MoverAtras);
            }
            else if (enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.Nulo)
            {
                structsEnemys.dataEnemy.columnaActual++;
                enumsEnemy.SetMovement(EnumsEnemy.Movimiento.Nulo);
                gridEnemy.CheckCuadrillaOcupada(structsEnemys.dataEnemy.columnaActual, structsEnemys.dataEnemy.CantCasillasOcupadas_X, structsEnemys.dataEnemy.CantCasillasOcupadas_Y);
                delaySelectMovement = 0;
            }
        }
        public void Attack(bool jampAttack, bool specialAttack, bool _doubleDamage)
        {
            bool shootDown = false;
            string nombreGenerador = "NADA XD";
            GameObject generador = null;
            GameObject go = null;
            Proyectil proyectil = null;
            if (!specialAttack)
            {
                go = poolObjectAttack.GetObject();
                proyectil = go.GetComponent<Proyectil>();
                proyectil.SetDobleDamage(_doubleDamage);
                proyectil.disparadorDelProyectil = Proyectil.DisparadorDelProyectil.Enemigo;

                if (_doubleDamage)
                {
                    proyectil.damage = proyectil.damage * 2;
                }
            }
            if (!isDuck && !specialAttack)
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
                    if (jampAttack)
                    {
                        shootDown = true;
                    }
                    go.transform.rotation = generador.transform.rotation;
                    go.transform.position = generador.transform.position;
                }
            }
            else if(!specialAttack && isDuck)
            {
                if (enumsEnemy.typeEnemy != EnumsEnemy.TiposDeEnemigo.Jefe)
                {
                    switch (enumsEnemy.typeEnemy)
                    {
                        case EnumsEnemy.TiposDeEnemigo.Balanceado:
                            nombreGenerador = "GeneradorPelotasAgachadoBalanceado";
                            break;
                        case EnumsEnemy.TiposDeEnemigo.Defensivo:
                            nombreGenerador = "GeneradorPelotasAgachadoDefensivo";
                            break;
                        case EnumsEnemy.TiposDeEnemigo.Agresivo:
                            nombreGenerador = "GeneradorPelotasAgachadoAgresivo";
                            break;
                    }
                }
                else
                {
                    switch (enumsEnemy.typeBoss)
                    {
                        case EnumsEnemy.TiposDeJefe.ProfeAnatomia:
                            nombreGenerador = "GeneradorPelotasAgachadoProfeAnatomia";
                            break;
                        case EnumsEnemy.TiposDeJefe.ProfeHistoria:
                            nombreGenerador = "GeneradorPelotasAgachadoProfeHistoria";
                            break;
                        case EnumsEnemy.TiposDeJefe.ProfeEducacionFisica:
                            nombreGenerador = "GeneradorPelotasAgachadoProfeEducacionFisica";
                            break;
                        case EnumsEnemy.TiposDeJefe.ProfeArte:
                            nombreGenerador = "GeneradorPelotasAgachadoProfeArte";
                            break;
                        case EnumsEnemy.TiposDeJefe.ProfeMatematica:
                            nombreGenerador = "GeneradorPelotasAgachadoProfeMatematica";
                            break;
                        case EnumsEnemy.TiposDeJefe.ProfeQuimica:
                            nombreGenerador = "GeneradorPelotasAgachadoProfeQuimica";
                            break;
                        case EnumsEnemy.TiposDeJefe.ProfeProgramacion:
                            nombreGenerador = "GeneradorPelotasAgachadoProfeProgramacion";
                            break;
                        case EnumsEnemy.TiposDeJefe.ProfeBaretto:
                            nombreGenerador = "GeneradorPelotasAgachadoProfeBaretto";
                            break;
                        case EnumsEnemy.TiposDeJefe.ProfeLautarito:
                            nombreGenerador = "GeneradorPelotasAgachadoProfeLautaro";
                            break;
                    }
                }
                for (int i = 0; i < generadorProyectilesAgachado.Count; i++)
                {
                    if (generadorProyectilesAgachado[i].name == nombreGenerador)
                    {
                        generador = generadorProyectilesAgachado[i];
                    }
                }
                if (generador != null)
                {
                    go.transform.rotation = generador.transform.rotation;
                    go.transform.position = generador.transform.position;
                }
            }
            if (specialAttack)
            {
                if (enumsEnemy.typeEnemy != EnumsEnemy.TiposDeEnemigo.Jefe)
                {
                    switch (enumsEnemy.typeEnemy)
                    {
                        case EnumsEnemy.TiposDeEnemigo.Balanceado:
                            if (!isDuck)
                            {
                                nombreGenerador = "GeneradorPelotasParabolaBalanceado";
                            }
                            else
                            {
                                nombreGenerador = "GeneradorPelotasParabolaAgachadoBalanceado";
                            }
                            
                            break;
                        case EnumsEnemy.TiposDeEnemigo.Defensivo:
                            if (!isDuck)
                            {
                                nombreGenerador = "GeneradorPelotasParabolaDefencivo";
                            }
                            else
                            {
                                nombreGenerador = "GeneradorPelotasParabolaAgachadoDefensivo";
                            }
                            break;
                        case EnumsEnemy.TiposDeEnemigo.Agresivo:
                            if (!isDuck)
                            {
                                nombreGenerador = "GeneradorPelotasParabolaAgresivo";
                            }
                            else
                            {
                                nombreGenerador = "GeneradorPelotasParabolaAgachadoAgresivo";
                            }
                            break;
                    }
                    CheckSpecialAttackEnemyController(0, 0, nombreGenerador, generador);
                }
                else
                {
                    switch (enumsEnemy.typeBoss)
                    {
                        case EnumsEnemy.TiposDeJefe.ProfeAnatomia:
                            int maxRutas = 3;//cantidad total de rutas posibles que seguira la bala al ser disparada.
                            int minRutas = 1;//minima cantidad de rutas que seguira la bala al ser disparada.
                            if (!isDuck)
                            {
                                nombreGenerador = "GeneradorPelotasParabolaProfeAnatomia";
                            }
                            else
                            {
                                nombreGenerador = "GeneradorPelotasParabolaAgachadoProfeAnatomia";
                            }
                            CheckSpecialAttackEnemyController(minRutas, maxRutas, nombreGenerador, generador);
                            break;
                            //PARA IMPLEMENTAR LOS DEMAS ENEMIGOS PONER LAS CONFIGURACIONES FALTANTES Y IR PROBANDOLOS TODOS.
                        case EnumsEnemy.TiposDeJefe.ProfeHistoria:
                            maxRutas = 3;//cantidad total de rutas posibles que seguira la bala al ser disparada.
                            minRutas = 1;//minima cantidad de rutas que seguira la bala al ser disparada.
                            if (!isDuck)
                            {
                                nombreGenerador = "GeneradorPelotasParabolaProfeHistoria";
                            }
                            else
                            {
                                nombreGenerador = "GeneradorPelotasParabolaAgachadoProfeHistoria";
                            }
                            CheckSpecialAttackEnemyController(minRutas, maxRutas, nombreGenerador, generador);
                            break;
                        case EnumsEnemy.TiposDeJefe.ProfeEducacionFisica:
                            maxRutas = 3;//cantidad total de rutas posibles que seguira la bala al ser disparada.
                            minRutas = 1;//minima cantidad de rutas que seguira la bala al ser disparada.
                            if (!isDuck)
                            {
                                nombreGenerador = "GeneradorPelotasParabolaProfeEducacionFisica";
                            }
                            else
                            {
                                nombreGenerador = "GeneradorPelotasParabolaAgachadoProfeEducacionFisica";
                            }
                            CheckSpecialAttackEnemyController(minRutas, maxRutas, nombreGenerador, generador);
                            break;
                        case EnumsEnemy.TiposDeJefe.ProfeArte:
                            maxRutas = 3;//cantidad total de rutas posibles que seguira la bala al ser disparada.
                            minRutas = 1;//minima cantidad de rutas que seguira la bala al ser disparada.
                            if (!isDuck)
                            {
                                nombreGenerador = "GeneradorPelotasParabolaProfeArte";
                            }
                            else
                            {
                                nombreGenerador = "GeneradorPelotasParabolaAgachadoProfeArte";
                            }
                            CheckSpecialAttackEnemyController(minRutas, maxRutas, nombreGenerador, generador);
                            break;
                        case EnumsEnemy.TiposDeJefe.ProfeMatematica:
                            maxRutas = 3;//cantidad total de rutas posibles que seguira la bala al ser disparada.
                            minRutas = 1;//minima cantidad de rutas que seguira la bala al ser disparada.
                            if (!isDuck)
                            {
                                nombreGenerador = "GeneradorPelotasParabolaProfeMatematica";
                            }
                            else
                            {
                                nombreGenerador = "GeneradorPelotasParabolaAgachadoProfeMatematica";
                            }
                            CheckSpecialAttackEnemyController(minRutas, maxRutas, nombreGenerador, generador);
                            break;
                        case EnumsEnemy.TiposDeJefe.ProfeQuimica:
                            maxRutas = 3;//cantidad total de rutas posibles que seguira la bala al ser disparada.
                            minRutas = 1;//minima cantidad de rutas que seguira la bala al ser disparada.
                            if (!isDuck)
                            {
                                nombreGenerador = "GeneradorPelotasParabolaProfeQuimica";
                            }
                            else
                            {
                                nombreGenerador = "GeneradorPelotasParabolaAgachadoProfeQuimica";
                            }
                            CheckSpecialAttackEnemyController(minRutas, maxRutas, nombreGenerador, generador);
                            break;
                        case EnumsEnemy.TiposDeJefe.ProfeProgramacion:
                            maxRutas = 3;//cantidad total de rutas posibles que seguira la bala al ser disparada.
                            minRutas = 1;//minima cantidad de rutas que seguira la bala al ser disparada.
                            if (!isDuck)
                            {
                                nombreGenerador = "GeneradorPelotasParabolaProfeProgramacion";
                            }
                            else
                            {
                                nombreGenerador = "GeneradorPelotasParabolaAgachadoProfeProgramacion";
                            }
                            CheckSpecialAttackEnemyController(minRutas, maxRutas, nombreGenerador, generador);
                            break;
                        case EnumsEnemy.TiposDeJefe.ProfeBaretto:
                            maxRutas = 3;//cantidad total de rutas posibles que seguira la bala al ser disparada.
                            minRutas = 1;//minima cantidad de rutas que seguira la bala al ser disparada.
                            if (!isDuck)
                            {
                                nombreGenerador = "GeneradorPelotasParabolaProfeBaretto";
                            }
                            else
                            {
                                nombreGenerador = "GeneradorPelotasParabolaAgachadoProfeBaretto";
                            }
                            CheckSpecialAttackEnemyController(minRutas, maxRutas, nombreGenerador, generador);
                            break;
                        case EnumsEnemy.TiposDeJefe.ProfeLautarito:
                            maxRutas = 3;//cantidad total de rutas posibles que seguira la bala al ser disparada.
                            minRutas = 1;//minima cantidad de rutas que seguira la bala al ser disparada.
                            if (!isDuck)
                            {
                                nombreGenerador = "GeneradorPelotasParabolaProfeLautaro";
                            }
                            else
                            {
                                nombreGenerador = "GeneradorPelotasParabolaAgachadoProfeLautaro";
                            }
                            CheckSpecialAttackEnemyController(minRutas, maxRutas, nombreGenerador, generador);
                            break;

                            //UNA VEZ INCORPORADA LA PARTE DE LOS BOSESS INCORPORAR ESTA PARTE EN BASE A LA PARTE DE ARRIBA.
                    }
                }
            }
            if (!specialAttack)
            {
                //Debug.Log("ENTRE");
                proyectil.On();
                if (!shootDown)
                {
                    proyectil.ShootForward();
                }
                else
                {
                    proyectil.ShootForwardDown();
                }
            }
        }
        public void CheckSpecialAttackEnemyController(int minRandomRootShoot, int maxRandomRootShoot, string nombreGenerador, GameObject generador)
        {
            if (!isDuck)
            {
                for (int i = 0; i < generadorProyectilParabola.Count; i++)
                {
                    if (generadorProyectilParabola[i].name == nombreGenerador)
                    {
                        generador = generadorProyectilParabola[i];
                    }
                }
                if (generador != null)
                {
                    specialAttackEnemyController.SpecialAttack(doubleDamage, isDuck, generador, null, enumsEnemy, structsEnemys, maxRandomRootShoot, minRandomRootShoot);
                }
            }
            else
            {
                for (int i = 0; i < generadorProyectilParabolaAgachado.Count; i++)
                {
                    if (generadorProyectilParabolaAgachado[i].name == nombreGenerador)
                    {
                        generador = generadorProyectilParabolaAgachado[i];
                    }
                }
                if (generador != null)
                {
                    specialAttackEnemyController.SpecialAttack(doubleDamage, isDuck, null, generador, enumsEnemy, structsEnemys, maxRandomRootShoot, minRandomRootShoot);
                }
            }
            
        }
        public void CounterAttack(bool dobleDamage)
        {
            Attack(false,false,true);
        }
        public void Jump(Vector3 alturaMaxima)
        {
            if (CheckMove(new Vector3(transform.position.x, alturaMaxima.y, transform.position.z)) && isJamping)
            {
                if (enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.SaltoAtaque && enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.SaltoDefensa && enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.AtacarEnParabolaSaltando)
                {
                    enumsEnemy.SetMovement(EnumsEnemy.Movimiento.Saltar);
                }
                MoveJamp(Vector3.up);
                if (SpeedJump <= 0)
                {
                    isJamping = false;
                }
                gridEnemy.matrizCuadrilla[gridEnemy.baseGrild][structsEnemys.dataEnemy.columnaActual].SetStateCuadrilla(Cuadrilla.StateCuadrilla.Libre);

                //Debug.Log(gridPlayer.matrizCuadrilla[gridPlayer.baseGrild][structsPlayer.dataPlayer.columnaActual].name);
                if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.SaltoDefensa)
                {
                    gridEnemy.matrizCuadrilla[gridEnemy.baseGrild - 1][structsEnemys.dataEnemy.columnaActual].SetStateCuadrilla(Cuadrilla.StateCuadrilla.Ocupado);
                    gridEnemy.matrizCuadrilla[gridEnemy.baseGrild - 2][structsEnemys.dataEnemy.columnaActual].SetStateCuadrilla(Cuadrilla.StateCuadrilla.Ocupado);
                    Deffence();
                }
            }

            CheckOutLimit();
        }
        public bool CheckMove(Vector3 PosicionDestino)
        {
            Vector3 distaciaObjetivo = transform.position - PosicionDestino;
            bool mover = false;
            if (distaciaObjetivo.magnitude > 0.1f)
            {
                mover = true;
            }
            return mover;
        }
        public void Move(Vector3 direccion)
        {
            transform.Translate(direccion * Speed * Time.deltaTime);
        }
        public void MoveJamp(Vector3 direccion)
        {
            if (direccion == Vector3.up)
            {
                transform.Translate(direccion * SpeedJump * Time.deltaTime);
                SpeedJump = SpeedJump - Time.deltaTime * Resistace;
            }
            else if (direccion == Vector3.down)
            {
                transform.Translate(direccion * SpeedJump * Time.deltaTime);
                SpeedJump = SpeedJump + Time.deltaTime * Gravity;
            }
        }
        public void Duck(int rangoAgachado)
        {
            isDuck = true;
            for (int i = 0; i < collidersSprites.Count; i++)
            {
                collidersSprites[i].enabled = false;
            }
            for (int i = 0; i < structsEnemys.dataEnemy.CantCasillasOcupadas_X; i++)
            {
                if (structsEnemys.dataEnemy.columnaActual + i < gridEnemy.GetCuadrilla_columnas())
                {
                    gridEnemy.matrizCuadrilla[gridEnemy.GetCuadrilla_columnas() - rangoAgachado][structsEnemys.dataEnemy.columnaActual + i].SetStateCuadrilla(Cuadrilla.StateCuadrilla.Libre);
                }
            }
        }
        public bool GetIsDeffended()
        {
            return isDeffended;
        }
        public bool GetIsJamping()
        {
            return isJamping;
        }
    }
}
