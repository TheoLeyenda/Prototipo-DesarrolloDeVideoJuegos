using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataCombatPvP : MonoBehaviour
{
    // ESTE SCRIPT DEBE TOMAR LA DEDICION CORRESPONDIENTE EN CUANTO AL NIVEL ELEJIDO Y PERSONAJE ELEJIDO DEPENDIENDO DE LA INFO QUE LE PASE EL
    // STRUCT GAME MANAGER
    public enum ModoDeJuego
    {
        PvP,
        TiroAlBlanco,
        DueloPorPuntos,
    }
    public enum Player_Selected
    {
        Balanceado,
        Agresivo,
        Defensivo,
        Protagonista,
        Famosa,
        Tomboy,
        Gotica,
        Count,
        Nulo,
    }
    public enum Level_Selected
    {
        Anatomia,
        Historia,
        EducacionFisica,
        Arte,
        Matematica,
        Quimica,
        Programacion,
        TESIS,
        Cafeteria,
        Count,
        Nulo,
    }
    //LAS LISTAS DE Player1 y Player2 DEBEN SER INICIALIZADAs EN EL MISMO ORDEN QUE EL ENUMERADOR Player_Selected.
    public UI_Manager uI_Manager;

    public List<GameObject> Players1;
    public List<GameObject> Players2;

    public List<Sprite> spriteWining;
    public GameObject fondoWining;
    public GameObject fondoEmpate;
    public GameObject prefabWinPlayer1;
    public GameObject prefabWinPlayer2;
    public GameObject eventWisse;
    //LA LISTA levels DEBE SER INICIALIZADA EN EL MISMO ORDEN QUE EL ENUMERADOR Level_Selected.
    public List<Sprite> levels;
    public Player player1;
    public Player player2;

    public SpriteRenderer spritePlayer1Win;
    public SpriteRenderer spritePlayer2Win;
    public Player_Selected player1_selected;
    public Player_Selected player2_selected;
    public Level_Selected level_selected;
    public ModoDeJuego modoDeJuego;
    public List<GameObject> FondosNivel;
    public GameObject menuPausa_P1;
    public GameObject menuPausa_P2;
    private GameManager gm;
    public bool resetScorePlayers;
    private bool startDelayFinishRound;
    public float delayFinishRound;
    public float auxDelayFinishRound;
    private bool soundEnter;
    void Start()
    {
        soundEnter = false;
        fondoWining.SetActive(false);
        fondoEmpate.SetActive(false);
        if (GameManager.instanceGameManager != null)
        {
            gm = GameManager.instanceGameManager;
        }
        InitDataCombat();
        player1 = GameObject.Find("Player1").GetComponent<Player>();
        player2 = GameObject.Find("Player2").GetComponent<Player>();
        uI_Manager.players[0] = player1;
        uI_Manager.players[1] = player2;
        uI_Manager.InitUI();
        player1.barraDeEscudo.SetPlayer(player1);
        player2.barraDeEscudo.SetPlayer(player2);
    }
    public void InitDataCombat()
    {
        player1_selected = gm.structGameManager.gm_dataCombatPvP.player1_selected;
        player2_selected = gm.structGameManager.gm_dataCombatPvP.player2_selected;
        level_selected = gm.structGameManager.gm_dataCombatPvP.level_selected;
        Instantiate(Players1[(int)player1_selected]);
        Instantiate(Players2[(int)player2_selected]);
        for (int i = 0; i < FondosNivel.Count; i++)
        {
            if (FondosNivel[i] != null)
            {
                FondosNivel[i].SetActive(false);
            }
        }
        if (FondosNivel[(int)level_selected] != null)
        {
            FondosNivel[(int)level_selected].SetActive(true);
        }
        //Debug.Log(player1_selected);
        //Debug.Log(player2_selected);
        //Debug.Log(level_selected);
    }
    private void Update()
    {
        if (player1 != null && player2 != null)
        {
            if (player1.PD.lifePlayer <= 0 && player1.transform.position.y <= player1.GetInitialPosition().y || player2.PD.lifePlayer <= 0 && player2.transform.position.y <= player2.GetInitialPosition().y) 
            {
                startDelayFinishRound = true;
            }
            else if (player1.PD.lifePlayer > 0 && player2.PD.lifePlayer > 0)
            {
                startDelayFinishRound = false;
                delayFinishRound = auxDelayFinishRound;
            }

            if (delayFinishRound > 0 && startDelayFinishRound)
            {
                delayFinishRound = delayFinishRound - Time.deltaTime;
            }
            else if (delayFinishRound <= 0)
            {
                CheckConditionWin();
            }
        }
    }
    public void CheckConditionWin()
    {
            
        switch (modoDeJuego)
        {
            case ModoDeJuego.PvP:
                CheckWinPvP();
                break;
            case ModoDeJuego.TiroAlBlanco:
                CheckWinTiroAlBlanco();
                break;
        }
    }
    public void CheckWinTiroAlBlanco()
    {
        if (player1.PD.lifePlayer <= 0 || player2.PD.lifePlayer <= 0)
        {
            if (player1.PD.score == player2.PD.score)
            {
                CheckEmpate();
            }
            else if (player1.PD.score > player2.PD.score)
            {
                CheckWinPlayer(ref gm.structGameManager.gm_dataCombatPvP.countRoundsWiningP1, prefabWinPlayer1, prefabWinPlayer2, spritePlayer1Win, (int)player1_selected);
            }
            else if (player2.PD.score > player1.PD.score)
            {
                CheckWinPlayer(ref gm.structGameManager.gm_dataCombatPvP.countRoundsWiningP2, prefabWinPlayer2, prefabWinPlayer1, spritePlayer2Win, (int)player2_selected);
            }
        }
    }

    public void CheckWinPvP()
    {
        if (player1.PD.lifePlayer <= 0)
        {
            ResetScore();
            CheckWinPlayer(ref gm.structGameManager.gm_dataCombatPvP.countRoundsWiningP2, prefabWinPlayer2, prefabWinPlayer1,spritePlayer2Win, (int)player2_selected);
        }
        else if (player2.PD.lifePlayer <= 0)
        {
            ResetScore();
            CheckWinPlayer(ref gm.structGameManager.gm_dataCombatPvP.countRoundsWiningP1, prefabWinPlayer1, prefabWinPlayer2, spritePlayer1Win, (int)player1_selected);
        }
    }
    public void ResetScore()
    {
        if (resetScorePlayers)
        {
            player1.PD.score = 0;
            player2.PD.score = 0;
        }
    }
    public void CheckEmpate()
    {
        gm.structGameManager.gm_dataCombatPvP.rondaActual++;
        if (gm.structGameManager.gm_dataCombatPvP.rondaActual < gm.structGameManager.gm_dataCombatPvP.countRounds)
        {
            ReiniciarRonda();
            soundEnter = false;
            player1.eventWise.StartEvent("fuego_termina");
            player2.eventWise.StartEvent("fuego_termina");
        }
        else
        {
            // EVENTO DE EMPATE DEL SONIDO 
            menuPausa_P1.SetActive(false);
            menuPausa_P2.SetActive(false);
            FondosNivel[(int)level_selected].SetActive(false);
            fondoEmpate.SetActive(true);
            prefabWinPlayer1.SetActive(true);
            prefabWinPlayer2.SetActive(true);
            spritePlayer1Win.sprite = spriteWining[(int)player1_selected];
            spritePlayer2Win.sprite = spriteWining[(int)player2_selected];
            uI_Manager.gameObject.SetActive(false);
            player1.gameObject.SetActive(false);
            player2.gameObject.SetActive(false);
            player1.gameObject.SetActive(false);
            player2.gameObject.SetActive(false);
            player1.eventWise.StartEvent("fuego_termina");
            player2.eventWise.StartEvent("fuego_termina");
        }
    }
    public void CheckWinPlayer(ref int countRoundsWinPlayer, GameObject prefabPlayerWin, GameObject prefabPlayerLose, SpriteRenderer spritePlayerWin, int playerSelected)
    {
        countRoundsWinPlayer++;
        if (countRoundsWinPlayer < gm.structGameManager.gm_dataCombatPvP.countRounds)
        {
            ReiniciarRonda();
            soundEnter = false;
            player1.eventWise.StartEvent("fuego_termina");
            player2.eventWise.StartEvent("fuego_termina");
        }
        else
        {
            if (!soundEnter)
            {
                AkSoundEngine.PostEvent("pvp_ganador", eventWisse);
                soundEnter = true;
            }

            menuPausa_P1.SetActive(false);
            menuPausa_P2.SetActive(false);
            FondosNivel[(int)level_selected].SetActive(false);
            fondoWining.SetActive(true);
            prefabPlayerWin.SetActive(true);
            prefabPlayerLose.SetActive(false);
            spritePlayerWin.sprite = spriteWining[playerSelected];
            uI_Manager.gameObject.SetActive(false);
            player1.gameObject.SetActive(false);
            player2.gameObject.SetActive(false);
            player1.eventWise.StartEvent("fuego_termina");
            player2.eventWise.StartEvent("fuego_termina");
        }
        gm.structGameManager.gm_dataCombatPvP.rondaActual++;
    }
    public void ReiniciarRonda()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    // ESTE SCRIPT DEBE TOMAR LA DEDICION CORRESPONDIENTE EN CUANTO AL NIVEL ELEJIDO Y PERSONAJE ELEJIDO DEPENDIENDO DE LA INFO QUE LE PASE EL
    // STRUCT GAME MANAGER
}
