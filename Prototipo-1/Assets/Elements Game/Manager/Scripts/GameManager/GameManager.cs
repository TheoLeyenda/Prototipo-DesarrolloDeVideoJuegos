using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)

public class GameManager : MonoBehaviour
{
    public EnumsGameManager enumsGameManager;
    public GameManagerCharactersController gameManagerCharacterController;
    [HideInInspector]
    public PushEventManager pushEventManager;
    public bool ActiveTime;
    public GameObject ImageClock;
    public GameObject canvasGameOver;
    public GameObject canvasLevel;
    public GameObject GeneradorEnemigos;
    [HideInInspector]
    public bool InGameOverScene;
    public GeneradorDeEnemigos EnemyGenerator;
    [HideInInspector]
    public bool generateEnemy;
    [HideInInspector]
    public float auxTimeOFF;
    public bool SiglePlayer;
    public bool MultiPlayer;//(EN CASO DE TENER MULTYPLAYER EL JUEGO SE TRANFORMA EN UN JUEGO POR TURNOS)
    [HideInInspector]
    public int countEnemysDead;
    public int RondasPorJefe;
    public Image TimeClockOfAttack;
    public Text TextTitulo;
    public Text START;
    public Text TextTimeStart;
    public static GameManager instanceGameManager;
    public float timeSelectionAttack;
    public float timerNextRond;
    public float timerStart;
    private bool initialGeneration;
    private FSM fsm;

    [HideInInspector]
    public float auxTimeSelectionAttack;
    private float auxTimerNextRond;
    private float auxTimerStart;

    private int roundCombat;

