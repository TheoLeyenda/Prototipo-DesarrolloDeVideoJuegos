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
        public StructsPlayer structsPlayer;
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
        public float Speed;
        private GameManager gm;
        private bool doubleDamage;
        private bool isMoving;
        public Pool poolObjectAttack;

        void Start()
        {
            enumsPlayers.movimiento = EnumsPlayers.Movimiento.Nulo;
            structsPlayer.dataEnemy.CantCasillasOcupadas_X = 1;
            structsPlayer.dataEnemy.CantCasillasOcupadas_Y = 2;
            structsPlayer.dataEnemy.columnaActual = 1;
            doubleDamage = false;
            enumsPlayers.movimiento = EnumsPlayers.Movimiento.Nulo;
            enumsPlayers.estadoJugador = EnumsPlayers.EstadoJugador.vivo;
            /*if (GameManager.instanceGameManager != null)
            {
                gm = GameManager.instanceGameManager;
            }*/
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
        public void MoveLeft()
        {
            if (structsPlayer.dataEnemy.columnaActual > 0 && structsPlayer.dataEnemy.columnaActual <= 2)
            {
                if (CheckMove(new Vector3(gridPlayer.leftCuadrilla.transform.position.x, transform.position.y, transform.position.z)) && transform.position.x>= gridPlayer.leftCuadrilla.transform.position.x)
                {
                    Move(Vector3.left);
                }
                else
                {
                    structsPlayer.dataEnemy.columnaActual--;
                    enumsPlayers.movimiento = EnumsPlayers.Movimiento.Nulo;
                    gridPlayer.CheckCuadrillaOcupada(structsPlayer.dataEnemy.columnaActual, structsPlayer.dataEnemy.CantCasillasOcupadas_X, structsPlayer.dataEnemy.CantCasillasOcupadas_Y);
                }
            }
        }
        public void MoveRight()
        {
            if (structsPlayer.dataEnemy.columnaActual >=0 && structsPlayer.dataEnemy.columnaActual < 2)
            {
                if (CheckMove(new Vector3(gridPlayer.leftCuadrilla.transform.position.x, transform.position.y, transform.position.z)) && transform.position.x <= gridPlayer.rightCuadrilla.transform.position.x)
                {
                    Move(Vector3.right);
                }
                else
                {
                    structsPlayer.dataEnemy.columnaActual++;
                    enumsPlayers.movimiento = EnumsPlayers.Movimiento.Nulo;
                    gridPlayer.CheckCuadrillaOcupada(structsPlayer.dataEnemy.columnaActual, structsPlayer.dataEnemy.CantCasillasOcupadas_X, structsPlayer.dataEnemy.CantCasillasOcupadas_Y);
                }
            }
        }
        //HIDE HECHO MIERDA / HECHO PIJA BUSCALO BOLUDO
        public void InputKeyBoard()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Attack();
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo 
                || enumsPlayers.movimiento == EnumsPlayers.Movimiento.MoverAtras)
            {
                enumsPlayers.movimiento = EnumsPlayers.Movimiento.MoverAtras;
                MoveLeft();
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo ||
                enumsPlayers.movimiento == EnumsPlayers.Movimiento.MoverAdelante)
            {
                enumsPlayers.movimiento = EnumsPlayers.Movimiento.MoverAdelante;
                MoveRight();
            }
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
    }
}
