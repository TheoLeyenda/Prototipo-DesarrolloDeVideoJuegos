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
        private GameManager gm;
        public string nameNextScene;
        public List<string> nameLevelsOptions;
        public Cursor CursorSelectorPlayer1;
        public Cursor CursorSelectorPlayer2;
        private string[,] grillaDeSeleccion;
        public int filas;
        public int columnas;
        private int idOption;
        private CursorMatriz cursorPlayer1;
        private CursorMatriz cursorPlayer2;
        private bool aviableMoveHorizontal;
        private bool aviableMoveVertical;
        void Start()
        {
            if (GameManager.instanceGameManager != null)
            {
                gm = GameManager.instanceGameManager;
            }
            // SACAR ESTO LUEGO
            gm.structGameManager.gm_dataCombatPvP.level_selected = DataCombatPvP.Level_Selected.Programacion;
            SceneManager.LoadScene(nameNextScene);

        }

        void Update()
        {
            MoveCursor();
            CheckSelectCursor();
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
                SceneManager.LoadScene(nameNextScene);
            }
        }
        // ESTE SCRIPT DEBE COMUNICAR AL STRUCT DEL GAME MANAGER LAS SELECCIONES DE LOS NIVELES
    }
}
