using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventWise : MonoBehaviour
{
    // CODIGO PARA HACER SWITCHES CON WWISE: AkSoundEngine.SwitchState(string del nombre del nuevo estado);
    public string nombre;
    public bool initEventInStart;
    public bool inMenu;
    private GameManager gm;

    private GameData gd;

    void Start()
    {
        gd = GameData.instaceGameData;
        gm = GameManager.instanceGameManager;

        if (gd.initScene)
        {
            if (inMenu)
            {
                StartEvent("volver_al_menu");
            }
            if (initEventInStart || gm.resetMusic)
            {
                StartEvent();
                gm.resetMusic = false;
            }
        }
    }

    public void StartEvent()
    {
        AkSoundEngine.PostEvent(nombre, gameObject);
    }
    public void StartEvent(string nameEvent)
    {
        AkSoundEngine.PostEvent(nameEvent, gameObject);
    }
    public void RestartWise()
    {
        //AkSoundEngine.ClearPreparedEvents();
        AkSoundEngine.ResetListenersToDefault(gameObject);
        AkSoundEngine.ClearPreparedEvents();
    }
    
}
