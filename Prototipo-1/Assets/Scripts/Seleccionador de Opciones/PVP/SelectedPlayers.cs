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

        public EnumsPlayers.NumberPlayer numberPlayer;
        public int index;
    }
    //ENUM PARA EL CHIMI DECORATIVO//
    public enum Characters
    {
        Balanceado,
        Agresivo,
        Defensivo,
        Protagonista,
        Famosa,
        Tomboy,
        Gotica,
        RandomPlayer,
        Count,
        Nulo,
    }

    [System.Serializable]
    public class ElementsCharacter
    {
        public string nameCharacter;
        public Characters characterSelected;
        public DataCombatPvP.Player_Selected player_Selected;
        public Vector3 position;
        public int x , y;
    }

    public List<ElementsCharacter> elementsCharacters;

    //-----------------------------//
    public bool enableRandomCharacter;
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
    public string nameRandomPlayer = "Random Player";

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

    // HACER QUE SE PUEDAN ELEGIR LOS TRES ENEMIGOS NUEVOS Y PONER UNA CASILLA MAS QUE PERMITA ELEGIR A UN JUGADOR DE FORMA ALEATORIA.
    private void Start()
    {
        aviableMoveHorizontalP1 = true;
        aviableMoveVerticalP1 = true;
        elementsCharacters[elementsCharacters.Count - 1].nameCharacter = nameRandomPlayer;
        if (GameManager.instanceGameManager != null)
        {
            gm = GameManager.instanceGameManager;
        }
        idOption = 0;

        cursorPlayer1.x = 1;
        cursorPlayer1.y = columnas - 1;
        cursorPlayer1.numberPlayer = EnumsPlayers.NumberPlayer.player1;

        cursorPlayer2.x = 2;
        cursorPlayer2.y = columnas - 1;
        cursorPlayer2.numberPlayer = EnumsPlayers.NumberPlayer.player2;

        if (filas > 0 && columnas > 0)
        {
            grillaDeSeleccion = new string[filas, columnas];
            //positionNameCharacter = new Vector3[filas, columnas];
            if (grillaDeSeleccion != null)
            {
                int i = columnas - 1;
                if (i > 0)
                {
                    i = 0;
                    while (i < columnas)
                    {
                        for (int j = 0; j < filas; j++)
                        {
                            if (idOption < namePlayersOptions.Count)
                            {
                                grillaDeSeleccion[j, i] = namePlayersOptions[idOption];
                                elementsCharacters[idOption].x = j;
                                elementsCharacters[idOption].y = i;
                                //positionNameCharacter[j, i] = positionsNameCharacters[idOption];
                                //Debug.Log("[" + j + "," + i + "]: " + grillaDeSeleccion[j, i]);
                            }
                            idOption++;
                        }
                        i++;
                    }
                }
                else
                {
                    for (int j = 0; j < filas; j++)
                    {
                        if (idOption < namePlayersOptions.Count)
                        {
                            grillaDeSeleccion[j, i] = namePlayersOptions[idOption];
                            elementsCharacters[idOption].x = j;
                            elementsCharacters[idOption].y = i;
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
        CheckSelectCursor("SelectButton_P1", ref cursorPlayer1, ref gm.structGameManager.gm_dataCombatPvP.player1_selected,ref CursorSelectorPlayer1,ref imagePlayer1);
        CheckSelectCursor("SelectButton_P2", ref cursorPlayer2, ref gm.structGameManager.gm_dataCombatPvP.player2_selected,ref CursorSelectorPlayer2, ref imagePlayer2);
        DecoratePlayerSelected(imagePlayer1,ref cursorPlayer1);
        DecoratePlayerSelected(imagePlayer2,ref cursorPlayer2);
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
    public void DecoratePlayerSelected(SpriteRenderer imagePlayer,ref CursorMatriz cursorPlayer)
    {
        for (int i = 0; i < elementsCharacters.Count; i++)
        {
            if (grillaDeSeleccion[cursorPlayer.x, cursorPlayer.y] == elementsCharacters[i].nameCharacter)
            {
                if (cursorPlayer.numberPlayer == EnumsPlayers.NumberPlayer.player2)
                {
                    if (elementsCharacters[i].nameCharacter == nameRandomPlayer)
                    {
                        imagePlayer.transform.eulerAngles = new Vector3(1.374f, 0, 11.159f);
                    }
                    else
                    {
                        imagePlayer.transform.eulerAngles = new Vector3(1.374f, 180, -11.054f);
                    }
                }
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
    public void CheckSelectCursor(string inputSelectButton, ref CursorMatriz cursorPlayer,ref DataCombatPvP.Player_Selected player_Selected, ref Cursor cursor, ref SpriteRenderer imagePlayer)
    {
        if (InputPlayerController.GetInputButtonDown(inputSelectButton))
        {
            for (int i = 0; i < elementsCharacters.Count; i++)
            {
                if (grillaDeSeleccion[cursorPlayer.x, cursorPlayer.y] != nameRandomPlayer)
                {
                    if (grillaDeSeleccion[cursorPlayer.x, cursorPlayer.y] == elementsCharacters[i].nameCharacter)
                    {
                        player_Selected = elementsCharacters[i].player_Selected;
                        cursorPlayer.condirmed = true;
                    }
                }
                else
                {
                    SelectRandomPlayer(ref cursor, ref player_Selected,ref imagePlayer,ref cursorPlayer);
                    cursorPlayer.condirmed = true;
                }
            }
        }
        if (cursorPlayer1.condirmed && cursorPlayer2.condirmed)
        {
            SceneManager.LoadScene(nameNextScene);
        }
    }
    public void SelectRandomPlayer(ref Cursor cursorPlayer, ref DataCombatPvP.Player_Selected player_Selected, ref SpriteRenderer imagePlayer, ref CursorMatriz cursor)
    {
        int index;
        do
        {
            index = Random.Range(0, namePlayersOptions.Count);
        } while (namePlayersOptions[index] == nameRandomPlayer);
        cursor.index = index;

        player_Selected = elementsCharacters[index].player_Selected;
        cursor.x = elementsCharacters[index].x;
        cursor.y = elementsCharacters[index].y;

        DecoratePlayerSelected(imagePlayer,ref cursor);
        cursorPlayer.transform.localPosition = elementsCharacters[index].position;
    }
}