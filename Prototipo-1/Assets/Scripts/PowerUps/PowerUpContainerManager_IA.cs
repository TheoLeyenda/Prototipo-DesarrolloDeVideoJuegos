using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PowerUpContainerManager_IA : PowerUpContainer
{
    // Start is called before the first frame update
    public static event Action<PowerUpContainerManager_IA, bool> OnRefreshDataPowerUpUI;
    public static event Action<PowerUpContainerManager_IA> OnNextPowerUpAsigned;
    public int minPowerUpGenerar = 2;
    public int maxPowerUpGenerar = 5;
    public int intentosDeAsignacion;
    private int auxIntentosDeAsignacion;
    public Enemy userEnemy;
    public float minDelayThrowPowerUp = 7;
    public float maxDelayThrowPowerUp = 18;
    [SerializeField]
    private float delayThrowPowerUp;
    [HideInInspector]
    public PowerUp prevPowerUp;
    [HideInInspector]
    public int prevIndex;
    [HideInInspector]
    public bool emptyPowerUps = false;
    private void Awake()
    {
        for (int i = 0; i < powerUpContainerContent.Count; i++)
        {
            if (powerUpContainerContent[i].powerUp != null)
            {
                powerUpContainerContent[i].namePowerUp = powerUpContainerContent[i].powerUp.namePowerUp;
                powerUpContainerContent[i].powerUp.enemy = userEnemy;
            }
        }
    }
    private void OnEnable()
    {
        currentIndexPowerUp = powerUpContainerContent.Count - 1;
        delayThrowPowerUp = UnityEngine.Random.Range(minDelayThrowPowerUp, maxDelayThrowPowerUp);
        PowerUp.OnDisablePowerUpEffect += EnableShootPowerUp;
        if (intentosDeAsignacion <= 1)
        {
            intentosDeAsignacion = 10;
        }
        SettingsDataPowerUpInSpawn();
    }
    private void OnDisable()
    {
        PowerUp.OnDisablePowerUpEffect -= EnableShootPowerUp;
    }
    void Start()
    {
        auxIntentosDeAsignacion = intentosDeAsignacion;
        userContainer = ThrowPowerUpController.UserPowerUpController.Enemy;
    }

    void Update()
    {
        if (userEnemy.GetInCombatPosition())
        {
            DelayEnableThrowPowerUp();
            CheckDelayThrowPowerUp();
        }
    }
    public void ResetCountPowerUps()
    {
        for (int i = 0; i < powerUpContainerContent.Count; i++)
        {
            powerUpContainerContent[i].countPowerUps = 0;
        }
    }
    public void CheckDelayThrowPowerUp()
    {
        if (delayThrowPowerUp > 0)
        {
            delayThrowPowerUp = delayThrowPowerUp - Time.deltaTime;
        }
        else
        {
            delayThrowPowerUp = UnityEngine.Random.Range(minDelayThrowPowerUp, maxDelayThrowPowerUp);
            ThrowPowerUp(currentIndexPowerUp);
        }
    }
    public void EnableShootPowerUp(PowerUp powerUp)
    {
        if (powerUp.userPowerUp == userContainer)
        {
            enableDelay = true;
            if (OnRefreshDataPowerUpUI != null)
            {
                OnRefreshDataPowerUpUI(this, false);
            }
        }
    }

    public void SettingsDataPowerUpInSpawn()
    {
        ResetCountPowerUps();
        int countPowerUp = UnityEngine.Random.Range(minPowerUpGenerar, maxPowerUpGenerar + 1);
        int countAsignedPowerUp = 0;
        while (countAsignedPowerUp < countPowerUp && intentosDeAsignacion > 0)
        {
            int randomIndexSelected = UnityEngine.Random.Range(1, powerUpContainerContent.Count);
            randomIndexSelected--;

            if (powerUpContainerContent[randomIndexSelected].countPowerUps <= 0 
                && powerUpContainerContent[randomIndexSelected].namePowerUp != "None")
            {
                DeselectAllPowerUps();
                int randomCount = UnityEngine.Random.Range(1, powerUpContainerContent[randomIndexSelected].maxCountPowerUps + 1);
                powerUpContainerContent[randomIndexSelected].countPowerUps = randomCount;
                powerUpContainerContent[randomIndexSelected].currentPowerUp = true;
                currentIndexPowerUp = randomIndexSelected;
                countAsignedPowerUp++;
                emptyPowerUps = false;
            }
            intentosDeAsignacion--;
        }

        for (int i = 0; i < powerUpContainerContent.Count; i++)
        {
            if (i < powerUpContainerContent.Count && powerUpContainerContent[i].powerUp != null)
            {
                powerUpContainerContent[i].powerUp.userPowerUp = userContainer;
            }
        }
    }
    public override void ThrowPowerUp(int index)
    {
        if (powerUpContainerContent[index].countPowerUps <= 0 || emptyPowerUps || index < 0 || index >= powerUpContainerContent.Count
            || powerUpContainerContent[index].powerUp.enableEffect)
            return;

        if (powerUpContainerContent[index].namePowerUp != "None")
        {
            bool characterEnableMovement = false;

            Enemy e  = powerUpContainerContent[index].powerUp.enemy;
            if (e == null) return;

            characterEnableMovement = (e.enableMovement || e.enumsEnemy.estado == EnumsCharacter.EstadoCharacter.Atrapado);

            if (powerUpContainerContent[index].currentPowerUp
            && powerUpContainerContent[index].countPowerUps > 0
            && characterEnableMovement && enableShootPowerUp)
            {
                powerUpContainerContent[index].powerUp.ActivatedPowerUp();
                powerUpContainerContent[index].countPowerUps--;
                enableShootPowerUp = false;

                prevPowerUp = powerUpContainerContent[index].powerUp;
                prevIndex = index;
                //Debug.Log("POWER UP LANZADO");

                CheckNextPowerUpAssigned();
            }
            if (OnRefreshDataPowerUpUI != null)
            {
                OnRefreshDataPowerUpUI(this, false);
            }
        }
    }

    public void CheckNextPowerUpAssigned()
    {
        bool powerUpAsigned = false;
        if (powerUpContainerContent[currentIndexPowerUp].countPowerUps > 0) return;

        powerUpContainerContent[currentIndexPowerUp].currentPowerUp = false;

        if (!CheckPowerUpAssigned(powerUpAsigned, currentIndexPowerUp))
        {
            if (!CheckPowerUpAssigned(powerUpAsigned, 0))
            {
                emptyPowerUps = true;
                currentIndexPowerUp = powerUpContainerContent.Count - 1;
                powerUpContainerContent[currentIndexPowerUp].currentPowerUp = true;
                OnRefreshDataPowerUpUI(this, false);
            }
        }
    }
    public bool CheckPowerUpAssigned(bool _powerUpAsigned, int _initIndex)
    {
        _powerUpAsigned = false;
        for (int j = _initIndex; j < powerUpContainerContent.Count; j++)
        {
            if (powerUpContainerContent[j].countPowerUps > 0)
            {
                _powerUpAsigned = true;
                powerUpContainerContent[j].currentPowerUp = true;
                currentIndexPowerUp = j;
                j = powerUpContainerContent.Count;
            }
        }
        return _powerUpAsigned;
    }

    
}
