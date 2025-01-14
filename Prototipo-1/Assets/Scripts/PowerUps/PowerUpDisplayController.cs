﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PowerUpDisplayController : MonoBehaviour
{
    public List<Sprite> spritesPowerUps;
    public PowerUpContainerManager powerUpContainerManager;
    private Scrollbar scrollbarPowerUp;
    private Image imageCurrentPowerUp;
    private TextMeshProUGUI textCountPowerUp;
    private UI_Manager ui_Manager;
    public string InputThrowPowerUp;
    private bool inNextPowerUp;
    private bool enableUpdateData = false;
    private float minSizeScrollbarPowerUp = 0.005f;
    private void Awake()
    {
        ui_Manager = UI_Manager.instanceUI_Manager;
    }

    private void OnEnable()
    {
        PowerUpContainerManager.OnRefreshDataPowerUpUI += UpdatePowerDataDisplay;

        switch (powerUpContainerManager.userContainer)
        {
            case ThrowPowerUpController.UserPowerUpController.Player1:
                scrollbarPowerUp = ui_Manager.PlayerIzquierdaHUD.scrollbarPowerUp;
                imageCurrentPowerUp = ui_Manager.PlayerIzquierdaHUD.imageCurrentPowerUp;
                textCountPowerUp = ui_Manager.PlayerIzquierdaHUD.textCountPowerUp;
                break;
            case ThrowPowerUpController.UserPowerUpController.Player2:
                scrollbarPowerUp = ui_Manager.PlayerDerechaHUD.scrollbarPowerUp;
                imageCurrentPowerUp = ui_Manager.PlayerDerechaHUD.imageCurrentPowerUp;
                textCountPowerUp = ui_Manager.PlayerDerechaHUD.textCountPowerUp;
                break;
        }
        UpdatePowerDataDisplay(powerUpContainerManager);
        scrollbarPowerUp.size = 0;
    }
    private void OnDisable()
    {
        PowerUpContainerManager.OnRefreshDataPowerUpUI -= UpdatePowerDataDisplay;
    }
    private void Start()
    {
        UpdatePowerDataDisplay(powerUpContainerManager);
        scrollbarPowerUp.size = 0;
    }
    
    public void UpdatePowerDataDisplay(PowerUpContainerManager _powerUpContainerManager)
    {
        if (_powerUpContainerManager == powerUpContainerManager)
        {
            if (powerUpContainerManager.emptyPowerUps 
                && powerUpContainerManager.powerUpContainerContent[powerUpContainerManager.prevIndex].countPowerUps <= 0
                && !powerUpContainerManager.prevPowerUp.enableEffect)
            {
                imageCurrentPowerUp.sprite = spritesPowerUps[spritesPowerUps.Count - 1];
                textCountPowerUp.text = " ";
                powerUpContainerManager.prevIndex = powerUpContainerManager.powerUpContainerContent.Count - 1;
                return;
            }
            if (powerUpContainerManager.prevPowerUp != null)
            {
                if (!powerUpContainerManager.prevPowerUp.enableEffect && powerUpContainerManager.powerUpContainerContent[powerUpContainerManager.prevIndex].countPowerUps <= 0)
                {
                    imageCurrentPowerUp.sprite = spritesPowerUps[powerUpContainerManager.currentIndexPowerUp];
                    textCountPowerUp.text = "" + powerUpContainerManager.powerUpContainerContent[powerUpContainerManager.currentIndexPowerUp].countPowerUps;
                }
                else
                {
                    imageCurrentPowerUp.sprite = spritesPowerUps[powerUpContainerManager.prevIndex];
                    textCountPowerUp.text = "" + powerUpContainerManager.powerUpContainerContent[powerUpContainerManager.prevIndex].countPowerUps;
                }
            }
            else
            {
                imageCurrentPowerUp.sprite = spritesPowerUps[powerUpContainerManager.currentIndexPowerUp];
                textCountPowerUp.text = "" + powerUpContainerManager.powerUpContainerContent[powerUpContainerManager.currentIndexPowerUp].countPowerUps;
            }
        }
    }
    
    public void UpdatePowerUpScrollbarDisplay()
    {
        if (powerUpContainerManager.prevPowerUp != null)
        {
            if (powerUpContainerManager.currentIndexPowerUp >= powerUpContainerManager.powerUpContainerContent.Count - 1 && !powerUpContainerManager.prevPowerUp.enableEffect)
            {
                scrollbarPowerUp.size = 0;
                return;
            }
        }
        PowerUp currentPowerUp = powerUpContainerManager.powerUpContainerContent[powerUpContainerManager.currentIndexPowerUp].powerUp;

        if (currentPowerUp != null)
        {
            if (currentPowerUp.enableEffect && currentPowerUp.delayEffect > 0)
            {
                float value = currentPowerUp.delayEffect;
                float maxValue = currentPowerUp.GetAuxDelayEffect();

                scrollbarPowerUp.size = value / maxValue;
                if (scrollbarPowerUp.size <= minSizeScrollbarPowerUp)
                {
                    currentPowerUp.DisableEffect();
                    scrollbarPowerUp.size = 0;
                    UpdatePowerDataDisplay(powerUpContainerManager);
                    enableUpdateData = true;
                }
            }
            else
            {
                scrollbarPowerUp.size = 0;
            }
        }
        if (powerUpContainerManager.prevPowerUp != null)
        {
            if (powerUpContainerManager.prevPowerUp.enableEffect && powerUpContainerManager.prevPowerUp.delayEffect > 0)
            {
                float value = powerUpContainerManager.prevPowerUp.delayEffect;
                float maxValue = powerUpContainerManager.prevPowerUp.GetAuxDelayEffect();

                scrollbarPowerUp.size = value / maxValue;
                if (scrollbarPowerUp.size <= minSizeScrollbarPowerUp)
                {
                    powerUpContainerManager.prevPowerUp.DisableEffect();
                    scrollbarPowerUp.size = 0;
                    UpdatePowerDataDisplay(powerUpContainerManager);
                    enableUpdateData = true;
                }
            }
            else
            {
                scrollbarPowerUp.size = 0;
            }
        }

        if (powerUpContainerManager.prevPowerUp != null && currentPowerUp != null && enableUpdateData)
            if (!currentPowerUp.enableEffect && !powerUpContainerManager.prevPowerUp.enableEffect)
            {
                UpdatePowerDataDisplay(powerUpContainerManager);
                enableUpdateData = false;
            }
    }

    private void Update()
    {
        UpdatePowerUpScrollbarDisplay();
    }

}
