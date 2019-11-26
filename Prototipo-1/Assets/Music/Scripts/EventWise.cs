using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventWise : MonoBehaviour
{
    // Start is called before the first frame update
    public string nombreEvento;
    public GameObject objetoMusical;
    void Start()
    {
        AkSoundEngine.PostEvent(nombreEvento,objetoMusical);
    }
    
}
