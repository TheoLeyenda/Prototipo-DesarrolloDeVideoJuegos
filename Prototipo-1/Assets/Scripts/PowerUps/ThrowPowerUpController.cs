using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowPowerUpController : MonoBehaviour
{
    public PowerUpContainerManager powerUpContainerManager;

    public enum UserPowerUpController
    {
        Player1,
        Player2,
        Enemy,
    }

    [System.Serializable]
    public class DataUser
    {
        public UserPowerUpController userPowerUpController;
        public string inputNameThrowPowerUp;
    }

    private GameData gameData;
    public DataUser dataUser;

    private void Start()
    {
        gameData = GameData.instaceGameData;
    }
    private void Update()
    {
        CheckThrowPowerUp();
    }
    public void CheckThrowPowerUp()
    {
        if (dataUser.userPowerUpController == UserPowerUpController.Player1
            || dataUser.userPowerUpController == UserPowerUpController.Player2)
        {
            if (InputPlayerController.GetInputButtonDown(dataUser.inputNameThrowPowerUp))
            {
                powerUpContainerManager.ThrowPowerUp(gameData.indexCurrentPowerUp);
            }
        }
    }
    
}
