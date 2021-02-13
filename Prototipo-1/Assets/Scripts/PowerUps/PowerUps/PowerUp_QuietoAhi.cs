using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PowerUp_QuietoAhi : PowerUp
{
    public static event Action<PowerUp> DisablePowerUp;
    public static event Action<PowerUp_QuietoAhi> OnEffectPowerUp;
    public static event Action<PowerUp_QuietoAhi> OnDisableEffectPowerUp_QuietoAhi;
    public static event Action<PowerUp_QuietoAhi> OnSavingDataPowerUp_QuietoAhi;
    public bool disableAttack = false;
    private bool savingData = true;
    private float auxSpeed;
    private float auxSpeedJump;
    private float auxResistence;
    private float auxGravity;
    private float auxDelayAttack;
    private float delayAttack = 100000;
    protected override void Start()
    {
        typePowerUp = TypePowerUp.PowerUpDelay;
        base.Start();
        enableEffect = false;
    }
    private void Update()
    {
        if (enableEffect)
        {
            if (savingData)
            {
                if (OnSavingDataPowerUp_QuietoAhi != null)
                {
                    savingData = false;
                    OnSavingDataPowerUp_QuietoAhi(this);
                }
            }

            if (delayEffect > 0)
            {
                ThrowEffect();
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
    public void ThrowEffect()
    {
        if (OnEffectPowerUp != null)
        {
            OnEffectPowerUp(this);
        }
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        Enemy.OnDie += CheckDeadTarget;
        Player.OnDie += CheckDeadTarget;
        Enemy.OnAlive += CheckDeadTarget;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        Player.OnDie -= CheckDeadTarget;
        Enemy.OnDie -= CheckDeadTarget;
        Enemy.OnAlive -= CheckDeadTarget;
    }
    public override void CheckDeadTarget(Enemy e)
    {
        if (enableEffect)
        {
            base.CheckDeadTarget(e);
            if (DisablePowerUp != null)
                DisablePowerUp(this);
            DisableEffect();
        }
    }
    public override void CheckDeadTarget(Player p)
    {
        if (enableEffect)
        {
            base.CheckDeadTarget(p);
            if (DisablePowerUp != null)
                DisablePowerUp(this);
            DisableEffect();
        }
    }
    public override void DisableEffect()
    {
        if (DisablePowerUp != null)
        {
            DisablePowerUp(this);
        }
        if (OnDisableEffectPowerUp_QuietoAhi != null)
        {
            OnDisableEffectPowerUp_QuietoAhi(this);
            //Deshabilito la variable enableEffect dentro de OnDisableEffectPowerUp_QuietoAhi
        }
        delayEffect = auxDelayEffect;
        savingData = true;
    }

    public void SetAuxSpeed(float _auxSpeed) => auxSpeed = _auxSpeed;
    public float GetAuxSpeed() { return auxSpeed; }

    public void SetAuxSpeedJump(float _auxSpeedJump) => auxSpeedJump = _auxSpeedJump;
    public float GetAuxSpeedJump() { return auxSpeedJump; }

    public void SetAuxResistence(float _auxResistence) => auxResistence = _auxResistence;
    public float GetAuxResistence(){ return auxResistence;}

    public void SetAuxGravity(float _auxGravity) => auxGravity = _auxGravity;
    public float GetAuxGravity(){ return auxGravity; }

    public void SetAuxDelayAttack(float delay) => auxDelayAttack = delay;
    public float GetAuxDelayAttack(){ return auxDelayAttack; }

    public float GetDelayAttack(){ return delayAttack; }

}
