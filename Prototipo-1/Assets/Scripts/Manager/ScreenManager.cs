using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    private GameObject fondo;
    private SpriteRenderer spriteRendererFondo;
    private GameManager gm;
    public List<Sprite> ListaNiveles;
    private int idListaNiveles;
    private GameData gameData;
    [SerializeField] private Transitions panelTransitions;
    [SerializeField] private bool useLoadSceneInMenu = true;
    [SerializeField] private bool registerLevel = true;
    [HideInInspector] public EventWise eventWise;

    private void Start()
    {
        idListaNiveles = -1;
        gm = GameManager.instanceGameManager;
        gameData = GameData.instaceGameData;
        if (registerLevel)
        {
            LevelLoader.prevLevel = SceneManager.GetActiveScene().name;
        }
    }
    public void Multijugador()
    {
        LevelLoader.nextLevel = "Multijugador";
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
                gameData.gd = GameData.GameMode.PvP;
                LevelLoader.nextLevel = "SelectPlayerScene";
                SceneManager.LoadScene("SelectPlayerScene");
            }
        }
    }
    public void LoadScene(string name)
    {
        LevelLoader.nextLevel = name;
        if (panelTransitions != null)
            panelTransitions.LoadScene(name);
        else
            SceneManager.LoadScene(name);
    }
    public void Controles()
    {
        LevelLoader.nextLevel = "Controles";
        SceneManager.LoadScene("Controles");
    }
    public void Creditos()
    {
        eventWise.StartEvent("creditos");
        LevelLoader.nextLevel = "Creditos";
        SceneManager.LoadScene("Creditos");
    }
    public void HighScore()
    {
        gm.CanvasGameOver.SetActive(false);
        if (!gm.CanvasGameOver.activeSelf)
        {
            LevelLoader.nextLevel = "HighScore";
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
                gameData.gd = GameData.GameMode.History;
                LevelLoader.nextLevel = "Nivel " + numerLevel;
                SceneManager.LoadScene("LoadScene");
            }
        }
    }
    public void SetGameMode(int _gd) 
    {
        switch (_gd)
        {
            case 1:
                gameData.gd = GameData.GameMode.History;
                break;
            case 2:
                gameData.gd = GameData.GameMode.Survival;
                break;
        }
    }
    public void CheckLoadLevel() 
    {
        switch (gameData.gd) 
        {
            case GameData.GameMode.History:
                LoadLevel();
                break;
            case GameData.GameMode.Survival:
                Supervivencia();
                break;
            case GameData.GameMode.PvP:
                break;
        }
    }
    public void LoadLevel()
    {
        if (gm != null)
        {
            gm.totalCountEnemysDead = gm.totalCountEnemysDead + gm.auxCountEnemysDead;
            gm.countEnemysDead = gm.auxCountEnemysDead;
            gm.auxCountEnemysDead = 0;
            gm.enumsGameManager.modoDeJuego = EnumsGameManager.ModosDeJuego.Historia;
            if (gm.enumsGameManager.modoDeJuego == EnumsGameManager.ModosDeJuego.Historia)
            {
                gameData.gd = GameData.GameMode.History;
                LevelLoader.nextLevel = "Nivel " + (int)(gameData.currentLevel + 1);
                if (panelTransitions != null)
                    panelTransitions.LoadScene("LoadScene");
                else
                SceneManager.LoadScene("LoadScene");
            }
        }
    }
    public void SelectPlayerScene()
    {
        eventWise.StartEvent("despausar");
        eventWise.StartEvent("fuego_termina");
        eventWise.StartEvent("volver_al_menu");
        LevelLoader.nextLevel = "SelectPlayerScene";
        SceneManager.LoadScene("SelectPlayerScene");
    }
    public void Supervivencia()
    {
        gm.enumsGameManager.modoDeJuego = EnumsGameManager.ModosDeJuego.Supervivencia;
        if (gm.enumsGameManager.modoDeJuego == EnumsGameManager.ModosDeJuego.Supervivencia)
        {
            gameData.gd = GameData.GameMode.Survival;
            LevelLoader.nextLevel = "Supervivencia";
            if (panelTransitions != null)
                panelTransitions.LoadScene("LoadScene");
            else
                SceneManager.LoadScene("LoadScene");
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
        if (ListaNiveles.Count <= 0) return;
        if (fondo != null) return;

        if (idListaNiveles < ListaNiveles.Count)
        {
            if ((gm != null && SceneManager.GetActiveScene().name != "MENU" && SceneManager.GetActiveScene().name != "Supervivencia"
                && SceneManager.GetActiveScene().name != "SampleScene" && SceneManager.GetActiveScene().name != "GameOver"
                && SceneManager.GetActiveScene().name != "SelectLevel" && SceneManager.GetActiveScene().name != "SelectPlayerScene"
                && SceneManager.GetActiveScene().name != "PvP" && SceneManager.GetActiveScene().name != "SelectedPowerUp"))
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
                gameData.gd = GameData.GameMode.PvP;
                LevelLoader.nextLevel = "SelectPlayerScene";
                SceneManager.LoadScene("SelectPlayerScene");
            }
        }
    }
    public void Menu()
    {
        eventWise.StartEvent("despausar");
        eventWise.StartEvent("fuego_termina");
        eventWise.StartEvent("volver_al_menu");
        
        gm.enumsGameManager.modoDeJuego = EnumsGameManager.ModosDeJuego.Nulo;
        idListaNiveles = -1;
        Time.timeScale = 1;
        if (idListaNiveles == -1 && gm.enumsGameManager.modoDeJuego == EnumsGameManager.ModosDeJuego.Nulo && Time.timeScale == 1)
        {
            gm.CanvasGameOver.SetActive(false);
            if (!gm.CanvasGameOver.activeSelf)
            {
                gameData.gd = GameData.GameMode.None;
                LevelLoader.nextLevel = "MENU";
                if (useLoadSceneInMenu && panelTransitions == null)
                    SceneManager.LoadScene("MENU");
                else if (panelTransitions != null)
                    panelTransitions.LoadScene("MENU"); 
            }
        }
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
        if (eventWise == null)
        {
            if (GameObject.Find("EventWise") != null)
            {
                eventWise = GameObject.Find("EventWise").GetComponent<EventWise>();
            }
        }
    }
    public int GetIdListLevel()
    {
        return idListaNiveles + 1;
    }
    public int GetCurrentIdListLevel()
    {
        return idListaNiveles;
    }
    public void SetIdListLevel(int id)
    {
        idListaNiveles = id;
    }
}