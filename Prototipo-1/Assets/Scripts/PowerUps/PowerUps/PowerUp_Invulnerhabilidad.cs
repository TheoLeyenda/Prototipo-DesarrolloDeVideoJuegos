using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PowerUp_Invulnerhabilidad : PowerUp
{
    private float currentLife;
    public static event Action<PowerUp> DisablePowerUp;
    protected override void Start()
    {
        typePowerUp = TypePowerUp.PowerUpDelay;
        base.Start();
    }
    private void Update()
    {
        if (enableEffect)
        {
            if (delayEffect > 0)
            {
                delayEffect = delayEffect - Time.deltaTime;
                if (player != null)
                {
                    if (player.PD.lifePlayer > 0)
                    {
                        player.PD.lifePlayer = currentLife;
                    }
                    else 
                    {
                        DisableEffect();
                    }
                }
                if (enemy != null)
                {
                    if (enemy.life > 0)
                    {
                        enemy.life = currentLife;
                    }
                    else 
                    {
                        DisableEffect();
                    }
                }
            }
            else
            {
                DisableEffect();
            }
        }
    }

    public override void ActivatedPowerUp()
    {
        if (player != null)
        {
            currentLife = player.PD.lifePlayer;
        }
        if (enemy != null)
        {
            currentLife = enemy.life;
        }
        enableEffect = true;
    }
    public override void DisableEffect()
    {
        base.DisableEffect();
        if (DisablePowerUp != null)
        {
            DisablePowerUp(this);
        }
        delayEffect = auxDelayEffect;
        enableEffect = false;
        currentLife = 0;
    }
}
