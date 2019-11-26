using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventWise : MonoBehaviour
{
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
