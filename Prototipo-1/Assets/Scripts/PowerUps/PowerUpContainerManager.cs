﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpContainerManager : MonoBehaviour
{
    // Start is called before the first frame update
    [System.Serializable]
    public class Container
    {
        public string namePowerUp;
        public PowerUp powerUp;
        public int countPowerUps;
        public int maxCountPowerUps;
        public bool currentPowerUp;
    }
    private bool enableShootPowerUp = true;
    private float delayEnableShootPowerUp = 0.1f;
    private float auxDelayEnableShootPowerUp = 0.1f;
    private bool enableDelay;
    private GameData gameData;
    public List<Container> powerUpContainerContent;
    public ThrowPowerUpController.UserPowerUpController userContainer;
    private GameManager gm;
    private void Awake()
    {
        gameData = GameData.instaceGameData;
        gm = GameManager.instanceGameManager;
    }
    private void OnEnable()
    {
        PowerUp.OnCollisionWhitProyectil += AddCountPowerUp;
        PowerUp.OnCollisionWhitPlayer += AddCountPowerUpWhitGameData;
        PowerUp.OnDisablePowerUpEffect += EnableShootPowerUp;
    }
    private void OnDisable()
    {
        PowerUp.OnCollisionWhitProyectil -= AddCountPowerUp;
        PowerUp.OnCollisionWhitPlayer -= AddCountPowerUpWhitGameData;
        PowerUp.OnDisablePowerUpEffect -= EnableShootPowerUp;
    }
    public void EnableShootPowerUp(PowerUp powerUp)
    {
        if (powerUp.userPowerUp == userContainer)
        {
            enableDelay = true;
        }
    }
    private void Start()
    {
        if (gameData == null) return;
        DeselectAllPowerUps();
        for (int i = 0; i < gameData.dataPlayerPowerUp.Length; i++)
        {
            if (i < powerUpContainerContent.Count && powerUpContainerContent[i].powerUp != null)
            {
                powerUpContainerContent[i].countPowerUps = gameData.dataPlayerPowerUp[i].countPowerUp;
                powerUpContainerContent[i].maxCountPowerUps = gameData.dataPlayerPowerUp[i].maxCountPowerUps;
                powerUpContainerContent[i].powerUp.userPowerUp = userContainer;
            }
            //else
            //{
                //Debug.Log("No entre XD");
            //}
        }
        if (gameData.indexCurrentPowerUp < powerUpContainerContent.Count && gameData.indexCurrentPowerUp >= 0)
        {
            powerUpContainerContent[gameData.indexCurrentPowerUp].currentPowerUp = true;
        }
    }
    private void Update()
    {
        if (enableDelay)
        {
            if (delayEnableShootPowerUp > 0)
            {
                delayEnableShootPowerUp = delayEnableShootPowerUp - Time.deltaTime;
            }
            else
            {
                delayEnableShootPowerUp = auxDelayEnableShootPowerUp;
                enableDelay = false;
                enableShootPowerUp = true;
            }
        }
    }
    public void ThrowPowerUp(int index)
    {
        if (gameData.indexCurrentPowerUp < 0 || gameData.indexCurrentPowerUp >= powerUpContainerContent.Count)
            return;

        if (powerUpContainerContent[index].namePowerUp != "None")
        {

            bool characterEnableMovement = false;

            Player p = powerUpContainerContent[index].powerUp.player;
            Enemy e = powerUpContainerContent[index].powerUp.enemy;
            if (p == null && e == null) return;

            if (p != null)
            {
                characterEnableMovement = (p.enableMovement || p.enumsPlayers.estadoJugador == EnumsPlayers.EstadoJugador.Atrapado);
            }
            else if (e != null)
            {
                characterEnableMovement = (e.enableMovement || e.enumsEnemy.GetStateEnemy() == EnumsEnemy.EstadoEnemigo.Atrapado);
            }

            if (powerUpContainerContent[index].currentPowerUp 
            && powerUpContainerContent[index].countPowerUps > 0 
            && characterEnableMovement && enableShootPowerUp)
            {
                //Debug.Log("Entre al disparo del efecto del powerUp");
                powerUpContainerContent[index].powerUp.ActivatedPowerUp();
                powerUpContainerContent[index].countPowerUps--;
                enableShootPowerUp = false;
                //Debug.Log("POWER UP LANZADO");
                if (gm.enumsGameManager.modoDeJuego == EnumsGameManager.ModosDeJuego.Historia 
                    || gm.enumsGameManager.modoDeJuego == EnumsGameManager.ModosDeJuego.Supervivencia)
                {
                    gameData.dataPlayerPowerUp[index].countPowerUp--;
                }
                if (powerUpContainerContent[index].countPowerUps <= 0)
                {
                    powerUpContainerContent[index].currentPowerUp = false;
                    for (int j = 0; j < powerUpContainerContent.Count; j++)
                    {
                        if (powerUpContainerContent[j].countPowerUps > 0)
                        {
                            Debug.Log("ASIGNO EL NUEVO POWER UP");
                            powerUpContainerContent[j].currentPowerUp = true;
                            gameData.indexCurrentPowerUp = j;
                            j = powerUpContainerContent.Count;
                        }
                    }
                }
            }
        }
        
    }
    
    public void DeselectAllPowerUps()
    {
        for (int i = 0; i < powerUpContainerContent.Count; i++)
        {
            powerUpContainerContent[i].currentPowerUp = false;
        }
    }
    public void AddCountPowerUpWhitGameData(PowerUp powerUp)
    {
        if (powerUp.userPowerUp == userContainer)
        {
            for (int i = 0; i < gameData.dataPlayerPowerUp.Length; i++)
            {
                if (powerUp.namePowerUp == gameData.dataPlayerPowerUp[i].namePowerUp)
                {
                    if (gameData.dataPlayerPowerUp[i].countPowerUp < gameData.dataPlayerPowerUp[i].maxCountPowerUps)
                    {
                        gameData.dataPlayerPowerUp[i].countPowerUp++;
                        powerUpContainerContent[i].countPowerUps++;
                        powerUp.EffectDisablePowerUp();
                    }
                    else
                    {
                        powerUp.EffectDestroyPowerUp();
                    }
                }
            }
        }
    }
    public void AddCountPowerUp(PowerUp powerUp)
    {
        if (powerUp.userPowerUp == userContainer)
        {
            for (int i = 0; i < powerUpContainerContent.Count; i++)
            {
                if (powerUp.namePowerUp == powerUpContainerContent[i].namePowerUp)
                {
                    if (powerUpContainerContent[i].countPowerUps < powerUpContainerContent[i].maxCountPowerUps)
                    {
                        powerUpContainerContent[i].countPowerUps++;
                        powerUp.EffectDisablePowerUp();
                    }
                    else
                    {
                        powerUp.EffectDestroyPowerUp();
                    }

                }
            }
        }
    }
}