using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventWise : MonoBehaviour
{
    // CODIGO PARA HACER SWITCHES CON WWISE: AkSoundEngine.SwitchState(string del nombre del nuevo estado);
    public string nombre;
    public bool initEventInStart;
    void Start()
    {
        if (initEventInStart)
        {
            StartEvent();
        }
    }

    public void StartEvent()
    {
        AkSoundEngine.PostEvent(nombre, gameObject);
    }
    
}
