using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototipo_2
{
    public class ProyectilInparable : Proyectil
    {
        // Start is called before the first frame update
        void Start()
        {
            ShootForward();
            timeLife = auxTimeLife;
            if (GameManager.instanceGameManager != null)
            {
                gm = GameManager.instanceGameManager;
            }
        }
        private void OnEnable()
        {
            timeLife = auxTimeLife;
        }
        private void Update()
        {
            CheckTimeLife();
        }

        // Update is called once per frame
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.tag == "Cuadrilla")
            {
                Cuadrilla cuadrilla = collision.GetComponent<Cuadrilla>();
                if (cuadrilla.enemy == null && cuadrilla.player == null || cuadrilla.enemy != null && cuadrilla.player != null)
                {
                    return;
                }
                if (cuadrilla.stateCuadrilla != Cuadrilla.StateCuadrilla.Libre)
                {
                    cuadrilla.stateCuadrilla = Cuadrilla.StateCuadrilla.Ocupado;
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
                            }
                        }
                    }
                    if (cuadrilla.player != null)
                    {

                        if (disparadorDelProyectil == DisparadorDelProyectil.Enemigo)
                        {
                            cuadrilla.player.PD.lifePlayer = cuadrilla.player.PD.lifePlayer - damage;
                            cuadrilla.player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.RecibirDanio;
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
                               
                            }
                        }
                    }
                }
            }
        }
    }
}
