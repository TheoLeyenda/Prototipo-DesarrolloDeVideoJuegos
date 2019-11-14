using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//CODIGO PARA VERSIONAR
namespace Prototipo_2 {
    public class OnFinishAnimation : MonoBehaviour
    {
        public DataCombatPvP dataCombatPvP;
        public void DisableObject()
        {
            gameObject.SetActive(false);
        }
        public void DisableMovementPlayers()
        {
            if (dataCombatPvP.player1 != null & dataCombatPvP.player2 != null)
            {
                dataCombatPvP.player1.GetInputManager().SetEnableMovementPlayer1(false);
                dataCombatPvP.player2.GetInputManager().SetEnableMovementPlayer2(false);
            }
        }
        public void EnableMovementPlayers()
        {
            if(dataCombatPvP.player1 != null & dataCombatPvP.player2 != null)
            {
                dataCombatPvP.player1.GetInputManager().SetEnableMovementPlayer1(true);
                dataCombatPvP.player2.GetInputManager().SetEnableMovementPlayer2(true);
            }
        }
    }
}
