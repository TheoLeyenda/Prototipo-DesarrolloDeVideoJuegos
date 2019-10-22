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
            idListaNiveles = 0;
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
            SceneManager.LoadScene("Nivel " + numerLevel); 
        }
        public void Prueba()
        {
            SceneManager.LoadScene("SampleScene");
        }
        public void Supervivencia()
        {
            gm.enumsGameManager.modoDeJuego = EnumsGameManager.ModosDeJuego.Supervivencia;
            SceneManager.LoadScene("Supervivencia");
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
            if (idListaNiveles < ListaNiveles.Count)
            {
                if (gm != null && SceneManager.GetActiveScene().name != "MENU" && SceneManager.GetActiveScene().name != "Supervivencia"
                    && SceneManager.GetActiveScene().name != "SampleScene" && SceneManager.GetActiveScene().name != "GameOver"
                    && SceneManager.GetActiveScene().name != "SelectLevel" && SceneManager.GetActiveScene().name != "SelectPlayerScene"
                    && SceneManager.GetActiveScene().name != "PvP")
                {

                    if (fondo == null)
                    {
                        
                        fondo = GameObject.Find("Fondo");
                        SpriteRenderer sr = fondo.GetComponent<SpriteRenderer>();
                        if (sr != null)
                        {
                            spriteRendererFondo = sr;
                            spriteRendererFondo.sprite = ListaNiveles[idListaNiveles];
                            idListaNiveles++;
                        }
                    }
                    
                }
            }
            else if(SceneManager.GetActiveScene().name != "MENU")
            {
                idListaNiveles = 0;
            }
        }
        public void PvP()
        {
            gm.enumsGameManager.modoDeJuego = EnumsGameManager.ModosDeJuego.Nulo;
            idListaNiveles = 0;
            SceneManager.LoadScene("SelectPlayerScene");
        }
        public void Menu()
        {
            gm.enumsGameManager.modoDeJuego = EnumsGameManager.ModosDeJuego.Nulo;
            idListaNiveles = 0;
            SceneManager.LoadScene("MENU");
        }
        private void Update()
        {
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
            return idListaNiveles;
        }
    }
}