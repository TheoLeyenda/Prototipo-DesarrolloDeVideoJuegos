using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpContainer : MonoBehaviour
{
    [System.Serializable]
    public class Container
    {
        public string namePowerUp;
        public PowerUp powerUp;
        public int countPowerUps;
        public int maxCountPowerUps;
        public bool currentPowerUp;
    }
    [HideInInspector]
    public bool enableShootPowerUp = true;
    protected float delayEnableShootPowerUp = 0.1f;
    protected float auxDelayEnableShootPowerUp = 0.1f;
    protected bool enableDelay;
    public List<Container> powerUpContainerContent;
    public ThrowPowerUpController.UserPowerUpController userContainer = ThrowPowerUpController.UserPowerUpController.Enemy;
    [HideInInspector]
    public int currentIndexPowerUp;

    public void DeselectAllPowerUps()
    {
        for (int i = 0; i < powerUpContainerContent.Count; i++)
        {
            powerUpContainerContent[i].currentPowerUp = false;
            if (powerUpContainerContent[i].powerUp != null)
                powerUpContainerContent[i].powerUp.enableEffect = false;
        }
    }
    public void DelayEnableThrowPowerUp()
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
    public virtual void ThrowPowerUp(int index){ }
}
