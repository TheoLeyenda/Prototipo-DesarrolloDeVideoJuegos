using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototipo_2
{
    public class SpriteEnemy : MonoBehaviour
    {
        // Start is called before the first frame update
        private void OnTriggerStay2D(Collider2D collision)
        {
            switch (collision.gameObject.tag)
            {
                case "Cuadrilla":
                    Cuadrilla cuadrilla = collision.GetComponent<Cuadrilla>();
                    cuadrilla.stateCuadrilla = Cuadrilla.StateCuadrilla.Ocupado;
                    //Debug.Log("ENTRE");
                    break;
            }
        }
    }
}
