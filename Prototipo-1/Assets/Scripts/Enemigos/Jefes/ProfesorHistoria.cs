using System;
using System.Collections.Generic;
using UnityEngine;

public class ProfesorHistoria : Enemy
{
    //HACER EL ATAQUE ESPECIAL DEL LIBRO DE EDISON BASANDOME EN LOS ATAQUES ESPECIALES DEL PROFESOR DE ANATOMIA.
    // Start is called before the first frame update
    public enum MyAnimations
    {
        Idle,
        InicioMasiveAttack,
        MasiveAttack,
        FinalMasiveAttack,
        LibroEdison,
        InicioDebateInjusto,
        DebateInjusto,
        FinalDebateInjusto,
        Death,
        RecibirDanio,
        Count,
    }

    [Header("Config Profesor Historia")]
    public float speedChargeSpecialAttack = 1;
    public List<string> NameAnimations;
    public bool ChargeInSpecialAttack;
    public SpriteBoss_ProfesorHistoria spriteBoss_ProfesorHistoria;
    private bool initMasiveAttack_Lanzado;
    public Pool poolLibrosAttack;
    [HideInInspector]
    public bool specialAttackLibroEdison_Lanzado;
    [HideInInspector]
    public bool specialAttackDebateInjusto_Lanzado;
    [HideInInspector]
    public bool NextSpecialAttack;

    [Header("Config Special Attack LIBRO EDISON")]
    public int DamageRayoEdison;
    public float DelayTitileoCasillaLibroEdison;
    public int countThrowSpecialAttackLibroEdison;
    public int auxCountThrowSpecialAttackLibroEdison;
    public float timeLifeLibroEdison;
    public Pool poolSpecialAttackLibroEdison;
    public int countCasillasAfectadas = 2;
    public List<GameObject> GeneratorsSpecialAttackLibroEdison;

    [Header("Config Special Attack DEBTATE INJUSTO")]
    public int DamageDebateInjusto;
    public float TimeLifeDebateInjusto;
    public float AuxTimeLifeDebateInjusto;
    public DisparoDeCarga ProyectilDebateInjusto;
    public GameObject GeneradorDebateInjusto;
    public int[] indexCasillasAfectadas;
    public float DelayTitileoCasillaDebateInjusto;

    [Header("Porcentage Direccion Attack")]
    public float porcentageUpDirection;
    public float porcentageForwardDirection;
    public float porcentageDownDirection;

    [Header("Vectores de Direccion")]
    public GameObject UpVector;
    public GameObject ForwardVector;
    public GameObject DownVector;

    [HideInInspector]
    public FSM fsmProfesorHistoria;
    private bool Idied = false;
    private bool OnProfesorHistoria = false;

    public static event Action<Enemy, float, int> OnInitTrowSpecialAttackLibroEdison;
    public static event Action<Enemy, float, int[]> OnInitTrowSpecialAttackDebateInjusto;
    public static event Action<ProfesorHistoria> InCombatPoint;

