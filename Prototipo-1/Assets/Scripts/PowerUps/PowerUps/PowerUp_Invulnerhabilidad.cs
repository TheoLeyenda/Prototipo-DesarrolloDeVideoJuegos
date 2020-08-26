using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PowerUp_Invulnerhabilidad : PowerUp
{
    private float currentLife;
    public static event Action<PowerUp> DisablePowerUp;

    private void Update()
    {
        if (enableEffect)
        {
            if (delayEffect > 0)
            {
                delayEffect = delayEffect - Time.deltaTime;
                if (player != null)
                {
                    player.PD.lifePlayer = currentLife;
                }
                if (enemy != null)
                {
                    enemy.life = currentLife; 
                }
            }
            else
            {
                if (DisablePowerUp != null)
                {
                    DisablePowerUp(this);
                }
                delayEffect = auxDelayEffect;
                enableEffect = false;
                currentLife = 0;
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
}
