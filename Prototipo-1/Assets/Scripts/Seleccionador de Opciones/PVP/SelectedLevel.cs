using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public List<string> nameLevelsOptions;
    public Cursor CursorSelectorPlayer1;
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
    public GameObject CamvasSeleccionRounds;
    public GameObject Fondo;
    public GameObject CuadrillaDeSeleccion;
    public GameObject PantallaAnterior;
    public bool activateCountRoundsSelection;

    public SpriteRenderer spriteCursor1;

    private GameData gd;

    private EventWise eventWise;

    private string soundMoveSelectionCharacter = "seleccion_de_personaje_op4";
    private string soundSelectCharacter = "seleccion_de_personaje_op2";
    private bool soundSelectCharacterPlayer = false;

    [SerializeField]
    private float timeTransitionScene = 0.2f;

    [System.Serializable]
    public class ElementsSelectionLevels
    {
        public string nameLevel;
        public DataCombatPvP.Level_Selected levelSelect;
    }

    public List<ElementsSelectionLevels> elementsSelectionLevels;

    [System.Serializable]
    public class BackGroundSelectionLevels
    {
        public string nameBackgroundActual;
        public BackgroundLevels backgroundActual;
    }

    public List<BackGroundSelectionLevels> backGroundSelectionLevels;
    void Start()
    {
        gd = GameData.instaceGameData;
        eventWise = GameObject.Find("EventWise").GetComponent<EventWise>();
        CamvasSeleccionRounds.SetActive(false);
        Fondo.SetActive(true);
        CuadrillaDeSeleccion.SetActive(true);
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
                int i = columnas-1;
                if (i > 0)
                {
                    while (i >= 0)
                    {
                        for (int j = 0; j < filas; j++)
                        {
                            if (idOption < nameLevelsOptions.Count)
                            {
                                grillaDeSeleccion[j, i] = nameLevelsOptions[idOption];
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
                        if (idOption < nameLevelsOptions.Count)
                        {
                            grillaDeSeleccion[j, i] = nameLevelsOptions[idOption];
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
        if (!cursorPlayer1.condirmed)
        {
            MoveCursor();
            CheckFondo();
        }
        CheckSelectCursor();
    }
    public void MoveCursor()
    {
        if (cursorPlayer1.x >= 0 && cursorPlayer1.x < filas)
        {
            if (InputPlayerController.GetInputAxis("Horizontal") > 0 && cursorPlayer1.x < filas - 1)
            {
                if (aviableMoveHorizontal)
                {
                    cursorPlayer1.x++;
                    CursorSelectorPlayer1.MoveRight();
                    aviableMoveHorizontal = false;

                    eventWise.StartEvent(soundMoveSelectionCharacter);
                }
            }
            else if (InputPlayerController.GetInputAxis("Horizontal") < 0 && cursorPlayer1.x > 0)
            {
                if (aviableMoveHorizontal)
                {
                    cursorPlayer1.x--;
                    CursorSelectorPlayer1.MoveLeft();
                    aviableMoveHorizontal = false;

                    eventWise.StartEvent(soundMoveSelectionCharacter);
                }
            }
        }
        if (cursorPlayer1.y >= 0 && cursorPlayer1.y < columnas)
        {
            if (InputPlayerController.GetInputAxis("Vertical") > 0 && cursorPlayer1.y > 0)
            {
                if (aviableMoveVertical)
                {
                    cursorPlayer1.y--;
                    CursorSelectorPlayer1.MoveUp();
                    aviableMoveVertical = false;

                    eventWise.StartEvent(soundMoveSelectionCharacter);
                }
            }
            else if (InputPlayerController.GetInputAxis("Vertical") < 0 && cursorPlayer1.y < columnas - 1)
            {
                if (aviableMoveVertical)
                {
                    cursorPlayer1.y++;
                    CursorSelectorPlayer1.MoveDown();
                    aviableMoveVertical = false;

                    eventWise.StartEvent(soundMoveSelectionCharacter);
                }
            }
        }
        if (InputPlayerController.GetInputAxis("Vertical") == 0)
        {
            aviableMoveVertical = true;
        }
        if (InputPlayerController.GetInputAxis("Horizontal") == 0)
        {
            aviableMoveHorizontal = true;
        }
    }
    public void CheckSelectCursor()
    {
        if (InputPlayerController.GetInputButtonDown("SelectButton_P1"))
        {
            for (int i = 0; i < elementsSelectionLevels.Count; i++)
            {
                if (grillaDeSeleccion[cursorPlayer1.x, cursorPlayer1.y] == elementsSelectionLevels[i].nameLevel)
                {
                    gm.structGameManager.gm_dataCombatPvP.level_selected = elementsSelectionLevels[i].levelSelect;
                    cursorPlayer1.condirmed = true;
                    spriteCursor1.color = Color.yellow;
                    if (!soundSelectCharacterPlayer)
                    {
                        eventWise.StartEvent(soundSelectCharacter);

                        soundSelectCharacterPlayer = true;
                    }
                }
            }
        }
        cursorPlayer2.condirmed = true;
        
        if (cursorPlayer1.condirmed && cursorPlayer2.condirmed)
        {
            if (timeTransitionScene > 0)
            {
                timeTransitionScene = timeTransitionScene - Time.deltaTime;
            }
            else
            {
                if (activateCountRoundsSelection)
                {
                    CamvasSeleccionRounds.SetActive(true);
                    Fondo.SetActive(true);
                    CuadrillaDeSeleccion.SetActive(true);
                    if (PantallaAnterior != null)
                    {
                        PantallaAnterior.SetActive(false);
                    }
                }
                else
                {
                    NextScene();
                }
            }
        }
    }
    public void NextScene()
    {
        if (gm.structGameManager.gm_dataCombatPvP.modoElegido == StructGameManager.ModoPvPElegido.PvP)
        {
            SceneManager.LoadScene("PvP");
        }
        else if(gm.structGameManager.gm_dataCombatPvP.modoElegido == StructGameManager.ModoPvPElegido.TiroAlBlanco)
        {
            SceneManager.LoadScene("TiroAlBlanco");
        }
    }
    public void CheckFondo()
    {
        for (int i = 0; i < backGroundSelectionLevels.Count; i++)
        {
            if (grillaDeSeleccion[cursorPlayer1.x, cursorPlayer1.y] == backGroundSelectionLevels[i].nameBackgroundActual)
            {
                background.sprite = fondos[(int)backGroundSelectionLevels[i].backgroundActual];
            }
        }
    }
}
