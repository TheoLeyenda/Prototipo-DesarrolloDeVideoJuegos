using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    private GameData gd;
    void Start()
    {
        GameData gd = GameData.instaceGameData;
        gd.SaveAuxData();
    }
}
