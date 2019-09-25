using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototipo_2 {
    public class InputManager : MonoBehaviour
    {
        public Player player1;
        public Player player2;
        private bool movePlayer1;
        private bool movePlayer2;
        // Update is called once per frame
        private void Start()
        {
            movePlayer1 = true;
            movePlayer2 = true;
        }
        void Update()
        {
            CheckInputPlayer1();
            CheckInputPlayer2();
        }
        public void CheckInputPlayer1()
        {
            if (InputPlayerController.Vertical_Button_P1() > 0)
            {
                if (player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo)
                {
                    player1.MovementJump();
                }
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

            if (InputPlayerController.Horizontal_Button_P1() < 0 && movePlayer1)
            {
                if (player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo)
                {
                    movePlayer1 = false;
                    player1.MovementLeft();
                }
            }
            else if (InputPlayerController.Horizontal_Button_P1() > 0 && movePlayer1)
            {
                if (player1.enumsPlayers.movimiento == EnumsPlayers.Movimiento.Nulo)
                {
                    movePlayer1 = false;
                    player1.MovementRight();
                }
            }
            else if (InputPlayerController.Horizontal_Button_P1() == 0)
            {
                movePlayer1 = true;
            }

            if (InputPlayerController.AttackButton_P1())
            {
                player1.Attack();
            }
        }
        public void CheckInputPlayer2()
        {

        }
    }
}
