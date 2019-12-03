using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_PvP : MonoBehaviour
{
    public enum PlayerSelected
    {
        Nulo,
        Protagonista,
        Balanceado,
        Agresivo,
        Defensivo,
    }
    public PlayerSelected playerSelected;
    public enum State
    {
        Nulo,
        Defendido,
    }
    public enum Player
    {
        player1,
        player2,
    }
    public enum StateDeffence
    {
        Nulo,
        NormalDeffense,
        CounterAttackDeffense,
    }
    public StateDeffence stateDeffence;
    public Player playerActual;
    public State playerState;
    public float delayCounterAttackDeffense;
    public float auxDelayCounterAttackDeffense;
    // Start is called before the first frame update
    private void Start()
    {
        auxDelayCounterAttackDeffense = delayCounterAttackDeffense;
    }
    // Update is called once per frame
}
