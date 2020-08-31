using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public ApplyColorShoot applyColorShoot; 
    public enum ApplyColorShoot
    {
        Stela,
        Proyectil,
        StelaAndProyectil,
        None,
    }
    public Sprite myHeadSprite;
    public string fraseVictoria;
    public Color colorShoot;
    public bool EnableChargerSpecialAttackForHit = true;
    public string nameEnemy;
    public bool enableColorShootSpecialAttack;

    [HideInInspector]
    public bool enableDeffence = true;

    //DATOS PARA EL MOVIMIENTO
    public bool enableMovement;
    [SerializeField]
    protected bool inCombatPosition;
    public GameObject alturaMaxima;
    public GameObject[] posicionesDeMovimiento;
    //-------------------------------------------//
    public BarraDeEscudo barraDeEscudo;
    [HideInInspector]
    public bool enableSpecialAttack;
    public SpriteEnemy spriteEnemy;
    [HideInInspector]
    public float Blindaje;
    [HideInInspector]
    public float MaxBlindaje;
    public GameObject enemyPrefab;
    public Grid gridEnemy;
    public EnumsEnemy enumsEnemy;
    public StructsEnemys structsEnemys;
    public SpriteRenderer SpriteRendererEnemigo;
    public SpecialAttackParabolaEnemyController specialAttackParabolaEnemyController;
    private float auxLife;
    private Animator animator;
    public bool InPool;
    public Pool pool;
    //private PoolObject poolObjectEnemy;
    public float life;
    public float maxLife;
    [HideInInspector]
    public float xpActual;
    public float xpNededSpecialAttack;
    public float xpForHit;
    public Pool poolParabolaAttack;
    public Pool poolObjectAttack;
    protected Rigidbody2D rg2D;
    private GameManager gm;
    public GameObject generadoresProyectiles;
    public GameObject generadorProyectilesAgachado;
    public GameObject generadorProyectilParabola;
    public GameObject generadorProyectilParabolaAgachado;
    private float MinRangeRandom = 0;
    private float MaxRangeRandom = 100;
    private float TypeRandom = 3;
    protected float delaySelectMovement;
    public float maxRandomDelayMovement;
    public float minRandomDelayMovement;
    public float delayAttack;
    protected float auxDelayAttack;
    //protected float auxDelayParabolaAttack;
    private bool doubleDamage;
    private bool isDuck;
    private bool isDeffended;
    public float anguloAtaqueSalto;
    public float Speed;
    public float SpeedJump;
    private float auxSpeedJump;
    public float Resistace;
    public float Gravity;
    public float delayAttackJumping;
    private bool isJamping;
    public Collider2D colliderSprites;
    [HideInInspector]
    public Vector3 InitialPosition;
    [HideInInspector]
    public Vector3 pointOfDeath;
    [HideInInspector]
    public Vector3 pointOfCombat;
    public bool damageCounterAttack;
    public bool activateComportamiento;
    public BoxColliderController boxColliderPiernas;
    public BoxColliderController boxColliderSprite;
    public BoxColliderController boxColliderControllerAgachado;
    public BoxColliderController boxColliderControllerParado;
    public BoxColliderController boxColliderControllerSaltando;
    public bool enableMecanicParabolaAttack;
    [HideInInspector]
    public bool inAttack;
    protected EventWise eventWise;
    private bool insound;
    [HideInInspector]
    public bool myVictory = false;
    protected float valueAttack;

    public bool DontRestart;

    [Header("Porcentage: Movimiento")]
    public float MovePorcentage;
    public float JumpPorcentage;
    public float DuckPorcentage;
    public float IdlePorcentage;

    [Header("Porcentaje: Direccion de Movimiento")]
    public float MoveForwardPorcentage;
    public float MoveBackPorcentage;

    [Header("Porcentaje: Accion De Salto")]
    public float AttackJumpPorcentage;
    public float DefenceJumpPorcentage;
    public float SimpleJumpPorcentage;

    [Header("Porcentaje: Accion Agacharse")]
    public float AttackDuckPorcentage;
    public float DefenceDuckPorcentage;
    public float SimpleDuckPorcentage;

    [Header("Porcentaje: Accion Quieto")]
    public float AttackPorcentage;
    public float DeffensePorcentage;

    [Header("Porcentaje: Ataque Especial")]
    public float AttackSpecialPorcentage;

    [Header("Porcentaje: Tipo de ataque")]
    public float parabolaAttack;

    [Header("Datos del personaje para la grilla")]
    public int CantCasillasOcupadas_X;
    public int CantCasillasOcupadas_Y;
    public int ColumnaActual;

    private float tolerableStillTime;
    private float auxTolerableStillTime;

    private bool weitVictory;
    [HideInInspector]
    public float timeStuned = 0;

    private float auxSpeed;
    //private bool onceJump = false;
    public static event Action<Enemy, string> OnModifireState;
    public static event Action<Enemy, string> OnDisableModifireState;
    public static event Action<Enemy> OnDie;
    public static event Action<Enemy, string, string, Sprite> OnVictory;

    protected virtual void OnEnable()
    {
        enableDeffence = true;
        xpActual = 0;
        life = maxLife;
        delaySelectMovement = 0.2f;
        isJamping = false;
        myVictory = false;
        weitVictory = false;
        enumsEnemy.SetStateEnemy(EnumsEnemy.EstadoEnemigo.vivo);
        Player.OnDie += AnimationVictory;
    }
    protected virtual void OnDisable()
    {
        enableDeffence = true;
        Player.OnDie -= AnimationVictory;
        ResetSpeedJump();
        enemyPrefab.transform.position = new Vector3(500, 500, 500);
        inCombatPosition = false;
        isJamping = false;
        myVictory = false;
        weitVictory = false;
    }
    public virtual void Start()
    {
        auxSpeed = Speed;
        weitVictory = false;
        specialAttackParabolaEnemyController = GetComponent<SpecialAttackParabolaEnemyController>();
        tolerableStillTime = maxRandomDelayMovement;
        auxTolerableStillTime = maxRandomDelayMovement;
        eventWise = GameObject.Find("EventWise").GetComponent<EventWise>();
        enableSpecialAttack = false;
        auxSpeedJump = SpeedJump;
        InitialPosition = transform.position;
        auxDelayAttack = delayAttack;
        delaySelectMovement = 0.5f;
        auxLife = life;
        //poolObjectEnemy = GetComponent<PoolObject>();
        animator = GetComponent<Animator>();
        if (GameManager.instanceGameManager != null)
        {
            gm = GameManager.instanceGameManager;
        }
        rg2D = GetComponent<Rigidbody2D>();
        CheckInitialCharacter();
        enumsEnemy.SetStateEnemy(EnumsEnemy.EstadoEnemigo.vivo);
    }
    public void AnimationVictory(Player p) 
    {
        //Debug.Log("ENTRE");
        if (transform.position.y <= InitialPosition.y)
        {
            //Debug.Log("ENTRE");
            if (enumsEnemy.typeEnemy != EnumsEnemy.TiposDeEnemigo.Jefe)
            {
                spriteEnemy.GetAnimator().Play("Victory");
            }
            enableMovement = false;
            myVictory = true;
            //Debug.Log("SOY GLORIOSO");
            if (OnVictory != null)
            {
                //Debug.Log("SOY GLORIOSO");
                OnVictory(this, fraseVictoria, nameEnemy, myHeadSprite);
            }
        }
    }
    public void ResetSpeedJump()
    {
        if (life <= 0)
        {
            //Debug.Log("ENTRE");
            SpeedJump = auxSpeedJump;
        }
    }
    public virtual void Update()
    {
        if (weitVictory && transform.position.y <= InitialPosition.y) 
        {
            if (enumsEnemy.typeEnemy != EnumsEnemy.TiposDeEnemigo.Jefe)
            {
                spriteEnemy.GetAnimator().Play("Victory");
            }
            enableMovement = false;
            myVictory = true;
            weitVictory = false;
        }
        ResetSpeedJump();
        //BORRAR LUEGO DE TESTEO
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log(enumsEnemy.GetMovement());
            //Debug.Log("Enable Movement: " + enableMovement);
            //Debug.Log("Speed:" + Speed);
            //Debug.Log("SpeedJump:" + SpeedJump);
            //Debug.Log("Delay Movimiento: " + delaySelectMovement);
            //Debug.Log("SpriteEnemy:"+ spriteEnemy.nameActual);
            //Debug.Log(inCombatPosition);
            //Debug.Log(enumsEnemy.GetStateEnemy());
            //life = 0;
        }
        
        //-----------------------------------
        CheckState();
        CheckDead();

        if (enableMovement)
        {
     
            CheckDeffense();
            CheckBoxColliders2D();

            if (enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.AtaqueEspecial
                && enumsEnemy.GetStateEnemy() != EnumsEnemy.EstadoEnemigo.muerto)
            {
                IA();
                tolerableStillTime = auxTolerableStillTime;
            }
            else
            {
                tolerableStillTime = auxTolerableStillTime;
            }
            
        }
        CheckOutLimit();
    }
    public Enemy() { }

    public void CheckState()
    {
        switch (enumsEnemy.GetStateEnemy())
        {
            case EnumsEnemy.EstadoEnemigo.Atrapado:
                if(OnModifireState != null)
                {
                    OnModifireState(this, "Atrapado Chicle");
                }
                CheckStune(timeStuned);

                break;
        }
    }

    public void CheckStune(float timeStuned)
    {
        if (timeStuned > 0)
        {
            timeStuned = timeStuned - Time.deltaTime;
            //hacer que el color del enemigo se vea azul;
            spriteEnemy.spriteRenderer.color = Color.cyan;
            enableMovement = false;
        }
        else if (timeStuned <= 0)
        {
            enableMovement = true;
            //hacer que el color del enemigo se vea blanco;
            spriteEnemy.spriteRenderer.color = Color.white;
            if (life > 0)
            {
                enumsEnemy.SetStateEnemy(EnumsEnemy.EstadoEnemigo.vivo);
            }
            else
            {
                enumsEnemy.SetStateEnemy(EnumsEnemy.EstadoEnemigo.muerto);
            }
            if (OnDisableModifireState != null)
            {
                OnDisableModifireState(this, "Atrapado Chicle");
            }

        }
    }

    public void CheckInitialCharacter()
    {
        structsEnemys.dataEnemy.CantCasillasOcupadas_X = CantCasillasOcupadas_X;
        structsEnemys.dataEnemy.CantCasillasOcupadas_Y = CantCasillasOcupadas_Y;
        structsEnemys.dataEnemy.columnaActual = ColumnaActual;

    }
    public void CheckOutLimit()
    {
        if (enumsEnemy.GetStateEnemy() != EnumsEnemy.EstadoEnemigo.muerto && inCombatPosition)
        {
            if (transform.position.y <= InitialPosition.y && enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.Saltar && !isJamping
                || transform.position.y <= InitialPosition.y && enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.SaltoAtaque && !isJamping
                || transform.position.y <= InitialPosition.y && enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.SaltoDefensa && !isJamping
                || transform.position.y <= InitialPosition.y && enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AtaqueEspecialSalto && !isJamping)
            {
                transform.position = new Vector3(transform.position.x, InitialPosition.y, transform.position.z);
                delaySelectMovement = 0.1f;
                enumsEnemy.SetMovement(EnumsEnemy.Movimiento.Nulo);
                SpeedJump = auxSpeedJump;
            }
            else if (transform.position.y > InitialPosition.y && (life <= 0))
            {
                if (CheckMove(new Vector3(transform.position.x, InitialPosition.y, transform.position.z)))
                {
                    enumsEnemy.SetMovement(EnumsEnemy.Movimiento.Saltar);
                    MoveJamp(Vector3.down);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x, InitialPosition.y, transform.position.z);
                    CheckDead();
                }
                delaySelectMovement = 0.1f;
            }
        }

    }
    public void OnEnemy()
    {
        if (enumsEnemy.typeEnemy != EnumsEnemy.TiposDeEnemigo.Jefe)
        {
            boxColliderControllerParado.GetBoxCollider2D().enabled = true;
            boxColliderControllerAgachado.GetBoxCollider2D().enabled = false;
            boxColliderControllerSaltando.GetBoxCollider2D().enabled = false;
        }
        enumsEnemy.SetStateEnemy(EnumsEnemy.EstadoEnemigo.vivo);
        tolerableStillTime = auxTolerableStillTime;
        //poolObjectEnemy = GetComponent<PoolObject>();
        if (GameManager.instanceGameManager != null)
        {
            gm = GameManager.instanceGameManager;
        }
        CheckInitialCharacter();
        //spriteEnemy.ActualSprite = SpriteCharacter.SpriteActual.Parado;
        xpActual = 0;
    }
    public void IA()
    {
        //Debug.Log("ENTRE A IA");
        //Debug.Log(enumsEnemy.GetMovement());


        if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.MoveToPointCombat)
        {
            //Debug.Log("ESTOY ENTRANDO ACA CONCHAETUMADRE XD");
            delaySelectMovement = 10;
            CheckMovement();
            return;
        }
        if (enumsEnemy.typeEnemy != EnumsEnemy.TiposDeEnemigo.Jefe)
        {
            //Debug.Log("IP: "+InitialPosition.y);
            //Debug.Log("TY:" + transform.position.y);
            if (transform.position.y > InitialPosition.y && enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.MoveToPointCombat && inCombatPosition)
            {
                delaySelectMovement = 0.1f;
                //Debug.Log("ESTOY ENTRANDO");
                if (enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.SaltoAtaque && enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.SaltoDefensa && enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.Saltar && enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.MoveToPointCombat)
                {
                    enumsEnemy.SetMovement(EnumsEnemy.Movimiento.Saltar);
                    isJamping = true;
                    Jump(alturaMaxima.transform.position);
                    isDeffended = false;
                }
            }

            if (life > 0 && enumsEnemy.GetStateEnemy() != EnumsEnemy.EstadoEnemigo.muerto && enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.MoveToPointCombat)
            {
                //Debug.Log("ESTOY ENTRANDO ACA");
                //Debug.Log(delaySelectMovement <= 0 && (enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.Saltar || enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.SaltoAtaque));
                if (delaySelectMovement <= 0 && (enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.Saltar || enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.SaltoAtaque))
                {
                    if (enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.AtaqueEspecial
                        && enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.AtaqueEspecialAgachado
                        && enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.AtaqueEspecialSalto
                        && enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.MoverAdelante
                        && enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.MoverAtras)
                    {
                        CheckComportamiento();
                    }
                }
                if (delaySelectMovement > 0)
                {
                    CheckMovement();
                    delaySelectMovement = delaySelectMovement - Time.deltaTime;
                }
            }
        }
    }
    public void CheckBoxColliders2D()
    {
        if (enumsEnemy.typeEnemy != EnumsEnemy.TiposDeEnemigo.Jefe)
        {
            if (isDuck || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.Agacharse
                || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AgacharseAtaque
                || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AgacheDefensa
                || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AtaqueEspecialAgachado)
            {
                if (boxColliderPiernas != null)
                {
                    boxColliderPiernas.GetBoxCollider2D().enabled = false;
                }
                boxColliderControllerAgachado.GetBoxCollider2D().enabled = true;
                boxColliderControllerParado.GetBoxCollider2D().enabled = false;
                boxColliderControllerSaltando.GetBoxCollider2D().enabled = false;
            }
            else if (isJamping || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.Saltar
                || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.SaltoAtaque
                || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.SaltoDefensa
                || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AtaqueEspecialSalto)
            {
                if (boxColliderPiernas != null)
                {
                    boxColliderPiernas.GetBoxCollider2D().enabled = false;
                }
                boxColliderControllerAgachado.GetBoxCollider2D().enabled = false;
                boxColliderControllerParado.GetBoxCollider2D().enabled = false;
                boxColliderControllerSaltando.GetBoxCollider2D().enabled = true;
            }
            else if (enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.Saltar
                || enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.SaltoAtaque
                || enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.SaltoDefensa
                || enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.AtaqueEspecialSalto)
            {
                if (boxColliderPiernas != null)
                {
                    boxColliderPiernas.GetBoxCollider2D().enabled = true;
                }
                boxColliderControllerAgachado.GetBoxCollider2D().enabled = false;
                boxColliderControllerParado.GetBoxCollider2D().enabled = true;
                boxColliderControllerSaltando.GetBoxCollider2D().enabled = false;
            }
        }
    }
    public void CheckComportamiento()
    {
        //Debug.Log("ENTRE AL COMPORTAMIENTO");
        //Debug.Log("inCombatPosition: "+inCombatPosition);
        EnumsEnemy.Movimiento movimiento = EnumsEnemy.Movimiento.Nulo;
        if (enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.MoveToPointCombat 
            && enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.MoveToPointDeath 
            && !GetIsJamping() && SpeedJump >= GetAuxSpeedJamp()
            && inCombatPosition)
        {
            if (activateComportamiento)
            {
                float opcionMovement = UnityEngine.Random.Range(MinRangeRandom, MaxRangeRandom);

                if (opcionMovement < MovePorcentage)
                {
                    //MOVIMIENTO 
                    if (structsEnemys.dataEnemy.columnaActual >= gridEnemy.GetCuadrilla_columnas() - 1)
                    {
                        movimiento = EnumsEnemy.Movimiento.MoverAdelante;
                    }
                    else if (structsEnemys.dataEnemy.columnaActual <= 0)
                    {
                        movimiento = EnumsEnemy.Movimiento.MoverAtras;
                    }
                    else
                    {
                        opcionMovement = UnityEngine.Random.Range(MinRangeRandom, MaxRangeRandom);
                        if (opcionMovement < MoveBackPorcentage)
                        {
                            movimiento = EnumsEnemy.Movimiento.MoverAtras;
                        }
                        else
                        {
                            movimiento = EnumsEnemy.Movimiento.MoverAdelante;
                        }
                    }
                }
                else if (opcionMovement >= MovePorcentage && opcionMovement < (MovePorcentage + JumpPorcentage))
                {
                    //SALTO
                    opcionMovement = UnityEngine.Random.Range(MinRangeRandom, MaxRangeRandom);
                    if (opcionMovement < AttackJumpPorcentage)
                    {
                        movimiento = EnumsEnemy.Movimiento.SaltoAtaque;
                    }
                    else if (opcionMovement >= AttackJumpPorcentage && opcionMovement < (AttackJumpPorcentage + DefenceJumpPorcentage))
                    {
                        movimiento = EnumsEnemy.Movimiento.SaltoDefensa;
                    }
                    else if (opcionMovement >= (AttackJumpPorcentage + DefenceJumpPorcentage))
                    {
                        movimiento = EnumsEnemy.Movimiento.Saltar;
                    }
                }
                else if (opcionMovement >= (MovePorcentage + JumpPorcentage) && opcionMovement < (MovePorcentage + JumpPorcentage + DuckPorcentage))
                {
                    //AGACHARSE
                    opcionMovement = UnityEngine.Random.Range(MinRangeRandom, MaxRangeRandom);
                    if (opcionMovement < AttackDuckPorcentage)
                    {
                        movimiento = EnumsEnemy.Movimiento.AgacharseAtaque;
                    }
                    else if (opcionMovement >= AttackDuckPorcentage && opcionMovement < (AttackDuckPorcentage + DefenceDuckPorcentage))
                    {
                        movimiento = EnumsEnemy.Movimiento.AgacheDefensa;
                    }
                    else if (opcionMovement >= (AttackDuckPorcentage + DefenceDuckPorcentage))
                    {
                        movimiento = EnumsEnemy.Movimiento.Agacharse;
                    }
                }
                else if (opcionMovement >= (MovePorcentage + JumpPorcentage + DuckPorcentage))
                {
                    //QUIETO EN EL LUGAR
                    opcionMovement = UnityEngine.Random.Range(MinRangeRandom, MaxRangeRandom);
                    if (opcionMovement < AttackPorcentage)
                    {
                        movimiento = EnumsEnemy.Movimiento.AtacarEnElLugar;
                    }
                    else if (opcionMovement >= AttackPorcentage && opcionMovement < (AttackPorcentage + DeffensePorcentage))
                    {
                        movimiento = EnumsEnemy.Movimiento.DefensaEnElLugar;
                    }
                }
            }
        }
        else
        {
            movimiento = enumsEnemy.GetMovement();
        }
        if (movimiento == EnumsEnemy.Movimiento.MoveToPointCombat)
        {
            delaySelectMovement = 10;
        }
        else
        {
            delaySelectMovement = UnityEngine.Random.Range(minRandomDelayMovement, maxRandomDelayMovement);
        }
        enumsEnemy.SetMovement(movimiento);
        if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.SaltoAtaque || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AtaqueEspecialSalto)
        {
            delayAttack = delayAttackJumping;
        }
        if (enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.SaltoDefensa || enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.DefensaEnElLugar)
        {
            isDeffended = false;
        }
    }
    public EnumsEnemy.Movimiento CheckSpecialAttack(EnumsEnemy.Movimiento _movimiento)
    {
        float specialMovement = UnityEngine.Random.Range(MinRangeRandom, MaxRangeRandom);
        EnumsEnemy.Movimiento movimiento = _movimiento;
        if (specialMovement < AttackSpecialPorcentage)
        {
            if (_movimiento == EnumsEnemy.Movimiento.Agacharse || _movimiento == EnumsEnemy.Movimiento.AgacharseAtaque || _movimiento == EnumsEnemy.Movimiento.AgacheDefensa)
            {
                movimiento = EnumsEnemy.Movimiento.AtaqueEspecialAgachado;
            }
            else if (_movimiento == EnumsEnemy.Movimiento.Saltar || _movimiento == EnumsEnemy.Movimiento.SaltoAtaque || _movimiento == EnumsEnemy.Movimiento.SaltoDefensa)
            {
                movimiento = EnumsEnemy.Movimiento.AtaqueEspecialSalto;
            }
            else if (_movimiento == EnumsEnemy.Movimiento.AtacarEnElLugar || _movimiento == EnumsEnemy.Movimiento.DefensaEnElLugar)
            {
                movimiento = EnumsEnemy.Movimiento.AtaqueEspecial;
            }
        }
        return movimiento;
    }
    public void DefaultBehavior()
    {
        EnumsEnemy.Movimiento movimiento;
        int min = (int)EnumsEnemy.Movimiento.Nulo + 1;
        int max = (int)EnumsEnemy.Movimiento.Count - 3;
        if (enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.MoveToPointCombat && enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.MoveToPointDeath)
        {
            movimiento = (EnumsEnemy.Movimiento)UnityEngine.Random.Range(min, max);
            switch (movimiento)
            {
                case EnumsEnemy.Movimiento.AgacheDefensa:
                    movimiento = EnumsEnemy.Movimiento.Agacharse;
                    break;
                case EnumsEnemy.Movimiento.DefensaEnElLugar:
                    movimiento = EnumsEnemy.Movimiento.AtacarEnElLugar;
                    break;
                case EnumsEnemy.Movimiento.SaltoDefensa:
                    movimiento = EnumsEnemy.Movimiento.Saltar;
                    break;
            }
        }
        else
        {
            movimiento = enumsEnemy.GetMovement();
        }
        delaySelectMovement = UnityEngine.Random.Range(minRandomDelayMovement, maxRandomDelayMovement);
        enumsEnemy.SetMovement(movimiento);
        if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.SaltoAtaque || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AtaqueEspecialSalto)
        {
            delayAttack = delayAttackJumping;
        }
        if (enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.SaltoDefensa || enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.DefensaEnElLugar)
        {
            isDeffended = false;
        }
    }

    public void CheckSpecialAttack()
    {
        if (enableSpecialAttack)
        {
            boxColliderControllerAgachado.state = BoxColliderController.StateBoxCollider.Normal;
            boxColliderControllerParado.state = BoxColliderController.StateBoxCollider.Normal;
            boxColliderControllerSaltando.state = BoxColliderController.StateBoxCollider.Normal;
            boxColliderSprite.state = BoxColliderController.StateBoxCollider.Normal;

            if (!isJamping && !isDuck
                && enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.Saltar
                && enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.SaltoAtaque
                && enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.SaltoDefensa)
            {
                delaySelectMovement = 0.1f;
                enableSpecialAttack = false;
                enumsEnemy.SetMovement(EnumsEnemy.Movimiento.AtaqueEspecial);
                AnimationAttack();
                //xpActual = 0;
            }
            else if (enumsEnemy.typeEnemy == EnumsEnemy.TiposDeEnemigo.Balanceado)
            {
                if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.Saltar
                    || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.SaltoAtaque
                    || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.SaltoDefensa)
                {
                    enumsEnemy.SetMovement(EnumsEnemy.Movimiento.AtaqueEspecialSalto);
                }
                else if (isDuck)
                {
                    enumsEnemy.SetMovement(EnumsEnemy.Movimiento.AtaqueEspecialAgachado);
                }
                else
                {
                    enumsEnemy.SetMovement(EnumsEnemy.Movimiento.AtaqueEspecial);
                }
                AnimationAttack();
                //xpActual = 0;

            }
        }
    }
    public void MoveToPoint(Vector3 pointCombat)
    {
        if (CheckMove(new Vector3(pointCombat.x, transform.position.y, transform.position.z)))
        {
            //Debug.Log("ME MUEVOOOO");
            delaySelectMovement = 999;
            if (pointCombat.x < enemyPrefab.transform.position.x)
            {
                enemyPrefab.transform.Translate(Vector3.left * Speed * Time.deltaTime);
            }
            else if (pointCombat.x > enemyPrefab.transform.position.x)
            {
                enemyPrefab.transform.Translate(Vector3.right * Speed * Time.deltaTime);
            }
        }
        else
        {
            //Debug.Log("EN POSICION");
            life = maxLife;
            delaySelectMovement = 0;
            enumsEnemy.SetMovement(EnumsEnemy.Movimiento.Nulo);
        }
    }

    //INTERACTUA CON GAME MANAGER
    public void Dead()
    {
        if(life <= 0)
        {
            if (OnDie != null)
            {
                OnDie(this);
            }
        }
        if (!InPool)
        {
            if (life <= 0)
            {
                gm.countEnemysDead++;
                gm.playerData_P1.score = gm.playerData_P1.score + gm.playerData_P1.scoreForEnemyDead;
                gm.ResetRoundCombat(false);
                ResetEnemy();
                enemyPrefab.gameObject.SetActive(false);
                xpActual = 0;
                inCombatPosition = false;
            }
        }
        else if (InPool)
        {
            switch (gm.enumsGameManager.modoDeJuego)
            {
                case EnumsGameManager.ModosDeJuego.Supervivencia:
                    if (life <= 0)
                    {
                        //life = auxLife;
                        gm.generateEnemy = true;
                        gm.countEnemysDead++;
                        gm.playerData_P1.score = gm.playerData_P1.score + gm.playerData_P1.scoreForEnemyDead;
                        gm.ResetRoundCombat(false);
                        ResetEnemy();
                        //poolObjectEnemy.Recycle();
                        pool.Recycle(enemyPrefab);
                        xpActual = 0;
                        inCombatPosition = false;
                    }
                    break;
                case EnumsGameManager.ModosDeJuego.Historia:
                    if (life <= 0)
                    {
                        //life = auxLife;
                        gm.generateEnemy = true;
                        gm.countEnemysDead++;
                        gm.playerData_P1.score = gm.playerData_P1.score + gm.playerData_P1.scoreForEnemyDead;
                        gm.ResetRoundCombat(false);
                        ResetEnemy();
                        //poolObjectEnemy.Recycle();
                        pool.Recycle(enemyPrefab);
                        xpActual = 0;
                        inCombatPosition = false;
                        enemyPrefab.gameObject.SetActive(false);
                    }
                    break;
                case EnumsGameManager.ModosDeJuego.Nulo:
                    if (life <= 0)
                    {
                        gm.countEnemysDead++;
                        gm.playerData_P1.score = gm.playerData_P1.score + gm.playerData_P1.scoreForEnemyDead;
                        gm.ResetRoundCombat(false);
                        ResetEnemy();
                        enemyPrefab.gameObject.SetActive(false);
                        xpActual = 0;
                        inCombatPosition = false;
                    }
                    break;
            }
        }
    }
    public void CheckDead()
    {
        if (life <= 0 && transform.position.y <= InitialPosition.y)
        {
            inCombatPosition = false;
            if (enumsEnemy.typeEnemy != EnumsEnemy.TiposDeEnemigo.Jefe)
            {
                spriteEnemy.PlayAnimation("Death");
            }
            enumsEnemy.SetMovement(EnumsEnemy.Movimiento.Nulo);
            enumsEnemy.SetStateEnemy(EnumsEnemy.EstadoEnemigo.muerto);
        }
    }
    public void CheckMovement()
    {
        
        if (enableDeffence && barraDeEscudo != null && inCombatPosition && enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.MoveToPointCombat)
        {
            if ((enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.DefensaEnElLugar
            && enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.AgacheDefensa
            && enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.SaltoDefensa)
            || barraDeEscudo.nededBarMaxPorcentage)
            {

                barraDeEscudo.AddPorcentageBar();

                if (barraDeEscudo.GetValueShild() <= barraDeEscudo.porcentageNededForDeffence)
                {
                    barraDeEscudo.SetEnableDeffence(false);
                    if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AgacheDefensa)
                    {
                        enumsEnemy.SetMovement(EnumsEnemy.Movimiento.Agacharse);
                    }
                    else if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.SaltoDefensa)
                    {
                        enumsEnemy.SetMovement(EnumsEnemy.Movimiento.Saltar);
                    }
                    else if (enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.SaltoDefensa
                        && enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.SaltoAtaque
                        && enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.Saltar
                        && !isDuck)
                    {
                        enumsEnemy.SetMovement(EnumsEnemy.Movimiento.Nulo);
                    }
                }
            }
        }
        if (!enableDeffence && transform.position.y <= InitialPosition.y
            && !isJamping && SpeedJump >= GetAuxSpeedJamp()
            && (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.DefensaEnElLugar
            || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AgacheDefensa))
        {
            float delay = 0.1f;
            while (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.DefensaEnElLugar
                || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AgacheDefensa)
            {
                CheckComportamiento();
                if (delay <= 0)
                {
                    enumsEnemy.SetMovement(EnumsEnemy.Movimiento.AtacarEnElLugar);
                }
                else
                {
                    delay = delay - Time.deltaTime;
                }
            }
        }
        if (!enableDeffence)
        {
            if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.SaltoDefensa)
            {
                enumsEnemy.SetMovement(EnumsEnemy.Movimiento.Saltar);
            }
        }
        //Debug.Log(isDuck);

        switch (enumsEnemy.GetMovement())
        {
            case EnumsEnemy.Movimiento.AtacarEnElLugar:
                CheckDelayAttack(false);
                isDeffended = false;
                break;
            case EnumsEnemy.Movimiento.AgacharseAtaque:
                Duck(structsEnemys.dataEnemy.CantCasillasOcupadas_Y);
                CheckDelayAttack(false);
                isDeffended = false;
                break;
            case EnumsEnemy.Movimiento.SaltoAtaque:
                if (!isJamping && SpeedJump > 0 && structsEnemys.particleMovement.particleJump != null)
                {
                    structsEnemys.particleMovement.particleJump.transform.position = new Vector3(transform.position.x, structsEnemys.particleMovement.particleJump.transform.position.y, structsEnemys.particleMovement.particleJump.transform.position.z);
                    structsEnemys.particleMovement.particleJump.SetActive(true);
                }
                CheckDelayAttack(false);
                isJamping = true;
                Jump(alturaMaxima.transform.position);
                isDeffended = false;
                break;
            case EnumsEnemy.Movimiento.MoverAtras:
                isDeffended = false;
                if (structsEnemys.dataEnemy.columnaActual < gridEnemy.GetCuadrilla_columnas() - 1)
                {
                    MoveRight(posicionesDeMovimiento[structsEnemys.dataEnemy.columnaActual + 1].transform.position);
                }
                else
                {
                    delaySelectMovement = 0;
                }
                break;
            case EnumsEnemy.Movimiento.MoverAdelante:
                isDeffended = false;
                if (structsEnemys.dataEnemy.columnaActual > 0)
                {
                    MoveLeft(posicionesDeMovimiento[structsEnemys.dataEnemy.columnaActual - 1].transform.position);
                }
                else
                {
                    delaySelectMovement = 0;
                }
                break;
            case EnumsEnemy.Movimiento.Saltar:
                if (!isJamping && SpeedJump > 0 && structsEnemys.particleMovement.particleJump != null)
                {
                    structsEnemys.particleMovement.particleJump.transform.position = new Vector3(transform.position.x, structsEnemys.particleMovement.particleJump.transform.position.y, structsEnemys.particleMovement.particleJump.transform.position.z);
                    structsEnemys.particleMovement.particleJump.SetActive(true);
                }
                isDeffended = false;
                isJamping = true;
                Jump(alturaMaxima.transform.position);
                break;
            case EnumsEnemy.Movimiento.DefensaEnElLugar:
                Deffence();
                break;
            case EnumsEnemy.Movimiento.SaltoDefensa:
                if (!isJamping && SpeedJump > 0 && structsEnemys.particleMovement.particleJump != null)
                {
                    structsEnemys.particleMovement.particleJump.transform.position = new Vector3(transform.position.x, structsEnemys.particleMovement.particleJump.transform.position.y, structsEnemys.particleMovement.particleJump.transform.position.z);
                    structsEnemys.particleMovement.particleJump.SetActive(true);
                }
                isJamping = true;
                Jump(alturaMaxima.transform.position);
                Deffence();
                break;
            case EnumsEnemy.Movimiento.AgacheDefensa:
                Duck(structsEnemys.dataEnemy.CantCasillasOcupadas_Y);
                Deffence();
                break;
            case EnumsEnemy.Movimiento.Agacharse:
                isDeffended = false;
                Duck(structsEnemys.dataEnemy.CantCasillasOcupadas_Y);
                break;
            case EnumsEnemy.Movimiento.AtaqueEspecial:
                isDeffended = false;
                break;
            case EnumsEnemy.Movimiento.AtaqueEspecialAgachado:
                Duck(structsEnemys.dataEnemy.CantCasillasOcupadas_Y);
                isDeffended = false;
                break;
            case EnumsEnemy.Movimiento.AtaqueEspecialSalto:
                if (!isJamping && SpeedJump > 0 && structsEnemys.particleMovement.particleJump != null)
                {
                    structsEnemys.particleMovement.particleJump.transform.position = new Vector3(transform.position.x, structsEnemys.particleMovement.particleJump.transform.position.y, structsEnemys.particleMovement.particleJump.transform.position.z);
                    structsEnemys.particleMovement.particleJump.SetActive(true);
                }
                isDeffended = false;
                isJamping = true;
                Jump(alturaMaxima.transform.position);
                break;
            case EnumsEnemy.Movimiento.MoveToPointCombat:
                //Debug.Log("ENTRE AL MOVIMIENTO");
                isDeffended = false;
                MoveToPoint(pointOfCombat);
                if(enumsEnemy.typeEnemy != EnumsEnemy.TiposDeEnemigo.Jefe)
                    spriteEnemy.ActualSprite = SpriteCharacter.SpriteActual.MoverAdelante;
                break;
            case EnumsEnemy.Movimiento.MoveToPointDeath:
                isDeffended = false;
                MoveToPoint(pointOfDeath);
                break;
        }
        if (enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.AgacharseAtaque && enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.AgacheDefensa && enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.Agacharse)
        {
            colliderSprites.enabled = true;
            isDuck = false;
        }
        if (barraDeEscudo != null)
        {
            if (spriteEnemy != null)
            {
                if (spriteEnemy.ActualSprite != SpriteEnemy.SpriteActual.AgachadoDefensa
                    && spriteEnemy.ActualSprite != SpriteEnemy.SpriteActual.ParadoDefensa
                    && spriteEnemy.ActualSprite != SpriteEnemy.SpriteActual.SaltoDefensa
                    && barraDeEscudo.GetValueShild() > barraDeEscudo.porcentageNededForDeffence
                        && barraDeEscudo.GetEnableDeffence())
                {
                    isDeffended = false;
                }
            }
        }
    }
    public void CheckDeffense()
    {
        if (barraDeEscudo != null)
        {
            if (isDeffended && barraDeEscudo.GetValueShild() > barraDeEscudo.porcentageNededForDeffence
                    && barraDeEscudo.GetEnableDeffence())
            {
                boxColliderControllerAgachado.state = BoxColliderController.StateBoxCollider.Defendido;
                boxColliderControllerParado.state = BoxColliderController.StateBoxCollider.Defendido;
                boxColliderControllerSaltando.state = BoxColliderController.StateBoxCollider.Defendido;
                boxColliderSprite.state = BoxColliderController.StateBoxCollider.Defendido;
            }
            else
            {
                boxColliderControllerAgachado.state = BoxColliderController.StateBoxCollider.Normal;
                boxColliderControllerParado.state = BoxColliderController.StateBoxCollider.Normal;
                boxColliderControllerSaltando.state = BoxColliderController.StateBoxCollider.Normal;
                boxColliderSprite.state = BoxColliderController.StateBoxCollider.Normal;
                if (barraDeEscudo != null)
                {
                    barraDeEscudo.AddPorcentageBar();

                    if (barraDeEscudo.GetValueShild() <= barraDeEscudo.porcentageNededForDeffence)
                    {
                        barraDeEscudo.SetEnableDeffence(false);
                    }
                }
            }
        }
    }
    public void Deffence()
    {
        isDeffended = true;
        if (!isDuck
            && enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.Saltar
            && enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.SaltoAtaque
            && enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.SaltoDefensa
            && !isJamping)
        {
            boxColliderControllerParado.state = BoxColliderController.StateBoxCollider.Defendido;
        }
        else if (isDuck
            && enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.Saltar
            && enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.SaltoAtaque
            && enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.SaltoDefensa
            && !isJamping)
        {
            boxColliderControllerAgachado.state = BoxColliderController.StateBoxCollider.Defendido;
        }
        else if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.Saltar
            || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.SaltoAtaque
            || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.SaltoDefensa
            || isJamping)
        {
            boxColliderControllerSaltando.state = BoxColliderController.StateBoxCollider.Defendido;
        }
        boxColliderSprite.state = BoxColliderController.StateBoxCollider.Defendido;
        if (barraDeEscudo != null)
        {
            barraDeEscudo.SubstractPorcentageBar();
            if (barraDeEscudo.nededBarMaxPorcentage && barraDeEscudo.ValueShild < barraDeEscudo.MaxValueShild)
            {
                delaySelectMovement = 0.0f;
            }
        }
            
    }
    public virtual void CheckDelayAttack(bool specialAttack)
    {
        if (delayAttack > 0)
        {
            delayAttack = delayAttack - Time.deltaTime;
        }
        else if (delayAttack <= 0)
        {
            delayAttack = auxDelayAttack;
            if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.SaltoAtaque || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.Nulo || enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AtaqueEspecialSalto)
            {
                delayAttack = delayAttackJumping;
                Attack(true, specialAttack,false);
            }
            else
            {
                Attack(false, specialAttack,false);
            }
        }
    }
    public void ResetEnemy()
    {
        if (!DontRestart)
        {
            life = maxLife;
            xpActual = 0;
            transform.position = InitialPosition;
            enemyPrefab.transform.position = InitialPosition;
        }
    }
    public void MoveLeft(Vector3 cuadrillaDestino)
    {
        if (CheckMove(new Vector3(posicionesDeMovimiento[0].transform.position.x, transform.position.y, transform.position.z)) && transform.position.x > cuadrillaDestino.x)
        {
            if (enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.MoverAdelante || !insound)
            {
                insound = true;
                eventWise.StartEvent("moverse");
            }
            Move(Vector3.left);
            enumsEnemy.SetMovement(EnumsEnemy.Movimiento.MoverAdelante);
        }
        else if (enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.Nulo)
        {
            insound = false;
            structsEnemys.dataEnemy.columnaActual--;
            enumsEnemy.SetMovement(EnumsEnemy.Movimiento.Nulo);
            delaySelectMovement = 0;
        }
    }
    public void MoveRight(Vector3 cuadrillaDestino)
    {
        if (CheckMove(new Vector3(posicionesDeMovimiento[posicionesDeMovimiento.Length-1].transform.position.x, transform.position.y, transform.position.z)) && transform.position.x < cuadrillaDestino.x)
        {
            if (enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.MoverAtras || !insound)
            {
                insound = true;
                eventWise.StartEvent("moverse");
            }
            Move(Vector3.right);
            enumsEnemy.SetMovement(EnumsEnemy.Movimiento.MoverAtras);
        }
        else if (enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.Nulo)
        {
            insound = false;
            structsEnemys.dataEnemy.columnaActual++;
            enumsEnemy.SetMovement(EnumsEnemy.Movimiento.Nulo);
            delaySelectMovement = 0;
        }
    }
    public virtual void AnimationAttack(){ }
    public virtual void Attack(bool jampAttack, bool specialAttack, bool _doubleDamage){ }
    public virtual void Attack(bool jampAttack, bool specialAttack, bool _doubleDamage, Proyectil cuadrilla) { }
    public void CheckSpecialAttackEnemyController(int minRandomRootShoot, int maxRandomRootShoot, GameObject generador)
    {
        if (!isDuck)
        {
            if (generador != null)
            {
                specialAttackParabolaEnemyController.SpecialAttack(this,doubleDamage, isDuck, generador, generador, enumsEnemy, structsEnemys, maxRandomRootShoot, minRandomRootShoot);
            }
        }
        else
        {
            if (generador != null)
            {
                specialAttackParabolaEnemyController.SpecialAttack(this,doubleDamage, isDuck, generador, generador, enumsEnemy, structsEnemys, maxRandomRootShoot, minRandomRootShoot);
            }
        }
            
    }
    public void CounterAttack(bool dobleDamage)
    {
        Attack(false,false, dobleDamage);
    }
    public void Jump(Vector3 alturaMaxima)
    {
        //Debug.Log(isJamping);
        //Debug.Log("CheckMove: "+CheckMove(new Vector3(transform.position.x, alturaMaxima.y, transform.position.z)));// esto es false
        //Debug.Log("isJumping: " + isJamping);
        if (transform.position.y > InitialPosition.y || isJamping)
        {
            if (transform.position.y <= InitialPosition.y)
            {
                eventWise.StartEvent("saltar");
            }
            if (enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.SaltoAtaque && enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.SaltoDefensa && enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.AtaqueEspecialSalto)
            {
                enumsEnemy.SetMovement(EnumsEnemy.Movimiento.Saltar);
            }
            //Debug.Log("SALTO");
            MoveJamp(Vector3.up);
            if (SpeedJump <= 0)
            {
                isJamping = false;
            }
            if (delaySelectMovement <= 0)
            {
                delaySelectMovement = 0.1f;
            }
            if (enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.SaltoDefensa)
            {
                Deffence();
            }
        }
    }
    public void ParabolaAttack()
    {
        if (enableMecanicParabolaAttack)
        {
            GameObject go = poolParabolaAttack.GetObject();
            ProyectilParabola proyectil = go.GetComponent<ProyectilParabola>();
            proyectil.SetDobleDamage(false);
            proyectil.disparadorDelProyectil = Proyectil.DisparadorDelProyectil.Enemigo;
            proyectil.SetEnemy(this);
            switch (applyColorShoot)
            {
                case ApplyColorShoot.None:
                    break;
                case ApplyColorShoot.Proyectil:
                    proyectil.SetColorProyectil(colorShoot);
                    break;
                case ApplyColorShoot.Stela:
                    proyectil.SetColorStela(colorShoot);
                    break;
                case ApplyColorShoot.StelaAndProyectil:
                    proyectil.SetColorProyectil(colorShoot);
                    proyectil.SetColorStela(colorShoot);
                    break;
            }
            
            if (!GetIsDuck())
            {
                proyectil.TypeRoot = 1;
                go.transform.position = generadorProyectilParabola.transform.position;
            }
            else
            {
                proyectil.TypeRoot = 2;
                go.transform.position = generadorProyectilParabolaAgachado.transform.position;
            }
            switch (proyectil.TypeRoot)
            {
                case 1:
                    proyectil.rutaParabola_AtaqueEnemigo = structsEnemys.ruta;
                    break;
                case 2:
                    proyectil.rutaParabolaAgachado_AtaqueEnemigo = structsEnemys.rutaAgachado;
                    break;
            }
            proyectil.rutaParabola_AtaqueEnemigo = structsEnemys.ruta;
            proyectil.OnParabola(this,null, Proyectil.typeProyectil.Nulo);
        }
    }
    public bool CheckMove(Vector3 PosicionDestino)
    {
        Vector3 distaciaObjetivo = transform.position - PosicionDestino;
        bool mover = false;
        //Debug.Log(distaciaObjetivo.magnitude + "/ 0.2f");
        //Debug.Log(enumsEnemy.GetMovement());
        if (distaciaObjetivo.magnitude > 0.2f)
        {
            mover = true;
        }
        return mover;
    }
    public void Move(Vector3 direccion)
    {
        transform.Translate(direccion * Speed * Time.deltaTime);
    }
    public void Move(Vector3 direccion, int substractSpeed)
    {
        transform.Translate(direccion * Speed/substractSpeed * Time.deltaTime);
    }
    public void MoveJamp(Vector3 direccion)
    {
        //Debug.Log("ENTRE AL MOVIMIENTO");
        /*if (Input.GetKey(KeyCode.X))
        {
            life = 0;
        }*/
        //
        if (direccion == Vector3.up)
        {
            transform.Translate(direccion * SpeedJump * Time.deltaTime);
            SpeedJump = SpeedJump - Time.deltaTime * Resistace;
        }
        else if (direccion == Vector3.down)
        {
            Debug.Log("ENTRE");
            transform.Translate(direccion * SpeedJump * Time.deltaTime);
            SpeedJump = SpeedJump + Time.deltaTime * Gravity;
        }
    }
    public void Duck(int rangoAgachado)
    {
        isDuck = true;
    }
    public bool GetIsDeffended()
    {
        return isDeffended;
    }
    public bool GetIsJamping()
    {
        return isJamping;
    }
    public bool GetIsDuck()
    {
        return isDuck;
    }
    public void SetXpActual(float xp)
    {
        xpActual = xp;
    }
    public float GetXpActual()
    {
        return xpActual;
    }
    public float GetAuxDelayAttack()
    {
        return auxDelayAttack;
    }
    public float GetAuxSpeedJamp()
    {
        return auxSpeedJump;
    }
    public void SetDelaySelectMovement(float delay)
    {
        delaySelectMovement = delay;
    }
    public void SetEnableSpecialAttack(bool _enableSpecialAttack)
    {
        enableSpecialAttack = _enableSpecialAttack;
    }
    public void SetIsDuck(bool _isDuck)
    {
        isDuck = _isDuck;
    }
    public void SetIsJumping(bool _isJumping)
    {
        isJamping = _isJumping;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "pointOfCombat")
        {
            //Debug.Log("ENTRE");
            inCombatPosition = true;
        }
    }
}
