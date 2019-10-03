using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototipo_2 {
    public class DataCombatPvP : MonoBehaviour
    {
        // ESTE SCRIPT DEBE TOMAR LA DEDICION CORRESPONDIENTE EN CUANTO AL NIVEL ELEJIDO Y PERSONAJE ELEJIDO DEPENDIENDO DE LA INFO QUE LE PASE EL
        // STRUCT GAME MANAGER
        public enum Player_Selected
        {
            Nulo,
            Balanceado,
            Agresivo,
            Defensivo,
            Protagonista,
            Count
        }
        public enum Level_Selected
        {
            Nulo,
            Anatomia,
            Historia,
            EducacionFisica,
            Arte,
            Matematica,
            Quimica,
            Programacion,
            TESIS,
            Cafeteria,
            Count
        }
        public Player_Selected player1_selected;
        public Player_Selected player2_selected;
        public Level_Selected level_selected;
        private GameManager gm;
        void Start()
        {
            if (GameManager.instanceGameManager != null)
            {
                gm = GameManager.instanceGameManager;
            }
            InitDataCombat();
        }
        public void InitDataCombat()
        {
            player1_selected = gm.structGameManager.gm_dataCombatPvP.player1_selected;
            player2_selected = gm.structGameManager.gm_dataCombatPvP.player2_selected;
            level_selected = gm.structGameManager.gm_dataCombatPvP.level_selected;
            switch (player1_selected)
            {
                case Player_Selected.Agresivo:
                    Debug.Log("Elegi Agresivo");
                    break;
                case Player_Selected.Balanceado:
                    Debug.Log("Elegi Balanceado");
                    break;
                case Player_Selected.Defensivo:
                    Debug.Log("Elegi Defensivo");
                    break;
                case Player_Selected.Protagonista:
                    Debug.Log("Elegi Protagonista");
                    break;
            }
            switch (player2_selected)
            {

            }
            switch (level_selected)
            {

            }
        }
        // ESTE SCRIPT DEBE TOMAR LA DEDICION CORRESPONDIENTE EN CUANTO AL NIVEL ELEJIDO Y PERSONAJE ELEJIDO DEPENDIENDO DE LA INFO QUE LE PASE EL
        // STRUCT GAME MANAGER
    }
}
