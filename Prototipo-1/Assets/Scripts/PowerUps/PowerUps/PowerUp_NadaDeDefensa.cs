using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PowerUp_NadaDeDefensa : PowerUp
{
    public static event Action<PowerUp> DisablePowerUp;
    public static event Action<PowerUp_NadaDeDefensa> OnSettingsPowerUp;
    public static event Action<PowerUp_NadaDeDefensa> OnDisablePowerUp_NadaDeDefensa;
    private BarraDeEscudo barraDeEscudo;
    private bool setingData = false;
    public bool disableEffectInDeathPlayer = false;
    protected override void Start()
    {
        typePowerUp = TypePowerUp.PowerUpDelay;
        base.Start();
    }
    private void Update()
    {
        if (enableEffect)
        {
            if (setingData)
            {
                setingData = false;
                if(OnSettingsPowerUp != null)
                    OnSettingsPowerUp(this);
            }
            else if (delayEffect > 0)
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
    protected override void OnEnable()
    {
        base.OnEnable();
        if (player != null)
        {
            Player.OnDie += CheckDeadTarget;
        }
        else if (enemy != null)
        {
            Enemy.OnDie += CheckDeadTarget;
        }
    }
    protected override void OnDisable()
    {
        base.OnDisable();
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
        if (enableEffect)
        {
            //Debug.Log("JAJA");
            base.CheckDeadTarget(e);
            DisableEffect();
            if (barraDeEscudo != null)
            {
                //Debug.Log("TREMENDO SETEO DE ESCUDO");
                barraDeEscudo.SetEnableDeffence(true);
                barraDeEscudo.SetValueShild(barraDeEscudo.MaxValueShild);
            }
        }
    }
    public override void CheckDeadTarget(Player p)
    {
        if (enableEffect && disableEffectInDeathPlayer)
        {
            base.CheckDeadTarget(p);
            DisableEffect();
        }
    }
    public override void DisableEffect()
    {
        if (DisablePowerUp != null)
            DisablePowerUp(this);

        DisableEffectPowerUp_NadaDeDefensa();

        if (OnDisablePowerUp_NadaDeDefensa != null)
            OnDisablePowerUp_NadaDeDefensa(this);
        //en el OnDisablePowerUp_NadaDeDefensa(this) hago que enableEffect = false;
        delayEffect = auxDelayEffect;
        
    }
    public void SetBarraDeEscudo(BarraDeEscudo _barraDeEscudo)
    {
        barraDeEscudo = _barraDeEscudo;
    }
    public BarraDeEscudo GetBarraDeEscudo()
    {
        return barraDeEscudo;
    }
    public override void ActivatedPowerUp()
    {
        enableEffect = true;
        setingData = true;
    }
    public void ThrowEffect()
    {
        if (barraDeEscudo != null)
        {
            barraDeEscudo.SetEnableDeffence(false);
            barraDeEscudo.SetValueShild(0);
        }
        //else
            //Debug.Log("barraDeEscudo is null");
    }
    public void DisableEffectPowerUp_NadaDeDefensa()
    {
        if (barraDeEscudo != null)
        {
            barraDeEscudo.SetEnableDeffence(true);
            //barraDeEscudo = null;
        }
        //else
            //Debug.Log("barraDeEscudo is null");
    }

}
