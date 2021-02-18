using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnumsEnemy : MonoBehaviour
{
    public enum TiposDeEnemigo
    {
        Jefe,
        Balanceado,
        Agresivo,
        Defensivo,
        Famosa,
        Gotica,
        Tomboy, //El enemigo femenino comun
        Count
    }
    public enum TiposDeJefe
    {
        ProfeAnatomia,
        ProfeHistoria,
        ProfeEducacionFisica,
        ProfeArte,
        ProfeMatematica,
        ProfeQuimica,
        ProfeProgramacion,
        ProfeBaretto,
        ProfeLautarito,
        Nulo,
        Count
    }
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
        Count,
    }
    public enum EstadoEnemigo
    {
        vivo,
        Atrapado,
        Congelado,
        muerto,
        Count,
    }
    public TiposDeJefe typeBoss;
    public TiposDeEnemigo typeEnemy;
    [SerializeField]
    private Movimiento movement = Movimiento.MoveToPointCombat;
    private EstadoEnemigo stateEnemy;

    public void SetMovement(Movimiento mov)
    {
        movement = mov;
    }
    public void SetStateEnemy(EstadoEnemigo _stateEnemy)
    {
        stateEnemy = _stateEnemy;
    }
    public Movimiento GetMovement()
    {
        return movement;
    }
    public EstadoEnemigo GetStateEnemy()
    {
        return stateEnemy;
    }
}