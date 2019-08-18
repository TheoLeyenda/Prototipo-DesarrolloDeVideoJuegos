using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)

public class GameManager : MonoBehaviour
{


    // Use this for initialization

    public static GameManager instanceGameManager;
    public float step;
    public float timeOFF;
    private float currentTime;
    [HideInInspector]
    public float auxTimeOFF;



    private void Awake()
    {
        if (instanceGameManager == null)
        {
            instanceGameManager = this;
        }
        else if (instanceGameManager != null)
        {
            gameObject.SetActive(false);
        }

    }
    private void Start()
    {
        currentTime = 0;
        DontDestroyOnLoad(gameObject);
    }


    // Update is called once per frame
    void Update()
    {

    }
    public IEnumerator Weit() {
        while (currentTime < timeOFF) {
            yield return new WaitForSeconds(step);
            currentTime = currentTime + step;
        }
    }
}
    

//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)