    public enum EstadoProfesorHistoria
    {
        Idle,
        MasiveAttack,
        FirstSpecialAttackLibroDeEdison,
        SecondSpecialAttackDebateInjusto,
        Death,
        Count
    }
    //HAGO UN ENUM DE Eventos
    public enum EventosProfesorHistoria
    {
        StartMasiveAttack,
        SpecialAttackBarInMiddleCharge,
        SpecialAttackBarCompleteCharge,
        LifeOut,
        Count
    }
    private void OnEnable()
    {
        OnProfesorHistoria = false;
        NextSpecialAttack = true;
        specialAttackLibroEdison_Lanzado = false;
        specialAttackDebateInjusto_Lanzado = false;
        Idied = false;
        Grid.OnSettingTitileo_2 += SetTargetGrid;
    }
    private void OnDisable()
    {
        OnProfesorHistoria = false;
        NextSpecialAttack = true;
        specialAttackLibroEdison_Lanzado = false;
        specialAttackDebateInjusto_Lanzado = false;
        Idied = false;
        enableMovement = true;
        Grid.OnSettingTitileo_2 -= SetTargetGrid;
    }
    private void Awake()
    {
        /*
        PATRÓN: Aparece detrás de una cortina de humo y comienza el diálogo.
        -Empieza el combate y el profesor tirara libros en direcciones al azar (diagonal arriba, centro diagonal abajo) 
        Hacer un random de tres vectores de dirección.
        -A medida que avanza el tiempo se carga su barra de ataque especial cuando esta llega a la mitad tira 
        su ataque especial de Libro de Edison, al llegar al 100% tira su ataque especial de Debate Injusto este se expandirá 
        en forma de cono haciendo daño por segundo al jugador si lo toca y afectando a las casillas trasera 
        y media también afecta el alto y bajo de la casilla trasera y el medio de la casilla media dejando como única 
        casilla de escape la casilla de adelante agachado(titilaran las dos ultimas casillas al mandar este ataque)
        -Luego hace un gesto de furioso y Grita “PEQUEÑA PULGA IGNORANTE” y vuelve al primer patron de ataque.
       */
        fsmProfesorHistoria = new FSM((int)EstadoProfesorHistoria.Count, (int)EventosProfesorHistoria.Count,
            (int)EstadoProfesorHistoria.Idle);

        fsmProfesorHistoria.SetRelations((int)EstadoProfesorHistoria.Idle, (int)EstadoProfesorHistoria.MasiveAttack,
            (int)EventosProfesorHistoria.StartMasiveAttack);

        fsmProfesorHistoria.SetRelations((int)EstadoProfesorHistoria.MasiveAttack, (int)EstadoProfesorHistoria.FirstSpecialAttackLibroDeEdison,
            (int)EventosProfesorHistoria.SpecialAttackBarInMiddleCharge);

        fsmProfesorHistoria.SetRelations((int)EstadoProfesorHistoria.FirstSpecialAttackLibroDeEdison, (int)EstadoProfesorHistoria.MasiveAttack,
            (int)EventosProfesorHistoria.StartMasiveAttack);

        fsmProfesorHistoria.SetRelations((int)EstadoProfesorHistoria.MasiveAttack, (int)EstadoProfesorHistoria.SecondSpecialAttackDebateInjusto,
            (int)EventosProfesorHistoria.SpecialAttackBarCompleteCharge);

        fsmProfesorHistoria.SetRelations((int)EstadoProfesorHistoria.SecondSpecialAttackDebateInjusto, (int)EstadoProfesorHistoria.MasiveAttack,
            (int)EventosProfesorHistoria.StartMasiveAttack);

        fsmProfesorHistoria.SetRelations((int)EstadoProfesorHistoria.Idle, (int)EstadoProfesorHistoria.Death,
            (int)EventosProfesorHistoria.LifeOut);

        fsmProfesorHistoria.SetRelations((int)EstadoProfesorHistoria.MasiveAttack, (int)EstadoProfesorHistoria.Death,
            (int)EventosProfesorHistoria.LifeOut);

        fsmProfesorHistoria.SetRelations((int)EstadoProfesorHistoria.FirstSpecialAttackLibroDeEdison, (int)EstadoProfesorHistoria.Death,
            (int)EventosProfesorHistoria.LifeOut);

        fsmProfesorHistoria.SetRelations((int)EstadoProfesorHistoria.SecondSpecialAttackDebateInjusto, (int)EstadoProfesorHistoria.Death,
            (int)EventosProfesorHistoria.LifeOut);

    }
    public override void Start()
    {
        NextSpecialAttack = true;
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        if (enableMovement)
        {
            if (!Idied)
            {
                base.Update();
                switch (fsmProfesorHistoria.GetCurrentState())
                {
                    case (int)EstadoProfesorHistoria.Idle:
                        Idle();
                        break;
                    case (int)EstadoProfesorHistoria.MasiveAttack:
                        if (enemyPrefab.transform.position.x <= 5.355f)
                        {
                            if (!OnProfesorHistoria && InCombatPoint != null)
                            {
                                OnProfesorHistoria = true;
                                InCombatPoint(this);
                            }
                            InitMasiveAttack();
                        }
                        break;
                    case (int)EstadoProfesorHistoria.FirstSpecialAttackLibroDeEdison:
                        if (NextSpecialAttack && countThrowSpecialAttackLibroEdison >= 0)
                        {
                            NextSpecialAttack = false;
                            spriteBoss_ProfesorHistoria.PlayAnimation(NameAnimations[(int)MyAnimations.LibroEdison]);
                            countThrowSpecialAttackLibroEdison--;
                        }
                        else if (countThrowSpecialAttackLibroEdison < 0)
                        {
                            countThrowSpecialAttackLibroEdison = auxCountThrowSpecialAttackLibroEdison;
                            spriteBoss_ProfesorHistoria.animator.SetBool(NameAnimations[(int)ProfesorHistoria.MyAnimations.FinalMasiveAttack], false);
                            fsmProfesorHistoria.SendEvent((int)EventosProfesorHistoria.StartMasiveAttack);
                            NextSpecialAttack = true;
                            initMasiveAttack_Lanzado = false;
                        }
                        break;
                    case (int)EstadoProfesorHistoria.SecondSpecialAttackDebateInjusto:
                        if (!specialAttackDebateInjusto_Lanzado)
                        {
                            spriteBoss_ProfesorHistoria.PlayAnimation(NameAnimations[(int)MyAnimations.InicioDebateInjusto]);
                            specialAttackDebateInjusto_Lanzado = true;
                        }
                        initMasiveAttack_Lanzado = false;
                        break;
                    case (int)EstadoProfesorHistoria.Death:
                        initMasiveAttack_Lanzado = false;
                        if (!Idied)
                        {
                            Idied = true;
                            spriteBoss_ProfesorHistoria.PlayAnimation(NameAnimations[(int)MyAnimations.Death]);
                        }
                        break;
                }

                if ((fsmProfesorHistoria.GetCurrentState() != (int)EstadoProfesorHistoria.FirstSpecialAttackLibroDeEdison
                    && fsmProfesorHistoria.GetCurrentState() != (int)EstadoProfesorHistoria.SecondSpecialAttackDebateInjusto)
                    || ChargeInSpecialAttack)
                {
                    ChargeSpecialAttack();
                }
                CheckThrowSpecialAttack();

                if (life <= 0)
                {
                    fsmProfesorHistoria.SendEvent((int)EventosProfesorHistoria.LifeOut);
                }

            }
        }
    }

