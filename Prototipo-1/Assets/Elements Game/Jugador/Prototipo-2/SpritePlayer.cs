using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototipo_2 {
    public class SpritePlayer : MonoBehaviour
    {
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
        private void OnTriggerExit2D(Collider2D collision)
        {
            switch (collision.gameObject.tag)
            {
                case "Cuadrilla":
                    Cuadrilla cuadrilla = collision.GetComponent<Cuadrilla>();
                    cuadrilla.stateCuadrilla = Cuadrilla.StateCuadrilla.Libre;
                    //Debug.Log("SALI");
                    break;
            }
        }
    }
}
