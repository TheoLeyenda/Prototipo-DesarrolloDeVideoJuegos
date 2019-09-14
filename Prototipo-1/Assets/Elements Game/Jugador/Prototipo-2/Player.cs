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
            gridPlayer.CheckCuadrillaOcupada(structsPlayer.dataEnemy.columnaActual, structsPlayer.dataEnemy.CantCasillasOcupadas_X, structsPlayer.dataEnemy.CantCasillasOcupadas_Y);
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
            //Debug.Log("Columna Actual:" + structsPlayer.dataEnemy.columnaActual);
            //Debug.Log("Movimiento actual:" + enumsPlayers.movimiento);
            if (Input.GetKeyDown(KeyCode.F))
            {
                Attack();
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo 
                || enumsPlayers.movimiento == EnumsPlayers.Movimiento.MoverAtras)
            {
                if (structsPlayer.dataEnemy.columnaActual > 0)
                {
                    MoveLeft(gridPlayer.matrizCuadrilla[gridPlayer.baseGrild][structsPlayer.dataEnemy.columnaActual-1].transform.position);
                }
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo ||
                enumsPlayers.movimiento == EnumsPlayers.Movimiento.MoverAdelante)
            {
                if (structsPlayer.dataEnemy.columnaActual < gridPlayer.GetCuadrilla_columnas()-1)
                {
                    MoveRight(gridPlayer.matrizCuadrilla[gridPlayer.baseGrild][structsPlayer.dataEnemy.columnaActual + 1].transform.position);
                }
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

        public void MoveLeft(Vector3 cuadrillaDestino)
        {   
            if (CheckMove(new Vector3(gridPlayer.leftCuadrilla.transform.position.x, transform.position.y, transform.position.z)) && transform.position.x > cuadrillaDestino.x)
            {
                Move(Vector3.left);
                enumsPlayers.movimiento = EnumsPlayers.Movimiento.MoverAtras;
            }
            else if(enumsPlayers.movimiento != EnumsPlayers.Movimiento.Nulo)
            {
                structsPlayer.dataEnemy.columnaActual--;
                enumsPlayers.movimiento = EnumsPlayers.Movimiento.Nulo;
                gridPlayer.CheckCuadrillaOcupada(structsPlayer.dataEnemy.columnaActual, structsPlayer.dataEnemy.CantCasillasOcupadas_X, structsPlayer.dataEnemy.CantCasillasOcupadas_Y);
            }
        }
        public void MoveRight(Vector3 cuadrillaDestino)
        {
            if (CheckMove(new Vector3(gridPlayer.rightCuadrilla.transform.position.x, transform.position.y, transform.position.z)) && transform.position.x < cuadrillaDestino.x)
            {
                Move(Vector3.right);
                enumsPlayers.movimiento = EnumsPlayers.Movimiento.MoverAdelante;
            }
            else if (enumsPlayers.movimiento != EnumsPlayers.Movimiento.Nulo)
            {
                structsPlayer.dataEnemy.columnaActual++;
                enumsPlayers.movimiento = EnumsPlayers.Movimiento.Nulo;
                gridPlayer.CheckCuadrillaOcupada(structsPlayer.dataEnemy.columnaActual, structsPlayer.dataEnemy.CantCasillasOcupadas_X, structsPlayer.dataEnemy.CantCasillasOcupadas_Y);
            }
        }
        public void Jump(Vector3 alturaMaxima)
        {
            if (CheckMove(alturaMaxima))
            {
                
            }
        }
        public void Duck()
        {

        }
    }
}
