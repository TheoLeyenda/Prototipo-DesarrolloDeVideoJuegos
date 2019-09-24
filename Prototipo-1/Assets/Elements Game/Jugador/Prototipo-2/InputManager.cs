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
                player1.enumsPlayers.movimiento = EnumsPlayers.Movimiento.Saltar;
            }
            else if (InputPlayerController.Vertical_Button_P1() < 0)
            {
                player1.enumsPlayers.movimiento = EnumsPlayers.Movimiento.Agacharse;
            }

            if (InputPlayerController.Horizontal_Button_P1() < 0)
            {
                player1.enumsPlayers.movimiento = EnumsPlayers.Movimiento.MoverAtras;
            }
            else if (InputPlayerController.Horizontal_Button_P1() > 0)
            {
                player1.enumsPlayers.movimiento = EnumsPlayers.Movimiento.MoverAdelante;
            }
        }
        public void CheckInputPlayer2()
        {

        }
    }
}
