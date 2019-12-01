using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototipo_2 { 
    public class ControladorDeMenuesDePausa : MonoBehaviour
    {
        public GameObject menuPlayer1;
        public GameObject menuPlayer2;
        // Update is called once per frame
        void Update()
        {
            CheckMenu();
        }
        public void CheckMenu()
        {
            if (InputPlayerController.GetInputButtonDown("PauseButton_P1"))
            {
                menuPlayer1.SetActive(true);
                menuPlayer2.SetActive(false);
            }
            else if(InputPlayerController.GetInputButtonDown("PauseButton_P2"))
            {
                menuPlayer1.SetActive(false);
                menuPlayer2.SetActive(true);
            }
        }
    }
}
