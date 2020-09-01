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

        PowerUp_DividirPuntuacion.SettingPowerUp_DividirPuntuacion += SavingDataPowerUp_DividirPuntuacion;
        PowerUp_DividirPuntuacion.OnEffectPowerUp += EffectPowerUp_DividirPuntuacion;
        PowerUp_DividirPuntuacion.DisableEffectPowerUp_DividirPuntuacion += DisableEffectPowerUp_DividirPuntuacion;


    }
    private void OnDisable()
    {
        PowerUp_QuietoAhi.OnEffectPowerUp -= EffectPowerUp_QuietoAhi;
        PowerUp_QuietoAhi.OnDisableEffectPowerUp_QuietoAhi -= DisableEffectPowerUp_QuietoAhi;
        PowerUp_QuietoAhi.OnSavingDataPowerUp_QuietoAhi -= SavingDataPowerUp_QuietoAhi;

        PowerUp_NadaDeDefensa.OnSettingsPowerUp -= SettingPowerUp_NadaDeDefensa;
        PowerUp_NadaDeDefensa.OnDisablePowerUp_NadaDeDefensa -= DisableEffectPowerUp_NadaDeDefensa;

        PowerUp_DividirPuntuacion.SettingPowerUp_DividirPuntuacion -= SavingDataPowerUp_DividirPuntuacion;
        PowerUp_DividirPuntuacion.OnEffectPowerUp -= EffectPowerUp_DividirPuntuacion;
        PowerUp_DividirPuntuacion.DisableEffectPowerUp_DividirPuntuacion -= DisableEffectPowerUp_DividirPuntuacion;
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
            Debug.Log("QUIETO");
        }
    }
    public void EffectPowerUp_QuietoAhi(PowerUp_QuietoAhi powerUp_QuietoAhí)
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
            enemyReference.Speed = 0;
            enemyReference.SpeedJump = 0;
            enemyReference.Resistace = 0;
            enemyReference.Gravity = 0;
            if (powerUp_QuietoAhí.disableAttack)
                enemyReference.delayAttack = powerUp_QuietoAhí.GetDelayAttack();
        }
        else if ((powerUp_QuietoAhí.player != null && playerReference != null) ||
                   (powerUp_QuietoAhí.enemy != null && playerReference != null))
        {
            playerReference.Speed = 0;
            playerReference.SpeedJump = 0;
            playerReference.Resistace = 0;
            playerReference.Gravity = 0;
            if (powerUp_QuietoAhí.disableAttack)
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
                    playerReference.delayAttack = powerUp_QuietoAhí.GetAuxDelayAttack();
                    Debug.Log("ENTRE");
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

    public void SavingDataPowerUp_DividirPuntuacion(PowerUp_DividirPuntuacion powerUp_DividirPuntuacion)
    {
        if (!powerUp_DividirPuntuacion.settedPowerUp)
        {
            if (playerReference != null && powerUp_DividirPuntuacion.player != null)
            {
                if (powerUp_DividirPuntuacion.player == playerReference ||
                   powerUp_DividirPuntuacion.player.enumsPlayers.numberPlayer == playerReference.enumsPlayers.numberPlayer)
                    return;
            }
            if ((powerUp_DividirPuntuacion.player != null && playerReference != null) ||
                       (powerUp_DividirPuntuacion.enemy != null && playerReference != null))
            {
                powerUp_DividirPuntuacion.newScoreForHit = playerReference.PD.scoreForHit / 2;
                powerUp_DividirPuntuacion.newScoreForKill = playerReference.PD.scoreForEnemyDead / 2;
                powerUp_DividirPuntuacion.settedPowerUp = true;
                Debug.Log("ENTRE");
            }
        }
    }
    public void EffectPowerUp_DividirPuntuacion(PowerUp_DividirPuntuacion powerUp_DividirPuntuacion)
    {
        if (playerReference != null && powerUp_DividirPuntuacion.player != null)
        {
            if (powerUp_DividirPuntuacion.player == playerReference ||
                powerUp_DividirPuntuacion.player.enumsPlayers.numberPlayer == playerReference.enumsPlayers.numberPlayer)
                return;
        }
        if ((powerUp_DividirPuntuacion.player != null && playerReference != null) ||
            (powerUp_DividirPuntuacion.enemy != null && playerReference != null))
        {
            playerReference.PD.scoreForHit = powerUp_DividirPuntuacion.newScoreForHit;
            playerReference.PD.scoreForEnemyDead = powerUp_DividirPuntuacion.newScoreForKill;
        }
    }
    public void DisableEffectPowerUp_DividirPuntuacion(PowerUp_DividirPuntuacion powerUp_DividirPuntuacion)
    {
        if (powerUp_DividirPuntuacion.enableEffect)
        {
            //Debug.Log("HIJO DE REMIL PUTA");
            if (playerReference != null && powerUp_DividirPuntuacion.player != null)
            {
                if (powerUp_DividirPuntuacion.player == playerReference ||
                powerUp_DividirPuntuacion.player.enumsPlayers.numberPlayer == playerReference.enumsPlayers.numberPlayer)
                    return;
            }
            if ((powerUp_DividirPuntuacion.player != null && playerReference != null) ||
                 (powerUp_DividirPuntuacion.enemy != null && playerReference != null))
            {
                playerReference.PD.ResetScoreValue();
                powerUp_DividirPuntuacion.enableEffect = false;
            }
            
        }
    }

}
