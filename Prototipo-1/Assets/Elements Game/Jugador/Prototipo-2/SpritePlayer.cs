using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototipo_2 {
    public class SpritePlayer : MonoBehaviour
    {
        public Player player;
        public SpriteRenderer spriteRenderer;
        private Animator animator;
        [System.Serializable]
        public class ElementsSprites
        {
            public Animation animation;
            public string name;

        }
        public enum SpriteActual
        {
            SaltoAtaque,
            SaltoDefensa,
            Salto,
            ParadoAtaque,
            ParadoDefensa,
            Parado,
            RecibirDanio,
            MoverAtras,
            MoverAdelante,
            AgachadoAtaque,
            AgachadoDefensa,
            Agachado,
            AnimacionAtaque,
            AtaqueEspecial,
            ContraAtaqueParado,
            ContraAtaqueSalto,
            ContraAtaqueAgachado,
            Count,
        }
        public List<ElementsSprites> Animations;
        public SpriteActual ActualSprite;
        public float delaySpriteRecibirDanio;
        private float auxDelaySpriteRecibirDanio;
        public float delaySpriteContraAtaque;
        private float auxDelaySpriteContraAtaque;
        private void Start()
        {
            animator = GetComponent<Animator>();
            ActualSprite = SpriteActual.Parado;
            auxDelaySpriteRecibirDanio = delaySpriteRecibirDanio;
            auxDelaySpriteContraAtaque = delaySpriteContraAtaque;
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            switch (collision.gameObject.tag)
            {
                case "Cuadrilla":
                    Cuadrilla cuadrilla = collision.GetComponent<Cuadrilla>();
                    if (cuadrilla.stateCuadrilla != Cuadrilla.StateCuadrilla.Defendido)
                    {
                        if (cuadrilla.posicionCuadrilla != Cuadrilla.PosicionCuadrilla.CuadrillaBajaCentral
                            && cuadrilla.posicionCuadrilla != Cuadrilla.PosicionCuadrilla.CuadrillaBajaDerecha
                            && cuadrilla.posicionCuadrilla != Cuadrilla.PosicionCuadrilla.CuadrillaBajaIzquierda || !cuadrilla.player.GetIsJumping())
                        {
                            cuadrilla.stateCuadrilla = Cuadrilla.StateCuadrilla.Ocupado;
                        }
                    }
                    //Debug.Log("ENTRE");
                    break;
            }
        }
        public void Update()
        {
            CheckActualSprite();
        }
        public float GetAuxDelaySpriteRecibirDanio()
        {
            return auxDelaySpriteRecibirDanio;
        }
        public void CheckDeleyRecibirDanio()
        {
            if (delaySpriteRecibirDanio > 0)
            {
                delaySpriteRecibirDanio = delaySpriteRecibirDanio - Time.deltaTime;
                ActualSprite = SpriteActual.RecibirDanio;
            }
            else if(delaySpriteRecibirDanio <= 0)
            {
                ActualSprite = SpriteActual.Parado;
            }
        }
        public void CheckDeleyContraAtaque()
        {
            if (delaySpriteContraAtaque > 0)
            {
                delaySpriteContraAtaque = delaySpriteContraAtaque - Time.deltaTime;
            }
            else if (delaySpriteContraAtaque <= 0)
            {
                delaySpriteContraAtaque = auxDelaySpriteContraAtaque;
                ActualSprite = SpriteActual.Parado;
            }
        }
        public void CheckActualSprite()
        {
            if (ActualSprite == SpriteActual.RecibirDanio || ActualSprite == SpriteActual.ContraAtaqueParado)
            {
                if (ActualSprite == SpriteActual.RecibirDanio)
                {
                    CheckDeleyRecibirDanio();
                }
                if (ActualSprite == SpriteActual.ContraAtaqueParado)
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
                        Debug.Log(ActualSprite);
                        switch (ActualSprite)
                        {
                            case SpriteActual.Parado:
                                if (player.enumsPlayers.specialAttackEquipped != EnumsPlayers.SpecialAttackEquipped.DisparoDeCarga)
                                {
                                    PlayAnimation("Parado protagonista");
                                }
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
                            case SpriteActual.ContraAtaqueParado:
                                PlayAnimation("Contra Ataque Parado protagonista");
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
                                if (player.enumsPlayers.specialAttackEquipped != EnumsPlayers.SpecialAttackEquipped.DisparoDeCarga)
                                {
                                    PlayAnimation("Parado protagonista");
                                }
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
                            case SpriteActual.ContraAtaqueParado:
                                PlayAnimation("Contra Ataque Parado protagonista");
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
        public void PlayAnimation(string nameAnimation)
        {
            if (animator != null)
            {
                animator.Play(nameAnimation);
            }
        }
        public void InPlayAnimationAttack()
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
                //Debug.Log("DISPARO EL PLAYER 1");
                player.Attack(Proyectil.DisparadorDelProyectil.Jugador1);
            }
            else if(player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player2)
            {
                //Debug.Log("DISPARO EL PLAYER 2");
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
        public Animator GetAnimator()
        {
            return animator;
        }
    }
    
}
