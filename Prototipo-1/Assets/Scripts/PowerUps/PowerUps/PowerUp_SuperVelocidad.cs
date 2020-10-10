using System;
using UnityEngine;
public class PowerUp_SuperVelocidad : PowerUp
{
    public static event Action<PowerUp> DisablePowerUp;
    public bool useMegaJump = true;
    public float NewSpeed = 24;
    public float NewSpeedJump;
    public float NewResitance;
    public float NewGravity;
    //private bool settingAuxData = false;
    protected override void Start()
    {
        typePowerUp = TypePowerUp.PowerUpDelay;
        base.Start();
    }
    public void Update()
    {
        if (enableEffect)
        {
            if (delayEffect > 0)
            {
                SetNewSpeed();
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
    }
   
    public void SetNewSpeed()
    {
        if (player != null)
        {
            if (player.transform.position.y <= player.GetInitialPosition().y)
            {
                player.Speed = NewSpeed;

                if (useMegaJump)
                {
                    player.SpeedJump = NewSpeedJump;
                    player.Gravity = NewGravity;
                    player.Resistace = NewResitance;
                }

            }
        }
        else if (enemy != null)
        {
            if (enemy.transform.position.y <= enemy.InitialPosition.y)
            {
                enemy.Speed = NewSpeed;

                if (useMegaJump)
                {
                    enemy.SpeedJump = NewSpeedJump;
                    enemy.Gravity = NewGravity;
                    enemy.Resistace = NewResitance;
                }
            }
        }
    }
    public override void CheckDeadTarget(Enemy e)
    {
        if (enableEffect)
        {
            base.CheckDeadTarget(e);
            if (e == enemy)
                DisableEffect();
        }
    }
    public override void CheckDeadTarget(Player p)
    {
        if (enableEffect)
        {
            base.CheckDeadTarget(p);
            if (p == player)
                DisableEffect();
        }
    }
    public override void DisableEffect()
    {
        DisablePowerUpEffect();
    }
    public void DisablePowerUpEffect()
    {
        if (player != null)
        {
            if (player.transform.position.y <= player.GetInitialPosition().y)
            {
                player.Speed = player.AuxSpeed;
                player.SpeedJump = player.AuxSpeedJump;
                player.Gravity = player.AuxGravity;
                player.Resistace = player.AuxResistace;

                enableEffect = false;
                delayEffect = auxDelayEffect;
                if (DisablePowerUp != null)
                {
                    DisablePowerUp(this);
                }
            }
        }
        else if (enemy != null)
        {
            if (enemy.transform.position.y <= enemy.InitialPosition.y)
            {
                enemy.Speed = enemy.auxSpeed;
                enemy.SpeedJump = enemy.auxSpeedJump;
                enemy.Gravity = enemy.auxGravity;
                enemy.Resistace = enemy.auxResistace;

                enableEffect = false;
                delayEffect = auxDelayEffect;
                if (DisablePowerUp != null)
                {
                    DisablePowerUp(this);
                }
            }
        }
    }
}
