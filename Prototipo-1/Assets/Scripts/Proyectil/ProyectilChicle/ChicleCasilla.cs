﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChicleCasilla : MonoBehaviour
{
    public Proyectil.DisparadorDelProyectil disparadorDelProyectil;

    public Player player;

    public Enemy enemy;

    public float timeStuned;

    private bool inStunedEffect;

    private bool targetAssigned;

    private void OnDisable()
    {
        inStunedEffect = false;
        targetAssigned = false;
    }
    void Update()
    {
        CheckEffect();
        if (player != null)
        {
            if (!player.gameObject.activeSelf)
            {
                gameObject.SetActive(false);
            }
        }
    }
    public void CheckEffect()
    {
        if(player != null && player.transform.position.y <= player.GetInitialPosition().y && !player.GetIsJumping() 
            && player.enumsPlayers.movimiento != EnumsPlayers.Movimiento.Saltar 
            && player.enumsPlayers.movimiento != EnumsPlayers.Movimiento.SaltoAtaque
            && player.enumsPlayers.movimiento != EnumsPlayers.Movimiento.SaltoDefensa
            && player.SpeedJump >= player.GetAuxSpeedJump())
        {
            if (!inStunedEffect)
            {
                player.timeStuned = timeStuned;
                player.enumsPlayers.estadoJugador = EnumsPlayers.EstadoJugador.Atrapado;
                inStunedEffect = true;
                targetAssigned = true;
            }

            if (player.timeStuned <= 0)
            {
                gameObject.SetActive(false);
            }
        }
        else if (enemy != null && enemy.transform.position.y <= enemy.InitialPosition.y && !enemy.GetIsJamping()
            && enemy.enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.Saltar
            && enemy.enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.SaltoAtaque
            && enemy.enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.SaltoDefensa
            && enemy.SpeedJump >= enemy.GetAuxSpeedJamp())
        {
            if (!inStunedEffect)
            {
                enemy.timeStuned = timeStuned;
                enemy.enumsEnemy.SetStateEnemy(EnumsEnemy.EstadoEnemigo.Atrapado);
                inStunedEffect = true;
                targetAssigned = true;
            }
            if (enemy.timeStuned <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!targetAssigned)
            {
                player = collision.GetComponent<Player>();
            }
            if (player == null)
            {
                return;
            }
            player.transform.position = new Vector3(transform.position.x, player.transform.position.y, player.transform.position.z);
            player.enumsPlayers.estadoJugador = EnumsPlayers.EstadoJugador.Atrapado;
        }
        if (collision.tag == "Enemy")
        {
            if (!targetAssigned)
            {
                enemy = collision.GetComponent<Enemy>();
            }
            if (enemy == null)
            {
                return;
            }
            enemy.transform.position = new Vector3(transform.position.x, enemy.transform.position.y, enemy.transform.position.z);
            enemy.enumsEnemy.SetStateEnemy(EnumsEnemy.EstadoEnemigo.Atrapado);
        }
    }
}
