using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototipo_2 {
    public class InputManager : MonoBehaviour
    {
        public string PauseButton;
        public bool enableAnalogic;
        public Player player1;
        public Player_PvP player1_PvP;
        public Player player2;
        public Player_PvP player2_PvP;
        public bool FindPlayersAndPlayers_PvP;
        private bool inPause = false;

        private bool moveHorizontalPlayer1;
        private bool moveVerticalPlayer1;
        private bool moveVerticalPlayer2;
        private bool moveHorizontalPlayer2;

        private bool enableMovementPlayer1;
        private bool enableMovementPlayer2;

        private void Start()
        {
            enableMovementPlayer1 = true;
            enableMovementPlayer2 = true;
            if (FindPlayersAndPlayers_PvP)
            {
                player1 = GameObject.Find("Player1").GetComponent<Player>();
                player1_PvP = player1.gameObject.GetComponent<Player_PvP>();

                player2 = GameObject.Find("Player2").GetComponent<Player>();
                player2_PvP = player2.gameObject.GetComponent<Player_PvP>();
            }
            moveHorizontalPlayer1 = true;
            moveVerticalPlayer1 = true;
            moveHorizontalPlayer2 = true;
            moveVerticalPlayer2 = true;
        }

        void Update()
        {
            CheckPauseButton_P1();
            CheckPauseButton_P2();
            if (Time.timeScale == 1)
            {
                if (player1 != null && player1.gameObject.activeSelf)
                {
                    if (!InputPlayerController.GetInputButton("DeffenseButton_P1") || player1.barraDeEscudo.nededBarMaxPorcentage)
                    {
                        player1.barraDeEscudo.AddPorcentageBar();
                        if (player1.barraDeEscudo.GetValueShild() <= player1.barraDeEscudo.porcentageNededForDeffence)
                        {
                            player1.barraDeEscudo.SetEnableDeffence(false);
                        }
                    }
                    if (enableMovementPlayer1)
                    {
                        CheckInputPlayer(player1);
                        if (player1.PD.lifePlayer > 0)
                        {
                            CheckSpritePlayer(player1);
                        }
                    }
                    else
                    {
                        if (player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Saltar && !inPause)
                        {
                            player1.SetControllerJoystick(true);
                            player1.MovementJump();
                            moveVerticalPlayer1 = false;
                            player1.SetIsDuck(false);
                        }
                    }

                }
                if (player2 != null && player2.gameObject.activeSelf)
                {
                    if (!InputPlayerController.GetInputButton("DeffenseButton_P2") || player2.barraDeEscudo.nededBarMaxPorcentage)
                    {
                        player2.barraDeEscudo.AddPorcentageBar();
                        if (player2.barraDeEscudo.GetValueShild() <= player2.barraDeEscudo.porcentageNededForDeffence)
                        {
                            player2.barraDeEscudo.SetEnableDeffence(false);
                        }
                    }
                    if (enableMovementPlayer2)
                    {
                        CheckInputPlayer(player2);
                        if (player2.PD.lifePlayer > 0)
                        {
                            CheckSpritePlayer(player2);
                        }
                    }
                    else
                    {
                        if (player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Saltar && !inPause)
                        {
                            player2.SetControllerJoystick(true);
                            player2.MovementJump();
                            moveVerticalPlayer1 = false;
                            player2.SetIsDuck(false);
                        }
                    }
                }
                inPause = false;
            }
        }

        public void CheckInputPlayer(Player player)
        {
            if (player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player1)
            {
                CheckParabolaAttack("ParabolaAttack_P1", "DeffenseButton_P1", ref enableMovementPlayer1, player);
                CheckVerticalUp("Vertical", "JumpButton_P1", "Vertical_Analogico", ref moveVerticalPlayer1, player);
                CheckVerticalDown("Vertical", "Vertical_Analogico", player);
                CheckVerticalCero("Vertical", "Vertical_Analogico", ref moveVerticalPlayer1, player);
                CheckHorizontalLeft("Horizontal", "Horizontal_Analogico", ref moveHorizontalPlayer1, player);
                CheckHorizontalRight("Horizontal", "Horizontal_Analogico", ref moveHorizontalPlayer1, player);
                CheckHorizontalCero("Horizontal", "Horizontal_Analogico", ref moveHorizontalPlayer1);
                CheckAttackButton("AttackButton_P1", "DeffenseButton_P1", "Vertical", "Vertical_Analogico" , ref enableMovementPlayer1, player);
                CheckDeffenceButton("AttackButton_P1", "DeffenseButton_P1", player1_PvP, player);
                CheckSpecialAttackButton("SpecialAttackButton_P1",ref enableMovementPlayer1, player);
            }
            else if (player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player2)
            {
                CheckParabolaAttack("ParabolaAttack_P2", "DeffenseButton_P2", ref enableMovementPlayer2, player);
                CheckVerticalUp("Vertical_P2", "JumpButton_P2", "Vertical_Analogico_P2", ref moveVerticalPlayer2, player);
                CheckVerticalDown("Vertical_P2", "Vertical_Analogico_P2", player);
                CheckVerticalCero("Vertical_P2", "Vertical_Analogico_P2", ref moveVerticalPlayer2, player);
                CheckHorizontalLeft("Horizontal_P2", "Horizontal_Analogico_P2", ref moveHorizontalPlayer2, player);
                CheckHorizontalRight("Horizontal_P2", "Horizontal_Analogico_P2", ref moveHorizontalPlayer2, player);
                CheckHorizontalCero("Horizontal_P2", "Horizontal_Analogico_P2", ref moveHorizontalPlayer2);
                CheckAttackButton("AttackButton_P2", "DeffenseButton_P2", "Vertical_P2", "Vertical_Analogico_P2", ref enableMovementPlayer2, player);
                CheckDeffenceButton("AttackButton_P2", "DeffenseButton_P2", player2_PvP,player);
                CheckSpecialAttackButton("SpecialAttackButton_P2",ref enableMovementPlayer2, player);
            }
        }

        public void CheckSpritePlayer(Player player)
        {
            if (player.spritePlayerActual.ActualSprite == SpritePlayer.SpriteActual.RecibirDanio || player.spritePlayerActual.ActualSprite == SpritePlayer.SpriteActual.ContraAtaque)
            {
                if (player.spritePlayerActual.ActualSprite == SpritePlayer.SpriteActual.RecibirDanio)
                {
                    player.spritePlayerActual.CheckDeleyRecibirDanio();
                }
                else
                {
                    player.spritePlayerActual.CheckDeleyContraAtaque();
                }
            }
            else
            {
                if (player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player1)
                {
                    CheckSpriteParado("Vertical", "SpecialAttackButton_P1", player);
                    CheckSpriteMoverDerecha("Horizontal", "Horizontal_Analogico", "Vertical", moveHorizontalPlayer1, player);
                    CheckSpriteMoverIzquierda("Horizontal", "Horizontal_Analogico", "Vertical", moveHorizontalPlayer1, player);
                    CheckSpritesSalto("Vertical", "Vertical_Analogico", "Horizontal", "AttackButton_P1", "DeffenseButton_P1", "SpecialAttackButton_P1", player);
                    CheckSpritesParado("Horizontal", "AttackButton_P1", "DeffenseButton_P1", player, player1_PvP);
                    CheckSpritesAgachado("Vertical", "Vertical_Analogico", "AttackButton_P1", "DeffenseButton_P1", player);
                }
                else if (player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player2)
                {
                    CheckSpriteParado("Vertical_P2", "SpecialAttackButton_P2", player);
                    CheckSpriteMoverDerecha("Horizontal_P2", "Horizontal_Analogico_P2", "Vertical_P2", moveHorizontalPlayer2, player);
                    CheckSpriteMoverIzquierda("Horizontal_P2", "Horizontal_Analogico_P2", "Vertical_P2", moveHorizontalPlayer2, player);
                    CheckSpritesSalto("Vertical_P2", "Vertical_Analogico_P2", "Horizontal_P2", "AttackButton_P2", "DeffenseButton_P2", "SpecialAttackButton_P2", player);
                    CheckSpritesParado("Horizontal_P2", "AttackButton_P2", "DeffenseButton_P2", player, player2_PvP);
                    CheckSpritesAgachado("Vertical_P2", "Vertical_Analogico_P2", "AttackButton_P2", "DeffenseButton_P2", player);
                }
            }
        }
        public void CheckValueInPause()
        {
            if (!inPause)
            {
                inPause = true;
            }
        }
        public void CheckInPause()
        {
            switch (inPause)
            {
                case true:
                    Time.timeScale = 0;
                    break;
                case false:
                    Time.timeScale = 1;
                    break;
            }
        }

        public void CheckPauseButton_P1()
        {
            if (InputPlayerController.GetInputButtonDown("PauseButton_P1"))
            {
                CheckValueInPause();
                CheckInPause();
            }
        }
        public void CheckParabolaAttack(string inputParabolaAttack, string inputDeffenceButton,ref bool enableMovement, Player player)
        {
            if (InputPlayerController.GetInputButtonDown(inputParabolaAttack) && player.GetEnableAttack()
                && player.enumsPlayers.movimiento != EnumsPlayers.Movimiento.MoverAdelante
                && player.enumsPlayers.movimiento != EnumsPlayers.Movimiento.MoverAtras
                && !InputPlayerController.GetInputButton(inputDeffenceButton))
            {
                if (player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Saltar)
                {
                    //ANIMACION ATAQUE EN PARABOLA SALTANDO
                    player.spritePlayerActual.PlayAnimation("Ataque Parabola Salto protagonista");
                    enableMovement = false;
                }
                else
                {
                    if (!player.GetIsDuck())
                    {
                        //ANIMACION ATAQUE EN PARABOLA PARADO
                        player.spritePlayerActual.PlayAnimation("Ataque Parabola protagonista");
                        enableMovement = false;
                    }
                    else if (player.GetIsDuck())
                    {
                        //ANIMACION ATAQUE EN PARABOLA AGACHADO
                        player.spritePlayerActual.PlayAnimation("Ataque Parabola Agachado protagonista");
                        enableMovement = false;
                    }
                }
            }
        }

        public void CheckVerticalUp(string inputVertical, string inputJumpButton, string VerticalAnalog, ref bool moveVerticalPlayer, Player player)
        {
            bool movimientoVerticalHabilitado = false;

            if (enableAnalogic)
            {
                movimientoVerticalHabilitado = ((player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo && (InputPlayerController.GetInputAxis(inputVertical) > 0 || InputPlayerController.GetInputButtonDown(inputJumpButton) || InputPlayerController.GetInputAxis(VerticalAnalog) < -0.9f) && moveVerticalPlayer) || player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Saltar);
            }
            else
            {
                movimientoVerticalHabilitado = ((player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo && (InputPlayerController.GetInputAxis(inputVertical) > 0 || InputPlayerController.GetInputButtonDown(inputJumpButton)) && moveVerticalPlayer) || player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Saltar);
            }
            if (movimientoVerticalHabilitado && !inPause)
            {
                player.SetControllerJoystick(true);
                player.MovementJump();
                moveVerticalPlayer = false;
                player.SetIsDuck(false);
            }
        }

        public void CheckVerticalDown(string inputVertical, string inputVerticalAnalog, Player player)
        {
            bool movimientoVerticalHabilitado = false;
            if (enableAnalogic)
            {

                movimientoVerticalHabilitado = (((InputPlayerController.GetInputAxis(inputVertical) < 0 || InputPlayerController.GetInputAxis(inputVerticalAnalog) > 0.5f) && player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo) || player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Agacharse);
            }
            else
            {
                movimientoVerticalHabilitado = ((InputPlayerController.GetInputAxis(inputVertical) < 0 && player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo) || player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Agacharse);
            }
            if  (movimientoVerticalHabilitado)
            { 
                player.SetControllerJoystick(true);
                player.MovementDuck();
                
                player.enumsPlayers.movimiento = EnumsPlayers.Movimiento.Agacharse;
                if (player.spritePlayerActual.ActualSprite != SpritePlayer.SpriteActual.RecibirDanio 
                    && player.spritePlayerActual.ActualSprite != SpritePlayer.SpriteActual.ContraAtaqueAgachado)
                {
                    player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.Agachado;
                }

                player.SetIsDuck(true);
            }
        }

        public void CheckVerticalCero(string inputVertical, string inputVerticalAnalog, ref bool moveVerticalPlayer, Player player)
        {
            if ((InputPlayerController.GetInputAxis(inputVertical) == 0 && ((InputPlayerController.GetInputAxis(inputVerticalAnalog) > -0.9 && InputPlayerController.GetInputAxis(inputVerticalAnalog) < 0.8f))) &&
                (player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Agacharse
                || player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.AgacharseAtaque
                || player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.AgacheDefensa))
            {
                player.enumsPlayers.movimiento = EnumsPlayers.Movimiento.Nulo;
                player.SetIsDuck(false);
            }
            else if (InputPlayerController.GetInputAxis(inputVertical) == 0 && ((InputPlayerController.GetInputAxis(inputVerticalAnalog) > -0.9 && InputPlayerController.GetInputAxis(inputVerticalAnalog) < 0.8)))
            {
                moveVerticalPlayer = true;
                player.SetIsDuck(false);
            }
        }
        
        public void CheckHorizontalLeft(string inputHorizontal, string inputHorizontalAnalog, ref bool moveHorizontalPlayer, Player player)
        {
            bool movimientoHorizontalHabilitado = false;

            if (enableAnalogic)
            {
                movimientoHorizontalHabilitado = (((InputPlayerController.GetInputAxis(inputHorizontal) < 0 || InputPlayerController.GetInputAxis(inputHorizontalAnalog) < -0.9f) && moveHorizontalPlayer && player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo) || player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.MoverAtras);
            }
            else
            {
                movimientoHorizontalHabilitado = ((InputPlayerController.GetInputAxis(inputHorizontal) < 0 && moveHorizontalPlayer && player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo) || player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.MoverAtras);
            }
            if (movimientoHorizontalHabilitado)
            {
                player.SetControllerJoystick(true);
                moveHorizontalPlayer = false;
                player.MovementLeft();
                player.SetIsDuck(false);
            }
        }
        public void CheckHorizontalRight(string inputHorizontal, string inputHorizontalAnalog, ref bool moveHorizontalPlayer, Player player)
        {
            bool movimientoHorizontalHabilitado = false;

            if (enableAnalogic)
            {
                movimientoHorizontalHabilitado = (((InputPlayerController.GetInputAxis(inputHorizontal) > 0 || InputPlayerController.GetInputAxis(inputHorizontalAnalog) > 0.9f) && moveHorizontalPlayer && player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo) || player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.MoverAdelante);
            }
            else
            {
                movimientoHorizontalHabilitado = ((InputPlayerController.GetInputAxis(inputHorizontal) > 0 && moveHorizontalPlayer && player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo) || player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.MoverAdelante);
            }
            if (movimientoHorizontalHabilitado)
            {
                player.SetControllerJoystick(true);
                moveHorizontalPlayer = false;
                player.MovementRight();
                player.SetIsDuck(false);
            }
        }

        public void CheckHorizontalCero(string inputHorizontal, string inputHorizontalAnalog, ref bool moveHorizontalPlayer)
        {
            if (InputPlayerController.GetInputAxis(inputHorizontal) == 0 && (InputPlayerController.GetInputAxis(inputHorizontalAnalog) > -0.9f && InputPlayerController.GetInputAxis(inputHorizontalAnalog) < 0.9f))
            {
                moveHorizontalPlayer = true;
            }
        }
        
        public void CheckAttackButton(string inputAttackButton, string inputDeffenseButton, string inputVertical, string inputVerticalAnalogico, ref bool enableMovementPlayer, Player player)
        {
            
            if (!inPause)
            {
                if (InputPlayerController.GetInputButtonDown(inputAttackButton) && player.GetEnableAttack()
                    && player.enumsPlayers.movimiento != EnumsPlayers.Movimiento.MoverAdelante
                    && player.enumsPlayers.movimiento != EnumsPlayers.Movimiento.MoverAtras
                    && !InputPlayerController.GetInputButton(inputDeffenseButton))
                {
                    player.SetControllerJoystick(true);
                    if (player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Saltar && (InputPlayerController.GetInputAxis(inputVertical) < 0
                        || (enableAnalogic && InputPlayerController.GetInputAxis(inputVerticalAnalogico) > 0.5f)))
                    {
                        player.spritePlayerActual.PlayAnimation("Ataque Abajo Salto protagonista");
                        enableMovementPlayer = false;
                    }
                    else if (player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Saltar && InputPlayerController.GetInputAxis(inputVertical) >= 0)
                    {
                        player.spritePlayerActual.PlayAnimation("Ataque Salto protagonista");
                        enableMovementPlayer = false;
                    }
                    else
                    {
                        if (!player.GetIsDuck())
                        {
                            player.spritePlayerActual.PlayAnimation("Ataque protagonista");
                            enableMovementPlayer = false;
                        }
                        else if (player.GetIsDuck())
                        {
                            player.spritePlayerActual.PlayAnimation("Ataque Agachado protagonista");
                            enableMovementPlayer = false;
                        }
                    }
                }
            }
        }
        
        public void CheckDeffenceButton(string inputAttackButton, string inputDeffenseButton, Player_PvP player_PvP, Player player)
        {
            if (!InputPlayerController.GetInputButton(inputAttackButton))
            {
                if (InputPlayerController.GetInputButton(inputDeffenseButton) 
                    && player.barraDeEscudo.GetValueShild() > player.barraDeEscudo.porcentageNededForDeffence
                    && player.barraDeEscudo.GetEnableDeffence())
                {
                    player.SetControllerJoystick(true);
                    player.Deffence();
                    player.boxColliderAgachado.state = BoxColliderController.StateBoxCollider.Defendido;
                    player.boxColliderParado.state = BoxColliderController.StateBoxCollider.Defendido;
                    player.boxColliderSaltando.state = BoxColliderController.StateBoxCollider.Defendido;
                    player.boxColliderSprite.state = BoxColliderController.StateBoxCollider.Defendido;
                }
                else
                {
                    if (player_PvP != null)
                    {
                        if (player_PvP.playerSelected == Player_PvP.PlayerSelected.Defensivo)
                        {
                            player_PvP.stateDeffence = Player_PvP.StateDeffence.CounterAttackDeffense;
                            player_PvP.delayCounterAttackDeffense = player_PvP.auxDelayCounterAttackDeffense;
                            player.spritePlayerActual.spriteRenderer.color = Color.white;
                        }
                    }
                    player.boxColliderAgachado.state = BoxColliderController.StateBoxCollider.Normal;
                    player.boxColliderParado.state = BoxColliderController.StateBoxCollider.Normal;
                    player.boxColliderSaltando.state = BoxColliderController.StateBoxCollider.Normal;
                    player.boxColliderSprite.state = BoxColliderController.StateBoxCollider.Normal;
                    player.GetPlayerPvP().stateDeffence = Player_PvP.StateDeffence.NormalDeffense;
                    player.barraDeEscudo.AddPorcentageBar();
                    if (player.barraDeEscudo.GetValueShild() <= player.barraDeEscudo.porcentageNededForDeffence)
                    {
                        player.barraDeEscudo.SetEnableDeffence(false);
                    }
                }
            }
        }
        
        public void CheckSpecialAttackButton(string inputSpecialAttackButton, ref bool enableMovementPlayer, Player player)
        {
            if (player.GetEnableSpecialAttack())
            {
                if (InputPlayerController.GetInputButtonDown(inputSpecialAttackButton))
                {
                    if (player.enumsPlayers.specialAttackEquipped != EnumsPlayers.SpecialAttackEquipped.ProyectilImparable)
                    {
                        if (player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Saltar
                            || player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.SaltoAtaque
                            || player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.SaltoDefensa)
                        {
                            player.spritePlayerActual.PlayAnimation("Ataque Especial protagonista");//ANIMACION DE ATAQUE ESPECIAL SALTANDO
                            enableMovementPlayer = false;
                        }
                        else if (player.GetIsDuck())
                        {
                            player.spritePlayerActual.PlayAnimation("Ataque Especial protagonista");//ANIMACION DE ATAQUE ESPECIAL AGACHADO
                            enableMovementPlayer = false;
                        }
                        else
                        {
                            player.spritePlayerActual.PlayAnimation("Ataque Especial protagonista");//ANIMACION DE ATAQUE ESPECIAL PARADO
                            enableMovementPlayer = false;
                        }
                    }
                    else
                    {
                        if (player.enumsPlayers.movimiento != EnumsPlayers.Movimiento.Saltar
                            && player.enumsPlayers.movimiento != EnumsPlayers.Movimiento.SaltoAtaque
                            && player.enumsPlayers.movimiento != EnumsPlayers.Movimiento.SaltoDefensa)
                        {
                            player.spritePlayerActual.PlayAnimation("Ataque Especial protagonista");//ANIMACION DE ATAQUE ESPECIAL SALTANDO
                            enableMovementPlayer = false;
                        }
                        else
                        {
                            player.spritePlayerActual.PlayAnimation("Salto protagonista");
                        }
                    }
                }
            }
        }

        public void CheckSpriteParado(string inputVertical, string inputSpecialAttackButton, Player player)
        {
            if (InputPlayerController.GetInputAxis(inputVertical) == 0 && !InputPlayerController.GetInputButtonDown(inputSpecialAttackButton))
            {
                player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.Parado;
            }
        }
        public void CheckSpriteMoverDerecha(string inputHorizontal , string inputHorizontalAnalog, string inputVertical, bool moveHorizontalPlayer, Player player)
        {

            bool cambioSpriteHabilitado = false;
            if (enableAnalogic)
            {
                cambioSpriteHabilitado = (((InputPlayerController.GetInputAxis(inputHorizontal) > 0 || InputPlayerController.GetInputAxis(inputHorizontalAnalog) > 0.9f) && InputPlayerController.GetInputAxis(inputVertical) == 0 && moveHorizontalPlayer) || player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.MoverAdelante);
            }
            else
            {

                cambioSpriteHabilitado = ((InputPlayerController.GetInputAxis(inputHorizontal) > 0 && InputPlayerController.GetInputAxis(inputVertical) == 0 && moveHorizontalPlayer) || player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.MoverAdelante);
            }
            if (cambioSpriteHabilitado)
            {
                if (player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player1)
                {
                    player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.MoverAdelante;
                }
                else if (player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player2)
                {
                    player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.MoverAtras;
                }
            }
        }

        public void CheckSpriteMoverIzquierda(string inputHorizontal, string inputHorizontalAnalog, string inputVertical,bool moveHorizontalPlayer, Player player)
        {
            bool cambioSpriteHabilitado = false;

            if (enableAnalogic)
            {
                cambioSpriteHabilitado = (((InputPlayerController.GetInputAxis(inputHorizontal) < 0 || InputPlayerController.GetInputAxis(inputHorizontalAnalog) < -0.9f) && InputPlayerController.GetInputAxis(inputVertical) == 0 && moveHorizontalPlayer) || player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.MoverAtras);
            }
            else
            {
                cambioSpriteHabilitado = ((InputPlayerController.GetInputAxis(inputHorizontal) < 0 && InputPlayerController.GetInputAxis(inputVertical) == 0 && moveHorizontalPlayer) || player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.MoverAtras);
            }
            if (cambioSpriteHabilitado)
            {
                if (player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player1)
                {
                    player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.MoverAtras;
                }
                else if (player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player2)
                {
                    player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.MoverAdelante;
                }
            }
        }
        
        public void CheckSpritesSalto(string inputVertical, string inputVertical_Analogico, string inputHorizontal, string inputAttackButton, string inputDeffenseButton, string inputSpecialAttackButton, Player player) 
        {
            bool spriteSaltoHabilitado = false;
            if (enableAnalogic)
            {
                spriteSaltoHabilitado = (((InputPlayerController.GetInputAxis(inputVertical) > 0 || InputPlayerController.GetInputAxis(inputVertical_Analogico) < -0.9f) && player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo && InputPlayerController.GetInputAxis(inputHorizontal) == 0) || player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Saltar || player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.SaltoDefensa || player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.SaltoAtaque);
            }
            else
            {
                spriteSaltoHabilitado = ((InputPlayerController.GetInputAxis(inputVertical) > 0 && InputPlayerController.GetInputAxis(inputHorizontal) == 0 && player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo) || player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Saltar || player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.SaltoDefensa || player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.SaltoAtaque);
            }

            if (spriteSaltoHabilitado)
            {
                if (InputPlayerController.GetInputButton(inputAttackButton))
                {
                    player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.SaltoAtaque;
                }
                else if (InputPlayerController.GetInputButton(inputDeffenseButton)
                     && player.barraDeEscudo.GetValueShild() > player.barraDeEscudo.porcentageNededForDeffence
                    && player.barraDeEscudo.GetEnableDeffence())
                {
                    player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.SaltoDefensa;
                }
                else if (InputPlayerController.GetInputButtonDown(inputSpecialAttackButton))
                {
                    //SPRITE O ANIMACION ATAQUE ESPECIAL JUGADOR.
                }
                else
                {
                    player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.Salto;
                }
                if (player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo)
                {
                    player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.Parado;
                }
            }
        }
        
        public void CheckSpritesParado(string inputHorizontal, string inputAttackButton, string inputDeffenseButton, Player player, Player_PvP player_PvP) 
        {
            if (player.enumsPlayers.movimiento != EnumsPlayers.Movimiento.Saltar
                    && player.enumsPlayers.movimiento != EnumsPlayers.Movimiento.Agacharse && InputPlayerController.GetInputAxis(inputHorizontal) == 0)
            {
                if (InputPlayerController.GetInputButton(inputAttackButton))
                {
                    player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.ParadoAtaque;
                }
                else if (InputPlayerController.GetInputButton(inputDeffenseButton) 
                    && player.barraDeEscudo.GetValueShild() > player.barraDeEscudo.porcentageNededForDeffence
                    && player.barraDeEscudo.GetEnableDeffence())
                {
                    player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.ParadoDefensa;
                    if (player_PvP != null)
                    {
                        player_PvP.playerState = Player_PvP.State.Defendido;
                    }
                }
                else
                {
                    player.spritePlayerActual.delaySpriteRecibirDanio = player.spritePlayerActual.GetAuxDelaySpriteRecibirDanio();
                }
            }
        }

        public void CheckSpritesAgachado(string inputVertical, string inputVerticalAnalog, string inputAttackButton, string inputDeffenseButton, Player player) 
        {
            bool spriteAgachadoHabilitado = false;
            if (enableAnalogic)
            {
                spriteAgachadoHabilitado = (InputPlayerController.GetInputAxis(inputVertical) < 0 || InputPlayerController.GetInputAxis(inputVerticalAnalog) > 0.5f) && player.enumsPlayers.movimiento != EnumsPlayers.Movimiento.Saltar;
            }
            else
            {
                spriteAgachadoHabilitado = (InputPlayerController.GetInputAxis(inputVertical) < 0 && player.enumsPlayers.movimiento != EnumsPlayers.Movimiento.Saltar);
            }
            if (spriteAgachadoHabilitado && player.GetIsDuck())
            {
                if (InputPlayerController.GetInputButton(inputAttackButton))
                {
                    player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.AgachadoAtaque;
                }
                else if (InputPlayerController.GetInputButton(inputDeffenseButton)
                     && player.barraDeEscudo.GetValueShild() > player.barraDeEscudo.porcentageNededForDeffence
                    && player.barraDeEscudo.GetEnableDeffence())
                {
                    player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.AgachadoDefensa;
                }
                else 
                {
                    player.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.Agachado;
                }
            }
        }
        
        public void CheckPauseButton_P2()
        {
            if (InputPlayerController.GetInputButtonDown("PauseButton_P2"))
            {
                CheckValueInPause();
                CheckInPause();
            }
        }

        public void SetEnableMovementPlayer1(bool enableMovement)
        {
            enableMovementPlayer1 = enableMovement;
        }
        public void SetEnableMovementPlayer2(bool enableMovement)
        {
            enableMovementPlayer2 = enableMovement;
        }
        public bool GetEnableMovementPlayer1()
        {
            return enableMovementPlayer1;
        }
        public bool GetEnableMovementPlayer2()
        {
            return enableMovementPlayer2;
        }
    }
}
