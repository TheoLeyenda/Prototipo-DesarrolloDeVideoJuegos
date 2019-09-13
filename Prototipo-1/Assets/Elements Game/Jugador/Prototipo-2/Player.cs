using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Prototipo_2
{
    public class Player : MonoBehaviour
    {
        // Start is called before the first frame update
        public Grid gridPlayer;
        public float life;
        public float maxLife;
        private float auxLife;
        public EnumsPlayers enumsPlayers;
        public GameObject BARRA_DE_VIDA;
        public GameObject generadorProyectiles;
        private Animator animator;
        public Image ImageHP;
        public Button PadArrowUp;
        public Button PadArrowDown;
        public Button PadArrowLeft;
        public Button PadArrowRigth;
        public float SpeedJump;
        private GameManager gm;
        private Rigidbody2D rg2D;
        private bool doubleDamage;
        public Pool poolObjectAttack;

        void Start()
        {
            doubleDamage = false;
            enumsPlayers.movimiento = EnumsPlayers.Movimiento.Nulo;
            enumsPlayers.estadoJugador = EnumsPlayers.EstadoJugador.vivo;
            /*if (GameManager.instanceGameManager != null)
            {
                gm = GameManager.instanceGameManager;
            }*/
            rg2D = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            InputKeyBoard();
        }
        public void CheckDead()
        {
            if (life <= 0)
            {
                enumsPlayers.estadoJugador = EnumsPlayers.EstadoJugador.muerto;
                gm.GameOver();
                gm.ResetRoundCombat(true);
            }
        }
        public void Attack()
        {
            GameObject go = poolObjectAttack.GetObject();
            Proyectil proyectil = go.GetComponent<Proyectil>();
            proyectil.SetDobleDamage(doubleDamage);
            if (doubleDamage)
            {
                proyectil.damage = proyectil.damage * 2;
            }
            go.transform.position = generadorProyectiles.transform.position;
            proyectil.On();
            proyectil.ShootForward();
        }
        //HIDE HECHO MIERDA / HECHO PIJA BUSCALO BOLUDO
        public void InputKeyBoard()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Attack();
            }
        }
    }
}
