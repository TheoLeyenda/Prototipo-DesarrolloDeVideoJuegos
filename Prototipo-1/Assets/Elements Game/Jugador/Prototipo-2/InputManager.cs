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


        bool movingLeft_P1;
        bool movingLeftAnalog_P1;
        bool verticalStill_P1;
        bool retroceder_P1;
        bool movingRight_P1;
        bool movingRightAnalog_P1;
        bool avanzando_P1;
        bool movimientoNulo_P1;
        bool movingUp_P1;
        bool pressButtonJamp_P1;
        bool saltando_P1;
        bool saltandoAtaque_P1;
        bool saltandoDefensa_P1;
        bool movingUpAnalog_P1;
        bool movingDown_P1;
        bool agachandose_P1;
        bool movingDownAnalog_P1;
        bool horizontalStill_P1;

        bool movingLeft_P2;
        bool movingLeftAnalog_P2;
        bool verticalStill_P2;
        bool retroceder_P2;
        bool movingRight_P2;
        bool movingRightAnalog_P2;
        bool avanzando_P2;
        bool movimientoNulo_P2;
        bool movingUp_P2;
        bool pressButtonJamp_P2;
        bool saltando_P2;
        bool saltandoAtaque_P2;
        bool saltandoDefensa_P2;
        bool movingUpAnalog_P2;
        bool movingDown_P2;
        bool agachandose_P2;
        bool movingDownAnalog_P2;
        bool horizontalStill_P2;
        // Update is called once per frame
        private void Start()
        {
            enableMovementPlayer1 = true;
            enableMovementPlayer2 = true;
            if (FindPlayersAndPlayers_PvP)
            {
                player1 = GameObject.Find("Player1").GetComponent<Player>();
                player1_PvP = player1.gameObject.GetComponent<Player_PvP>();

                //UNA VEZ TERMINE DE INCORPORAR AL SEGUNDO JUGADOR DESCOMENTAR ESTO.
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
            horizontalStill_P1 = InputPlayerController.Horizontal_Button_P1() == 0;
            //Debug.Log(InputPlayerController.Vertical_Analogico_P1());
            movingDown_P1 = InputPlayerController.Vertical_Button_P1() < 0;
            agachandose_P1 = player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Agacharse;
            movingDownAnalog_P1 = InputPlayerController.Vertical_Analogico_P1() > 0.5f;

            movingUpAnalog_P1 = InputPlayerController.Vertical_Analogico_P1() < -0.9f;
            movimientoNulo_P1 = player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo;
            movingUp_P1 = InputPlayerController.Vertical_Button_P1() > 0;
            pressButtonJamp_P1 = InputPlayerController.JumpButton_P1();
            saltando_P1 = player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Saltar;
            saltandoAtaque_P1 = player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.SaltoAtaque;
            saltandoDefensa_P1 = player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.SaltoDefensa;

            movingLeft_P1 = InputPlayerController.Horizontal_Button_P1() < 0;
            movingLeftAnalog_P1 = InputPlayerController.Horizontal_Analogico_P1() < -0.9f;
            verticalStill_P1 = InputPlayerController.Vertical_Button_P1() == 0;
            retroceder_P1 = player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.MoverAtras;

            movingRight_P1 = InputPlayerController.Horizontal_Button_P1() > 0;
            movingRightAnalog_P1 = InputPlayerController.Horizontal_Analogico_P1() > 0.9f;
            avanzando_P1 = player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.MoverAdelante;
        }

        public void CheckBools_P2()
        {
            horizontalStill_P2 = InputPlayerController.Horizontal_Button_P2() == 0;
            movingDown_P2 = InputPlayerController.Vertical_Button_P2() < 0;
            agachandose_P2 = player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Agacharse;
            movingDownAnalog_P2 = InputPlayerController.Vertical_Analogico_P2() > 0.5f;

            movingUpAnalog_P2 = InputPlayerController.Vertical_Analogico_P2() < -0.9f;
            movimientoNulo_P2 = player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo;
            movingUp_P2 = InputPlayerController.Vertical_Button_P2() > 0;
            pressButtonJamp_P2 = InputPlayerController.JumpButton_P2();
            saltando_P2 = player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Saltar;
            saltandoAtaque_P2 = player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.SaltoAtaque;
            saltandoDefensa_P2 = player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.SaltoDefensa;

            movingLeft_P2 = InputPlayerController.Horizontal_Button_P2() < 0;
            movingLeftAnalog_P2 = InputPlayerController.Horizontal_Analogico_P2() < -0.9f;
            verticalStill_P2 = InputPlayerController.Vertical_Button_P2() == 0;
            retroceder_P2 = player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.MoverAtras;

            movingRight_P2 = InputPlayerController.Horizontal_Button_P2() > 0;
            movingRightAnalog_P2 = InputPlayerController.Horizontal_Analogico_P2() > 0.9f;
            avanzando_P2 = player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.MoverAdelante;
        }
        void Update()
        {
            //Debug.Log(enableMovementPlayer1);
            CheckPauseButton_P1();
            CheckPauseButton_P2();
            //Debug.Log(player1.enumsPlayers.movimiento);
            if (player1 != null && player1.gameObject.activeSelf)
            {
                if (!InputPlayerController.CheckPressDeffenseButton_P1() || player1.barraDeEscudo.nededBarMaxPorcentage)
                {
                    player1.barraDeEscudo.AddPorcentageBar();
                    if (player1.barraDeEscudo.GetValueShild() <= player1.barraDeEscudo.porcentageNededForDeffence)
                    {
                        //Debug.Log("ENTRE");
                        player1.barraDeEscudo.SetEnableDeffence(false);
                    }
                }
                if (enableMovementPlayer1)
                {
                    CheckInputPlayer1();
                    if (player1.PD.lifePlayer > 0)
                    {
                        CheckSpritePlayer1();
                    }
                }
                else
                {
                    if (player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Saltar)
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
                if (!InputPlayerController.CheckPressDeffenseButton_P2() || player2.barraDeEscudo.nededBarMaxPorcentage)
                {
                    player2.barraDeEscudo.AddPorcentageBar();
                    if (player2.barraDeEscudo.GetValueShild() <= player2.barraDeEscudo.porcentageNededForDeffence)
                    {
                        //Debug.Log("ENTRE");
                        player2.barraDeEscudo.SetEnableDeffence(false);
                    }
                }
                if (enableMovementPlayer2)
                {
                    CheckInputPlayer2();
                    if (player2.PD.lifePlayer > 0)
                    {
                        CheckSpritePlayer2();
                    }
                }
                else
                {
                    if (player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Saltar)
                    {
                        player2.SetControllerJoystick(true);
                        player2.MovementJump();
                        moveVerticalPlayer1 = false;
                        player2.SetIsDuck(false);
                    }
                }
            }
            
        }
        public void CheckValueInPause()
        {
            /*if (inPause)
            {
                inPause = false;
            }
            else */if (!inPause)
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
            if (InputPlayerController.PauseButton_P1())
            {
                CheckValueInPause();
                CheckInPause();
            }
        }
        public void CheckParabolaAttack_P1()
        {
            if (InputPlayerController.ParabolaAttack_P1() && player1.GetEnableAttack()
                && player1.enumsPlayers.movimiento != EnumsPlayers.Movimiento.MoverAdelante
                && player1.enumsPlayers.movimiento != EnumsPlayers.Movimiento.MoverAtras
                && !InputPlayerController.CheckPressDeffenseButton_P1())
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
                //player1.ParabolaAttack(Proyectil.DisparadorDelProyectil.Jugador1);
            }
        }
        public void CheckVerticalUp_P1()
        {
            bool movimientoVerticalHabilitado = false;


            if (enableAnalogic)
            {
                movimientoVerticalHabilitado = ((movimientoNulo_P1 && (movingUp_P1 || pressButtonJamp_P1 || movingUpAnalog_P1) && moveVerticalPlayer1) || saltando_P1);
            }
            else
            {
                movimientoVerticalHabilitado = ((movimientoNulo_P1 && (movingUp_P1 || pressButtonJamp_P1) && moveVerticalPlayer1) || saltando_P1);
            }
            if (movimientoVerticalHabilitado)
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

                movimientoVerticalHabilitado = (((movingDown_P1 || movingDownAnalog_P1) && movimientoNulo_P1) || agachandose_P1);
                //Debug.Log(movimientoVerticalHabilitado);
            }
            else
            {
                movimientoVerticalHabilitado = ((movingDown_P1 && movimientoNulo_P1) || agachandose_P1);
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
            if ((InputPlayerController.Vertical_Button_P1() == 0 && ((InputPlayerController.Vertical_Analogico_P1() > -0.9 && InputPlayerController.Vertical_Analogico_P1() < 0.8f))) &&
                (player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Agacharse
                || player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.AgacharseAtaque
                || player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.AgacheDefensa))
            {
                player1.enumsPlayers.movimiento = EnumsPlayers.Movimiento.Nulo;
                player1.SetIsDuck(false);
            }
            else if (InputPlayerController.Vertical_Button_P1() == 0 && ((InputPlayerController.Vertical_Analogico_P1() > -0.9 && InputPlayerController.Vertical_Analogico_P1() < 0.8)))
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
                movimientoHorizontalHabilitado = (((movingLeft_P1 || movingLeftAnalog_P1) && moveHorizontalPlayer1 && movimientoNulo_P1) || retroceder_P1);
            }
            else
            {
                movimientoHorizontalHabilitado = ((movingLeft_P1 && moveHorizontalPlayer1 && movimientoNulo_P1) || retroceder_P1);
            }
            if (movimientoHorizontalHabilitado)
            {
                //Debug.Log("ENTRE");
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
                movimientoHorizontalHabilitado = (((movingRight_P1 || movingRightAnalog_P1) && moveHorizontalPlayer1 && movimientoNulo_P1) || avanzando_P1);
            }
            else
            {
                movimientoHorizontalHabilitado = ((movingRight_P1 && moveHorizontalPlayer1 && movimientoNulo_P1) || avanzando_P1);
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
            if (InputPlayerController.Horizontal_Button_P1() == 0 && (InputPlayerController.Horizontal_Analogico_P1() > -0.9f && InputPlayerController.Horizontal_Analogico_P1() < 0.9f))
            {
                moveHorizontalPlayer1 = true;
            }
        }
        public void CheckAttackButton_P1()
        {
            if (InputPlayerController.AttackButton_P1() && player1.GetEnableAttack() 
                && player1.enumsPlayers.movimiento != EnumsPlayers.Movimiento.MoverAdelante 
                && player1.enumsPlayers.movimiento != EnumsPlayers.Movimiento.MoverAtras 
                && !InputPlayerController.CheckPressDeffenseButton_P1())
            {
                //Debug.Log("JUGADOR 1 ATAQUE ACTIVED");
                player1.SetControllerJoystick(true);
                if (player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Saltar && (InputPlayerController.Vertical_Button_P1() < 0 
                    || (enableAnalogic && movingDownAnalog_P1)))
                {
                    player1.spritePlayerActual.PlayAnimation("Ataque Abajo Salto protagonista");
                    enableMovementPlayer1 = false;
                }
                else if (player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Saltar && InputPlayerController.Vertical_Button_P1() >= 0)
                {
                    player1.spritePlayerActual.PlayAnimation("Ataque Salto protagonista");
                    enableMovementPlayer1 = false;
                }
                else
                {
                    if (!player1.GetIsDuck())
                    {
                        //Debug.Log("ENTRE AL ATAQUE PARADO");
                        player1.spritePlayerActual.PlayAnimation("Ataque protagonista");
                        enableMovementPlayer1 = false;
                    }
                    else if (player1.GetIsDuck())
                    {
                        //Debug.Log("ENTRE AL ATAQUE AGACHADO");
                        player1.spritePlayerActual.PlayAnimation("Ataque Agachado protagonista");
                        enableMovementPlayer1 = false;
                    }
                }
            }
           
        } 
        public void CheckDeffenceButton_P1()
        {
            if (!InputPlayerController.CheckPressAttackButton_P1())
            {
                if (InputPlayerController.CheckPressDeffenseButton_P1() 
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
                        //Debug.Log("ENTRE");
                        player1.barraDeEscudo.SetEnableDeffence(false);
                    }
                }
            }

        }
        public void CheckSpecialAttackButton_P1()
        {
            if (player1.GetEnableSpecialAttack())
            {
                if (InputPlayerController.SpecialAttackButton_P1())
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
            if (player1_PvP == null)
            {
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
            else
            {
                switch (player1_PvP.playerSelected)
                {
                    case Player_PvP.PlayerSelected.Agresivo:
                        CheckParabolaAttack_P1();
                        CheckVerticalUp_P1();
                        //CheckButtonJump_P1();
                        CheckVerticalDown_P1();
                        CheckVerticalCero_P1();
                        CheckHorizontalLeft_P1();
                        CheckHorizontalRight_P1();
                        CheckHorizontalCero_P1();
                        CheckAttackButton_P1();
                        CheckDeffenceButton_P1();
                        CheckSpecialAttackButton_P1();
                        break;
                    case Player_PvP.PlayerSelected.Balanceado:
                        CheckParabolaAttack_P1();
                        CheckVerticalUp_P1();
                        //CheckButtonJump_P1();
                        CheckVerticalDown_P1();
                        CheckVerticalCero_P1();
                        CheckHorizontalLeft_P1();
                        CheckHorizontalRight_P1();
                        CheckHorizontalCero_P1();
                        CheckAttackButton_P1();
                        CheckDeffenceButton_P1();
                        CheckSpecialAttackButton_P1();
                        break;
                    case Player_PvP.PlayerSelected.Defensivo:
                        CheckParabolaAttack_P1();
                        CheckVerticalUp_P1();
                        //CheckButtonJump_P1();
                        CheckVerticalDown_P1();
                        CheckVerticalCero_P1();
                        CheckHorizontalLeft_P1();
                        CheckHorizontalRight_P1();
                        CheckHorizontalCero_P1();
                        CheckAttackButton_P1();
                        CheckDeffenceButton_P1();
                        CheckSpecialAttackButton_P1();
                        break;
                    case Player_PvP.PlayerSelected.Protagonista:
                        CheckParabolaAttack_P1();
                        CheckVerticalUp_P1();
                        //CheckButtonJump_P1();
                        CheckVerticalDown_P1();
                        CheckVerticalCero_P1();
                        CheckHorizontalLeft_P1();
                        CheckHorizontalRight_P1();
                        CheckHorizontalCero_P1();
                        CheckAttackButton_P1();
                        CheckDeffenceButton_P1();
                        CheckSpecialAttackButton_P1();
                        break;
                }
            }
            /*if (!InputPlayerController.CheckPressDeffenseButton_P1() && !player1.GetIsJumping()
                && player1.enumsPlayers.movimiento != EnumsPlayers.Movimiento.Saltar
                && player1.enumsPlayers.movimiento != EnumsPlayers.Movimiento.SaltoAtaque
                && player1.enumsPlayers.movimiento != EnumsPlayers.Movimiento.SaltoDefensa)
            {
                player1.gridPlayer.CheckCuadrillaOcupada(player1.structsPlayer.dataPlayer.columnaActual, player1.structsPlayer.dataPlayer.CantCasillasOcupadas_X, player1.structsPlayer.dataPlayer.CantCasillasOcupadas_Y);
            }*/
        }
        
        public void CheckSpriteParado_P1()
        {
            if (InputPlayerController.Vertical_Button_P1() == 0 && !InputPlayerController.SpecialAttackButton_P1())
            {
                player1.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.Parado;
            }
        }
        public void CheckSpriteMoverAdelante_P1()
        {

            bool cambioSpriteHabilitado = false;
            if (enableAnalogic)
            {
                cambioSpriteHabilitado = (((movingRight_P1 || movingRightAnalog_P1) && verticalStill_P1 && moveHorizontalPlayer1) || avanzando_P1);
            }
            else
            {
                cambioSpriteHabilitado = ((movingRight_P1 && verticalStill_P1 && moveHorizontalPlayer1) || avanzando_P1);
            }
            if (cambioSpriteHabilitado)
            {
                player1.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.MoverAdelante;
            }
        }
        public void CheckSpriteMoverAtras_P1()
        {
            bool cambioSpriteHabilitado = false;

            if (enableAnalogic)
            {
                cambioSpriteHabilitado = (((movingLeft_P1 || movingLeftAnalog_P1) && verticalStill_P1 && moveHorizontalPlayer1) || retroceder_P1);
            }
            else
            {
                cambioSpriteHabilitado = ((movingLeft_P1 && verticalStill_P1 && moveHorizontalPlayer1) || retroceder_P1);
            }
            if (cambioSpriteHabilitado)
            {
                player1.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.MoverAtras;
            }
            
        }
        public void CheckSpritesSalto_P1() 
        {
            bool spriteSaltoHabilitado = false;
            if (enableAnalogic)
            {
                spriteSaltoHabilitado = (((movingUp_P1 || movingUpAnalog_P1) && movimientoNulo_P1 && horizontalStill_P1) || saltando_P1 || saltandoDefensa_P1 || saltandoAtaque_P1);
            }
            else
            {
                spriteSaltoHabilitado = ((movingUp_P1 && horizontalStill_P1 && movimientoNulo_P1) || saltando_P1 || saltandoDefensa_P1 || saltandoAtaque_P1);
            }

            if (spriteSaltoHabilitado)
            {
                if (InputPlayerController.CheckPressAttackButton_P1())
                {
                    player1.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.SaltoAtaque;
                }
                else if (InputPlayerController.CheckPressDeffenseButton_P1()
                     && player1.barraDeEscudo.GetValueShild() > player1.barraDeEscudo.porcentageNededForDeffence
                    && player1.barraDeEscudo.GetEnableDeffence())
                {
                    player1.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.SaltoDefensa;
                }
                else if (InputPlayerController.SpecialAttackButton_P1())
                {
                    //SPRITE O ANIMACION ATAQUE ESPECIAL JUGADOR.
                }
                else
                {
                    player1.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.Salto;
                }
                if (player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo)
                {
                    player1.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.Parado;
                }
            }
        }
        public void CheckSpritesParado_P1() 
        {
            if (player1.enumsPlayers.movimiento != EnumsPlayers.Movimiento.Saltar
                    && player1.enumsPlayers.movimiento != EnumsPlayers.Movimiento.Agacharse && InputPlayerController.Horizontal_Button_P1() == 0)
            {
                if (InputPlayerController.CheckPressAttackButton_P1())
                {
                    player1.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.ParadoAtaque;
                }
                else if (InputPlayerController.CheckPressDeffenseButton_P1() 
                    && player1.barraDeEscudo.GetValueShild() > player1.barraDeEscudo.porcentageNededForDeffence
                    && player1.barraDeEscudo.GetEnableDeffence())
                {
                    player1.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.ParadoDefensa;
                    if (player1_PvP != null)
                    {
                        player1_PvP.playerState = Player_PvP.State.Defendido;
                    }
                }
                else
                {
                    player1.spritePlayerActual.delaySpriteRecibirDanio = player1.spritePlayerActual.GetAuxDelaySpriteRecibirDanio();
                }
            }
        }
        public void CheckSpritesAgachado_P1() 
        {
            /*horizontalStill_P1 = InputPlayerController.Horizontal_Button_P1() == 0;
            //Debug.Log(InputPlayerController.Vertical_Analogico_P1());
            movingDown_P1 = InputPlayerController.Vertical_Button_P1() < 0;
            agachandose_P1 = player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Agacharse;
            movingDownAnalog_P1 = InputPlayerController.Vertical_Analogico_P1() > 0.5f;*/

            bool spriteAgachadoHabilitado = false;
            if (enableAnalogic)
            {
                spriteAgachadoHabilitado = (movingDown_P1 || movingDownAnalog_P1) && !saltando_P1;
            }
            else
            {
                spriteAgachadoHabilitado = (movingDown_P1 && !saltando_P1);
            }
            //Debug.Log(player1.GetIsDuck());
            if (spriteAgachadoHabilitado && player1.GetIsDuck())
            {
                if (InputPlayerController.CheckPressAttackButton_P1())
                {
                    player1.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.AgachadoAtaque;
                }
                else if (InputPlayerController.CheckPressDeffenseButton_P1()
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
        public void CheckSpritePlayer1()
        {
            if (player1.spritePlayerActual.ActualSprite == SpritePlayer.SpriteActual.RecibirDanio || player1.spritePlayerActual.ActualSprite == SpritePlayer.SpriteActual.ContraAtaqueParado)
            {
                if (player1.spritePlayerActual.ActualSprite == SpritePlayer.SpriteActual.RecibirDanio)
                {
                    player1.spritePlayerActual.CheckDeleyRecibirDanio();
                }
                else
                {
                    player1.spritePlayerActual.CheckDeleyContraAtaque();
                }
            }
            else
            {
                if (player1_PvP == null)
                {
                    CheckSpriteParado_P1();
                    CheckSpriteMoverAdelante_P1();
                    CheckSpriteMoverAtras_P1();
                    CheckSpritesSalto_P1();
                    CheckSpritesParado_P1();
                    CheckSpritesAgachado_P1();
                }
                else
                {
                    switch (player1_PvP.playerSelected)
                    {
                        case Player_PvP.PlayerSelected.Agresivo:
                            CheckSpriteParado_P1();
                            CheckSpriteMoverAdelante_P1();
                            CheckSpriteMoverAtras_P1();
                            CheckSpritesSalto_P1();
                            CheckSpritesParado_P1();
                            CheckSpritesAgachado_P1();
                            break;
                        case Player_PvP.PlayerSelected.Balanceado:
                            CheckSpriteParado_P1();
                            CheckSpriteMoverAdelante_P1();
                            CheckSpriteMoverAtras_P1();
                            CheckSpritesSalto_P1();
                            CheckSpritesParado_P1();
                            CheckSpritesAgachado_P1();
                            break;
                        case Player_PvP.PlayerSelected.Defensivo:
                            CheckSpriteParado_P1();
                            CheckSpriteMoverAdelante_P1();
                            CheckSpriteMoverAtras_P1();
                            CheckSpritesSalto_P1();
                            CheckSpritesParado_P1();
                            CheckSpritesAgachado_P1();
                            break;
                        case Player_PvP.PlayerSelected.Protagonista:
                            CheckSpriteParado_P1();
                            CheckSpriteMoverAdelante_P1();
                            CheckSpriteMoverAtras_P1();
                            CheckSpritesSalto_P1();
                            CheckSpritesParado_P1();
                            CheckSpritesAgachado_P1();
                            break;
                    }
                }
            }
        }
        //-----------------------------------------------//


        //----- FUNCIONES Y CONTROLES DEL JUGADOR 2 -----//
        public void CheckParabolaAttack_P2()
        {
            if (InputPlayerController.ParabolaAttack_P2() && player2.GetEnableAttack()
                && player2.enumsPlayers.movimiento != EnumsPlayers.Movimiento.MoverAdelante
                && player2.enumsPlayers.movimiento != EnumsPlayers.Movimiento.MoverAtras
                && !InputPlayerController.CheckPressDeffenseButton_P2())
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
                //player1.ParabolaAttack(Proyectil.DisparadorDelProyectil.Jugador1);
            }
        }
        public void CheckPauseButton_P2()
        {
            if (InputPlayerController.PauseButton_P2())
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
                movimientoVerticalHabilitado = ((movimientoNulo_P2 && (movingUp_P2 || pressButtonJamp_P2 || movingUpAnalog_P2) && moveVerticalPlayer2) || saltando_P2);
            }
            else
            {
                movimientoVerticalHabilitado = ((movimientoNulo_P2 && (movingUp_P2 || pressButtonJamp_P2) && moveVerticalPlayer2) || saltando_P2);
            }
            if (movimientoVerticalHabilitado)
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

                movimientoVerticalHabilitado = (((movingDown_P2 || movingDownAnalog_P2) && movimientoNulo_P2) || agachandose_P2);
                //Debug.Log(movimientoVerticalHabilitado);
            }
            else
            {
                movimientoVerticalHabilitado = ((movingDown_P2 && movimientoNulo_P2) || agachandose_P2);
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
            if ((InputPlayerController.Vertical_Button_P2() == 0 && ((InputPlayerController.Vertical_Analogico_P2() > -0.9 && InputPlayerController.Vertical_Analogico_P2() < 0.8f))) &&
                (player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Agacharse
                || player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.AgacharseAtaque
                || player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.AgacheDefensa))
            {
                player2.enumsPlayers.movimiento = EnumsPlayers.Movimiento.Nulo;
                player2.SetIsDuck(false);
            }
            else if (InputPlayerController.Vertical_Button_P2() == 0 && ((InputPlayerController.Vertical_Analogico_P2() > -0.9 && InputPlayerController.Vertical_Analogico_P2() < 0.8)))
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
                movimientoHorizontalHabilitado = (((movingLeft_P2 || movingLeftAnalog_P2) && moveHorizontalPlayer2 && movimientoNulo_P2) || retroceder_P2);
            }
            else
            {
                movimientoHorizontalHabilitado = ((movingLeft_P2 && moveHorizontalPlayer2 && movimientoNulo_P2) || retroceder_P2);
            }
            if (movimientoHorizontalHabilitado)
            {
                //Debug.Log("ENTRE");
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
                movimientoHorizontalHabilitado = (((movingRight_P2 || movingRightAnalog_P2) && moveHorizontalPlayer2 && movimientoNulo_P2) || avanzando_P2);
            }
            else
            {
                movimientoHorizontalHabilitado = ((movingRight_P2 && moveHorizontalPlayer2 && movimientoNulo_P2) || avanzando_P2);
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
            if (InputPlayerController.Horizontal_Button_P2() == 0 && (InputPlayerController.Horizontal_Analogico_P2() > -0.9f && InputPlayerController.Horizontal_Analogico_P2() < 0.9f))
            {
                moveHorizontalPlayer2 = true;
            }
        }
        public void CheckAttackButton_P2()
        {
            if (InputPlayerController.AttackButton_P2() && player2.GetEnableAttack()
                && player2.enumsPlayers.movimiento != EnumsPlayers.Movimiento.MoverAdelante
                && player2.enumsPlayers.movimiento != EnumsPlayers.Movimiento.MoverAtras
                && !InputPlayerController.CheckPressDeffenseButton_P2())
            {
                //Debug.Log("JUGADOR 1 ATAQUE ACTIVED");
                player2.SetControllerJoystick(true);
                if (player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Saltar && (InputPlayerController.Vertical_Button_P2() < 0
                    || (enableAnalogic && movingDownAnalog_P2)))
                {
                    player2.spritePlayerActual.PlayAnimation("Ataque Abajo Salto protagonista");
                    enableMovementPlayer2 = false;
                }
                else if (player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Saltar && InputPlayerController.Vertical_Button_P2() >= 0)
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
            if (!InputPlayerController.CheckPressAttackButton_P2())
            {
                if (InputPlayerController.CheckPressDeffenseButton_P2()
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
                        //Debug.Log("ENTRE");
                        player2.barraDeEscudo.SetEnableDeffence(false);
                    }
                }
            }
            
        }
        public void CheckSpecialAttackButton_P2()
        {
            if (player2.GetEnableSpecialAttack())
            {
                if (InputPlayerController.SpecialAttackButton_P2())
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
        public void CheckInputPlayer2()
        {
            CheckBools_P2();
            if (player2 != null)
            {
                if (player2_PvP == null)
                {
                    CheckParabolaAttack_P2();
                    CheckVerticalUp_P2();
                    //CheckButtonJump_P2();
                    CheckVerticalDown_P2();
                    CheckVerticalCero_P2();
                    CheckHorizontalLeft_P2();
                    CheckHorizontalRight_P2();
                    CheckHorizontalCero_P2();
                    CheckAttackButton_P2();
                    CheckDeffenceButton_P2();
                    CheckSpecialAttackButton_P2();
                }
                else
                {
                    switch (player2_PvP.playerSelected)
                    {
                        case Player_PvP.PlayerSelected.Agresivo:
                            CheckParabolaAttack_P2();
                            CheckVerticalUp_P2();
                            //CheckButtonJump_P2();
                            CheckVerticalDown_P2();
                            CheckVerticalCero_P2();
                            CheckHorizontalLeft_P2();
                            CheckHorizontalRight_P2();
                            CheckHorizontalCero_P2();
                            CheckAttackButton_P2();
                            CheckDeffenceButton_P2();
                            CheckSpecialAttackButton_P2();
                            break;
                        case Player_PvP.PlayerSelected.Balanceado:
                            CheckParabolaAttack_P2();
                            CheckVerticalUp_P2();
                            //CheckButtonJump_P2();
                            CheckVerticalDown_P2();
                            CheckVerticalCero_P2();
                            CheckHorizontalLeft_P2();
                            CheckHorizontalRight_P2();
                            CheckHorizontalCero_P2();
                            CheckAttackButton_P2();
                            CheckDeffenceButton_P2();
                            CheckSpecialAttackButton_P2();
                            break;
                        case Player_PvP.PlayerSelected.Defensivo:
                            CheckParabolaAttack_P2();
                            CheckVerticalUp_P2();
                            //CheckButtonJump_P2();
                            CheckVerticalDown_P2();
                            CheckVerticalCero_P2();
                            CheckHorizontalLeft_P2();
                            CheckHorizontalRight_P2();
                            CheckHorizontalCero_P2();
                            CheckAttackButton_P2();
                            CheckDeffenceButton_P2();
                            CheckSpecialAttackButton_P2();
                            break;
                        case Player_PvP.PlayerSelected.Protagonista:
                            CheckParabolaAttack_P2();
                            CheckVerticalUp_P2();
                            //CheckButtonJump_P2();
                            CheckVerticalDown_P2();
                            CheckVerticalCero_P2();
                            CheckHorizontalLeft_P2();
                            CheckHorizontalRight_P2();
                            CheckHorizontalCero_P2();
                            CheckAttackButton_P2();
                            CheckDeffenceButton_P2();
                            CheckSpecialAttackButton_P2();
                            break;
                    }
                }

                /*if (!InputPlayerController.CheckPressDeffenseButton_P2() && !player2.GetIsJumping()
                    && player2.enumsPlayers.movimiento != EnumsPlayers.Movimiento.Saltar
                    && player2.enumsPlayers.movimiento != EnumsPlayers.Movimiento.SaltoAtaque
                    && player2.enumsPlayers.movimiento != EnumsPlayers.Movimiento.SaltoDefensa)
                {
                    player2.gridPlayer.CheckCuadrillaOcupada(player2.structsPlayer.dataPlayer.columnaActual, player2.structsPlayer.dataPlayer.CantCasillasOcupadas_X, player2.structsPlayer.dataPlayer.CantCasillasOcupadas_Y);
                }*/
            }
        }
        public void CheckSpriteParado_P2()
        {
            if (InputPlayerController.Vertical_Button_P2() == 0 && !InputPlayerController.SpecialAttackButton_P2())
            {
                player2.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.Parado;
            }
        }
        public void CheckSpriteMoverAdelante_P2()
        {
            bool cambioSpriteHabilitado = false;

            if (enableAnalogic)
            {
                cambioSpriteHabilitado = (((movingLeft_P2 || movingLeftAnalog_P2) && verticalStill_P2 && moveHorizontalPlayer2) || retroceder_P2);
            }
            else
            {
                cambioSpriteHabilitado = ((movingLeft_P2 && verticalStill_P2 && moveHorizontalPlayer2) || retroceder_P2);
            }
            if (cambioSpriteHabilitado)
            {
                player2.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.MoverAdelante;
            }

        }
        public void CheckSpriteMoverAtras_P2()
        {
            bool cambioSpriteHabilitado = false;
            if (enableAnalogic)
            {
                cambioSpriteHabilitado = (((movingRight_P2 || movingRightAnalog_P2) && verticalStill_P2 && moveHorizontalPlayer2) || avanzando_P2);
            }
            else
            {
                cambioSpriteHabilitado = ((movingRight_P2 && verticalStill_P2 && moveHorizontalPlayer2) || avanzando_P2);
            }
            if (cambioSpriteHabilitado)
            {
                player2.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.MoverAtras;
            }
        }
        public void CheckSpritesSalto_P2()
        {
            bool spriteSaltoHabilitado = false;
            if (enableAnalogic)
            {
                spriteSaltoHabilitado = (((movingUp_P2 || movingUpAnalog_P2) && movimientoNulo_P2 && horizontalStill_P2) || saltando_P2 || saltandoDefensa_P2 || saltandoAtaque_P2);
            }
            else
            {
                spriteSaltoHabilitado = ((movingUp_P2 && horizontalStill_P2 && movimientoNulo_P2) || saltando_P2 || saltandoDefensa_P2 || saltandoAtaque_P2);
            }

            if (spriteSaltoHabilitado)
            {
                if (InputPlayerController.CheckPressAttackButton_P2())
                {
                    player2.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.SaltoAtaque;
                }
                else if (InputPlayerController.CheckPressDeffenseButton_P2()
                     && player2.barraDeEscudo.GetValueShild() > player2.barraDeEscudo.porcentageNededForDeffence
                    && player2.barraDeEscudo.GetEnableDeffence())
                {
                    player2.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.SaltoDefensa;
                }
                else if (InputPlayerController.SpecialAttackButton_P2())
                {
                    //SPRITE O ANIMACION ATAQUE ESPECIAL JUGADOR.
                }
                else
                {
                    player2.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.Salto;
                }
                if (player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo)
                {
                    player2.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.Parado;
                }
            }
        }
        public void CheckSpritesParado_P2()
        {
            if (player2.enumsPlayers.movimiento != EnumsPlayers.Movimiento.Saltar
                    && player2.enumsPlayers.movimiento != EnumsPlayers.Movimiento.Agacharse && InputPlayerController.Horizontal_Button_P2() == 0)
            {
                if (InputPlayerController.CheckPressAttackButton_P2())
                {
                    player2.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.ParadoAtaque;
                }
                else if (InputPlayerController.CheckPressDeffenseButton_P2()
                    && player2.barraDeEscudo.GetValueShild() > player2.barraDeEscudo.porcentageNededForDeffence
                    && player2.barraDeEscudo.GetEnableDeffence())
                {
                    player2.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.ParadoDefensa;
                    if (player2_PvP != null)
                    {
                        player2_PvP.playerState = Player_PvP.State.Defendido;
                    }
                }
                else
                {
                    player2.spritePlayerActual.delaySpriteRecibirDanio = player2.spritePlayerActual.GetAuxDelaySpriteRecibirDanio();
                }
            }
        }
        public void CheckSpritesAgachado_P2()
        {
            /*horizontalStill_P1 = InputPlayerController.Horizontal_Button_P1() == 0;
            //Debug.Log(InputPlayerController.Vertical_Analogico_P1());
            movingDown_P1 = InputPlayerController.Vertical_Button_P1() < 0;
            agachandose_P1 = player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Agacharse;
            movingDownAnalog_P1 = InputPlayerController.Vertical_Analogico_P1() > 0.5f;*/

            bool spriteAgachadoHabilitado = false;
            if (enableAnalogic)
            {
                spriteAgachadoHabilitado = (movingDown_P2 || movingDownAnalog_P2) && !saltando_P2;
            }
            else
            {
                spriteAgachadoHabilitado = (movingDown_P2 && !saltando_P2);
            }
            //Debug.Log(player1.GetIsDuck());
            if (spriteAgachadoHabilitado && player2.GetIsDuck())
            {
                if (InputPlayerController.CheckPressAttackButton_P2())
                {
                    player2.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.AgachadoAtaque;
                }
                else if (InputPlayerController.CheckPressDeffenseButton_P2()
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
        public void CheckSpritePlayer2()
        {
            if (player2.spritePlayerActual.ActualSprite == SpritePlayer.SpriteActual.RecibirDanio || player2.spritePlayerActual.ActualSprite == SpritePlayer.SpriteActual.ContraAtaqueParado)
            {
                if (player2.spritePlayerActual.ActualSprite == SpritePlayer.SpriteActual.RecibirDanio)
                {
                    player2.spritePlayerActual.CheckDeleyRecibirDanio();
                }
                else
                {
                    player2.spritePlayerActual.CheckDeleyContraAtaque();
                }
            }
            else
            {
                if (player2_PvP == null)
                {
                    CheckSpriteParado_P2();
                    CheckSpriteMoverAdelante_P2();
                    CheckSpriteMoverAtras_P2();
                    CheckSpritesSalto_P2();
                    CheckSpritesParado_P2();
                    CheckSpritesAgachado_P2();
                }
                else
                {
                    switch (player2_PvP.playerSelected)
                    {
                        case Player_PvP.PlayerSelected.Agresivo:
                            CheckSpriteParado_P2();
                            CheckSpriteMoverAdelante_P2();
                            CheckSpriteMoverAtras_P2();
                            CheckSpritesSalto_P2();
                            CheckSpritesParado_P2();
                            CheckSpritesAgachado_P2();
                            break;
                        case Player_PvP.PlayerSelected.Balanceado:
                            CheckSpriteParado_P2();
                            CheckSpriteMoverAdelante_P2();
                            CheckSpriteMoverAtras_P2();
                            CheckSpritesSalto_P2();
                            CheckSpritesParado_P2();
                            CheckSpritesAgachado_P2();
                            break;
                        case Player_PvP.PlayerSelected.Defensivo:
                            CheckSpriteParado_P2();
                            CheckSpriteMoverAdelante_P2();
                            CheckSpriteMoverAtras_P2();
                            CheckSpritesSalto_P2();
                            CheckSpritesParado_P2();
                            CheckSpritesAgachado_P2();
                            break;
                        case Player_PvP.PlayerSelected.Protagonista:
                            CheckSpriteParado_P2();
                            CheckSpriteMoverAdelante_P2();
                            CheckSpriteMoverAtras_P2();
                            CheckSpritesSalto_P2();
                            CheckSpritesParado_P2();
                            CheckSpritesAgachado_P2();
                            break;
                    }
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
