using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetData : MonoBehaviour
{
    private GameData gameData;
    public bool resetCurrentLevel = true;

    void Start()
    {
        gameData = GameData.instaceGameData;

        if (resetCurrentLevel)
        {
            ResetCurrentLevel();
        }
    }

    void ResetCurrentLevel()
    {
        if (gameData != null)
        {
            gameData.currentLevel = 0;
        }
    }

}
