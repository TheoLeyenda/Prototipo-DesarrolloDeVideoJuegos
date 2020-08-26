using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PowerUpDisplayController : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Sprite> spritesPowerUps;
    public PowerUpContainerManager powerUpContainerManager;
    private Scrollbar scrollbarPowerUp;
    private Image imageCurrentPowerUp;
    private TextMeshProUGUI textCountPowerUp;
    private UI_Manager ui_Manager;
    //private bool enableScrollBar;
    public string InputThrowPowerUp;
    private bool inNextPowerUp;
    PowerUp prevPowerUp;
    private void Awake()
    {
        ui_Manager = UI_Manager.instanceUI_Manager;
    }

    private void OnEnable()
    {
        PowerUpContainerManager.OnRefreshDataPowerUpUI += UpdatePowerDataDisplay;
        PowerUpContainerManager.OnNextPowerUpAsigned += NextPowerUpAsigned;

        switch (powerUpContainerManager.userContainer)
        {
            case ThrowPowerUpController.UserPowerUpController.Enemy:
                scrollbarPowerUp = ui_Manager.enemyHUD.scrollbarPowerUp;
                imageCurrentPowerUp = ui_Manager.enemyHUD.imageCurrentPowerUp;
                textCountPowerUp = ui_Manager.enemyHUD.textCountPowerUp;
                break;
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
        prevPowerUp = powerUpContainerManager.powerUpContainerContent[powerUpContainerManager.currentIndexPowerUp].powerUp;
        scrollbarPowerUp.size = 0;
    }
    private void OnDisable()
    {
        PowerUpContainerManager.OnRefreshDataPowerUpUI -= UpdatePowerDataDisplay;
        PowerUpContainerManager.OnNextPowerUpAsigned -= NextPowerUpAsigned;
    }
    private void Start()
    {
        UpdatePowerDataDisplay(powerUpContainerManager);
        prevPowerUp = powerUpContainerManager.powerUpContainerContent[powerUpContainerManager.currentIndexPowerUp].powerUp;
        scrollbarPowerUp.size = 0;
    }
    
    public void UpdatePowerDataDisplay(PowerUpContainerManager _powerUpContainerManager)
    {
        if (_powerUpContainerManager == powerUpContainerManager)
        {
            imageCurrentPowerUp.sprite = spritesPowerUps[powerUpContainerManager.currentIndexPowerUp];
            textCountPowerUp.text = "" + powerUpContainerManager.powerUpContainerContent[powerUpContainerManager.currentIndexPowerUp].countPowerUps;
            scrollbarPowerUp.size = 1;
            //enableScrollBar = true;
        }
    }
    public void NextPowerUpAsigned(PowerUpContainerManager _powerUpContainerManager)
    {
        if (_powerUpContainerManager == powerUpContainerManager)
        {
            //enableScrollBar = true;
            prevPowerUp = powerUpContainerManager.powerUpContainerContent[powerUpContainerManager.currentIndexPowerUp - 1].powerUp;
            scrollbarPowerUp.size = 1;
        }
    }
    public void UpdatePowerUpScrollbarDisplay()
    {
        if (powerUpContainerManager.currentIndexPowerUp >= powerUpContainerManager.powerUpContainerContent.Count - 1)
        {
            scrollbarPowerUp.size = 0;
            return;
        }

        if (scrollbarPowerUp == null
            || powerUpContainerManager.powerUpContainerContent[powerUpContainerManager.currentIndexPowerUp].powerUp == null) return;

        if (powerUpContainerManager.userContainer == ThrowPowerUpController.UserPowerUpController.Player1
            || powerUpContainerManager.userContainer == ThrowPowerUpController.UserPowerUpController.Player2)
        {
            if (Input.GetButtonDown(InputThrowPowerUp) /*&& !enableScrollBar*/)
            {
                //enableScrollBar = true;
            }
        }
        //if (enableScrollBar)
        //{
        if (powerUpContainerManager.powerUpContainerContent[powerUpContainerManager.currentIndexPowerUp].powerUp.enableEffect
            && powerUpContainerManager.powerUpContainerContent[powerUpContainerManager.currentIndexPowerUp].powerUp.delayEffect > 0)
        {
            float value = powerUpContainerManager.powerUpContainerContent[powerUpContainerManager.currentIndexPowerUp].powerUp.delayEffect;
            float maxValue = powerUpContainerManager.powerUpContainerContent[powerUpContainerManager.currentIndexPowerUp].powerUp.GetAuxDelayEffect();

            if (value <= maxValue && value > 0)
            {
                scrollbarPowerUp.size = value / maxValue;
            }
            else if (value > maxValue)
            {
                value = maxValue;
            }
            else if (value <= 0)
            {
                value = 0;
            }
        }
        else if (prevPowerUp.enableEffect && prevPowerUp.delayEffect > 0)
        {
            float value = prevPowerUp.delayEffect;
            float maxValue = prevPowerUp.GetAuxDelayEffect();

            if (value <= maxValue && value > 0)
            {
                scrollbarPowerUp.size = value / maxValue;
            }
            else if (value > maxValue)
            {
                value = maxValue;
            }
            else if (value <= 0)
            {
                value = 0;
            }
        }
        else if (powerUpContainerManager.powerUpContainerContent[powerUpContainerManager.currentIndexPowerUp].powerUp.enableEffect)
        {
            scrollbarPowerUp.size = 1;
        }
        else if (prevPowerUp.enableEffect)
        {
            scrollbarPowerUp.size = 1;
        }
        else if (!prevPowerUp.enableEffect)
        {
            scrollbarPowerUp.size = 0;
        }
        
            
    }

    private void Update()
    {
        UpdatePowerUpScrollbarDisplay();
    }

}
