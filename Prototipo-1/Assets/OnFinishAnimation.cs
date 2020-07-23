using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//CODIGO PARA VERSIONAR
public class OnFinishAnimation : MonoBehaviour
{
    public GameObject camvasStartCombat;
    public InputManager inputManager;
    public DataCombatPvP dataCombatPvP;
    private Player player;
    private Enemy enemy;
    float delayActivateMovement = 0.5f;
    private bool disableMovementCharacters;
    //public bool enableMovementCharacterInDisableMe = true;
    private void OnEnable()
    {
        enemy = null;
        player = null;
       
    }
    private void OnDisable()
    {
        disableMovementCharacters = false;
        /*if (enableMovementCharacterInDisableMe)
        {
            EnableMovementPlayers();
        }*/
    }
    private void Start()
    {
        disableMovementCharacters = false;
        inputManager = GameObject.Find("InputManagerController").GetComponent<InputManager>();
    }
    private void Update()
    {
        if (dataCombatPvP == null && (player == null || enemy == null))
        {
            player = FindObjectOfType<Player>();
            enemy = FindObjectOfType<Enemy>();
            //Debug.Log(enemy);
        }
        if (!disableMovementCharacters)
        {
            Debug.Log("ENTRA CONCHUDO");
            DisableMovementPlayers();
            disableMovementCharacters = true;
        }
    }
    public void DisableObject()
    {
        gameObject.SetActive(false);
    }
    public void DisableMovementPlayers()
    {
        if (dataCombatPvP != null)
        {
            if (dataCombatPvP.player1 != null & dataCombatPvP.player2 != null)
            {
                dataCombatPvP.player1.enableMovementPlayer = false;
                dataCombatPvP.player1.enableMovement = false;
                dataCombatPvP.player2.enableMovementPlayer = false;
                dataCombatPvP.player2.enableMovement = false;
            }
        }
        else if (player != null && enemy != null)
        {
            player.enableMovementPlayer = false;
            player.enableMovement = false;
            enemy.enableMovement = false;
        }
    }
    public void EnableMovementPlayers()
    {
        if (dataCombatPvP != null)
        {
            if (dataCombatPvP.player1 != null & dataCombatPvP.player2 != null)
            {
                dataCombatPvP.player1.enableMovementPlayer = true;
                dataCombatPvP.player1.enableMovement = true;
                dataCombatPvP.player2.enableMovementPlayer = true;
                dataCombatPvP.player2.enableMovement = true;
            }
        }
        else if (player != null && enemy != null)
        {
            player.enableMovementPlayer = true;
            player.enableMovement = true;
            enemy.enableMovement = true;
        }
        if (inputManager != null)
        {
            inputManager.SetInPause(false);
            inputManager.CheckInPause();
        }
    }
    public void DisableCamvas()
    {
        if (camvasStartCombat != null)
        {
            camvasStartCombat.SetActive(false);
        }
        else
        {
            Debug.Log("camvasStartCombat is null");
        }
    }
}
