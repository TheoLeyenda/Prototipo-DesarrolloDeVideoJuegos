using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class Enemy : Character
{
    public string fraseVictoria;
    public bool EnableChargerSpecialAttackForHit = true;
    public string nameEnemy;
    public bool enableColorShootSpecialAttack;
    [HideInInspector]
    public bool enableDeffence = true;
    [SerializeField]
    protected bool inCombatPosition;
    public SpriteEnemy spriteEnemy;
    [HideInInspector]
    public float Blindaje;
    [HideInInspector]
    public float MaxBlindaje;
    public EnumsEnemy enumsEnemy;
    public StructsEnemys structsEnemys;
    public SpriteRenderer SpriteRendererEnemigo;
    public SpecialAttackParabolaEnemyController specialAttackParabolaEnemyController;
    private float auxLife;
    public bool InPool;
    public Pool pool;
    public float life;
    public float maxLife;
    public Pool poolParabolaAttack;
    public Pool poolObjectAttack;
    private float MinRangeRandom = 0;
    private float MaxRangeRandom = 100;
    protected float delaySelectMovement;
    public float maxRandomDelayMovement;
    public float minRandomDelayMovement;
    private bool doubleDamage;
    private bool isDeffended;
    public float anguloAtaqueSalto;
    [SerializeField] private float auxSpeed = 0;
    [SerializeField] private float auxResistace = 0;
    [SerializeField] private float auxGravity = 0;
    public float delayAttackJumping;
    public Collider2D colliderSprites;
    [HideInInspector]
    public Vector3 pointOfDeath;
    [HideInInspector]
    public Vector3 pointOfCombat;
    public bool damageCounterAttack;
    public bool activateComportamiento;

    public bool enableMecanicParabolaAttack;
    [HideInInspector]
    public bool inAttack;
    protected EventWise eventWise;
    private bool insound;
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

    private Vector3 outPosition; 
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
        
        enumsEnemy.estado = EnumsCharacter.EstadoCharacter.vivo;
        Player.OnDie += AnimationVictory;
        if (!inCombatPosition && enumsEnemy.typeEnemy != EnumsEnemy.TiposDeEnemigo.Jefe) 
        {
            enumsEnemy.movimiento = EnumsCharacter.Movimiento.MoveToPointCombat;
        }
    }
    protected virtual void OnDisable()
    {
        enableDeffence = true;
        Player.OnDie -= AnimationVictory;
        ResetSpeedJump();
        myPrefab.transform.position = outPosition;
        inCombatPosition = false;
        isJamping = false;
        myVictory = false;
        weitVictory = false;
        inCombatPosition = false;

        Speed = auxSpeed;
        SpeedJump = auxSpeedJump;
        Resistace = auxResistace;
        Gravity = auxGravity;
    }
    public virtual void Start()
    {
        distanceMove = 0.2f;
        outPosition = new Vector3(500, 500, 500);
        weitVictory = false;
        specialAttackParabolaEnemyController = GetComponent<SpecialAttackParabolaEnemyController>();
        tolerableStillTime = maxRandomDelayMovement;
        auxTolerableStillTime = maxRandomDelayMovement;
        eventWise = GameObject.Find("EventWise").GetComponent<EventWise>();
        enableSpecialAttack = false;
        InitialPosition = transform.position;
        auxDelayAttack = delayAttack;
        delaySelectMovement = 0.5f;
        auxLife = life;
        if (GameManager.instanceGameManager != null)
        {
            gm = GameManager.instanceGameManager;
        }
        CheckInitialCharacter();
        enumsEnemy.estado = EnumsCharacter.EstadoCharacter.vivo;
    }
    public void AnimationVictory(Player p) 
    {
        if (transform.position.y <= InitialPosition.y)
        {
            if (enumsEnemy.typeEnemy != EnumsEnemy.TiposDeEnemigo.Jefe)
            {
                spriteEnemy.GetAnimator().Play("Victory");
            }
            enableMovement = false;
            myVictory = true;
            if (OnVictory != null)
            {
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
        CheckState();
        CheckDead();

        if (enableMovement)
        {
            CheckShild();
            CheckBoxColliders2D();

            if (enumsEnemy.movimiento != EnumsCharacter.Movimiento.AtaqueEspecial
                && enumsEnemy.estado != EnumsCharacter.EstadoCharacter.muerto)
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
        switch (enumsEnemy.estado)
        {
            case EnumsCharacter.EstadoCharacter.Atrapado:
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
                enumsEnemy.estado = EnumsCharacter.EstadoCharacter.vivo;
            }
            else
            {
                enumsEnemy.estado = EnumsCharacter.EstadoCharacter.muerto;
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
        if (enumsEnemy.estado != EnumsCharacter.EstadoCharacter.muerto && inCombatPosition)
        {
            if (transform.position.y <= InitialPosition.y && enumsEnemy.movimiento == EnumsCharacter.Movimiento.Saltar && !isJamping
                || transform.position.y <= InitialPosition.y && enumsEnemy.movimiento == EnumsCharacter.Movimiento.SaltoAtaque && !isJamping
                || transform.position.y <= InitialPosition.y && enumsEnemy.movimiento == EnumsCharacter.Movimiento.SaltoDefensa && !isJamping
                || transform.position.y <= InitialPosition.y && enumsEnemy.movimiento == EnumsCharacter.Movimiento.AtaqueEspecialSalto && !isJamping)
            {
                transform.position = new Vector3(transform.position.x, InitialPosition.y, transform.position.z);
                delaySelectMovement = 0.1f;
                enumsEnemy.movimiento = EnumsCharacter.Movimiento.Nulo;
                if (SpeedJump <= auxSpeedJump)
                    SpeedJump = auxSpeedJump;
            }
            else if (transform.position.y > InitialPosition.y && (life <= 0))
            {
                if (CheckMove(new Vector3(transform.position.x, InitialPosition.y, transform.position.z), distanceMove))
                {
                    enumsEnemy.movimiento = EnumsCharacter.Movimiento.Saltar;
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
        enumsEnemy.estado = EnumsCharacter.EstadoCharacter.vivo;
        tolerableStillTime = auxTolerableStillTime;
        if (GameManager.instanceGameManager != null)
        {
            gm = GameManager.instanceGameManager;
        }
        CheckInitialCharacter();
        xpActual = 0;
    }
    public void IA()
    {
        if (enumsEnemy.movimiento == EnumsEnemy.Movimiento.MoveToPointCombat)
        {
            delaySelectMovement = 10;
            CheckMovement();
            return;
        }
        if (enumsEnemy.typeEnemy != EnumsEnemy.TiposDeEnemigo.Jefe)
        {
            if (transform.position.y > InitialPosition.y && enumsEnemy.movimiento != EnumsCharacter.Movimiento.MoveToPointCombat && inCombatPosition)
            {
                delaySelectMovement = 0.1f;
                if (enumsEnemy.movimiento != EnumsCharacter.Movimiento.SaltoAtaque && enumsEnemy.movimiento != EnumsCharacter.Movimiento.SaltoDefensa && enumsEnemy.movimiento != EnumsEnemy.Movimiento.Saltar && enumsEnemy.movimiento != EnumsEnemy.Movimiento.MoveToPointCombat)
                {
                    enumsEnemy.movimiento = EnumsCharacter.Movimiento.Saltar;
                    isJamping = true;
                    Jump(alturaMaxima.transform.position);
                    isDeffended = false;
                }
            }

            if (life > 0 && enumsEnemy.estado != EnumsCharacter.EstadoCharacter.muerto && enumsEnemy.movimiento != EnumsCharacter.Movimiento.MoveToPointCombat)
            {
               
                if (delaySelectMovement <= 0 && (enumsEnemy.movimiento != EnumsCharacter.Movimiento.Saltar || enumsEnemy.movimiento != EnumsCharacter.Movimiento.SaltoAtaque))
                {
                    if (enumsEnemy.movimiento != EnumsCharacter.Movimiento.AtaqueEspecial
                        && enumsEnemy.movimiento != EnumsCharacter.Movimiento.AtaqueEspecialAgachado
                        && enumsEnemy.movimiento != EnumsCharacter.Movimiento.AtaqueEspecialSalto
                        && enumsEnemy.movimiento != EnumsCharacter.Movimiento.MoverAdelante
                        && enumsEnemy.movimiento != EnumsCharacter.Movimiento.MoverAtras)
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
            if (isDuck || enumsEnemy.movimiento == EnumsCharacter.Movimiento.Agacharse
                || enumsEnemy.movimiento == EnumsCharacter.Movimiento.AgacharseAtaque
                || enumsEnemy.movimiento == EnumsCharacter.Movimiento.AgacheDefensa
                || enumsEnemy.movimiento == EnumsCharacter.Movimiento.AtaqueEspecialAgachado)
            {
                if (boxColliderControllerPiernas != null)
                {
                    boxColliderControllerPiernas.GetBoxCollider2D().enabled = false;
                }
                boxColliderControllerAgachado.GetBoxCollider2D().enabled = true;
                boxColliderControllerParado.GetBoxCollider2D().enabled = false;
                boxColliderControllerSaltando.GetBoxCollider2D().enabled = false;
            }
            else if (isJamping || enumsEnemy.movimiento == EnumsCharacter.Movimiento.Saltar
                || enumsEnemy.movimiento == EnumsCharacter.Movimiento.SaltoAtaque
                || enumsEnemy.movimiento == EnumsCharacter.Movimiento.SaltoDefensa
                || enumsEnemy.movimiento == EnumsCharacter.Movimiento.AtaqueEspecialSalto)
            {
                if (boxColliderControllerPiernas != null)
                {
                    boxColliderControllerPiernas.GetBoxCollider2D().enabled = false;
                }
                boxColliderControllerAgachado.GetBoxCollider2D().enabled = false;
                boxColliderControllerParado.GetBoxCollider2D().enabled = false;
                boxColliderControllerSaltando.GetBoxCollider2D().enabled = true;
            }
            else if (enumsEnemy.movimiento != EnumsCharacter.Movimiento.Saltar
                || enumsEnemy.movimiento != EnumsCharacter.Movimiento.SaltoAtaque
                || enumsEnemy.movimiento != EnumsCharacter.Movimiento.SaltoDefensa
                || enumsEnemy.movimiento != EnumsCharacter.Movimiento.AtaqueEspecialSalto)
            {
                if (boxColliderControllerPiernas != null)
                {
                    boxColliderControllerPiernas.GetBoxCollider2D().enabled = true;
                }
                boxColliderControllerAgachado.GetBoxCollider2D().enabled = false;
                boxColliderControllerParado.GetBoxCollider2D().enabled = true;
                boxColliderControllerSaltando.GetBoxCollider2D().enabled = false;
            }
        }
    }
    public void CheckComportamiento()
    {
        EnumsCharacter.Movimiento movimiento = EnumsCharacter.Movimiento.Nulo;
        if (enumsEnemy.movimiento != EnumsCharacter.Movimiento.MoveToPointCombat 
            && enumsEnemy.movimiento != EnumsCharacter.Movimiento.MoveToPointDeath 
            && !GetIsJumping() && SpeedJump >= GetAuxSpeedJump()
            && inCombatPosition)
        {
            if (activateComportamiento)
            {
                float opcionMovement = UnityEngine.Random.Range(MinRangeRandom, MaxRangeRandom);

                if (opcionMovement < MovePorcentage)
                {
                    //MOVIMIENTO 
                    if (structsEnemys.dataEnemy.columnaActual >= grid.GetCuadrilla_columnas() - 1)
                    {
                        movimiento = EnumsCharacter.Movimiento.MoverAdelante;
                    }
                    else if (structsEnemys.dataEnemy.columnaActual <= 0)
                    {
                        movimiento = EnumsCharacter.Movimiento.MoverAtras;
                    }
                    else
                    {
                        opcionMovement = UnityEngine.Random.Range(MinRangeRandom, MaxRangeRandom);
                        if (opcionMovement < MoveBackPorcentage)
                        {
                            movimiento = EnumsCharacter.Movimiento.MoverAtras;
                        }
                        else
                        {
                            movimiento = EnumsCharacter.Movimiento.MoverAdelante;
                        }
                    }
                }
                else if (opcionMovement >= MovePorcentage && opcionMovement < (MovePorcentage + JumpPorcentage))
                {
                    //SALTO
                    opcionMovement = UnityEngine.Random.Range(MinRangeRandom, MaxRangeRandom);
                    if (opcionMovement < AttackJumpPorcentage)
                    {
                        movimiento = EnumsCharacter.Movimiento.SaltoAtaque;
                    }
                    else if (opcionMovement >= AttackJumpPorcentage && opcionMovement < (AttackJumpPorcentage + DefenceJumpPorcentage))
                    {
                        movimiento = EnumsCharacter.Movimiento.SaltoDefensa;
                    }
                    else if (opcionMovement >= (AttackJumpPorcentage + DefenceJumpPorcentage))
                    {
                        movimiento = EnumsCharacter.Movimiento.Saltar;
                    }
                }
                else if (opcionMovement >= (MovePorcentage + JumpPorcentage) && opcionMovement < (MovePorcentage + JumpPorcentage + DuckPorcentage))
                {
                    //AGACHARSE
                    opcionMovement = UnityEngine.Random.Range(MinRangeRandom, MaxRangeRandom);
                    if (opcionMovement < AttackDuckPorcentage)
                    {
                        movimiento = EnumsCharacter.Movimiento.AgacharseAtaque;
                    }
                    else if (opcionMovement >= AttackDuckPorcentage && opcionMovement < (AttackDuckPorcentage + DefenceDuckPorcentage))
                    {
                        movimiento = EnumsCharacter.Movimiento.AgacheDefensa;
                    }
                    else if (opcionMovement >= (AttackDuckPorcentage + DefenceDuckPorcentage))
                    {
                        movimiento = EnumsCharacter.Movimiento.Agacharse;
                    }
                }
                else if (opcionMovement >= (MovePorcentage + JumpPorcentage + DuckPorcentage))
                {
                    //QUIETO EN EL LUGAR
                    opcionMovement = UnityEngine.Random.Range(MinRangeRandom, MaxRangeRandom);
                    if (opcionMovement < AttackPorcentage)
                    {
                        movimiento = EnumsCharacter.Movimiento.AtacarEnElLugar;
                    }
                    else if (opcionMovement >= AttackPorcentage && opcionMovement < (AttackPorcentage + DeffensePorcentage))
                    {
                        movimiento = EnumsCharacter.Movimiento.DefensaEnElLugar;
                    }
                }
            }
        }
        else
        {
            movimiento = enumsEnemy.movimiento;
        }
        if (movimiento == EnumsCharacter.Movimiento.MoveToPointCombat)
        {
            delaySelectMovement = 10;
        }
        else
        {
            delaySelectMovement = UnityEngine.Random.Range(minRandomDelayMovement, maxRandomDelayMovement);
        }
        enumsEnemy.movimiento = movimiento;
        if (enumsEnemy.movimiento == EnumsCharacter.Movimiento.SaltoAtaque || enumsEnemy.movimiento == EnumsCharacter.Movimiento.AtaqueEspecialSalto)
        {
            delayAttack = delayAttackJumping;
        }
        if (enumsEnemy.movimiento != EnumsCharacter.Movimiento.SaltoDefensa || enumsEnemy.movimiento != EnumsCharacter.Movimiento.DefensaEnElLugar)
        {
            isDeffended = false;
        }
    }
    public EnumsCharacter.Movimiento CheckSpecialAttack(EnumsCharacter.Movimiento _movimiento)
    {
        float specialMovement = UnityEngine.Random.Range(MinRangeRandom, MaxRangeRandom);
        EnumsCharacter.Movimiento movimiento = _movimiento;
        if (specialMovement < AttackSpecialPorcentage)
        {
            if (_movimiento == EnumsCharacter.Movimiento.Agacharse || _movimiento == EnumsCharacter.Movimiento.AgacharseAtaque || _movimiento == EnumsCharacter.Movimiento.AgacheDefensa)
            {
                movimiento = EnumsCharacter.Movimiento.AtaqueEspecialAgachado;
            }
            else if (_movimiento == EnumsCharacter.Movimiento.Saltar || _movimiento == EnumsCharacter.Movimiento.SaltoAtaque || _movimiento == EnumsCharacter.Movimiento.SaltoDefensa)
            {
                movimiento = EnumsCharacter.Movimiento.AtaqueEspecialSalto;
            }
            else if (_movimiento == EnumsCharacter.Movimiento.AtacarEnElLugar || _movimiento == EnumsCharacter.Movimiento.DefensaEnElLugar)
            {
                movimiento = EnumsCharacter.Movimiento.AtaqueEspecial;
            }
        }
        return movimiento;
    }
    public void DefaultBehavior()
    {
        EnumsCharacter.Movimiento movimiento;
        int min = (int)EnumsCharacter.Movimiento.Nulo + 1;
        int max = (int)EnumsCharacter.Movimiento.Count - 3;
        if (enumsEnemy.movimiento != EnumsCharacter.Movimiento.MoveToPointCombat && enumsEnemy.movimiento != EnumsCharacter.Movimiento.MoveToPointDeath)
        {
            movimiento = (EnumsCharacter.Movimiento)UnityEngine.Random.Range(min, max);
            switch (movimiento)
            {
                case EnumsCharacter.Movimiento.AgacheDefensa:
                    movimiento = EnumsCharacter.Movimiento.Agacharse;
                    break;
                case EnumsCharacter.Movimiento.DefensaEnElLugar:
                    movimiento = EnumsCharacter.Movimiento.AtacarEnElLugar;
                    break;
                case EnumsCharacter.Movimiento.SaltoDefensa:
                    movimiento = EnumsCharacter.Movimiento.Saltar;
                    break;
            }
        }
        else
        {
            movimiento = enumsEnemy.movimiento;
        }
        delaySelectMovement = UnityEngine.Random.Range(minRandomDelayMovement, maxRandomDelayMovement);
        enumsEnemy.movimiento = movimiento;
        if (enumsEnemy.movimiento == EnumsCharacter.Movimiento.SaltoAtaque || enumsEnemy.movimiento == EnumsCharacter.Movimiento.AtaqueEspecialSalto)
        {
            delayAttack = delayAttackJumping;
        }
        if (enumsEnemy.movimiento != EnumsCharacter.Movimiento.SaltoDefensa || enumsEnemy.movimiento != EnumsCharacter.Movimiento.DefensaEnElLugar)
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
            boxColliderControllerSprite.state = BoxColliderController.StateBoxCollider.Normal;

            if (!isJamping && !isDuck
                && enumsEnemy.movimiento != EnumsCharacter.Movimiento.Saltar
                && enumsEnemy.movimiento != EnumsCharacter.Movimiento.SaltoAtaque
                && enumsEnemy.movimiento != EnumsCharacter.Movimiento.SaltoDefensa)
            {
                delaySelectMovement = 0.1f;
                enableSpecialAttack = false;
                enumsEnemy.movimiento = EnumsCharacter.Movimiento.AtaqueEspecial;
                AnimationAttack();
            }
            else if (enumsEnemy.typeEnemy == EnumsEnemy.TiposDeEnemigo.Balanceado)
            {
                if (enumsEnemy.movimiento == EnumsCharacter.Movimiento.Saltar
                    || enumsEnemy.movimiento == EnumsCharacter.Movimiento.SaltoAtaque
                    || enumsEnemy.movimiento == EnumsCharacter.Movimiento.SaltoDefensa)
                {
                    enumsEnemy.movimiento = EnumsCharacter.Movimiento.AtaqueEspecialSalto;
                }
                else if (isDuck)
                {
                    enumsEnemy.movimiento = EnumsCharacter.Movimiento.AtaqueEspecialAgachado;
                }
                else
                {
                    enumsEnemy.movimiento = EnumsCharacter.Movimiento.AtaqueEspecial;
                }
                AnimationAttack();
            }
        }
    }
    public void MoveToPoint(Vector3 pointCombat)
    {
        if (CheckMove(new Vector3(pointCombat.x, transform.position.y, transform.position.z), distanceMove))
        {
            delaySelectMovement = 999;
            if (pointCombat.x < myPrefab.transform.position.x)
            {
                myPrefab.transform.Translate(Vector3.left * Speed * Time.deltaTime);
            }
            else if (pointCombat.x > myPrefab.transform.position.x)
            {
                myPrefab.transform.Translate(Vector3.right * Speed * Time.deltaTime);
            }
        }
        else
        {
            life = maxLife;
            delaySelectMovement = 0;
            enumsEnemy.movimiento = EnumsCharacter.Movimiento.Nulo;
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
                ResetEnemy();
                myPrefab.gameObject.SetActive(false);
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
                        gm.generateEnemy = true;
                        gm.countEnemysDead++;
                        gm.playerData_P1.score = gm.playerData_P1.score + gm.playerData_P1.scoreForEnemyDead;
                        ResetEnemy();
                        pool.Recycle(myPrefab);
                        xpActual = 0;
                        inCombatPosition = false;
                    }
                    break;
                case EnumsGameManager.ModosDeJuego.Historia:
                    if (life <= 0)
                    {
                        gm.generateEnemy = true;
                        gm.countEnemysDead++;
                        gm.playerData_P1.score = gm.playerData_P1.score + gm.playerData_P1.scoreForEnemyDead;
                        ResetEnemy();
                        pool.Recycle(myPrefab);
                        xpActual = 0;
                        inCombatPosition = false;
                        myPrefab.gameObject.SetActive(false);
                    }
                    break;
                case EnumsGameManager.ModosDeJuego.Nulo:
                    if (life <= 0)
                    {
                        gm.countEnemysDead++;
                        gm.playerData_P1.score = gm.playerData_P1.score + gm.playerData_P1.scoreForEnemyDead;
                        ResetEnemy();
                        myPrefab.gameObject.SetActive(false);
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
            enumsEnemy.movimiento = EnumsCharacter.Movimiento.Nulo;
            enumsEnemy.estado = EnumsCharacter.EstadoCharacter.muerto;
        }
    }
    public void CheckMovement()
    {
        
        if (enableDeffence && barraDeEscudo != null && inCombatPosition && enumsEnemy.movimiento != EnumsCharacter.Movimiento.MoveToPointCombat)
        {
            if ((enumsEnemy.movimiento != EnumsCharacter.Movimiento.DefensaEnElLugar
            && enumsEnemy.movimiento != EnumsCharacter.Movimiento.AgacheDefensa
            && enumsEnemy.movimiento != EnumsCharacter.Movimiento.SaltoDefensa)
            || barraDeEscudo.nededBarMaxPorcentage)
            {

                barraDeEscudo.AddPorcentageBar();

                if (barraDeEscudo.GetValueShild() <= barraDeEscudo.porcentageNededForDeffence)
                {
                    barraDeEscudo.SetEnableDeffence(false);
                    if (enumsEnemy.movimiento == EnumsCharacter.Movimiento.AgacheDefensa)
                    {
                        enumsEnemy.movimiento = EnumsCharacter.Movimiento.Agacharse;
                    }
                    else if (enumsEnemy.movimiento == EnumsCharacter.Movimiento.SaltoDefensa)
                    {
                        enumsEnemy.movimiento = EnumsCharacter.Movimiento.Saltar;
                    }
                    else if (enumsEnemy.movimiento != EnumsCharacter.Movimiento.SaltoDefensa
                        && enumsEnemy.movimiento != EnumsCharacter.Movimiento.SaltoAtaque
                        && enumsEnemy.movimiento != EnumsCharacter.Movimiento.Saltar
                        && !isDuck)
                    {
                        enumsEnemy.movimiento = EnumsCharacter.Movimiento.Nulo;
                    }
                }
            }
        }
        if (!enableDeffence && transform.position.y <= InitialPosition.y
            && !isJamping && SpeedJump >= GetAuxSpeedJump()
            && (enumsEnemy.movimiento == EnumsCharacter.Movimiento.DefensaEnElLugar
            || enumsEnemy.movimiento == EnumsCharacter.Movimiento.AgacheDefensa))
        {
            float delay = 0.1f;
            while (enumsEnemy.movimiento == EnumsCharacter.Movimiento.DefensaEnElLugar
                || enumsEnemy.movimiento == EnumsCharacter.Movimiento.AgacheDefensa)
            {
                CheckComportamiento();
                if (delay <= 0)
                {
                    enumsEnemy.movimiento = EnumsCharacter.Movimiento.AtacarEnElLugar;
                }
                else
                {
                    delay = delay - Time.deltaTime;
                }
            }
        }
        if (!enableDeffence)
        {
            if (enumsEnemy.movimiento == EnumsCharacter.Movimiento.SaltoDefensa)
            {
                enumsEnemy.movimiento = EnumsCharacter.Movimiento.Saltar;
            }
        }

        switch (enumsEnemy.movimiento)
        {
            case EnumsCharacter.Movimiento.AtacarEnElLugar:
                CheckDelayAttack(false);
                isDeffended = false;
                break;
            case EnumsCharacter.Movimiento.AgacharseAtaque:
                Duck(structsEnemys.dataEnemy.CantCasillasOcupadas_Y);
                CheckDelayAttack(false);
                isDeffended = false;
                break;
            case EnumsCharacter.Movimiento.SaltoAtaque:
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
            case EnumsCharacter.Movimiento.MoverAtras:
                isDeffended = false;
                if (structsEnemys.dataEnemy.columnaActual < grid.GetCuadrilla_columnas() - 1)
                {
                    MoveRight(posicionesDeMovimiento[structsEnemys.dataEnemy.columnaActual + 1].transform.position);
                }
                else
                {
                    delaySelectMovement = 0;
                }
                break;
            case EnumsCharacter.Movimiento.MoverAdelante:
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
            case EnumsCharacter.Movimiento.Saltar:
                if (!isJamping && SpeedJump > 0 && structsEnemys.particleMovement.particleJump != null)
                {
                    structsEnemys.particleMovement.particleJump.transform.position = new Vector3(transform.position.x, structsEnemys.particleMovement.particleJump.transform.position.y, structsEnemys.particleMovement.particleJump.transform.position.z);
                    structsEnemys.particleMovement.particleJump.SetActive(true);
                }
                isDeffended = false;
                isJamping = true;
                Jump(alturaMaxima.transform.position);
                break;
            case EnumsCharacter.Movimiento.DefensaEnElLugar:
                Deffence();
                break;
            case EnumsCharacter.Movimiento.SaltoDefensa:
                if (!isJamping && SpeedJump > 0 && structsEnemys.particleMovement.particleJump != null)
                {
                    structsEnemys.particleMovement.particleJump.transform.position = new Vector3(transform.position.x, structsEnemys.particleMovement.particleJump.transform.position.y, structsEnemys.particleMovement.particleJump.transform.position.z);
                    structsEnemys.particleMovement.particleJump.SetActive(true);
                }
                isJamping = true;
                Jump(alturaMaxima.transform.position);
                Deffence();
                break;
            case EnumsCharacter.Movimiento.AgacheDefensa:
                Duck(structsEnemys.dataEnemy.CantCasillasOcupadas_Y);
                Deffence();
                break;
            case EnumsCharacter.Movimiento.Agacharse:
                isDeffended = false;
                Duck(structsEnemys.dataEnemy.CantCasillasOcupadas_Y);
                break;
            case EnumsCharacter.Movimiento.AtaqueEspecial:
                isDeffended = false;
                break;
            case EnumsCharacter.Movimiento.AtaqueEspecialAgachado:
                Duck(structsEnemys.dataEnemy.CantCasillasOcupadas_Y);
                isDeffended = false;
                break;
            case EnumsCharacter.Movimiento.AtaqueEspecialSalto:
                if (!isJamping && SpeedJump > 0 && structsEnemys.particleMovement.particleJump != null)
                {
                    structsEnemys.particleMovement.particleJump.transform.position = new Vector3(transform.position.x, structsEnemys.particleMovement.particleJump.transform.position.y, structsEnemys.particleMovement.particleJump.transform.position.z);
                    structsEnemys.particleMovement.particleJump.SetActive(true);
                }
                isDeffended = false;
                isJamping = true;
                Jump(alturaMaxima.transform.position);
                break;
            case EnumsCharacter.Movimiento.MoveToPointCombat:
                isDeffended = false;
                MoveToPoint(pointOfCombat);
                if(enumsEnemy.typeEnemy != EnumsEnemy.TiposDeEnemigo.Jefe)
                    spriteEnemy.ActualSprite = SpriteCharacter.SpriteActual.MoverAdelante;
                break;
            case EnumsCharacter.Movimiento.MoveToPointDeath:
                isDeffended = false;
                MoveToPoint(pointOfDeath);
                break;
        }
        if (enumsEnemy.movimiento != EnumsCharacter.Movimiento.AgacharseAtaque && enumsEnemy.movimiento != EnumsCharacter.Movimiento.AgacheDefensa && enumsEnemy.movimiento != EnumsCharacter.Movimiento.Agacharse)
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
    public void CheckShild()
    {
        if (barraDeEscudo != null)
        {
            if (isDeffended && barraDeEscudo.GetValueShild() > barraDeEscudo.porcentageNededForDeffence
                    && barraDeEscudo.GetEnableDeffence())
            {
                boxColliderControllerAgachado.state = BoxColliderController.StateBoxCollider.Defendido;
                boxColliderControllerParado.state = BoxColliderController.StateBoxCollider.Defendido;
                boxColliderControllerSaltando.state = BoxColliderController.StateBoxCollider.Defendido;
                boxColliderControllerSprite.state = BoxColliderController.StateBoxCollider.Defendido;
            }
            else
            {
                boxColliderControllerAgachado.state = BoxColliderController.StateBoxCollider.Normal;
                boxColliderControllerParado.state = BoxColliderController.StateBoxCollider.Normal;
                boxColliderControllerSaltando.state = BoxColliderController.StateBoxCollider.Normal;
                boxColliderControllerSprite.state = BoxColliderController.StateBoxCollider.Normal;
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
        CheckDeffense(enumsEnemy);
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
            if (enumsEnemy.movimiento == EnumsCharacter.Movimiento.SaltoAtaque || enumsEnemy.movimiento == EnumsCharacter.Movimiento.Nulo || enumsEnemy.movimiento == EnumsCharacter.Movimiento.AtaqueEspecialSalto)
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
            myPrefab.transform.position = InitialPosition;
        }
    }
    public void MoveLeft(Vector3 cuadrillaDestino)
    {
        if (CheckMove(new Vector3(posicionesDeMovimiento[0].transform.position.x, transform.position.y, transform.position.z), distanceMove) && transform.position.x > cuadrillaDestino.x)
        {
            if (enumsEnemy.movimiento != EnumsCharacter.Movimiento.MoverAdelante || !insound)
            {
                insound = true;
                eventWise.StartEvent("moverse");
            }
            Move(Vector3.left);
            enumsEnemy.movimiento = EnumsCharacter.Movimiento.MoverAdelante;
        }
        else if (enumsEnemy.movimiento != EnumsCharacter.Movimiento.Nulo)
        {
            insound = false;
            structsEnemys.dataEnemy.columnaActual--;
            enumsEnemy.movimiento = EnumsCharacter.Movimiento.Nulo;
            delaySelectMovement = 0;
        }
    }
    public void MoveRight(Vector3 cuadrillaDestino)
    {
        if (CheckMove(new Vector3(posicionesDeMovimiento[posicionesDeMovimiento.Length-1].transform.position.x, transform.position.y, transform.position.z), distanceMove) && transform.position.x < cuadrillaDestino.x)
        {
            if (enumsEnemy.movimiento != EnumsCharacter.Movimiento.MoverAtras || !insound)
            {
                insound = true;
                eventWise.StartEvent("moverse");
            }
            Move(Vector3.right);
            enumsEnemy.movimiento = EnumsCharacter.Movimiento.MoverAtras;
        }
        else if (enumsEnemy.movimiento != EnumsCharacter.Movimiento.Nulo)
        {
            insound = false;
            structsEnemys.dataEnemy.columnaActual++;
            enumsEnemy.movimiento = EnumsCharacter.Movimiento.Nulo;
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
        if (transform.position.y > InitialPosition.y || isJamping)
        {
            if (transform.position.y <= InitialPosition.y)
            {
                eventWise.StartEvent("saltar");
            }
            if (enumsEnemy.movimiento != EnumsCharacter.Movimiento.SaltoAtaque && enumsEnemy.movimiento != EnumsCharacter.Movimiento.SaltoDefensa && enumsEnemy.movimiento != EnumsCharacter.Movimiento.AtaqueEspecialSalto)
            {
                enumsEnemy.movimiento = EnumsCharacter.Movimiento.Saltar;
            }
            MoveJamp(Vector3.up);
            if (SpeedJump <= 0)
            {
                isJamping = false;
            }
            if (delaySelectMovement <= 0)
            {
                delaySelectMovement = 0.1f;
            }
            if (enumsEnemy.movimiento == EnumsCharacter.Movimiento.SaltoDefensa)
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

    public bool GetIsDeffended()
    {
        return isDeffended;
    }

    public void SetDelaySelectMovement(float delay)
    {
        delaySelectMovement = delay;
    }

    public bool GetInCombatPosition() 
    {
        return inCombatPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "pointOfCombat")
        {
            inCombatPosition = true;
        }
    }
}
