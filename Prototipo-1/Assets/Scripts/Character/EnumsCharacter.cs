using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumsCharacter : MonoBehaviour
{
    public enum Movimiento
    {
        Nulo,
        AtacarEnElLugar,
        AgacharseAtaque,
        SaltoAtaque,
        MoverAtras,
        MoverAdelante,
        Saltar,
        DefensaEnElLugar,
        SaltoDefensa,
        AgacheDefensa,
        Agacharse,
        AtaqueEspecial,
        AtaqueEspecialAgachado,
        AtaqueEspecialSalto,
        MoveToPointCombat,
        MoveToPointDeath,
        AtacarEnParabola,
        Count,
    }
    public enum EstadoCharacter
    {
        vivo,
        Atrapado,
        muerto,
        Count,
    }
    public Movimiento movimiento;
    public EstadoCharacter estado;
}
