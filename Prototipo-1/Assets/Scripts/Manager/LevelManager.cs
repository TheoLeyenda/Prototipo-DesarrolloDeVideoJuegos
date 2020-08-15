using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public enum WaysToLevelUp
    {
        ByButton,
        ByTime,
        Count,
    }
    public WaysToLevelUp waysToLevelUp = WaysToLevelUp.ByTime;
    public bool NivelFinal;
    public string NameFinishSceneStoryMode;
    public GameObject marcoTexto;
    public Image imageHabladorActual;
    //public GameObject imageEnemigoHablando;
    public GameObject CamvasInicioPelea;
    public TextMeshProUGUI textDialog;
    //public TextMeshProUGUI textHabladorActual;
    public List<Dialogos> DialogoInicial;
    private GameManager gm;
    public bool InitDialog;
    [HideInInspector]
    public int ObjectiveOfPassLevel;
    public float delayPassLevel = 4f;
    private float auxDelayPassLevel;
    private int Level;
    private bool inDialog;
    private int idDialogo;
    public bool inSurvival;
    private bool goToGameOver;
    private bool disableOnlyOnce = false;
    private GameData gameData;

    private Player hablador_Player;
    private Enemy hablador_Enemy;
    [System.Serializable]
    public class Dialogos
    {
        [HideInInspector]
        public string nombreHabladorActual;
        public string habladorActual;
        public string dialogoPersonaje;
        public Sprite spriteHabladorActual;
        public enum CharacterDialog
        {
            Player,
            Enemy,
        }
        public CharacterDialog characterDialog;
        [HideInInspector]
        public Player p;
        [HideInInspector]
        public Enemy e;
    }
    private void OnEnable()
    {
        Player.OnDie += EnableGameOver;
        ActivatorDialogueManager.OnDialogueVictoryEnemyActivated += SwitchWaysToLevelUp;
    }
    private void OnDisable()
    {
        Player.OnDie -= EnableGameOver;
        ActivatorDialogueManager.OnDialogueVictoryEnemyActivated -= SwitchWaysToLevelUp;
        waysToLevelUp = WaysToLevelUp.ByTime;
    }
    private void Start()
    {
        gameData = GameData.instaceGameData;
        auxDelayPassLevel = delayPassLevel;
        if (InitDialog)
        {
            inDialog = true;
        }
        if (GameManager.instanceGameManager != null)
        {
            gm = GameManager.instanceGameManager;
        }
        Level = 1;
        ObjectiveOfPassLevel = 1;
        

    }
    
    void Update()
    {
        if (hablador_Enemy == null || hablador_Player == null && InitDialog)
        {
            GameObject enemy_go = GameObject.Find("Enemigo");
            GameObject player_go = GameObject.Find("Player1");
            if (enemy_go != null)
            {
                hablador_Enemy = enemy_go.GetComponent<Enemy>();
            }
            if (player_go != null)
            {
                hablador_Player = player_go.GetComponent<Player>();
            }
        }
        if (InitDialog && hablador_Enemy != null && hablador_Player != null)
        {
            for (int i = 0; i < DialogoInicial.Count; i++)
            {
                if (DialogoInicial[i].characterDialog == Dialogos.CharacterDialog.Player)
                {
                    DialogoInicial[i].p = hablador_Player;
                    DialogoInicial[i].nombreHabladorActual = DialogoInicial[i].p.namePlayer;
                }
                else
                {
                    DialogoInicial[i].e = hablador_Enemy;
                    DialogoInicial[i].nombreHabladorActual = DialogoInicial[i].e.nameEnemy;
                }
            }
            CheckDiagolos();
            InitDialog = false;
        }
        CheckDiagolos();
        if (inSurvival)
        {
            CheckPassGameOver();
        }
        else
        {
            CheckPassGameOver();
            CheckPassLevel();
        }
        if (InputPlayerController.GetInputButtonDown("SelectButton_P1"))
        {
            NextId();
        }
    }
    public void SwitchWaysToLevelUp(ActivatorDialogueManager activatorDialogueManager)
    {
        waysToLevelUp = WaysToLevelUp.ByButton;
        delayPassLevel = auxDelayPassLevel;
    }
    public void CheckPassLevel()
    {
        if (ObjectiveOfPassLevel <= 0)
        {
            NextLevel();
            gm.playerData_P1.auxScore = gm.playerData_P1.score;
            ObjectiveOfPassLevel = 1;
        }
    }
    public void EnableGameOver(Player p) 
    {
        goToGameOver = true;
    }
    public void CheckPassGameOver()
    {
        if (goToGameOver)
        {
            goToGameOver = false;
            switch (waysToLevelUp) 
            {
                case WaysToLevelUp.ByTime:
                    if (delayPassLevel <= 0)
                    {
                        delayPassLevel = auxDelayPassLevel;
                        if (SceneManager.GetActiveScene().name == "Supervivencia")
                        {
                            gm.GameOver("GameOverSupervivencia");
                        }
                        else if (SceneManager.GetActiveScene().name != "PvP" && SceneManager.GetActiveScene().name != "TiroAlBlanco")
                        {
                            gm.GameOver("GameOverHistoria");
                        }
                    }
                    else
                    {
                        delayPassLevel = delayPassLevel - Time.deltaTime;
                    }
                    break;
                case WaysToLevelUp.ByButton:
                    if (InputPlayerController.GetInputButtonDown("SelectButton_P1"))
                    {
                        if (SceneManager.GetActiveScene().name == "Supervivencia")
                        {
                            gm.GameOver("GameOverSupervivencia");
                        }
                        else if (SceneManager.GetActiveScene().name != "PvP" && SceneManager.GetActiveScene().name != "TiroAlBlanco")
                        {
                            gm.GameOver("GameOverHistoria");
                        }
                    }
                    break;
            }
            
        }
    }
    public void CheckDiagolos()
    {
        if (idDialogo < DialogoInicial.Count && inDialog)
        {
            Time.timeScale = 0;
            inDialog = true;
            imageHabladorActual.sprite = DialogoInicial[idDialogo].spriteHabladorActual;
            //textHabladorActual.text = " "; ;
            textDialog.text = DialogoInicial[idDialogo].nombreHabladorActual + ": " + DialogoInicial[idDialogo].dialogoPersonaje;
        }
        else
        {
            if (inDialog && !InputPlayerController.GetInputButtonDown("JumpButton_P1"))
            {
                Time.timeScale = 1;
            }
            inDialog = false;
            if (!disableOnlyOnce)
            {
                disableOnlyOnce = true;
                DisableChat();
                if (CamvasInicioPelea != null)
                {
                    CamvasInicioPelea.SetActive(true);
                }
            }
           
        }
    }
    public void NextId()
    {
        idDialogo++;
    }
    public void DisableChat()
    { 
            idDialogo = 0;
            marcoTexto.SetActive(false);
    }
    public void NextLevel()
    {
        switch (waysToLevelUp)
        {
            case WaysToLevelUp.ByTime:
                if (delayPassLevel <= 0)
                {
                    delayPassLevel = auxDelayPassLevel;
                    if (!NivelFinal)
                    {
                        //gm.screenManager.LoadLevel(gm.screenManager.GetIdListLevel() + 1);
                        gameData.currentLevel++;
                        gm.screenManager.LoadScene("SelectedPowerUp");
                    }
                    else
                    {
                        gm.GameOver(NameFinishSceneStoryMode);
                    }
                }
                else
                {
                    delayPassLevel = delayPassLevel - Time.deltaTime;
                }
                break;
            case WaysToLevelUp.ByButton:
                if (InputPlayerController.GetInputButtonDown("SelectButton_P1"))
                {
                    if (!NivelFinal)
                    {
                        //gm.screenManager.LoadLevel(gm.screenManager.GetIdListLevel() + 1);
                        gameData.currentLevel++;
                        gm.screenManager.LoadScene("SelectedPowerUp");
                    }
                    else
                    {
                        gm.GameOver(NameFinishSceneStoryMode);
                    }
                }
                break;
        }
    }
    public bool GetInDialog()
    {
        return inDialog;
    }
    public void SetInDialog(bool _inDialog)
    {
        inDialog = _inDialog;
    }
}
