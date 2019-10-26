using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Prototipo_2
{
    public class Proyectil : MonoBehaviour
    {

        public enum DisparadorDelProyectil
        {
            Nulo,
            Enemigo,
            Jugador1,
            Jugador2,
        }
        public enum TypeShoot
        {
            Recto,
            EnParabola,
            Nulo,
        }
        public enum PosicionDisparo
        {
            Nulo,
            PosicionAlta,
            PosicionMedia,
            PosicionBaja,
        }
        public float speed;
        public float timeLife;
        public float auxTimeLife;
        public float damageCounterAttack;
        public float damage;
        [SerializeField]
        private float auxDamage;
        public Rigidbody2D rg2D;
        public Transform vectorForward;
        public Transform vectorForwardUp;
        public Transform vectorForwardDown;
        public Pool pool;
        protected bool dobleDamage;
        private PoolObject poolObject;
        protected Player PLAYER1;
        protected Player PLAYER2;
        protected Enemy ENEMY;
        protected GameManager gm;
        public DisparadorDelProyectil disparadorDelProyectil;
        private TrailRenderer trailRenderer;
        [HideInInspector]
        public PosicionDisparo posicionDisparo;
        private void OnDisable()
        {
            if (trailRenderer != null)
            {
                trailRenderer.enabled = false;
            }
        }
        private void Start()
        {
            trailRenderer = GetComponent<TrailRenderer>();
            if (GameManager.instanceGameManager != null)
            {
                gm = GameManager.instanceGameManager;
            }
            poolObject = GetComponent<PoolObject>();
        }
        private void OnEnable()
        {
            timeLife = auxTimeLife;
        }
        private void Update()
        {
            CheckTimeLife();
        }
        public void On()
        {
            if (trailRenderer != null)
            {
                trailRenderer.enabled = true;
            }
            if (!dobleDamage)
            {
                damage = auxDamage;
            }
            poolObject = GetComponent<PoolObject>();
            rg2D.velocity = Vector2.zero;
            rg2D.angularVelocity = 0;
            timeLife = auxTimeLife;
        }
        public void CheckTimeLife()
        {
            if (timeLife > 0)
            {
                timeLife = timeLife - Time.deltaTime;
            }
            else if (timeLife <= 0)
            {
                damage = auxDamage;
                dobleDamage = false;
                if (poolObject != null)
                {
                    poolObject.Recycle();
                }
            }
        }
        public void ShootForward()
        {
            rg2D.AddForce(transform.right * speed, ForceMode2D.Force);
        }
        public void ShootForwardUp()
        {
            rg2D.AddRelativeForce(vectorForwardUp.right * speed);
        }
        public void ShootForwardDown()
        {
            rg2D.AddRelativeForce(vectorForwardDown.right * speed, ForceMode2D.Force);
        }
        public PoolObject GetPoolObject()
        {
            return poolObject;
        }
        public void SetDobleDamage(bool _dobleDamage)
        {
            dobleDamage = _dobleDamage;
        }
        public void SetPlayer(Player player)
        {
            PLAYER1 = player;
        }
        public void SetPlayer2(Player player2)
        {
            PLAYER2 = player2;
        }
        public void SetEnemy(Enemy enemy)
        {
            ENEMY = enemy;
        }
        public Player GetPlayer()
        {
            return PLAYER1;        
        }
        public Player GetPlayer2()
        {
            return PLAYER2;
        }
        public Enemy GetEnemy()
        {
            return ENEMY;
        }
        public float GetAuxDamage()
        {
            return auxDamage;
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            switch (collision.tag)
            {
                case "Escudo":
                    timeLife = 0;
                    break;
                case "Player":
                    bool enableDamagePlayer = true;
                    //PASADO
                    /*if (ENEMY != null)
                    {

                        if (disparadorDelProyectil == DisparadorDelProyectil.Enemigo)
                        {
                            player.SetEnableCounterAttack(true);
                            if (player.delayCounterAttack > 0)
                            {
                                player.delayCounterAttack = player.delayCounterAttack - Time.deltaTime;
                                if (InputPlayerController.DeffenseButton_P1())
                                {
                                    player.Attack(DisparadorDelProyectil.Jugador1);
                                    player.delayCounterAttack = player.GetAuxDelayCounterAttack();
                                    timeLife = 0;
                                    enableDamagePlayer = false;
                                }
                            }
                            if (player.delayCounterAttack <= 0 && timeLife <= 0 && enableDamagePlayer)
                            {
                                ENEMY.SetXpActual(ENEMY.GetXpActual() + ENEMY.xpForHit);
                                player.PD.lifePlayer = player.PD.lifePlayer - damage;
                                player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.RecibirDanio;
                            }
                            else if (player.delayCounterAttack <= 0 && timeLife > 0 && enableDamagePlayer)
                            {
                                ENEMY.SetXpActual(ENEMY.GetXpActual() + ENEMY.xpForHit);
                                player.delayCounterAttack = player.GetAuxDelayCounterAttack();
                                player.SetEnableCounterAttack(false);
                                player.PD.lifePlayer = player.PD.lifePlayer - damage;
                                player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.RecibirDanio;
                                timeLife = 0;
                                gameObject.SetActive(false);
                            }
                        }
                    }*/

                    //PASADO
                    /*if (PLAYER1 != null)
                    {
                        if (disparadorDelProyectil == DisparadorDelProyectil.Jugador1
                            || player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player2)
                        {
                            //AUMENTO XP PARA EL ATAQUE ESPECIAL
                            PLAYER1.SetXpActual(PLAYER1.GetXpActual() + PLAYER1.xpForHit);
                            PLAYER1.PD.score = PLAYER1.PD.score + PLAYER1.PD.scoreForHit;
                            player.SetEnableCounterAttack(true);
                            if (player.delayCounterAttack > 0)
                            {
                                player.delayCounterAttack = player.delayCounterAttack - Time.deltaTime;
                                if (InputPlayerController.DeffenseButton_P2())
                                {
                                    player.Attack(DisparadorDelProyectil.Jugador2);
                                    player.delayCounterAttack = player.GetAuxDelayCounterAttack();
                                    timeLife = 0;
                                    enableDamagePlayer = false;
                                }
                            }
                            if (player.delayCounterAttack <= 0 && timeLife <= 0 && enableDamagePlayer)
                            {
                                player.PD.lifePlayer = player.PD.lifePlayer - damage;
                                player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.RecibirDanio;
                            }
                            else if (player.delayCounterAttack <= 0 && timeLife > 0 && enableDamagePlayer)
                            {
                                player.delayCounterAttack = player.GetAuxDelayCounterAttack();
                                player.SetEnableCounterAttack(false);
                                player.PD.lifePlayer = player.PD.lifePlayer - damage;
                                player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.RecibirDanio;
                                timeLife = 0;
                                gameObject.SetActive(false);
                            }
                        }
                    }*/
                    //PASADO
                    /*
                    if (PLAYER2 != null)
                    {
                        if (disparadorDelProyectil == DisparadorDelProyectil.Jugador2 ||
                        player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player1 ||
                        PLAYER2 != null)
                        {
                            //AUMENTO XP PARA EL ATAQUE ESPECIAL
                            PLAYER2.SetXpActual(PLAYER2.GetXpActual() + PLAYER2.xpForHit);
                            PLAYER2.PD.score = PLAYER2.PD.score + PLAYER2.PD.scoreForHit;
                            player.SetEnableCounterAttack(true);
                            if (player.delayCounterAttack > 0)
                            {
                                player.delayCounterAttack = player.delayCounterAttack - Time.deltaTime;
                                if (InputPlayerController.DeffenseButton_P1())
                                {
                                    player.Attack(DisparadorDelProyectil.Jugador1);
                                    player.delayCounterAttack = player.GetAuxDelayCounterAttack();
                                    timeLife = 0;
                                    enableDamagePlayer = false;
                                }
                            }
                            if (player.delayCounterAttack <= 0 && timeLife <= 0 && enableDamagePlayer)
                            {
                                player.PD.lifePlayer = player.PD.lifePlayer - damage;
                                player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.RecibirDanio;
                            }
                            else if (player.delayCounterAttack <= 0 && timeLife > 0 && enableDamagePlayer)
                            {
                                player.delayCounterAttack = player.GetAuxDelayCounterAttack();
                                player.SetEnableCounterAttack(false);
                                player.PD.lifePlayer = player.PD.lifePlayer - damage;
                                player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.RecibirDanio;
                                timeLife = 0;
                                gameObject.SetActive(false);
                            }
                        }
                    }*/
                    break;
                case "Enemy":
                    //PASADO
                    /*
                    Enemy enemy = collision.GetComponent<Enemy>();
                    Debug.Log(disparadorDelProyectil);
                    if (disparadorDelProyectil == DisparadorDelProyectil.Jugador1)
                    {
                        if (enemy.enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.MoveToPointCombat && enemy.enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.MoveToPointDeath)
                        {
                            enemy.spriteEnemy.ActualSprite = SpriteEnemy.SpriteActual.RecibirDanio;
                            enemy.life = enemy.life - damage;

                            //AUMENTO XP PARA EL ATAQUE ESPECIAL
                            PLAYER1.SetXpActual(PLAYER1.GetXpActual() + PLAYER1.xpForHit);
                            PLAYER1.PD.score = PLAYER1.PD.score + PLAYER1.PD.scoreForHit;
                        }
                        timeLife = 0;
                        gameObject.SetActive(false);
                        poolObject.Recycle();
                    }*/
                    break;
                case "Cuadrilla":
                    enableDamagePlayer = true;
                    Cuadrilla cuadrilla = collision.GetComponent<Cuadrilla>();
                    if (cuadrilla.enemy == null && cuadrilla.player == null || cuadrilla.enemy != null && cuadrilla.player != null)
                    {
                        return;
                    }
                    if (cuadrilla.GetStateCuadrilla() == Cuadrilla.StateCuadrilla.Defendido)
                    {
                        if (cuadrilla.player != null)
                        {
                            //PASADO 
                            /*
                            Player_PvP player_PvP = cuadrilla.player.gameObject.GetComponent<Player_PvP>();
                            float realDamage;
                            if (PLAYER1 != null)
                            {
                                if (disparadorDelProyectil == DisparadorDelProyectil.Jugador1
                                    || cuadrilla.player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player2)
                                {
                                    //AUMENTO XP PARA EL ATAQUE ESPECIAL
                                    //SI SE DEFIENDE CARGA LA MITAD DE LA BARRA DEL ATAQUE ESPECIAL.
                                    PLAYER1.SetXpActual(PLAYER1.GetXpActual() + (PLAYER1.xpForHit / 2));
                                    PLAYER1.PD.score = PLAYER1.PD.score + (PLAYER1.PD.scoreForHit / 2);
                                }
                            }
                            if (player_PvP != null)
                            {
                                
                                if (PLAYER2 != null)
                                {
                                    if (disparadorDelProyectil == DisparadorDelProyectil.Jugador2
                                    || cuadrilla.player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player1)
                                    {
                                        //AUMENTO XP PARA EL ATAQUE ESPECIAL
                                        //SI SE DEFIENDE CARGA LA MITAD DE LA BARRA DEL ATAQUE ESPECIAL.
                                        PLAYER2.SetXpActual(PLAYER2.GetXpActual() + (PLAYER2.xpForHit / 2));
                                        PLAYER2.PD.score = PLAYER2.PD.score + (PLAYER2.PD.scoreForHit/2);
                                    }
                                }
                                if (player_PvP.playerSelected == Player_PvP.PlayerSelected.Defensivo)
                                {
                                    switch (player_PvP.playerState)
                                    {
                                        case Player_PvP.State.Defendido:
                                            cuadrilla.player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.ContraAtaqueParado;
                                            if (player_PvP.playerActual == Player_PvP.Player.player1)
                                            {
                                                cuadrilla.player.Attack(DisparadorDelProyectil.Jugador1);
                                            }
                                            else if (player_PvP.playerActual == Player_PvP.Player.player2)
                                            {
                                                cuadrilla.player.Attack(DisparadorDelProyectil.Jugador2);
                                            }
                                            realDamage = damage - cuadrilla.player.pointsDeffence;
                                            cuadrilla.player.PD.lifePlayer = cuadrilla.player.PD.lifePlayer - realDamage;
                                            timeLife = 0;
                                            damage = auxDamage;
                                            poolObject.Recycle();
                                            gameObject.SetActive(false);
                                            break;
                                        default:
                                            if (PLAYER1 != null)
                                            {
                                                if (disparadorDelProyectil == DisparadorDelProyectil.Jugador1
                                                    || cuadrilla.player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player2)
                                                {
                                                    //AUMENTO XP PARA EL ATAQUE ESPECIAL
                                                    //SI SE DEFIENDE CARGA LA MITAD DE LA BARRA DEL ATAQUE ESPECIAL.
                                                    PLAYER1.SetXpActual(PLAYER1.GetXpActual() + (PLAYER1.xpForHit / 2));
                                                    PLAYER1.PD.score = PLAYER1.PD.score + (PLAYER1.PD.scoreForHit / 2);
                                                }
                                            }
                                            else if (PLAYER2 != null)
                                            {
                                                if (disparadorDelProyectil == DisparadorDelProyectil.Jugador2
                                                || cuadrilla.player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player1)
                                                {
                                                    //AUMENTO XP PARA EL ATAQUE ESPECIAL
                                                    //SI SE DEFIENDE CARGA LA MITAD DE LA BARRA DEL ATAQUE ESPECIAL.
                                                    PLAYER2.SetXpActual(PLAYER2.GetXpActual() + (PLAYER2.xpForHit / 2));
                                                    PLAYER2.PD.score = PLAYER2.PD.score + (PLAYER2.PD.scoreForHit / 2);
                                                }
                                            }
                                            realDamage = damage - cuadrilla.player.pointsDeffence;
                                            cuadrilla.player.PD.lifePlayer = cuadrilla.player.PD.lifePlayer - realDamage;
                                            timeLife = 0;
                                            damage = auxDamage;
                                            poolObject.Recycle();
                                            gameObject.SetActive(false);
                                            break;
                                    }
                                }
                                else if(ENEMY != null)
                                {
                                    ENEMY.SetXpActual(ENEMY.GetXpActual() + (ENEMY.xpForHit/2));
                                    realDamage = damage - cuadrilla.player.pointsDeffence;
                                    cuadrilla.player.PD.lifePlayer = cuadrilla.player.PD.lifePlayer - realDamage;
                                    timeLife = 0;
                                    damage = auxDamage;
                                    poolObject.Recycle();
                                    gameObject.SetActive(false);
                                }
                            }

                            if (disparadorDelProyectil == DisparadorDelProyectil.Enemigo||disparadorDelProyectil == DisparadorDelProyectil.Jugador2 || disparadorDelProyectil == DisparadorDelProyectil.Jugador1)
                            {
                                if (cuadrilla.player.delayCounterAttack <= 0)
                                {
                                    realDamage = damage - cuadrilla.player.pointsDeffence;
                                    cuadrilla.player.PD.lifePlayer = cuadrilla.player.PD.lifePlayer - realDamage;
                                    cuadrilla.player.delayCounterAttack = cuadrilla.player.GetAuxDelayCounterAttack();
                                    timeLife = 0;
                                    damage = auxDamage;
                                    poolObject.Recycle();
                                    gameObject.SetActive(false);
                                }
                                else
                                {
                                    cuadrilla.player.delayCounterAttack = cuadrilla.player.delayCounterAttack - Time.deltaTime;
                                }
                            }
                            */
                        }
                        if (cuadrilla.enemy != null)
                        {
                            //PASADO
                            /*
                            if (disparadorDelProyectil == DisparadorDelProyectil.Jugador1)
                            {
                                //AUMENTO XP PARA EL ATAQUE ESPECIAL
                                //SI SE DEFIENDE CARGA LA MITAD DE LA BARRA DEL ATAQUE ESPECIAL.
                                PLAYER1.SetXpActual(PLAYER1.GetXpActual() + (PLAYER1.xpForHit/2));
                                PLAYER1.PD.score = PLAYER1.PD.score + (PLAYER1.PD.scoreForHit / 2);
                                if (cuadrilla.enemy.enumsEnemy.typeEnemy != EnumsEnemy.TiposDeEnemigo.Defensivo)
                                {
                                    float realDamage = damage - cuadrilla.enemy.pointsDeffence;
                                    cuadrilla.enemy.life = cuadrilla.enemy.life - realDamage;
                                }
                                else
                                {
                                    if (cuadrilla.enemy.enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.DefensaEnElLugar)
                                    {
                                        //MECANICA DEFENSIVA DEL ENEMIGO DEFENSIVO//
                                        if (cuadrilla.enemy.GetComponent<Defensivo>() != null)
                                        {
                                            Defensivo enemyDeffended = cuadrilla.enemy.GetComponent<Defensivo>();
                                            if (enemyDeffended.GetStateDeffence() == Defensivo.StateDeffence.CounterAttackDeffense)
                                            {
                                                cuadrilla.enemy.spriteEnemy.ActualSprite = SpriteEnemy.SpriteActual.ContraAtaque;
                                                cuadrilla.enemy.Attack(false,false,true,cuadrilla);
                                                enemyDeffended.SetStateDeffense(Defensivo.StateDeffence.Nulo);
                                                enemyDeffended.delayStateCounterAttackDeffense = 0;
                                                enemyDeffended.delayStateDeffense = enemyDeffended.GetAuxDelayStateDeffense();
                                                enemyDeffended.delayVulnerable = enemyDeffended.GetAuxDelayVulnerable();
                                                if (cuadrilla.enemy.damageCounterAttack)
                                                {
                                                    float realDamage = damage - cuadrilla.enemy.pointsDeffence;
                                                    cuadrilla.enemy.life = cuadrilla.enemy.life - realDamage;
                                                }
                                            }
                                            else if(enemyDeffended.GetStateDeffence() == Defensivo.StateDeffence.NormalDeffense)
                                            {
                                                if (cuadrilla.enemy.damageCounterAttack)
                                                {
                                                    float realDamage = damage - cuadrilla.enemy.pointsDeffence;
                                                    cuadrilla.enemy.life = cuadrilla.enemy.life - realDamage;
                                                }
                                            }
                                        }
                                        //-----------------------------------------//
                                    }
                                    else
                                    {
                                        cuadrilla.enemy.life = cuadrilla.enemy.life - damage;
                                    }
                                }
                                timeLife = 0;
                                gameObject.SetActive(false);

                            }
                            */
                        }
                        
                    }
                    break;
            }
        }
    }
}
