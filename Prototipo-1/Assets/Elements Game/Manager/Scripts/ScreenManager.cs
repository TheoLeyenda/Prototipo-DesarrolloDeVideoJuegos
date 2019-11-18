using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Prototipo_2 {
    public class ScreenManager : MonoBehaviour
    {
        private GameObject fondo;
        private SpriteRenderer spriteRendererFondo;
        private GameManager gm;
        public List<Sprite> ListaNiveles;
        private int idListaNiveles;

        private void Start()
        {
            idListaNiveles = -1;
            if (GameManager.instanceGameManager != null)
            {
                gm = GameManager.instanceGameManager;
            }
        }
        public void Multijugador()
        {
            SceneManager.LoadScene("Multijugador");
        }
        public void TiroAlBlanco()
        {
            if (gm != null)
            {
                gm.structGameManager.gm_dataCombatPvP.modoElegido = StructGameManager.ModoPvPElegido.TiroAlBlanco;
                gm.enumsGameManager.modoDeJuego = EnumsGameManager.ModosDeJuego.Nulo;
                idListaNiveles = -1;
                if (idListaNiveles == -1 && gm.enumsGameManager.modoDeJuego == EnumsGameManager.ModosDeJuego.Nulo
                    && gm.structGameManager.gm_dataCombatPvP.modoElegido == StructGameManager.ModoPvPElegido.TiroAlBlanco)
                {
                    SceneManager.LoadScene("SelectPlayerScene");
                }
            }
        }
        public void Controles()
        {
            SceneManager.LoadScene("Controles");
        }
        public void Creditos()
        {
            SceneManager.LoadScene("Creditos");
        }
        public void HighScore()
        {
            gm.CanvasGameOver.SetActive(false);
            if (!gm.CanvasGameOver.activeSelf)
            {
                SceneManager.LoadScene("HighScore");
            }
        }
        
        public void LoadLevel(int numerLevel)
        {
            if (gm != null)
            {
                gm.totalCountEnemysDead = gm.totalCountEnemysDead + gm.auxCountEnemysDead;
                gm.countEnemysDead = gm.auxCountEnemysDead;
                gm.auxCountEnemysDead = 0;
                gm.enumsGameManager.modoDeJuego = EnumsGameManager.ModosDeJuego.Historia;
                if (gm.enumsGameManager.modoDeJuego == EnumsGameManager.ModosDeJuego.Historia)
                {
                    SceneManager.LoadScene("Nivel " + numerLevel);
                }
            }
        }
        public void SelectPlayerScene()
        {
            SceneManager.LoadScene("SelectPlayerScene");
        }
        public void Prueba()
        {
            SceneManager.LoadScene("SampleScene");
        }
        public void Supervivencia()
        {
            gm.enumsGameManager.modoDeJuego = EnumsGameManager.ModosDeJuego.Supervivencia;
            if (gm.enumsGameManager.modoDeJuego == EnumsGameManager.ModosDeJuego.Supervivencia)
            {
                SceneManager.LoadScene("Supervivencia");
            }
        }
        public void Salir()
        {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
                Application.Quit();
        }
        public void Historia()
        {
            CheckMainCameraInScreen();
        }
        public void CheckMainCameraInScreen()
        {
            //Debug.Log(gm.restartLevel);
            if (idListaNiveles < ListaNiveles.Count)
            {
                if ((gm != null && SceneManager.GetActiveScene().name != "MENU" && SceneManager.GetActiveScene().name != "Supervivencia"
                    && SceneManager.GetActiveScene().name != "SampleScene" && SceneManager.GetActiveScene().name != "GameOver"
                    && SceneManager.GetActiveScene().name != "SelectLevel" && SceneManager.GetActiveScene().name != "SelectPlayerScene"
                    && SceneManager.GetActiveScene().name != "PvP"))
                {
                    if (fondo == null)
                    {
                        fondo = GameObject.Find("SpriteFondo");
                        if (fondo != null)
                        {
                            SpriteRenderer sr = fondo.GetComponent<SpriteRenderer>();
                            if (sr != null)
                            {
                                if (!gm.restartLevel)
                                {
                                    idListaNiveles++;
                                }
                                else
                                {
                                    gm.restartLevel = false;
                                }
                                spriteRendererFondo = sr;
                                //Debug.Log(idListaNiveles);
                                if (idListaNiveles < ListaNiveles.Count)
                                {
                                    spriteRendererFondo.sprite = ListaNiveles[idListaNiveles];
                                }
                            }
                        }
                    }
                }
                
            }
        }
        public void PvP()
        {
            if (gm != null)
            {
                gm.structGameManager.gm_dataCombatPvP.modoElegido = StructGameManager.ModoPvPElegido.PvP;
                gm.enumsGameManager.modoDeJuego = EnumsGameManager.ModosDeJuego.Nulo;
                idListaNiveles = -1;
                if (idListaNiveles == -1 && gm.enumsGameManager.modoDeJuego == EnumsGameManager.ModosDeJuego.Nulo)
                {
                    SceneManager.LoadScene("SelectPlayerScene");
                }
            }
        }
        public void Menu()
        {
            gm.enumsGameManager.modoDeJuego = EnumsGameManager.ModosDeJuego.Nulo;
            idListaNiveles = -1;
            Time.timeScale = 1;
            if (idListaNiveles == -1 && gm.enumsGameManager.modoDeJuego == EnumsGameManager.ModosDeJuego.Nulo && Time.timeScale == 1)
            {
                gm.CanvasGameOver.SetActive(false);
                if (!gm.CanvasGameOver.activeSelf)
                {
                    SceneManager.LoadScene("MENU");
                }
            }
        }
        private void Update()
        {
            //Debug.Log(idListaNiveles);
            if (gm != null)
            {
                if (gm.enumsGameManager.modoDeJuego == EnumsGameManager.ModosDeJuego.Historia)
                {
                    Historia();
                }
            }
        }
        public int GetIdListLevel()
        {
            return idListaNiveles + 1;
        }
        public void SetIdListLevel(int id)
        {
            idListaNiveles = id;
        }
    }
}