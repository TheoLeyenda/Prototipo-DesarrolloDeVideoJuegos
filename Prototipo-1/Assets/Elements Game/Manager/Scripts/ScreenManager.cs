using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Prototipo_2 {
    public class ScreenManager : MonoBehaviour
    {
        private GameObject fondo;
        private SpriteRenderer spriteRendererFondo;
        public GameManager gm;
        public List<Sprite> ListaNiveles;
        private int idListaNiveles;

        private void Start()
        {
            idListaNiveles = -1;
            if (gm == null)
            {
                GameObject go = GameObject.Find("GameManager");
                if (go != null)
                {
                    gm = go.GetComponent<GameManager>();
                }
            }
        }
        public void LoadLevel(int numerLevel)
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
                        fondo = GameObject.Find("Fondo");
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
                            if (idListaNiveles < ListaNiveles.Count)
                            {
                                spriteRendererFondo.sprite = ListaNiveles[idListaNiveles];
                            }
                        }
                    }
                }
                
            }
        }
        public void PvP()
        {
            gm.enumsGameManager.modoDeJuego = EnumsGameManager.ModosDeJuego.Nulo;
            idListaNiveles = -1;
            if (idListaNiveles == -1 && gm.enumsGameManager.modoDeJuego == EnumsGameManager.ModosDeJuego.Nulo)
            {
                SceneManager.LoadScene("SelectPlayerScene");
            }
        }
        public void Menu()
        {
            gm.enumsGameManager.modoDeJuego = EnumsGameManager.ModosDeJuego.Nulo;
            idListaNiveles = -1;
            if (idListaNiveles == -1 && gm.enumsGameManager.modoDeJuego == EnumsGameManager.ModosDeJuego.Nulo)
            {
                SceneManager.LoadScene("MENU");
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