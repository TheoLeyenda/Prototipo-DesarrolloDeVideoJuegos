using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)

public class GameManager : MonoBehaviour
{
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
    private FSM fsm;
    private Player.Movimiento movimientoJugador1;
    private Player.EstadoJugador estadoJugador1;

    private Enemy.EstadoEnemigo estadoEnemigo;
    private Enemy.Movimiento movimientoEnemigo;
    // HACER LO MISMO PERO PARA EL ENEMIGO 

    private bool EventoEspecial;
    private EstadoResultado estadoResultado; 
    public Text TextTimeOfAttack;
    public Text TextTitulo;
    public Text START;
    public Text TextTimeStart;
    public static GameManager instanceGameManager;
    public float timeSelectionAttack;
    public float timerNextRond;
    public float timerStart;

    private float auxTimeSelectionAttack;
    private float auxTimerNextRond;
    private float auxTimerStart;
    public List<Enemy> enemiesActivate;
    private Player player1;
    private Player player2;
    private Player player3;
    private Player player4;
    [HideInInspector]
    public float auxTimeOFF;
    public bool SiglePlayer;
    public bool MultiPlayer;//(EN CASO DE TENER MULTYPLAYER EL JUEGO SE TRANFORMA EN UN JUEGO POR TURNOS)



    private void Awake()
    {
        estadoResultado = EstadoResultado.Nulo;
        fsm = new FSM((int)GameState.Count,(int)GameEvents.Count,(int)GameState.Idle);
        fsm.SetRelations((int)GameState.Idle, (int)GameState.EnComienzo, (int)GameEvents.Comenzar);
        fsm.SetRelations((int)GameState.EnComienzo, (int)GameState.RespuestaJugadores, (int)GameEvents.JugadasElejidas);
        fsm.SetRelations((int)GameState.RespuestaJugadores, (int)GameState.Resultado, (int)GameEvents.TiempoFuera);
        fsm.SetRelations((int)GameState.Resultado, (int)GameState.EnComienzo, (int)GameEvents.Comenzar);
        fsm.SetRelations((int)GameState.Resultado, (int)GameState.Idle, (int)GameEvents.Quieto);

        if (instanceGameManager == null)
        {
            instanceGameManager = this;
        }
        else if (instanceGameManager != null)
        {
            gameObject.SetActive(false);
        }

    }
    private void Start()
    {
        EventoEspecial = false;
        auxTimerNextRond = timerNextRond;
        auxTimeSelectionAttack = timeSelectionAttack;
        auxTimerStart = timerStart;
        enemiesActivate = new List<Enemy>();
        
        /*for (int i = 0; i < SceneManager.GetActiveScene().GetRootGameObjects().Length; i++) {
            if (SceneManager.GetActiveScene().GetRootGameObjects()[i].tag == "Enemy")
            {
                if (SceneManager.GetActiveScene().GetRootGameObjects()[i].GetComponent<Enemy>().gameObject.activeSelf)
                {
                    enemiesActivate.Add(SceneManager.GetActiveScene().GetRootGameObjects()[i].GetComponent<Enemy>());
                }
            }
            if (SceneManager.GetActiveScene().GetRootGameObjects()[i].tag == "Player")
            {
                if (SceneManager.GetActiveScene().GetRootGameObjects()[i].GetComponent<Player>() != null)
                {
                    player1 = SceneManager.GetActiveScene().GetRootGameObjects()[i].GetComponent<Player>();
                }
            }
            if (SceneManager.GetActiveScene().GetRootGameObjects()[i].tag == "Player2")
            {
                if (SceneManager.GetActiveScene().GetRootGameObjects()[i].GetComponent<Player>() != null)
                {
                    player2 = SceneManager.GetActiveScene().GetRootGameObjects()[i].GetComponent<Player>();
                }

            }
            if (SceneManager.GetActiveScene().GetRootGameObjects()[i].tag == "Player3")
            {
                if (SceneManager.GetActiveScene().GetRootGameObjects()[i].GetComponent<Player>() != null)
                {
                    player3 = SceneManager.GetActiveScene().GetRootGameObjects()[i].GetComponent<Player>();
                }
            }
            if (SceneManager.GetActiveScene().GetRootGameObjects()[i].tag == "Player4")
            {
                if (SceneManager.GetActiveScene().GetRootGameObjects()[i].GetComponent<Player>() != null)
                {
                    player4 = SceneManager.GetActiveScene().GetRootGameObjects()[i].GetComponent<Player>();
                }
            }
        }*/
        
        DontDestroyOnLoad(gameObject);
    }


