using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Prototipo_2 {
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
        public List<GameObject> Players1;
        public List<GameObject> Players2;

        public List<Sprite> spriteWining;
        public GameObject fondoWining;
        public GameObject fondoEmpate;
        public GameObject prefabWinPlayer1;
        public GameObject prefabWinPlayer2;
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
        private GameManager gm;
        public bool resetScorePlayers;
        void Start()
        {
            fondoWining.SetActive(false);
            fondoEmpate.SetActive(false);
            if (GameManager.instanceGameManager != null)
            {
                gm = GameManager.instanceGameManager;
            }
            InitDataCombat();
            player1 = GameObject.Find("Player1").GetComponent<Player>();
            player2 = GameObject.Find("Player2").GetComponent<Player>();
            
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
        }
        private void Update()
        {
            //Debug.Log("P1:"+player1);
            //Debug.Log("P2:"+player2);
            if (player1 != null && player2 != null)
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
        }
        public void CheckWinTiroAlBlanco()
        {
            if (player1.PD.lifePlayer <= 0 || player1.PD.lifePlayer <= 0)
            {
                if (player1.PD.score == player2.PD.score)
                {
                    CheckEmpate();
                }
                else if (player1.PD.score > player2.PD.score)
                {
                    CheckWinPlayer1();
                }
                else if (player2.PD.score > player1.PD.score)
                {
                    CheckWinPlayer2();
                }
            }
        }

        public void CheckWinPvP()
        {
            if (player1.PD.lifePlayer <= 0)
            {
                ResetScore();
                CheckWinPlayer2();
            }
            else if (player2.PD.lifePlayer <= 0)
            {
                ResetScore();
                CheckWinPlayer1();
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
            if (gm.structGameManager.gm_dataCombatPvP.countRoundsWiningP1 < gm.structGameManager.gm_dataCombatPvP.countRounds
                && gm.structGameManager.gm_dataCombatPvP.countRoundsWiningP2 < gm.structGameManager.gm_dataCombatPvP.countRounds)
            {
                ReiniciarRonda();
            }
            else
            {
                FondosNivel[(int)level_selected].SetActive(false);
                fondoEmpate.SetActive(true);
                prefabWinPlayer1.SetActive(true);
                prefabWinPlayer2.SetActive(true);
                spritePlayer1Win.sprite = spriteWining[(int)player1_selected];
                spritePlayer2Win.sprite = spriteWining[(int)player2_selected];
                player1.BARRA_DE_CARGA.SetActive(false);
                player1.BARRA_DE_VIDA.SetActive(false);
                player2.BARRA_DE_CARGA.SetActive(false);
                player2.BARRA_DE_VIDA.SetActive(false);
                player1.gameObject.SetActive(false);
                player2.gameObject.SetActive(false);
                player1.BARRA_DE_CARGA.SetActive(false);
                player1.BARRA_DE_VIDA.SetActive(false);
                player2.BARRA_DE_CARGA.SetActive(false);
                player2.BARRA_DE_VIDA.SetActive(false);
                player1.gameObject.SetActive(false);
                player2.gameObject.SetActive(false);
            }
            gm.structGameManager.gm_dataCombatPvP.rondaActual++;
        }
        public void CheckWinPlayer1()
        {
            gm.structGameManager.gm_dataCombatPvP.countRoundsWiningP1++;
            if (gm.structGameManager.gm_dataCombatPvP.countRoundsWiningP1 < gm.structGameManager.gm_dataCombatPvP.countRounds)
            {
                ReiniciarRonda();
            }
            else
            {
                FondosNivel[(int)level_selected].SetActive(false);
                fondoWining.SetActive(true);
                prefabWinPlayer2.SetActive(false);
                prefabWinPlayer1.SetActive(true);
                spritePlayer1Win.sprite = spriteWining[(int)player1_selected];
                player1.BARRA_DE_CARGA.SetActive(false);
                player1.BARRA_DE_VIDA.SetActive(false);
                player2.BARRA_DE_CARGA.SetActive(false);
                player2.BARRA_DE_VIDA.SetActive(false);
                player1.gameObject.SetActive(false);
                player2.gameObject.SetActive(false);
            }
            gm.structGameManager.gm_dataCombatPvP.rondaActual++;
        }
        public void CheckWinPlayer2()
        {
            gm.structGameManager.gm_dataCombatPvP.countRoundsWiningP2++;
            if (gm.structGameManager.gm_dataCombatPvP.countRoundsWiningP2 < gm.structGameManager.gm_dataCombatPvP.countRounds)
            {
                ReiniciarRonda();

            }
            else
            {
                FondosNivel[(int)level_selected].SetActive(false);
                fondoWining.SetActive(true);
                prefabWinPlayer2.SetActive(true);
                prefabWinPlayer1.SetActive(false);
                spritePlayer2Win.sprite = spriteWining[(int)player2_selected];
                player1.BARRA_DE_CARGA.SetActive(false);
                player1.BARRA_DE_VIDA.SetActive(false);
                player2.BARRA_DE_CARGA.SetActive(false);
                player2.BARRA_DE_VIDA.SetActive(false);
                player1.gameObject.SetActive(false);
                player2.gameObject.SetActive(false);
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
}
