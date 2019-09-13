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
    public Movimiento movimiento;
    public EstadoJugador estadoJugador;
    //CAMBIAR ESTO Y PONER SETERS Y GETTERS
}
