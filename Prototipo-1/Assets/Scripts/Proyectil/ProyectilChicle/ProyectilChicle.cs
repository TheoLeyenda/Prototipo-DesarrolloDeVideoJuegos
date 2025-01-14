﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilChicle : Proyectil
{
    public float checkMagnitude = 2f;
    public float timeEffectStuned;
    public Pool poolChicleCasilla;
    public float speedDown = 1.5f;
    private Grid refPlataformas;
    private GameObject[] grid;
    private int idPlataforma;
    private GameObject cuadrillaColision;
    List<GameObject> cuadrillasAbajo = new List<GameObject>();
    private Player player;
    private Enemy enemy;
    private bool collisionWhitBoxColliderController;
    void Start()
    {
        tipoDeProyectil = Proyectil.typeProyectil.AtaqueEspecial;
        ShootForward();
        timeLife = auxTimeLife;
        if (GameManager.instanceGameManager != null)
        {
            gm = GameManager.instanceGameManager;
        }
    }
    public override void ShootForward()
    {
        float _speed = speed;
        if (collisionWhitBoxColliderController)
        {
            _speed = speed * speedDown;
            collisionWhitBoxColliderController = false;
        }
        if (trailRenderer != null)
        {
            trailRenderer.enabled = true;
        }
        rg2D.AddForce(-transform.right * _speed, ForceMode2D.Force);
    }
    void Update()
    {
        CheckTimeLife();
    }
    private void OnDisable()
    {
        timeLife = auxTimeLife;
    }
    private void OnEnable()
    {
        animator.enabled = true;
    }
    public void InitRefPlataformas()
    {
        for (int i = 0; i < refPlataformas.plataformas.Length; i++)
        {
            if (refPlataformas.plataformas[i].gameObject.activeSelf)
            {
                idPlataforma = i;
            }
        }
    }
    public void DisableObject()
    {
        timeLife = 0;
        if (poolObject != null)
        {
            poolObject.Recycle();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    public void CreateChicleCasilla(int cantProyectiles, Player p, Enemy e)
    {
        if (cuadrillaColision != null)
        {
            cuadrillasAbajo.Clear();
            GameObject go = null;
            ChicleCasilla chicleCasilla = null;
            Vector3 distanceOfLeftFloor = transform.position - grid[0].transform.position;
            Vector3 distanceOfCenterFloor = transform.position - grid[1].transform.position;
            Vector3 distanceOfRightFloor = transform.position - grid[2].transform.position;
            float x = 0;
            if (cantProyectiles == 1)
            {
                if (p != null)
                {
                    cuadrillasAbajo.Add(grid[p.structsPlayer.dataPlayer.columnaActual]);
                    switch (p.structsPlayer.dataPlayer.columnaActual)
                    {
                        case 0:
                            x = refPlataformas.plataformas[idPlataforma].plataformaIzquierda.transform.position.x;
                            break;
                        case 1:
                            x = refPlataformas.plataformas[idPlataforma].plataformaCentral.transform.position.x;
                            break;
                        case 2:
                            x = refPlataformas.plataformas[idPlataforma].plataformaDerecha.transform.position.x;
                            break;
                    }
                }
                else if (e != null)
                {
                    cuadrillasAbajo.Add(grid[e.structsEnemys.dataEnemy.columnaActual]);
                    switch (e.structsEnemys.dataEnemy.columnaActual)
                    {
                        case 0:
                            x = refPlataformas.plataformas[idPlataforma].plataformaIzquierda.transform.position.x;
                            break;
                        case 1:
                            x = refPlataformas.plataformas[idPlataforma].plataformaCentral.transform.position.x;
                            break;
                        case 2:
                            x = refPlataformas.plataformas[idPlataforma].plataformaDerecha.transform.position.x;
                            break;
                    }
                }
                for (int i = 0; i < cuadrillasAbajo.Count; i++)
                {
                    go = poolChicleCasilla.GetObject();
                    chicleCasilla = go.GetComponent<ChicleCasilla>();
                    chicleCasilla.disparadorDelProyectil = disparadorDelProyectil;
                    chicleCasilla.timeStuned = timeEffectStuned;
                    chicleCasilla.transform.position = new Vector3(x, cuadrillasAbajo[i].transform.position.y, cuadrillasAbajo[i].transform.position.z);
                }
            }
            gameObject.SetActive(false);
            timeLife = 0f;
            if (GetPoolObject() != null)
            {
                GetPoolObject().Recycle();
            }
        }
    }


    public void CheckGrid(Piso piso)
    {
        if (disparadorDelProyectil == DisparadorDelProyectil.Jugador2 || disparadorDelProyectil == DisparadorDelProyectil.Enemigo)
        {
            if (piso.player != null && piso.player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player1)
            {
                grid = piso.player.posicionesDeMovimiento;
                refPlataformas = piso.player.grid;
                InitRefPlataformas();
            }
        }
        if (disparadorDelProyectil == DisparadorDelProyectil.Jugador1 || disparadorDelProyectil == DisparadorDelProyectil.Enemigo)
        {
            if (piso.player != null && piso.player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player2)
            {
                grid = piso.player.posicionesDeMovimiento;
                refPlataformas = piso.player.grid;
                InitRefPlataformas();
            }
        }
        if (disparadorDelProyectil == DisparadorDelProyectil.Jugador2 || disparadorDelProyectil == DisparadorDelProyectil.Jugador1)
        {
            if (piso.enemy != null)
            {
                grid = piso.enemy.posicionesDeMovimiento;
                refPlataformas = piso.enemy.grid;
                InitRefPlataformas();
            }
        }
    }
    protected void CheckCollision(Collider2D collision, Player PlayerDisparador)
    {
        if (collision.tag == "BoxColliderController")
        {
            BoxColliderController boxColliderController = collision.GetComponent<BoxColliderController>();
            collisionWhitBoxColliderController = true;
            Enemy e = boxColliderController.enemy;
            Player p = boxColliderController.player;
            
            if (e == null && p == null || e != null && p != null)
            {
                return;
            }

            if (e != null)
            {
                if (disparadorDelProyectil == DisparadorDelProyectil.Jugador1)
                {
                    if (e.enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.MoveToPointCombat && e.enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.MoveToPointDeath)
                    {
                        if (e.Blindaje <= 0)
                        {
                            e.life = e.life - damage;
                        }
                        else
                        {
                            e.Blindaje = e.Blindaje - damage / 2;
                        }

                        transform.eulerAngles = new Vector3(0,0, 90);
                        rg2D.velocity = Vector2.zero;
                        ShootForward();
                    }
                }
            }
            if (p != null)
            {

                if (disparadorDelProyectil == DisparadorDelProyectil.Enemigo)
                {
                    if (p.PD.Blindaje <= 0)
                    {
                        p.PD.lifePlayer = p.PD.lifePlayer - damage;
                    }
                    else
                    {
                        p.PD.Blindaje = p.PD.Blindaje - damage / 2;
                    }
                    p.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.RecibirDanio;

                    transform.eulerAngles = new Vector3(0, 0, 90);
                    rg2D.velocity = Vector2.zero;
                    ShootForward();
                }
                if (PlayerDisparador != null)
                {
                    if (boxColliderController != PlayerDisparador.boxColliderAgachado
                        && boxColliderController != PlayerDisparador.boxColliderParado
                        && boxColliderController != PlayerDisparador.boxColliderPiernas
                        && boxColliderController != PlayerDisparador.boxColliderSaltando
                        && boxColliderController != PlayerDisparador.boxColliderSprite)
                    {
                        p.SetEnableCounterAttack(true);
                        if (p.PD.Blindaje <= 0)
                        {
                            p.PD.lifePlayer = p.PD.lifePlayer - damage;
                        }
                        else
                        {
                            p.PD.Blindaje = p.PD.Blindaje - damage / 2;
                        }
                        p.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.RecibirDanio;

                        transform.eulerAngles = new Vector3(0, 0, 90);
                        rg2D.velocity = Vector2.zero;
                        ShootForward();
                    }
                }
            }

        }
        if (collision.tag == "Piso")
        {
            GameObject cuadrilla = collision.gameObject;
            Piso piso = collision.GetComponent<Piso>();
            CheckGrid(piso);
            if (disparadorDelProyectil == DisparadorDelProyectil.Jugador1)
            {
                cuadrillaColision = cuadrilla;
                if (PLAYER1 != null)
                {
                    CreateChicleCasilla(1, piso.player, null);
                    CreateChicleCasilla(1, null, piso.enemy);
                }

            }
            else if (disparadorDelProyectil == DisparadorDelProyectil.Jugador2)
            {

                cuadrillaColision = cuadrilla;
                if (PLAYER2 != null)
                {
                    CreateChicleCasilla(1, piso.player, null);
                }
            }
            else if (disparadorDelProyectil == DisparadorDelProyectil.Enemigo)
            {
                cuadrillaColision = cuadrilla;
                CreateChicleCasilla(1, piso.player, null);

            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckCollision(collision, PLAYER1);
        CheckCollision(collision, PLAYER2);
    }
}
