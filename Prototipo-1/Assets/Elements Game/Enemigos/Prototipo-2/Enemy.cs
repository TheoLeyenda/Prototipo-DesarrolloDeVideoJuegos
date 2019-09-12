using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace Prototipo_2
{
    public class Enemy : MonoBehaviour
    {
        public Grid gridEnemy;
        private Cuadrilla cuadrillaActual;
        public int rangeCuadrillas_x;
        public int rangeCuadrillas_y;
        public EnumsEnemy enumsEnemy;
        public SpriteRenderer SpriteRendererEnemigoBalanceado;
        public SpriteRenderer SpriteRendererEnemigoAgresivo;
        public SpriteRenderer SpriteRendererEnemigoDefensivo;
        public SpriteRenderer SpriteRendererJefeProfeAnatomia;
        public GameObject BARRA_DE_VIDA;
        [HideInInspector]
        public float alturaPredeterminada;// LA ALTURA PREDETERMINADA SERA IGUAL A LA POSICION y DE PLATAFORMA + 0.5;
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
        public Transform tranformAtaque;
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
        private float JumpPorcentage;
        private float DuckPorcentage;
        private float MinRangeRandom = 0;
        private float MaxRangeRandom = 100;
        private float TypeRandom = 3;
        void Start()
        {
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
        public void CheckInitialSprite()
        {
            if (enumsEnemy.typeEnemy != EnumsEnemy.TiposDeEnemigo.Jefe)
            {
                switch (enumsEnemy.typeEnemy)
                {
                    case EnumsEnemy.TiposDeEnemigo.Agresivo:
                        SpriteRendererEnemigoAgresivo.gameObject.SetActive(true);
                        SpriteRendererEnemigoBalanceado.gameObject.SetActive(false);
                        SpriteRendererEnemigoDefensivo.gameObject.SetActive(false);
                        SpriteRendererJefeProfeAnatomia.gameObject.SetActive(false);
                        break;
                    case EnumsEnemy.TiposDeEnemigo.Balanceado:
                        SpriteRendererEnemigoBalanceado.gameObject.SetActive(true);
                        SpriteRendererEnemigoAgresivo.gameObject.SetActive(false);
                        SpriteRendererEnemigoDefensivo.gameObject.SetActive(false);
                        SpriteRendererJefeProfeAnatomia.gameObject.SetActive(false);
                        break;
                    case EnumsEnemy.TiposDeEnemigo.Defensivo:
                        SpriteRendererEnemigoDefensivo.gameObject.SetActive(true);
                        SpriteRendererEnemigoBalanceado.gameObject.SetActive(false);
                        SpriteRendererEnemigoAgresivo.gameObject.SetActive(false);
                        SpriteRendererJefeProfeAnatomia.gameObject.SetActive(false);
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
            if (gm == null)
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
        public void CheckCasillaOcupada()
        {

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
                    gameObject.SetActive(false);
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

            if (poolObjectAttack.count > 0)
            {
                //ELIJE EL ATAQUE EN BASE AL MOVIMIENTO QUE RESIVE
            }
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
