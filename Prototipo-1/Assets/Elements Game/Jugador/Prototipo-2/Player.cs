using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Prototipo_2
{
    public class Player : MonoBehaviour
    {
        // Start is called before the first frame update
        public bool resetPlayer;
        public PlayerData PD;
        public Grid gridPlayer;
        private float auxLife;
        public StructsPlayer structsPlayer;
        public List<SpritePlayer> spritePlayers;
        [HideInInspector]
        public SpritePlayer spritePlayerActual;
        public EnumsPlayers enumsPlayers;
        public GameObject BARRA_DE_VIDA;
        public GameObject BARRA_DE_CARGA;
        public GameObject generadorProyectiles;
        public GameObject generadorProyectilesAgachado;
        public GameObject generadorProyectilesParabola;
        public GameObject generadorProyectilesParabolaAgachado;
        private Animator animator;
        public Image ImageHP;
        public Image ImageCarga;
        private float xpActual;
        public float xpNededSpecialAttack;
        public float xpForHit;
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
        private bool isJumping;
        private bool isDuck;
        private bool EnableCounterAttack;
        private Vector3 InitialPosition;
        public BoxCollider2D colliderSprite;
        public string ButtonDeffence;
        public string ButtonAttack;
        public string ButtonSpecialAttack;
        //public BoxCollider2D colliderCounterAttack;
        public float delayCounterAttack;
        public bool SpecialAttackEnabelEveryMoment;
        private float auxDelayCounterAttack;
        private bool controllerJoystick;
        public bool DoubleSpeed;
        public bool LookingForward;
        public bool LookingBack;
        public float delayAttack;
        private float auxDelayAttack;
        private bool enableAttack;
        private bool enableSpecialAttack;
        public BoxCollider2D boxColliderParado;
        public BoxCollider2D boxColliderAgachado;
        public string NameInputManager;
        private InputManager inputManager;
        void Start()
        {
            GameObject go = GameObject.Find(NameInputManager);
            inputManager = go.GetComponent<InputManager>();
            xpActual = 0;
            enableSpecialAttack = false;
            enableAttack = true;
            auxDelayAttack = delayAttack;
            delayAttack = 0;
            controllerJoystick = false;
            if (resetPlayer)
            {
                ResetPlayer();
            }
            CheckSpritePlayerActual();
            auxDelayCounterAttack = delayCounterAttack;
            colliderSprite.enabled = true;
            isDuck = false;
            auxSpeedJump = SpeedJump;
            InitialPosition = transform.position;
            isJumping = false;
            enumsPlayers.movimiento = EnumsPlayers.Movimiento.Nulo;
            structsPlayer.dataPlayer.CantCasillasOcupadas_X = 1;
            structsPlayer.dataPlayer.CantCasillasOcupadas_Y = 2;
            structsPlayer.dataPlayer.CantCasillasOcupadasAgachado = structsPlayer.dataPlayer.CantCasillasOcupadas_Y /2;
            structsPlayer.dataPlayer.CantCasillasOcupadasParado = structsPlayer.dataPlayer.CantCasillasOcupadas_Y;
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
            CheckOutLimit();
            CheckDead();
            CheckLifeBar();
            DelayEnableAttack();
            CheckLoadSpecialAttackBar();
            CheckBoxColliderActivate();
            CheckMovementInSpecialAttack();
        }
        public void CheckMovementInSpecialAttack()
        {
            switch (enumsPlayers.specialAttackEquipped)
            {
                case EnumsPlayers.SpecialAttackEquipped.DisparoDeCarga:
                    if (structsPlayer.dataAttack.DisparoDeCarga.activeSelf)
                    {
                        if (enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player1)
                        {
                            inputManager.SetEnableMovementPlayer1(false);
                        }
                        else if (enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player2)
                        {
                            inputManager.SetEnableMovementPlayer2(false);
                        }
                    }
                    else
                    {
                        if (enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player1)
                        {
                            inputManager.SetEnableMovementPlayer1(true);
                        }
                        else if (enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player2)
                        {
                            inputManager.SetEnableMovementPlayer2(true);
                        }
                    }
                    break;
            }
        }
        public void ResetPlayer()
        {
            PD.lifePlayer = PD.maxLifePlayer;
        }

        public void CheckSpritePlayerActual()
        {
            for (int i = 0; i < spritePlayers.Count; i++)
            {
                if (spritePlayers[i].gameObject.activeSelf)
                {
                    spritePlayerActual = spritePlayers[i];
                }
            }
        }
        public void CheckLoadSpecialAttackBar()
        {
            if (xpActual >= xpNededSpecialAttack)
            {
                xpActual = xpNededSpecialAttack;
                enableSpecialAttack = true;
            }
            if (xpActual <= xpNededSpecialAttack)
            {
                ImageCarga.fillAmount = xpActual / xpNededSpecialAttack;
            }
            if (xpActual < 0)
            {
                xpActual = 0;
            }
        }
        public void CheckLifeBar()
        {
            if (PD.lifePlayer <= PD.maxLifePlayer)
            {
                ImageHP.fillAmount = PD.lifePlayer / PD.maxLifePlayer;
            }
            else if (PD.lifePlayer > PD.maxLifePlayer)
            {
                PD.lifePlayer = PD.maxLifePlayer;
            }
            else if (PD.lifePlayer < 0)
            {
                PD.lifePlayer = 0;
            }
        }
        public void CheckDead()
        {
            if (PD.lifePlayer <= 0)
            {
                PD.lifePlayer = PD.maxLifePlayer;
                enumsPlayers.estadoJugador = EnumsPlayers.EstadoJugador.muerto;
                gm.GameOver("GameOver");
                gm.ResetRoundCombat(true);
            }
        }
        public void AttackDown(Proyectil.DisparadorDelProyectil disparador)
        {
            if (enableAttack)
            {
                GameObject go = structsPlayer.dataAttack.poolProyectil.GetObject();
                Proyectil proyectil = go.GetComponent<Proyectil>();
                switch (enumsPlayers.numberPlayer)
                {
                    case EnumsPlayers.NumberPlayer.player1:
                        proyectil.SetPlayer(gameObject.GetComponent<Player>());
                        disparador = Proyectil.DisparadorDelProyectil.Jugador1;
                        break;
                    case EnumsPlayers.NumberPlayer.player2:
                        proyectil.SetPlayer2(gameObject.GetComponent<Player>());
                        disparador = Proyectil.DisparadorDelProyectil.Jugador2;
                        break;
                }
                proyectil.SetDobleDamage(doubleDamage);
                if (doubleDamage)
                {
                    proyectil.damage = proyectil.damage * 2;
                }
                if (!isDuck)
                {
                    go.transform.position = generadorProyectiles.transform.position;
                    go.transform.rotation = generadorProyectiles.transform.rotation;
                }
                else
                {
                    go.transform.position = generadorProyectilesAgachado.transform.position;
                    go.transform.rotation = generadorProyectilesAgachado.transform.rotation;
                }
                proyectil.disparadorDelProyectil = disparador;
                proyectil.On();
                proyectil.ShootForwardDown();
                delayAttack = auxDelayAttack;
            }
        }
        public void DelayEnableAttack()
        {
            if (delayAttack > 0)
            {
                delayAttack = delayAttack - Time.deltaTime;
                enableAttack = false;
            }
            else
            {
                enableAttack = true;
            }
        }
        public void Attack(Proyectil.DisparadorDelProyectil disparador)
        {
            if (enableAttack)
            {
                GameObject go = structsPlayer.dataAttack.poolProyectil.GetObject();
                Proyectil proyectil = go.GetComponent<Proyectil>();
                switch (enumsPlayers.numberPlayer)
                {
                    case EnumsPlayers.NumberPlayer.player1:
                        proyectil.SetPlayer(gameObject.GetComponent<Player>());
                        disparador = Proyectil.DisparadorDelProyectil.Jugador1;
                        break;
                    case EnumsPlayers.NumberPlayer.player2:
                        proyectil.SetPlayer2(gameObject.GetComponent<Player>());
                        disparador = Proyectil.DisparadorDelProyectil.Jugador2;
                        break;
                }
                proyectil.SetDobleDamage(doubleDamage);
                if (doubleDamage)
                {
                    proyectil.damage = proyectil.damage * 2;
                }
                if (!isDuck)
                {
                    go.transform.position = generadorProyectiles.transform.position;
                    go.transform.rotation = generadorProyectiles.transform.rotation;
                }
                else
                {
                    go.transform.position = generadorProyectilesAgachado.transform.position;
                    go.transform.rotation = generadorProyectilesAgachado.transform.rotation;
                }
                proyectil.On();
                proyectil.disparadorDelProyectil = disparador;
                proyectil.ShootForward();
                delayAttack = auxDelayAttack;
            }
        }

        //ATAQUE EN PARABOLA.
        public void SpecialAttack(Proyectil.DisparadorDelProyectil disparador)
        {
            switch (enumsPlayers.specialAttackEquipped)
            {
                case EnumsPlayers.SpecialAttackEquipped.Default:
                    if (enableSpecialAttack)
                    {
                        GameObject go = structsPlayer.dataAttack.poolProyectilParabola.GetObject();
                        ProyectilParabola proyectil = go.GetComponent<ProyectilParabola>();
                        proyectil.SetDobleDamage(doubleDamage);
                        proyectil.disparadorDelProyectil = disparador;
                        if (doubleDamage)
                        {
                            proyectil.damage = proyectil.damage * 2;
                        }
                        if (!isDuck)
                        {
                            proyectil.TypeRoot = 1;
                            go.transform.position = generadorProyectilesParabola.transform.position;
                        }
                        else
                        {
                            proyectil.TypeRoot = 2;
                            go.transform.position = generadorProyectilesParabolaAgachado.transform.position;
                        }
                        switch (proyectil.TypeRoot)
                        {
                            case 1:
                                proyectil.rutaParabola_AtaqueJugador = structsPlayer.ruta;
                                break;
                            case 2:
                                proyectil.rutaParabolaAgachado_AtaqueJugador = structsPlayer.rutaAgachado;
                                break;
                        }
                        proyectil.rutaParabola_AtaqueJugador = structsPlayer.ruta;
                        proyectil.OnParabola();
                        enableSpecialAttack = false;
                        xpActual = 0;
                    }
                    break;
                case EnumsPlayers.SpecialAttackEquipped.GranadaGaseosa:
                    if (enableSpecialAttack)
                    {
                        GameObject go = structsPlayer.dataAttack.poolGranadaGaseosa.GetObject();
                        ProyectilParabola proyectil = go.GetComponent<ProyectilParabola>();
                        proyectil.SetDobleDamage(doubleDamage);
                        proyectil.disparadorDelProyectil = disparador;
                        if (doubleDamage)
                        {
                            proyectil.damage = proyectil.damage * 2;
                        }
                        if (!isDuck)
                        {
                            proyectil.TypeRoot = 1;
                            go.transform.position = generadorProyectilesParabola.transform.position;
                        }
                        else
                        {
                            proyectil.TypeRoot = 2;
                            go.transform.position = generadorProyectilesParabolaAgachado.transform.position;
                        }
                        switch (proyectil.TypeRoot)
                        {
                            case 1:
                                proyectil.rutaParabola_AtaqueJugador = structsPlayer.ruta;
                                break;
                            case 2:
                                proyectil.rutaParabolaAgachado_AtaqueJugador = structsPlayer.rutaAgachado;
                                break;
                        }
                        proyectil.rutaParabola_AtaqueJugador = structsPlayer.ruta;
                        proyectil.OnParabola();
                        enableSpecialAttack = false;
                        xpActual = 0;
                    }
                    break;
                case EnumsPlayers.SpecialAttackEquipped.DisparoDeCarga:
                    if (enableSpecialAttack)
                    {
                        if (!isJumping && !isDuck
                        && enumsPlayers.movimiento != EnumsPlayers.Movimiento.Saltar
                        && enumsPlayers.movimiento != EnumsPlayers.Movimiento.SaltoAtaque
                        && enumsPlayers.movimiento != EnumsPlayers.Movimiento.SaltoDefensa)
                        {
                            structsPlayer.dataAttack.DisparoDeCarga.SetActive(true);
                            enableSpecialAttack = false;
                            xpActual = 0;
                        }
                    }
                    break;
                case EnumsPlayers.SpecialAttackEquipped.ProyectilImparable:
                    if (enableSpecialAttack)
                    {
                        if (!isJumping && !isDuck
                        && enumsPlayers.movimiento != EnumsPlayers.Movimiento.Saltar
                        && enumsPlayers.movimiento != EnumsPlayers.Movimiento.SaltoAtaque
                        && enumsPlayers.movimiento != EnumsPlayers.Movimiento.SaltoDefensa)
                        {
                            GameObject go = structsPlayer.dataAttack.poolProyectilImparable.GetObject();
                            ProyectilInparable proyectilInparable = go.GetComponent<ProyectilInparable>();
                            proyectilInparable.SetEnemy(gameObject.GetComponent<Enemy>());
                            proyectilInparable.disparadorDelProyectil = Proyectil.DisparadorDelProyectil.Enemigo;
                            if (enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player1)
                            {
                                proyectilInparable.SetPlayer(gameObject.GetComponent<Player>());
                            }
                            else if (enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player2)
                            {
                                proyectilInparable.SetPlayer2(gameObject.GetComponent<Player>());
                            }
                            go.transform.position = generadorProyectilesAgachado.transform.position;
                            go.transform.rotation = generadorProyectilesAgachado.transform.rotation;
                            proyectilInparable.ShootForward();
                            enableSpecialAttack = false;
                            xpActual = 0;
                        }
                    }
                    break;
            }
        }
        public void CheckOutLimit()
        {
            if (transform.position.y <= InitialPosition.y)
            {
                transform.position = new Vector3(transform.position.x, InitialPosition.y, transform.position.z);
            }
        }
        //HIDE HECHO MIERDA / HECHO PIJA BUSCALO BOLUDO
        
        public void MovementLeft()
        {
            if (LookingForward)
            {
                if (structsPlayer.dataPlayer.columnaActual > 0)
                {
                    MoveLeft(gridPlayer.matrizCuadrilla[gridPlayer.baseGrild][structsPlayer.dataPlayer.columnaActual - 1].transform.position);
                }
            }
            else if (LookingBack)
            {
                if (structsPlayer.dataPlayer.columnaActual > 0)
                {
                    MoveLeft(gridPlayer.matrizCuadrilla[gridPlayer.baseGrild][structsPlayer.dataPlayer.columnaActual - 1].transform.position);
                }
            }
        }
        public void MovementRight()
        {
            if (LookingForward)
            {
                if (structsPlayer.dataPlayer.columnaActual < gridPlayer.GetCuadrilla_columnas() - 1)
                {
                    MoveRight(gridPlayer.matrizCuadrilla[gridPlayer.baseGrild][structsPlayer.dataPlayer.columnaActual + 1].transform.position);
                }
            }
            else if (LookingBack)
            {
                if (structsPlayer.dataPlayer.columnaActual < gridPlayer.GetCuadrilla_columnas() - 1)
                {
                    MoveRight(gridPlayer.matrizCuadrilla[gridPlayer.baseGrild][structsPlayer.dataPlayer.columnaActual + 1].transform.position);
                }
            }
        }
        public void MovementJump()
        {
            if (enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo)
            {
                isJumping = true;
                SpeedJump = auxSpeedJump;
            }
            Jump(gridPlayer.matrizCuadrilla[0][structsPlayer.dataPlayer.columnaActual].transform.position);
        }
        public void MovementDuck()
        {
            Duck(structsPlayer.dataPlayer.CantCasillasOcupadas_Y);
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
                SpeedJump = SpeedJump - Time.deltaTime * Resistace;
            }
            else if (direccion == Vector3.down)
            {
                transform.Translate(direccion * SpeedJump * Time.deltaTime);
                SpeedJump = SpeedJump + Time.deltaTime * Gravity;
            }
        }

        public void MoveLeft(Vector3 cuadrillaDestino)
        {
            if (LookingForward)
            {
                if (CheckMove(new Vector3(gridPlayer.leftCuadrilla.transform.position.x, transform.position.y, transform.position.z)) && transform.position.x > cuadrillaDestino.x)
                {
                    Move(Vector3.left);
                    enumsPlayers.movimiento = EnumsPlayers.Movimiento.MoverAtras;
                }
                else if (enumsPlayers.movimiento != EnumsPlayers.Movimiento.Nulo)
                {
                    structsPlayer.dataPlayer.columnaActual--;
                    enumsPlayers.movimiento = EnumsPlayers.Movimiento.Nulo;
                    gridPlayer.CheckCuadrillaOcupada(structsPlayer.dataPlayer.columnaActual, structsPlayer.dataPlayer.CantCasillasOcupadas_X, structsPlayer.dataPlayer.CantCasillasOcupadas_Y);
                }
            }
            else if (LookingBack)
            {
                if (CheckMove(new Vector3(gridPlayer.leftCuadrilla.transform.position.x, transform.position.y, transform.position.z)) && transform.position.x > cuadrillaDestino.x)
                {
                    Move(-Vector3.left);
                    enumsPlayers.movimiento = EnumsPlayers.Movimiento.MoverAtras;
                }
                else 
                {
                    Debug.Log("ENTRE BACK");
                    structsPlayer.dataPlayer.columnaActual--;
                    enumsPlayers.movimiento = EnumsPlayers.Movimiento.Nulo;
                    gridPlayer.CheckCuadrillaOcupada(structsPlayer.dataPlayer.columnaActual, structsPlayer.dataPlayer.CantCasillasOcupadas_X, structsPlayer.dataPlayer.CantCasillasOcupadas_Y);
                }
            }
        }
        public void MoveRight(Vector3 cuadrillaDestino)
        {
            if (LookingForward)
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
            else if (LookingBack)
            {
                if (CheckMove(new Vector3(gridPlayer.rightCuadrilla.transform.position.x, transform.position.y, transform.position.z)) && transform.position.x < cuadrillaDestino.x)
                {
                    Move(-Vector3.right);
                    enumsPlayers.movimiento = EnumsPlayers.Movimiento.MoverAdelante;
                }
                else 
                {
                    structsPlayer.dataPlayer.columnaActual++;
                    enumsPlayers.movimiento = EnumsPlayers.Movimiento.Nulo;
                    gridPlayer.CheckCuadrillaOcupada(structsPlayer.dataPlayer.columnaActual, structsPlayer.dataPlayer.CantCasillasOcupadas_X, structsPlayer.dataPlayer.CantCasillasOcupadas_Y);
                }
            }
        }
        public void Jump(Vector3 alturaMaxima)
        {
            if (CheckMove(new Vector3(transform.position.x, alturaMaxima.y, transform.position.z)) && isJumping)
            {
                enumsPlayers.movimiento = EnumsPlayers.Movimiento.Saltar;
                MoveJamp(Vector3.up);
                if (SpeedJump <= 0)
                {
                    isJumping = false;
                }
                gridPlayer.matrizCuadrilla[gridPlayer.baseGrild][structsPlayer.dataPlayer.columnaActual].SetStateCuadrilla(Cuadrilla.StateCuadrilla.Libre);
                //Debug.Log(gridPlayer.matrizCuadrilla[gridPlayer.baseGrild][structsPlayer.dataPlayer.columnaActual].name);
            }
            else
            {
                isJumping = false;
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
        public void CheckBoxColliderActivate()
        {
            if (isDuck)
            {
                boxColliderParado.enabled = false;
                boxColliderAgachado.enabled = true;
            }
            else
            {
                boxColliderAgachado.enabled = false;
                boxColliderParado.enabled = true;
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

        public bool GetIsDuck()
        {
            return isDuck;
        }
        public void SetIsDuck(bool _isDuck)
        {
            isDuck = _isDuck;
        }
        public bool GetIsJumping()
        {
            return isJumping;
        }
        public void SetIsJumping(bool _isJumping)
        {
            isJumping = _isJumping;
        }
        public float GetAuxSpeedJump()
        {
            return auxSpeedJump;
        }
        public void SetAuxSpeedJump(float _auxSpeedJump)
        {
            auxSpeedJump = _auxSpeedJump;
        }
        public void SetControllerJoystick(bool _controllerJoystick)
        {
            controllerJoystick = _controllerJoystick;
        }
        public bool GetControllerJoystick()
        {
            return controllerJoystick;
        }
        public void SetEnableSpecialAttack(bool _enableSpecialAttack)
        {
            enableSpecialAttack = _enableSpecialAttack;
        }
        public void SetXpActual(float _xpActual)
        {
            xpActual = _xpActual;
        }
        public float GetXpActual()
        {
            return xpActual;
        }
        public bool GetEnableAttack()
        {
            return enableAttack;
        }
        public InputManager GetInputManager()
        {
            return inputManager;
        }
        public bool GetEnableSpecialAttack()
        {
            return enableSpecialAttack;
        }
    }
}
