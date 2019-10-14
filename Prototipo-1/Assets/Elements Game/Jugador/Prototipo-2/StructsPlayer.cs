using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototipo_2
{
    public class StructsPlayer : MonoBehaviour
    {
        // Start is called before the first frame update
        public struct DataPlayer
        {
            public int CantCasillasOcupadas_X;
            public int CantCasillasOcupadas_Y;
            public int CantCasillasOcupadasAgachado;
            public int CantCasillasOcupadasParado;
            public int columnaActual;
        }
        
        [System.Serializable]
        public struct DataSpecialAttack
        {
            public Pool poolProyectil;
            public Pool poolProyectilParabola;
            public Pool poolProyectilImparable;
            public GameObject DisparoDeCarga;
            public Pool poolGranadaGaseosa;
        }
        
        public DataSpecialAttack dataAttack;
        public GameObject ruta;
        public GameObject rutaAgachado;
        public DataPlayer dataPlayer;
    }
}
