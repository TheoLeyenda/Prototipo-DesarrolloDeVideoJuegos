using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetData : MonoBehaviour
{
    // Start is called before the first frame update
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
