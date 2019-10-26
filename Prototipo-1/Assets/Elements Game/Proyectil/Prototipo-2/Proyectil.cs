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
            
        }
    }
}
