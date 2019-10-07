using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumsPlayers : MonoBehaviour
{
    public enum Movimiento
    {
        Nulo,
        AtacarEnParabola,
        SaltoAtaque,
        AgacharseAtaque,
        AtacarEnElLugar,
        SaltoDefensa,
        AgacheDefensa,
        DefensaEnElLugar,
        Saltar,
        Agacharse,
        MoverAdelante,
        MoverAtras,
        Count,
    }
    public enum EstadoJugador
    {
        vivo,
        muerto,
        Count,
    }
    public enum NumberPlayer
    {
        player1,
        player2,
        Count
    }
    public Movimiento movimiento;
    public EstadoJugador estadoJugador;
    public NumberPlayer numberPlayer;
    //CAMBIAR ESTO Y PONER SETERS Y GETTERS
}
