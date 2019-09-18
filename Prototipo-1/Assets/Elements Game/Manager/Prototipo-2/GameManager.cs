using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Prototipo_2
{
    public class GameManager : MonoBehaviour
    {
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

        private int roundCombat;
        // Start is called before the first frame update
        private void Awake()
        {
            if (instanceGameManager == null)
            {
                instanceGameManager = this;
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
            SceneManager.LoadScene("GameOver");
        }
        
    }
}
