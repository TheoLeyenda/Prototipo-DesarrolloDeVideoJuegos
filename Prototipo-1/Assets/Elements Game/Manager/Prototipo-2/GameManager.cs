using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
namespace Prototipo_2
{
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
        public GameObject FondoPushButton;
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
        // Start is called before the first frame update
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
            FondoPushButton.SetActive(false);
            TituloPushButton.SetActive(false);
            buttonGameOver.SetActive(false);
            countEnemysDead = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if (SceneManager.GetActiveScene().name == "MENU")
            {
                CanvasGameOver.SetActive(false);
                ResetGameManager();
                screenManager.SetIdListLevel(-1);
            }
            if (SceneManager.GetActiveScene().name == "GameOver")
            {
                CanvasGameOver.SetActive(true);
            }
            //Debug.Log(totalCountEnemysDead);
        }
        public void ResetGameManager()
        {
            countEnemysDead = 0;
            roundCombat = 0;
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
            textCountEnemigosAbatidos.text = "Enemigos Abatidos: " + countEnemysDead;
            textScorePlayer.text = "Puntaje total: " + playerData_P1.score;
            SceneManager.LoadScene(finishScene);
        }
    }
}