    private void Awake()
    {
        
        enumsGameManager.specialEvent = EnumsGameManager.EventoEspecial.Nulo;
        roundCombat = 1;
        initialGeneration = true;
        countEnemysDead = 0;
        generateEnemy = false;
        enumsGameManager.estadoResultado = EnumsGameManager.EstadoResultado.Nulo;
        fsm = new FSM((int)EnumsGameManager.GameState.Count, (int)EnumsGameManager.GameEvents.Count, (int)EnumsGameManager.GameState.Idle);
        fsm.SetRelations((int)EnumsGameManager.GameState.Idle, (int)EnumsGameManager.GameState.EnComienzo, (int)EnumsGameManager.GameEvents.Comenzar);
        fsm.SetRelations((int)EnumsGameManager.GameState.EnComienzo, (int)EnumsGameManager.GameState.RespuestaJugadores, (int)EnumsGameManager.GameEvents.JugadasElejidas);
        fsm.SetRelations((int)EnumsGameManager.GameState.RespuestaJugadores, (int)EnumsGameManager.GameState.Resultado, (int)EnumsGameManager.GameEvents.TiempoFuera);
        fsm.SetRelations((int)EnumsGameManager.GameState.Resultado, (int)EnumsGameManager.GameState.EnComienzo, (int)EnumsGameManager.GameEvents.Comenzar);
        fsm.SetRelations((int)EnumsGameManager.GameState.Resultado, (int)EnumsGameManager.GameState.Idle, (int)EnumsGameManager.GameEvents.Quieto);
        fsm.SetRelations((int)EnumsGameManager.GameState.RespuestaJugadores, (int)EnumsGameManager.GameState.Idle, (int)EnumsGameManager.GameEvents.Quieto);
        fsm.SetRelations((int)EnumsGameManager.GameState.EnComienzo, (int)EnumsGameManager.GameState.Idle, (int)EnumsGameManager.GameEvents.Quieto);

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
        enumsGameManager.specialEvent = EnumsGameManager.EventoEspecial.Nulo;
        roundCombat = 1;
        canvasGameOver.SetActive(false);
        canvasLevel.SetActive(true);
        initialGeneration = true;
        countEnemysDead = 0;
        auxTimerNextRond = timerNextRond;
        auxTimeSelectionAttack = timeSelectionAttack;
        auxTimerStart = timerStart;
        DontDestroyOnLoad(gameObject);
    }
    void Update()
    {
        CheckInGameOverScene();
        if (!InGameOverScene)
        {
            GeneradorEnemigos.SetActive(true);
            canvasLevel.SetActive(true);
            canvasGameOver.SetActive(false);
            switch (fsm.GetCurrentState())
            {
                case (int)EnumsGameManager.GameState.Idle:
                    Idle();
                    break;
                case (int)EnumsGameManager.GameState.EnComienzo:
                    EnComienzo();
                    break;
                case (int)EnumsGameManager.GameState.RespuestaJugadores:
                    RespuestaJugadores();
                    break;
                case (int)EnumsGameManager.GameState.Resultado:
                    Resultado();
                    break;
            }
        }
        else
        {
            GeneradorEnemigos.SetActive(false);
            canvasLevel.SetActive(false);
            canvasGameOver.SetActive(true);
            //ACA DEBERIA MOSTRAR PUNTAJE Y ESTADISTICAS DEL JUGADOR
        }
        
    }
    public void Idle()
    {
        TextTitulo.text = "RONDA 1";
        TextTitulo.gameObject.SetActive(false);
        TimeClockOfAttack.gameObject.SetActive(false);
        TextTimeStart.gameObject.SetActive(true);
        if (enumsGameManager.estadoResultado == EnumsGameManager.EstadoResultado.Nulo)
        {
            if (SceneManager.GetActiveScene().name == "Supervivencia")
            {
                if (initialGeneration)
                {
                    initialGeneration = false;
                    generateEnemy = false;
                    EnemyGenerator.GenerateEnemy();
                }
            }
            if (timerStart > 0)
            {
                TextTimeStart.gameObject.SetActive(true);
                START.gameObject.SetActive(true);
                timerStart = timerStart - Time.deltaTime;
                TextTimeStart.text = "" + Mathf.Ceil(timerStart);
            }
            else if (timerStart <= 0)
            {
                TextTitulo.gameObject.SetActive(true);
                TextTitulo.text = "ELIJAN MOVIMIENTO";
                timerStart = auxTimerStart;
                fsm.SendEvent((int)EnumsGameManager.GameEvents.Comenzar);
                TextTimeStart.gameObject.SetActive(false);
                START.gameObject.SetActive(false);
            }

        }
        else if (enumsGameManager.estadoResultado == EnumsGameManager.EstadoResultado.GanasteNivel)
        {
            //HAGO UNA LISTA DE NIVELES Y PASO AL SIGUIENTE NIVEL EN LA LISTA
            //RESETEO EL GAME MANAGER
            enumsGameManager.estadoResultado = EnumsGameManager.EstadoResultado.Nulo;
            fsm.SendEvent((int)EnumsGameManager.GameEvents.Quieto);
        }

    }
    public void EnComienzo()
    {
        enumsGameManager.specialEvent = EnumsGameManager.EventoEspecial.Nulo;
        TextTimeStart.gameObject.SetActive(false);
        TimeClockOfAttack.gameObject.SetActive(true);
        TextTitulo.gameObject.SetActive(true);
        if (generateEnemy)
        {
            generateEnemy = false;
            EnemyGenerator.GenerateEnemy();
        }
        if (ActiveTime)
        {
            ImageClock.SetActive(true);
            if (TimeClockOfAttack != null)
            {
                gameManagerCharacterController.CheckTimeAttackCharacters();
            }
        }
        else if (!ActiveTime)
        {
            ImageClock.SetActive(false);
            if (timeSelectionAttack <= 0)
            {
                fsm.SendEvent((int)EnumsGameManager.GameEvents.JugadasElejidas);
            }
        }
    }
    public void RespuestaJugadores()
    {
        gameManagerCharacterController.AnswerPlayers();
    }
    public void Resultado()
    {
        TimeClockOfAttack.gameObject.SetActive(false);
        TextTimeStart.gameObject.SetActive(false);
        CheckTimerNextRound();
    }
    public void ResetAll()
    {
        timerNextRond = auxTimerNextRond;
        timeSelectionAttack = auxTimeSelectionAttack;
        TextTitulo.text = "ELIJAN MOVIMIENTO";
        gameManagerCharacterController.ResetCharacters();
    }
    private void CheckTimerNextRound()
    {
        if (timerNextRond > 0)
        {
            timerNextRond = timerNextRond - Time.deltaTime;
            if (roundCombat > 0)
            {
                TextTitulo.text = "RONDA " + roundCombat;
            }
            else
            {
                TextTitulo.text = " ";
            }
        }
        if (timerNextRond <= 0)
        {
            fsm.SendEvent((int)EnumsGameManager.GameEvents.Comenzar);
            roundCombat++;
            ResetAll();
        }
    }
    public void CheckInGameOverScene()
    {
        if (SceneManager.GetActiveScene().name == "GameOver" || SceneManager.GetActiveScene().name == "MENU")
        {
            InGameOverScene = true;
        }
        else
        {
            InGameOverScene = false;
        }
    }
    public void GameOver()
    {
        
        SceneManager.LoadScene("GameOver");

    }
    public void SetRespuestaJugador1(Player.Movimiento movimiento)
    {
        gameManagerCharacterController.movimientoJugador1 = movimiento;
    }
    public void SetEstadoJugador1(Player.EstadoJugador estado)
    {
        gameManagerCharacterController.estadoJugador1 = estado;
    }
    public void SetEstadoEnemigo(Enemy.EstadoEnemigo estado)
    {
        gameManagerCharacterController.estadoEnemigo = estado;
    }
    public void SetMovimientoEnemigo(Enemy.Movimiento movimiento)
    {
        gameManagerCharacterController.movimientoEnemigo = movimiento;
    }
    public EnumsGameManager.GameState GetGameState()
    {
        return (EnumsGameManager.GameState)fsm.GetCurrentState();
    }
    public void ResetGameManager()
    {
        countEnemysDead = 0;
        initialGeneration = true;
        timerNextRond = auxTimerNextRond;
        timerStart = auxTimerStart;
        timeSelectionAttack = auxTimeSelectionAttack;
        TimeClockOfAttack.fillAmount = 1;
        TextTimeStart.text = " ";
        fsm.SendEvent((int)EnumsGameManager.GameEvents.Quieto);
    }
    public void InSurvivalMode()
    {
        enumsGameManager.modoDeJuego = EnumsGameManager.ModosDeJuego.Supervivencia;
        countEnemysDead = 0;
    }
    public void InStoryMode()
    {
        enumsGameManager.modoDeJuego = EnumsGameManager.ModosDeJuego.Historia;
        countEnemysDead = 0;
    }
    public EnumsGameManager.ModosDeJuego GetGameMode()
    {
        return enumsGameManager.modoDeJuego;
    }
    public void ResetRoundCombat(bool PlayerDeath)
    {
        if (!PlayerDeath)
        {
            roundCombat = 0;
        }
        else if (PlayerDeath)
        {
            roundCombat = 1;
        }
    }
    public FSM GetFSM()
    {
        return fsm;
    }
}