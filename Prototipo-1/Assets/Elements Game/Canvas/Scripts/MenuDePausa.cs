using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Prototipo_2
{
    public class MenuDePausa : MonoBehaviour
    {
        public GameObject MenuPausa;
        public GameObject MenuOpciones;
        public GameObject MenuControles;
        public LevelManager levelManager;
        private GameManager gm;
        // Start is called before the first frame update
        void Start()
        {
            if (GameManager.instanceGameManager != null)
            {
                gm = GameManager.instanceGameManager;
            }
            MenuPausa.SetActive(false);
        }
        private void OnEnable()
        {
            OnMenuPausa();
        }
        // Update is called once per frame
        void Update()
        {
            if (!levelManager.GetInDialog())
            {
                CheckInPause();
            }
        }
        public void Reanudar()
        {
            Time.timeScale = 1;
        }
        public void ReiniciarNivel()
        {
            gm.auxCountEnemysDead = gm.countEnemysDead;
            gm.restartLevel = true;
            if (gm.countEnemysDead == gm.auxCountEnemysDead && gm.restartLevel)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        public void CheckInPause()
        {
            if (Time.timeScale == 1)
            {
                MenuPausa.gameObject.SetActive(false);
            }
            else if (Time.timeScale == 0)
            {
                MenuPausa.gameObject.SetActive(true);
            }
        }
        public void OnMenuPausa()
        {
            MenuPausa.SetActive(true);
            MenuOpciones.SetActive(false);
            MenuControles.SetActive(false);
        }
        public void OnMenuOpciones()
        {
            MenuPausa.SetActive(false);
            MenuOpciones.SetActive(true);
            MenuControles.SetActive(false);
        }
        public void OnMenuControles()
        {
            MenuPausa.SetActive(false);
            MenuOpciones.SetActive(false);
            MenuControles.SetActive(true);
        }
    }
}
