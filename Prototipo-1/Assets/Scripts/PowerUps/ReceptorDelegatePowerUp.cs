using UnityEngine;

public class ReceptorDelegatePowerUp : MonoBehaviour
{
    public Player playerReference;
    public Enemy enemyReference;

    private void OnEnable()
    {
        PowerUp_QuietoAhi.OnEffectPowerUp += EffectPowerUp_QuietoAhi;
        PowerUp_QuietoAhi.OnDisableEffectPowerUp_QuietoAhi += DisableEffectPowerUp_QuietoAhi;
        PowerUp_QuietoAhi.OnSavingDataPowerUp_QuietoAhi += SavingDataPowerUp_QuietoAhi;

        PowerUp_NadaDeDefensa.OnSettingsPowerUp += SettingPowerUp_NadaDeDefensa;
        PowerUp_NadaDeDefensa.OnDisablePowerUp_NadaDeDefensa += DisableEffectPowerUp_NadaDeDefensa;
    }
    private void OnDisable()
    {
        PowerUp_QuietoAhi.OnEffectPowerUp -= EffectPowerUp_QuietoAhi;
        PowerUp_QuietoAhi.OnDisableEffectPowerUp_QuietoAhi -= DisableEffectPowerUp_QuietoAhi;
        PowerUp_QuietoAhi.OnSavingDataPowerUp_QuietoAhi -= SavingDataPowerUp_QuietoAhi;

        PowerUp_NadaDeDefensa.OnSettingsPowerUp -= SettingPowerUp_NadaDeDefensa;
        PowerUp_NadaDeDefensa.OnDisablePowerUp_NadaDeDefensa -= DisableEffectPowerUp_NadaDeDefensa;
    }
    public void SavingDataPowerUp_QuietoAhi(PowerUp_QuietoAhi powerUp_QuietoAhí)
    {
        if (playerReference != null && powerUp_QuietoAhí.player != null)
        {
            if (powerUp_QuietoAhí.player == playerReference ||
                powerUp_QuietoAhí.player.enumsPlayers.numberPlayer == playerReference.enumsPlayers.numberPlayer)
                return;
        }
        if (powerUp_QuietoAhí.player != null && enemyReference != null)
        {
            powerUp_QuietoAhí.SetAuxSpeed(enemyReference.Speed);
            powerUp_QuietoAhí.SetAuxSpeedJump(enemyReference.SpeedJump);
            powerUp_QuietoAhí.SetAuxGravity(enemyReference.Gravity);
            powerUp_QuietoAhí.SetAuxResistence(enemyReference.Resistace);

            if (powerUp_QuietoAhí.disableAttack)
            {
                powerUp_QuietoAhí.SetAuxDelayAttack(enemyReference.delayAttack);
            }
        }
        else if ((powerUp_QuietoAhí.player != null && playerReference != null) ||
                   (powerUp_QuietoAhí.enemy != null && playerReference != null))
        {
            powerUp_QuietoAhí.SetAuxSpeed(playerReference.Speed);
            powerUp_QuietoAhí.SetAuxSpeedJump(playerReference.SpeedJump);
            powerUp_QuietoAhí.SetAuxGravity(playerReference.Gravity);
            powerUp_QuietoAhí.SetAuxResistence(playerReference.Resistace);
            if (powerUp_QuietoAhí.disableAttack)
            {
                powerUp_QuietoAhí.SetAuxDelayAttack(playerReference.delayAttack);
                playerReference.delayAttack = powerUp_QuietoAhí.GetDelayAttack();
            }
        }
    }
    public void EffectPowerUp_QuietoAhi(PowerUp_QuietoAhi powerUp_QuietoAhí)
    {
        //Debug.Log("HIJO DE REMIL PUTA");
        if (playerReference != null)
        {
            if (powerUp_QuietoAhí.player == playerReference ||
                powerUp_QuietoAhí.player.enumsPlayers.numberPlayer == playerReference.enumsPlayers.numberPlayer)
                return;
        }

        if (powerUp_QuietoAhí.player != null && enemyReference != null)
        {
            enemyReference.Speed = 0;
            enemyReference.SpeedJump = 0;
            enemyReference.Resistace = 0;
            enemyReference.Gravity = 0;
            enemyReference.delayAttack = powerUp_QuietoAhí.GetDelayAttack();
        }
        else if ((powerUp_QuietoAhí.player != null && playerReference != null) ||
                   (powerUp_QuietoAhí.enemy != null && playerReference != null))
        {
            playerReference.Speed = 0;
            playerReference.SpeedJump = 0;
            playerReference.Resistace = 0;
            playerReference.Gravity = 0;
            playerReference.delayAttack = powerUp_QuietoAhí.GetDelayAttack();
        }
    }
    public void DisableEffectPowerUp_QuietoAhi(PowerUp_QuietoAhi powerUp_QuietoAhí)
    {
        if (powerUp_QuietoAhí.enableEffect)
        {
            //Debug.Log("HIJO DE REMIL PUTA");
            if (playerReference != null && powerUp_QuietoAhí.player != null)
            {
                if (powerUp_QuietoAhí.player == playerReference ||
                    powerUp_QuietoAhí.player.enumsPlayers.numberPlayer == playerReference.enumsPlayers.numberPlayer)
                    return;
            }

            if (powerUp_QuietoAhí.player != null && enemyReference != null)
            {
                if (powerUp_QuietoAhí.disableAttack)
                {
                    enemyReference.delayAttack = powerUp_QuietoAhí.GetAuxDelayAttack();
                }
                enemyReference.Speed = powerUp_QuietoAhí.GetAuxSpeed();
                enemyReference.SpeedJump = powerUp_QuietoAhí.GetAuxSpeedJump();
                enemyReference.Resistace = powerUp_QuietoAhí.GetAuxResistence();
                enemyReference.Gravity = powerUp_QuietoAhí.GetAuxGravity();
            }
            else if ((powerUp_QuietoAhí.player != null && playerReference != null) ||
                 (powerUp_QuietoAhí.enemy != null && playerReference != null))
            {

                if (powerUp_QuietoAhí.disableAttack)
                {
                    playerReference.delayAttack = powerUp_QuietoAhí.GetDelayAttack();
                }

                playerReference.Speed = powerUp_QuietoAhí.GetAuxSpeed();
                playerReference.SpeedJump = powerUp_QuietoAhí.GetAuxSpeedJump();
                playerReference.Resistace = powerUp_QuietoAhí.GetAuxResistence();
                playerReference.Gravity = powerUp_QuietoAhí.GetAuxGravity();

            }
            powerUp_QuietoAhí.enableEffect = false;
        }
        
    }