    public void Idle()
    {
        initMasiveAttack_Lanzado = false;
        fsmProfesorHistoria.SendEvent((int)EventosProfesorHistoria.StartMasiveAttack);
    }
    public void InitMasiveAttack()
    {
        if (!initMasiveAttack_Lanzado)
        {
            //Debug.Log("ENTRE");
            initMasiveAttack_Lanzado = true;
            spriteBoss_ProfesorHistoria.PlayAnimation(NameAnimations[(int)MyAnimations.InicioMasiveAttack]);
        }
    }
    public void MasiveAttack()
    {
        float DirecctionSelector = UnityEngine.Random.Range(0, 100);
        GameObject go = null;
        ProyectilLibro proyectil = null;

        go = poolLibrosAttack.GetObject();
        proyectil = go.GetComponent<ProyectilLibro>();
        proyectil.SetEnemy(gameObject.GetComponent<Enemy>());
        proyectil.SetDobleDamage(false);
        proyectil.disparadorDelProyectil = Proyectil.DisparadorDelProyectil.Enemigo;
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
        if (applyColorShoot == ApplyColorShoot.None || applyColorShoot == ApplyColorShoot.Stela)
        {
            proyectil.On(Proyectil.typeProyectil.Nulo, false);
        }
        else
        {
            proyectil.On(Proyectil.typeProyectil.Nulo, true);
        }


        if (DirecctionSelector < porcentageDownDirection)
        {
            //DOWN SHOOT.
            proyectil.posicionDisparo = Proyectil.PosicionDisparo.PosicionBaja;
            go.transform.rotation = DownVector.transform.rotation;
            go.transform.position = DownVector.transform.position;
        }
        else if (DirecctionSelector >= porcentageDownDirection && DirecctionSelector < porcentageDownDirection + porcentageForwardDirection)
        {
            //FORWARD SHOOT.
            proyectil.posicionDisparo = Proyectil.PosicionDisparo.PosicionMedia;
            go.transform.rotation = ForwardVector.transform.rotation;
            go.transform.position = ForwardVector.transform.position;
        }
        else if (DirecctionSelector >= porcentageDownDirection + porcentageForwardDirection)
        {
            //UP SHOOT.
            proyectil.posicionDisparo = Proyectil.PosicionDisparo.PosicionAlta;
            go.transform.rotation = UpVector.transform.rotation;
            go.transform.position = UpVector.transform.position;
        }

        proyectil.ShootForward();
    }
    public void ChargeSpecialAttack()
    {
        xpActual = xpActual + Time.deltaTime * speedChargeSpecialAttack;
    }

