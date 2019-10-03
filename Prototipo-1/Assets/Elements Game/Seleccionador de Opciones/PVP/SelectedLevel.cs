using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Prototipo_2
{
    public class SelectedLevel : MonoBehaviour
    {
        // ESTE SCRIPT DEBE COMUNICAR AL STRUCT DEL GAME MANAGER LAS SELECCIONES DE LOS NIVELES
        struct CursorMatriz
        {
            public int x;
            public int y;
            public bool condirmed;
        }
        public enum BackgroundLevels
        {
            Anatomia,
            Historia,
            EducacionFisica,
            Arte,
            Matematica,
            Quimica,
            Programacion,
            TESIS,
            Cafeteria,
            Count
        }
        private GameManager gm;
        public string nameNextScene;
        public List<string> nameLevelsOptions;
        public Cursor CursorSelectorPlayer1;
        //public Cursor CursorSelectorPlayer2;
        private string[,] grillaDeSeleccion;
        public int filas;
        public int columnas;
        private int idOption;
        private CursorMatriz cursorPlayer1;
        private CursorMatriz cursorPlayer2;
        private bool aviableMoveHorizontal;
        private bool aviableMoveVertical;
        public SpriteRenderer background;
        public List<Sprite> fondos;
        void Start()
        {
            aviableMoveHorizontal = true;
            aviableMoveVertical = true;
            if (GameManager.instanceGameManager != null)
            {
                gm = GameManager.instanceGameManager;
            }
            idOption = 0;

            cursorPlayer1.x = 1;
            cursorPlayer1.y = columnas-1;

            cursorPlayer2.x = 2;
            cursorPlayer2.y = 0;
            if (filas > 0 && columnas > 0)
            {
                grillaDeSeleccion = new string[filas, columnas];
                if (grillaDeSeleccion != null)
                {
                    for (int i = 0; i < filas; i++)
                    {
                        for (int j = 0; j < columnas; j++)
                        {
                            if (idOption < nameLevelsOptions.Count)
                            {
                                grillaDeSeleccion[i, j] = nameLevelsOptions[idOption];
                            }
                            idOption++;
                        }
                    }
                }
            }
            idOption = 0;
        }

        void Update()
        {
            MoveCursor();
            CheckSelectCursor();
            CheckFondo();
        }
        public void MoveCursor()
        {
            if (cursorPlayer1.x >= 0 && cursorPlayer1.x < filas)
            {
                if (InputPlayerController.Horizontal_Button_P1() > 0 && cursorPlayer1.x < filas - 1)
                {
                    if (aviableMoveHorizontal)
                    {
                        cursorPlayer1.x++;
                        CursorSelectorPlayer1.MoveRight();
                        aviableMoveHorizontal = false;
                    }
                }
                else if (InputPlayerController.Horizontal_Button_P1() < 0 && cursorPlayer1.x > 0)
                {
                    if (aviableMoveHorizontal)
                    {
                        cursorPlayer1.x--;
                        CursorSelectorPlayer1.MoveLeft();
                        aviableMoveHorizontal = false;
                    }
                }
            }
            if (cursorPlayer1.y >= 0 && cursorPlayer1.y < columnas)
            {
                if (InputPlayerController.Vertical_Button_P1() > 0 && cursorPlayer1.y > 0)
                {
                    if (aviableMoveVertical)
                    {
                        Debug.Log("ENTRE");
                        cursorPlayer1.y--;
                        CursorSelectorPlayer1.MoveUp();
                        aviableMoveVertical = false;
                    }
                }
                else if (InputPlayerController.Vertical_Button_P1() < 0 && cursorPlayer1.y < columnas - 1)
                {
                    if (aviableMoveVertical)
                    {
                        cursorPlayer1.y++;
                        CursorSelectorPlayer1.MoveDown();
                        aviableMoveVertical = false;
                    }
                }
            }
            if (InputPlayerController.Vertical_Button_P1() == 0)
            {
                aviableMoveVertical = true;
            }
            if (InputPlayerController.Horizontal_Button_P1() == 0)
            {
                aviableMoveHorizontal = true;
            }
        }
        public void CheckSelectCursor()
        {
            if (InputPlayerController.SelectButton_P1())
            {
                Debug.Log(cursorPlayer1.x + ", " + cursorPlayer1.y);
                Debug.Log(grillaDeSeleccion[cursorPlayer1.x, cursorPlayer1.y]);
                switch (grillaDeSeleccion[cursorPlayer1.x, cursorPlayer1.y])
                {
                    case "Aula_Anatomia":
                        gm.structGameManager.gm_dataCombatPvP.level_selected = DataCombatPvP.Level_Selected.Anatomia;
                        cursorPlayer1.condirmed = true;
                        break;
                    case "Aula_Historia":
                        gm.structGameManager.gm_dataCombatPvP.level_selected = DataCombatPvP.Level_Selected.Historia;
                        cursorPlayer1.condirmed = true;
                        break;
                    case "Aula_EducacionFisica":
                        gm.structGameManager.gm_dataCombatPvP.level_selected = DataCombatPvP.Level_Selected.EducacionFisica;
                        cursorPlayer1.condirmed = true;
                        break;
                    case "Aula_Arte":
                        gm.structGameManager.gm_dataCombatPvP.level_selected = DataCombatPvP.Level_Selected.Arte;
                        cursorPlayer1.condirmed = true;
                        break;
                    case "Aula_Matematica":
                        gm.structGameManager.gm_dataCombatPvP.level_selected = DataCombatPvP.Level_Selected.Matematica;
                        cursorPlayer1.condirmed = true;
                        break;
                    case "Aula_Quimica":
                        gm.structGameManager.gm_dataCombatPvP.level_selected = DataCombatPvP.Level_Selected.Quimica;
                        cursorPlayer1.condirmed = true;
                        break;
                    case "Aula_Programacion":
                        gm.structGameManager.gm_dataCombatPvP.level_selected = DataCombatPvP.Level_Selected.Programacion;
                        cursorPlayer1.condirmed = true;
                        break;
                    case "Aula_TESIS":
                        gm.structGameManager.gm_dataCombatPvP.level_selected = DataCombatPvP.Level_Selected.TESIS;
                        cursorPlayer1.condirmed = true;
                        break;
                    case "Aula_Cafeteria":
                        gm.structGameManager.gm_dataCombatPvP.level_selected = DataCombatPvP.Level_Selected.Cafeteria;
                        cursorPlayer1.condirmed = true;
                        break;
                    default:
                        cursorPlayer1.condirmed = false;
                        break;
                }
            }
            cursorPlayer2.condirmed = true; // SACAR ESTO Y REMPLAZARLO POR LO MISMO QUE HICE CON cursorPlayer1 PERO UTILIZANDO cursorPlayer2
            if (cursorPlayer1.condirmed && cursorPlayer2.condirmed)
            {
                Debug.Log("ENTRE");
                SceneManager.LoadScene(nameNextScene);
            }
        }
        public void CheckFondo()
        {
            switch (grillaDeSeleccion[cursorPlayer1.x, cursorPlayer1.y])
            {
                case "Aula_Anatomia":
                    background.sprite = fondos[(int)BackgroundLevels.Anatomia];
                    break;
                case "Aula_Historia":
                    background.sprite = fondos[(int)BackgroundLevels.Historia];
                    break;
                case "Aula_EducacionFisica":
                    background.sprite = fondos[(int)BackgroundLevels.EducacionFisica];
                    break;
                case "Aula_Arte":
                    background.sprite = fondos[(int)BackgroundLevels.Arte];
                    break;
                case "Aula_Matematica":
                    background.sprite = fondos[(int)BackgroundLevels.Matematica];
                    break;
                case "Aula_Quimica":
                    background.sprite = fondos[(int)BackgroundLevels.Quimica];
                    break;
                case "Aula_Programacion":
                    background.sprite = fondos[(int)BackgroundLevels.Programacion];
                    break;
                case "Aula_TESIS":
                    background.sprite = fondos[(int)BackgroundLevels.TESIS];
                    break;
                case "Aula_Cafeteria":
                    background.sprite = fondos[(int)BackgroundLevels.Cafeteria];
                    break;
            }
        }
        // ESTE SCRIPT DEBE COMUNICAR AL STRUCT DEL GAME MANAGER LAS SELECCIONES DE LOS NIVELES
    }
}
