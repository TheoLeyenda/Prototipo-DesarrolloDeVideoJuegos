using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Prototipo_2
{
    public class MenuDePausa : MonoBehaviour
    {
        public enum MenuActual
        {
            MenuPausa,
            MenuOpciones,
            MenuControles,
        }
        public GameObject MenuPausa;
        public GameObject MenuOpciones;
        public GameObject MenuControles;
        public LevelManager levelManager;
        private GameManager gm;
        private MenuActual menuActual;
        // Start is called before the first frame update
        void Start()
        {
            menuActual = MenuActual.MenuPausa;
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
            if (levelManager != null)
            {
                if (!levelManager.GetInDialog())
                {
                    CheckInPause();
                }
            }
            else
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
            DisableMenues();
            if (levelManager != null)
            {
                gm.restartLevel = true;
                // SI NO FUNCIONA BIEN EL RESTArt DESCOMENTAR ESA LINEA
                if (/*gm.countEnemysDead == gm.auxCountEnemysDead && */gm.restartLevel)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        public void CheckInPause()
        {
            if (menuActual == MenuActual.MenuPausa)
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
        }
        public void DisableMenues()
        {
            Time.timeScale = 1;
            MenuPausa.SetActive(false);
            MenuOpciones.SetActive(false);
            MenuControles.SetActive(false);
        }
        public void OnMenuPausa()
        {
            MenuPausa.SetActive(true);
            MenuOpciones.SetActive(false);
            MenuControles.SetActive(false);
            menuActual = MenuActual.MenuPausa;
        }
        public void OnMenuOpciones()
        {
            MenuPausa.SetActive(false);
            MenuOpciones.SetActive(true);
            MenuControles.SetActive(false);
            menuActual = MenuActual.MenuOpciones;
        }
        public void OnMenuControles()
        {
            MenuPausa.SetActive(false);
            MenuOpciones.SetActive(false);
            MenuControles.SetActive(true);
            menuActual = MenuActual.MenuControles;
        }
    }
}
