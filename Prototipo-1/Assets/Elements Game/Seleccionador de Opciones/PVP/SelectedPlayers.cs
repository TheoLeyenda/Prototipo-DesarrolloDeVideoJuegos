using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace Prototipo_2
{
    public class SelectedPlayers : MonoBehaviour
    {
        // ESTE SCRIPT DEBE COMUNICAR AL STRUCT DEL GAME MANAGER LAS SELECCIONES DE LOS JUGADORES (tanto player1 como player2)
        struct CursorMatriz
        {
            public int x;
            public int y;
            public bool condirmed;
        }
        //ENUM PARA EL CHIMI DECORATIVO//
        public enum Characters
        {
            Balanceado,
            Agresivo,
            Defensivo,
            Protagonista,
            Count,
        }
        //-----------------------------//
        public string nameNextScene;
        public List<string> namePlayersOptions;
        public Cursor CursorSelectorPlayer1;
        public Cursor CursorSelectorPlayer2;
        public GameObject CursorGrandePlayer1;
        public GameObject CursorChicoPlayer1;
        private string[,] grillaDeSeleccion;
        public int filas;
        public int columnas;
        private int idOption;
        private CursorMatriz cursorPlayer1;
        private CursorMatriz cursorPlayer2;
        private GameManager gm;
        private bool aviableMoveHorizontalP1;
        private bool aviableMoveVerticalP1;
        private bool aviableMoveHorizontalP2;
        private bool aviableMoveVerticalP2;

        //VARIABLES PARA EL CHIMI DECORATIVO//
        public SpriteRenderer imagePlayer1;
        public GameObject vs;
        public SpriteRenderer imagePlayer2;
        public List<Sprite> spritesPlayers;
        public GameObject DireccionLeft;
        public GameObject DireccionRight;
        public SpriteRenderer spriteCursor1;
        public SpriteRenderer spriteCursor2;
        //----------------------------------//
        private void Start()
        {
            aviableMoveHorizontalP1 = true;
            aviableMoveVerticalP1 = true;
            if (GameManager.instanceGameManager != null)
            {
                gm = GameManager.instanceGameManager;
            }
            idOption = 0;

            cursorPlayer1.x = 1;
            cursorPlayer1.y = columnas - 1;

            cursorPlayer2.x = 2;
            cursorPlayer2.y = columnas - 1;
            if (filas > 0 && columnas > 0)
            {
                grillaDeSeleccion = new string[filas, columnas];
                if (grillaDeSeleccion != null)
                {
                    int i = columnas - 1;
                    if (i > 0)
                    {
                        while (i > 0)
                        {
                            for (int j = 0; j < filas; j++)
                            {
                                if (idOption < namePlayersOptions.Count)
                                {
                                    grillaDeSeleccion[j, i] = namePlayersOptions[idOption];
                                }
                                idOption++;
                            }
                            i--;
                        }
                    }
                    else
                    {
                        for (int j = 0; j < filas; j++)
                        {
                            if (idOption < namePlayersOptions.Count)
                            {
                                grillaDeSeleccion[j, i] = namePlayersOptions[idOption];
                            }
                            idOption++;
                        }
                    }
                }
            }
            idOption = 0;
        }
        private void Update()
        {
            MoveCursor();
            CheckSelectCursor();
            DecoratePlayerSelected();
            CheckPositionCursor();
            CheckCursorSelected();
        }
        public void CheckPositionCursor()
        {
            if (cursorPlayer1.x == cursorPlayer2.x && cursorPlayer2.y == cursorPlayer1.y)
            {
                CursorGrandePlayer1.SetActive(true);
                CursorChicoPlayer1.SetActive(false);
            }
            else
            {
                CursorChicoPlayer1.SetActive(true);
                CursorGrandePlayer1.SetActive(false);
            }
        }
        public void DecoratePlayerSelected()
        {
            switch (grillaDeSeleccion[cursorPlayer1.x, cursorPlayer1.y])
            {
                case "Balanceado":
                    imagePlayer1.sprite = spritesPlayers[(int)Characters.Balanceado];
                    break;
                case "Agresivo":
                    imagePlayer1.sprite = spritesPlayers[(int)Characters.Agresivo];
                    break;
                case "Defensivo":
                    imagePlayer1.sprite = spritesPlayers[(int)Characters.Defensivo];
                    break;
                case "Protagonista":
                    imagePlayer1.sprite = spritesPlayers[(int)Characters.Protagonista];
                    break;
            }
            switch (grillaDeSeleccion[cursorPlayer2.x, cursorPlayer2.y])
            {
                case "Balanceado":
                    imagePlayer2.sprite = spritesPlayers[(int)Characters.Balanceado];
                    break;
                case "Agresivo":
                    imagePlayer2.sprite = spritesPlayers[(int)Characters.Agresivo];
                    break;
                case "Defensivo":
                    imagePlayer2.sprite = spritesPlayers[(int)Characters.Defensivo];
                    break;
                case "Protagonista":
                    imagePlayer2.sprite = spritesPlayers[(int)Characters.Protagonista];
                    break;
            }
        }
        public void MoveCursor()
        {
            if (!cursorPlayer1.condirmed)
            {
                //---MOVIMIENTO DEL CURSOR DEL PLAYER 1---//
                if (cursorPlayer1.x >= 0 && cursorPlayer1.x < filas)
                {
                    if (InputPlayerController.GetInputAxis("Horizontal") > 0 && cursorPlayer1.x < filas - 1)
                    {
                        if (aviableMoveHorizontalP1)
                        {
                            cursorPlayer1.x++;
                            CursorSelectorPlayer1.MoveRight();
                            aviableMoveHorizontalP1 = false;
                        }
                    }
                    else if (InputPlayerController.GetInputAxis("Horizontal") < 0 && cursorPlayer1.x > 0)
                    {
                        if (aviableMoveHorizontalP1)
                        {
                            cursorPlayer1.x--;
                            CursorSelectorPlayer1.MoveLeft();
                            aviableMoveHorizontalP1 = false;
                        }
                    }
                }
                if (cursorPlayer1.y >= 0 && cursorPlayer1.y < columnas)
                {
                    if (InputPlayerController.GetInputAxis("Vertical") > 0 && cursorPlayer1.y > 0)
                    {
                        if (aviableMoveVerticalP1)
                        {
                            cursorPlayer1.y--;
                            CursorSelectorPlayer1.MoveUp();
                            aviableMoveVerticalP1 = false;
                        }
                    }
                    else if (InputPlayerController.GetInputAxis("Vertical") < 0 && cursorPlayer1.y < columnas - 1)
                    {
                        if (aviableMoveVerticalP1)
                        {
                            cursorPlayer1.y++;
                            CursorSelectorPlayer1.MoveDown();
                            aviableMoveVerticalP1 = false;
                        }
                    }
                }
            }
            if (InputPlayerController.GetInputAxis("Vertical") == 0)
            {
                aviableMoveVerticalP1 = true;
            }
            if (InputPlayerController.GetInputAxis("Horizontal") == 0)
            {
                aviableMoveHorizontalP1 = true;
            }
            //----------------------------------------//

            //---MOVIMIENTO DEL CURSOR DEL PLAYER 2---//
            if (!cursorPlayer2.condirmed)
            {
                if (cursorPlayer2.x >= 0 && cursorPlayer2.x < filas)
                {
                    if (InputPlayerController.GetInputAxis("Horizontal_P2") > 0 && cursorPlayer2.x < filas - 1)
                    {
                        if (aviableMoveHorizontalP2)
                        {
                            cursorPlayer2.x++;
                            CursorSelectorPlayer2.MoveRight();
                            aviableMoveHorizontalP2 = false;
                        }
                    }
                    else if (InputPlayerController.GetInputAxis("Horizontal_P2") < 0 && cursorPlayer2.x > 0)
                    {
                        if (aviableMoveHorizontalP2)
                        {
                            cursorPlayer2.x--;
                            CursorSelectorPlayer2.MoveLeft();
                            aviableMoveHorizontalP2 = false;
                        }
                    }
                }
                if (cursorPlayer2.y >= 0 && cursorPlayer2.y < columnas)
                {
                    if (InputPlayerController.GetInputAxis("Vertical_P2") > 0 && cursorPlayer2.y > 0)
                    {
                        if (aviableMoveVerticalP2)
                        {
                            cursorPlayer2.y--;
                            CursorSelectorPlayer2.MoveUp();
                            aviableMoveVerticalP2 = false;
                        }
                    }
                    else if (InputPlayerController.GetInputAxis("Vertical_P2") < 0 && cursorPlayer2.y < columnas - 1)
                    {
                        if (aviableMoveVerticalP2)
                        {
                            cursorPlayer2.y++;
                            CursorSelectorPlayer2.MoveDown();
                            aviableMoveVerticalP2 = false;
                        }
                    }
                }
            }
            if (InputPlayerController.GetInputAxis("Vertical_P2") == 0)
            {
                aviableMoveVerticalP2 = true;
            }
            if (InputPlayerController.GetInputAxis("Horizontal_P2") == 0)
            {
                aviableMoveHorizontalP2 = true;
            }
            //----------------------------------------//
        }
        public void CheckCursorSelected()
        {
            if (cursorPlayer1.condirmed)
            {
                spriteCursor1.color = Color.yellow;
                CursorGrandePlayer1.GetComponent<SpriteRenderer>().color = Color.yellow;
            }
            if (cursorPlayer2.condirmed)
            {
                spriteCursor2.color = Color.yellow;
            }
        }
        public void CheckSelectCursor()
        {
            if (InputPlayerController.GetInputButtonDown("SelectButton_P1"))
            {
                switch (grillaDeSeleccion[cursorPlayer1.x, cursorPlayer1.y])
                {
                    case "Balanceado":
                        gm.structGameManager.gm_dataCombatPvP.player1_selected = DataCombatPvP.Player_Selected.Balanceado;
                        cursorPlayer1.condirmed = true;
                        break;
                    case "Agresivo":
                        gm.structGameManager.gm_dataCombatPvP.player1_selected = DataCombatPvP.Player_Selected.Agresivo;
                        cursorPlayer1.condirmed = true;
                        break;
                    case "Defensivo":
                        gm.structGameManager.gm_dataCombatPvP.player1_selected = DataCombatPvP.Player_Selected.Defensivo;
                        cursorPlayer1.condirmed = true;
                        break;
                    case "Protagonista":
                        gm.structGameManager.gm_dataCombatPvP.player1_selected = DataCombatPvP.Player_Selected.Protagonista;
                        cursorPlayer1.condirmed = true;
                        break;
                    default:
                        cursorPlayer1.condirmed = false;
                        break;
                }
            }
            if (InputPlayerController.GetInputButtonDown("SelectButton_P2"))
            {
                switch (grillaDeSeleccion[cursorPlayer2.x, cursorPlayer2.y])
                {
                    case "Balanceado":
                        gm.structGameManager.gm_dataCombatPvP.player2_selected = DataCombatPvP.Player_Selected.Balanceado;
                        cursorPlayer2.condirmed = true;
                        break;
                    case "Agresivo":
                        gm.structGameManager.gm_dataCombatPvP.player2_selected = DataCombatPvP.Player_Selected.Agresivo;
                        cursorPlayer2.condirmed = true;
                        break;
                    case "Defensivo":
                        gm.structGameManager.gm_dataCombatPvP.player2_selected = DataCombatPvP.Player_Selected.Defensivo;
                        cursorPlayer2.condirmed = true;
                        break;
                    case "Protagonista":
                        gm.structGameManager.gm_dataCombatPvP.player2_selected = DataCombatPvP.Player_Selected.Protagonista;
                        cursorPlayer2.condirmed = true;
                        break;
                    default:
                        cursorPlayer2.condirmed = false;
                        break;
                }
            }

            if (cursorPlayer1.condirmed && cursorPlayer2.condirmed)
            {
                SceneManager.LoadScene(nameNextScene);
            }
        }
    }
}