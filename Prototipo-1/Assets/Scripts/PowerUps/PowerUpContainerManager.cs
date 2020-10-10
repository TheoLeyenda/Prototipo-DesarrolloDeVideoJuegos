using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
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
    private bool enableBehaviour = false;
    [HideInInspector]
    public bool enableShootPowerUp = true;
    private float delayEnableShootPowerUp = 0.1f;
    private float auxDelayEnableShootPowerUp = 0.1f;
    private bool enableDelay;
    private GameData gameData;
    public List<Container> powerUpContainerContent;
    public ThrowPowerUpController.UserPowerUpController userContainer;
    public static event Action<PowerUpContainerManager> OnRefreshDataPowerUpUI;
    //public static event Action<PowerUpContainerManager> OnNextPowerUpAsigned;
    private GameManager gm;
    [HideInInspector]
    public int currentIndexPowerUp;
    [HideInInspector]
    public PowerUp prevPowerUp;
    [HideInInspector]
    public int prevIndex;
    [HideInInspector]
    public bool emptyPowerUps = false;
    private void Awake()
    {
        gameData = GameData.instaceGameData;
        gm = GameManager.instanceGameManager;
        for (int i = 0; i < powerUpContainerContent.Count; i++)
        {
            if(powerUpContainerContent[i].powerUp != null)
                powerUpContainerContent[i].namePowerUp = powerUpContainerContent[i].powerUp.namePowerUp;
        }

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
        currentIndexPowerUp = powerUpContainerContent.Count - 1;
        for (int i = 0; i < gameData.dataPlayerPowerUp.Length; i++)
        {
            if (i < powerUpContainerContent.Count && powerUpContainerContent[i].powerUp != null)
            {
                powerUpContainerContent[i].countPowerUps = gameData.dataPlayerPowerUp[i].countPowerUp;
                powerUpContainerContent[i].maxCountPowerUps = gameData.dataPlayerPowerUp[i].maxCountPowerUps;
                powerUpContainerContent[i].powerUp.userPowerUp = userContainer;
            }
        }
        if (powerUpContainerContent[gameData.indexCurrentPowerUp].powerUp != null)
        {
            if (powerUpContainerContent[gameData.indexCurrentPowerUp].powerUp.player != null)
            {
                if (gameData.indexCurrentPowerUp < powerUpContainerContent.Count && gameData.indexCurrentPowerUp >= 0)
                {
                    powerUpContainerContent[gameData.indexCurrentPowerUp].currentPowerUp = true;
                    currentIndexPowerUp = gameData.indexCurrentPowerUp;
                }
            }
        }

        if (gameData.gd == GameData.GameMode.PvP)
        {
            enableBehaviour = false;
        }
        else 
        {
            enableBehaviour = true;
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
                if (OnRefreshDataPowerUpUI != null)
                {
                    OnRefreshDataPowerUpUI(this);
                }
            }
        }
    }
    public void ThrowPowerUp(int index)
    {
        if (prevPowerUp != null)
        {
            if (gameData.indexCurrentPowerUp < 0 || gameData.indexCurrentPowerUp >= powerUpContainerContent.Count
                || emptyPowerUps || !enableBehaviour || prevPowerUp.enableEffect)
                return;
        }
        else 
        {
            if (gameData.indexCurrentPowerUp < 0 || gameData.indexCurrentPowerUp >= powerUpContainerContent.Count
                    || emptyPowerUps || !enableBehaviour)
                return;
        }

        currentIndexPowerUp = index;

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

                prevPowerUp = powerUpContainerContent[index].powerUp;
                prevIndex = index;
                //Debug.Log("POWER UP LANZADO");
                if (p != null)
                {
                    if (gm.enumsGameManager.modoDeJuego == EnumsGameManager.ModosDeJuego.Historia
                        || gm.enumsGameManager.modoDeJuego == EnumsGameManager.ModosDeJuego.Supervivencia)
                    {
                        gameData.dataPlayerPowerUp[index].countPowerUp--;
                    }
                    
                    CheckNextPowerUpAssigned();
                }
                if (OnRefreshDataPowerUpUI != null)
                {
                    OnRefreshDataPowerUpUI(this);
                }
            }
        }
        else if (OnRefreshDataPowerUpUI != null)
        {
            OnRefreshDataPowerUpUI(this);
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
                OnRefreshDataPowerUpUI(this);
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
                gameData.indexCurrentPowerUp = j;
                currentIndexPowerUp = j;
                j = powerUpContainerContent.Count;
            }
        }
        return _powerUpAsigned;
    }
    public void DeselectAllPowerUps()
    {
        for (int i = 0; i < powerUpContainerContent.Count; i++)
        {
            powerUpContainerContent[i].currentPowerUp = false;
            if(powerUpContainerContent[i].powerUp != null)
                powerUpContainerContent[i].powerUp.enableEffect = false;
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
                        emptyPowerUps = false;
                        if (powerUpContainerContent[currentIndexPowerUp].namePowerUp == "None") 
                        {
                            //powerUpContainerContent[currentIndexPowerUp].currentPowerUp = true;
                            CheckNextPowerUpAssigned();
                            if (OnRefreshDataPowerUpUI != null)
                            {
                                OnRefreshDataPowerUpUI(this);
                            }
                        }
                    }
                    else
                    {
                        powerUp.EffectDestroyPowerUp();
                    }
                    
                }
            }
            //SACAR ESTO UNA VEZ EL EFECTO DEL ITEM AL SER AGARRADO ESTE INCORPORADO
            if (powerUp.disableCollision)
                powerUp.transform.parent.gameObject.SetActive(false);
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
                        emptyPowerUps = false;
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
