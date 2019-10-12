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
        private Player PLAYER1;
        private Player PLAYER2;
        private Enemy ENEMY;
        protected GameManager gm;
        public DisparadorDelProyectil disparadorDelProyectil;
        private void Start()
        {
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
        private void OnTriggerStay2D(Collider2D collision)
        {
            switch (collision.tag)
            {
                case "Escudo":
                    timeLife = 0;
                    break;
                case "Cuadrilla":
                    bool enableDamagePlayer = true;
                    Cuadrilla cuadrilla = collision.GetComponent<Cuadrilla>();
                    if (cuadrilla.enemy == null && cuadrilla.player == null || cuadrilla.enemy != null && cuadrilla.player != null)
                    {
                        return;
                    }
                    if (cuadrilla.enemy != null)
                    {
                        if (cuadrilla.GetStateCuadrilla() == Cuadrilla.StateCuadrilla.Ocupado && cuadrilla.enemy.GetIsDeffended())
                        {
                            cuadrilla.SetStateCuadrilla(Cuadrilla.StateCuadrilla.Defendido);
                        }
                    }
                    if (cuadrilla.GetStateCuadrilla() == Cuadrilla.StateCuadrilla.Ocupado)
                    {
                        if (cuadrilla.enemy != null)
                        {
                            if (disparadorDelProyectil == DisparadorDelProyectil.Jugador1)
                            {
                                if (cuadrilla.enemy.enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.MoveToPointCombat && cuadrilla.enemy.enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.MoveToPointDeath)
                                {
                                    cuadrilla.enemy.spriteEnemy.ActualSprite = SpriteEnemy.SpriteActual.RecibirDanio;
                                    cuadrilla.enemy.life = cuadrilla.enemy.life - damage;

                                    //AUMENTO XP PARA EL ATAQUE ESPECIAL
                                    PLAYER1.SetXpActual(PLAYER1.GetXpActual() + PLAYER1.xpForHit);
                                }
                                timeLife = 0;
                                gameObject.SetActive(false);
                            }
                        }
                        if (cuadrilla.player != null)
                        {
                            
                            if (disparadorDelProyectil == DisparadorDelProyectil.Enemigo)
                            {
                                cuadrilla.player.SetEnableCounterAttack(true);
                                if (cuadrilla.player.delayCounterAttack > 0)
                                {
                                    cuadrilla.player.delayCounterAttack = cuadrilla.player.delayCounterAttack - Time.deltaTime;
                                    if (InputPlayerController.DeffenseButton_P1())
                                    {
                                        cuadrilla.player.Attack(DisparadorDelProyectil.Jugador1);
                                        cuadrilla.player.delayCounterAttack = cuadrilla.player.GetAuxDelayCounterAttack();
                                        timeLife = 0;
                                        enableDamagePlayer = false;
                                    }
                                }
                                if (cuadrilla.player.delayCounterAttack <= 0 && timeLife <= 0 && enableDamagePlayer)
                                {
                                    cuadrilla.player.PD.lifePlayer = cuadrilla.player.PD.lifePlayer - damage;
                                    cuadrilla.player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.RecibirDanio;
                                }
                                else if (cuadrilla.player.delayCounterAttack <= 0 && timeLife > 0 && enableDamagePlayer)
                                {
                                    cuadrilla.player.delayCounterAttack = cuadrilla.player.GetAuxDelayCounterAttack();
                                    cuadrilla.player.SetEnableCounterAttack(false);
                                    cuadrilla.player.PD.lifePlayer = cuadrilla.player.PD.lifePlayer - damage;
                                    cuadrilla.player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.RecibirDanio;
                                    timeLife = 0;
                                    gameObject.SetActive(false);;
                                }
                            }
                            if (PLAYER1 != null)
                            {
                                if (disparadorDelProyectil == DisparadorDelProyectil.Jugador1
                                    || cuadrilla.player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player1)
                                {
                                    //AUMENTO XP PARA EL ATAQUE ESPECIAL
                                    PLAYER1.SetXpActual(PLAYER1.GetXpActual() + PLAYER1.xpForHit);
                                    cuadrilla.player.SetEnableCounterAttack(true);
                                    if (cuadrilla.player.delayCounterAttack > 0)
                                    {
                                        cuadrilla.player.delayCounterAttack = cuadrilla.player.delayCounterAttack - Time.deltaTime;
                                        if (InputPlayerController.DeffenseButton_P1())
                                        {
                                            cuadrilla.player.Attack(DisparadorDelProyectil.Jugador2);
                                            cuadrilla.player.delayCounterAttack = cuadrilla.player.GetAuxDelayCounterAttack();
                                            timeLife = 0;
                                            enableDamagePlayer = false;
                                        }
                                    }
                                    if (cuadrilla.player.delayCounterAttack <= 0 && timeLife <= 0 && enableDamagePlayer)
                                    {
                                        cuadrilla.player.PD.lifePlayer = cuadrilla.player.PD.lifePlayer - damage;
                                        cuadrilla.player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.RecibirDanio;
                                    }
                                    else if (cuadrilla.player.delayCounterAttack <= 0 && timeLife > 0 && enableDamagePlayer)
                                    {
                                        cuadrilla.player.delayCounterAttack = cuadrilla.player.GetAuxDelayCounterAttack();
                                        cuadrilla.player.SetEnableCounterAttack(false);
                                        cuadrilla.player.PD.lifePlayer = cuadrilla.player.PD.lifePlayer - damage;
                                        cuadrilla.player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.RecibirDanio;
                                        timeLife = 0;
                                        gameObject.SetActive(false);
                                    }
                                }
                            }
                            if (PLAYER2 != null)
                            {
                                if (disparadorDelProyectil == DisparadorDelProyectil.Jugador2 ||
                                cuadrilla.player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player1 ||
                                PLAYER2 != null)
                                {
                                    //AUMENTO XP PARA EL ATAQUE ESPECIAL
                                    PLAYER2.SetXpActual(PLAYER2.GetXpActual() + PLAYER2.xpForHit);
                                    cuadrilla.player.SetEnableCounterAttack(true);
                                    if (cuadrilla.player.delayCounterAttack > 0)
                                    {
                                        cuadrilla.player.delayCounterAttack = cuadrilla.player.delayCounterAttack - Time.deltaTime;
                                        if (InputPlayerController.DeffenseButton_P1())
                                        {
                                            cuadrilla.player.Attack(DisparadorDelProyectil.Jugador2);
                                            cuadrilla.player.delayCounterAttack = cuadrilla.player.GetAuxDelayCounterAttack();
                                            timeLife = 0;
                                            enableDamagePlayer = false;
                                        }
                                    }
                                    if (cuadrilla.player.delayCounterAttack <= 0 && timeLife <= 0 && enableDamagePlayer)
                                    {
                                        cuadrilla.player.PD.lifePlayer = cuadrilla.player.PD.lifePlayer - damage;
                                        cuadrilla.player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.RecibirDanio;
                                    }
                                    else if (cuadrilla.player.delayCounterAttack <= 0 && timeLife > 0 && enableDamagePlayer)
                                    {
                                        cuadrilla.player.delayCounterAttack = cuadrilla.player.GetAuxDelayCounterAttack();
                                        cuadrilla.player.SetEnableCounterAttack(false);
                                        cuadrilla.player.PD.lifePlayer = cuadrilla.player.PD.lifePlayer - damage;
                                        cuadrilla.player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.RecibirDanio;
                                        timeLife = 0;
                                        gameObject.SetActive(false);
                                    }
                                }
                            }
                        }
                    }
                    if (cuadrilla.GetStateCuadrilla() == Cuadrilla.StateCuadrilla.Defendido)
                    {
                        if (cuadrilla.player != null)
                        {
                            Player_PvP player1_PvP = cuadrilla.player.gameObject.GetComponent<Player_PvP>();
                            float realDamage;
                            if (PLAYER1 != null)
                            {
                                if (disparadorDelProyectil == DisparadorDelProyectil.Jugador1
                                    || cuadrilla.player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player2)
                                {
                                    //AUMENTO XP PARA EL ATAQUE ESPECIAL
                                    //SI SE DEFIENDE CARGA LA MITAD DE LA BARRA DEL ATAQUE ESPECIAL.
                                    PLAYER1.SetXpActual(PLAYER1.GetXpActual() + (PLAYER1.xpForHit / 2));
                                }
                            }
                            if (player1_PvP != null)
                            {
                                
                                if (PLAYER2 != null)
                                {
                                    if (disparadorDelProyectil == DisparadorDelProyectil.Jugador2
                                    || cuadrilla.player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player1)
                                    {
                                        //AUMENTO XP PARA EL ATAQUE ESPECIAL
                                        //SI SE DEFIENDE CARGA LA MITAD DE LA BARRA DEL ATAQUE ESPECIAL.
                                        PLAYER2.SetXpActual(PLAYER2.GetXpActual() + (PLAYER2.xpForHit / 2));
                                    }
                                }
                                if (player1_PvP.playerSelected == Player_PvP.PlayerSelected.Defensivo)
                                {
                                    switch (player1_PvP.playerState)
                                    {
                                        case Player_PvP.State.Defendido:
                                            cuadrilla.player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.ContraAtaque;
                                            if (player1_PvP.playerActual == Player_PvP.Player.player1)
                                            {
                                                cuadrilla.player.Attack(DisparadorDelProyectil.Jugador1);
                                            }
                                            else if (player1_PvP.playerActual == Player_PvP.Player.player2)
                                            {
                                                cuadrilla.player.Attack(DisparadorDelProyectil.Enemigo);
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
                                else
                                {
                                    realDamage = damage - cuadrilla.player.pointsDeffence;
                                    cuadrilla.player.PD.lifePlayer = cuadrilla.player.PD.lifePlayer - realDamage;
                                    timeLife = 0;
                                    damage = auxDamage;
                                    poolObject.Recycle();
                                    gameObject.SetActive(false);
                                }
                            }
                            else
                            {
                                if (disparadorDelProyectil == DisparadorDelProyectil.Enemigo||disparadorDelProyectil == DisparadorDelProyectil.Jugador2 || disparadorDelProyectil == DisparadorDelProyectil.Jugador1 && gm.enumsGameManager.modoDeJuego == EnumsGameManager.ModosDeJuego.PvP)
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
                            }
                            
                        }
                        if (cuadrilla.enemy != null)
                        {
                            if (disparadorDelProyectil == DisparadorDelProyectil.Jugador1)
                            {
                                //AUMENTO XP PARA EL ATAQUE ESPECIAL
                                //SI SE DEFIENDE CARGA LA MITAD DE LA BARRA DEL ATAQUE ESPECIAL.
                                PLAYER1.SetXpActual(PLAYER1.GetXpActual() + (PLAYER1.xpForHit/2));
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
                                            Debug.Log("ENTRE");
                                            Defensivo enemyDeffended = cuadrilla.enemy.GetComponent<Defensivo>();
                                            if (enemyDeffended.GetStateDeffence() == Defensivo.StateDeffence.CounterAttackDeffense)
                                            {
                                                cuadrilla.enemy.spriteEnemy.ActualSprite = SpriteEnemy.SpriteActual.ContraAtaque;
                                                cuadrilla.enemy.CounterAttack(true);
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
                            
                        }
                        
                    }
                    break;
            }
        }
    }
}
