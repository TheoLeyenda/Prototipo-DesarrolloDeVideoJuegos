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
            Balanceado,
            Agresivo,
            Defensivo,
            Protagonista,
            Count,
            Nulo,
        }
        public enum Level_Selected
        {
            Anatomia,
            Historia,
            EducacionFisica,
            Arte,
            Matematica,
            Quimica,
            Programacion,
            TESIS,
            Cafeteria,
            Count,
            Nulo,
        }
        //LAS LISTAS DE Player1 y Player2 DEBEN SER INICIALIZADAs EN EL MISMO ORDEN QUE EL ENUMERADOR Player_Selected.
        public List<GameObject> Players1;
        public List<GameObject> Players2;

        //LA LISTA levels DEBE SER INICIALIZADA EN EL MISMO ORDEN QUE EL ENUMERADOR Level_Selected.
        public List<Sprite> levels;

        public SpriteRenderer fondoNivel;
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
            Instantiate(Players1[(int)player1_selected]);
            Instantiate(Players2[(int)player2_selected]);
            fondoNivel.sprite = levels[(int)level_selected];
        }
        // ESTE SCRIPT DEBE TOMAR LA DEDICION CORRESPONDIENTE EN CUANTO AL NIVEL ELEJIDO Y PERSONAJE ELEJIDO DEPENDIENDO DE LA INFO QUE LE PASE EL
        // STRUCT GAME MANAGER
    }
}
