using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilMagicBust : DisparoDeCarga
{
    private bool enableDamage = false;
    public bool DestroyProjectileEnemy;
    public Animator animator;

    public EventWise eventWise { set; get; }

    private string nameEventActivate = "ataque_especial_gotica_op1";
    private string nameEventDisable = "parar_ataque_especial_gotica_op1";

    private GameData gd;

    private void Start()
    {
        gd = GameData.instaceGameData;
    }

    private void OnDisable()
    {
        DisableDamage();
        timeLife = auxTimeLife;
        animator.SetBool("FinalTornado", false);
        
        if(gd.initScene)
            eventWise.StartEvent(nameEventDisable);
    }

    private void OnEnable()
    {
        if(gd.initScene)
            eventWise.StartEvent(nameEventActivate);
    }

    protected override void Update()
    {
        base.Update();
    }
    public void EnableDamage()
    {
        enableDamage = true;
    }
    public override void CheckTimeLife()
    {
        if (timeLife > 0)
        {
            timeLife = timeLife - Time.deltaTime;
        }
        else if(timeLife <= 0)
        {
            animator.SetBool("FinalTornado", true);            
        }
    }
    public void DisableProjectile()
    {
        timeLife = auxTimeLife;
        gameObject.SetActive(false);
    }
    public void DisableDamage()
    {
        enableDamage = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "BoxColliderController" && enableDamage)
        {
            BoxColliderController boxColliderController = collision.GetComponent<BoxColliderController>();
            Player p = boxColliderController.player;
            Enemy e = boxColliderController.enemy;
            if (p != null)
            {
                if (p.PD.Blindaje <= 0)
                {
                    p.PD.lifePlayer = p.PD.lifePlayer - damage;
                }
                else
                {
                    p.PD.Blindaje = p.PD.Blindaje - damage / 2;
                }
            }
            else if (e != null)
            {
                if (e.Blindaje <= 0)
                {
                    e.life = e.life - damage;
                }
                else
                {
                    e.Blindaje = e.Blindaje - damage / 2;
                }
            }
        }
        if((collision.tag == "Proyectil" || collision.tag == "ProyectilGaseosa") && DestroyProjectileEnemy)
        {
            Proyectil proyectil = collision.GetComponent<Proyectil>();
            ProyectilLimo proyectilLimo = collision.GetComponent<ProyectilLimo>();
            GranadaGaseosa granadaGaseosa = collision.GetComponent<GranadaGaseosa>();
            if (granadaGaseosa != null)
            {
                granadaGaseosa.timeLife = 0f;
                granadaGaseosa.GetPoolObject().Recycle();
            }
            if (proyectil.animator != null)
            {
                proyectil.AnimationHit();
            }
            else if (proyectilLimo != null)
            {
                proyectilLimo.AnimationHit();
            }
        }
    }
}
