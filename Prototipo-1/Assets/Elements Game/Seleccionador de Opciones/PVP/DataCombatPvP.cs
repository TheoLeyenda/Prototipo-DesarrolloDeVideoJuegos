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
                case Level_Selected.Anatomia:
                    Debug.Log("Elegi aula anatomia");
                    break;
                case Level_Selected.Historia:
                    Debug.Log("Elegi aula Historia");
                    break;
                case Level_Selected.EducacionFisica:
                    Debug.Log("Elegi aula Educacion Fisica");
                    break;
                case Level_Selected.Arte:
                    Debug.Log("Elegi aula Arte");
                    break;
                case Level_Selected.Matematica:
                    Debug.Log("Elegi aula Matematica");
                    break;
                case Level_Selected.Quimica:
                    Debug.Log("Elegi aula Quimica");
                    break;
                case Level_Selected.Programacion:
                    Debug.Log("Elegi aula Programacion");
                    break;
                case Level_Selected.TESIS:
                    Debug.Log("Elegi aula TESIS");
                    break;
                case Level_Selected.Cafeteria:
                    Debug.Log("Elegi aula Cafeteria");
                    break;
            }
        }
        // ESTE SCRIPT DEBE TOMAR LA DEDICION CORRESPONDIENTE EN CUANTO AL NIVEL ELEJIDO Y PERSONAJE ELEJIDO DEPENDIENDO DE LA INFO QUE LE PASE EL
        // STRUCT GAME MANAGER
    }
}