    public void SettingPowerUp_NadaDeDefensa(PowerUp_NadaDeDefensa powerUp_NadaDeDefensa)
    {
        if (playerReference != null && powerUp_NadaDeDefensa.player != null)
        {
            if (powerUp_NadaDeDefensa.player == playerReference ||
                powerUp_NadaDeDefensa.player.enumsPlayers.numberPlayer == playerReference.enumsPlayers.numberPlayer)
                return;
        }
        if (powerUp_NadaDeDefensa.player != null && enemyReference != null)
        {
            powerUp_NadaDeDefensa.SetBarraDeEscudo(enemyReference.barraDeEscudo);
            enemyReference.enableDeffence = false;
        }
        else if ((powerUp_NadaDeDefensa.player != null && playerReference != null) ||
                   (powerUp_NadaDeDefensa.enemy != null && playerReference != null))
        {
            powerUp_NadaDeDefensa.SetBarraDeEscudo(playerReference.barraDeEscudo);
        }
    }
    public void DisableEffectPowerUp_NadaDeDefensa(PowerUp_NadaDeDefensa powerUp_NadaDeDefensa)
    {
        if (enemyReference == null || !powerUp_NadaDeDefensa.enableEffect)
            return;

        enemyReference.enableDeffence = true;
        powerUp_NadaDeDefensa.enableEffect = false;
    }

}
