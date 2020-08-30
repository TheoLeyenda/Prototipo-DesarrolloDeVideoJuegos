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
    private bool enableUpdateData = false;
    private float minSizeScrollbarPowerUp = 0.005f;
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
            if (powerUpContainerManager.currentIndexPowerUp >= powerUpContainerManager.powerUpContainerContent.Count - 1)
            {
                int index = powerUpContainerManager.powerUpContainerContent.Count - 1;
                prevPowerUp = powerUpContainerManager.powerUpContainerContent[index].powerUp;
                imageCurrentPowerUp.sprite = spritesPowerUps[spritesPowerUps.Count - 1];
                textCountPowerUp.text = "" + powerUpContainerManager.powerUpContainerContent[index].countPowerUps;
            }
            else if (prevPowerUp != null)
            {
                if (prevPowerUp.typePowerUp == PowerUp.TypePowerUp.PowerUpDisable || !prevPowerUp.enableEffect)
                {
                    imageCurrentPowerUp.sprite = spritesPowerUps[powerUpContainerManager.currentIndexPowerUp];
                    textCountPowerUp.text = "" + powerUpContainerManager.powerUpContainerContent[powerUpContainerManager.currentIndexPowerUp].countPowerUps;
                    //Debug.Log("ACTUALICE CORRECTAMENTE");
                }
                else
                {
                    textCountPowerUp.text = "" + powerUpContainerManager.powerUpContainerContent[powerUpContainerManager.currentIndexPowerUp - 1].countPowerUps;
                }
            }
            else
            {
                imageCurrentPowerUp.sprite = spritesPowerUps[powerUpContainerManager.currentIndexPowerUp];
                textCountPowerUp.text = "" + powerUpContainerManager.powerUpContainerContent[powerUpContainerManager.currentIndexPowerUp].countPowerUps;
            }
        }
    }
    public void NextPowerUpAsigned(PowerUpContainerManager _powerUpContainerManager)
    {
        if (_powerUpContainerManager == powerUpContainerManager)
        {
            prevPowerUp = powerUpContainerManager.powerUpContainerContent[powerUpContainerManager.currentIndexPowerUp - 1].powerUp;
            if (!prevPowerUp.enableEffect)
            {
                scrollbarPowerUp.size = 0;
            }
        }
    }
    public void UpdatePowerUpScrollbarDisplay()
    {
        if (powerUpContainerManager.currentIndexPowerUp >= powerUpContainerManager.powerUpContainerContent.Count - 1)
        {
            scrollbarPowerUp.size = 0;
            return;
        }
        PowerUp currentPowerUp = powerUpContainerManager.powerUpContainerContent[powerUpContainerManager.currentIndexPowerUp].powerUp;
        if (scrollbarPowerUp == null
            || currentPowerUp == null
            || prevPowerUp == null) return;

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
                UpdatePowerDataDisplay(powerUpContainerManager);
                enableUpdateData = true;
            }
        }
        else if (prevPowerUp.enableEffect && prevPowerUp.delayEffect > 0)
        {
            float value = prevPowerUp.delayEffect;
            float maxValue = prevPowerUp.GetAuxDelayEffect();

            scrollbarPowerUp.size = value / maxValue;
            if (scrollbarPowerUp.size <= minSizeScrollbarPowerUp)
            {
                //Debug.Log("ENTRO");
                currentPowerUp.DisableEffect();
                scrollbarPowerUp.size = 0;
                UpdatePowerDataDisplay(powerUpContainerManager);
                enableUpdateData = true;
            }
        }
        else if (currentPowerUp.enableEffect && currentPowerUp.typePowerUp == PowerUp.TypePowerUp.PowerUpDisable)
        {
            scrollbarPowerUp.size = 1;
            //Debug.Log("CUACK");
        }
        else if (prevPowerUp.enableEffect && prevPowerUp.typePowerUp == PowerUp.TypePowerUp.PowerUpDisable)
        {
            scrollbarPowerUp.size = 1;
        }
        else if (!prevPowerUp.enableEffect)
        {
            scrollbarPowerUp.size = 0;
        }

        if (prevPowerUp != null && currentPowerUp != null && enableUpdateData)
            if (!currentPowerUp.enableEffect && !prevPowerUp.enableEffect)
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
