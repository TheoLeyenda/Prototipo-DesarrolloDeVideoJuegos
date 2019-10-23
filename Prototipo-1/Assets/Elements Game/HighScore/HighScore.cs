using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Prototipo_2
{
    public class HighScore : MonoBehaviour
    {
        // Start is called before the first frame update

        [System.Serializable]
        public class ScoreData
        {
            public string namePlayer;
            public float scorePlayer;
            public TextMeshProUGUI textNamePlayer;
            public TextMeshProUGUI textScorePlayer;
        }
        // SERA UNA LISTA DE LOS MEJORES 10 PUNTAJES
        public GameObject prefabHighScoreList;
        public GameObject inputFiled;
        public List<ScoreData> scores;
        public List<ScoreData> initialHighScoreData;
        private GameManager gm;
        [HideInInspector]
        public float scoreActual;
        [HideInInspector]
        public string nameActual;
        public Text nombreIngresado;

        void Start()
        {
            inputFiled.SetActive(true);
            prefabHighScoreList.SetActive(false);
            if (GameManager.instanceGameManager != null)
            {
                gm = GameManager.instanceGameManager;
            }
            //USAR EL VIDEO DE "Input Feld erstellen - Text / Passwort richtig auslesen - Unity 2018" PARA INCORPORAR UNA INTERFAZ
            //PARA QUE EL JUGADOR INGRESE SU NOMBRE.

        }
        public void LoadData()
        {
            prefabHighScoreList.SetActive(true);
            inputFiled.SetActive(false);
            nameActual = nombreIngresado.text;
            scoreActual = gm.playerData_P1.score;
            CheckListScore();
            SaveListScore();
            LoadListScore();
        }
        public void LoadListScore()
        {
            for (int i = 0; i < scores.Count; i++)
            {
                scores[i].namePlayer = PlayerPrefs.GetString(scores[i].namePlayer, scores[i].namePlayer);
                Debug.Log(PlayerPrefs.GetFloat(scores[i].namePlayer, scores[i].scorePlayer));
                scores[i].scorePlayer = PlayerPrefs.GetFloat(scores[i].namePlayer, scores[i].scorePlayer);
                scores[i].textNamePlayer.text = "" + scores[i].namePlayer;
                scores[i].textScorePlayer.text = "" + scores[i].scorePlayer;
            }
        }
        public void SaveListScore()
        {
            for (int i = 0; i < scores.Count; i++)
            {
                PlayerPrefs.SetString(scores[i].namePlayer, scores[i].namePlayer);
                PlayerPrefs.SetFloat(scores[i].namePlayer, scores[i].scorePlayer);
            }
        }
        // Update is called once per frame
        void Update()
        {

        }
        public void CheckListScore()
        {
            int id = scores.Count;
            for (int i = scores.Count-1; i >= 0 ; i--)
            {
                if (scoreActual > scores[i].scorePlayer)
                {
                    id = i;
                }
            }
            if (id >= 0 && id < scores.Count)
            {
                scores[id].namePlayer = nameActual;
                scores[id].scorePlayer = scoreActual;
            }
        }
        public void ResetScoreList()
        {
            scores.Clear();
            scores = initialHighScoreData;
        }
    }
}