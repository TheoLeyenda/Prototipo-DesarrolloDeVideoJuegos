using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototipo_2
{
    public class EnumsGameManager : MonoBehaviour
    {

        public enum EventoEspecial
        {
            Nulo,
            CartelClash,
            PushButtonEvent,
            ContraAtaque,
            Count,
        }
        public enum ModosDeJuego
        {
            Nulo,
            Supervivencia,
            Historia,
            Count
        }
        [HideInInspector]
        public EventoEspecial specialEvent;
        public ModosDeJuego modoDeJuego;
    }
}
