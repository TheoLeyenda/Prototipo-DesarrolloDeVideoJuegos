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
            Defendido,
            Libre,
            Count,
        }
        public enum PosicionCuadrilla
        {
            CuadrillaAltaIzquierda,
            CuadrillaAltaCentral,
            CuadrillaAltaDerecha,
            CuadrillaMediaIzquierda,
            CuadrillaMediaCentral,
            CuadrillaMediaDerecha,
            CuadrillaBajaIzquierda,
            CuadrillaBajaCentral,
            CuadrillaBajaDerecha,
            Count,
        }
        
        public Enemy enemy;
        public Player player;
        public StateCuadrilla stateCuadrilla;
        public PosicionCuadrilla posicionCuadrilla;

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
