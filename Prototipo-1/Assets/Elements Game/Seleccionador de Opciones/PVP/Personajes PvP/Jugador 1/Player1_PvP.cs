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
        public enum State
        {
            Defendido,
        }
        // Start is called before the first frame update
        private void Start()
        {
            
        }
        // Update is called once per frame
    }
}
