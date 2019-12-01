using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototipo_2
{
    public class BotonManager : MonoBehaviour
    {
        public enum Direccion
        {
            Vertical,
            Horizontal,
            Nulo,
        }
        public Direccion direccionSelect;
        public Boton[] botones;
        public int posicion = 0;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void CheckInput()
        {
            if (direccionSelect == Direccion.Horizontal)
            {
                // EN CASO DE USAR ESTE SCRIPT EN OTRO PROYECTO TENEMOS DOS OPCIONES:
                // 1) Llevarme el script de InputManager y el de InputPlayerController y configurar el playerSetting -> input tal cual como en este proyecto
                // 2) cambiar los la condicion del if con el Input.GetKeyDown(KeyCode.BotonCorrespondiente) para que seleccione el boton.
                if (InputPlayerController.GetInputAxis("Horizontal") > 0)
                {
                    SumarPosicion();
                }
                // EN CASO DE USAR ESTE SCRIPT EN OTRO PROYECTO TENEMOS DOS OPCIONES:
                // 1) Llevarme el script de InputManager y el de InputPlayerController y configurar el playerSetting -> input tal cual como en este proyecto
                // 2) cambiar los la condicion del if con el Input.GetKeyDown(KeyCode.BotonCorrespondiente) para que seleccione el boton.
                if (InputPlayerController.GetInputAxis("Horizontal") < 0)
                {
                    RestarPosicion();
                }
            }
            else if (direccionSelect == Direccion.Vertical)
            {
                // EN CASO DE USAR ESTE SCRIPT EN OTRO PROYECTO TENEMOS DOS OPCIONES:
                // 1) Llevarme el script de InputManager y el de InputPlayerController y configurar el playerSetting -> input tal cual como en este proyecto
                // 2) cambiar los la condicion del if con el Input.GetKeyDown(KeyCode.BotonCorrespondiente) para que seleccione el boton.
                if (InputPlayerController.GetInputAxis("Vertical") > 0)
                {
                    SumarPosicion();
                }
                // EN CASO DE USAR ESTE SCRIPT EN OTRO PROYECTO TENEMOS DOS OPCIONES:
                // 1) Llevarme el script de InputManager y el de InputPlayerController y configurar el playerSetting -> input tal cual como en este proyecto
                // 2) cambiar los la condicion del if con el Input.GetKeyDown(KeyCode.BotonCorrespondiente) para que seleccione el boton.
                if (InputPlayerController.GetInputAxis("Vertical") < 0)
                {
                    RestarPosicion();
                }
            }
        }
        public void SumarPosicion()
        {
            botones[posicion].selected = false;
            posicion++;

            if (posicion < 0)
            {
                posicion = botones.Length - 1;
                botones[posicion].selected = true;
                return;
            }
            if (posicion > botones.Length - 1)
            {
                posicion = 0;
                botones[posicion].selected = true;
                return;
            }

            botones[posicion].selected = true;
        }
        public void RestarPosicion()
        {
            botones[posicion].selected = false;
            posicion--;

            if (posicion < 0)
            {
                posicion = botones.Length - 1;
                botones[posicion].selected = true;
                return;
            }
            if (posicion >= botones.Length - 1)
            {
                posicion = 0;
                botones[posicion].selected = true;
                return;
            }

            botones[posicion].selected = true;
        }
    }
}
