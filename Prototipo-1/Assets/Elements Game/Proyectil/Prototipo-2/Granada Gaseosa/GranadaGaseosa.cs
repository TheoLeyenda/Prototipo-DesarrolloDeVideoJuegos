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
        public List<Sprite> propsProyectilGaseosa;
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
            int random = Random.Range(0, propsProyectilGaseosa.Count);
            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = propsProyectilGaseosa[random];
            }
            OnParabola();
        }
        
        public void CreateGaseosas(int cantProyectiles)
        {
            if (cuadrillaColision != null)
            {
                cuadrillasAbajo.Clear();
                cuadrillasAbajo.Add(cuadrillaColision);
                int num;
                GameObject go = null;
                Gaseosa gaseosa = null;
                if (cantProyectiles == 1)
                {
                    for (int i = 0; i < cuadrillasAbajo.Count; i++)
                    {
                        go = poolGaseosa.GetObject();
                        gaseosa = go.GetComponent<Gaseosa>();
                        gaseosa.disparadorDelProyectil = disparadorDelProyectil;
                        gaseosa.transform.position = cuadrillasAbajo[i].transform.position;
                    }
                }
                else if (cantProyectiles == 2)
                {
                    if (grid != null)
                    {
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
                }
                else if (cantProyectiles == 3)
                {
                    cuadrillasAbajo.Clear();
                    if (grid != null)
                    {
                        for (int i = 0; i <= grid.baseGrild; i++)
                        {
                            cuadrillasAbajo.Add(grid.matrizCuadrilla[grid.baseGrild][i]);
                        }
                        for (int i = 0; i < cuadrillasAbajo.Count; i++)
                        {
                            go = poolGaseosa.GetObject();
                            gaseosa = go.GetComponent<Gaseosa>();
                            gaseosa.disparadorDelProyectil = disparadorDelProyectil;
                            gaseosa.transform.position = cuadrillasAbajo[i].transform.position;
                        }
                    }
                }
                gameObject.SetActive(false);
                timeLife = 0;
            }
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
                    if (cuadrilla.player != null)
                    {
                        if (disparadorDelProyectil == DisparadorDelProyectil.Jugador1)
                        {
                            if (cuadrilla.player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player2)
                            {
                                CheckGrid(cuadrilla);
                                cuadrillaColision = cuadrilla;
                                if (PLAYER1 != null)
                                {
                                    if (PLAYER1.GetPlayerPvP().playerSelected == Player_PvP.PlayerSelected.Protagonista)
                                    {
                                        CreateGaseosas(3);
                                    }
                                    else if (PLAYER1.GetPlayerPvP().playerSelected == Player_PvP.PlayerSelected.Balanceado)
                                    {
                                        CreateGaseosas(2);
                                    }
                                    else
                                    {
                                        CreateGaseosas(1);
                                    }
                                }
                            }
                        }
                        else if (disparadorDelProyectil == DisparadorDelProyectil.Jugador2)
                        {
                            if (cuadrilla.player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player1)
                            {
                                CheckGrid(cuadrilla);
                                cuadrillaColision = cuadrilla;
                                if (PLAYER2 != null)
                                {
                                    if (PLAYER2.GetPlayerPvP().playerSelected == Player_PvP.PlayerSelected.Protagonista)
                                    {
                                        CreateGaseosas(3);
                                    }
                                    else if (PLAYER2.GetPlayerPvP().playerSelected == Player_PvP.PlayerSelected.Balanceado)
                                    {
                                        CreateGaseosas(2);
                                    }
                                    else
                                    {
                                        CreateGaseosas(1);
                                    }
                                }
                            }
                        }
                        if (disparadorDelProyectil == DisparadorDelProyectil.Enemigo)
                        {

                            CheckGrid(cuadrilla);
                            cuadrillaColision = cuadrilla;
                            CreateGaseosas(2);

                        }
                    }
                    if (cuadrilla.enemy != null)
                    {
                        if (disparadorDelProyectil == DisparadorDelProyectil.Jugador1)
                        {
                            CheckGrid(cuadrilla);
                            cuadrillaColision = cuadrilla;
                            if (PLAYER1 != null)
                            {
                                if (PLAYER1.GetPlayerPvP().playerSelected == Player_PvP.PlayerSelected.Protagonista)
                                {
                                    CreateGaseosas(3);
                                }
                                else if (PLAYER1.GetPlayerPvP().playerSelected == Player_PvP.PlayerSelected.Balanceado)
                                {
                                    CreateGaseosas(2);
                                }
                                else
                                {
                                    CreateGaseosas(1);
                                }
                            }
                        }
                        else if (disparadorDelProyectil == DisparadorDelProyectil.Jugador2)
                        {
                            Debug.Log("ENTRE");
                            CheckGrid(cuadrilla);
                            cuadrillaColision = cuadrilla;
                            if (PLAYER2 != null)
                            {
                                if (PLAYER2.GetPlayerPvP().playerSelected == Player_PvP.PlayerSelected.Protagonista)
                                {
                                    CreateGaseosas(3);
                                }
                                else if (PLAYER2.GetPlayerPvP().playerSelected == Player_PvP.PlayerSelected.Balanceado)
                                {
                                    CreateGaseosas(2);
                                }
                                else
                                {
                                    CreateGaseosas(1);
                                }
                            }
                            
                        }
                        
                    }
                }
               
            }
        }
    }
}
