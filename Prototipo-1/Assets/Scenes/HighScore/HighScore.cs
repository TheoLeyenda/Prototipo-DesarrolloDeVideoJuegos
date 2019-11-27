using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

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
            public string claveNombre;
            public string clavePuntos;
            public TextMeshProUGUI textNamePlayer;
            public TextMeshProUGUI textScorePlayer;
        }
        // SERA UNA LISTA DE LOS MEJORES 10 PUNTAJES
        public GameObject prefabHighScoreList;
        public GameObject inputFiled;
        public GameObject TextIngresado;
        public GameObject Placeholder;
        public List<ScoreData> scores;
        private List<ScoreData> initialHighScoreData;
        private GameManager gm;
        [HideInInspector]
        public float scoreActual;
        [HideInInspector]
        public string nameActual;
        public Text nombreIngresado;
        public CheckSelectedEventSystem checkSelectedEventSystem;
        public bool RestartTable;
        private void Start()
        {
            initialHighScoreData = scores;
            inputFiled.SetActive(true);
            prefabHighScoreList.SetActive(false);
            if (GameManager.instanceGameManager != null)
            {
                gm = GameManager.instanceGameManager;
                gm.CanvasGameOver.SetActive(false);
            }
            if (RestartTable)
            {
                ResetScoreList();
            }

        }
        public void LoadData()
        {
            if (RestartTable)
            {
                ResetScoreList();
            }
            else if (nombreIngresado.text != "")
            {
                prefabHighScoreList.SetActive(true);
                nameActual = nombreIngresado.text;
                scoreActual = gm.playerData_P1.score;
                //SaveListScore();
                CheckListScore();
                LoadListScore();
                
                if (inputFiled != null && inputFiled.activeSelf)
                {
                    inputFiled.GetComponent<InputField>().enabled = false;
                    inputFiled.GetComponent<Image>().enabled = false;
                    Placeholder.gameObject.SetActive(false);
                    TextIngresado.gameObject.SetActive(false);
                }
                checkSelectedEventSystem.NewObjectCurrentFirst();
            }
        }
        public void LoadListScore()
        {
            for (int i = 0; i < scores.Count; i++)
            {
                //Debug.Log(PlayerPrefs.GetString(scores[i].claveNombre));
                scores[i].namePlayer = PlayerPrefs.GetString(scores[i].claveNombre, scores[i].namePlayer);
                scores[i].scorePlayer = PlayerPrefs.GetFloat(scores[i].clavePuntos, scores[i].scorePlayer);
                scores[i].textNamePlayer.text = "" + scores[i].namePlayer;
                scores[i].textScorePlayer.text = "" + scores[i].scorePlayer;
            }
        }
        public void SaveListScore()
        {
            for (int i = 0; i < scores.Count; i++)
            {
                PlayerPrefs.SetString(scores[i].claveNombre, scores[i].namePlayer);
                PlayerPrefs.SetFloat(scores[i].clavePuntos, scores[i].scorePlayer);
            }
        }
        // Update is called once per frame
        public void CheckListScore()
        {
            LoadListScore();
            int id = scores.Count + 1;
            for (int i = scores.Count-1; i >= 0 ; i--)
            {
                if (scoreActual > scores[i].scorePlayer)
                {
                    id = i;
                }
            }
            //Debug.Log(id);
            if (id >= 0 && id < scores.Count)
            {
                PlayerPrefs.DeleteKey(scores[id].claveNombre);
                PlayerPrefs.DeleteKey(scores[id].clavePuntos);
                PlayerPrefs.SetString(scores[id].claveNombre, nameActual);
                PlayerPrefs.SetFloat(scores[id].clavePuntos, scoreActual);
            }
        }
        public void ResetScoreList()
        {
            PlayerPrefs.DeleteAll();
            scores.Clear();
            scores = initialHighScoreData;
            SaveListScore();
        }
    }
}