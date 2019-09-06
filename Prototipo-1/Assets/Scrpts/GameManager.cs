using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)

public class GameManager : MonoBehaviour
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
    [Header("PushEvent")]
    public Text textClashEvent;
    public List<ButtonEvent> buttonsEvents;
    public Image panelClash;
    private float textScaleX;
    private float textScaleY;
    private float auxTextScaleX;
    private float auxTextScaleY;
    public float maxTextScaleX;
    public float maxTextScaleY;
    public float speedOfSize;
    private int TypePushEvent;
    private int countTypePushEvent = 3;
    private int id_button = 0;
    private int cantButtonUse;
    private int ObjectivePushs;
    private float minCantButtonUse = 5;
    private float maxCantButtonUse;
    private Vector2 positionButton;
    public Text TextBotonesPrecionados;
    [Header("-----------")]
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
    public List<Enemy> enemiesActivate;
    [HideInInspector]
    public float auxTimeOFF;
    public bool SiglePlayer;
    public bool MultiPlayer;//(EN CASO DE TENER MULTYPLAYER EL JUEGO SE TRANFORMA EN UN JUEGO POR TURNOS)
    [HideInInspector]
    public int countEnemysDead;
    public int RondasPorJefe;
    //public Text TextTimeOfAttack;
    public Image TimeClockOfAttack; 
    public Text TextTitulo;
    public Text START;
    public Text TextTimeStart;
    public static GameManager instanceGameManager;
    public float timeSelectionAttack;
    public float timerNextRond;
    public float timerStart;
    public ModosDeJuego modoDeJuego;

    private bool initialGeneration;
    private FSM fsm;
    private Player.Movimiento movimientoJugador1;
    private Player.EstadoJugador estadoJugador1;
    private Enemy.EstadoEnemigo estadoEnemigo;
    private Enemy.Movimiento movimientoEnemigo;
    // HACER LO MISMO PERO PARA EL ENEMIGO 
    private EventoEspecial specialEvent;
    private EstadoResultado estadoResultado;
    private float auxTimeSelectionAttack;
    private float auxTimerNextRond;
    private float auxTimerStart;

    private Player player1;
    private Player player2;
    private Player player3;
    private Player player4;

    private int roundCombat;





    private void Awake()
    {
        for (int i = 0; i < buttonsEvents.Count; i++)
        {
            buttonsEvents[i].gameObject.SetActive(false);
        }
        maxCantButtonUse = buttonsEvents.Count;
        id_button = 0;
        auxTextScaleX = textClashEvent.rectTransform.rect.width;
        auxTextScaleY = textClashEvent.rectTransform.rect.height;
        textClashEvent.gameObject.SetActive(false);
        panelClash.gameObject.SetActive(false);
        specialEvent = EventoEspecial.Nulo;
        roundCombat = 1;
        initialGeneration = true;
        countEnemysDead = 0;
        generateEnemy = false;
        estadoResultado = EstadoResultado.Nulo;
        fsm = new FSM((int)GameState.Count, (int)GameEvents.Count, (int)GameState.Idle);
        fsm.SetRelations((int)GameState.Idle, (int)GameState.EnComienzo, (int)GameEvents.Comenzar);
        fsm.SetRelations((int)GameState.EnComienzo, (int)GameState.RespuestaJugadores, (int)GameEvents.JugadasElejidas);
        fsm.SetRelations((int)GameState.RespuestaJugadores, (int)GameState.Resultado, (int)GameEvents.TiempoFuera);
        fsm.SetRelations((int)GameState.Resultado, (int)GameState.EnComienzo, (int)GameEvents.Comenzar);
        fsm.SetRelations((int)GameState.Resultado, (int)GameState.Idle, (int)GameEvents.Quieto);
        fsm.SetRelations((int)GameState.RespuestaJugadores, (int)GameState.Idle, (int)GameEvents.Quieto);
        fsm.SetRelations((int)GameState.EnComienzo, (int)GameState.Idle, (int)GameEvents.Quieto);

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
        for (int i = 0; i < buttonsEvents.Count; i++)
        {
            buttonsEvents[i].gameObject.SetActive(false);
        }
        maxCantButtonUse = buttonsEvents.Count - 1;
        id_button = 0;
        auxTextScaleX = textClashEvent.rectTransform.rect.width;
        auxTextScaleY = textClashEvent.rectTransform.rect.height;
        textClashEvent.gameObject.SetActive(false);
        panelClash.gameObject.SetActive(false);
        specialEvent = EventoEspecial.Nulo;
        roundCombat = 1;
        canvasGameOver.SetActive(false);
        canvasLevel.SetActive(true);
        initialGeneration = true;
        countEnemysDead = 0;
        specialEvent = EventoEspecial.Nulo;
        auxTimerNextRond = timerNextRond;
        auxTimeSelectionAttack = timeSelectionAttack;
        auxTimerStart = timerStart;
        enemiesActivate = new List<Enemy>();

        DontDestroyOnLoad(gameObject);
    }


    // Update is called once per frame
    void Update()
    {
        CheckInGameOverScene();
        if (!InGameOverScene)
        {
            GeneradorEnemigos.SetActive(true);
            canvasLevel.SetActive(true);
            canvasGameOver.SetActive(false);
            //Debug.Log((GameState)fsm.GetCurrentState());
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
        //TextTitulo.gameObject.SetActive(false);
        TextTimeStart.gameObject.SetActive(true);
        //NO OCURRE NADA
        if (estadoResultado == EstadoResultado.Nulo)
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
                //generateEnemy = true;
                TextTitulo.gameObject.SetActive(true);
                TextTitulo.text = "ELIJAN MOVIMIENTO";
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
        specialEvent = EventoEspecial.Nulo;
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
                CheckTimeAttackCharacters();
            }
        }
        else if (!ActiveTime)
        {
            ImageClock.SetActive(false);
            if (timeSelectionAttack <= 0)
            {
                fsm.SendEvent((int)GameEvents.JugadasElejidas);
            }
        }
    }
    public void RespuestaJugadores()
    {
        TimeClockOfAttack.gameObject.SetActive(false);
        //TextTitulo.gameObject.SetActive(false);
        TextTimeStart.gameObject.SetActive(false);
        CheckCharcaters();
        if (specialEvent == EventoEspecial.Nulo)
        {
            fsm.SendEvent((int)GameEvents.TiempoFuera);
        }

    }
    public void Resultado()
    {
        TimeClockOfAttack.gameObject.SetActive(false);
        //TextTitulo.gameObject.SetActive(false);
        TextTimeStart.gameObject.SetActive(false);
        // POR AHORA SOLAMENTE VOLVEMOS AL COMIENZO

        CheckTimerNextRound();
    }

    public void ResetAll()
    {
        timerNextRond = auxTimerNextRond;
        timeSelectionAttack = auxTimeSelectionAttack;
        TextTitulo.text = "ELIJAN MOVIMIENTO";
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
            TimeClockOfAttack.fillAmount = timeSelectionAttack / auxTimeSelectionAttack;
        }
        else if (timeSelectionAttack <= 0)
        {
            fsm.SendEvent((int)GameEvents.JugadasElejidas);
        }


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
            fsm.SendEvent((int)GameEvents.Comenzar);
            roundCombat++;
            ResetAll();
        }
    }
    public void CheckCharcaters()
    {
        if (SiglePlayer)
        {
            
            enemiesActivate.Clear();
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
                if (specialEvent == EventoEspecial.Nulo)
                {
                    specialEvent = EventoEspecial.CartelClash;
                    TypePushEvent = Random.Range(0,countTypePushEvent);
                    maxCantButtonUse = buttonsEvents.Count - 1;
                    cantButtonUse = (int)Random.Range(minCantButtonUse, maxCantButtonUse);
                    cantButtonUse = cantButtonUse - 1;
                    ObjectivePushs = 0;
                    for (int i = 0; i < buttonsEvents.Count; i++)
                    {
                        buttonsEvents[i].disappear = false;
                        buttonsEvents[i].gameObject.SetActive(false);
                    }
                    buttonsEvents[id_button].gameObject.SetActive(true);
                    TextBotonesPrecionados.text = ""+ObjectivePushs+"/" + cantButtonUse;
                }
            }
            else if (movimientoEnemigo == Enemy.Movimiento.AtacarTorso && movimientoJugador1 == Player.Movimiento.AtacarTorso)
            {
                //EVENTO CUANDO EL ENEMIGO Y EL JUGADOR ATACAN AL MISMO OBJETIVO
                if (specialEvent == EventoEspecial.Nulo)
                {
                    specialEvent = EventoEspecial.CartelClash;
                    TypePushEvent = Random.Range(0, countTypePushEvent);
                    maxCantButtonUse = buttonsEvents.Count - 1;
                    cantButtonUse = (int)Random.Range(minCantButtonUse, maxCantButtonUse);
                    cantButtonUse = cantButtonUse - 1;
                    ObjectivePushs = 0;
                    for (int i = 0; i < buttonsEvents.Count; i++)
                    {
                        buttonsEvents[i].disappear = false;
                        buttonsEvents[i].gameObject.SetActive(false);
                    }
                    buttonsEvents[id_button].gameObject.SetActive(true);
                    TextBotonesPrecionados.text = "" + ObjectivePushs + "/" + cantButtonUse;
                }
            }
            else if (movimientoEnemigo == Enemy.Movimiento.AtacarPies && movimientoJugador1 == Player.Movimiento.AtacarPies)
            {
                //EVENTO CUANDO EL ENEMIGO Y EL JUGADOR ATACAN AL MISMO OBJETIVO
                if (specialEvent == EventoEspecial.Nulo)
                {
                    specialEvent = EventoEspecial.CartelClash;
                    TypePushEvent = Random.Range(0, countTypePushEvent);
                    maxCantButtonUse = buttonsEvents.Count - 1;
                    cantButtonUse = (int)Random.Range(minCantButtonUse, maxCantButtonUse);
                    cantButtonUse = cantButtonUse - 1;
                    ObjectivePushs = 0;
                    for (int i = 0; i < buttonsEvents.Count; i++)
                    {
                        buttonsEvents[i].disappear = false;
                        buttonsEvents[i].gameObject.SetActive(false);
                    }
                    buttonsEvents[id_button].gameObject.SetActive(true);
                    TextBotonesPrecionados.text = "" + ObjectivePushs + "/" + cantButtonUse;
                }
            }
            else if (movimientoEnemigo == Enemy.Movimiento.Saltar && movimientoJugador1 == Player.Movimiento.AtacarPies)
            {
                player1.Attack(Player.Objetivo.Piernas,false);
                for (int i = 0; i < enemiesActivate.Count; i++)
                {
                    if (enemiesActivate[i].typeEnemy == Enemy.TiposDeEnemigo.Balanceado)
                    {
                        enemiesActivate[i].Jump();
                        enemiesActivate[i].CounterAttack(false);
                        specialEvent = EventoEspecial.ContraAtaque;
                    }
                }

            }
            else if (movimientoEnemigo == Enemy.Movimiento.Agacharse && movimientoJugador1 == Player.Movimiento.AtacarCabeza)
            {
                player1.Attack(Player.Objetivo.Cabeza,false);
                for (int i = 0; i < enemiesActivate.Count; i++)
                {
                    if (enemiesActivate[i].typeEnemy == Enemy.TiposDeEnemigo.Balanceado)
                    {
                        enemiesActivate[i].Duck();
                        enemiesActivate[i].CounterAttack(false);
                        specialEvent = EventoEspecial.ContraAtaque;
                    }
                }

            }
            else if (movimientoJugador1 == Player.Movimiento.Agacharse && movimientoEnemigo == Enemy.Movimiento.AtacarCabeza)
            {
                for (int i = 0; i < enemiesActivate.Count; i++)
                {
                    enemiesActivate[i].Attack(Enemy.Objetivo.Cabeza,false);
                }
                player1.Duck();
                player1.CounterAttack(false);
                specialEvent = EventoEspecial.ContraAtaque;
            }
            else if (movimientoJugador1 == Player.Movimiento.Saltar && movimientoEnemigo == Enemy.Movimiento.AtacarPies)
            {
                for (int i = 0; i < enemiesActivate.Count; i++)
                {
                    enemiesActivate[i].Attack(Enemy.Objetivo.Piernas,false);
                }
                player1.Jump();
                player1.CounterAttack(false);
                specialEvent = EventoEspecial.ContraAtaque;
            }
            else if (specialEvent == EventoEspecial.Nulo)
            {
                switch (movimientoJugador1)
                {
                    case Player.Movimiento.AtacarCabeza:
                        player1.Attack(Player.Objetivo.Cabeza,false);
                        break;
                    case Player.Movimiento.AtacarTorso:
                        player1.Attack(Player.Objetivo.Torso,false);
                        break;
                    case Player.Movimiento.AtacarPies:
                        player1.Attack(Player.Objetivo.Piernas,false);
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
                                enemiesActivate[i].Attack(Enemy.Objetivo.Cabeza,false);
                                break;
                            case Enemy.Movimiento.AtacarTorso:
                                enemiesActivate[i].Attack(Enemy.Objetivo.Torso,false);
                                break;
                            case Enemy.Movimiento.AtacarPies:
                                enemiesActivate[i].Attack(Enemy.Objetivo.Piernas,false);
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
            switch (specialEvent)
            {
                case EventoEspecial.CartelClash:
                    textClashEvent.gameObject.SetActive(true);
                    ActivateCartelClash();
                    break;
                case EventoEspecial.PushButtonEvent:
                    EventPushButton(TypePushEvent);
                    break;
                case EventoEspecial.ContraAtaque:
                    specialEvent = EventoEspecial.Nulo;
                    break;
            }
        }
        if (MultiPlayer)
        {

        }
    }
    public void ActivateCartelClash()
    {
        if (textScaleX < maxTextScaleX && textScaleY < maxTextScaleY && specialEvent == EventoEspecial.CartelClash)
        {
            textScaleX = textScaleX + Time.deltaTime * speedOfSize;
            textScaleY = textScaleX;
            textClashEvent.rectTransform.sizeDelta = new Vector2(textScaleX, textScaleY);
            
        }
        else
        {
            textScaleX = auxTextScaleX;
            textScaleY = auxTextScaleY;
            textClashEvent.gameObject.SetActive(false);
            panelClash.gameObject.SetActive(true);
            specialEvent = EventoEspecial.PushButtonEvent;
        }
    }
    public void EventPushButton(int _typeEvent)
    {
        _typeEvent = 0;
        DisableUICharacters();
        switch (_typeEvent)
        {
            case 0:
                if (id_button < cantButtonUse && (id_button + 1) < cantButtonUse)
                {
                    if (buttonsEvents[id_button].disappear)
                    {

                        if (buttonsEvents[id_button].GetPressed())
                        {
                            buttonsEvents[id_button].gameObject.SetActive(false);
                            id_button++;
                            ObjectivePushs++;
                            TextBotonesPrecionados.text = "" + ObjectivePushs + "/" + cantButtonUse;
                        }
                        else
                        {
                            //Debug.Log("PERDI");
                            //PERDES
                            DefaultPushEvent();
                        }
                    }
                    else if (buttonsEvents[id_button + 1].GetPressed() && buttonsEvents[id_button + 1].gameObject.activeSelf && !buttonsEvents[id_button].GetPressed())
                    {
                        DefaultPushEvent();
                    }
                    //ESTO SIEMPRE DEBE ESTAR ABAJO
                    if (ObjectivePushs >= cantButtonUse)
                    {
                        //GANAS
                        VictoryPushEvent();
                    }
                    //------------------------------------------------//
                }
                else
                {
                    if (buttonsEvents[id_button].disappear)
                    {
                        if (buttonsEvents[id_button].GetPressed())
                        {
                            buttonsEvents[id_button].gameObject.SetActive(false);
                            id_button++;
                            ObjectivePushs++;
                            TextBotonesPrecionados.text = "" + ObjectivePushs + "/" + cantButtonUse;
                        }
                        else
                        {

                            //PERDES
                            DefaultPushEvent();
                        }
                    }
                    if (ObjectivePushs >= cantButtonUse)
                    {
                        //GANAS
                        VictoryPushEvent();
                    }
                }
                break;
            case 1:

                break;
            case 2:

                break;
        }
    }
    public void DefaultPushEvent()
    {
        id_button = 0;
        ObjectivePushs = 0;
        specialEvent = EventoEspecial.Nulo;
        for (int i = 0; i < buttonsEvents.Count; i++)
        {
            buttonsEvents[i].gameObject.SetActive(false);
        }
        panelClash.gameObject.SetActive(false);
        ActivateUICharacters();
        for (int i = 0; i < enemiesActivate.Count; i++)
        {
            enemiesActivate[i].CounterAttack(true);
        }
    }
    public void VictoryPushEvent()
    {
        id_button = 0;
        ObjectivePushs = 0;
        specialEvent = EventoEspecial.Nulo;
        for (int i = 0; i < buttonsEvents.Count; i++)
        {
            buttonsEvents[i].gameObject.SetActive(false);
        }
        panelClash.gameObject.SetActive(false);
        ActivateUICharacters();
        player1.EstadoMovimiento_ContraAtaque();
        player1.CounterAttack(true);
    }
    public void DisableUICharacters()
    {
        player1.BARRA_DE_VIDA.SetActive(false);
        player1.PanelMovement.SetActive(false);
        player1.PanelAttack.SetActive(false);
        player1.PanelDeffense.SetActive(false);
        player1.PanelDodge.SetActive(false);
        player1.PanelDeLogos.SetActive(false);
        for (int i = 0; i < enemiesActivate.Count; i++)
        {
            enemiesActivate[i].PanelDeLogos.SetActive(false);
            enemiesActivate[i].BARRA_DE_VIDA.SetActive(false);
        }
    }
    public void ActivateUICharacters()
    {
        player1.BARRA_DE_VIDA.SetActive(true);
        player1.PanelMovement.SetActive(true);
        player1.PanelDeLogos.SetActive(true);
        for (int i = 0; i < enemiesActivate.Count; i++)
        {
            enemiesActivate[i].PanelDeLogos.SetActive(true);
            enemiesActivate[i].BARRA_DE_VIDA.SetActive(true);
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
    public void ResetGameManager()
    {
        countEnemysDead = 0;
        initialGeneration = true;
        timerNextRond = auxTimerNextRond;
        timerStart = auxTimerStart;
        timeSelectionAttack = auxTimeSelectionAttack;
        //TextTimeOfAttack.text = " ";
        TimeClockOfAttack.fillAmount = 1;
        TextTimeStart.text = " ";
        
        fsm.SendEvent((int)GameEvents.Quieto);
    }
    public void InSurvivalMode()
    {
        modoDeJuego = ModosDeJuego.Supervivencia;
        countEnemysDead = 0;
    }
    public void InStoryMode()
    {
        modoDeJuego = ModosDeJuego.Historia;
        countEnemysDead = 0;
    }
    public ModosDeJuego GetGameMode()
    {
        return modoDeJuego;
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
}
    

//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)
