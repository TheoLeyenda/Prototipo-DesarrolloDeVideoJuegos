using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototipo_2
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instanceGameManager;
        public EnumsGameManager enumsGameManager;
        [HideInInspector]
        public bool InGameOverScene;
        [HideInInspector]
        public bool generateEnemy;
        public int RondasPorJefe;
        public bool SiglePlayer;
        public bool MultiPlayer;//(EN CASO DE TENER MULTYPLAYER EL JUEGO SE TRANFORMA EN UN JUEGO POR TURNOS)
        [HideInInspector]
        public int countEnemysDead;

        private int roundCombat;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void ResetRoundCombat(bool PlayerDeath)
        {
            if (!PlayerDeath)
            {
                roundCombat = 0;
            }
            else if (PlayerDeath)
            {
                roundCombat = 1;
            }
        }
    }
}
