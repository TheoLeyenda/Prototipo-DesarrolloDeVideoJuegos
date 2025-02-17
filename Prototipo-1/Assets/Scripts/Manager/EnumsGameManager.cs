﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
        //PvP,
        Count
    }
    [HideInInspector]
    public EventoEspecial specialEvent;
    public ModosDeJuego modoDeJuego;
}
