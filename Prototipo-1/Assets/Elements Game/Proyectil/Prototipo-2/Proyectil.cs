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
            Jugador,
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
        protected GameManager gm;
        public DisparadorDelProyectil disparadorDelProyectil;
        private void Start()
        {
            if (GameManager.instanceGameManager != null)
            {
                gm = GameManager.instanceGameManager;
            }
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
                poolObject.Recycle();
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
        private void OnTriggerStay2D(Collider2D collision)
        {
            switch (collision.tag)
            {
                case "Escudo":
                    timeLife = 0;
                    break;
                case "Cuadrilla":
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
                            if (disparadorDelProyectil == DisparadorDelProyectil.Jugador)
                            {
                                if (cuadrilla.enemy.enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.MoveToPointCombat && cuadrilla.enemy.enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.MoveToPointDeath)
                                {
                                    cuadrilla.enemy.spriteEnemyActual.ActualSprite = SpriteEnemy.SpriteActual.RecibirDanio;
                                    cuadrilla.enemy.life = cuadrilla.enemy.life - damage;
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
                                    if (Input.GetKey(cuadrilla.player.ButtonDeffence))
                                    {
                                        cuadrilla.player.Attack();
                                        cuadrilla.player.delayCounterAttack = cuadrilla.player.GetAuxDelayCounterAttack();
                                        timeLife = 0;
                                    }
                                }
                                if (cuadrilla.player.delayCounterAttack <= 0 && timeLife <= 0)
                                {
                                    cuadrilla.player.PD.lifePlayer = cuadrilla.player.PD.lifePlayer - damage;
                                    cuadrilla.player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.RecibirDanio;
                                }
                                else if (cuadrilla.player.delayCounterAttack <= 0 && timeLife > 0)
                                {
                                    cuadrilla.player.delayCounterAttack = cuadrilla.player.GetAuxDelayCounterAttack();
                                    cuadrilla.player.SetEnableCounterAttack(false);
                                    cuadrilla.player.PD.lifePlayer = cuadrilla.player.PD.lifePlayer - damage;
                                    cuadrilla.player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.RecibirDanio;
                                    timeLife = 0;
                                    gameObject.SetActive(false);
                                    //Debug.Log("ENTRE AL DESTRUCTOR POR CONTRA ATAQUE");
                                }
                            }
                            
                        }
                    }
                    if (cuadrilla.GetStateCuadrilla() == Cuadrilla.StateCuadrilla.Defendido)
                    {

                        if (cuadrilla.player != null)
                        {
                            if (disparadorDelProyectil == DisparadorDelProyectil.Enemigo)
                            {
                                if (cuadrilla.player.delayCounterAttack <= 0)
                                {
                                    float realDamage = damage - cuadrilla.player.pointsDeffence;
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
                        if (cuadrilla.enemy != null)
                        {
                            if (disparadorDelProyectil == DisparadorDelProyectil.Jugador)
                            {
                                if (cuadrilla.enemy.enumsEnemy.typeEnemy != EnumsEnemy.TiposDeEnemigo.Defensivo)
                                {
                                    float realDamage = damage - cuadrilla.enemy.pointsDeffence;
                                    cuadrilla.enemy.life = cuadrilla.enemy.life - realDamage;
                                }
                                else
                                {
                                    cuadrilla.enemy.spriteEnemyActual.ActualSprite = SpriteEnemy.SpriteActual.ContraAtaque;
                                    cuadrilla.enemy.CounterAttack(true);
                                    if (cuadrilla.enemy.damageCounterAttack)
                                    {
                                        float realDamage = damage - cuadrilla.enemy.pointsDeffence;
                                        cuadrilla.enemy.life = cuadrilla.enemy.life - realDamage;
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
