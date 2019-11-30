using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototipo_2 {
    public class SpritePlayer : SpriteCharacter
    {
        public Player player;
        
        [System.Serializable]
        public class ElementsSprites
        {
            public Animation animation;
            public string name;

        }
        
        public List<ElementsSprites> Animations;
        
        private void Start()
        {
            animator = GetComponent<Animator>();
            ActualSprite = SpriteActual.Parado;
            auxDelaySpriteRecibirDanio = delaySpriteRecibirDanio;
            auxDelaySpriteContraAtaque = delaySpriteContraAtaque;
        }
        
        public void Update()
        {
            CheckActualSprite();
        }
      
        public void CheckActualSprite()
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
            if (player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player1)
            {
                if (player.GetInputManager() != null)
                {
                    if (player.GetInputManager().GetEnableMovementPlayer1())
                    {
                        switch (ActualSprite)
                        {
                            case SpriteActual.Parado:
                                //if (player.enumsPlayers.specialAttackEquipped != EnumsPlayers.SpecialAttackEquipped.DisparoDeCarga)
                                //{
                                    PlayAnimation("Parado protagonista");
                                //}
                                break;
                            case SpriteActual.ParadoDefensa:
                                PlayAnimation("Parado Defensa protagonista");
                                break;
                            case SpriteActual.Salto:
                                PlayAnimation("Salto protagonista");
                                break;
                            case SpriteActual.SaltoDefensa:
                                PlayAnimation("Salto Defensa protagonista");
                                break;
                            case SpriteActual.MoverAdelante:
                                PlayAnimation("Mover Adelante protagonista");
                                break;
                            case SpriteActual.MoverAtras:
                                PlayAnimation("Mover Atras protagonista");
                                break;
                            case SpriteActual.RecibirDanio:
                                PlayAnimation("Recibir Danio protagonista");
                                break;
                            case SpriteActual.Agachado:
                                PlayAnimation("Agachado protagonista");
                                break;
                            case SpriteActual.AgachadoDefensa:
                                PlayAnimation("Agachado Defensa protagonista");
                                break;
                            case SpriteActual.ContraAtaque:
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
                                break;
                            case SpriteActual.ParadoAtaque:
                                break;
                            case SpriteActual.AgachadoAtaque:
                                break;
                            case SpriteActual.SaltoAtaque:
                                break;
                        }
                    }
                }
            }
            else if (player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player2)
            {
                if (player.GetInputManager() != null)
                {
                    if (player.GetInputManager().GetEnableMovementPlayer2())
                    {
                        switch (ActualSprite)
                        {
                            case SpriteActual.Parado:
                                //if (player.enumsPlayers.specialAttackEquipped != EnumsPlayers.SpecialAttackEquipped.DisparoDeCarga)
                                //{
                                    PlayAnimation("Parado protagonista");
                                //}
                                break;
                            case SpriteActual.ParadoDefensa:
                                PlayAnimation("Parado Defensa protagonista");
                                break;
                            case SpriteActual.Salto:
                                PlayAnimation("Salto protagonista");
                                break;
                            case SpriteActual.SaltoDefensa:
                                PlayAnimation("Salto Defensa protagonista");
                                break;
                            case SpriteActual.MoverAdelante:
                                PlayAnimation("Mover Adelante protagonista");
                                break;
                            case SpriteActual.MoverAtras:
                                PlayAnimation("Mover Atras protagonista");
                                break;
                            case SpriteActual.RecibirDanio:
                                PlayAnimation("Recibir Danio protagonista");
                                break;
                            case SpriteActual.Agachado:
                                PlayAnimation("Agachado protagonista");
                                break;
                            case SpriteActual.AgachadoDefensa:
                                PlayAnimation("Agachado Defensa protagonista");
                                break;
                            case SpriteActual.ContraAtaque:
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
                                break;
                            case SpriteActual.ParadoAtaque:
                                break;
                            case SpriteActual.AgachadoAtaque:
                                break;
                            case SpriteActual.SaltoAtaque:
                                break;
                        }
                    }
                }
            }
        }
        public Animation CheckListAnimations(string nameSprite)
        {
            for (int i = 0; i < Animations.Count; i++)
            {
                if (nameSprite == Animations[i].name)
                {
                    return Animations[i].animation;
                }
            }
            return null;
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
            if (player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player1)
            {
                player.GetInputManager().SetEnableMovementPlayer1(true);
            }
            else if (player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player2)
            {
                player.GetInputManager().SetEnableMovementPlayer2(true);
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
    
}
