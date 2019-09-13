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
            public Cuadrilla cuadrillaActual;
        }
        public DataEnemy dataEnemy;
    }
}
