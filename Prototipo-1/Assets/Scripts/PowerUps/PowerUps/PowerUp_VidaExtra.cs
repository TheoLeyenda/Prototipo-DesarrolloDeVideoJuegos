using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PowerUp_VidaExtra : PowerUp, IDHealth<int>
{
    // Start is called before the first frame update
    public int recoverHealth;
    public static event Action<PowerUp> DisablePowerUp;
    protected override void Start()
    {
        typePowerUp = TypePowerUp.PowerUpDisable;
        base.Start();
    }
    public override void ActivatedPowerUp()
    {
        base.ActivatedPowerUp();
        GiveHealth(recoverHealth);
        enableEffect = true;
    }
    public void GiveHealth(int _health)
    {
        if (player != null)
        {
            if (player.PD.lifePlayer < player.PD.maxLifePlayer)
            {
                player.PD.lifePlayer = player.PD.lifePlayer + _health;
                if (player.PD.lifePlayer >= player.PD.maxLifePlayer)
                {
                    player.PD.lifePlayer = player.PD.maxLifePlayer;
                }
            }
        }
        else if (enemy != null)
        {
            if (enemy.life < enemy.maxLife)
            {
                enemy.life = enemy.life + _health;
                if (enemy.life >= enemy.maxLife)
                {
                    enemy.life = enemy.maxLife;
                }
            }
        }
        if (DisablePowerUp != null)
        {
            DisablePowerUp(this);
        }
        enableEffect = false;
    }
}
