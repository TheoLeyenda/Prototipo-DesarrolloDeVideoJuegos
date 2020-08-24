using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PowerUp_Blindaje : PowerUp
{
    public static event Action<PowerUp> DisablePowerUp;
    public static event Action<PowerUp_Blindaje> OnEffectPowerUp_Blindaje;
    public float valueShild = 500;
    public bool enablePowerUp = false;
    private float auxLife;
    public override void ActivatedPowerUp()
    {
        enablePowerUp = true;
        if (player != null)
        {
            player.PD.Blindaje = valueShild;
            player.PD.MaxBlindaje = valueShild;
            auxLife = player.PD.lifePlayer;
        }
        else if (enemy != null)
        {
            enemy.Blindaje = valueShild;
            enemy.MaxBlindaje = valueShild;
            auxLife = enemy.life;
        }
        if (OnEffectPowerUp_Blindaje != null)
        {
            OnEffectPowerUp_Blindaje(this);
        }
    }
    private void Update()
    {
        if (enablePowerUp)
        {
            if (player != null)
            {
                player.PD.lifePlayer = auxLife;
                if (player.PD.Blindaje <= 0)
                {
                    player.PD.Blindaje = 0;
                    if (DisablePowerUp != null)
                    {
                        DisablePowerUp(this);
                    }
                    enablePowerUp = false;
                }
            }
            else if (enemy != null)
            {
                enemy.life = auxLife;
                if (enemy.Blindaje <= 0)
                {
                    enemy.Blindaje = 0;
                    if (DisablePowerUp != null)
                    {
                        DisablePowerUp(this);
                    }
                    enablePowerUp = false;
                }
            }
        }
    }
}
