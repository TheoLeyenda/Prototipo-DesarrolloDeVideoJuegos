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
        // Update is called once per frame
        private void Start()
        {
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
            CheckInputPlayer1();
            CheckSpritePlayer1();
            CheckInputPlayer2();
            CheckSpritePlayer2();
        }
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
            if (InputPlayerController.AttackButton_P1())
            {
                player1.SetControllerJoystick(true);
                if (player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Saltar && InputPlayerController.Vertical_Button_P1() < 0)
                {
                    player1.AttackDown();
                }
                else
                {
                    player1.Attack(Proyectil.DisparadorDelProyectil.Jugador);
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
                player1.SpecialAttack();
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
            if (!InputPlayerController.CheckPressDeffenseButton_P1())
            {
                player1.gridPlayer.CheckCuadrillaOcupada(player1.structsPlayer.dataPlayer.columnaActual, player1.structsPlayer.dataPlayer.CantCasillasOcupadas_X, player1.structsPlayer.dataPlayer.CantCasillasOcupadas_Y);
            }
        }
        public void CheckInputPlayer2()
        {

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
                    player1_PvP.playerState = Player_PvP.State.Defendido;
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
            if (player1.spritePlayerActual.ActualSprite == SpritePlayer.SpriteActual.RecibirDanio || player1.spritePlayerActual.ActualSprite == SpritePlayer.SpriteActual.ContraAtaque)
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
        public void CheckSpritePlayer2()
        {

        }
    }
}
