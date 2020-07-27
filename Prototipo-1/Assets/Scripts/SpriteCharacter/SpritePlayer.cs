using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpritePlayer : SpriteCharacter
{
    public Player player;

    private void Start()
    {
        animator = GetComponent<Animator>();
        ActualSprite = SpriteActual.Parado;
        auxDelaySpriteRecibirDanio = delaySpriteRecibirDanio;
        auxDelaySpriteContraAtaque = delaySpriteContraAtaque;
    }
        
    public void Update()
    {
        if(!player.myVictory && player.PD.lifePlayer > 0)
            CheckActualSprite();
    }

    public override void CheckActualSprite()
    {
        if (ActualSprite == SpriteActual.RecibirDanio || ActualSprite == SpriteActual.ContraAtaque)
        {
            if (ActualSprite == SpriteActual.RecibirDanio)
            {
                CheckDeleyRecibirDanio();
            }
            if (ActualSprite == SpriteActual.ContraAtaque)
            {
                CheckDeleyContraAtaque();
            }
        }

        if (player.GetInputManager() != null)
        {
            if (player.enableMovementPlayer)
            {
                for (int i = 0; i < Animations.Count; i++)
                {
                    if (ActualSprite.ToString() == Animations[i].nameSpriteActual
                        && ActualSprite != SpriteActual.ContraAtaque)
                    {
                        PlayAnimation(Animations[i].nameAnimation);
                    }
                    else if (ActualSprite == SpriteActual.ContraAtaque)
                    {
                        if (player.GetPlayerPvP() != null)
                        {
                            if (player.GetPlayerPvP().playerSelected == Player_PvP.PlayerSelected.Defensivo)
                            {
                                if (player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.SaltoDefensa
                                    || player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Saltar)
                                {
                                    PlayAnimation("Contra Ataque Salto protagonista");
                                }
                                else if (player.GetIsDuck() || player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.AgacheDefensa
                                    || player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Agacharse)
                                {
                                    PlayAnimation("Contra Ataque Agachado protagonista");
                                }

                                else if (!player.GetIsDuck())
                                {
                                    PlayAnimation("Contra Ataque Parado protagonista");
                                }
                            }
                        }
                        else
                        {
                            PlayAnimation("Contra Ataque Parado protagonista");
                        }
                    }
                }
            }
        }
    }
    public override void InPlayAnimationAttack()
    {
        player.barraDeEscudo.AddPorcentageBar();
    }
    public void PlayerSpecialAttack()
    {
        if (player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player1)
        {
            player.SpecialAttack(Proyectil.DisparadorDelProyectil.Jugador1);
        }
        else if (player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player2)
        {
            player.SpecialAttack(Proyectil.DisparadorDelProyectil.Jugador2);
        }
    }
    public void PlayerAttack()
    {
        if (player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player1)
        {
            player.Attack(Proyectil.DisparadorDelProyectil.Jugador1);
        }
        else if(player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player2)
        {
            player.Attack(Proyectil.DisparadorDelProyectil.Jugador2);
        }
    }
    public void PlayerAttackDown()
    {
        if (player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player1)
        {
            player.AttackDown(Proyectil.DisparadorDelProyectil.Jugador1);
        }
        else if (player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player2)
        {
            player.AttackDown(Proyectil.DisparadorDelProyectil.Jugador2);
        }
    }
    public void EnableMovementPlayer()
    {
        player.enableMovementPlayer = true;
        if (player.enumsPlayers.specialAttackEquipped == EnumsPlayers.SpecialAttackEquipped.DisparoDeCarga)
        {
            player.SetInFuegoEmpieza(false);
            player.eventWise.StartEvent("fuego_termina");
        }
        else if (player.enumsPlayers.specialAttackEquipped == EnumsPlayers.SpecialAttackEquipped.Limusina)
        {

        }
        else if (player.enumsPlayers.specialAttackEquipped == EnumsPlayers.SpecialAttackEquipped.MagicBust) 
        {
        }
    }
    public void SetActualSprite(SpriteActual spriteActual)
    {
        ActualSprite = spriteActual;
    }
    public void SpecialAttackPlayer()
    {
        if (player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player1)
        {
            player.SpecialAttack(Proyectil.DisparadorDelProyectil.Jugador1);
        }
        else if (player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player2)
        {
            player.SpecialAttack(Proyectil.DisparadorDelProyectil.Jugador2);
        }
    }
    public void ParabolaAttackPlayer()
    {
        if (player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player1)
        {
            player.ParabolaAttack(Proyectil.DisparadorDelProyectil.Jugador1);
        }
        else if (player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player2)
        {
            player.ParabolaAttack(Proyectil.DisparadorDelProyectil.Jugador2);
        }
        player.delayAttack = player.GetAuxDelayAttack();
    }

    public void DeathPlayer()
    {
        player.Dead();
    }
}
