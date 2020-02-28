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
                dataCombatPvP.player2.enableMovementPlayer = false;
            }
        }
        else if (player != null && enemy != null)
        {
            player.enableMovementPlayer = false;
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
                dataCombatPvP.player2.enableMovementPlayer = true;
            }
        }
        else if (player != null && enemy != null)
        {
            player.enableMovementPlayer = true;
            enemy.enableMovement = true;
        }
    }
}
