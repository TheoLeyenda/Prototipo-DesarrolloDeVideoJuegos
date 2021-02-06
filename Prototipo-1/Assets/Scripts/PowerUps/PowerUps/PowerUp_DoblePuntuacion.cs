using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PowerUp_DoblePuntuacion : PowerUp
{
    private float auxScoreForHit = 0;
    private float auxScoreForKill = 0;
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
            }
            else
            {
                DisableEffect();
            }
        }
    }
    public override void ActivatedPowerUp()
    {
        enableEffect = true;
        if (player != null)
        {
            auxScoreForHit = player.PD.scoreForHit;
            auxScoreForKill = player.PD.scoreForEnemyDead;
            player.PD.scoreForHit = player.PD.scoreForHit * 2;
            player.PD.scoreForEnemyDead = player.PD.scoreForEnemyDead * 2;
        }
    }
    public override void DisableEffect()
    {
        base.DisableEffect();
        if (DisablePowerUp != null)
        {
            DisablePowerUp(this);
        }
        if (player != null)
        {
            player.PD.scoreForHit = auxScoreForHit;
            player.PD.scoreForEnemyDead = auxScoreForKill;
        }
        enableEffect = false;
        delayEffect = auxDelayEffect;
    }
}
