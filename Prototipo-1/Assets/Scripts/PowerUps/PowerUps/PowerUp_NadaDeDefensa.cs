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
                if (DisablePowerUp != null)
                    DisablePowerUp(this);

                DisableEffect();
            }
        }
    }
    public override void CheckDeadTarget(Enemy e)
    {
        if (enableEffect)
        {
            //Debug.Log("JAJA");
            base.CheckDeadTarget(e);
            if (DisablePowerUp != null)
                DisablePowerUp(this);
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
            if (DisablePowerUp != null)
                DisablePowerUp(this);
            DisableEffect();
        }
    }
    public override void DisableEffect()
    {
        DisableEffectPowerUp_NadaDeDefensa();

        if (OnDisablePowerUp_NadaDeDefensa != null)
            OnDisablePowerUp_NadaDeDefensa(this);

        delayEffect = auxDelayEffect;
        enableEffect = false;
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
        else
            Debug.Log("barraDeEscudo is null");
    }
    public void DisableEffectPowerUp_NadaDeDefensa()
    {
        if (barraDeEscudo != null)
        {
            barraDeEscudo.SetEnableDeffence(true);
            //barraDeEscudo = null;
        }
        else
            Debug.Log("barraDeEscudo is null");
    }

}
