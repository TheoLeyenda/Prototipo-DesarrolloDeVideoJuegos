using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorOfPowerUpContainer : MonoBehaviour
{
    public bool useGameData = true;
    [HideInInspector]
    public int indexPowerUpContainerContent = 0;
    public PowerUpContainerManager powerUpContainerManager;
    private GameData gameData;
    public SelectSpriteListPowerUp selectSpriteListForImage;
    private void Awake()
    {
        gameData = GameData.instaceGameData;
        gameData.indexCurrentPowerUp = 0;
        CheckEnablePowerUp();
    }
    public void Start()
    {
        if (useGameData)
        {
            for (int i = 0; i < gameData.dataPlayerPowerUp.Length; i++)
            {
                if (i < powerUpContainerManager.powerUpContainerContent.Count && powerUpContainerManager.powerUpContainerContent[i].powerUp != null)
                {
                    powerUpContainerManager.powerUpContainerContent[i].countPowerUps = gameData.dataPlayerPowerUp[i].countPowerUp;
                }
            }
        }
        CheckEnablePowerUp();
    }
    public void NextPowerUp()
    {
        if (indexPowerUpContainerContent < powerUpContainerManager.powerUpContainerContent.Count - 1)
            indexPowerUpContainerContent++;
        CheckEnablePowerUp();
    }
    public void PrevPowerUp()
    {
        if (indexPowerUpContainerContent > 0)
            indexPowerUpContainerContent--;
        CheckEnablePowerUp();
    }
    public void CheckEnablePowerUp()
    {
        if (powerUpContainerManager.powerUpContainerContent[indexPowerUpContainerContent].countPowerUps <= 0 && indexPowerUpContainerContent < powerUpContainerManager.powerUpContainerContent.Count - 1)
        {
            selectSpriteListForImage.image.color = selectSpriteListForImage.disableImageColor;
        }
        else
        {
            selectSpriteListForImage.image.color = Color.white;
        }
    }
    public void SelectPowerUp()
    {
        if (gameData.dataPlayerPowerUp.Length <= 0) return;

        gameData.indexCurrentPowerUp = indexPowerUpContainerContent;
        if (powerUpContainerManager.powerUpContainerContent[indexPowerUpContainerContent].countPowerUps <= 0)
        {
            bool asignedPowerUp = false;
            for (int i = 0; i < powerUpContainerManager.powerUpContainerContent.Count; i++)
            {
                if (powerUpContainerManager.powerUpContainerContent[i].countPowerUps > 0)
                {
                    gameData.indexCurrentPowerUp = i;
                    asignedPowerUp = true;
                }
            }
            if (!asignedPowerUp)
            {
                gameData.indexCurrentPowerUp = powerUpContainerManager.powerUpContainerContent.Count - 1;
            }
        }
    }
    
}
