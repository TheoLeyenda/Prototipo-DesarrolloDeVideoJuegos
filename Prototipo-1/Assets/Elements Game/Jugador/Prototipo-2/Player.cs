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
        public GameObject generadorProyectilesAgachado;
        public GameObject generadorProyectilesParabola;
        public GameObject generadorProyectilesParabolaAgachado;
        private Animator animator;
        public Image ImageHP;
        public Button PadArrowUp;
        public Button PadArrowDown;
        public Button PadArrowLeft;
        public Button PadArrowRigth;
        public float SpeedJump;
        public float Speed;
        public float pointsDeffence;
        public float Resistace;
        public float Gravity;
        private float auxSpeedJump;
        private GameManager gm;
        private bool doubleDamage;
        private bool isJamping;
        private bool isDuck;
        private bool EnableCounterAttack;
        public Pool poolObjectAttack;
        public Pool poolObjectSpecialAttack;
        private Vector3 InitialPosition;
        public BoxCollider2D colliderSprite;
        public string ButtonDeffence;
        public string ButtonAttack;
        public string ButtonSpecialAttack;
        //public BoxCollider2D colliderCounterAttack;
        public float delayCounterAttack;
        private float auxDelayCounterAttack;
        void Start()
        {
            auxDelayCounterAttack = delayCounterAttack;
            colliderSprite.enabled = true;
            isDuck = false;
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
            if (GameManager.instanceGameManager != null)
            {
                gm = GameManager.instanceGameManager;
            }
            animator = GetComponent<Animator>();
            gridPlayer.CheckCuadrillaOcupada(structsPlayer.dataPlayer.columnaActual, structsPlayer.dataPlayer.CantCasillasOcupadas_X, structsPlayer.dataPlayer.CantCasillasOcupadas_Y);
        }

        // Update is called once per frame
        void Update()
        {
            InputKeyBoard();
            CheckOutLimit();
            CheckDead();
            CheckLifeBar();
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
        public void CheckDead()
        {
            if (life <= 0)
            {
                enumsPlayers.estadoJugador = EnumsPlayers.EstadoJugador.muerto;
                gm.GameOver();
                gm.ResetRoundCombat(true);
            }
        }
        public void AttackDown()
        {
            GameObject go = poolObjectAttack.GetObject();
            Proyectil proyectil = go.GetComponent<Proyectil>();
            proyectil.SetDobleDamage(doubleDamage);
            if (doubleDamage)
            {
                proyectil.damage = proyectil.damage * 2;
            }
            if (!isDuck)
            {
                go.transform.position = generadorProyectiles.transform.position;
            }
            else
            {
                go.transform.position = generadorProyectilesAgachado.transform.position;
            }
            proyectil.On();
            proyectil.disparadorDelProyectil = Proyectil.DisparadorDelProyectil.Jugador;
            proyectil.ShootForwardDown();
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
            if (!isDuck)
            {
                go.transform.position = generadorProyectiles.transform.position;
            }
            else
            {
                go.transform.position = generadorProyectilesAgachado.transform.position;
            }
            proyectil.On();
            proyectil.disparadorDelProyectil = Proyectil.DisparadorDelProyectil.Jugador;
            proyectil.ShootForward();
        }

        //ATAQUE EN PARABOLA.
        public void SpecialAttack()
        {
            GameObject go = poolObjectSpecialAttack.GetObject();
            ProyectilParabola proyectil = go.GetComponent<ProyectilParabola>();
            proyectil.SetDobleDamage(doubleDamage);
            if (doubleDamage)
            {
                proyectil.damage = proyectil.damage * 2;
            }
            if (!isDuck)
            {
                go.transform.position = generadorProyectilesParabola.transform.position;
            }
            else
            {
                go.transform.position = generadorProyectilesParabolaAgachado.transform.position;
            }
            proyectil.disparadorDelProyectil = Proyectil.DisparadorDelProyectil.Jugador;
            proyectil.rutaParabola_AtaqueJugador = structsPlayer.ruta;
            proyectil.OnParabola();
        }
        public void CheckOutLimit()
        {
            if (transform.position.y <= InitialPosition.y)
            {
                transform.position = new Vector3(transform.position.x, InitialPosition.y, transform.position.z);
            }
        }
        //HIDE HECHO MIERDA / HECHO PIJA BUSCALO BOLUDO
        public void InputKeyBoard()
        {
            //Debug.Log("Columna Actual:" + structsPlayer.dataEnemy.columnaActual);
            //Debug.Log("Movimiento actual:" + enumsPlayers.movimiento);
            if (Input.GetKeyDown(ButtonAttack) && enumsPlayers.movimiento == EnumsPlayers.Movimiento.Saltar && Input.GetKey(KeyCode.DownArrow))
            {
                AttackDown();
            }
            else if (Input.GetKeyDown(ButtonAttack))
            {
                Attack();
            }
            if (Input.GetKey(ButtonDeffence))
            {
                Deffence();
            }
            if (Input.GetKeyDown(ButtonSpecialAttack))
            {
                SpecialAttack();
            }
            if (Input.GetKeyUp(ButtonDeffence))
            {
                gridPlayer.CheckCuadrillaOcupada(structsPlayer.dataPlayer.columnaActual, structsPlayer.dataPlayer.CantCasillasOcupadas_X, structsPlayer.dataPlayer.CantCasillasOcupadas_Y);
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
                    SpeedJump = auxSpeedJump;
                }
                Jump(gridPlayer.matrizCuadrilla[0][structsPlayer.dataPlayer.columnaActual].transform.position);
            }

            if (Input.GetKey(KeyCode.DownArrow) && enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo)
            {
                Duck(structsPlayer.dataPlayer.CantCasillasOcupadas_Y);
            }
            else if(enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo && isDuck)
            {
                isDuck = false;
                colliderSprite.enabled = true;
                gridPlayer.CheckCuadrillaOcupada(structsPlayer.dataPlayer.columnaActual, structsPlayer.dataPlayer.CantCasillasOcupadas_X, structsPlayer.dataPlayer.CantCasillasOcupadas_Y);
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
                gridPlayer.matrizCuadrilla[gridPlayer.baseGrild][structsPlayer.dataPlayer.columnaActual].SetStateCuadrilla(Cuadrilla.StateCuadrilla.Libre);
                //Debug.Log(gridPlayer.matrizCuadrilla[gridPlayer.baseGrild][structsPlayer.dataPlayer.columnaActual].name);
            }
            else
            {
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
        public void Duck(int rangoAgachado)
        {
            isDuck = true;
            colliderSprite.enabled = false;
            for (int i = 0; i < structsPlayer.dataPlayer.CantCasillasOcupadas_X; i++)
            {
                gridPlayer.matrizCuadrilla[gridPlayer.GetCuadrilla_columnas() - rangoAgachado][structsPlayer.dataPlayer.columnaActual + i].SetStateCuadrilla(Cuadrilla.StateCuadrilla.Libre);
            }
            
        }
        public void Deffence()
        {
            for (int i = 0; i < gridPlayer.matrizCuadrilla.Count; i++)
            {
                for (int j = 0; j < gridPlayer.matrizCuadrilla[i].Count; j++) {
                    if (gridPlayer.matrizCuadrilla[i][j].GetStateCuadrilla() == Cuadrilla.StateCuadrilla.Ocupado)
                    {
                        gridPlayer.matrizCuadrilla[i][j].SetStateCuadrilla(Cuadrilla.StateCuadrilla.Defendido);
                    }
                }
            }
        }
        public bool GetEnableCounterAttack()
        {
            return EnableCounterAttack;
        }
        public void SetEnableCounterAttack(bool _enableCounterAttack)
        {
            EnableCounterAttack = _enableCounterAttack;
        }
        public float GetAuxDelayCounterAttack()
        {
            return auxDelayCounterAttack;
        }
    }
}
