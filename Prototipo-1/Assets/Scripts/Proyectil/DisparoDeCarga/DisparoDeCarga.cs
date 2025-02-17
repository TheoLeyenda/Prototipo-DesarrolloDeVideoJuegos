﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DisparoDeCarga : MonoBehaviour
{
    public float timeLife;
    public float auxTimeLife;
    public float damage;
    private float auxDamage;

    public ParticleSystem particleSystemProyectil;
    public ParticleSystem.MainModule mainModule;
    private float auxStartSpeedParticleSystem;
    private GameObject objectCollisionPartycleSystem;
    private float proximity = 2f;

    private void Start()
    {
        auxDamage = damage;
        if(particleSystemProyectil != null)
        {
            mainModule = particleSystemProyectil.main;
            auxStartSpeedParticleSystem = mainModule.startSpeedMultiplier;
        }
    }
    protected virtual void Update()
    {
        CheckTimeLife();
    }
        
    public virtual void CheckTimeLife()
    {
        if (timeLife > 0)
        {
            timeLife = timeLife - Time.deltaTime;
        }
        else if (timeLife <= 0)
        {
            timeLife = auxTimeLife;
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "BoxColliderController")
        {
            BoxColliderController boxColliderController = collision.GetComponent<BoxColliderController>();
            if (boxColliderController.player == null)
            {
                return;
            }
            if (boxColliderController.player.PD.Blindaje <= 0)
                boxColliderController.player.PD.lifePlayer = boxColliderController.player.PD.lifePlayer - damage;
            else
                boxColliderController.player.PD.Blindaje = boxColliderController.player.PD.Blindaje - damage;
        }
        if(collision.tag == "MagicBust")
        {
            objectCollisionPartycleSystem = collision.gameObject;
            Vector3 Distance = objectCollisionPartycleSystem.transform.position - collision.transform.position;
            damage = 0;
            if(particleSystemProyectil != null)
            {
                mainModule.startLifetime = 0.65f;
                mainModule.startSpeedMultiplier = Distance.x + proximity;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "MagicBust")
        {
            damage = auxDamage;
            if(particleSystemProyectil != null)
            {
                mainModule.startLifetime = 2.1f;
                mainModule.startSpeedMultiplier = auxStartSpeedParticleSystem;
            }
        }
    }
}
