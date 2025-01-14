﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PowerUp : MonoBehaviour
{
    public enum TypeCheckCollision
    {
        None,
        CollisionPlayer,
        CollisionProyectile,
    }
    public enum TypePowerUp
    {
        PowerUpDelay,
        PowerUpDisable,
    }
    public bool disableCollision;
    [HideInInspector]
    public TypePowerUp typePowerUp;
    public TypeCheckCollision typeCheckCollision;
    [HideInInspector]
    public ThrowPowerUpController.UserPowerUpController userPowerUp;
    public Player player;
    public Enemy enemy;
    public string namePowerUp;
    public float delayEffect;
    protected float auxDelayEffect;
    [HideInInspector]
    public bool enableEffect = false;
    public static event Action<PowerUp> OnDisablePowerUpEffect;
    public static event Action<PowerUp> OnCollisionWhitPlayer;
    public static event Action<PowerUp> OnCollisionWhitProyectil;

    protected virtual void Start()
    {
        auxDelayEffect = delayEffect;
    }

    protected virtual void OnEnable()
    {
        PowerUp_VidaExtra.DisablePowerUp += DisablePowerUpEffect;
        PowerUp_SuperVelocidad.DisablePowerUp += DisablePowerUpEffect;
        PowerUp_QuietoAhi.DisablePowerUp += DisablePowerUpEffect;
        PowerUp_NadaDeDefensa.DisablePowerUp += DisablePowerUpEffect;
        PowerUp_Invulnerhabilidad.DisablePowerUp += DisablePowerUpEffect;
        PowerUp_DoblePuntuacion.DisablePowerUp += DisablePowerUpEffect;
        PowerUp_Blindaje.DisablePowerUp += DisablePowerUpEffect;
        PowerUp_DividirPuntuacion.DisablePowerUp += DisablePowerUpEffect;
        Enemy.OnDie += CheckDeadTarget;
        Player.OnDie += CheckDeadTarget;
    }
    protected virtual void OnDisable()
    {
        if(OnDisablePowerUpEffect != null)
            OnDisablePowerUpEffect(this);
        PowerUp_VidaExtra.DisablePowerUp -= DisablePowerUpEffect;
        PowerUp_SuperVelocidad.DisablePowerUp -= DisablePowerUpEffect;
        PowerUp_QuietoAhi.DisablePowerUp -= DisablePowerUpEffect;
        PowerUp_NadaDeDefensa.DisablePowerUp -= DisablePowerUpEffect;
        PowerUp_Invulnerhabilidad.DisablePowerUp -= DisablePowerUpEffect;
        PowerUp_DoblePuntuacion.DisablePowerUp -= DisablePowerUpEffect;
        PowerUp_Blindaje.DisablePowerUp -= DisablePowerUpEffect;
        PowerUp_DividirPuntuacion.DisablePowerUp -= DisablePowerUpEffect;
        Enemy.OnDie -= CheckDeadTarget;
        Player.OnDie -= CheckDeadTarget;
    }
    public void DisablePowerUpEffect(PowerUp powerUp)
    {
        if (OnDisablePowerUpEffect != null)
        {
            OnDisablePowerUpEffect(this);
        }
    }
    public void EffectDestroyPowerUp()
    {
        //Animacion de powerUp al ser destruido.
    }
    public void EffectDisablePowerUp()
    {
        //Animacion de powerUp al ser adquirido.
    }
    public virtual void CheckDeadTarget(Enemy e)
    {
        OnDisablePowerUpEffect(this);
    }
    public virtual void CheckDeadTarget(Player p)
    {
        OnDisablePowerUpEffect(this);
    }
    public virtual void DisableEffect() { }
    public virtual void ActivatedPowerUp() { }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (typeCheckCollision == TypeCheckCollision.None) return;

        switch (typeCheckCollision) {
            case TypeCheckCollision.CollisionPlayer:
                if (player != null) return;

                if (collision.tag == "Player")
                {
                    if(OnCollisionWhitPlayer != null)
                    {
                        OnCollisionWhitPlayer(this);
                    }
                }
                break;
            case TypeCheckCollision.CollisionProyectile:
                if (collision.tag == "Proyectil")
                {
                    Proyectil proyectil = collision.GetComponent<Proyectil>();
                    if (proyectil.tipoDeProyectil == Proyectil.typeProyectil.AtaqueEspecial || proyectil.GetPlayer() == null && proyectil.GetPlayer2() == null)
                        return;

                    if (proyectil.GetPlayer() != null && OnCollisionWhitProyectil != null)
                    {
                        OnCollisionWhitProyectil(this);
                        
                    }
                    else if(OnCollisionWhitProyectil != null)
                    {
                        OnCollisionWhitProyectil(this);
                    }
                }
                
                break;
        }
    }
    public float GetAuxDelayEffect()
    {
        return auxDelayEffect;
    }
}
