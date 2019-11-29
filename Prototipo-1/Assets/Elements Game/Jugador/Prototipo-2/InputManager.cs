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

        bool pressButtonJamp_P1;
        bool movingDown_P1;
        bool agachandose_P1;
        bool movingDownAnalog_P1;

        bool pressButtonJamp_P2;
        bool movingDown_P2;
        bool agachandose_P2;
        bool movingDownAnalog_P2;

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
        public void CheckBools_P1()
        {
            movingDown_P1 = InputPlayerController.GetInputAxis("Vertical") < 0;
            agachandose_P1 = player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Agacharse;
            movingDownAnalog_P1 = InputPlayerController.GetInputAxis("Vertical_Analogico") > 0.5f;
            pressButtonJamp_P1 = InputPlayerController.GetInputButtonDown("JumpButton_P1");

        }

        public void CheckBools_P2()
        {
            movingDown_P2 = InputPlayerController.GetInputAxis("Vertical_P2") < 0;
            agachandose_P2 = player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Agacharse;
            movingDownAnalog_P2 = InputPlayerController.GetInputAxis("Vertical_Analogico_P2") > 0.5f;
            pressButtonJamp_P2 = InputPlayerController.GetInputButtonDown("JumpButton_P2");

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
                        CheckInputPlayer1();
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
                        CheckInputPlayer2();
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
        //----- FUNCIONES Y CONTROLES DEL JUGADOR 1 -----//
        public void CheckPauseButton_P1()
        {
            if (InputPlayerController.GetInputButtonDown("PauseButton_P1"))
            {
                CheckValueInPause();
                CheckInPause();
            }
        }
        public void CheckParabolaAttack_P1()
        {
            if (InputPlayerController.GetInputButtonDown("ParabolaAttack_P1") && player1.GetEnableAttack()
                && player1.enumsPlayers.movimiento != EnumsPlayers.Movimiento.MoverAdelante
                && player1.enumsPlayers.movimiento != EnumsPlayers.Movimiento.MoverAtras
                && !InputPlayerController.GetInputButton("DeffenseButton_P1"))
            {
                if (player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Saltar)
                {
                    //ANIMACION ATAQUE EN PARABOLA SALTANDO
                    player1.spritePlayerActual.PlayAnimation("Ataque Parabola Salto protagonista");
                    enableMovementPlayer1 = false;
                }
                else
                {
                    if (!player1.GetIsDuck())
                    {
                        //ANIMACION ATAQUE EN PARABOLA PARADO
                        player1.spritePlayerActual.PlayAnimation("Ataque Parabola protagonista");
                        enableMovementPlayer1 = false;
                    }
                    else if (player1.GetIsDuck())
                    {
                        //ANIMACION ATAQUE EN PARABOLA AGACHADO
                        player1.spritePlayerActual.PlayAnimation("Ataque Parabola Agachado protagonista");
                        enableMovementPlayer1 = false;
                    }
                }
            }
        }
        public void CheckVerticalUp_P1()
        {
            bool movimientoVerticalHabilitado = false;


            if (enableAnalogic)
            {
                movimientoVerticalHabilitado = ((player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo && (InputPlayerController.GetInputAxis("Vertical") > 0 || pressButtonJamp_P1 || InputPlayerController.GetInputAxis("Vertical_Analogico") < -0.9f) && moveVerticalPlayer1) || player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Saltar);
            }
            else
            {
                movimientoVerticalHabilitado = ((player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo && (InputPlayerController.GetInputAxis("Vertical") > 0 || pressButtonJamp_P1) && moveVerticalPlayer1) || player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Saltar);
            }
            if (movimientoVerticalHabilitado && !inPause)
            {
                player1.SetControllerJoystick(true);
                player1.MovementJump();
                moveVerticalPlayer1 = false;
                player1.SetIsDuck(false);
            }
        }
        
        public void CheckVerticalDown_P1()
        {
            bool movimientoVerticalHabilitado = false;
            if (enableAnalogic)
            {

                movimientoVerticalHabilitado = (((movingDown_P1 || movingDownAnalog_P1) && player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo) || agachandose_P1);
            }
            else
            {
                movimientoVerticalHabilitado = ((movingDown_P1 && player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo) || agachandose_P1);
            }
            if  (movimientoVerticalHabilitado)
            { 
                player1.SetControllerJoystick(true);
                player1.MovementDuck();
                
                player1.enumsPlayers.movimiento = EnumsPlayers.Movimiento.Agacharse;
                if (player1.spritePlayerActual.ActualSprite != SpritePlayer.SpriteActual.RecibirDanio 
                    && player1.spritePlayerActual.ActualSprite != SpritePlayer.SpriteActual.ContraAtaqueAgachado)
                {
                    player1.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.Agachado;
                }

                player1.SetIsDuck(true);
            }
            
        }
        public void CheckVerticalCero_P1()
        {
            if ((InputPlayerController.GetInputAxis("Vertical") == 0 && ((InputPlayerController.GetInputAxis("Vertical_Analogico") > -0.9 && InputPlayerController.GetInputAxis("Vertical_Analogico") < 0.8f))) &&
                (player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Agacharse
                || player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.AgacharseAtaque
                || player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.AgacheDefensa))
            {
                player1.enumsPlayers.movimiento = EnumsPlayers.Movimiento.Nulo;
                player1.SetIsDuck(false);
            }
            else if (InputPlayerController.GetInputAxis("Vertical") == 0 && ((InputPlayerController.GetInputAxis("Vertical_Analogico") > -0.9 && InputPlayerController.GetInputAxis("Vertical_Analogico") < 0.8)))
            {
                moveVerticalPlayer1 = true;
                player1.SetIsDuck(false);
            }
        }
        public void CheckHorizontalLeft_P1()
        {
            bool movimientoHorizontalHabilitado = false;

            if (enableAnalogic)
            {
                movimientoHorizontalHabilitado = (((InputPlayerController.GetInputAxis("Horizontal") < 0 || InputPlayerController.GetInputAxis("Horizontal_Analogico") < -0.9f) && moveHorizontalPlayer1 && player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo) || player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.MoverAtras);
            }
            else
            {
                movimientoHorizontalHabilitado = ((InputPlayerController.GetInputAxis("Horizontal") < 0 && moveHorizontalPlayer1 && player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo) || player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.MoverAtras);
            }
            if (movimientoHorizontalHabilitado)
            {
                player1.SetControllerJoystick(true);
                moveHorizontalPlayer1 = false;
                player1.MovementLeft();
                player1.SetIsDuck(false);
            }
        }
        public void CheckHorizontalRight_P1()
        {
            bool movimientoHorizontalHabilitado = false;
            if (enableAnalogic)
            {
                movimientoHorizontalHabilitado = (((InputPlayerController.GetInputAxis("Horizontal") > 0 || InputPlayerController.GetInputAxis("Horizontal_Analogico") > 0.9f) && moveHorizontalPlayer1 && player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo) || player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.MoverAdelante);
            }
            else
            {
                movimientoHorizontalHabilitado = ((InputPlayerController.GetInputAxis("Horizontal") > 0 && moveHorizontalPlayer1 && player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo) || player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.MoverAdelante);
            }
            if (movimientoHorizontalHabilitado)
            {
                player1.SetControllerJoystick(true);
                moveHorizontalPlayer1 = false;
                player1.MovementRight();
                player1.SetIsDuck(false);
            }
        }
        public void CheckHorizontalCero_P1()
        {
            if (InputPlayerController.GetInputAxis("Horizontal") == 0 && (InputPlayerController.GetInputAxis("Horizontal_Analogico") > -0.9f && InputPlayerController.GetInputAxis("Horizontal_Analogico") < 0.9f))
            {
                moveHorizontalPlayer1 = true;
            }
        }
        public void CheckAttackButton_P1()
        {
            if (!inPause)
            {
                if (InputPlayerController.GetInputButtonDown("AttackButton_P1") && player1.GetEnableAttack()
                    && player1.enumsPlayers.movimiento != EnumsPlayers.Movimiento.MoverAdelante
                    && player1.enumsPlayers.movimiento != EnumsPlayers.Movimiento.MoverAtras
                    && !InputPlayerController.GetInputButton("DeffenseButton_P1"))
                {
                    player1.SetControllerJoystick(true);
                    if (player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Saltar && (InputPlayerController.GetInputAxis("Vertical") < 0
                        || (enableAnalogic && movingDownAnalog_P1)))
                    {
                        player1.spritePlayerActual.PlayAnimation("Ataque Abajo Salto protagonista");
                        enableMovementPlayer1 = false;
                    }
                    else if (player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Saltar && InputPlayerController.GetInputAxis("Vertical") >= 0)
                    {
                        player1.spritePlayerActual.PlayAnimation("Ataque Salto protagonista");
                        enableMovementPlayer1 = false;
                    }
                    else
                    {
                        if (!player1.GetIsDuck())
                        {
                            player1.spritePlayerActual.PlayAnimation("Ataque protagonista");
                            enableMovementPlayer1 = false;
                        }
                        else if (player1.GetIsDuck())
                        {
                            player1.spritePlayerActual.PlayAnimation("Ataque Agachado protagonista");
                            enableMovementPlayer1 = false;
                        }
                    }
                }
            }
           
        } 
        public void CheckDeffenceButton_P1()
        {
            if (!InputPlayerController.GetInputButton("AttackButton_P1"))
            {
                if (InputPlayerController.GetInputButton("DeffenseButton_P1") 
                    && player1.barraDeEscudo.GetValueShild() > player1.barraDeEscudo.porcentageNededForDeffence
                    && player1.barraDeEscudo.GetEnableDeffence())
                {
                    player1.SetControllerJoystick(true);
                    player1.Deffence();
                    player1.boxColliderAgachado.state = BoxColliderController.StateBoxCollider.Defendido;
                    player1.boxColliderParado.state = BoxColliderController.StateBoxCollider.Defendido;
                    player1.boxColliderSaltando.state = BoxColliderController.StateBoxCollider.Defendido;
                    player1.boxColliderSprite.state = BoxColliderController.StateBoxCollider.Defendido;
                }
                else
                {
                    if (player1_PvP != null)
                    {
                        if (player1_PvP.playerSelected == Player_PvP.PlayerSelected.Defensivo)
                        {
                            player1_PvP.stateDeffence = Player_PvP.StateDeffence.CounterAttackDeffense;
                            player1_PvP.delayCounterAttackDeffense = player1_PvP.auxDelayCounterAttackDeffense;
                            player1.spritePlayerActual.spriteRenderer.color = Color.white;
                        }
                    }
                    player1.boxColliderAgachado.state = BoxColliderController.StateBoxCollider.Normal;
                    player1.boxColliderParado.state = BoxColliderController.StateBoxCollider.Normal;
                    player1.boxColliderSaltando.state = BoxColliderController.StateBoxCollider.Normal;
                    player1.boxColliderSprite.state = BoxColliderController.StateBoxCollider.Normal;
                    player1.GetPlayerPvP().stateDeffence = Player_PvP.StateDeffence.NormalDeffense;
                    player1.barraDeEscudo.AddPorcentageBar();
                    if (player1.barraDeEscudo.GetValueShild() <= player1.barraDeEscudo.porcentageNededForDeffence)
                    {
                        player1.barraDeEscudo.SetEnableDeffence(false);
                    }
                }
            }

        }
        public void CheckSpecialAttackButton_P1()
        {
            if (player1.GetEnableSpecialAttack())
            {
                if (InputPlayerController.GetInputButtonDown("SpecialAttackButton_P1"))
                {
                    if (player1.enumsPlayers.specialAttackEquipped != EnumsPlayers.SpecialAttackEquipped.ProyectilImparable)
                    {
                        if (player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Saltar
                            || player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.SaltoAtaque
                            || player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.SaltoDefensa)
                        {
                            player1.spritePlayerActual.PlayAnimation("Ataque Especial protagonista");//ANIMACION DE ATAQUE ESPECIAL SALTANDO
                            enableMovementPlayer1 = false;
                        }
                        else if (player1.GetIsDuck())
                        {
                            player1.spritePlayerActual.PlayAnimation("Ataque Especial protagonista");//ANIMACION DE ATAQUE ESPECIAL AGACHADO
                            enableMovementPlayer1 = false;
                        }
                        else
                        {
                            player1.spritePlayerActual.PlayAnimation("Ataque Especial protagonista");//ANIMACION DE ATAQUE ESPECIAL PARADO
                            enableMovementPlayer1 = false;
                        }
                    }
                    else
                    {
                        if (player1.enumsPlayers.movimiento != EnumsPlayers.Movimiento.Saltar
                            && player1.enumsPlayers.movimiento != EnumsPlayers.Movimiento.SaltoAtaque
                            && player1.enumsPlayers.movimiento != EnumsPlayers.Movimiento.SaltoDefensa)
                        {
                            player1.spritePlayerActual.PlayAnimation("Ataque Especial protagonista");//ANIMACION DE ATAQUE ESPECIAL SALTANDO
                            enableMovementPlayer1 = false;
                        }
                        else
                        {
                            player1.spritePlayerActual.PlayAnimation("Salto protagonista");
                        }
                    }
                }
            }
        }
        public void CheckInputPlayer1()
        {
            CheckBools_P1();
            CheckParabolaAttack_P1();
            CheckVerticalUp_P1();
            CheckVerticalDown_P1();
            CheckVerticalCero_P1();
            CheckHorizontalLeft_P1();
            CheckHorizontalRight_P1();
            CheckHorizontalCero_P1();
            CheckAttackButton_P1();
            CheckDeffenceButton_P1();
            CheckSpecialAttackButton_P1();
        }
        public void CheckInputPlayer2()
        {
            CheckBools_P2();
            CheckParabolaAttack_P2();
            CheckVerticalUp_P2();
            CheckVerticalDown_P2();
            CheckVerticalCero_P2();
            CheckHorizontalLeft_P2();
            CheckHorizontalRight_P2();
            CheckHorizontalCero_P2();
            CheckAttackButton_P2();
            CheckDeffenceButton_P2();
            CheckSpecialAttackButton_P2();

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

        public void CheckSpritesAgachado_P1() 
        {
            bool spriteAgachadoHabilitado = false;
            if (enableAnalogic)
            {
                spriteAgachadoHabilitado = (movingDown_P1 || movingDownAnalog_P1) && player1.enumsPlayers.movimiento != EnumsPlayers.Movimiento.Saltar;
            }
            else
            {
                spriteAgachadoHabilitado = (movingDown_P1 && player1.enumsPlayers.movimiento != EnumsPlayers.Movimiento.Saltar);
            }
            if (spriteAgachadoHabilitado && player1.GetIsDuck())
            {
                if (InputPlayerController.GetInputButton("AttackButton_P1"))
                {
                    player1.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.AgachadoAtaque;
                }
                else if (InputPlayerController.GetInputButton("DeffenseButton_P1")
                     && player1.barraDeEscudo.GetValueShild() > player1.barraDeEscudo.porcentageNededForDeffence
                    && player1.barraDeEscudo.GetEnableDeffence())
                {
                    player1.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.AgachadoDefensa;
                }
                else 
                {
                    player1.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.Agachado;
                }
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
                    CheckSpriteParado("Vertical", "SpecialAttackButton_P1" , player);
                    CheckSpriteMoverDerecha("Horizontal", "Horizontal_Analogico", "Vertical",moveHorizontalPlayer1,player);
                    CheckSpriteMoverIzquierda("Horizontal", "Horizontal_Analogico", "Vertical", moveHorizontalPlayer1,player);
                    CheckSpritesSalto("Vertical", "Vertical_Analogico", "Horizontal", "AttackButton_P1", "DeffenseButton_P1", "SpecialAttackButton_P1", player);
                    CheckSpritesParado("Horizontal", "AttackButton_P1", "DeffenseButton_P1",player, player1_PvP);
                    CheckSpritesAgachado_P1();
                }
                else if(player.enumsPlayers.numberPlayer == EnumsPlayers.NumberPlayer.player2)
                {
                    CheckSpriteParado("Vertical_P2","SpecialAttackButton_P2", player);
                    CheckSpriteMoverDerecha("Horizontal_P2", "Horizontal_Analogico_P2", "Vertical_P2",moveHorizontalPlayer2, player);
                    CheckSpriteMoverIzquierda("Horizontal_P2", "Horizontal_Analogico_P2", "Vertical_P2",moveHorizontalPlayer2, player);
                    CheckSpritesSalto("Vertical_P2", "Vertical_Analogico_P2", "Horizontal_P2", "AttackButton_P2", "DeffenseButton_P2", "SpecialAttackButton_P2", player);
                    CheckSpritesParado("Horizontal_P2", "AttackButton_P2", "DeffenseButton_P2",player, player2_PvP);
                    CheckSpritesAgachado_P2();
                }
            }
        }
        //-----------------------------------------------//

        //----- FUNCIONES Y CONTROLES DEL JUGADOR 2 -----//
        public void CheckParabolaAttack_P2()
        {
            if (InputPlayerController.GetInputButtonDown("ParabolaAttack_P2") && player2.GetEnableAttack()
                && player2.enumsPlayers.movimiento != EnumsPlayers.Movimiento.MoverAdelante
                && player2.enumsPlayers.movimiento != EnumsPlayers.Movimiento.MoverAtras
                && !InputPlayerController.GetInputButton("DeffenseButton_P2"))
            {
                if (player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Saltar)
                {
                    //ANIMACION ATAQUE EN PARABOLA SALTANDO
                    player2.spritePlayerActual.PlayAnimation("Ataque Parabola Salto protagonista");
                    enableMovementPlayer2 = false;
                }
                else
                {
                    if (!player2.GetIsDuck())
                    {
                        //ANIMACION ATAQUE EN PARABOLA PARADO
                        player2.spritePlayerActual.PlayAnimation("Ataque Parabola protagonista");
                        enableMovementPlayer2 = false;
                    }
                    else if (player2.GetIsDuck())
                    {
                        //ANIMACION ATAQUE EN PARABOLA AGACHADO
                        player2.spritePlayerActual.PlayAnimation("Ataque Parabola Agachado protagonista");
                        enableMovementPlayer2 = false;
                    }
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
        public void CheckVerticalUp_P2()
        {
            bool movimientoVerticalHabilitado = false;


            if (enableAnalogic)
            {
                movimientoVerticalHabilitado = ((player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo && (InputPlayerController.GetInputAxis("Vertical_P2") > 0 || pressButtonJamp_P2 || InputPlayerController.GetInputAxis("Vertical_Analogico_P2") < -0.9f) && moveVerticalPlayer2) || player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Saltar);
            }
            else
            {
                movimientoVerticalHabilitado = ((player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo && (InputPlayerController.GetInputAxis("Vertical_P2") > 0 || pressButtonJamp_P2) && moveVerticalPlayer2) || player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Saltar);
            }
            if (movimientoVerticalHabilitado && !inPause)
            {
                player2.SetControllerJoystick(true);
                player2.MovementJump();
                moveVerticalPlayer2 = false;
                player2.SetIsDuck(false);
            }
        }
        public void CheckVerticalDown_P2()
        {
            bool movimientoVerticalHabilitado = false;
            if (enableAnalogic)
            {
                movimientoVerticalHabilitado = (((movingDown_P2 || movingDownAnalog_P2) && player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo) || agachandose_P2);
            }
            else
            {
                movimientoVerticalHabilitado = ((movingDown_P2 && player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo) || agachandose_P2);
            }
            if (movimientoVerticalHabilitado)
            {
                player2.SetControllerJoystick(true);
                player2.MovementDuck();

                player2.enumsPlayers.movimiento = EnumsPlayers.Movimiento.Agacharse;
                if (player2.spritePlayerActual.ActualSprite != SpritePlayer.SpriteActual.RecibirDanio
                    && player2.spritePlayerActual.ActualSprite != SpritePlayer.SpriteActual.ContraAtaqueAgachado)
                {
                    player2.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.Agachado;
                }

                player2.SetIsDuck(true);
            }

        }
        public void CheckVerticalCero_P2()
        {
            if ((InputPlayerController.GetInputAxis("Vertical_P2") == 0 && ((InputPlayerController.GetInputAxis("Vertical_Analogico_P2") > -0.9 && InputPlayerController.GetInputAxis("Vertical_Analogico_P2") < 0.8f))) &&
                (player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Agacharse
                || player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.AgacharseAtaque
                || player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.AgacheDefensa))
            {
                player2.enumsPlayers.movimiento = EnumsPlayers.Movimiento.Nulo;
                player2.SetIsDuck(false);
            }
            else if (InputPlayerController.GetInputAxis("Vertical_P2") == 0 && ((InputPlayerController.GetInputAxis("Vertical_Analogico_P2") > -0.9 && InputPlayerController.GetInputAxis("Vertical_Analogico_P2") < 0.8)))
            {
                moveVerticalPlayer2 = true;
                player2.SetIsDuck(false);
            }
        }
        public void CheckHorizontalLeft_P2()
        {
            bool movimientoHorizontalHabilitado = false;

            if (enableAnalogic)
            {
                movimientoHorizontalHabilitado = (((InputPlayerController.GetInputAxis("Horizontal_P2") < 0 || InputPlayerController.GetInputAxis("Horizontal_Analogico_P2") < -0.9f) && moveHorizontalPlayer2 && player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo) || player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.MoverAtras);
            }
            else
            {
                movimientoHorizontalHabilitado = ((InputPlayerController.GetInputAxis("Horizontal_P2") < 0 && moveHorizontalPlayer2 && player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo) || player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.MoverAtras);
            }
            if (movimientoHorizontalHabilitado)
            {
                player2.SetControllerJoystick(true);
                moveHorizontalPlayer2 = false;
                player2.MovementLeft();
                player2.SetIsDuck(false);
            }
        }
        public void CheckHorizontalRight_P2()
        {
            bool movimientoHorizontalHabilitado = false;
            if (enableAnalogic)
            {
                movimientoHorizontalHabilitado = (((InputPlayerController.GetInputAxis("Horizontal_P2") > 0 || InputPlayerController.GetInputAxis("Horizontal_Analogico_P2") > 0.9f) && moveHorizontalPlayer2 && player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo) || player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.MoverAdelante);
            }
            else
            {
                movimientoHorizontalHabilitado = ((InputPlayerController.GetInputAxis("Horizontal_P2") > 0 && moveHorizontalPlayer2 && player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo) || player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.MoverAdelante);
            }
            if (movimientoHorizontalHabilitado)
            {
                player2.SetControllerJoystick(true);
                moveHorizontalPlayer2 = false;
                player2.MovementRight();
                player2.SetIsDuck(false);
            }
        }
        public void CheckHorizontalCero_P2()
        {
            if (InputPlayerController.GetInputAxis("Horizontal_P2") == 0 && (InputPlayerController.GetInputAxis("Horizontal_Analogico_P2") > -0.9f && InputPlayerController.GetInputAxis("Horizontal_Analogico_P2") < 0.9f))
            {
                moveHorizontalPlayer2 = true;
            }
        }
        public void CheckAttackButton_P2()
        {
            if (InputPlayerController.GetInputButtonDown("AttackButton_P2") && player2.GetEnableAttack()
                && player2.enumsPlayers.movimiento != EnumsPlayers.Movimiento.MoverAdelante
                && player2.enumsPlayers.movimiento != EnumsPlayers.Movimiento.MoverAtras
                && !InputPlayerController.GetInputButton("DeffenseButton_P2"))
            {
                player2.SetControllerJoystick(true);
                if (player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Saltar && (InputPlayerController.GetInputAxis("Vertical_P2") < 0
                    || (enableAnalogic && movingDownAnalog_P2)))
                {
                    player2.spritePlayerActual.PlayAnimation("Ataque Abajo Salto protagonista");
                    enableMovementPlayer2 = false;
                }
                else if (player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Saltar && InputPlayerController.GetInputAxis("Vertical_P2") >= 0)
                {
                    player2.spritePlayerActual.PlayAnimation("Ataque Salto protagonista");
                    enableMovementPlayer2 = false;
                }
                else
                {
                    if (!player2.GetIsDuck())
                    {
                        player2.spritePlayerActual.PlayAnimation("Ataque protagonista");
                        enableMovementPlayer2 = false;
                    }
                    else if (player2.GetIsDuck())
                    {
                        player2.spritePlayerActual.PlayAnimation("Ataque Agachado protagonista");
                        enableMovementPlayer2 = false;
                    }
                }
            }

        }
        public void CheckDeffenceButton_P2()
        {
            if (!InputPlayerController.GetInputButton("AttackButton_P2"))
            {
                if (InputPlayerController.GetInputButton("DeffenseButton_P2")
                    && player2.barraDeEscudo.GetValueShild() > player2.barraDeEscudo.porcentageNededForDeffence
                    && player2.barraDeEscudo.GetEnableDeffence())
                {
                    player2.SetControllerJoystick(true);
                    player2.Deffence();
                    player2.boxColliderAgachado.state = BoxColliderController.StateBoxCollider.Defendido;
                    player2.boxColliderParado.state = BoxColliderController.StateBoxCollider.Defendido;
                    player2.boxColliderSaltando.state = BoxColliderController.StateBoxCollider.Defendido;
                    player2.boxColliderSprite.state = BoxColliderController.StateBoxCollider.Defendido;
                }
                else
                {
                    if (player2_PvP != null)
                    {
                        if (player2_PvP.playerSelected == Player_PvP.PlayerSelected.Defensivo)
                        {
                            player2_PvP.stateDeffence = Player_PvP.StateDeffence.CounterAttackDeffense;
                            player2_PvP.delayCounterAttackDeffense = player2_PvP.auxDelayCounterAttackDeffense;
                            player2.spritePlayerActual.spriteRenderer.color = Color.white;
                        }
                    }
                    player2.boxColliderAgachado.state = BoxColliderController.StateBoxCollider.Normal;
                    player2.boxColliderParado.state = BoxColliderController.StateBoxCollider.Normal;
                    player2.boxColliderSaltando.state = BoxColliderController.StateBoxCollider.Normal;
                    player2.boxColliderSprite.state = BoxColliderController.StateBoxCollider.Normal;
                    player2.GetPlayerPvP().stateDeffence = Player_PvP.StateDeffence.NormalDeffense;
                    player2.barraDeEscudo.AddPorcentageBar();
                    if (player2.barraDeEscudo.GetValueShild() <= player2.barraDeEscudo.porcentageNededForDeffence)
                    {
                        player2.barraDeEscudo.SetEnableDeffence(false);
                    }
                }
            }
            
        }
        public void CheckSpecialAttackButton_P2()
        {
            if (player2.GetEnableSpecialAttack())
            {
                if (InputPlayerController.GetInputButtonDown("SpecialAttackButton_P2"))
                {
                    if (player2.enumsPlayers.specialAttackEquipped != EnumsPlayers.SpecialAttackEquipped.ProyectilImparable)
                    {
                        if (player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Saltar
                            || player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.SaltoAtaque
                            || player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.SaltoDefensa)
                        {
                            player2.spritePlayerActual.PlayAnimation("Ataque Especial protagonista");//ANIMACION DE ATAQUE ESPECIAL SALTANDO
                            enableMovementPlayer2 = false;
                        }
                        else if (player2.GetIsDuck())
                        {
                            player2.spritePlayerActual.PlayAnimation("Ataque Especial protagonista");//ANIMACION DE ATAQUE ESPECIAL AGACHADO
                            enableMovementPlayer2 = false;
                        }
                        else
                        {
                            player2.spritePlayerActual.PlayAnimation("Ataque Especial protagonista");//ANIMACION DE ATAQUE ESPECIAL PARADO
                            enableMovementPlayer2 = false;
                        }
                    }
                    else
                    {
                        if (player2.enumsPlayers.movimiento != EnumsPlayers.Movimiento.Saltar
                            && player2.enumsPlayers.movimiento != EnumsPlayers.Movimiento.SaltoAtaque
                            && player2.enumsPlayers.movimiento != EnumsPlayers.Movimiento.SaltoDefensa)
                        {
                            player2.spritePlayerActual.PlayAnimation("Ataque Especial protagonista");//ANIMACION DE ATAQUE ESPECIAL SALTANDO
                            enableMovementPlayer2 = false;
                        }
                        else
                        {
                            //player2.spritePlayerActual.PlayAnimation("Salto protagonista");
                        }
                    }
                }
            }
        }
        
        public void CheckSpritesAgachado_P2()
        {
            bool spriteAgachadoHabilitado = false;
            if (enableAnalogic)
            {
                spriteAgachadoHabilitado = (movingDown_P2 || movingDownAnalog_P2) && player2.enumsPlayers.movimiento != EnumsPlayers.Movimiento.Saltar;
            }
            else
            {
                spriteAgachadoHabilitado = (movingDown_P2 && player2.enumsPlayers.movimiento != EnumsPlayers.Movimiento.Saltar);
            }
            if (spriteAgachadoHabilitado && player2.GetIsDuck())
            {
                if (InputPlayerController.GetInputButton("AttackButton_P2"))
                {
                    player2.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.AgachadoAtaque;
                }
                else if (InputPlayerController.GetInputButton("DeffenseButton_P2")
                     && player2.barraDeEscudo.GetValueShild() > player2.barraDeEscudo.porcentageNededForDeffence
                    && player2.barraDeEscudo.GetEnableDeffence())
                {
                    player2.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.AgachadoDefensa;
                }
                else
                {
                    player2.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.Agachado;
                }
            }
        }
        
        //-----------------------------------------------//
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
