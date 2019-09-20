using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
namespace Prototipo_2
{
    public class LevelManager : MonoBehaviour
    {
        public GameObject marcoTexto;
        public GameObject imageJugadorHablando;
        public GameObject imageEnemigoHablando;
        public TextMeshProUGUI textDialog;
        public List<Dialogos> dialogos;
        private GameManager gm;
        public bool InitDialog;
        public string ButtonPassText;
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
        }
        void Update()
        {
            CheckDiagolos();
            CheckPassLevel();
            if (Input.GetKeyDown(ButtonPassText))
            {
                NextId();
            }
        }
        public void CheckPassLevel()
        {
            if (ObjectiveOfPassLevel <= 0)
            {
                NextLevel();
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
                inDialog = false;
                Time.timeScale = 1;
                DisableChat();
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
            gm.screenManager.LoadLevel(gm.screenManager.GetIdListLevel()+1);
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
}
