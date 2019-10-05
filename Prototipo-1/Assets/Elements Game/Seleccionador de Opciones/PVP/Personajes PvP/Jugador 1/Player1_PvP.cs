using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototipo_2
{
    public class Player1_PvP : MonoBehaviour
    {
        public enum PlayerSelected
        {
            Nulo,
            Protagonista,
            Balanceado,
            Agresivo,
            Defensivo,
        }
        public PlayerSelected playerSelected;
        public InputPlayer1_PvP inputControllerPlayer1;
        public enum State
        {
            Defendido,
        }
        // Start is called before the first frame update
        private void Start()
        {
            
        }
        // Update is called once per frame
        void Update()
        {
            switch (playerSelected)
            {
                case PlayerSelected.Protagonista:
                    inputControllerPlayer1.InputProtagonista();
                    break;
                case PlayerSelected.Agresivo:
                    inputControllerPlayer1.InputAgresivo();
                    break;
                case PlayerSelected.Defensivo:
                    inputControllerPlayer1.InputDefensivo();
                    break;
                case PlayerSelected.Balanceado:
                    inputControllerPlayer1.InputBalanceado();
                    break;
            }
        }
        
    }
}
