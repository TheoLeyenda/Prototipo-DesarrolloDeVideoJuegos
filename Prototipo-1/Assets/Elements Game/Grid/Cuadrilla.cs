using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Prototipo_2
{
    public class Cuadrilla : MonoBehaviour
    {
        public enum StateCuadrilla
        {
            Ocupado,
            Libre,
            Count,
        }
        public Enemy enemy;
        public Player player;
        private StateCuadrilla stateCuadrilla;
        private void Start()
        {
            ResetCuadrilla();
        }

        public void SetStateCuadrilla(StateCuadrilla state)
        {
            stateCuadrilla = state;
        }
        public StateCuadrilla GetStateCuadrilla()
        {
            return stateCuadrilla;
        }
        public void ResetCuadrilla()
        {
            stateCuadrilla = StateCuadrilla.Libre;
        }

    }
}
