using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public PlayerData playerData_P1;
    public PlayerData playerData_P2;
    public GameObject CanvasGameOver;
    public TextMeshProUGUI textCountEnemigosAbatidos;
    public TextMeshProUGUI textScorePlayer;
    public static GameManager instanceGameManager;
    public EnumsGameManager enumsGameManager;
    [HideInInspector]
    public bool InGameOverScene;
    [HideInInspector]
    public bool generateEnemy;
    public GameObject TituloPushButton;
    public GameObject buttonGameOver;
    [HideInInspector]
    public int countEnemysDead;
    [HideInInspector]
    public int auxCountEnemysDead;
    [HideInInspector]
    public int totalCountEnemysDead;
    public ScreenManager screenManager;
    public StructGameManager structGameManager;
    private int roundCombat;
    [HideInInspector]
    public bool restartLevel;
    private void Awake()
    {
        if (instanceGameManager == null)
        {
            instanceGameManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instanceGameManager != null)
        {
            gameObject.SetActive(false);
        }
    }
    void Start()
    {
        structGameManager.gm_dataCombatPvP.pointsForHit = true;
        TituloPushButton.SetActive(false);
        buttonGameOver.SetActive(false);
        countEnemysDead = 0;
    }

    void Update()
    {
        UnityEngine.Cursor.visible = false;
        if (SceneManager.GetActiveScene().name == "MENU" 
            || SceneManager.GetActiveScene().name == "SelectPlayerScene")
        {
            CanvasGameOver.SetActive(false);
            ResetGameManager();
            screenManager.SetIdListLevel(-1);
        }
        CheckGameOverScene("GameOverHistoria");
        CheckGameOverScene("GameOverSupervivencia");
    }
    public void ResetGameManager()
    {
        countEnemysDead = 0;
        roundCombat = 0;
        structGameManager.gm_dataCombatPvP.rondaActual = 0;
        structGameManager.gm_dataCombatPvP.countRoundsWiningP1 = 0;
        structGameManager.gm_dataCombatPvP.countRoundsWiningP2 = 0;
        structGameManager.gm_dataCombatPvP.countRounds = 0;
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
    public void GameOver(string finishScene)
    {
        if (screenManager.eventWise != null)
        {
            screenManager.eventWise.StartEvent("volver_al_menu");
        }
        SceneManager.LoadScene(finishScene);
    }
    public void CheckGameOverScene(string finishScene)
    {
        if (SceneManager.GetActiveScene().name == finishScene)
        {
            textCountEnemigosAbatidos.text = "Enemigos Abatidos: " + countEnemysDead;
            textScorePlayer.text = "Puntaje total: " + playerData_P1.score;
            CanvasGameOver.SetActive(true);
        }
    }
}
