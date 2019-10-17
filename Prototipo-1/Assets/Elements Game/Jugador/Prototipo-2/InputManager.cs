using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototipo_2 {
    public class InputManager : MonoBehaviour
    {
        public Player player1;
        public Player_PvP player1_PvP;
        public Player player2;
        public Player_PvP player2_PvP;
        public bool FindPlayersAndPlayers_PvP;

        private bool moveHorizontalPlayer1;
        private bool moveVerticalPlayer1;
        private bool moveVerticalPlayer2;
        private bool moveHorizontalPlayer2;

        private bool enableMovementPlayer1;
        private bool enableMovementPlayer2;
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
        void Update()
        {
            //Debug.Log(player1.enumsPlayers.movimiento);
            if (player1 != null && player1.gameObject.activeSelf && enableMovementPlayer1)
            {
                CheckInputPlayer1();
                CheckSpritePlayer1();
            }
            if (player2 != null && player2.gameObject.activeSelf && enableMovementPlayer2)
            {
                CheckInputPlayer2();
                CheckSpritePlayer2();
            }
        }

        //----- FUNCIONES Y CONTROLES DEL JUGADOR 1 -----//
        public void CheckVerticalUp_P1()
        {
            if (player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo && InputPlayerController.Vertical_Button_P1() > 0 && moveVerticalPlayer1
                || player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Saltar)
            {
                player1.SetControllerJoystick(true);
                player1.MovementJump();
                moveVerticalPlayer1 = false;
                player1.SetIsDuck(false);
            }
        }
        public void CheckVerticalDown_P1()
        {
            if (InputPlayerController.Vertical_Button_P1() < 0 && player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo
                || player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Agacharse)
            {
                player1.SetControllerJoystick(true);
                player1.MovementDuck();
                player1.enumsPlayers.movimiento = EnumsPlayers.Movimiento.Agacharse;
                player1.SetIsDuck(true);
            }
            if (player1.GetIsDuck())
            {
                player1.structsPlayer.dataPlayer.CantCasillasOcupadas_Y = player1.structsPlayer.dataPlayer.CantCasillasOcupadasAgachado;
            }
            else
            {
                player1.structsPlayer.dataPlayer.CantCasillasOcupadas_Y = player1.structsPlayer.dataPlayer.CantCasillasOcupadasParado;
            }
        }
        public void CheckVerticalCero_P1()
        {
            if (InputPlayerController.Vertical_Button_P1() == 0 &&
                (player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Agacharse
                || player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.AgacharseAtaque
                || player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.AgacheDefensa))
            {
                player1.enumsPlayers.movimiento = EnumsPlayers.Movimiento.Nulo;
                player1.SetIsDuck(false);
            }
            else if (InputPlayerController.Vertical_Button_P1() == 0)
            {
                moveVerticalPlayer1 = true;
                player1.SetIsDuck(false);
            }
        }
        public void CheckHorizontalLeft_P1()
        {
            if (InputPlayerController.Horizontal_Button_P1() < 0 && moveHorizontalPlayer1 && player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo
                || player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.MoverAtras)
            {
                player1.SetControllerJoystick(true);
                moveHorizontalPlayer1 = false;
                player1.MovementLeft();
                player1.SetIsDuck(false);

            }
        }
        public void CheckHorizontalRight_P1()
        {
            if (InputPlayerController.Horizontal_Button_P1() > 0 && moveHorizontalPlayer1 && player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo 
                || player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.MoverAdelante)
            {
                player1.SetControllerJoystick(true);
                moveHorizontalPlayer1 = false;
                player1.MovementRight();
                player1.SetIsDuck(false);
            }
            
        }
        public void CheckHorizontalCero_P1()
        {
            if (InputPlayerController.Horizontal_Button_P1() == 0)
            {
                moveHorizontalPlayer1 = true;
            }
        }
        public void CheckAttackButton_P1()
        {
            if (InputPlayerController.AttackButton_P1() && player1.GetEnableAttack())
            {
                //Debug.Log("JUGADOR 1 ATAQUE ACTIVED");
                player1.SetControllerJoystick(true);
                if (player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Saltar && InputPlayerController.Vertical_Button_P1() < 0)
                {
                    player1.spritePlayerActual.PlayAnimation("Ataque Abajo Salto protagonista");
                }
                else if (player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Saltar && InputPlayerController.Vertical_Button_P1() >= 0)
                {
                    player1.spritePlayerActual.PlayAnimation("Ataque Salto protagonista");
                }
                else
                {
                    if (!player1.GetIsDuck())
                    {
                        player1.spritePlayerActual.PlayAnimation("Ataque protagonista");
                    }
                    else if (player1.GetIsDuck())
                    {
                        player1.spritePlayerActual.PlayAnimation("Ataque Agachado protagonista");
                    }
                }
            }
        }
        public void CheckDeffenceButton_P1()
        {
            if (InputPlayerController.CheckPressDeffenseButton_P1())
            {
                player1.SetControllerJoystick(true);
                player1.Deffence();
            }
        }
        public void CheckSpecialAttackButton_P1()
        {
            if (InputPlayerController.SpecialAttackButton_P1())
            {
                player1.SpecialAttack(Proyectil.DisparadorDelProyectil.Jugador1);
            }
        }
        public void CheckInputPlayer1()
        {
            if (player1_PvP == null)
            {
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
                        CheckVerticalUp_P1();
                        CheckVerticalCero_P1();
                        CheckHorizontalLeft_P1();
                        CheckHorizontalRight_P1();
                        CheckHorizontalCero_P1();
                        CheckAttackButton_P1();
                        CheckDeffenceButton_P1();
                        CheckDeffenceButton_P1();
                        CheckSpecialAttackButton_P1();
                        break;
                    case Player_PvP.PlayerSelected.Balanceado:
                        CheckVerticalUp_P1();
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
                        CheckVerticalCero_P1();
                        CheckHorizontalLeft_P1();
                        CheckHorizontalRight_P1();
                        CheckHorizontalCero_P1();
                        CheckAttackButton_P1();
                        CheckDeffenceButton_P1();
                        CheckSpecialAttackButton_P1();
                        break;
                    case Player_PvP.PlayerSelected.Protagonista:
                        CheckVerticalUp_P1();
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
            if (!InputPlayerController.CheckPressDeffenseButton_P1() && !player1.GetIsJumping()
                && player1.enumsPlayers.movimiento != EnumsPlayers.Movimiento.Saltar
                && player1.enumsPlayers.movimiento != EnumsPlayers.Movimiento.SaltoAtaque
                && player1.enumsPlayers.movimiento != EnumsPlayers.Movimiento.SaltoDefensa)
            {
                player1.gridPlayer.CheckCuadrillaOcupada(player1.structsPlayer.dataPlayer.columnaActual, player1.structsPlayer.dataPlayer.CantCasillasOcupadas_X, player1.structsPlayer.dataPlayer.CantCasillasOcupadas_Y);
            }
        }
        
        public void CheckSpriteParado_P1()
        {
            if (InputPlayerController.Vertical_Button_P1() == 0)
            {
                player1.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.Parado;
            }
        }
        public void CheckSpriteMoverAdelante_P1()
        {
            if (InputPlayerController.Horizontal_Button_P1() > 0 && InputPlayerController.Vertical_Button_P1() == 0
                    || player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.MoverAdelante)
            {
                if (player1.structsPlayer.dataPlayer.columnaActual < player1.gridPlayer.GetCuadrilla_columnas() - 1)
                {
                    player1.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.MoverAdelante;
                }
                else
                {
                    player1.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.Parado;
                }
            }
        }
        public void CheckSpriteMoverAtras_P1()
        {
            if (InputPlayerController.Horizontal_Button_P1() < 0 && InputPlayerController.Vertical_Button_P1() == 0
                    || player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.MoverAtras)
            {
                if (player1.structsPlayer.dataPlayer.columnaActual > 0)
                {
                    player1.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.MoverAtras;
                }
                else
                {
                    player1.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.Parado;
                }
            }
        }
        public void CheckSpritesSalto_P1()
        {
            if (InputPlayerController.Vertical_Button_P1() > 0 && InputPlayerController.Horizontal_Button_P1() == 0 && player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo || player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Saltar || player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.SaltoAtaque || player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.SaltoDefensa)
            {
                if (InputPlayerController.CheckPressAttackButton_P1())
                {
                    player1.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.SaltoAtaque;
                }
                else if (InputPlayerController.CheckPressDeffenseButton_P1())
                {
                    player1.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.SaltoDefensa;
                }
                else if (InputPlayerController.CheckSpecialAttackButton_P1())
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
                else if (InputPlayerController.CheckPressDeffenseButton_P1())
                {
                    player1.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.ParadoDefensa;
                    if (player1_PvP != null)
                    {
                        player1_PvP.playerState = Player_PvP.State.Defendido;
                    }
                }
                else
                {
                    player1.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.Parado;
                    player1.spritePlayerActual.delaySpriteRecibirDanio = player1.spritePlayerActual.GetAuxDelaySpriteRecibirDanio();
                }
            }
        }
        public void CheckSpritesAgachado_P1()
        {
            if (InputPlayerController.Vertical_Button_P1() < 0 && player1.enumsPlayers.movimiento != EnumsPlayers.Movimiento.Saltar)
            {
                if (InputPlayerController.CheckPressAttackButton_P1())
                {
                    player1.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.AgachadoAtaque;
                }
                else if (InputPlayerController.CheckPressDeffenseButton_P1())
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
                            CheckSpritesParado_P1();
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

        public void CheckVerticalUp_P2()
        {
            if (player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo && InputPlayerController.Vertical_Button_P2() > 0 && moveVerticalPlayer2
                || player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Saltar)
            {
                player2.SetControllerJoystick(true);
                player2.MovementJump();
                moveVerticalPlayer1 = false;
                player2.SetIsDuck(false);
            }
        }
        public void CheckVerticalDown_P2()
        {
            if (InputPlayerController.Vertical_Button_P2() < 0 && player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo
                || player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Agacharse)
            {
                player2.SetControllerJoystick(true);
                player2.MovementDuck();
                player2.enumsPlayers.movimiento = EnumsPlayers.Movimiento.Agacharse;
                player2.SetIsDuck(true);
            }
            if (player2.GetIsDuck())
            {
                player2.structsPlayer.dataPlayer.CantCasillasOcupadas_Y = player2.structsPlayer.dataPlayer.CantCasillasOcupadasAgachado;
            }
            else
            {
                player2.structsPlayer.dataPlayer.CantCasillasOcupadas_Y = player2.structsPlayer.dataPlayer.CantCasillasOcupadasParado;
            }
        }
        public void CheckVerticalCero_P2()
        {
            if (InputPlayerController.Vertical_Button_P2() == 0 &&
                (player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Agacharse
                || player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.AgacharseAtaque
                || player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.AgacheDefensa))
            {
                player2.enumsPlayers.movimiento = EnumsPlayers.Movimiento.Nulo;
                player2.SetIsDuck(false);
            }
            else if (InputPlayerController.Vertical_Button_P2() == 0)
            {
                moveVerticalPlayer2 = true;
                player2.SetIsDuck(false);
            }
        }
        public void CheckHorizontalLeft_P2()
        {
            if (player2.LookingForward)
            {
                if (InputPlayerController.Horizontal_Button_P2() < 0 && moveHorizontalPlayer2 && player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo
                    || player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.MoverAtras)
                {
                    player2.SetControllerJoystick(true);
                    moveHorizontalPlayer2 = false;
                    player2.MovementLeft();
                    player2.SetIsDuck(false);

                }
            }
            else if (player2.LookingBack)
            {
                if (InputPlayerController.Horizontal_Button_P2() < 0 && moveHorizontalPlayer2 && player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo
                    || player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.MoverAtras)
                {
                    player2.SetControllerJoystick(true);
                    moveHorizontalPlayer2 = false;
                    player2.MovementLeft();
                    player2.SetIsDuck(false);

                }
            }
        }
        public void CheckHorizontalRight_P2()
        {
            if (player2.LookingForward)
            {
                if (InputPlayerController.Horizontal_Button_P2() > 0 && moveHorizontalPlayer2 && player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo
                || player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.MoverAdelante)
                {
                    player2.SetControllerJoystick(true);
                    moveHorizontalPlayer2 = false;
                    player2.MovementRight();
                    player2.SetIsDuck(false);
                }
            }
            else if (player2.LookingBack)
            {
                if (InputPlayerController.Horizontal_Button_P2() > 0 && moveHorizontalPlayer2 && player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo
                || player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.MoverAdelante)
                {
                    player2.SetControllerJoystick(true);
                    moveHorizontalPlayer2 = false;
                    player2.MovementRight();
                    player2.SetIsDuck(false);
                }
            }

        }
        public void CheckHorizontalCero_P2()
        {
            if (InputPlayerController.Horizontal_Button_P2() == 0)
            {
                moveHorizontalPlayer2 = true;
            }
        }
        public void CheckAttackButton_P2()
        {
            if (InputPlayerController.AttackButton_P2())
            {
                //Debug.Log("JUGADOR 2 ATAQUE ACTIVED");
                player2.SetControllerJoystick(true);
                if (player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Saltar && InputPlayerController.Vertical_Button_P2() < 0)
                {
                    player2.AttackDown(Proyectil.DisparadorDelProyectil.Jugador2);
                }
                else
                {
                    player2.Attack(Proyectil.DisparadorDelProyectil.Jugador2);
                }
            }
        }
        public void CheckDeffenceButton_P2()
        {
            if (InputPlayerController.CheckPressDeffenseButton_P2())
            {
                player2.SetControllerJoystick(true);
                player2.Deffence();
            }
        }
        public void CheckSpecialAttackButton_P2()
        {
            if (InputPlayerController.SpecialAttackButton_P2())
            {
                player2.SpecialAttack(Proyectil.DisparadorDelProyectil.Jugador2);
            }
        }
        public void CheckInputPlayer2()
        {
            if (player2 != null)
            {
                if (player2_PvP == null)
                {
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
                else
                {
                    switch (player2_PvP.playerSelected)
                    {
                        case Player_PvP.PlayerSelected.Agresivo:
                            CheckVerticalUp_P2();
                            CheckVerticalCero_P2();
                            CheckHorizontalLeft_P2();
                            CheckHorizontalRight_P2();
                            CheckHorizontalCero_P2();
                            CheckAttackButton_P2();
                            CheckDeffenceButton_P2();
                            CheckDeffenceButton_P2();
                            CheckSpecialAttackButton_P2();
                            break;
                        case Player_PvP.PlayerSelected.Balanceado:
                            CheckVerticalUp_P2();
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
                            CheckVerticalCero_P2();
                            CheckHorizontalLeft_P2();
                            CheckHorizontalRight_P2();
                            CheckHorizontalCero_P2();
                            CheckAttackButton_P2();
                            CheckDeffenceButton_P2();
                            CheckSpecialAttackButton_P2();
                            break;
                        case Player_PvP.PlayerSelected.Protagonista:
                            CheckVerticalUp_P2();
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

                if (!InputPlayerController.CheckPressDeffenseButton_P2() && !player2.GetIsJumping()
                    && player2.enumsPlayers.movimiento != EnumsPlayers.Movimiento.Saltar
                    && player2.enumsPlayers.movimiento != EnumsPlayers.Movimiento.SaltoAtaque
                    && player2.enumsPlayers.movimiento != EnumsPlayers.Movimiento.SaltoDefensa)
                {
                    player2.gridPlayer.CheckCuadrillaOcupada(player2.structsPlayer.dataPlayer.columnaActual, player2.structsPlayer.dataPlayer.CantCasillasOcupadas_X, player2.structsPlayer.dataPlayer.CantCasillasOcupadas_Y);
                }
            }
        }
        public void CheckSpriteParado_P2()
        {
            if (InputPlayerController.Vertical_Button_P2() == 0)
            {
                player2.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.Parado;
            }
        }
        public void CheckSpriteMoverAtras_P2()
        {
            if (InputPlayerController.Horizontal_Button_P2() > 0 && InputPlayerController.Vertical_Button_P2() == 0
                    || player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.MoverAdelante)
            {
                if (player2.structsPlayer.dataPlayer.columnaActual < player2.gridPlayer.GetCuadrilla_columnas() - 1)
                {
                    player2.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.MoverAtras;
                }
                else
                {
                    player2.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.Parado;
                }
            }
        }
        public void CheckSpriteMoverAdelante_P2()
        {
            if (InputPlayerController.Horizontal_Button_P2() < 0 && InputPlayerController.Vertical_Button_P2() == 0
                    || player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.MoverAtras)
            {
                if (player2.structsPlayer.dataPlayer.columnaActual > 0)
                {
                    player2.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.MoverAdelante;
                }
                else
                {
                    player2.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.Parado;
                }
            }
        }
        public void CheckSpritesSalto_P2()
        {
            if (InputPlayerController.Vertical_Button_P2() > 0 && InputPlayerController.Horizontal_Button_P2() == 0 && player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo || player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Saltar || player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.SaltoAtaque || player2.enumsPlayers.movimiento == EnumsPlayers.Movimiento.SaltoDefensa)
            {
                if (InputPlayerController.CheckPressAttackButton_P2())
                {
                    player2.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.SaltoAtaque;
                }
                else if (InputPlayerController.CheckPressDeffenseButton_P2())
                {
                    player2.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.SaltoDefensa;
                }
                else if (InputPlayerController.CheckSpecialAttackButton_P2())
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
                else if (InputPlayerController.CheckPressDeffenseButton_P1())
                {
                    player2.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.ParadoDefensa;
                    player2_PvP.playerState = Player_PvP.State.Defendido;
                }
                else
                {
                    player2.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.Parado;
                    player2.spritePlayerActual.delaySpriteRecibirDanio = player2.spritePlayerActual.GetAuxDelaySpriteRecibirDanio();
                }
            }
        }
        public void CheckSpritesAgachado_P2()
        {
            if (InputPlayerController.Vertical_Button_P2() < 0 && player2.enumsPlayers.movimiento != EnumsPlayers.Movimiento.Saltar)
            {
                if (InputPlayerController.CheckPressAttackButton_P2())
                {
                    player2.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.AgachadoAtaque;
                }
                else if (InputPlayerController.CheckPressDeffenseButton_P2())
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
                            CheckSpritesParado_P2();
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
