using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilMagicBust : DisparoDeCarga
{
    // Start is called before the first frame update
    private bool enableDamage = false;
    public bool DestroyProjectileEnemy;
    public Animator animator;
    private void OnDisable()
    {
        DisableDamage();
        timeLife = auxTimeLife;
        animator.SetBool("FinalTornado", false);
    }
    // Update is called once per frame
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
            //GranadaGaseosa proyectilParabola = collision.GetComponent<GranadaGaseosa>();
            //ProyectilInparable proyectilImparabe = collision.GetComponent<ProyectilInparable>();
            ProyectilLimo proyectilLimo = collision.GetComponent<ProyectilLimo>();
            //ProyectilChicle proyectilChicle = collision.GetComponent<ProyectilChicle>();
            //Debug.Log("Entre colision con proyectil");
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
            /*else if(proyectilParabola != null)
            {
                //Debug.Log("Entre AnimacioHit");
                proyectilParabola.AnimationHit();
            }
            else if(proyectilImparabe != null)
            {
                proyectilImparabe.AnimationHit();
            }

            else if(proyectilChicle != null)
            {
                proyectilChicle.AnimationHit();
            }*/
        }
    }
}
