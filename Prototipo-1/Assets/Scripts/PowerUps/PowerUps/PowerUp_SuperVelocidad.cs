using System;
using UnityEngine;
public class PowerUp_SuperVelocidad : PowerUp
{
    public static event Action<PowerUp> DisablePowerUp;
    public bool useMegaJump = true;
    public float NewSpeed = 24;
    private float auxSpeed;
    private bool enableAddStats = true;
    public float NewSpeedJump;
    private float auxSpeedJump;
    public float NewResitance;
    private float auxResitance;
    public float NewGravity;
    private float auxGravity;
    private bool enableEffect = false;

    private void OnEnable()
    {
        if (player != null)
        {
            auxSpeed = player.Speed;
            auxSpeedJump = player.SpeedJump;
            auxGravity = player.Gravity;
            auxResitance = player.Resistace;
        }
        if (enemy != null)
        {
            auxSpeed = enemy.Speed;
            auxSpeedJump = enemy.SpeedJump;
            auxGravity = enemy.Gravity;
            auxResitance = enemy.Resistace;
        }
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
                DisablePowerUpEffect();
            }
        }
    }
    public override void ActivatedPowerUp()
    {
        enableEffect = true;
    }
    public void SetNewSpeed()
    {
        if (!enableAddStats) return;

        if (player != null)
        {
            if (player.transform.position.y <= player.GetInitialPosition().y)
            {
                auxSpeed = player.Speed;
                player.Speed = NewSpeed;

                if (useMegaJump)
                {
                    auxSpeedJump = player.SpeedJump;
                    auxGravity = player.Gravity;
                    auxResitance = player.Resistace;

                    player.SpeedJump = NewSpeedJump;
                    player.Gravity = NewGravity;
                    player.Resistace = NewResitance;
                }
                enableAddStats = false;
            }
        }
        else if (enemy != null)
        {
            if (enemy.transform.position.y <= enemy.InitialPosition.y)
            {
                auxSpeed = enemy.Speed;
                enemy.Speed = NewSpeed;

                if (useMegaJump)
                {
                    auxSpeedJump = enemy.SpeedJump;
                    auxGravity = enemy.Gravity;
                    auxResitance = enemy.Resistace;

                    enemy.SpeedJump = NewSpeedJump;
                    enemy.Gravity = NewGravity;
                    enemy.Resistace = NewResitance;
                }
                enableAddStats = false;
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
        if (enableAddStats) return;

        if (player != null)
        {
            if (player.transform.position.y <= player.GetInitialPosition().y)
            {
                player.Speed = auxSpeed;
                player.SpeedJump = auxSpeedJump;
                player.Gravity = auxGravity;
                player.Resistace = auxResitance;

                enableEffect = false;
                enableAddStats = true;
                delayEffect = auxDelayEffect;
            }
        }
        else if (enemy != null)
        {
            if (enemy.transform.position.y <= enemy.InitialPosition.y)
            {
                enemy.Speed = auxSpeed;
                enemy.SpeedJump = auxSpeedJump;
                enemy.Gravity = auxGravity;
                enemy.Resistace = auxResitance;

                enableEffect = false;
                enableAddStats = true;
                delayEffect = auxDelayEffect;
            }
        }

        if (DisablePowerUp != null)
        {
            DisablePowerUp(this);
        }
    }
}