    // Update is called once per frame
    void Update()
    {
        switch (fsm.GetCurrentState())
        {
            case (int)GameState.Idle:
                Idle();
                break;
            case (int)GameState.EnComienzo:
                EnComienzo();
                break;
            case (int)GameState.RespuestaJugadores:
                RespuestaJugadores();
                break;
            case (int)GameState.Resultado:
                Resultado();
                break;
        }

    }
    public void Idle()
    {
        //NO OCURRE NADA
        if (estadoResultado == EstadoResultado.Nulo)
        {
            if (timerStart > 0)
            {

                TextTimeStart.gameObject.SetActive(true);
                START.gameObject.SetActive(true);
                timerStart = timerStart - Time.deltaTime;
                TextTimeStart.text = "" + Mathf.Ceil(timerStart);
            }
            else if (timerStart <= 0)
            {
                timerStart = auxTimerStart;
                fsm.SendEvent((int)GameEvents.Comenzar);
                TextTimeStart.gameObject.SetActive(false);
                START.gameObject.SetActive(false);
            }

        }
        else if (estadoResultado == EstadoResultado.GanasteNivel)
        {
            //HAGO UNA LISTA DE NIVELES Y PASO AL SIGUIENTE NIVEL EN LA LISTA
            //RESETEO EL GAME MANAGER
            estadoResultado = EstadoResultado.Nulo;
            fsm.SendEvent((int)GameEvents.Quieto);
            
        }

    }
    public void EnComienzo()
    {
        if (TextTimeOfAttack != null)
        {
            CheckTimeAttackCharacters();
        }
    }
    public void RespuestaJugadores()
    {
        CheckCharcaters();
        fsm.SendEvent((int)GameEvents.TiempoFuera);

    }
    public void Resultado()
    {
        // POR AHORA SOLAMENTE VOLVEMOS AL COMIENZO
        
        CheckTimerNextRound();
    }
    
