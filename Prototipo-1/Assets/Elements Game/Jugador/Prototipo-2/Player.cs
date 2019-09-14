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
        public float Resistace;
        public float Gravity;
        private float auxSpeedJump;
        private GameManager gm;
        private bool doubleDamage;
        private bool isJamping;
        public Pool poolObjectAttack;
        private Vector3 InitialPosition;
        void Start()
        {
            auxSpeedJump = SpeedJump;
            InitialPosition = transform.position;
            isJamping = false;
            enumsPlayers.movimiento = EnumsPlayers.Movimiento.Nulo;
            structsPlayer.dataPlayer.CantCasillasOcupadas_X = 1;
            structsPlayer.dataPlayer.CantCasillasOcupadas_Y = 2;
            structsPlayer.dataPlayer.columnaActual = 1;
            doubleDamage = false;
            enumsPlayers.movimiento = EnumsPlayers.Movimiento.Nulo;
            enumsPlayers.estadoJugador = EnumsPlayers.EstadoJugador.vivo;
            /*if (GameManager.instanceGameManager != null)
            {
                gm = GameManager.instanceGameManager;
            }*/
            animator = GetComponent<Animator>();
            gridPlayer.CheckCuadrillaOcupada(structsPlayer.dataPlayer.columnaActual, structsPlayer.dataPlayer.CantCasillasOcupadas_X, structsPlayer.dataPlayer.CantCasillasOcupadas_Y);
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
                if (structsPlayer.dataPlayer.columnaActual > 0)
                {
                    MoveLeft(gridPlayer.matrizCuadrilla[gridPlayer.baseGrild][structsPlayer.dataPlayer.columnaActual-1].transform.position);
                }
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo ||
                enumsPlayers.movimiento == EnumsPlayers.Movimiento.MoverAdelante)
            {
                if (structsPlayer.dataPlayer.columnaActual < gridPlayer.GetCuadrilla_columnas()-1)
                {
                    MoveRight(gridPlayer.matrizCuadrilla[gridPlayer.baseGrild][structsPlayer.dataPlayer.columnaActual + 1].transform.position);
                }
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) && enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo
                || enumsPlayers.movimiento == EnumsPlayers.Movimiento.Saltar)
            {
                if (enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo)
                {
                    isJamping = true;
                }
                Jump(gridPlayer.matrizCuadrilla[0][structsPlayer.dataPlayer.columnaActual].transform.position);
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
        public void MoveJamp(Vector3 direccion)
        {
            if (direccion == Vector3.up)
            {
                transform.Translate(direccion * SpeedJump * Time.deltaTime);
                SpeedJump = SpeedJump - Time.deltaTime *Resistace;
            }
            else if (direccion == Vector3.down)
            {
                transform.Translate(direccion * SpeedJump * Time.deltaTime);
                SpeedJump = SpeedJump + Time.deltaTime * Gravity;
            }
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
                structsPlayer.dataPlayer.columnaActual--;
                enumsPlayers.movimiento = EnumsPlayers.Movimiento.Nulo;
                gridPlayer.CheckCuadrillaOcupada(structsPlayer.dataPlayer.columnaActual, structsPlayer.dataPlayer.CantCasillasOcupadas_X, structsPlayer.dataPlayer.CantCasillasOcupadas_Y);
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
                structsPlayer.dataPlayer.columnaActual++;
                enumsPlayers.movimiento = EnumsPlayers.Movimiento.Nulo;
                gridPlayer.CheckCuadrillaOcupada(structsPlayer.dataPlayer.columnaActual, structsPlayer.dataPlayer.CantCasillasOcupadas_X, structsPlayer.dataPlayer.CantCasillasOcupadas_Y);
            }
        }
        public void Jump(Vector3 alturaMaxima)
        {
            if (CheckMove(new Vector3(transform.position.x,alturaMaxima.y, transform.position.z)) && isJamping)
            {
                enumsPlayers.movimiento = EnumsPlayers.Movimiento.Saltar;
                MoveJamp(Vector3.up);
                if (SpeedJump <= 0)
                {
                    isJamping = false;
                }
            }
            else
            {
                Debug.Log("ENTRE");
                isJamping = false;
                if (CheckMove(new Vector3(transform.position.x, InitialPosition.y, transform.position.z)))
                {
                    MoveJamp(Vector3.down);
                }
                else
                {
                    enumsPlayers.movimiento = EnumsPlayers.Movimiento.Nulo;
                    SpeedJump = auxSpeedJump;
                }
            }
        }
        public void Duck()
        {
            
        }
    }
}
