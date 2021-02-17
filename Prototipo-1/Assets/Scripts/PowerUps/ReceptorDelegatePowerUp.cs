using UnityEngine;

public class ReceptorDelegatePowerUp : MonoBehaviour
{
    public Player playerReference;
    public Enemy enemyReference;
    [SerializeField] private bool inBoss;
    private void OnEnable()
    {
        if (!inBoss)
        {
            PowerUp_QuietoAhi.OnEffectPowerUp += EffectPowerUp_QuietoAhi;
            PowerUp_QuietoAhi.OnDisableEffectPowerUp_QuietoAhi += DisableEffectPowerUp_QuietoAhi;
            PowerUp_QuietoAhi.OnSavingDataPowerUp_QuietoAhi += SavingDataPowerUp_QuietoAhi;

            PowerUp_DividirPuntuacion.SettingPowerUp_DividirPuntuacion += SavingDataPowerUp_DividirPuntuacion;
            PowerUp_DividirPuntuacion.OnEffectPowerUp += EffectPowerUp_DividirPuntuacion;
            PowerUp_DividirPuntuacion.DisableEffectPowerUp_DividirPuntuacion += DisableEffectPowerUp_DividirPuntuacion;
        }

        PowerUp_NadaDeDefensa.OnSettingsPowerUp += SettingPowerUp_NadaDeDefensa;
        PowerUp_NadaDeDefensa.OnDisablePowerUp_NadaDeDefensa += DisableEffectPowerUp_NadaDeDefensa;

    }
    private void OnDisable()
    {
        if (!inBoss)
        {
            PowerUp_QuietoAhi.OnEffectPowerUp -= EffectPowerUp_QuietoAhi;
            PowerUp_QuietoAhi.OnDisableEffectPowerUp_QuietoAhi -= DisableEffectPowerUp_QuietoAhi;
            PowerUp_QuietoAhi.OnSavingDataPowerUp_QuietoAhi -= SavingDataPowerUp_QuietoAhi;

            PowerUp_DividirPuntuacion.SettingPowerUp_DividirPuntuacion -= SavingDataPowerUp_DividirPuntuacion;
            PowerUp_DividirPuntuacion.OnEffectPowerUp -= EffectPowerUp_DividirPuntuacion;
            PowerUp_DividirPuntuacion.DisableEffectPowerUp_DividirPuntuacion -= DisableEffectPowerUp_DividirPuntuacion;
        }

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
            powerUp_QuietoAhí.SetAuxSpeed(enemyReference.auxSpeed);
            powerUp_QuietoAhí.SetAuxSpeedJump(enemyReference.auxSpeedJump);
            powerUp_QuietoAhí.SetAuxGravity(enemyReference.auxGravity);
            powerUp_QuietoAhí.SetAuxResistence(enemyReference.auxResistace);

            if (powerUp_QuietoAhí.disableAttack)
            {
                powerUp_QuietoAhí.SetAuxDelayAttack(enemyReference.delayAttack);
            }
        }
        else if ((powerUp_QuietoAhí.player != null && playerReference != null) ||
                   (powerUp_QuietoAhí.enemy != null && playerReference != null))
        {
            powerUp_QuietoAhí.SetAuxSpeed(playerReference.AuxSpeed);
            powerUp_QuietoAhí.SetAuxSpeedJump(playerReference.AuxSpeedJump);
            powerUp_QuietoAhí.SetAuxGravity(playerReference.AuxGravity);
            powerUp_QuietoAhí.SetAuxResistence(playerReference.AuxResistace);
            if (powerUp_QuietoAhí.disableAttack)
            {
                powerUp_QuietoAhí.SetAuxDelayAttack(playerReference.delayAttack);
                playerReference.delayAttack = powerUp_QuietoAhí.GetDelayAttack();
            }
        }
    }
    public void EffectPowerUp_QuietoAhi(PowerUp_QuietoAhi powerUp_QuietoAhí)
    {
        if (playerReference != null && powerUp_QuietoAhí.player != null)
        {
            if (powerUp_QuietoAhí.player == playerReference ||
                powerUp_QuietoAhí.player.enumsPlayers.numberPlayer == playerReference.enumsPlayers.numberPlayer)
                return;
        }

        if (powerUp_QuietoAhí.player != null && enemyReference != null)
        {
            if (enemyReference.GetInCombatPosition())
            {
                enemyReference.Speed = 0;
                enemyReference.SpeedJump = 0;
                enemyReference.Resistace = 0;
                enemyReference.Gravity = 0;
                enemyReference.delayAttack = 99999;
                
                if (enemyReference.enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AgacharseAtaque
                    || enemyReference.enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AgacheDefensa
                    || enemyReference.enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.AtaqueEspecialAgachado
                    || enemyReference.enumsEnemy.GetMovement() == EnumsEnemy.Movimiento.Agacharse)
                {
                    enemyReference.enumsEnemy.SetMovement(EnumsEnemy.Movimiento.Agacharse);
                }
                else
                {
                    enemyReference.enumsEnemy.SetMovement(EnumsEnemy.Movimiento.Nulo);
                }
            }
        }
        else if ((powerUp_QuietoAhí.player != null && playerReference != null) ||
                   (powerUp_QuietoAhí.enemy != null && playerReference != null))
        {
            playerReference.Speed = 0;
            playerReference.SpeedJump = 0;
            playerReference.Resistace = 0;
            playerReference.Gravity = 0;
            playerReference.enableMovement = false;
            playerReference.delayAttack = 99999;
            playerReference.enumsPlayers.estadoJugador = EnumsPlayers.EstadoJugador.Atrapado;
            playerReference.timeStuned = 99999;
        }
    }
    public void DisableEffectPowerUp_QuietoAhi(PowerUp_QuietoAhi powerUp_QuietoAhí)
    {
        if (powerUp_QuietoAhí.enableEffect)
        {
            if (playerReference != null && powerUp_QuietoAhí.player != null)
            {
                if (powerUp_QuietoAhí.player == playerReference ||
                    powerUp_QuietoAhí.player.enumsPlayers.numberPlayer == playerReference.enumsPlayers.numberPlayer)
                    return;
            }

            if (powerUp_QuietoAhí.player != null && enemyReference != null)
            {
                enemyReference.delayAttack = powerUp_QuietoAhí.GetAuxDelayAttack();
                enemyReference.Speed = enemyReference.auxSpeed;
                enemyReference.SpeedJump = enemyReference.auxSpeedJump;
                enemyReference.Resistace = enemyReference.auxResistace;
                enemyReference.Gravity = enemyReference.auxGravity;
            }
            else if ((powerUp_QuietoAhí.player != null && playerReference != null) ||
                 (powerUp_QuietoAhí.enemy != null && playerReference != null))
            {
                
                playerReference.delayAttack = powerUp_QuietoAhí.GetAuxDelayAttack();
                playerReference.Speed = playerReference.AuxSpeed;
                playerReference.SpeedJump = playerReference.AuxSpeedJump;
                playerReference.Resistace = playerReference.AuxResistace;
                playerReference.Gravity = playerReference.AuxGravity;
                playerReference.enableMovement = true;

                playerReference.timeStuned = 0;

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
            if (enemyReference.GetInCombatPosition() || inBoss)
            {
                powerUp_NadaDeDefensa.SetBarraDeEscudo(enemyReference.barraDeEscudo);
                enemyReference.enableDeffence = false;
            }
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
