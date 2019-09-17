using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Prototipo_2 {
    public class StructsEnemys : MonoBehaviour
    {
        public struct DataEnemy
        {
            public int CantCasillasOcupadas_X;
            public int CantCasillasOcupadas_Y;
            public int columnaActual;
        }
        public DataEnemy dataEnemy;
        public GameObject ruta;
        public GameObject rutaAgachado;
        public int countRoot;
        public int initialRoot;
    }
}
