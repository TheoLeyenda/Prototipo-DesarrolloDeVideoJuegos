using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototipo_2
{
    //ALMACENA TODOS LOS DATOS ELEGIDOS TANTOS POR EL SELECT LEVEL COMO POR EL SELEC PLAYER.
    public class StructGameManager : MonoBehaviour
    {
        public struct GM_DataCombatPvP
        {
            public DataCombatPvP.Level_Selected level_selected;
            public DataCombatPvP.Player_Selected player1_selected;
            public DataCombatPvP.Player_Selected player2_selected;
        }
        public GM_DataCombatPvP gm_dataCombatPvP;
    }
    //ALMACENA TODOS LOS DATOS ELEGIDOS TANTOS POR EL SELECT LEVEL COMO POR EL SELEC PLAYER.
}
