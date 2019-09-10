using System.Collections;
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
        Count
    }
    public enum GameState
    {
        Idle,
        EnComienzo,
        RespuestaJugadores,
        Resultado,
        Count
    }
    public enum GameEvents
    {
        Quieto,
        Comenzar,
        JugadasElejidas,
        TiempoFuera,
        Count
    }
    public enum EstadoResultado
    {
        Nulo,
        GanastePelea,
        GanasteNivel,
        Perdiste,
        Count,
    }
    [HideInInspector]
    public EventoEspecial specialEvent;
    public ModosDeJuego modoDeJuego;
    [HideInInspector]
    public EstadoResultado estadoResultado;
}
