using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//CODIGO PARA VERSIONAR
public class OnFinishAnimation : MonoBehaviour
{
    public DataCombatPvP dataCombatPvP;
    private Player player;
    private Enemy enemy;
    private void Update()
    {
        if (dataCombatPvP == null && player == null && enemy == null)
        {
            player = FindObjectOfType<Player>();
            enemy = FindObjectOfType<Enemy>();
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
    }
}
