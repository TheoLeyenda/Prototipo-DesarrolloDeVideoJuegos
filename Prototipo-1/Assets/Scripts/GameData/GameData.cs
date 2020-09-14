using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    //Aqui se guardara toda la data qie se deba guardar en playerPref
    //la cantidad de powerUps.
    public enum GameMode
    {
        None,
        Survival,
        History,
        PvP,
    }
    public string[] nameLokedObjects;
    public string[] nameUnlokedObjects;
    public string[] auxNameLokedObjects;
    //[HideInInspector]
    public string[] auxNameUnlokedObjects;
    public int currentLevel;
    public int indexCurrentPowerUp;
    private int auxIndexCurrentPowerUp;
    public string currentNameUser = "None";
    public int generalScore;
    public GameMode gd = GameMode.None;

    [System.Serializable]
    public class InventoryPlayer
    {
        public string namePowerUp;
        public int countPowerUp;
        public int maxCountPowerUps;
    }

    public InventoryPlayer[] dataPlayerPowerUp;
    public InventoryPlayer[] auxDataPlayerPowerUp;
    public static GameData instaceGameData;

    private void Awake()
    {
        if (instaceGameData == null)
        {
            instaceGameData = this;
            DontDestroyOnLoad(this);
        }
        else if (instaceGameData != null)
        {
            Destroy(gameObject);
        }

        for (int i = 0; i < nameLokedObjects.Length; i++)
        {
            auxNameLokedObjects[i] = nameLokedObjects[i];
        }
        for (int i = 0; i < nameUnlokedObjects.Length; i++)
        {
            auxNameUnlokedObjects[i] = nameUnlokedObjects[i];
        }
        SaveAuxData();
        
    }
    public void SaveAuxData()
    {
        for (int i = 0; i < dataPlayerPowerUp.Length; i++)
        {
            auxDataPlayerPowerUp[i].countPowerUp = dataPlayerPowerUp[i].countPowerUp;
            auxDataPlayerPowerUp[i].maxCountPowerUps = dataPlayerPowerUp[i].maxCountPowerUps;
            auxDataPlayerPowerUp[i].namePowerUp = dataPlayerPowerUp[i].namePowerUp;
        }
        auxIndexCurrentPowerUp = indexCurrentPowerUp;
    }
    public void LoadAuxData()
    {
        for (int i = 0; i < auxDataPlayerPowerUp.Length; i++)
        {
            dataPlayerPowerUp[i].countPowerUp = auxDataPlayerPowerUp[i].countPowerUp;
            dataPlayerPowerUp[i].maxCountPowerUps = auxDataPlayerPowerUp[i].maxCountPowerUps;
            dataPlayerPowerUp[i].namePowerUp = auxDataPlayerPowerUp[i].namePowerUp;
        }
        indexCurrentPowerUp = auxIndexCurrentPowerUp;
    }

    public void ClearData()
    {
        for (int i = 0; i < dataPlayerPowerUp.Length; i++)
        {
            dataPlayerPowerUp[i].countPowerUp = 0;
        }
    }
    public bool CheckUnlokedObject(string name)
    {
        for (int i = 0; i < nameUnlokedObjects.Length; i++)
        {
            if (nameLokedObjects[i] == name)
            {
                return true;
            }
        }
        return false;
    }
    public void UnlokedObject(string name)
    {
        for (int i = 0; i < nameLokedObjects.Length; i++)
        {
            if (name == nameLokedObjects[i])
            {
                nameUnlokedObjects[i] = nameLokedObjects[i];
                nameLokedObjects[i] = " ";
            }
        }
    }
}
