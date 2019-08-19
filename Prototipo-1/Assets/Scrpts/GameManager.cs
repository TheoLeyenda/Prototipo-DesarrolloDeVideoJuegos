using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)

public class GameManager : MonoBehaviour
{


    // Use this for initialization
    public Text TextTimeOfAttack;
    public static GameManager instanceGameManager;
    public float timeSelectionAttack;
    [HideInInspector]
    public float auxTimeSelectionAttack;
    private List<Enemy> enemies;
    private List<Player> players;
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
        auxTimeSelectionAttack = timeSelectionAttack;
        enemies = new List<Enemy>();
        players = new List<Player>();
        for (int i = 0; i < SceneManager.GetActiveScene().GetRootGameObjects().Length; i++) {
            if (SceneManager.GetActiveScene().GetRootGameObjects()[i].tag == "Enemy")
            {
                if (SceneManager.GetActiveScene().GetRootGameObjects()[i].GetComponent<Enemy>() != null)
                {
                    enemies.Add(SceneManager.GetActiveScene().GetRootGameObjects()[i].GetComponent<Enemy>());
                }
            }
            if (SceneManager.GetActiveScene().GetRootGameObjects()[i].tag == "Player")
            {
                if (SceneManager.GetActiveScene().GetRootGameObjects()[i].GetComponent<Player>() != null)
                {
                    players.Add(SceneManager.GetActiveScene().GetRootGameObjects()[i].GetComponent<Player>());
                }
            }
        }
        for (int i = 0; i < enemies.Count; i++) {
            enemies[i].mover = false;
        }
        DontDestroyOnLoad(gameObject);
    }


    // Update is called once per frame
    void Update()
    {
        if (TextTimeOfAttack != null)
        {
            CheckTimeAttackCharacters();
        }
    }
    private void CheckTimeAttackCharacters() {
        if (timeSelectionAttack > 0)
        {
            timeSelectionAttack = timeSelectionAttack - Time.deltaTime;
            TextTimeOfAttack.text = "" + (int)timeSelectionAttack;
        }
    }

}
    

//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)
