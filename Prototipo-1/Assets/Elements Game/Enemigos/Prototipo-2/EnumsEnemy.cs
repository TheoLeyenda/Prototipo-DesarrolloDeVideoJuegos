using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototipo_2
{
    public class EnumsEnemy : MonoBehaviour
    {
        public enum TiposDeEnemigo
        {
            Jefe,
            Balanceado,
            Agresivo,
            Defensivo,
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
            AtacarEnParabola,
            Count,
        }
        public enum EstadoEnemigo
        {
            vivo,
            muerto,
            Count,
        }
        public TiposDeJefe typeBoss;
        public TiposDeEnemigo typeEnemy;
        private Movimiento movement;
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
}