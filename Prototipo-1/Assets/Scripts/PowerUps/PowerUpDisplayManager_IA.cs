using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerUpDisplayManager_IA : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Sprite> spritesPowerUps;
    public PowerUpContainerManager_IA powerUpContainerManager_IA;
    private Scrollbar scrollbarPowerUp;
    private Image imageCurrentPowerUp;
    private TextMeshProUGUI textCountPowerUp;
    private UI_Manager ui_Manager;
    //public string InputThrowPowerUp;
    private bool inNextPowerUp;
    private bool enableUpdateData = false;
    private float minSizeScrollbarPowerUp = 0.005f;
    //PowerUp prevPowerUp;
    private void Awake()
    {
        ui_Manager = UI_Manager.instanceUI_Manager;
    }

    private void OnEnable()
    {
        PowerUpContainerManager_IA.OnRefreshDataPowerUpUI += UpdatePowerDataDisplay;
        //PowerUpContainerManager_IA.OnNextPowerUpAsigned += NextPowerUpAsigned;

        scrollbarPowerUp = ui_Manager.enemyHUD.scrollbarPowerUp;
        imageCurrentPowerUp = ui_Manager.enemyHUD.imageCurrentPowerUp;
        textCountPowerUp = ui_Manager.enemyHUD.textCountPowerUp;
            
        UpdatePowerDataDisplay(powerUpContainerManager_IA);
        //prevPowerUp = powerUpContainerManager_IA.powerUpContainerContent[powerUpContainerManager_IA.currentIndexPowerUp].powerUp;
        scrollbarPowerUp.size = 0;
    }
    private void OnDisable()
    {
        PowerUpContainerManager_IA.OnRefreshDataPowerUpUI -= UpdatePowerDataDisplay;
        //PowerUpContainerManager_IA.OnNextPowerUpAsigned -= NextPowerUpAsigned;
    }
    private void Start()
    {
        UpdatePowerDataDisplay(powerUpContainerManager_IA);
        //prevPowerUp = powerUpContainerManager_IA.powerUpContainerContent[powerUpContainerManager_IA.currentIndexPowerUp].powerUp;
        scrollbarPowerUp.size = 0;
    }

    public void UpdatePowerDataDisplay(PowerUpContainerManager_IA _powerUpContainerManager)
    {
        
        if (_powerUpContainerManager == powerUpContainerManager_IA)
        {

            if (powerUpContainerManager_IA.emptyPowerUps
                && powerUpContainerManager_IA.powerUpContainerContent[powerUpContainerManager_IA.prevIndex].countPowerUps <= 0
                && !powerUpContainerManager_IA.prevPowerUp.enableEffect)
            {
                imageCurrentPowerUp.sprite = spritesPowerUps[spritesPowerUps.Count - 1];
                textCountPowerUp.text = " ";
                powerUpContainerManager_IA.prevIndex = powerUpContainerManager_IA.powerUpContainerContent.Count - 1;
                return;
            }
            if (powerUpContainerManager_IA.prevPowerUp != null)
            {
                if (!powerUpContainerManager_IA.prevPowerUp.enableEffect && powerUpContainerManager_IA.powerUpContainerContent[powerUpContainerManager_IA.prevIndex].countPowerUps <= 0)
                {
                    imageCurrentPowerUp.sprite = spritesPowerUps[powerUpContainerManager_IA.currentIndexPowerUp];
                    textCountPowerUp.text = "" + powerUpContainerManager_IA.powerUpContainerContent[powerUpContainerManager_IA.currentIndexPowerUp].countPowerUps;
                }
                else
                {
                    imageCurrentPowerUp.sprite = spritesPowerUps[powerUpContainerManager_IA.prevIndex];
                    textCountPowerUp.text = "" + powerUpContainerManager_IA.powerUpContainerContent[powerUpContainerManager_IA.prevIndex].countPowerUps;
                }
            }
            else
            {
                imageCurrentPowerUp.sprite = spritesPowerUps[powerUpContainerManager_IA.currentIndexPowerUp];
                textCountPowerUp.text = "" + powerUpContainerManager_IA.powerUpContainerContent[powerUpContainerManager_IA.currentIndexPowerUp].countPowerUps;
            }
        }
    }
    /*public void NextPowerUpAsigned(PowerUpContainerManager_IA _powerUpContainerManager)
    {
        if (_powerUpContainerManager == powerUpContainerManager_IA)
        {
            prevPowerUp = powerUpContainerManager_IA.powerUpContainerContent[powerUpContainerManager_IA.currentIndexPowerUp - 1].powerUp;
            if (!prevPowerUp.enableEffect)
            {
                scrollbarPowerUp.size = 0;
            }
        }
    }*/
    public void UpdatePowerUpScrollbarDisplay()
    {
        if (powerUpContainerManager_IA.currentIndexPowerUp >= powerUpContainerManager_IA.powerUpContainerContent.Count - 1 && !powerUpContainerManager_IA.prevPowerUp.enableEffect)
        {
            scrollbarPowerUp.size = 0;
            return;
        }
        PowerUp currentPowerUp = powerUpContainerManager_IA.powerUpContainerContent[powerUpContainerManager_IA.currentIndexPowerUp].powerUp;

        if (currentPowerUp != null)
        {
            if (currentPowerUp.enableEffect && currentPowerUp.delayEffect > 0)
            {
                float value = currentPowerUp.delayEffect;
                float maxValue = currentPowerUp.GetAuxDelayEffect();

                scrollbarPowerUp.size = value / maxValue;
                if (scrollbarPowerUp.size <= minSizeScrollbarPowerUp)
                {
                    //Debug.Log("ENTRO");
                    currentPowerUp.DisableEffect();
                    scrollbarPowerUp.size = 0;
                    UpdatePowerDataDisplay(powerUpContainerManager_IA);
                    enableUpdateData = true;
                }
            }
            else
            {
                scrollbarPowerUp.size = 0;
            }
        }
        if (powerUpContainerManager_IA.prevPowerUp != null)
        {
            if (powerUpContainerManager_IA.prevPowerUp.enableEffect && powerUpContainerManager_IA.prevPowerUp.delayEffect > 0)
            {
                float value = powerUpContainerManager_IA.prevPowerUp.delayEffect;
                float maxValue = powerUpContainerManager_IA.prevPowerUp.GetAuxDelayEffect();

                scrollbarPowerUp.size = value / maxValue;
                if (scrollbarPowerUp.size <= minSizeScrollbarPowerUp)
                {
                    //Debug.Log("ENTRO");
                    powerUpContainerManager_IA.prevPowerUp.DisableEffect();
                    scrollbarPowerUp.size = 0;
                    UpdatePowerDataDisplay(powerUpContainerManager_IA);
                    enableUpdateData = true;
                }
            }
            else
            {
                scrollbarPowerUp.size = 0;
            }
        }

        if (powerUpContainerManager_IA.prevPowerUp != null && currentPowerUp != null && enableUpdateData)
            if (!currentPowerUp.enableEffect && !powerUpContainerManager_IA.prevPowerUp.enableEffect)
            {
                UpdatePowerDataDisplay(powerUpContainerManager_IA);
                enableUpdateData = false;
            }
    }

    private void Update()
    {
        UpdatePowerUpScrollbarDisplay();
    }
}
