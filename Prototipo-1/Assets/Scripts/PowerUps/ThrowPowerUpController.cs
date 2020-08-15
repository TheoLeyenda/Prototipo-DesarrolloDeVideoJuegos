using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowPowerUpController : MonoBehaviour
{
    // Start is called before the first frame update
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

    // Update is called once per frame
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
        else if (dataUser.userPowerUpController == UserPowerUpController.Enemy)
        {
            //powerUpContainerManager.ThrowPowerUp(gameData.indexCurrentPowerUp);
        }
    }
    
}
