using System;
using UnityEngine;
public class PowerUp_SuperVelocidad : PowerUp
{
    public static event Action<PowerUp> DisablePowerUp;
    public bool useMegaJump = true;
    public float NewSpeed = 24;
    private float auxSpeed;
    public float NewSpeedJump;
    private float auxSpeedJump;
    public float NewResitance;
    private float auxResitance;
    public float NewGravity;
    private float auxGravity;
    //private bool settingAuxData = false;
    protected override void Start()
    {
        typePowerUp = TypePowerUp.PowerUpDelay;
        base.Start();
        SetAuxSpeed();
    }
    private void OnEnable()
    {
        SetAuxSpeed();
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
    public void SetAuxSpeed()
    {
        if (player != null)
        {
            auxSpeed = player.Speed;
            if (useMegaJump)
            {
                auxSpeedJump = player.SpeedJump;
                auxGravity = player.Gravity;
                auxResitance = player.Resistace;
            }
        }
        else if (enemy != null)
        {
            auxSpeed = enemy.Speed;
            if (useMegaJump)
            {
                auxSpeedJump = enemy.SpeedJump;
                auxGravity = enemy.Gravity;
                auxResitance = enemy.Resistace;
            }
        }
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
            if (enemy.transform.position.y <= enemy.GetInitialPosition().y)
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
                player.Speed = auxSpeed;
                player.SpeedJump = auxSpeedJump;
                player.Gravity = auxGravity;
                player.Resistace = auxResitance;

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
            if (enemy.transform.position.y <= enemy.GetInitialPosition().y)
            {
                enemy.Speed = auxSpeed;
                enemy.SpeedJump = auxSpeedJump;
                enemy.Gravity = auxGravity;
                enemy.Resistace = auxResitance;

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
