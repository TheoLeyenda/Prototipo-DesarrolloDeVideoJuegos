using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SelectedPlayers : MonoBehaviour
{
    // ESTE SCRIPT DEBE COMUNICAR AL STRUCT DEL GAME MANAGER LAS SELECCIONES DE LOS JUGADORES (tanto player1 como player2)
    public struct CursorMatriz
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

    [System.Serializable]
    public class ElementsCharacter
    {
        public string nameCharacter;
        public Characters characterSelected;
        public DataCombatPvP.Player_Selected player_Selected;
    }

    public List<ElementsCharacter> elementsCharacters;

    //-----------------------------//
    public TextMeshProUGUI namePlayer1;
    public TextMeshProUGUI namePlayer2;
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
    //public List<Vector3> positionsNameCharacters;
    //private Vector3[,] positionNameCharacter;
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
            //positionNameCharacter = new Vector3[filas, columnas];
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
                                //positionNameCharacter[j, i] = positionsNameCharacters[idOption];
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
                            //positionNameCharacter[j, i] = positionsNameCharacters[idOption];
                        }
                        idOption++;
                    }
                }
            }
        }
        idOption = 0;
        CheckNamePlayersSelect();
    }
    private void Update()
    {
        MoveCursor("Horizontal", "Vertical", ref aviableMoveHorizontalP1,ref aviableMoveVerticalP1,ref cursorPlayer1,ref CursorSelectorPlayer1);
        MoveCursor("Horizontal_P2", "Vertical_P2", ref aviableMoveHorizontalP2, ref aviableMoveVerticalP2,ref cursorPlayer2,ref CursorSelectorPlayer2);
        CheckSelectCursor("SelectButton_P1", ref cursorPlayer1, ref gm.structGameManager.gm_dataCombatPvP.player1_selected);
        CheckSelectCursor("SelectButton_P2", ref cursorPlayer2, ref gm.structGameManager.gm_dataCombatPvP.player2_selected);
        DecoratePlayerSelected(imagePlayer1, cursorPlayer1);
        DecoratePlayerSelected(imagePlayer2, cursorPlayer2);
        CheckPositionCursor();
        CheckCursorSelected();
    }
    public void CheckNamePlayersSelect()
    {
        namePlayer1.text = grillaDeSeleccion[cursorPlayer1.x, cursorPlayer1.y];
        namePlayer2.text = grillaDeSeleccion[cursorPlayer2.x, cursorPlayer2.y];
        //namePlayer1.rectTransform.localPosition = positionNameCharacter[cursorPlayer1.x, cursorPlayer1.y];
        //namePlayer2.rectTransform.localPosition = positionNameCharacter[cursorPlayer2.x, cursorPlayer2.y];
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
    public void DecoratePlayerSelected(SpriteRenderer imagePlayer, CursorMatriz cursorPlayer)
    {
        for (int i = 0; i < elementsCharacters.Count; i++)
        {
            if (grillaDeSeleccion[cursorPlayer.x, cursorPlayer.y] == elementsCharacters[i].nameCharacter)
            {
                imagePlayer.sprite = spritesPlayers[(int)elementsCharacters[i].characterSelected];
            }
        }
    }
    public void MoveCursor(string inputHorizontal, string inputVertical, ref bool aviableMoveHorizontal, ref bool aviableMoveVertical, ref CursorMatriz cursorPlayer, ref Cursor CursorSelectorPlayer)
    {
            
        if (!cursorPlayer.condirmed)
        {
            if (cursorPlayer.x >= 0 && cursorPlayer.x < filas)
            {
                if (InputPlayerController.GetInputAxis(inputHorizontal) > 0 && cursorPlayer.x < filas - 1)
                {
                    if (aviableMoveHorizontal)
                    {
                        cursorPlayer.x++;
                        CursorSelectorPlayer.MoveRight();
                        aviableMoveHorizontal = false;
                        CheckNamePlayersSelect();
                    }
                }
                else if (InputPlayerController.GetInputAxis(inputHorizontal) < 0 && cursorPlayer.x > 0)
                {
                    if (aviableMoveHorizontal)
                    {
                        cursorPlayer.x--;
                        CursorSelectorPlayer.MoveLeft();
                        aviableMoveHorizontal = false;
                        CheckNamePlayersSelect();
                    }
                }
            }
            if (cursorPlayer.y >= 0 && cursorPlayer.y < columnas)
            {
                if (InputPlayerController.GetInputAxis(inputVertical) > 0 && cursorPlayer.y > 0)
                {
                    if (aviableMoveVertical)
                    {
                        cursorPlayer.y--;
                        CursorSelectorPlayer.MoveUp();
                        aviableMoveVertical = false;
                        CheckNamePlayersSelect();
                    }
                }
                else if (InputPlayerController.GetInputAxis(inputVertical) < 0 && cursorPlayer.y < columnas - 1)
                {
                    if (aviableMoveVertical)
                    {
                        cursorPlayer.y++;
                        CursorSelectorPlayer.MoveDown();
                        aviableMoveVertical = false;
                        CheckNamePlayersSelect();
                    }
                }
            }
        }
        if (InputPlayerController.GetInputAxis(inputVertical) == 0)
        {
            aviableMoveVertical = true;
        }
        if (InputPlayerController.GetInputAxis(inputHorizontal) == 0)
        {
            aviableMoveHorizontal = true;
        }
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
    public void CheckSelectCursor(string inputSelectButton, ref CursorMatriz cursorPlayer,ref DataCombatPvP.Player_Selected player_Selected)
    {
        if (InputPlayerController.GetInputButtonDown(inputSelectButton))
        {
            for (int i = 0; i < elementsCharacters.Count; i++)
            {
                if (grillaDeSeleccion[cursorPlayer.x, cursorPlayer.y] == elementsCharacters[i].nameCharacter)
                {
                    player_Selected = elementsCharacters[i].player_Selected;
                    cursorPlayer.condirmed = true;
                }
            }
        }
        if (cursorPlayer1.condirmed && cursorPlayer2.condirmed)
        {
            SceneManager.LoadScene(nameNextScene);
        }
    }
}