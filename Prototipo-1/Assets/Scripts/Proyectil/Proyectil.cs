﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public enum typeProyectil
    {
        Nulo,
        ProyectilAereo,
        ProyectilNormal,
        ProyectilBajo,
        ProyectilParabola,
        AtaqueEspecial,
    }
    public bool deadForTimeOnly;
    public bool completeColorStela = true;
    public string nameAnimationHit = "Empty";
    public SpriteRenderer spriteRenderer;
    public List<Sprite> propsDisparoAereo;
    public List<Sprite> propsDisparoNormal;
    public List<Sprite> propsDisparoBajo;
    public List<Sprite> propsDisparoAereoBlanco;
    public List<Sprite> propsDisparoNormalBlanco;
    public List<Sprite> propsDisparoBajoBlanco;
    public typeProyectil tipoDeProyectil;
    public float speed;
    public float timeLife;
    public float auxTimeLife;
    public float damageCounterAttack;
    public float damage;
    public float auxDamage;
    public Rigidbody2D rg2D;
    public Transform vectorForward;
    public Transform vectorForwardUp;
    public Transform vectorForwardDown;
    public Pool pool;
    protected bool dobleDamage;
    protected PoolObject poolObject;
    protected Player PLAYER1;
    protected Player PLAYER2;
    protected Enemy ENEMY;
    protected GameManager gm;
    public DisparadorDelProyectil disparadorDelProyectil;
    public TrailRenderer trailRenderer;
    [HideInInspector]
    public PosicionDisparo posicionDisparo;
    public Animator animator;
    private CircleCollider2D circleCollider2D;
    private float auxTimeTrileRenderer;
    [HideInInspector]
    public Transform initialPosition;
    public bool colisionPlayer;
    protected bool inAnimation;
    protected bool soundgenerate;

    protected GameData gd; 
    [HideInInspector] public EventWise eventWise;

    private void Awake()
    {
        soundgenerate = false;
        circleCollider2D = GetComponent<CircleCollider2D>();
        if (circleCollider2D != null)
        {
            circleCollider2D.enabled = true;
        }
        if (animator != null)
        {
            animator.enabled = false;
        }
    }
    private void OnDisable()
    {
        if (trailRenderer != null)
        {
            trailRenderer.Clear();
        }
        if (trailRenderer != null)
        {
            trailRenderer.enabled = false;
        }

        soundgenerate = false;
        SetColorProyectil(Color.white);
    }
    public void SetColorProyectil(Color color)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = color;
        }
    }
    private void Start()
    {
        gd = GameData.instaceGameData;
        inAnimation = false;
        if (GameManager.instanceGameManager != null)
        {
            gm = GameManager.instanceGameManager;
        }
        poolObject = GetComponent<PoolObject>();
    }
    private void OnEnable()
    {
        inAnimation = false;
        timeLife = auxTimeLife;
        if (circleCollider2D != null)
        {
            circleCollider2D.enabled = true;
        }
        if (trailRenderer != null)
        {
            trailRenderer.enabled = true;
        }
    }
    public virtual void Sonido()
    {
        eventWise.StartEvent("tirar_goma");
    }
    private void Update()
    {
        if (eventWise == null)
        {
            eventWise = GameObject.Find("EventWise").GetComponent<EventWise>();
        }
        if (eventWise != null && !soundgenerate)
        {
            soundgenerate = true;
            Sonido();
        }
        CheckTimeLife();
    }
    public void Dead()
    {
        if (poolObject != null && poolObject.gameObject.activeSelf)
        {
            if (trailRenderer != null)
            {
                trailRenderer.Clear();
            }
            damage = auxDamage;
            dobleDamage = false;
            poolObject.Recycle();
            gameObject.SetActive(false);
        }
    }
    public void On(typeProyectil _tipoDeProyectil,bool WhiteProyectile)
    {
        if (trailRenderer != null)
        {
            trailRenderer.enabled = true;
        }
        if (circleCollider2D != null)
        {
            circleCollider2D.enabled = true;
        }
        if (animator != null)
        {
            animator.enabled = false;
        }   
        tipoDeProyectil = _tipoDeProyectil;
        int subIndiceSprite;
        if (propsDisparoAereo.Count > 0 && propsDisparoBajo.Count > 0 && propsDisparoNormal.Count > 0)
        {
            switch (tipoDeProyectil)
            {
                case typeProyectil.ProyectilAereo:
                    if (!WhiteProyectile)
                    {
                        subIndiceSprite = Random.Range(0, propsDisparoAereo.Count);
                        spriteRenderer.sprite = propsDisparoAereo[subIndiceSprite];
                    }
                    else
                    {
                        subIndiceSprite = Random.Range(0, propsDisparoAereoBlanco.Count);
                        spriteRenderer.sprite = propsDisparoAereoBlanco[subIndiceSprite];
                    }
                    break;
                case typeProyectil.ProyectilNormal:
                    if (!WhiteProyectile)
                    {
                        subIndiceSprite = Random.Range(0, propsDisparoNormal.Count);
                        spriteRenderer.sprite = propsDisparoNormal[subIndiceSprite];
                    }
                    else
                    {
                        subIndiceSprite = Random.Range(0, propsDisparoNormalBlanco.Count);
                        spriteRenderer.sprite = propsDisparoNormalBlanco[subIndiceSprite];
                    }
                    break;
                case typeProyectil.ProyectilBajo:
                    if (!WhiteProyectile)
                    {
                        subIndiceSprite = Random.Range(0, propsDisparoBajo.Count);
                        spriteRenderer.sprite = propsDisparoBajo[subIndiceSprite];
                    }
                    else
                    {
                        subIndiceSprite = Random.Range(0, propsDisparoBajoBlanco.Count);
                        spriteRenderer.sprite = propsDisparoBajoBlanco[subIndiceSprite];
                    }
                    break;
            }
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
            if (trailRenderer != null)
            {
                trailRenderer.Clear();
            }
            damage = auxDamage;
            dobleDamage = false;
            AnimationHit();
            if (poolObject != null)
            {
                poolObject.Recycle();
            }
            else
            {
                gameObject.SetActive(false);
            }
            
        }
    }
    public virtual void ShootForward()
    {
        if (trailRenderer != null)
        {
            trailRenderer.enabled = true;
        }
        rg2D.AddForce(transform.right * speed, ForceMode2D.Force);
    }
    public void ShootForwardUp()
    {
        if (trailRenderer != null)
        {
            trailRenderer.enabled = true;
        }
        rg2D.AddRelativeForce(vectorForwardUp.right * speed);
    }
    public void ShootForwardDown()
    {
        if (trailRenderer != null)
        {
            trailRenderer.enabled = true;
        }
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
    public void AnimationHit()
    {
        if (animator != null)
        {
            SetColorProyectil(Color.white);
            if (trailRenderer != null)
            {
                trailRenderer.Clear();
            }
            inAnimation = true;
            rg2D.velocity = Vector3.zero;
            animator.enabled = true;
            if (circleCollider2D != null)
            {
                circleCollider2D.enabled = false;
            }
            if (gameObject.activeSelf && animator.gameObject.activeSelf && nameAnimationHit != "Empty")
            {
                animator.Play(nameAnimationHit);
            }
            if (trailRenderer != null)
            {
                trailRenderer.enabled = false;
            }
        }
        else
        {
            Dead();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "limite" && !deadForTimeOnly)
        {
            Dead();
        }
    }
    public void SetColorStela(Color color)
    {
        if (tipoDeProyectil != typeProyectil.AtaqueEspecial)
        {
            if (trailRenderer != null)
            {
                trailRenderer.startColor = color;
                if (completeColorStela)
                {
                    trailRenderer.endColor = color;
                }
            }
        }
        
    }
}
