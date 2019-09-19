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
        }
        public void Prueba()
        {
            SceneManager.LoadScene("SampleScene");
        }
        public void Supervivencia()
        {
            SceneManager.LoadScene("Supervivencia");
        }
        public void Historia()
        {
            CheckMainCameraInScreen();
        }
        public void CheckMainCameraInScreen()
        {
            if (gm != null && SceneManager.GetActiveScene().name != "MENU" && SceneManager.GetActiveScene().name != "Supervivencia" 
                && SceneManager.GetActiveScene().name != "SampleScene" && SceneManager.GetActiveScene().name != "GameOver")
            {
                if (fondo == null)
                {
                    fondo = GameObject.Find("Fondo");
                    Debug.Log(fondo);
                    spriteRendererFondo = fondo.GetComponent<SpriteRenderer>();
                    Debug.Log(spriteRendererFondo);
                    spriteRendererFondo.sprite = ListaNiveles[idListaNiveles];
                    idListaNiveles++;
                }
            }
        }
        public void Menu()
        {
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
    }
}