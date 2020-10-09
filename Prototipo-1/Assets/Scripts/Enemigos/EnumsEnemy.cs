using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnumsEnemy : EnumsCharacter
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
   
    public TiposDeJefe typeBoss;
    public TiposDeEnemigo typeEnemy;
}