using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototipo_2
{
    public class InputPlayer1_PvP : Player1_PvP
    {
        // Start is called before the first frame update
        public void InputBalanceado()
        {
            InputKeyBoard();
        }
        public void InputDefensivo()
        {
            /*if (Input.GetKeyDown(ButtonAttack) && enumsPlayers.movimiento == EnumsPlayers.Movimiento.Saltar && Input.GetKey(KeyCode.DownArrow))
            {
                SetControllerJoystick(false);
                AttackDown();
            }*/
            if (Input.GetKeyDown(ButtonAttack))
            {
                SetControllerJoystick(false);
                if (!Input.GetKey(ButtonDeffence))
                {
                    Attack( Proyectil.DisparadorDelProyectil.Jugador);
                }
            }
            if (Input.GetKey(ButtonDeffence))
            {
                SetControllerJoystick(false);
                Deffence();
            }
            if (Input.GetKeyDown(ButtonSpecialAttack))
            {
                SetControllerJoystick(false);
                if (!GetIsDuck() && !GetIsJumping() && enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo || SpecialAttackEnabelEveryMoment)
                {
                    SpecialAttack();
                }
            }
            if (Input.GetKeyUp(ButtonDeffence))
            {
                SetControllerJoystick(false);
                gridPlayer.CheckCuadrillaOcupada(structsPlayer.dataPlayer.columnaActual, structsPlayer.dataPlayer.CantCasillasOcupadas_X, structsPlayer.dataPlayer.CantCasillasOcupadas_Y);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo
                || enumsPlayers.movimiento == EnumsPlayers.Movimiento.MoverAtras)
            {
                SetControllerJoystick(false);
                MovementLeft();
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo ||
                enumsPlayers.movimiento == EnumsPlayers.Movimiento.MoverAdelante)
            {
                SetControllerJoystick(false);
                MovementRight();
            }
            /*if (Input.GetKeyDown(KeyCode.UpArrow) && enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo
                || enumsPlayers.movimiento == EnumsPlayers.Movimiento.Saltar)
            {
                SetControllerJoystick(false);
                MovementJump();
            }

            if (Input.GetKey(KeyCode.DownArrow) && enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo)
            {
                SetControllerJoystick(false);
                MovementDuck();
            }
            else if (enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo && GetIsDuck())
            {
                SetControllerJoystick(false);
                SetIsDuck(false);
                colliderSprite.enabled = true;
                gridPlayer.CheckCuadrillaOcupada(structsPlayer.dataPlayer.columnaActual, structsPlayer.dataPlayer.CantCasillasOcupadas_X, structsPlayer.dataPlayer.CantCasillasOcupadas_Y);
            }*/
        }
        public void InputAgresivo()
        {

        }
        public void InputProtagonista()
        {
            InputKeyBoard();
        }
    }
}
