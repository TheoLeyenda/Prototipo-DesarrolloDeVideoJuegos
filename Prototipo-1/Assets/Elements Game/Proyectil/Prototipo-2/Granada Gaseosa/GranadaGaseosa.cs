using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototipo_2
{
    public class GranadaGaseosa : ProyectilParabola
    {
        // Start is called before the first frame update
        private Grid grid;
        public Pool poolGaseosa;
        private PoolObject poolObject;
        private Cuadrilla cuadrillaColision;
        List<Cuadrilla> cuadrillasAbajo = new List<Cuadrilla>();
        private void Start()
        {
            poolObject = GetComponent<PoolObject>();
        }
        // Update is called once per frame
        void Update()
        {
            CheckMovement();
            CheckTimeLife();
        }
        private void OnEnable()
        {
            timeLife = auxTimeLife;
            OnParabola();
        }
        
        public void CreateGaseosas()
        {
            if (grid != null)
            {
                cuadrillasAbajo.Clear();
                cuadrillasAbajo.Add(cuadrillaColision);
                int num;
                GameObject go = null;
                Gaseosa gaseosa = null;
                if (Cuadrilla.PosicionCuadrilla.CuadrillaBajaIzquierda == cuadrillaColision.posicionCuadrilla)
                {
                    cuadrillasAbajo.Add(grid.matrizCuadrilla[grid.baseGrild][1]);
                }
                else if (Cuadrilla.PosicionCuadrilla.CuadrillaBajaCentral == cuadrillaColision.posicionCuadrilla)
                {
                    num = (int)Random.Range(0, 100);
                    if (num >= 50)
                    {
                        num = 2;
                    }
                    else if (num < 50)
                    {
                        num = 0;
                    }
                    cuadrillasAbajo.Add(grid.matrizCuadrilla[grid.baseGrild][num]);
                }
                else if (Cuadrilla.PosicionCuadrilla.CuadrillaBajaDerecha == cuadrillaColision.posicionCuadrilla)
                { 
                    cuadrillasAbajo.Add(grid.matrizCuadrilla[grid.baseGrild][1]);
                }
                for (int i = 0; i < cuadrillasAbajo.Count; i++)
                {
                    go = poolGaseosa.GetObject();
                    gaseosa = go.GetComponent<Gaseosa>();
                    gaseosa.disparadorDelProyectil = disparadorDelProyectil;
                    gaseosa.transform.position = cuadrillasAbajo[i].transform.position;
                }
            }
            timeLife = 0;
        }
        public void CheckGrid(Cuadrilla cuadrilla)
        {
            if (cuadrilla.player != null)
            {
                if (disparadorDelProyectil == DisparadorDelProyectil.Enemigo || disparadorDelProyectil == DisparadorDelProyectil.Jugador2)
                {
                    if (cuadrilla.player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player1)
                    {
                        grid = cuadrilla.player.gridPlayer;
                    }
                }
                else if (disparadorDelProyectil == DisparadorDelProyectil.Enemigo || disparadorDelProyectil == DisparadorDelProyectil.Jugador1)
                {
                    if (cuadrilla.player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player2)
                    {
                        grid = cuadrilla.player.gridPlayer;
                    }
                }
            }
            else if (cuadrilla.enemy != null)
            {
                if (disparadorDelProyectil == DisparadorDelProyectil.Jugador1 || disparadorDelProyectil == DisparadorDelProyectil.Jugador2)
                {
                    grid = cuadrilla.enemy.gridEnemy;
                }
            }
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.tag == "Cuadrilla")
            {
                Cuadrilla cuadrilla = collision.GetComponent<Cuadrilla>();
                if ((cuadrilla.enemy == null && cuadrilla.player == null || cuadrilla.enemy != null && cuadrilla.player != null) || cuadrilla == null)
                {
                    return;
                }
                if ((cuadrilla.posicionCuadrilla == Cuadrilla.PosicionCuadrilla.CuadrillaBajaCentral
                    || cuadrilla.posicionCuadrilla == Cuadrilla.PosicionCuadrilla.CuadrillaBajaDerecha
                    || cuadrilla.posicionCuadrilla == Cuadrilla.PosicionCuadrilla.CuadrillaBajaIzquierda) && cuadrilla != null)
                {
                    
                    if (disparadorDelProyectil == DisparadorDelProyectil.Jugador1)
                    {
                        if (cuadrilla.player.enumsPlayers.numberPlayer != EnumsPlayers.NumberPlayer.player1)
                        {
                            CheckGrid(cuadrilla);
                            cuadrillaColision = cuadrilla;
                            CreateGaseosas();
                        }
                    }
                    else if (disparadorDelProyectil == DisparadorDelProyectil.Jugador2)
                    {
                        if (cuadrilla.player.enumsPlayers.numberPlayer != EnumsPlayers.NumberPlayer.player2)
                        {
                            CheckGrid(cuadrilla);
                            cuadrillaColision = cuadrilla;
                            CreateGaseosas();
                        }
                    }
                    else if (disparadorDelProyectil == DisparadorDelProyectil.Enemigo)
                    {
                        if (cuadrilla.enemy == null)
                        {
                            CheckGrid(cuadrilla);
                            cuadrillaColision = cuadrilla;
                            CreateGaseosas();
                        }
                    }
                }
                if (cuadrilla.GetStateCuadrilla() == Cuadrilla.StateCuadrilla.Ocupado)
                {
                    if (cuadrilla.enemy != null)
                    {
                        if (disparadorDelProyectil == DisparadorDelProyectil.Jugador1)
                        {
                            if (cuadrilla.enemy.enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.MoveToPointCombat && cuadrilla.enemy.enumsEnemy.GetMovement() != EnumsEnemy.Movimiento.MoveToPointDeath)
                            {
                                cuadrilla.enemy.spriteEnemy.ActualSprite = SpriteEnemy.SpriteActual.RecibirDanio;
                                cuadrilla.enemy.life = cuadrilla.enemy.life - damage;
                                //AUMENTO XP PARA EL ATAQUE ESPECIAL
                                PLAYER1.SetXpActual(PLAYER1.GetXpActual() + PLAYER1.xpForHit);
                                CheckGrid(cuadrilla);
                                cuadrillaColision = grid.matrizCuadrilla[grid.GetCuadrilla_columnas() - 1][cuadrilla.enemy.structsEnemys.dataEnemy.columnaActual];
                                CreateGaseosas();
                            }
                        }
                    }
                    if (cuadrilla.player != null)
                    {

                        if (disparadorDelProyectil == DisparadorDelProyectil.Enemigo)
                        {
                            cuadrilla.player.PD.lifePlayer = cuadrilla.player.PD.lifePlayer - damage;
                            cuadrilla.player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.RecibirDanio;
                            CheckGrid(cuadrilla);
                            cuadrillaColision = grid.matrizCuadrilla[grid.GetCuadrilla_columnas() - 1][cuadrilla.player.structsPlayer.dataPlayer.columnaActual];
                            CreateGaseosas();
                        }
                        if (PLAYER1 != null)
                        {
                            if (disparadorDelProyectil == DisparadorDelProyectil.Jugador1
                                || cuadrilla.player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player1)
                            {
                                //AUMENTO XP PARA EL ATAQUE ESPECIAL
                                PLAYER1.SetXpActual(PLAYER1.GetXpActual() + PLAYER1.xpForHit);
                                cuadrilla.player.SetEnableCounterAttack(true);

                                cuadrilla.player.PD.lifePlayer = cuadrilla.player.PD.lifePlayer - damage;
                                cuadrilla.player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.RecibirDanio;
                                CheckGrid(cuadrilla);
                                cuadrillaColision = grid.matrizCuadrilla[grid.GetCuadrilla_columnas() - 1][cuadrilla.player.structsPlayer.dataPlayer.columnaActual];
                                CreateGaseosas();
                            }
                        }
                        if (PLAYER2 != null)
                        {
                            if (disparadorDelProyectil == DisparadorDelProyectil.Jugador2 ||
                            cuadrilla.player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player1 ||
                            PLAYER2 != null)
                            {
                                //AUMENTO XP PARA EL ATAQUE ESPECIAL
                                PLAYER2.SetXpActual(PLAYER2.GetXpActual() + PLAYER2.xpForHit);
                                cuadrilla.player.SetEnableCounterAttack(true);
                                cuadrilla.player.PD.lifePlayer = cuadrilla.player.PD.lifePlayer - damage;
                                cuadrilla.player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.RecibirDanio;
                                CheckGrid(cuadrilla);
                                cuadrillaColision = grid.matrizCuadrilla[grid.GetCuadrilla_columnas() - 1][cuadrilla.player.structsPlayer.dataPlayer.columnaActual];
                                CreateGaseosas();

                            }
                        }
                    }
                }
            }
        }
    }
}
