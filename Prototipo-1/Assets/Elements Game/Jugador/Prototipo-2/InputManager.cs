using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototipo_2 {
    public class InputManager : MonoBehaviour
    {
        public Player player1;
        public Player player2;
        // Update is called once per frame
        void Update()
        {
            CheckInputPlayer1();
            CheckInputPlayer2();
        }
        public void CheckInputPlayer1()
        {
            if (InputPlayerController.Vertical_Button_P1() > 0)
            {
                player1.MovementJump();
            }
            else if (InputPlayerController.Vertical_Button_P1() < 0)
            {
                if (player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo)
                {
                    player1.MovementDuck();
                    player1.enumsPlayers.movimiento = EnumsPlayers.Movimiento.Agacharse;
                    player1.spritePlayerActual.ActualSprite = SpritePlayer.SpriteActual.Agachado;
                }
            }
            else if (InputPlayerController.Vertical_Button_P1() == 0 && 
                (player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Agacharse 
                || player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.AgacharseAtaque 
                || player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.AgacheDefensa))
            {
                player1.enumsPlayers.movimiento = EnumsPlayers.Movimiento.Nulo;
            }

            if (InputPlayerController.Horizontal_Button_P1() < 0)
            {
                if (player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo)
                {
                    player1.MovementLeft();
                }
            }
            else if (InputPlayerController.Horizontal_Button_P1() > 0)
            {
                if (player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo)
                {
                    player1.MovementRight();
                }
            }
        }
        public void CheckInputPlayer2()
        {

        }
    }
}
