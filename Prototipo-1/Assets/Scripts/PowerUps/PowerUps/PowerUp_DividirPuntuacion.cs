using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PowerUp_DividirPuntuacion : PowerUp
{
    [HideInInspector]
    public float newScoreForHit = 0;
    [HideInInspector]
    public float newScoreForKill = 0;
    public static event Action<PowerUp> DisablePowerUp;
    public static event Action<PowerUp_DividirPuntuacion> SettingPowerUp_DividirPuntuacion;
    public static event Action<PowerUp_DividirPuntuacion> OnEffectPowerUp;
    public static event Action<PowerUp_DividirPuntuacion> DisableEffectPowerUp_DividirPuntuacion;
    [HideInInspector]
    public bool settedPowerUp = false;
    protected override void Start()
    {
        typePowerUp = TypePowerUp.PowerUpDelay;
        base.Start();
    }
    private void Update()
    {
        if (enableEffect)
        {
            if (!settedPowerUp)
            {
                if(SettingPowerUp_DividirPuntuacion != null)
                    SettingPowerUp_DividirPuntuacion(this);
            }
            if (delayEffect > 0)
            {
                delayEffect = delayEffect - Time.deltaTime;
                if (OnEffectPowerUp != null)
                {
                    OnEffectPowerUp(this);
                }
            }
            else
            {
                if (DisableEffectPowerUp_DividirPuntuacion != null)
                {
                    DisableEffectPowerUp_DividirPuntuacion(this);
                }
                DisableEffect();
            }
        }
    }
    private void OnEnable()
    {
        if (player != null)
        {
            Player.OnDie += CheckDeadTarget;
        }
        else if (enemy != null) 
        {
            Enemy.OnDie += CheckDeadTarget;
        }
    }
    private void OnDisable()
    {
        if (player != null)
        {
            Player.OnDie -= CheckDeadTarget;
        }
        else if (enemy != null)
        {
            Enemy.OnDie -= CheckDeadTarget;
        }
    }
    public override void CheckDeadTarget(Enemy e) 
    {
        if (e == enemy) 
        {
            base.CheckDeadTarget(e);
            if (DisableEffectPowerUp_DividirPuntuacion != null)
            {
                DisableEffectPowerUp_DividirPuntuacion(this);
            }
            DisableEffect();
        }
    }
    public override void CheckDeadTarget(Player p)
    {
        if (p == player)
        {
            base.CheckDeadTarget(p);
            if (DisableEffectPowerUp_DividirPuntuacion != null)
            {
                DisableEffectPowerUp_DividirPuntuacion(this);
            }
            DisableEffect();
        }
    }
    public override void ActivatedPowerUp()
    {
        enableEffect = true;
    }
    public override void DisableEffect()
    {
        settedPowerUp = false;
    }
}
