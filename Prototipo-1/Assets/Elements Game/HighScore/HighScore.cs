using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    // Start is called before the first frame update

    [System.Serializable]
    public class ScoreData
    {
        public string namePlayer;
        public float scorePlayer;
    }
    // SERA UNA LISTA DE LOS MEJORES 10 PUNTAJES
    public List<ScoreData> scores;
    public List<ScoreData> initialHighScoreData;
    public float scoreActual;
    public string nameActual;
    void Start()
    {
        LoadListScore();
        CheckListScore();
        SaveListScore();
    }
    public void LoadListScore()
    {
        for (int i = 0; i < scores.Count; i++)
        {
            scores[i].namePlayer = PlayerPrefs.GetString(scores[i].namePlayer, scores[i].namePlayer);
            scores[i].scorePlayer = PlayerPrefs.GetFloat(scores[i].namePlayer, 0);
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
        int id = -1;
        for (int i = 0; i < scores.Count; i++)
        {
            if(scoreActual > scores[i].scorePlayer)
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
