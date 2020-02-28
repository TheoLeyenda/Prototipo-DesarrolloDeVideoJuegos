using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public bool NivelFinal;
    public string NameFinishSceneStoryMode;
    public GameObject marcoTexto;
    public GameObject imageJugadorHablando;
    public GameObject imageEnemigoHablando;
    public GameObject CamvasInicioPelea;
    public TextMeshProUGUI textDialog;
    public List<Dialogos> dialogos;
    private GameManager gm;
    public bool InitDialog;
    public int ObjectiveOfPassLevel;
    private int Level;
    private bool inDialog;
    private int idDialogo;

    [System.Serializable]
    public class Dialogos
    {
        public string habladorActual;
        public string dialogoPersonaje;
    }
    private void Start()
    {
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
        CheckDiagolos();
    }
    void Update()
    {
        CheckDiagolos();
        CheckPassLevel();
        if (InputPlayerController.GetInputButtonDown("SelectButton_P1"))
        {
            NextId();
        }
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
    public void CheckDiagolos()
    {
        if (idDialogo < dialogos.Count && inDialog)
        {
            Time.timeScale = 0;
            inDialog = true;
            if (dialogos[idDialogo].habladorActual == "Jugador")
            {
                imageJugadorHablando.SetActive(true);
                imageEnemigoHablando.SetActive(false);
            }
            else if (dialogos[idDialogo].habladorActual == "Enemigo")
            {
                imageJugadorHablando.SetActive(false);
                imageEnemigoHablando.SetActive(true);
            }
            textDialog.text = dialogos[idDialogo].dialogoPersonaje;
        }
        else
        {
            if (inDialog && !InputPlayerController.GetInputButtonDown("JumpButton_P1"))
            {
                Time.timeScale = 1;
            }
            inDialog = false;
            DisableChat();

            CamvasInicioPelea.SetActive(true);
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
            imageEnemigoHablando.SetActive(false);
            imageJugadorHablando.SetActive(false);
            textDialog.gameObject.SetActive(false);
    }
    public void NextLevel()
    {
        if (!NivelFinal)
        {
            gm.screenManager.LoadLevel(gm.screenManager.GetIdListLevel() + 1);
        }
        else
        {
            gm.GameOver(NameFinishSceneStoryMode);
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
