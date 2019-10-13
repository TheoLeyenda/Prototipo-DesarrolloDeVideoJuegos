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
            List<Cuadrilla> cuadrillasAbajo = new List<Cuadrilla>();
            cuadrillasAbajo.Add(cuadrillaColision);
            int num;
            GameObject go = null;
            Gaseosa gaseosa = null;
            switch (cuadrillaColision.posicionCuadrilla)
            {
                case Cuadrilla.PosicionCuadrilla.CuadrillaBajaIzquierda:
                    cuadrillasAbajo.Add(grid.matrizCuadrilla[grid.baseGrild][1]);
                    break;
                case Cuadrilla.PosicionCuadrilla.CuadrillaBajaCentral:
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
                    break;
                case Cuadrilla.PosicionCuadrilla.CuadrillaBajaDerecha:
                    cuadrillasAbajo.Add(grid.matrizCuadrilla[grid.baseGrild][1]);
                    break;
            }
            for (int i = 0; i < cuadrillasAbajo.Count; i++)
            {
                go = poolGaseosa.GetObject();
                gaseosa = go.GetComponent<Gaseosa>();
                gaseosa.transform.position = cuadrillasAbajo[i].transform.position;
            }
            timeLife = 0;
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.tag == "Cuadrilla")
            {
                Cuadrilla cuadrilla = collision.GetComponent<Cuadrilla>();
                
                if (cuadrilla.enemy == null && cuadrilla.player == null || cuadrilla.enemy != null && cuadrilla.player != null)
                {
                    return;
                }
                if (cuadrilla.GetStateCuadrilla() == Cuadrilla.StateCuadrilla.Ocupado)
                {
                    cuadrillaColision = cuadrilla;
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
                                CreateGaseosas();

                            }
                        }
                    }
                }
            }
        }
    }
}