    public void ResetAll()
    {
        timerNextRond = auxTimerNextRond;
        timeSelectionAttack = auxTimeSelectionAttack;
        TextTitulo.text = "TIEMPO RESTANTE";
        if (player1 != null)
        {
            player1.RestartPlayer();
        }
        if (player2 != null)
        {
            player2.RestartPlayer();
        }
        if (player3 != null)
        {
            player3.RestartPlayer();
        }
        if (player4 != null)
        {
            player4.RestartPlayer();
        }
        for (int i = 0; i < enemiesActivate.Count; i++)
        {
            if (enemiesActivate[i] != null)
            {
                enemiesActivate[i].ResetEnemy();
            }
        }
    }
    private void CheckTimeAttackCharacters() {
        if (timeSelectionAttack > 0)
        {
            timeSelectionAttack = timeSelectionAttack - Time.deltaTime;
            TextTimeOfAttack.text = "" + ((int)timeSelectionAttack-1);
            if (((int)timeSelectionAttack - 1) < 0)
            {
                TextTimeOfAttack.text = "0";
            }
        }
        else if(timeSelectionAttack <= 0)
        {
            fsm.SendEvent((int)GameEvents.JugadasElejidas);
        }
        
        
    }
    private void CheckTimerNextRound()
    {
        if (timerNextRond > 0 )
        {
            timerNextRond = timerNextRond - Time.deltaTime;
            TextTitulo.text = "LA SIGUIENTE RONDA COMIENZA EN";
            TextTimeOfAttack.text = "" + (int)timerNextRond;

        }
        if (timerNextRond <= 0)
        {
            fsm.SendEvent((int)GameEvents.Comenzar);
            ResetAll();
        }
    }
    public void CheckCharcaters()
    {
        if (SiglePlayer)
        {
            //Debug.Log("ENTRE");
            enemiesActivate.Clear();
            //Debug.Log("Estado:" + estadoJugador1);
            for (int i = 0; i < SceneManager.GetActiveScene().GetRootGameObjects().Length; i++)
            {
                if (SceneManager.GetActiveScene().GetRootGameObjects()[i].tag == "Enemy")
                {
                    if (SceneManager.GetActiveScene().GetRootGameObjects()[i].activeSelf)
                    {
                        enemiesActivate.Add(SceneManager.GetActiveScene().GetRootGameObjects()[i].GetComponent<Enemy>());
                    }
                }
                if (SceneManager.GetActiveScene().GetRootGameObjects()[i].tag == "Player")
                {
                    if (SceneManager.GetActiveScene().GetRootGameObjects()[i].activeSelf)
                    {
                        player1 = SceneManager.GetActiveScene().GetRootGameObjects()[i].GetComponent<Player>();
                    }
                }
            }
            if (movimientoEnemigo == Enemy.Movimiento.AtacarCabeza && movimientoJugador1 == Player.Movimiento.AtacarCabeza)
            {
                //EVENTO CUANDO EL ENEMIGO Y EL JUGADOR ATACAN AL MISMO OBJETIVO
                EventPushButton();
                EventoEspecial = true;
            }
            else if (movimientoEnemigo == Enemy.Movimiento.AtacarTorso && movimientoJugador1 == Player.Movimiento.AtacarTorso)
            {
                //EVENTO CUANDO EL ENEMIGO Y EL JUGADOR ATACAN AL MISMO OBJETIVO
                EventPushButton();
                EventoEspecial = true;
            }
            else if (movimientoEnemigo == Enemy.Movimiento.AtacarPies && movimientoJugador1 == Player.Movimiento.AtacarPies)
            {
                //EVENTO CUANDO EL ENEMIGO Y EL JUGADOR ATACAN AL MISMO OBJETIVO
                EventPushButton();
                EventoEspecial = true;
            }
            else if (movimientoEnemigo == Enemy.Movimiento.Saltar && movimientoJugador1 == Player.Movimiento.AtacarPies)
            {
                player1.Attack(Player.Objetivo.Piernas);
                for (int i = 0; i < enemiesActivate.Count; i++)
                {
                    if (enemiesActivate[i].typeEnemy == Enemy.Categoria.Balanceado)
                    {
                        enemiesActivate[i].Jump();
                        enemiesActivate[i].CounterAttack();
                        EventoEspecial = true;
                    }
                }
                
            }
            else if (movimientoEnemigo == Enemy.Movimiento.Agacharse && movimientoJugador1 == Player.Movimiento.AtacarCabeza)
            {
                player1.Attack(Player.Objetivo.Cabeza);
                for (int i = 0; i < enemiesActivate.Count; i++)
                {
                    if (enemiesActivate[i].typeEnemy == Enemy.Categoria.Balanceado)
                    {
                        enemiesActivate[i].Duck();
                        enemiesActivate[i].CounterAttack();
                        EventoEspecial = true;
                    }
                }
                
            }
            else if (movimientoJugador1 == Player.Movimiento.Agacharse && movimientoEnemigo == Enemy.Movimiento.AtacarCabeza)
            {
                for (int i = 0; i < enemiesActivate.Count; i++)
                {
                    enemiesActivate[i].Attack(Enemy.Objetivo.Cabeza);
                }
                player1.Duck();
                player1.CounterAttack();
                EventoEspecial = true;
            }
            else if (movimientoJugador1 == Player.Movimiento.Saltar && movimientoEnemigo == Enemy.Movimiento.AtacarPies)
            {
                for (int i = 0; i < enemiesActivate.Count; i++)
                {
                    enemiesActivate[i].Attack(Enemy.Objetivo.Piernas);
                }
                player1.Jump();
                player1.CounterAttack();
                EventoEspecial = true;
            }
            else if (!EventoEspecial)
            {
                switch (movimientoJugador1)
                {
                    case Player.Movimiento.AtacarCabeza:
                        player1.Attack(Player.Objetivo.Cabeza);
                        break;
                    case Player.Movimiento.AtacarTorso:
                        player1.Attack(Player.Objetivo.Torso);
                        break;
                    case Player.Movimiento.AtacarPies:
                        player1.Attack(Player.Objetivo.Piernas);
                        break;
                    case Player.Movimiento.DefenderCabeza:
                        player1.Deffense(Player.Objetivo.Cabeza);
                        break;
                    case Player.Movimiento.DefenderTorsoPies:
                        player1.Deffense(Player.Objetivo.Cuerpo);
                        break;
                    case Player.Movimiento.Saltar:
                        player1.Jump();
                        break;
                    case Player.Movimiento.Agacharse:
                        player1.Duck();
                        break;
                }
                for (int i = 0; i < enemiesActivate.Count; i++)
                {
                    if (enemiesActivate != null)
                    {
                        switch (movimientoEnemigo)
                        {
                            case Enemy.Movimiento.AtacarCabeza:
                                enemiesActivate[i].Attack(Enemy.Objetivo.Cabeza);
                                break;
                            case Enemy.Movimiento.AtacarTorso:
                                enemiesActivate[i].Attack(Enemy.Objetivo.Torso);
                                break;
                            case Enemy.Movimiento.AtacarPies:
                                enemiesActivate[i].Attack(Enemy.Objetivo.Piernas);
                                break;
                            case Enemy.Movimiento.DefenderCabeza:
                                enemiesActivate[i].Deffense(Enemy.Objetivo.Cabeza);
                                break;
                            case Enemy.Movimiento.DefenderTorso:
                                enemiesActivate[i].Deffense(Enemy.Objetivo.Torso);
                                break;
                            case Enemy.Movimiento.DefenderPies:
                                enemiesActivate[i].Deffense(Enemy.Objetivo.Piernas);
                                break;
                            case Enemy.Movimiento.DefenderTorsoPies:
                                enemiesActivate[i].Deffense(Enemy.Objetivo.Cuerpo);
                                break;
                            case Enemy.Movimiento.Saltar:
                                enemiesActivate[i].Jump();
                                break;
                            case Enemy.Movimiento.Agacharse:
                                enemiesActivate[i].Duck();
                                break;
                        }
                    }
                }
            }
            
            //EL SWITCH DEL ENEMIGO
            
        }
        if (MultiPlayer)
        {
             
        }
        EventoEspecial = false;
    }
    public void EventPushButton()
    {
        Debug.Log("Event Push Button");
    }
    public void SetRespuestaJugador1(Player.Movimiento movimiento)
    {
        movimientoJugador1 = movimiento;
    }
    public void SetEstadoJugador1(Player.EstadoJugador estado)
    {
        estadoJugador1 = estado;
    }
    public void SetEstadoEnemigo(Enemy.EstadoEnemigo estado)
    {
        estadoEnemigo = estado;
    }
    public void SetMovimientoEnemigo(Enemy.Movimiento movimiento)
    {
        movimientoEnemigo = movimiento;
    }
    public GameState GetGameState()
    {
        return (GameState)fsm.GetCurrentState();
    }

}
    

//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)
