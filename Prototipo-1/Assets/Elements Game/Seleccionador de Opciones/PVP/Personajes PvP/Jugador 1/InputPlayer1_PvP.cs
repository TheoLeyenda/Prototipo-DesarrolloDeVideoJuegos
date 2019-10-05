using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototipo_2
{
    public class InputPlayer1_PvP : MonoBehaviour
    {
        // Start is called before the first frame update
        public Player player;
        public void InputBalanceado()
        {
            player.InputKeyBoard();
        }
        public void InputDefensivo()
        {
            /*if (Input.GetKeyDown(ButtonAttack) && enumsPlayers.movimiento == EnumsPlayers.Movimiento.Saltar && Input.GetKey(KeyCode.DownArrow))
            {
                SetControllerJoystick(false);
                AttackDown();
            }*/
            if (Input.GetKeyDown(player.ButtonAttack))
            {
                player.SetControllerJoystick(false);
                if (!Input.GetKey(player.ButtonDeffence))
                {
                    player.Attack( Proyectil.DisparadorDelProyectil.Jugador);
                }
            }
            if (Input.GetKey(player.ButtonDeffence))
            {
                player.SetControllerJoystick(false);
                player.Deffence();
            }
            if (Input.GetKeyDown(player.ButtonSpecialAttack))
            {
                player.SetControllerJoystick(false);
                if (!player.GetIsDuck() && !player.GetIsJumping() && player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo || player.SpecialAttackEnabelEveryMoment)
                {
                    player.SpecialAttack();
                }
            }
            if (Input.GetKeyUp(player.ButtonDeffence))
            {
                player.SetControllerJoystick(false);
                player.gridPlayer.CheckCuadrillaOcupada(player.structsPlayer.dataPlayer.columnaActual, player.structsPlayer.dataPlayer.CantCasillasOcupadas_X, player.structsPlayer.dataPlayer.CantCasillasOcupadas_Y);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo
                || player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.MoverAtras)
            {
                player.SetControllerJoystick(false);
                player.MovementLeft();
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo ||
                player.enumsPlayers.movimiento == EnumsPlayers.Movimiento.MoverAdelante)
            {
                player.SetControllerJoystick(false);
                player.MovementRight();
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
            player.InputKeyBoard();
        }
    }
}