    public void CheckThrowSpecialAttack()
    {
        if (xpActual >= xpNededSpecialAttack / 2 && !specialAttackLibroEdison_Lanzado)
        {
            spriteBoss_ProfesorHistoria.animator.SetBool(NameAnimations[(int)MyAnimations.FinalMasiveAttack], true);
        }
        else if (xpActual >= xpNededSpecialAttack && !specialAttackDebateInjusto_Lanzado)
        {
            spriteBoss_ProfesorHistoria.animator.SetBool(NameAnimations[(int)MyAnimations.FinalMasiveAttack], true);
        }
    }
    //------------------------ PRIMER ATAQUE ESPECIAL ---------------------//
    public void InitSpecialAttack_LibroEdison()
    {
        if (OnInitTrowSpecialAttackLibroEdison != null)
        {
            OnInitTrowSpecialAttackLibroEdison(this, DelayTitileoCasillaLibroEdison, countCasillasAfectadas);
        }
    }
    public void SetTargetGrid(Grid g, List<Vector3> targets)
    {
        if (g != null)
        {
            if (targets.Count >= GeneratorsSpecialAttackLibroEdison.Count)
            {
                GeneratorsSpecialAttackLibroEdison[0].transform.position = new Vector3(targets[0].x, GeneratorsSpecialAttackLibroEdison[0].transform.position.y, targets[0].z);
                GeneratorsSpecialAttackLibroEdison[1].transform.position = new Vector3(targets[1].x, GeneratorsSpecialAttackLibroEdison[1].transform.position.y, targets[1].z);
            }
            else
            {
                Debug.Log("Targets insuficientes");
            }
        }
    }

    public void SpecialAttack_LibroEdison()
    {
        GameObject go_1 = poolSpecialAttackLibroEdison.GetObject();
        GameObject go_2 = poolSpecialAttackLibroEdison.GetObject();
        if (go_1 != null && go_2 != null)
        {
            go_1.transform.position = GeneratorsSpecialAttackLibroEdison[0].transform.position;
            go_2.transform.position = GeneratorsSpecialAttackLibroEdison[1].transform.position;
            RayoEdison rayoEdison_1 = go_1.GetComponent<RayoEdison>();
            RayoEdison rayoEdison_2 = go_2.GetComponent<RayoEdison>();
            rayoEdison_1.damage = DamageRayoEdison;
            rayoEdison_2.damage = DamageRayoEdison;
            rayoEdison_1.DestroyForTime = true;
            rayoEdison_2.DestroyForTime = true;
            rayoEdison_1.timeLife = timeLifeLibroEdison;
            rayoEdison_1.auxTimeLife = timeLifeLibroEdison;
            rayoEdison_2.timeLife = timeLifeLibroEdison;
            rayoEdison_2.auxTimeLife = timeLifeLibroEdison;
            rayoEdison_1.RayoEdisonAnimation();
            rayoEdison_2.RayoEdisonAnimation();
        }
    }
    //------------------------ PRIMER ATAQUE ESPECIAL ---------------------//

    //------------------------ SEGUNDO ATAQUE ESPECIAL ---------------------//
    public void InitSpecialAttack_DebateInjusto()
    {
        if (OnInitTrowSpecialAttackDebateInjusto != null)
        {
            OnInitTrowSpecialAttackDebateInjusto(this, DelayTitileoCasillaDebateInjusto, indexCasillasAfectadas);
        }
        xpActual = 0;
    }
    public void SpecialAttack_DebateInjusto()
    {
        ProyectilDebateInjusto.timeLife = TimeLifeDebateInjusto;
        ProyectilDebateInjusto.auxTimeLife = AuxTimeLifeDebateInjusto;
        ProyectilDebateInjusto.damage = DamageDebateInjusto;
        ProyectilDebateInjusto.transform.position = GeneradorDebateInjusto.transform.position;
        ProyectilDebateInjusto.gameObject.SetActive(true);
    }
    //------------------------ SEGUNDO ATAQUE ESPECIAL ---------------------//

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Proyectil proyectil = collision.GetComponent<Proyectil>();
        if (proyectil != null)
        {
            if ((proyectil.disparadorDelProyectil == Proyectil.DisparadorDelProyectil.Jugador1
                    || proyectil.disparadorDelProyectil == Proyectil.DisparadorDelProyectil.Jugador2)
                    && proyectil.tipoDeProyectil != Proyectil.typeProyectil.AtaqueEspecial)
            {
                life = life - proyectil.damage;
                proyectil.AnimationHit();
                if (fsmProfesorHistoria.GetCurrentState() == (int)ProfesorHistoria.EstadoProfesorHistoria.MasiveAttack)
                {
                    spriteBoss_ProfesorHistoria.PlayAnimation("RecibirDanio");
                }
            }
        }
    }
}
