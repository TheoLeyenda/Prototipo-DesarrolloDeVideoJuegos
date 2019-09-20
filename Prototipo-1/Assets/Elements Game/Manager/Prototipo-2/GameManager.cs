using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
namespace Prototipo_2
{
    public class GameManager : MonoBehaviour
    {
        public GameObject CanvasGameOver;
        public TextMeshProUGUI textCountEnemigosAbatidos;
        public static GameManager instanceGameManager;
        public EnumsGameManager enumsGameManager;
        [HideInInspector]
        public bool InGameOverScene;
        [HideInInspector]
        public bool generateEnemy;
        public int RondasPorJefe;
        public bool SiglePlayer;
        public bool MultiPlayer;
        public GameObject TituloPushButton;
        public GameObject FondoPushButton;
        public GameObject buttonGameOver;
        [HideInInspector]
        public int countEnemysDead;
        public ScreenManager screenManager;
        private int roundCombat;
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
            }
            if (SceneManager.GetActiveScene().name == "GameOver")
            {
                CanvasGameOver.SetActive(true);
            }

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
        public void GameOver()
        {
            textCountEnemigosAbatidos.text = "Enemigos Abatidos: "+countEnemysDead;
            SceneManager.LoadScene("GameOver");
        }
        
    }
}
