using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventWise : MonoBehaviour
{
    void Start()
    {
        AkSoundEngine.PostEvent("inicio",gameObject);
    }
    
}
